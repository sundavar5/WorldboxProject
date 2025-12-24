using System;

// Token: 0x020003FC RID: 1020
public static class CursedSacrifice
{
	// Token: 0x0600233D RID: 9021 RVA: 0x001260E4 File Offset: 0x001242E4
	public static void checkGoodForSacrifice(Actor pActor)
	{
		bool tGoodForForbiddenKnowledge = false;
		if (pActor.hasSubspecies())
		{
			bool tHasControlledStatus = pActor.hasStatus("magnetized") || pActor.hasStatus("strange_urge") || pActor.hasStatus("possessed");
			if (pActor.hasSubspeciesTrait("pure") && tHasControlledStatus)
			{
				tGoodForForbiddenKnowledge = true;
			}
		}
		if (tGoodForForbiddenKnowledge)
		{
			if (pActor.asset.id == "elf")
			{
				World.world.game_stats.data.elvesSacrificed += 1L;
				CursedSacrifice._latest_sacrificed_was_egg = pActor.isEgg();
				CursedSacrifice.spawnVoidElves();
			}
			World.world.game_stats.data.creaturesSacrificed += 1L;
			CursedSacrifice.countSacrifice();
		}
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x001261A0 File Offset: 0x001243A0
	public static void spawnVoidElves()
	{
		Subspecies tTargetSubspecies = CursedSacrifice.getVoidElvesSubspecies();
		if (tTargetSubspecies == null)
		{
			return;
		}
		TileZone tVisibleZone = World.world.zone_camera.getVisibleZones().GetRandom<TileZone>();
		if (tVisibleZone == null)
		{
			return;
		}
		WorldTile tRandomTile = tVisibleZone.getRandomTile();
		World.world.units.spawnNewUnit("elf", tRandomTile, false, true, 6f, tTargetSubspecies, true, true);
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x001261F8 File Offset: 0x001243F8
	private static Subspecies getVoidElvesSubspecies()
	{
		Subspecies result;
		using (ListPool<Subspecies> tVoidElvesPot = new ListPool<Subspecies>())
		{
			ActorAsset tAssetElf = AssetManager.actor_library.get("elf");
			foreach (Subspecies tSubspecies in World.world.subspecies)
			{
				if (tSubspecies.getActorAsset() == tAssetElf && tSubspecies.hasTrait("mutation_skin_void"))
				{
					tVoidElvesPot.Add(tSubspecies);
				}
			}
			Subspecies tTargetSubspecies;
			if (tVoidElvesPot.Count == 0)
			{
				WorldTile tRandomGroundTile = World.world.islands_calculator.tryGetRandomGround();
				if (tRandomGroundTile == null)
				{
					return null;
				}
				Subspecies subspecies = World.world.subspecies.newSpecies(tAssetElf, tRandomGroundTile, false);
				subspecies.addTrait("mutation_skin_void", false);
				subspecies.addTrait("gift_of_void", false);
				subspecies.addTrait("reproduction_soulborne", false);
				subspecies.addTrait("big_stomach", false);
				subspecies.addTrait("voracious", false);
				subspecies.addTrait("genetic_mirror", false);
				subspecies.addTrait("genetic_psychosis", false);
				subspecies.addTrait("enhanced_strength", false);
				subspecies.addTrait("cold_resistance", false);
				subspecies.addTrait("heat_resistance", false);
				subspecies.addTrait("adaptation_corruption", false);
				subspecies.addTrait("adaptation_desert", false);
				subspecies.addTrait("hovering", false);
				subspecies.removeTrait("pure");
				subspecies.removeTrait("prefrontal_cortex");
				subspecies.removeTrait("advanced_hippocampus");
				subspecies.removeTrait("amygdala");
				subspecies.removeTrait("wernicke_area");
				subspecies.addBirthTrait("desire_harp");
				subspecies.addBirthTrait("evil");
				subspecies.data.name = "Elfus Voidus";
				tTargetSubspecies = subspecies;
			}
			else
			{
				tTargetSubspecies = tVoidElvesPot.GetRandom<Subspecies>();
			}
			result = tTargetSubspecies;
		}
		return result;
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x001263FC File Offset: 0x001245FC
	public static void countAllSacrificesDebug()
	{
		for (int i = 0; i < 314; i++)
		{
			CursedSacrifice.countSacrifice();
		}
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x00126420 File Offset: 0x00124620
	private static void countSacrifice()
	{
		if (CursedSacrifice._current_sacrifice_count == 314)
		{
			return;
		}
		CursedSacrifice._current_sacrifice_count++;
		int tNewMessageIndex = (int)(CursedSacrifice.getCurseProgressRatio() * 9f);
		if (tNewMessageIndex > CursedSacrifice._last_message_index)
		{
			CursedSacrifice._last_message_index = tNewMessageIndex;
			string tColorHex = "#F3961F";
			if (CursedSacrifice._last_message_index > 6)
			{
				tColorHex = "#FF637D";
			}
			if (CursedSacrifice._last_message_index == 9)
			{
				tColorHex = "#E060CD";
			}
			WorldTip.showNow("world_curse_message_" + CursedSacrifice._last_message_index.ToString(), true, "top", 3f, tColorHex);
			int last_message_index = CursedSacrifice._last_message_index;
			World.world.startShake(0.3f + (float)CursedSacrifice._last_message_index * 0.1f, 0.01f, 0.23f + (float)CursedSacrifice._last_message_index * 0.02f, true, true);
		}
	}

	// Token: 0x06002342 RID: 9026 RVA: 0x001264E9 File Offset: 0x001246E9
	public static float getCurseProgressRatio()
	{
		return (float)CursedSacrifice._current_sacrifice_count / 314f;
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x001264F7 File Offset: 0x001246F7
	public static float getCurseProgressRatioForBlackhole()
	{
		if (AchievementLibrary.isUnlocked("achievementCursedWorld"))
		{
			return 1f;
		}
		return (float)CursedSacrifice._current_sacrifice_count / 314f;
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x00126517 File Offset: 0x00124717
	public static void reset()
	{
		CursedSacrifice._current_sacrifice_count = 0;
		CursedSacrifice._last_message_index = 0;
		CursedSacrifice._latest_sacrificed_was_egg = false;
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x0012652B File Offset: 0x0012472B
	private static int getCurrentSacrificeCount()
	{
		return CursedSacrifice._current_sacrifice_count;
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x00126532 File Offset: 0x00124732
	public static void loadAlreadyCursedState()
	{
		CursedSacrifice._current_sacrifice_count = 314;
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x0012653E File Offset: 0x0012473E
	public static bool isWorldReadyForCURSE()
	{
		return AchievementLibrary.isUnlocked("achievementCursedWorld") || CursedSacrifice.isAllSacrificesDone();
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x00126553 File Offset: 0x00124753
	public static bool isAllSacrificesDone()
	{
		return CursedSacrifice.getCurrentSacrificeCount() >= 314;
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x00126564 File Offset: 0x00124764
	public static void justCursedWorld()
	{
		if (!Config.hasPremium)
		{
			return;
		}
		CursedSacrifice._cursed_world_timestamp = World.world.getCurSessionTime();
		AchievementLibrary.cursed_world.check(null);
	}

	// Token: 0x0600234A RID: 9034 RVA: 0x00126589 File Offset: 0x00124789
	public static bool justGotCursedWorld()
	{
		return World.world.getRealTimeElapsedSince(CursedSacrifice._cursed_world_timestamp) < 1f;
	}

	// Token: 0x0600234B RID: 9035 RVA: 0x001265A1 File Offset: 0x001247A1
	public static bool isLatestWasEgg()
	{
		return CursedSacrifice._latest_sacrificed_was_egg;
	}

	// Token: 0x04001987 RID: 6535
	private const int SACRIFICE_COUNT = 314;

	// Token: 0x04001988 RID: 6536
	private const int MAX_MESSAGES = 9;

	// Token: 0x04001989 RID: 6537
	private static int _last_message_index = -1;

	// Token: 0x0400198A RID: 6538
	private static int _current_sacrifice_count = 0;

	// Token: 0x0400198B RID: 6539
	private static double _cursed_world_timestamp = 0.0;

	// Token: 0x0400198C RID: 6540
	private static bool _latest_sacrificed_was_egg;
}
