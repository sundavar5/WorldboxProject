using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000908 RID: 2312
	public class BehThrowResources : BehCityActor
	{
		// Token: 0x0600458D RID: 17805 RVA: 0x001D29EF File Offset: 0x001D0BEF
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x001D2A08 File Offset: 0x001D0C08
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.isCarryingResources())
			{
				return BehResult.Continue;
			}
			Building tBuildingStockpile = pActor.beh_building_target;
			float tBaseDuration = Toolbox.DistTile(pActor.current_tile, tBuildingStockpile.current_tile);
			tBaseDuration = Mathf.Max(tBaseDuration, 1f);
			if (tBaseDuration > 1.5f)
			{
				tBaseDuration = 1.5f;
			}
			using (Dictionary<string, ResourceContainer>.Enumerator enumerator = pActor.inventory.getResources().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, ResourceContainer> tContainer = enumerator.Current;
					string tResourceID = tContainer.Key;
					ResourceAsset tResAsset = AssetManager.resources.get(tResourceID);
					if (pActor.is_visible)
					{
						int tAmount = tContainer.Value.amount;
						float tDuration = tBaseDuration;
						Vector2 tStartPosition = pActor.getThrowStartPosition();
						Vector2 tEndPosition = tBuildingStockpile.current_position + tBuildingStockpile.asset.stockpile_center_offset;
						tEndPosition.x += Randy.randomFloat(-0.1f, 0.1f);
						tEndPosition.y += Randy.randomFloat(-0.1f, 0.1f);
						BehaviourActionBase<Actor>.world.resource_throw_manager.addNew(tStartPosition, tEndPosition, tDuration, tResourceID, tAmount, 4f, tBuildingStockpile);
					}
					pActor.takeFromInventory(tResourceID, 1);
					tBuildingStockpile.addResources(tResourceID, 1);
					pActor.addLoot(tResAsset.loot_value);
					pActor.makeWait(0.2f);
					if (pActor.isCarryingResources())
					{
						return BehResult.StepBack;
					}
				}
			}
			return BehResult.Continue;
		}
	}
}
