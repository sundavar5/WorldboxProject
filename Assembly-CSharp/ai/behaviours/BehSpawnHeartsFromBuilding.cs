using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020008FE RID: 2302
	public class BehSpawnHeartsFromBuilding : BehaviourActionActor
	{
		// Token: 0x06004574 RID: 17780 RVA: 0x001D2420 File Offset: 0x001D0620
		public BehSpawnHeartsFromBuilding(float pAmount = 1f)
		{
			this._amount = pAmount;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001D242F File Offset: 0x001D062F
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x001D2445 File Offset: 0x001D0645
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasLover())
			{
				pActor.addAfterglowStatus();
				pActor.lover.addAfterglowStatus();
				this.spawnHearts(pActor);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x001D246C File Offset: 0x001D066C
		private void spawnHearts(Actor pActor)
		{
			Building tBuilding = pActor.beh_building_target;
			int i = 0;
			while ((float)i < this._amount)
			{
				float tPosX = (float)tBuilding.current_tile.x + Randy.randomFloat(-1f, 1f);
				float tPosY = (float)tBuilding.current_tile.y + Randy.randomFloat(0f, 1f) + 2f;
				Vector3 tPos = new Vector3(tPosX, tPosY);
				EffectsLibrary.spawnAt("fx_hearts", tPos, 0.15f);
				i++;
			}
		}

		// Token: 0x040031B4 RID: 12724
		private float _amount;
	}
}
