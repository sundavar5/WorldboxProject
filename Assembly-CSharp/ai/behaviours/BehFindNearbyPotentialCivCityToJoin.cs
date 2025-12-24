using System;

namespace ai.behaviours
{
	// Token: 0x020008C9 RID: 2249
	public class BehFindNearbyPotentialCivCityToJoin : BehaviourActionActor
	{
		// Token: 0x060044F6 RID: 17654 RVA: 0x001CFF3C File Offset: 0x001CE13C
		public override BehResult execute(Actor pActor)
		{
			City tCity = BehFindNearbyPotentialCivCityToJoin.getCityToJoin(pActor);
			if (tCity == null)
			{
				return BehResult.Stop;
			}
			if (tCity.kingdom != pActor.kingdom)
			{
				pActor.removeFromPreviousFaction();
			}
			pActor.stopBeingWarrior();
			pActor.joinCity(tCity);
			return BehResult.Continue;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x001CFF78 File Offset: 0x001CE178
		private static City getCityToJoin(Actor pActor)
		{
			City tZoneCity = pActor.current_tile.zone_city;
			if (tZoneCity != null && !tZoneCity.isNeutral() && tZoneCity.isWelcomedToJoin(pActor))
			{
				return tZoneCity;
			}
			return BehFindNearbyPotentialCivCityToJoin.getPotentialCityNearby(pActor.current_tile, pActor);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x001CFFB4 File Offset: 0x001CE1B4
		public static City getPotentialCityNearby(WorldTile pFromTile, Actor pActor)
		{
			City tBestCity = null;
			foreach (City tCity in BehaviourActionBase<Actor>.world.cities.list.LoopRandom<City>())
			{
				WorldTile tCityTile = tCity.getTile(false);
				if (tCityTile != null && !tCity.isNeutral())
				{
					float tMaxDistance = SimGlobals.m.nomad_check_far_city_range;
					if (!pActor.isNomad() && tCity.kingdom != pActor.kingdom)
					{
						tMaxDistance *= 3f;
					}
					if (Toolbox.DistVec3(pFromTile.posV, tCity.city_center) <= tMaxDistance && tCity.isWelcomedToJoin(pActor) && tCityTile.isSameIsland(pFromTile))
					{
						if (tBestCity != null)
						{
							if (tCity.kingdom == pActor.kingdom && tBestCity.kingdom != pActor.kingdom)
							{
								tBestCity = tCity;
							}
						}
						else
						{
							tBestCity = tCity;
						}
					}
				}
			}
			return tBestCity;
		}
	}
}
