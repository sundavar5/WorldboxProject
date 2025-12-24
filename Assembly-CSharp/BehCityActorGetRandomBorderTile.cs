using System;
using ai.behaviours;

// Token: 0x02000395 RID: 917
public class BehCityActorGetRandomBorderTile : BehCityActor
{
	// Token: 0x060021B3 RID: 8627 RVA: 0x0011D2AC File Offset: 0x0011B4AC
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.city.hasZones())
		{
			return BehResult.Stop;
		}
		if (pActor.city.border_zones.Count == 0)
		{
			return BehResult.Stop;
		}
		WorldTile tRandomTile = pActor.city.border_zones.GetRandom<TileZone>().tiles.GetRandom<WorldTile>();
		if (!tRandomTile.Type.ground)
		{
			return BehResult.Stop;
		}
		pActor.beh_tile_target = tRandomTile;
		return BehResult.Continue;
	}
}
