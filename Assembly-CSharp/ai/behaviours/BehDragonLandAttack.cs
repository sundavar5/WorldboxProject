using System;

namespace ai.behaviours
{
	// Token: 0x0200094F RID: 2383
	public class BehDragonLandAttack : BehDragon
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x001DCDF8 File Offset: 0x001DAFF8
		public override BehResult execute(Actor pActor)
		{
			SpriteAnimation tSpriteAnimation = this.dragon.spriteAnimation;
			if (tSpriteAnimation.currentFrameIndex == 4)
			{
				pActor.data.set("shouldAttack", true);
			}
			if (tSpriteAnimation.currentFrameIndex == 5)
			{
				bool tShouldAttack;
				pActor.data.get("shouldAttack", out tShouldAttack, false);
				if (tShouldAttack)
				{
					pActor.data.removeBool("shouldAttack");
					foreach (WorldTile tTile in this.dragon.landAttackTiles(pActor.current_tile))
					{
						if (tTile != null && (tTile.hasUnits() || !Randy.randomBool()))
						{
							this.dragon.attackTile(tTile);
						}
					}
					int landAttacks;
					pActor.data.get("landAttacks", out landAttacks, 0);
					pActor.data.set("landAttacks", ++landAttacks);
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
