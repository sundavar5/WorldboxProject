using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061B RID: 1563
public class WorldTip : MonoBehaviour
{
	// Token: 0x0600333E RID: 13118 RVA: 0x0018254A File Offset: 0x0018074A
	private void Awake()
	{
		this.status = TipStatus.Hidden;
		this.canvasGroup.alpha = 0f;
		WorldTip.instance = this;
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x0018256C File Offset: 0x0018076C
	public void show(string pText, bool pTranslate = true, string pPosition = "center", float pTime = 3f, string pColor = "#F3961F")
	{
		if (WorldTip.instance == null)
		{
			return;
		}
		WorldTip.instance.text.color = Toolbox.makeColor(pColor);
		if (pTranslate)
		{
			WorldTip.instance.text.text = LocalizedTextManager.getText(pText, null, false);
			if (WorldTip.replacementDict != null)
			{
				WorldTip.instance.text.text = WorldTip.replaceWords(WorldTip.instance.text.text);
			}
			WorldTip.instance.text.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		}
		else
		{
			WorldTip.instance.text.text = pText;
		}
		this.updateTextWidth();
		base.transform.SetParent(this.transform_main);
		WorldTip.instance.startShow(pTime);
		if (pPosition == "center")
		{
			base.transform.position = Vector3.zero;
			return;
		}
		if (pPosition == "top")
		{
			base.transform.position = this.transform_positionTop.position;
			return;
		}
		WorldTip.instance.transform.position = Input.mousePosition;
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x00182681 File Offset: 0x00180881
	public static void showNowCenter(string pText)
	{
		WorldTip.showNow(pText, true, "center", 3f, "#F3961F");
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x00182699 File Offset: 0x00180899
	public static void showNowTop(string pText, bool pTranslate = true)
	{
		WorldTip.showNow(pText, pTranslate, "top", 3f, "#F3961F");
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x001826B1 File Offset: 0x001808B1
	public static void addWordReplacement(string key, string value)
	{
		if (WorldTip.replacementDict == null)
		{
			WorldTip.replacementDict = new Dictionary<string, string>();
		}
		WorldTip.replacementDict[key] = value;
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x001826D0 File Offset: 0x001808D0
	public static string replaceWords(string text)
	{
		foreach (string key in WorldTip.replacementDict.Keys)
		{
			text = text.Replace(key, WorldTip.replacementDict[key]);
		}
		WorldTip.replacementDict = null;
		return text;
	}

	// Token: 0x06003344 RID: 13124 RVA: 0x0018273C File Offset: 0x0018093C
	public static void showNow(string pText, bool pTranslate = true, string pPosition = "center", float pTime = 3f, string pColor = "#F3961F")
	{
		if (WorldTip.instance == null)
		{
			return;
		}
		WorldTip.instance.show(pText, pTranslate, pPosition, pTime, pColor);
	}

	// Token: 0x06003345 RID: 13125 RVA: 0x0018275C File Offset: 0x0018095C
	public void showToolbarText(string pText)
	{
		this.text.text = pText;
		LocalizedText tText;
		if (this.text.TryGetComponent<LocalizedText>(out tText))
		{
			tText.checkSpecialLanguages(null);
		}
		this.updateTextWidth();
		this.startShow(3f);
		base.transform.position = this.transform_toolbar.position;
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x001827B4 File Offset: 0x001809B4
	public void showToolbarText(GodPower pPower, bool pShowOnComputer = true)
	{
		if (!pShowOnComputer && Config.isComputer)
		{
			return;
		}
		string tTitle;
		string tDescription;
		if (pPower.type == PowerActionType.PowerSpawnActor)
		{
			ActorAsset actorAsset = pPower.getActorAsset();
			tTitle = actorAsset.getLocalizedName();
			tDescription = actorAsset.getLocalizedDescription();
		}
		else
		{
			tTitle = LocalizedTextManager.getText(pPower.getLocaleID(), null, false);
			tDescription = LocalizedTextManager.getText(pPower.getDescriptionID(), null, false);
		}
		this.showToolbarText(tTitle, tDescription, pShowOnComputer);
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x00182810 File Offset: 0x00180A10
	public void showToolbarText(string pTextMain, string pTextDescription, bool pShowOnComputer = true)
	{
		if (!pShowOnComputer && Config.isComputer)
		{
			return;
		}
		this.text.text = pTextMain + "\n" + pTextDescription;
		LocalizedText tText;
		if (this.text.TryGetComponent<LocalizedText>(out tText))
		{
			tText.checkSpecialLanguages(null);
		}
		this.updateTextWidth();
		this.startShow(3f);
		base.transform.position = this.transform_toolbar.position;
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x0018287C File Offset: 0x00180A7C
	public void setText(string pText, bool pAddSKip = false)
	{
		this.text.text = LocalizedTextManager.getText(pText, null, false);
		this.text.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		if (pAddSKip)
		{
			this.text.text = "\n" + this.text.text;
		}
		this.updateTextWidth();
		base.transform.position = this.transform_toolbar.position;
		this.startShow(3f);
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x001828F7 File Offset: 0x00180AF7
	private void updateTextWidth()
	{
	}

	// Token: 0x0600334A RID: 13130 RVA: 0x001828F9 File Offset: 0x00180AF9
	private void startShow(float pTime = 3f)
	{
		this.status = TipStatus.Shown;
		this.timeout = pTime;
		this.scale = 1.5f;
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x00182914 File Offset: 0x00180B14
	public static void hideNow()
	{
		if (WorldTip.instance == null)
		{
			return;
		}
		if (!WorldTip.instance.gameObject.activeSelf)
		{
			return;
		}
		WorldTip.instance.startHide();
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x00182940 File Offset: 0x00180B40
	internal void startHide()
	{
		this.status = TipStatus.Hidden;
		this.timeout = 0f;
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x00182954 File Offset: 0x00180B54
	private void Update()
	{
		if (this.scale > 1f)
		{
			this.scale -= Time.deltaTime * 3f;
			if (this.scale < 1f)
			{
				this.scale = 1f;
			}
			base.transform.localScale = new Vector3(this.scale, this.scale, 1f);
		}
		TipStatus tipStatus = this.status;
		if (tipStatus != TipStatus.Hidden)
		{
			if (tipStatus != TipStatus.Shown)
			{
				return;
			}
			if (this.canvasGroup.alpha < 1f)
			{
				this.canvasGroup.alpha += Time.deltaTime * 3f;
			}
			if (this.canvasGroup.alpha == 1f)
			{
				this.timeout -= Time.deltaTime;
				if (this.timeout <= 0f)
				{
					this.startHide();
				}
			}
		}
		else if (this.canvasGroup.alpha > 0f)
		{
			this.canvasGroup.alpha -= Time.deltaTime * 2f;
			return;
		}
	}

	// Token: 0x040026DB RID: 9947
	public Transform transform_toolbar;

	// Token: 0x040026DC RID: 9948
	public Transform transform_main;

	// Token: 0x040026DD RID: 9949
	public Transform transform_positionTop;

	// Token: 0x040026DE RID: 9950
	public static WorldTip instance;

	// Token: 0x040026DF RID: 9951
	public Canvas canvas;

	// Token: 0x040026E0 RID: 9952
	public Text text;

	// Token: 0x040026E1 RID: 9953
	public TipStatus status;

	// Token: 0x040026E2 RID: 9954
	public CanvasGroup canvasGroup;

	// Token: 0x040026E3 RID: 9955
	private float timeout;

	// Token: 0x040026E4 RID: 9956
	private float scale = 1f;

	// Token: 0x040026E5 RID: 9957
	public static Dictionary<string, string> replacementDict;
}
