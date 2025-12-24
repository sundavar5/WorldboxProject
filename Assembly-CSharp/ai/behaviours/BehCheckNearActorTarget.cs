using System;

namespace ai.behaviours
{
	// Token: 0x0200089A RID: 2202
	public class BehCheckNearActorTarget : BehaviourActionActor
	{
		// Token: 0x0600447E RID: 17534 RVA: 0x001CE15F File Offset: 0x001CC35F
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_actor_target = true;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x001CE170 File Offset: 0x001CC370
		public override BehResult execute(Actor pActor)
		{
			Actor tTarget = pActor.beh_actor_target.a;
			if (!pActor.canTalkWith(tTarget))
			{
				return BehResult.Stop;
			}
			if (Toolbox.SquaredDistVec2Float(pActor.current_position, tTarget.current_position) < 4f)
			{
				return BehResult.Continue;
			}
			return BehResult.RestartTask;
		}
	}
}
