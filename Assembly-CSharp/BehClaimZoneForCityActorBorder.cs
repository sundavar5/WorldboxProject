using System;
using ai.behaviours;

// Token: 0x02000396 RID: 918
public class BehClaimZoneForCityActorBorder : BehCityActor
{
	// Token: 0x060021B5 RID: 8629 RVA: 0x0011D316 File Offset: 0x0011B516
	public override BehResult execute(Actor pActor)
	{
		return BehClaimZoneForCityActorBorder.tryClaimZone(pActor);
	}

	// Token: 0x060021B6 RID: 8630 RVA: 0x0011D320 File Offset: 0x0011B520
	public static BehResult tryClaimZone(Actor pActor)
	{
		TileZone tCurZone = pActor.current_tile.zone;
		City tCity = pActor.city;
		WorldTile tCityTile = tCity.getTile(false);
		if (tCityTile == null)
		{
			return BehResult.Stop;
		}
		if (!tCity.isZoneToClaimStillGood(pActor, tCurZone, tCityTile))
		{
			return BehResult.Stop;
		}
		bool tGrabZonesAround = pActor.hasCultureTrait("expansionists") || DebugConfig.isOn(DebugOption.CityFastZonesGrowth);
		bool flag = tCurZone.city != null && tCurZone.city != tCity;
		tCity.addZone(tCurZone);
		if (flag)
		{
			tGrabZonesAround = false;
		}
		if (tGrabZonesAround)
		{
			foreach (TileZone tZone in tCurZone.neighbours_all)
			{
				if (!tZone.hasCity() && tZone.centerTile.isSameIsland(tCityTile) && tCity.isZoneToClaimStillGood(pActor, tZone, tCityTile))
				{
					tCity.addZone(tZone);
				}
			}
		}
		pActor.addLoot(SimGlobals.m.coins_for_zone);
		return BehResult.Continue;
	}
}
