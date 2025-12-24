using System;
using UnityEngine;

// Token: 0x020006CF RID: 1743
public class ActorSelectedContainerEquipment : SelectedElementBase<EquipmentButton>
{
	// Token: 0x060037F5 RID: 14325 RVA: 0x00192FCC File Offset: 0x001911CC
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<EquipmentButton>(this._prefab_equipment, this._grid);
	}

	// Token: 0x060037F6 RID: 14326 RVA: 0x00192FE5 File Offset: 0x001911E5
	public void update(Actor pActor)
	{
		if (!pActor.hasEquipment())
		{
			base.clear();
			return;
		}
		this.refresh(pActor);
	}

	// Token: 0x060037F7 RID: 14327 RVA: 0x00193000 File Offset: 0x00191200
	protected override void refresh(NanoObject pNano)
	{
		base.clear();
		foreach (Item tItem in ((Actor)pNano).equipment.getItems())
		{
			this.loadEquipmentButton(tItem);
		}
	}

	// Token: 0x060037F8 RID: 14328 RVA: 0x00193060 File Offset: 0x00191260
	private void loadEquipmentButton(Item pItem)
	{
		this._pool.getNext().load(pItem);
	}

	// Token: 0x04002988 RID: 10632
	[SerializeField]
	private EquipmentButton _prefab_equipment;
}
