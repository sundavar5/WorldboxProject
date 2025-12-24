using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081C RID: 2076
public class WorldTemplateButton : MonoBehaviour
{
	// Token: 0x06004109 RID: 16649 RVA: 0x001BBF6A File Offset: 0x001BA16A
	private void OnEnable()
	{
		this.updateCounter();
	}

	// Token: 0x0600410A RID: 16650 RVA: 0x001BBF74 File Offset: 0x001BA174
	public void clickSwitch()
	{
		if (this.settings_asset == null)
		{
			Debug.LogError("Forgot to setup gen button - " + base.transform.name);
			return;
		}
		this.settings_asset.action_switch(this.settings_asset);
		this.updateCounter();
	}

	// Token: 0x0600410B RID: 16651 RVA: 0x001BBFC0 File Offset: 0x001BA1C0
	public void clickLeft()
	{
		if (this.settings_asset == null)
		{
			Debug.LogError("Forgot to setup gen button - " + base.transform.name);
			return;
		}
		if (this.settings_asset.decrease == null)
		{
			Debug.LogError("Forgot to setup gen button DECREASE - " + base.transform.name);
			return;
		}
		this.settings_asset.decrease(this.settings_asset);
		this.updateCounter();
	}

	// Token: 0x0600410C RID: 16652 RVA: 0x001BC034 File Offset: 0x001BA234
	public void clickRight()
	{
		if (this.settings_asset == null)
		{
			Debug.LogError("Forgot to setup gen button - " + base.transform.name);
			return;
		}
		if (this.settings_asset.increase == null)
		{
			Debug.LogError("Forgot to setup gen button INCREASE - " + base.transform.name);
			return;
		}
		this.settings_asset.increase(this.settings_asset);
		this.updateCounter();
	}

	// Token: 0x0600410D RID: 16653 RVA: 0x001BC0A8 File Offset: 0x001BA2A8
	public void updateCounter()
	{
		int tValue = this.settings_asset.action_get();
		this.text.GetComponent<LocalizedText>().setKeyAndUpdate(this.settings_asset.getLocaleID());
		if (!this.settings_asset.is_switch)
		{
			this.counter.text = tValue.ToString();
		}
		if (tValue == 0)
		{
			this.text.color = this.color_disabled;
			this.counter.color = this.color_disabled;
			this.icon.color = this.color_disabled;
		}
		else
		{
			this.text.color = this.color_enabled;
			this.counter.color = this.color_enabled;
			this.icon.color = this.color_enabled;
		}
		if (this.settings_asset.is_switch)
		{
			if (tValue == 1)
			{
				this.button_switch.GetComponent<CanvasGroup>().alpha = 1f;
				this.button_switch.transform.Find("Text").GetComponent<LocalizedText>().setKeyAndUpdate("short_on");
				this.button_switch.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOn");
				return;
			}
			this.button_switch.GetComponent<CanvasGroup>().alpha = 0.8f;
			this.button_switch.transform.Find("Text").GetComponent<LocalizedText>().setKeyAndUpdate("short_off");
			this.button_switch.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOff");
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x0600410E RID: 16654 RVA: 0x001BC226 File Offset: 0x001BA426
	private MapGenTemplate _template
	{
		get
		{
			return AssetManager.map_gen_templates.get(Config.current_map_template);
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x0600410F RID: 16655 RVA: 0x001BC237 File Offset: 0x001BA437
	private MapGenSettingsAsset settings_asset
	{
		get
		{
			return AssetManager.map_gen_settings.get(base.transform.name);
		}
	}

	// Token: 0x04002F3E RID: 12094
	public Image icon;

	// Token: 0x04002F3F RID: 12095
	public Text counter;

	// Token: 0x04002F40 RID: 12096
	public Text text;

	// Token: 0x04002F41 RID: 12097
	public PowerButton button_left;

	// Token: 0x04002F42 RID: 12098
	public PowerButton button_right;

	// Token: 0x04002F43 RID: 12099
	public PowerButton button_switch;

	// Token: 0x04002F44 RID: 12100
	public Action eventLeft;

	// Token: 0x04002F45 RID: 12101
	public Action eventRight;

	// Token: 0x04002F46 RID: 12102
	public Color color_enabled;

	// Token: 0x04002F47 RID: 12103
	public Color color_disabled;
}
