using System;
using ai.behaviours;

// Token: 0x020004D0 RID: 1232
public class TesterBehRequireUnits : BehaviourActionTester
{
	// Token: 0x060029F2 RID: 10738 RVA: 0x0014A44D File Offset: 0x0014864D
	public TesterBehRequireUnits(string pActorAssetID, int pAmount, RequireCondition pCondition = RequireCondition.AtLeast)
	{
		this._actor_asset_id = pActorAssetID;
		this._amount = pAmount;
		this._cond = pCondition;
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x0014A46C File Offset: 0x0014866C
	public override BehResult execute(AutoTesterBot pObject)
	{
		int tCount = AssetManager.actor_library.get(this._actor_asset_id).units.Count;
		switch (this._cond)
		{
		case RequireCondition.AtLeast:
			if (tCount < this._amount)
			{
				goto IL_57;
			}
			break;
		case RequireCondition.AtMost:
			if (tCount > this._amount)
			{
				goto IL_57;
			}
			break;
		case RequireCondition.Exactly:
			if (tCount != this._amount)
			{
				goto IL_57;
			}
			break;
		default:
			goto IL_57;
		}
		return BehResult.Continue;
		IL_57:
		pObject.wait = 1.5f;
		return BehResult.Stop;
	}

	// Token: 0x04001F45 RID: 8005
	private string _actor_asset_id;

	// Token: 0x04001F46 RID: 8006
	private int _amount;

	// Token: 0x04001F47 RID: 8007
	private RequireCondition _cond;
}
