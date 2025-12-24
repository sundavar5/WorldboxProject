using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020002E1 RID: 737
public class SubspeciesData : MetaObjectData
{
	// Token: 0x06001BCD RID: 7117 RVA: 0x000FDC4C File Offset: 0x000FBE4C
	public override void Dispose()
	{
		base.Dispose();
		List<ChromosomeData> list = this.saved_chromosome_data;
		if (list != null)
		{
			list.Clear();
		}
		this.saved_chromosome_data = null;
		List<string> list2 = this.saved_traits;
		if (list2 != null)
		{
			list2.Clear();
		}
		this.saved_traits = null;
		List<string> list3 = this.saved_actor_birth_traits;
		if (list3 != null)
		{
			list3.Clear();
		}
		this.saved_actor_birth_traits = null;
	}

	// Token: 0x04001550 RID: 5456
	public List<ChromosomeData> saved_chromosome_data;

	// Token: 0x04001551 RID: 5457
	public List<string> saved_traits;

	// Token: 0x04001552 RID: 5458
	public List<string> saved_actor_birth_traits;

	// Token: 0x04001553 RID: 5459
	public string biome_variant = "default_color";

	// Token: 0x04001554 RID: 5460
	public int banner_background_id;

	// Token: 0x04001555 RID: 5461
	public string species_id = "human";

	// Token: 0x04001556 RID: 5462
	[DefaultValue(-1L)]
	public long parent_subspecies = -1L;

	// Token: 0x04001557 RID: 5463
	[DefaultValue(-1L)]
	public long evolved_into_subspecies = -1L;

	// Token: 0x04001558 RID: 5464
	[DefaultValue(0)]
	public int evolved_into_subspecies_next_spawn;

	// Token: 0x04001559 RID: 5465
	public double last_evolution_timestamp;

	// Token: 0x0400155A RID: 5466
	public int skin_id;

	// Token: 0x0400155B RID: 5467
	public int mutation;

	// Token: 0x0400155C RID: 5468
	[DefaultValue(1)]
	public int generation = 1;

	// Token: 0x0400155D RID: 5469
	[DefaultValue(-1L)]
	public long skeleton_form_id = -1L;
}
