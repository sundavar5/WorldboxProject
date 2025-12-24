using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x02000011 RID: 17
public static class MapAction
{
	// Token: 0x0600002A RID: 42 RVA: 0x00003D54 File Offset: 0x00001F54
	public static void checkAcidTerraform(WorldTile pTile)
	{
		if (pTile.isTemporaryFrozen())
		{
			pTile.unfreeze(99);
		}
		if (pTile.top_type != null && pTile.top_type.wasteland)
		{
			return;
		}
		if (pTile.top_type != null)
		{
			MapAction.decreaseTile(pTile, true, "flash");
			return;
		}
		if (pTile.Type.ground)
		{
			if (pTile.isTileRank(TileRank.Low))
			{
				MapAction.terraformTop(pTile, TopTileLibrary.wasteland_low, false);
			}
			else if (pTile.isTileRank(TileRank.High))
			{
				MapAction.terraformTop(pTile, TopTileLibrary.wasteland_high, false);
			}
			AchievementLibrary.lets_not.check(null);
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003DE1 File Offset: 0x00001FE1
	public static void terraformMain(WorldTile pTile, TileType pType, bool pSkipTerraform = false)
	{
		MapAction.terraformTile(pTile, pType, null, TerraformLibrary.flash, pSkipTerraform);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003DF1 File Offset: 0x00001FF1
	public static void terraformTop(WorldTile pTile, TopTileType pTopType, bool pSkipTerraform = false)
	{
		MapAction.terraformTile(pTile, pTile.main_type, pTopType, TerraformLibrary.flash, pSkipTerraform);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003E06 File Offset: 0x00002006
	public static void terraformMain(WorldTile pTile, TileType pType, TerraformOptions pOptions, bool pSkipTerraform = false)
	{
		MapAction.terraformTile(pTile, pType, null, pOptions, pSkipTerraform);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003E12 File Offset: 0x00002012
	public static void terraformTop(WorldTile pTile, TopTileType pTopType, TerraformOptions pOptions, bool pSkipTerraform = false)
	{
		MapAction.terraformTile(pTile, pTile.main_type, pTopType, pOptions, pSkipTerraform);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003E24 File Offset: 0x00002024
	public static void terraformTile(WorldTile pTile, TileType pNewTypeMain, TopTileType pTopType, TerraformOptions pOptions = null, bool pSkipTerraform = false)
	{
		if (pOptions == null)
		{
			pOptions = TerraformLibrary.flash;
		}
		TileTypeBase tOldLayerType = pTile.Type;
		TileTypeBase type = pTile.Type;
		if (pOptions.remove_fire)
		{
			pTile.stopFire();
		}
		if (!pSkipTerraform)
		{
			if (pOptions.remove_water && pTile.Type.ocean)
			{
				pNewTypeMain = pTile.Type.decrease_to;
			}
			if (pOptions.remove_top_tile && pTile.top_type != null)
			{
				pNewTypeMain = pTile.Type.decrease_to;
			}
			if (pOptions.remove_roads && pTile.Type.road)
			{
				pNewTypeMain = pTile.Type.decrease_to;
			}
			if (pOptions.remove_frozen && pTile.isTemporaryFrozen())
			{
				pTile.unfreeze(99);
			}
			if (pNewTypeMain != null)
			{
				pTile.setTileTypes(pNewTypeMain, pTopType, true);
			}
			else
			{
				pTile.setTopTileType(pTopType, true);
			}
		}
		if (type.can_be_farm != pTile.Type.can_be_farm && !pTile.zone.hasCity())
		{
			World.world.city_zone_helper.city_place_finder.setDirty();
		}
		if ((pTile.burned_stages > 0 && !pTile.Type.can_be_set_on_fire) || pOptions.remove_burned)
		{
			pTile.removeBurn();
		}
		World.world.resetRedrawTimer();
		if (pOptions.remove_borders)
		{
			World.world.checkCityZone(pTile);
		}
		if (pOptions.flash)
		{
			World.world.flash_effects.flashPixel(pTile, 20, ColorType.White);
		}
		if (pTile.hasBuilding() && !pTile.building.isRuin() && !pTile.building.asset.isOverlaysBiomeTags(pTile.Type))
		{
			if (pTile.building.asset.has_ruins_graphics)
			{
				pTile.building.startMakingRuins();
			}
			else
			{
				pTile.building.startDestroyBuilding();
			}
		}
		if (pOptions.make_ruins && pTile.hasBuilding())
		{
			Building tBuilding = pTile.building;
			if (tBuilding.asset.has_ruins_graphics)
			{
				tBuilding.startMakingRuins();
			}
			else
			{
				tBuilding.startDestroyBuilding();
			}
			if (!tBuilding.asset.can_be_placed_on_blocks && pTile.Type.rocks)
			{
				tBuilding.startDestroyBuilding();
			}
			if (!tBuilding.asset.can_be_placed_on_liquid && pTile.Type.liquid)
			{
				tBuilding.startDestroyBuilding();
			}
		}
		if (pOptions.destroy_buildings && pTile.hasBuilding())
		{
			bool tDestroy;
			if (pOptions.ignore_kingdoms != null)
			{
				tDestroy = true;
				for (int i = 0; i < pOptions.ignore_kingdoms.Length; i++)
				{
					string a = pOptions.ignore_kingdoms[i];
					Kingdom kingdom = pTile.building.kingdom;
					if (!(a != ((kingdom != null) ? kingdom.name : null)))
					{
						tDestroy = false;
						break;
					}
				}
			}
			else if (pOptions.destroy_only != null)
			{
				tDestroy = false;
				for (int j = 0; j < pOptions.destroy_only.Count; j++)
				{
					if (!(pOptions.destroy_only[j] != pTile.building.asset.group))
					{
						tDestroy = true;
						break;
					}
				}
			}
			else
			{
				tDestroy = (pOptions.ignore_buildings == null || !pOptions.ignore_buildings.Contains(pTile.building.asset.id));
			}
			if (tDestroy)
			{
				pTile.building.startDestroyBuilding();
			}
		}
		MapAction.checkTileState(pTile, tOldLayerType, false);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00004138 File Offset: 0x00002338
	public static void checkTileState(WorldTile pTile, TileTypeBase pOldType, bool pForceMapChunk = false)
	{
		if (pOldType.layer_type != pTile.Type.layer_type || pForceMapChunk)
		{
			World.world.map_chunk_manager.setDirty(pTile.chunk, true, true);
			foreach (MapChunk tNeighbour in pTile.chunk.neighbours_all)
			{
				World.world.map_chunk_manager.setDirty(tNeighbour, false, true);
			}
		}
		if (pTile.Type.layer_type != TileLayerType.Ground)
		{
			World.world.checkCityZone(pTile);
		}
		if (pTile.hasBuilding() && !pTile.building.asset.can_be_placed_on_liquid && pTile.Type.ocean)
		{
			pTile.building.startDestroyBuilding();
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000041F4 File Offset: 0x000023F4
	public static void setOcean(WorldTile pTile)
	{
		if (pTile.Type.fill_to_ocean == null)
		{
			return;
		}
		TileType tType = AssetManager.tiles.get(pTile.Type.fill_to_ocean);
		if (pTile.Type.water_fill_sound != string.Empty)
		{
			MusicBox.playSound(pTile.Type.water_fill_sound, pTile, false, false);
		}
		MapAction.terraformMain(pTile, tType, TerraformLibrary.water_fill, false);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000425C File Offset: 0x0000245C
	public static void decreaseTile(WorldTile pTile, bool pDamage, TerraformOptions pTerraformOption)
	{
		if (!MapAction.checkTileDamageGaiaCovenant(pTile, pDamage))
		{
			return;
		}
		if (pTile.isTemporaryFrozen())
		{
			pTile.unfreeze(100);
			return;
		}
		if (pTile.top_type != null)
		{
			MapAction.terraformTile(pTile, pTile.main_type, null, pTerraformOption, false);
			return;
		}
		if (pTile.Type.decrease_to == null)
		{
			return;
		}
		MapAction.terraformMain(pTile, pTile.Type.decrease_to, pTerraformOption, false);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000042C0 File Offset: 0x000024C0
	public static bool checkTileDamageGaiaCovenant(WorldTile pTile, bool pDamage)
	{
		bool tDamageAnyway = pTile.Type.life || pTile.Type.explodable;
		return !pDamage || !WorldLawLibrary.world_law_gaias_covenant.isEnabled() || tDamageAnyway;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000042FE File Offset: 0x000024FE
	public static void decreaseTile(WorldTile pTile, bool pDamage, string pTerraformOption = "flash")
	{
		if (!MapAction.checkTileDamageGaiaCovenant(pTile, pDamage))
		{
			return;
		}
		MapAction.decreaseTile(pTile, pDamage, AssetManager.terraform.get(pTerraformOption));
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000431C File Offset: 0x0000251C
	public static void increaseTile(WorldTile pTile, bool pDamage, string pTerraformOption = "flash")
	{
		if (!MapAction.checkTileDamageGaiaCovenant(pTile, pDamage))
		{
			return;
		}
		if (pTile.top_type != null)
		{
			MapAction.terraformTile(pTile, pTile.main_type, null, AssetManager.terraform.get(pTerraformOption), false);
			return;
		}
		if (pTile.Type.increase_to == null)
		{
			return;
		}
		MapAction.terraformMain(pTile, pTile.Type.increase_to, AssetManager.terraform.get(pTerraformOption), false);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004380 File Offset: 0x00002580
	public static void removeLiquid(WorldTile pTile)
	{
		if (!pTile.Type.liquid)
		{
			return;
		}
		MapAction.decreaseTile(pTile, false, "flash");
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000439C File Offset: 0x0000259C
	public static void growGreens(WorldTile pTile, TopTileType pTopType)
	{
		MapAction.terraformTop(pTile, pTopType, TerraformLibrary.flash, false);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000043AB File Offset: 0x000025AB
	public static void removeGreens(WorldTile pTile)
	{
		MapAction.decreaseTile(pTile, false, "flash");
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000043BC File Offset: 0x000025BC
	private static void applyLightningEffect(WorldTile pTile)
	{
		if (pTile.Type.lava && pTile.heat > 20)
		{
			MapAction.decreaseTile(pTile, true, "flash");
			if (Randy.randomChance(0.9f))
			{
				int tExtra = pTile.heat / 10;
				World.world.drop_manager.spawnParabolicDrop(pTile, "lava", 0f, 0.15f, 33f + (float)(tExtra * 2), 1f, 40f + (float)tExtra, -1f);
			}
			AchievementLibrary.lava_strike.check(null);
		}
		if (pTile.Type.layer_type == TileLayerType.Ocean)
		{
			MapAction.removeLiquid(pTile);
			if (Randy.randomChance(0.8f))
			{
				World.world.drop_manager.spawnParabolicDrop(pTile, "rain", 0f, 1f, 66f, 1f, 45f, -1f);
			}
		}
		if (pTile.hasBuilding() && pTile.building.asset.spawn_drops)
		{
			if (!pTile.building.data.hasFlag("stop_spawn_drops"))
			{
				pTile.building.spawnBurstSpecial(10);
			}
			if (pTile.building.data.hasFlag("stop_spawn_drops"))
			{
				pTile.building.data.removeFlag("stop_spawn_drops");
				return;
			}
			pTile.building.data.addFlag("stop_spawn_drops");
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004520 File Offset: 0x00002720
	public static void applyTileDamage(WorldTile pTargetTile, float pRad, TerraformOptions pOptions)
	{
		World.world.resetRedrawTimer();
		BrushData pBrush = Brush.get((int)pRad, "circ_");
		World.world.conway_layer.checkKillRange(pTargetTile.pos, pBrush.size);
		if (pOptions.remove_tornado)
		{
			MapAction.tryRemoveTornadoFromTile(pTargetTile);
		}
		WorldBehaviourTileEffects.checkTileForEffectKill(pTargetTile, pBrush.size);
		Action<Actor> <>9__0;
		for (int i = 0; i < pBrush.pos.Length; i++)
		{
			BrushPixelData tBrushPixel = pBrush.pos[i];
			int tX = pTargetTile.pos.x + tBrushPixel.x;
			int tY = pTargetTile.pos.y + tBrushPixel.y;
			if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
			{
				WorldTile tTile = World.world.GetTileSimple(tX, tY);
				if (tTile.Type.grey_goo)
				{
					Config.grey_goo_damaged = true;
				}
				if (pOptions.add_burned && !tTile.Type.liquid)
				{
					tTile.setBurned(-1);
				}
				if (pOptions.lightning_effect)
				{
					MapAction.applyLightningEffect(tTile);
				}
				if (pOptions.add_heat != 0)
				{
					World.world.heat.addTile(tTile, pOptions.add_heat);
				}
				if (tTile.hasBuilding() && pOptions.damage_buildings)
				{
					bool tTryDamageBuilding = true;
					if (pOptions.ignore_kingdoms != null && tTile.building.isAlive() && !tTile.building.kingdom.isNature())
					{
						for (int j = 0; j < pOptions.ignore_kingdoms.Length; j++)
						{
							string tKingdomID = pOptions.ignore_kingdoms[j];
							Kingdom tKingdom = World.world.kingdoms_wild.get(tKingdomID);
							if (tTile.building.kingdom == tKingdom)
							{
								tTryDamageBuilding = false;
							}
						}
					}
					if (tTryDamageBuilding)
					{
						tTile.building.getHit((float)pOptions.damage, true, AttackType.Other, null, true, false, true);
					}
				}
				if (pOptions.set_fire)
				{
					tTile.startFire(true);
				}
				bool tTileExploded = false;
				if (pOptions.explode_tile)
				{
					tTileExploded = MapAction.explodeTile(tTile, (float)tBrushPixel.dist, pRad, pTargetTile, pOptions);
				}
				if (pOptions.transform_to_wasteland && !tTileExploded)
				{
					MapAction.checkAcidTerraform(tTile);
				}
				if (tTile.hasUnits() && !string.IsNullOrEmpty(pOptions.add_trait))
				{
					WorldTile worldTile = tTile;
					Action<Actor> pAction;
					if ((pAction = <>9__0) == null)
					{
						pAction = (<>9__0 = delegate(Actor tActor)
						{
							tActor.addTrait(pOptions.add_trait, false);
						});
					}
					worldTile.doUnits(pAction);
				}
			}
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000047E8 File Offset: 0x000029E8
	public static bool explodeTile(WorldTile pTile, float pDist, float pRadius, WorldTile pExplosionCenter, TerraformOptions pOptions)
	{
		if (pOptions.damage > 0)
		{
			pTile.doUnits(delegate(Actor tActor)
			{
				if (tActor.asset.very_high_flyer && !pOptions.applies_to_high_flyers)
				{
					return;
				}
				tActor.getHit((float)pOptions.damage, true, AttackType.Explosion, null, true, false, true);
			});
		}
		if (pTile.isTemporaryFrozen())
		{
			pTile.unfreeze(1);
		}
		float tMode = pDist / pRadius;
		float tModeInverse = 1f - tMode;
		int tDamage = (int)(30f * tModeInverse);
		if (tDamage <= 0)
		{
			return false;
		}
		bool wasLiquid = pTile.Type.liquid;
		if (!pTile.Type.explodable && Randy.random() > tModeInverse)
		{
			return false;
		}
		World.world.game_stats.data.pixelsExploded += 1L;
		if (pOptions.explosion_pixel_effect)
		{
			World.world.explosion_layer.setDirty(pTile, pDist, pRadius);
		}
		tDamage -= (int)((double)tDamage * 0.5 * (double)Randy.random() * (double)tMode);
		if (pTile.Type.explodable && pTile.explosion_wave == 0)
		{
			World.world.explosion_layer.explodeBomb(pTile, false);
		}
		if (pTile.Type.explodable_delayed)
		{
			World.world.explosion_layer.activateDelayedBomb(pTile);
		}
		if (pTile.Type.strength <= pOptions.explode_strength)
		{
			MapAction.decreaseTile(pTile, true, TerraformLibrary.flash);
		}
		if (pTile.hasBuilding() && pTile.Type.liquid && !pTile.building.asset.can_be_placed_on_liquid)
		{
			pTile.building.startDestroyBuilding();
		}
		if (!wasLiquid)
		{
			pTile.setBurned(-1);
			if (pOptions.explode_and_set_random_fire)
			{
				if ((double)Randy.random() > 0.8)
				{
					pTile.startFire(true);
				}
				else
				{
					pTile.startFire(false);
				}
			}
		}
		return true;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000049A4 File Offset: 0x00002BA4
	public static void damageWorld(WorldTile pTile, int pRad, TerraformOptions pOptions, BaseSimObject pByWho = null)
	{
		if (pOptions.shake)
		{
			World.world.startShake(pOptions.shake_duration, pOptions.shake_interval, pOptions.shake_intensity, false, true);
		}
		if (pOptions.apply_force)
		{
			World.world.applyForceOnTile(pTile, pRad, pOptions.force_power, true, pOptions.damage, pOptions.ignore_kingdoms, pByWho, pOptions, false);
		}
		MapAction.applyTileDamage(pTile, (float)pRad, pOptions);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00004A0A File Offset: 0x00002C0A
	public static void makeTileChanged(WorldTile pTile)
	{
		World.world.resetRedrawTimer();
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00004A18 File Offset: 0x00002C18
	public static void removeLifeFromTile(WorldTile pTile)
	{
		World.world.conway_layer.remove(pTile);
		if (pTile.Type.life)
		{
			MapAction.decreaseTile(pTile, false, "destroy_life");
		}
		double tClickStartedAt = World.world.player_control.click_started_at;
		pTile.doUnits(delegate(Actor tActor)
		{
			if (!tActor.a.asset.can_be_killed_by_life_eraser)
			{
				return;
			}
			if (tActor.a.created_time_unscaled >= tClickStartedAt)
			{
				return;
			}
			AchievementLibrary.not_on_my_watch.check(tActor);
			tActor.applyRandomForce(1.5f, 2f);
			tActor.getHitFullHealth(AttackType.Divine);
		});
		HashSet<TornadoEffect> tEffects = TornadoEffect.getTornadoesFromTile(pTile);
		if (tEffects == null)
		{
			return;
		}
		using (ListPool<TornadoEffect> tEffectsList = new ListPool<TornadoEffect>(tEffects))
		{
			foreach (TornadoEffect ptr in tEffectsList)
			{
				ptr.die();
			}
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004AE4 File Offset: 0x00002CE4
	public static void createRoadTile(WorldTile pTile)
	{
		MapAction.terraformTop(pTile, TopTileLibrary.road, AssetManager.terraform.get("road"), false);
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00004B04 File Offset: 0x00002D04
	public static void createRoadTilesToBuild(List<WorldTile> pPath, WorldTile pFrom, WorldTile pTarget, bool pForceFinished = false)
	{
		if (pPath.Count > 20)
		{
			return;
		}
		if (pTarget.road_island != null && pTarget.road_island == pFrom.road_island)
		{
			return;
		}
		for (int i = 0; i < pPath.Count; i++)
		{
			WorldTile tTile = pPath[i];
			if (!tTile.Type.road)
			{
				if (pFrom != tTile && pFrom.road_island != null && tTile.road_island == pTarget.road_island)
				{
					return;
				}
				MapAction.temp_list_tiles_road_tiles_to_build.Add(tTile);
				if (pForceFinished)
				{
					MapAction.createRoadTile(tTile);
				}
			}
		}
		World.world.resetRedrawTimer();
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004B94 File Offset: 0x00002D94
	public static void makeRoadBetween(WorldTile pTile1, WorldTile pTile2, City pCity = null, bool pForceFinished = false)
	{
		if (pTile1.road_island != null && pTile1.road_island == pTile2.road_island)
		{
			return;
		}
		MapAction.temp_list_tiles_road_path.Clear();
		MapAction.temp_list_tiles_road_tiles_to_build.Clear();
		World.world.pathfinding_param.resetParam();
		World.world.pathfinding_param.roads = true;
		World.world.calcPath(pTile1, pTile2, MapAction.temp_list_tiles_road_path);
		MapAction.createRoadTilesToBuild(MapAction.temp_list_tiles_road_path, pTile1, pTile2, pForceFinished);
		if (pCity != null)
		{
			pCity.addRoads(MapAction.temp_list_tiles_road_tiles_to_build);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004C18 File Offset: 0x00002E18
	public static void tryRemoveTornadoFromTile(WorldTile pTile)
	{
		HashSet<TornadoEffect> tEffects = TornadoEffect.getTornadoesFromTile(pTile);
		if (tEffects == null)
		{
			return;
		}
		using (ListPool<TornadoEffect> tEffectsList = new ListPool<TornadoEffect>(tEffects))
		{
			foreach (TornadoEffect ptr in tEffectsList)
			{
				ptr.die();
			}
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00004C8C File Offset: 0x00002E8C
	public static void checkSantaHit(Vector2Int pPos, int pRad)
	{
		List<BaseEffect> tList = World.world.stack_effects.get("fx_santa").getList();
		for (int i = 0; i < tList.Count; i++)
		{
			Santa tSanta = tList[i].GetComponent<Santa>();
			if (tSanta.active && tSanta.alive)
			{
				Vector3 tSantaPos = tSanta.transform.localPosition;
				if (Toolbox.Dist((float)pPos.x, 0f, tSantaPos.x, 0f) <= (float)pRad && tSantaPos.y >= (float)pPos.y && tSantaPos.y - 20f <= (float)pPos.y)
				{
					tSanta.alive = false;
					AchievementLibrary.mayday.check(null);
				}
			}
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00004D50 File Offset: 0x00002F50
	public static void checkUFOHit(Vector2Int pPos, int pRad, Actor pActor)
	{
		Kingdom ufoKingdom = World.world.kingdoms_wild.get("aliens");
		if (ufoKingdom.units.Count == 0)
		{
			return;
		}
		List<Actor> list = ufoKingdom.units;
		for (int i = 0; i < list.Count; i++)
		{
			Actor tObject = list[i];
			if (tObject.isAlive())
			{
				Vector3 tCurrentPos = tObject.current_position;
				if (Toolbox.Dist((float)pPos.x, 0f, tCurrentPos.x, 0f) <= (float)pRad && tCurrentPos.y >= (float)pPos.y && tCurrentPos.y - 10f <= (float)pPos.y && tObject.asset.flag_ufo)
				{
					tObject.getHit((float)tObject.getHealth(), true, AttackType.Other, pActor, true, false, true);
				}
			}
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00004E28 File Offset: 0x00003028
	public static void checkTornadoHit(Vector2Int pPos, int pRad)
	{
		if (!World.world.stack_effects.get("fx_tornado").isAnyActive())
		{
			return;
		}
		using (ListPool<BaseEffect> tList = new ListPool<BaseEffect>(World.world.stack_effects.get("fx_tornado").getList()))
		{
			for (int i = 0; i < tList.Count; i++)
			{
				if (tList[i].active)
				{
					TornadoEffect tTornado = (TornadoEffect)tList[i];
					if (!tTornado.isKilled() && Toolbox.DistVec2Float(tTornado.transform.localPosition, pPos) <= (float)pRad)
					{
						tTornado.split();
					}
				}
			}
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00004EE8 File Offset: 0x000030E8
	public static void checkLightningAction(Vector2Int pPos, int pRad, Actor pActor = null, bool pCheckForImmortal = false, bool pCheckMayIInterrupt = false)
	{
		bool tImmortalGiven = false;
		int tRad = pRad * pRad;
		List<Actor> tList = World.world.units.getSimpleList();
		for (int i = 0; i < tList.Count; i++)
		{
			Actor tActor = tList[i];
			if (Toolbox.SquaredDistVec2(tActor.current_tile.pos, pPos) <= tRad)
			{
				if (tActor.asset.flag_finger)
				{
					tActor.getActorComponent<GodFinger>().lightAction();
					tActor.getHit(1f, true, AttackType.Other, pActor, true, false, true);
				}
				else
				{
					if (pCheckForImmortal && !tImmortalGiven && !tActor.hasTrait("immortal") && Randy.randomChance(0.2f))
					{
						tActor.addTrait("immortal", false);
						tActor.addTrait("energized", false);
						tImmortalGiven = true;
					}
					if (pCheckMayIInterrupt)
					{
						Achievement may_i_interrupt = AchievementLibrary.may_i_interrupt;
						BehaviourTaskActor task = tActor.ai.task;
						may_i_interrupt.checkBySignal((task != null) ? task.id : null);
					}
				}
			}
		}
	}

	// Token: 0x04000033 RID: 51
	private static List<WorldTile> temp_list_tiles_road_path = new List<WorldTile>();

	// Token: 0x04000034 RID: 52
	private static List<WorldTile> temp_list_tiles_road_tiles_to_build = new List<WorldTile>();
}
