using System;

namespace ai.behaviours
{
	// Token: 0x02000951 RID: 2385
	public class BehDragonSetAnimation : BehDragon
	{
		// Token: 0x06004654 RID: 18004 RVA: 0x001DCF30 File Offset: 0x001DB130
		public BehDragonSetAnimation(DragonState pState, bool pLooped = true, bool pForceRestart = true)
		{
			this.state = pState;
			this.looped = pLooped;
			this.forceRestart = pForceRestart;
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001DCF4D File Offset: 0x001DB14D
		public override BehResult execute(Actor pActor)
		{
			if (pActor.flipAnimationActive())
			{
				return BehResult.RepeatStep;
			}
			SpriteAnimation spriteAnimation = this.dragon.spriteAnimation;
			this.dragon.setFrames(this.state, this.forceRestart);
			spriteAnimation.looped = this.looped;
			return BehResult.Continue;
		}

		// Token: 0x040031E1 RID: 12769
		private DragonState state;

		// Token: 0x040031E2 RID: 12770
		private bool looped;

		// Token: 0x040031E3 RID: 12771
		private bool forceRestart;
	}
}
