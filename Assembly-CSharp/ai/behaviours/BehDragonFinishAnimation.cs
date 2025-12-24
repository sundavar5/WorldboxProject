using System;

namespace ai.behaviours
{
	// Token: 0x0200094C RID: 2380
	public class BehDragonFinishAnimation : BehDragon
	{
		// Token: 0x0600464A RID: 17994 RVA: 0x001DCD00 File Offset: 0x001DAF00
		public override BehResult execute(Actor pActor)
		{
			if (pActor.flipAnimationActive())
			{
				return BehResult.RepeatStep;
			}
			SpriteAnimation tSpriteAnimation = this.dragon.spriteAnimation;
			if (tSpriteAnimation.currentFrameIndex < tSpriteAnimation.frames.Length - 1)
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
