using System;
using System.ComponentModel;

// Token: 0x020002A8 RID: 680
[Serializable]
public class MetaRepresentationAsset : Asset, ILocalizedAsset
{
	// Token: 0x0600197B RID: 6523 RVA: 0x000F0799 File Offset: 0x000EE999
	public string getLocaleID()
	{
		return this.title_locale;
	}

	// Token: 0x040013F5 RID: 5109
	public MetaType meta_type;

	// Token: 0x040013F6 RID: 5110
	public string title_locale;

	// Token: 0x040013F7 RID: 5111
	public IconPathGetter icon_getter;

	// Token: 0x040013F8 RID: 5112
	public CheckActorHasMeta check_has_meta;

	// Token: 0x040013F9 RID: 5113
	public GetMetaFromActor meta_getter;

	// Token: 0x040013FA RID: 5114
	public GetMetaTotalFromActor meta_getter_total;

	// Token: 0x040013FB RID: 5115
	public GetWorldUnits world_units_getter;

	// Token: 0x040013FC RID: 5116
	public string general_icon_path;

	// Token: 0x040013FD RID: 5117
	public bool show_none_percent;

	// Token: 0x040013FE RID: 5118
	[DefaultValue(true)]
	public bool show_none_percent_for_total = true;

	// Token: 0x040013FF RID: 5119
	[DefaultValue(true)]
	public bool show_species_icon = true;
}
