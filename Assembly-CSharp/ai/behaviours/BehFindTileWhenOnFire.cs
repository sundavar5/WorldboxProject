using System;

namespace ai.behaviours
{
	// Token: 0x020008D8 RID: 2264
	public class BehFindTileWhenOnFire : BehaviourActionActor
	{
		// Token: 0x06004517 RID: 17687 RVA: 0x001D0B08 File Offset: 0x001CED08
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTile = this.findWaterIn(pActor.chunk);
			if (tTile == null)
			{
				foreach (MapChunk tChunk in pActor.chunk.neighbours_all)
				{
					tTile = this.findWaterIn(tChunk);
					if (tTile != null)
					{
						break;
					}
				}
			}
			if (tTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tTile;
			return BehResult.Continue;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x001D0B5C File Offset: 0x001CED5C
		private WorldTile findWaterIn(MapChunk pChunk)
		{
			foreach (MapRegion tRegion in pChunk.regions.LoopRandom<MapRegion>())
			{
				if (tRegion.type == TileLayerType.Ocean)
				{
					return tRegion.tiles.GetRandom<WorldTile>();
				}
			}
			return null;
		}
	}
}
