using System;
using ai.behaviours;

// Token: 0x020003E2 RID: 994
public class CityBehCheckFarms : BehaviourActionCity
{
	// Token: 0x060022B2 RID: 8882 RVA: 0x00122C44 File Offset: 0x00120E44
	public override bool shouldRetry(City pCity)
	{
		return false;
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x00122C47 File Offset: 0x00120E47
	public override BehResult execute(City pCity)
	{
		CityBehCheckFarms.check(pCity);
		return BehResult.Continue;
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x00122C50 File Offset: 0x00120E50
	public static void check(City pCity)
	{
		pCity.calculated_place_for_farms.Clear();
		pCity.calculated_grown_wheat.Clear();
		pCity.calculated_farm_fields.Clear();
		pCity.calculated_crops.Clear();
		CityBehCheckFarms.behFindTileForFarm(pCity);
		pCity.calculated_place_for_farms.checkAddRemove();
		pCity.calculated_farm_fields.checkAddRemove();
		pCity.calculated_crops.checkAddRemove();
		CityBehCheckFarms.behCheckWheat(pCity);
		pCity.calculated_grown_wheat.checkAddRemove();
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x00122CC4 File Offset: 0x00120EC4
	private static void behCheckWheat(City pCity)
	{
		foreach (WorldTile tTile in pCity.calculated_crops)
		{
			if (tTile.hasBuilding() && tTile.building.asset.wheat && tTile.building.component_wheat.isMaxLevel())
			{
				pCity.calculated_grown_wheat.Add(tTile);
			}
		}
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x00122D44 File Offset: 0x00120F44
	private static void behFindTileForFarm(City pCity)
	{
		Building tBuilding = pCity.getBuildingOfType("type_windmill", true, false, false, null);
		if (tBuilding == null)
		{
			return;
		}
		CityBehCheckFarms.checkRegion(tBuilding.current_tile.region, tBuilding, pCity);
		for (int i = 0; i < tBuilding.current_tile.region.neighbours.Count; i++)
		{
			CityBehCheckFarms.checkRegion(tBuilding.current_tile.region.neighbours[i], tBuilding, pCity);
		}
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x00122DB4 File Offset: 0x00120FB4
	private static void checkRegion(MapRegion pRegion, Building pBuilding, City pCity)
	{
		MapChunk tChunk = pRegion.chunk;
		for (int i = 0; i < tChunk.zones.Count; i++)
		{
			CityBehCheckFarms.checkZone(tChunk.zones[i], pBuilding, pCity);
		}
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x00122DF4 File Offset: 0x00120FF4
	private static void checkZone(TileZone pZone, Building pBuilding, City pCity)
	{
		if (!pZone.isSameCityHere(pCity))
		{
			return;
		}
		WorldTile[] tTiles = pZone.tiles;
		int tCount = tTiles.Length;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile tTile = tTiles[i];
			if ((float)Toolbox.SquaredDistTile(pBuilding.current_tile, tTile) <= 81f)
			{
				if (tTile.Type.can_be_farm)
				{
					pCity.calculated_place_for_farms.Add(tTile);
				}
				if (tTile.Type.farm_field)
				{
					pCity.calculated_farm_fields.Add(tTile);
					if (tTile.hasBuilding() && tTile.building.asset.wheat)
					{
						pCity.calculated_crops.Add(tTile);
					}
				}
			}
		}
	}
}
