using System;
using ai.behaviours;

// Token: 0x020004C9 RID: 1225
public class TesterBehPause : BehaviourActionTester
{
	// Token: 0x060029E6 RID: 10726 RVA: 0x0014A10D File Offset: 0x0014830D
	public TesterBehPause(bool pValue)
	{
		this.value = pValue;
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x0014A11C File Offset: 0x0014831C
	public override BehResult execute(AutoTesterBot pObject)
	{
		Config.paused = this.value;
		return BehResult.Continue;
	}

	// Token: 0x04001F3A RID: 7994
	private bool value;
}
