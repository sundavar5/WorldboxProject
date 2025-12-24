using System;
using System.Collections.Generic;

// Token: 0x0200031D RID: 797
[Serializable]
public class CityResources : IDisposable
{
	// Token: 0x06001ED1 RID: 7889 RVA: 0x0010D770 File Offset: 0x0010B970
	public void loadFromSave()
	{
		if (this.saved_resources != null)
		{
			foreach (CityStorageSlot tRes in this.saved_resources)
			{
				if (AssetManager.resources.get(tRes.id) != null && tRes.amount >= 0)
				{
					tRes.create(tRes.id);
					this.putToDict(tRes);
				}
			}
		}
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x0010D7F4 File Offset: 0x0010B9F4
	public int get(string pRes)
	{
		CityStorageSlot tSlot;
		if (this._resources.TryGetValue(pRes, out tSlot))
		{
			return tSlot.amount;
		}
		return 0;
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x0010D81C File Offset: 0x0010BA1C
	public int change(string pRes, int pAmount = 1)
	{
		if (DebugConfig.isOn(DebugOption.CityInfiniteResources))
		{
			pAmount = 999;
		}
		CityStorageSlot tSlot;
		int tResult;
		if (this._resources.TryGetValue(pRes, out tSlot))
		{
			tSlot.amount += pAmount;
			if (tSlot.amount > tSlot.asset.maximum)
			{
				tSlot.amount = tSlot.asset.maximum;
			}
			tResult = tSlot.amount;
		}
		else
		{
			tResult = this.addNew(pRes, pAmount);
		}
		return tResult;
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x0010D894 File Offset: 0x0010BA94
	private int addNew(string pResID, int pAmount)
	{
		CityStorageSlot tRes = new CityStorageSlot(pResID);
		tRes.amount = pAmount;
		this.putToDict(tRes);
		return tRes.amount;
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x0010D8BC File Offset: 0x0010BABC
	public bool hasSpaceForResource(ResourceAsset pAsset)
	{
		return this.get(pAsset.id) < pAsset.storage_max;
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x0010D8D8 File Offset: 0x0010BAD8
	public bool hasResourcesForNewItems()
	{
		foreach (ResourceAsset tAsset in AssetManager.resources.strategic_resource_assets)
		{
			if (this.get(tAsset.id) > 10)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x0010D940 File Offset: 0x0010BB40
	public void set(string pRes, int pAmount)
	{
		CityStorageSlot tSlot;
		if (this._resources.TryGetValue(pRes, out tSlot))
		{
			tSlot.amount = pAmount;
			return;
		}
		this.addNew(pRes, pAmount);
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x0010D970 File Offset: 0x0010BB70
	private void putToDict(CityStorageSlot pRes)
	{
		if (this._resources.ContainsKey(pRes.id))
		{
			return;
		}
		this._resources.Add(pRes.id, pRes);
		if (pRes.asset.food)
		{
			this._list_food.Add(pRes);
			return;
		}
		this._list_other.Add(pRes);
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x0010D9CC File Offset: 0x0010BBCC
	public ResourceAsset getRandomSuitableFood(Subspecies pSubspecies, string pSpecificFood = null)
	{
		if (this._list_food.Count == 0)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(pSpecificFood) && this.get(pSpecificFood) > 0)
		{
			return AssetManager.resources.get(pSpecificFood);
		}
		HashSet<string> tAllowedFood = pSubspecies.getAllowedFoodByDiet();
		ResourceAsset tResult = this.getAvailableFoodAsset(this._list_food, tAllowedFood, true);
		if (tResult == null)
		{
			tResult = this.getAvailableFoodAsset(this._list_other, tAllowedFood, false);
		}
		return tResult;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x0010DA30 File Offset: 0x0010BC30
	private ResourceAsset getAvailableFoodAsset(List<CityStorageSlot> pList, HashSet<string> pAllowedFood, bool pSort)
	{
		if (pSort)
		{
			pList.Sort(new Comparison<CityStorageSlot>(this.foodSorter));
		}
		for (int i = 0; i < pList.Count; i++)
		{
			CityStorageSlot tSlot = pList[i];
			if (tSlot.amount != 0 && pAllowedFood.Contains(tSlot.id))
			{
				return tSlot.asset;
			}
		}
		return null;
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x0010DA89 File Offset: 0x0010BC89
	public int foodSorter(CityStorageSlot o1, CityStorageSlot o2)
	{
		return o2.amount.CompareTo(o1.amount);
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x0010DA9C File Offset: 0x0010BC9C
	public int countFood()
	{
		int tResult = 0;
		foreach (CityStorageSlot tSlot in this._list_food)
		{
			tResult += tSlot.amount;
		}
		return tResult;
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x0010DAF4 File Offset: 0x0010BCF4
	public ResourceAsset getRandomFoodAsset()
	{
		if (this._list_food.Count == 0)
		{
			return null;
		}
		return this._list_food.GetRandom<CityStorageSlot>().asset;
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x0010DB18 File Offset: 0x0010BD18
	public void save()
	{
		this.saved_resources = new List<CityStorageSlot>();
		foreach (CityStorageSlot tSlot in this.getSlots())
		{
			if (tSlot.amount != 0)
			{
				this.saved_resources.Add(tSlot);
			}
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x0010DB80 File Offset: 0x0010BD80
	public IEnumerable<string> getKeys()
	{
		return this._resources.Keys;
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x0010DB8D File Offset: 0x0010BD8D
	public IEnumerable<CityStorageSlot> getSlots()
	{
		return this._resources.Values;
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x0010DB9A File Offset: 0x0010BD9A
	public void Dispose()
	{
		this._resources.Clear();
		this._list_food.Clear();
		this._list_other.Clear();
		List<CityStorageSlot> list = this.saved_resources;
		if (list == null)
		{
			return;
		}
		list.Clear();
	}

	// Token: 0x0400168A RID: 5770
	[NonSerialized]
	private Dictionary<string, CityStorageSlot> _resources = new Dictionary<string, CityStorageSlot>();

	// Token: 0x0400168B RID: 5771
	[NonSerialized]
	private List<CityStorageSlot> _list_food = new List<CityStorageSlot>();

	// Token: 0x0400168C RID: 5772
	[NonSerialized]
	private List<CityStorageSlot> _list_other = new List<CityStorageSlot>();

	// Token: 0x0400168D RID: 5773
	public List<CityStorageSlot> saved_resources;
}
