using System;

namespace ai.behaviours
{
	// Token: 0x020008B0 RID: 2224
	public class BehConsumeActorsBloodTarget : BehaviourActionActor
	{
		// Token: 0x060044B9 RID: 17593 RVA: 0x001CEF7F File Offset: 0x001CD17F
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_actor_target = true;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x001CEF90 File Offset: 0x001CD190
		public override BehResult execute(Actor pActor)
		{
			Actor tActorTarget = pActor.beh_actor_target.a;
			if (Toolbox.DistTile(pActor.current_tile, tActorTarget.current_tile) > 1f)
			{
				return BehResult.StepBack;
			}
			this.consume(pActor, tActorTarget);
			if (tActorTarget.hasHealth())
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x001CEFD8 File Offset: 0x001CD1D8
		private void consume(Actor pMain, Actor pTarget)
		{
			pMain.timer_action = 0.3f;
			if (pMain.current_position.x > pTarget.current_position.x)
			{
				if (pTarget.target_angle.z > -45f)
				{
					pTarget.target_angle.z = pTarget.target_angle.z - BehaviourActionBase<Actor>.world.elapsed * 100f;
					if (pTarget.target_angle.z < -90f)
					{
						pTarget.target_angle.z = -90f;
					}
					pTarget.rotation_cooldown = 1f;
				}
			}
			else if (pTarget.target_angle.z < 45f)
			{
				pTarget.target_angle.z = pTarget.target_angle.z + BehaviourActionBase<Actor>.world.elapsed * 100f;
				pTarget.rotation_cooldown = 1f;
			}
			if (pMain.target_angle.z == 0f)
			{
				pMain.punchTargetAnimation(pTarget.current_position, false, false, -40f);
				int tDamage = (int)((float)pTarget.getMaxHealth() * 0.05f) + 1;
				pTarget.getHit((float)tDamage, true, AttackType.Eaten, pMain, false, false, true);
				pTarget.startShake(0.2f, 0.1f, true, true);
				if (pTarget.hasHealth())
				{
					pMain.addNutritionFromEating(10, false, false);
				}
				else
				{
					pMain.addNutritionFromEating(100, true, true);
					pMain.countConsumed();
				}
			}
			pTarget.cancelAllBeh();
		}
	}
}
