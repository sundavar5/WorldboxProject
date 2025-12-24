using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020001F6 RID: 502
public class WorldTilemap : BaseMapObject
{
	// Token: 0x06000EF4 RID: 3828 RVA: 0x000C3D10 File Offset: 0x000C1F10
	internal override void create()
	{
		base.create();
		this._layers = new Dictionary<int, TilemapExtended>();
		this._asset_border_water_outline = AssetManager.tiles.get("border_water");
		this._asset_border_water_runup = AssetManager.tiles.get("border_water_runup");
		this._asset_border_pit = AssetManager.tiles.get("border_pit");
		for (int i = 0; i < AssetManager.tiles.list.Count; i++)
		{
			TileTypeBase tType = AssetManager.tiles.list[i];
			this.createTileMapFor(tType);
		}
		for (int j = 0; j < AssetManager.top_tiles.list.Count; j++)
		{
			TileTypeBase tType2 = AssetManager.top_tiles.list[j];
			this.createTileMapFor(tType2);
		}
		this._layer_border_water_runup = this._layers[this._asset_border_water_runup.render_z];
		this._layer_water_outline = this._layers[this._asset_border_water_outline.render_z];
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x000C3E09 File Offset: 0x000C2009
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool needsRedraw(WorldTile pTile)
	{
		return pTile.last_rendered_tile_type != pTile.Type;
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000C3E1C File Offset: 0x000C201C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void addToQueueToRedraw(WorldTile pTile)
	{
		TileZone tZone = pTile.zone;
		this._dirty_zones.Add(tZone);
		this._tiles_by_zone[tZone.id].Add(pTile);
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x000C3E54 File Offset: 0x000C2054
	private void createTileMapFor(TileTypeBase pTileBase)
	{
		if (this._layers.ContainsKey(pTileBase.render_z))
		{
			return;
		}
		TilemapExtended tLayer = Object.Instantiate<TilemapExtended>(this._prefab_tilemap_layer, base.transform);
		tLayer.create(pTileBase);
		if (pTileBase.id == "border_water_runup")
		{
			tLayer.GetComponent<TilemapRenderer>().sharedMaterial = this._water_rims_material;
		}
		this._layers.Add(pTileBase.render_z, tLayer);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x000C3EC3 File Offset: 0x000C20C3
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!World.world.isPaused())
		{
			this.updateWaterRunup(Time.deltaTime);
		}
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000C3EE4 File Offset: 0x000C20E4
	private void updateWaterRunup(float pElapsed)
	{
		if (this._color_water_runup_timer > 0f)
		{
			this._color_water_runup_timer -= pElapsed;
			return;
		}
		this._color_water_runup_timer = 0.01f;
		if (this._color_water_runup_state_fade_in)
		{
			this._color_water_runup_alpha_current += pElapsed * 0.6f;
			if (this._color_water_runup_alpha_current >= 0.7f)
			{
				this._color_water_runup_alpha_current = 0.7f;
				this._color_water_runup_state_fade_in = false;
			}
		}
		else
		{
			this._color_water_runup_alpha_current -= pElapsed * 0.6f;
			if (this._color_water_runup_alpha_current <= 0.02f)
			{
				this._color_water_runup_alpha_current = 0.02f;
				this._color_water_runup_state_fade_in = true;
			}
		}
		float tNightMod = World.world.era_manager.getNightMod();
		Color tColor = Toolbox.blendColor(Toolbox.color_night, this._color_border_water_runup_default, tNightMod);
		tColor.a = this._color_water_runup_alpha_current;
		this._water_rims_material.color = tColor;
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x000C3FC8 File Offset: 0x000C21C8
	internal void clear()
	{
		if (this._tiles_by_zone != null)
		{
			HashSet<WorldTile>[] tiles_by_zone = this._tiles_by_zone;
			for (int i = 0; i < tiles_by_zone.Length; i++)
			{
				tiles_by_zone[i].Clear();
			}
		}
		this._dirty_zones.Clear();
		this._clear_list_zones.Clear();
		foreach (TilemapExtended tilemapExtended in this._layers.Values)
		{
			tilemapExtended.clear();
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x000C4058 File Offset: 0x000C2258
	internal void generate(int pCount)
	{
		this._tiles_by_zone = new HashSet<WorldTile>[pCount];
		for (int i = 0; i < pCount; i++)
		{
			this._tiles_by_zone[i] = new HashSet<WorldTile>(64);
		}
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x000C408C File Offset: 0x000C228C
	private void prepareToDraw()
	{
		foreach (TilemapExtended tilemapExtended in this._layers.Values)
		{
			tilemapExtended.prepareDraw();
		}
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x000C40E4 File Offset: 0x000C22E4
	internal void redrawTiles(bool pForceAll = false)
	{
		if (this._dirty_zones.Count == 0)
		{
			return;
		}
		if (!MapBox.isRenderGameplay() && !pForceAll)
		{
			return;
		}
		this.prepareToDraw();
		if (pForceAll)
		{
			using (HashSet<TileZone>.Enumerator enumerator = this._dirty_zones.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TileZone tZone = enumerator.Current;
					this.checkZoneToRender(tZone);
				}
				goto IL_89;
			}
		}
		List<TileZone> tListZones = World.world.zone_camera.getVisibleZones();
		for (int iZone = 0; iZone < tListZones.Count; iZone++)
		{
			TileZone tZone2 = tListZones[iZone];
			this.checkZoneToRender(tZone2);
		}
		IL_89:
		if (pForceAll)
		{
			this._clear_list_zones.Clear();
			this._dirty_zones.Clear();
		}
		this.redrawAllLayers();
		this.drawFinish();
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x000C41B0 File Offset: 0x000C23B0
	private void drawFinish()
	{
		this._dirty_zones.ExceptWith(this._clear_list_zones);
		this._clear_list_zones.Clear();
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x000C41D0 File Offset: 0x000C23D0
	private void redrawAllLayers()
	{
		foreach (TilemapExtended tilemapExtended in this._layers.Values)
		{
			tilemapExtended.redraw();
		}
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x000C4228 File Offset: 0x000C2428
	private void checkZoneToRender(TileZone pZone)
	{
		if (!this._dirty_zones.Contains(pZone))
		{
			return;
		}
		HashSet<WorldTile> tTiles = this._tiles_by_zone[pZone.id];
		foreach (WorldTile tTile in tTiles)
		{
			this.renderTile(tTile);
		}
		this._clear_list_zones.Add(pZone);
		tTiles.Clear();
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x000C42A8 File Offset: 0x000C24A8
	private void renderTile(WorldTile pTile)
	{
		TileTypeBase tTypeToDraw = pTile.main_type;
		if (pTile.Type != null)
		{
			tTypeToDraw = pTile.Type;
		}
		int tRenderZ = tTypeToDraw.render_z;
		Vector3Int tVec = new Vector3Int(pTile.pos.x, pTile.pos.y, tRenderZ);
		Vector3Int tLastRenderedTilePos = pTile.last_rendered_pos_tile;
		int tLastRenderedBorderTileZ = tLastRenderedTilePos.z;
		if (tVec.z != tLastRenderedBorderTileZ || pTile.last_rendered_tile_type != tTypeToDraw)
		{
			if (tLastRenderedBorderTileZ != -1000)
			{
				this._layers[tLastRenderedBorderTileZ].addToQueueToRedraw(pTile, tLastRenderedTilePos, null, false);
				pTile.last_rendered_pos_tile = WorldTilemap.EMPTY_TILE_POS;
			}
			pTile.last_rendered_tile_type = tTypeToDraw;
			TilemapExtended tilemapExtended = this._layers[tVec.z];
			Tile tTileVariation = this.getVariation(pTile);
			tilemapExtended.addToQueueToRedraw(pTile, tVec, tTileVariation, false);
			pTile.last_rendered_pos_tile = tVec;
		}
		Vector3Int tLastRenderedBorderOceanPos = pTile.last_rendered_border_pos_ocean;
		int tLastRenderedBorderOceanZ_2 = tLastRenderedBorderOceanPos.z;
		if (tLastRenderedBorderOceanZ_2 != -1000)
		{
			this._layers[tLastRenderedBorderOceanZ_2].addToQueueToRedraw(pTile, tLastRenderedBorderOceanPos, null, false);
			pTile.last_rendered_border_pos_ocean = WorldTilemap.EMPTY_TILE_POS;
			this._layer_border_water_runup.addToQueueToRedraw(pTile, tLastRenderedTilePos, null, true);
		}
		if ((pTile.main_type.ground || pTile.main_type.block) && !pTile.main_type.can_be_filled_with_ocean)
		{
			TileType tType = null;
			bool tWater = false;
			if (pTile.has_tile_down && pTile.tile_down.main_type.can_be_filled_with_ocean)
			{
				tType = this._asset_border_pit;
				tRenderZ = tType.render_z;
			}
			else if (pTile.isWaterAround())
			{
				tType = this._asset_border_water_outline;
				tRenderZ = tType.render_z;
				tWater = true;
			}
			if (tType != null)
			{
				TilemapExtended tilemapExtended2 = this._layers[tRenderZ];
				tVec.y = pTile.pos.y;
				tVec.z = tRenderZ;
				tilemapExtended2.addToQueueToRedraw(pTile, tVec, tType.sprites.main, false);
				pTile.last_rendered_border_pos_ocean = tVec;
				if (tWater)
				{
					this._layer_border_water_runup.addToQueueToRedraw(pTile, tVec, this._asset_border_water_runup.sprites.main, true);
				}
			}
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x000C44A9 File Offset: 0x000C26A9
	internal void enableTiles(bool pValue)
	{
		if (base.gameObject.activeSelf != pValue)
		{
			base.gameObject.SetActive(pValue);
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x000C44C8 File Offset: 0x000C26C8
	private Tile getVariation(WorldTile pTile)
	{
		TileSprites tSprites = pTile.main_type.sprites;
		if (pTile.Type != null)
		{
			tSprites = pTile.Type.sprites;
		}
		if (pTile.Type.force_edge_variation && pTile.has_tile_up && pTile.tile_up.Type != pTile.Type)
		{
			return pTile.Type.sprites.getVariation(pTile.Type.force_edge_variation_frame);
		}
		return tSprites.getRandom();
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x000C4540 File Offset: 0x000C2740
	internal void debug(DebugTool pTool)
	{
		pTool.setText("_dirty_zones", this._dirty_zones.Count, 0f, false, 0L, false, false, "");
		pTool.setText("_clear_list_zones", this._clear_list_zones.Count, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x000C45A4 File Offset: 0x000C27A4
	public void checkEnableForWaterRunups(bool pIsLowRes)
	{
		if (pIsLowRes)
		{
			if (this._layer_border_water_runup.gameObject.activeSelf)
			{
				this._layer_border_water_runup.gameObject.SetActive(false);
				this._layer_water_outline.gameObject.SetActive(false);
				return;
			}
		}
		else if (!this._layer_border_water_runup.gameObject.activeSelf)
		{
			this._layer_border_water_runup.gameObject.SetActive(true);
			this._layer_water_outline.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000F05 RID: 3845
	private const int EMPTY_Z = -1000;

	// Token: 0x04000F06 RID: 3846
	public static readonly Vector3Int EMPTY_TILE_POS = new Vector3Int(-1, -1, -1000);

	// Token: 0x04000F07 RID: 3847
	private Dictionary<int, TilemapExtended> _layers;

	// Token: 0x04000F08 RID: 3848
	[SerializeField]
	private TilemapExtended _prefab_tilemap_layer;

	// Token: 0x04000F09 RID: 3849
	[SerializeField]
	private Material _water_rims_material;

	// Token: 0x04000F0A RID: 3850
	private TileType _asset_border_water_outline;

	// Token: 0x04000F0B RID: 3851
	private TileType _asset_border_water_runup;

	// Token: 0x04000F0C RID: 3852
	private TileType _asset_border_pit;

	// Token: 0x04000F0D RID: 3853
	private TilemapExtended _layer_border_water_runup;

	// Token: 0x04000F0E RID: 3854
	private TilemapExtended _layer_water_outline;

	// Token: 0x04000F0F RID: 3855
	private readonly HashSet<TileZone> _dirty_zones = new HashSet<TileZone>();

	// Token: 0x04000F10 RID: 3856
	private readonly List<TileZone> _clear_list_zones = new List<TileZone>();

	// Token: 0x04000F11 RID: 3857
	private HashSet<WorldTile>[] _tiles_by_zone;

	// Token: 0x04000F12 RID: 3858
	private readonly Color _color_border_water_runup_default = Toolbox.makeColor("#DDFCFF", 0.7f);

	// Token: 0x04000F13 RID: 3859
	private float _color_water_runup_alpha_current = 0.4f;

	// Token: 0x04000F14 RID: 3860
	private float _color_water_runup_timer;

	// Token: 0x04000F15 RID: 3861
	private bool _color_water_runup_state_fade_in = true;

	// Token: 0x04000F16 RID: 3862
	private const float WATER_RUNUP_INTERVAL = 0.01f;

	// Token: 0x04000F17 RID: 3863
	private const float WATER_RUNUP_SPEED_CHANGE = 0.6f;

	// Token: 0x04000F18 RID: 3864
	private const float COLOR_WATER_RUNUP_ALPHA_BOUND_MIN = 0.02f;

	// Token: 0x04000F19 RID: 3865
	private const float COLOR_WATER_RUNUP_ALPHA_BOUND_M = 0.7f;
}
