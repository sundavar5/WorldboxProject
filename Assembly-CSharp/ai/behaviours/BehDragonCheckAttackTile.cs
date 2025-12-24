using System;

namespace ai.behaviours
{
	// Token: 0x02000948 RID: 2376
	public class BehDragonCheckAttackTile : BehDragon
	{
		// Token: 0x06004642 RID: 17986 RVA: 0x001DC954 File Offset: 0x001DAB54
		public override BehResult execute(Actor pActor)
		{
			if (this.dragon.aggroTargets.Count == 0)
			{
				return BehResult.Continue;
			}
			Actor tActorToAttack = Toolbox.getClosestActor(this.dragon.aggroTargets, pActor.current_tile);
			if (tActorToAttack != null && tActorToAttack.data != null && tActorToAttack.isAlive() && tActorToAttack.current_tile != null)
			{
				pActor.beh_tile_target = this.dragon.randomTileWithinLandAttackRange(tActorToAttack.current_tile);
				if (pActor.current_tile != this.dragon.lastLanded && this.dragon.landAttackRange(tActorToAttack.current_tile) && Dragon.canLand(pActor, null))
				{
					return base.forceTask(pActor, "dragon_land", true, false);
				}
			}
			if (pActor.isFlying())
			{
				foreach (Actor tActor in this.dragon.aggroTargets)
				{
					if (tActor != null && tActor.isAlive() && this.dragon.targetWithinSlide(tActor.current_tile))
					{
						return base.forceTask(pActor, "dragon_slide", true, false);
					}
				}
				return BehResult.Continue;
			}
			return BehResult.Continue;
		}
	}
}
