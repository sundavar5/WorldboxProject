using System;

// Token: 0x02000253 RID: 595
public class BuildingWaypointComputer : BuildingWaypoint
{
	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06001646 RID: 5702 RVA: 0x000E2843 File Offset: 0x000E0A43
	protected override string effect_id
	{
		get
		{
			return "fx_waypoint_computer_launch_bottom";
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06001647 RID: 5703 RVA: 0x000E284A File Offset: 0x000E0A4A
	protected override string trait_id
	{
		get
		{
			return "desire_computer";
		}
	}
}
