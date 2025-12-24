using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x0200025D RID: 605
[Serializable]
public class CultureData : MetaObjectData
{
	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060016BE RID: 5822 RVA: 0x000E42B0 File Offset: 0x000E24B0
	// (set) Token: 0x060016BF RID: 5823 RVA: 0x000E42B8 File Offset: 0x000E24B8
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string original_actor_asset { get; set; }

	// Token: 0x17000152 RID: 338
	// (set) Token: 0x060016C0 RID: 5824 RVA: 0x000E42C1 File Offset: 0x000E24C1
	[Preserve]
	[Obsolete("use .original_actor_asset instead", true)]
	public string race
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.original_actor_asset))
			{
				return;
			}
			this.original_actor_asset = value;
		}
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x000E42E1 File Offset: 0x000E24E1
	public override void Dispose()
	{
		List<string> list = this.saved_traits;
		if (list != null)
		{
			list.Clear();
		}
		this.saved_traits = null;
		Dictionary<MetaType, string> dictionary = this.onomastics;
		if (dictionary != null)
		{
			dictionary.Clear();
		}
		this.onomastics = null;
		base.Dispose();
	}

	// Token: 0x040012B7 RID: 4791
	public int banner_decor_id;

	// Token: 0x040012B8 RID: 4792
	public int banner_element_id;

	// Token: 0x040012B9 RID: 4793
	public string creator_city_name = string.Empty;

	// Token: 0x040012BA RID: 4794
	[DefaultValue(-1L)]
	public long creator_city_id = -1L;

	// Token: 0x040012BB RID: 4795
	[DefaultValue(-1L)]
	public long creator_id = -1L;

	// Token: 0x040012BC RID: 4796
	public string creator_name = string.Empty;

	// Token: 0x040012BD RID: 4797
	public string creator_species_id = string.Empty;

	// Token: 0x040012BE RID: 4798
	public string creator_subspecies_name = string.Empty;

	// Token: 0x040012BF RID: 4799
	[DefaultValue(-1L)]
	public long creator_subspecies_id = -1L;

	// Token: 0x040012C0 RID: 4800
	[DefaultValue(-1L)]
	public long creator_kingdom_id = -1L;

	// Token: 0x040012C1 RID: 4801
	public string creator_kingdom_name = string.Empty;

	// Token: 0x040012C2 RID: 4802
	[DefaultValue(-1L)]
	public long creator_clan_id = -1L;

	// Token: 0x040012C3 RID: 4803
	public string creator_clan_name = string.Empty;

	// Token: 0x040012C5 RID: 4805
	public List<string> saved_traits;

	// Token: 0x040012C6 RID: 4806
	public double timestamp_last_written_book;

	// Token: 0x040012C7 RID: 4807
	public Dictionary<MetaType, string> onomastics;

	// Token: 0x040012C8 RID: 4808
	[JsonProperty("year")]
	[Preserve]
	[Obsolete("not used anymore", false)]
	public int year_obsolete;

	// Token: 0x040012C9 RID: 4809
	[DefaultValue("")]
	public string name_template_set = "";

	// Token: 0x040012CA RID: 4810
	[DefaultValue(-1L)]
	public long parent_culture_id = -1L;
}
