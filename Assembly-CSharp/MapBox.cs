using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ai;
using ai.behaviours;
using db;
using DG.Tweening;
using EpPathFinding.cs;
using life.taxi;
using tools.debug;
using UnityEngine;
using WorldBoxConsole;

// Token: 0x02000012 RID: 18
public class MapBox : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000048 RID: 72 RVA: 0x00004FEB File Offset: 0x000031EB
	internal LibraryMaterials library_materials
	{
		get
		{
			return LibraryMaterials.instance;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000049 RID: 73 RVA: 0x00004FF2 File Offset: 0x000031F2
	// (set) Token: 0x0600004A RID: 74 RVA: 0x00004FFA File Offset: 0x000031FA
	internal Camera camera { get; private set; }

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00005003 File Offset: 0x00003203
	// (set) Token: 0x0600004C RID: 76 RVA: 0x0000500B File Offset: 0x0000320B
	internal MoveCamera move_camera { get; private set; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00005014 File Offset: 0x00003214
	// (set) Token: 0x0600004E RID: 78 RVA: 0x0000501C File Offset: 0x0000321C
	internal ZoneCamera zone_camera { get; private set; }

	// Token: 0x0600004F RID: 79 RVA: 0x00005028 File Offset: 0x00003228
	private void Awake()
	{
		MapBox.instance = this;
		this.player_control = new PlayerControl();
		this.parallel_options = new ParallelOptions
		{
			CancellationToken = base.destroyCancellationToken
		};
		this.auto_tester = Object.FindFirstObjectByType<AutoTesterBot>(FindObjectsInactive.Include);
		this.save_manager = base.GetComponentInChildren<SaveManager>();
		this.game_stats = base.GetComponentInChildren<GameStats>();
		this.tilemap = base.GetComponentInChildren<WorldTilemap>();
		this._map_border = base.GetComponentInChildren<MapBorder>();
		this.stack_effects = base.GetComponentInChildren<StackEffects>();
		this.resource_throw_manager = new ResourceThrowManager();
		this.heat_ray_fx = base.GetComponentInChildren<HeatRayEffect>();
		this.fx_divine_light = base.GetComponentInChildren<EffectDivineLight>();
		this.particles_fire = base.transform.Find("Particles Fire").GetComponent<GlowParticles>();
		this.particles_smoke = base.transform.Find("Particles Smoke").GetComponent<GlowParticles>();
		this._shake_camera = GameObject.Find("CameraShake").transform;
		Transform mainCanvas = GameObject.Find("Canvas Container Main").transform;
		this.canvas = mainCanvas.FindRecursive("Canvas - UI/General").GetComponent<Canvas>();
		this.transition_screen = mainCanvas.GetComponentInChildren<LoadingScreen>(true);
		this.console = mainCanvas.GetComponentInChildren<Console>(true);
		this.nameplate_manager = mainCanvas.GetComponentInChildren<NameplateManager>(true);
		this.tutorial = mainCanvas.GetComponentInChildren<Tutorial>(true);
		this.selected_buttons = mainCanvas.GetComponentInChildren<PowerButtonSelector>();
		MapBox.cursor_speed = new CursorSpeed();
		this._signal_manager = new SignalManager();
		this.joys = GameObject.Find("Joys");
		this.joys.gameObject.SetActive(false);
		this.magnet = new Magnet();
		this.islands_calculator = new IslandsCalculator();
		this.sim_object_zones = new SimObjectsZones();
		this._world_log = new WorldLog();
		this.quality_changer = base.GetComponent<QualityChanger>();
		this.transform_units = base.transform.FindRecursive("Units");
		this.stack_effects.create();
		this.tiles_dirty = new HashSet<WorldTile>();
		this.tiles_list = new WorldTile[0];
		this.tile_manager = new TileManager();
		this.drop_manager = new DropManager(base.transform.Find("Drops"));
		this._list_meta_main_managers.Add(this.subspecies = new SubspeciesManager());
		this._list_meta_main_managers.Add(this.families = new FamilyManager());
		this._list_meta_main_managers.Add(this.armies = new ArmyManager());
		this._list_meta_main_managers.Add(this.languages = new LanguageManager());
		this._list_meta_main_managers.Add(this.religions = new ReligionManager());
		this._list_meta_main_managers.Add(this.cities = new CityManager());
		this._list_meta_main_managers.Add(this.clans = new ClanManager());
		this._list_meta_main_managers.Add(this.alliances = new AllianceManager());
		this._list_meta_main_managers.Add(this.kingdoms = new KingdomManager());
		this._list_meta_main_managers.Add(this.kingdoms_wild = new WildKingdomsManager());
		this._list_meta_main_managers.Add(this.cultures = new CultureManager());
		this._list_meta_main_managers.Add(this.plots = new PlotManager());
		this._list_meta_main_managers.Add(this.wars = new WarManager());
		this._list_meta_main_managers.Add(this.items = new ItemManager());
		this._list_meta_other_managers.Add(this.books = new BookManager());
		this._list_meta_other_managers.Add(this.diplomacy = new DiplomacyManager());
		this._list_meta_other_managers.Add(this.projectiles = new ProjectileManager());
		this._list_meta_other_managers.Add(this.statuses = new StatusManager());
		this._list_sim_objects_managers.Add(this.units = new ActorManager());
		this._list_sim_objects_managers.Add(this.buildings = new BuildingManager());
		this.list_all_sim_managers.AddRange(this._list_sim_objects_managers);
		this.list_all_sim_managers.AddRange(this._list_meta_main_managers);
		this.list_all_sim_managers.AddRange(this._list_meta_other_managers);
		this.heat = new Heat();
		this.wind_direction = new Vector2(-0.1f, 0.2f);
		this.era_manager = new WorldAgeManager();
		this.delayed_actions_manager = new DelayedActionsManager();
		AssetManager.world_behaviours.createManagers();
		base.gameObject.AddOrGetComponent<MusicBox>();
		DOTween.SetTweensCapacity(1000, 100);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000054E8 File Offset: 0x000036E8
	private void Start()
	{
		Application.lowMemory += AutoSaveManager.OnLowMemory;
		Application.lowMemory += PlayerConfig.turnOffAssetsPreloading;
		PlayerConfig.instance.start();
		this.explosion_checker = new ExplosionChecker();
		this.camera = Camera.main;
		this.move_camera = this.camera.GetComponent<MoveCamera>();
		this._initiated = true;
		this._map_layers = new List<MapLayer>();
		this._map_modules = new List<BaseModule>();
		this._map_layers.Add(this.world_layer = base.GetComponentInChildren<WorldLayer>());
		this._map_layers.Add(this.world_layer_edges = base.GetComponentInChildren<WorldLayerEdges>());
		this._world_layer_switch_effect = base.gameObject.transform.Find("world_layer_back").GetComponent<SpriteRenderer>();
		this._map_layers.Add(this.unit_layer = base.GetComponentInChildren<UnitLayer>());
		this._map_layers.Add(this.zone_calculator = base.GetComponentInChildren<ZoneCalculator>());
		this._map_layers.Add(this.burned_layer = base.GetComponentInChildren<BurnedTilesLayer>());
		this._map_layers.Add(this.explosion_layer = base.GetComponentInChildren<ExplosionsEffects>());
		this._map_layers.Add(this.conway_layer = base.GetComponentInChildren<ConwayLife>());
		this._map_layers.Add(this.fire_layer = base.GetComponentInChildren<FireLayer>());
		this._map_layers.Add(this._lava_layer = base.GetComponentInChildren<LavaLayer>());
		this._map_layers.Add(this._debug_layer = base.GetComponentInChildren<DebugLayer>());
		this._map_layers.Add(base.GetComponentInChildren<DebugLayerCursor>());
		this._map_layers.Add(this.path_finding_visualiser = base.GetComponentInChildren<PathFindingVisualiser>());
		this._map_layers.Add(this.flash_effects = base.GetComponentInChildren<PixelFlashEffects>());
		this._map_modules.Add(this.roads_calculator = base.GetComponentInChildren<RoadsCalculator>());
		this._map_modules.Add(this.grey_goo_layer = base.GetComponentInChildren<GreyGooLayer>());
		this.map_chunk_manager = new MapChunkManager();
		this.zone_camera = new ZoneCamera();
		if (Config.isComputer || Config.isEditor)
		{
			GameObject tPrefab = (GameObject)Resources.Load("effects/PrefabUnitSelectionEffect");
			this._unit_select_effect = Object.Instantiate<GameObject>(tPrefab, base.gameObject.transform).GetComponent<UnitSelectionEffect>();
			this._unit_select_effect.create();
		}
		this.addNewSystem(this._debug_text_group_system = new GameObject().AddComponent<DebugTextGroupSystem>());
		foreach (SpriteGroupSystem<GroupSpriteObject> spriteGroupSystem in this._list_systems)
		{
			spriteGroupSystem.create();
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000057D4 File Offset: 0x000039D4
	private void addNewSystem(SpriteGroupSystem<GroupSpriteObject> pSystem)
	{
		this._list_systems.Add(pSystem);
		pSystem.transform.parent = base.transform;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000057F3 File Offset: 0x000039F3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isGameplayControlsLocked()
	{
		return ScrollWindow.isWindowActive() || ScrollWindow.isAnimationActive() || RewardedAds.isShowing();
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000580A File Offset: 0x00003A0A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isWindowOnScreen()
	{
		return ScrollWindow.isWindowActive() || ScrollWindow.isAnimationActive();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000581C File Offset: 0x00003A1C
	internal bool calcPath(WorldTile pFrom, WorldTile pTargetTile, List<WorldTile> pSavePath)
	{
		pSavePath.Clear();
		StaticGrid tGrid = this._search_grid_ground;
		HeuristicMode tHeuristicMode = HeuristicMode.MANHATTAN;
		float tWeight = 2f;
		DiagonalMovement tDiagonalMovement;
		if (this.pathfinding_param.ocean)
		{
			tDiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
		}
		else
		{
			if (this.pathfinding_param.limit)
			{
			}
			tDiagonalMovement = DiagonalMovement.Always;
		}
		int tMaxOpenList = -1;
		if (this.pathfinding_param.roads)
		{
			tWeight = 1f;
			tDiagonalMovement = DiagonalMovement.Never;
			tHeuristicMode = HeuristicMode.EUCLIDEAN;
		}
		tGrid.Reset();
		if (!pFrom.isSameIsland(pTargetTile) && !this.pathfinding_param.ocean)
		{
			pSavePath.Add(pFrom);
			pSavePath.Add(pTargetTile);
			this.path_finding_visualiser.showPath(tGrid, pSavePath);
			return true;
		}
		GridPos tStartPos = new GridPos(pFrom.pos.x, pFrom.pos.y);
		GridPos tEndPos = new GridPos(pTargetTile.pos.x, pTargetTile.pos.y);
		this.pathfinding_param.setGrid(tGrid, tStartPos, tEndPos);
		this.pathfinding_param.DiagonalMovement = tDiagonalMovement;
		this.pathfinding_param.SetHeuristic(tHeuristicMode);
		this.pathfinding_param.max_open_list = tMaxOpenList;
		this.pathfinding_param.weight = tWeight;
		AStarFinder.FindPath(this.pathfinding_param, pSavePath);
		this.path_finding_visualiser.showPath(tGrid, pSavePath);
		return pSavePath.Count != 0;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00005970 File Offset: 0x00003B70
	public void startTheGame(bool pForceGenerate = false)
	{
		LogText.log("MapBox", "startTheGame", "st");
		Randy.fullReset();
		Config.game_loaded = true;
		Config.current_brush = "circ_5";
		if (Config.isMobile)
		{
			PlayInterstitialAd.setActive(true);
		}
		Config.LOAD_TIME_CREATE = Time.realtimeSinceStartup;
		if (pForceGenerate || Config.load_new_map)
		{
			this.generateNewMap();
		}
		else if (Config.load_random_test_map)
		{
			TestMaps.loadNextMap();
		}
		else if (Config.load_dragon)
		{
			SaveManager.loadMapFromResources("mapTemplates/map_dragon");
		}
		else if (Config.load_save_on_start)
		{
			this._first_gen = false;
			string tPath = SaveManager.getSlotSavePath(Config.load_save_on_start_slot);
			this.save_manager.loadWorld(tPath, false);
		}
		else if (Config.load_save_from_path)
		{
			SaveManager.loadMapFromResources(Config.load_test_save_path);
		}
		else if (Config.load_test_map)
		{
			DebugMap.makeDebugMap();
		}
		else
		{
			string day = "";
			try
			{
				day = DateTime.Now.ToString("MM/dd");
			}
			catch (Exception)
			{
				day = "";
			}
			if (day == "04/01")
			{
				SaveManager.loadMapFromResources("mapTemplates/map_april_fools");
			}
			else if (FavoriteWorld.hasFavoriteWorldSet(true))
			{
				int favorite_world = PlayerConfig.instance.data.favorite_world;
				FavoriteWorld.cacheSaveSlotID(favorite_world);
				FavoriteWorld.clearFavoriteWorld();
				this._first_gen = false;
				string tPath2 = SaveManager.getSlotSavePath(favorite_world);
				this.save_manager.loadWorld(tPath2, false);
			}
			else if (this.game_stats.data.gameLaunches <= 3L)
			{
				SaveManager.loadMapFromResources("mapTemplates/map_dragon");
			}
			else
			{
				this.generateNewMap();
				SmoothLoader.add(delegate
				{
					this.buildings.addBuilding("volcano", this.GetTile(MapBox.width / 2, MapBox.height / 2), false, false, BuildPlacingType.New);
				}, "add_volcano", false, 0.001f, false);
				SmoothLoader.add(delegate
				{
					WorldTile topLeft = this.GetTile(0, MapBox.height - 1);
					WorldTile topRight = this.GetTile(MapBox.width - 1, MapBox.height - 1);
					WorldTile bottomLeft = this.GetTile(0, 0);
					WorldTile bottomRight = this.GetTile(MapBox.width - 1, 0);
					this.units.spawnNewUnit("angle", topLeft, false, true, 6f, null, false, false).setName("DAB", false);
					this.units.spawnNewUnit("angle", topRight, false, true, 6f, null, false, false).setName("ABC", false);
					this.units.spawnNewUnit("angle", bottomLeft, false, true, 6f, null, false, false).setName("CDA", false);
					this.units.spawnNewUnit("angle", bottomRight, false, true, 6f, null, false, false).setName("BCD", false);
				}, "spawn_angles", false, 0.001f, false);
			}
		}
		SmoothLoader.add(new MapLoaderAction(this.addLastStep), "Prepare Game Launch", false, 0.001f, false);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005B58 File Offset: 0x00003D58
	private void addLastStep()
	{
		SmoothLoader.add(delegate
		{
			Config.LOAD_TIME_GENERATE = Time.realtimeSinceStartup;
			base.GetComponent<SpriteRenderer>().enabled = true;
			this.nameplate_manager.gameObject.SetActive(true);
			FavoriteWorld.restoreCachedFavoriteWorldOnSuccess();
			if (!Config.disable_startup_window)
			{
				if (PlayerConfig.instance.data.tutorialFinished || Config.disable_tutorial)
				{
					ScrollWindow.get("welcome").forceShow();
				}
				else
				{
					this.tutorial.startTutorial();
				}
			}
			PremiumElementsChecker.checkElements();
			MonoBehaviour.print("LOAD TIME INIT: " + Config.LOAD_TIME_INIT.ToString());
			MonoBehaviour.print("LOAD TIME CREATE: " + (Config.LOAD_TIME_CREATE - Config.LOAD_TIME_INIT).ToString());
			MonoBehaviour.print("LOAD TIME GENERATE: " + (Config.LOAD_TIME_GENERATE - Config.LOAD_TIME_CREATE).ToString());
			LogText.log("MapBox", "startTheGame", "en");
		}, "Start the Game", false, 0.001f, true);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005B77 File Offset: 0x00003D77
	private void afterLoadEvent()
	{
		Debug.Log("afterLoadEvent--------------------------");
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005B88 File Offset: 0x00003D88
	internal void centerCamera()
	{
		Vector3 tVec = this.camera.transform.position;
		tVec.x = (float)(MapBox.width / 2);
		tVec.y = (float)(MapBox.height / 2);
		this.camera.transform.position = tVec;
		this.move_camera.resetZoom();
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00005BE0 File Offset: 0x00003DE0
	private void resetTiles()
	{
		StaticGrid search_grid_ground = this._search_grid_ground;
		if (search_grid_ground != null)
		{
			search_grid_ground.Reset();
		}
		WorldTile[] array = this.tiles_list;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].clear();
		}
		this.tiles_dirty.Clear();
		this.tilemap.clear();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00005C34 File Offset: 0x00003E34
	private void clearTiles()
	{
		StaticGrid search_grid_ground = this._search_grid_ground;
		if (search_grid_ground != null)
		{
			search_grid_ground.Dispose();
		}
		this._search_grid_ground = null;
		this.zone_calculator.clean();
		this.map_chunk_manager.clean();
		WorldTile[] array = this.tiles_list;
		for (int k = 0; k < array.Length; k++)
		{
			array[k].Dispose();
		}
		this.tiles_list = new WorldTile[0];
		for (int i = 0; i < MapBox.width; i++)
		{
			for (int j = 0; j < MapBox.height; j++)
			{
				this.tiles_map[i, j] = null;
			}
		}
		this.tiles_map = null;
		this.tiles_dirty.Clear();
		this.tilemap.clear();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00005CE4 File Offset: 0x00003EE4
	private void createTiles()
	{
		SmoothLoader.add(delegate
		{
			this.tiles_list = new WorldTile[MapBox.width * MapBox.height];
			this.tiles_map = new WorldTile[MapBox.width, MapBox.height];
			GeneratorTool.Setup(this.tiles_map);
		}, "Prepare Tiles", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			MapBox._tile_id = 0;
			for (int y = 0; y < MapBox.height; y++)
			{
				for (int x = 0; x < MapBox.width; x++)
				{
					WorldTile tTile = new WorldTile(x, y, MapBox._tile_id);
					this._search_grid_ground.SetTileNode(x, y, tTile);
					this.tiles_map[x, y] = tTile;
					this.tiles_list[MapBox._tile_id] = tTile;
					MapBox._tile_id++;
				}
			}
		}, "Create Tiles (" + (MapBox.height * MapBox.width).ToString() + ")", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			int num = this.tiles_list.Length;
		}, "Create Neighbours [" + (MapBox.height * MapBox.width).ToString() + "] (1/3)", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			int tCount = this.tiles_list.Length;
			for (int i = 0; i < tCount; i++)
			{
				this.tiles_list[i].resetNeighbourLists();
			}
		}, "Create Neighbours [" + (MapBox.height * MapBox.width).ToString() + "] (2/3)", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			GeneratorTool.GenerateTileNeighbours(this.tiles_list);
		}, "Create Neighbours [" + (MapBox.height * MapBox.width).ToString() + "] (3/3)", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.zone_calculator.generate();
			WorldBehaviourActionFire.prepare();
		}, "Create Zones", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.map_chunk_manager.prepare();
		}, "Create Chunks", true, 0.001f, false);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005E30 File Offset: 0x00004030
	public static AttackDataResult newAttack(AttackData pData)
	{
		if (pData.hit_tile == null)
		{
			return new AttackDataResult(ApplyAttackState.Continue, -1L);
		}
		int tTargets = pData.targets;
		AttackDataResult tAttackResult = new AttackDataResult(ApplyAttackState.Continue, -1L);
		if (pData.target != null)
		{
			tAttackResult = MapBox.checkAttackFor(pData, pData.target);
			ApplyAttackState state = tAttackResult.state;
			if (state != ApplyAttackState.Hit)
			{
				if (state - ApplyAttackState.Block <= 1)
				{
					return tAttackResult;
				}
			}
			else
			{
				tTargets--;
			}
			if (tTargets == 0)
			{
				return tAttackResult;
			}
		}
		if (tTargets == 0)
		{
			return tAttackResult;
		}
		List<BaseSimObject> tList = EnemiesFinder.findEnemiesFrom(pData.hit_tile, pData.kingdom, 0).list;
		if (tList == null)
		{
			return tAttackResult;
		}
		foreach (BaseSimObject tObject in tList.LoopRandom<BaseSimObject>())
		{
			if (tObject != pData.target)
			{
				if (tTargets == 0)
				{
					break;
				}
				tAttackResult = MapBox.checkAttackFor(pData, tObject);
				ApplyAttackState state = tAttackResult.state;
				if (state != ApplyAttackState.Hit)
				{
					if (state - ApplyAttackState.Block <= 1)
					{
						return tAttackResult;
					}
				}
				else
				{
					tTargets--;
				}
			}
		}
		return tAttackResult;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00005F30 File Offset: 0x00004130
	public static AttackDataResult checkAttackFor(AttackData pData, BaseSimObject pTargetToCheck)
	{
		if (pTargetToCheck.isRekt())
		{
			return AttackDataResult.Continue;
		}
		if (pData.initiator.isRekt())
		{
			return AttackDataResult.Continue;
		}
		if (pTargetToCheck == pData.initiator)
		{
			return AttackDataResult.Continue;
		}
		if (!pData.initiator.canAttackTarget(pTargetToCheck, true, true))
		{
			return AttackDataResult.Continue;
		}
		if (pTargetToCheck.isActor() && pTargetToCheck.hasStatus("dodge"))
		{
			return AttackDataResult.Continue;
		}
		Vector3 tTargetCurrentPosition = pTargetToCheck.current_position;
		float num = Toolbox.SquaredDist(tTargetCurrentPosition.x, tTargetCurrentPosition.y + pTargetToCheck.getHeight(), pData.hit_position.x, pData.hit_position.y + pData.hit_position.z);
		float tRange = pData.area_of_effect + pTargetToCheck.stats["size"];
		tRange *= tRange;
		if (num < tRange)
		{
			Vector3.MoveTowards(pData.hit_position, tTargetCurrentPosition, pTargetToCheck.stats["size"] * 0.9f).y += pTargetToCheck.getHeight();
			AttackDataResult attackDataResult = MapBox.applyAttack(pData, pTargetToCheck);
			if (attackDataResult.state == ApplyAttackState.Hit)
			{
				Vector3 tEffectPos = pData.hit_position;
				tEffectPos.y += tEffectPos.z;
				if (pData.critical)
				{
					EffectsLibrary.spawnAt("fx_hit_critical", tEffectPos, 0.1f);
					return attackDataResult;
				}
				EffectsLibrary.spawnAt("fx_hit", tEffectPos, 0.1f);
			}
			return attackDataResult;
		}
		return AttackDataResult.Miss;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00006094 File Offset: 0x00004294
	private static AttackDataResult applyAttack(AttackData pData, BaseSimObject pTargetToCheck)
	{
		bool tIsTargetActor = pTargetToCheck.isActor();
		Actor tTargetActor = pTargetToCheck.a;
		ProjectileAsset tProjectileAsset = null;
		if (pData.is_projectile)
		{
			tProjectileAsset = AssetManager.projectiles.get(pData.projectile_id);
		}
		if (tIsTargetActor && ControllableUnit.isControllingUnit(tTargetActor) && tTargetActor.hasMeleeAttack() && tTargetActor.isJustAttacked())
		{
			CombatActionLibrary.combat_action_deflect.action_actor(pTargetToCheck.a, pData, 0f, 0f);
			return new AttackDataResult(ApplyAttackState.Deflect, pTargetToCheck.a.data.id);
		}
		CombatActionAsset tCombatAsset;
		if (tIsTargetActor && tTargetActor.tryToUseAdvancedCombatAction(tTargetActor.getCombatActionPool(CombatActionPool.BEFORE_HIT_DEFLECT), null, out tCombatAsset))
		{
			tCombatAsset.action_actor(pTargetToCheck.a, pData, 0f, 0f);
			return new AttackDataResult(ApplyAttackState.Deflect, pTargetToCheck.a.data.id);
		}
		bool tCanBeBlocked = false;
		if (tProjectileAsset != null && tProjectileAsset.can_be_blocked)
		{
			tCanBeBlocked = true;
		}
		if (tCanBeBlocked && tIsTargetActor && tTargetActor.tryToUseAdvancedCombatAction(tTargetActor.getCombatActionPool(CombatActionPool.BEFORE_HIT_BLOCK), null, out tCombatAsset))
		{
			tCombatAsset.action_actor(pTargetToCheck.a, pData, 0f, 0f);
			return AttackDataResult.Block;
		}
		if (tIsTargetActor && tTargetActor.tryToUseAdvancedCombatAction(tTargetActor.getCombatActionPool(CombatActionPool.BEFORE_HIT), null, out tCombatAsset))
		{
			tCombatAsset.action_actor(tTargetActor, pData, 0f, 0f);
			return AttackDataResult.Continue;
		}
		int tDamage = (int)Randy.randomFloat(pData.damage_range * (float)pData.damage, (float)pData.damage);
		if (pData.critical)
		{
			tDamage *= pData.critical_damage_multiplier;
		}
		if (pData.initiator.isActor() && pTargetToCheck.isAlive())
		{
			pData.initiator.a.addExperience(2);
		}
		float pDamage = (float)tDamage;
		bool pFlash = true;
		AttackType attack_type = pData.attack_type;
		BaseSimObject initiator = pData.initiator;
		bool metallic_weapon = pData.metallic_weapon;
		pTargetToCheck.getHit(pDamage, pFlash, attack_type, initiator, pData.skip_shake, metallic_weapon, true);
		if (!pTargetToCheck.hasHealth())
		{
			ActorTool.applyForceToUnit(pData, pTargetToCheck, 1f, false);
		}
		else
		{
			ActorTool.applyForceToUnit(pData, pTargetToCheck, 0.5f, true);
		}
		if (pData.initiator.isActor())
		{
			pData.initiator.a.attackTargetActions(pTargetToCheck, pData.hit_tile);
		}
		if (tIsTargetActor && pData.initiator.isActor() && !pTargetToCheck.hasHealth() && pData.initiator.a.needsFood() && pData.initiator.a.subspecies.diet_meat && tTargetActor.asset.source_meat)
		{
			pData.initiator.a.addNutritionFromEating(70, true, false);
			pData.initiator.a.countConsumed();
		}
		return AttackDataResult.Hit;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00006322 File Offset: 0x00004522
	public void clearArchitectMood()
	{
		this._cached_architect_mood = null;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000632C File Offset: 0x0000452C
	public void clearWorld()
	{
		this.clearArchitectMood();
		MapBox.current_world_seed_id++;
		DBInserter.Lock();
		this.tile_manager.clear();
		CursedSacrifice.reset();
		LogText.log("MapBox", "clearWorld", "st");
		AutoTesterBot autoTesterBot = this.auto_tester;
		if (autoTesterBot != null)
		{
			autoTesterBot.clearWorld();
		}
		Analytics.worldLoading();
		SelectedUnit.clear();
		ControllableUnit.clear(true);
		DBManager.clearAndClose();
		this.explosion_checker.clear();
		BattleKeeperManager.clear();
		ZoneMetaDataVisualizer.clearAll();
		Finder.clear();
		this._debug_layer.clear();
		this.selected_buttons.unselectAll();
		this.player_control.clear();
		MusicBox.clearAllSounds();
		this.clearFrameCaches();
		EnemiesFinder.disposeAll();
		TaxiManager.clear();
		this.islands_calculator.clear();
		RegionLinkHashes.clear();
		this.nameplate_manager.clearAll();
		this.map_chunk_manager.clearAll();
		this.islands_calculator.clear();
		this.quality_changer.reset();
		this.tilemap.clear();
		this.zone_camera.clear();
		Config.paused = false;
		if (DebugConfig.isOn(DebugOption.PauseOnStart))
		{
			Config.paused = true;
		}
		this.selected_buttons.checkToggleIcons();
		this.heat.clear();
		this.era_manager.clear();
		this.delayed_actions_manager.clear();
		foreach (TileType tileType in AssetManager.tiles.list)
		{
			tileType.hashsetClear();
		}
		foreach (TopTileType topTileType in AssetManager.top_tiles.list)
		{
			topTileType.hashsetClear();
		}
		foreach (WorldBehaviourAsset worldBehaviourAsset in AssetManager.world_behaviours.list)
		{
			worldBehaviourAsset.manager.clear();
		}
		WildKingdomsManager.neutral.clearListCities();
		AutoSaveManager.resetAutoSaveTimer();
		AssetManager.actor_library.clear();
		AssetManager.buildings.clear();
		BehaviourActionActor.clear();
		this.city_zone_helper.clear();
		this.region_path_finder.clear();
		Toolbox.clearAll();
		this.drop_manager.clear();
		this.armies.clear();
		foreach (BaseSystemManager baseSystemManager in this.list_all_sim_managers)
		{
			baseSystemManager.clear();
		}
		this.particles_fire.clear();
		this.particles_smoke.clear();
		this.stack_effects.clear();
		TornadoEffect.Clear();
		this.resource_throw_manager.clear();
		foreach (MapLayer mapLayer in this._map_layers)
		{
			mapLayer.clear();
		}
		foreach (BaseModule baseModule in this._map_modules)
		{
			baseModule.clear();
		}
		this.sim_object_zones.fullClear();
		this.resetTiles();
		this.zone_camera.fullClear();
		this.world_layer_edges.clear();
		WorldBehaviourActionFire.clearFires();
		HistoryHud.instance.Clear();
		DBInserter.Unlock();
		DBManager.clearAndClose();
		LogText.log("MapBox", "clearWorld", "en");
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000066EC File Offset: 0x000048EC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public WorldTile GetTile(int pX, int pY)
	{
		if (pX < 0 || pX >= MapBox.width || pY < 0 || pY >= MapBox.height)
		{
			return null;
		}
		return this.tiles_map[pX, pY];
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00006715 File Offset: 0x00004915
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public WorldTile GetTileSimple(int pX, int pY)
	{
		return this.tiles_map[pX, pY];
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00006724 File Offset: 0x00004924
	public void setMapSize(int pWidth, int pHeight)
	{
		Config.ZONE_AMOUNT_X = pWidth;
		Config.ZONE_AMOUNT_Y = pHeight;
		MapBox.width = Config.ZONE_AMOUNT_X * 64;
		MapBox.height = Config.ZONE_AMOUNT_Y * 64;
		int tTotalTiles = MapBox.width * MapBox.height;
		if (this.tiles_list.Length != tTotalTiles)
		{
			this.recreateSizes();
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00006774 File Offset: 0x00004974
	private void afterTransitionGeneration()
	{
		this.generateNewMap();
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000677C File Offset: 0x0000497C
	public void clickGenerateNewMap()
	{
		this.transition_screen.startTransition(new LoadingScreen.TransitionAction(this.afterTransitionGeneration));
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006798 File Offset: 0x00004998
	public void generateNewMap()
	{
		if (!this._initiated)
		{
			return;
		}
		if (Config.show_console_on_start)
		{
			this.console.Toggle();
		}
		SmoothLoader.prepare();
		SmoothLoader.add(delegate
		{
			LogText.log("MapBox", "generateNewMap", "st");
			Analytics.worldLoading();
			if (this._first_gen)
			{
				Config.customMapSize = Config.customMapSizeDefault;
			}
			if (!this._first_gen)
			{
				AchievementLibrary.custom_world.check(null);
			}
			this._first_gen = false;
			int tSize = MapSizeLibrary.getSize(Config.customMapSize);
			this.addClearWorld(tSize, tSize);
		}, "Generate New Map (1/3)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			Config.ZONE_AMOUNT_Y = (Config.ZONE_AMOUNT_X = MapSizeLibrary.getSize(Config.customMapSize));
		}, "Generate New Map (2/3)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.setMapSize(Config.ZONE_AMOUNT_X, Config.ZONE_AMOUNT_Y);
		}, "Generate New Map (3/3)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			LogText.log("MapBox", "GenerateMap", "st");
			AssetManager.tiles.setListTo(DepthGeneratorType.Generator);
			this.world_laws = new WorldLaws();
			this.world_laws.init(true);
			this.hotkey_tabs_data = new HotkeyTabsData();
		}, "gen: World Laws", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.map_stats = new MapStats();
			this.map_stats.initNewWorld();
			Randy.resetSeed(Randy.randomInt(1, 555555555));
		}, "gen: Generating Name", false, 0.001f, false);
		if (!Config.disable_db)
		{
			SmoothLoader.add(delegate
			{
				DBManager.createDB();
			}, "Creating Stats DB", false, 0.001f, false);
			DBTables.createOrMigrateTablesLoader(true);
		}
		WindowPreloader.addWaitForWindowResources();
		SmoothLoader.add(delegate
		{
			this.era_manager.setDefaultAges();
		}, "gen: World Ages", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			MapGenerator.prepare();
			LogText.log("MapBox", "GenerateMap", "en");
		}, "Preparing Map", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.cleanUpWorld(true);
		}, "Cleaning Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.redrawTiles();
		}, "Drawing Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.preloadRenderedSprites();
		}, "Preload rendered sprites...", false, 0.2f, false);
		SmoothLoader.add(delegate
		{
			this.finishMakingWorld();
			LogText.log("MapBox", "generateNewMap", "en");
		}, "Tidying Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.lastGC();
		}, "Rewriting The World", true, 0.001f, false);
		this.addLoadAutoTester();
		this.addKillAllUnits();
		this.addLoadWorldCallbacks();
		SmoothLoader.add(delegate
		{
			this.finishingUpLoading();
		}, "Finishing up...", false, 0.2f, false);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000069B8 File Offset: 0x00004BB8
	public void finishingUpLoading()
	{
		CanvasMain.instance.setMainUiEnabled(true);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000069C8 File Offset: 0x00004BC8
	public void preloadRenderedSprites()
	{
		foreach (Actor actor in this.units)
		{
			actor.checkSpriteToRender();
		}
		foreach (Building building in this.buildings)
		{
			building.checkSpriteToRender();
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00006A50 File Offset: 0x00004C50
	public void addUnloadResources()
	{
		this._load_counter++;
		if (this._load_counter <= 5)
		{
			return;
		}
		this._load_counter = 0;
		SmoothLoader.add(delegate
		{
			Resources.UnloadUnusedAssets();
		}, "UnloadUnusedAssets", true, 0.001f, false);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00006AAC File Offset: 0x00004CAC
	public void addClearWorld(int pNextWidth, int pNextHeight)
	{
		SmoothLoader.add(delegate
		{
			LogText.log("MapBox", "clearWorld", "st");
			this.clearWorld();
			LogText.log("MapBox", "clearWorld", "en");
		}, "Clearing World", true, 0.001f, false);
		DebugMemory.addMemorySnapshot("clearWorld");
		int num = pNextWidth * 64;
		int tHeightInTiles = pNextHeight * 64;
		int tTotalTiles = num * tHeightInTiles;
		if (this.tiles_list.Length != tTotalTiles)
		{
			SmoothLoader.add(delegate
			{
				this.clearTiles();
			}, "Clean old Tiles", false, 0.001f, false);
			DebugMemory.addMemorySnapshot("clearTiles");
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00006B1E File Offset: 0x00004D1E
	public void addKillAllUnits()
	{
		if (DebugConfig.isOn(DebugOption.KillAllUnitsOnLoad))
		{
			SmoothLoader.add(delegate
			{
				foreach (Actor actor in this.units)
				{
					actor.dieAndDestroy(AttackType.None);
				}
			}, "Killing All Units", true, 0.001f, false);
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00006B46 File Offset: 0x00004D46
	public void addLoadAutoTester()
	{
		if (DebugConfig.isOn(DebugOption.TesterLibs))
		{
			SmoothLoader.add(delegate
			{
				if (!string.IsNullOrEmpty(Config.auto_test_on_start))
				{
					this.auto_tester.create(Config.auto_test_on_start);
					this.auto_tester.gameObject.SetActive(true);
				}
			}, "Loading Auto Tester", true, 0.001f, false);
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00006B71 File Offset: 0x00004D71
	public void addLoadWorldCallbacks()
	{
		SmoothLoader.add(delegate
		{
			Config.debug_worlds_loaded++;
			Action action = MapBox.on_world_loaded;
			if (action == null)
			{
				return;
			}
			action();
		}, "World Loaded", true, 0.001f, false);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00006BA3 File Offset: 0x00004DA3
	private void generateMap(string pType = "islands")
	{
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00006BA8 File Offset: 0x00004DA8
	public void cleanUpWorld(bool pSetChunksDirty = true)
	{
		MapGenerator.clear();
		this.updateDirtyMetaContainersAndCleanup();
		this.era_manager.prepare();
		if (pSetChunksDirty)
		{
			this.map_chunk_manager.allDirty();
			this.map_chunk_manager.update(0f, true);
		}
		foreach (City city in this.cities)
		{
			city.forceDoChecks();
		}
		foreach (City city2 in this.cities)
		{
			city2.executeAllActionsForCity();
		}
		this.allTilesDirty();
		this.centerCamera();
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006C6C File Offset: 0x00004E6C
	public void redrawTiles()
	{
		this._meta_skip = true;
		if (MusicBox.new_world_on_start_played)
		{
			MusicBox.reserveFlag("new_world", true);
		}
		this.tilemap.redrawTiles(true);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006C94 File Offset: 0x00004E94
	public void finishMakingWorld()
	{
		ToolbarButtons toolbarButtons = ToolbarButtons.instance;
		if (toolbarButtons != null)
		{
			toolbarButtons.resetBar();
		}
		this.game_stats.data.mapsCreated += 1L;
		AchievementLibrary.gen_5_worlds.check(null);
		AchievementLibrary.gen_50_worlds.check(null);
		AchievementLibrary.gen_100_worlds.check(null);
		Analytics.worldLoaded();
		Config.LAST_LOAD_TIME = Time.realtimeSinceStartup;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00006CFD File Offset: 0x00004EFD
	public void lastGC()
	{
		Config.forceGC("finish making world", false);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00006D0C File Offset: 0x00004F0C
	private void recreateSizes()
	{
		SmoothLoader.add(delegate
		{
			this._search_grid_ground = new StaticGrid(MapBox.width, MapBox.height, null);
		}, "Recreate Sizes (1/4)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.createTiles();
		}, "Recreate Sizes (2/4)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			this.tile_manager.setup(MapBox.width, MapBox.height, this.tiles_map);
		}, "Tile Manager", true, 0.001f, false);
		for (int i = 0; i < this._map_layers.Count; i++)
		{
			int j = i;
			SmoothLoader.add(delegate
			{
				this._map_layers[j].createTextureNew();
			}, string.Concat(new string[]
			{
				"Recreate Sizes (3/4) (",
				(i + 1).ToString(),
				"/",
				this._map_layers.Count.ToString(),
				")"
			}), false, 0.001f, false);
		}
		SmoothLoader.add(delegate
		{
			if (Globals.TRAILER_MODE)
			{
				Object.Destroy(this._map_border.gameObject);
				return;
			}
			this._map_border.generateTexture();
		}, "Recreate Sizes (4/4)", false, 0.001f, false);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00006E17 File Offset: 0x00005017
	public Actor getActorNearCursor()
	{
		return ActionLibrary.getActorNearPos(MapBox.instance.getMousePos());
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00006E28 File Offset: 0x00005028
	public WorldTile getMouseTilePosCachedFrame()
	{
		return this.player_control.getMouseTilePosCachedFrame();
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00006E35 File Offset: 0x00005035
	public Vector2 getMousePos()
	{
		return this.player_control.getMousePos();
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00006E42 File Offset: 0x00005042
	public WorldTile getMouseTilePos()
	{
		return this.player_control.getMouseTilePos();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00006E4F File Offset: 0x0000504F
	public bool isPointerInGame()
	{
		return this.player_control.isPointerInGame();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00006E5C File Offset: 0x0000505C
	public bool isPointerInsideMapBounds()
	{
		return this.getMouseTilePos() != null;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00006E67 File Offset: 0x00005067
	public bool isOverUI()
	{
		return this.player_control.isOverUI(true);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006E75 File Offset: 0x00005075
	public bool isTouchOverUI(Touch pTouch)
	{
		return this.player_control.isTouchOverUI(pTouch);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00006E83 File Offset: 0x00005083
	public static bool controlsLocked()
	{
		return PlayerControl.controlsLocked();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006E8A File Offset: 0x0000508A
	public static bool isControllingUnit()
	{
		return PlayerControl.isControllingUnit();
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00006E91 File Offset: 0x00005091
	public bool isBusyWithUI()
	{
		return this.player_control.isBusyWithUI();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006E9E File Offset: 0x0000509E
	public bool isActionHappening()
	{
		return this.player_control.isActionHappening();
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00006EAB File Offset: 0x000050AB
	public bool isOverUiButton()
	{
		PlayerControl playerControl = this.player_control;
		return playerControl != null && playerControl.isPointerOverUIButton();
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00006EC0 File Offset: 0x000050C0
	public void loopWithBrush(WorldTile pCenterTile, BrushData pBrush, PowerAction pAction, GodPower pPower = null)
	{
		BrushPixelData[] tPos = pBrush.pos;
		int tLen = tPos.Length;
		for (int i = 0; i < tLen; i++)
		{
			BrushPixelData tPixelData = tPos[i];
			int tX = pCenterTile.x + tPixelData.x;
			int tY = pCenterTile.y + tPixelData.y;
			if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
			{
				WorldTile tTile = MapBox.instance.GetTileSimple(tX, tY);
				pAction(tTile, pPower);
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00006F41 File Offset: 0x00005141
	public void highlightTilesBrush(WorldTile pCenterTile, BrushData pBrush, PowerAction pAction, GodPower pPower = null)
	{
		this.loopWithBrush(pCenterTile, pBrush, pAction, pPower);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00006F4E File Offset: 0x0000514E
	public void loopWithBrushPowerForDropsFull(WorldTile pCenterTile, BrushData pBrush, PowerAction pAction, GodPower pPower = null)
	{
		this.loopWithBrush(pCenterTile, pBrush, pAction, pPower);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00006F5C File Offset: 0x0000515C
	public void loopWithBrushPowerForDropsRandom(WorldTile pCenterTile, BrushData pBrush, PowerAction pAction, GodPower pPower = null)
	{
		BrushPixelData[] tPos = pBrush.pos;
		int tLen = tPos.Length;
		using (ListPool<WorldTile> tListPool = new ListPool<WorldTile>())
		{
			for (int i = 0; i < tLen; i++)
			{
				BrushPixelData tPixelData = tPos[i];
				int tX = pCenterTile.x + tPixelData.x;
				int tY = pCenterTile.y + tPixelData.y;
				if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
				{
					WorldTile tTile = MapBox.instance.GetTileSimple(tX, tY);
					tListPool.Add(tTile);
				}
			}
			int tTotalDrops = pBrush.drops;
			tListPool.Shuffle<WorldTile>();
			for (int j = 0; j < tTotalDrops; j++)
			{
				if (tListPool.Count == 0)
				{
					break;
				}
				WorldTile tTile2 = tListPool.Pop<WorldTile>();
				pAction(tTile2, pPower);
			}
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00007040 File Offset: 0x00005240
	public void loopWithBrush(WorldTile pCenterTile, BrushData pBrush, PowerActionWithID pAction, string pPowerID = null)
	{
		BrushPixelData[] tPos = pBrush.pos;
		int tLen = tPos.Length;
		for (int i = 0; i < tLen; i++)
		{
			BrushPixelData tPixelData = tPos[i];
			int tX = pCenterTile.x + tPixelData.x;
			int tY = pCenterTile.y + tPixelData.y;
			if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
			{
				WorldTile tTile = MapBox.instance.GetTileSimple(tX, tY);
				pAction(tTile, pPowerID);
			}
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000070C4 File Offset: 0x000052C4
	public void checkCityZone(WorldTile pTile)
	{
		if (pTile.zone.city == null)
		{
			return;
		}
		bool tBuildingsFound = false;
		HashSet<Building> tSet = pTile.zone.getHashset(BuildingList.Civs);
		if (tSet != null)
		{
			using (HashSet<Building>.Enumerator enumerator = tSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.city == pTile.zone.city)
					{
						tBuildingsFound = true;
						break;
					}
				}
			}
		}
		if (!tBuildingsFound)
		{
			pTile.zone.city.removeZone(pTile.zone);
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000715C File Offset: 0x0000535C
	public static void spawnLightningBig(WorldTile pTile, float pScale = 0.25f, Actor pActor = null)
	{
		BaseEffect tEffect = EffectsLibrary.spawnAtTile("fx_lightning_big", pTile, pScale);
		if (tEffect == null)
		{
			return;
		}
		int tRadius = (int)(pScale * 25f);
		MapAction.checkLightningAction(pTile.pos, tRadius, pActor, true, true);
		MapAction.damageWorld(pTile, tRadius, AssetManager.terraform.get("lightning_power"), pActor);
		tEffect.sprite_renderer.flipX = Randy.randomBool();
		MapAction.checkSantaHit(pTile.pos, tRadius);
		MapAction.checkUFOHit(pTile.pos, tRadius, pActor);
		MapAction.checkTornadoHit(pTile.pos, tRadius);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000071E4 File Offset: 0x000053E4
	public static void spawnLightningMedium(WorldTile pTile, float pScale = 0.25f, Actor pActor = null)
	{
		BaseEffect tEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, pScale);
		if (tEffect == null)
		{
			return;
		}
		int tRadius = (int)(pScale * 15f);
		MapAction.checkLightningAction(pTile.pos, tRadius, pActor, false, false);
		MapAction.damageWorld(pTile, tRadius, AssetManager.terraform.get("lightning_normal"), pActor);
		tEffect.sprite_renderer.flipX = Randy.randomBool();
		MapAction.checkTornadoHit(pTile.pos, tRadius);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00007254 File Offset: 0x00005454
	public static void spawnLightningSmall(WorldTile pTile, float pScale = 0.25f, Actor pActor = null)
	{
		BaseEffect tEffect = EffectsLibrary.spawnAtTile("fx_lightning_small", pTile, pScale);
		if (tEffect == null)
		{
			return;
		}
		int tRadius = (int)(pScale * 10f);
		MapAction.checkLightningAction(pTile.pos, tRadius, pActor, false, false);
		MapAction.damageWorld(pTile, tRadius, AssetManager.terraform.get("lightning_normal"), pActor);
		tEffect.sprite_renderer.flipX = Randy.randomBool();
		MapAction.checkTornadoHit(pTile.pos, tRadius);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000072C4 File Offset: 0x000054C4
	public void applyForceOnTile(WorldTile pTile, int pRad = 10, float pForceAmount = 1.5f, bool pForceOut = true, int pDamage = 0, string[] pIgnoreKingdoms = null, BaseSimObject pByWho = null, TerraformOptions pOptions = null, bool pChangeHappiness = false)
	{
		int tRad = pRad * pRad;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 0f, false))
		{
			if (tActor != ((pByWho != null) ? pByWho.a : null) && (float)Toolbox.SquaredDistTile(tActor.current_tile, pTile) <= (float)tRad && (pOptions == null || !tActor.asset.very_high_flyer || pOptions.applies_to_high_flyers))
			{
				if (pIgnoreKingdoms != null)
				{
					bool continueNext = false;
					for (int i = 0; i < pIgnoreKingdoms.Length; i++)
					{
						Kingdom tKingdom = this.kingdoms_wild.get(pIgnoreKingdoms[i]);
						if (tActor.kingdom == tKingdom)
						{
							continueNext = true;
							break;
						}
					}
					if (continueNext)
					{
						continue;
					}
				}
				tActor.makeStunned(4f);
				if (pChangeHappiness)
				{
					tActor.changeHappiness("just_forced_power", 0);
				}
				if (tActor.asset.can_be_hurt_by_powers && pDamage > 0)
				{
					AttackType tAttackType = AttackType.Other;
					if (pOptions != null)
					{
						tAttackType = pOptions.attack_type;
					}
					tActor.getHit((float)pDamage, true, tAttackType, pByWho, true, false, true);
				}
				if (pForceAmount > 0f)
				{
					if (pForceOut)
					{
						tActor.calculateForce((float)tActor.current_tile.x, (float)tActor.current_tile.y, (float)pTile.x, (float)pTile.y, pForceAmount, 0f, true);
					}
					else
					{
						tActor.calculateForce((float)pTile.x, (float)pTile.y, (float)tActor.current_tile.x, (float)tActor.current_tile.y, pForceAmount, 0f, true);
					}
				}
			}
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007470 File Offset: 0x00005670
	internal void stopAttacksFor(bool pMonsters)
	{
		foreach (Actor tActor in this.units)
		{
			if (tActor.has_attack_target && tActor.isEnemyTargetAlive() && (tActor.kingdom.asset.mobs || tActor.attack_target.kingdom.asset.mobs) == pMonsters)
			{
				tActor.cancelAllBeh();
			}
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000074FC File Offset: 0x000056FC
	public void allDirty()
	{
		for (int i = 0; i < this.tiles_list.Length; i++)
		{
			WorldTile tTile = this.tiles_list[i];
			this.tiles_dirty.Add(tTile);
			this.tilemap.addToQueueToRedraw(tTile);
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000753E File Offset: 0x0000573E
	private void OnApplicationFocus(bool pFocus)
	{
		this.has_focus = pFocus;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00007547 File Offset: 0x00005747
	private void OnApplicationPause(bool pPause)
	{
		this.has_focus = !pPause;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00007553 File Offset: 0x00005753
	private void OnApplicationQuit()
	{
		DOTween.KillAll(false);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000755C File Offset: 0x0000575C
	private void updateShake(float pElapsed)
	{
		if (this._shake_timer == 0f)
		{
			return;
		}
		if (this._shake_timer > 0f)
		{
			this._shake_timer -= pElapsed;
		}
		if (this._shake_timer <= 0f)
		{
			this._shake_timer = 0f;
			this._shake_camera.position = new Vector3(0f, 0f);
			return;
		}
		if (this._shake_interval_timer > 0f)
		{
			this._shake_interval_timer -= pElapsed;
			return;
		}
		this._shake_interval_timer = this._shake_interval;
		Vector3 tVec = default(Vector3);
		if (this._shake_x)
		{
			tVec.x = Randy.randomFloat(-this._shake_intensity, this._shake_intensity);
		}
		if (this._shake_y)
		{
			tVec.y = Randy.randomFloat(-this._shake_intensity, this._shake_intensity);
		}
		this._shake_camera.position = tVec;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00007643 File Offset: 0x00005843
	public void startShake(float pDuration = 0.3f, float pInterval = 0.01f, float pIntensity = 2f, bool pShakeX = false, bool pShakeY = true)
	{
		this._shake_timer = pDuration;
		this._shake_interval = pInterval;
		this._shake_intensity = pIntensity;
		this._shake_x = pShakeX;
		this._shake_y = pShakeY;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000766C File Offset: 0x0000586C
	private void updateMapLayers(float pElapsed)
	{
		Bench.bench("heat", "game_total", false);
		this.heat.update(pElapsed);
		Bench.benchEnd("heat", "game_total", false, 0L, false);
		Bench.bench("map_chunk_manager", "game_total", false);
		this.map_chunk_manager.update(pElapsed, false);
		Bench.benchEnd("map_chunk_manager", "game_total", false, 0L, false);
		Bench.bench("map_layers", "game_total", false);
		for (int i = 0; i < this._map_layers.Count; i++)
		{
			this._map_layers[i].update(pElapsed);
		}
		Bench.benchEnd("map_layers", "game_total", false, 0L, false);
		Bench.bench("map_layers_draw", "game_total", false);
		for (int j = 0; j < this._map_layers.Count; j++)
		{
			this._map_layers[j].draw(pElapsed);
		}
		Bench.benchEnd("map_layers_draw", "game_total", false, 0L, false);
		Bench.bench("map_modules", "game_total", false);
		for (int k = 0; k < this._map_modules.Count; k++)
		{
			this._map_modules[k].update(pElapsed);
		}
		Bench.benchEnd("map_modules", "game_total", false, 0L, false);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000077C3 File Offset: 0x000059C3
	public float calculateCurElapsed()
	{
		return Time.fixedDeltaTime * Config.time_scale_asset.multiplier;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000077D5 File Offset: 0x000059D5
	private void clearFrameCaches()
	{
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000077D7 File Offset: 0x000059D7
	private void LateUpdate()
	{
		this.player_control.clearLateUpdate();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000077E4 File Offset: 0x000059E4
	private void Update()
	{
		FPS.update();
		if (!Config.game_loaded)
		{
			return;
		}
		Config.parallel_jobs_updater = DebugConfig.isOn(DebugOption.ParallelJobsUpdater);
		Bench.bench_ai_enabled = DebugConfig.isOn(DebugOption.BenchAiEnabled);
		if (SmoothLoader.isLoading())
		{
			if (DebugConfig.isOn(DebugOption.GenerateNewMapOnMapLoadingError))
			{
				try
				{
					SmoothLoader.update(Time.deltaTime);
					return;
				}
				catch (Exception message)
				{
					Debug.LogError(message);
					this.generateNewMap();
					return;
				}
			}
			SmoothLoader.update(Time.deltaTime);
			return;
		}
		Randy.nextSeed();
		Bench.bench("game_total", "main", false);
		ScrollingHelper.update();
		Bench.bench("move_camera", "game_total", false);
		this.move_camera.update();
		Bench.benchEnd("move_camera", "game_total", false, 0L, false);
		Bench.bench("mapbox_update_1", "game_total", false);
		this.stack_effects.light_blobs.Clear();
		this._signal_manager.update();
		this.clearFrameCaches();
		Config.updateCrashMetadata();
		PlayerConfig.instance.update();
		Tooltip.checkClearAll();
		CursorTooltipHelper.update();
		this.delta_time = Time.fixedDeltaTime;
		this.fixed_delta_time = Time.fixedDeltaTime;
		this.game_stats.updateStats(Time.deltaTime);
		this._is_paused = (Config.paused || ScrollWindow.isWindowActive() || RewardedAds.isShowing());
		this._cached_map_meta_asset = Zones.getMapMetaAsset();
		Bench.benchEnd("mapbox_update_1", "game_total", false, 0L, false);
		Bench.bench("battle_keeper", "game_total", false);
		BattleKeeperManager.update(this.delta_time);
		Bench.benchEnd("battle_keeper", "game_total", false, 0L, false);
		this.elapsed = this.calculateCurElapsed();
		if (Config.fps_lock_30)
		{
			this.elapsed *= 2f;
		}
		MapBox.cursor_speed.update();
		Bench.bench("music_box", "game_total", false);
		MusicBox.inst.update(this.delta_time);
		Bench.benchEnd("music_box", "game_total", false, 0L, false);
		Bench.bench("auto_tester", "game_total", false);
		this.auto_tester.update(this.elapsed);
		Bench.benchEnd("auto_tester", "game_total", false, 0L, false);
		if (Config.isMobile)
		{
			if (RewardedAds.isShowing())
			{
				return;
			}
			if (PlayInterstitialAd.isShowing())
			{
				return;
			}
		}
		Bench.bench("auto_save", "game_total", false);
		AutoSaveManager.update();
		Bench.benchEnd("auto_save", "game_total", false, 0L, false);
		Bench.bench("send_to_sql", "game_total", false);
		DBInserter.executeCommandsAsync();
		Bench.benchEnd("send_to_sql", "game_total", false, 0L, false);
		this.checkMainSimulationUpdate();
		this.delayed_actions_manager.update(this.elapsed, this.delta_time);
		this.tilemap.update(this.elapsed);
		Bench.bench("update_shake", "game_total", false);
		this.updateShake(this.elapsed);
		Bench.benchEnd("update_shake", "game_total", false, 0L, false);
		Bench.bench("update_panel", "game_total", false);
		this.map_stats.updateStatsForPanel(this.delta_time);
		Bench.benchEnd("update_panel", "game_total", false, 0L, false);
		Bench.bench("quality_changer", "game_total", false);
		this.quality_changer.update();
		Bench.benchEnd("quality_changer", "game_total", false, 0L, false);
		this.updateTransitionEffect();
		Bench.bench("update_controls", "game_total", false);
		this.player_control.updateControls();
		Bench.benchEnd("update_controls", "game_total", false, 0L, false);
		Bench.bench("zone_camera", "game_total", false);
		this.zone_camera.update();
		Bench.benchEnd("zone_camera", "game_total", false, 0L, false);
		Bench.bench("unit_select_effect", "game_total", false);
		if (this._unit_select_effect != null)
		{
			this._unit_select_effect.update(this.elapsed);
		}
		Bench.benchEnd("unit_select_effect", "game_total", false, 0L, false);
		Bench.bench("zone_selection_effect", "game_total", false);
		this.zone_calculator.updateAnimationsAndSelections();
		Bench.benchEnd("zone_selection_effect", "game_total", false, 0L, false);
		Bench.bench("nameplates", "game_total", false);
		this.nameplate_manager.update();
		Bench.benchEnd("nameplates", "game_total", false, 0L, false);
		if (Config.time_scale_asset.render_skip)
		{
			if (this._render_skip < 2)
			{
				this._render_skip++;
			}
			else
			{
				this._render_skip = 0;
				this.calculateVisibleObjects();
				this.renderStuff();
			}
		}
		else
		{
			this.calculateVisibleObjects();
			this.renderStuff();
		}
		Bench.bench("update_sprite_constructor", "game_total", false);
		this.updateDynamicSprites();
		Bench.benchEnd("update_sprite_constructor", "game_total", false, 0L, false);
		Bench.bench("light_renderer", "game_total", false);
		LightRenderer.instance.update(this.delta_time);
		Bench.benchEnd("light_renderer", "game_total", false, 0L, false);
		Bench.bench("update_finish", "game_total", false);
		this.updateFinish();
		Bench.benchEnd("update_finish", "game_total", false, 0L, false);
		Bench.bench("end_checks", "game_total", false);
		this.checkMinWindowSize();
		this.checkVersionCallbacks();
		Bench.update();
		Bench.benchEnd("end_checks", "game_total", false, 0L, false);
		Bench.benchEnd("game_total", "main", false, 0L, false);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00007D7C File Offset: 0x00005F7C
	private void checkMainSimulationUpdate()
	{
		int tUpdatesPerFrame = ScrollWindow.isWindowActive() ? 1 : Config.time_scale_asset.ticks;
		for (int i = 0; i < tUpdatesPerFrame; i++)
		{
			this.updateSimulation(this.elapsed);
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00007DB8 File Offset: 0x00005FB8
	private void updateTransitionEffect()
	{
		if (this._world_layer_switch_effect.color.a == 0f)
		{
			return;
		}
		Color tColor = this._world_layer_switch_effect.color;
		tColor.a -= this.delta_time * 0.1f;
		if (tColor.a < 0f)
		{
			tColor.a = 0f;
		}
		this._world_layer_switch_effect.color = tColor;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00007E28 File Offset: 0x00006028
	private void updateSimulation(float pElapsed)
	{
		this.updateDirtyMetaContainersAndCleanup();
		this.explosion_checker.update(pElapsed);
		this.city_zone_helper.update(pElapsed);
		if (!this.isPaused())
		{
			this.updateTimerNutrition(pElapsed);
			Bench.bench("update_age", "game_total", false);
			this.map_stats.updateWorldTime(pElapsed);
			Bench.benchEnd("update_age", "game_total", false, 0L, false);
			Bench.bench("taxi", "game_total", false);
			TaxiManager.update(pElapsed);
			Bench.benchEnd("taxi", "game_total", false, 0L, false);
			Bench.bench("update_meta_history", "game_total", false);
			this.updateMetaHistory();
			Bench.benchEnd("update_meta_history", "game_total", false, 0L, false);
		}
		AnimationHelper.updateTime(pElapsed, this.delta_time);
		EnemiesFinder.clear();
		ControllableUnit.updateControllableUnit();
		this.updateMapLayers(pElapsed);
		this.updateCities(pElapsed);
		this.updateActors(pElapsed);
		this.updateBuildings(pElapsed);
		this.drop_manager.update(pElapsed);
		this.cultures.update(pElapsed);
		this.stack_effects.update(pElapsed);
		this.resource_throw_manager.update(pElapsed);
		this.updateWorldBehaviours(pElapsed);
		Bench.bench("army_manager", "game_total", false);
		this.armies.update(pElapsed);
		Bench.benchEnd("army_manager", "game_total", false, 0L, false);
		Bench.bench("kingdoms", "game_total", false);
		this.kingdoms.update(pElapsed);
		Bench.benchEnd("kingdoms", "game_total", false, 0L, false);
		Bench.bench("diplomacy", "game_total", false);
		this.diplomacy.update(pElapsed);
		Bench.benchEnd("diplomacy", "game_total", false, 0L, false);
		Bench.bench("subspecies", "game_total", false);
		this.subspecies.update(pElapsed);
		Bench.benchEnd("subspecies", "game_total", false, 0L, false);
		Bench.bench("plots", "game_total", false);
		this.plots.update(pElapsed);
		Bench.benchEnd("plots", "game_total", false, 0L, false);
		Bench.bench("clans", "game_total", false);
		this.clans.update(pElapsed);
		Bench.benchEnd("clans", "game_total", false, 0L, false);
		Bench.bench("alliances", "game_total", false);
		this.alliances.update(pElapsed);
		Bench.benchEnd("alliances", "game_total", false, 0L, false);
		Bench.bench("wars", "game_total", false);
		this.wars.update(pElapsed);
		Bench.benchEnd("wars", "game_total", false, 0L, false);
		Bench.bench("languages", "game_total", false);
		this.languages.update(pElapsed);
		Bench.benchEnd("languages", "game_total", false, 0L, false);
		Bench.bench("religions", "game_total", false);
		this.religions.update(pElapsed);
		Bench.benchEnd("religions", "game_total", false, 0L, false);
		Bench.bench("projectiles", "game_total", false);
		this.projectiles.update(pElapsed);
		Bench.benchEnd("projectiles", "game_total", false, 0L, false);
		Bench.bench("stasuses", "game_total", false);
		this.statuses.update(pElapsed);
		Bench.benchEnd("stasuses", "game_total", false, 0L, false);
		Bench.bench("era_manager", "game_total", false);
		this.era_manager.update(pElapsed);
		Bench.benchEnd("era_manager", "game_total", false, 0L, false);
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000081D4 File Offset: 0x000063D4
	private void updateMetaHistory()
	{
		if (!Config.graphs)
		{
			return;
		}
		if (Config.disable_db)
		{
			return;
		}
		if (Date.getCurrentMonth() != 12)
		{
			return;
		}
		int tYearNow = Date.getCurrentYear();
		if (tYearNow == this.map_stats.history_current_year)
		{
			return;
		}
		this.map_stats.history_current_year = tYearNow;
		foreach (BaseSystemManager baseSystemManager in this.list_all_sim_managers)
		{
			baseSystemManager.startCollectHistoryData();
		}
		this.world_object.startCollectHistoryData();
		foreach (BaseSystemManager baseSystemManager2 in this.list_all_sim_managers)
		{
			baseSystemManager2.clearLastYearStats();
		}
		this.world_object.clearLastYearStats();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000082B4 File Offset: 0x000064B4
	private void updateDirtyMetaContainersAndCleanup()
	{
		BuildingZonesSystem.update();
		this.checkSimManagerLists();
		this.units.checkContainer();
		this.buildings.checkContainer();
		this.sim_object_zones.update();
		Bench.bench("prepare_for_meta_checks", "game_total", false);
		this.units.prepareForMetaChecks();
		Bench.benchEnd("prepare_for_meta_checks", "game_total", false, 0L, false);
		Bench.bench("check_dirty_meta_units", "game_total", false);
		this.checkDirtyUnits();
		Bench.benchEnd("check_dirty_meta_units", "game_total", false, 0L, false);
		Bench.bench("check_dirty_meta_objects", "game_total", false);
		this.checkDirtyMetaObjects();
		Bench.benchEnd("check_dirty_meta_objects", "game_total", false, 0L, false);
		if (!this.isWindowOnScreen())
		{
			Bench.bench("check_meta_obj_destroy", "game_total", false);
			this.checkMetaObjectsDestroy();
			Bench.benchEnd("check_meta_obj_destroy", "game_total", false, 0L, false);
			Bench.bench("check_obj_destroy", "game_total", false);
			this.checkObjectsToDestroy();
			Bench.benchEnd("check_obj_destroy", "game_total", false, 0L, false);
		}
		this.checkSimManagerLists();
		Bench.bench("check_references_units", "game_total", false);
		this.checkEventUnitsDestroy();
		Bench.benchEnd("check_references_units", "game_total", false, 0L, false);
		Bench.bench("check_references_buildings", "game_total", false);
		this.checkEventBuildingsDestroy();
		Bench.benchEnd("check_references_buildings", "game_total", false, 0L, false);
		Bench.bench("check_references_houses", "game_total", false);
		this.checkEventHouses();
		Bench.benchEnd("check_references_houses", "game_total", false, 0L, false);
		Bench.bench("check_dirty_meta_objects_2", "game_total", false);
		this.checkDirtyMetaObjects();
		Bench.benchEnd("check_dirty_meta_objects_2", "game_total", false, 0L, false);
		Bench.bench("check_anything_changed", "game_total", false);
		this.checkAnyMetaAddedRemoved();
		Bench.benchEnd("check_anything_changed", "game_total", false, 0L, false);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000084B0 File Offset: 0x000066B0
	private void checkEventUnitsDestroy()
	{
		if (!this.units.event_destroy)
		{
			return;
		}
		this.units.event_destroy = false;
		foreach (Actor tActor in this.units)
		{
			if (tActor.beh_actor_target != null && !tActor.beh_actor_target.isAlive())
			{
				tActor.beh_actor_target = null;
			}
			if (tActor.attackedBy != null && !tActor.attackedBy.isAlive())
			{
				tActor.attackedBy = null;
			}
			if (tActor.hasLover() && !tActor.lover.isAlive())
			{
				tActor.lover.lover = null;
				tActor.lover = null;
			}
		}
		TaxiManager.removeDeadUnits();
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00008578 File Offset: 0x00006778
	private void checkEventBuildingsDestroy()
	{
		if (!this.buildings.event_destroy)
		{
			return;
		}
		List<Actor> tUnitList = this.units.getSimpleList();
		for (int i = 0; i < tUnitList.Count; i++)
		{
			Actor tActor = tUnitList[i];
			if (tActor.beh_building_target != null && !tActor.beh_building_target.isAlive())
			{
				tActor.beh_building_target = null;
			}
			if (tActor.attackedBy != null && !tActor.attackedBy.isAlive())
			{
				tActor.attackedBy = null;
			}
		}
		this.buildings.event_destroy = false;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000085FC File Offset: 0x000067FC
	private void checkEventHouses()
	{
		if (!this.buildings.event_houses)
		{
			return;
		}
		foreach (Building tBuilding in this.buildings.occupied_buildings)
		{
			tBuilding.residents.Clear();
			if (tBuilding.asset.docks)
			{
				tBuilding.component_docks.clearBoatCounter();
			}
		}
		List<Actor> tUnitList = this.units.getSimpleList();
		for (int i = 0; i < tUnitList.Count; i++)
		{
			Actor tActor = tUnitList[i];
			tActor.checkHomeBuilding();
			Building tHome = tActor.home_building;
			if (tHome != null)
			{
				if (tHome.asset.docks)
				{
					tHome.component_docks.increaseBoatCounter(tActor);
				}
				else
				{
					tHome.residents.Add(tActor.data.id);
				}
			}
			Building tInsideBuilding = tActor.inside_building;
			if (tInsideBuilding != null && (!tInsideBuilding.isUsable() || tInsideBuilding.isAbandoned()))
			{
				tActor.exitBuilding();
				tActor.cancelAllBeh();
			}
		}
		this.buildings.event_houses = false;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00008730 File Offset: 0x00006930
	private void debugHouses()
	{
		foreach (Building tBuilding in this.buildings)
		{
			if (!tBuilding.isUsable() && tBuilding.countResidents() > 0)
			{
				Debug.LogError("Building " + tBuilding.data.id.ToString() + " has residents but is not usable");
			}
			if (!tBuilding.asset.docks && tBuilding.countResidents() > tBuilding.asset.housing_slots)
			{
				Debug.LogError(string.Concat(new string[]
				{
					tBuilding.asset.id,
					" has more residents than housing ",
					tBuilding.countResidents().ToString(),
					"/",
					tBuilding.asset.housing_slots.ToString()
				}));
			}
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00008828 File Offset: 0x00006A28
	public void checkSimManagerLists()
	{
		for (int i = 0; i < this.list_all_sim_managers.Count; i++)
		{
			this.list_all_sim_managers[i].checkLists();
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000885C File Offset: 0x00006A5C
	private void checkDirtyUnits()
	{
		bool tDirtyUnits = false;
		int tCountDirtyUnits = 0;
		for (int i = 0; i < this._list_meta_main_managers.Count; i++)
		{
			if (this._list_meta_main_managers[i].isUnitsDirty())
			{
				tDirtyUnits = true;
				tCountDirtyUnits++;
			}
		}
		if (tDirtyUnits)
		{
			if (tCountDirtyUnits < 3)
			{
				this.subspecies.beginChecksUnits();
				this.families.beginChecksUnits();
				this.armies.beginChecksUnits();
				this.clans.beginChecksUnits();
				this.plots.beginChecksUnits();
				this.languages.beginChecksUnits();
				this.cultures.beginChecksUnits();
				this.religions.beginChecksUnits();
				this.cities.beginChecksUnits();
				this.kingdoms.beginChecksUnits();
				this.kingdoms_wild.beginChecksUnits();
				return;
			}
			Parallel.ForEach<BaseSystemManager>(this._list_meta_main_managers, this.parallel_options, delegate(BaseSystemManager pSystem)
			{
				pSystem.parallelDirtyUnitsCheck();
			});
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00008954 File Offset: 0x00006B54
	private void checkDirtyMetaObjects()
	{
		this.kingdoms_wild.beginChecksBuildings();
		this.kingdoms.beginChecksBuildings();
		this.cities.beginChecksBuildings();
		this.kingdoms.beginChecksCities();
		this.religions.beginChecksKingdoms();
		this.religions.beginChecksCities();
		this.cultures.beginChecksKingdoms();
		this.cultures.beginChecksCities();
		this.languages.beginChecksKingdoms();
		this.languages.beginChecksCities();
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000089D0 File Offset: 0x00006BD0
	private void checkAnyMetaAddedRemoved()
	{
		if (!BaseSystemManager.anything_changed)
		{
			return;
		}
		Config.selected_objects_graph.RemoveWhere((NanoObject pNanoObject) => pNanoObject.isRekt());
		if (ScrollWindow.isWindowActive())
		{
			ScrollWindow.checkElements();
			if (!MetaSwitchManager.isAnimationActive())
			{
				MetaSwitchManager.checkAndRefresh();
			}
		}
		SpriteSwitcher.checkAllStates();
		BaseSystemManager.anything_changed = false;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00008A34 File Offset: 0x00006C34
	private void checkMetaObjectsDestroy()
	{
		if (this._meta_skip)
		{
			this._meta_skip = false;
			return;
		}
		foreach (BaseSystemManager baseSystemManager in this._list_meta_main_managers)
		{
			baseSystemManager.checkDeadObjects();
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00008A94 File Offset: 0x00006C94
	private void calculateVisibleObjects()
	{
		this.buildings.calculateVisibleBuildings();
		this.units.calculateVisibleActors();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00008AAC File Offset: 0x00006CAC
	public void resetRedrawTimer()
	{
		this._redraw_timer = -1f;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00008ABC File Offset: 0x00006CBC
	private void renderStuff()
	{
		QuantumSpriteManager.update();
		Bench.bench("redraw_mini_map", "game_total", false);
		if (this._redraw_timer > 0f)
		{
			this._redraw_timer -= Time.deltaTime;
		}
		else
		{
			this._redraw_timer = 0.001f;
			if (this.tiles_dirty.Count > 0)
			{
				this.redrawMiniMap(false);
			}
		}
		Bench.benchEnd("redraw_mini_map", "game_total", false, 0L, false);
		Bench.bench("redraw_tiles", "game_total", false);
		this.tilemap.redrawTiles(false);
		Bench.benchEnd("redraw_tiles", "game_total", false, 0L, false);
		Bench.bench("update_debug_texts", "game_total", false);
		this.updateDebugGroupSystem();
		Bench.benchEnd("update_debug_texts", "game_total", false, 0L, false);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00008B90 File Offset: 0x00006D90
	private void updateFinish()
	{
		if (this.timer_nutrition_decay <= 0f)
		{
			this.timer_nutrition_decay = SimGlobals.m.interval_nutrition_decay;
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00008BAF File Offset: 0x00006DAF
	private void checkVersionCallbacks()
	{
		if (VersionCallbacks.timer > 0f)
		{
			VersionCallbacks.updateVC(this.elapsed);
		}
		if (Config.EVERYTHING_FIREWORKS)
		{
			this.spawnForcedFireworks();
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00008BD8 File Offset: 0x00006DD8
	private void checkMinWindowSize()
	{
		if (!Screen.fullScreen)
		{
			if (Screen.width < 720)
			{
				Screen.SetResolution(720, Screen.height, false);
				return;
			}
			if (Screen.height < 480)
			{
				Screen.SetResolution(Screen.width, 480, false);
			}
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00008C25 File Offset: 0x00006E25
	private void checkObjectsToDestroy()
	{
		this.buildings.checkObjectsToDestroy();
		this.units.checkObjectsToDestroy();
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00008C40 File Offset: 0x00006E40
	private void updateWorldBehaviours(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemWorldBehaviours))
		{
			return;
		}
		Bench.bench("world_beh", "game_total", false);
		List<WorldBehaviourAsset> tList = AssetManager.world_behaviours.list;
		for (int i = 0; i < tList.Count; i++)
		{
			WorldBehaviourAsset tAsset = tList[i];
			if (tAsset.enabled)
			{
				Bench.bench(tAsset.id, "world_beh", false);
				tAsset.manager.update(pElapsed);
				Bench.benchEnd(tAsset.id, "world_beh", false, 0L, false);
			}
		}
		Bench.benchEnd("world_beh", "game_total", false, 0L, false);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00008CDB File Offset: 0x00006EDB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float getWorldTimeElapsedSince(double pTime)
	{
		return (float)(this.map_stats.world_time - pTime);
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00008CEB File Offset: 0x00006EEB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float getRealTimeElapsedSince(double pTime)
	{
		return (float)(this.getCurSessionTime() - pTime);
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00008CF6 File Offset: 0x00006EF6
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double getCurWorldTime()
	{
		return this.map_stats.world_time;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00008D03 File Offset: 0x00006F03
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double getCurSessionTime()
	{
		return this.game_stats.data.gameTime;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00008D15 File Offset: 0x00006F15
	public void updateDynamicSprites()
	{
		AssetManager.dynamic_sprites_library.checkDirty();
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00008D21 File Offset: 0x00006F21
	public void updateDebugGroupSystem()
	{
		this._debug_text_group_system.update(this.elapsed);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00008D34 File Offset: 0x00006F34
	internal void updateTimerNutrition(float pElapsed)
	{
		if (this.timer_nutrition_decay > 0f)
		{
			this.timer_nutrition_decay -= pElapsed;
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00008D54 File Offset: 0x00006F54
	internal void updateObjectAge()
	{
		foreach (Actor actor in this.units)
		{
			actor.updateAge();
		}
		this.cities.updateAge();
		this.kingdoms.updateAge();
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00008DB4 File Offset: 0x00006FB4
	private void updateCities(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateCities))
		{
			return;
		}
		Bench.bench("cities", "game_total", false);
		this.cities.update(pElapsed);
		Bench.benchEnd("cities", "game_total", false, 0L, false);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00008DF1 File Offset: 0x00006FF1
	private void updateBuildings(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateBuildings))
		{
			return;
		}
		this.buildings.update(pElapsed);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00008E09 File Offset: 0x00007009
	private void updateActors(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateUnits))
		{
			return;
		}
		this.units.update(pElapsed);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00008E24 File Offset: 0x00007024
	private void allTilesDirty()
	{
		this.tiles_dirty.Clear();
		this.tilemap.clear();
		for (int i = 0; i < this.tiles_list.Length; i++)
		{
			WorldTile tTile = this.tiles_list[i];
			this.setTileDirty(tTile);
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00008E6A File Offset: 0x0000706A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void redrawRenderedTile(WorldTile pTile)
	{
		pTile.last_rendered_tile_type = null;
		this.setTileDirty(pTile);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00008E7C File Offset: 0x0000707C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void setTileDirty(WorldTile pTile)
	{
		pTile.updateStats();
		this.tiles_dirty.Add(pTile);
		if (this.tilemap.needsRedraw(pTile))
		{
			this.tilemap.addToQueueToRedraw(pTile);
			if (pTile.has_tile_up)
			{
				this.tilemap.addToQueueToRedraw(pTile.tile_up);
			}
			if (pTile.has_tile_down)
			{
				this.tilemap.addToQueueToRedraw(pTile.tile_down);
			}
			if (pTile.has_tile_left)
			{
				this.tilemap.addToQueueToRedraw(pTile.tile_left);
			}
			if (pTile.has_tile_right)
			{
				this.tilemap.addToQueueToRedraw(pTile.tile_right);
			}
		}
		this.world_layer_edges.addDirtyChunk(pTile.chunk);
		this.checkBehaviours(pTile);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00008F32 File Offset: 0x00007132
	internal void setZoomOrthographic(float pZoom)
	{
		this.quality_changer.setZoomOrthographic(pZoom);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00008F40 File Offset: 0x00007140
	public void redrawMiniMap(bool pForce = false)
	{
		if (!DebugConfig.isOn(DebugOption.SystemRedrawMap))
		{
			return;
		}
		if (!MapBox.isRenderMiniMap() && !pForce)
		{
			return;
		}
		this.dirty_tiles_last = this.tiles_dirty.Count;
		foreach (WorldTile tTile in this.tiles_dirty)
		{
			this.updateDirtyTile(tTile);
		}
		this.world_layer_edges.redraw();
		this.tiles_dirty.Clear();
		this.world_layer.updatePixels();
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00008FDC File Offset: 0x000071DC
	internal void checkBehaviours(WorldTile pTile)
	{
		if (pTile.Type.explodable_timed)
		{
			this.explosion_layer.addTimedTnt(pTile);
		}
		if (pTile.Type.can_be_filled_with_ocean)
		{
			WorldBehaviourOcean.tiles.Add(pTile);
			return;
		}
		WorldBehaviourOcean.tiles.Remove(pTile);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00009028 File Offset: 0x00007228
	private void updateDirtyTile(WorldTile pTile)
	{
		if (pTile.hasBuilding())
		{
			Color grey = Color.grey;
			if (!(pTile.building.getColorForMinimap(pTile) == Toolbox.clear))
			{
				this.world_layer.pixels[pTile.data.tile_id] = pTile.building.getColorForMinimap(pTile);
				return;
			}
		}
		this.world_layer.pixels[pTile.data.tile_id] = pTile.getColor();
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000090AE File Offset: 0x000072AE
	public void followUnit(Actor pActor)
	{
		SelectedUnit.clear();
		this.move_camera.focusOnAndFollow(pActor, null, null);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000090C4 File Offset: 0x000072C4
	public void locateSelectedVillage()
	{
		City tCity = SelectedMetas.selected_city;
		ScrollWindow.hideAllEvent(true);
		this.move_camera.focusOn(tCity.city_center);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000090F3 File Offset: 0x000072F3
	public void locatePosition(Vector3 pVector)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.move_camera.focusOn(pVector);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000910F File Offset: 0x0000730F
	public void locatePosition(Vector3 pVector, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.move_camera.focusOn(pVector, pFocusReachedCallback, pFocusCancelCallback);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000912D File Offset: 0x0000732D
	public void locateAndFollow(Actor pActor, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.move_camera.focusOnAndFollow(pActor, pFocusReachedCallback, pFocusCancelCallback);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000914B File Offset: 0x0000734B
	public bool isSelectedPower(string pPower)
	{
		return this.isAnyPowerSelected() && this.selected_power.id == pPower;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000916D File Offset: 0x0000736D
	public string getSelectedPowerID()
	{
		if (!this.isAnyPowerSelected())
		{
			return string.Empty;
		}
		return this.selected_power.id;
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000C6 RID: 198 RVA: 0x00009188 File Offset: 0x00007388
	public GodPower selected_power
	{
		get
		{
			PowerButton selectedButton = this.selected_buttons.selectedButton;
			if (selectedButton == null)
			{
				return null;
			}
			return selectedButton.godPower;
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000091A0 File Offset: 0x000073A0
	public MouseHoldAnimation getSelectedPowerHoldAnimation()
	{
		if (!this.isAnyPowerSelected())
		{
			return MouseHoldAnimation.Default;
		}
		return this.getSelectedPowerAsset().mouse_hold_animation;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000091B7 File Offset: 0x000073B7
	public bool canDragMap()
	{
		return !this.isAnyPowerSelected() || this.getSelectedPowerAsset().can_drag_map;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x000091CE File Offset: 0x000073CE
	public GodPower getSelectedPowerAsset()
	{
		if (!this.isAnyPowerSelected())
		{
			return null;
		}
		return AssetManager.powers.get(this.getSelectedPowerID());
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000091EA File Offset: 0x000073EA
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isAnyPowerSelected()
	{
		return this.selected_buttons.selectedButton != null;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000091FD File Offset: 0x000073FD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isPaused()
	{
		return this._is_paused;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00009208 File Offset: 0x00007408
	internal void spawnCongratulationFireworks()
	{
		City tCity = this.cities.getRandom();
		if (tCity == null)
		{
			return;
		}
		Building tBuilding = Randy.getRandom<Building>(tCity.buildings);
		if (tBuilding == null)
		{
			return;
		}
		if (tBuilding.isUnderConstruction())
		{
			return;
		}
		EffectsLibrary.spawn("fx_fireworks", tBuilding.current_tile, null, null, 0f, -1f, -1f, null);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00009264 File Offset: 0x00007464
	internal void spawnForcedFireworks()
	{
		WorldTile tTile = Randy.getRandom<WorldTile>(this.tiles_list);
		PlayerConfig.dict["sound"].boolVal = true;
		EffectsLibrary.spawn("fx_fireworks", tTile, null, null, 0f, -1f, -1f, null);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000092B0 File Offset: 0x000074B0
	public int getCivWorldPopulation()
	{
		int tUnits = 0;
		foreach (Actor actor in this.units)
		{
			if (actor.isSapient())
			{
				tUnits++;
			}
			if (actor.asset.is_boat)
			{
				tUnits++;
			}
		}
		return tUnits;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00009314 File Offset: 0x00007514
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isRenderMiniMap()
	{
		return MapBox.instance.quality_changer.isLowRes();
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00009325 File Offset: 0x00007525
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isRenderGameplay()
	{
		return !MapBox.instance.quality_changer.isLowRes();
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00009339 File Offset: 0x00007539
	internal static void aye()
	{
		CornerAye.instance.startAye();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00009345 File Offset: 0x00007545
	public MetaTypeAsset getCachedMapMetaAsset()
	{
		return this._cached_map_meta_asset;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000934D File Offset: 0x0000754D
	public ArchitectMood getArchitectMood()
	{
		if (this._cached_architect_mood == null)
		{
			this._cached_architect_mood = this.map_stats.getArchitectMood();
		}
		return this._cached_architect_mood;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000936E File Offset: 0x0000756E
	public Color getArchitectColor()
	{
		return this.getArchitectMood().getColor();
	}

	// Token: 0x04000035 RID: 53
	internal GameObject joys;

	// Token: 0x04000036 RID: 54
	public const float TRANSITION_EFFECT_ALPHA = 0.1f;

	// Token: 0x04000037 RID: 55
	public const float TRANSITION_EFFECT_ALPHA_SPEED = 0.1f;

	// Token: 0x04000038 RID: 56
	internal SaveManager save_manager;

	// Token: 0x04000039 RID: 57
	internal ResourceThrowManager resource_throw_manager;

	// Token: 0x0400003A RID: 58
	internal GameStats game_stats;

	// Token: 0x0400003B RID: 59
	internal WorldObject world_object = new WorldObject();

	// Token: 0x0400003C RID: 60
	internal MapStats map_stats = new MapStats();

	// Token: 0x0400003D RID: 61
	internal WorldLaws world_laws;

	// Token: 0x0400003E RID: 62
	internal HotkeyTabsData hotkey_tabs_data;

	// Token: 0x0400003F RID: 63
	internal Canvas canvas;

	// Token: 0x04000040 RID: 64
	public static MapBox instance;

	// Token: 0x04000041 RID: 65
	internal PowerButtonSelector selected_buttons;

	// Token: 0x04000042 RID: 66
	internal ParallelOptions parallel_options;

	// Token: 0x04000043 RID: 67
	public Transform drag_parent;

	// Token: 0x04000044 RID: 68
	public static int width;

	// Token: 0x04000045 RID: 69
	public static int height;

	// Token: 0x04000046 RID: 70
	public static int current_world_seed_id;

	// Token: 0x04000047 RID: 71
	internal WorldTile[,] tiles_map;

	// Token: 0x04000048 RID: 72
	internal WorldTile[] tiles_list;

	// Token: 0x04000049 RID: 73
	public readonly List<BaseSystemManager> list_all_sim_managers = new List<BaseSystemManager>();

	// Token: 0x0400004A RID: 74
	private readonly List<BaseSystemManager> _list_meta_other_managers = new List<BaseSystemManager>();

	// Token: 0x0400004B RID: 75
	private readonly List<BaseSystemManager> _list_meta_main_managers = new List<BaseSystemManager>();

	// Token: 0x0400004C RID: 76
	private readonly List<BaseSystemManager> _list_sim_objects_managers = new List<BaseSystemManager>();

	// Token: 0x0400004D RID: 77
	public ProjectileManager projectiles;

	// Token: 0x0400004E RID: 78
	public StatusManager statuses;

	// Token: 0x0400004F RID: 79
	public CityManager cities;

	// Token: 0x04000050 RID: 80
	public WarManager wars;

	// Token: 0x04000051 RID: 81
	public PlotManager plots;

	// Token: 0x04000052 RID: 82
	public AllianceManager alliances;

	// Token: 0x04000053 RID: 83
	public ClanManager clans;

	// Token: 0x04000054 RID: 84
	public KingdomManager kingdoms;

	// Token: 0x04000055 RID: 85
	public WildKingdomsManager kingdoms_wild;

	// Token: 0x04000056 RID: 86
	public CultureManager cultures;

	// Token: 0x04000057 RID: 87
	public BookManager books;

	// Token: 0x04000058 RID: 88
	public SubspeciesManager subspecies;

	// Token: 0x04000059 RID: 89
	public ReligionManager religions;

	// Token: 0x0400005A RID: 90
	public LanguageManager languages;

	// Token: 0x0400005B RID: 91
	public FamilyManager families;

	// Token: 0x0400005C RID: 92
	public ArmyManager armies;

	// Token: 0x0400005D RID: 93
	public ItemManager items;

	// Token: 0x0400005E RID: 94
	public DiplomacyManager diplomacy;

	// Token: 0x0400005F RID: 95
	public BuildingManager buildings;

	// Token: 0x04000060 RID: 96
	public ActorManager units;

	// Token: 0x04000061 RID: 97
	public TileManager tile_manager;

	// Token: 0x04000062 RID: 98
	internal WorldTilemap tilemap;

	// Token: 0x04000063 RID: 99
	private float _redraw_timer;

	// Token: 0x04000064 RID: 100
	private bool _initiated;

	// Token: 0x04000065 RID: 101
	private DebugLayer _debug_layer;

	// Token: 0x04000066 RID: 102
	internal readonly RegionPathFinder region_path_finder = new RegionPathFinder();

	// Token: 0x04000067 RID: 103
	internal LoadingScreen transition_screen;

	// Token: 0x04000068 RID: 104
	internal StackEffects stack_effects;

	// Token: 0x04000069 RID: 105
	public DropManager drop_manager;

	// Token: 0x0400006A RID: 106
	internal PathFindingVisualiser path_finding_visualiser;

	// Token: 0x0400006B RID: 107
	internal WorldLayer world_layer;

	// Token: 0x0400006C RID: 108
	internal SpriteRenderer _world_layer_switch_effect;

	// Token: 0x0400006D RID: 109
	internal WorldLayerEdges world_layer_edges;

	// Token: 0x0400006E RID: 110
	internal UnitLayer unit_layer;

	// Token: 0x0400006F RID: 111
	internal GreyGooLayer grey_goo_layer;

	// Token: 0x04000070 RID: 112
	internal FireLayer fire_layer;

	// Token: 0x04000071 RID: 113
	private LavaLayer _lava_layer;

	// Token: 0x04000072 RID: 114
	internal PixelFlashEffects flash_effects;

	// Token: 0x04000073 RID: 115
	internal IslandsCalculator islands_calculator;

	// Token: 0x04000074 RID: 116
	internal ZoneCalculator zone_calculator;

	// Token: 0x04000075 RID: 117
	internal RoadsCalculator roads_calculator;

	// Token: 0x04000076 RID: 118
	internal BurnedTilesLayer burned_layer;

	// Token: 0x04000077 RID: 119
	internal ExplosionsEffects explosion_layer;

	// Token: 0x04000078 RID: 120
	internal ConwayLife conway_layer;

	// Token: 0x04000079 RID: 121
	internal MapChunkManager map_chunk_manager;

	// Token: 0x0400007A RID: 122
	internal AutoCivilization civilization_maker;

	// Token: 0x0400007B RID: 123
	private List<MapLayer> _map_layers;

	// Token: 0x0400007C RID: 124
	private List<BaseModule> _map_modules;

	// Token: 0x0400007D RID: 125
	internal Earthquake earthquake_manager;

	// Token: 0x0400007E RID: 126
	public Vector2 wind_direction;

	// Token: 0x0400007F RID: 127
	private StaticGrid _search_grid_ground;

	// Token: 0x04000080 RID: 128
	internal HashSet<WorldTile> tiles_dirty;

	// Token: 0x04000081 RID: 129
	internal GlowParticles particles_fire;

	// Token: 0x04000082 RID: 130
	internal GlowParticles particles_smoke;

	// Token: 0x04000083 RID: 131
	internal NameplateManager nameplate_manager;

	// Token: 0x04000084 RID: 132
	internal Console console;

	// Token: 0x04000085 RID: 133
	internal bool has_focus = true;

	// Token: 0x04000086 RID: 134
	internal Heat heat;

	// Token: 0x04000087 RID: 135
	internal HeatRayEffect heat_ray_fx;

	// Token: 0x04000088 RID: 136
	internal EffectDivineLight fx_divine_light;

	// Token: 0x04000089 RID: 137
	private MapBorder _map_border;

	// Token: 0x0400008A RID: 138
	internal QualityChanger quality_changer;

	// Token: 0x0400008B RID: 139
	internal Transform transform_units;

	// Token: 0x0400008F RID: 143
	internal SimObjectsZones sim_object_zones;

	// Token: 0x04000090 RID: 144
	internal Tutorial tutorial;

	// Token: 0x04000091 RID: 145
	private WorldLog _world_log;

	// Token: 0x04000092 RID: 146
	internal Magnet magnet;

	// Token: 0x04000093 RID: 147
	internal float timer_nutrition_decay;

	// Token: 0x04000094 RID: 148
	internal AutoTesterBot auto_tester;

	// Token: 0x04000095 RID: 149
	private UnitSelectionEffect _unit_select_effect;

	// Token: 0x04000096 RID: 150
	private readonly List<SpriteGroupSystem<GroupSpriteObject>> _list_systems = new List<SpriteGroupSystem<GroupSpriteObject>>();

	// Token: 0x04000097 RID: 151
	public static CursorSpeed cursor_speed;

	// Token: 0x04000098 RID: 152
	private DebugTextGroupSystem _debug_text_group_system;

	// Token: 0x04000099 RID: 153
	private SignalManager _signal_manager;

	// Token: 0x0400009A RID: 154
	internal ExplosionChecker explosion_checker;

	// Token: 0x0400009B RID: 155
	internal WorldAgeManager era_manager;

	// Token: 0x0400009C RID: 156
	public DelayedActionsManager delayed_actions_manager;

	// Token: 0x0400009D RID: 157
	public PlayerControl player_control;

	// Token: 0x0400009E RID: 158
	private bool _first_gen = true;

	// Token: 0x0400009F RID: 159
	private int _load_counter;

	// Token: 0x040000A0 RID: 160
	private float _shake_timer;

	// Token: 0x040000A1 RID: 161
	private float _shake_interval_timer;

	// Token: 0x040000A2 RID: 162
	private float _shake_intensity = 1f;

	// Token: 0x040000A3 RID: 163
	private float _shake_interval = 0.1f;

	// Token: 0x040000A4 RID: 164
	private bool _shake_x = true;

	// Token: 0x040000A5 RID: 165
	private bool _shake_y = true;

	// Token: 0x040000A6 RID: 166
	private Transform _shake_camera;

	// Token: 0x040000A7 RID: 167
	internal float elapsed;

	// Token: 0x040000A8 RID: 168
	internal float delta_time;

	// Token: 0x040000A9 RID: 169
	internal float fixed_delta_time;

	// Token: 0x040000AA RID: 170
	public readonly CityZoneHelper city_zone_helper = new CityZoneHelper();

	// Token: 0x040000AB RID: 171
	internal static Action on_world_loaded;

	// Token: 0x040000AC RID: 172
	private static int _tile_id;

	// Token: 0x040000AD RID: 173
	internal readonly AStarParam pathfinding_param = new AStarParam();

	// Token: 0x040000AE RID: 174
	internal int dirty_tiles_last;

	// Token: 0x040000AF RID: 175
	private bool _is_paused;

	// Token: 0x040000B0 RID: 176
	private int _render_skip;

	// Token: 0x040000B1 RID: 177
	private bool _meta_skip = true;

	// Token: 0x040000B2 RID: 178
	private MetaTypeAsset _cached_map_meta_asset;

	// Token: 0x040000B3 RID: 179
	private ArchitectMood _cached_architect_mood;
}
