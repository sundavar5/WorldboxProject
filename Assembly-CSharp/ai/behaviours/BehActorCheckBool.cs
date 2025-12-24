using System;

namespace ai.behaviours
{
	// Token: 0x02000880 RID: 2176
	public class BehActorCheckBool : BehaviourActionActor
	{
		// Token: 0x06004443 RID: 17475 RVA: 0x001CD766 File Offset: 0x001CB966
		public BehActorCheckBool(string pBool, string pActionIfHit)
		{
			this.actionIfBool = pActionIfHit;
			this.boolCheck = pBool;
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x001CD77C File Offset: 0x001CB97C
		public override BehResult execute(Actor pActor)
		{
			bool tBoolToCheck;
			pActor.data.get(this.boolCheck, out tBoolToCheck, false);
			if (tBoolToCheck)
			{
				pActor.data.removeBool(this.boolCheck);
				return base.forceTask(pActor, this.actionIfBool, true, false);
			}
			return BehResult.Continue;
		}

		// Token: 0x0400315C RID: 12636
		private string actionIfBool;

		// Token: 0x0400315D RID: 12637
		private string boolCheck;
	}
}
