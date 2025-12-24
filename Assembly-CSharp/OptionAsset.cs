using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

// Token: 0x0200006A RID: 106
[Serializable]
public class OptionAsset : Asset, IDescription2Asset, IDescriptionAsset, ILocalizedAsset, IMultiLocalesAsset
{
	// Token: 0x06000394 RID: 916 RVA: 0x000209E8 File Offset: 0x0001EBE8
	public string getLocaleID()
	{
		if (!this.has_locales)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(this.translation_key))
		{
			return this.translation_key;
		}
		return this.id;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00020A0E File Offset: 0x0001EC0E
	public string getDescriptionID()
	{
		if (!this.has_locales)
		{
			return null;
		}
		return this.translation_key_description;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00020A20 File Offset: 0x0001EC20
	public string getDescriptionID2()
	{
		if (!this.has_locales)
		{
			return null;
		}
		return this.translation_key_description_2;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00020A32 File Offset: 0x0001EC32
	public string getOptionLocaleID(int pIndex)
	{
		return this.locale_options_ids[pIndex];
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00020A3C File Offset: 0x0001EC3C
	public string getOptionLocaleID()
	{
		return this.getOptionLocaleID(this.current_int_value);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00020A4A File Offset: 0x0001EC4A
	public string getTranslatedOption()
	{
		return this.getOptionLocaleID().Localize();
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00020A57 File Offset: 0x0001EC57
	public IEnumerable<string> getLocaleIDs()
	{
		if (!this.has_locales)
		{
			yield break;
		}
		yield return this.getLocaleID();
		yield return this.getDescriptionID();
		yield return this.getDescriptionID2();
		if (this.locale_options_ids != null)
		{
			foreach (string tLocaleID in this.locale_options_ids)
			{
				yield return tLocaleID;
			}
			string[] array = null;
		}
		yield break;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00020A68 File Offset: 0x0001EC68
	public bool isActive()
	{
		bool tActive;
		if (this.type == OptionType.Bool)
		{
			tActive = PlayerConfig.optionBoolEnabled(this.id);
		}
		else
		{
			tActive = (PlayerConfig.getOptionInt(this.id) > 0);
		}
		return tActive;
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600039C RID: 924 RVA: 0x00020A9B File Offset: 0x0001EC9B
	[JsonIgnore]
	public PlayerOptionData data
	{
		get
		{
			return PlayerConfig.dict[this.id];
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600039D RID: 925 RVA: 0x00020AAD File Offset: 0x0001ECAD
	[JsonIgnore]
	public int current_int_value
	{
		get
		{
			return PlayerConfig.dict[this.id].intVal;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600039E RID: 926 RVA: 0x00020AC4 File Offset: 0x0001ECC4
	[JsonIgnore]
	public bool current_bool_value
	{
		get
		{
			return PlayerConfig.optionBoolEnabled(this.id);
		}
	}

	// Token: 0x0400031B RID: 795
	public OptionType type;

	// Token: 0x0400031C RID: 796
	[DefaultValue(true)]
	public bool has_locales = true;

	// Token: 0x0400031D RID: 797
	public string translation_key;

	// Token: 0x0400031E RID: 798
	public string translation_key_description;

	// Token: 0x0400031F RID: 799
	public string translation_key_description_2;

	// Token: 0x04000320 RID: 800
	[DefaultValue(true)]
	public bool reset_to_default_on_launch;

	// Token: 0x04000321 RID: 801
	[DefaultValue(true)]
	public bool default_bool = true;

	// Token: 0x04000322 RID: 802
	public bool computer_only;

	// Token: 0x04000323 RID: 803
	public bool override_bool_mobile;

	// Token: 0x04000324 RID: 804
	[DefaultValue(true)]
	public bool default_bool_mobile = true;

	// Token: 0x04000325 RID: 805
	public string default_string = string.Empty;

	// Token: 0x04000326 RID: 806
	public int default_int;

	// Token: 0x04000327 RID: 807
	public ActionOptionAsset action;

	// Token: 0x04000328 RID: 808
	public ActionFormatCounterOptionAsset counter_format;

	// Token: 0x04000329 RID: 809
	public int min_value;

	// Token: 0x0400032A RID: 810
	public int max_value;

	// Token: 0x0400032B RID: 811
	public bool multi_toggle;

	// Token: 0x0400032C RID: 812
	public bool counter_percent;

	// Token: 0x0400032D RID: 813
	public string[] locale_options_ids;

	// Token: 0x0400032E RID: 814
	public bool update_all_elements_after_click;

	// Token: 0x0400032F RID: 815
	[DefaultValue(true)]
	public bool interactable = true;
}
