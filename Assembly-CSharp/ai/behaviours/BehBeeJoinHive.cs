using System;

namespace ai.behaviours
{
	// Token: 0x02000921 RID: 2337
	public class BehBeeJoinHive : BehaviourActionActor
	{
		// Token: 0x060045DA RID: 17882 RVA: 0x001D3E08 File Offset: 0x001D2008
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x001D3E20 File Offset: 0x001D2020
		public override BehResult execute(Actor pActor)
		{
			Building tHomeBuilding = pActor.beh_building_target;
			pActor.setHomeBuilding(tHomeBuilding);
			return BehResult.Continue;
		}
	}
}
