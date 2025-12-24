using System;

namespace ai.behaviours
{
	// Token: 0x02000888 RID: 2184
	public class BehAnimalBreedingTime : BehaviourActionActor
	{
		// Token: 0x06004454 RID: 17492 RVA: 0x001CDA0D File Offset: 0x001CBC0D
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_actor_target = true;
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x001CDA1C File Offset: 0x001CBC1C
		public override BehResult execute(Actor pActor)
		{
			if (Toolbox.DistTile(pActor.current_tile, pActor.beh_actor_target.current_tile) > 1f)
			{
				return BehResult.Stop;
			}
			pActor.beh_actor_target.a.startShake(0.3f, 0.1f, true, true);
			pActor.startShake(0.3f, 0.1f, true, true);
			pActor.beh_actor_target.a.timer_action = 2f;
			EffectsLibrary.spawnAt("fx_hearts", pActor.current_position, 0.15f);
			return BehResult.Continue;
		}
	}
}
