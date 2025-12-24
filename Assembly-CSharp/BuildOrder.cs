using System;

// Token: 0x0200002D RID: 45
[Serializable]
public class BuildOrder : Asset
{
	// Token: 0x06000222 RID: 546 RVA: 0x00013B29 File Offset: 0x00011D29
	public BuildingAsset getBuildingAsset(City pCity, string pOrderID = null)
	{
		if (string.IsNullOrEmpty(pOrderID))
		{
			pOrderID = this.id;
		}
		return pCity.getActorAsset().architecture_asset.getBuilding(pOrderID);
	}

	// Token: 0x040001BF RID: 447
	public int required_pop;

	// Token: 0x040001C0 RID: 448
	public int required_buildings;

	// Token: 0x040001C1 RID: 449
	public int limit_type;

	// Token: 0x040001C2 RID: 450
	public bool check_full_village;

	// Token: 0x040001C3 RID: 451
	public bool check_house_limit;

	// Token: 0x040001C4 RID: 452
	public int min_zones;

	// Token: 0x040001C5 RID: 453
	public bool upgrade;

	// Token: 0x040001C6 RID: 454
	public string[] requirements_orders;

	// Token: 0x040001C7 RID: 455
	public string[] requirements_types;
}
