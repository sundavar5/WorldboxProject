using System;

namespace ai.behaviours
{
	// Token: 0x020008A2 RID: 2210
	public class BehCityActorFindBuilding : BehCityActor
	{
		// Token: 0x06004492 RID: 17554 RVA: 0x001CE36F File Offset: 0x001CC56F
		public BehCityActorFindBuilding(string pType, bool pFreeTile = true)
		{
			this._type = pType;
			this._only_free_tile = pFreeTile;
			if (pType.Contains(","))
			{
				this._types = pType.Split(',', StringSplitOptions.None);
			}
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x001CE3A1 File Offset: 0x001CC5A1
		public override BehResult execute(Actor pActor)
		{
			if (this._types != null)
			{
				this._type = this._types.GetRandom<string>();
			}
			pActor.beh_building_target = ActorTool.findNewBuildingTarget(pActor, this._type, true);
			return BehResult.Continue;
		}

		// Token: 0x0400316C RID: 12652
		private string _type;

		// Token: 0x0400316D RID: 12653
		private string[] _types;

		// Token: 0x0400316E RID: 12654
		private bool _only_free_tile;
	}
}
