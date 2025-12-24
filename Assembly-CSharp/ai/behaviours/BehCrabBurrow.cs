using System;

namespace ai.behaviours
{
	// Token: 0x020008B4 RID: 2228
	public class BehCrabBurrow : BehaviourActionActor
	{
		// Token: 0x060044C5 RID: 17605 RVA: 0x001CF2B3 File Offset: 0x001CD4B3
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.force_animation = true;
			this.force_animation_id = "burrow";
			this.special_prevent_can_be_attacked = true;
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x001CF2D4 File Offset: 0x001CD4D4
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isHungry())
			{
				pActor.endJob();
				return BehResult.Stop;
			}
			if (!Toolbox.hasDifferentSpeciesInChunkAround(pActor.current_tile, pActor.asset.id))
			{
				pActor.endJob();
				return BehResult.Stop;
			}
			pActor.timer_action = Randy.randomFloat(10f, 20f);
			return BehResult.RepeatStep;
		}
	}
}
