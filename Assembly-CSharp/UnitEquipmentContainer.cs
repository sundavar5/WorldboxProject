using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006DD RID: 1757
public class UnitEquipmentContainer : UnitElement
{
	// Token: 0x0600385D RID: 14429 RVA: 0x00195175 File Offset: 0x00193375
	protected override void Awake()
	{
		this._pool_equipment = new ObjectPoolGenericMono<EquipmentButton>(this._prefab_equipment, this._grid);
		base.Awake();
	}

	// Token: 0x0600385E RID: 14430 RVA: 0x00195194 File Offset: 0x00193394
	protected override IEnumerator showContent()
	{
		if (this.actor == null)
		{
			yield break;
		}
		if (!this.actor.isAlive())
		{
			yield break;
		}
		yield return this.loadEquipment(true);
		yield break;
	}

	// Token: 0x0600385F RID: 14431 RVA: 0x001951A3 File Offset: 0x001933A3
	private IEnumerator loadEquipment(bool pAnimated = true)
	{
		UnitEquipmentContainer.<loadEquipment>d__6 <loadEquipment>d__ = new UnitEquipmentContainer.<loadEquipment>d__6(0);
		<loadEquipment>d__.<>4__this = this;
		<loadEquipment>d__.pAnimated = pAnimated;
		return <loadEquipment>d__;
	}

	// Token: 0x06003860 RID: 14432 RVA: 0x001951BC File Offset: 0x001933BC
	private void loadEquipmentButton(Item pItem)
	{
		EquipmentButton tButton = this._pool_equipment.getNext();
		tButton.load(pItem);
		this._items[pItem] = tButton;
		AugmentationUnlockedAction tAction = new AugmentationUnlockedAction(((IAugmentationsWindow<IEquipmentEditor>)this.unit_window).getEditor().reloadButtons);
		tButton.removeElementUnlockedAction(tAction);
		tButton.addElementUnlockedAction(tAction);
	}

	// Token: 0x06003861 RID: 14433 RVA: 0x0019520F File Offset: 0x0019340F
	protected override void clear()
	{
		this._items.Clear();
		ObjectPoolGenericMono<EquipmentButton> pool_equipment = this._pool_equipment;
		if (pool_equipment != null)
		{
			pool_equipment.clear(true);
		}
		base.clear();
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x00195234 File Offset: 0x00193434
	protected override void clearInitial()
	{
		for (int i = 0; i < this._grid.childCount; i++)
		{
			Transform tTransform = this._grid.GetChild(i);
			if (!(tTransform.name == "Title"))
			{
				Object.Destroy(tTransform.gameObject);
			}
		}
		base.clearInitial();
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x00195287 File Offset: 0x00193487
	public void reloadEquipment(bool pAnimated)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.loadEquipment(pAnimated));
	}

	// Token: 0x040029D8 RID: 10712
	[SerializeField]
	private EquipmentButton _prefab_equipment;

	// Token: 0x040029D9 RID: 10713
	[SerializeField]
	private Transform _grid;

	// Token: 0x040029DA RID: 10714
	private ObjectPoolGenericMono<EquipmentButton> _pool_equipment;

	// Token: 0x040029DB RID: 10715
	private Dictionary<Item, EquipmentButton> _items = new Dictionary<Item, EquipmentButton>();
}
