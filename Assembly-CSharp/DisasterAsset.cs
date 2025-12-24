using System;
using System.Collections.Generic;

// Token: 0x0200003C RID: 60
[Serializable]
public class DisasterAsset : Asset
{
	// Token: 0x04000222 RID: 546
	public DisasterAction action;

	// Token: 0x04000223 RID: 547
	public int rate = 1;

	// Token: 0x04000224 RID: 548
	public float chance = 1f;

	// Token: 0x04000225 RID: 549
	public string world_log;

	// Token: 0x04000226 RID: 550
	public int min_world_population;

	// Token: 0x04000227 RID: 551
	public int min_world_cities;

	// Token: 0x04000228 RID: 552
	public bool premium_only;

	// Token: 0x04000229 RID: 553
	public DisasterType type = DisasterType.Other;

	// Token: 0x0400022A RID: 554
	public string spawn_asset_unit = string.Empty;

	// Token: 0x0400022B RID: 555
	public string spawn_asset_building = string.Empty;

	// Token: 0x0400022C RID: 556
	public int max_existing_units = 20;

	// Token: 0x0400022D RID: 557
	public int units_min = 1;

	// Token: 0x0400022E RID: 558
	public int units_max = 10;

	// Token: 0x0400022F RID: 559
	public HashSet<string> ages_allow = new HashSet<string>();

	// Token: 0x04000230 RID: 560
	public HashSet<string> ages_forbid = new HashSet<string>();
}
