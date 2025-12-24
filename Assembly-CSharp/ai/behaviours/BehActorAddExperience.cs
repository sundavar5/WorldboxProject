using System;

namespace ai.behaviours
{
	// Token: 0x0200087E RID: 2174
	public class BehActorAddExperience : BehaviourActionActor
	{
		// Token: 0x0600443F RID: 17471 RVA: 0x001CD6CF File Offset: 0x001CB8CF
		public BehActorAddExperience(int pMin, int pMax)
		{
			this._min = pMin;
			this._max = pMax;
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x001CD6E8 File Offset: 0x001CB8E8
		public override BehResult execute(Actor pActor)
		{
			int tValue = Randy.randomInt(this._min, this._max + 1);
			pActor.addExperience(tValue);
			return BehResult.Continue;
		}

		// Token: 0x04003156 RID: 12630
		private int _min;

		// Token: 0x04003157 RID: 12631
		private int _max;
	}
}
