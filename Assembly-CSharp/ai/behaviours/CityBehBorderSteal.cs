using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000968 RID: 2408
	public class CityBehBorderSteal : BehaviourActionCity
	{
		// Token: 0x0600468B RID: 18059 RVA: 0x001DED08 File Offset: 0x001DCF08
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemZoneGrowth))
			{
				return BehResult.Stop;
			}
			if (!WorldLawLibrary.world_law_border_stealing.isEnabled())
			{
				return BehResult.Stop;
			}
			if (pCity.status.population == 0)
			{
				return BehResult.Stop;
			}
			if (pCity.buildings.Count == 0)
			{
				return BehResult.Stop;
			}
			int i = 0;
			while (i < 3 && !this.tryStealZone(pCity))
			{
				i++;
			}
			return BehResult.Continue;
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001DED64 File Offset: 0x001DCF64
		private bool tryStealZone(City pCity)
		{
			CityBehBorderSteal._zones.Clear();
			foreach (TileZone tZone in pCity.buildings.GetRandom<Building>().current_tile.zone.neighbours)
			{
				if (tZone.city != pCity && tZone.city != null && !tZone.hasAnyBuildingsInSet(BuildingList.Civs) && tZone.city.kingdom.isEnemy(pCity.kingdom))
				{
					this.stealZone(tZone, pCity);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001DEDE5 File Offset: 0x001DCFE5
		private void stealZone(TileZone pZone, City pCity)
		{
			if (pZone.city != null)
			{
				pZone.city.removeZone(pZone);
			}
			pCity.addZone(pZone);
		}

		// Token: 0x040031F5 RID: 12789
		private static List<TileZone> _zones = new List<TileZone>();
	}
}
