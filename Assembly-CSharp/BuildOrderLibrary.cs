using System;

// Token: 0x0200002E RID: 46
public class BuildOrderLibrary : AssetLibrary<CityBuildOrderAsset>
{
	// Token: 0x06000224 RID: 548 RVA: 0x00013B54 File Offset: 0x00011D54
	public override void init()
	{
		base.init();
		this.initCivsBasic();
		this.initCivsBasic2();
		this.initCivsAdvanced();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00013B70 File Offset: 0x00011D70
	private void initCivsBasic()
	{
		this.add(new CityBuildOrderAsset
		{
			id = "build_order_basic"
		});
		this.t.addBuilding("order_bonfire", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_stockpile", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_hall_0", 1, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_house_0", 0, 0, 0, false, true, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_watch_tower", 1, 30, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0"
		});
		this.t.addBuilding("order_temple", 1, 90, 20, false, false, 20);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0",
			"order_statue"
		});
		this.t.addBuilding("order_statue", 1, 70, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_hall"
		});
		this.t.addBuilding("order_well", 1, 20, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_hall"
		});
		this.t.addBuilding("order_mine", 1, 20, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_library", 1, 50, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_docks_0", 5, 0, 2, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addUpgrade("order_docks_0", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_docks_0"
		});
		this.t.addBuilding("order_windmill_0", 1, 6, 5, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_barracks", 1, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_training_dummy", 3, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_barracks"
		});
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00013EA8 File Offset: 0x000120A8
	private void initCivsBasic2()
	{
		this.add(new CityBuildOrderAsset
		{
			id = "build_order_basic_2"
		});
		this.t.addBuilding("order_bonfire", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_stockpile", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_hall_0", 1, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_house_0", 0, 0, 0, false, true, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_watch_tower", 1, 30, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0"
		});
		this.t.addBuilding("order_temple", 1, 90, 20, false, false, 20);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0"
		});
		this.t.addBuilding("order_mine", 1, 20, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_library", 1, 50, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_docks_0", 5, 0, 2, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addUpgrade("order_docks_0", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_docks_0"
		});
		this.t.addBuilding("order_windmill_0", 1, 6, 5, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_barracks", 1, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_training_dummy", 3, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_barracks"
		});
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0001416C File Offset: 0x0001236C
	private void initCivsAdvanced()
	{
		this.add(new CityBuildOrderAsset
		{
			id = "build_order_advanced"
		});
		this.t.addBuilding("order_hall_0", 1, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_bonfire", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_stockpile", 1, 0, 0, false, false, 0);
		this.t.addBuilding("order_house_0", 0, 0, 0, false, true, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addBuilding("order_tent", 0, 0, 0, false, true, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addUpgrade("order_tent", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_tent"
		});
		this.t.addUpgrade("order_house_0", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_0",
			"order_house_0"
		});
		this.t.addUpgrade("order_house_1", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_1",
			"order_house_1"
		});
		this.t.addUpgrade("order_house_2", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_1",
			"order_house_2"
		});
		this.t.addUpgrade("order_house_3", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_2",
			"order_house_3"
		});
		this.t.addUpgrade("order_house_4", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_2",
			"order_house_4"
		});
		this.t.addUpgrade("order_hall_0", 0, 30, 8, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_house_1"
		});
		this.t.addUpgrade("order_hall_1", 0, 100, 20, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_statue",
			"order_mine",
			"order_barracks"
		});
		this.t.addBuilding("order_windmill_0", 1, 6, 5, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addUpgrade("order_windmill_0", 0, 40, 10, false, false, 0);
		this.t.addBuilding("order_docks_0", 5, 0, 2, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire"
		});
		this.t.addUpgrade("order_docks_0", 0, 0, 0, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_docks_0"
		});
		this.t.addBuilding("order_well", 1, 20, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_hall"
		});
		this.t.addBuilding("order_mine", 1, 20, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0"
		});
		this.t.addBuilding("order_barracks", 1, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_1"
		});
		this.t.addBuilding("order_training_dummy", 3, 50, 16, false, false, 20);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_barracks"
		});
		this.t.addBuilding("order_watch_tower", 1, 30, 10, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_0"
		});
		this.t.addBuilding("order_temple", 1, 90, 20, false, false, 20);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_bonfire",
			"order_hall_1",
			"order_statue"
		});
		this.t.addBuilding("order_statue", 1, 70, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_orders = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"order_hall_1"
		});
		this.t.addBuilding("order_library", 1, 50, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
		this.t.addBuilding("order_market", 1, 60, 15, false, false, 0);
		BuildOrderLibrary.b.requirements_types = AssetLibrary<CityBuildOrderAsset>.a<string>(new string[]
		{
			"type_bonfire",
			"type_hall"
		});
	}

	// Token: 0x06000228 RID: 552 RVA: 0x000146FC File Offset: 0x000128FC
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (CityBuildOrderAsset cityBuildOrderAsset in this.list)
		{
			cityBuildOrderAsset.prepareForAssetGeneration();
		}
	}

	// Token: 0x040001C8 RID: 456
	public static BuildOrder b;
}
