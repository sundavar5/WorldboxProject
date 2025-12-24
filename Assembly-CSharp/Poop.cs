using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class Poop : BaseBuildingComponent
{
	// Token: 0x0600162E RID: 5678 RVA: 0x000E2370 File Offset: 0x000E0570
	public override void update(float pElapsed)
	{
		if (Time.frameCount % 30 != 0)
		{
			return;
		}
		if (Date.getYearsSince(this.building.data.created_time) < 1)
		{
			return;
		}
		BuildingActions.tryGrowVegetationRandom(this.building.current_tile, VegetationType.Plants, false, false, false);
		this.building.startDestroyBuilding();
	}
}
