using System;
using System.Collections.Generic;

// Token: 0x020002A0 RID: 672
[Serializable]
public class MetaCustomizationAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x0600195B RID: 6491 RVA: 0x000EF079 File Offset: 0x000ED279
	public IEnumerable<string> getLocaleIDs()
	{
		if (!this.editable)
		{
			yield break;
		}
		yield return this.localization_title;
		yield return this.title_locale;
		yield return this.option_1_locale;
		yield return this.option_2_locale;
		yield return this.color_locale;
		yield break;
	}

	// Token: 0x040013D9 RID: 5081
	public string localization_title;

	// Token: 0x040013DA RID: 5082
	public MetaType meta_type;

	// Token: 0x040013DB RID: 5083
	public string banner_prefab_id;

	// Token: 0x040013DC RID: 5084
	public MetaBanner get_banner;

	// Token: 0x040013DD RID: 5085
	public bool editable = true;

	// Token: 0x040013DE RID: 5086
	public bool option_1_editable = true;

	// Token: 0x040013DF RID: 5087
	public bool option_2_editable = true;

	// Token: 0x040013E0 RID: 5088
	public bool option_2_color_editable = true;

	// Token: 0x040013E1 RID: 5089
	public bool color_editable = true;

	// Token: 0x040013E2 RID: 5090
	public MetaCustomizationComponent customize_component;

	// Token: 0x040013E3 RID: 5091
	public string customize_window_id;

	// Token: 0x040013E4 RID: 5092
	public MetaCustomizationOptionGet option_1_get;

	// Token: 0x040013E5 RID: 5093
	public MetaCustomizationOptionSet option_1_set;

	// Token: 0x040013E6 RID: 5094
	public MetaCustomizationOptionGet option_2_get;

	// Token: 0x040013E7 RID: 5095
	public MetaCustomizationOptionSet option_2_set;

	// Token: 0x040013E8 RID: 5096
	public MetaCustomizationOptionGet color_get;

	// Token: 0x040013E9 RID: 5097
	public MetaCustomizationOptionSet color_set;

	// Token: 0x040013EA RID: 5098
	public MetaCustomizationCounter option_1_count;

	// Token: 0x040013EB RID: 5099
	public MetaCustomizationCounter option_2_count;

	// Token: 0x040013EC RID: 5100
	public MetaCustomizationCounter color_count;

	// Token: 0x040013ED RID: 5101
	public MetaCustomizationColorLibrary color_library;

	// Token: 0x040013EE RID: 5102
	public MetaCustomization on_new_color = delegate()
	{
		World.world.zone_calculator.dirtyAndClear();
	};

	// Token: 0x040013EF RID: 5103
	public string title_locale;

	// Token: 0x040013F0 RID: 5104
	public string option_1_locale;

	// Token: 0x040013F1 RID: 5105
	public string option_2_locale;

	// Token: 0x040013F2 RID: 5106
	public string color_locale;

	// Token: 0x040013F3 RID: 5107
	public string icon_banner;

	// Token: 0x040013F4 RID: 5108
	public string icon_creature;
}
