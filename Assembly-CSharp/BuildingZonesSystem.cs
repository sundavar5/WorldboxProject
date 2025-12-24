using System;
using System.Collections.Generic;

// Token: 0x020001C7 RID: 455
public class BuildingZonesSystem
{
	// Token: 0x06000D5C RID: 3420 RVA: 0x000BBC98 File Offset: 0x000B9E98
	public static void setDirty()
	{
		BuildingZonesSystem._dirty = true;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x000BBCA0 File Offset: 0x000B9EA0
	public static void update()
	{
		if (!BuildingZonesSystem._dirty)
		{
			return;
		}
		BuildingZonesSystem._dirty = false;
		List<TileZone> tZones = World.world.zone_calculator.zones;
		using (ListPool<TileZone> tDirtyZones = new ListPool<TileZone>())
		{
			for (int i = 0; i < tZones.Count; i++)
			{
				TileZone tZone = tZones[i];
				if (tZone.isDirty())
				{
					tDirtyZones.Add(tZone);
				}
			}
			for (int j = 0; j < tDirtyZones.Count; j++)
			{
				TileZone tZone2 = tDirtyZones[j];
				tZone2.clearBuildingLists();
				tZone2.setDirty(false);
				foreach (Building tBuilding in tZone2.buildings_all)
				{
					if (!tBuilding.isOnRemove() && !tBuilding.isRemoved())
					{
						if (tBuilding.current_tile.zone == tZone2)
						{
							tZone2.buildings_render_list.Add(tBuilding);
						}
						tZone2.addBuildingToSet(tBuilding);
						if (tBuilding.asset.city_building && !tZone2.hasCity())
						{
							if (tBuilding.isCiv())
							{
								tBuilding.makeAbandoned();
							}
							else
							{
								tBuilding.makeAbandoned();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04000CB4 RID: 3252
	private static bool _dirty;
}
