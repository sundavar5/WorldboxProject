using System;

namespace ai.behaviours
{
	// Token: 0x02000884 RID: 2180
	public class BehActorSetFlying : BehaviourActionActor
	{
		// Token: 0x0600444B RID: 17483 RVA: 0x001CD82F File Offset: 0x001CBA2F
		public BehActorSetFlying(bool pFlying)
		{
			this._flying = pFlying;
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x001CD83E File Offset: 0x001CBA3E
		public override BehResult execute(Actor pActor)
		{
			pActor.setFlying(this._flying);
			return BehResult.Continue;
		}

		// Token: 0x04003162 RID: 12642
		private bool _flying;
	}
}
