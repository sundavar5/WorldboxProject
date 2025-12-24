using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200088A RID: 2186
	public class BehAnimalFindTile : BehaviourActionActor
	{
		// Token: 0x06004459 RID: 17497 RVA: 0x001CDAC0 File Offset: 0x001CBCC0
		public override BehResult execute(Actor pActor)
		{
			if (Randy.randomChance(0.8f))
			{
				using (IEnumerator<Actor> enumerator = Finder.findSpeciesAroundTileChunk(pActor.current_tile, "druid").GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Actor tActor = enumerator.Current;
						pActor.beh_tile_target = tActor.current_tile.region.getRandomTile();
						return BehResult.Continue;
					}
				}
			}
			MapRegion tRegion = pActor.current_tile.region;
			if (tRegion.neighbours.Count > 0 && Randy.randomBool())
			{
				tRegion = tRegion.neighbours.GetRandom<MapRegion>();
			}
			if (tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.getRandomTile();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
