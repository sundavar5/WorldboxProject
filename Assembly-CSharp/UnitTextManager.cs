using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
public class UnitTextManager : MonoBehaviour
{
	// Token: 0x060038B1 RID: 14513 RVA: 0x00195DCC File Offset: 0x00193FCC
	private void Start()
	{
		foreach (UnitTextPhrases tPhrase in base.GetComponentsInChildren<UnitTextPhrases>(true))
		{
			this._phrases.Add(tPhrase);
		}
	}

	// Token: 0x060038B2 RID: 14514 RVA: 0x00195E00 File Offset: 0x00194000
	private void Update()
	{
		if (!SelectedUnit.isSet())
		{
			return;
		}
		if (SelectedUnit.unit.isLying())
		{
			return;
		}
		if (this._follow_object == null)
		{
			return;
		}
		if (Time.frameCount % 50 != 0)
		{
			return;
		}
		if (!Config.isDraggingItem())
		{
			return;
		}
		if (!Config.dragging_item_object.HasComponent<UnitAvatarLoader>())
		{
			return;
		}
		this.startNewCurse();
	}

	// Token: 0x060038B3 RID: 14515 RVA: 0x00195E58 File Offset: 0x00194058
	public void startNew(string pText)
	{
		using (ListPool<UnitTextPhrases> tTempPool = new ListPool<UnitTextPhrases>())
		{
			foreach (UnitTextPhrases tPhrase in this._phrases)
			{
				if (!tPhrase.isTweening())
				{
					tTempPool.Add(tPhrase);
				}
			}
			if (tTempPool.Count != 0)
			{
				UnitTextPhrases random = tTempPool.GetRandom<UnitTextPhrases>();
				IDraggable dragging_item_object = Config.dragging_item_object;
				random.startNewTween(pText, (dragging_item_object != null) ? dragging_item_object.transform : null);
			}
		}
	}

	// Token: 0x060038B4 RID: 14516 RVA: 0x00195EF8 File Offset: 0x001940F8
	public void startNewCurse()
	{
		string tText = this.getRandomInsultText();
		this.startNew(tText);
	}

	// Token: 0x060038B5 RID: 14517 RVA: 0x00195F14 File Offset: 0x00194114
	public void startNewWhat()
	{
		if (SelectedUnit.unit.isLying())
		{
			return;
		}
		if (Randy.randomChance(0.3f))
		{
			this.startNewCurse();
			return;
		}
		int tRandomInt = Randy.randomInt(1, 7);
		bool tQuestionMarks = Randy.randomBool();
		using (StringBuilderPool tBuilder = new StringBuilderPool(tRandomInt))
		{
			for (int i = 0; i < tRandomInt; i++)
			{
				if (tQuestionMarks)
				{
					tBuilder.Append('?');
				}
				else
				{
					tBuilder.Append('!');
				}
			}
			string tText = tBuilder.ToString();
			this.startNew(tText);
		}
	}

	// Token: 0x060038B6 RID: 14518 RVA: 0x00195FA8 File Offset: 0x001941A8
	public void spawnAvatarText(Actor pActor = null)
	{
		if ((!pActor.isRekt() && pActor.isLying()) || (pActor == null && SelectedUnit.unit.isLying()))
		{
			this.startNewCurse();
			return;
		}
		ActorAsset tAsset = pActor.isRekt() ? null : pActor.getActorAsset();
		string tText = this.getAssetText(tAsset);
		this.startNew(tText);
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x00195FFC File Offset: 0x001941FC
	public string getAssetText(ActorAsset pAsset = null)
	{
		ActorAsset tAsset = pAsset ?? SelectedUnit.unit.asset;
		int tRandomInt = Randy.randomInt(1, 3);
		string tString = "click_" + tAsset.id + "_" + tRandomInt.ToString();
		if (LocalizedTextManager.instance.contains(tString))
		{
			return LocalizedTextManager.getText(tString, null, false);
		}
		return this.getRandomInsultText();
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x0019605A File Offset: 0x0019425A
	private string getRandomInsultText()
	{
		return InsultStringGenerator.getRandomText(4, 9, false);
	}

	// Token: 0x04002A0D RID: 10765
	[SerializeField]
	private DragSnapElement _follow_object;

	// Token: 0x04002A0E RID: 10766
	private List<UnitTextPhrases> _phrases = new List<UnitTextPhrases>();
}
