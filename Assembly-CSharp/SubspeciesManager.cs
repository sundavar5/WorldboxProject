using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class SubspeciesManager : MetaSystemManager<Subspecies, SubspeciesData>
{
	// Token: 0x06001BCF RID: 7119 RVA: 0x000FDCE4 File Offset: 0x000FBEE4
	public SubspeciesManager()
	{
		this.type_id = "subspecies";
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000FDCF8 File Offset: 0x000FBEF8
	public Subspecies newSpecies(ActorAsset pAsset, WorldTile pTile, bool pMutation = false)
	{
		World.world.game_stats.data.subspeciesCreated += 1L;
		World.world.map_stats.subspeciesCreated += 1L;
		Subspecies tSubspecies = base.newObject();
		tSubspecies.newSpecies(pAsset, pTile, pMutation);
		this.addRandomTraitFromBiomeToSubspecies(tSubspecies, pTile);
		this.addTraitsFromBiomeToSubspecies(tSubspecies, pTile);
		return tSubspecies;
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000FDD5B File Offset: 0x000FBF5B
	protected override void finishDirtyUnits()
	{
		base.finishDirtyUnits();
		this.cacheCounters();
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000FDD6C File Offset: 0x000FBF6C
	private void cacheCounters()
	{
		foreach (Subspecies subspecies in this.list)
		{
			subspecies.cacheCounters();
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000FDDBC File Offset: 0x000FBFBC
	public void addRandomTraitFromBiomeToSubspecies(Subspecies pSubspecies, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pSubspecies.addRandomTraitFromBiome<SubspeciesTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_subspecies : null, AssetManager.subspecies_traits);
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000FDDE1 File Offset: 0x000FBFE1
	public void addTraitsFromBiomeToSubspecies(Subspecies pSubspecies, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pSubspecies.addTraitFromBiome<SubspeciesTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_subspecies_always : null, AssetManager.subspecies_traits);
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000FDE06 File Offset: 0x000FC006
	public override void removeObject(Subspecies pObject)
	{
		World.world.game_stats.data.subspeciesExtinct += 1L;
		World.world.map_stats.subspeciesExtinct += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000FDE44 File Offset: 0x000FC044
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (World.world.isPaused())
		{
			return;
		}
		this._timer_unstable_genome -= pElapsed;
		if (this._timer_unstable_genome <= 0f)
		{
			this._timer_unstable_genome = Randy.randomFloat(300f, 600f);
			this.checkSpecialTraits();
		}
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000FDE9C File Offset: 0x000FC09C
	private void checkSpecialTraits()
	{
		foreach (Subspecies tSubspecies in this.list)
		{
			if (tSubspecies.hasTrait("unstable_genome"))
			{
				tSubspecies.unstableGenomeEvent();
			}
		}
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000FDEFC File Offset: 0x000FC0FC
	protected override void updateDirtyUnits()
	{
		for (int i = 0; i < World.world.units.units_only_dying.Count; i++)
		{
			Actor tUnit = World.world.units.units_only_dying[i];
			if (tUnit.subspecies != null)
			{
				tUnit.subspecies.preserveAlive();
			}
		}
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int j = 0; j < tActorList.Count; j++)
		{
			Actor tUnit2 = tActorList[j];
			Subspecies tSubspecies = tUnit2.subspecies;
			if (tSubspecies != null && tSubspecies.isDirtyUnits())
			{
				tSubspecies.listUnit(tUnit2);
			}
		}
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000FDF9A File Offset: 0x000FC19A
	public override void clear()
	{
		base.clear();
		this._timer_unstable_genome = Randy.randomFloat(300f, 600f);
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x000FDFB8 File Offset: 0x000FC1B8
	public Subspecies getNearbySpecies(ActorAsset pAsset, WorldTile pTile, out Actor pSubspeciesActor, bool pLookForSapientSubspecies = false, bool pStopAtFirst = false)
	{
		Subspecies tBestResult = null;
		pSubspeciesActor = null;
		Subspecies result;
		using (ListPool<Actor> tList = new ListPool<Actor>(pAsset.units))
		{
			string.IsNullOrEmpty(pTile.Type.biome_id);
			float tBestDist = (float)pAsset.species_spawn_radius;
			for (int i = 0; i < tList.Count; i++)
			{
				Actor tUnit = tList[i];
				if (tUnit.isAlive() && tUnit.hasSubspecies() && tUnit.subspecies.isSpecies(pAsset.id) && (!pLookForSapientSubspecies || tUnit.subspecies.isSapient()))
				{
					float tTempDist = Toolbox.DistTile(pTile, tUnit.current_tile);
					if (tTempDist <= (float)pAsset.species_spawn_radius && tTempDist < tBestDist)
					{
						tBestDist = tTempDist;
						tBestResult = tUnit.subspecies;
						pSubspeciesActor = tUnit;
						if (pStopAtFirst && tBestDist < 10f)
						{
							break;
						}
					}
				}
			}
			result = tBestResult;
		}
		return result;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000FE0A0 File Offset: 0x000FC2A0
	public Sprite getTopicSprite()
	{
		Subspecies tRandomSpecies = this.list.GetRandom<Subspecies>();
		if (tRandomSpecies == null)
		{
			return null;
		}
		return tRandomSpecies.getActorAsset().getSpriteIcon();
	}

	// Token: 0x0400155E RID: 5470
	public static readonly string[] NAME_ENDINGS = new string[]
	{
		"us",
		"as",
		"os",
		"is",
		"es",
		"um",
		"ys",
		"bres",
		"bros"
	};

	// Token: 0x0400155F RID: 5471
	private const float UNSTABLE_GENOME_INTERVAL = 300f;

	// Token: 0x04001560 RID: 5472
	private float _timer_unstable_genome;
}
