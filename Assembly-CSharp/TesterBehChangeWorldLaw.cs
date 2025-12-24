using System;
using ai.behaviours;

// Token: 0x020004B4 RID: 1204
public class TesterBehChangeWorldLaw : BehaviourActionTester
{
	// Token: 0x060029BA RID: 10682 RVA: 0x0014967E File Offset: 0x0014787E
	public TesterBehChangeWorldLaw(string pWorldLaw, bool pValue)
	{
		this.world_law = pWorldLaw;
		this.value = pValue;
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x00149694 File Offset: 0x00147894
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehaviourActionBase<AutoTesterBot>.world.world_laws.dict[this.world_law].boolVal = this.value;
		return BehResult.Continue;
	}

	// Token: 0x04001F29 RID: 7977
	private string world_law;

	// Token: 0x04001F2A RID: 7978
	private bool value;
}
