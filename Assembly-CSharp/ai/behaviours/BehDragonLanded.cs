using System;

namespace ai.behaviours
{
	// Token: 0x02000950 RID: 2384
	public class BehDragonLanded : BehDragon
	{
		// Token: 0x06004652 RID: 18002 RVA: 0x001DCF14 File Offset: 0x001DB114
		public override BehResult execute(Actor pActor)
		{
			this.dragon.lastLanded = pActor.current_tile;
			return BehResult.Continue;
		}
	}
}
