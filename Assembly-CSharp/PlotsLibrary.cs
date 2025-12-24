using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class PlotsLibrary : BaseLibraryWithUnlockables<PlotAsset>
{
	// Token: 0x06000A12 RID: 2578 RVA: 0x00093418 File Offset: 0x00091618
	private static bool tryStartReligionRiteOnEnemyCity(Actor pActor, PlotAsset pPlotAsset, bool pForced)
	{
		bool result;
		using (ListPool<Kingdom> tEnemies = pActor.kingdom.getEnemiesKingdoms())
		{
			if (tEnemies.Count == 0)
			{
				result = false;
			}
			else
			{
				Kingdom tEnemy = tEnemies.GetRandom<Kingdom>();
				if (!tEnemy.hasCities())
				{
					result = false;
				}
				else
				{
					World.world.plots.newPlot(pActor, pPlotAsset, pForced).target_city = tEnemy.getCities().GetRandom<City>();
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00093494 File Offset: 0x00091694
	private static bool tryStartCauseRebellion(Actor pActor, PlotAsset pPlotAsset, bool pForced)
	{
		bool result;
		using (ListPool<Kingdom> tEnemies = pActor.kingdom.getEnemiesKingdoms())
		{
			if (tEnemies.Count == 0)
			{
				result = false;
			}
			else
			{
				Kingdom tEnemy = tEnemies.GetRandom<Kingdom>();
				if (!tEnemy.hasCities())
				{
					result = false;
				}
				else
				{
					using (ListPool<City> tCities = new ListPool<City>())
					{
						foreach (City tCity in tEnemy.getCities())
						{
							if (!tCity.isCapitalCity())
							{
								tCities.Add(tCity);
							}
						}
						if (tCities.Count == 0)
						{
							result = false;
						}
						else
						{
							World.world.plots.newPlot(pActor, pPlotAsset, pForced).target_city = tCities.GetRandom<City>();
							result = true;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00093584 File Offset: 0x00091784
	private static bool tryStartReligionRiteOnSelfCity(Actor pActor, PlotAsset pPlotAsset, bool pForced)
	{
		World.world.plots.newPlot(pActor, pPlotAsset, pForced).target_city = pActor.city;
		return true;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000935A4 File Offset: 0x000917A4
	private static bool tryStartReligionRiteOnSelfKingdom(Actor pActor, PlotAsset pPlotAsset, bool pForced)
	{
		World.world.plots.newPlot(pActor, pPlotAsset, pForced).target_kingdom = pActor.kingdom;
		return true;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000935C4 File Offset: 0x000917C4
	private static bool checkShouldContinueReligionRiteOnEnemyCity(Actor pActor)
	{
		if (!pActor.hasKingdom())
		{
			return false;
		}
		if (!pActor.hasReligion())
		{
			return false;
		}
		if (!pActor.kingdom.hasEnemies())
		{
			return false;
		}
		City tTarget = pActor.plot.target_city;
		return tTarget != null && tTarget.isAlive() && tTarget.hasKingdom() && tTarget.kingdom.isEnemy(pActor.kingdom);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00093630 File Offset: 0x00091830
	private static bool summonCloudAction(Actor pActor, string pCloud, int pMin, int pMax, float pLifespanMin, float pLifespanMax)
	{
		City target_city = pActor.plot.target_city;
		int tMinX = MapBox.width;
		int tMinY = MapBox.height;
		int tMaxY = 0;
		foreach (TileZone tZone in target_city.zones)
		{
			if (tZone.centerTile.x < tMinX)
			{
				tMinX = tZone.centerTile.x;
			}
			if (tZone.centerTile.y < tMinY)
			{
				tMinY = tZone.centerTile.y;
			}
			if (tZone.centerTile.y > tMaxY)
			{
				tMaxY = tZone.centerTile.y;
			}
		}
		int tAmount = Randy.randomInt(pMin, pMax + 1);
		int tYStep = (tMaxY - tMinY) / tAmount;
		for (int i = 0; i < tAmount; i++)
		{
			int tXOffset = Randy.randomInt(-7, 8);
			int tX = Mathf.Clamp(tMinX + tXOffset, 0, MapBox.width - 1);
			int tY = tMinY + tYStep * i;
			WorldTile tTargetTile = MapBox.instance.GetTileSimple(tX, tY);
			((Cloud)EffectsLibrary.spawn("fx_cloud", tTargetTile, pCloud, null, 0f, -1f, -1f, null)).setLifespan(Randy.randomFloat(pLifespanMin, pLifespanMax));
		}
		return true;
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00093774 File Offset: 0x00091974
	private static bool summonUnitsAction(Actor pActor, string pActorAssetId, int pMin, int pMax, bool pJoinCasters)
	{
		City tTarget = pActor.plot.target_city;
		if (tTarget == null)
		{
			return false;
		}
		int tAmount = Randy.randomInt(pMin, pMax + 1);
		for (int i = 0; i < tAmount; i++)
		{
			WorldTile tTargetTile = tTarget.zones.GetRandom<TileZone>().getRandomTile();
			Actor tSummonedUnit = World.world.units.createNewUnit(pActorAssetId, tTargetTile, false, 0f, null, null, true, true, false, pJoinCasters);
			EffectsLibrary.spawn("fx_spawn", tTargetTile, null, null, 0f, -1f, -1f, null);
			if (pJoinCasters && tSummonedUnit != null && pActor.isKingdomCiv())
			{
				if (tSummonedUnit.subspecies.isJustCreated())
				{
					tSummonedUnit.subspecies.addTrait("prefrontal_cortex", false);
				}
				tSummonedUnit.joinKingdom(pActor.kingdom);
			}
		}
		return true;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0009383C File Offset: 0x00091A3C
	private static bool afterRiteWorldTransform(Actor pActor)
	{
		if (WorldLawLibrary.world_law_gaias_covenant.isEnabled())
		{
			return false;
		}
		Religion tReligion = pActor.religion;
		if (tReligion == null)
		{
			return false;
		}
		bool result;
		using (ListPool<ReligionTrait> tTraits = new ListPool<ReligionTrait>())
		{
			foreach (ReligionTrait tTrait in tReligion.getTraits())
			{
				if (!(tTrait.group_id != "transformation"))
				{
					tTraits.Add(tTrait);
				}
			}
			if (tTraits.Count == 0)
			{
				result = false;
			}
			else
			{
				ReligionTrait tTransformTrait = tTraits.GetRandom<ReligionTrait>();
				TileTypeBase tSpreadType = AssetManager.biome_library.get(tTransformTrait.transformation_biome_id).getTileHigh();
				WorldTile tTile = World.world.islands_calculator.getRandomIslandGround(true).getRandomTile();
				WorldBehaviourActionBiomes.trySpreadBiomeAround(tTile, tSpreadType, false, false, false, false);
				BrushData brushData = Brush.get(2, "special_");
				int tPixelsAmount = brushData.pos.Length;
				int tGrowSize = Randy.randomInt((int)((float)tPixelsAmount * 0.25f), (int)((float)tPixelsAmount * 0.5f));
				using (ListPool<BrushPixelData> tPositions = new ListPool<BrushPixelData>(brushData.pos))
				{
					for (int i = 0; i < tGrowSize; i++)
					{
						BrushPixelData tData = tPositions.GetRandom<BrushPixelData>();
						tPositions.Remove(tData);
						WorldTile tTileToGrow = MapBox.instance.GetTile(tTile.x + tData.x, tTile.y + tData.y);
						if (tTileToGrow != null)
						{
							WorldBehaviourActionBiomes.trySpreadBiomeAround(tTileToGrow, tSpreadType, false, false, true, false);
						}
					}
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00093A08 File Offset: 0x00091C08
	public override void init()
	{
		Debug.Log("Init PlotLibrary");
		base.init();
		this.addBasic();
		this.addBasicMetas();
		this.addMagicRites();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00093A2C File Offset: 0x00091C2C
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (PlotAsset tPlotAsset in this.list)
		{
			if (tPlotAsset.is_basic_plot)
			{
				this.basic_plots.Add(tPlotAsset);
			}
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00093A94 File Offset: 0x00091C94
	public override PlotAsset add(PlotAsset pAsset)
	{
		base.add(pAsset);
		pAsset.get_formatted_description = new PlotDescription(PlotsLibrary.getDescriptionGeneric);
		return pAsset;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00093AB4 File Offset: 0x00091CB4
	public override void editorDiagnosticLocales()
	{
		foreach (PlotAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
			this.checkLocale(tAsset, tAsset.getDescriptionID2());
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00093B30 File Offset: 0x00091D30
	private static string getDescriptionGeneric(Plot pPlot)
	{
		string pString = pPlot.getAsset().getDescriptionID().Localize();
		Actor tLeader = pPlot.getAuthor();
		string pString2 = PlotsLibrary.tryToReplace(pString, "$initiator_actor$", (tLeader != null) ? tLeader.name : null);
		string pReplaceID = "$initiator_kingdom$";
		string pName;
		if (tLeader == null)
		{
			pName = null;
		}
		else
		{
			Kingdom kingdom = tLeader.kingdom;
			pName = ((kingdom != null) ? kingdom.name : null);
		}
		string pString3 = PlotsLibrary.tryToReplace(pString2, pReplaceID, pName);
		string pReplaceID2 = "$initiator_city$";
		string pName2;
		if (tLeader == null)
		{
			pName2 = null;
		}
		else
		{
			City city = tLeader.city;
			pName2 = ((city != null) ? city.name : null);
		}
		string pString4 = PlotsLibrary.tryToReplace(pString3, pReplaceID2, pName2);
		string pReplaceID3 = "$target_kingdom$";
		Kingdom target_kingdom = pPlot.target_kingdom;
		string pString5 = PlotsLibrary.tryToReplace(pString4, pReplaceID3, (target_kingdom != null) ? target_kingdom.name : null);
		string pReplaceID4 = "$target_alliance$";
		Alliance target_alliance = pPlot.target_alliance;
		string pString6 = PlotsLibrary.tryToReplace(pString5, pReplaceID4, (target_alliance != null) ? target_alliance.name : null);
		string pReplaceID5 = "$target_war$";
		War target_war = pPlot.target_war;
		return PlotsLibrary.tryToReplace(pString6, pReplaceID5, (target_war != null) ? target_war.name : null);
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00093C02 File Offset: 0x00091E02
	private static string tryToReplace(string pString, string pReplaceID, string pName)
	{
		if (pString.Contains(pReplaceID))
		{
			pString = pString.Replace(pReplaceID, pName);
		}
		return pString;
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00093C18 File Offset: 0x00091E18
	public void addBasic()
	{
		PlotAsset plotAsset = new PlotAsset();
		plotAsset.id = "rebellion";
		plotAsset.is_basic_plot = true;
		plotAsset.path_icon = "plots/icons/plot_rebellion";
		plotAsset.group_id = "diplomacy";
		plotAsset.min_level = 2;
		plotAsset.money_cost = 15;
		plotAsset.min_stewardship = 6;
		plotAsset.min_warfare = 5;
		plotAsset.can_be_done_by_leader = true;
		plotAsset.check_target_kingdom = true;
		plotAsset.requires_diplomacy = true;
		plotAsset.requires_rebellion = true;
		plotAsset.check_is_possible = delegate(Actor pActor)
		{
			Kingdom tKingdom = pActor.kingdom;
			if (pActor.isKing())
			{
				return false;
			}
			if (tKingdom.countCities() == 1)
			{
				return false;
			}
			if (!tKingdom.hasCapital())
			{
				return false;
			}
			if (tKingdom.getAge() <= SimGlobals.m.rebellions_min_age)
			{
				return false;
			}
			if (pActor.hasTrait("pacifist"))
			{
				return false;
			}
			City tCity = pActor.city;
			if (tCity.isCapitalCity())
			{
				return false;
			}
			if (tCity.isHappy())
			{
				return false;
			}
			float tArmyCity = (float)tCity.countWarriors();
			float tArmyCapital = (float)tKingdom.capital.countWarriors();
			if (tArmyCity < (float)SimGlobals.m.rebellions_min_warriors)
			{
				return false;
			}
			float tArmyMultiplier = (float)tKingdom.countUnhappyCities() * SimGlobals.m.rebellions_unhappy_multiplier;
			float tPowerArmy = tArmyCapital - tArmyCapital * tArmyMultiplier;
			if (tArmyCity < tPowerArmy)
			{
				return false;
			}
			foreach (War tWar in tKingdom.getWars(false))
			{
				if (tWar.getAsset() == WarTypeLibrary.rebellion && tWar.isMainDefender(tKingdom))
				{
					return false;
				}
			}
			int tCities = tKingdom.countCities();
			if (tCities <= 6)
			{
				int tMaxCities = tKingdom.getMaxCities();
				if (tCities <= tMaxCities)
				{
					return false;
				}
			}
			foreach (Plot tPlot in World.world.plots)
			{
				if (tPlot.isActive() && tPlot.isSameType(PlotsLibrary.rebellion) && tPlot.target_kingdom == tKingdom)
				{
					return false;
				}
			}
			return true;
		};
		plotAsset.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			Plot plot = World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			plot.target_kingdom = pActor.kingdom;
			if (!plot.checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotRebellion is missing start requirements");
				return true;
			}
			return true;
		};
		plotAsset.check_other_plots = ((Actor pActor, Plot pPlot) => pActor.kingdom == pPlot.target_kingdom);
		plotAsset.check_can_be_forced = delegate(Actor pActor)
		{
			if (pActor.isKing())
			{
				return false;
			}
			Kingdom tKingdom = pActor.kingdom;
			return !pActor.city.isCapitalCity() && tKingdom.countCities() != 1;
		};
		plotAsset.check_should_continue = ((Actor pActor) => pActor.hasCity() && pActor.isCityLeader() && !pActor.isKing() && !pActor.city.isCapitalCity());
		plotAsset.action = delegate(Actor pActor)
		{
			bool tCheckForHappiness = !pActor.plot.data.forced;
			DiplomacyHelpersRebellion.startRebellion(pActor, pActor.plot, tCheckForHappiness);
			return true;
		};
		PlotAsset pAsset = plotAsset;
		this.t = plotAsset;
		PlotsLibrary.rebellion = this.add(pAsset);
		PlotAsset plotAsset2 = new PlotAsset();
		plotAsset2.id = "new_war";
		plotAsset2.is_basic_plot = true;
		plotAsset2.path_icon = "plots/icons/plot_new_war";
		plotAsset2.group_id = "diplomacy";
		plotAsset2.min_level = 3;
		plotAsset2.min_warfare = 6;
		plotAsset2.money_cost = 20;
		plotAsset2.min_renown_kingdom = 50;
		plotAsset2.can_be_done_by_king = true;
		plotAsset2.check_target_kingdom = true;
		plotAsset2.requires_diplomacy = true;
		plotAsset2.check_is_possible = delegate(Actor pActor)
		{
			Kingdom tKingdom = pActor.kingdom;
			if (!DiplomacyHelpers.isWarNeeded(tKingdom))
			{
				return false;
			}
			if (pActor.hasCulture() && pActor.culture.hasTrait("serenity_now"))
			{
				return false;
			}
			if (pActor.hasTrait("pacifist"))
			{
				return false;
			}
			if (tKingdom.hasAlliance())
			{
				foreach (Kingdom iKingdom in tKingdom.getAlliance().kingdoms_hashset)
				{
					if (iKingdom != tKingdom && iKingdom.hasKing())
					{
						Actor tKing = iKingdom.king;
						if (tKing.hasPlot() && tKing.plot.isSameType(PlotsLibrary.new_war))
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		};
		plotAsset2.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			Kingdom tPossibleTarget = DiplomacyHelpers.getWarTarget(pActor.kingdom);
			if (tPossibleTarget == null)
			{
				return false;
			}
			Plot plot = World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			plot.target_kingdom = tPossibleTarget;
			if (!plot.checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotWar is missing start requirements");
				return true;
			}
			return true;
		};
		plotAsset2.check_should_continue = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			return tPlot.target_kingdom.isAlive() && (!pActor.kingdom.hasAlliance() || pActor.kingdom.getAlliance() != tPlot.target_kingdom.getAlliance()) && DiplomacyHelpers.isWarNeeded(pActor.kingdom);
		};
		plotAsset2.action = delegate(Actor pActor)
		{
			World.world.diplomacy.startWar(pActor.kingdom, pActor.plot.target_kingdom, WarTypeLibrary.normal, true);
			return true;
		};
		pAsset = plotAsset2;
		this.t = plotAsset2;
		this.add(pAsset);
		PlotAsset plotAsset3 = new PlotAsset();
		plotAsset3.id = "alliance_create";
		plotAsset3.is_basic_plot = true;
		plotAsset3.path_icon = "plots/icons/plot_alliance_create";
		plotAsset3.group_id = "diplomacy";
		plotAsset3.min_level = 5;
		plotAsset3.money_cost = 20;
		plotAsset3.min_renown_kingdom = 60;
		plotAsset3.can_be_done_by_king = true;
		plotAsset3.requires_diplomacy = true;
		plotAsset3.check_is_possible = delegate(Actor pActor)
		{
			Kingdom tKingdom = pActor.kingdom;
			return !tKingdom.hasAlliance() && !tKingdom.hasEnemies() && !tKingdom.isSupreme() && Date.getYearsSince(tKingdom.data.timestamp_alliance) >= SimGlobals.m.alliance_timeout && !World.world.plots.isPlotTypeAlreadyRunning(pActor, PlotsLibrary.alliance_create);
		};
		plotAsset3.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			if (DiplomacyHelpers.getAllianceTarget(pActor.kingdom) == null)
			{
				return false;
			}
			if (!World.world.plots.newPlot(pActor, pPlotAsset, pForced).checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotNewAlliance is missing start requirements");
				return true;
			}
			return true;
		};
		plotAsset3.check_other_plots = delegate(Actor pActor, Plot pPlot)
		{
			Kingdom tActorKingdom = pActor.kingdom;
			using (List<Actor>.Enumerator enumerator = pPlot.units.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (DiplomacyHelpers.areKingdomsClose(enumerator.Current.kingdom, tActorKingdom))
					{
						return true;
					}
				}
			}
			return false;
		};
		plotAsset3.check_can_be_forced = ((Actor pActor) => !pActor.kingdom.hasAlliance());
		plotAsset3.check_should_continue = delegate(Actor pActor)
		{
			foreach (Actor actor in pActor.plot.units)
			{
				Kingdom tKingdom = actor.kingdom;
				if (!tKingdom.isAlive())
				{
					return false;
				}
				if (tKingdom.hasEnemies())
				{
					return false;
				}
				if (tKingdom != pActor.kingdom && !tKingdom.isOpinionTowardsKingdomGood(pActor.kingdom))
				{
					return false;
				}
			}
			return true;
		};
		plotAsset3.action = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			Kingdom tPossibleTarget = DiplomacyHelpers.getAllianceTarget(pActor.kingdom);
			if (tPossibleTarget == null)
			{
				return false;
			}
			bool result;
			using (ListPool<Kingdom> tListKingdoms = new ListPool<Kingdom>(World.world.kingdoms.Count))
			{
				tListKingdoms.Add(pActor.kingdom);
				tListKingdoms.Add(tPossibleTarget);
				Kingdom tK = tListKingdoms.Shift<Kingdom>();
				Kingdom tK2 = tListKingdoms.Shift<Kingdom>();
				if (!tK.isOpinionTowardsKingdomGood(tK2))
				{
					result = false;
				}
				else
				{
					Alliance tAlliance = World.world.alliances.newAlliance(tK, tK2);
					if (tK.hasKing())
					{
						tK.king.leavePlot();
					}
					if (tK2.hasKing())
					{
						tK2.king.leavePlot();
					}
					foreach (Kingdom ptr in tListKingdoms)
					{
						Kingdom tKingdom = ptr;
						tAlliance.join(tKingdom, false, false);
					}
					foreach (Actor actor in tPlot.units)
					{
						actor.leavePlot();
					}
					tAlliance.recalculate();
					result = true;
				}
			}
			return result;
		};
		pAsset = plotAsset3;
		this.t = plotAsset3;
		PlotsLibrary.alliance_create = this.add(pAsset);
		PlotAsset plotAsset4 = new PlotAsset();
		plotAsset4.id = "alliance_join";
		plotAsset4.is_basic_plot = true;
		plotAsset4.path_icon = "plots/icons/plot_alliance_create";
		plotAsset4.group_id = "diplomacy";
		plotAsset4.min_level = 2;
		plotAsset4.money_cost = 5;
		plotAsset4.min_diplomacy = 5;
		plotAsset4.min_renown_kingdom = 50;
		plotAsset4.can_be_done_by_king = true;
		plotAsset4.check_target_alliance = true;
		plotAsset4.requires_diplomacy = true;
		plotAsset4.check_is_possible = delegate(Actor pActor)
		{
			Kingdom tKingdom = pActor.kingdom;
			return !tKingdom.isSupreme() && !tKingdom.hasAlliance() && !tKingdom.hasEnemies() && Date.getYearsSince(tKingdom.data.timestamp_alliance) >= SimGlobals.m.alliance_timeout && World.world.alliances.anyAlliances();
		};
		plotAsset4.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			Kingdom tCurKingdom = pActor.kingdom;
			int power = tCurKingdom.power;
			Alliance tPossibleTarget = null;
			foreach (Alliance tAlliance in World.world.alliances.list.LoopRandom<Alliance>())
			{
				if (!tAlliance.hasWars())
				{
					if (pForced)
					{
						tPossibleTarget = tAlliance;
						break;
					}
					if (tAlliance.canJoin(tCurKingdom) && !tAlliance.hasSupremeKingdom())
					{
						int power2 = tAlliance.power;
						bool tGoodTarget = false;
						if (tCurKingdom.countCities() <= 2 && !tCurKingdom.hasNearbyKingdoms())
						{
							tGoodTarget = true;
						}
						if (!tGoodTarget && tAlliance.hasSharedBordersWithKingdom(tCurKingdom))
						{
							tGoodTarget = true;
						}
						if (tGoodTarget)
						{
							tPossibleTarget = tAlliance;
						}
					}
				}
			}
			if (tPossibleTarget == null)
			{
				return false;
			}
			Plot tPlot = World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			tPlot.target_alliance = tPossibleTarget;
			if (!tPlot.checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotJoinAlliance is missing start requirements");
				return true;
			}
			pActor.setPlot(tPlot);
			return true;
		};
		plotAsset4.check_other_plots = PlotsLibrary.alliance_create.check_other_plots;
		plotAsset4.check_can_be_forced = ((Actor pActor) => !pActor.kingdom.hasAlliance());
		plotAsset4.check_should_continue = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			return !pActor.kingdom.hasAlliance() && tPlot.target_alliance.isAlive() && tPlot.target_alliance.canJoin(pActor.kingdom) && !pActor.kingdom.hasEnemies() && !tPlot.target_alliance.hasWars();
		};
		plotAsset4.action = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			if (pActor.kingdom.hasAlliance())
			{
				return false;
			}
			if (!tPlot.target_alliance.isAlive())
			{
				return false;
			}
			tPlot.target_alliance.join(pActor.kingdom, true, false);
			if (pActor.kingdom.hasKing())
			{
				pActor.kingdom.king.leavePlot();
			}
			return true;
		};
		pAsset = plotAsset4;
		this.t = plotAsset4;
		this.add(pAsset);
		PlotAsset plotAsset5 = new PlotAsset();
		plotAsset5.id = "alliance_destroy";
		plotAsset5.is_basic_plot = true;
		plotAsset5.path_icon = "plots/icons/plot_alliance_destroy";
		plotAsset5.group_id = "diplomacy";
		plotAsset5.min_level = 5;
		plotAsset5.money_cost = 15;
		plotAsset5.can_be_done_by_king = true;
		plotAsset5.check_target_alliance = true;
		plotAsset5.requires_diplomacy = true;
		plotAsset5.check_is_possible = delegate(Actor pActor)
		{
			Kingdom tKingdom = pActor.kingdom;
			if (!tKingdom.hasAlliance())
			{
				return false;
			}
			if (tKingdom.hasEnemies())
			{
				return false;
			}
			Alliance tAlliance = tKingdom.getAlliance();
			if (tAlliance.isForcedType())
			{
				return false;
			}
			if ((float)Date.getYearsSince(tAlliance.data.timestamp_member_joined) < 10f)
			{
				return false;
			}
			if (!tKingdom.isSupreme() && !tKingdom.isSecondBest())
			{
				return false;
			}
			if (tAlliance.getAge() < 10)
			{
				return false;
			}
			float tAlliancePower = (float)tAlliance.power;
			float tOtherWorldPowers = 0f;
			foreach (Kingdom iKingdom in World.world.kingdoms)
			{
				if (iKingdom.getAlliance() != tAlliance)
				{
					tOtherWorldPowers += (float)iKingdom.power;
				}
			}
			if (tAlliancePower < tOtherWorldPowers * 2f)
			{
				return false;
			}
			using (HashSet<Kingdom>.Enumerator enumerator2 = tAlliance.kingdoms_hashset.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.isSupreme())
					{
						return true;
					}
				}
			}
			return false;
		};
		plotAsset5.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			Kingdom tCurKingdom = pActor.kingdom;
			if (!tCurKingdom.hasAlliance())
			{
				return false;
			}
			Plot plot = World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			plot.target_alliance = tCurKingdom.getAlliance();
			if (!plot.checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotDisbandAlliance is missing start requirements");
				return true;
			}
			return true;
		};
		plotAsset5.check_should_continue = ((Actor pActor) => pActor.kingdom.hasAlliance());
		plotAsset5.action = delegate(Actor pActor)
		{
			if (!pActor.kingdom.hasAlliance())
			{
				return false;
			}
			Alliance tAlliance = pActor.kingdom.getAlliance();
			World.world.alliances.dissolveAlliance(tAlliance);
			return true;
		};
		pAsset = plotAsset5;
		this.t = plotAsset5;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_should_continue;
		PlotAsset plotAsset6 = new PlotAsset();
		plotAsset6.id = "attacker_stop_war";
		plotAsset6.is_basic_plot = true;
		plotAsset6.path_icon = "plots/icons/plot_stop_war";
		plotAsset6.group_id = "diplomacy";
		plotAsset6.min_level = 4;
		plotAsset6.money_cost = 20;
		plotAsset6.min_diplomacy = 8;
		plotAsset6.can_be_done_by_king = true;
		plotAsset6.check_target_war = true;
		plotAsset6.requires_diplomacy = true;
		plotAsset6.check_is_possible = ((Actor pActor) => pActor.kingdom.hasEnemies());
		plotAsset6.try_to_start_advanced = delegate(Actor pActor, PlotAsset pPlotAsset, bool pForced)
		{
			Kingdom tInitiatorKingdom = pActor.kingdom;
			War tTargetWar = null;
			foreach (War tWar in tInitiatorKingdom.getWars(true))
			{
				if (tWar.getAge() > SimGlobals.m.minimum_age_before_war_stop && tWar.getAsset().can_end_with_plot && tWar.isAttacker(tInitiatorKingdom))
				{
					int tPowerAttackers = tWar.countAttackersWarriors();
					int tPowerDefenders = tWar.countDefendersWarriors();
					if (!tWar.areDefendersGettingCaptured() && !tWar.areAttackersAttackingAnotherCity() && !tWar.areAttackersGettingCaptured() && !tWar.areDefendersAttackingAnotherCity() && (tPowerAttackers <= 100 || tPowerDefenders <= 100))
					{
						if (tWar.isAttacker(tInitiatorKingdom))
						{
							if (tPowerAttackers < tPowerDefenders * 2)
							{
								continue;
							}
						}
						else if (tPowerDefenders < tPowerAttackers * 2)
						{
							continue;
						}
						tTargetWar = tWar;
						break;
					}
				}
			}
			if (tTargetWar == null)
			{
				return false;
			}
			foreach (Plot iPlot in World.world.plots)
			{
				if (iPlot.isActive() && iPlot.isSameType(pPlotAsset) && iPlot.target_war == tTargetWar)
				{
					pActor.setPlot(iPlot);
					return true;
				}
			}
			Plot plot = World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			plot.target_war = tTargetWar;
			if (!plot.checkInitiatorAndTargets())
			{
				Debug.Log("tryPlotStopWar is missing start requirements");
				return true;
			}
			return true;
		};
		plotAsset6.check_can_be_forced = ((Actor pActor) => pActor.kingdom.hasEnemies());
		plotAsset6.check_should_continue = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			War tWar = tPlot.target_war;
			return tWar != null && !tWar.isRekt() && !tPlot.target_war.hasEnded();
		};
		plotAsset6.action = delegate(Actor pActor)
		{
			Plot tPlot = pActor.plot;
			if (tPlot.target_war.hasEnded())
			{
				return false;
			}
			World.world.wars.endWar(tPlot.target_war, WarWinner.Peace);
			return true;
		};
		pAsset = plotAsset6;
		this.t = plotAsset6;
		this.add(pAsset);
		PlotAsset plotAsset7 = new PlotAsset();
		plotAsset7.id = "new_book";
		plotAsset7.is_basic_plot = true;
		plotAsset7.path_icon = "plots/icons/plot_new_book";
		plotAsset7.group_id = "plots_others";
		plotAsset7.can_be_done_by_king = true;
		plotAsset7.can_be_done_by_leader = true;
		plotAsset7.can_be_done_by_clan_member = true;
		plotAsset7.progress_needed = 200f;
		plotAsset7.min_level = 10;
		plotAsset7.money_cost = 200;
		plotAsset7.min_intelligence = 3;
		plotAsset7.pot_rate = 5;
		plotAsset7.check_is_possible = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.hasLanguage() && pActor.city.hasBookSlots());
		plotAsset7.check_should_continue = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.hasLanguage() && pActor.city.hasBookSlots());
		plotAsset7.action = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.city.hasBookSlots())
			{
				return false;
			}
			World.world.books.generateNewBook(pActor);
			return true;
		};
		pAsset = plotAsset7;
		this.t = plotAsset7;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0009442C File Offset: 0x0009262C
	public void addBasicMetas()
	{
		PlotAsset plotAsset = new PlotAsset();
		plotAsset.id = "new_language";
		plotAsset.is_basic_plot = true;
		plotAsset.path_icon = "plots/icons/plot_new_language";
		plotAsset.group_id = "language";
		plotAsset.priority = 99;
		plotAsset.min_level = 1;
		plotAsset.money_cost = 0;
		plotAsset.min_intelligence = 3;
		plotAsset.can_be_done_by_king = true;
		plotAsset.can_be_done_by_leader = true;
		plotAsset.check_is_possible = ((Actor pActor) => !pActor.hasLanguage() && pActor.subspecies.has_advanced_communication);
		plotAsset.check_should_continue = ((Actor pActor) => !pActor.hasLanguage() && pActor.subspecies.has_advanced_communication);
		plotAsset.action = delegate(Actor pActor)
		{
			Language tLanguage = World.world.languages.newLanguage(pActor, true);
			pActor.joinLanguage(tLanguage);
			tLanguage.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		PlotAsset pAsset = plotAsset;
		this.t = plotAsset;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.subspecies.has_advanced_communication);
		PlotAsset plotAsset2 = new PlotAsset();
		plotAsset2.id = "new_religion";
		plotAsset2.path_icon = "plots/icons/plot_new_religion";
		plotAsset2.group_id = "religion";
		plotAsset2.priority = 99;
		plotAsset2.min_level = 5;
		plotAsset2.money_cost = 25;
		plotAsset2.min_intelligence = 8;
		plotAsset2.is_basic_plot = true;
		plotAsset2.can_be_done_by_king = true;
		plotAsset2.can_be_done_by_leader = true;
		plotAsset2.check_is_possible = ((Actor pActor) => !pActor.hasReligion() && pActor.hasCulture() && pActor.hasLanguage() && pActor.subspecies.has_advanced_memory && pActor.culture.countUnits() >= 50);
		plotAsset2.check_should_continue = ((Actor pActor) => !pActor.hasReligion() && pActor.subspecies.has_advanced_memory);
		plotAsset2.action = delegate(Actor pActor)
		{
			Religion tReligion = World.world.religions.newReligion(pActor, true);
			pActor.setReligion(tReligion);
			tReligion.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		pAsset = plotAsset2;
		this.t = plotAsset2;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.subspecies.has_advanced_memory);
		PlotAsset plotAsset3 = new PlotAsset();
		plotAsset3.id = "new_culture";
		plotAsset3.is_basic_plot = true;
		plotAsset3.path_icon = "plots/icons/plot_new_culture";
		plotAsset3.group_id = "culture";
		plotAsset3.priority = 99;
		plotAsset3.min_level = 1;
		plotAsset3.money_cost = 0;
		plotAsset3.min_intelligence = 2;
		plotAsset3.can_be_done_by_king = true;
		plotAsset3.can_be_done_by_leader = true;
		plotAsset3.check_is_possible = ((Actor pActor) => !pActor.hasCulture() && pActor.subspecies.has_advanced_memory);
		plotAsset3.check_should_continue = ((Actor pActor) => !pActor.hasCulture() && pActor.subspecies.has_advanced_memory);
		plotAsset3.action = delegate(Actor pActor)
		{
			Culture tCulture = World.world.cultures.newCulture(pActor, true);
			pActor.setCulture(tCulture);
			tCulture.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		pAsset = plotAsset3;
		this.t = plotAsset3;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.subspecies.has_advanced_memory);
		PlotAsset plotAsset4 = new PlotAsset();
		plotAsset4.id = "clan_ascension";
		plotAsset4.path_icon = "plots/icons/plot_clan_ascension";
		plotAsset4.can_be_done_by_king = true;
		plotAsset4.group_id = "rites_various";
		plotAsset4.min_level = 7;
		plotAsset4.money_cost = 100;
		plotAsset4.min_intelligence = 5;
		plotAsset4.check_is_possible = ((Actor pActor) => pActor.hasReligion() && pActor.hasClan() && !pActor.clan.hasTrait("mark_of_becoming") && pActor.clan.hasChief() && pActor.clan.getChief() == pActor && pActor.kingdom.getPopulationPeople() >= 500 && pActor.hasTrait("evil") && !pActor.hasSubspeciesTrait("pure"));
		plotAsset4.check_should_continue = ((Actor pActor) => pActor.hasClan());
		plotAsset4.action = delegate(Actor pActor)
		{
			Clan tClan = pActor.clan;
			Actor tNewEvolvedActor;
			if (!ActionLibrary.tryToEvolveUnitViaAscension(pActor, out tNewEvolvedActor))
			{
				return false;
			}
			tClan.addTrait("mark_of_becoming", false);
			Subspecies tNewSubspecies = tNewEvolvedActor.subspecies;
			if (tClan != null)
			{
				foreach (Actor actor in tClan.units)
				{
					actor.setSubspecies(tNewSubspecies);
				}
			}
			return true;
		};
		plotAsset4.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset4;
		this.t = plotAsset4;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => !pActor.hasSubspeciesTrait("pure"));
		PlotAsset plotAsset5 = new PlotAsset();
		plotAsset5.id = "culture_divide";
		plotAsset5.is_basic_plot = true;
		plotAsset5.path_icon = "plots/icons/plot_culture_divide";
		plotAsset5.group_id = "culture";
		plotAsset5.can_be_done_by_leader = true;
		plotAsset5.min_level = 5;
		plotAsset5.money_cost = 20;
		plotAsset5.min_intelligence = 6;
		plotAsset5.check_is_possible = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.hasCulture())
			{
				return false;
			}
			if (!pActor.hasLanguage())
			{
				return false;
			}
			if (pActor.isOneCityKingdom())
			{
				return false;
			}
			Culture tCulture = pActor.culture;
			return tCulture.countUnits() >= SimGlobals.m.people_before_meta_divide && tCulture.getAge() >= SimGlobals.m.years_before_meta_divide && !tCulture.hasTrait("legacy_keepers") && pActor.wantsToSplitMeta() && tCulture == pActor.kingdom.culture && pActor.subspecies.has_advanced_memory && tCulture.data.creator_kingdom_id != pActor.kingdom.id && tCulture.data.creator_clan_id != pActor.clan.id;
		};
		plotAsset5.check_should_continue = ((Actor pActor) => pActor.hasCulture() && pActor.subspecies.has_advanced_memory);
		plotAsset5.action = delegate(Actor pActor)
		{
			Culture tOldCulture = pActor.culture;
			Culture tNewCulture = World.world.cultures.newCulture(pActor, false);
			tNewCulture.cloneAndEvolveOnomastics(tOldCulture);
			tNewCulture.copyTraits(tOldCulture.getTraits());
			pActor.setCulture(tNewCulture);
			tNewCulture.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		pAsset = plotAsset5;
		this.t = plotAsset5;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.hasLanguage() && pActor.subspecies.has_advanced_memory);
		PlotAsset plotAsset6 = new PlotAsset();
		plotAsset6.id = "religion_schism";
		plotAsset6.is_basic_plot = true;
		plotAsset6.path_icon = "plots/icons/plot_religion_schism";
		plotAsset6.can_be_done_by_leader = true;
		plotAsset6.group_id = "religion";
		plotAsset6.min_level = 5;
		plotAsset6.money_cost = 15;
		plotAsset6.min_intelligence = 6;
		plotAsset6.check_is_possible = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.hasCulture())
			{
				return false;
			}
			if (!pActor.hasLanguage())
			{
				return false;
			}
			if (!pActor.hasReligion())
			{
				return false;
			}
			if (pActor.isOneCityKingdom())
			{
				return false;
			}
			if (!pActor.wantsToSplitMeta())
			{
				return false;
			}
			if (pActor.culture.hasTrait("legacy_keepers"))
			{
				return false;
			}
			Religion tReligion = pActor.religion;
			return tReligion.countUnits() >= SimGlobals.m.people_before_meta_divide && tReligion.getAge() >= SimGlobals.m.years_before_meta_divide && tReligion == pActor.kingdom.religion && pActor.subspecies.has_advanced_memory && tReligion.data.creator_kingdom_id != pActor.kingdom.id && tReligion.data.creator_clan_id != pActor.clan.id;
		};
		plotAsset6.check_should_continue = ((Actor pActor) => pActor.hasReligion() && pActor.subspecies.has_advanced_memory);
		plotAsset6.action = delegate(Actor pActor)
		{
			Religion tOldReligion = pActor.religion;
			Religion tNewReligion = World.world.religions.newReligion(pActor, false);
			tNewReligion.copyTraits(tOldReligion.getTraits());
			pActor.setReligion(tNewReligion);
			tNewReligion.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		pAsset = plotAsset6;
		this.t = plotAsset6;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.hasLanguage() && pActor.hasReligion() && pActor.religion == pActor.kingdom.religion && pActor.subspecies.has_advanced_memory);
		PlotAsset plotAsset7 = new PlotAsset();
		plotAsset7.id = "language_divergence";
		plotAsset7.is_basic_plot = true;
		plotAsset7.path_icon = "plots/icons/plot_language_divergence";
		plotAsset7.can_be_done_by_leader = true;
		plotAsset7.group_id = "language";
		plotAsset7.min_level = 5;
		plotAsset7.money_cost = 15;
		plotAsset7.min_intelligence = 8;
		plotAsset7.check_is_possible = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.hasCulture())
			{
				return false;
			}
			if (!pActor.hasLanguage())
			{
				return false;
			}
			if (!pActor.hasReligion())
			{
				return false;
			}
			if (pActor.isOneCityKingdom())
			{
				return false;
			}
			if (!pActor.wantsToSplitMeta())
			{
				return false;
			}
			if (pActor.culture.hasTrait("legacy_keepers"))
			{
				return false;
			}
			Language tLanguage = pActor.language;
			return tLanguage.countUnits() >= SimGlobals.m.people_before_meta_divide && tLanguage.getAge() >= SimGlobals.m.years_before_meta_divide && tLanguage == pActor.kingdom.language && pActor.subspecies.has_advanced_communication && tLanguage.data.creator_kingdom_id != pActor.kingdom.id && tLanguage.data.creator_clan_id != pActor.clan.id;
		};
		plotAsset7.check_should_continue = ((Actor pActor) => pActor.hasLanguage() && pActor.subspecies.has_advanced_communication);
		plotAsset7.action = delegate(Actor pActor)
		{
			Language tOldLanguage = pActor.language;
			Language tNewLanguage = World.world.languages.newLanguage(pActor, false);
			tNewLanguage.copyTraits(tOldLanguage.getTraits());
			pActor.joinLanguage(tNewLanguage);
			tNewLanguage.forceConvertSameSpeciesAroundUnit(pActor);
			return true;
		};
		pAsset = plotAsset7;
		this.t = plotAsset7;
		this.add(pAsset);
		this.t.check_can_be_forced = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.hasLanguage() && pActor.hasReligion() && pActor.subspecies.has_advanced_communication);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00094B14 File Offset: 0x00092D14
	public void addMagicRites()
	{
		PlotAsset plotAsset = new PlotAsset();
		plotAsset.id = "summon_meteor_rain";
		plotAsset.path_icon = "plots/icons/plot_summon_meteor_rain";
		plotAsset.group_id = "rites_wrathful";
		plotAsset.can_be_done_by_king = true;
		plotAsset.can_be_done_by_leader = true;
		plotAsset.check_target_city = true;
		plotAsset.progress_needed = 200f;
		plotAsset.min_level = 13;
		plotAsset.money_cost = 1000;
		plotAsset.check_is_possible = ((Actor pActor) => pActor.kingdom.hasEnemies());
		plotAsset.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset.action = delegate(Actor pActor)
		{
			City tTarget = pActor.plot.target_city;
			int pMinInclusive = 3;
			int tMeteoritesMax = 7;
			int tAmount = Randy.randomInt(pMinInclusive, tMeteoritesMax + 1);
			for (int i = 0; i < tAmount; i++)
			{
				Meteorite.spawnMeteoriteDisaster(tTarget.zones.GetRandom<TileZone>().getRandomTile(), pActor);
			}
			return true;
		};
		plotAsset.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		PlotAsset pAsset = plotAsset;
		this.t = plotAsset;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset2 = new PlotAsset();
		plotAsset2.id = "summon_earthquake";
		plotAsset2.path_icon = "plots/icons/plot_summon_earthquake";
		plotAsset2.group_id = "rites_wrathful";
		plotAsset2.can_be_done_by_king = true;
		plotAsset2.can_be_done_by_leader = true;
		plotAsset2.check_target_city = true;
		plotAsset2.progress_needed = 200f;
		plotAsset2.min_level = 13;
		plotAsset2.money_cost = 600;
		plotAsset2.pot_rate = 2;
		plotAsset2.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset2.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset2.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset2.action = delegate(Actor pActor)
		{
			Earthquake.startQuake(pActor.plot.target_city.zones.GetRandom<TileZone>().getRandomTile(), EarthquakeType.SmallDisaster);
			return true;
		};
		plotAsset2.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset2;
		this.t = plotAsset2;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset3 = new PlotAsset();
		plotAsset3.id = "summon_thunderstorm";
		plotAsset3.path_icon = "plots/icons/plot_summon_thunderstorm";
		plotAsset3.group_id = "rites_wrathful";
		plotAsset3.can_be_done_by_king = true;
		plotAsset3.can_be_done_by_leader = true;
		plotAsset3.check_target_city = true;
		plotAsset3.progress_needed = 200f;
		plotAsset3.min_level = 13;
		plotAsset3.money_cost = 500;
		plotAsset3.pot_rate = 4;
		plotAsset3.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset3.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset3.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset3.action = delegate(Actor pActor)
		{
			City tTarget = pActor.plot.target_city;
			if (!tTarget.hasZones())
			{
				return false;
			}
			WorldTile tTargetTile = tTarget.zones.GetRandom<TileZone>().getRandomTile();
			MapBox.spawnLightningMedium(tTargetTile, 0.15f, pActor);
			int tAmount = Randy.randomInt(5, 13);
			for (int i = 0; i < tAmount; i++)
			{
				WorldTile tTargetTileL = Toolbox.getRandomTileWithinDistance(tTargetTile, 10);
				float tDelay = Randy.randomFloat(0.1f, 0.75f);
				DelayedActionsManager.addAction(delegate
				{
					MapBox.spawnLightningMedium(tTargetTileL, 0.15f, pActor);
				}, tDelay * (float)i, true);
			}
			return true;
		};
		plotAsset3.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset3;
		this.t = plotAsset3;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset4 = new PlotAsset();
		plotAsset4.id = "summon_stormfront";
		plotAsset4.path_icon = "plots/icons/plot_summon_stormfront";
		plotAsset4.group_id = "rites_wrathful";
		plotAsset4.can_be_done_by_king = true;
		plotAsset4.can_be_done_by_leader = true;
		plotAsset4.check_target_city = true;
		plotAsset4.progress_needed = 100f;
		plotAsset4.min_level = 13;
		plotAsset4.money_cost = 200;
		plotAsset4.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset4.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset4.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset4.action = ((Actor pActor) => PlotsLibrary.summonCloudAction(pActor, "cloud_lightning", 5, 15, 20f, 60f));
		plotAsset4.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset4;
		this.t = plotAsset4;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset5 = new PlotAsset();
		plotAsset5.id = "summon_hellstorm";
		plotAsset5.path_icon = "plots/icons/plot_summon_hellstorm";
		plotAsset5.group_id = "rites_wrathful";
		plotAsset5.can_be_done_by_king = true;
		plotAsset5.can_be_done_by_leader = true;
		plotAsset5.check_target_city = true;
		plotAsset5.progress_needed = 100f;
		plotAsset5.min_level = 13;
		plotAsset5.money_cost = 300;
		plotAsset5.pot_rate = 4;
		plotAsset5.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset5.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset5.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset5.action = ((Actor pActor) => PlotsLibrary.summonCloudAction(pActor, "cloud_fire", 3, 5, 20f, 60f));
		plotAsset5.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset5;
		this.t = plotAsset5;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset6 = new PlotAsset();
		plotAsset6.id = "summon_demons";
		plotAsset6.path_icon = "plots/icons/plot_summon_demons";
		plotAsset6.group_id = "rites_summoning";
		plotAsset6.can_be_done_by_king = true;
		plotAsset6.can_be_done_by_leader = true;
		plotAsset6.check_target_city = true;
		plotAsset6.progress_needed = 100f;
		plotAsset6.min_level = 13;
		plotAsset6.money_cost = 300;
		plotAsset6.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset6.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset6.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset6.action = delegate(Actor pActor)
		{
			if (Randy.randomChance(0.0666f))
			{
				pActor.plot.target_city = pActor.city;
			}
			return PlotsLibrary.summonUnitsAction(pActor, "demon", 5, 10, false);
		};
		plotAsset6.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset6;
		this.t = plotAsset6;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset7 = new PlotAsset();
		plotAsset7.id = "summon_angles";
		plotAsset7.path_icon = "plots/icons/plot_summon_angles";
		plotAsset7.group_id = "rites_summoning";
		plotAsset7.can_be_done_by_king = true;
		plotAsset7.can_be_done_by_leader = true;
		plotAsset7.check_target_city = true;
		plotAsset7.progress_needed = 100f;
		plotAsset7.min_level = 12;
		plotAsset7.money_cost = 200;
		plotAsset7.pot_rate = 2;
		plotAsset7.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset7.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset7.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset7.action = ((Actor pActor) => PlotsLibrary.summonUnitsAction(pActor, "angle", 3, 7, true));
		plotAsset7.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset7;
		this.t = plotAsset7;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset8 = new PlotAsset();
		plotAsset8.id = "summon_skeletons";
		plotAsset8.path_icon = "plots/icons/plot_summon_skeletons";
		plotAsset8.group_id = "rites_summoning";
		plotAsset8.can_be_done_by_king = true;
		plotAsset8.can_be_done_by_leader = true;
		plotAsset8.check_target_city = true;
		plotAsset8.progress_needed = 100f;
		plotAsset8.min_level = 13;
		plotAsset8.money_cost = 150;
		plotAsset8.pot_rate = 2;
		plotAsset8.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset8.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset8.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset8.action = ((Actor pActor) => PlotsLibrary.summonUnitsAction(pActor, "skeleton", 6, 13, true));
		plotAsset8.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset8;
		this.t = plotAsset8;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset9 = new PlotAsset();
		plotAsset9.id = "summon_living_plants";
		plotAsset9.path_icon = "plots/icons/plot_summon_living_plants";
		plotAsset9.group_id = "rites_summoning";
		plotAsset9.can_be_done_by_king = true;
		plotAsset9.can_be_done_by_leader = true;
		plotAsset9.check_target_city = true;
		plotAsset9.progress_needed = 100f;
		plotAsset9.min_level = 13;
		plotAsset9.money_cost = 400;
		plotAsset9.pot_rate = 2;
		plotAsset9.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies() && AssetManager.actor_library.get("living_plants").units.Count <= 100);
		plotAsset9.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset9.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset9.action = delegate(Actor pActor)
		{
			foreach (WorldTile tFarmField in pActor.plot.target_city.calculated_farm_fields)
			{
				if (tFarmField.hasBuilding())
				{
					ActionLibrary.tryToMakeFloraAlive(tFarmField.building, false);
				}
			}
			return true;
		};
		plotAsset9.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset9;
		this.t = plotAsset9;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset10 = new PlotAsset();
		plotAsset10.id = "big_cast_coffee";
		plotAsset10.path_icon = "plots/icons/plot_big_cast_coffee";
		plotAsset10.group_id = "rites_merciful";
		plotAsset10.can_be_done_by_king = true;
		plotAsset10.can_be_done_by_leader = true;
		plotAsset10.check_target_city = true;
		plotAsset10.progress_needed = 60f;
		plotAsset10.min_level = 9;
		plotAsset10.money_cost = 100;
		plotAsset10.pot_rate = 2;
		plotAsset10.check_is_possible = ((Actor pActor) => pActor.hasCity());
		plotAsset10.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnSelfCity);
		plotAsset10.check_should_continue = ((Actor pActor) => pActor.hasCity());
		plotAsset10.action = delegate(Actor pActor)
		{
			foreach (Actor actor in pActor.plot.target_city.getUnits())
			{
				actor.addStatusEffect("caffeinated", 0f, true);
			}
			return true;
		};
		plotAsset10.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset10;
		this.t = plotAsset10;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset11 = new PlotAsset();
		plotAsset11.id = "big_cast_bubble_shield";
		plotAsset11.path_icon = "plots/icons/plot_big_cast_bubble_shield";
		plotAsset11.group_id = "rites_merciful";
		plotAsset11.can_be_done_by_king = true;
		plotAsset11.can_be_done_by_leader = true;
		plotAsset11.check_target_kingdom = true;
		plotAsset11.progress_needed = 50f;
		plotAsset11.min_level = 11;
		plotAsset11.money_cost = 200;
		plotAsset11.pot_rate = 3;
		plotAsset11.check_is_possible = ((Actor pActor) => pActor.hasKingdom());
		plotAsset11.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnSelfKingdom);
		plotAsset11.check_should_continue = ((Actor pActor) => pActor.hasKingdom());
		plotAsset11.action = delegate(Actor pActor)
		{
			foreach (Actor tActor in pActor.plot.target_kingdom.getUnits())
			{
				if (tActor.isProfession(UnitProfession.Warrior))
				{
					tActor.addStatusEffect("shield", 0f, true);
				}
			}
			return true;
		};
		plotAsset11.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset11;
		this.t = plotAsset11;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset12 = new PlotAsset();
		plotAsset12.id = "big_cast_madness";
		plotAsset12.path_icon = "plots/icons/plot_big_cast_madness";
		plotAsset12.group_id = "rites_wrathful";
		plotAsset12.can_be_done_by_king = true;
		plotAsset12.can_be_done_by_leader = true;
		plotAsset12.check_target_city = true;
		plotAsset12.progress_needed = 120f;
		plotAsset12.min_level = 11;
		plotAsset12.money_cost = 600;
		plotAsset12.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset12.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset12.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset12.action = delegate(Actor pActor)
		{
			MetaObject<CityData> target_city = pActor.plot.target_city;
			float tChance = 0.8f;
			foreach (Actor tActor in target_city.getUnits())
			{
				if (Randy.randomChance(tChance))
				{
					tActor.addTrait("madness", false);
				}
			}
			return true;
		};
		plotAsset12.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset12;
		this.t = plotAsset12;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset13 = new PlotAsset();
		plotAsset13.id = "big_cast_slowness";
		plotAsset13.path_icon = "plots/icons/plot_big_cast_slowness";
		plotAsset13.group_id = "rites_wrathful";
		plotAsset13.can_be_done_by_king = true;
		plotAsset13.can_be_done_by_leader = true;
		plotAsset13.check_target_city = true;
		plotAsset13.pot_rate = 3;
		plotAsset13.progress_needed = 70f;
		plotAsset13.min_level = 11;
		plotAsset13.money_cost = 150;
		plotAsset13.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset13.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartReligionRiteOnEnemyCity);
		plotAsset13.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset13.action = delegate(Actor pActor)
		{
			foreach (Actor tActor in pActor.plot.target_city.getUnits())
			{
				if (tActor.isProfession(UnitProfession.Warrior))
				{
					tActor.addStatusEffect("slowness", 0f, true);
				}
			}
			return true;
		};
		plotAsset13.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset13;
		this.t = plotAsset13;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
		PlotAsset plotAsset14 = new PlotAsset();
		plotAsset14.id = "cause_rebellion";
		plotAsset14.path_icon = "plots/icons/plot_cause_rebellion";
		plotAsset14.group_id = "rites_wrathful";
		plotAsset14.can_be_done_by_king = true;
		plotAsset14.can_be_done_by_leader = true;
		plotAsset14.check_target_city = true;
		plotAsset14.pot_rate = 2;
		plotAsset14.progress_needed = 70f;
		plotAsset14.min_level = 12;
		plotAsset14.money_cost = 400;
		plotAsset14.check_is_possible = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.hasEnemies());
		plotAsset14.try_to_start_advanced = new PlotTryToStart(PlotsLibrary.tryStartCauseRebellion);
		plotAsset14.check_should_continue = new PlotCheckerDelegate(PlotsLibrary.checkShouldContinueReligionRiteOnEnemyCity);
		plotAsset14.action = delegate(Actor pActor)
		{
			City tTarget = pActor.plot.target_city;
			if (tTarget.isCapitalCity())
			{
				return false;
			}
			if (tTarget.units.Count == 0)
			{
				return false;
			}
			Actor tActor = tTarget.getUnits().GetRandom<Actor>();
			if (!tActor.isAlive())
			{
				return false;
			}
			DiplomacyHelpersRebellion.startRebellion(tActor, pActor.plot, false);
			return true;
		};
		plotAsset14.post_action = new PlotAction(PlotsLibrary.afterRiteWorldTransform);
		pAsset = plotAsset14;
		this.t = plotAsset14;
		this.add(pAsset);
		this.t.check_can_be_forced = this.t.check_is_possible;
	}

	// Token: 0x040009F5 RID: 2549
	public static PlotAsset rebellion;

	// Token: 0x040009F6 RID: 2550
	public static PlotAsset new_war;

	// Token: 0x040009F7 RID: 2551
	public static PlotAsset alliance_create;

	// Token: 0x040009F8 RID: 2552
	[NonSerialized]
	public readonly List<PlotAsset> basic_plots = new List<PlotAsset>();
}
