using System;
using System.Collections.Generic;

// Token: 0x02000322 RID: 802
public class CityZoneAbandon : CityZoneWorkerBase
{
	// Token: 0x06001EF7 RID: 7927 RVA: 0x0010E160 File Offset: 0x0010C360
	public void checkCities()
	{
		foreach (City city in World.world.cities)
		{
			city.checkAbandon();
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x0010E1B0 File Offset: 0x0010C3B0
	public void check(City pCity, bool pDebug = false, HashSet<TileZone> pSetToFill = null)
	{
		if (pCity.buildings.Count == 0)
		{
			return;
		}
		this.clearAll();
		this.prepareCityZones(pCity);
		this.startCheckingFromBuildings(pCity);
		this._split_areas.Sort(new Comparison<ListPool<TileZone>>(CityZoneAbandon.sorter));
		if (pDebug)
		{
			return;
		}
		this.abandonLeftoverZones(pCity);
		if (this._split_areas.Count >= 2)
		{
			this._split_areas[0].Dispose();
			this._split_areas.RemoveAt(0);
			if (this._split_areas.Count > 0)
			{
				this.abandonSmallAreas(pCity);
			}
		}
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x0010E244 File Offset: 0x0010C444
	private void startCheckingFromBuildings(City pCity)
	{
		for (int i = 0; i < pCity.buildings.Count; i++)
		{
			Building building = pCity.buildings[i];
			WorldTile tTile = building.current_tile;
			if (!building.asset.docks)
			{
				this.startWaveFromTile(tTile, pCity);
			}
		}
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x0010E290 File Offset: 0x0010C490
	private void startWaveFromTile(WorldTile pTile, City pCity)
	{
		if (!this._zones_to_check.Contains(pTile.zone))
		{
			return;
		}
		base.prepareWave();
		Queue<ZoneConnection> tWaveQ = this._wave;
		Queue<ZoneConnection> tNextWaveQ = this._next_wave;
		ListPool<TileZone> tNewArea = new ListPool<TileZone>(this._wave.Count + this._next_wave.Count);
		this._split_areas.Add(tNewArea);
		this.queueConnection(new ZoneConnection(pTile.zone, pTile.region), tWaveQ, true);
		using (ListPool<MapRegion> tListRegions = new ListPool<MapRegion>())
		{
			int tWaveID = 0;
			while (tNextWaveQ.Count > 0 || tWaveQ.Count > 0)
			{
				if (tWaveQ.Count == 0)
				{
					Queue<ZoneConnection> queue = tWaveQ;
					tWaveQ = tNextWaveQ;
					tNextWaveQ = queue;
					tWaveID++;
				}
				ZoneConnection zoneConnection = tWaveQ.Dequeue();
				TileZone tMainZone = zoneConnection.zone;
				MapRegion tMainRegion = zoneConnection.region;
				if (tMainZone.isSameCityHere(pCity))
				{
					tNewArea.Add(tMainZone);
				}
				foreach (TileZone tZone in tMainZone.neighbours)
				{
					if (tZone.isSameCityHere(pCity) && tZone.tiles_with_ground != 0)
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
									this.queueConnection(tNewConnection, tNextWaveQ, false);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x0010E410 File Offset: 0x0010C610
	private void abandonLeftoverZones(City pCity)
	{
		if (this._zones_to_check.Count == 0)
		{
			return;
		}
		foreach (TileZone tZone in this._zones_to_check)
		{
			pCity.removeZone(tZone);
		}
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x0010E474 File Offset: 0x0010C674
	private void abandonSmallAreas(City pCity)
	{
		for (int i = 0; i < this._split_areas.Count; i++)
		{
			ListPool<TileZone> tList = this._split_areas[i];
			for (int j = 0; j < tList.Count; j++)
			{
				TileZone tZone = tList[j];
				pCity.removeZone(tZone);
			}
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x0010E4C4 File Offset: 0x0010C6C4
	private void prepareCityZones(City pCity)
	{
		this._zones_to_check.UnionWith(pCity.zones);
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0010E4D8 File Offset: 0x0010C6D8
	internal override void clearAll()
	{
		base.clearAll();
		foreach (ListPool<TileZone> listPool in this._split_areas)
		{
			listPool.Dispose();
		}
		this._split_areas.Clear();
		this._zones_to_check.Clear();
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0010E544 File Offset: 0x0010C744
	private static int sorter(ListPool<TileZone> pList1, ListPool<TileZone> pList2)
	{
		return pList2.Count.CompareTo(pList1.Count);
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x0010E565 File Offset: 0x0010C765
	protected override void queueConnection(ZoneConnection pConnection, Queue<ZoneConnection> pWave, bool pCheck = false)
	{
		base.queueConnection(pConnection, pWave, pCheck);
		this._zones_to_check.Remove(pConnection.zone);
	}

	// Token: 0x040016AF RID: 5807
	private List<ListPool<TileZone>> _split_areas = new List<ListPool<TileZone>>();

	// Token: 0x040016B0 RID: 5808
	private HashSetTileZone _zones_to_check = new HashSetTileZone();
}
