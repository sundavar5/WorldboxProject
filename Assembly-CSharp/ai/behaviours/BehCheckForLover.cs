using System;

namespace ai.behaviours
{
	// Token: 0x02000896 RID: 2198
	public class BehCheckForLover : BehCitizenActionCity
	{
		// Token: 0x06004475 RID: 17525 RVA: 0x001CE064 File Offset: 0x001CC264
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001CE07C File Offset: 0x001CC27C
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasLover())
			{
				return BehResult.Stop;
			}
			Actor tLover = pActor.lover;
			if (tLover.isTask("sexual_reproduction_civ_go") && tLover.beh_building_target == pActor.beh_building_target && tLover.ai.action_index > 3)
			{
				pActor.stayInBuilding(pActor.beh_building_target);
				tLover.stayInBuilding(tLover.beh_building_target);
				tLover.setTask("sexual_reproduction_civ_wait", false, false, true);
				return base.forceTask(pActor, "sexual_reproduction_civ_action", false, true);
			}
			if (!tLover.isTask("sexual_reproduction_civ_go"))
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
