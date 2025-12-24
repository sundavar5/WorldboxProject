using System;

namespace ai.behaviours
{
	// Token: 0x0200092B RID: 2347
	public class BehBoatFindOceanNeutralTile : BehBoat
	{
		// Token: 0x060045F1 RID: 17905 RVA: 0x001D40F8 File Offset: 0x001D22F8
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			Building tHomeBuilding = this.boat.actor.getHomeBuilding();
			if (tHomeBuilding != null)
			{
				if (pActor.getSimpleComponent<Boat>().isNearDock())
				{
					return BehResult.Stop;
				}
				WorldTile tWaterTileFromDock = tHomeBuilding.component_docks.getOceanTileInSameOcean(pActor.current_tile);
				if (tWaterTileFromDock != null)
				{
					pActor.beh_tile_target = tWaterTileFromDock;
					return BehResult.Continue;
				}
			}
			WorldTile tTile = ActorTool.getRandomTileForBoat(pActor);
			if (tTile == null)
			{
				return BehResult.Stop;
			}
			if (tTile.zone.city != null && tTile.zone.city.kingdom.isEnemy(pActor.kingdom))
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tTile;
			return BehResult.Continue;
		}
	}
}
