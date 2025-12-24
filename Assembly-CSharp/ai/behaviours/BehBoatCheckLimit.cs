using System;

namespace ai.behaviours
{
	// Token: 0x02000927 RID: 2343
	public class BehBoatCheckLimit : BehBoat
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x001D4025 File Offset: 0x001D2225
		public override BehResult execute(Actor pActor)
		{
			if (pActor.getSimpleComponent<Boat>().isHomeDockFull())
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
