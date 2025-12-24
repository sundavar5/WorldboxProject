using System;

namespace ai.behaviours
{
	// Token: 0x02000967 RID: 2407
	public class BehCityActorCheckAttack : BehCityActor
	{
		// Token: 0x06004688 RID: 18056 RVA: 0x001DEB64 File Offset: 0x001DCD64
		public override BehResult execute(Actor pActor)
		{
			TileZone tAttackZone = pActor.city.target_attack_zone;
			City tAttackedCity = pActor.city.target_attack_zone.city;
			if (!this.isAttackingZoneAvailable(pActor, tAttackZone, tAttackedCity))
			{
				return BehResult.Stop;
			}
			if (pActor.current_tile.zone.city != tAttackZone.city)
			{
				pActor.beh_tile_target = tAttackZone.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			Building tTower = tAttackedCity.getBuildingOfType("type_watch_tower", false, false, false, pActor.current_island);
			if (tTower != null)
			{
				pActor.beh_tile_target = tTower.current_tile.region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			foreach (TileZone tZone in pActor.current_tile.zone.neighbours_all)
			{
				if (tZone.city == tAttackedCity)
				{
					WorldTile tTile = tZone.tiles.GetRandom<WorldTile>();
					if (tTile.isSameIsland(pActor.current_tile))
					{
						pActor.beh_tile_target = tTile;
						return BehResult.Continue;
					}
				}
			}
			foreach (TileZone tileZone in tAttackedCity.zones)
			{
				WorldTile tTile2 = tileZone.tiles.GetRandom<WorldTile>();
				if (tTile2.isSameIsland(pActor.current_tile))
				{
					pActor.beh_tile_target = tTile2;
					return BehResult.Continue;
				}
			}
			return BehResult.Stop;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x001DECC0 File Offset: 0x001DCEC0
		private bool isAttackingZoneAvailable(Actor pActor, TileZone pAttackZone, City pAttackCity)
		{
			return (!pActor.army.isGroupInCityAndHaveLeader() || pActor.city.isOkToSendArmy()) && pAttackCity != null && pAttackZone != null && pAttackZone.centerTile.isSameIsland(pActor.current_tile);
		}
	}
}
