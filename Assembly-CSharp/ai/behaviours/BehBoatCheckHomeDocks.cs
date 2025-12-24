using System;

namespace ai.behaviours
{
	// Token: 0x02000926 RID: 2342
	public class BehBoatCheckHomeDocks : BehBoat
	{
		// Token: 0x060045E7 RID: 17895 RVA: 0x001D3FFF File Offset: 0x001D21FF
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			if (this.boat.actor.getHomeBuilding() == null)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
