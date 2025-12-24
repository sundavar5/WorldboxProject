using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020008B5 RID: 2229
	public class BehDealDamageToTargetBuilding : BehaviourActionActor
	{
		// Token: 0x060044C8 RID: 17608 RVA: 0x001CF32F File Offset: 0x001CD52F
		public BehDealDamageToTargetBuilding(float pMinMultiplier, float pMaxMultiplier)
		{
			this._min = pMinMultiplier;
			this._max = pMaxMultiplier;
			this.null_check_building_target = true;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x001CF34C File Offset: 0x001CD54C
		public override BehResult execute(Actor pActor)
		{
			Building tTarget = pActor.beh_building_target;
			float tDamageMultiplier = Randy.randomFloat(this._min, this._max);
			if (tDamageMultiplier <= 0f)
			{
				return BehResult.Continue;
			}
			int tDamage = (int)Mathf.Max(pActor.stats["damage"] * tDamageMultiplier, 1f);
			tTarget.getHit((float)tDamage, true, AttackType.Other, null, true, false, true);
			pActor.spawnSlash(tTarget.current_position, null, 2f, 0f, 0f, null);
			return BehResult.Continue;
		}

		// Token: 0x04003171 RID: 12657
		private const float DAMAGE_MULTIPLIER = 0.1f;

		// Token: 0x04003172 RID: 12658
		private float _min;

		// Token: 0x04003173 RID: 12659
		private float _max;
	}
}
