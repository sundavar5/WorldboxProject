using System;
using System.Collections.Generic;

// Token: 0x0200031B RID: 795
[Serializable]
public class CityEquipment : IDisposable
{
	// Token: 0x06001EB1 RID: 7857 RVA: 0x0010CC54 File Offset: 0x0010AE54
	public CityEquipment()
	{
		this.init();
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x0010CCB0 File Offset: 0x0010AEB0
	internal void init()
	{
		if (this.items_dicts == null)
		{
			this.items_dicts = new Dictionary<EquipmentType, List<long>>();
		}
		else
		{
			this.items_dicts.Clear();
		}
		this.items_dicts.Add(EquipmentType.Weapon, this.item_storage_weapons);
		this.items_dicts.Add(EquipmentType.Helmet, this.item_storage_helmets);
		this.items_dicts.Add(EquipmentType.Armor, this.item_storage_armor);
		this.items_dicts.Add(EquipmentType.Boots, this.item_storage_boots);
		this.items_dicts.Add(EquipmentType.Ring, this.item_storage_rings);
		this.items_dicts.Add(EquipmentType.Amulet, this.item_storage_amulets);
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x0010CD4C File Offset: 0x0010AF4C
	public void clearItems()
	{
		foreach (List<long> list in this.getAllEquipmentLists())
		{
			foreach (long tID in list)
			{
				Item tItem = World.world.items.get(tID);
				if (tItem != null)
				{
					tItem.clearCity();
				}
			}
		}
		this.clearCollections();
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x0010CDE8 File Offset: 0x0010AFE8
	public int countItems()
	{
		int tCount = 0;
		foreach (List<long> tItems in this.getAllEquipmentLists())
		{
			tCount += tItems.Count;
		}
		return tCount;
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x0010CE3C File Offset: 0x0010B03C
	public bool hasAnyItem()
	{
		using (IEnumerator<List<long>> enumerator = this.getAllEquipmentLists().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Count > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x0010CE90 File Offset: 0x0010B090
	public void addItem(City pCity, Item pItem, List<long> pList = null)
	{
		EquipmentAsset tItemAsset = pItem.getAsset();
		if (pList == null)
		{
			pList = this.getEquipmentList(tItemAsset.equipment_type);
		}
		pItem.setInCityStorage(pCity);
		pList.Add(pItem.id);
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x0010CEC8 File Offset: 0x0010B0C8
	public List<long> getEquipmentList(EquipmentType pType)
	{
		return this.items_dicts[pType];
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x0010CED6 File Offset: 0x0010B0D6
	public IEnumerable<List<long>> getAllEquipmentLists()
	{
		foreach (List<long> tList in this.items_dicts.Values)
		{
			yield return tList;
		}
		Dictionary<EquipmentType, List<long>>.ValueCollection.Enumerator enumerator = default(Dictionary<EquipmentType, List<long>>.ValueCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x0010CEE8 File Offset: 0x0010B0E8
	public void loadFromSave(City pCity)
	{
		this.init();
		using (IEnumerator<List<long>> enumerator = this.getAllEquipmentLists().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.RemoveAll(delegate(long pID)
				{
					Item tItem = World.world.items.get(pID);
					return tItem == null || tItem.getAsset() == null;
				});
			}
		}
		foreach (List<long> tItems in this.getAllEquipmentLists())
		{
			for (int i = 0; i < tItems.Count; i++)
			{
				long tId = tItems[i];
				World.world.items.get(tId).setInCityStorage(pCity);
			}
		}
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x0010CFBC File Offset: 0x0010B1BC
	public void Dispose()
	{
		Dictionary<EquipmentType, List<long>> dictionary = this.items_dicts;
		if (dictionary != null)
		{
			dictionary.Clear();
		}
		this.clearCollections();
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x0010CFD8 File Offset: 0x0010B1D8
	private void clearCollections()
	{
		this.item_storage_weapons.Clear();
		this.item_storage_helmets.Clear();
		this.item_storage_armor.Clear();
		this.item_storage_boots.Clear();
		this.item_storage_rings.Clear();
		this.item_storage_amulets.Clear();
	}

	// Token: 0x04001682 RID: 5762
	internal Dictionary<EquipmentType, List<long>> items_dicts;

	// Token: 0x04001683 RID: 5763
	public List<long> item_storage_weapons = new List<long>();

	// Token: 0x04001684 RID: 5764
	public List<long> item_storage_helmets = new List<long>();

	// Token: 0x04001685 RID: 5765
	public List<long> item_storage_armor = new List<long>();

	// Token: 0x04001686 RID: 5766
	public List<long> item_storage_boots = new List<long>();

	// Token: 0x04001687 RID: 5767
	public List<long> item_storage_rings = new List<long>();

	// Token: 0x04001688 RID: 5768
	public List<long> item_storage_amulets = new List<long>();
}
