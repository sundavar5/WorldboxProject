using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityPools;

// Token: 0x020001F4 RID: 500
public class WorldTile : IEquatable<WorldTile>, IDisposable
{
	// Token: 0x06000EA4 RID: 3748 RVA: 0x000C29AC File Offset: 0x000C0BAC
	public WorldTile(int pX, int pY, int pTileID)
	{
		this.last_rendered_pos_tile = WorldTilemap.EMPTY_TILE_POS;
		this._units = UnsafeCollectionPool<List<Actor>, Actor>.Get();
		this.data = new WorldTileData(pTileID);
		this.pos = new Vector2Int(pX, pY);
		this.posV3 = new Vector3((float)pX, (float)pY);
		this.posV = new Vector3((float)pX, (float)pY);
		this.posV3.x = this.posV3.x + Actor.sprite_offset.x;
		this.posV3.y = this.posV3.y + Actor.sprite_offset.y;
		this.x = pX;
		this.y = pY;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000C2A74 File Offset: 0x000C0C74
	public bool hasWallsAround()
	{
		if (this.wall_check_dirty)
		{
			this.wall_check_dirty = false;
			this._has_walls_around = false;
			int iN = 0;
			int iNLen = this.neighboursAll.Length;
			while (iN < iNLen)
			{
				if (this.neighboursAll[iN].Type.wall)
				{
					this._has_walls_around = true;
					break;
				}
				iN++;
			}
		}
		return this._has_walls_around;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x000C2ACF File Offset: 0x000C0CCF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isTargeted()
	{
		return this._targeted_by != null;
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x000C2ADA File Offset: 0x000C0CDA
	public bool isTargetedBy(Actor pActor)
	{
		return this._targeted_by == pActor;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000C2AE5 File Offset: 0x000C0CE5
	public void cleanTargetedBy()
	{
		this._targeted_by = null;
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x000C2AEE File Offset: 0x000C0CEE
	public void setTargetedBy(Actor pActor)
	{
		this._targeted_by = pActor;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void doUnits(Action<Actor> pAction)
	{
		List<Actor> tUnits = this._units;
		if (tUnits.Count == 0)
		{
			return;
		}
		for (int i = 0; i < tUnits.Count; i++)
		{
			Actor tActor = tUnits[i];
			if (tActor.isAlive())
			{
				pAction(tActor);
			}
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x000C2B40 File Offset: 0x000C0D40
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void doUnits(Func<Actor, bool> pAction)
	{
		List<Actor> tUnits = this._units;
		if (tUnits.Count == 0)
		{
			return;
		}
		for (int i = 0; i < tUnits.Count; i++)
		{
			Actor tActor = tUnits[i];
			if (tActor.isAlive() && !pAction(tActor))
			{
				return;
			}
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000C2B88 File Offset: 0x000C0D88
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countUnits()
	{
		return this._units.Count;
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000C2B95 File Offset: 0x000C0D95
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasUnits()
	{
		return this._units.Count > 0;
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x000C2BA5 File Offset: 0x000C0DA5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void addUnit(Actor pActor)
	{
		this._units.Add(pActor);
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x000C2BB3 File Offset: 0x000C0DB3
	public void resetNeighbourLists()
	{
		this.neighbours = null;
		this.neighboursAll = null;
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x000C2BC3 File Offset: 0x000C0DC3
	public void pollinate()
	{
		this.pollinated++;
		if (this.pollinated > 5)
		{
			this.growFlowers();
			this.pollinated = 0;
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x000C2BEC File Offset: 0x000C0DEC
	private void growFlowers()
	{
		WorldTile tTile = Toolbox.getRandomChunkFromTile(this).tiles.GetRandom<WorldTile>();
		BiomeAsset tBiomeAsset = tTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return;
		}
		if (tBiomeAsset.grow_type_selector_plants != null)
		{
			BuildingActions.tryGrowVegetationRandom(tTile, VegetationType.Plants, false, true, true);
		}
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000C2C2C File Offset: 0x000C0E2C
	public bool canBuildOn(BuildingAsset pNewTemplate)
	{
		if (pNewTemplate.needs_farms_ground && !this.main_type.can_be_farm)
		{
			return false;
		}
		if (this.Type.liquid && !pNewTemplate.can_be_placed_on_liquid)
		{
			return false;
		}
		if (pNewTemplate.burnable && this.isOnFire())
		{
			return false;
		}
		if (pNewTemplate.affected_by_lava && this.Type.lava)
		{
			return false;
		}
		if (!pNewTemplate.can_be_placed_on_blocks && this.Type.block)
		{
			return false;
		}
		if (this.building != null && !this.building.isUsable() && !this.building.asset.flora && !pNewTemplate.remove_ruins)
		{
			return false;
		}
		if (this.building != null && this.building.isUsable() && pNewTemplate.ignore_same_building_id && this.building.asset == pNewTemplate)
		{
			return false;
		}
		if (!pNewTemplate.ignore_buildings && this.building != null && this.building.isUsable() && !this.building.asset.ignored_by_cities)
		{
			return false;
		}
		if (pNewTemplate.remove_buildings_when_dropped && this.building != null)
		{
			if (!this.building.isUsable() && pNewTemplate.remove_ruins)
			{
				return true;
			}
			if (!pNewTemplate.remove_civ_buildings && this.building.asset.city_building)
			{
				return false;
			}
		}
		return pNewTemplate.ignore_buildings || this.building == null || !this.building.asset.city_building || !this.building.isUsable() || this.building.asset.priority < pNewTemplate.priority;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000C2DC3 File Offset: 0x000C0FC3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasBuilding()
	{
		return this.building != null;
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x000C2DCE File Offset: 0x000C0FCE
	public void setRoad()
	{
		World.world.roads_calculator.setDirty(this);
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x000C2DE0 File Offset: 0x000C0FE0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isSameIsland(WorldTile pTile)
	{
		return pTile.region.island == this.region.island;
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000C2DFA File Offset: 0x000C0FFA
	public Color32 getColor()
	{
		return this.Type.color;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x000C2E08 File Offset: 0x000C1008
	internal void addNeighbour(WorldTile pNeighbour, TileDirection pDirection, List<WorldTile> pNeighbours, List<WorldTile> pNeighboursAll, bool pDiagonal = false)
	{
		if (pNeighbour == null)
		{
			this.world_edge = true;
			return;
		}
		pNeighboursAll.Add(pNeighbour);
		if (pDiagonal)
		{
			return;
		}
		pNeighbours.Add(pNeighbour);
		switch (pDirection)
		{
		case TileDirection.Left:
			this.tile_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.tile_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.tile_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.tile_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x000C2E6A File Offset: 0x000C106A
	public BiomeAsset getBiome()
	{
		if (this.Type.is_biome)
		{
			return this.Type.biome_asset;
		}
		return null;
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000C2E86 File Offset: 0x000C1086
	public bool is_liquid
	{
		get
		{
			return this.Type.liquid;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000C2E93 File Offset: 0x000C1093
	public TileTypeBase Type
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this.cur_tile_type;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000C2E9B File Offset: 0x000C109B
	// (set) Token: 0x06000EBC RID: 3772 RVA: 0x000C2EA8 File Offset: 0x000C10A8
	public int Height
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this.data.height;
		}
		set
		{
			this.data.height = value;
			if (this.data.height < 0)
			{
				this.data.height = 0;
				return;
			}
			if (this.data.height > 255)
			{
				this.data.height = 255;
			}
		}
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000C2F00 File Offset: 0x000C1100
	internal bool IsOceanAround()
	{
		for (int i = 0; i < this.neighbours.Length; i++)
		{
			if (this.neighbours[i].Type.layer_type == TileLayerType.Ocean)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x000C2F38 File Offset: 0x000C1138
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isGoodForBoat()
	{
		return this.Type.layer_type == TileLayerType.Ocean;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000C2F48 File Offset: 0x000C1148
	internal bool IsTypeAround(TileTypeBase pType)
	{
		for (int i = 0; i < this.neighbours.Length; i++)
		{
			if (this.neighbours[i].Type == pType)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000C2F7C File Offset: 0x000C117C
	internal bool startFire(bool pForce = false)
	{
		if (this.Type.explodable)
		{
			World.world.explosion_layer.explodeBomb(this, false);
		}
		if (!pForce && (!this.Type.burnable || this.isOnFire()))
		{
			return false;
		}
		if (this.Type.liquid)
		{
			return false;
		}
		this.unfreeze(99);
		bool tFireStarted = false;
		if (this.building != null && this.building.isBurnable())
		{
			ActionLibrary.addBurningEffectOnTarget(null, this.building, null);
			tFireStarted = true;
		}
		if (this.Type.burnable || tFireStarted || pForce)
		{
			tFireStarted = true;
			if (this.Type.IsType("fireworks"))
			{
				EffectsLibrary.spawn("fx_fireworks", this, null, null, 0f, -1f, -1f, null);
			}
			this.data.fire_timestamp = World.world.getCurWorldTime();
			if (this.Type.burnable)
			{
				this.health -= this.Type.burn_rate;
				this.setBurned(-1);
				World.world.flash_effects.flashPixel(this, 10, ColorType.White);
				if (this.health <= 0)
				{
					MapAction.decreaseTile(this, true, "flash");
				}
			}
			this.setFireData(true);
		}
		return tFireStarted;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x000C30BF File Offset: 0x000C12BF
	public void setFireData(bool pVal)
	{
		World.world.tile_manager.fires[this.data.tile_id] = pVal;
		if (this.isOnFire())
		{
			WorldBehaviourActionFire.addFire(this);
			return;
		}
		WorldBehaviourActionFire.removeFire(this);
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x000C30F4 File Offset: 0x000C12F4
	public void updateStats()
	{
		if (this.top_type != null)
		{
			this.cur_tile_type = this.top_type;
		}
		else
		{
			this.cur_tile_type = this.main_type;
		}
		WorldTile[] array = this.neighboursAll;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].wall_check_dirty = true;
		}
		if (this.isTemporaryFrozen())
		{
			if (!this.cur_tile_type.can_be_frozen)
			{
				this.data.frozen = false;
				return;
			}
			TopTileType tTypeFrozen = AssetManager.top_tiles.get(this.main_type.freeze_to_id);
			if (tTypeFrozen == null)
			{
				if (!this.main_type.can_be_frozen && this.cur_tile_type.can_be_frozen)
				{
					Debug.LogError("TILE SETTINGS CONFILICT! SET TOP TILE TO can_be_frozen FALSE!");
				}
				Debug.LogError("TILE 1 f:" + this.cur_tile_type.freeze_to_id + " m: " + this.cur_tile_type.id);
				Debug.LogError("TILE 2 f:" + this.main_type.freeze_to_id + " m: " + this.main_type.id);
				return;
			}
			this.cur_tile_type = tTypeFrozen;
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x000C3200 File Offset: 0x000C1400
	public void setTopTileType(TopTileType pTopTile, bool pUpdateStats = true)
	{
		if (this.top_type != pTopTile)
		{
			if (this.top_type != null)
			{
				this.zone.removeTileType(this.top_type, this);
			}
			if (pTopTile != null)
			{
				this.zone.addTileType(pTopTile, this);
			}
		}
		if (this.top_type != null)
		{
			this.top_type.hashsetRemove(this);
		}
		this.top_type = pTopTile;
		if (this.top_type != null)
		{
			this.top_type.hashsetAdd(this);
		}
		if (pUpdateStats)
		{
			World.world.setTileDirty(this);
			this.updateStats();
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x000C3283 File Offset: 0x000C1483
	public void setTileTypes(TileType pType, TopTileType pTopTile, bool pSetDirty = true)
	{
		this.setTopTileType(pTopTile, false);
		this.setTileType(pType, pSetDirty);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x000C3295 File Offset: 0x000C1495
	public void setTileTypes(string pType, TopTileType pTopTile)
	{
		this.setTopTileType(pTopTile, false);
		this.setTileType(pType);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x000C32A8 File Offset: 0x000C14A8
	public void setTileType(TileType pType, bool pSetDirty = true)
	{
		this.health = 10;
		if (this.zone != null)
		{
			if (this.main_type != pType)
			{
				if (this.main_type != null)
				{
					this.zone.removeTileType(this.main_type, this);
				}
				this.zone.addTileType(pType, this);
			}
			if (this.main_type == null)
			{
				if (pType.liquid)
				{
					this.zone.tiles_with_liquid++;
				}
				if (pType.ground)
				{
					this.zone.tiles_with_ground++;
				}
			}
			else
			{
				if (!this.main_type.liquid && pType.liquid)
				{
					this.zone.tiles_with_liquid++;
				}
				else if (this.main_type.liquid && !pType.liquid)
				{
					this.zone.tiles_with_liquid--;
				}
				if (!this.main_type.ground && pType.ground)
				{
					this.zone.tiles_with_ground++;
				}
				else if (this.main_type.ground && !pType.ground)
				{
					this.zone.tiles_with_ground--;
				}
			}
		}
		if (this.main_type != null)
		{
			this.main_type.hashsetRemove(this);
		}
		this.main_type = pType;
		this.main_type.hashsetAdd(this);
		this.updateStats();
		if (pSetDirty)
		{
			World.world.setTileDirty(this);
		}
		this.timestamp_type_changed = World.world.getCurWorldTime();
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x000C3430 File Offset: 0x000C1630
	public void setTileType(string pType)
	{
		TileType newType = AssetManager.tiles.get(pType);
		if (newType == null)
		{
			newType = TileLibrary.soil_low;
		}
		this.setTileType(newType, true);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x000C345C File Offset: 0x000C165C
	public void setBurned(int pForceVal = -1)
	{
		if (!this.Type.can_be_set_on_fire)
		{
			return;
		}
		if (pForceVal == -1)
		{
			this.setBurnedStage(15 - Randy.randomInt(0, 10));
		}
		else
		{
			this.setBurnedStage(this.burned_stages);
		}
		World.world.burned_layer.setTileDirty(this);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x000C34AA File Offset: 0x000C16AA
	public void setBurnedStage(int pValue)
	{
		if (this.burned_stages == 0 && pValue == 0)
		{
			return;
		}
		this.burned_stages = pValue;
		WorldBehaviourActionBurnedTiles.addTile(this);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x000C34C5 File Offset: 0x000C16C5
	public void removeBurn()
	{
		if (this.burned_stages == 0)
		{
			return;
		}
		this.setBurnedStage(0);
		World.world.burned_layer.setTileDirty(this);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x000C34E7 File Offset: 0x000C16E7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isOnFire()
	{
		return World.world.tile_manager.fires[this.data.tile_id];
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x000C3504 File Offset: 0x000C1704
	internal void stopFire()
	{
		if (!this.isOnFire())
		{
			return;
		}
		this.setFireData(false);
		this.data.fire_timestamp = -1.0;
		this.setBurned(-1);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x000C3531 File Offset: 0x000C1731
	internal bool canGrow()
	{
		return !this.isOnFire() && this.burned_stages == 0;
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000C3546 File Offset: 0x000C1746
	public void removeTrees(bool pFlash = true)
	{
		if (pFlash)
		{
			World.world.flash_effects.flashPixel(this, 20, ColorType.White);
		}
		World.world.setTileDirty(this);
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x000C3569 File Offset: 0x000C1769
	public void removeGrass(bool pFlash = true)
	{
		if (pFlash)
		{
			World.world.flash_effects.flashPixel(this, 20, ColorType.White);
		}
		MapAction.removeGreens(this);
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x000C3587 File Offset: 0x000C1787
	public void topTileEaten(int pTicks = 5)
	{
		this.removeGrass(true);
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x000C3590 File Offset: 0x000C1790
	public bool isTileRank(TileRank pRank)
	{
		return this.main_type.rank_type == pRank;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000C35A0 File Offset: 0x000C17A0
	internal void clearUnits()
	{
		this._units.Clear();
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x000C35B0 File Offset: 0x000C17B0
	internal void clear()
	{
		this.last_rendered_tile_type = null;
		this.health = 10;
		this.minimap_building_x = 0;
		this.minimap_building_y = 0;
		this.clearUnits();
		this.cleanTargetedBy();
		this.explosion_wave = 0;
		this.explosion_power = 0;
		this.pollinated = 0;
		this.setTileTypes(TileLibrary.deep_ocean, null, false);
		this.delayed_timer_bomb = 0f;
		this.Height = 0;
		this.current_rendered_tile_graphics = null;
		this.heat = 0;
		this.flash_state = 0;
		this.burned_stages = 0;
		this.building = null;
		this.data.clear();
		this.explosion_fx_stage = 0;
		this.region = null;
		this.last_rendered_pos_tile = WorldTilemap.EMPTY_TILE_POS;
		this.world_tile_zone_border.reset();
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x000C366C File Offset: 0x000C186C
	public void Dispose()
	{
		this.clear();
		this.wall_check_dirty = false;
		this._has_walls_around = false;
		if (this.main_type != null)
		{
			this.main_type.hashsetRemove(this);
		}
		this.main_type = null;
		if (this.top_type != null)
		{
			this.top_type.hashsetRemove(this);
		}
		this.top_type = null;
		this.cur_tile_type = null;
		this.color_array = null;
		this.tile_up = null;
		this.tile_down = null;
		this.tile_left = null;
		this.tile_right = null;
		this.neighbours = null;
		this.neighboursAll = null;
		this.road_island = null;
		this.world_tile_zone_border = null;
		this.region = null;
		this.zone = null;
		this.chunk = null;
		UnsafeCollectionPool<List<Actor>, Actor>.Release(this._units);
		this._units = null;
		this.data = null;
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x000C3737 File Offset: 0x000C1937
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int GetHashCode()
	{
		return this.data.tile_id;
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x000C3744 File Offset: 0x000C1944
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(WorldTile pTile)
	{
		return this.data.tile_id == pTile.data.tile_id;
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x000C375E File Offset: 0x000C195E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool reachableFrom(WorldTile pFromTile)
	{
		return this.isSameIsland(pFromTile) || this.region.island.reachableByCityFrom(pFromTile.region.island);
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x000C3788 File Offset: 0x000C1988
	public bool freeze(int pDamage = 1)
	{
		if (!this.canBeFrozen())
		{
			return false;
		}
		if (this.building != null && this.building.isUsable() && this.building.asset.prevent_freeze)
		{
			return false;
		}
		this.data.frozen = true;
		if (this.Type.fast_freeze)
		{
			for (int i = 0; i < this.neighbours.Length; i++)
			{
				WorldTile tTile = this.neighbours[i];
				if (tTile.Type.fast_freeze && tTile.canBeFrozen() && Randy.randomChance(0.35f))
				{
					tTile.freeze(pDamage);
				}
			}
		}
		this.health = 10;
		World.world.setTileDirty(this);
		if (this.zone.visible)
		{
			World.world.flash_effects.flashPixel(this, 20, ColorType.White);
		}
		if (this.Type.chunk_dirty_when_temperature)
		{
			MapAction.checkTileState(this, this.main_type, true);
			this.updateStats();
		}
		return true;
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x000C387C File Offset: 0x000C1A7C
	public void unfreeze(int pDamage = 1)
	{
		if (!this.canBeUnFrozen())
		{
			return;
		}
		if (this.health > 0)
		{
			this.health -= pDamage;
			if (this.health > 0)
			{
				return;
			}
		}
		this.data.frozen = false;
		this.health = 10;
		World.world.setTileDirty(this);
		if (this.zone.visible)
		{
			World.world.flash_effects.flashPixel(this, 20, ColorType.White);
		}
		if (this.Type.chunk_dirty_when_temperature)
		{
			MapAction.checkTileState(this, this.main_type, true);
			this.updateStats();
		}
		if (this.Type.fast_freeze)
		{
			for (int i = 0; i < this.neighbours.Length; i++)
			{
				WorldTile tTile = this.neighbours[i];
				if (tTile.canBeUnFrozen() && Randy.randomChance(0.2f))
				{
					tTile.unfreeze(pDamage);
				}
			}
		}
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000C3956 File Offset: 0x000C1B56
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isSameLayer(WorldTile pTile1, WorldTile pTile2)
	{
		return pTile1.Type.layer_type == pTile2.Type.layer_type;
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x000C3970 File Offset: 0x000C1B70
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canBeFrozen()
	{
		return !this.isFrozen() && this.Type.can_be_frozen;
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000C3987 File Offset: 0x000C1B87
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canBeUnFrozen()
	{
		return this.data.frozen && this.Type.can_be_unfrozen && !this.Type.forever_frozen;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x000C39B3 File Offset: 0x000C1BB3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isTemporaryFrozen()
	{
		return this.data.frozen;
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x000C39C0 File Offset: 0x000C1BC0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isFrozen()
	{
		return this.data.frozen || this.Type.forever_frozen;
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000C39DC File Offset: 0x000C1BDC
	public TileRank getCreepTileRank()
	{
		return this.main_type.creep_rank_type;
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000C39E9 File Offset: 0x000C1BE9
	public City zone_city
	{
		get
		{
			return this.zone.city;
		}
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x000C39F6 File Offset: 0x000C1BF6
	public bool hasCity()
	{
		return this.zone_city != null;
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000C3A01 File Offset: 0x000C1C01
	public bool has_tile_up
	{
		get
		{
			return this.tile_up != null;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x000C3A0C File Offset: 0x000C1C0C
	public bool has_tile_down
	{
		get
		{
			return this.tile_down != null;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000C3A17 File Offset: 0x000C1C17
	public bool has_tile_left
	{
		get
		{
			return this.tile_left != null;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000C3A22 File Offset: 0x000C1C22
	public bool has_tile_right
	{
		get
		{
			return this.tile_right != null;
		}
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x000C3A2D File Offset: 0x000C1C2D
	public void tryToBreak()
	{
		this.health = 0;
		this.unfreeze(99);
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000C3A40 File Offset: 0x000C1C40
	public WorldTile getWalkableTileAround(WorldTile pFrom)
	{
		foreach (WorldTile tTile in this.neighboursAll.LoopRandom<WorldTile>())
		{
			if (tTile.isSameIsland(pFrom))
			{
				return tTile;
			}
		}
		return null;
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x000C3A9C File Offset: 0x000C1C9C
	public IEnumerable<WorldTile> getTilesAround(int pRadius)
	{
		int num;
		for (int iX = -pRadius; iX <= pRadius; iX = num + 1)
		{
			for (int iY = -pRadius; iY <= pRadius; iY = num + 1)
			{
				int tTileX = this.x + iX;
				int tTileY = this.y + iY;
				WorldTile tTile = World.world.GetTile(tTileX, tTileY);
				yield return tTile;
				num = iY;
			}
			num = iX;
		}
		yield break;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000C3AB4 File Offset: 0x000C1CB4
	public WorldTile getTileAroundThisOnSameIsland(WorldTile pTileFrom)
	{
		foreach (WorldTile tTile in this.neighboursAll.LoopRandom<WorldTile>())
		{
			if (tTile.isSameIsland(this))
			{
				return tTile;
			}
		}
		return null;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x000C3B10 File Offset: 0x000C1D10
	public WorldTile getTileAroundThisOnSameIsland(WorldTile pTileFrom, bool pClosest)
	{
		if (!pClosest)
		{
			return this.getTileAroundThisOnSameIsland(pTileFrom);
		}
		int tBestDist = int.MaxValue;
		WorldTile tBestTile = null;
		foreach (WorldTile tTile in this.neighboursAll)
		{
			int tDist = Toolbox.SquaredDistTile(pTileFrom, tTile);
			if (tDist < tBestDist && tTile.isSameIsland(this))
			{
				tBestDist = tDist;
				tBestTile = tTile;
			}
		}
		return tBestTile;
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x000C3B6C File Offset: 0x000C1D6C
	public bool isDiagonal(WorldTile pTile)
	{
		int num = Math.Abs(pTile.x - this.x);
		int tY = Math.Abs(pTile.y - this.y);
		return num == 1 && tY == 1;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x000C3BA8 File Offset: 0x000C1DA8
	public bool isSameCityHere(City pCity)
	{
		return this.zone.isSameCityHere(pCity);
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x000C3BB8 File Offset: 0x000C1DB8
	public bool isWaterAround()
	{
		return !this.has_tile_down || !this.has_tile_up || !this.has_tile_left || !this.has_tile_right || (this.tile_down.Type.liquid || this.tile_up.Type.liquid || this.tile_left.Type.liquid || this.tile_right.Type.liquid);
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000C3C32 File Offset: 0x000C1E32
	public int random_animation_seed
	{
		get
		{
			return World.world.tile_manager.random_seeds[this.data.tile_id];
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000C3C4F File Offset: 0x000C1E4F
	public int tile_id
	{
		get
		{
			return this.data.tile_id;
		}
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x000C3C5C File Offset: 0x000C1E5C
	public float distanceTo(WorldTile pTile)
	{
		return Toolbox.DistTile(this, pTile);
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x000C3C68 File Offset: 0x000C1E68
	public WorldTile getNeighbourTileSameIsland()
	{
		foreach (WorldTile tTile in this.neighboursAll.LoopRandom<WorldTile>())
		{
			if (tTile.isSameIsland(this))
			{
				return tTile;
			}
		}
		return this;
	}

	// Token: 0x04000ECF RID: 3791
	[CanBeNull]
	public TopTileType top_type;

	// Token: 0x04000ED0 RID: 3792
	[CanBeNull]
	public TileType main_type;

	// Token: 0x04000ED1 RID: 3793
	private TileTypeBase cur_tile_type;

	// Token: 0x04000ED2 RID: 3794
	public bool obstacle_is_around;

	// Token: 0x04000ED3 RID: 3795
	internal TileBase current_rendered_tile_graphics;

	// Token: 0x04000ED4 RID: 3796
	public int burned_stages;

	// Token: 0x04000ED5 RID: 3797
	internal WorldTileZoneBorder world_tile_zone_border = new WorldTileZoneBorder();

	// Token: 0x04000ED6 RID: 3798
	public const int DEFAULT_HEALTH = 10;

	// Token: 0x04000ED7 RID: 3799
	public int health = 10;

	// Token: 0x04000ED8 RID: 3800
	public Vector3Int last_rendered_border_pos_ocean;

	// Token: 0x04000ED9 RID: 3801
	public Vector3Int last_rendered_pos_tile;

	// Token: 0x04000EDA RID: 3802
	public TileTypeBase last_rendered_tile_type;

	// Token: 0x04000EDB RID: 3803
	public float delayed_timer_bomb;

	// Token: 0x04000EDC RID: 3804
	public string delayed_bomb_type = "";

	// Token: 0x04000EDD RID: 3805
	public double timestamp_type_changed;

	// Token: 0x04000EDE RID: 3806
	public WorldTileData data;

	// Token: 0x04000EDF RID: 3807
	public int heat;

	// Token: 0x04000EE0 RID: 3808
	internal int explosion_wave;

	// Token: 0x04000EE1 RID: 3809
	internal int explosion_power;

	// Token: 0x04000EE2 RID: 3810
	private Actor _targeted_by;

	// Token: 0x04000EE3 RID: 3811
	public bool world_edge;

	// Token: 0x04000EE4 RID: 3812
	public WorldTile tile_up;

	// Token: 0x04000EE5 RID: 3813
	public WorldTile tile_down;

	// Token: 0x04000EE6 RID: 3814
	public WorldTile tile_left;

	// Token: 0x04000EE7 RID: 3815
	public WorldTile tile_right;

	// Token: 0x04000EE8 RID: 3816
	public WorldTile[] neighbours;

	// Token: 0x04000EE9 RID: 3817
	public WorldTile[] neighboursAll;

	// Token: 0x04000EEA RID: 3818
	public TileIsland road_island;

	// Token: 0x04000EEB RID: 3819
	public int pollinated;

	// Token: 0x04000EEC RID: 3820
	public readonly int x;

	// Token: 0x04000EED RID: 3821
	public readonly int y;

	// Token: 0x04000EEE RID: 3822
	public readonly Vector2Int pos;

	// Token: 0x04000EEF RID: 3823
	public readonly Vector3 posV3;

	// Token: 0x04000EF0 RID: 3824
	public readonly Vector3 posV;

	// Token: 0x04000EF1 RID: 3825
	internal int minimap_building_x;

	// Token: 0x04000EF2 RID: 3826
	internal int minimap_building_y;

	// Token: 0x04000EF3 RID: 3827
	internal int flash_state;

	// Token: 0x04000EF4 RID: 3828
	internal ColorArray color_array;

	// Token: 0x04000EF5 RID: 3829
	public MapRegion region;

	// Token: 0x04000EF6 RID: 3830
	public TileZone zone;

	// Token: 0x04000EF7 RID: 3831
	public MapChunk chunk;

	// Token: 0x04000EF8 RID: 3832
	public Building building;

	// Token: 0x04000EF9 RID: 3833
	private List<Actor> _units;

	// Token: 0x04000EFA RID: 3834
	internal int explosion_fx_stage;

	// Token: 0x04000EFB RID: 3835
	internal bool is_checked_tile;

	// Token: 0x04000EFC RID: 3836
	internal int score = -1;

	// Token: 0x04000EFD RID: 3837
	public bool wall_check_dirty;

	// Token: 0x04000EFE RID: 3838
	private bool _has_walls_around;
}
