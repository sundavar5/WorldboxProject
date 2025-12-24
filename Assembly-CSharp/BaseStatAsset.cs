using System;
using System.ComponentModel;

// Token: 0x02000218 RID: 536
[Serializable]
public class BaseStatAsset : Asset, ILocalizedAsset
{
	// Token: 0x06001348 RID: 4936 RVA: 0x000D7694 File Offset: 0x000D5894
	public string getLocaleID()
	{
		return this.translation_key ?? this.id;
	}

	// Token: 0x0400116C RID: 4460
	public bool hidden;

	// Token: 0x0400116D RID: 4461
	public string icon;

	// Token: 0x0400116E RID: 4462
	public bool normalize;

	// Token: 0x0400116F RID: 4463
	public float normalize_min;

	// Token: 0x04001170 RID: 4464
	[DefaultValue(2.1474836E+09f)]
	public float normalize_max = 2.1474836E+09f;

	// Token: 0x04001171 RID: 4465
	public bool used_only_for_civs;

	// Token: 0x04001172 RID: 4466
	public bool actor_data_attribute;

	// Token: 0x04001173 RID: 4467
	public bool show_as_percents;

	// Token: 0x04001174 RID: 4468
	[DefaultValue(1f)]
	public float tooltip_multiply_for_visual_number = 1f;

	// Token: 0x04001175 RID: 4469
	public bool multiplier;

	// Token: 0x04001176 RID: 4470
	public string main_stat_to_multiply;

	// Token: 0x04001177 RID: 4471
	public int sort_rank;

	// Token: 0x04001178 RID: 4472
	public bool ignore;

	// Token: 0x04001179 RID: 4473
	public string translation_key;
}
