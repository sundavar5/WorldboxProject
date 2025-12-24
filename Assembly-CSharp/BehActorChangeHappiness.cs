using System;
using ai.behaviours;

// Token: 0x02000388 RID: 904
public class BehActorChangeHappiness : BehaviourActionActor
{
	// Token: 0x06002198 RID: 8600 RVA: 0x0011CEC9 File Offset: 0x0011B0C9
	public BehActorChangeHappiness(string pID)
	{
		this._happiness_id = pID;
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x0011CED8 File Offset: 0x0011B0D8
	public override BehResult execute(Actor pActor)
	{
		pActor.changeHappiness(this._happiness_id, 0);
		return BehResult.Continue;
	}

	// Token: 0x040018DD RID: 6365
	private string _happiness_id;
}
