using System;
using ai.behaviours;

// Token: 0x020003C4 RID: 964
public class BehAddHappiness : BehaviourActionActor
{
	// Token: 0x0600224A RID: 8778 RVA: 0x00120615 File Offset: 0x0011E815
	public BehAddHappiness(string pHappinessID)
	{
		this._happiness_id = pHappinessID;
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x00120624 File Offset: 0x0011E824
	public override BehResult execute(Actor pActor)
	{
		pActor.a.changeHappiness(this._happiness_id, 0);
		return BehResult.Continue;
	}

	// Token: 0x040018EB RID: 6379
	private string _happiness_id;
}
