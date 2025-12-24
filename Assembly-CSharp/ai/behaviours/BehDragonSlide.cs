using System;

namespace ai.behaviours
{
	// Token: 0x02000954 RID: 2388
	public class BehDragonSlide : BehDragon
	{
		// Token: 0x0600465A RID: 18010 RVA: 0x001DD090 File Offset: 0x001DB290
		public override BehResult execute(Actor pActor)
		{
			SpriteAnimation tSpriteAnimation = this.dragon.spriteAnimation;
			if (tSpriteAnimation.currentFrameIndex == 7)
			{
				foreach (WorldTile tTile in this.dragon.attackRange(pActor.flip))
				{
					if (tTile != null && (tTile.hasUnits() || !Randy.randomBool()))
					{
						this.dragon.attackTile(tTile);
					}
				}
			}
			if (tSpriteAnimation.currentFrameIndex < tSpriteAnimation.frames.Length - 1)
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
