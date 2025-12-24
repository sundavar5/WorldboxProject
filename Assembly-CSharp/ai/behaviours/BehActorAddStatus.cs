using System;

namespace ai.behaviours
{
	// Token: 0x0200087F RID: 2175
	public class BehActorAddStatus : BehaviourActionActor
	{
		// Token: 0x06004441 RID: 17473 RVA: 0x001CD711 File Offset: 0x001CB911
		public BehActorAddStatus(string pStatusID, float pOverrideTimer = -1f, bool pEffectOn = true, bool pAddActionTimer = false)
		{
			this._status_id = pStatusID;
			this._override_timer = pOverrideTimer;
			this._effect_on = pEffectOn;
			this._add_action_timer = pAddActionTimer;
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x001CD736 File Offset: 0x001CB936
		public override BehResult execute(Actor pActor)
		{
			pActor.addStatusEffect(this._status_id, this._override_timer, this._effect_on);
			if (this._add_action_timer)
			{
				pActor.makeWait(this._override_timer);
			}
			return BehResult.Continue;
		}

		// Token: 0x04003158 RID: 12632
		private string _status_id;

		// Token: 0x04003159 RID: 12633
		private float _override_timer;

		// Token: 0x0400315A RID: 12634
		private bool _effect_on;

		// Token: 0x0400315B RID: 12635
		private bool _add_action_timer;
	}
}
