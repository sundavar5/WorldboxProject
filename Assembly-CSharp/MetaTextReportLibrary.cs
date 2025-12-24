using System;

// Token: 0x020002AF RID: 687
public class MetaTextReportLibrary : AssetLibrary<MetaTextReportAsset>
{
	// Token: 0x0600199E RID: 6558 RVA: 0x000F1920 File Offset: 0x000EFB20
	public override void init()
	{
		base.init();
		this.addGeneralMeta();
		this.addCity();
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x000F1934 File Offset: 0x000EFB34
	private void addCity()
	{
		MetaTextReportAsset metaTextReportAsset = new MetaTextReportAsset();
		metaTextReportAsset.id = "happy";
		metaTextReportAsset.color = "#ADADAD";
		metaTextReportAsset.report_action = ((IMetaObject pObject) => pObject.getRatioHappy() > 0.8f);
		this.add(metaTextReportAsset);
		MetaTextReportAsset metaTextReportAsset2 = new MetaTextReportAsset();
		metaTextReportAsset2.id = "unhappy";
		metaTextReportAsset2.color = "#919191";
		metaTextReportAsset2.report_action = ((IMetaObject pObject) => pObject.getRatioUnhappy() > 0.8f);
		this.add(metaTextReportAsset2);
		MetaTextReportAsset metaTextReportAsset3 = new MetaTextReportAsset();
		metaTextReportAsset3.id = "many_children";
		metaTextReportAsset3.color = "#ADADAD";
		metaTextReportAsset3.report_action = ((IMetaObject pObject) => pObject.countUnits() >= 20 && pObject.getRatioChildren() > 0.7f);
		this.add(metaTextReportAsset3);
		MetaTextReportAsset metaTextReportAsset4 = new MetaTextReportAsset();
		metaTextReportAsset4.id = "many_homeless";
		metaTextReportAsset4.color = "#919191";
		metaTextReportAsset4.report_action = ((IMetaObject pObject) => pObject.countUnits() >= 20 && pObject.getRatioHomeless() > 0.8f);
		this.add(metaTextReportAsset4);
		MetaTextReportAsset metaTextReportAsset5 = new MetaTextReportAsset();
		metaTextReportAsset5.id = "food_plenty";
		metaTextReportAsset5.color = "#ADADAD";
		metaTextReportAsset5.report_action = delegate(IMetaObject pObject)
		{
			City city = pObject as City;
			int tTotalFood = city.countFoodTotal();
			int tPopulationPeople = city.getPopulationPeople();
			return tTotalFood > tPopulationPeople * 4;
		};
		this.add(metaTextReportAsset5);
		MetaTextReportAsset metaTextReportAsset6 = new MetaTextReportAsset();
		metaTextReportAsset6.id = "food_running_out";
		metaTextReportAsset6.color = "#919191";
		metaTextReportAsset6.report_action = delegate(IMetaObject pObject)
		{
			City tCity = pObject as City;
			int tTotalFood = tCity.countFoodTotal();
			if (tTotalFood == 0)
			{
				return false;
			}
			int tPopulationPeople = tCity.getPopulationPeople();
			return tTotalFood < tPopulationPeople * 2;
		};
		this.add(metaTextReportAsset6);
		MetaTextReportAsset metaTextReportAsset7 = new MetaTextReportAsset();
		metaTextReportAsset7.id = "food_none";
		metaTextReportAsset7.color = "#919191";
		metaTextReportAsset7.report_action = ((IMetaObject pObject) => (pObject as City).countFoodTotal() == 0);
		this.add(metaTextReportAsset7);
		MetaTextReportAsset metaTextReportAsset8 = new MetaTextReportAsset();
		metaTextReportAsset8.id = "stone_none";
		metaTextReportAsset8.color = "#919191";
		metaTextReportAsset8.report_action = ((IMetaObject pObject) => (pObject as City).amount_stone == 0);
		this.add(metaTextReportAsset8);
		MetaTextReportAsset metaTextReportAsset9 = new MetaTextReportAsset();
		metaTextReportAsset9.id = "wood_none";
		metaTextReportAsset9.color = "#919191";
		metaTextReportAsset9.report_action = ((IMetaObject pObject) => (pObject as City).amount_wood == 0);
		this.add(metaTextReportAsset9);
		MetaTextReportAsset metaTextReportAsset10 = new MetaTextReportAsset();
		metaTextReportAsset10.id = "metal_none";
		metaTextReportAsset10.color = "#919191";
		metaTextReportAsset10.report_action = ((IMetaObject pObject) => (pObject as City).amount_common_metals == 0);
		this.add(metaTextReportAsset10);
		MetaTextReportAsset metaTextReportAsset11 = new MetaTextReportAsset();
		metaTextReportAsset11.id = "gold_none";
		metaTextReportAsset11.color = "#919191";
		metaTextReportAsset11.report_action = ((IMetaObject pObject) => (pObject as City).amount_gold == 0);
		this.add(metaTextReportAsset11);
		MetaTextReportAsset metaTextReportAsset12 = new MetaTextReportAsset();
		metaTextReportAsset12.id = "war_high_casualties";
		metaTextReportAsset12.color = "#919191";
		metaTextReportAsset12.report_action = ((IMetaObject pObject) => (pObject as War).getTotalDeaths() > 100L);
		this.add(metaTextReportAsset12);
		MetaTextReportAsset metaTextReportAsset13 = new MetaTextReportAsset();
		metaTextReportAsset13.id = "war_long";
		metaTextReportAsset13.color = "#919191";
		metaTextReportAsset13.report_action = ((IMetaObject pObject) => pObject.getAge() > 100);
		this.add(metaTextReportAsset13);
		MetaTextReportAsset metaTextReportAsset14 = new MetaTextReportAsset();
		metaTextReportAsset14.id = "war_fresh";
		metaTextReportAsset14.color = "#ADADAD";
		metaTextReportAsset14.report_action = ((IMetaObject pObject) => pObject.getAge() < 5);
		this.add(metaTextReportAsset14);
		MetaTextReportAsset metaTextReportAsset15 = new MetaTextReportAsset();
		metaTextReportAsset15.id = "war_defenders_getting_captured";
		metaTextReportAsset15.color = "#ADADAD";
		metaTextReportAsset15.report_action = delegate(IMetaObject pObject)
		{
			War tWar = pObject as War;
			return tWar.areDefendersGettingCaptured() && !tWar.areAttackersGettingCaptured();
		};
		this.add(metaTextReportAsset15);
		MetaTextReportAsset metaTextReportAsset16 = new MetaTextReportAsset();
		metaTextReportAsset16.id = "war_attackers_getting_captured";
		metaTextReportAsset16.color = "#ADADAD";
		metaTextReportAsset16.report_action = delegate(IMetaObject pObject)
		{
			War tWar = pObject as War;
			return tWar.areAttackersGettingCaptured() && !tWar.areDefendersGettingCaptured();
		};
		this.add(metaTextReportAsset16);
		MetaTextReportAsset metaTextReportAsset17 = new MetaTextReportAsset();
		metaTextReportAsset17.id = "war_quiet";
		metaTextReportAsset17.color = "#ADADAD";
		metaTextReportAsset17.report_action = delegate(IMetaObject pObject)
		{
			War tWar = pObject as War;
			return !tWar.areAttackersGettingCaptured() && !tWar.areDefendersGettingCaptured();
		};
		this.add(metaTextReportAsset17);
		MetaTextReportAsset metaTextReportAsset18 = new MetaTextReportAsset();
		metaTextReportAsset18.id = "war_full_on_battle";
		metaTextReportAsset18.color = "#ADADAD";
		metaTextReportAsset18.report_action = delegate(IMetaObject pObject)
		{
			War tWar = pObject as War;
			return tWar.areAttackersGettingCaptured() && tWar.areDefendersGettingCaptured();
		};
		this.add(metaTextReportAsset18);
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x000F1E3F File Offset: 0x000F003F
	private void addGeneralMeta()
	{
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x000F1E44 File Offset: 0x000F0044
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MetaTextReportAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
	}
}
