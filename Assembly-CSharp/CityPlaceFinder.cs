using System;
using System.Collections.Generic;

// Token: 0x02000321 RID: 801
public class CityPlaceFinder : CityZoneWorkerBase
{
	// Token: 0x06001EEB RID: 7915 RVA: 0x0010DD2C File Offset: 0x0010BF2C
	internal bool isDirty()
	{
		return DebugConfig.isOn(DebugOption.SystemCityPlaceFinder) && this._dirty;
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x0010DD3F File Offset: 0x0010BF3F
	internal void recalc()
	{
		if (!this.isDirty())
		{
			return;
		}
		this._dirty = false;
		this.clearAll();
		this.prepareBasicZones();
		this.prepareQueueFromCities();
		this.startWave();
		this.createFinalList();
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x0010DD70 File Offset: 0x0010BF70
	internal override void clearAll()
	{
		base.clearAll();
		this.zones.Clear();
		List<TileZone> tZones = World.world.zone_calculator.zones;
		for (int i = 0; i < tZones.Count; i++)
		{
			tZones[i].setGoodForNewCity(true);
		}
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x0010DDBC File Offset: 0x0010BFBC
	private void prepareBasicZones()
	{
		List<TileZone> tZones = World.world.zone_calculator.zones;
		for (int i = 0; i < tZones.Count; i++)
		{
			TileZone tZone = tZones[i];
			if (!tZone.canStartCityHere())
			{
				tZone.setGoodForNewCity(false);
			}
			else if (tZone.centerTile.region.island.getTileCount() < 300)
			{
				tZone.setGoodForNewCity(false);
			}
		}
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x0010DE28 File Offset: 0x0010C028
	private void prepareQueueFromCities()
	{
		base.prepareWave();
		foreach (City tCity in World.world.cities)
		{
			this.checkCity(tCity, this._wave);
		}
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x0010DE88 File Offset: 0x0010C088
	private void checkCity(City pCity, Queue<ZoneConnection> pWaveQ)
	{
		WorldTile tCityTile = pCity.getTile(false);
		if (tCityTile == null)
		{
			return;
		}
		TileIsland tCityIsland = tCityTile.region.island;
		foreach (TileZone tZone in pCity.border_zones)
		{
			for (int i = 0; i < tZone.centerTile.chunk.regions.Count; i++)
			{
				MapRegion tRegion = tZone.chunk.regions[i];
				if (tRegion.isTypeGround() && tRegion.zones.Contains(tZone) && tRegion.island == tCityIsland)
				{
					this.queueConnection(new ZoneConnection(tZone, tRegion), pWaveQ, true);
				}
			}
		}
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x0010DF58 File Offset: 0x0010C158
	private void startWave()
	{
		Queue<ZoneConnection> tWaveQ = this._wave;
		Queue<ZoneConnection> tNextWaveQ = this._next_wave;
		using (ListPool<MapRegion> tListRegions = new ListPool<MapRegion>())
		{
			int tMaxWave = 3;
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
				if (tWaveID > tMaxWave)
				{
					break;
				}
				ZoneConnection zoneConnection = tWaveQ.Dequeue();
				TileZone tMainZone = zoneConnection.zone;
				MapRegion tMainRegion = zoneConnection.region;
				foreach (TileZone tZone in tMainZone.neighbours)
				{
					if (!tZone.hasCity())
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
									tZone.setGoodForNewCity(false);
									this.queueConnection(tNewConnection, tNextWaveQ, false);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x0010E074 File Offset: 0x0010C274
	private void createFinalList()
	{
		for (int i = 0; i < World.world.zone_calculator.zones.Count; i++)
		{
			TileZone tZone = World.world.zone_calculator.zones[i];
			if (tZone.isGoodForNewCity())
			{
				this.zones.Add(tZone);
			}
		}
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x0010E0CA File Offset: 0x0010C2CA
	public bool hasPossibleZones()
	{
		return !this._dirty && this.zones.Count > 0;
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x0010E0E4 File Offset: 0x0010C2E4
	internal void setDirty()
	{
		this._dirty = true;
		this.clearCurrentZones();
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x0010E0F4 File Offset: 0x0010C2F4
	private void clearCurrentZones()
	{
		if (this.zones.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].setGoodForNewCity(false);
		}
		this.zones.Clear();
	}

	// Token: 0x040016AD RID: 5805
	private bool _dirty;

	// Token: 0x040016AE RID: 5806
	internal List<TileZone> zones = new List<TileZone>();
}
