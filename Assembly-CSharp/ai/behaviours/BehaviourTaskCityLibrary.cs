using System;

namespace ai.behaviours
{
	// Token: 0x02000973 RID: 2419
	public class BehaviourTaskCityLibrary : AssetLibrary<BehaviourTaskCity>
	{
		// Token: 0x060046D5 RID: 18133 RVA: 0x001E1118 File Offset: 0x001DF318
		public override void init()
		{
			base.init();
			BehaviourTaskCity behaviourTaskCity = new BehaviourTaskCity();
			behaviourTaskCity.id = "nothing";
			BehaviourTaskCity pAsset = behaviourTaskCity;
			this.t = behaviourTaskCity;
			this.add(pAsset);
			BehaviourTaskCity behaviourTaskCity2 = new BehaviourTaskCity();
			behaviourTaskCity2.id = "wait1";
			pAsset = behaviourTaskCity2;
			this.t = behaviourTaskCity2;
			this.add(pAsset);
			this.t.addBeh(new CityBehRandomWait(0f, 1f));
			BehaviourTaskCity behaviourTaskCity3 = new BehaviourTaskCity();
			behaviourTaskCity3.id = "wait5";
			pAsset = behaviourTaskCity3;
			this.t = behaviourTaskCity3;
			this.add(pAsset);
			this.t.addBeh(new CityBehRandomWait(5f, 5f));
			BehaviourTaskCity behaviourTaskCity4 = new BehaviourTaskCity();
			behaviourTaskCity4.id = "random_wait_test";
			pAsset = behaviourTaskCity4;
			this.t = behaviourTaskCity4;
			this.add(pAsset);
			this.t.addBeh(new CityBehRandomWait(5f, 10f));
			this.t.addBeh(new CityBehRandomWait(5f, 10f));
			this.t.addBeh(new CityBehRandomWait(5f, 10f));
			BehaviourTaskCity behaviourTaskCity5 = new BehaviourTaskCity();
			behaviourTaskCity5.id = "do_checks";
			pAsset = behaviourTaskCity5;
			this.t = behaviourTaskCity5;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckLeader());
			this.t.addBeh(new CityBehRandomWait(0.1f, 1f));
			this.t.addBeh(new CityBehCheckAttackZone());
			this.t.addBeh(new CityBehRandomWait(0.1f, 1f));
			this.t.addBeh(new CityBehCheckCitizenTasks());
			this.t.addBeh(new CityBehRandomWait(0.1f, 1f));
			BehaviourTaskCity behaviourTaskCity6 = new BehaviourTaskCity();
			behaviourTaskCity6.id = "do_initial_load_check";
			pAsset = behaviourTaskCity6;
			this.t = behaviourTaskCity6;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckCitizenTasks());
			this.t.addBeh(new CityBehCheckLoyalty());
			BehaviourTaskCity behaviourTaskCity7 = new BehaviourTaskCity();
			behaviourTaskCity7.id = "check_farms";
			pAsset = behaviourTaskCity7;
			this.t = behaviourTaskCity7;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckFarms());
			BehaviourTaskCity behaviourTaskCity8 = new BehaviourTaskCity();
			behaviourTaskCity8.id = "check_loyalty";
			behaviourTaskCity8.single_interval = 2f;
			pAsset = behaviourTaskCity8;
			this.t = behaviourTaskCity8;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckLoyalty());
			BehaviourTaskCity behaviourTaskCity9 = new BehaviourTaskCity();
			behaviourTaskCity9.id = "check_destruction";
			behaviourTaskCity9.single_interval = 2f;
			pAsset = behaviourTaskCity9;
			this.t = behaviourTaskCity9;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckDestruction());
			BehaviourTaskCity behaviourTaskCity10 = new BehaviourTaskCity();
			behaviourTaskCity10.id = "produce_boat";
			pAsset = behaviourTaskCity10;
			this.t = behaviourTaskCity10;
			this.add(pAsset);
			this.t.addBeh(new CityBehProduceBoat());
			BehaviourTaskCity behaviourTaskCity11 = new BehaviourTaskCity();
			behaviourTaskCity11.id = "border_shrink";
			pAsset = behaviourTaskCity11;
			this.t = behaviourTaskCity11;
			this.add(pAsset);
			this.t.addBeh(new CityBehBorderShrink());
			BehaviourTaskCity behaviourTaskCity12 = new BehaviourTaskCity();
			behaviourTaskCity12.id = "build";
			behaviourTaskCity12.single_interval = 0f;
			pAsset = behaviourTaskCity12;
			this.t = behaviourTaskCity12;
			this.add(pAsset);
			this.t.addBeh(new CityBehBuild());
			BehaviourTaskCity behaviourTaskCity13 = new BehaviourTaskCity();
			behaviourTaskCity13.id = "supply_kingdom_cities";
			pAsset = behaviourTaskCity13;
			this.t = behaviourTaskCity13;
			this.add(pAsset);
			this.t.addBeh(new CityBehSupplyKingdomCities());
			BehaviourTaskCity behaviourTaskCity14 = new BehaviourTaskCity();
			behaviourTaskCity14.id = "produce_resources";
			pAsset = behaviourTaskCity14;
			this.t = behaviourTaskCity14;
			this.add(pAsset);
			this.t.addBeh(new CityBehProduceResources());
			BehaviourTaskCity behaviourTaskCity15 = new BehaviourTaskCity();
			behaviourTaskCity15.id = "check_army";
			pAsset = behaviourTaskCity15;
			this.t = behaviourTaskCity15;
			this.add(pAsset);
			this.t.addBeh(new CityBehCheckArmy());
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x001E14DC File Offset: 0x001DF6DC
		public override void editorDiagnosticLocales()
		{
		}
	}
}
