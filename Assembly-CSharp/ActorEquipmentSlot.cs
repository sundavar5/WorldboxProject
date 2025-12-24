using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class ActorEquipmentSlot
{
	// Token: 0x06000863 RID: 2147 RVA: 0x000753CF File Offset: 0x000735CF
	public ActorEquipmentSlot(EquipmentType pType = EquipmentType.Armor)
	{
		this.type = pType;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x000753DE File Offset: 0x000735DE
	public Item getItem()
	{
		return this._item;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x000753E6 File Offset: 0x000735E6
	public bool isEmpty()
	{
		if (this._item == null)
		{
			return true;
		}
		if (this._item.shouldbe_removed)
		{
			Debug.LogError("Item should be removed but it's still in the slot!");
			return true;
		}
		return false;
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000866 RID: 2150 RVA: 0x0007540C File Offset: 0x0007360C
	public bool is_empty
	{
		get
		{
			return this.isEmpty();
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00075414 File Offset: 0x00073614
	public void takeAwayItem()
	{
		if (this.isEmpty())
		{
			return;
		}
		this._item.clearUnit();
		this._item = null;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00075431 File Offset: 0x00073631
	public void setEmptyDebug()
	{
		this._item = null;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0007543A File Offset: 0x0007363A
	internal void setItem(Item pItem, Actor pActor)
	{
		if (!this.isEmpty())
		{
			this.takeAwayItem();
		}
		this._item = pItem;
		this._item.setUnitHasIt(pActor);
		pActor.setStatsDirty();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00075463 File Offset: 0x00073663
	public bool canChangeSlot()
	{
		return this.isEmpty() || !this.getItem().isCursed();
	}

	// Token: 0x040008B8 RID: 2232
	private Item _item;

	// Token: 0x040008B9 RID: 2233
	public EquipmentType type;
}
