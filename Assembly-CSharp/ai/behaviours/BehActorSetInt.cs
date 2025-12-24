using System;

namespace ai.behaviours
{
	// Token: 0x02000885 RID: 2181
	public class BehActorSetInt : BehaviourActionActor
	{
		// Token: 0x0600444D RID: 17485 RVA: 0x001CD84D File Offset: 0x001CBA4D
		public BehActorSetInt(string pIntName, int pIntValue)
		{
			this.intName = pIntName;
			this.intValue = pIntValue;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x001CD863 File Offset: 0x001CBA63
		public override BehResult execute(Actor pActor)
		{
			pActor.data.set(this.intName, this.intValue);
			return BehResult.Continue;
		}

		// Token: 0x04003163 RID: 12643
		private string intName;

		// Token: 0x04003164 RID: 12644
		private int intValue;
	}
}
