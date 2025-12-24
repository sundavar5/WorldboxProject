using System;

namespace ai.behaviours
{
	// Token: 0x020008BD RID: 2237
	public class BehFamilyGroupNew : BehaviourActionActor
	{
		// Token: 0x060044DE RID: 17630 RVA: 0x001CF828 File Offset: 0x001CDA28
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasFamily())
			{
				return BehResult.Stop;
			}
			Actor tTarget = this.getNearbySameSpecies(pActor, pActor.asset, pActor.current_tile);
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			BehaviourActionBase<Actor>.world.families.newFamily(pActor, pActor.current_tile, tTarget);
			return BehResult.Continue;
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x001CF874 File Offset: 0x001CDA74
		public Actor getNearbySameSpecies(Actor pActor, ActorAsset pUnitAsset, WorldTile pTile)
		{
			foreach (Actor tUnit in Finder.getUnitsFromChunk(pTile, 4, (float)pUnitAsset.family_spawn_radius, true))
			{
				if (tUnit != pActor && tUnit.current_tile.isSameIsland(pTile) && !tUnit.hasFamily() && tUnit.isSameSpecies(pUnitAsset.id) && tUnit.isSameSubspecies(pActor.subspecies))
				{
					return tUnit;
				}
			}
			return null;
		}
	}
}
