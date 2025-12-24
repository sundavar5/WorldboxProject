using System;

namespace ai.behaviours
{
	// Token: 0x02000922 RID: 2338
	public class BehBeeReturnHome : BehaviourActionActor
	{
		// Token: 0x060045DD RID: 17885 RVA: 0x001D3E44 File Offset: 0x001D2044
		public override BehResult execute(Actor pActor)
		{
			Building tHomeBuilding = pActor.getHomeBuilding();
			if (tHomeBuilding.isRekt())
			{
				return BehResult.Stop;
			}
			if (Toolbox.DistTile(pActor.current_tile, tHomeBuilding.current_tile) > 3f)
			{
				return BehResult.Stop;
			}
			if (pActor.data.pollen == 3 && pActor.current_tile.building == tHomeBuilding)
			{
				pActor.data.pollen = 0;
				if (pActor.isKingdomCiv())
				{
					pActor.addToInventory("honey", 1);
				}
				else
				{
					tHomeBuilding.component_beehive.addHoney();
				}
				pActor.timer_action = 3f;
			}
			return BehResult.Continue;
		}
	}
}
