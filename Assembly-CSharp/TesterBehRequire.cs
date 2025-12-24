using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004CE RID: 1230
public class TesterBehRequire : BehaviourActionTester
{
	// Token: 0x060029EE RID: 10734 RVA: 0x0014A2EC File Offset: 0x001484EC
	public TesterBehRequire(string pType, int pAmount, RequireCondition pCondition = RequireCondition.AtLeast)
	{
		this._type = pType;
		this._amount = pAmount;
		this._cond = pCondition;
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x0014A30C File Offset: 0x0014850C
	public override BehResult execute(AutoTesterBot pObject)
	{
		MetaTypeAsset tAsset = AssetManager.meta_type_library.get(this._type);
		if (tAsset == null)
		{
			Debug.LogError("TesterBehRequire: No asset found for type: " + this._type);
			return BehResult.Stop;
		}
		int tCount = 0;
		foreach (NanoObject nanoObject in tAsset.get_list())
		{
			tCount++;
		}
		switch (this._cond)
		{
		case RequireCondition.AtLeast:
			if (tCount < this._amount)
			{
				goto IL_9B;
			}
			break;
		case RequireCondition.AtMost:
			if (tCount > this._amount)
			{
				goto IL_9B;
			}
			break;
		case RequireCondition.Exactly:
			if (tCount != this._amount)
			{
				goto IL_9B;
			}
			break;
		default:
			goto IL_9B;
		}
		return BehResult.Continue;
		IL_9B:
		pObject.wait = 1.5f;
		return BehResult.Stop;
	}

	// Token: 0x04001F40 RID: 8000
	private string _type;

	// Token: 0x04001F41 RID: 8001
	private int _amount;

	// Token: 0x04001F42 RID: 8002
	private RequireCondition _cond;
}
