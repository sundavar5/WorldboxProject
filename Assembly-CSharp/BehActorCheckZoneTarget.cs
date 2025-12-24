using System;
using ai.behaviours;

// Token: 0x02000389 RID: 905
public class BehActorCheckZoneTarget : BehCityActor
{
	// Token: 0x0600219A RID: 8602 RVA: 0x0011CEEC File Offset: 0x0011B0EC
	public override BehResult execute(Actor pActor)
	{
		City tCity = pActor.city;
		TileZone tZoneToClaim = BehaviourActionBase<Actor>.world.city_zone_helper.city_growth.getZoneToClaim(pActor, pActor.city, false, null, 0);
		if (tZoneToClaim == null)
		{
			return BehResult.Stop;
		}
		if (tZoneToClaim.city == tCity)
		{
			return BehResult.Stop;
		}
		WorldTile tTargetTile = null;
		if (tZoneToClaim.centerTile.isSameIsland(pActor.current_tile))
		{
			tTargetTile = tZoneToClaim.centerTile;
		}
		else
		{
			foreach (WorldTile tTile in tZoneToClaim.tiles.LoopRandom<WorldTile>())
			{
				if (tTile.isSameIsland(pActor.current_tile))
				{
					tTargetTile = tTile;
					break;
				}
			}
		}
		if (tTargetTile == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_tile_target = tTargetTile;
		return BehResult.Continue;
	}
}
