using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Ionic.Zlib;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Scripting;

// Token: 0x02000598 RID: 1432
[Serializable]
public class SavedMap
{
	// Token: 0x06002FDB RID: 12251 RVA: 0x00172DC0 File Offset: 0x00170FC0
	public SavedMap()
	{
		this.width = Config.ZONE_AMOUNT_X_DEFAULT;
		this.height = Config.ZONE_AMOUNT_Y_DEFAULT;
	}

	// Token: 0x06002FDC RID: 12252 RVA: 0x00172EE8 File Offset: 0x001710E8
	public void check()
	{
		if (this.worldLaws == null)
		{
			this.worldLaws = new WorldLaws();
		}
		if (this.mapStats == null)
		{
			this.mapStats = new MapStats();
		}
		if (this.hotkey_tabs_data == null)
		{
			this.hotkey_tabs_data = new HotkeyTabsData();
		}
		if (this.tileMap == null)
		{
			this.tileMap = new List<string>();
		}
		if (this.fire == null)
		{
			this.fire = new List<int>();
		}
		if (this.conwayEater == null)
		{
			this.conwayEater = new List<int>();
		}
		if (this.conwayCreator == null)
		{
			this.conwayCreator = new List<int>();
		}
		if (this.frozen_tiles == null)
		{
			this.frozen_tiles = new List<int>();
		}
		if (this.tiles == null)
		{
			this.tiles = new List<WorldTileData>();
		}
		if (this.cities == null)
		{
			this.cities = new List<CityData>();
		}
		if (this.actors_data == null)
		{
			this.actors_data = new List<ActorData>();
		}
		if (this.buildings == null)
		{
			this.buildings = new List<BuildingData>();
		}
		if (this.kingdoms == null)
		{
			this.kingdoms = new List<KingdomData>();
		}
		if (this.clans == null)
		{
			this.clans = new List<ClanData>();
		}
		if (this.alliances == null)
		{
			this.alliances = new List<AllianceData>();
		}
		if (this.wars == null)
		{
			this.wars = new List<WarData>();
		}
		if (this.plots == null)
		{
			this.plots = new List<PlotData>();
		}
		if (this.relations == null)
		{
			this.relations = new List<DiplomacyRelationData>();
		}
		if (this.cultures == null)
		{
			this.cultures = new List<CultureData>();
		}
		if (this.books == null)
		{
			this.books = new List<BookData>();
		}
		if (this.subspecies == null)
		{
			this.subspecies = new List<SubspeciesData>();
		}
		if (this.languages == null)
		{
			this.languages = new List<LanguageData>();
		}
		if (this.religions == null)
		{
			this.religions = new List<ReligionData>();
		}
		if (this.families == null)
		{
			this.families = new List<FamilyData>();
		}
		if (this.armies == null)
		{
			this.armies = new List<ArmyData>();
		}
		if (this.items == null)
		{
			this.items = new List<ItemData>();
		}
		this.worldLaws.check();
	}

	// Token: 0x06002FDD RID: 12253 RVA: 0x001730EE File Offset: 0x001712EE
	public void init()
	{
		this.worldLaws = new WorldLaws();
		this.worldLaws.init(false);
	}

	// Token: 0x06002FDE RID: 12254 RVA: 0x00173107 File Offset: 0x00171307
	public int getTileMapID(string pTileString)
	{
		if (!this.tileMap.Contains(pTileString))
		{
			this.tileMap.Add(pTileString);
		}
		return this.tileMap.IndexOf(pTileString);
	}

