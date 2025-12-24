using System;
using ai.behaviours;

// Token: 0x020003C1 RID: 961
public class BehAddAggroForBehTarget : BehaviourActionActor
{
	// Token: 0x06002241 RID: 8769 RVA: 0x00120404 File Offset: 0x0011E604
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.null_check_actor_target = true;
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x00120413 File Offset: 0x0011E613
	public override BehResult execute(Actor pActor)
	{
		pActor.addAggro(pActor.beh_actor_target.a);
		return BehResult.Continue;
	}
}
