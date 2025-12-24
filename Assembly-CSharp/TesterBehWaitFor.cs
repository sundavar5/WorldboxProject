using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class TesterBehWaitFor : BehaviourActionTester
{
	// Token: 0x06002A34 RID: 10804 RVA: 0x0014BBF8 File Offset: 0x00149DF8
	public TesterBehWaitFor(string pType, int pAmount)
	{
		this._type = pType;
		this._amount = pAmount;
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x0014BC10 File Offset: 0x00149E10
	public override BehResult execute(AutoTesterBot pObject)
	{
		MetaTypeAsset tAsset = AssetManager.meta_type_library.get(this._type);
		if (tAsset == null)
		{
			Debug.LogError("TesterBehWaitFor: No asset found for type: " + this._type);
			return BehResult.Stop;
		}
		int tCount = 0;
		foreach (NanoObject nanoObject in tAsset.get_list())
		{
			tCount++;
			if (tCount >= this._amount)
			{
				return BehResult.Continue;
			}
		}
		pObject.wait = 1.5f;
		return BehResult.RepeatStep;
	}

	// Token: 0x04001F75 RID: 8053
	private string _type;

	// Token: 0x04001F76 RID: 8054
	private int _amount;
}
