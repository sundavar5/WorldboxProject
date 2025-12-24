using System;

// Token: 0x02000252 RID: 594
public class BuildingWaypointAlienMold : BuildingWaypoint
{
	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06001643 RID: 5699 RVA: 0x000E282D File Offset: 0x000E0A2D
	protected override string effect_id
	{
		get
		{
			return "fx_waypoint_alien_mold_launch_bottom";
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06001644 RID: 5700 RVA: 0x000E2834 File Offset: 0x000E0A34
	protected override string trait_id
	{
		get
		{
			return "desire_alien_mold";
		}
	}
}
