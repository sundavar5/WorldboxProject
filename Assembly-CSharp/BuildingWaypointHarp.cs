using System;

// Token: 0x02000255 RID: 597
public class BuildingWaypointHarp : BuildingWaypoint
{
	// Token: 0x1700013C RID: 316
	// (get) Token: 0x0600164C RID: 5708 RVA: 0x000E286F File Offset: 0x000E0A6F
	protected override string effect_id
	{
		get
		{
			return "fx_waypoint_harp_launch_bottom";
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x0600164D RID: 5709 RVA: 0x000E2876 File Offset: 0x000E0A76
	protected override string trait_id
	{
		get
		{
			return "desire_harp";
		}
	}
}
