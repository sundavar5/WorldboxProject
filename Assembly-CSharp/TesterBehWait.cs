using System;
using ai.behaviours;

// Token: 0x020004EE RID: 1262
public class TesterBehWait : BehaviourActionTester
{
	// Token: 0x06002A32 RID: 10802 RVA: 0x0014BBD4 File Offset: 0x00149DD4
	public TesterBehWait(float pWait)
	{
		this.wait = pWait;
	}

	// Token: 0x06002A33 RID: 10803 RVA: 0x0014BBE3 File Offset: 0x00149DE3
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.wait = this.wait;
		return base.execute(pObject);
	}

	// Token: 0x04001F74 RID: 8052
	private float wait;
}
