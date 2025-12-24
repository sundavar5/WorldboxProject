using System;

namespace ai.behaviours
{
	// Token: 0x0200095F RID: 2399
	public class BehFingerWaitForFlying : BehFinger
	{
		// Token: 0x06004678 RID: 18040 RVA: 0x001DE4A2 File Offset: 0x001DC6A2
		public override BehResult execute(Actor pActor)
		{
			if (this.finger.flying_target != pActor.position_height)
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
