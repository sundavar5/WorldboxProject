using System;
using System.Collections.Generic;

// Token: 0x02000258 RID: 600
public class ClanManager : MetaSystemManager<Clan, ClanData>
{
	// Token: 0x0600167B RID: 5755 RVA: 0x000E31B5 File Offset: 0x000E13B5
	public ClanManager()
	{
		this.type_id = "clan";
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000E31C8 File Offset: 0x000E13C8
	public Clan newClan(Actor pFounder, bool pAddDefaultTraits)
	{
		World.world.game_stats.data.clansCreated += 1L;
		World.world.map_stats.clansCreated += 1L;
		Clan tNewClan = base.newObject();
		tNewClan.newClan(pFounder, pAddDefaultTraits);
		MetaHelper.addRandomTrait<ClanTrait>(tNewClan, AssetManager.clan_traits);
		pFounder.setClan(tNewClan);
		if (pFounder.isKing())
		{
			pFounder.kingdom.trySetRoyalClan();
		}
		this.convertFamilyToClan(pFounder, tNewClan);
		this.addRandomTraitFromBiomeToClan(tNewClan, pFounder.current_tile);
		return tNewClan;
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000E3254 File Offset: 0x000E1454
	private void convertFamilyToClan(Actor pFounder, Clan pNewClan)
	{
		if (!pFounder.hasFamily())
		{
			return;
		}
		foreach (Actor tFamilyMember in pFounder.getChildren(true))
		{
			if (!tFamilyMember.hasClan())
			{
				tFamilyMember.setClan(pNewClan);
			}
		}
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000E32B4 File Offset: 0x000E14B4
	public override void removeObject(Clan pClan)
	{
		foreach (Kingdom tKingdom in World.world.kingdoms.list)
		{
			if (tKingdom.data.royal_clan_id == pClan.getID() && pClan.getRenown() >= 10)
			{
				tKingdom.logRoyalClanLost(pClan);
			}
		}
		World.world.game_stats.data.clansDestroyed += 1L;
		World.world.map_stats.clansDestroyed += 1L;
		base.removeObject(pClan);
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000E336C File Offset: 0x000E156C
	public void addRandomTraitFromBiomeToClan(Clan pClan, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pClan.addRandomTraitFromBiome<ClanTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_clan : null, AssetManager.clan_traits);
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000E3394 File Offset: 0x000E1594
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		foreach (Clan tClan in this.list)
		{
			if (!tClan.hasChief())
			{
				tClan.checkMembersForNewChief();
			}
		}
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x000E33F8 File Offset: 0x000E15F8
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Clan tClan = tUnit.clan;
			if (tClan != null && tClan.isDirtyUnits())
			{
				tClan.listUnit(tUnit);
			}
		}
	}
}
