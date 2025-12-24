using System;

// Token: 0x0200054B RID: 1355
public class AssetsDebugManager
{
	// Token: 0x06002C31 RID: 11313 RVA: 0x0015C1E0 File Offset: 0x0015A3E0
	public static void changeSex()
	{
		if (AssetsDebugManager.actors_sex == ActorSex.Female)
		{
			AssetsDebugManager.actors_sex = ActorSex.Male;
			return;
		}
		AssetsDebugManager.actors_sex = ActorSex.Female;
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x0015C1F8 File Offset: 0x0015A3F8
	public static void newKingdomColors()
	{
		foreach (KingdomAsset kingdomAsset in AssetManager.kingdoms.list)
		{
			kingdomAsset.debug_color_asset = AssetManager.kingdom_colors_library.list.GetRandom<ColorAsset>();
		}
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x0015C25C File Offset: 0x0015A45C
	public static void setRandomKingdomColor(string pKingdomAssetId)
	{
		KingdomAsset kingdomAsset = AssetManager.kingdoms.get(pKingdomAssetId);
		ColorAsset tColorAsset = AssetManager.kingdom_colors_library.list.GetRandom<ColorAsset>();
		kingdomAsset.debug_color_asset = tColorAsset;
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x0015C28C File Offset: 0x0015A48C
	public static void newSkinColors()
	{
		foreach (ActorAsset tAsset in AssetManager.actor_library.list)
		{
			if (tAsset.use_phenotypes)
			{
				AssetsDebugManager.setRandomSkinColor(tAsset);
			}
		}
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x0015C2EC File Offset: 0x0015A4EC
	public static void setRandomSkinColor(ActorAsset pAsset)
	{
		string tSkinColor = AssetsDebugManager.getRandomSkinColor(pAsset);
		pAsset.debug_phenotype_colors = tSkinColor;
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x0015C307 File Offset: 0x0015A507
	private static string getRandomSkinColor(ActorAsset pAsset)
	{
		if (pAsset.phenotypes_list == null || pAsset.phenotypes_list.Count == 0)
		{
			return null;
		}
		return pAsset.phenotypes_list.GetRandom<string>();
	}

	// Token: 0x040021E4 RID: 8676
	public static ActorSex actors_sex;
}
