using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
public class UnitStatusContainer : UnitElement
{
	// Token: 0x060038AC RID: 14508 RVA: 0x00195D67 File Offset: 0x00193F67
	protected override void Awake()
	{
		this._pool_status = new ObjectPoolGenericMono<StatusEffectButton>(this._prefab_status, this._grid);
		base.Awake();
	}

	// Token: 0x060038AD RID: 14509 RVA: 0x00195D86 File Offset: 0x00193F86
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
		if (!this.actor.hasAnyStatusEffect())
		{
			yield break;
		}
		this._grid.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(0.025f);
		foreach (Status tData in this.actor.getStatuses())
		{
			if (!tData.is_finished)
			{
				yield return CoroutineHelper.wait_for_next_frame;
				this.loadStatusButton(tData);
				tData = null;
			}
		}
		Dictionary<string, Status>.ValueCollection.Enumerator enumerator = default(Dictionary<string, Status>.ValueCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060038AE RID: 14510 RVA: 0x00195D95 File Offset: 0x00193F95
	private void loadStatusButton(Status pStatus)
	{
		this._pool_status.getNext().load(pStatus);
	}

	// Token: 0x060038AF RID: 14511 RVA: 0x00195DA8 File Offset: 0x00193FA8
	protected override void clear()
	{
		ObjectPoolGenericMono<StatusEffectButton> pool_status = this._pool_status;
		if (pool_status != null)
		{
			pool_status.clear(true);
		}
		base.clear();
	}

	// Token: 0x04002A0A RID: 10762
	[SerializeField]
	private StatusEffectButton _prefab_status;

	// Token: 0x04002A0B RID: 10763
	[SerializeField]
	private Transform _grid;

	// Token: 0x04002A0C RID: 10764
	private ObjectPoolGenericMono<StatusEffectButton> _pool_status;
}
