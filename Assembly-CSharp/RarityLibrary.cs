using System;

// Token: 0x02000166 RID: 358
public class RarityLibrary : AssetLibrary<RarityAsset>
{
	// Token: 0x06000AF8 RID: 2808 RVA: 0x000A0904 File Offset: 0x0009EB04
	public override void init()
	{
		base.init();
		RarityLibrary.normal = this.add(new RarityAsset
		{
			id = Rarity.R0_Normal.ToString().ToLower(),
			color_hex = "#FFFFFF",
			material_path = string.Empty,
			rarity_trait_string = "trait_common"
		});
		RarityLibrary.rare = this.add(new RarityAsset
		{
			id = Rarity.R1_Rare.ToString().ToLower(),
			color_hex = "#66AFFF",
			material_path = "materials/ItemRare",
			rarity_trait_string = "trait_rare"
		});
		RarityLibrary.epic = this.add(new RarityAsset
		{
			id = Rarity.R2_Epic.ToString().ToLower(),
			color_hex = "#FFF15E",
			material_path = "materials/ItemEpic",
			rarity_trait_string = "trait_epic"
		});
		RarityLibrary.legendary = this.add(new RarityAsset
		{
			id = Rarity.R3_Legendary.ToString().ToLower(),
			color_hex = "#FF7028",
			material_path = "materials/ItemLegendary",
			rarity_trait_string = "trait_legendary"
		});
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x000A0A44 File Offset: 0x0009EC44
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (RarityAsset tAsset in this.list)
		{
			tAsset.color_container = new ContainerItemColor(tAsset.color_hex, tAsset.material_path);
		}
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x000A0AB0 File Offset: 0x0009ECB0
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (RarityAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}

	// Token: 0x04000A70 RID: 2672
	public static RarityAsset normal;

	// Token: 0x04000A71 RID: 2673
	public static RarityAsset rare;

	// Token: 0x04000A72 RID: 2674
	public static RarityAsset epic;

	// Token: 0x04000A73 RID: 2675
	public static RarityAsset legendary;
}
