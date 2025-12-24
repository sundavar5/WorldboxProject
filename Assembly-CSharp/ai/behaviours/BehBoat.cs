using System;

namespace ai.behaviours
{
	// Token: 0x02000924 RID: 2340
	public class BehBoat : BehaviourActionActor
	{
		// Token: 0x060045E1 RID: 17889 RVA: 0x001D3FA9 File Offset: 0x001D21A9
		internal void checkHomeDocks(Actor pActor)
		{
			ActorTool.checkHomeDocks(pActor);
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x001D3FB1 File Offset: 0x001D21B1
		public override void prepare(Actor pActor)
		{
			base.prepare(pActor);
			this.boat = pActor.getSimpleComponent<Boat>();
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001D3FC6 File Offset: 0x001D21C6
		public override BehResult execute(Actor pActor)
		{
			return BehResult.Continue;
		}

		// Token: 0x040031C1 RID: 12737
		internal Boat boat;
	}
}
