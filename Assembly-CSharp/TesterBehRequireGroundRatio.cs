using System;
using ai.behaviours;

// Token: 0x020004CF RID: 1231
public class TesterBehRequireGroundRatio : BehaviourActionTester
{
	// Token: 0x060029F0 RID: 10736 RVA: 0x0014A3D0 File Offset: 0x001485D0
	public TesterBehRequireGroundRatio(float pRatio, RequireCondition pCondition = RequireCondition.AtLeast)
	{
		this._ratio = pRatio;
		this._cond = pCondition;
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x0014A3E8 File Offset: 0x001485E8
	public override BehResult execute(AutoTesterBot pObject)
	{
		float tCurrentRatio = BehaviourActionBase<AutoTesterBot>.world.islands_calculator.realGroundRatio();
		switch (this._cond)
		{
		case RequireCondition.AtLeast:
			if (tCurrentRatio < this._ratio)
			{
				goto IL_4C;
			}
			break;
		case RequireCondition.AtMost:
			if (tCurrentRatio > this._ratio)
			{
				goto IL_4C;
			}
			break;
		case RequireCondition.Exactly:
			if (tCurrentRatio != this._ratio)
			{
				goto IL_4C;
			}
			break;
		default:
			goto IL_4C;
		}
		return BehResult.Continue;
		IL_4C:
		pObject.wait = 1.5f;
		return BehResult.Stop;
	}

	// Token: 0x04001F43 RID: 8003
	private float _ratio;

	// Token: 0x04001F44 RID: 8004
	private RequireCondition _cond;
}
