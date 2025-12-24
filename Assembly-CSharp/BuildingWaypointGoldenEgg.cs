using System;

// Token: 0x02000254 RID: 596
public class BuildingWaypointGoldenEgg : BuildingWaypoint
{
	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06001649 RID: 5705 RVA: 0x000E2859 File Offset: 0x000E0A59
	protected override string effect_id
	{
		get
		{
			return "fx_waypoint_golden_egg_launch_bottom";
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x000E2860 File Offset: 0x000E0A60
	protected override string trait_id
	{
		get
		{
			return "desire_golden_egg";
		}
	}
}
