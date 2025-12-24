using System;
using ai.behaviours;

// Token: 0x020004C5 RID: 1221
public class TesterBehLog : BehaviourActionTester
{
	// Token: 0x060029DE RID: 10718 RVA: 0x00149EB7 File Offset: 0x001480B7
	public TesterBehLog(string pMessage)
	{
		this._msg = pMessage;
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x00149EC6 File Offset: 0x001480C6
	public override BehResult execute(AutoTesterBot pObject)
	{
		new WorldLogMessage(WorldLogLibrary.auto_tester, this._msg, null, null).add();
		return BehResult.Continue;
	}

	// Token: 0x04001F33 RID: 7987
	private string _msg;
}
