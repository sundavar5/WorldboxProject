using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x02000734 RID: 1844
public class GlobusPreview : MonoBehaviour
{
	// Token: 0x06003AB1 RID: 15025 RVA: 0x0019EC44 File Offset: 0x0019CE44
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (this.use_current_world_info)
		{
			this.setCurrentWorldSprite();
		}
		else if (SaveManager.currentWorkshopMapData != null)
		{
			this.setWorkshopSlotSprite();
		}
		else
		{
			this.startLoadCurrentSaveSlotSprite();
		}
		this.startTweenGlobus();
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x0019EC79 File Offset: 0x0019CE79
	private void startLoadCurrentSaveSlotSprite()
	{
		base.StartCoroutine(this.loadSaveSlotImage());
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x0019EC88 File Offset: 0x0019CE88
	private void setCurrentWorldSprite()
	{
		Sprite tSprite = PreviewHelper.getCurrentWorldPreview();
		this.setSprites(tSprite);
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x0019ECA4 File Offset: 0x0019CEA4
	private void setWorkshopSlotSprite()
	{
		Sprite tSprite = PreviewHelper.loadWorkshopMapPreview();
		this.setSprites(tSprite);
	}

	// Token: 0x06003AB5 RID: 15029 RVA: 0x0019ECBE File Offset: 0x0019CEBE
	private void setSprites(Sprite pSprite)
	{
		this.makeGradient(pSprite);
		this.main_image_1.sprite = pSprite;
		this.main_image_2.sprite = pSprite;
	}

	// Token: 0x06003AB6 RID: 15030 RVA: 0x0019ECDF File Offset: 0x0019CEDF
	private void showDefaultImage()
	{
		this.main_image_1.sprite = this.preview_default;
		this.main_image_2.sprite = this.preview_default;
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x0019ED03 File Offset: 0x0019CF03
	private IEnumerator loadSaveSlotImage()
	{
		string path = SaveManager.getCurrentPreviewPath();
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			this.showDefaultImage();
			yield break;
		}
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + path))
		{
			yield return webRequest.SendWebRequest();
			if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				this.showDefaultImage();
			}
			else
			{
				Texture2D tTexture = DownloadHandlerTexture.GetContent(webRequest);
				tTexture.name = "save_slot_preview_" + Path.GetFileNameWithoutExtension(path);
				Sprite tSprite = Sprite.Create(tTexture, new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height), new Vector2(0.5f, 0.5f));
				this.setSprites(tSprite);
			}
		}
		UnityWebRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x06003AB8 RID: 15032 RVA: 0x0019ED14 File Offset: 0x0019CF14
	private void makeGradient(Sprite pSprite)
	{
		float tGradientWidth = (float)pSprite.texture.width * 0.1f;
		Texture2D tTexture = pSprite.texture;
		tTexture.name = "gradient_" + tTexture.name;
		int xx = 0;
		while ((float)xx < tGradientWidth)
		{
			for (int yy = 0; yy < tTexture.height; yy++)
			{
				int tX = xx;
				Color tColor = tTexture.GetPixel(tX, yy);
				tColor.a = (float)tX / tGradientWidth;
				tTexture.SetPixel(tX, yy, tColor);
				tX = pSprite.texture.width - xx;
				tColor = tTexture.GetPixel(tX, yy);
				tColor.a = (float)xx / tGradientWidth;
				tTexture.SetPixel(tX, yy, tColor);
			}
			xx++;
		}
		tTexture.Apply();
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x0019EDCC File Offset: 0x0019CFCC
	private void startTweenGlobus()
	{
		float tDist = this._box_size + this._gap_size;
		float tTime = tDist / this._tweenSpeed;
		this.images_parent.transform.DOKill(false);
		this.images_parent.transform.localPosition = new Vector3(this._gap_size, 0f, 0f);
		this.images_parent.transform.DOLocalMove(new Vector3(-tDist, 0f, 0f), tTime, false).SetEase(Ease.Linear).onComplete = new TweenCallback(this.tweenLoop);
	}

	// Token: 0x06003ABA RID: 15034 RVA: 0x0019EE64 File Offset: 0x0019D064
	private void tweenLoop()
	{
		float tDist = this._box_size + this._gap_size;
		float tTime = tDist / this._tweenSpeed;
		this.images_parent.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.images_parent.transform.DOLocalMove(new Vector3(-tDist, 0f, 0f), tTime, false).SetEase(Ease.Linear).onComplete = new TweenCallback(this.tweenLoop);
	}

	// Token: 0x04002B65 RID: 11109
	public bool use_current_world_info;

	// Token: 0x04002B66 RID: 11110
	public Image main_image_1;

	// Token: 0x04002B67 RID: 11111
	public Image main_image_2;

	// Token: 0x04002B68 RID: 11112
	public GameObject images_parent;

	// Token: 0x04002B69 RID: 11113
	public Image clouds;

	// Token: 0x04002B6A RID: 11114
	public Sprite preview_default;

	// Token: 0x04002B6B RID: 11115
	private float _tweenSpeed = 18f;

	// Token: 0x04002B6C RID: 11116
	private float _gap_size = 25f;

	// Token: 0x04002B6D RID: 11117
	private float _box_size = 100f;
}
