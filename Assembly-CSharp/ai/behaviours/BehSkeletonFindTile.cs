using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008FD RID: 2301
	public class BehSkeletonFindTile : BehaviourActionActor
	{
		// Token: 0x06004572 RID: 17778 RVA: 0x001D2358 File Offset: 0x001D0558
		public override BehResult execute(Actor pActor)
		{
			using (IEnumerator<Actor> enumerator = Finder.findSpeciesAroundTileChunk(pActor.current_tile, "necromancer").GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Actor tActor = enumerator.Current;
					pActor.beh_tile_target = tActor.current_tile.region.tiles.GetRandom<WorldTile>();
					return BehResult.Continue;
				}
			}
			MapRegion tRegion = pActor.current_tile.region;
			if (tRegion.neighbours.Count > 0 && Randy.randomBool())
			{
				tRegion = tRegion.neighbours.GetRandom<MapRegion>();
			}
			if (tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
