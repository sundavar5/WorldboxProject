using System;
using System.Collections.Generic;

// Token: 0x02000563 RID: 1379
public class DelayedActionsManager
{
	// Token: 0x06002CBF RID: 11455 RVA: 0x0015E814 File Offset: 0x0015CA14
	public void update(float pElapsed, float pDeltaTime)
	{
		for (int i = 0; i < this._delayed_actions.Count; i++)
		{
			DelayedAction tAction = this._delayed_actions[i];
			if (tAction.update(pElapsed, pDeltaTime))
			{
				this._to_remove.Add(tAction);
			}
		}
		for (int j = 0; j < this._to_remove.Count; j++)
		{
			DelayedAction tAction2 = this._to_remove[j];
			this._delayed_actions.Remove(tAction2);
		}
		this._to_remove.Clear();
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x0015E895 File Offset: 0x0015CA95
	public static void addAction(Action pAction, float pDelay, bool pGameSpeed = true)
	{
		MapBox.instance.delayed_actions_manager.addActionInstance(pAction, pDelay, pGameSpeed);
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x0015E8AC File Offset: 0x0015CAAC
	private void addActionInstance(Action pAction, float pDelay, bool pGameSpeed = true)
	{
		DelayedAction tAction = new DelayedAction(pAction, pDelay, pGameSpeed);
		this._delayed_actions.Add(tAction);
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x0015E8CE File Offset: 0x0015CACE
	public void clear()
	{
		this._delayed_actions.Clear();
		this._to_remove.Clear();
	}

	// Token: 0x0400224E RID: 8782
	private List<DelayedAction> _delayed_actions = new List<DelayedAction>();

	// Token: 0x0400224F RID: 8783
	private List<DelayedAction> _to_remove = new List<DelayedAction>();
}
