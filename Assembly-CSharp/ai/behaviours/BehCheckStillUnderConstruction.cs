using System;

namespace ai.behaviours
{
	// Token: 0x0200089D RID: 2205
	public class BehCheckStillUnderConstruction : BehaviourActionActor
	{
		// Token: 0x06004486 RID: 17542 RVA: 0x001CE275 File Offset: 0x001CC475
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_city = true;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x001CE28B File Offset: 0x001CC48B
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_building_target.isUnderConstruction())
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
