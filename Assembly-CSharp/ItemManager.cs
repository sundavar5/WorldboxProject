using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPools;

// Token: 0x0200027B RID: 635
public class ItemManager : CoreSystemManager<Item, ItemData>
{
	// Token: 0x060017B9 RID: 6073 RVA: 0x000E7F98 File Offset: 0x000E6198
	public ItemManager()
	{
		this.type_id = "item";
		MapBox.on_world_loaded = (Action)Delegate.Combine(MapBox.on_world_loaded, new Action(delegate()
		{
			this.diagnostic();
		}));
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000E7FEC File Offset: 0x000E61EC
	public bool isDirty()
	{
		return this._dirty;
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000E7FF4 File Offset: 0x000E61F4
	public void setDirty()
	{
		this._dirty = true;
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x000E7FFD File Offset: 0x000E61FD
	public Item newItem(EquipmentAsset pAsset)
	{
		Item item = base.newObject();
		item.newItem(pAsset);
		return item;
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000E800C File Offset: 0x000E620C
	public void diagnostic()
	{
		Dictionary<Item, int> tDictCities = UnsafeCollectionPool<Dictionary<Item, int>, KeyValuePair<Item, int>>.Get();
		Dictionary<Item, int> tDictUnits = UnsafeCollectionPool<Dictionary<Item, int>, KeyValuePair<Item, int>>.Get();
		foreach (City city in World.world.cities)
		{
			foreach (List<long> list in city.data.equipment.getAllEquipmentLists())
			{
				foreach (long tItemID in list)
				{
					Item tItem = base.get(tItemID);
					if (tItem != null)
					{
						if (!tDictCities.ContainsKey(tItem))
						{
							tDictCities.Add(tItem, 0);
						}
						Dictionary<Item, int> dictionary = tDictCities;
						Item key = tItem;
						int num = dictionary[key];
						dictionary[key] = num + 1;
					}
				}
			}
		}
		foreach (Actor tUnit in World.world.units)
		{
			if (tUnit.hasEquipment())
			{
				foreach (ActorEquipmentSlot actorEquipmentSlot in tUnit.equipment)
				{
					Item tItem2 = actorEquipmentSlot.getItem();
					if (tItem2 != null)
					{
						if (!tDictUnits.ContainsKey(tItem2))
						{
							tDictUnits.Add(tItem2, 0);
						}
						Dictionary<Item, int> dictionary2 = tDictUnits;
						Item key = tItem2;
						int num = dictionary2[key];
						dictionary2[key] = num + 1;
					}
				}
			}
		}
		foreach (Item tItem3 in this.list)
		{
			if (tDictCities.ContainsKey(tItem3) && tDictUnits.ContainsKey(tItem3))
			{
				Debug.LogError("Item Error. Item in city and in unit " + tItem3.id.ToString());
			}
		}
		UnsafeCollectionPool<Dictionary<Item, int>, KeyValuePair<Item, int>>.Release(tDictCities);
		UnsafeCollectionPool<Dictionary<Item, int>, KeyValuePair<Item, int>>.Release(tDictUnits);
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000E8250 File Offset: 0x000E6450
	public override Item loadObject(ItemData pData)
	{
		if (AssetManager.items.get(pData.asset_id) == null)
		{
			return null;
		}
		return base.loadObject(pData);
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x000E8270 File Offset: 0x000E6470
	private List<ItemModAsset> getModPool(EquipmentType pType)
	{
		switch (pType)
		{
		case EquipmentType.Weapon:
			return AssetManager.items_modifiers.pools["weapon"];
		case EquipmentType.Ring:
		case EquipmentType.Amulet:
			return AssetManager.items_modifiers.pools["accessory"];
		}
		return AssetManager.items_modifiers.pools["armor"];
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000E82E1 File Offset: 0x000E64E1
	private ItemModAsset getRandomModFromPool(EquipmentType pType)
	{
		return this.getModPool(pType).GetRandom<ItemModAsset>();
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x000E82F0 File Offset: 0x000E64F0
	public void generateModsFor(Item pItem, int pTries = 1, Actor pActor = null, bool pAddName = true)
	{
		EquipmentAsset tAsset = pItem.getAsset();
		using (ListPool<string> tNewNames = new ListPool<string>())
		{
			for (int i = 0; i < pTries; i++)
			{
				if (!Randy.randomBool())
				{
					ItemModAsset tModAsset = this.getRandomModFromPool(tAsset.equipment_type);
					if (tModAsset.mod_can_be_given)
					{
						bool tModAdded = this.tryToAddMod(pItem, tModAsset);
						string tName3;
						if (pAddName && tModAdded && this.checkModName(pItem, tModAsset, tAsset, pActor, out tName3))
						{
							tNewNames.Add(tName3);
						}
					}
				}
			}
			if (tAsset.item_modifiers != null)
			{
				for (int j = 0; j < tAsset.item_modifiers.Length; j++)
				{
					ItemModAsset tModAsset2 = tAsset.item_modifiers[j];
					bool tModAdded2 = this.tryToAddMod(pItem, tModAsset2);
					string tName2;
					if (pAddName && tModAdded2 && this.checkModName(pItem, tModAsset2, tAsset, pActor, out tName2))
					{
						tNewNames.Add(tName2);
					}
				}
			}
			tNewNames.RemoveAll((string tName) => string.IsNullOrEmpty(tName));
			if (tNewNames.Count > 0)
			{
				pItem.setName(Randy.getRandom<string>(tNewNames), true);
			}
		}
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x000E8404 File Offset: 0x000E6604
	public Item generateItem(EquipmentAsset pItemAsset, Kingdom pKingdom = null, string pWho = null, int pTries = 1, Actor pActor = null, int pFakeCreationYear = 0, bool pByPlayer = false)
	{
		Item tNewItem = this.newItem(pItemAsset);
		this.generateModsFor(tNewItem, pTries, pActor, true);
		tNewItem.data.asset_id = pItemAsset.id;
		tNewItem.data.by = pWho;
		if (!pByPlayer && !pActor.isRekt() && pActor.name == pWho)
		{
			tNewItem.data.creator_id = pActor.getID();
		}
		else
		{
			tNewItem.data.creator_id = -1L;
		}
		tNewItem.created_time_unscaled = (double)Time.time;
		tNewItem.data.created_time -= (double)((float)pFakeCreationYear * 60f);
		tNewItem.data.created_by_player = pByPlayer;
		if (pKingdom != null)
		{
			tNewItem.data.byColor = pKingdom.getColor().color_text;
			tNewItem.data.creator_kingdom_id = pKingdom.id;
			tNewItem.data.from = pKingdom.name;
			tNewItem.data.fromColor = pKingdom.getColor().color_text;
		}
		tNewItem.initItem();
		return tNewItem;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000E850C File Offset: 0x000E670C
	public override void removeObject(Item pObject)
	{
		base.removeObject(pObject);
		pObject.setShouldBeRemoved();
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000E851B File Offset: 0x000E671B
	public override void clear()
	{
		base.clear();
		this.unique_legendary_names.Clear();
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000E852E File Offset: 0x000E672E
	private bool tryToAddMod(Item pItem, ItemModAsset pModAsset)
	{
		return pItem.addMod(pModAsset);
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000E8538 File Offset: 0x000E6738
	private bool checkModName(Item pItem, ItemModAsset pModAsset, EquipmentAsset pItemAsset, Actor pActor, out string pName)
	{
		pName = null;
		if (pModAsset.quality == Rarity.R3_Legendary)
		{
			int loop = 0;
			while (string.IsNullOrEmpty(pName) || this.unique_legendary_names.Contains(pName))
			{
				string tNameTemplate = pItemAsset.getRandomNameTemplate(pActor);
				NameGeneratorAsset tNameAsset = AssetManager.name_generator.get(tNameTemplate);
				pName = NameGenerator.generateNameFromTemplate(tNameAsset, pActor, null, false, 0, null, null, false, null, ActorSex.None, false);
				if (++loop > 100)
				{
					this.unique_legendary_names.Clear();
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000E85B8 File Offset: 0x000E67B8
	public override void checkDeadObjects()
	{
		base.checkDeadObjects();
		if (!this.isDirty())
		{
			return;
		}
		foreach (Item tItem in this)
		{
			if (tItem.isReadyForRemoval())
			{
				this._to_remove.Add(tItem);
			}
		}
		foreach (Item tItem2 in this._to_remove)
		{
			this.removeObject(tItem2);
		}
		this._to_remove.Clear();
		this._dirty = false;
	}

	// Token: 0x0400133C RID: 4924
	private HashSet<string> unique_legendary_names = new HashSet<string>();

	// Token: 0x0400133D RID: 4925
	private List<Item> _to_remove = new List<Item>();

	// Token: 0x0400133E RID: 4926
	private bool _dirty;
}
