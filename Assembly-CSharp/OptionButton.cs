using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007A1 RID: 1953
public class OptionButton : MonoBehaviour
{
	// Token: 0x06003DCF RID: 15823 RVA: 0x001AFCE3 File Offset: 0x001ADEE3
	private void Start()
	{
		this.settings_window = base.transform.parent.GetComponentInParent<SettingsWindow>();
		this.settings_window.buttons.Add(this);
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x001AFD0C File Offset: 0x001ADF0C
	private void OnDestroy()
	{
		if (this.settings_window != null)
		{
			this.settings_window.buttons.Remove(this);
		}
	}

	// Token: 0x06003DD1 RID: 15825 RVA: 0x001AFD30 File Offset: 0x001ADF30
	private void OnEnable()
	{
		if (this.option_asset == null)
		{
			Debug.LogError("Missing Option - " + base.transform.name);
			return;
		}
		string tKey = this.option_asset.getLocaleID();
		this.text.GetComponent<LocalizedText>().setKeyAndUpdate(tKey);
		TipButton tTip;
		if (this.option_asset.type == OptionType.Bool)
		{
			tTip = this.button_switch.GetComponent<TipButton>();
		}
		else
		{
			tTip = this.slider.GetComponent<TipButton>();
		}
		tTip.setHoverAction(delegate
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showTooltip();
		}, true);
		if (!string.IsNullOrEmpty(this.option_asset.getDescriptionID2()))
		{
			this.slider.GetComponent<TipButton>().text_description_2 = this.option_asset.getDescriptionID2();
			this.button_switch.GetComponent<TipButton>().text_description_2 = this.option_asset.getDescriptionID2();
		}
		if (this.option_asset.type == OptionType.Bool)
		{
			this.counter.gameObject.SetActive(false);
			this.button_switch.gameObject.SetActive(true);
			this.sliderArea.SetActive(false);
		}
		else if (this.option_asset.type == OptionType.Int)
		{
			this.counter.gameObject.SetActive(true);
			this.button_switch.gameObject.SetActive(false);
			this.sliderArea.SetActive(true);
			this.updateSlider(false);
			this.slider.onValueChanged.AddListener(new UnityAction<float>(this.sliderChanged));
			this.slider.addCallbackPointerDown(new SliderPointerDownEvent(this.checkShowSliderTooltip));
		}
		this.updateElements(false);
	}

	// Token: 0x06003DD2 RID: 15826 RVA: 0x001AFEB9 File Offset: 0x001AE0B9
	public void showTooltip()
	{
		Tooltip.show(this, "tip", this.getTooltipData());
	}

