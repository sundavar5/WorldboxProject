using System;
using UnityEngine;

// Token: 0x020006D0 RID: 1744
public class ActorSelectedContainerStatus : SelectedElementBase<StatusEffectButton>
{
	// Token: 0x060037FA RID: 14330 RVA: 0x0019307B File Offset: 0x0019127B
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<StatusEffectButton>(this._prefab_status, this._grid);
	}

	// Token: 0x060037FB RID: 14331 RVA: 0x00193094 File Offset: 0x00191294
	public void update(NanoObject pNano)
	{
		this.refresh(pNano);
	}

	// Token: 0x060037FC RID: 14332 RVA: 0x001930A0 File Offset: 0x001912A0
	protected override void refresh(NanoObject pNano)
	{
		base.clear();
		foreach (Status tData in ((Actor)pNano).getStatuses())
		{
			if (!tData.is_finished)
			{
				this.loadStatusButton(tData);
			}
		}
	}

	// Token: 0x060037FD RID: 14333 RVA: 0x00193108 File Offset: 0x00191308
	private void loadStatusButton(Status pStatus)
	{
		StatusEffectButton next = this._pool.getNext();
		next.load(pStatus);
		next.setUpdatableTooltip(true);
	}

	// Token: 0x04002989 RID: 10633
	[SerializeField]
	private StatusEffectButton _prefab_status;
}
