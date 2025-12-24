using System;

namespace ai.behaviours
{
	// Token: 0x02000883 RID: 2179
	public class BehActorSetFlip : BehaviourActionActor
	{
		// Token: 0x06004449 RID: 17481 RVA: 0x001CD811 File Offset: 0x001CBA11
		public BehActorSetFlip(bool pFlip)
		{
			this.flip = pFlip;
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x001CD820 File Offset: 0x001CBA20
		public override BehResult execute(Actor pActor)
		{
			pActor.setFlip(this.flip);
			return BehResult.Continue;
		}

		// Token: 0x04003161 RID: 12641
		private bool flip;
	}
}
