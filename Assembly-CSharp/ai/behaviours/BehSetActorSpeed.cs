using System;

namespace ai.behaviours
{
	// Token: 0x020008F9 RID: 2297
	public class BehSetActorSpeed : BehaviourActionActor
	{
		// Token: 0x06004569 RID: 17769 RVA: 0x001D221F File Offset: 0x001D041F
		public BehSetActorSpeed(float pSpeed = 0f)
		{
			this.speed = pSpeed;
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x001D222E File Offset: 0x001D042E
		public override BehResult execute(Actor pActor)
		{
			pActor.stats["speed"] = this.speed;
			return BehResult.Continue;
		}

		// Token: 0x040031AD RID: 12717
		private float speed;
	}
}
