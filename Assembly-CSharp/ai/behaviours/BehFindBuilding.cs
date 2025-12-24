using System;

namespace ai.behaviours
{
	// Token: 0x020008BF RID: 2239
	public class BehFindBuilding : BehaviourActionActor
	{
		// Token: 0x060044E3 RID: 17635 RVA: 0x001CFA57 File Offset: 0x001CDC57
		public BehFindBuilding(string pType, bool pOnlyNonTargeted, bool pOnlyWithResources)
		{
			this._type = pType;
			this._only_non_targeted = pOnlyNonTargeted;
			this._only_with_resources = pOnlyWithResources;
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x001CFA74 File Offset: 0x001CDC74
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_building_target = this.findBuildingType(pActor, this._type);
			if (pActor.beh_building_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x001CFA94 File Offset: 0x001CDC94
		private Building findBuildingType(Actor pActor, string pType)
		{
			Building tResult = null;
			ValueTuple<MapChunk[], int> allChunksFromTile = Toolbox.getAllChunksFromTile(pActor.current_tile);
			MapChunk[] tChunks = allChunksFromTile.Item1;
			int tLength = allChunksFromTile.Item2;
			foreach (MapChunk pChunk in tChunks.LoopRandom(tLength))
			{
				foreach (Building tBuilding in Toolbox.getBuildingsTypeFromChunk(pChunk, pType, this._only_non_targeted, this._only_with_resources))
				{
					if (tBuilding.current_tile.isSameIsland(pActor.current_tile))
					{
						return tBuilding;
					}
					tResult = tBuilding;
				}
			}
			return tResult;
		}

		// Token: 0x04003174 RID: 12660
		private readonly bool _only_non_targeted;

		// Token: 0x04003175 RID: 12661
		private readonly bool _only_with_resources;

		// Token: 0x04003176 RID: 12662
		private readonly string _type;
	}
}
