using System;

namespace ai.behaviours
{
	// Token: 0x02000964 RID: 2404
	public class BehUFOExplore : BehaviourActionActor
	{
		// Token: 0x06004682 RID: 18050 RVA: 0x001DE813 File Offset: 0x001DCA13
		public override BehResult execute(Actor pActor)
		{
			pActor.data.set("exploringTicks", Randy.randomInt(3, 7));
			return BehResult.Continue;
		}
	}
}
