using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000920 RID: 2336
	public class BehBeeCreateHive : BehaviourActionActor
	{
		// Token: 0x060045D7 RID: 17879 RVA: 0x001D3D54 File Offset: 0x001D1F54
		public override BehResult execute(Actor pActor)
		{
			if (BehBeeCreateHive.isAnotherBeehiveNearby(pActor))
			{
				return BehResult.Stop;
			}
			Building tNewBeehive = BehaviourActionBase<Actor>.world.buildings.addBuilding("beehive", pActor.beh_tile_target, true, false, BuildPlacingType.New);
			pActor.beh_building_target = tNewBeehive;
			return BehResult.Continue;
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x001D3D94 File Offset: 0x001D1F94
		public static bool isAnotherBeehiveNearby(Actor pActor)
		{
			using (IEnumerator<Building> enumerator = Finder.getBuildingsFromChunk(pActor.current_tile, 2, 0, false).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.asset.id == "beehive")
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
