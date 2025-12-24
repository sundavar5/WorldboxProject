using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B9 RID: 1465
public class TrailerMonolith : MonoBehaviour
{
	// Token: 0x06003052 RID: 12370 RVA: 0x00175E48 File Offset: 0x00174048
	public void Start()
	{
		this.camera_object = Camera.main.gameObject;
		this.calculateTimings();
		this.camera_object.AddComponent<AudioListener>();
		DebugConfig.setOption(DebugOption.ArrowsUnitsAttackTargets, false, true);
		this._drums = new double[64];
		for (int i = 0; i < this._drums.Length; i++)
		{
			this._drums[i] = (double)i * 29.538000106811523 / 64.0;
		}
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x00175EC0 File Offset: 0x001740C0
	private void newLoop()
	{
		this._processed_keys.Clear();
		this._processed_drums.Clear();
		this._processed_buildings.Clear();
		this.resetDancingTrees();
		double tDiff = 0.0;
		if (this._timer_song > 29.538000106811523)
		{
			tDiff = this._timer_song - 29.538000106811523;
		}
		this._timer_song = 0.0 + tDiff;
	}

	// Token: 0x06003054 RID: 12372 RVA: 0x00175F31 File Offset: 0x00174131
	private void resetTrack()
	{
		this.audio_source.Stop();
		this.audio_source.time = 0f;
		this.audio_source.Play();
	}

	// Token: 0x06003055 RID: 12373 RVA: 0x00175F5C File Offset: 0x0017415C
	private void calculateTimings()
	{
		this._last_offset = this.offset_timings;
		this._keys_timings = new double[this._keys.Length];
		for (int i = 0; i < this._keys.Length; i++)
		{
			this._keys_timings[i] = (double)this._keys[i] * 29.538000106811523 / 64.0 + this.offset_timings;
		}
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x00175FC8 File Offset: 0x001741C8
	private void resetDancingTrees()
	{
		double tDiff;
		if (this._timer_dancing_trees > 0.46153125166893005)
		{
			tDiff = this._timer_dancing_trees - 0.46153125166893005;
		}
		else
		{
			tDiff = 0.0;
		}
		this._timer_dancing_trees = 0.0 + tDiff;
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x00176014 File Offset: 0x00174214
	public void Update()
	{
		if (Config.worldLoading)
		{
			return;
		}
		if (!this.enabled_auto)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.reset = true;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (this._last_offset != this.offset_timings)
		{
			this.calculateTimings();
		}
		if (Time.frameCount % 5 == 0 && TrailerMonolith.harp_frame_index < 19)
		{
			TrailerMonolith.harp_frame_index++;
		}
		this.track_time = this._timer_song;
		if (this.reset)
		{
			this.reset = false;
			this.resetTrack();
			this.newLoop();
			World.world.move_camera.forceZoom(30f);
			this._camera_switch_timer = 2f;
		}
		this.updateCamera();
		if (this._timer_song < 29.538000106811523)
		{
			for (int i = 0; i < this._drums.Length; i++)
			{
				if (!this._processed_drums.Contains(i) && this._timer_song >= this._drums[i])
				{
					this._processed_drums.Add(i);
					this.dancingTrees();
				}
			}
			for (int j = 0; j < this._keys_timings.Length; j++)
			{
				if (!this._processed_keys.Contains(j) && this._timer_song >= this._keys_timings[j])
				{
					this.glowMonolith(j);
					this.spawnRandomUnit();
					this.spawnRandomUnit();
					this.spawnRandomUnit();
					this.spawnRandomLightning();
					this.spawnRandomLightning();
					this.spawnRandomLightning();
					this.spawnRandomLightning();
					this._processed_keys.Add(j);
					if (j == 8 || j == 14 || j == 6)
					{
						this.doMonolithAction();
						this.switchBiome();
						this.spawnRandoMTornado();
					}
				}
			}
		}
		if (this._timer_song < 29.538000106811523 && !this.transition)
		{
			this._timer_song += (double)Time.deltaTime;
		}
		else
		{
			this.newLoop();
			this.doMonolithAction();
			this.switchBiome();
		}
		if (this.transition)
		{
			this.transition = false;
		}
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x00176208 File Offset: 0x00174408
	private void updateCamera()
	{
		World.world.move_camera.camera_zoom_speed = 0.2f;
		if (this._camera_switch_timer > 0f)
		{
			this._camera_switch_timer -= Time.deltaTime;
		}
		else
		{
			this._camera_switch_timer = 10f;
			this._camera_go_zoom = !this._camera_go_zoom;
		}
		if (this._camera_go_zoom)
		{
			World.world.move_camera.setTargetZoom(30f);
			return;
		}
		World.world.move_camera.setTargetZoom(60f);
	}

	// Token: 0x06003059 RID: 12377 RVA: 0x00176298 File Offset: 0x00174498
	private void spawnRandoMTornado()
	{
		WorldTile tTile = TopTileLibrary.wall_ancient.getCurrentTiles().GetRandom<WorldTile>();
		EffectsLibrary.spawnAtTile("fx_tornado", tTile, 0.125f);
	}

	// Token: 0x0600305A RID: 12378 RVA: 0x001762C6 File Offset: 0x001744C6
	private void spawnRandomLightning()
	{
		MapBox.spawnLightningSmall(World.world.islands_calculator.tryGetRandomGround(), 0.25f, null);
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x001762E4 File Offset: 0x001744E4
	private void doMonolithAction()
	{
		Building tMonolith = this.findMonolith();
		EffectsLibrary.spawnAt("fx_monolith_launch_bottom", tMonolith.current_tile.posV3, tMonolith.current_scale.y);
		EffectsLibrary.spawnAt("fx_monolith_launch", tMonolith.current_tile.posV3, tMonolith.current_scale.y);
	}

	// Token: 0x0600305C RID: 12380 RVA: 0x0017633C File Offset: 0x0017453C
	private void spawnRandomUnit()
	{
		string tID = this._unit_assets_to_spawn.GetRandom<string>();
		WorldTile tTile = TileLibrary.hills.getCurrentTiles().GetRandom<WorldTile>();
		bool tMiracleSpawn = Randy.randomChance(0.8f);
		World.world.units.spawnNewUnit(tID, tTile, false, tMiracleSpawn, 6f, null, false, false);
		EffectsLibrary.spawn("fx_spawn", tTile, null, null, 0f, -1f, -1f, null);
	}

	// Token: 0x0600305D RID: 12381 RVA: 0x001763AC File Offset: 0x001745AC
	private void glowMonolith(int pIndex)
	{
		foreach (Actor actor in World.world.units)
		{
			actor.makeStunned(1f);
			actor.applyRandomForce(1.5f, 2f);
		}
		Building tMonolith = this.findMonolith();
		if (tMonolith == null)
		{
			return;
		}
		if (pIndex == 5 || pIndex == 9 || pIndex == 13)
		{
			EffectsLibrary.spawnAt("fx_monolith_glow_1", tMonolith.current_tile.posV3, tMonolith.current_scale.y);
		}
		else
		{
			EffectsLibrary.spawnAt("fx_monolith_glow_2", tMonolith.current_tile.posV3, tMonolith.current_scale.y);
		}
		TrailerMonolith.harp_frame_index = 11;
		foreach (Building tBuilding in World.world.buildings)
		{
			if (!(tBuilding.asset.id == "monolith") && !(tBuilding.asset.id == "waypoint_harp"))
			{
				tBuilding.startShake(0.5f, 0.1f, 0.1f);
			}
		}
	}

	// Token: 0x0600305E RID: 12382 RVA: 0x001764F0 File Offset: 0x001746F0
	private Building findMonolith()
	{
		foreach (Building tBuilding in World.world.buildings)
		{
			if (!(tBuilding.asset.id != "monolith"))
			{
				tBuilding.setMaxHealth();
				return tBuilding;
			}
		}
		return null;
	}

	// Token: 0x0600305F RID: 12383 RVA: 0x00176560 File Offset: 0x00174760
	private void dancingTrees()
	{
		Building tMonolith = this.findMonolith();
		if (tMonolith == null)
		{
			return;
		}
		foreach (Building tBuilding in World.world.buildings)
		{
			if (!(tBuilding.asset.id == "monolith") && !(tBuilding.asset.id == "waypoint_harp"))
			{
				tBuilding.setScaleTween(0.9f, 0.2f, 1f, null, null, 0);
			}
		}
		foreach (Building tBuilding2 in World.world.buildings)
		{
			if (!(tBuilding2.asset.id != "waypoint_harp"))
			{
				tBuilding2.setScaleTween(0.8f, 0.2f, 1f, null, null, 0);
			}
		}
		foreach (Building tBuilding3 in World.world.buildings)
		{
			if (!(tBuilding3.asset.id == "monolith") && !(tBuilding3.asset.id == "waypoint_harp") && tBuilding3.asset.building_type == BuildingType.Building_Tree)
			{
				tBuilding3.setScaleTween(0.9f, 0.2f, 1f, null, null, 0);
			}
		}
		foreach (Building tBuilding4 in World.world.buildings)
		{
			if (!(tBuilding4.asset.id == "monolith") && !(tBuilding4.asset.id == "waypoint_harp") && tBuilding4.asset.building_type == BuildingType.Building_Tree && !this._processed_buildings.Contains(tBuilding4))
			{
				float tDistance = Vector3.Distance(tBuilding4.current_tile.posV3, tMonolith.current_position);
				float tMinDistance = 1f;
				switch (this._tree_wave)
				{
				case 0:
					tMinDistance = 10f;
					break;
				case 1:
					tMinDistance = 15f;
					break;
				case 2:
					tMinDistance = 25f;
					break;
				case 3:
					tMinDistance = 35f;
					break;
				case 4:
					tMinDistance = 50f;
					break;
				}
				if (tDistance <= tMinDistance)
				{
					this._processed_buildings.Add(tBuilding4);
					float tDuration = 0.3f * (float)(5 - this._tree_wave) + Randy.randomFloat(0f, 0.1f);
					tBuilding4.setScaleTween(0.3f, tDuration, 1f, null, null, 0);
				}
			}
		}
		this._tree_wave++;
		if (this._tree_wave >= 5)
		{
			this._tree_wave = 0;
			this._processed_buildings.Clear();
		}
	}

	// Token: 0x06003060 RID: 12384 RVA: 0x001768A8 File Offset: 0x00174AA8
	private void switchBiome()
	{
		this._current_biome++;
		if (this._current_biome >= this._biomes.Length)
		{
			this._current_biome = 0;
		}
		this._tree_wave = 0;
		World.world.era_manager.startNextAge(0f);
		BiomeAsset tBiomeAsset = AssetManager.biome_library.get(this._biomes[this._current_biome]);
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.main_type.soil && tTile.top_type != null && tTile.top_type.is_biome)
			{
				if (tTile.main_type.rank_type == TileRank.High)
				{
					tTile.setTopTileType(tBiomeAsset.getTileHigh(), true);
				}
				else
				{
					tTile.setTopTileType(tBiomeAsset.getTileLow(), true);
				}
			}
		}
		foreach (Building tBuilding in World.world.buildings)
		{
			if (tBuilding.asset.flora)
			{
				if (tBuilding.asset.building_type == BuildingType.Building_Tree)
				{
					if (!(tBuilding.asset.id == "palm_tree"))
					{
						string tAssetID = tBiomeAsset.pot_trees_spawn.GetRandom<string>();
						BuildingAsset tBuildingAsset = AssetManager.buildings.get(tAssetID);
						tBuilding.asset = tBuildingAsset;
						tBuilding.clearSprites();
					}
				}
				else if (tBuilding.asset.building_type == BuildingType.Building_Plant)
				{
					string tAssetID2 = tBiomeAsset.pot_plants_spawn.GetRandom<string>();
					if (tAssetID2 == "fruit_bush")
					{
						for (int i = 0; i < tBiomeAsset.pot_plants_spawn.Count; i++)
						{
							if (!(tBiomeAsset.pot_plants_spawn[i] == "fruit_bush"))
							{
								tAssetID2 = tBiomeAsset.pot_plants_spawn[i];
								break;
							}
						}
					}
					BuildingAsset tBuildingAsset2 = AssetManager.buildings.get(tAssetID2);
					tBuilding.asset = tBuildingAsset2;
					tBuilding.clearSprites();
				}
			}
		}
	}

	// Token: 0x04002474 RID: 9332
	public static readonly bool enable_trailer_stuff;

	// Token: 0x04002475 RID: 9333
	public bool enabled_auto = true;

	// Token: 0x04002476 RID: 9334
	public AudioSource audio_source;

	// Token: 0x04002477 RID: 9335
	private GameObject camera_object;

	// Token: 0x04002478 RID: 9336
	private string[] _biomes = new string[]
	{
		"biome_savanna",
		"biome_grass",
		"biome_infernal",
		"biome_crystal",
		"biome_lemon",
		"biome_singularity",
		"biome_garlic",
		"biome_clover",
		"biome_candy",
		"biome_permafrost",
		"biome_desert",
		"biome_swamp",
		"biome_maple",
		"biome_birch",
		"biome_flower",
		"biome_paradox",
		"biome_mushroom",
		"biome_rocklands",
		"biome_enchanted",
		"biome_corrupted",
		"biome_jungle"
	};

	// Token: 0x04002479 RID: 9337
	private string[] _unit_assets_to_spawn = new string[]
	{
		"demon",
		"cold_one",
		"sheep",
		"angle",
		"skeleton",
		"evil_mage",
		"white_mage",
		"alien",
		"necromancer"
	};

	// Token: 0x0400247A RID: 9338
	private int[] _keys = new int[]
	{
		0,
		4,
		8,
		10,
		11,
		13,
		16,
		24,
		32,
		36,
		40,
		42,
		43,
		45,
		48,
		56
	};

	// Token: 0x0400247B RID: 9339
	private double[] _keys_timings;

	// Token: 0x0400247C RID: 9340
	private double[] _drums;

	// Token: 0x0400247D RID: 9341
	private int _current_biome;

	// Token: 0x0400247E RID: 9342
	private const double INTERVAL_DANCING_TREES = 0.46153125166893005;

	// Token: 0x0400247F RID: 9343
	private double _timer_dancing_trees;

	// Token: 0x04002480 RID: 9344
	private const double INTERVAL_LOOP = 29.538000106811523;

	// Token: 0x04002481 RID: 9345
	private double _timer_song;

	// Token: 0x04002482 RID: 9346
	private double offset_timings = -0.15000000596046448;

	// Token: 0x04002483 RID: 9347
	private double _last_offset;

	// Token: 0x04002484 RID: 9348
	public double track_time;

	// Token: 0x04002485 RID: 9349
	private HashSet<int> _processed_keys = new HashSet<int>();

	// Token: 0x04002486 RID: 9350
	private HashSet<int> _processed_drums = new HashSet<int>();

	// Token: 0x04002487 RID: 9351
	private HashSet<Building> _processed_buildings = new HashSet<Building>();

	// Token: 0x04002488 RID: 9352
	public bool reset;

	// Token: 0x04002489 RID: 9353
	public bool transition;

	// Token: 0x0400248A RID: 9354
	public static int harp_frame_index;

	// Token: 0x0400248B RID: 9355
	private const int HARP_MAX_FRAMES = 19;

	// Token: 0x0400248C RID: 9356
	private bool _camera_go_zoom = true;

	// Token: 0x0400248D RID: 9357
	private float _camera_switch_timer = 10f;

	// Token: 0x0400248E RID: 9358
	private const int MAX_WAVE = 5;

	// Token: 0x0400248F RID: 9359
	private int _tree_wave;
}
