using System;

namespace ai.behaviours
{
	// Token: 0x02000882 RID: 2178
	public class BehActorSetBool : BehaviourActionActor
	{
		// Token: 0x06004447 RID: 17479 RVA: 0x001CD7E0 File Offset: 0x001CB9E0
		public BehActorSetBool(string pBoolName, bool pBoolValue)
		{
			this.boolName = pBoolName;
			this.boolValue = pBoolValue;
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x001CD7F6 File Offset: 0x001CB9F6
		public override BehResult execute(Actor pActor)
		{
			pActor.data.set(this.boolName, this.boolValue);
			return BehResult.Continue;
		}

		// Token: 0x0400315F RID: 12639
		private string boolName;

		// Token: 0x04003160 RID: 12640
		private bool boolValue;
	}
}
