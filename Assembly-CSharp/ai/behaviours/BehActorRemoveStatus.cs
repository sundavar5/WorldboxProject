using System;

namespace ai.behaviours
{
	// Token: 0x02000881 RID: 2177
	public class BehActorRemoveStatus : BehaviourActionActor
	{
		// Token: 0x06004445 RID: 17477 RVA: 0x001CD7C2 File Offset: 0x001CB9C2
		public BehActorRemoveStatus(string pStatus)
		{
			this.status = pStatus;
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x001CD7D1 File Offset: 0x001CB9D1
		public override BehResult execute(Actor pActor)
		{
			pActor.finishStatusEffect(this.status);
			return BehResult.Continue;
		}

		// Token: 0x0400315E RID: 12638
		private string status;
	}
}
