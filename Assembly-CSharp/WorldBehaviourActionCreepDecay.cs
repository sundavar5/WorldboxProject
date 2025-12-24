using System;
using System.Collections.Generic;

// Token: 0x02000304 RID: 772
public static class WorldBehaviourActionCreepDecay
{
	// Token: 0x06001D35 RID: 7477 RVA: 0x001056E0 File Offset: 0x001038E0
	public static void checkCreep()
	{
		if (WorldLawLibrary.world_law_forever_tumor_creep.isEnabled())
		{
			return;
		}
		WorldBehaviourActionCreepDecay.checkBiome("tumor", "biome_tumor");
		WorldBehaviourActionCreepDecay.checkBiome("biomass", "biome_biomass");
		WorldBehaviourActionCreepDecay.checkBiome("super_pumpkin", "biome_pumpkin");
		WorldBehaviourActionCreepDecay.checkBiome("cybercore", "biome_cybertile");
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x00105738 File Offset: 0x00103938
	private static void checkBiome(string pCreepHubID, string pBiomeID)
	{
		BuildingAsset tBuildingAsset = AssetManager.buildings.get(pCreepHubID);
		Kingdom tKingdom = World.world.kingdoms_wild.get(tBuildingAsset.kingdom);
		BiomeAsset biomeAsset = AssetManager.biome_library.get(pBiomeID);
		WorldBehaviourActionCreepDecay.clear();
		WorldBehaviourActionCreepDecay.addToNotChecked(biomeAsset.getTileLow());
		WorldBehaviourActionCreepDecay.addToNotChecked(biomeAsset.getTileHigh());
		if (WorldBehaviourActionCreepDecay.not_checked_tiles.Count == 0)
		{
			return;
		}
		if (tKingdom.buildings.Count > 0)
		{
			List<Building> list = tKingdom.buildings;
			for (int i = 0; i < list.Count; i++)
			{
				Building tBuilding = list[i];
				if (tBuilding.isUsable())
				{
					WorldBehaviourActionCreepDecay.checkTile(tBuilding.current_tile);
					WorldBehaviourActionCreepDecay.next_wave.Add(tBuilding.current_tile);
				}
			}
		}
		WorldBehaviourActionCreepDecay.startWave(pBiomeID);
		if (WorldBehaviourActionCreepDecay.not_checked_tiles.Count > 0)
		{
			WorldBehaviourActionCreepDecay.destroyNonCheckedCreep();
		}
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x00105808 File Offset: 0x00103A08
	private static void startWave(string pBiomeID)
	{
		if (WorldBehaviourActionCreepDecay.next_wave.Count == 0)
		{
			return;
		}
		WorldBehaviourActionCreepDecay.cur_wave.AddRange(WorldBehaviourActionCreepDecay.next_wave);
		WorldBehaviourActionCreepDecay.next_wave.Clear();
		while (WorldBehaviourActionCreepDecay.cur_wave.Count > 0)
		{
			WorldTile tTile = WorldBehaviourActionCreepDecay.cur_wave[WorldBehaviourActionCreepDecay.cur_wave.Count - 1];
			WorldBehaviourActionCreepDecay.cur_wave.RemoveAt(WorldBehaviourActionCreepDecay.cur_wave.Count - 1);
			for (int i = 0; i < tTile.neighboursAll.Length; i++)
			{
				WorldTile tNeighbour = tTile.neighboursAll[i];
				if (!(tNeighbour.Type.biome_id != pBiomeID) && !WorldBehaviourActionCreepDecay.checked_tiles.Contains(tNeighbour))
				{
					WorldBehaviourActionCreepDecay.checkTile(tNeighbour);
					WorldBehaviourActionCreepDecay.next_wave.Add(tNeighbour);
				}
			}
		}
		if (WorldBehaviourActionCreepDecay.next_wave.Count > 0)
		{
			WorldBehaviourActionCreepDecay.startWave(pBiomeID);
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x001058DC File Offset: 0x00103ADC
	private static void destroyNonCheckedCreep()
	{
		foreach (WorldTile tTile in WorldBehaviourActionCreepDecay.not_checked_tiles)
		{
			WorldBehaviourActionCreepDecay._list_of_disconnected_tiles.Add(tTile);
		}
		foreach (WorldTile pTile in WorldBehaviourActionCreepDecay._list_of_disconnected_tiles.LoopRandom(3))
		{
			MapAction.decreaseTile(pTile, false, "flash");
		}
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x00105978 File Offset: 0x00103B78
	private static void checkTile(WorldTile pTile)
	{
		WorldBehaviourActionCreepDecay.checked_tiles.Add(pTile);
		WorldBehaviourActionCreepDecay.not_checked_tiles.Remove(pTile);
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x00105992 File Offset: 0x00103B92
	private static void addToNotChecked(TopTileType pTileType)
	{
		if (pTileType.hashset.Count == 0)
		{
			return;
		}
		WorldBehaviourActionCreepDecay.not_checked_tiles.UnionWith(pTileType.hashset);
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x001059B2 File Offset: 0x00103BB2
	public static void clear()
	{
		WorldBehaviourActionCreepDecay.checked_tiles.Clear();
		WorldBehaviourActionCreepDecay.not_checked_tiles.Clear();
		WorldBehaviourActionCreepDecay.next_wave.Clear();
		WorldBehaviourActionCreepDecay.cur_wave.Clear();
		WorldBehaviourActionCreepDecay._list_of_disconnected_tiles.Clear();
	}

	// Token: 0x04001600 RID: 5632
	private const int MAX_CREEP_TO_DESTROY_IN_ONE_STEP = 3;

	// Token: 0x04001601 RID: 5633
	private static List<WorldTile> next_wave = new List<WorldTile>();

	// Token: 0x04001602 RID: 5634
	private static List<WorldTile> cur_wave = new List<WorldTile>();

	// Token: 0x04001603 RID: 5635
	private static HashSetWorldTile checked_tiles = new HashSetWorldTile();

	// Token: 0x04001604 RID: 5636
	private static HashSetWorldTile not_checked_tiles = new HashSetWorldTile();

	// Token: 0x04001605 RID: 5637
	private static List<WorldTile> _list_of_disconnected_tiles = new List<WorldTile>();
}
