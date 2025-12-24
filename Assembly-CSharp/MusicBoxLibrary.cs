using System;
using System.Collections.Generic;

// Token: 0x02000437 RID: 1079
public class MusicBoxLibrary : AssetLibrary<MusicAsset>
{
	// Token: 0x06002592 RID: 9618 RVA: 0x00135C00 File Offset: 0x00133E00
	public override void init()
	{
		base.init();
		MusicBoxLibrary.Menu = this.add(new MusicAsset
		{
			id = "Menu",
			disable_param_after_start = true
		});
		MusicBoxLibrary.Neutral_001 = this.add(new MusicAsset
		{
			id = "Neutral_001",
			disable_param_after_start = true
		});
		MusicBoxLibrary.New_World = this.add(new MusicAsset
		{
			id = "New_World",
			disable_param_after_start = true
		});
		this.add(new MusicAsset
		{
			id = "wea_rain"
		});
		this.add(new MusicAsset
		{
			id = "wea_snow"
		});
		this.addUnits();
		this.addUnique();
		this.addLoc();
		this.addEnv();
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x00135CC0 File Offset: 0x00133EC0
	private void addEnv()
	{
		this.add(new MusicAsset
		{
			id = "LavaEnvironment",
			fmod_path = "event:/SFX/ENVIRONMENT/LavaEnvironment",
			is_environment = true,
			is_param = true,
			min_tiles_to_play = 30
		});
		this.t.setTileTypes(new string[]
		{
			"lava0",
			"lava1",
			"lava2",
			"lava3"
		});
		this.add(new MusicAsset
		{
			id = "MapEnvironment",
			fmod_path = "event:/SFX/ENVIRONMENT/MapEnvironment",
			is_environment = true,
			is_param = true,
			mini_map_only = true
		});
		this.add(new MusicAsset
		{
			id = "Ocean",
			fmod_path = "event:/SFX/ENVIRONMENT/Ocean",
			is_environment = true,
			is_param = true,
			min_tiles_to_play = 100
		});
		this.t.setTileTypes(new string[]
		{
			"deep_ocean"
		});
		this.add(new MusicAsset
		{
			id = "Sea",
			fmod_path = "event:/SFX/ENVIRONMENT/Sea",
			is_param = true,
			is_environment = true,
			min_tiles_to_play = 100
		});
		this.t.setTileTypes(new string[]
		{
			"close_ocean"
		});
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x00135E10 File Offset: 0x00134010
	private void addLoc()
	{
		this.add(new MusicAsset
		{
			id = "Locations_Forest",
			is_param = true
		});
		this.t.setTileTypes(new string[]
		{
			"grass_high",
			"grass_low",
			"enchanted_low",
			"enchanted_high"
		});
		MusicBoxLibrary.Locations_Desert = this.add(new MusicAsset
		{
			id = "Locations_Desert",
			is_param = true
		});
		this.add(new MusicAsset
		{
			id = "Locations_Mountains",
			is_param = true
		});
		this.t.setTileTypes(new string[]
		{
			"hills",
			"mountains"
		});
		this.add(new MusicAsset
		{
			id = "Locations_Ocean",
			is_param = true
		});
		this.t.setTileTypes(new string[]
		{
			"deep_ocean",
			"close_ocean"
		});
		this.add(new MusicAsset
		{
			id = "Locations_Snow",
			is_param = true
		});
		this.t.setTileTypes(new string[]
		{
			"snow_block",
			"permafrost_high",
			"snow_hills",
			"permafrost_low",
			"snow_sand"
		});
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x00135F64 File Offset: 0x00134164
	private void addUnique()
	{
		MusicAsset musicAsset = new MusicAsset();
		musicAsset.id = "Crabzilla";
		musicAsset.is_unit_param = true;
		musicAsset.special_delegate_units = delegate(MusicBoxContainerUnits pContainer)
		{
			if (ControllableUnit.isControllingCrabzilla())
			{
				pContainer.units = 1;
			}
		};
		musicAsset.priority = MusicLayerPriority.High;
		this.add(musicAsset);
		MusicAsset musicAsset2 = new MusicAsset();
		musicAsset2.id = "GreyGoo";
		musicAsset2.is_unit_param = true;
		musicAsset2.special_delegate_units = delegate(MusicBoxContainerUnits pContainer)
		{
			if (!World.world.grey_goo_layer.isActive())
			{
				return;
			}
			if (!WorldLawLibrary.world_law_gaias_covenant.isEnabled() || MapBox.isRenderMiniMap())
			{
				pContainer.units = 1;
				return;
			}
			List<TileZone> tZones = World.world.zone_camera.getVisibleZones();
			for (int i = 0; i < tZones.Count; i++)
			{
				if (tZones[i].hasTilesOfType(TileLibrary.grey_goo))
				{
					pContainer.units = 1;
					return;
				}
			}
		};
		musicAsset2.priority = MusicLayerPriority.Medium;
		this.add(musicAsset2);
		MusicAsset musicAsset3 = new MusicAsset();
		musicAsset3.id = "Unique_ZombieInfection";
		musicAsset3.is_unit_param = true;
		musicAsset3.special_delegate_units = delegate(MusicBoxContainerUnits pContainer)
		{
		};
		musicAsset3.priority = MusicLayerPriority.Medium;
		this.add(musicAsset3);
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x00136050 File Offset: 0x00134250
	private void addUnits()
	{
		this.add(new MusicAsset
		{
			id = "_units_param",
			is_unit_param = true
		});
		this.clone("Units_Bandits", "_units_param");
		this.clone("Units_Bear", "_units_param");
		this.clone("Units_BeeHive", "_units_param");
		this.clone("Units_Cat", "_units_param");
		this.clone("Units_Chicken", "_units_param");
		this.clone("Units_ColdOne", "_units_param");
		this.clone("Units_Demon", "_units_param");
		this.clone("Units_Fairy", "_units_param");
		this.clone("Units_LivingPlants", "_units_param");
		this.clone("Units_Piranha", "_units_param");
		this.clone("Units_Rabbit", "_units_param");
		this.clone("Units_Rat", "_units_param");
		this.clone("Units_Skeleton", "_units_param");
		this.clone("Units_Sheep", "_units_param");
		this.clone("Units_Snowman", "_units_param");
		this.clone("Units_Wolf", "_units_param");
		this.clone("Units_Worm", "_units_param");
		this.clone("Units_Zombie", "_units_param");
		this.clone("Buildings_Tumor", "_units_param");
		this.clone("Humans_Neutral", "_units_param");
		this.clone("Elves_Neutral", "_units_param");
		this.clone("Orcs_Neutral", "_units_param");
		this.clone("Dwarves_Neutral", "_units_param");
		this.add(new MusicAsset
		{
			id = "Units_GodFinger",
			is_unit_param = true,
			special_delegate_units = new MusicAssetDelegate(this.special_god_finger)
		});
		this.add(new MusicAsset
		{
			id = "Units_Dragon",
			is_unit_param = true,
			special_delegate_units = new MusicAssetDelegate(this.special_dragon)
		});
		this.add(new MusicAsset
		{
			id = "Units_Santa",
			is_unit_param = true
		});
		this.add(new MusicAsset
		{
			id = "Units_UFO",
			is_unit_param = true,
			special_delegate_units = new MusicAssetDelegate(this.special_ufo)
		});
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x001362B0 File Offset: 0x001344B0
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (MusicAsset tAsset in this.list)
		{
			if (tAsset.civilization)
			{
				this.addCivContainer(tAsset);
			}
			if (tAsset.is_param)
			{
				this.addTileContainer(tAsset);
			}
			if (tAsset.is_unit_param)
			{
				this.addUnitContainer(tAsset);
			}
			string[] tile_type_strings = tAsset.tile_type_strings;
			if (tile_type_strings != null && tile_type_strings.Length != 0)
			{
				using (ListPool<TileTypeBase> tTileTypes = new ListPool<TileTypeBase>(tAsset.tile_type_strings.Length))
				{
					foreach (string pTileType in tAsset.tile_type_strings)
					{
						if (AssetManager.top_tiles.has(pTileType))
						{
							tTileTypes.Add(AssetManager.top_tiles.get(pTileType));
						}
						else if (AssetManager.tiles.has(pTileType))
						{
							tTileTypes.Add(AssetManager.tiles.get(pTileType));
						}
						else
						{
							BaseAssetLibrary.logAssetError("MusicBoxLibrary: No matching Tile Type found for", pTileType);
						}
					}
					tAsset.tile_types = tTileTypes.ToArray<TileTypeBase>();
				}
			}
		}
		foreach (MusicAsset tMusicAsset in this.list)
		{
			if (tMusicAsset.tile_types != null)
			{
				TileTypeBase[] tile_types = tMusicAsset.tile_types;
				for (int i = 0; i < tile_types.Length; i++)
				{
					TileTypeBase tileTypeBase2;
					TileTypeBase tileTypeBase = tileTypeBase2 = tile_types[i];
					if (tileTypeBase2.music_assets == null)
					{
						tileTypeBase2.music_assets = new List<MusicAsset>();
					}
					tileTypeBase.music_assets.Add(tMusicAsset);
				}
			}
		}
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x0013647C File Offset: 0x0013467C
	private MusicBoxContainerUnits addUnitContainer(MusicAsset pAsset)
	{
		MusicBoxContainerUnits tNewCont = new MusicBoxContainerUnits();
		this.c_dict_units.Add(pAsset.id, tNewCont);
		tNewCont.asset = pAsset;
		return tNewCont;
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x001364AC File Offset: 0x001346AC
	private MusicBoxContainerCivs addCivContainer(MusicAsset pAsset)
	{
		MusicBoxContainerCivs tNewCont = new MusicBoxContainerCivs();
		this.c_dict_civs.Add(pAsset.id, tNewCont);
		tNewCont.asset = pAsset;
		return tNewCont;
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x001364DC File Offset: 0x001346DC
	private MusicBoxContainerTiles addTileContainer(MusicAsset pAsset)
	{
		MusicBoxContainerTiles tNewContainer = new MusicBoxContainerTiles();
		tNewContainer.asset = pAsset;
		pAsset.container_tiles = tNewContainer;
		this.c_list_params.Add(tNewContainer);
		if (pAsset.is_environment)
		{
			this.c_list_environments.Add(tNewContainer);
		}
		return tNewContainer;
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x00136520 File Offset: 0x00134720
	private void special_god_finger(MusicBoxContainerUnits pContainer)
	{
		Kingdom tKingdom = World.world.kingdoms_wild.get("godfinger");
		pContainer.units += tKingdom.units.Count;
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x0013655C File Offset: 0x0013475C
	private void special_dragon(MusicBoxContainerUnits pContainer)
	{
		Kingdom tKingdom = World.world.kingdoms_wild.get("dragons");
		pContainer.units += tKingdom.units.Count;
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x00136598 File Offset: 0x00134798
	private void special_ufo(MusicBoxContainerUnits pContainer)
	{
		ActorAsset tAsset = AssetManager.actor_library.get("UFO");
		pContainer.units += tAsset.units.Count;
	}

	// Token: 0x04001C88 RID: 7304
	internal readonly List<MusicBoxContainerTiles> c_list_params = new List<MusicBoxContainerTiles>();

	// Token: 0x04001C89 RID: 7305
	internal readonly Dictionary<string, MusicBoxContainerCivs> c_dict_civs = new Dictionary<string, MusicBoxContainerCivs>();

	// Token: 0x04001C8A RID: 7306
	internal readonly List<MusicBoxContainerTiles> c_list_environments = new List<MusicBoxContainerTiles>();

	// Token: 0x04001C8B RID: 7307
	internal readonly Dictionary<string, MusicBoxContainerUnits> c_dict_units = new Dictionary<string, MusicBoxContainerUnits>();

	// Token: 0x04001C8C RID: 7308
	public static MusicAsset New_World;

	// Token: 0x04001C8D RID: 7309
	public static MusicAsset Menu;

	// Token: 0x04001C8E RID: 7310
	public static MusicAsset Neutral_001;

	// Token: 0x04001C8F RID: 7311
	public static MusicAsset Locations_Desert;
}
