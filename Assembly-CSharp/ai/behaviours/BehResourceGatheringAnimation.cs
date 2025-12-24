using System;

namespace ai.behaviours
{
	// Token: 0x020008F6 RID: 2294
	public class BehResourceGatheringAnimation : BehaviourActionActor
	{
		// Token: 0x06004564 RID: 17764 RVA: 0x001D2135 File Offset: 0x001D0335
		public BehResourceGatheringAnimation(float pTimerAction, string pSound = "", bool pLandIfHovering = true)
		{
			this._sound_event_id = pSound;
			this._timer_action = pTimerAction;
			this.land_if_hovering = pLandIfHovering;
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x001D2152 File Offset: 0x001D0352
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x001D2168 File Offset: 0x001D0368
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.asset.gatherable && !pActor.beh_building_target.hasResourcesToCollect())
			{
				return BehResult.Stop;
			}
			pActor.punchTargetAnimation(pActor.beh_building_target.current_tile.posV3, true, false, 40f);
			pActor.beh_building_target.resourceGathering(BehaviourActionBase<Actor>.world.elapsed);
			pActor.beh_building_target.startShake(0.01f, 0.1f, 0.1f);
			if (!string.IsNullOrEmpty(this._sound_event_id))
			{
				MusicBox.playSound(this._sound_event_id, pActor.beh_building_target.current_tile, true, true);
			}
			pActor.timer_action = this._timer_action;
			return BehResult.Continue;
		}

		// Token: 0x040031A2 RID: 12706
		private float _timer_action;

		// Token: 0x040031A3 RID: 12707
		private string _sound_event_id;
	}
}
