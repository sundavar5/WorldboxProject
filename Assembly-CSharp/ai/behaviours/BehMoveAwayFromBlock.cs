using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008EA RID: 2282
	public class BehMoveAwayFromBlock : BehaviourActionActor
	{
		// Token: 0x06004548 RID: 17736 RVA: 0x001D1968 File Offset: 0x001CFB68
		public override BehResult execute(Actor pActor)
		{
			if (pActor.current_tile.region.island.type != TileLayerType.Block)
			{
				return BehResult.Stop;
			}
			WorldTile tBest = null;
			int tBestDist = int.MaxValue;
			tBest = BehMoveAwayFromBlock.getBestTileToMove(pActor.current_tile.region, pActor);
			if (tBest == null)
			{
				foreach (MapRegion pRegion in pActor.current_tile.region.neighbours)
				{
					WorldTile tCornerTile = BehMoveAwayFromBlock.getBestTileToMove(pRegion, pActor);
					if (tCornerTile != null)
					{
						int tDist = Toolbox.SquaredDistTile(pActor.current_tile, tCornerTile);
						if (tDist < tBestDist)
						{
							tBest = tCornerTile;
							tBestDist = tDist;
						}
					}
				}
			}
			if (tBest == null)
			{
				foreach (MapRegion pRegion2 in pActor.current_tile.region.island.insideRegionEdges)
				{
					WorldTile tCornerTile2 = BehMoveAwayFromBlock.getBestTileToMove(pRegion2, pActor);
					if (tCornerTile2 != null)
					{
						int tDist2 = Toolbox.SquaredDistTile(pActor.current_tile, tCornerTile2);
						if (tDist2 < tBestDist)
						{
							tBest = tCornerTile2;
							tBestDist = tDist2;
						}
					}
				}
			}
			if (tBest == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tBest;
			return BehResult.Continue;
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x001D1A98 File Offset: 0x001CFC98
		private static WorldTile getBestTileToMove(MapRegion pRegion, Actor pActor)
		{
			List<WorldTile> tListCorners = pRegion.getEdgeTiles();
			if (tListCorners.Count == 0)
			{
				return null;
			}
			WorldTile tCurrentTile = pActor.current_tile;
			WorldTile tBest = null;
			int tBestDist = int.MaxValue;
			TileIsland tCurrentIsland = tCurrentTile.region.island;
			foreach (WorldTile tCornerTile in tListCorners.LoopRandom<WorldTile>())
			{
				int tDist = Toolbox.SquaredDistTile(tCurrentTile, tCornerTile);
				if (tDist < tBestDist)
				{
					MapRegion tCornerRegion = tCornerTile.region;
					if (BehMoveAwayFromBlock.isGoodTileRegion(tCornerRegion, pActor) && tCornerRegion.island != tCurrentIsland && tCornerRegion.island.getTileCount() > 5)
					{
						tBest = tCornerTile;
						tBestDist = tDist;
					}
				}
			}
			return tBest;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x001D1B54 File Offset: 0x001CFD54
		private static bool isGoodTileRegion(MapRegion pRegion, Actor pActor)
		{
			return (pRegion.type != TileLayerType.Ocean || !pActor.isDamagedByOcean()) && (pRegion.type != TileLayerType.Lava || !pActor.asset.die_in_lava);
		}
	}
}
