using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ai;
using strings;
using UnityEngine;

// Token: 0x02000307 RID: 775
public static class WorldBehaviourActionFire
{
	// Token: 0x06001D40 RID: 7488 RVA: 0x00105B74 File Offset: 0x00103D74
	public static void prepare()
	{
		if (WorldBehaviourActionFire._fires == null || WorldBehaviourActionFire._fires.Length != World.world.zone_calculator.zones.Count)
		{
			WorldBehaviourActionFire._fires = new int[World.world.zone_calculator.zones.Count];
			return;
		}
		WorldBehaviourActionFire.clearFires();
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00105BCC File Offset: 0x00103DCC
	public static void clearFires()
	{
		if (WorldBehaviourActionFire._fires == null)
		{
			return;
		}
		for (int i = 0; i < WorldBehaviourActionFire._fires.Length; i++)
		{
			WorldBehaviourActionFire._fires[i] = 0;
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x00105BFB File Offset: 0x00103DFB
	public static void addFire(WorldTile pTile)
	{
		if (!WorldBehaviourActionFire._tiles.Add(pTile))
		{
			return;
		}
		World.world.fire_layer.setTileDirty(pTile);
		WorldBehaviourActionFire._fires[pTile.zone.id]++;
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00105C38 File Offset: 0x00103E38
	public static void removeFire(WorldTile pTile)
	{
		if (!WorldBehaviourActionFire._tiles.Remove(pTile))
		{
			return;
		}
		World.world.fire_layer.setTileDirty(pTile);
		if (WorldBehaviourActionFire._fires[pTile.zone.id] == 0)
		{
			Debug.Log("FIRE ERROR");
			return;
		}
		WorldBehaviourActionFire._fires[pTile.zone.id]--;
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x00105C9C File Offset: 0x00103E9C
	public static void updateFire()
	{
		if (WorldBehaviourActionFire._tiles.Count == 0)
		{
			return;
		}
		WorldBehaviourActionFire._list_updater.Clear();
		WorldBehaviourActionFire._list_updater.AddRange(WorldBehaviourActionFire._tiles);
		foreach (WorldTile tTile in WorldBehaviourActionFire._list_updater)
		{
			float tFireDuration = World.world.getWorldTimeElapsedSince(tTile.data.fire_timestamp);
			bool tSpreadFires = true;
			if (World.world.era_manager.getCurrentAge().particles_rain)
			{
				tSpreadFires = false;
			}
			if (WorldLawLibrary.world_law_gaias_covenant.isEnabled())
			{
				tSpreadFires = false;
			}
			if (tFireDuration >= SimGlobals.m.fire_spread_time && tSpreadFires)
			{
				for (int i = 0; i < tTile.neighbours.Length; i++)
				{
					WorldTile tNeighbour = tTile.neighbours[i];
					if (Randy.randomChance(tNeighbour.Type.fire_chance * World.world_era.fire_spread_rate_bonus) && tNeighbour.startFire(false))
					{
						World.world.flash_effects.flashPixel(tNeighbour, 10, ColorType.White);
					}
				}
			}
			if (Randy.randomChance(0.1f))
			{
				World.world.particles_fire.spawn(tTile.posV3);
			}
			bool tStop = false;
			float tRandomStopChance = 0f;
			if (tFireDuration > SimGlobals.m.fire_stop_time)
			{
				tRandomStopChance = 0.1f + tFireDuration / SimGlobals.m.fire_time * 0.3f;
			}
			if (tTile.Type.ocean)
			{
				tStop = true;
			}
			else if (tFireDuration >= SimGlobals.m.fire_time)
			{
				tStop = true;
			}
			else if (Randy.randomChance(tRandomStopChance))
			{
				tStop = true;
			}
			if (tStop)
			{
				tTile.stopFire();
				WorldBehaviourActionFire.checkFireElementalSpawn(tTile);
			}
		}
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x00105E64 File Offset: 0x00104064
	private static void checkFireElementalSpawn(WorldTile pTile)
	{
		if (!WorldLawLibrary.world_law_disasters_other.isEnabled())
		{
			return;
		}
		if (!World.world_era.era_disaster_fire_elemental_spawn_on_fire)
		{
			return;
		}
		if (!Randy.randomChance(World.world_era.fire_elemental_spawn_chance))
		{
			return;
		}
		if (ActorTool.countUnitsFrom("fire_elemental") > 100)
		{
			return;
		}
		string tElementalID = SA.fire_elementals.GetRandom<string>();
		World.world.units.spawnNewUnit(tElementalID, pTile, false, false, 6f, null, false, true);
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x00105ED3 File Offset: 0x001040D3
	public static void clear()
	{
		WorldBehaviourActionFire._tiles.Clear();
		WorldBehaviourActionFire._list_updater.Clear();
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00105EE9 File Offset: 0x001040E9
	public static bool hasFires()
	{
		return WorldBehaviourActionFire._tiles.Count > 0;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00105EF8 File Offset: 0x001040F8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int countFires(TileZone pZone)
	{
		return WorldBehaviourActionFire._fires[pZone.id];
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x00105F06 File Offset: 0x00104106
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool hasFires(TileZone pZone)
	{
		return WorldBehaviourActionFire._fires[pZone.id] > 0;
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x00105F17 File Offset: 0x00104117
	public static int[] getFires()
	{
		return WorldBehaviourActionFire._fires;
	}

	// Token: 0x04001608 RID: 5640
	private static readonly HashSetWorldTile _tiles = new HashSetWorldTile();

	// Token: 0x04001609 RID: 5641
	private static int[] _fires;

	// Token: 0x0400160A RID: 5642
	private static readonly List<WorldTile> _list_updater = new List<WorldTile>();
}
