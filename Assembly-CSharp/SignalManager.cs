using System;
using System.Collections.Generic;

// Token: 0x020005AB RID: 1451
public class SignalManager
{
	// Token: 0x06003012 RID: 12306 RVA: 0x00174178 File Offset: 0x00172378
	public SignalManager()
	{
		SignalManager.instance = this;
	}

	// Token: 0x06003013 RID: 12307 RVA: 0x00174191 File Offset: 0x00172391
	public static void add(SignalAsset pSignal, object pObject = null)
	{
		if (pSignal.isBanned())
		{
			return;
		}
		SignalManager.instance.plan(pSignal, pObject);
	}

	// Token: 0x06003014 RID: 12308 RVA: 0x001741A8 File Offset: 0x001723A8
	private void plan(SignalAsset pSignal, object pObject = null)
	{
		this._signals.TryAdd(pSignal, pObject);
	}

	// Token: 0x06003015 RID: 12309 RVA: 0x001741B8 File Offset: 0x001723B8
	public void update()
	{
		if (this._signals.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<SignalAsset, object> tSignal in this._signals)
		{
			SignalAsset tAsset = tSignal.Key;
			object tObject = tSignal.Value;
			if (tAsset.has_action)
			{
				tAsset.action(tObject);
			}
			if (tAsset.has_action_achievement)
			{
				tAsset.action_achievement(tObject);
			}
			if (tAsset.has_ban_check_action && tAsset.ban_check_action(tObject))
			{
				tAsset.ban();
			}
		}
		this._signals.Clear();
	}

	// Token: 0x04002445 RID: 9285
	public static SignalManager instance;

	// Token: 0x04002446 RID: 9286
	private Dictionary<SignalAsset, object> _signals = new Dictionary<SignalAsset, object>();
}
