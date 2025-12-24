using System;

namespace ai.behaviours
{
	// Token: 0x020008E7 RID: 2279
	public class BehMagicMakeSkeleton : BehaviourActionActor
	{
		// Token: 0x06004541 RID: 17729 RVA: 0x001D17EC File Offset: 0x001CF9EC
		public override BehResult execute(Actor pActor)
		{
			int tCount = 0;
			foreach (Actor actor in Finder.findSpeciesAroundTileChunk(pActor.current_tile, "skeleton"))
			{
				if (tCount++ > 6)
				{
					return BehResult.Stop;
				}
			}
			WorldTile current_tile = pActor.current_tile;
			WorldTile worldTile;
			if (current_tile == null)
			{
				worldTile = null;
			}
			else
			{
				MapRegion region = current_tile.region;
				worldTile = ((region != null) ? region.tiles.GetRandom<WorldTile>() : null);
			}
			WorldTile tTileTarget = worldTile;
			if (tTileTarget == null)
			{
				return BehResult.Stop;
			}
			if (tTileTarget.hasUnits())
			{
				return BehResult.Stop;
			}
			pActor.doCastAnimation();
			ActionLibrary.spawnSkeleton(pActor, tTileTarget);
			return BehResult.Continue;
		}
	}
}
