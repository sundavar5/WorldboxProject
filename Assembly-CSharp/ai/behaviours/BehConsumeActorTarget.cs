using System;

namespace ai.behaviours
{
	// Token: 0x020008AF RID: 2223
	public class BehConsumeActorTarget : BehaviourActionActor
	{
		// Token: 0x060044B5 RID: 17589 RVA: 0x001CED8C File Offset: 0x001CCF8C
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_actor_target = true;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x001CED9C File Offset: 0x001CCF9C
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

		// Token: 0x060044B7 RID: 17591 RVA: 0x001CEDE4 File Offset: 0x001CCFE4
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
			if (pTarget.hasTrait("poisonous"))
			{
				pMain.addStatusEffect("poisoned", 0f, true);
			}
			if (pMain.target_angle.z == 0f)
			{
				pMain.punchTargetAnimation(pTarget.current_position, false, false, 40f);
				int tDamage = (int)((float)pTarget.getMaxHealth() * 0.15f) + 1;
				Clan tTargetClan = pTarget.clan;
				pTarget.getHit((float)tDamage, true, AttackType.Eaten, pMain, false, false, true);
				pTarget.startShake(0.2f, 0.1f, true, true);
				if (pTarget.hasHealth())
				{
					pMain.addNutritionFromEating(tDamage, false, false);
				}
				else
				{
					pMain.addNutritionFromEating(100, true, true);
					pMain.countConsumed();
					AchievementLibrary.clannibals.checkBySignal(new ValueTuple<Actor, Clan>(pMain, tTargetClan));
				}
			}
			pTarget.cancelAllBeh();
		}
	}
}
