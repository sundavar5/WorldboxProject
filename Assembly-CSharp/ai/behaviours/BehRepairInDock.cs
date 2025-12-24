using System;

namespace ai.behaviours
{
	// Token: 0x02000939 RID: 2361
	public class BehRepairInDock : BehCityActor
	{
		// Token: 0x0600460F RID: 17935 RVA: 0x001D4849 File Offset: 0x001D2A49
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x001D4860 File Offset: 0x001D2A60
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasMaxHealth())
			{
				return BehResult.Continue;
			}
			int tDamage = pActor.getMaxHealth() - pActor.getHealth();
			tDamage = ((tDamage > 100) ? 100 : tDamage);
			pActor.restoreHealth(tDamage);
			float tDamageTimer = (float)(tDamage / 25);
			pActor.timer_action = (float)Math.Ceiling((double)tDamageTimer);
			pActor.stayInBuilding(pActor.beh_building_target);
			pActor.beh_tile_target = null;
			pActor.beh_building_target.startShake(0.5f, 0.1f, 0.1f);
			if (!pActor.hasMaxHealth())
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
