using System;

namespace ai.behaviours
{
	// Token: 0x02000932 RID: 2354
	public class BehBoatRemoveIfLimit : BehBoat
	{
		// Token: 0x06004601 RID: 17921 RVA: 0x001D43A6 File Offset: 0x001D25A6
		public override BehResult execute(Actor pActor)
		{
			if (this.boat.isHomeDockOverfilled())
			{
				this.boat.destroyBecauseOverfilled();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
