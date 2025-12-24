using System;
using ai.behaviours;

// Token: 0x020004B5 RID: 1205
public class TesterBehChangeWorldSpeed : BehaviourActionTester
{
	// Token: 0x060029BC RID: 10684 RVA: 0x001496BC File Offset: 0x001478BC
	public TesterBehChangeWorldSpeed(string pTimeScaleID)
	{
		this._time_scale_id = pTimeScaleID;
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x001496CB File Offset: 0x001478CB
	public override BehResult execute(AutoTesterBot pObject)
	{
		Config.setWorldSpeed(this._time_scale_id, true);
		return BehResult.Continue;
	}

	// Token: 0x04001F2B RID: 7979
	private string _time_scale_id;
}
