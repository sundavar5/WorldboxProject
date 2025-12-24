using System;

namespace ai.behaviours
{
	// Token: 0x0200096B RID: 2411
	public class CityBehCheckAttackZone : BehaviourActionCity
	{
		// Token: 0x060046B5 RID: 18101 RVA: 0x001E0078 File Offset: 0x001DE278
		public override BehResult execute(City pCity)
		{
			City tTargetCity = pCity.target_attack_city;
			bool tTargetStillOk = true;
			if (tTargetCity == null)
			{
				tTargetStillOk = false;
			}
			else if (!tTargetCity.isAlive() || !pCity.hasAnyWarriors() || !tTargetCity.kingdom.isEnemy(pCity.kingdom) || !tTargetCity.reachableFrom(pCity))
			{
				tTargetStillOk = false;
			}
			if (!tTargetStillOk)
			{
				pCity.target_attack_city = null;
				pCity.target_attack_zone = null;
				tTargetCity = null;
			}
			if (pCity.target_attack_city != null && pCity.target_attack_zone.city != pCity.target_attack_city)
			{
				pCity.target_attack_zone = null;
			}
			if (pCity.hasAttackZoneOrder())
			{
				return BehResult.Continue;
			}
			if (tTargetCity == null)
			{
				if (!pCity.isOkToSendArmy())
				{
					return BehResult.Continue;
				}
				tTargetCity = this.findTargetCity(pCity);
			}
			if (tTargetCity == null)
			{
				return BehResult.Continue;
			}
			pCity.target_attack_city = tTargetCity;
			if (tTargetCity.buildings.Count > 0)
			{
				Building tBuilding = tTargetCity.buildings.GetRandom<Building>();
				pCity.target_attack_zone = tBuilding.current_tile.zone;
			}
			else if (pCity.hasZones())
			{
				pCity.target_attack_zone = pCity.zones.GetRandom<TileZone>();
			}
			return BehResult.Continue;
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001E016C File Offset: 0x001DE36C
		private City findTargetCity(City pOurCity)
		{
			Kingdom pK = pOurCity.kingdom;
			City tBestTarget = null;
			float tBestDist = float.MaxValue;
			City result;
			using (ListPool<City> tListCities = new ListPool<City>(BehaviourActionBase<City>.world.cities.Count))
			{
				BehaviourActionBase<City>.world.wars.getWarCities(pK, tListCities);
				foreach (City ptr in tListCities)
				{
					City tPotentialCity = ptr;
					float tDist = Toolbox.SquaredDistVec2Float(tPotentialCity.city_center, pOurCity.city_center);
					if (tDist < tBestDist && tPotentialCity.reachableFrom(pOurCity))
					{
						tBestTarget = tPotentialCity;
						tBestDist = tDist;
					}
				}
				result = tBestTarget;
			}
			return result;
		}
	}
}
