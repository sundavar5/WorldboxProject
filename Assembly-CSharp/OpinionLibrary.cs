using System;

// Token: 0x02000067 RID: 103
public class OpinionLibrary : AssetLibrary<OpinionAsset>
{
	// Token: 0x06000389 RID: 905 RVA: 0x00020248 File Offset: 0x0001E448
	public override void init()
	{
		base.init();
		OpinionAsset opinionAsset = new OpinionAsset();
		opinionAsset.id = "opinion_king";
		opinionAsset.translation_key = "opinion_king";
		opinionAsset.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasKing())
			{
				tResult += (int)pTarget.king.stats["diplomacy"];
			}
			return tResult;
		};
		this.add(opinionAsset);
		OpinionAsset opinionAsset2 = new OpinionAsset();
		opinionAsset2.id = "opinion_kings_mood";
		opinionAsset2.translation_key = "opinion_kings_mood";
		opinionAsset2.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pMain.hasKing())
			{
				tResult = (int)pMain.king.stats["opinion"];
			}
			return tResult;
		};
		this.add(opinionAsset2);
		OpinionAsset opinionAsset3 = new OpinionAsset();
		opinionAsset3.id = "opinion_is_supreme";
		opinionAsset3.translation_key = "opinion_is_supreme";
		opinionAsset3.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.isSupreme() && World.world.kingdoms.Count >= 3)
			{
				tResult = -100;
			}
			return tResult;
		};
		this.add(opinionAsset3);
		OpinionAsset opinionAsset4 = new OpinionAsset();
		opinionAsset4.id = "opinion_borders";
		opinionAsset4.translation_key = "opinion_far_borders";
		opinionAsset4.translation_key_negative = "opinion_close_borders";
		opinionAsset4.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult;
			if (DiplomacyHelpers.areKingdomsClose(pMain, pTarget))
			{
				tResult = -25;
			}
			else
			{
				tResult = 25;
			}
			return tResult;
		};
		this.add(opinionAsset4);
		OpinionAsset opinionAsset5 = new OpinionAsset();
		opinionAsset5.id = "opinion_far_lands";
		opinionAsset5.translation_key = "opinion_far_lands";
		opinionAsset5.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (!DiplomacyHelpers.areKingdomsClose(pMain, pTarget) && pMain.hasCapital() && pTarget.hasCapital())
			{
				WorldTile tCapitalTile = pMain.capital.getTile(false);
				WorldTile tTargetTile = pTarget.capital.getTile(false);
				if (tCapitalTile != null && tTargetTile != null && !tCapitalTile.isSameIsland(tTargetTile))
				{
					tResult = 60;
				}
			}
			return tResult;
		};
		this.add(opinionAsset5);
		OpinionAsset opinionAsset6 = new OpinionAsset();
		opinionAsset6.id = "opinion_in_war";
		opinionAsset6.translation_key = "opinion_in_war";
		opinionAsset6.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pMain.isEnemy(pTarget))
			{
				tResult = -500;
			}
			return tResult;
		};
		this.add(opinionAsset6);
		OpinionAsset opinionAsset7 = new OpinionAsset();
		opinionAsset7.id = "opinion_same_wars";
		opinionAsset7.translation_key = "opinion_same_wars";
		opinionAsset7.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (pMain.isEnemy(pTarget))
			{
				return 0;
			}
			if (pMain.isInWarOnSameSide(pTarget))
			{
				return 50;
			}
			return 0;
		};
		this.add(opinionAsset7);
		OpinionAsset opinionAsset8 = new OpinionAsset();
		opinionAsset8.id = "opinion_species";
		opinionAsset8.translation_key = "opinion_same_species";
		opinionAsset8.translation_key_negative = "opinion_different_species";
		opinionAsset8.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			Actor tKing = pMain.king;
			Actor tKing2 = pTarget.king;
			if (tKing == null || tKing2 == null)
			{
				return 0;
			}
			if (!tKing.canHavePrejudice())
			{
				return 0;
			}
			int tResult;
			if (pMain.getSpecies() == pTarget.getSpecies())
			{
				tResult = 15;
			}
			else
			{
				tResult = -10;
			}
			return tResult;
		};
		this.add(opinionAsset8);
		OpinionAsset opinionAsset9 = new OpinionAsset();
		opinionAsset9.id = "opinion_zones";
		opinionAsset9.translation_key = "opinion_zones";
		opinionAsset9.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int zones_main = 0;
			int zones_target = 0;
			foreach (City tCity in pMain.getCities())
			{
				zones_main += tCity.zones.Count;
			}
			foreach (City tCity2 in pTarget.getCities())
			{
				zones_target += tCity2.zones.Count;
			}
			int tResult = (zones_main - zones_target) / 5;
			if (tResult > 0)
			{
				tResult = 0;
			}
			if (tResult < -20)
			{
				tResult = -20;
			}
			return tResult;
		};
		this.add(opinionAsset9);
		OpinionAsset opinionAsset10 = new OpinionAsset();
		opinionAsset10.id = "opinion_peace_time";
		opinionAsset10.translation_key = "opinion_peace_time";
		opinionAsset10.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (pMain.isEnemy(pTarget))
			{
				return 0;
			}
			DiplomacyRelation tRelation = World.world.diplomacy.getRelation(pMain, pTarget);
			if (tRelation == null)
			{
				return 0;
			}
			float tYearsSinceWar = (float)Date.getYearsSince(tRelation.data.timestamp_last_war_ended);
			if (tYearsSinceWar <= (float)SimGlobals.m.minimum_years_between_wars)
			{
				return 0;
			}
			int tResult = (int)tYearsSinceWar;
			if (tYearsSinceWar > 20f)
			{
				tResult = 20;
			}
			return tResult;
		};
		this.add(opinionAsset10);
		OpinionAsset opinionAsset11 = new OpinionAsset();
		opinionAsset11.id = "opinion_power";
		opinionAsset11.translation_key = "opinion_power";
		opinionAsset11.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			int power_rating_main = pMain.countCities() * 5 + pMain.getPopulationPeople();
			int power_rating_diff = pTarget.countCities() * 5 + pTarget.getPopulationPeople() - power_rating_main;
			if (power_rating_diff > 0)
			{
				tResult = power_rating_diff / 10;
			}
			return tResult;
		};
		this.add(opinionAsset11);
		OpinionAsset opinionAsset12 = new OpinionAsset();
		opinionAsset12.id = "opinion_loyalty_traits";
		opinionAsset12.translation_key = "opinion_traits";
		opinionAsset12.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasCulture() && pMain.hasKing() && pTarget.hasKing())
			{
				int tBonus = AssetManager.traits.checkTraitsMod(pTarget.king, pMain.king);
				tResult += tBonus;
			}
			return tResult;
		};
		this.add(opinionAsset12);
		OpinionAsset opinionAsset13 = new OpinionAsset();
		opinionAsset13.id = "opinion_culture";
		opinionAsset13.translation_key = "opinion_culture_same";
		opinionAsset13.translation_key_negative = "opinion_culture_different";
		opinionAsset13.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasCulture())
			{
				if (pTarget.getCulture() == pMain.getCulture())
				{
					tResult = 15;
				}
				else
				{
					tResult = -15;
				}
			}
			return tResult;
		};
		this.add(opinionAsset13);
		OpinionAsset opinionAsset14 = new OpinionAsset();
		opinionAsset14.id = "opinion_religion";
		opinionAsset14.translation_key = "opinion_religion_same";
		opinionAsset14.translation_key_negative = "opinion_religion_different";
		opinionAsset14.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasReligion())
			{
				if (pTarget.getReligion() == pMain.getReligion())
				{
					tResult = 15;
				}
				else
				{
					tResult = -15;
				}
			}
			return tResult;
		};
		this.add(opinionAsset14);
		OpinionAsset opinionAsset15 = new OpinionAsset();
		opinionAsset15.id = "opinion_language";
		opinionAsset15.translation_key = "opinion_language_same";
		opinionAsset15.translation_key_negative = "opinion_language_different";
		opinionAsset15.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasLanguage())
			{
				if (pTarget.getLanguage() == pMain.getLanguage())
				{
					tResult = 15;
				}
				else
				{
					tResult = -15;
				}
			}
			return tResult;
		};
		this.add(opinionAsset15);
		OpinionAsset opinionAsset16 = new OpinionAsset();
		opinionAsset16.id = "opinion_subspecies";
		opinionAsset16.translation_key = "opinion_subspecies_same";
		opinionAsset16.translation_key_negative = "opinion_subspecies_different";
		opinionAsset16.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (!pMain.hasKing())
			{
				return 0;
			}
			if (pMain.getSpecies() == pTarget.getSpecies())
			{
				return 0;
			}
			if (!pMain.king.canHavePrejudice())
			{
				return 0;
			}
			int tResult;
			if (pTarget.getMainSubspecies() == pMain.getMainSubspecies())
			{
				tResult = 10;
			}
			else
			{
				tResult = -10;
			}
			return tResult;
		};
		this.add(opinionAsset16);
		OpinionAsset opinionAsset17 = new OpinionAsset();
		opinionAsset17.id = "opinion_clan";
		opinionAsset17.translation_key = "opinion_same_clan";
		opinionAsset17.translation_key_negative = "opinion_different_clan";
		opinionAsset17.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			Actor tKing = pMain.king;
			Actor tKing2 = pTarget.king;
			if (tKing == null || tKing2 == null)
			{
				return 0;
			}
			if (tKing.subspecies != tKing2.subspecies)
			{
				return 0;
			}
			if (!tKing.hasClan() || !tKing2.hasClan())
			{
				return 0;
			}
			if (!tKing.canHavePrejudice())
			{
				return 0;
			}
			int tResult;
			if (tKing.clan == tKing2.clan)
			{
				tResult = 40;
			}
			else
			{
				tResult = -40;
			}
			return tResult;
		};
		this.add(opinionAsset17);
		OpinionAsset opinionAsset18 = new OpinionAsset();
		opinionAsset18.id = "opinion_in_alliance";
		opinionAsset18.translation_key = "opinion_in_alliance";
		opinionAsset18.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			int tResult = 0;
			if (pTarget.hasAlliance() && pMain.hasAlliance() && pTarget.getAlliance() == pMain.getAlliance())
			{
				tResult = 30;
			}
			return tResult;
		};
		this.add(opinionAsset18);
		OpinionAsset opinionAsset19 = new OpinionAsset();
		opinionAsset19.id = "opinion_truce";
		opinionAsset19.translation_key = "opinion_truce";
		opinionAsset19.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (pMain.isEnemy(pTarget))
			{
				return 0;
			}
			if ((float)Date.getYearsSince(World.world.diplomacy.getRelation(pMain, pTarget).data.timestamp_last_war_ended) <= (float)SimGlobals.m.minimum_years_between_wars)
			{
				return 100;
			}
			return 0;
		};
		this.add(opinionAsset19);
		OpinionAsset opinionAsset20 = new OpinionAsset();
		opinionAsset20.id = "opinion_world_era";
		opinionAsset20.translation_key = "opinion_world_era";
		opinionAsset20.calc = ((Kingdom pMain, Kingdom pTarget) => World.world_era.bonus_opinion);
		this.add(opinionAsset20);
		OpinionAsset opinionAsset21 = new OpinionAsset();
		opinionAsset21.id = "opinion_baby_king";
		opinionAsset21.translation_key = "opinion_baby_king";
		opinionAsset21.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (!pMain.hasKing())
			{
				return 0;
			}
			if (!pTarget.hasKing())
			{
				return 0;
			}
			if (pMain.king.isBaby())
			{
				return 0;
			}
			if (pTarget.king.isBaby())
			{
				return -50;
			}
			return 0;
		};
		this.add(opinionAsset21);
		OpinionAsset opinionAsset22 = new OpinionAsset();
		opinionAsset22.id = "opinion_ethnocentric_guard";
		opinionAsset22.translation_key = "opinion_ethnocentric_guard";
		opinionAsset22.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (!pMain.hasCulture())
			{
				return 0;
			}
			if (!pMain.culture.hasTrait("ethnocentric_guard"))
			{
				return 0;
			}
			if (pMain.getMainSubspecies() != pTarget.getMainSubspecies())
			{
				return 0;
			}
			if (pMain.culture == pTarget.culture)
			{
				return 0;
			}
			return -50;
		};
		this.add(opinionAsset22);
		OpinionAsset opinionAsset23 = new OpinionAsset();
		opinionAsset23.id = "opinion_xenophobic";
		opinionAsset23.translation_key = "opinion_xenophobic";
		opinionAsset23.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (!pMain.hasCulture())
			{
				return 0;
			}
			if (!pMain.culture.hasTrait("xenophobic"))
			{
				return 0;
			}
			if (pMain.getMainSubspecies() == pTarget.getMainSubspecies())
			{
				return 0;
			}
			return -50;
		};
		this.add(opinionAsset23);
		OpinionAsset opinionAsset24 = new OpinionAsset();
		opinionAsset24.id = "opinion_xenophiles";
		opinionAsset24.translation_key = "opinion_xenophiles";
		opinionAsset24.calc = delegate(Kingdom pMain, Kingdom pTarget)
		{
			if (!pMain.hasCulture())
			{
				return 0;
			}
			if (!pMain.culture.hasTrait("xenophiles"))
			{
				return 0;
			}
			if (pMain.getMainSubspecies() != pTarget.getMainSubspecies())
			{
				return 0;
			}
			return 20;
		};
		this.add(opinionAsset24);
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00020950 File Offset: 0x0001EB50
	public override void editorDiagnosticLocales()
	{
		foreach (OpinionAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
		base.editorDiagnosticLocales();
	}
}
