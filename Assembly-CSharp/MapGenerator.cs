using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public static class MapGenerator
{
	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00128885 File Offset: 0x00126A85
	private static MapGenValues gen_values
	{
		get
		{
			return MapGenerator.template.values;
		}
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x00128891 File Offset: 0x00126A91
	public static void clear()
	{
		MapGenerator._tilesMap = null;
		MapGenerator._rooms.Clear();
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x001288A4 File Offset: 0x00126AA4
	public static void prepare()
	{
		MapGenerator.template = AssetManager.map_gen_templates.get(Config.current_map_template);
		MapGenerator._width = MapBox.width;
		MapGenerator._height = MapBox.height;
		MapGenerator._tilesMap = World.world.tiles_map;
		MapGenerator.schedulePerlinNoiseMap();
		MapGenerator.scheduleUpdateTileTypes();
		if (MapGenerator.gen_values.forbidden_knowledge_start)
		{
			World.world.world_laws.enable("world_law_cursed_world");
			CursedSacrifice.loadAlreadyCursedState();
		}
		if (MapGenerator.gen_values.remove_mountains)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.removeMountains();
			}, "Normalize Ground", false, 0.001f, false);
		}
		if (MapGenerator.template.special_anthill)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.specialAnthill();
			}, "Anthill", false, 0.001f, false);
		}
		if (MapGenerator.template.special_checkerboard)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.specialCheckerBoard();
			}, "Checkerboard", false, 0.001f, false);
		}
		if (MapGenerator.template.special_cubicles)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.specialCubicles();
			}, "Cubicles", false, 0.001f, false);
		}
		MapGenerator.scheduleRandomShapes(Randy.randomBool());
		if (MapGenerator.template.perlin_replace.Count > 0)
		{
			using (List<PerlinReplaceContainer>.Enumerator enumerator = MapGenerator.template.perlin_replace.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PerlinReplaceContainer tOption = enumerator.Current;
					SmoothLoader.add(delegate
					{
						GeneratorTool.ApplyPerlinReplace(tOption);
					}, "Perlin Replace", false, 0.001f, false);
				}
			}
		}
		SmoothLoader.add(delegate
		{
			World.world.map_chunk_manager.allDirty();
		}, "Map Chunk Manager (1/2)", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			World.world.map_chunk_manager.update(0f, true);
		}, "Map Chunk Manager (2/2)", false, 0.1f, false);
		if (MapGenerator.gen_values.random_biomes)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.generateBiomes();
			}, "Add Random Biome", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.add_mountain_edges)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.addMountainEdges();
			}, "Add Mountain Edges", false, 0.001f, false);
		}
		if (MapGenerator.template.freeze_mountains)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.freezeMountainTops();
			}, "Freeze Mountain Tops", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.add_vegetation)
		{
			int vegTimes = 12;
			int vegSpread = World.world.tiles_list.Length / 20;
			MapLoaderAction <>9__11;
			for (int i = 0; i < vegTimes; i++)
			{
				MapLoaderAction pAction;
				if ((pAction = <>9__11) == null)
				{
					pAction = (<>9__11 = delegate()
					{
						AssetManager.tiles.setListTo(DepthGeneratorType.Gameplay);
						MapGenerator.spawnVegetation(vegSpread);
					});
				}
				SmoothLoader.add(pAction, string.Concat(new string[]
				{
					"Add Vegetation (",
					(i + 1).ToString(),
					"/",
					vegTimes.ToString(),
					")"
				}), false, 0.001f, false);
			}
		}
		if (MapGenerator.gen_values.add_resources)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.spawnResources();
			}, "Add Resources", false, 0.001f, false);
		}
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x00128C80 File Offset: 0x00126E80
	private static void scheduleUpdateTileTypes()
	{
		int tileTypesSplit = 4;
		int tilesLoadedPerFrame = World.world.tiles_list.Length / tileTypesSplit;
		int loadedTiles = 0;
		for (int i = 0; i < tileTypesSplit; i++)
		{
			int tileAmount = Mathf.Min(World.world.tiles_list.Length - loadedTiles, tilesLoadedPerFrame);
			int startIndex = loadedTiles;
			loadedTiles += tileAmount;
			SmoothLoader.add(delegate
			{
				GeneratorTool.UpdateTileTypes(true, startIndex, tileAmount);
			}, string.Concat(new string[]
			{
				"Generate Tiles (",
				loadedTiles.ToString(),
				"/",
				World.world.tiles_list.Length.ToString(),
				")"
			}), true, 0.001f, false);
		}
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x00128D44 File Offset: 0x00126F44
	private static void scheduleRandomShapes(bool pSubstract)
	{
		if (MapGenerator.gen_values.random_shapes_amount == 0)
		{
			return;
		}
		SmoothLoader.add(delegate
		{
			GeneratorTool.Init();
		}, "Perlin Random Shapes (Init)", false, 0.001f, false);
		MapLoaderAction <>9__1;
		for (int i = 0; i < MapGenerator.gen_values.random_shapes_amount; i++)
		{
			MapLoaderAction pAction;
			if ((pAction = <>9__1) == null)
			{
				pAction = (<>9__1 = delegate()
				{
					GeneratorTool.ApplyRandomShape("height", 2f, 0.7f, pSubstract);
				});
			}
			SmoothLoader.add(pAction, string.Concat(new string[]
			{
				"Perlin Random Shapes (",
				(i + 1).ToString(),
				"/",
				MapGenerator.gen_values.random_shapes_amount.ToString(),
				")"
			}), false, 0.001f, false);
		}
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x00128E20 File Offset: 0x00127020
	private static void specialCubicles()
	{
		HashSet<MapChunk> chunks_used = new HashSet<MapChunk>();
		List<MapChunk> chunks_left = new List<MapChunk>();
		foreach (MapChunk tChunk in World.world.map_chunk_manager.chunks)
		{
			chunks_left.Add(tChunk);
		}
		MapGenerator._rooms.Clear();
		while (chunks_left.Count > 0)
		{
			MapChunk tChunk2 = chunks_left.GetRandom<MapChunk>();
			MapGenerator.startCubicle(chunks_used, chunks_left, tChunk2);
		}
		MapGenerator.createDoors();
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x00128E90 File Offset: 0x00127090
	private static void startCubicle(HashSet<MapChunk> pChunksUsed, List<MapChunk> pListLeft, MapChunk pStartRoomChunk)
	{
		MapChunk tNextChunk = pStartRoomChunk;
		List<MapChunk> tNewRoom = new List<MapChunk>();
		MapGenerator.rememberChunk(tNextChunk, pChunksUsed, pListLeft, tNewRoom);
		int tMin = 2;
		int tMax = MapGenerator.gen_values.cubicle_size + 2;
		int tWidth = Randy.randomInt(tMin, tMax);
		int i = 0;
		while (i < tWidth && tNextChunk != null)
		{
			tNextChunk = tNextChunk.chunk_right;
			if (tNextChunk != null && !pChunksUsed.Contains(tNextChunk))
			{
				MapGenerator.rememberChunk(tNextChunk, pChunksUsed, pListLeft, tNewRoom);
			}
			i++;
		}
		tNextChunk = pStartRoomChunk;
		tWidth = Randy.randomInt(tMin, tMax);
		int j = 0;
		while (j < tWidth && tNextChunk != null)
		{
			tNextChunk = tNextChunk.chunk_left;
			if (tNextChunk != null && !pChunksUsed.Contains(tNextChunk))
			{
				MapGenerator.rememberChunk(tNextChunk, pChunksUsed, pListLeft, tNewRoom);
			}
			j++;
		}
		List<MapChunk> tInitialLine = new List<MapChunk>();
		tInitialLine.AddRange(tNewRoom);
		List<MapChunk> tPrevLine = new List<MapChunk>();
		tPrevLine.AddRange(tInitialLine);
		int tHeight = Randy.randomInt(tMin, tMax);
		for (int k = 0; k < tHeight; k++)
		{
			List<MapChunk> tNewLine = new List<MapChunk>();
			bool tCanAddBottom = true;
			foreach (MapChunk tChunk in tPrevLine)
			{
				if (tChunk.chunk_down == null)
				{
					tCanAddBottom = false;
					k = tHeight;
					break;
				}
				if (pChunksUsed.Contains(tChunk.chunk_down))
				{
					tCanAddBottom = false;
					k = tHeight;
					break;
				}
				tNewLine.Add(tChunk);
			}
			if (tCanAddBottom)
			{
				tPrevLine.Clear();
				foreach (MapChunk tChunk2 in tNewLine)
				{
					MapGenerator.rememberChunk(tChunk2.chunk_down, pChunksUsed, pListLeft, tNewRoom);
					tPrevLine.Add(tChunk2.chunk_down);
				}
			}
		}
		tPrevLine.Clear();
		tPrevLine.AddRange(tInitialLine);
		tHeight = Randy.randomInt(tMin, tMax);
		for (int l = 0; l < tHeight; l++)
		{
			List<MapChunk> tNewLine2 = new List<MapChunk>();
			bool tCanAddBottom2 = true;
			foreach (MapChunk tChunk3 in tPrevLine)
			{
				if (tChunk3.chunk_up == null)
				{
					tCanAddBottom2 = false;
					l = tHeight;
					break;
				}
				if (pChunksUsed.Contains(tChunk3.chunk_up))
				{
					tCanAddBottom2 = false;
					l = tHeight;
					break;
				}
				tNewLine2.Add(tChunk3);
			}
			if (tCanAddBottom2)
			{
				tPrevLine.Clear();
				foreach (MapChunk tChunk4 in tNewLine2)
				{
					MapGenerator.rememberChunk(tChunk4.chunk_up, pChunksUsed, pListLeft, tNewRoom);
					tPrevLine.Add(tChunk4.chunk_up);
				}
			}
		}
		MapGenerator.finishRoom(tNewRoom);
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x00129158 File Offset: 0x00127358
	private static void rememberChunk(MapChunk pChunk, HashSet<MapChunk> pChunksUsed, List<MapChunk> pListLeft, List<MapChunk> pNewRoom)
	{
		pChunksUsed.Add(pChunk);
		pListLeft.Remove(pChunk);
		pNewRoom.Add(pChunk);
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x00129174 File Offset: 0x00127374
	private static void finishRoom(List<MapChunk> pChunks)
	{
		BiomeAsset tBiome = BiomeLibrary.pool_biomes.GetRandom<BiomeAsset>();
		TileType tType = TileLibrary.soil_high;
		if (Randy.randomBool())
		{
			tType = TileLibrary.soil_low;
		}
		WorldTile t_u_l = World.world.GetTileSimple(0, 0);
		WorldTile t_u_r = World.world.GetTileSimple(MapBox.width - 1, 0);
		WorldTile t_d_l = World.world.GetTileSimple(0, MapBox.height - 1);
		WorldTile t_d_r = World.world.GetTileSimple(MapBox.width - 1, MapBox.height - 1);
		WorldTile tRoom_u_l = null;
		WorldTile tRoom_u_r = null;
		WorldTile tRoom_d_l = null;
		WorldTile tRoom_d_r = null;
		float best_dist_u_l = 0f;
		float best_dist_u_r = 0f;
		float best_dist_d_l = 0f;
		float best_dist_d_r = 0f;
		for (int i = 0; i < pChunks.Count; i++)
		{
			WorldTile[] tTiles = pChunks[i].tiles;
			int tCount = tTiles.Length;
			for (int j = 0; j < tCount; j++)
			{
				WorldTile tTile = tTiles[j];
				MapAction.terraformTile(tTile, tType, null, null, false);
				if (MapGenerator.gen_values.random_biomes)
				{
					DropsLibrary.useSeedOn(tTile, tBiome.getTileLow(), tBiome.getTileHigh());
				}
				float dist_u_l = Toolbox.DistTile(t_u_l, tTile);
				float dist_u_r = Toolbox.DistTile(t_u_r, tTile);
				float dist_d_l = Toolbox.DistTile(t_d_l, tTile);
				float dist_d_r = Toolbox.DistTile(t_d_r, tTile);
				if (tRoom_u_l == null || dist_u_l < best_dist_u_l)
				{
					tRoom_u_l = tTile;
					best_dist_u_l = dist_u_l;
				}
				if (tRoom_u_r == null || dist_u_r < best_dist_u_r)
				{
					tRoom_u_r = tTile;
					best_dist_u_r = dist_u_r;
				}
				if (tRoom_d_l == null || dist_d_l < best_dist_d_l)
				{
					tRoom_d_l = tTile;
					best_dist_d_l = dist_d_l;
				}
				if (tRoom_d_r == null || dist_d_r < best_dist_d_r)
				{
					tRoom_d_r = tTile;
					best_dist_d_r = dist_d_r;
				}
			}
		}
		GeneratedRoom tRoomObject = new GeneratedRoom();
		tRoomObject.id_debug = MapGenerator._rooms.Count;
		MapGenerator._rooms.Add(tRoomObject);
		tRoomObject.edges_up = MapGenerator.fillTiles(tRoom_u_l, tRoom_u_r, TileLibrary.mountains);
		tRoomObject.edges_down = MapGenerator.fillTiles(tRoom_d_l, tRoom_d_r, TileLibrary.mountains);
		tRoomObject.edges_left = MapGenerator.fillTiles(tRoom_u_l, tRoom_d_l, TileLibrary.mountains);
		tRoomObject.edges_right = MapGenerator.fillTiles(tRoom_d_r, tRoom_u_r, TileLibrary.mountains);
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x00129378 File Offset: 0x00127578
	private static void createDoors()
	{
		foreach (GeneratedRoom generatedRoom in MapGenerator._rooms)
		{
			MapGenerator.makeDoor(generatedRoom.edges_down);
			MapGenerator.makeDoor(generatedRoom.edges_left);
			MapGenerator.makeDoor(generatedRoom.edges_right);
			MapGenerator.makeDoor(generatedRoom.edges_up);
		}
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x001293F0 File Offset: 0x001275F0
	private static void makeDoor(List<WorldTile> pTiles)
	{
		using (List<WorldTile>.Enumerator enumerator = pTiles.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.main_type != TileLibrary.mountains)
				{
					return;
				}
			}
		}
		WorldTile tDoorTile;
		if (pTiles.Count > 3)
		{
			int tRandomIndex = Randy.randomInt(3, pTiles.Count - 3);
			tDoorTile = pTiles[tRandomIndex];
		}
		else
		{
			int tIndex = pTiles.Count / 2;
			tDoorTile = pTiles[tIndex];
		}
		MapAction.terraformTile(tDoorTile, TileLibrary.hills, null, null, false);
		foreach (WorldTile tNTile in tDoorTile.neighboursAll)
		{
			if (tNTile.main_type == TileLibrary.mountains)
			{
				MapAction.terraformTile(tNTile, TileLibrary.hills, null, null, false);
			}
		}
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x001294C8 File Offset: 0x001276C8
	private static void specialCheckerBoard()
	{
		BiomeAsset tBiome = BiomeLibrary.pool_biomes.GetRandom<BiomeAsset>();
		BiomeAsset tBiome2 = BiomeLibrary.pool_biomes.GetRandom<BiomeAsset>();
		foreach (MapChunk tChunk in World.world.map_chunk_manager.chunks)
		{
			WorldTile[] tTiles = tChunk.tiles;
			if ((tChunk.x + tChunk.y) % 2 == 0)
			{
				int tCount = tTiles.Length;
				for (int j = 0; j < tCount; j++)
				{
					WorldTile tTile = tTiles[j];
					MapAction.terraformTile(tTile, TileLibrary.soil_high, null, null, false);
					if (MapGenerator.gen_values.random_biomes)
					{
						DropsLibrary.useSeedOn(tTile, tBiome.getTileLow(), tBiome.getTileHigh());
					}
				}
			}
			else
			{
				int tCount2 = tTiles.Length;
				for (int k = 0; k < tCount2; k++)
				{
					WorldTile tTile2 = tTiles[k];
					MapAction.terraformTile(tTile2, TileLibrary.soil_low, null, null, false);
					if (MapGenerator.gen_values.random_biomes)
					{
						DropsLibrary.useSeedOn(tTile2, tBiome2.getTileLow(), tBiome2.getTileHigh());
					}
				}
			}
		}
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x001295D0 File Offset: 0x001277D0
	private static void specialAnthill()
	{
		WorldTile[] tiles_list = World.world.tiles_list;
		for (int i = 0; i < tiles_list.Length; i++)
		{
			MapAction.terraformTile(tiles_list[i], TileLibrary.mountains, null, null, false);
		}
		List<TileZone> list = new List<TileZone>();
		List<WorldTile> tTunnels = new List<WorldTile>();
		ZoneCalculator tZones = World.world.zone_calculator;
		int tOffset_x = tZones.zones_total_x / 10 + 1;
		int tOffset_y = tZones.zones_total_y / 10 + 1;
		TileZone t_U_L = tZones.map[tOffset_x, tOffset_y];
		TileZone t_U_R = tZones.map[tZones.zones_total_x - tOffset_x, tOffset_y];
		TileZone t_D_R = tZones.map[tZones.zones_total_x - tOffset_x, tZones.zones_total_y - tOffset_y];
		TileZone t_D_L = tZones.map[tOffset_x, tZones.zones_total_y - tOffset_y];
		MapGenerator.makeJailRoom(list, tZones.map[tZones.zones_total_x / 2, tZones.zones_total_y / 2]);
		MapGenerator.makeJailRoom(list, tZones.map[tZones.zones_total_x / 2, tZones.zones_total_y / 2]);
		MapGenerator.makeJailRoom(list, t_U_L);
		MapGenerator.makeJailRoom(list, t_U_L);
		MapGenerator.makeJailRoom(list, t_U_R);
		MapGenerator.makeJailRoom(list, t_U_R);
		MapGenerator.makeJailRoom(list, t_D_R);
		MapGenerator.makeJailRoom(list, t_D_R);
		MapGenerator.makeJailRoom(list, t_D_L);
		MapGenerator.makeJailRoom(list, t_D_L);
		MapGenerator.makeWay(t_U_L, t_U_R, tTunnels);
		MapGenerator.makeWay(t_D_L, t_D_R, tTunnels);
		MapGenerator.makeWay(t_U_L, t_D_L, tTunnels);
		MapGenerator.makeWay(t_D_R, t_U_R, tTunnels);
		MapGenerator.makeWay(t_D_R, t_U_L, tTunnels);
		foreach (TileZone pZone in list)
		{
			MapGenerator.carveZone(pZone);
		}
		foreach (WorldTile pTile in tTunnels)
		{
			MapGenerator.carveTunnel(pTile);
		}
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x001297C4 File Offset: 0x001279C4
	private static List<WorldTile> fillTiles(WorldTile pTile1, WorldTile pTile2, TileType pType)
	{
		List<WorldTile> list = PathfinderTools.raycast(pTile1, pTile2, 1f);
		List<WorldTile> tNewList = new List<WorldTile>(list);
		list.Clear();
		foreach (WorldTile pTile3 in tNewList)
		{
			MapAction.terraformTile(pTile3, pType, null, null, false);
		}
		return tNewList;
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x0012982C File Offset: 0x00127A2C
	private static void makeWay(TileZone tZone1, TileZone tZone2, List<WorldTile> pTunnels)
	{
		List<WorldTile> tRaycastResult = PathfinderTools.raycast(tZone1.centerTile, tZone2.centerTile, 0.99f);
		foreach (WorldTile worldTile in tRaycastResult)
		{
			foreach (WorldTile tNeighbour in worldTile.neighboursAll)
			{
				MapAction.terraformTile(tNeighbour, TileLibrary.soil_high, null, null, false);
				pTunnels.Add(tNeighbour);
			}
		}
		tRaycastResult.Clear();
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x001298C0 File Offset: 0x00127AC0
	private static void carveTunnel(WorldTile pTile)
	{
		for (int i = 0; i < 10; i++)
		{
			WorldTile tTile = pTile.neighbours.GetRandom<WorldTile>();
			for (int tN = 10; tN > 0; tN--)
			{
				WorldTile tNeighbour = tTile.neighbours.GetRandom<WorldTile>();
				if (tNeighbour.Type.rocks)
				{
					MapAction.terraformTile(tTile, TileLibrary.soil_high, null, null, false);
					tTile = tNeighbour;
				}
			}
		}
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x0012991C File Offset: 0x00127B1C
	private static void carveZone(TileZone pZone)
	{
		for (int i = 0; i < 20; i++)
		{
			WorldTile tTile = pZone.tiles.GetRandom<WorldTile>();
			for (int tN = 15; tN > 0; tN--)
			{
				WorldTile tNeighbour = tTile.neighbours.GetRandom<WorldTile>();
				if (tNeighbour.Type.rocks)
				{
					MapAction.terraformTile(tTile, TileLibrary.soil_high, null, null, false);
					tTile = tNeighbour;
				}
			}
		}
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x00129978 File Offset: 0x00127B78
	private static void makeJailRoom(List<TileZone> pZones, TileZone pStartZone)
	{
		int tRoomSize = World.world.zone_calculator.zones.Count / 10;
		TileZone tZone = pStartZone;
		if (tZone.world_edge)
		{
			return;
		}
		for (int i = 0; i < tRoomSize; i++)
		{
			if (tZone.world_edge)
			{
				tZone = tZone.neighbours.GetRandom<TileZone>();
			}
			else
			{
				WorldTile[] tTiles = tZone.tiles;
				int tCount = tTiles.Length;
				for (int j = 0; j < tCount; j++)
				{
					MapAction.terraformTile(tTiles[j], TileLibrary.soil_high, null, null, false);
				}
				pZones.Add(tZone);
				tZone = tZone.neighbours.GetRandom<TileZone>();
			}
		}
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x00129A0C File Offset: 0x00127C0C
	private static void schedulePerlinNoiseMap()
	{
		MapGenerator.scheduleRandomShapes(true);
		if (MapGenerator.gen_values.main_perlin_noise_stage && MapGenerator.gen_values.perlin_scale_stage_1 > 0)
		{
			SmoothLoader.add(delegate
			{
				int tPerlinX = Randy.randomInt(0, 1000000);
				int tPerlinY = Randy.randomInt(0, 1000000);
				GeneratorTool.ApplyPerlinNoise(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, (float)tPerlinX, (float)tPerlinY, 1f, 1f * (float)MapGenerator.gen_values.perlin_scale_stage_1, false, GeneratorTarget.Height);
			}, "Perlin Noise", true, 0.001f, false);
		}
		if (MapGenerator.template.force_height_to > 0)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.forceHeight();
			}, "Add Height", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.add_center_gradient_land)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.addCenterGradient();
			}, "Center Gradient", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.center_gradient_mountains)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.addCenterMountains();
			}, "Center Mountains", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.add_center_lake)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.addCenterLake();
			}, "Center Lake", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.perlin_noise_stage_2 && MapGenerator.gen_values.perlin_scale_stage_2 > 0)
		{
			SmoothLoader.add(delegate
			{
				float tScale = (float)MapGenerator.gen_values.perlin_scale_stage_2;
				int tRandomX = Randy.randomInt(0, 1000000);
				int tRandomY = Randy.randomInt(0, 1000000);
				GeneratorTool.ApplyPerlinNoise(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, (float)tRandomX, (float)tRandomY, 0.2f, 4f * tScale, true, GeneratorTarget.Height);
			}, "Perlin Noise (1)", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.perlin_noise_stage_3 && MapGenerator.gen_values.perlin_scale_stage_3 > 0)
		{
			SmoothLoader.add(delegate
			{
				float tScale = (float)MapGenerator.gen_values.perlin_scale_stage_3;
				int tRandomX = Randy.randomInt(0, 1000000);
				int tRandomY = Randy.randomInt(0, 1000000);
				GeneratorTool.ApplyPerlinNoise(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, (float)tRandomX, (float)tRandomY, 0.1f, tScale * 10f, true, GeneratorTarget.Height);
			}, "Perlin Noise (2)", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.low_ground)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.lowGround();
			}, "Lower Ground", false, 0.001f, false);
		}
		if (MapGenerator.gen_values.high_ground)
		{
			SmoothLoader.add(delegate
			{
				MapGenerator.highGround();
			}, "High Ground", false, 0.001f, false);
		}
		MapGenerator.scheduleRandomShapes(true);
		if (MapGenerator.gen_values.ring_effect)
		{
			SmoothLoader.add(delegate
			{
				GeneratorTool.ApplyRingEffect();
			}, "Perlin Ring", true, 0.001f, false);
		}
		if (MapGenerator.gen_values.gradient_round_edges)
		{
			SmoothLoader.add(delegate
			{
				MapEdges.AddEdgeGradientCircle(World.world.tiles_map, "height");
			}, "Gradient Circle Edges", true, 0.001f, false);
		}
		if (MapGenerator.gen_values.square_edges)
		{
			SmoothLoader.add(delegate
			{
				MapEdges.AddEdgeSquare(World.world.tiles_map, "height");
			}, "Gradient Circle Edges", true, 0.001f, false);
		}
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x00129D20 File Offset: 0x00127F20
	private static void forceHeight()
	{
		WorldTile[] tiles_list = World.world.tiles_list;
		for (int i = 0; i < tiles_list.Length; i++)
		{
			tiles_list[i].Height = MapGenerator.template.force_height_to;
		}
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x00129D58 File Offset: 0x00127F58
	private static void removeMountains()
	{
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.Type.rocks)
			{
				MapAction.decreaseTile(tTile, false, "flash");
			}
			if (tTile.Type.rocks)
			{
				MapAction.decreaseTile(tTile, false, "flash");
			}
		}
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x00129DB4 File Offset: 0x00127FB4
	private static void lowGround()
	{
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.Height > 150)
			{
				tTile.Height -= 50;
			}
			if (tTile.Height > 130)
			{
				tTile.Height -= 20;
			}
		}
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x00129E18 File Offset: 0x00128018
	private static void highGround()
	{
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.Height < 20)
			{
				tTile.Height += 80;
			}
			else if (tTile.Height < 120)
			{
				tTile.Height += 40;
			}
		}
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x00129E78 File Offset: 0x00128078
	private static void addCenterGradient()
	{
		WorldTile tCenter = World.world.tiles_map[MapBox.width / 2, MapBox.height / 2];
		float tMaxMod = 0.9f;
		float tGradientMod = 0.6f;
		float tMaxCenter = (float)(MapBox.width / 2) * tMaxMod;
		float tGradient = (float)(MapBox.width / 2) * tGradientMod;
		float tDiff = tMaxCenter - tGradient;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			float tDist = Toolbox.DistTile(tTile, tCenter);
			if (tDist <= tMaxCenter)
			{
				float tMod = (tMaxCenter - tDist) / tDiff;
				int tNewHeight = (int)(45f * tMod);
				tTile.Height += tNewHeight;
			}
		}
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x00129F28 File Offset: 0x00128128
	private static void addCenterLake()
	{
		WorldTile tCenter = World.world.tiles_map[MapBox.width / 2, MapBox.height / 2];
		float tMaxMod = 0.6f;
		float tGradientMod = 0.2f;
		float tMaxCenter = (float)(MapBox.width / 2) * tMaxMod;
		float tGradient = (float)(MapBox.width / 2) * tGradientMod;
		float tDiff = tMaxCenter - tGradient;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			float tDist = Toolbox.DistTile(tTile, tCenter);
			if (tDist <= tMaxCenter)
			{
				float tVal = tMaxCenter - tDist;
				float tMod = 1f - tVal / tDiff;
				int tNewHeight = (int)((float)tTile.Height * tMod);
				tTile.Height = tNewHeight;
			}
		}
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x00129FDC File Offset: 0x001281DC
	private static void addCenterMountains()
	{
		WorldTile tCenter = World.world.tiles_map[MapBox.width / 2, MapBox.height / 2];
		float tMaxMod = 0.3f;
		float tGradientMod = 0f;
		float tMaxCenter = (float)(MapBox.width / 2) * tMaxMod;
		float tGradient = (float)(MapBox.width / 2) * tGradientMod;
		float tDiff = tMaxCenter - tGradient;
		foreach (WorldTile worldTile in World.world.tiles_list)
		{
			float tDist = Toolbox.DistTile(worldTile, tCenter);
			float tMod = (tMaxCenter - tDist) / tDiff;
			int tNewHeight = (int)(75f * tMod);
			worldTile.Height -= tNewHeight;
		}
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x0012A080 File Offset: 0x00128280
	private static void generateBiomes()
	{
		HashSetWorldTile tTilesSoilHashset = new HashSetWorldTile();
		for (int i = 0; i < World.world.tiles_list.Length; i++)
		{
			WorldTile tTile = World.world.tiles_list[i];
			if (tTile.Type.soil)
			{
				tTilesSoilHashset.Add(tTile);
			}
		}
		using (ListPool<WorldTile> tTempSoilTiles = new ListPool<WorldTile>(tTilesSoilHashset.Count))
		{
			bool tRecreateList = true;
			while (tTilesSoilHashset.Count > 0)
			{
				if (tRecreateList)
				{
					tRecreateList = false;
					MapGenerator.recreateSoilList(tTilesSoilHashset, tTempSoilTiles);
				}
				WorldTile tStartTile = tTempSoilTiles.Last<WorldTile>();
				BiomeAsset tBiome = BiomeLibrary.pool_biomes.GetRandom<BiomeAsset>();
				int tMaxSteps = MapGenerator.tryMakeBiomeSteps(tStartTile, tBiome);
				if (tMaxSteps == 0)
				{
					tTempSoilTiles.Pop<WorldTile>();
				}
				else
				{
					MapGenerator.tryMakeBiome(tStartTile, tTilesSoilHashset, tMaxSteps, tBiome);
					tRecreateList = true;
				}
			}
		}
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x0012A14C File Offset: 0x0012834C
	private static void recreateSoilList(HashSetWorldTile pHashSet, ListPool<WorldTile> pTempSoilTiles)
	{
		pTempSoilTiles.Clear();
		pTempSoilTiles.AddRange(pHashSet);
		pTempSoilTiles.Shuffle<WorldTile>();
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x0012A164 File Offset: 0x00128364
	private static int tryMakeBiomeSteps(WorldTile pStartTile, BiomeAsset pBiome)
	{
		if (!MapGenerator.reported_tryMakeBiomeSteps)
		{
			if (pBiome == null)
			{
				Debug.Log("pBiome is null");
				MapGenerator.reported_tryMakeBiomeSteps = true;
			}
			if (pStartTile == null)
			{
				Debug.Log("pStartTile is null");
				MapGenerator.reported_tryMakeBiomeSteps = true;
			}
			if (pStartTile.region == null)
			{
				Debug.Log("pStartTile.region is null");
				MapGenerator.reported_tryMakeBiomeSteps = true;
			}
			if (pStartTile.region.island == null)
			{
				Debug.Log("pStartTile.region.island is null");
				MapGenerator.reported_tryMakeBiomeSteps = true;
			}
		}
		int tIslandTileCount = pStartTile.region.island.getTileCount();
		int tMaxSteps;
		if (tIslandTileCount < 400)
		{
			tMaxSteps = tIslandTileCount;
		}
		else if (tIslandTileCount < 600)
		{
			tMaxSteps = tIslandTileCount / 2;
		}
		else
		{
			tMaxSteps = tIslandTileCount / 3;
		}
		if (tMaxSteps > pBiome.generator_max_size && pBiome.generator_max_size != 0)
		{
			tMaxSteps = pBiome.generator_max_size;
		}
		return tMaxSteps;
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x0012A21C File Offset: 0x0012841C
	private static void tryMakeBiome(WorldTile pStartTile, HashSetWorldTile pSoilTiles, int pMaxSteps, BiomeAsset pBiome)
	{
		int tCurrentSteps = 0;
		using (ListPool<WorldTile> tWave = new ListPool<WorldTile>(pMaxSteps))
		{
			HashSetWorldTile tCheckedTiles = new HashSetWorldTile();
			tWave.Add(pStartTile);
			tCheckedTiles.Add(pStartTile);
			while (tWave.Count > 0 && tCurrentSteps < pMaxSteps)
			{
				tWave.ShuffleLast<WorldTile>();
				WorldTile tNewTile = tWave.Pop<WorldTile>();
				if (tNewTile.isTileRank(TileRank.Low))
				{
					tNewTile.setTopTileType(pBiome.getTileLow(), true);
				}
				else
				{
					tNewTile.setTopTileType(pBiome.getTileHigh(), true);
				}
				pSoilTiles.Remove(tNewTile);
				tCurrentSteps++;
				for (int i = 0; i < tNewTile.neighboursAll.Length; i++)
				{
					WorldTile tTile = tNewTile.neighboursAll[i];
					if (!tCheckedTiles.Contains(tTile) && tTile.Type.soil)
					{
						tWave.Add(tTile);
						tCheckedTiles.Add(tTile);
					}
				}
			}
			if (tCurrentSteps <= 10)
			{
				MapGenerator.removeSmallBiomePatches(tCheckedTiles, pStartTile);
			}
		}
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x0012A310 File Offset: 0x00128510
	private static void removeSmallBiomePatches(HashSetWorldTile pTiles, WorldTile pStartTile)
	{
		WorldTile tCopyFrom = null;
		foreach (WorldTile tTile in pStartTile.neighboursAll)
		{
			if (tTile.Type.is_biome && !tTile.Type.biome_asset.special_biome && tTile.Type.biome_asset != pStartTile.Type.biome_asset)
			{
				tCopyFrom = tTile;
				break;
			}
		}
		if (tCopyFrom == null)
		{
			return;
		}
		BiomeAsset tBiome = tCopyFrom.top_type.biome_asset;
		foreach (WorldTile tCheckedTile in pTiles)
		{
			TopTileType tBiomeTileType = tBiome.getTile(tCheckedTile);
			tCheckedTile.setTopTileType(tBiomeTileType, true);
		}
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x0012A3D8 File Offset: 0x001285D8
	private static void addMountainEdges()
	{
		int tOffset_x = 0;
		int tOffset_y = 0;
		WorldTile tileSimple = World.world.GetTileSimple(tOffset_x, tOffset_y);
		WorldTile t_U_R = World.world.GetTileSimple(MapBox.width - tOffset_x - 1, tOffset_y);
		WorldTile t_D_R = World.world.GetTileSimple(MapBox.width - tOffset_x - 1, MapBox.height - tOffset_y - 1);
		WorldTile t_D_L = World.world.GetTileSimple(tOffset_x, MapBox.height - tOffset_y - 1);
		MapGenerator.fillTiles(tileSimple, t_U_R, TileLibrary.mountains);
		MapGenerator.fillTiles(t_D_L, t_D_R, TileLibrary.mountains);
		MapGenerator.fillTiles(tileSimple, t_D_L, TileLibrary.mountains);
		MapGenerator.fillTiles(t_D_R, t_U_R, TileLibrary.mountains);
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x0012A474 File Offset: 0x00128674
	private static void freezeMountainTops()
	{
		for (int i = 0; i < World.world.tiles_list.Length; i++)
		{
			WorldTile tTile = World.world.tiles_list[i];
			if (tTile.Type.IsType("mountains") && tTile.Height > 220)
			{
				tTile.freeze(1);
			}
		}
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x0012A4CC File Offset: 0x001286CC
	private static void spawnVegetation(int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile tObjTile = World.world.tiles_list.GetRandom<WorldTile>();
			if (tObjTile.Type.ground && tObjTile.zone.countBuildingsType(BuildingList.Trees) < 3)
			{
				BiomeAsset tBiomeAsset = tObjTile.Type.biome_asset;
				if (tBiomeAsset != null && tBiomeAsset.grow_vegetation_auto)
				{
					switch (Randy.randomInt(0, 3))
					{
					case 0:
						BuildingActions.tryGrowVegetationRandom(tObjTile, VegetationType.Plants, true, true, true);
						break;
					case 1:
						BuildingActions.tryGrowVegetationRandom(tObjTile, VegetationType.Trees, true, true, true);
						break;
					case 2:
						BuildingActions.tryGrowVegetationRandom(tObjTile, VegetationType.Bushes, true, true, true);
						break;
					}
				}
			}
		}
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x0012A56C File Offset: 0x0012876C
	private static void spawnResources()
	{
		BuildingActions.spawnResource(World.world.tiles_list.Length / 1000 / 2 / 2, "fruit_bush", false);
	}

	// Token: 0x040019C5 RID: 6597
	public static MapGenTemplate template;

	// Token: 0x040019C6 RID: 6598
	private static int _width = 0;

	// Token: 0x040019C7 RID: 6599
	private static int _height = 0;

	// Token: 0x040019C8 RID: 6600
	private static WorldTile[,] _tilesMap;

	// Token: 0x040019C9 RID: 6601
	private static List<GeneratedRoom> _rooms = new List<GeneratedRoom>();

	// Token: 0x040019CA RID: 6602
	private static bool reported_tryMakeBiomeSteps = false;
}
