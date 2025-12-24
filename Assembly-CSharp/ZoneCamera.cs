using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
public class ZoneCamera
{
	// Token: 0x060028B6 RID: 10422 RVA: 0x00146388 File Offset: 0x00144588
	public ZoneCamera()
	{
		this._zone_manager = World.world.zone_calculator;
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x001463F0 File Offset: 0x001445F0
	private void calculateBounds(out int pResultStartX, out int pResultStartY, out int pResultWidth, out int pResultHeight, out int pResultMainX)
	{
		int tBonusHorizontal = 0;
		int tBonusVerticalTop = 0;
		int tBonusVerticalBottom = 0;
		tBonusHorizontal++;
		tBonusVerticalBottom++;
		TileZone tZoneBL = this.getZoneWithinCamera(0, 0, 0f);
		TileZone tZoneBR = this.getZoneWithinCamera(1, 0, 0f);
		TileZone tZoneTR = this.getZoneWithinCamera(0, 1, 0f);
		TileZone tZoneTP = this.getZoneWithinCamera(1, 1, 0f);
		int tStartX;
		if (tZoneTR.x > tZoneBL.x)
		{
			tStartX = tZoneTR.x;
		}
		else
		{
			tStartX = tZoneBL.x;
		}
		int tStartY;
		if (tZoneBR.y > tZoneBL.y)
		{
			tStartY = tZoneBR.y;
		}
		else
		{
			tStartY = tZoneBL.y;
		}
		tStartX -= tBonusHorizontal;
		tStartY -= tBonusVerticalTop;
		int tWidth = tZoneTP.x - tStartX + tBonusHorizontal;
		int tHeight = tZoneTP.y - tStartY + tBonusVerticalBottom;
		if (tZoneBR.x < tZoneTP.x)
		{
			tWidth = tZoneBR.x - tStartX;
		}
		if (tZoneTR.y < tZoneTP.y)
		{
			tHeight = tZoneTR.y - tStartY;
		}
		int tMainX = tStartX;
		if (tMainX > 0)
		{
			tMainX++;
		}
		if (tStartX < 0)
		{
			tStartX = 0;
		}
		if (tStartY < 0)
		{
			tStartY = 0;
		}
		if (tWidth > this._zone_manager.zones_total_x)
		{
			tWidth = this._zone_manager.zones_total_x;
		}
		if (tHeight > this._zone_manager.zones_total_y)
		{
			tHeight = this._zone_manager.zones_total_y;
		}
		pResultStartX = tStartX;
		pResultStartY = tStartY;
		pResultWidth = tWidth;
		pResultHeight = tHeight;
		pResultMainX = tMainX;
	}

	// Token: 0x060028B8 RID: 10424 RVA: 0x00146558 File Offset: 0x00144758
	internal void update()
	{
		Bench.bench("zone_camera", "zone_camera_total", false);
		Bench.bench("calc_bounds", "zone_camera", false);
		int tStartX;
		int tStartY;
		int tWidth;
		int tHeight;
		int tMainX;
		this.calculateBounds(out tStartX, out tStartY, out tWidth, out tHeight, out tMainX);
		Bench.benchEnd("calc_bounds", "zone_camera", false, 0L, false);
		if (tStartX == this._last_start_x && tStartY == this._last_start_y && tWidth == this._last_width && tHeight == this._last_height && tMainX == this._last_main_x)
		{
			return;
		}
		this._last_start_x = tStartX;
		this._last_start_y = tStartY;
		this._last_width = tWidth;
		this._last_height = tHeight;
		this._last_main_x = tMainX;
		Bench.bench("clear", "zone_camera", false);
		this.clear();
		Bench.benchEnd("clear", "zone_camera", false, 0L, false);
		Bench.bench("fill", "zone_camera", false);
		this.fillVisibleZones(tStartX, tStartY, tWidth, tHeight, tMainX);
		Bench.benchEnd("fill", "zone_camera", false, 0L, false);
		Bench.benchEnd("zone_camera", "zone_camera_total", false, 0L, false);
	}

	// Token: 0x060028B9 RID: 10425 RVA: 0x00146670 File Offset: 0x00144870
	private void fillVisibleZones(int pStartX, int pStartY, int pWidth, int pHeight, int pMainX)
	{
		int tTotalZonesX = this._zone_manager.zones_total_x;
		int tTotalZonesY = this._zone_manager.zones_total_y;
		HashSet<MapChunk> tSetChunks = this._set_visible_chunks;
		List<TileZone> tListToAdd = this._visible_zones;
		float tPowerBarPosition = World.world.move_camera.power_bar_position_y;
		if (pStartX == 0 && pStartY == 0 && pWidth == tTotalZonesX && pHeight == tTotalZonesY)
		{
			tListToAdd.AddRange(this._zone_manager.zones);
			foreach (TileZone tileZone in tListToAdd)
			{
				tileZone.visible = true;
				tileZone.visible_main_centered = true;
			}
			this._list_visible_chunks.AddRange(World.world.map_chunk_manager.chunks);
			tSetChunks.UnionWith(this._list_visible_chunks);
			return;
		}
		for (int tX = 0; tX <= pWidth; tX++)
		{
			for (int tY = 0; tY <= pHeight; tY++)
			{
				int tCurZoneX = pStartX + tX;
				if (tCurZoneX >= 0 && tCurZoneX < tTotalZonesX)
				{
					int tCurZoneY = pStartY + tY;
					if (tCurZoneY >= 0 && tCurZoneY < tTotalZonesY)
					{
						TileZone tZone = this._zone_manager.getZoneUnsafe(tCurZoneX, tCurZoneY);
						tListToAdd.Add(tZone);
						tSetChunks.Add(tZone.chunk);
						tZone.visible = true;
						if (tCurZoneX >= pMainX && tX < pWidth && tY < pHeight && (float)tZone.top_left_corner_tile.y >= tPowerBarPosition)
						{
							tZone.visible_main_centered = true;
						}
					}
				}
			}
		}
		this._list_visible_chunks.AddRange(tSetChunks);
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x001467F4 File Offset: 0x001449F4
	public List<TileZone> getVisibleZones()
	{
		return this._visible_zones;
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x001467FC File Offset: 0x001449FC
	public List<MapChunk> getVisibleChunks()
	{
		return this._list_visible_chunks;
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x00146804 File Offset: 0x00144A04
	public bool hasVisibleZones()
	{
		return this._visible_zones.Count > 0;
	}

	// Token: 0x060028BD RID: 10429 RVA: 0x00146814 File Offset: 0x00144A14
	public int countVisibleZones()
	{
		return this._visible_zones.Count;
	}

	// Token: 0x060028BE RID: 10430 RVA: 0x00146824 File Offset: 0x00144A24
	private TileZone getZoneWithinCamera(int pX, int pY, float pBonusY = 0f)
	{
		Vector3 vector = World.world.camera.ViewportToWorldPoint(new Vector3((float)pX, (float)pY, World.world.camera.nearClipPlane));
		int tX = (int)vector.x;
		int tY = (int)vector.y + (int)(pBonusY * 8f);
		int tTotalZonesBoundX = this._zone_manager.zones_total_x - 1;
		int tTotalZonesBoundY = this._zone_manager.zones_total_y - 1;
		WorldTile tTile = World.world.GetTile(tX, tY);
		if (tTile != null)
		{
			return tTile.zone;
		}
		if (pX == 0 && pY == 0)
		{
			return this._zone_manager.getZone(0, 0);
		}
		if (pX == 1 && pY == 1)
		{
			return this._zone_manager.getZone(tTotalZonesBoundX, tTotalZonesBoundY);
		}
		if (pX == 0 && pY == 1)
		{
			return this._zone_manager.getZone(0, tTotalZonesBoundY);
		}
		if (pX == 1 && pY == 0)
		{
			return this._zone_manager.getZone(tTotalZonesBoundX, 0);
		}
		return null;
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x001468FC File Offset: 0x00144AFC
	public void clear()
	{
		List<TileZone> tList = this._visible_zones;
		foreach (TileZone tileZone in tList)
		{
			tileZone.visible = false;
			tileZone.visible_main_centered = false;
		}
		tList.Clear();
		this._list_visible_chunks.Clear();
		this._set_visible_chunks.Clear();
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x00146974 File Offset: 0x00144B74
	public void fullClear()
	{
		this._visible_zones.Clear();
		this._last_start_x = -1;
		this._last_start_y = -1;
		this._last_width = -1;
		this._last_height = -1;
		this._last_main_x = -1;
	}

	// Token: 0x04001E94 RID: 7828
	private readonly List<TileZone> _visible_zones = new List<TileZone>();

	// Token: 0x04001E95 RID: 7829
	private readonly HashSet<MapChunk> _set_visible_chunks = new HashSet<MapChunk>();

	// Token: 0x04001E96 RID: 7830
	private readonly List<MapChunk> _list_visible_chunks = new List<MapChunk>();

	// Token: 0x04001E97 RID: 7831
	private readonly ZoneCalculator _zone_manager;

	// Token: 0x04001E98 RID: 7832
	private int _last_start_x = -1;

	// Token: 0x04001E99 RID: 7833
	private int _last_start_y = -1;

	// Token: 0x04001E9A RID: 7834
	private int _last_width = -1;

	// Token: 0x04001E9B RID: 7835
	private int _last_height = -1;

	// Token: 0x04001E9C RID: 7836
	private int _last_main_x = -1;
}
