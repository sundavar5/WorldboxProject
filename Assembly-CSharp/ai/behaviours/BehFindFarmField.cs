using System;

namespace ai.behaviours
{
	// Token: 0x020008C3 RID: 2243
	public class BehFindFarmField : BehCityActor
	{
		// Token: 0x060044EC RID: 17644 RVA: 0x001CFC30 File Offset: 0x001CDE30
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.calculated_grown_wheat.Count > 0)
			{
				return BehResult.Stop;
			}
			int tBestDist = int.MaxValue;
			WorldTile tBestTile = null;
			WorldTile tActorTile = pActor.current_tile;
			WorldTileContainer calculated_farm_fields = pActor.city.calculated_farm_fields;
			calculated_farm_fields.checkAddRemove();
			foreach (WorldTile tTile in calculated_farm_fields)
			{
				int tDist = Toolbox.SquaredDistTile(tActorTile, tTile);
				if (tDist < tBestDist && tTile.Type.farm_field && !tTile.isTargeted() && tActorTile.isSameIsland(tTile) && (!tTile.hasBuilding() || (tTile.building.canRemoveForFarms() && !tTile.building.asset.wheat)))
				{
					tBestDist = tDist;
					tBestTile = tTile;
				}
			}
			if (tBestTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tBestTile;
			return BehResult.Continue;
		}
	}
}
