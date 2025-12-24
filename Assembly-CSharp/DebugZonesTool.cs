using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public static class DebugZonesTool
{
	// Token: 0x06001F10 RID: 7952 RVA: 0x0010EB70 File Offset: 0x0010CD70
	public static void actionGrowBorder()
	{
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile == null)
		{
			return;
		}
		TileZone tMainZone = tCursorTile.zone;
		if (!tMainZone.hasCity())
		{
			return;
		}
		World.world.city_zone_helper.city_growth.getZoneToClaim(null, tMainZone.city, false, null, 0);
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x0010EBBC File Offset: 0x0010CDBC
	public static void actionAbandonZones()
	{
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile == null)
		{
			return;
		}
		TileZone tMainZone = tCursorTile.zone;
		if (!tMainZone.hasCity())
		{
			return;
		}
		Bench.bench("abandon_stuff", "meh", false);
		World.world.city_zone_helper.city_abandon.check(tMainZone.city, true, null);
		Debug.Log("bench abandon: " + Bench.benchEnd("abandon_stuff", "meh", false, 0L, false).ToString());
	}
}
