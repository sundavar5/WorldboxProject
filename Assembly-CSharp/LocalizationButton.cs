using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005E8 RID: 1512
public class LocalizationButton : MonoBehaviour
{
	// Token: 0x06003191 RID: 12689 RVA: 0x0017AEDD File Offset: 0x001790DD
	public GameLanguageAsset getAsset()
	{
		return this._asset;
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x0017AEE8 File Offset: 0x001790E8
	private void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._localized_text = this._text_field.GetComponent<LocalizedText>();
		this._tip_button = this._button.GetComponent<TipButton>();
		this._tip_button.hoverAction = delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showTooltip();
		};
		TipButton tip_button = this._tip_button;
		tip_button.clickAction = (TooltipAction)Delegate.Combine(tip_button.clickAction, new TooltipAction(delegate()
		{
			if (InputHelpers.mouseSupported)
			{
				this.changeLanguage();
				return;
			}
			if (Tooltip.isShowingFor(this))
			{
				this.changeLanguage();
				return;
			}
			this.showTooltip();
		}));
		base.gameObject.name = this._asset.id;
		if (this._asset.path_icon != null)
		{
			this._icon.sprite = SpriteTextureLoader.getSprite(this._asset.path_icon);
			this._icon.gameObject.SetActive(true);
			RectTransform _text_field_rect = this._text_field.GetComponent<RectTransform>();
			_text_field_rect.offsetMin = new Vector2(18.5f, _text_field_rect.offsetMin.y);
			_text_field_rect.offsetMax = new Vector2(-4f, _text_field_rect.offsetMax.y);
		}
		else
		{
			this._icon.gameObject.SetActive(false);
			RectTransform _text_field_rect2 = this._text_field.GetComponent<RectTransform>();
			_text_field_rect2.offsetMin = new Vector2(4f, _text_field_rect2.offsetMin.y);
			_text_field_rect2.offsetMax = new Vector2(-4f, _text_field_rect2.offsetMax.y);
		}
		this._text_field.text = this._asset.name;
		this._localized_text.checkSpecialLanguages(this._asset);
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x0017B074 File Offset: 0x00179274
	private void showTooltip()
	{
		TooltipData tData = new TooltipData
		{
			game_language_asset = this._asset
		};
		Tooltip.show(this, "game_language", tData);
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x0017B09F File Offset: 0x0017929F
	private void changeLanguage()
	{
		LocalizedTextManager.instance.setLanguage(this._asset.id);
		WorldLanguagesWindow.updateButtons();
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x0017B0BC File Offset: 0x001792BC
	internal void checkSprite()
	{
		if (LocalizedTextManager.current_language == this._asset)
		{
			this._bg_image.sprite = this.button_current;
			return;
		}
		if (LocalizedTextManager.getCulture(base.transform.gameObject.name) == LocalizedTextManager.getCurrentCulture())
		{
			this._bg_image.sprite = this.button_highlight;
			return;
		}
		this._bg_image.sprite = this.button_normal;
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x0017B128 File Offset: 0x00179328
	public void SetAsset(GameLanguageAsset pAsset, int pDone)
	{
		this._asset = pAsset;
		if (pDone > 0)
		{
			if (pDone < 40)
			{
				this._percent.color = Toolbox.color_negative_RGBA;
			}
			else if (pDone < 60)
			{
				this._percent.color = Toolbox.color_log_warning;
			}
			else if (pDone < 80)
			{
				this._percent.color = Toolbox.color_text_default;
			}
			else
			{
				this._percent.color = Toolbox.color_positive_RGBA;
			}
			this._percent.text = pDone.ToString() + "%";
			this._percent.gameObject.SetActive(true);
		}
		else
		{
			this._percent.gameObject.SetActive(false);
		}
		this.init();
		this.checkSprite();
	}

	// Token: 0x0400256B RID: 9579
	public Sprite button_current;

	// Token: 0x0400256C RID: 9580
	public Sprite button_normal;

	// Token: 0x0400256D RID: 9581
	public Sprite button_highlight;

	// Token: 0x0400256E RID: 9582
	[SerializeField]
	private Image _icon;

	// Token: 0x0400256F RID: 9583
	[SerializeField]
	private Image _bg_image;

	// Token: 0x04002570 RID: 9584
	[SerializeField]
	private Button _button;

	// Token: 0x04002571 RID: 9585
	[SerializeField]
	private Text _text_field;

	// Token: 0x04002572 RID: 9586
	private TipButton _tip_button;

	// Token: 0x04002573 RID: 9587
	private LocalizedText _localized_text;

	// Token: 0x04002574 RID: 9588
	[SerializeField]
	private Text _percent;

	// Token: 0x04002575 RID: 9589
	private GameLanguageAsset _asset;

	// Token: 0x04002576 RID: 9590
	private bool _initialized;
}
