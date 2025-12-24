using System;

namespace ai.behaviours
{
	// Token: 0x020008B9 RID: 2233
	public class BehFamilyAlphaMove : BehaviourActionActor
	{
		// Token: 0x060044D2 RID: 17618 RVA: 0x001CF4E4 File Offset: 0x001CD6E4
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTile = null;
			if (pActor.isHerbivore())
			{
				tTile = this.findTileForHerbivore(pActor);
			}
			else if (pActor.isCarnivore())
			{
				tTile = this.findTileForCarnivore(pActor);
			}
			if (tTile != null)
			{
				tTile = tTile.region.tiles.GetRandom<WorldTile>();
			}
			if (tTile == null)
			{
				return base.forceTask(pActor, "random_move", true, false);
			}
			pActor.beh_tile_target = tTile.region.tiles.GetRandom<WorldTile>();
			return BehResult.Continue;
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x001CF554 File Offset: 0x001CD754
		private Building getNearbyBuildings(WorldTile pTile)
		{
			float tBestDist = float.MaxValue;
			Building tBestObject = null;
			foreach (Building tBuilding in Finder.getBuildingsFromChunk(pTile, 3, 0, true))
			{
				float tDist = (float)Toolbox.SquaredDistTile(tBuilding.current_tile, pTile);
				if (tDist < tBestDist && tBuilding.asset.flora && tBuilding.current_tile.isSameIsland(pTile))
				{
					tBestObject = tBuilding;
					tBestDist = tDist;
					if (tBestDist < 25f)
					{
						return tBestObject;
					}
				}
			}
			return tBestObject;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x001CF5EC File Offset: 0x001CD7EC
		private Actor getNearbyActor(Actor pActor, WorldTile pTile)
		{
			float tBestDist = float.MaxValue;
			Actor tBestObject = null;
			foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 3, 0f, true))
			{
				float tDist = (float)Toolbox.SquaredDistTile(tActor.current_tile, pTile);
				if (tDist < tBestDist && tActor.family != pActor.family && !tActor.isSameSpecies(pActor) && tActor.current_tile.isSameIsland(pTile) && tActor.asset.source_meat)
				{
					tBestObject = tActor;
					tBestDist = tDist;
					if (tBestDist < 5f)
					{
						return tBestObject;
					}
				}
			}
			return tBestObject;
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x001CF6A0 File Offset: 0x001CD8A0
		private WorldTile findTileForHerbivore(Actor pActor)
		{
			Building tBuilding = this.getNearbyBuildings(pActor.current_tile);
			if (tBuilding != null)
			{
				return tBuilding.current_tile.region.tiles.GetRandom<WorldTile>();
			}
			return null;
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x001CF6D4 File Offset: 0x001CD8D4
		private WorldTile findTileForCarnivore(Actor pActor)
		{
			WorldTile tCurrentTile = pActor.current_tile;
			Actor tActor = this.getNearbyActor(pActor, tCurrentTile);
			if (tActor != null)
			{
				return tActor.current_tile.region.tiles.GetRandom<WorldTile>();
			}
			if (tActor == null)
			{
				return tCurrentTile.region.island.getRandomTile();
			}
			return null;
		}
	}
}
