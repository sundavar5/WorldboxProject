using System;

namespace ai.behaviours
{
	// Token: 0x0200091C RID: 2332
	public class BehCheckSexualReproductionOutside : BehaviourActionActor
	{
		// Token: 0x060045CE RID: 17870 RVA: 0x001D3C68 File Offset: 0x001D1E68
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return BehResult.Stop;
			}
			if (!pActor.hasLover())
			{
				return BehResult.Stop;
			}
			Actor tLover = pActor.lover;
			if (!tLover.canBreed())
			{
				return BehResult.Stop;
			}
			if (!tLover.canCurrentTaskBeCancelledByReproduction())
			{
				return BehResult.Stop;
			}
			if (this.tryStartBreeding(pActor, tLover))
			{
				return BehResult.RepeatStep;
			}
			pActor.addAfterglowStatus();
			return BehResult.Stop;
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x001D3CB7 File Offset: 0x001D1EB7
		internal bool tryStartBreeding(Actor pActor, Actor pLover)
		{
			pActor.setTask("sexual_reproduction_outside", true, true, false);
			pActor.beh_actor_target = pLover;
			pLover.makeWait(10f);
			return true;
		}
	}
}
