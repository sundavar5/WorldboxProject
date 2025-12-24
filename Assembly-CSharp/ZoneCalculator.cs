using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class ZoneCalculator : MapLayer
{
	// Token: 0x060024E4 RID: 9444 RVA: 0x00131AD4 File Offset: 0x0012FCD4
	internal override void create()
	{
		base.create();
		this.colorValues = new Color(1f, 0.46f, 0.19f, 1f);
		Color tColor = this.sprRnd.color;
		tColor.a = 0.78f;
		this.sprRnd.color = tColor;
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x00131B2C File Offset: 0x0012FD2C
	public void clearDebug()
	{
		if (!DebugConfig.isOn(DebugOption.DebugZones))
		{
			return;
		}
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].clearDebug();
		}
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x00131B6C File Offset: 0x0012FD6C
	internal override void clear()
	{
		base.clear();
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].clear();
		}
		this._current_drawn_zones.Clear();
		this._to_clean_up.Clear();
		this._last_selected_kingdom = null;
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00131BC4 File Offset: 0x0012FDC4
	public void clean()
	{
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.Dispose();
		}
		this.zones.Clear();
		this._zones_dict_id.Clear();
		this.map = null;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x00131C34 File Offset: 0x0012FE34
	public void generate()
	{
		this.zones.Clear();
		this._zones_dict_id.Clear();
		int tMod = 8;
		this.zones_total_x = Config.ZONE_AMOUNT_X * tMod;
		this.zones_total_y = Config.ZONE_AMOUNT_Y * tMod;
		this.map = new TileZone[this.zones_total_x, this.zones_total_y];
		int iZone = 0;
		for (int yy = 0; yy < this.zones_total_y; yy++)
		{
			for (int xx = 0; xx < this.zones_total_x; xx++)
			{
				TileZone tZone = new TileZone
				{
					x = xx,
					y = yy,
					id = iZone++
				};
				if ((xx + yy) % 2 == 0)
				{
					tZone.debug_zone_color = this.color1;
				}
				else
				{
					tZone.debug_zone_color = this.color2;
				}
				this.map[xx, yy] = tZone;
				this.zones.Add(tZone);
				this._zones_dict_id.Add(tZone.id, tZone);
				this.fillZone(tZone);
			}
		}
		World.world.tilemap.generate(this.zones.Count);
		this.generateNeighbours();
		this.zones.Shuffle<TileZone>();
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x00131D5D File Offset: 0x0012FF5D
	public TileZone getZoneByID(int pID)
	{
		return this._zones_dict_id[pID];
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00131D6B File Offset: 0x0012FF6B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TileZone getZone(int pX, int pY)
	{
		if (pX < 0 || pX >= this.zones_total_x || pY < 0 || pY >= this.zones_total_y)
		{
			return null;
		}
		return this.map[pX, pY];
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x00131D96 File Offset: 0x0012FF96
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TileZone getZoneUnsafe(int pX, int pY)
	{
		return this.map[pX, pY];
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00131DA8 File Offset: 0x0012FFA8
	private void generateNeighbours()
	{
		using (ListPool<TileZone> tNeighbours = new ListPool<TileZone>(4))
		{
			using (ListPool<TileZone> tNeighboursAll = new ListPool<TileZone>(8))
			{
				List<TileZone> tZones = this.zones;
				int tLen = tZones.Count;
				for (int i = 0; i < tLen; i++)
				{
					TileZone tTile = tZones[i];
					TileZone tNeighbour = this.getZone(tTile.x - 1, tTile.y);
					tTile.addNeighbour(tNeighbour, TileDirection.Left, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.getZone(tTile.x + 1, tTile.y);
					tTile.addNeighbour(tNeighbour, TileDirection.Right, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.getZone(tTile.x, tTile.y - 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Down, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.getZone(tTile.x, tTile.y + 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Up, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.getZone(tTile.x - 1, tTile.y - 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.getZone(tTile.x - 1, tTile.y + 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.getZone(tTile.x + 1, tTile.y - 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.getZone(tTile.x + 1, tTile.y + 1);
					tTile.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tTile.neighbours = tNeighbours.ToArray<TileZone>();
					tTile.neighbours_all = tNeighboursAll.ToArray<TileZone>();
					tNeighbours.Clear();
					tNeighboursAll.Clear();
				}
			}
		}
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x00131F94 File Offset: 0x00130194
	private void fillZone(TileZone pZone)
	{
		int tStartX = pZone.x * 8;
		int tStartY = pZone.y * 8;
		int tHalfSize = 4;
		for (int xx = 0; xx < 8; xx++)
		{
			for (int yy = 0; yy < 8; yy++)
			{
				WorldTile tTile = World.world.GetTile(xx + tStartX, yy + tStartY);
				if (tTile != null)
				{
					tTile.zone = pZone;
					pZone.addTile(tTile, xx, yy);
					if (xx == tHalfSize && yy == tHalfSize)
					{
						pZone.centerTile = tTile;
					}
				}
			}
		}
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x0013200C File Offset: 0x0013020C
	private void updateOutlineAnimation(float pElapsed)
	{
		if (this._selected_nano_object == null && this._cursor_nano_object == null)
		{
			this._outline_animation_in = true;
			this.outline_animation = 0f;
			return;
		}
		if (this._outline_animation_in)
		{
			this.outline_animation += pElapsed * 2f;
			if (this.outline_animation >= 1f)
			{
				this.outline_animation = 1f;
				this._outline_animation_in = false;
				return;
			}
		}
		else
		{
			this.outline_animation -= pElapsed * 2f;
			if (this.outline_animation <= 0f)
			{
				this.outline_animation = 0f;
				this._outline_animation_in = true;
			}
		}
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x001320AC File Offset: 0x001302AC
	public void updateAnimationsAndSelections()
	{
		this._cached_map_opacity = this.getMapOpacity();
		this._cached_ony_favorited_meta = PlayerConfig.optionBoolEnabled("only_favorited_meta");
		this._cached_check_animation = (!ScrollWindow.isWindowActive() && (!this._cursor_nano_object.isRekt() || !this._selected_nano_object.isRekt()));
		this._cached_should_be_clear_color = this.shouldBeClearColor();
		this.checkCursorNanoObject();
		this.checkSelectedNanoObject();
		this.updateOutlineAnimation(World.world.delta_time);
		MetaTypeAsset tMetaAsset = World.world.getCachedMapMetaAsset();
		int tZoneState = (tMetaAsset != null) ? tMetaAsset.getZoneOptionState() : -1;
		this.checkClearAllZonesToRedraw();
		this.checkDrawnZonesDirty();
		this._last_should_be_clear_color = this._cached_should_be_clear_color;
		bool tShowZones = Zones.showMapBorders();
		if (tMetaAsset != null && tShowZones)
		{
			this.sprRnd.enabled = true;
			this.setMode(tMetaAsset);
			this._last_zone_state = tZoneState;
		}
		else
		{
			this.setMode(null);
			this.sprRnd.enabled = false;
		}
		Color tColor = Color.white;
		tColor.r = this.border_brightness;
		tColor.g = this.border_brightness;
		tColor.b = this.border_brightness;
		if (World.world.era_manager.getCurrentAge().overlay_darkness)
		{
			this._night_multiplier = Mathf.Lerp(this._night_multiplier, 0.6f, World.world.delta_time * 0.5f);
		}
		else
		{
			this._night_multiplier = Mathf.Lerp(this._night_multiplier, 1f, World.world.delta_time * 0.5f);
		}
		tColor.a = this._cached_map_opacity;
		this.sprRnd.color = tColor;
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00132242 File Offset: 0x00130442
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x0013224C File Offset: 0x0013044C
	private void checkClearAllZonesToRedraw()
	{
		MetaTypeAsset cachedMapMetaAsset = World.world.getCachedMapMetaAsset();
		int tZoneState = (cachedMapMetaAsset != null) ? cachedMapMetaAsset.getZoneOptionState() : -1;
		if ((tZoneState != -1 && tZoneState != this._last_zone_state) || this._selection_changed_dirty || this._last_should_be_clear_color != this._cached_should_be_clear_color)
		{
			this.clearCurrentDrawnZones(true);
			this._selection_changed_dirty = false;
		}
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x001322A1 File Offset: 0x001304A1
	public override void draw(float pElapsed)
	{
		this.redrawZones();
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x001322A9 File Offset: 0x001304A9
	private static float getCameraScaleZoom()
	{
		return Mathf.Clamp(MoveCamera.instance.main_camera.orthographicSize / 20f, 1f, 30f);
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x001322CF File Offset: 0x001304CF
	private void setDirty()
	{
		this._dirty = true;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x001322D8 File Offset: 0x001304D8
	private void setMode(MetaTypeAsset pAsset)
	{
		if (this._mode_asset != pAsset)
		{
			this.clearAllRedrawTimers();
		}
		this._mode_asset = pAsset;
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x001322F0 File Offset: 0x001304F0
	public void clearAllRedrawTimers()
	{
		this.clearTimer();
		this.clearWorldBehaviourTimer();
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x001322FE File Offset: 0x001304FE
	public void clearTimer()
	{
		this._redraw_timer = 0f;
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x0013230B File Offset: 0x0013050B
	public void clearWorldBehaviourTimer()
	{
		AssetManager.world_behaviours.get("zones_meta_data_visualizer").manager.timerClear();
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00132326 File Offset: 0x00130526
	public bool isModeNone()
	{
		return this._mode_asset == null;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00132334 File Offset: 0x00130534
	private bool isBorderColor_relations(TileZone pZone, City pCity, bool pCheckFriendly = false)
	{
		return (pZone == null || pZone.city == pCity || pZone.city == null || pZone.city.kingdom != pCity.kingdom) && (pZone == null || pZone.city == null || pZone.city.kingdom != pCity.kingdom);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00132390 File Offset: 0x00130590
	public void debug(DebugTool pTool)
	{
		if (this._debug_redrawn_last_amount != 0)
		{
			this._debug_redrawn_last = this._debug_redrawn_last_amount;
		}
		pTool.setText("_toCleanUp", this._to_clean_up.Count, 0f, false, 0L, false, false, "");
		pTool.setText("_lastDrawnZones", this._current_drawn_zones.Count, 0f, false, 0L, false, false, "");
		pTool.setText("redrawn_last", this._debug_redrawn_last, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x00132430 File Offset: 0x00130630
	public TileZone getMapCenterZone()
	{
		int tX = this.zones_total_x / 2;
		int tY = this.zones_total_y / 2;
		return this.map[tX, tY];
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x0013245C File Offset: 0x0013065C
	public void drawZoneMeta(TileZone pZone, MetaTypeAsset pMetaTypeAsset, MetaZoneGetMetaSimple pZoneGetDelegate)
	{
		IMetaObject tNanoObjectCenter = pZoneGetDelegate(pZone);
		bool tUp = this.isBorderColorSameNanoObject(pZone.zone_up, pZoneGetDelegate, tNanoObjectCenter);
		bool tDown = this.isBorderColorSameNanoObject(pZone.zone_down, pZoneGetDelegate, tNanoObjectCenter);
		bool tLeft = this.isBorderColorSameNanoObject(pZone.zone_left, pZoneGetDelegate, tNanoObjectCenter);
		bool tRight = this.isBorderColorSameNanoObject(pZone.zone_right, pZoneGetDelegate, tNanoObjectCenter);
		MetaObjectData tMetaData = null;
		if (tNanoObjectCenter != null)
		{
			tMetaData = tNanoObjectCenter.getMetaData();
		}
		this.drawZoneMeta(tNanoObjectCenter, pZone, tUp, tDown, tLeft, tRight, tMetaData, pMetaTypeAsset);
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x001324CC File Offset: 0x001306CC
	public void drawZoneAlliance(TileZone pZone, int pZoneOption)
	{
		Alliance tAlliance = pZone.getAllianceOnZone(pZoneOption);
		bool tUp = this.isBorderColor_alliance(pZone.zone_up, tAlliance, pZoneOption, false);
		bool tDown = this.isBorderColor_alliance(pZone.zone_down, tAlliance, pZoneOption, false);
		bool tLeft = this.isBorderColor_alliance(pZone.zone_left, tAlliance, pZoneOption, false);
		bool tRight = this.isBorderColor_alliance(pZone.zone_right, tAlliance, pZoneOption, false);
		this.drawZoneMeta(tAlliance, pZone, tUp, tDown, tLeft, tRight, tAlliance.data, MetaTypeLibrary.alliance);
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x0013253C File Offset: 0x0013073C
	private bool isBorderColor_alliance(TileZone pZone, Alliance pAlliance, int pZoneOption, bool pCheckFriendly = false)
	{
		if (pZone == null)
		{
			return true;
		}
		NanoObject tZoneAlliance = pZone.getAllianceOnZone(pZoneOption);
		return tZoneAlliance == null || tZoneAlliance != pAlliance;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x00132564 File Offset: 0x00130764
	private void drawZoneMeta(IMetaObject pMeta, TileZone pZone, bool pUp, bool pDown, bool pLeft, bool pRight, MetaObjectData pMetaData, MetaTypeAsset pMetaTypeAsset)
	{
		int tNewMetaHashCode = -1;
		if (pMeta != null)
		{
			tNewMetaHashCode = pMeta.GetHashCode();
		}
		int tNewID = this.generateIdForDraw(this._mode_asset, tNewMetaHashCode, pUp, pDown, pLeft, pRight);
		bool tShowSelection = false;
		int tColorAssetID = 0;
		Color32 tColorBorder;
		Color32 tColorInside;
		if (pMeta != null && pMeta.isAlive())
		{
			ColorAsset tColorAsset = pMeta.getColor();
			tColorAssetID = tColorAsset.index_id;
			tColorBorder = tColorAsset.getColorMainSecond32();
			if (this._cached_should_be_clear_color)
			{
				tColorInside = this._color_clear;
			}
			else
			{
				tColorInside = tColorAsset.getColorBorderInsideAlpha32();
			}
			tShowSelection = this.checkFadeAndSelectionColors(pZone, ref tColorInside, ref tColorBorder, 0f, pMeta, pMetaTypeAsset, pMetaData.favorite);
		}
		else
		{
			tColorInside = this._color_clear;
			tColorBorder = this._color_clear;
		}
		if (!pZone.checkShouldReRender(tNewMetaHashCode, tNewID, tColorAssetID, tShowSelection))
		{
			return;
		}
		this.applyMetaColorsToZone(pZone, ref tColorInside, ref tColorBorder, pUp, pDown, pLeft, pRight);
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x00132624 File Offset: 0x00130824
	public void drawZoneCity(TileZone pZone)
	{
		City tCity = pZone.city;
		Kingdom tKingdom = tCity.kingdom;
		ColorAsset color = tCity.getColor();
		Color32 tColorBorder = color.getColorBorderInsideAlpha32();
		Color32 tColorBorderOut = color.getColorMainSecond32();
		if (this._cached_should_be_clear_color)
		{
			tColorBorder = this._color_clear;
		}
		bool tUp = this.isBorderColor_cities(pZone.zone_up, tCity, true);
		bool tDown = this.isBorderColor_cities(pZone.zone_down, tCity, false);
		bool tLeft = this.isBorderColor_cities(pZone.zone_left, tCity, false);
		bool tRight = this.isBorderColor_cities(pZone.zone_right, tCity, true);
		int tNewHashCode = tCity.GetHashCode();
		int tNewID = this.generateIdForDraw(this._mode_asset, tNewHashCode, tUp, tDown, tLeft, tRight);
		tNewID += tKingdom.GetHashCode();
		bool tShowSelection = this.checkFadeAndSelectionColors(pZone, ref tColorBorder, ref tColorBorderOut, 0f, tCity, MetaTypeLibrary.city, tCity.data.favorite);
		if (!pZone.checkShouldReRender(tNewHashCode, tNewID, 0, tShowSelection))
		{
			return;
		}
		this.applyMetaColorsToZone(pZone, ref tColorBorder, ref tColorBorderOut, tUp, tDown, tLeft, tRight);
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x00132714 File Offset: 0x00130914
	private bool isBorderColorSameNanoObject(TileZone pZone, MetaZoneGetMetaSimple pZoneGetMetaDelegate, IMetaObject pNanoObjectMain)
	{
		if (pZone == null)
		{
			return true;
		}
		IMetaObject tZoneMetaObject = pZoneGetMetaDelegate(pZone);
		return tZoneMetaObject == null || tZoneMetaObject != pNanoObjectMain;
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x0013273C File Offset: 0x0013093C
	private bool isBorderColor_cities(TileZone pZone, City pCityMain, bool pCheckFriendly = false)
	{
		if (pZone == null)
		{
			return true;
		}
		City tZoneCity = pZone.city;
		Kingdom tZoneKingdom = (tZoneCity != null) ? tZoneCity.kingdom : null;
		return (!pCheckFriendly || tZoneCity == pCityMain || tZoneCity == null || tZoneKingdom != pCityMain.kingdom) && (tZoneCity == null || tZoneKingdom != pCityMain.kingdom || tZoneCity != pCityMain);
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x0013278C File Offset: 0x0013098C
	private bool checkShouldDrawObject(bool pMetaFavorite)
	{
		return !this._cached_ony_favorited_meta || pMetaFavorite;
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x0013279C File Offset: 0x0013099C
	private float getMapOpacity()
	{
		float tAlpha;
		if (MapBox.isRenderMiniMap())
		{
			tAlpha = this.minimap_opacity;
		}
		else
		{
			tAlpha = Mathf.Clamp(ZoneCalculator.getCameraScaleZoom() * 0.3f, 0f, 0.78f);
		}
		return tAlpha * this._night_multiplier;
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x001327DE File Offset: 0x001309DE
	private bool shouldBeClearColor()
	{
		return MapBox.isRenderGameplay();
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x001327EA File Offset: 0x001309EA
	public void drawEnd(TileZone pZone)
	{
		this._current_drawn_zones.Add(pZone);
		this._to_clean_up.Remove(pZone);
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x00132808 File Offset: 0x00130A08
	private void checkCursorNanoObject()
	{
		NanoObject tCursorNanoObject = null;
		if (MapBox.isRenderMiniMap() && !World.world.isOverUI())
		{
			WorldTile tMouseTile = World.world.getMouseTilePosCachedFrame();
			MetaTypeAsset tMetaTypeAsset = World.world.getCachedMapMetaAsset();
			if (tMouseTile != null && tMetaTypeAsset != null)
			{
				MetaZoneGetMeta tile_get_metaobject = tMetaTypeAsset.tile_get_metaobject;
				tCursorNanoObject = (((tile_get_metaobject != null) ? tile_get_metaobject(tMouseTile.zone, tMetaTypeAsset.getZoneOptionState()) : null) as NanoObject);
			}
		}
		if (this._cursor_nano_object != tCursorNanoObject)
		{
			this._selection_changed_dirty = true;
		}
		this._cursor_nano_object = tCursorNanoObject;
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x00132884 File Offset: 0x00130A84
	private void checkSelectedNanoObject()
	{
		NanoObject tSelectedNanoObject = null;
		if (MapBox.isRenderMiniMap())
		{
			MetaTypeAsset tMetaTypeAsset = World.world.getCachedMapMetaAsset();
			if (tMetaTypeAsset != null)
			{
				tSelectedNanoObject = tMetaTypeAsset.get_selected();
			}
		}
		if (tSelectedNanoObject != this._selected_nano_object)
		{
			this._selection_changed_dirty = true;
		}
		this._selected_nano_object = tSelectedNanoObject;
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x001328CC File Offset: 0x00130ACC
	private void redrawZones()
	{
		Bench.bench("borders_renderer", "borders_renderer_total", false);
		Bench.bench("clearAllRedrawTimers", "borders_renderer", false);
		if (this._last_selected_kingdom != SelectedMetas.selected_kingdom)
		{
			this._last_selected_kingdom = SelectedMetas.selected_kingdom;
			this.clearAllRedrawTimers();
		}
		Bench.benchEnd("clearAllRedrawTimers", "borders_renderer", false, 0L, false);
		if (this._redraw_timer > 0f)
		{
			this._redraw_timer -= Time.deltaTime;
			Bench.clearBenchmarkEntrySkipMultiple("borders_renderer_total", new string[]
			{
				"_to_clean_up.union",
				"draw_zones.Invoke",
				"clearDrawnZones",
				"updatePixels"
			});
			return;
		}
		this._redraw_timer = 0.01f;
		Bench.bench("_to_clean_up.union", "borders_renderer", false);
		this._debug_redrawn_last_amount = 0;
		if (this._current_drawn_zones.Any<TileZone>())
		{
			this._to_clean_up.UnionWith(this._current_drawn_zones);
		}
		Bench.benchEnd("_to_clean_up.union", "borders_renderer", false, 0L, false);
		Bench.bench("draw_zones.Invoke", "borders_renderer", false);
		if (this._mode_asset != null)
		{
			this._mode_asset.draw_zones(this._mode_asset);
		}
		Bench.benchEnd("draw_zones.Invoke", "borders_renderer", false, 0L, false);
		Bench.bench("clearDrawnZones", "borders_renderer", false);
		if (this._to_clean_up.Any<TileZone>())
		{
			this.clearDrawnZones();
		}
		Bench.benchEnd("clearDrawnZones", "borders_renderer", false, 0L, false);
		Bench.bench("updatePixels", "borders_renderer", false);
		if (this._dirty)
		{
			this._dirty = false;
			base.updatePixels();
		}
		Bench.benchEnd("updatePixels", "borders_renderer", false, 0L, false);
		Bench.benchEnd("borders_renderer", "borders_renderer_total", false, 0L, false);
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x00132A9C File Offset: 0x00130C9C
	private void clearDrawnZones()
	{
		foreach (TileZone tZone in this._to_clean_up)
		{
			this.drawZoneClear(tZone);
			tZone.resetRenderHelpers();
			this._current_drawn_zones.Remove(tZone);
		}
		this._to_clean_up.Clear();
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x00132B10 File Offset: 0x00130D10
	public void dirtyAndClear()
	{
		this.setDrawnZonesDirty();
		this.clearCurrentDrawnZones(true);
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x00132B20 File Offset: 0x00130D20
	internal void clearCurrentDrawnZones(bool pCleanTimer = true)
	{
		foreach (TileZone tZone in this._current_drawn_zones)
		{
			this.drawZoneClear(tZone);
			tZone.resetRenderHelpers();
		}
		this._current_drawn_zones.Clear();
		if (pCleanTimer)
		{
			this.clearAllRedrawTimers();
		}
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x00132B90 File Offset: 0x00130D90
	private int generateIdForDraw(MetaTypeAsset pModeAsset, int pHashCode, bool pUp, bool pDown, bool pLeft, bool pRight)
	{
		int tNewID = (pModeAsset.GetHashCode() + 1) * 10000000;
		if (pUp)
		{
			tNewID += 100000;
		}
		if (pDown)
		{
			tNewID += 10000;
		}
		if (pLeft)
		{
			tNewID += 1000;
		}
		if (pRight)
		{
			tNewID += 100;
		}
		return tNewID;
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x00132BDC File Offset: 0x00130DDC
	public MetaType getCurrentModeDebug()
	{
		MetaType tMode = Zones.getForcedMapMode();
		if (tMode.isNone())
		{
			MetaTypeAsset tAsset = World.world.getCachedMapMetaAsset();
			if (tAsset != null)
			{
				tMode = tAsset.map_mode;
			}
		}
		return tMode;
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x00132C10 File Offset: 0x00130E10
	private void applyAlphaFadeToColor(ref Color32 pColorBorderInside, ref Color32 pColorBorderOutside, MetaTypeAsset pMetaTypeAsset, float pDiff, int pUnits, double pTimestampNew)
	{
		float tZonesAlphaCamera = this._cached_map_opacity;
		float tMod = pDiff / 5f;
		float tDelayFactor = 0.6f;
		float tOriginalAlphaInside = (float)pColorBorderInside.a / 255f;
		float tOriginalAlphaOutside = (float)pColorBorderOutside.a / 255f;
		float tFadeInMultiplier = 1f;
		World.world.getWorldTimeElapsedSince(pTimestampNew);
		float tAlphaOutside = 1f - tMod;
		float tAdjustedMod = Mathf.Clamp01((tMod - tDelayFactor) / (1f - tDelayFactor));
		float num = (1f - tAdjustedMod * tAdjustedMod * tAdjustedMod) * (tZonesAlphaCamera * tFadeInMultiplier);
		tAlphaOutside *= tZonesAlphaCamera * tFadeInMultiplier;
		byte tAlphaByteInside = (byte)(num * tOriginalAlphaInside * 255f);
		byte tAlphaByteOutside = (byte)(tAlphaOutside * tOriginalAlphaOutside * 255f);
		pColorBorderInside.a = tAlphaByteInside;
		pColorBorderOutside.a = tAlphaByteOutside;
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x00132CCA File Offset: 0x00130ECA
	private bool shouldShowSelectionFor(IMetaObject pNanoObject)
	{
		return pNanoObject == this._cursor_nano_object || pNanoObject == this._selected_nano_object;
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x00132CE4 File Offset: 0x00130EE4
	private bool checkFadeAndSelectionColors(TileZone pZone, ref Color32 pColorBorderInside, ref Color32 pColorBorderOut, float pDiff, IMetaObject pMetaObjectToDraw, MetaTypeAsset pMetaTypeAsset, bool pFavorite)
	{
		bool tShowSelection = false;
		if (this._cached_check_animation)
		{
			if (this.shouldShowSelectionFor(pMetaObjectToDraw))
			{
				tShowSelection = true;
			}
			else if (!this._selected_nano_object.isRekt())
			{
				pColorBorderInside.a = (byte)((float)pColorBorderInside.a * 0.6f);
				pColorBorderOut.a = (byte)((float)pColorBorderOut.a * 0.6f);
			}
		}
		bool tShouldDrawFav;
		if (tShowSelection)
		{
			tShouldDrawFav = true;
		}
		else if (SelectedUnit.isSet())
		{
			tShouldDrawFav = (SelectedUnit.unit.getMetaObjectOfType(pMetaTypeAsset.map_mode) == pMetaObjectToDraw);
		}
		else
		{
			tShouldDrawFav = this.checkShouldDrawObject(pFavorite);
		}
		if (!tShouldDrawFav)
		{
			pColorBorderInside.a = (byte)((float)pColorBorderInside.a * 0.5f);
			pColorBorderOut.a = (byte)((float)pColorBorderOut.a * 0.5f);
		}
		if (!tShouldDrawFav && this._cached_should_be_clear_color)
		{
			pColorBorderInside = this._color_clear;
		}
		if (tShowSelection)
		{
			pZone.resetRenderHelpers();
			float tAnimation = this.outline_animation;
			pColorBorderOut = Color32.Lerp(pColorBorderOut, Toolbox.color_black_32, tAnimation);
		}
		return tShowSelection;
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x00132DD9 File Offset: 0x00130FD9
	public void drawBegin()
	{
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x00132DDB File Offset: 0x00130FDB
	internal void setDrawnZonesDirty()
	{
		this._dirty_draw_zones = true;
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x00132DE4 File Offset: 0x00130FE4
	private void checkDrawnZonesDirty()
	{
		if (!this._dirty_draw_zones)
		{
			return;
		}
		this._dirty_draw_zones = false;
		this.clearAllRedrawTimers();
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x00132DFC File Offset: 0x00130FFC
	public void drawGenericFluid(ZoneMetaData pData, MetaTypeAsset pMetaTypeAsset)
	{
		TileZone tZone = pData.zone;
		IMetaObject tMetaObject = pData.meta_object;
		bool tFavorite = tMetaObject.isFavorite();
		double tCurTime = World.world.getCurWorldTime();
		float tDiff = pData.getDiffTime(tCurTime);
		if (tDiff > 5f)
		{
			return;
		}
		ColorAsset color = tMetaObject.getColor();
		Color32 tColorBorder = color.getColorText();
		Color32 tColorBorderOut = color.getColorText();
		if (tDiff != 0f)
		{
			this.applyAlphaFadeToColor(ref tColorBorder, ref tColorBorderOut, pMetaTypeAsset, tDiff, tMetaObject.countUnits(), pData.timestamp_new);
		}
		bool tUp = false;
		bool tDown = false;
		bool tLeft = false;
		bool tRight = false;
		bool flag = this.shouldShowSelectionFor(tMetaObject);
		if (flag)
		{
			tUp = this.isBorderNanoMetaFluid(tZone.zone_up, tMetaObject, tCurTime);
			tDown = this.isBorderNanoMetaFluid(tZone.zone_down, tMetaObject, tCurTime);
			tLeft = this.isBorderNanoMetaFluid(tZone.zone_left, tMetaObject, tCurTime);
			tRight = this.isBorderNanoMetaFluid(tZone.zone_right, tMetaObject, tCurTime);
		}
		int tNewHashCode = tMetaObject.GetHashCode();
		int tNewID = this.generateIdForDraw(this._mode_asset, tNewHashCode, tUp, tDown, tLeft, tRight);
		tZone.last_drawn_id = tNewID;
		tZone.last_drawn_hashcode = tNewHashCode;
		this.checkFadeAndSelectionColors(tZone, ref tColorBorder, ref tColorBorderOut, tDiff, tMetaObject, pMetaTypeAsset, tFavorite);
		if (flag)
		{
			this.applyMetaColorsToZone(tZone, ref tColorBorder, ref tColorBorderOut, tUp, tDown, tLeft, tRight);
			return;
		}
		this.applyMetaColorsToZoneFull(tZone, ref tColorBorder);
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x00132F34 File Offset: 0x00131134
	private bool isBorderNanoMetaFluid(TileZone pZone, IMetaObject pMetaMain, double pCurTime)
	{
		if (pZone == null)
		{
			return true;
		}
		if (ZoneMetaDataVisualizer.hasZoneData(pZone))
		{
			ZoneMetaData tData = ZoneMetaDataVisualizer.getZoneMetaData(pZone);
			if (tData.getDiffTime(pCurTime) > 5f)
			{
				return true;
			}
			if (tData.meta_object == pMetaMain)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x00132F74 File Offset: 0x00131174
	private void applyMetaColorsToZoneFull(TileZone pZone, ref Color32 pColor)
	{
		this.setDirty();
		WorldTile[] tTiles = pZone.tiles;
		Color32[] tPixels = this.pixels;
		int tCount = tTiles.Length;
		Color32 tLastColor = tPixels[tTiles[0].data.tile_id];
		if (tLastColor.r == pColor.r && tLastColor.g == pColor.g && tLastColor.b == pColor.b && tLastColor.a == pColor.a)
		{
			return;
		}
		for (int i = 0; i < tCount; i++)
		{
			int tTileID = tTiles[i].data.tile_id;
			tPixels[tTileID] = pColor;
		}
		this._debug_redrawn_last_amount++;
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x00133024 File Offset: 0x00131224
	private void applyMetaColorsToZone(TileZone pZone, ref Color32 pColorInside, ref Color32 pColorOutside, bool pUp, bool pDown, bool pLeft, bool pRight)
	{
		this.setDirty();
		WorldTile[] tTiles = pZone.tiles;
		Color32[] tPixels = this.pixels;
		int tCount = tTiles.Length;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile worldTile = tTiles[i];
			int tTileID = worldTile.data.tile_id;
			WorldTileZoneBorder tBorderData = worldTile.world_tile_zone_border;
			if (!tBorderData.border)
			{
				tPixels[tTileID] = pColorInside;
			}
			else if (pUp && tBorderData.border_up)
			{
				tPixels[tTileID] = pColorOutside;
			}
			else if (pDown && tBorderData.border_down)
			{
				tPixels[tTileID] = pColorOutside;
			}
			else if (pLeft && tBorderData.border_left)
			{
				tPixels[tTileID] = pColorOutside;
			}
			else if (pRight && tBorderData.border_right)
			{
				tPixels[tTileID] = pColorOutside;
			}
			else
			{
				tPixels[tTileID] = pColorInside;
			}
		}
		this._debug_redrawn_last_amount++;
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x0013311E File Offset: 0x0013131E
	private void drawZoneClear(TileZone pZone)
	{
		this.colorZone(pZone, Toolbox.clear);
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x0013312C File Offset: 0x0013132C
	private void colorZone(TileZone pZone, Color32 pColor)
	{
		this.setDirty();
		Color32[] tPixels = this.pixels;
		WorldTile[] tTiles = pZone.tiles;
		int tCount = tTiles.Length;
		Color32 tLastColor = tPixels[tTiles[0].data.tile_id];
		if (tLastColor.r == pColor.r && tLastColor.g == pColor.g && tLastColor.b == pColor.b && tLastColor.a == pColor.a)
		{
			return;
		}
		for (int i = 0; i < tCount; i++)
		{
			int tTileID = tTiles[i].data.tile_id;
			tPixels[tTileID] = pColor;
		}
	}

	// Token: 0x04001AA5 RID: 6821
	public readonly List<TileZone> zones = new List<TileZone>();

	// Token: 0x04001AA6 RID: 6822
	private readonly Dictionary<int, TileZone> _zones_dict_id = new Dictionary<int, TileZone>();

	// Token: 0x04001AA7 RID: 6823
	internal TileZone[,] map;

	// Token: 0x04001AA8 RID: 6824
	private bool _dirty;

	// Token: 0x04001AA9 RID: 6825
	private int _last_zone_state = -1;

	// Token: 0x04001AAA RID: 6826
	public float outline_animation;

	// Token: 0x04001AAB RID: 6827
	private bool _outline_animation_in;

	// Token: 0x04001AAC RID: 6828
	private float _cached_map_opacity;

	// Token: 0x04001AAD RID: 6829
	private bool _cached_ony_favorited_meta;

	// Token: 0x04001AAE RID: 6830
	private bool _cached_check_animation;

	// Token: 0x04001AAF RID: 6831
	private bool _cached_should_be_clear_color;

	// Token: 0x04001AB0 RID: 6832
	private bool _last_should_be_clear_color;

	// Token: 0x04001AB1 RID: 6833
	private Kingdom _last_selected_kingdom;

	// Token: 0x04001AB2 RID: 6834
	public int zones_total_x;

	// Token: 0x04001AB3 RID: 6835
	public int zones_total_y;

	// Token: 0x04001AB4 RID: 6836
	private const float ALPHA_NON_FAVORITED_META = 0.5f;

	// Token: 0x04001AB5 RID: 6837
	private const float ALPHA_NON_SELECTED_META = 0.6f;

	// Token: 0x04001AB6 RID: 6838
	public Color color1 = Color.gray;

	// Token: 0x04001AB7 RID: 6839
	public Color color2 = Color.white;

	// Token: 0x04001AB8 RID: 6840
	private readonly Color32 _color_clear = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);

	// Token: 0x04001AB9 RID: 6841
	private readonly HashSetTileZone _to_clean_up = new HashSetTileZone();

	// Token: 0x04001ABA RID: 6842
	private float _night_multiplier = 1f;

	// Token: 0x04001ABB RID: 6843
	private int _debug_redrawn_last;

	// Token: 0x04001ABC RID: 6844
	private float _redraw_timer;

	// Token: 0x04001ABD RID: 6845
	private bool _dirty_draw_zones;

	// Token: 0x04001ABE RID: 6846
	public float minimap_opacity = 1f;

	// Token: 0x04001ABF RID: 6847
	public float border_brightness = 1f;

	// Token: 0x04001AC0 RID: 6848
	private MetaTypeAsset _mode_asset;

	// Token: 0x04001AC1 RID: 6849
	private bool _selection_changed_dirty;

	// Token: 0x04001AC2 RID: 6850
	private NanoObject _cursor_nano_object;

	// Token: 0x04001AC3 RID: 6851
	private NanoObject _selected_nano_object;

	// Token: 0x04001AC4 RID: 6852
	private readonly HashSetTileZone _current_drawn_zones = new HashSetTileZone();

	// Token: 0x04001AC5 RID: 6853
	private int _debug_redrawn_last_amount;
}
