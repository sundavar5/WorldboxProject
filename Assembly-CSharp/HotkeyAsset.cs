using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x0200010A RID: 266
[Serializable]
public class HotkeyAsset : Asset
{
	// Token: 0x0600081A RID: 2074 RVA: 0x00071BA8 File Offset: 0x0006FDA8
	public bool isJustPressed()
	{
		if (!Input.anyKeyDown)
		{
			return false;
		}
		if (this.disable_for_controlled_unit && ControllableUnit.isControllingUnit())
		{
			return false;
		}
		if (this.hasModKey())
		{
			if (!this.isHoldingModKey())
			{
				return false;
			}
			if (!this.hasKey() && this.isJustPressedModKey())
			{
				return true;
			}
		}
		else if (!this.ignore_mod_keys && HotkeyAsset.isHoldingAnyModKey())
		{
			return false;
		}
		return this.hasKey() && this.isJustPressedKey();
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00071C18 File Offset: 0x0006FE18
	public bool isHolding()
	{
		if (this.use_mouse_wheel)
		{
			if (Input.mouseScrollDelta.y == 0f)
			{
				return false;
			}
		}
		else if (!Input.anyKey)
		{
			return false;
		}
		if (this.disable_for_controlled_unit && ControllableUnit.isControllingUnit())
		{
			return false;
		}
		if (this.hasModKey())
		{
			if (!this.isHoldingModKey())
			{
				return false;
			}
			if (!this.hasKey() && this.isHoldingModKey())
			{
				return true;
			}
		}
		else if (!this.ignore_mod_keys && HotkeyAsset.isHoldingAnyModKey())
		{
			return false;
		}
		return (this.hasKey() && this.isHoldingKey()) || this.use_mouse_wheel;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00071CAC File Offset: 0x0006FEAC
	private bool isHoldingModKey()
	{
		return Input.anyKey && (!this.disable_for_controlled_unit || !ControllableUnit.isControllingUnit()) && ((this.default_key_mod_1 != KeyCode.None && Input.GetKey(this.default_key_mod_1)) || (this.default_key_mod_2 != KeyCode.None && Input.GetKey(this.default_key_mod_2)) || (this.default_key_mod_3 != KeyCode.None && Input.GetKey(this.default_key_mod_3)));
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00071D19 File Offset: 0x0006FF19
	public static bool isHoldingAnyModKey()
	{
		return AssetManager.hotkey_library.isHoldingAnyModKey();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00071D25 File Offset: 0x0006FF25
	private bool hasKey()
	{
		return this.default_key_1 > KeyCode.None;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00071D30 File Offset: 0x0006FF30
	private bool hasModKey()
	{
		return this.default_key_mod_1 > KeyCode.None;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00071D3C File Offset: 0x0006FF3C
	public string getLocalizedKeys()
	{
		string tResult = "";
		string tKey = HotkeysLocalized.getLocalizedKey(this.default_key_1);
		string tKey2 = HotkeysLocalized.getLocalizedKey(this.default_key_2);
		string tKey3 = HotkeysLocalized.getLocalizedKey(this.default_key_3);
		string tMod = HotkeysLocalized.getLocalizedKey(this.default_key_mod_1);
		string tMod2 = HotkeysLocalized.getLocalizedKey(this.default_key_mod_2);
		string tMod3 = HotkeysLocalized.getLocalizedKey(this.default_key_mod_3);
		List<string> tKeys = new List<string>();
		if (!string.IsNullOrEmpty(tKey))
		{
			tKeys.Add(tKey);
		}
		if (!string.IsNullOrEmpty(tKey2))
		{
			tKeys.Add(tKey2);
		}
		if (!string.IsNullOrEmpty(tKey3))
		{
			tKeys.Add(tKey3);
		}
		List<string> tMods = new List<string>();
		if (!string.IsNullOrEmpty(tMod))
		{
			tMods.Add(tMod);
		}
		if (!string.IsNullOrEmpty(tMod2))
		{
			tMods.Add(tMod2);
		}
		if (!string.IsNullOrEmpty(tMod3))
		{
			tMods.Add(tMod3);
		}
		tKeys = new List<string>(new HashSet<string>(tKeys));
		tMods = new List<string>(new HashSet<string>(tMods));
		if (this.hasKey() && this.hasModKey())
		{
			int tLength = Mathf.Max(tKeys.Count, tMods.Count);
			string tMod4 = "";
			string tKey4 = "";
			for (int i = 0; i < tLength; i++)
			{
				if (i > 0)
				{
					tResult += " / ";
				}
				if (i < tMods.Count)
				{
					tMod4 = tMods[i];
				}
				if (i < tKeys.Count)
				{
					tKey4 = tKeys[i];
				}
				tResult = tResult + tMod4 + " + " + tKey4;
			}
		}
		else if (this.hasModKey())
		{
			tResult += string.Join(", ", tMods);
		}
		else if (this.hasKey())
		{
			tResult += string.Join(", ", tKeys);
		}
		return tResult;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00071EFC File Offset: 0x000700FC
	private bool isHoldingKey()
	{
		return Input.anyKey && ((this.default_key_1 != KeyCode.None && Input.GetKey(this.default_key_1)) || (this.default_key_2 != KeyCode.None && Input.GetKey(this.default_key_2)) || (this.default_key_3 != KeyCode.None && Input.GetKey(this.default_key_3)));
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00071F58 File Offset: 0x00070158
	private bool isJustPressedKey()
	{
		return Input.anyKeyDown && ((this.default_key_1 != KeyCode.None && Input.GetKeyDown(this.default_key_1)) || (this.default_key_2 != KeyCode.None && Input.GetKeyDown(this.default_key_2)) || (this.default_key_3 != KeyCode.None && Input.GetKeyDown(this.default_key_3)));
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00071FB4 File Offset: 0x000701B4
	private bool isJustPressedModKey()
	{
		return Input.anyKeyDown && ((this.default_key_mod_1 != KeyCode.None && Input.GetKeyDown(this.default_key_mod_1)) || (this.default_key_mod_2 != KeyCode.None && Input.GetKeyDown(this.default_key_mod_2)) || (this.default_key_mod_3 != KeyCode.None && Input.GetKeyDown(this.default_key_mod_3)));
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x00072010 File Offset: 0x00070210
	public bool checkIsPossible()
	{
		if (this.check_render_gameplay && !MapBox.isRenderGameplay())
		{
			return false;
		}
		if (this.check_render_minimap && !MapBox.isRenderMiniMap())
		{
			return false;
		}
		if (this.check_window_active)
		{
			if (!ScrollWindow.isWindowActive())
			{
				return false;
			}
			if (ScrollWindow.isAnimationActive())
			{
				return false;
			}
		}
		return (!this.check_window_not_active || !ScrollWindow.isWindowActive()) && (!this.check_no_selection || !SelectedUnit.isSet()) && (!this.check_no_multi_unit_selection || !SelectedUnit.multipleSelected()) && (!this.check_multi_unit_selection || SelectedUnit.multipleSelected()) && (!this.check_only_not_controllable_unit || !ControllableUnit.isControllingUnit()) && (!this.check_only_controllable_unit || ControllableUnit.isControllingUnit());
	}

	// Token: 0x0400086B RID: 2155
	public KeyCode default_key_mod_1;

	// Token: 0x0400086C RID: 2156
	public KeyCode default_key_mod_2;

	// Token: 0x0400086D RID: 2157
	public KeyCode default_key_mod_3;

	// Token: 0x0400086E RID: 2158
	public KeyCode default_key_1;

	// Token: 0x0400086F RID: 2159
	public KeyCode default_key_2;

	// Token: 0x04000870 RID: 2160
	public KeyCode default_key_3;

	// Token: 0x04000871 RID: 2161
	public KeyCode overridden_key_1;

	// Token: 0x04000872 RID: 2162
	public KeyCode overridden_key_2;

	// Token: 0x04000873 RID: 2163
	public KeyCode overridden_key_3;

	// Token: 0x04000874 RID: 2164
	public KeyCode overridden_key_mod_1;

	// Token: 0x04000875 RID: 2165
	public KeyCode overridden_key_mod_2;

	// Token: 0x04000876 RID: 2166
	public KeyCode overridden_key_mod_3;

	// Token: 0x04000877 RID: 2167
	public bool use_mouse_wheel;

	// Token: 0x04000878 RID: 2168
	public HotkeyAction just_pressed_action;

	// Token: 0x04000879 RID: 2169
	public HotkeyAction holding_action;

	// Token: 0x0400087A RID: 2170
	[DefaultValue(0.1f)]
	public float holding_cooldown = 0.1f;

	// Token: 0x0400087B RID: 2171
	[DefaultValue(0.33f)]
	public float holding_cooldown_first_action = 0.33f;

	// Token: 0x0400087C RID: 2172
	public bool ignore_same_key_diagnostic;

	// Token: 0x0400087D RID: 2173
	public bool disable_for_controlled_unit;

	// Token: 0x0400087E RID: 2174
	public bool ignore_mod_keys;

	// Token: 0x0400087F RID: 2175
	public bool check_only_controllable_unit;

	// Token: 0x04000880 RID: 2176
	public bool check_only_not_controllable_unit;

	// Token: 0x04000881 RID: 2177
	public bool check_controls_locked;

	// Token: 0x04000882 RID: 2178
	public bool check_window_active;

	// Token: 0x04000883 RID: 2179
	public bool check_window_not_active;

	// Token: 0x04000884 RID: 2180
	public bool check_render_gameplay;

	// Token: 0x04000885 RID: 2181
	public bool check_render_minimap;

	// Token: 0x04000886 RID: 2182
	public bool check_debug_active;

	// Token: 0x04000887 RID: 2183
	public bool check_no_multi_unit_selection;

	// Token: 0x04000888 RID: 2184
	public bool check_no_selection;

	// Token: 0x04000889 RID: 2185
	public bool check_multi_unit_selection;

	// Token: 0x0400088A RID: 2186
	public bool allow_unit_control;
}
