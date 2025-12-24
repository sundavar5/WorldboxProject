using System;
using System.ComponentModel;

// Token: 0x02000274 RID: 628
public class FamilyData : MetaObjectData
{
	// Token: 0x04001309 RID: 4873
	public int banner_background_id;

	// Token: 0x0400130A RID: 4874
	public int banner_frame_id;

	// Token: 0x0400130B RID: 4875
	[DefaultValue(-1L)]
	public long alpha_id = -1L;

	// Token: 0x0400130C RID: 4876
	public string founder_actor_name_1;

	// Token: 0x0400130D RID: 4877
	public string founder_actor_name_2;

	// Token: 0x0400130E RID: 4878
	[DefaultValue(-1L)]
	public long main_founder_id_1 = -1L;

	// Token: 0x0400130F RID: 4879
	[DefaultValue(-1L)]
	public long main_founder_id_2 = -1L;

	// Token: 0x04001310 RID: 4880
	[DefaultValue(-1L)]
	public long subspecies_id = -1L;

	// Token: 0x04001311 RID: 4881
	public string subspecies_name = string.Empty;

	// Token: 0x04001312 RID: 4882
	public string species_id = string.Empty;

	// Token: 0x04001313 RID: 4883
	[DefaultValue(-1L)]
	public long founder_city_id = -1L;

	// Token: 0x04001314 RID: 4884
	public string founder_city_name = string.Empty;

	// Token: 0x04001315 RID: 4885
	[DefaultValue(-1L)]
	public long founder_kingdom_id = -1L;

	// Token: 0x04001316 RID: 4886
	public string founder_kingdom_name = string.Empty;

	// Token: 0x04001317 RID: 4887
	[DefaultValue(-1L)]
	public long original_family_1 = -1L;

	// Token: 0x04001318 RID: 4888
	[DefaultValue(-1L)]
	public long original_family_2 = -1L;
}
