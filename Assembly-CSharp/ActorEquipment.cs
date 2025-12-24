using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200010E RID: 270
public class ActorEquipment : IEnumerable<ActorEquipmentSlot>, IEnumerable
{
	// Token: 0x06000858 RID: 2136 RVA: 0x000750E4 File Offset: 0x000732E4
	public ActorEquipment()
	{
		this.initDictionary();
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00075148 File Offset: 0x00073348
	public void destroyAllEquipment()
	{
		foreach (ActorEquipmentSlot tSlot in this._dictionary.Values)
		{
			if (!tSlot.isEmpty())
			{
				tSlot.takeAwayItem();
			}
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000751A8 File Offset: 0x000733A8
	private void initDictionary()
	{
		this._dictionary = new Dictionary<EquipmentType, ActorEquipmentSlot>();
		this._dictionary.Add(EquipmentType.Helmet, this.helmet);
		this._dictionary.Add(EquipmentType.Armor, this.armor);
		this._dictionary.Add(EquipmentType.Weapon, this.weapon);
		this._dictionary.Add(EquipmentType.Boots, this.boots);
		this._dictionary.Add(EquipmentType.Ring, this.ring);
		this._dictionary.Add(EquipmentType.Amulet, this.amulet);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0007522C File Offset: 0x0007342C
	public bool hasItems()
	{
		using (Dictionary<EquipmentType, ActorEquipmentSlot>.ValueCollection.Enumerator enumerator = this._dictionary.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isEmpty())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0007528C File Offset: 0x0007348C
	public IEnumerable<Item> getItems()
	{
		foreach (ActorEquipmentSlot tSlot in this._dictionary.Values)
		{
			if (!tSlot.isEmpty())
			{
				yield return tSlot.getItem();
			}
		}
		Dictionary<EquipmentType, ActorEquipmentSlot>.ValueCollection.Enumerator enumerator = default(Dictionary<EquipmentType, ActorEquipmentSlot>.ValueCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0007529C File Offset: 0x0007349C
	public List<long> getDataForSave()
	{
		List<long> tItemsIDs = new List<long>();
		foreach (ActorEquipmentSlot tSlot in this)
		{
			tItemsIDs.Add(tSlot.getItem().data.id);
		}
		if (tItemsIDs.Count == 0)
		{
			return null;
		}
		return tItemsIDs;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00075304 File Offset: 0x00073504
	public void load(List<long> pList, Actor pActor)
	{
		if (pList == null || pList.Count == 0)
		{
			return;
		}
		foreach (long tID in pList)
		{
			Item tItem = World.world.items.get(tID);
			if (tItem != null)
			{
				EquipmentAsset tAsset = tItem.getAsset();
				if (tAsset != null)
				{
					this.getSlot(tAsset.equipment_type).setItem(tItem, pActor);
				}
			}
		}
		this.initDictionary();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00075390 File Offset: 0x00073590
	public void setItem(Item pItem, Actor pActor)
	{
		this.getSlot(pItem.getAsset().equipment_type).setItem(pItem, pActor);
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x000753AA File Offset: 0x000735AA
	public ActorEquipmentSlot getSlot(EquipmentType pType)
	{
		return this._dictionary[pType];
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x000753B8 File Offset: 0x000735B8
	public IEnumerator<ActorEquipmentSlot> GetEnumerator()
	{
		foreach (ActorEquipmentSlot tSlot in this._dictionary.Values)
		{
			if (!tSlot.isEmpty())
			{
				yield return tSlot;
			}
		}
		Dictionary<EquipmentType, ActorEquipmentSlot>.ValueCollection.Enumerator enumerator = default(Dictionary<EquipmentType, ActorEquipmentSlot>.ValueCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x000753C7 File Offset: 0x000735C7
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x040008AF RID: 2223
	public const int SLOTS_AMOUNT = 6;

	// Token: 0x040008B0 RID: 2224
	public const string NONE = "none";

	// Token: 0x040008B1 RID: 2225
	public ActorEquipmentSlot helmet = new ActorEquipmentSlot(EquipmentType.Helmet);

	// Token: 0x040008B2 RID: 2226
	public ActorEquipmentSlot armor = new ActorEquipmentSlot(EquipmentType.Armor);

	// Token: 0x040008B3 RID: 2227
	public ActorEquipmentSlot weapon = new ActorEquipmentSlot(EquipmentType.Weapon);

	// Token: 0x040008B4 RID: 2228
	public ActorEquipmentSlot boots = new ActorEquipmentSlot(EquipmentType.Boots);

	// Token: 0x040008B5 RID: 2229
	public ActorEquipmentSlot ring = new ActorEquipmentSlot(EquipmentType.Ring);

	// Token: 0x040008B6 RID: 2230
	public ActorEquipmentSlot amulet = new ActorEquipmentSlot(EquipmentType.Amulet);

	// Token: 0x040008B7 RID: 2231
	private Dictionary<EquipmentType, ActorEquipmentSlot> _dictionary;
}
