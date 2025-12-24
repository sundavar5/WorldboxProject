using System;

namespace ai.behaviours
{
	// Token: 0x020008D2 RID: 2258
	public class BehFindTile : BehaviourActionActor
	{
		// Token: 0x06004509 RID: 17673 RVA: 0x001D0590 File Offset: 0x001CE790
		public BehFindTile(TileFinderType pType)
		{
			this._type = pType;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x001D05A0 File Offset: 0x001CE7A0
		public override BehResult execute(Actor pActor)
		{
			if (this._type == TileFinderType.NewRoad)
			{
				using (ListPool<WorldTile> tTiles = new ListPool<WorldTile>(5))
				{
					WorldTile tTile = pActor.city.getRoadTileToBuild(pActor);
					if (tTile != null)
					{
						tTiles.Add(tTile);
					}
					if (tTiles.Count == 0)
					{
						return BehResult.Stop;
					}
					pActor.beh_tile_target = Randy.getRandom<WorldTile>(tTiles);
					return BehResult.Continue;
				}
			}
			WorldTile tTile2 = Finder.findTileInChunk(pActor.current_tile, this._type);
			if (tTile2 != null)
			{
				pActor.beh_tile_target = tTile2;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x04003185 RID: 12677
		private TileFinderType _type;
	}
}
