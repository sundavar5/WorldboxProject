using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000733 RID: 1843
public class BoxPreview : MonoBehaviour
{
	// Token: 0x06003AA7 RID: 15015 RVA: 0x0019E8E4 File Offset: 0x0019CAE4
	private void Awake()
	{
		this._button.OnHover(delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showHoverTooltip();
		});
		this._button.OnHoverOut(delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			Tooltip.hideTooltip();
		});
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x0019E934 File Offset: 0x0019CB34
	public void setSlot(int pID)
	{
		this._metaData = null;
		this._text_id.text = "#" + pID.ToString();
		this._slot_id = pID;
		this._world_path = SaveManager.getSlotSavePath(pID);
		if (SaveManager.doesSaveExist(this._world_path))
		{
			this._metaData = SaveManager.getMetaFor(this._world_path);
		}
		this._preview_image.sprite = this._preview_default;
		this._icon_gift.gameObject.SetActive(false);
		this._icon_premium.gameObject.SetActive(false);
		this._icon_broken.gameObject.SetActive(false);
		this._icon_modded.gameObject.SetActive(false);
		this._cursed_bg.enabled = false;
		this._cursed_overlay.enabled = false;
		if (this._metaData != null)
		{
			if (this._metaData.saveVersion > Config.WORLD_SAVE_VERSION)
			{
				this._icon_broken.gameObject.SetActive(true);
			}
			if (this._metaData.modded)
			{
				this._icon_modded.gameObject.SetActive(true);
			}
			if (this._metaData.cursed)
			{
				this._cursed_bg.enabled = true;
				this._cursed_overlay.enabled = true;
			}
		}
		this._wantLoad_preview = true;
		this._timer_preview = 0.02f * (float)pID;
		base.gameObject.name = "BoxPreview " + pID.ToString();
		bool tIsFavorite = PlayerConfig.instance.data.favorite_world == pID;
		this._favorited.SetActive(tIsFavorite);
	}

	// Token: 0x06003AA9 RID: 15017 RVA: 0x0019EAC0 File Offset: 0x0019CCC0
	private void showHoverTooltip()
	{
		if (this._metaData == null)
		{
			return;
		}
		if (!Config.tooltips_active)
		{
			return;
		}
		this._metaData.temp_date_string = SaveManager.getMapCreationTime(this._world_path);
		Tooltip.show(this._button, "map_meta", new TooltipData
		{
			map_meta = this._metaData
		});
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x0019EB15 File Offset: 0x0019CD15
	private void Update()
	{
		if (this._wantLoad_preview)
		{
			if (this._timer_preview > 0f)
			{
				this._timer_preview -= Time.deltaTime;
				return;
			}
			this._wantLoad_preview = false;
			base.StartCoroutine(this.loadSaveSlotImage());
		}
	}

	// Token: 0x06003AAB RID: 15019 RVA: 0x0019EB53 File Offset: 0x0019CD53
	public void showDefaultImage()
	{
		this._preview_image.sprite = this._preview_default;
	}

	// Token: 0x06003AAC RID: 15020 RVA: 0x0019EB68 File Offset: 0x0019CD68
	private void showPreview(Texture2D pTexture)
	{
		Sprite tSprite = Sprite.Create(Toolbox.ScaleTexture(pTexture, 100, 100), new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		this._preview_image.sprite = tSprite;
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x0019EBB9 File Offset: 0x0019CDB9
	private IEnumerator loadSaveSlotImage()
	{
		string tPath = SaveManager.generatePngPreviewPath(this._world_path);
		if (string.IsNullOrEmpty(tPath) || !File.Exists(tPath))
		{
			this.showDefaultImage();
			yield break;
		}
		yield return CoroutineHelper.wait_for_next_frame;
		Texture2D tTexture = new Texture2D(100, 100);
		tTexture.name = "preview_" + this._slot_id.ToString();
		try
		{
			byte[] pngBytes = File.ReadAllBytes(tPath);
			if (tTexture.LoadImage(pngBytes))
			{
				if (tTexture == null)
				{
					Debug.LogError(base.gameObject.name + " texture is null from " + tPath);
					this.showDefaultImage();
				}
				else
				{
					this.showPreview(tTexture);
				}
			}
			else
			{
				Debug.LogError(base.gameObject.name + " cannot load image from " + tPath);
				this.showDefaultImage();
			}
		}
		catch (Exception e)
		{
			Debug.LogError(string.Concat(new string[]
			{
				base.gameObject.name,
				" ",
				e.Message,
				" when trying to load ",
				tPath
			}));
			this.showDefaultImage();
		}
		Object.Destroy(tTexture);
		yield break;
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x0019EBC8 File Offset: 0x0019CDC8
	public void click()
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			Application.OpenURL("file://" + this._world_path);
			return;
		}
		SaveManager.setCurrentPathAndId(this._world_path, this._slot_id);
		if (SaveManager.currentSlotExists())
		{
			ScrollWindow.showWindow("save_slot");
			return;
		}
		ScrollWindow.showWindow("save_slot_new");
	}

	// Token: 0x04002B55 RID: 11093
	[SerializeField]
	private Sprite _preview_default;

	// Token: 0x04002B56 RID: 11094
	[SerializeField]
	private Image _icon_gift;

	// Token: 0x04002B57 RID: 11095
	[SerializeField]
	private Image _icon_premium;

	// Token: 0x04002B58 RID: 11096
	[SerializeField]
	private Image _icon_broken;

	// Token: 0x04002B59 RID: 11097
	[SerializeField]
	private Image _icon_modded;

	// Token: 0x04002B5A RID: 11098
	[SerializeField]
	private Image _cursed_bg;

	// Token: 0x04002B5B RID: 11099
	[SerializeField]
	private Image _cursed_overlay;

	// Token: 0x04002B5C RID: 11100
	[SerializeField]
	private GameObject _favorited;

	// Token: 0x04002B5D RID: 11101
	[SerializeField]
	private Image _preview_image;

	// Token: 0x04002B5E RID: 11102
	[SerializeField]
	private Button _button;

	// Token: 0x04002B5F RID: 11103
	[SerializeField]
	private Text _text_id;

	// Token: 0x04002B60 RID: 11104
	private bool _wantLoad_preview;

	// Token: 0x04002B61 RID: 11105
	private float _timer_preview;

	// Token: 0x04002B62 RID: 11106
	private string _world_path;

	// Token: 0x04002B63 RID: 11107
	private int _slot_id;

	// Token: 0x04002B64 RID: 11108
	private MapMetaData _metaData;
}
