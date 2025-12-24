using System;
using System.Collections.Generic;

// Token: 0x02000075 RID: 117
public class SettingsWindow : TabbedWindow
{
	// Token: 0x06000442 RID: 1090 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
	public void resetToDefault()
	{
		foreach (OptionButton optionButton in this.buttons)
		{
			OptionAsset tAsset = optionButton.option_asset;
			if (tAsset.type == OptionType.Bool)
			{
				PlayerConfig.setOptionBool(tAsset.id, tAsset.default_bool);
			}
			else if (tAsset.type == OptionType.String)
			{
				PlayerConfig.setOptionString(tAsset.id, tAsset.default_string);
			}
			else if (tAsset.type == OptionType.Int)
			{
				PlayerConfig.setOptionInt(tAsset.id, tAsset.default_int);
			}
			if (Config.isMobile && tAsset.override_bool_mobile)
			{
				PlayerConfig.setOptionBool(tAsset.id, tAsset.default_bool_mobile);
			}
		}
		this.updateAllElements(true);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0002D6AC File Offset: 0x0002B8AC
	public void updateAllElements(bool pCallCallbacks = false)
	{
		foreach (OptionButton optionButton in this.buttons)
		{
			optionButton.updateElements(pCallCallbacks);
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0002D700 File Offset: 0x0002B900
	private void OnDisable()
	{
		if (OptionButton.player_config_dirty)
		{
			OptionButton.player_config_dirty = false;
			PlayerConfig.saveData();
		}
	}

	// Token: 0x04000383 RID: 899
	internal List<OptionButton> buttons = new List<OptionButton>();
}
