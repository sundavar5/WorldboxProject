using System;

namespace ai.behaviours
{
	// Token: 0x020008B8 RID: 2232
	public class BehExtractResourcesFromBuilding : BehaviourActionActor
	{
		// Token: 0x060044CF RID: 17615 RVA: 0x001CF420 File Offset: 0x001CD620
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001CF438 File Offset: 0x001CD638
		public override BehResult execute(Actor pActor)
		{
			BuildingAsset tAsset = pActor.beh_building_target.asset;
			pActor.beh_building_target.extractResources(pActor);
			if (tAsset.resources_given != null)
			{
				foreach (ResourceContainer tContainer in tAsset.resources_given)
				{
					int tAmount = tContainer.amount;
					if (tAsset.building_type == BuildingType.Building_Mineral && pActor.hasTrait("miner") && Randy.randomBool())
					{
						tAmount++;
					}
					pActor.addToInventory(tContainer.id, tAmount);
				}
			}
			return BehResult.Continue;
		}
	}
}
