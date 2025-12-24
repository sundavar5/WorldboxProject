using System;
using System.Collections.Generic;

// Token: 0x02000323 RID: 803
public class CityZoneGrowth : CityZoneWorkerBase
{
	// Token: 0x06001F02 RID: 7938 RVA: 0x0010E58C File Offset: 0x0010C78C
	public TileZone getZoneToClaim(Actor pActor, City pCity, bool pDebug = false, HashSet<TileZone> pSetToFill = null, int pBonusRange = 0)
	{
		this.clearAll();
		WorldTile tTile = pCity.getTile(false);
		if (tTile == null)
		{
			return null;
		}
		bool tStopWaveWhenEmptyZoneFound = !pDebug;
		this.startWaveFromTile(pActor, tTile, pCity, tStopWaveWhenEmptyZoneFound, pBonusRange);
		if (pDebug && pSetToFill != null)
		{
			foreach (ZoneConnection tConnection in this._zones_checked)
			{
				pSetToFill.Add(tConnection.zone);
			}
			return null;
		}
		return this.checkGrowBorder(pCity);
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0010E61C File Offset: 0x0010C81C
	private TileZone checkGrowBorder(City pCity)
	{
		bool flag = Randy.randomChance(0.7f);
		TileZone tResultZone = null;
		if (flag)
		{
			TileZone tRandomZone = this.getRandomZone(pCity);
			if (tRandomZone != null)
			{
				tResultZone = tRandomZone;
			}
		}
		else
		{
			tResultZone = this.getRandomCheckedZone(pCity);
		}
		return tResultZone;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0010E650 File Offset: 0x0010C850
	private TileZone getRandomZone(City pCity)
	{
		TileZone result;
		using (ListPool<TileZone> tListZonesToCheck = new ListPool<TileZone>(pCity.border_zones))
		{
			WorldTile tMainCityTile = pCity.getTile(false);
			if (tMainCityTile == null)
			{
				result = null;
			}
			else
			{
				TileZone tMainCityZone = tMainCityTile.zone;
				float tMaxRadius = (float)pCity.getZoneRange(true) * 0.75f;
				tMaxRadius *= tMaxRadius;
				foreach (TileZone tileZone in tListZonesToCheck.LoopRandom<TileZone>())
				{
					foreach (TileZone tZone in tileZone.neighbours.LoopRandom<TileZone>())
					{
						if (tZone.canBeClaimedByCity(pCity) && tZone.centerTile.isSameIsland(tMainCityTile) && (float)Toolbox.SquaredDist(tMainCityZone.x, tMainCityZone.y, tZone.x, tZone.y) <= tMaxRadius)
						{
							return tZone;
						}
					}
				}
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x0010E774 File Offset: 0x0010C974
	private TileZone getBestZoneFromList(City pCity, List<TileZone> pList)
	{
		TileZone tResultZone = null;
		TileZone tMainCityZone = pCity.getTile(false).zone;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < pList.Count; i++)
		{
			TileZone tZone = pList[i];
			int tCurrentDist = Toolbox.SquaredDist(tZone.x, tZone.y, tMainCityZone.x, tMainCityZone.y);
			if (tCurrentDist < tBestDist)
			{
				tResultZone = tZone;
				tBestDist = tCurrentDist;
			}
		}
		return tResultZone;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x0010E7E0 File Offset: 0x0010C9E0
	private TileZone getRandomCheckedZone(City pCity)
	{
		TileZone result;
		using (ListPool<TileZone> tListZones = new ListPool<TileZone>(this._zones_checked.Count))
		{
			foreach (ZoneConnection zoneConnection in this._zones_checked)
			{
				TileZone tZone = zoneConnection.zone;
				if (tZone.canBeClaimedByCity(pCity))
				{
					tListZones.Add(tZone);
				}
			}
			if (tListZones.Count > 0)
			{
				result = tListZones.GetRandom<TileZone>();
			}
			else
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x0010E880 File Offset: 0x0010CA80
	private void startWaveFromTile(Actor pActor, WorldTile pTile, City pCity, bool pStopWaveWhenEmptyZoneFound = true, int pBonusRange = 0)
	{
		base.prepareWave();
		if (pActor == null)
		{
			pActor = pCity.leader;
		}
		Queue<ZoneConnection> tWaveQ = this._wave;
		Queue<ZoneConnection> tNextWaveQ = this._next_wave;
		this.queueConnection(new ZoneConnection(pTile.zone, pTile.region), tWaveQ, true);
		using (ListPool<MapRegion> tListRegions = new ListPool<MapRegion>())
		{
			int tMaxWaves = pCity.getZoneRange(true) + pBonusRange;
			float tMaxRadius = (float)tMaxWaves * 0.75f;
			tMaxRadius *= tMaxRadius;
			int tWaveID = 0;
			bool tEmptyZoneFound = false;
			while (tNextWaveQ.Count > 0 || tWaveQ.Count > 0)
			{
				if (pStopWaveWhenEmptyZoneFound && tEmptyZoneFound)
				{
					break;
				}
				if (tWaveQ.Count == 0)
				{
					Queue<ZoneConnection> queue = tWaveQ;
					tWaveQ = tNextWaveQ;
					tNextWaveQ = queue;
					tWaveID++;
					if (tWaveID > tMaxWaves)
					{
						break;
					}
				}
				ZoneConnection zoneConnection = tWaveQ.Dequeue();
				TileZone tMainZone = zoneConnection.zone;
				MapRegion tMainRegion = zoneConnection.region;
				for (int i = 0; i < tMainZone.neighbours.Length; i++)
				{
					TileZone tZone = tMainZone.neighbours[i];
					if ((!pStopWaveWhenEmptyZoneFound || !tZone.hasCity() || tZone.isSameCityHere(pCity)) && tZone.tiles_with_ground != 0 && (pActor == null || !pActor.hasSubspecies() || tZone.checkCanSettleInThisBiomes(pActor.subspecies)))
					{
						tListRegions.Clear();
						if (TileZone.hasZonesConnectedViaRegions(tMainZone, tZone, tMainRegion, tListRegions))
						{
							for (int iReg = 0; iReg < tListRegions.Count; iReg++)
							{
								MapRegion tRegionToCheck = tListRegions[iReg];
								ZoneConnection tNewConnection = new ZoneConnection(tZone, tRegionToCheck);
								if (this._zones_checked.Add(tNewConnection))
								{
									if (tZone.canBeClaimedByCity(pCity))
									{
										tEmptyZoneFound = true;
									}
									if ((float)Toolbox.SquaredDist(pTile.zone.x, pTile.zone.y, tZone.x, tZone.y) <= tMaxRadius)
									{
										if (pStopWaveWhenEmptyZoneFound && tEmptyZoneFound)
										{
											break;
										}
										this.queueConnection(tNewConnection, tNextWaveQ, false);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040016B1 RID: 5809
	private const float MOD_RADIUS = 0.75f;
}
