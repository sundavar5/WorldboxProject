using System;

namespace ai.behaviours
{
	// Token: 0x02000956 RID: 2390
	public class BehFinger : BehaviourActionActor
	{
		// Token: 0x0600465E RID: 18014 RVA: 0x001DD268 File Offset: 0x001DB468
		public override void prepare(Actor pActor)
		{
			this.finger = (pActor.children_special[0] as GodFinger);
			base.prepare(pActor);
		}

		// Token: 0x040031E4 RID: 12772
		protected GodFinger finger;

		// Token: 0x040031E5 RID: 12773
		public bool drawing_action;
	}
}
