using System;
using System.Collections.Generic;

// Token: 0x02000317 RID: 791
public static class ZoneMetaDataVisualizer
{
	// Token: 0x06001D94 RID: 7572 RVA: 0x00107C23 File Offset: 0x00105E23
	public static bool hasZoneData(TileZone pZone)
	{
		return ZoneMetaDataVisualizer.zone_data_dict.ContainsKey(pZone);
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x00107C30 File Offset: 0x00105E30
	public static ZoneMetaData getZoneMetaData(TileZone pZone)
	{
		ZoneMetaData tData;
		ZoneMetaDataVisualizer.zone_data_dict.TryGetValue(pZone, out tData);
		return tData;
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00107C4C File Offset: 0x00105E4C
	public static ListPool<TileZone> getZonesWithMeta(IMetaObject pMeta)
	{
		ListPool<TileZone> tList = new ListPool<TileZone>();
		foreach (ZoneMetaData tData in ZoneMetaDataVisualizer.zone_data_dict.Values)
		{
			if (tData.meta_object == pMeta)
			{
				tList.Add(tData.zone);
			}
		}
		return tList;
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x00107CB8 File Offset: 0x00105EB8
	private static bool shouldUpdateEntry(ZoneMetaData pData, IMetaObject pNewMetaObject)
	{
		IMetaObject tOldMetaObject = pData.meta_object;
		return tOldMetaObject == null || tOldMetaObject.getMetaTypeAsset().map_mode != pNewMetaObject.getMetaTypeAsset().map_mode || pData.previous_priority_amount < pNewMetaObject.countUnits() || tOldMetaObject == pNewMetaObject;
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x00107D04 File Offset: 0x00105F04
	public static void countMetaZone(TileZone pZone, IMetaObject pMetaObject, double pTimestamp)
	{
		ZoneMetaData tData;
		if (ZoneMetaDataVisualizer.zone_data_dict.TryGetValue(pZone, out tData))
		{
			if (ZoneMetaDataVisualizer.shouldUpdateEntry(tData, pMetaObject))
			{
				tData.meta_object = pMetaObject;
				tData.timestamp = pTimestamp;
				tData.previous_priority_amount = pMetaObject.countUnits();
				ZoneMetaDataVisualizer.zone_data_dict[pZone] = tData;
				return;
			}
		}
		else
		{
			tData = new ZoneMetaData
			{
				meta_object = pMetaObject,
				zone = pZone,
				timestamp = pTimestamp,
				timestamp_new = pTimestamp,
				previous_priority_amount = pMetaObject.countUnits()
			};
			ZoneMetaDataVisualizer.zone_data_dict.Add(pZone, tData);
		}
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x00107D96 File Offset: 0x00105F96
	private static void start()
	{
		ZoneMetaDataVisualizer._to_remove.Clear();
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x00107DA4 File Offset: 0x00105FA4
	private static void checkDynamicZones()
	{
		MetaTypeAsset tMetaZoneAsset = World.world.getCachedMapMetaAsset();
		if (tMetaZoneAsset != null && tMetaZoneAsset.map_mode != ZoneMetaDataVisualizer._last_meta_type)
		{
			ZoneMetaDataVisualizer.clearAll();
			ZoneMetaDataVisualizer._last_meta_type = tMetaZoneAsset.map_mode;
		}
		if (tMetaZoneAsset == null)
		{
			return;
		}
		if (!tMetaZoneAsset.has_dynamic_zones)
		{
			return;
		}
		if (!tMetaZoneAsset.isMetaZoneOptionSelectedFluid())
		{
			return;
		}
		tMetaZoneAsset.dynamic_zones();
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x00107E00 File Offset: 0x00106000
	private static void clearOldAndDeadZones()
	{
		double tCurTime = World.world.getCurWorldTime();
		List<TileZone> tToRemove = ZoneMetaDataVisualizer._to_remove;
		foreach (KeyValuePair<TileZone, ZoneMetaData> tPair in ZoneMetaDataVisualizer.zone_data_dict)
		{
			ZoneMetaData tData = tPair.Value;
			if (tData.meta_object == null || !tData.meta_object.isAlive())
			{
				tToRemove.Add(tPair.Key);
			}
			else if (tData.getDiffTime(tCurTime) > 5f)
			{
				tToRemove.Add(tPair.Key);
			}
		}
		foreach (TileZone tZone in tToRemove)
		{
			ZoneMetaDataVisualizer.zone_data_dict.Remove(tZone);
		}
		ZoneMetaDataVisualizer._to_remove.Clear();
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x00107EF8 File Offset: 0x001060F8
	public static void updateMetaZones()
	{
		Bench.bench("fluid_zones_data", "fluid_zones_data_total", false);
		Bench.bench("start", "fluid_zones_data", false);
		ZoneMetaDataVisualizer.start();
		Bench.benchEnd("start", "fluid_zones_data", false, 0L, false);
		Bench.bench("checkDynamicZones", "fluid_zones_data", false);
		ZoneMetaDataVisualizer.checkDynamicZones();
		Bench.benchEnd("checkDynamicZones", "fluid_zones_data", false, 0L, false);
		Bench.bench("clearOldAndDeadZones", "fluid_zones_data", false);
		ZoneMetaDataVisualizer.clearOldAndDeadZones();
		Bench.benchEnd("clearOldAndDeadZones", "fluid_zones_data", false, 0L, false);
		Bench.bench("checkCenterTitles", "fluid_zones_data", false);
		ZoneMetaDataVisualizer.checkCenterTitles();
		Bench.benchEnd("checkCenterTitles", "fluid_zones_data", false, 0L, false);
		Bench.benchEnd("fluid_zones_data", "fluid_zones_data_total", false, 0L, false);
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x00107FD4 File Offset: 0x001061D4
	private static void checkCenterTitles()
	{
		foreach (Culture culture in World.world.cultures)
		{
			culture.updateTitleCenter();
		}
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x00108024 File Offset: 0x00106224
	public static void clearAll()
	{
		ZoneMetaDataVisualizer.zone_data_dict.Clear();
	}

	// Token: 0x04001624 RID: 5668
	public const float FADE_TIME = 5f;

	// Token: 0x04001625 RID: 5669
	public static readonly Dictionary<TileZone, ZoneMetaData> zone_data_dict = new Dictionary<TileZone, ZoneMetaData>();

	// Token: 0x04001626 RID: 5670
	private static readonly List<TileZone> _to_remove = new List<TileZone>();

	// Token: 0x04001627 RID: 5671
	private static MetaType _last_meta_type = MetaType.None;
}
