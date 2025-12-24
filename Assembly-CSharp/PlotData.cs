using System;
using System.ComponentModel;

// Token: 0x020002CF RID: 719
public class PlotData : MetaObjectData
{
	// Token: 0x06001AB4 RID: 6836 RVA: 0x000F9C12 File Offset: 0x000F7E12
	public override void Dispose()
	{
		base.Dispose();
	}

	// Token: 0x040014B1 RID: 5297
	public string plot_type_id;

	// Token: 0x040014B2 RID: 5298
	public string founder_name;

	// Token: 0x040014B3 RID: 5299
	[DefaultValue(-1L)]
	public long founder_id = -1L;

	// Token: 0x040014B4 RID: 5300
	[DefaultValue(-1L)]
	public long id_initiator_actor = -1L;

	// Token: 0x040014B5 RID: 5301
	[DefaultValue(-1L)]
	public long id_initiator_city = -1L;

	// Token: 0x040014B6 RID: 5302
	[DefaultValue(-1L)]
	public long id_initiator_kingdom = -1L;

	// Token: 0x040014B7 RID: 5303
	[DefaultValue(-1L)]
	public long id_target_actor = -1L;

	// Token: 0x040014B8 RID: 5304
	[DefaultValue(-1L)]
	public long id_target_city = -1L;

	// Token: 0x040014B9 RID: 5305
	[DefaultValue(-1L)]
	public long id_target_kingdom = -1L;

	// Token: 0x040014BA RID: 5306
	[DefaultValue(-1L)]
	public long id_target_alliance = -1L;

	// Token: 0x040014BB RID: 5307
	[DefaultValue(-1L)]
	public long id_target_war = -1L;

	// Token: 0x040014BC RID: 5308
	public bool forced;

	// Token: 0x040014BD RID: 5309
	public float progress_current;
}
