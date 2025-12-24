using System;
using ai.behaviours;

// Token: 0x020003CA RID: 970
public class BehCheckCuriosityTile : BehaviourActionActor
{
	// Token: 0x0600225D RID: 8797 RVA: 0x00120D34 File Offset: 0x0011EF34
	public override BehResult execute(Actor pActor)
	{
		if (pActor.scheduled_tile_target == null)
		{
			return BehResult.Stop;
		}
		WorldTile tTileToInvestigate = pActor.scheduled_tile_target;
		pActor.scheduled_tile_target = null;
		float tChanceToInvestigate = 0.6f;
		if (pActor.hasSubspecies() && pActor.subspecies.has_trait_curious)
		{
			tChanceToInvestigate += 0.3f;
		}
		if (!Randy.randomChance(tChanceToInvestigate))
		{
			return BehResult.Stop;
		}
		WorldTile tTile = tTileToInvestigate.getWalkableTileAround(pActor.current_tile);
		if (tTile == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_tile_target = tTile;
		return BehResult.Continue;
	}
}
