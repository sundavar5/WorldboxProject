using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000661 RID: 1633
public class UICityResources : CitySortableElement
{
	// Token: 0x060034F2 RID: 13554 RVA: 0x00187715 File Offset: 0x00185915
	protected override void Awake()
	{
		this._pool_resources = new ObjectPoolGenericMono<ButtonResource>(this._prefab_resource, base.transform);
		base.Awake();
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x00187734 File Offset: 0x00185934
	protected override IEnumerator showContent()
	{
		this.showResources();
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x00187744 File Offset: 0x00185944
	protected void showResources()
	{
		this._loaded_slots.Clear();
		this._pool_resources.clear(true);
		if (base.city.hasStorages())
		{
			using (ListPool<CityStorageSlot> tList = base.city.getTotalResourceSlots(this._res_types))
			{
				foreach (CityStorageSlot ptr in tList)
				{
					CityStorageSlot tSlot = ptr;
					this.loadResource(tSlot);
				}
			}
		}
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x001877E0 File Offset: 0x001859E0
	private void loadResource(CityStorageSlot pSlot)
	{
		ButtonResource tB = this._pool_resources.getNext();
		tB.load(pSlot.asset, pSlot.amount);
		this._loaded_slots[pSlot] = tB;
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x00187818 File Offset: 0x00185A18
	protected override void onListChange()
	{
		if (!base.city.hasStorages())
		{
			return;
		}
		using (ListPool<CityStorageSlot> tCheckList = base.city.getTotalResourceSlots(this._res_types))
		{
			if (tCheckList.SetEquals(this._loaded_slots.Keys))
			{
				using (ListPool<CityStorageSlot> tList = new ListPool<CityStorageSlot>(this._loaded_slots.Keys))
				{
					tList.Sort((CityStorageSlot a, CityStorageSlot b) => a.asset.order.CompareTo(b.asset.order));
					tList.RemoveAll((CityStorageSlot pSlot) => pSlot.amount == 0);
					using (ListPool<int> tSortOrder = new ListPool<int>(tList.Count))
					{
						foreach (CityStorageSlot ptr in tList)
						{
							CityStorageSlot tSlot = ptr;
							tSortOrder.Add(tSlot.asset.order);
						}
						tList.Sort((CityStorageSlot a, CityStorageSlot b) => this._loaded_slots[a].transform.GetSiblingIndex().CompareTo(this._loaded_slots[b].transform.GetSiblingIndex()));
						for (int i = 0; i < tSortOrder.Count; i++)
						{
							tList[i].asset.order = tSortOrder[i];
						}
					}
				}
			}
		}
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x001879C8 File Offset: 0x00185BC8
	protected override void clear()
	{
		this._loaded_slots.Clear();
		this._pool_resources.clear(true);
		base.clear();
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x001879E8 File Offset: 0x00185BE8
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

	// Token: 0x040027C4 RID: 10180
	[SerializeField]
	private ResType[] _res_types;

	// Token: 0x040027C5 RID: 10181
	[SerializeField]
	private ButtonResource _prefab_resource;

	// Token: 0x040027C6 RID: 10182
	private ObjectPoolGenericMono<ButtonResource> _pool_resources;

	// Token: 0x040027C7 RID: 10183
	private Dictionary<CityStorageSlot, ButtonResource> _loaded_slots = new Dictionary<CityStorageSlot, ButtonResource>();
}