	// Token: 0x06002FDF RID: 12255 RVA: 0x00173130 File Offset: 0x00171330
	public void create()
	{
		this.init();
		this.width = Config.ZONE_AMOUNT_X;
		this.height = Config.ZONE_AMOUNT_Y;
		this.camera_pos_x = World.world.camera.transform.position.x;
		this.camera_pos_y = World.world.camera.transform.position.y;
		this.camera_zoom = MoveCamera.instance.getTargetZoom();
		this.saveVersion = Config.WORLD_SAVE_VERSION;
		this.hotkey_tabs_data = World.world.hotkey_tabs_data;
		this.mapStats = World.world.map_stats;
		this.worldLaws = World.world.world_laws;
		this.mapStats.population = (long)World.world.units.Count;
		this.items = World.world.items.save(null);
		this.books = World.world.books.save(null);
		this.subspecies = World.world.subspecies.save(null);
		this.families = World.world.families.save(null);
		this.armies = World.world.armies.save(null);
		this.languages = World.world.languages.save(null);
		this.religions = World.world.religions.save(null);
		this.cultures = World.world.cultures.save(null);
		this.kingdoms = World.world.kingdoms.save(null);
		this.clans = World.world.clans.save(null);
		this.alliances = World.world.alliances.save(null);
		this.wars = World.world.wars.save(null);
		this.plots = World.world.plots.save(null);
		this.relations = World.world.diplomacy.save(null);
		this.cities = World.world.cities.save(null);
		if (this.tileMap == null)
		{
			this.check();
		}
		this.tileMap.Clear();
		this.fire.Clear();
		this.conwayEater.Clear();
		this.conwayCreator.Clear();
		this.frozen_tiles.Clear();
		using (global::ListPool<int[]> tTileArray = new global::ListPool<int[]>())
		{
			using (global::ListPool<int[]> tTileAmounts = new global::ListPool<int[]>())
			{
				string lastTileId = string.Empty;
				int tileAmount = 0;
				int tileY = 0;
				int xWidth = this.width * 64;
				tTileArray.Add(new int[xWidth]);
				tTileAmounts.Add(new int[xWidth]);
				int aPos = 0;
				for (int i = 0; i < World.world.tiles_list.Length; i++)
				{
					WorldTile tTile = World.world.tiles_list[i];
					string tCurId = this.getWholeTileIDForSave(tTile);
					if (tCurId != lastTileId || tileY != tTile.pos.y)
					{
						if (tileAmount > 0)
						{
							tTileAmounts[tileY][aPos] = tileAmount;
							tTileArray[tileY][aPos++] = this.getTileMapID(lastTileId);
							tileAmount = 0;
						}
						lastTileId = tCurId;
						if (tileY != tTile.pos.y)
						{
							tTileArray[tileY] = Toolbox.resizeArray<int>(tTileArray[tileY], aPos);
							tTileAmounts[tileY] = Toolbox.resizeArray<int>(tTileAmounts[tileY], aPos);
							tileY = tTile.pos.y;
							tTileArray.Add(new int[xWidth]);
							tTileAmounts.Add(new int[xWidth]);
							aPos = 0;
						}
					}
					tileAmount++;
					if (tTile.isOnFire())
					{
						this.fire.Add(tTile.data.tile_id);
					}
					if (tTile.data.conwayType == ConwayType.Eater)
					{
						this.conwayEater.Add(tTile.data.tile_id);
					}
					if (tTile.data.conwayType == ConwayType.Creator)
					{
						this.conwayCreator.Add(tTile.data.tile_id);
					}
					if (tTile.data.frozen)
					{
						this.frozen_tiles.Add(tTile.data.tile_id);
					}
				}
				if (tileAmount > 0)
				{
					tTileAmounts[tileY][aPos] = tileAmount;
					tTileArray[tileY][aPos++] = this.getTileMapID(lastTileId);
					tTileArray[tileY] = Toolbox.resizeArray<int>(tTileArray[tileY], aPos);
					tTileAmounts[tileY] = Toolbox.resizeArray<int>(tTileAmounts[tileY], aPos);
				}
				this.tileArray = tTileArray.ToArray<int[]>();
				this.tileAmounts = tTileAmounts.ToArray<int[]>();
				foreach (Actor tActor in World.world.units)
				{
					if (tActor.isAlive() && !tActor.asset.skip_save)
					{
						tActor.prepareForSave();
						ActorData tData = tActor.data;
						this.actors_data.Add(tData);
					}
				}
				foreach (Building tBuilding in World.world.buildings)
				{
					if (tBuilding.data.state != BuildingState.Removed)
					{
						tBuilding.prepareForSave();
						this.buildings.Add(tBuilding.data);
					}
				}
			}
		}
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x00173700 File Offset: 0x00171900
	private string getWholeTileIDForSave(WorldTile pTile)
	{
		if (pTile.top_type == null)
		{
			return pTile.main_type.id;
		}
		return pTile.main_type.id + ":" + pTile.top_type.id;
	}

	// Token: 0x06002FE1 RID: 12257 RVA: 0x00173738 File Offset: 0x00171938
	public void toJson(string pFilePath)
	{
		if (this.worldLaws == null)
		{
			this.create();
		}
		try
		{
			using (FileStream fs = new FileStream(pFilePath, FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs)
				{
					NewLine = "\n"
				})
				{
					using (JsonWriter writer = new JsonTextWriter(sw))
					{
						JsonHelper.writer.Serialize(writer, this);
					}
				}
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			throw;
		}
		Config.scheduleGC("toJson", false);
	}

	// Token: 0x06002FE2 RID: 12258 RVA: 0x001737F0 File Offset: 0x001719F0
	public string toJson(bool pBeautify = false)
	{
		if (this.worldLaws == null)
		{
			this.create();
		}
		string jsonString = "";
		try
		{
			JsonSerializerSettings tSettings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
				Formatting = Formatting.None
			};
			if (pBeautify)
			{
				tSettings.Formatting = Formatting.Indented;
			}
			jsonString = JsonConvert.SerializeObject(this, tSettings);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		if (string.IsNullOrEmpty(jsonString) || jsonString.Length < 20)
		{
			throw new Exception("Error while creating json ( empty string < 20 )");
		}
		return jsonString;
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x00173870 File Offset: 0x00171A70
	public void toZip(string pFilePath)
	{
		using (FileStream fs = new FileStream(pFilePath, FileMode.Create, FileAccess.Write))
		{
			using (ZlibStream zs = new ZlibStream(fs, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
			{
				using (StreamWriter sw = new StreamWriter(zs))
				{
					using (JsonWriter writer = new JsonTextWriter(sw))
					{
						JsonHelper.writer.Serialize(writer, this);
						Config.scheduleGC("toZip", false);
					}
				}
			}
		}
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x00173914 File Offset: 0x00171B14
	public byte[] toZip()
	{
		return ZlibStream.CompressString(this.toJson(false));
	}

	// Token: 0x06002FE5 RID: 12261 RVA: 0x00173924 File Offset: 0x00171B24
	public MapMetaData getMeta()
	{
		MapMetaData tMetaData = new MapMetaData();
		int tPopulation = 0;
		int tMobs = 0;
		int tFavorites = 0;
		Dictionary<long, bool> tSapientSubspeciesPool = CollectionPool<Dictionary<long, bool>, KeyValuePair<long, bool>>.Get();
		if (this.subspecies != null)
		{
			foreach (SubspeciesData tSubspecies in this.subspecies)
			{
				if (tSubspecies.saved_traits.Contains("prefrontal_cortex"))
				{
					tSapientSubspeciesPool.Add(tSubspecies.id, true);
				}
			}
		}
		if (this.actors_data != null)
		{
			foreach (ActorData tData in this.actors_data)
			{
				if (AssetManager.actor_library.has(tData.asset_id))
				{
					if (tData.favorite)
					{
						tFavorites++;
					}
					if (tData.civ_kingdom_id != -1L)
					{
						tPopulation++;
					}
					else if (tSapientSubspeciesPool.ContainsKey(tData.subspecies))
					{
						tPopulation++;
					}
					else
					{
						ActorAsset tAsset = AssetManager.actor_library.get(tData.asset_id);
						if (tAsset != null)
						{
							if (tAsset.civ)
							{
								tPopulation++;
							}
							else
							{
								tMobs++;
							}
						}
					}
				}
			}
		}
		CollectionPool<Dictionary<long, bool>, KeyValuePair<long, bool>>.Release(tSapientSubspeciesPool);
		int tItems = 0;
		int tFavoriteItems = 0;
		if (this.items != null)
		{
			foreach (ItemData tData2 in this.items)
			{
				if (AssetManager.items.has(tData2.asset_id))
				{
					if (tData2.favorite)
					{
						tFavoriteItems++;
					}
					tItems++;
				}
			}
		}
		tMetaData.saveVersion = this.saveVersion;
		tMetaData.width = this.width;
		tMetaData.height = this.height;
		tMetaData.mapStats = this.mapStats;
		tMetaData.cities = this.cities.Count;
		MapMetaData mapMetaData = tMetaData;
		List<ActorData> list = this.actors_data;
		mapMetaData.units = ((list != null) ? list.Count : 0);
		tMetaData.population = tPopulation;
		tMetaData.mobs = tMobs;
		tMetaData.deaths = World.world.map_stats.deaths;
		tMetaData.favorites = tFavorites;
		tMetaData.favorite_items = tFavoriteItems;
		tMetaData.equipment = tItems;
		tMetaData.books = this.books.Count;
		tMetaData.wars = this.wars.Count;
		tMetaData.alliances = this.alliances.Count;
		tMetaData.families = this.families.Count;
		tMetaData.clans = this.clans.Count;
		tMetaData.cultures = this.cultures.Count;
		tMetaData.religions = this.religions.Count;
		tMetaData.languages = this.languages.Count;
		tMetaData.subspecies = this.subspecies.Count;
		tMetaData.cursed = WorldLawLibrary.world_law_cursed_world.isEnabled();
		int tBuildings = 0;
		int tStructures = 0;
		int tVegetation = 0;
		foreach (BuildingData tBuilding in this.buildings)
		{
			if (AssetManager.buildings.has(tBuilding.asset_id))
			{
				if (tBuilding.cityID.hasValue())
				{
					tBuildings++;
				}
				if (AssetManager.buildings.get(tBuilding.asset_id).flora)
				{
					tVegetation++;
				}
				tStructures++;
			}
		}
		tMetaData.buildings = tBuildings;
		tMetaData.structures = tStructures;
		tMetaData.kingdoms = this.kingdoms.Count;
		tMetaData.vegetation = tVegetation;
		return tMetaData;
	}

	// Token: 0x06002FE6 RID: 12262 RVA: 0x00173CDC File Offset: 0x00171EDC
	[OnDeserializing]
	private void OnDeserializingMethod(StreamingContext context)
	{
		LongJsonConverter.reset();
	}

	// Token: 0x040023DD RID: 9181
	public int saveVersion;

	// Token: 0x040023DE RID: 9182
	public int width;

	// Token: 0x040023DF RID: 9183
	public int height;

	// Token: 0x040023E0 RID: 9184
	public HotkeyTabsData hotkey_tabs_data;

	// Token: 0x040023E1 RID: 9185
	public float camera_pos_x;

	// Token: 0x040023E2 RID: 9186
	public float camera_pos_y;

	// Token: 0x040023E3 RID: 9187
	public float camera_zoom;

	// Token: 0x040023E4 RID: 9188
	public MapStats mapStats;

	// Token: 0x040023E5 RID: 9189
	public WorldLaws worldLaws;

	// Token: 0x040023E6 RID: 9190
	public string tileString;

	// Token: 0x040023E7 RID: 9191
	public List<string> tileMap = new List<string>();

	// Token: 0x040023E8 RID: 9192
	public int[][] tileArray;

	// Token: 0x040023E9 RID: 9193
	public int[][] tileAmounts;

	// Token: 0x040023EA RID: 9194
	public List<int> fire = new List<int>();

	// Token: 0x040023EB RID: 9195
	public List<int> conwayEater = new List<int>();

	// Token: 0x040023EC RID: 9196
	public List<int> conwayCreator = new List<int>();

	// Token: 0x040023ED RID: 9197
	public List<int> frozen_tiles = new List<int>();

	// Token: 0x040023EE RID: 9198
	public List<WorldTileData> tiles = new List<WorldTileData>();

	// Token: 0x040023EF RID: 9199
	public List<CityData> cities = new List<CityData>();

	// Token: 0x040023F0 RID: 9200
	[Preserve]
	[Obsolete]
	public List<ActorDataObsolete> actors;

	// Token: 0x040023F1 RID: 9201
	public List<ActorData> actors_data = new List<ActorData>();

	// Token: 0x040023F2 RID: 9202
	public List<BuildingData> buildings = new List<BuildingData>();

	// Token: 0x040023F3 RID: 9203
	public List<KingdomData> kingdoms = new List<KingdomData>();

	// Token: 0x040023F4 RID: 9204
	public List<ClanData> clans = new List<ClanData>();

	// Token: 0x040023F5 RID: 9205
	public List<AllianceData> alliances = new List<AllianceData>();

	// Token: 0x040023F6 RID: 9206
	public List<WarData> wars = new List<WarData>();

	// Token: 0x040023F7 RID: 9207
	public List<PlotData> plots = new List<PlotData>();

	// Token: 0x040023F8 RID: 9208
	public List<DiplomacyRelationData> relations = new List<DiplomacyRelationData>();

	// Token: 0x040023F9 RID: 9209
	public List<CultureData> cultures = new List<CultureData>();

	// Token: 0x040023FA RID: 9210
	public List<BookData> books = new List<BookData>();

	// Token: 0x040023FB RID: 9211
	public List<SubspeciesData> subspecies = new List<SubspeciesData>();

	// Token: 0x040023FC RID: 9212
	public List<LanguageData> languages = new List<LanguageData>();

	// Token: 0x040023FD RID: 9213
	public List<ReligionData> religions = new List<ReligionData>();

	// Token: 0x040023FE RID: 9214
	public List<FamilyData> families = new List<FamilyData>();

	// Token: 0x040023FF RID: 9215
	public List<ArmyData> armies = new List<ArmyData>();

	// Token: 0x04002400 RID: 9216
	public List<ItemData> items = new List<ItemData>();
}
