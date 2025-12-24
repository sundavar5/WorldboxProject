using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class BehGenerateLootFromHouse : BehCityActor
{
	// Token: 0x06002234 RID: 8756 RVA: 0x001202C0 File Offset: 0x0011E4C0
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasHouse())
		{
			return BehResult.Stop;
		}
		Building homeBuilding = pActor.getHomeBuilding();
		int tCoinsFromHouse = homeBuilding.asset.loot_generation;
		int tCoinsFromBiome = 0;
		BiomeAsset tBiomeAsset = homeBuilding.current_tile.getBiome();
		if (tBiomeAsset != null)
		{
			tCoinsFromBiome = tBiomeAsset.loot_generation;
		}
		int tTotalCoins = tCoinsFromHouse + tCoinsFromBiome;
		tTotalCoins = Mathf.Max(1, tTotalCoins);
		pActor.addLoot(tTotalCoins);
		return BehResult.Continue;
	}
}
