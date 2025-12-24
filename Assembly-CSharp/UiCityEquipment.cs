using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000660 RID: 1632
public class UiCityEquipment : CitySortableElement
{
	// Token: 0x060034EA RID: 13546 RVA: 0x001875A9 File Offset: 0x001857A9
	protected override void Awake()
	{
		this._pool_equipment = new ObjectPoolGenericMono<EquipmentButton>(this._prefab_equipment, base.transform);
		base.Awake();
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x001875C8 File Offset: 0x001857C8
	protected override IEnumerator showContent()
	{
		UiCityEquipment.<showContent>d__5 <showContent>d__ = new UiCityEquipment.<showContent>d__5(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x001875D8 File Offset: 0x001857D8
	private void loadEquipmentButton(Item pItem, long pItemID)
	{
		EquipmentButton tButton = this._pool_equipment.getNext();
		tButton.load(pItem);
		this._equipment[pItemID] = tButton;
	}

	// Token: 0x060034ED RID: 13549 RVA: 0x00187608 File Offset: 0x00185808
	protected override void onListChange()
	{
		List<long> tList = base.city.getEquipmentList(this._equipment_type);
		if (!tList.SetEquals(this._equipment.Keys))
		{
			return;
		}
		tList.Sort((long a, long b) => this._equipment[a].transform.GetSiblingIndex().CompareTo(this._equipment[b].transform.GetSiblingIndex()));
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x0018764D File Offset: 0x0018584D
	protected override void clear()
	{
		this._equipment.Clear();
		this._pool_equipment.clear(true);
		base.clear();
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x0018766C File Offset: 0x0018586C
	protected override void clearInitial()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform tTransform = base.transform.GetChild(i);
			if (!(tTransform.name == "Title"))
			{
				Object.Destroy(tTransform.gameObject);
			}
		}
		base.clearInitial();
	}

	// Token: 0x040027C0 RID: 10176
	[SerializeField]
	private EquipmentType _equipment_type;

	// Token: 0x040027C1 RID: 10177
	[SerializeField]
	private EquipmentButton _prefab_equipment;

	// Token: 0x040027C2 RID: 10178
	private ObjectPoolGenericMono<EquipmentButton> _pool_equipment;

	// Token: 0x040027C3 RID: 10179
	private Dictionary<long, EquipmentButton> _equipment = new Dictionary<long, EquipmentButton>();
}