	// Token: 0x06003DD3 RID: 15827 RVA: 0x001AFECC File Offset: 0x001AE0CC
	public void checkShowSliderTooltip()
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		if (Input.touchCount >= 2)
		{
			Tooltip.hideTooltip();
			return;
		}
		this.showTooltip();
	}

	// Token: 0x06003DD4 RID: 15828 RVA: 0x001AFEEC File Offset: 0x001AE0EC
	private TooltipData getTooltipData()
	{
		TooltipData tResult = new TooltipData
		{
			tip_name = this.option_asset.getLocaleID(),
			tip_description = this.option_asset.getDescriptionID()
		};
		string tDescription2Key = this.option_asset.getDescriptionID2();
		if (!string.IsNullOrEmpty(tDescription2Key))
		{
			tResult.tip_description_2 = tDescription2Key;
		}
		return tResult;
	}

	// Token: 0x06003DD5 RID: 15829 RVA: 0x001AFF40 File Offset: 0x001AE140
	public void switchBoolOption()
	{
		bool tCurrent = PlayerConfig.optionBoolEnabled(this.option_asset.id);
		PlayerConfig.setOptionBool(this.option_asset.id, !tCurrent);
		OptionButton.player_config_dirty = true;
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x001AFF78 File Offset: 0x001AE178
	public void clickSwitch()
	{
		if (this.option_asset == null)
		{
			Debug.LogError("Missing Option - " + base.transform.name);
			return;
		}
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
		{
			this.showTooltip();
			return;
		}
		this.switchBoolOption();
		ActionOptionAsset action = this.option_asset.action;
		if (action != null)
		{
			action(this.option_asset);
		}
		if (this.option_asset.update_all_elements_after_click)
		{
			this.settings_window.updateAllElements(false);
			return;
		}
		this.updateElements(false);
	}

	// Token: 0x06003DD7 RID: 15831 RVA: 0x001B0001 File Offset: 0x001AE201
	private int clampAssetValue(int pValue)
	{
		return Mathf.Clamp(pValue, this.option_asset.min_value, this.option_asset.max_value);
	}

	// Token: 0x06003DD8 RID: 15832 RVA: 0x001B0020 File Offset: 0x001AE220
	private void sliderChanged(float pValue)
	{
		int tValue = (int)pValue;
		tValue = this.clampAssetValue(tValue);
		PlayerConfig.setOptionInt(this.option_asset.id, tValue);
		OptionButton.player_config_dirty = true;
		ActionOptionAsset action = this.option_asset.action;
		if (action != null)
		{
			action(this.option_asset);
		}
		this.updateElements(false);
		this.checkShowSliderTooltip();
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x001B0078 File Offset: 0x001AE278
	public void updateElements(bool pCallCallbacks = false)
	{
		this.updateCounter();
		this.updateSwitchButton();
		this.updateColors();
		this.updateSlider(pCallCallbacks);
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x001B0094 File Offset: 0x001AE294
	private void updateSlider(bool pCallCallbacks = false)
	{
		if (this.option_asset.type != OptionType.Int)
		{
			return;
		}
		this.slider.minValue = (float)this.option_asset.min_value;
		this.slider.maxValue = (float)this.option_asset.max_value;
		if (!pCallCallbacks)
		{
			this.slider.SetValueWithoutNotify((float)PlayerConfig.getIntValue(this.option_asset.id));
		}
		else
		{
			this.slider.value = (float)PlayerConfig.getIntValue(this.option_asset.id);
		}
		this.slider.wholeNumbers = false;
	}

	// Token: 0x06003DDB RID: 15835 RVA: 0x001B0128 File Offset: 0x001AE328
	private void updateColors()
	{
		if (this.option_asset.type != OptionType.Bool)
		{
			return;
		}
		if (!this.option_asset.interactable)
		{
			this.text.color = this.color_disabled;
			this.icon.color = this.color_disabled;
			return;
		}
		if (PlayerConfig.optionBoolEnabled(this.option_asset.id))
		{
			this.text.color = this.color_enabled;
			this.icon.color = this.color_enabled;
			return;
		}
		this.text.color = this.color_disabled;
		this.icon.color = this.color_disabled;
	}

	// Token: 0x06003DDC RID: 15836 RVA: 0x001B01CC File Offset: 0x001AE3CC
	private void updateSwitchButton()
	{
		if (this.option_asset.type == OptionType.Bool)
		{
			Image tBackground = this.button_switch.GetComponent<Image>();
			CanvasGroup tGroup = this.button_switch.GetComponent<CanvasGroup>();
			if (!this.option_asset.interactable)
			{
				tGroup.alpha = 0.8f;
				tGroup.interactable = false;
				tBackground.sprite = this._button_sprite_not_interactable;
				return;
			}
			tGroup.interactable = true;
			tBackground.sprite = this._button_sprite_interactable;
			if (PlayerConfig.optionBoolEnabled(this.option_asset.id))
			{
				tGroup.alpha = 1f;
				this.button_switch.transform.Find("Text").GetComponent<LocalizedText>().setKeyAndUpdate("short_on");
				this.button_switch.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOn");
				return;
			}
			tGroup.alpha = 0.8f;
			this.button_switch.transform.Find("Text").GetComponent<LocalizedText>().setKeyAndUpdate("short_off");
			this.button_switch.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOff");
		}
	}

	// Token: 0x06003DDD RID: 15837 RVA: 0x001B02E8 File Offset: 0x001AE4E8
	public void updateCounter()
	{
		if (this.option_asset.type != OptionType.Int)
		{
			return;
		}
		int tIntValue = PlayerConfig.getIntValue(this.option_asset.id);
		string tText = tIntValue.ToString();
		if (this.option_asset.counter_format != null)
		{
			tText = this.option_asset.counter_format(this.option_asset);
		}
		else if (this.option_asset.counter_percent)
		{
			tText += "%";
		}
		this.counter.text = tText;
		if (tIntValue == 0)
		{
			this.text.color = this.color_disabled;
			this.counter.color = this.color_disabled;
			this.icon.color = this.color_disabled;
			return;
		}
		this.text.color = this.color_enabled;
		this.counter.color = this.color_enabled;
		this.icon.color = this.color_enabled;
	}

	// Token: 0x06003DDE RID: 15838 RVA: 0x001B03D2 File Offset: 0x001AE5D2
	private void sliderDragEnded()
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		Tooltip.hideTooltip();
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06003DDF RID: 15839 RVA: 0x001B03E1 File Offset: 0x001AE5E1
	public OptionAsset option_asset
	{
		get
		{
			return AssetManager.options_library.get(base.transform.name);
		}
	}

	// Token: 0x04002CE9 RID: 11497
	private const float DISABLED_ALPHA = 0.8f;

	// Token: 0x04002CEA RID: 11498
	public static bool player_config_dirty;

	// Token: 0x04002CEB RID: 11499
	public Image icon;

	// Token: 0x04002CEC RID: 11500
	public Text counter;

	// Token: 0x04002CED RID: 11501
	public Text text;

	// Token: 0x04002CEE RID: 11502
	public PowerButton button_switch;

	// Token: 0x04002CEF RID: 11503
	public SliderExtended slider;

	// Token: 0x04002CF0 RID: 11504
	public GameObject sliderArea;

	// Token: 0x04002CF1 RID: 11505
	public GameObject optionArea;

	// Token: 0x04002CF2 RID: 11506
	public Action eventLeft;

	// Token: 0x04002CF3 RID: 11507
	public Action eventRight;

	// Token: 0x04002CF4 RID: 11508
	public Color color_enabled;

	// Token: 0x04002CF5 RID: 11509
	public Color color_disabled;

	// Token: 0x04002CF6 RID: 11510
	[SerializeField]
	private Sprite _button_sprite_interactable;

	// Token: 0x04002CF7 RID: 11511
	[SerializeField]
	private Sprite _button_sprite_not_interactable;

	// Token: 0x04002CF8 RID: 11512
	public SettingsWindow settings_window;
}
