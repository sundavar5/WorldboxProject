using System;
using ai.behaviours;

// Token: 0x02000398 RID: 920
public class BehFindRandomFarTile : BehaviourActionActor
{
	// Token: 0x060021BC RID: 8636 RVA: 0x0011D5D0 File Offset: 0x0011B7D0
	public override BehResult execute(Actor pActor)
	{
		MapRegion tRegion = pActor.current_tile.region;
		int i = 0;
		while (i < 5 && tRegion.neighbours.Count != 0)
		{
			tRegion = tRegion.neighbours.GetRandom<MapRegion>();
			i++;
		}
		if (tRegion.tiles.Count > 0)
		{
			pActor.beh_tile_target = tRegion.tiles.GetRandom<WorldTile>();
			return BehResult.Continue;
		}
		return BehResult.Stop;
	}
}
