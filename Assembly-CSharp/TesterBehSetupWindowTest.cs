using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004DF RID: 1247
public class TesterBehSetupWindowTest : BehaviourActionTester
{
	// Token: 0x06002A13 RID: 10771 RVA: 0x0014AFAC File Offset: 0x001491AC
	public override BehResult execute(AutoTesterBot pObject)
	{
		List<WindowAsset> tWindows = AssetManager.window_library.getTestableWindows();
		if (this.currentWindow >= tWindows.Count)
		{
			this.currentWindow = 0;
		}
		List<WindowAsset> list = tWindows;
		int num = this.currentWindow;
		this.currentWindow = num + 1;
		string id = list[num].id;
		this.pickTheBest();
		SelectedMetas.selected_city = this.city;
		SelectedMetas.selected_clan = this.clan;
		SelectedMetas.selected_plot = this.plot;
		SelectedMetas.selected_alliance = this.alliance;
		SelectedMetas.selected_war = this.war;
		SelectedMetas.selected_kingdom = this.kingdom;
		SelectedMetas.selected_culture = this.culture;
		if (!this.unit.isRekt())
		{
			SelectedUnit.select(this.unit, true);
		}
		else
		{
			SelectedUnit.clear();
		}
		Config.current_brush = Brush.getRandom();
		Config.power_to_unlock = GodPower.premium_powers.Find((GodPower tPower) => tPower.id == "cybercore");
		Config.selected_trait_editor = PowerLibrary.traits_delta_rain_edit.id;
		SaveManager.setCurrentSlot(1);
		if (id.Contains("workshop"))
		{
			SaveManager.currentWorkshopMapData = WorkshopMapData.currentMapToWorkshop();
		}
		return BehResult.Continue;
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x0014B0D0 File Offset: 0x001492D0
	private void pickTheBest()
	{
		List<City> tCities = new List<City>();
		tCities.AddRange(BehaviourActionBase<AutoTesterBot>.world.cities);
		tCities.Sort(new Comparison<City>(ComponentListBase<CityListElement, City, CityData, CityListComponent>.sortByPopulation));
		this.city = Randy.getRandom<City>(tCities);
		List<Clan> tClans = new List<Clan>();
		tClans.AddRange(BehaviourActionBase<AutoTesterBot>.world.clans);
		tClans.Sort(new Comparison<Clan>(ComponentListBase<ClanListElement, Clan, ClanData, ClanListComponent>.sortByPopulation));
		this.clan = Randy.getRandom<Clan>(tClans);
		List<Actor> tUnits = new List<Actor>();
		tUnits.AddRange(BehaviourActionBase<AutoTesterBot>.world.units);
		tUnits.Sort(new Comparison<Actor>(TesterBehSetupWindowTest.sortByActorMaturity));
		this.unit = Randy.getRandom<Actor>(tUnits);
		List<Kingdom> tKingdoms = new List<Kingdom>();
		tKingdoms.AddRange(BehaviourActionBase<AutoTesterBot>.world.kingdoms);
		tKingdoms.Sort(new Comparison<Kingdom>(KingdomListComponent.sortByArmy));
		this.kingdom = Randy.getRandom<Kingdom>(tKingdoms);
		List<Alliance> tAlliances = new List<Alliance>();
		tAlliances.AddRange(BehaviourActionBase<AutoTesterBot>.world.alliances);
		tAlliances.Sort(new Comparison<Alliance>(ComponentListBase<AllianceListElement, Alliance, AllianceData, AllianceListComponent>.sortByPopulation));
		this.alliance = Randy.getRandom<Alliance>(tAlliances);
		List<Culture> tCultures = new List<Culture>();
		tCultures.AddRange(BehaviourActionBase<AutoTesterBot>.world.cultures);
		tCultures.Sort(new Comparison<Culture>(ComponentListBase<CultureListElement, Culture, CultureData, CultureListComponent>.sortByPopulation));
		this.culture = Randy.getRandom<Culture>(tCultures);
		List<Plot> tPlots = new List<Plot>();
		tPlots.AddRange(BehaviourActionBase<AutoTesterBot>.world.plots);
		tPlots.Sort(new Comparison<Plot>(PlotListComponent.sortBySupporters));
		this.plot = Randy.getRandom<Plot>(tPlots);
		List<War> tWars = new List<War>();
		tWars.AddRange(BehaviourActionBase<AutoTesterBot>.world.wars);
		tWars.Sort(new Comparison<War>(WarListComponent.sortByAge));
		this.war = Randy.getRandom<War>(tWars);
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x0014B290 File Offset: 0x00149490
	public static int sortByActorMaturity(Actor pActor1, Actor pActor2)
	{
		if (pActor2.hasClan() && !pActor1.hasClan())
		{
			return 1;
		}
		if (pActor1.hasClan() && !pActor2.hasClan())
		{
			return -1;
		}
		if (pActor2.hasCulture() && !pActor1.hasCulture())
		{
			return 1;
		}
		if (pActor1.hasCulture() && !pActor2.hasCulture())
		{
			return -1;
		}
		if (pActor2.isKing() && !pActor1.isKing())
		{
			return 1;
		}
		if (pActor1.isKing() && !pActor2.isKing())
		{
			return -1;
		}
		int tTraits = pActor2.countTraits().CompareTo(pActor1.countTraits());
		if (tTraits != 0)
		{
			return tTraits;
		}
		int tLevel = pActor2.data.level.CompareTo(pActor1.data.level);
		if (tLevel != 0)
		{
			return tLevel;
		}
		return pActor2.getAge().CompareTo(pActor1.getAge());
	}

	// Token: 0x04001F60 RID: 8032
	private int currentWindow;

	// Token: 0x04001F61 RID: 8033
	private City city;

	// Token: 0x04001F62 RID: 8034
	private Clan clan;

	// Token: 0x04001F63 RID: 8035
	private Plot plot;

	// Token: 0x04001F64 RID: 8036
	private Alliance alliance;

	// Token: 0x04001F65 RID: 8037
	private War war;

	// Token: 0x04001F66 RID: 8038
	private Kingdom kingdom;

	// Token: 0x04001F67 RID: 8039
	private Culture culture;

	// Token: 0x04001F68 RID: 8040
	private Actor unit;
}
