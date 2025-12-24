using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020000B7 RID: 183
[Serializable]
public class ArchitectureAsset : Asset
{
	// Token: 0x060005CC RID: 1484 RVA: 0x0005548C File Offset: 0x0005368C
	public void addBuildingOrderKey(string pKey, string pID)
	{
		if (this.building_ids_for_construction == null)
		{
			this.building_ids_for_construction = new Dictionary<string, string>();
		}
		this.building_ids_for_construction[pKey] = pID;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x000554B0 File Offset: 0x000536B0
	public void replaceSharedID(string pID, string pNewID)
	{
		for (int i = 0; i < this.shared_building_orders.Length; i++)
		{
			if (this.shared_building_orders[i].Item1 == pID)
			{
				this.shared_building_orders[i].Item2 = pNewID;
				return;
			}
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x000554FC File Offset: 0x000536FC
	public BuildingAsset getBuilding(string pOrderID)
	{
		string tBuildingID = this.getBuildingID(pOrderID);
		return AssetManager.buildings.get(tBuildingID);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0005551C File Offset: 0x0005371C
	public string getBuildingID(string pOrderID)
	{
		return this.building_ids_for_construction[pOrderID];
	}

	// Token: 0x040005E2 RID: 1506
	public bool generate_buildings;

	// Token: 0x040005E3 RID: 1507
	public string generation_target;

	// Token: 0x040005E4 RID: 1508
	public bool spread_biome;

	// Token: 0x040005E5 RID: 1509
	public string spread_biome_id;

	// Token: 0x040005E6 RID: 1510
	[DefaultValue("")]
	public string projectile_id = string.Empty;

	// Token: 0x040005E7 RID: 1511
	[DefaultValue(true)]
	public bool burnable_buildings = true;

	// Token: 0x040005E8 RID: 1512
	[DefaultValue(true)]
	public bool acid_affected_buildings = true;

	// Token: 0x040005E9 RID: 1513
	[DefaultValue(true)]
	public bool has_shadows = true;

	// Token: 0x040005EA RID: 1514
	[DefaultValue("building")]
	public string material = "building";

	// Token: 0x040005EB RID: 1515
	public Dictionary<string, string> building_ids_for_construction;

	// Token: 0x040005EC RID: 1516
	public string[] styled_building_orders;

	// Token: 0x040005ED RID: 1517
	public ValueTuple<string, string>[] shared_building_orders;

	// Token: 0x040005EE RID: 1518
	[DefaultValue("boat_fishing")]
	public string actor_asset_id_boat_fishing = "boat_fishing";

	// Token: 0x040005EF RID: 1519
	public string actor_asset_id_trading;

	// Token: 0x040005F0 RID: 1520
	public string actor_asset_id_transport;
}
