using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x02000833 RID: 2099
public class LevelPreviewButton : MonoBehaviour
{
	// Token: 0x0600414B RID: 16715 RVA: 0x001BC9E0 File Offset: 0x001BABE0
	public void click()
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		if (this.buttonAnimation == null)
		{
			this.buttonAnimation = base.transform.parent.parent.parent.GetComponent<ButtonAnimation>();
		}
		this.buttonAnimation.clickAnimation();
		SaveManager.setCurrentSlot(this.slotData.slotID);
		if (this.worldNetUpload)
		{
			if (!SaveManager.currentSlotExists())
			{
				return;
			}
			if (!SaveManager.currentPreviewExists())
			{
				return;
			}
			if (!SaveManager.currentMetaExists())
			{
				return;
			}
			ScrollWindow.showWindow("worldnet_upload_world_name");
			return;
		}
		else
		{
			if (SaveManager.currentSlotExists())
			{
				ScrollWindow.showWindow("save_slot");
				return;
			}
			ScrollWindow.showWindow("save_slot_new");
			return;
		}
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x001BCA88 File Offset: 0x001BAC88
	public void checkTextureDestroy()
	{
		if (this.button.image.sprite.texture != this.defaultSprite.texture)
		{
			Object.Destroy(this.button.image.sprite.texture);
		}
	}

	// Token: 0x0600414D RID: 16717 RVA: 0x001BCAD6 File Offset: 0x001BACD6
	private void OnEnable()
	{
		if (this.autoload)
		{
			this.reloadImage();
		}
	}

	// Token: 0x0600414E RID: 16718 RVA: 0x001BCAE8 File Offset: 0x001BACE8
	private void OnDisable()
	{
		Button button = this.button;
		Object x;
		if (button == null)
		{
			x = null;
		}
		else
		{
			Image image = button.image;
			if (image == null)
			{
				x = null;
			}
			else
			{
				Sprite sprite = image.sprite;
				x = ((sprite != null) ? sprite.texture : null);
			}
		}
		if (x == this.defaultSprite.texture)
		{
			return;
		}
		Button button2 = this.button;
		Object obj;
		if (button2 == null)
		{
			obj = null;
		}
		else
		{
			Image image2 = button2.image;
			if (image2 == null)
			{
				obj = null;
			}
			else
			{
				Sprite sprite2 = image2.sprite;
				obj = ((sprite2 != null) ? sprite2.texture : null);
			}
		}
		Object.Destroy(obj);
		Button button3 = this.button;
		Object obj2;
		if (button3 == null)
		{
			obj2 = null;
		}
		else
		{
			Image image3 = button3.image;
			obj2 = ((image3 != null) ? image3.sprite : null);
		}
		Object.Destroy(obj2);
	}

	// Token: 0x0600414F RID: 16719 RVA: 0x001BCB84 File Offset: 0x001BAD84
	public void reloadImage()
	{
		if (this == null)
		{
			return;
		}
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (this.loaded)
		{
			Button button = this.button;
			Object x;
			if (button == null)
			{
				x = null;
			}
			else
			{
				Image image = button.image;
				x = ((image != null) ? image.sprite : null);
			}
			if (x != null)
			{
				return;
			}
		}
		if (this.loading)
		{
			return;
		}
		this.loading = true;
		if (SaveManager.currentWorkshopMapData != null)
		{
			this.loadWorkshopMapPreview();
			return;
		}
		bool saveExists = SaveManager.currentSlotExists();
		if (this.slotData.slotID == -1 && !saveExists)
		{
			this.loadImage(PreviewHelper.getCurrentWorldPreview());
			return;
		}
		base.StartCoroutine(this.loadSaveSlotImage(this.slotData.slotID));
	}

	// Token: 0x06004150 RID: 16720 RVA: 0x001BCC2C File Offset: 0x001BAE2C
	private void loadWorkshopMapPreview()
	{
		this.loadImage(PreviewHelper.loadWorkshopMapPreview());
	}

	// Token: 0x06004151 RID: 16721 RVA: 0x001BCC39 File Offset: 0x001BAE39
	private IEnumerator loadSaveSlotImage(int slotID)
	{
		string path = SaveManager.getPngSlotPath(slotID);
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			this.loadImage(null);
			yield break;
		}
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + path))
		{
			yield return webRequest.SendWebRequest();
			if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.LogError(string.Concat(new string[]
				{
					base.gameObject.name,
					" ",
					webRequest.error,
					" ",
					path
				}));
				this.loadImage(null);
			}
			else
			{
				Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
				Sprite tSprite = Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
				this.loadImage(tSprite);
			}
		}
		UnityWebRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x06004152 RID: 16722 RVA: 0x001BCC50 File Offset: 0x001BAE50
	public void loadImage(Sprite pSource)
	{
		if (this == null || !base.isActiveAndEnabled)
		{
			this.loaded = false;
			this.loading = false;
			return;
		}
		if (!this.premiumOnly || Config.hasPremium)
		{
			this.premiumIcon.gameObject.SetActive(false);
		}
		bool tMapFound = false;
		if (pSource != null)
		{
			tMapFound = true;
			pSource.texture.anisoLevel = 0;
			pSource.texture.filterMode = FilterMode.Point;
		}
		else
		{
			pSource = this.defaultSprite;
		}
		this.button.image.sprite = pSource;
		base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pSource.rect.width, pSource.rect.height);
		RectTransform component = this.button.transform.parent.parent.GetComponent<RectTransform>();
		float tModWidth = component.sizeDelta.x / pSource.rect.width;
		float tModHeight = component.sizeDelta.y / pSource.rect.height;
		float tMod = (tModWidth > tModHeight) ? tModWidth : tModHeight;
		Transform parent = base.transform.parent;
		if (!tMapFound)
		{
			tMod = 1f;
		}
		parent.localScale = new Vector3(tMod, tMod, 1f);
		this.loaded = true;
		this.loading = false;
	}

	// Token: 0x04002FA6 RID: 12198
	public bool premiumOnly = true;

	// Token: 0x04002FA7 RID: 12199
	public bool worldNetUpload;

	// Token: 0x04002FA8 RID: 12200
	public Image premiumIcon;

	// Token: 0x04002FA9 RID: 12201
	public Image rewardAdIcon;

	// Token: 0x04002FAA RID: 12202
	public Button button;

	// Token: 0x04002FAB RID: 12203
	public SlotButtonCallback slotData;

	// Token: 0x04002FAC RID: 12204
	public Sprite defaultSprite;

	// Token: 0x04002FAD RID: 12205
	private ButtonAnimation buttonAnimation;

	// Token: 0x04002FAE RID: 12206
	public bool loaded;

	// Token: 0x04002FAF RID: 12207
	public bool loading;

	// Token: 0x04002FB0 RID: 12208
	public bool autoload;
}
