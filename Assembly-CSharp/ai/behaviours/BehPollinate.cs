using System;

namespace ai.behaviours
{
	// Token: 0x02000923 RID: 2339
	public class BehPollinate : BehaviourActionActor
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x001D3ED9 File Offset: 0x001D20D9
		public BehPollinate()
		{
			this.land_if_hovering = true;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x001D3EE8 File Offset: 0x001D20E8
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.current_tile.hasBuilding())
			{
				return BehResult.Stop;
			}
			if (pActor.current_tile.building.asset.type == "type_flower")
			{
				ActorData data = pActor.data;
				int pollen = data.pollen;
				data.pollen = pollen + 1;
				pActor.current_tile.pollinate();
				if (pActor.asset.id != "bee" && pActor.data.pollen >= 10)
				{
					pActor.data.pollen -= 10;
					if (pActor.isKingdomCiv())
					{
						pActor.addToInventory("honey", 1);
					}
				}
			}
			pActor.timer_action = Randy.randomFloat(4f, 10f);
			return BehResult.Continue;
		}
	}
}
