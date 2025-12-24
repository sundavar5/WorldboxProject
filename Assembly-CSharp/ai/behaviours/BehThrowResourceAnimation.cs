using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000907 RID: 2311
	public class BehThrowResourceAnimation : BehCityActor
	{
		// Token: 0x0600458A RID: 17802 RVA: 0x001D2905 File Offset: 0x001D0B05
		public BehThrowResourceAnimation(string pResourceId)
		{
			this._resource_id = pResourceId;
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x001D2914 File Offset: 0x001D0B14
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x001D292C File Offset: 0x001D0B2C
		public override BehResult execute(Actor pActor)
		{
			Building tBuilding = pActor.beh_building_target;
			float tBaseDuration = Toolbox.DistTile(pActor.current_tile, tBuilding.current_tile);
			tBaseDuration = Mathf.Max(tBaseDuration, 1f);
			if (tBaseDuration > 1.5f)
			{
				tBaseDuration = 1.5f;
			}
			if (pActor.is_visible)
			{
				float tDuration = tBaseDuration;
				Vector2 tStartPosition = pActor.getThrowStartPosition();
				Vector2 tEndPosition = tBuilding.current_position + tBuilding.asset.stockpile_center_offset;
				tEndPosition.x += Randy.randomFloat(-0.1f, 0.1f);
				tEndPosition.y += Randy.randomFloat(-0.1f, 0.1f);
				BehaviourActionBase<Actor>.world.resource_throw_manager.addNew(tStartPosition, tEndPosition, tDuration, this._resource_id, 1, 2f, tBuilding);
			}
			return BehResult.Continue;
		}

		// Token: 0x040031BA RID: 12730
		private string _resource_id;
	}
}
