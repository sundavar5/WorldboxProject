using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020003E1 RID: 993
public class CityBehBorderShrink : BehaviourActionCity
{
	// Token: 0x060022AC RID: 8876 RVA: 0x00122B4C File Offset: 0x00120D4C
	public override bool errorsFound(City pCity)
	{
		return false;
	}

	// Token: 0x060022AD RID: 8877 RVA: 0x00122B4F File Offset: 0x00120D4F
	public override bool shouldRetry(City pCity)
	{
		return false;
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x00122B54 File Offset: 0x00120D54
	public override BehResult execute(City pCity)
	{
		if (BehaviourActionBase<City>.world.getWorldTimeElapsedSince(pCity.timestamp_shrink) < SimGlobals.m.empty_city_borders_shrink_time)
		{
			return BehResult.Stop;
		}
		if (pCity.hasUnits())
		{
			return BehResult.Stop;
		}
		TileZone tZoneToRemove = this.getZoneToRemove(pCity);
		if (tZoneToRemove == null)
		{
			return BehResult.Stop;
		}
		pCity.removeZone(tZoneToRemove);
		pCity.timestamp_shrink = BehaviourActionBase<City>.world.getCurWorldTime();
		return BehResult.Continue;
	}

	// Token: 0x060022AF RID: 8879 RVA: 0x00122BB0 File Offset: 0x00120DB0
	private TileZone getZoneToRemove(City pCity)
	{
		TileZone tZoneToRemove = null;
		if (pCity.border_zones.Count > 0)
		{
			tZoneToRemove = this.getRandomZoneFromList(pCity.border_zones);
		}
		else if (pCity.zones.Count > 0)
		{
			tZoneToRemove = this.getRandomZoneFromList(pCity.zones);
		}
		return tZoneToRemove;
	}

	// Token: 0x060022B0 RID: 8880 RVA: 0x00122BF8 File Offset: 0x00120DF8
	private TileZone getRandomZoneFromList(IReadOnlyCollection<TileZone> pList)
	{
		if (pList.Count == 0)
		{
			return null;
		}
		TileZone random;
		using (ListPool<TileZone> tZones = new ListPool<TileZone>(pList))
		{
			random = tZones.GetRandom<TileZone>();
		}
		return random;
	}
}
