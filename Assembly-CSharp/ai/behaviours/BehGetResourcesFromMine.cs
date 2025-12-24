using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008DB RID: 2267
	public class BehGetResourcesFromMine : BehActorUsableBuildingTarget
	{
		// Token: 0x0600451E RID: 17694 RVA: 0x001D0CEE File Offset: 0x001CEEEE
		public override void create()
		{
			base.create();
			if (BehGetResourcesFromMine.pool_mineral_assets_default.Count == 0)
			{
				BehGetResourcesFromMine.initPool();
			}
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x001D0D08 File Offset: 0x001CEF08
		private static void initPool()
		{
			BehGetResourcesFromMine.addToPool("mineral_gems", 1, BehGetResourcesFromMine.pool_mineral_assets_default);
			BehGetResourcesFromMine.addToPool("mineral_gems", 4, BehGetResourcesFromMine.pool_mineral_assets_default);
			BehGetResourcesFromMine.addToPool("mineral_stone", 20, BehGetResourcesFromMine.pool_mineral_assets_default);
			BehGetResourcesFromMine.addToPool("mineral_metals", 10, BehGetResourcesFromMine.pool_mineral_assets_default);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001D0D58 File Offset: 0x001CEF58
		private static void addToPool(string pID, int pAmount, List<string> pPool)
		{
			for (int i = 0; i < pAmount; i++)
			{
				pPool.Add(pID);
			}
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x001D0D78 File Offset: 0x001CEF78
		public override BehResult execute(Actor pActor)
		{
			if (Randy.randomChance(0.4f))
			{
				return BehResult.Continue;
			}
			BuildingAsset tAsset = BehGetResourcesFromMine.getRandomAssetFromPool(pActor);
			if (tAsset == null)
			{
				return BehResult.Continue;
			}
			BuildingHelper.tryToBuildNear(pActor.beh_building_target.current_tile, tAsset);
			pActor.addLoot(SimGlobals.m.coins_for_mine);
			return BehResult.Continue;
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x001D0DC4 File Offset: 0x001CEFC4
		private static BuildingAsset getRandomAssetFromPool(Actor pActor)
		{
			Building tBuilding = pActor.beh_building_target;
			WorldTile tResultTile = tBuilding.current_tile;
			if (!tResultTile.Type.is_biome)
			{
				bool tFound = false;
				foreach (WorldTile tBuildingTile in tBuilding.tiles)
				{
					if (tBuildingTile.Type.is_biome)
					{
						tFound = true;
						tResultTile = tBuildingTile;
						break;
					}
				}
				if (!tFound)
				{
					for (int i = 0; i < tResultTile.neighboursAll.Length; i++)
					{
						WorldTile tNeighbourTile = tResultTile.neighboursAll[i];
						if (tNeighbourTile.Type.is_biome)
						{
							tFound = true;
							tResultTile = tNeighbourTile;
							break;
						}
					}
				}
				if (!tFound)
				{
					return AssetManager.buildings.get("mineral_stone");
				}
			}
			BiomeAsset biome_asset = tResultTile.Type.biome_asset;
			List<string> tPool = (biome_asset != null) ? biome_asset.pot_minerals_spawn : null;
			if (tPool == null)
			{
				tPool = BehGetResourcesFromMine.pool_mineral_assets_default;
			}
			string tRandomID = tPool.GetRandom<string>();
			return AssetManager.buildings.get(tRandomID);
		}

		// Token: 0x04003186 RID: 12678
		private const string ASSET_FOR_NO_BIOME = "mineral_stone";

		// Token: 0x04003187 RID: 12679
		private static List<string> pool_mineral_assets_default = new List<string>();
	}
}
