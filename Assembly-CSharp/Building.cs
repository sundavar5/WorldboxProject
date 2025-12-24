using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class Building : BaseSimObject, IEquatable<Building>, IComparable<Building>, ILoadable<BuildingData>
{
	// Token: 0x1700012E RID: 302
	// (get) Token: 0x0600153C RID: 5436 RVA: 0x000DD2A0 File Offset: 0x000DB4A0
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Building;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600153D RID: 5437 RVA: 0x000DD2A4 File Offset: 0x000DB4A4
	internal WorldTile door_tile
	{
		get
		{
			if (!this.current_tile.has_tile_down)
			{
				return this.current_tile;
			}
			return this.current_tile.tile_down;
		}
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000DD2C8 File Offset: 0x000DB4C8
	internal bool isBurnable()
	{
		if (!base.hasHealth())
		{
			return false;
		}
		if (base.hasCity())
		{
			City tCity = this.getCity();
			if (tCity.hasReligion() && tCity.getReligion().hasMetaTag("building_immunity_fire"))
			{
				return false;
			}
		}
		return this.asset.burnable;
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000DD315 File Offset: 0x000DB515
	public float getExistenceTime()
	{
		return World.world.getWorldTimeElapsedSince(this.data.created_time);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000DD32C File Offset: 0x000DB52C
	public float getExistenceMonths()
	{
		return this.getExistenceTime() / 5f;
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x000DD33C File Offset: 0x000DB53C
	public void setAnimData(int pIndex)
	{
		if (pIndex >= this.asset.building_sprites.animation_data.Count || pIndex < 0)
		{
			pIndex = 0;
		}
		this.animData = this.asset.building_sprites.animation_data[pIndex];
		this.animData_index = pIndex;
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000DD38B File Offset: 0x000DB58B
	internal void stopFire()
	{
		base.finishStatusEffect("burning");
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000DD398 File Offset: 0x000DB598
	internal override void create()
	{
		base.create();
		base.setObjectType(MapObjectType.Building);
		this.startShake(0.3f, 0.1f, 0.1f);
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000DD3BC File Offset: 0x000DB5BC
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this.flip_x = false;
		this.positionDirty = false;
		this.sprite_dirty = false;
		this.tiles_dirty = false;
		this._last_colored_sprite = null;
		this._last_color_asset = null;
		this._shake_timer = 0f;
		this.lastAngle = 0f;
		this.residents.Clear();
		this._shake_offset = Vector2.zero;
		this.animation_state = BuildingAnimationState.Normal;
		this.state_ownership = BuildingOwnershipState.None;
		this.chopped = false;
		this.is_visible = false;
		this.check_spawn_animation = false;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000DD448 File Offset: 0x000DB648
	private T addComponent<T>() where T : BaseBuildingComponent, new()
	{
		T pObject = World.world.buildings.component_pool.get<T>();
		if (this.components_list == null)
		{
			this.components_list = new ListPool<BaseBuildingComponent>();
		}
		this.components_list.Add(pObject);
		pObject.create(this);
		this.batch.c_components.Add(this);
		return pObject;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000DD4AC File Offset: 0x000DB6AC
	public bool hasBooks()
	{
		return this.data.books != null && this.data.books.hasAny();
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x000DD4CD File Offset: 0x000DB6CD
	public bool hasFreeBookSlot()
	{
		return this.asset.book_slots != 0 && this.asset.book_slots > this.data.books.totalBooks();
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000DD4FB File Offset: 0x000DB6FB
	public void addBook(Book pBook)
	{
		this.data.books.addBook(pBook);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000DD50E File Offset: 0x000DB70E
	public bool isState(BuildingState pState)
	{
		return this.data.state == pState;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x000DD520 File Offset: 0x000DB720
	internal void setBuilding(WorldTile pTile, BuildingAsset pAsset, BuildingData pData)
	{
		this.current_tile = pTile;
		this.current_tile.zone.addBuildingMain(this);
		if (pData == null)
		{
			this.setTemplate(pAsset);
			this.data.mainX = pTile.pos.x;
			this.data.mainY = pTile.pos.y;
			this.setState(BuildingState.Normal);
			this.updateStats();
			base.setMaxHealth();
			if (this.asset.has_resources_grown_to_collect)
			{
				this.setHaveResourcesToCollect(this.asset.has_resources_grown_to_collect_on_spawn);
			}
		}
		else
		{
			this.setData(pData);
			this.setTemplate(pAsset);
		}
		this.setStatsDirty();
		this.current_position = this.current_tile.pos;
		this.current_scale.x = this.asset.scale_base.x;
		this.current_scale.y = this.asset.scale_base.y;
		this.fillTiles();
		if (!string.IsNullOrEmpty(this.asset.kingdom))
		{
			Kingdom tKingdom = World.world.kingdoms_wild.get(this.asset.kingdom);
			this.setKingdom(tKingdom);
		}
		if (!this.isUnderConstruction())
		{
			int tFrame = -1;
			if (pData != null)
			{
				tFrame = pData.frameID;
			}
			this.initAnimationData();
			if (tFrame != -1)
			{
				this.setAnimData(tFrame);
			}
		}
		this.checkMaterial();
		this.setPositionDirty();
		this.updatePosition();
		if (pAsset.storage && this.data.resources == null)
		{
			this.data.resources = new CityResources();
		}
		if (pAsset.book_slots > 0 && this.data.books == null)
		{
			this.data.books = new StorageBooks();
		}
		if (pAsset.smoke)
		{
			this.addComponent<BuildingSmokeEffect>();
		}
		if (pAsset.building_type == BuildingType.Building_Poops)
		{
			this.batch.c_poop.Add(this);
		}
		if (pAsset.spread)
		{
			switch (pAsset.flora_type)
			{
			case FloraType.Tree:
				this.batch.c_spread_trees.Add(this);
				break;
			case FloraType.Fungi:
				this.batch.c_spread_fungi.Add(this);
				break;
			case FloraType.Plant:
				this.batch.c_spread_plants.Add(this);
				break;
			}
		}
		if (pAsset.produce_biome_food)
		{
			this.component_food_producer = this.addComponent<BuildingBiomeFoodProducer>();
		}
		if (pAsset.spawn_drops)
		{
			this.addComponent<BuildingEffectSpawnDrop>();
		}
		if (pAsset.id == "monolith")
		{
			this.component_monolith = this.addComponent<BuildingMonolith>();
		}
		if (pAsset.waypoint)
		{
			string kingdom = pAsset.kingdom;
			BuildingWaypoint buildingWaypoint;
			if (!(kingdom == "alien_mold"))
			{
				if (!(kingdom == "computer"))
				{
					if (!(kingdom == "golden_egg"))
					{
						if (!(kingdom == "harp"))
						{
							throw new ArgumentOutOfRangeException(pAsset.kingdom + " is not a valid kingdom for a waypoint");
						}
						buildingWaypoint = this.addComponent<BuildingWaypointHarp>();
					}
					else
					{
						buildingWaypoint = this.addComponent<BuildingWaypointGoldenEgg>();
					}
				}
				else
				{
					buildingWaypoint = this.addComponent<BuildingWaypointComputer>();
				}
			}
			else
			{
				buildingWaypoint = this.addComponent<BuildingWaypointAlienMold>();
			}
			this.component_waypoint = buildingWaypoint;
		}
		if (pAsset.grow_creep)
		{
			this.addComponent<BuildingCreepHUB>();
		}
		if (pAsset.wheat)
		{
			this.component_wheat = this.addComponent<Wheat>();
		}
		if (pAsset.building_type == BuildingType.Building_Fruits)
		{
			this.component_fruit_growth = this.addComponent<BuildingFruitGrowth>();
		}
		if (pAsset.ice_tower)
		{
			this.addComponent<IceTower>();
		}
		if (pAsset.id == "poop")
		{
			this.addComponent<Poop>();
		}
		if (pAsset.spawn_units)
		{
			this.component_unit_spawner = this.addComponent<UnitSpawner>();
		}
		if (pAsset.spread_biome)
		{
			this.component_biome_spreader = this.addComponent<BuildingSpreadBiome>();
		}
		if (pAsset.beehive)
		{
			this.component_beehive = this.addComponent<Beehive>();
		}
		if (pAsset.docks)
		{
			this.component_docks = this.addComponent<Docks>();
		}
		if (pAsset.tower)
		{
			this.addComponent<BuildingTower>();
		}
		if (pData == null && !pAsset.city_building)
		{
			this.setAnimationState(BuildingAnimationState.Normal);
			this.setScaleTween(0f, 0.2f, 1f, null, null, 0);
		}
		if (this.isRuin())
		{
			this.makeRuins();
			return;
		}
		if (this.asset.city_building && base.hasCity())
		{
			this.setKingdom(this.current_tile.zone_city.kingdom);
			return;
		}
		if (this.asset.city_building && !base.hasCity() && this.isAbandoned())
		{
			this.makeAbandoned();
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000DD973 File Offset: 0x000DBB73
	private void debugCheckResourcesOnSpawn(BuildingAsset pAsset)
	{
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000DD975 File Offset: 0x000DBB75
	public override void setStatsDirty()
	{
		base.setStatsDirty();
		if (base.isAlive())
		{
			this.batch.c_stats_dirty.Add(this);
		}
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000DD996 File Offset: 0x000DBB96
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void setPositionDirty()
	{
		this.positionDirty = true;
		this.batch.c_position_dirty.Add(this);
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000DD9B0 File Offset: 0x000DBBB0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override BaseObjectData getData()
	{
		return this.data;
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000DD9B8 File Offset: 0x000DBBB8
	public void setData(BuildingData pData)
	{
		this.data = pData;
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000DD9C1 File Offset: 0x000DBBC1
	public void loadData(BuildingData pData)
	{
		this.setData(pData);
		pData.load();
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x000DD9D0 File Offset: 0x000DBBD0
	public void loadBuilding(BuildingData pData)
	{
		if (!this.isUnderConstruction())
		{
			this.setAnimData(pData.frameID);
		}
		if (this.data.resources != null)
		{
			this.resources.loadFromSave();
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000DD9FE File Offset: 0x000DBBFE
	internal void setHaveResourcesToCollect(bool pValue)
	{
		if (pValue)
		{
			this.data.addFlag("has_resources");
			return;
		}
		this.data.removeFlag("has_resources");
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000DDA25 File Offset: 0x000DBC25
	public bool hasResourcesToCollect()
	{
		if (this.asset.has_resources_grown_to_collect)
		{
			return this.data.hasFlag("has_resources");
		}
		return !this.chopped && this.asset.has_resources_to_collect;
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000DDA5A File Offset: 0x000DBC5A
	internal bool canBeUpgraded()
	{
		return !this.isUnderConstruction() && (!this.asset.city_building || this.isCiv()) && this.asset.can_be_upgraded;
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000DDA88 File Offset: 0x000DBC88
	internal bool upgradeBuilding()
	{
		if (!this.canBeUpgraded())
		{
			return false;
		}
		BuildingAsset tNewTemplate = AssetManager.buildings.get(this.asset.upgrade_to);
		if ((tNewTemplate.fundament.left != this.asset.fundament.left || tNewTemplate.fundament.right != this.asset.fundament.right || tNewTemplate.fundament.top != this.asset.fundament.top || tNewTemplate.fundament.bottom != this.asset.fundament.bottom) && !this.checkTilesForUpgrade(this.current_tile, tNewTemplate))
		{
			return false;
		}
		this.makeZoneDirty();
		this.setTemplate(tNewTemplate);
		this.initAnimationData();
		this.updateStats();
		base.setMaxHealth();
		this.fillTiles();
		return true;
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000DDB60 File Offset: 0x000DBD60
	private void setTemplate(BuildingAsset pTemplate)
	{
		this.asset = pTemplate;
		this.data.asset_id = this.asset.id;
		this.asset.buildings.Add(this);
		if (this.asset.canBeOccupied())
		{
			World.world.buildings.occupied_buildings.Add(this);
		}
		this.asset.checkSpritesAreLoaded();
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000DDBCA File Offset: 0x000DBDCA
	internal void setMaterial(string pMaterialID)
	{
		this.material = LibraryMaterials.instance.dict[pMaterialID];
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000DDBE2 File Offset: 0x000DBDE2
	internal void setKingdomCiv(Kingdom pKingdom)
	{
		if (this.kingdom == pKingdom && base.hasKingdom())
		{
			return;
		}
		this.setKingdom(pKingdom);
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000DDBFD File Offset: 0x000DBDFD
	internal void makeRuins()
	{
		this.setKingdom(World.world.kingdoms_wild.get("ruins"));
		this.setState(BuildingState.Ruins);
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000DDC20 File Offset: 0x000DBE20
	public void makeAbandoned()
	{
		this.setKingdom(WildKingdomsManager.abandoned);
		if (this.isUnderConstruction())
		{
			this.startDestroyBuilding();
			return;
		}
		if (!this.asset.can_be_abandoned)
		{
			if (this.asset.has_ruin_state)
			{
				this.startMakingRuins();
				return;
			}
			this.startDestroyBuilding();
		}
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000DDC70 File Offset: 0x000DBE70
	public void setKingdom(Kingdom pKingdom)
	{
		if (this.kingdom == pKingdom)
		{
			return;
		}
		if (this.kingdom != pKingdom)
		{
			this.makeZoneDirty();
		}
		this.checkKingdom();
		this.kingdom = pKingdom;
		this.checkKingdom();
		if (base.isKingdomCiv())
		{
			this.setOwnershipState(BuildingOwnershipState.Civilization);
		}
		else
		{
			this.setOwnershipState(BuildingOwnershipState.World);
		}
		this.setTilesDirty();
		World.world.sim_object_zones.setBuildingsDirty(base.chunk);
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000DDCDC File Offset: 0x000DBEDC
	private void checkKingdom()
	{
		if (base.hasKingdom())
		{
			if (this.kingdom.wild)
			{
				World.world.kingdoms_wild.setDirtyBuildings();
				return;
			}
			World.world.kingdoms.setDirtyBuildings();
		}
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000DDD12 File Offset: 0x000DBF12
	public bool hasHousingLogic()
	{
		return this.asset.canBeOccupied();
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x000DDD24 File Offset: 0x000DBF24
	private void setState(BuildingState pState)
	{
		if (this.hasHousingLogic())
		{
			World.world.buildings.event_houses = true;
		}
		if (this.isRemoved())
		{
			return;
		}
		if (pState == BuildingState.Ruins && !this.isRuin())
		{
			bool tIsOnLava = false;
			if (tIsOnLava)
			{
				using (List<WorldTile>.Enumerator enumerator = this.tiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Type.lava)
						{
							tIsOnLava = true;
							break;
						}
					}
				}
			}
			if (tIsOnLava)
			{
				base.setHealth(base.getMaxHealthPercent(0.5f), true);
			}
			else
			{
				base.setMaxHealth();
			}
			this.stats["health"] = (float)base.getHealth();
		}
		this.data.state = pState;
		this.checkAutoRemove();
		this.checkMaterial();
		this.clearZones();
		if (!this.isRemoved())
		{
			this.fillTiles();
		}
		this.setTilesDirty();
		World.world.sim_object_zones.setBuildingsDirty(base.chunk);
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000DDE2C File Offset: 0x000DC02C
	public void checkMaterial()
	{
		if (this.data.state == BuildingState.Ruins)
		{
			this.setMaterial(BuildingRendererSettings.cur_default_material);
			return;
		}
		if (BuildingRendererSettings.wobbly_material_enabled)
		{
			this.setMaterial(this.asset.material);
			return;
		}
		this.setMaterial(BuildingRendererSettings.cur_default_material);
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000DDE6C File Offset: 0x000DC06C
	internal void updateKingdomColors()
	{
		this.setTilesDirty();
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000DDE74 File Offset: 0x000DC074
	internal bool checkTilesForUpgrade(WorldTile pTile, BuildingAsset pTemplate)
	{
		int tX = pTile.pos.x - pTemplate.fundament.left;
		int tY = pTile.pos.y - pTemplate.fundament.bottom;
		int tWidth = pTemplate.fundament.right + pTemplate.fundament.left + 1;
		int tHeight = pTemplate.fundament.top + pTemplate.fundament.bottom + 1;
		for (int xx = 0; xx < tWidth; xx++)
		{
			for (int yy = 0; yy < tHeight; yy++)
			{
				WorldTile tTile = World.world.GetTile(tX + xx, tY + yy);
				if (tTile == null)
				{
					return false;
				}
				if (!tTile.Type.can_build_on)
				{
					return false;
				}
				if (tTile.zone.city != this.city)
				{
					return false;
				}
				Building tBuilding = tTile.building;
				if (tBuilding != null && tBuilding != this)
				{
					if (tBuilding.asset.priority >= this.asset.priority)
					{
						return false;
					}
					if (tBuilding.asset.upgrade_level >= this.asset.upgrade_level)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000DDFA1 File Offset: 0x000DC1A1
	internal void debugConstructions()
	{
		if (this.asset.building_sprites.construction == null)
		{
			return;
		}
		this.setUnderConstruction();
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000DDFC4 File Offset: 0x000DC1C4
	private void initAnimationData()
	{
		this.asset.checkSpritesAreLoaded();
		int tRandomVariation = Randy.randomInt(0, this.asset.building_sprites.animation_data.Count);
		this.setAnimData(tRandomVariation);
		if (this.asset.random_flip && !this.asset.shadow)
		{
			this.flip_x = Randy.randomBool();
		}
		this.setScaleTween(0f, 0.2f, 1f, null, null, 0);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000DE03C File Offset: 0x000DC23C
	private void fillTiles()
	{
		if (this.tiles.Count != 0)
		{
			this.clearTiles();
		}
		int tX = this.current_tile.pos.x - this.asset.fundament.left;
		int tY = this.current_tile.pos.y - this.asset.fundament.bottom;
		int tWidth = this.asset.fundament.right + this.asset.fundament.left + 1;
		int tHeight = this.asset.fundament.top + this.asset.fundament.bottom + 1;
		int num = 0;
		int tStartY = 0;
		for (int xx = num; xx < tWidth; xx++)
		{
			for (int yy = tStartY; yy < tHeight; yy++)
			{
				WorldTile tTile = World.world.GetTile(tX + xx, tY + yy);
				if (tTile != null)
				{
					this.setBuildingTile(tTile, xx, yy);
				}
			}
		}
		this.setTilesDirty();
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000DE140 File Offset: 0x000DC340
	internal void checkDirtyTiles()
	{
		if (!this.tiles_dirty)
		{
			return;
		}
		this.tiles_dirty = false;
		for (int i = 0; i < this.tiles.Count; i++)
		{
			WorldTile tTile = this.tiles[i];
			World.world.setTileDirty(tTile);
		}
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_tiles_dirty.Remove(this);
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000DE1A1 File Offset: 0x000DC3A1
	private void setTilesDirty()
	{
		this.tiles_dirty = true;
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_tiles_dirty.Add(this);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000DE1C0 File Offset: 0x000DC3C0
	private void forceUpdateTilesDirty()
	{
		this.setTilesDirty();
		this.checkDirtyTiles();
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000DE1D0 File Offset: 0x000DC3D0
	private void setBuildingTile(WorldTile pTile, int pX, int pY)
	{
		if (pTile.hasBuilding() && pTile.building != this)
		{
			pTile.building.startDestroyBuilding();
		}
		pTile.building = this;
		pTile.minimap_building_x = pX;
		pTile.minimap_building_y = pY;
		if (!this.tiles.Contains(pTile))
		{
			this.tiles.Add(pTile);
			if (!this.zones.Contains(pTile.zone))
			{
				this.zones.Add(pTile.zone);
			}
		}
		TileType tType = null;
		TopTileType tTopType = null;
		if (this.asset.transform_tiles_to_tile_type != null)
		{
			tType = AssetManager.tiles.get(this.asset.transform_tiles_to_tile_type);
		}
		if (this.asset.transform_tiles_to_top_tiles != null)
		{
			tTopType = AssetManager.top_tiles.get(this.asset.transform_tiles_to_top_tiles);
		}
		if (tType != null || tTopType != null)
		{
			if (tType == null)
			{
				tType = pTile.main_type;
			}
			if (tType.can_be_biome)
			{
				MapAction.terraformTile(pTile, tType, tTopType, TerraformLibrary.nothing, false);
			}
		}
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000DE2BC File Offset: 0x000DC4BC
	public void setOwnershipState(BuildingOwnershipState pState)
	{
		if (this.state_ownership != pState)
		{
			this.makeZoneDirty();
		}
		this.state_ownership = pState;
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000DE2D4 File Offset: 0x000DC4D4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isRuin()
	{
		return this.data.state == BuildingState.Ruins;
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000DE2E4 File Offset: 0x000DC4E4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isRemoved()
	{
		return this.data.state == BuildingState.Removed;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isNormal()
	{
		return this.data.state == BuildingState.Normal;
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x000DE304 File Offset: 0x000DC504
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isAbandoned()
	{
		return this.state_ownership == BuildingOwnershipState.World && this.asset.city_building;
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000DE31C File Offset: 0x000DC51C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isCiv()
	{
		return this.state_ownership == BuildingOwnershipState.Civilization;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x000DE328 File Offset: 0x000DC528
	public void prepareForSave()
	{
		if (base.hasCity())
		{
			this.data.cityID = this.city.data.id;
		}
		else
		{
			this.data.cityID = -1L;
		}
		CityResources resources = this.resources;
		if (resources != null)
		{
			resources.save();
		}
		this.data.frameID = this.animData_index;
		this.data.save();
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000DE394 File Offset: 0x000DC594
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isUsable()
	{
		return base.isAlive() && !this.isRuin() && !this.isOnRemove() && !this.isRemoved();
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000DE3BF File Offset: 0x000DC5BF
	internal void startDestroyBuilding()
	{
		if (this.isOnRemove())
		{
			return;
		}
		if (this.asset.has_ruins_graphics && !this.isUnderConstruction())
		{
			this.setState(BuildingState.Ruins);
		}
		this.startRemove();
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000DE3EC File Offset: 0x000DC5EC
	private void clearZones()
	{
		this.zones.Clear();
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x000DE3FC File Offset: 0x000DC5FC
	internal void kill()
	{
		if (!base.isAlive())
		{
			return;
		}
		this.clearZones();
		this.setAlive(false);
		if (this.asset.city_building)
		{
			World.world.map_stats.housesDestroyed += 1L;
		}
		if (this.hasBooks())
		{
			foreach (long tBookID in this.data.books.list_books)
			{
				Book tBook = World.world.books.get(tBookID);
				World.world.books.burnBook(tBook);
			}
		}
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x000DE4B8 File Offset: 0x000DC6B8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override City getCity()
	{
		return this.city;
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06001575 RID: 5493 RVA: 0x000DE4C0 File Offset: 0x000DC6C0
	public City city
	{
		get
		{
			return this.current_tile.zone.city;
		}
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
	internal override void updateStats()
	{
		base.updateStats();
		this.stats.clear();
		this.stats.mergeStats(this.asset.base_stats, 1f);
		if (base.getHealth() > base.getMaxHealth())
		{
			base.setMaxHealth();
		}
		this.batch.c_stats_dirty.Remove(this);
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000DE534 File Offset: 0x000DC734
	internal void chopTree()
	{
		if (this.chopped)
		{
			return;
		}
		if ((this.asset.become_alive_when_chopped || WorldLawLibrary.world_law_bark_bites_back.isEnabled()) && Randy.randomChance(0.2f) && ActionLibrary.tryToMakeFloraAlive(this, true))
		{
			return;
		}
		base.finishAllStatusEffects();
		MusicBox.playSound("event:/SFX/NATURE/TreeFall", this.current_tile, true, true);
		this.chopped = true;
		this.setHaveResourcesToCollect(false);
		float tAngle = (float)(Randy.randomBool() ? 90 : -90);
		this.scale_helper.doRotateTween(tAngle, 1f, new Action(this.finishChop));
		this.batch.c_angle.Add(this);
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x000DE5DC File Offset: 0x000DC7DC
	private void finishChop()
	{
		this.startRemove();
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x000DE5E4 File Offset: 0x000DC7E4
	private void startRemove()
	{
		if (this.isOnRemove())
		{
			return;
		}
		if (!this.isUnderConstruction() && this.asset.has_sound_destroyed)
		{
			MusicBox.playSound(this.asset.sound_destroyed, this.current_tile, true, true);
		}
		this.setAnimationState(BuildingAnimationState.OnRemove);
		this.clearTiles();
		this.clearComponents();
		this.setHaveResourcesToCollect(false);
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000DE641 File Offset: 0x000DC841
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isAnimationState(BuildingAnimationState pState)
	{
		return this.animation_state == pState;
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000DE64C File Offset: 0x000DC84C
	internal void startMakingRuins()
	{
		if (!this.asset.has_ruin_state)
		{
			this.startRemove();
			return;
		}
		if (this.isAnimationState(BuildingAnimationState.OnRuin))
		{
			return;
		}
		if (this.data.state == BuildingState.Ruins)
		{
			return;
		}
		this.setAnimationState(BuildingAnimationState.OnRuin);
		this.makeRuins();
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000DE688 File Offset: 0x000DC888
	internal void removeBuildingFinal()
	{
		this.setState(BuildingState.Removed);
		this.clearZones();
		this.clearTiles();
		this.kill();
		this.current_tile.zone.removeBuildingMain(this);
		World.world.buildings.scheduleDestroyOnPlay(this);
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000DE6C4 File Offset: 0x000DC8C4
	internal void clearTiles()
	{
		this.forceUpdateTilesDirty();
		for (int i = 0; i < this.tiles.Count; i++)
		{
			this.tiles[i].building = null;
		}
		this.tiles.Clear();
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000DE70C File Offset: 0x000DC90C
	private void clearComponents()
	{
		if (this.asset.flora_type == FloraType.Tree)
		{
			this.batch.c_spread_trees.Remove(this);
		}
		if (this.asset.flora_type == FloraType.Fungi)
		{
			this.batch.c_spread_fungi.Remove(this);
		}
		if (this.asset.flora_type == FloraType.Plant)
		{
			this.batch.c_spread_plants.Remove(this);
		}
		if (this.asset.building_type == BuildingType.Building_Poops)
		{
			this.batch.c_poop.Remove(this);
		}
		if (this.components_list == null)
		{
			return;
		}
		this.batch.c_components.Remove(this);
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000DE7AF File Offset: 0x000DC9AF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isOnRemove()
	{
		return this.animation_state == BuildingAnimationState.OnRemove;
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000DE7BA File Offset: 0x000DC9BA
	internal void setAnimationState(BuildingAnimationState pState)
	{
		if (this.isOnRemove())
		{
			return;
		}
		this.animation_state = pState;
		this.checkTweens();
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000DE7D2 File Offset: 0x000DC9D2
	internal void completeMakingRuin()
	{
		this.setState(BuildingState.Ruins);
		this.setAnimationState(BuildingAnimationState.Normal);
		this.setScaleTween(0f, 0.2f, 1f, null, null, 0);
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000DE7FC File Offset: 0x000DC9FC
	private void checkAutoRemove()
	{
		if (this.batch == null)
		{
			return;
		}
		if (this.asset.auto_remove_ruin && this.isRuin() && !this.isCiv())
		{
			this.batch.c_auto_remove.Add(this);
			return;
		}
		this.batch.c_auto_remove.Remove(this);
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000DE852 File Offset: 0x000DCA52
	internal void updateAutoRemove(float pElapsed)
	{
		if (this._auto_remove_timer < 300f)
		{
			this._auto_remove_timer += pElapsed;
			return;
		}
		this._auto_remove_timer = 0f;
		this.batch.c_auto_remove.Remove(this);
		this.startDestroyBuilding();
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000DE892 File Offset: 0x000DCA92
	internal void updateTimerShakeResources(float pElapsed)
	{
		if (this._timer_shake_resource > 0f)
		{
			this._timer_shake_resource -= pElapsed;
			if (this._timer_shake_resource <= 0f)
			{
				this.batch.c_resource_shaker.Remove(this);
			}
		}
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x000DE8D0 File Offset: 0x000DCAD0
	internal void updateComponents(float pElapsed)
	{
		for (int i = 0; i < this.components_list.Count; i++)
		{
			this.components_list[i].update(pElapsed);
		}
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000DE908 File Offset: 0x000DCB08
	public void updatePosition()
	{
		if (!this.positionDirty)
		{
			return;
		}
		this.positionDirty = false;
		this.batch.c_position_dirty.Remove(this);
		this.cur_transform_position = this.current_tile.posV3;
		if (this.cur_transform_position.z < 0f)
		{
			this.cur_transform_position.z = 0f;
		}
		this.cur_transform_position.x = this.cur_transform_position.x + this._shake_offset.x;
		this.cur_transform_position.y = this.cur_transform_position.y + this._shake_offset.y;
		this.cur_transform_position.z = -0.2f + this.asset.bonus_z;
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x000DE9BC File Offset: 0x000DCBBC
	internal void spawnBurstSpecial(int pAmount = 1)
	{
		WorldTile tTile = World.world.GetTile(this.current_tile.pos.x, this.current_tile.pos.y);
		if (tTile == null)
		{
			tTile = this.current_tile;
		}
		for (int i = 0; i < pAmount; i++)
		{
			World.world.drop_manager.spawnParabolicDrop(tTile, this.asset.spawn_drop_id, this.asset.spawn_drop_start_height, this.asset.spawn_drop_min_height, this.asset.spawn_drop_max_height, this.asset.spawn_drop_min_radius, this.asset.spawn_drop_max_radius, -1f);
		}
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000DEA68 File Offset: 0x000DCC68
	internal bool updateBuild(int pProgress = 1)
	{
		this.data.change("construction_progress", pProgress, 0, 1000);
		this.startShake(0.3f, 0.1f, 0.1f);
		bool tFinished = false;
		if (this.getConstructionProgress() > this.asset.construction_progress_needed)
		{
			tFinished = true;
			this.completeConstruction();
			if (this.asset.has_sound_built)
			{
				MusicBox.playSound(this.asset.sound_built, this.current_tile, true, true);
			}
			this.initAnimationData();
			this.setScaleTween(0.25f, 0.2f, 1f, null, null, 0);
		}
		else
		{
			this.setScaleTween(0.75f, 0.2f, 1f, null, null, 0);
		}
		return tFinished;
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x000DEB1C File Offset: 0x000DCD1C
	private void makeZoneDirty()
	{
		this.current_tile.zone.setDirty(true);
		if (this.hasHousingLogic())
		{
			World.world.buildings.event_houses = true;
		}
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x000DEB47 File Offset: 0x000DCD47
	public bool hasResidentSlots()
	{
		return this.asset.hasHousingSlots() && this.asset.housing_slots > this.countResidents();
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000DEB6E File Offset: 0x000DCD6E
	public int countResidents()
	{
		return this.residents.Count;
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000DEB7B File Offset: 0x000DCD7B
	public bool hasResidents()
	{
		return this.countResidents() > 0;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x000DEB86 File Offset: 0x000DCD86
	public void startShake(float pDuration, float pIntensityX = 0.1f, float pIntensityY = 0.1f)
	{
		this._shake_timer = pDuration;
		this._shake_intensity_x = pIntensityX;
		this._shake_intensity_y = pIntensityY;
		BatchBuildings batchBuildings = this.batch;
		if (batchBuildings == null)
		{
			return;
		}
		batchBuildings.c_shake.Add(this);
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000DEBB4 File Offset: 0x000DCDB4
	internal void resourceGathering(float pElapsed)
	{
		if (this._timer_shake_resource > 0f)
		{
			return;
		}
		this.batch.c_resource_shaker.Add(this);
		this.startShake(0.3f, 0.1f, 0.1f);
		this._timer_shake_resource = 1f;
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000DEC00 File Offset: 0x000DCE00
	public void updateShake(float pElapsed)
	{
		if (this._shake_timer > 0f)
		{
			this._shake_timer -= pElapsed;
			if (this._shake_timer < 0f)
			{
				this._shake_offset = Vector2.zero;
				this.batch.c_shake.Remove(this);
			}
			else
			{
				this._shake_offset.x = this.batch.rnd.NextFloat(-this._shake_intensity_x, this._shake_intensity_x);
				this._shake_offset.y = this.batch.rnd.NextFloat(-this._shake_intensity_y, this._shake_intensity_y);
			}
			this.setPositionDirty();
		}
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000DECAC File Offset: 0x000DCEAC
	internal override void getHitFullHealth(AttackType pAttackType)
	{
		this.getHit((float)base.getHealth(), false, pAttackType, null, false, false, false);
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x000DECC4 File Offset: 0x000DCEC4
	internal override void getHit(float pDamage, bool pFlash = true, AttackType pAttackType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true, bool pMetallicWeapon = false, bool pCheckDamageReduction = true)
	{
		if (!this.isAnimationState(BuildingAnimationState.Normal))
		{
			return;
		}
		base.changeHealth((int)(-(int)pDamage));
		if (pAttackType == AttackType.Weapon && this.asset.has_sound_hit)
		{
			MusicBox.playSound(this.asset.sound_hit, this.current_tile, true, true);
		}
		this.startShake(0.3f, 0.1f, 0.1f);
		if (base.hasHealth())
		{
			this.setScaleTween(0.75f, 0.2f, 1f, null, null, 0);
			return;
		}
		if (this.data.state == BuildingState.Ruins)
		{
			this.startDestroyBuilding();
			return;
		}
		this.startMakingRuins();
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000DED60 File Offset: 0x000DCF60
	internal void extractResources(Actor pBy)
	{
		this.setScaleTween(0.75f, 0.2f, 1f, null, null, 0);
		switch (this.asset.building_type)
		{
		case BuildingType.Building_Tree:
			this.chopTree();
			return;
		case BuildingType.Building_Fruits:
			this.component_fruit_growth.reset();
			this.setHaveResourcesToCollect(false);
			if (Randy.randomChance(0.2f))
			{
				this.startDestroyBuilding();
				return;
			}
			break;
		case BuildingType.Building_Hives:
			this.component_beehive.honey = 0;
			this.setHaveResourcesToCollect(false);
			break;
		case BuildingType.Building_Poops:
		case BuildingType.Building_Mineral:
			this.startRemove();
			return;
		case BuildingType.Building_Wheat:
		case BuildingType.Building_Plant:
			this.startDestroyBuilding();
			return;
		default:
			return;
		}
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000DEE03 File Offset: 0x000DD003
	internal Color32 getColorForMinimap(WorldTile pTile)
	{
		if (Config.EVERYTHING_MAGIC_COLOR)
		{
			return Toolbox.EVERYTHING_MAGIC_COLOR32;
		}
		return this.asset.building_sprites.map_icon.getColor(pTile.minimap_building_x, pTile.minimap_building_y, this);
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000DEE34 File Offset: 0x000DD034
	public WorldTile getConstructionTile()
	{
		if (this.asset.docks)
		{
			ValueTuple<TileZone[], int> allZonesFromTile = Toolbox.getAllZonesFromTile(this.current_tile);
			TileZone[] tZones = allZonesFromTile.Item1;
			int tLength = allZonesFromTile.Item2;
			foreach (TileZone tTileZone in tZones.LoopRandom(tLength))
			{
				using (IEnumerator<WorldTile> enumerator2 = this.checkZoneForDockConstruction(tTileZone).GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						return enumerator2.Current;
					}
				}
			}
		}
		return Randy.getRandom<WorldTile>(this.tiles);
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000DEEEC File Offset: 0x000DD0EC
	public int getConstructionProgress()
	{
		int tResult;
		this.data.get("construction_progress", out tResult, 0);
		return tResult;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000DEF0D File Offset: 0x000DD10D
	public void completeConstruction()
	{
		this.data.removeInt("construction_progress");
		this.data.removeFlag("under_construction");
		this.makeZoneDirty();
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000DEF35 File Offset: 0x000DD135
	public bool isUnderConstruction()
	{
		return this.asset.has_sprite_construction && this.data.hasFlag("under_construction");
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000DEF56 File Offset: 0x000DD156
	public void setUnderConstruction()
	{
		if (!this.asset.has_sprite_construction)
		{
			return;
		}
		this.data.addFlag("under_construction");
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000DEF77 File Offset: 0x000DD177
	public bool canRemoveForFarms()
	{
		return this.asset.flora;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000DEF84 File Offset: 0x000DD184
	internal IEnumerable<WorldTile> checkZoneForDockConstruction(TileZone pZone)
	{
		if (pZone.city == null)
		{
			yield break;
		}
		if (pZone.city != this.city)
		{
			yield break;
		}
		foreach (WorldTile iTile in pZone.tiles.LoopRandom<WorldTile>())
		{
			if (iTile.Type.ground && Toolbox.SquaredDistTile(this.current_tile, iTile) <= 49)
			{
				yield return iTile;
			}
		}
		IEnumerator<WorldTile> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000DEF9B File Offset: 0x000DD19B
	internal void checkStartSpawnAnimation()
	{
		Sprite[] spawn = this.animData.spawn;
		if (spawn != null && spawn.Length != 0)
		{
			this.check_spawn_animation = true;
		}
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000DEFBC File Offset: 0x000DD1BC
	public Sprite calculateMainSprite()
	{
		bool tAnimationAllowed = true;
		bool tIsRuin = this.isRuin();
		if (tIsRuin)
		{
			tAnimationAllowed = false;
		}
		if (this.isUnderConstruction())
		{
			this.last_main_sprite = this.asset.building_sprites.construction;
			return this.last_main_sprite;
		}
		Sprite[] tSprites;
		if (this.asset.has_special_animation_state)
		{
			if (this.hasResourcesToCollect())
			{
				tSprites = this.animData.main;
			}
			else
			{
				tSprites = this.animData.special;
			}
		}
		else if (tIsRuin && this.asset.has_ruins_graphics)
		{
			tAnimationAllowed = false;
			tSprites = this.animData.ruins;
		}
		else if (this.asset.spawn_drops && this.data.hasFlag("stop_spawn_drops"))
		{
			tSprites = this.animData.main_disabled;
		}
		else if (this.asset.can_be_abandoned && this.isAbandoned())
		{
			Sprite[] main_disabled = this.animData.main_disabled;
			if (main_disabled != null && main_disabled.Length != 0)
			{
				tSprites = this.animData.main_disabled;
			}
			else
			{
				tSprites = this.animData.main;
			}
			tAnimationAllowed = false;
		}
		else
		{
			tSprites = this.animData.main;
			if (this.asset.get_override_sprites_main != null)
			{
				Sprite[] tOverride = this.asset.get_override_sprites_main(this);
				if (tOverride != null)
				{
					tSprites = tOverride;
				}
			}
		}
		Sprite tMainSprite;
		if (this.check_spawn_animation)
		{
			tMainSprite = this.getSpawnFrameSprite();
		}
		else if (!tAnimationAllowed || tSprites.Length == 1)
		{
			tMainSprite = tSprites[0];
		}
		else
		{
			tMainSprite = AnimationHelper.getSpriteFromList(this.GetHashCode(), tSprites, this.asset.animation_speed);
		}
		return tMainSprite;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000DF13E File Offset: 0x000DD33E
	public bool isColoredSpriteNeedsCheck(Sprite pMainSprite)
	{
		return this.last_main_sprite == null || this.last_main_sprite.GetHashCode() != pMainSprite.GetHashCode() || this._last_color_asset != this.kingdom.getColor();
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000DF174 File Offset: 0x000DD374
	public Sprite calculateColoredSprite(Sprite pMainSprite)
	{
		if (this.isColoredSpriteNeedsCheck(pMainSprite))
		{
			this._last_colored_sprite = DynamicSprites.getRecoloredBuilding(pMainSprite, this.kingdom.getColor(), this.asset.atlas_asset);
			this.last_main_sprite = pMainSprite;
			this._last_color_asset = this.kingdom.getColor();
		}
		return this._last_colored_sprite;
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000DF1CA File Offset: 0x000DD3CA
	public Sprite getLastColoredSprite()
	{
		return this._last_colored_sprite;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000DF1D2 File Offset: 0x000DD3D2
	public void clearSprites()
	{
		this.last_main_sprite = null;
		this._last_colored_sprite = null;
		this._last_color_asset = null;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000DF1EC File Offset: 0x000DD3EC
	public Sprite checkSpriteToRender()
	{
		Sprite tMainSprite = this.calculateMainSprite();
		return this.calculateColoredSprite(tMainSprite);
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x000DF208 File Offset: 0x000DD408
	public Vector3 getCurrentScale()
	{
		float tTweenBuildingsValue = World.world.quality_changer.getTweenBuildingsValue();
		float tScaleY = this.current_scale.y * tTweenBuildingsValue;
		float tScaleX = this.current_scale.x * tTweenBuildingsValue;
		if (this._last_scale.y != tScaleY || this._last_scale.x != tScaleX)
		{
			this._last_scale.Set(tScaleX, tScaleY, 1f);
		}
		return this._last_scale;
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x000DF275 File Offset: 0x000DD475
	public bool isFullyGrown()
	{
		return !this.asset.can_be_grown || (this.asset.wheat && this.component_wheat.isMaxLevel());
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x000DF2A0 File Offset: 0x000DD4A0
	private Sprite getSpawnFrameSprite()
	{
		Sprite[] tSpawnSprites = this.animData.spawn;
		float tSpawnTime = World.world.getWorldTimeElapsedSince(this.data.created_time);
		float tTotalAnimationTime = (float)tSpawnSprites.Length * this.asset.animation_speed / 60f;
		Sprite tResult;
		if (tTotalAnimationTime > tSpawnTime)
		{
			int tSpawnIndex = (int)(tSpawnTime / tTotalAnimationTime * (float)tSpawnSprites.Length);
			tResult = tSpawnSprites[tSpawnIndex];
		}
		else
		{
			tResult = tSpawnSprites.Last<Sprite>();
			this.check_spawn_animation = false;
		}
		return tResult;
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000DF30B File Offset: 0x000DD50B
	public CityResources resources
	{
		get
		{
			return this.data.resources;
		}
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x000DF318 File Offset: 0x000DD518
	public int takeResource(string pResourceID, int pAmount)
	{
		return this.resources.change(pResourceID, -pAmount);
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000DF328 File Offset: 0x000DD528
	public int getResourcesAmount(string pResourceID)
	{
		return this.resources.get(pResourceID);
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000DF336 File Offset: 0x000DD536
	public int addResources(string pResourceID, int pAmount)
	{
		return this.resources.change(pResourceID, pAmount);
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000DF345 File Offset: 0x000DD545
	public bool hasSpaceForResource(ResourceAsset pResourceAsset)
	{
		return this.resources.hasSpaceForResource(pResourceAsset);
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000DF353 File Offset: 0x000DD553
	public bool hasResourcesForNewItems()
	{
		return this.resources.hasResourcesForNewItems();
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000DF360 File Offset: 0x000DD560
	public int countFood()
	{
		return this.resources.countFood();
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x000DF36D File Offset: 0x000DD56D
	public ResourceAsset getRandomSuitableFood(Subspecies pSubspecies, string pFavoriteFood = null)
	{
		return this.resources.getRandomSuitableFood(pSubspecies, pFavoriteFood);
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x000DF37C File Offset: 0x000DD57C
	public override void Dispose()
	{
		this.kingdom = null;
		this._last_colored_sprite = null;
		this._last_color_asset = null;
		this.last_main_sprite = null;
		this.batch = null;
		this.data = null;
		this.asset = null;
		this.tiles.Clear();
		this.animData = null;
		this.zones.Clear();
		if (this.components_list != null)
		{
			for (int i = 0; i < this.components_list.Count; i++)
			{
				BaseBuildingComponent tComponent = this.components_list[i];
				tComponent.Dispose();
				World.world.buildings.component_pool.release(tComponent);
			}
			this.components_list.Clear();
			this.components_list.Dispose();
			this.components_list = null;
		}
		this.component_docks = null;
		this.component_wheat = null;
		this.component_fruit_growth = null;
		this.component_unit_spawner = null;
		this.component_biome_spreader = null;
		this.component_monolith = null;
		this.component_waypoint = null;
		this.component_food_producer = null;
		this.component_beehive = null;
		this.scale_helper.reset();
		base.Dispose();
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x000DF48A File Offset: 0x000DD68A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Building pObject)
	{
		return this.GetHashCode() == pObject.GetHashCode();
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x000DF49C File Offset: 0x000DD69C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Building pTarget)
	{
		return this.GetHashCode().CompareTo(pTarget.GetHashCode());
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000DF4C0 File Offset: 0x000DD6C0
	public void checkVegetationSpread(float pElapsed)
	{
		BuildingAsset tBuildingAsset = this.asset;
		if (!Randy.randomChance(tBuildingAsset.spread_chance))
		{
			return;
		}
		WorldTile tRandomTile = this.current_tile.neighboursAll.GetRandom<WorldTile>();
		int i = 0;
		while ((float)i < tBuildingAsset.spread_steps)
		{
			tRandomTile = tRandomTile.neighboursAll.GetRandom<WorldTile>();
			i++;
		}
		string tRandomSpreadId = tBuildingAsset.spread_ids.GetRandom<string>();
		BuildingAsset tRandomSpreadAsset = AssetManager.buildings.get(tRandomSpreadId);
		this.tryToGrowOnTile(tRandomTile, tRandomSpreadAsset, true);
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000DF538 File Offset: 0x000DD738
	private bool tryToGrowOnTile(WorldTile pTile, BuildingAsset pAsset, bool pCheckLimit = true)
	{
		if (pCheckLimit && pTile.zone.hasReachedBuildingLimit(pTile, pAsset))
		{
			return false;
		}
		if (!World.world.buildings.canBuildFrom(pTile, pAsset, null, BuildPlacingType.New, true))
		{
			return false;
		}
		World.world.buildings.addBuilding(pAsset, pTile, false, false, BuildPlacingType.New);
		if (pAsset.flora_type == FloraType.Tree)
		{
			World.world.game_stats.data.treesGrown += 1L;
		}
		else if (pAsset.flora_type == FloraType.Plant || pAsset.flora_type == FloraType.Fungi)
		{
			World.world.game_stats.data.floraGrown += 1L;
		}
		return true;
	}

	// Token: 0x040011FA RID: 4602
	public BatchBuildings batch;

	// Token: 0x040011FB RID: 4603
	internal bool positionDirty;

	// Token: 0x040011FC RID: 4604
	internal bool sprite_dirty;

	// Token: 0x040011FD RID: 4605
	internal bool tiles_dirty;

	// Token: 0x040011FE RID: 4606
	private Sprite _last_colored_sprite;

	// Token: 0x040011FF RID: 4607
	private ColorAsset _last_color_asset;

	// Token: 0x04001200 RID: 4608
	internal Sprite last_main_sprite;

	// Token: 0x04001201 RID: 4609
	internal BuildingData data;

	// Token: 0x04001202 RID: 4610
	internal BuildingAsset asset;

	// Token: 0x04001203 RID: 4611
	public bool flip_x;

	// Token: 0x04001204 RID: 4612
	internal readonly List<WorldTile> tiles = new List<WorldTile>();

	// Token: 0x04001205 RID: 4613
	public BuildingAnimationData animData;

	// Token: 0x04001206 RID: 4614
	public int animData_index;

	// Token: 0x04001207 RID: 4615
	private float _shake_timer;

	// Token: 0x04001208 RID: 4616
	private float _shake_intensity_x;

	// Token: 0x04001209 RID: 4617
	private float _shake_intensity_y;

	// Token: 0x0400120A RID: 4618
	internal float lastAngle;

	// Token: 0x0400120B RID: 4619
	private Vector2 _shake_offset;

	// Token: 0x0400120C RID: 4620
	internal readonly List<TileZone> zones = new List<TileZone>();

	// Token: 0x0400120D RID: 4621
	internal BuildingAnimationState animation_state;

	// Token: 0x0400120E RID: 4622
	internal BuildingOwnershipState state_ownership;

	// Token: 0x0400120F RID: 4623
	internal ListPool<BaseBuildingComponent> components_list;

	// Token: 0x04001210 RID: 4624
	internal Docks component_docks;

	// Token: 0x04001211 RID: 4625
	internal Wheat component_wheat;

	// Token: 0x04001212 RID: 4626
	internal BuildingFruitGrowth component_fruit_growth;

	// Token: 0x04001213 RID: 4627
	internal UnitSpawner component_unit_spawner;

	// Token: 0x04001214 RID: 4628
	internal BuildingSpreadBiome component_biome_spreader;

	// Token: 0x04001215 RID: 4629
	internal BuildingMonolith component_monolith;

	// Token: 0x04001216 RID: 4630
	internal BuildingWaypoint component_waypoint;

	// Token: 0x04001217 RID: 4631
	internal BuildingBiomeFoodProducer component_food_producer;

	// Token: 0x04001218 RID: 4632
	internal Beehive component_beehive;

	// Token: 0x04001219 RID: 4633
	internal readonly BuildingTweenScaleHelper scale_helper = new BuildingTweenScaleHelper();

	// Token: 0x0400121A RID: 4634
	internal bool chopped;

	// Token: 0x0400121B RID: 4635
	internal bool is_visible;

	// Token: 0x0400121C RID: 4636
	internal bool check_spawn_animation;

	// Token: 0x0400121D RID: 4637
	private float _timer_shake_resource;

	// Token: 0x0400121E RID: 4638
	private float _auto_remove_timer;

	// Token: 0x0400121F RID: 4639
	public HashSet<long> residents = new HashSet<long>();

	// Token: 0x04001220 RID: 4640
	private Vector3 _last_scale = Vector3.zero;

	// Token: 0x04001221 RID: 4641
	public Material material;
}
