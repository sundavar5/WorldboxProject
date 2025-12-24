using System;
using System.Collections.Generic;
using UnityPools;

// Token: 0x02000032 RID: 50
[Serializable]
public class CityBuildOrderAsset : Asset
{
	// Token: 0x06000233 RID: 563 RVA: 0x00014B5C File Offset: 0x00012D5C
	public void addUpgrade(string pID, int pLimitType = 0, int pPop = 0, int pBuildings = 0, bool pCheckFullVillage = false, bool pZonesCheck = false, int pMinZones = 0)
	{
		this.addBuilding(pID, pLimitType, pPop, pBuildings, pCheckFullVillage, pZonesCheck, pMinZones).upgrade = true;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00014B78 File Offset: 0x00012D78
	public BuildOrder addBuilding(string pID, int pLimitType = 0, int pPop = 0, int pBuildings = 0, bool pCheckFullVillage = false, bool pCheckHouseLimit = false, int pMinZones = 0)
	{
		BuildOrder tAsset = new BuildOrder();
		tAsset.id = pID;
		tAsset.limit_type = pLimitType;
		tAsset.required_pop = pPop;
		tAsset.required_buildings = pBuildings;
		tAsset.check_full_village = pCheckFullVillage;
		tAsset.check_house_limit = pCheckHouseLimit;
		tAsset.min_zones = pMinZones;
		this.list.Add(tAsset);
		BuildOrderLibrary.b = tAsset;
		return tAsset;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00014BD4 File Offset: 0x00012DD4
	public void prepareForAssetGeneration()
	{
		HashSet<string> tTempHashset = UnsafeCollectionPool<HashSet<string>, string>.Get();
		foreach (BuildOrder tBuildOrder in this.list)
		{
			tTempHashset.Add(tBuildOrder.id);
			if (tBuildOrder.requirements_orders != null)
			{
				tTempHashset.UnionWith(tBuildOrder.requirements_orders);
			}
		}
		this.list_for_generation = tTempHashset.ToArray<string>();
		UnsafeCollectionPool<HashSet<string>, string>.Release(tTempHashset);
	}

	// Token: 0x040001E3 RID: 483
	[NonSerialized]
	public string[] list_for_generation;

	// Token: 0x040001E4 RID: 484
	public List<BuildOrder> list = new List<BuildOrder>();
}
