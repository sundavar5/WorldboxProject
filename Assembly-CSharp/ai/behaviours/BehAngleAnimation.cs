using System;

namespace ai.behaviours
{
	// Token: 0x02000887 RID: 2183
	public class BehAngleAnimation : BehaviourActionActor
	{
		// Token: 0x06004451 RID: 17489 RVA: 0x001CD8BD File Offset: 0x001CBABD
		public BehAngleAnimation(AngleAnimationTarget pTarget, string pSound = null, float pTimerAction = 0f, float pAngle = 40f, bool pCheckFlip = true, bool pLandIfHovering = false)
		{
			this._sound_event_id = pSound;
			this._angle = pAngle;
			this._target = pTarget;
			this._timer_action = pTimerAction;
			this._check_flip = pCheckFlip;
			this.land_if_hovering = pLandIfHovering;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x001CD8F4 File Offset: 0x001CBAF4
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			switch (this._target)
			{
			case AngleAnimationTarget.Tile:
				this.null_check_tile_target = true;
				return;
			case AngleAnimationTarget.Building:
				this.null_check_building_target = true;
				this.check_building_target_non_usable = true;
				return;
			case AngleAnimationTarget.Actor:
				this.null_check_actor_target = true;
				return;
			case AngleAnimationTarget.Ruin:
				this.null_check_building_target = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x001CD94C File Offset: 0x001CBB4C
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTarget = pActor.current_tile;
			switch (this._target)
			{
			case AngleAnimationTarget.Tile:
				tTarget = pActor.beh_tile_target;
				break;
			case AngleAnimationTarget.Building:
				tTarget = pActor.beh_building_target.current_tile;
				pActor.beh_building_target.startShake(0.3f, 0.1f, 0.1f);
				break;
			case AngleAnimationTarget.Actor:
				if (pActor.beh_actor_target.a.isInsideSomething())
				{
					return BehResult.Stop;
				}
				tTarget = pActor.beh_actor_target.current_tile;
				break;
			}
			pActor.punchTargetAnimation(tTarget.posV3, this._check_flip, false, this._angle);
			if (!string.IsNullOrEmpty(this._sound_event_id))
			{
				MusicBox.playSound(this._sound_event_id, tTarget, true, false);
			}
			pActor.timer_action = this._timer_action;
			return BehResult.Continue;
		}

		// Token: 0x04003165 RID: 12645
		private AngleAnimationTarget _target;

		// Token: 0x04003166 RID: 12646
		private float _timer_action;

		// Token: 0x04003167 RID: 12647
		private float _angle;

		// Token: 0x04003168 RID: 12648
		private string _sound_event_id;

		// Token: 0x04003169 RID: 12649
		private bool _check_flip;
	}
}
