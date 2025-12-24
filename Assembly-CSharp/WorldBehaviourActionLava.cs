using System;

// Token: 0x02000309 RID: 777
public class WorldBehaviourActionLava : WorldBehaviourTilesRunner
{
	// Token: 0x06001D4F RID: 7503 RVA: 0x00105FE7 File Offset: 0x001041E7
	public static void update()
	{
		if (!WorldBehaviourActionLava.hasLava())
		{
			return;
		}
		AchievementLibrary.the_hell.check(null);
		WorldBehaviourActionLava.updateSingleTiles();
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x00106004 File Offset: 0x00104204
	public static bool hasLava()
	{
		return TileLibrary.lava0.hashset.Count > 0 || TileLibrary.lava1.hashset.Count > 0 || TileLibrary.lava2.hashset.Count > 0 || TileLibrary.lava3.hashset.Count > 0;
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x00106064 File Offset: 0x00104264
	private static void updateSingleTiles()
	{
		WorldBehaviourTilesRunner.checkTiles();
		WorldTile[] tTilesToCheck = WorldBehaviourTilesRunner._tiles_to_check;
		int tMax = World.world.map_chunk_manager.amount_x * 15;
		if (WorldBehaviourTilesRunner._tile_next_check + tMax >= tTilesToCheck.Length)
		{
			tMax = tTilesToCheck.Length - WorldBehaviourTilesRunner._tile_next_check;
		}
		while (tMax-- > 0)
		{
			WorldBehaviourTilesRunner._tiles_to_check.ShuffleOne(WorldBehaviourTilesRunner._tile_next_check);
			WorldTile tTile = tTilesToCheck[WorldBehaviourTilesRunner._tile_next_check++];
			if (tTile.Type.lava)
			{
				WorldBehaviourActionLava.tryToMoveLava(tTile);
			}
		}
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x001060E4 File Offset: 0x001042E4
	private static void damageBuildingOnTile(WorldTile pTile)
	{
		if (pTile.hasBuilding() && pTile.building.asset.affected_by_lava)
		{
			float tDamage = (float)pTile.building.getMaxHealthPercent(0.4f);
			pTile.building.getHit(tDamage, true, AttackType.Other, null, true, false, true);
		}
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x00106130 File Offset: 0x00104330
	private static bool tryToMoveLava(WorldTile pTile)
	{
		bool tChanged = false;
		WorldBehaviourActionLava.damageBuildingOnTile(pTile);
		if (WorldLawLibrary.world_law_forever_lava.isEnabled())
		{
			return false;
		}
		if (World.world.getWorldTimeElapsedSince(pTile.timestamp_type_changed) < (float)pTile.Type.lava_change_state_after)
		{
			return false;
		}
		if (pTile.Type.lava_level == 0)
		{
			if (WorldLawLibrary.world_law_forever_lava.isEnabled())
			{
				return false;
			}
			bool tHasLava = false;
			foreach (WorldTile tTile in pTile.neighbours)
			{
				if (tTile.Type.lava && tTile.Type.lava_level > 0)
				{
					tHasLava = true;
					break;
				}
			}
			if (!tHasLava)
			{
				WorldBehaviourActionLava.changeLavaTile(pTile, TileLibrary.hills);
				tChanged = true;
			}
		}
		else if (Randy.randomBool())
		{
			tChanged = WorldBehaviourActionLava.moveLava(pTile);
			if (pTile.flash_state <= 0 && Randy.randomChance(0.995f))
			{
				pTile.startFire(true);
			}
		}
		return tChanged;
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x0010620C File Offset: 0x0010440C
	private static bool moveLava(WorldTile pLavaTile)
	{
		if (Randy.randomChance(0.1f))
		{
			World.world.particles_fire.spawn(pLavaTile.posV3);
		}
		WorldTile tTargetTile = null;
		foreach (WorldTile tTile in pLavaTile.neighbours.LoopRandom<WorldTile>())
		{
			if (!tTile.Type.lava)
			{
				if (tTile.isTemporaryFrozen())
				{
					World.world.heat.addTile(tTile, 15);
					tTile.unfreeze(99);
				}
				else
				{
					tTile.startFire(false);
				}
			}
			if (!tTile.Type.hold_lava && tTile.Type.lava_level != pLavaTile.Type.lava_level && (!tTile.Type.lava || !string.IsNullOrEmpty(tTile.Type.lava_increase)))
			{
				if (tTargetTile == null)
				{
					tTargetTile = tTile;
				}
				else if (tTargetTile.Type.render_z >= tTile.Type.render_z)
				{
					if (tTargetTile.Type.lava)
					{
						if (tTile.Type.lava_level < tTargetTile.Type.lava_level)
						{
							tTargetTile = tTile;
						}
					}
					else
					{
						tTargetTile = tTile;
					}
				}
			}
		}
		if (tTargetTile == null)
		{
			return false;
		}
		WorldBehaviourActionLava.changeLavaTile(pLavaTile, pLavaTile.Type.lava_decrease);
		if (!tTargetTile.Type.lava)
		{
			WorldBehaviourActionLava.changeLavaTile(tTargetTile, TileLibrary.lava0);
		}
		else
		{
			WorldBehaviourActionLava.changeLavaTile(tTargetTile, pLavaTile.Type.lava_increase);
		}
		World.world.flash_effects.flashPixel(tTargetTile, 10, ColorType.White);
		return true;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x001063A0 File Offset: 0x001045A0
	private static void changeLavaTile(WorldTile pTile, TileType pType)
	{
		if (pTile.Type == pType)
		{
			return;
		}
		MapAction.terraformMain(pTile, pType, TerraformLibrary.lava_damage, false);
		WorldBehaviourActionLava.damageBuildingOnTile(pTile);
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x001063BF File Offset: 0x001045BF
	private static void changeLavaTile(WorldTile pTile, string pType)
	{
		WorldBehaviourActionLava.changeLavaTile(pTile, AssetManager.tiles.get(pType));
	}

	// Token: 0x0400160B RID: 5643
	public const int CHUNK_MOD_AMOUNT = 15;
}
