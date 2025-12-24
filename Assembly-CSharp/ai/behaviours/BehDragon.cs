using System;

namespace ai.behaviours
{
	// Token: 0x02000944 RID: 2372
	public class BehDragon : BehaviourActionActor
	{
		// Token: 0x0600463A RID: 17978 RVA: 0x001DC7A0 File Offset: 0x001DA9A0
		public override void prepare(Actor pActor)
		{
			base.prepare(pActor);
			this.dragon = (pActor.children_special[0] as Dragon);
		}

		// Token: 0x040031DF RID: 12767
		internal Dragon dragon;
	}
}
