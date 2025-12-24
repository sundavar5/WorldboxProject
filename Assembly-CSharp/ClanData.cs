using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x02000257 RID: 599
[Serializable]
public class ClanData : MetaObjectData
{
	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06001676 RID: 5750 RVA: 0x000E30F2 File Offset: 0x000E12F2
	// (set) Token: 0x06001677 RID: 5751 RVA: 0x000E30FA File Offset: 0x000E12FA
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string original_actor_asset { get; set; }

	// Token: 0x17000146 RID: 326
	// (set) Token: 0x06001678 RID: 5752 RVA: 0x000E3103 File Offset: 0x000E1303
	[Preserve]
	[Obsolete("use .original_actor_asset instead", true)]
	public string race_id
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

	// Token: 0x06001679 RID: 5753 RVA: 0x000E3123 File Offset: 0x000E1323
	public override void Dispose()
	{
		base.Dispose();
		List<string> list = this.saved_traits;
		if (list != null)
		{
			list.Clear();
		}
		this.saved_traits = null;
		List<LeaderEntry> list2 = this.past_chiefs;
		if (list2 != null)
		{
			list2.Clear();
		}
		this.past_chiefs = null;
	}

	// Token: 0x0400128F RID: 4751
	public string motto;

	// Token: 0x04001291 RID: 4753
	[DefaultValue(-1L)]
	public long chief_id = -1L;

	// Token: 0x04001292 RID: 4754
	[DefaultValue(-1L)]
	public long culture_id = -1L;

	// Token: 0x04001293 RID: 4755
	public List<LeaderEntry> past_chiefs;

	// Token: 0x04001294 RID: 4756
	public List<string> saved_traits;

	// Token: 0x04001295 RID: 4757
	public int books_written;

	// Token: 0x04001296 RID: 4758
	public int banner_background_id;

	// Token: 0x04001297 RID: 4759
	public int banner_icon_id;

	// Token: 0x04001298 RID: 4760
	public string founder_actor_name;

	// Token: 0x04001299 RID: 4761
	[DefaultValue(-1L)]
	public long founder_actor_id = -1L;

	// Token: 0x0400129A RID: 4762
	public string founder_kingdom_name;

	// Token: 0x0400129B RID: 4763
	[DefaultValue(-1L)]
	public long founder_kingdom_id = -1L;

	// Token: 0x0400129C RID: 4764
	public string founder_city_name;

	// Token: 0x0400129D RID: 4765
	[DefaultValue(-1L)]
	public long founder_city_id = -1L;

	// Token: 0x0400129E RID: 4766
	public string creator_species_id = string.Empty;

	// Token: 0x0400129F RID: 4767
	public string creator_subspecies_name = string.Empty;

	// Token: 0x040012A0 RID: 4768
	[DefaultValue(-1L)]
	public long creator_subspecies_id = -1L;
}
