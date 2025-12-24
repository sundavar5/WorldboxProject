using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class PhenotypeLibrary : AssetLibrary<PhenotypeAsset>
{
	// Token: 0x060003D0 RID: 976 RVA: 0x00022854 File Offset: 0x00020A54
	public override void init()
	{
		base.init();
		PhenotypeLibrary.default_asset = this.add(new PhenotypeAsset
		{
			id = "skin_pale",
			shades_from = "#FFF9F4",
			shades_to = "#ECD9D5"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_light",
			shades_from = "#F5D6BA",
			shades_to = "#F3C6BE"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_medium",
			shades_from = "#C5966D",
			shades_to = "#C06859"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_dark",
			shades_from = "#7C5A3C",
			shades_to = "#593731"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_black",
			shades_from = "#46372A",
			shades_to = "#34211E"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_mixed",
			shades_from = "#E9BA90",
			shades_to = "#34211E"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_red",
			shades_from = "#DB4D48",
			shades_to = "#7F2E39"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_yellow",
			shades_from = "#FFE18E",
			shades_to = "#AB8B51"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_green",
			shades_from = "#9DB361",
			shades_to = "#425338"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_blue",
			shades_from = "#7FC1C7",
			shades_to = "#37617A"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_purple",
			shades_from = "#C29FD5",
			shades_to = "#5C54A2"
		});
		this.add(new PhenotypeAsset
		{
			id = "skin_pink",
			shades_from = "#FFAFC0",
			shades_to = "#D1539F"
		});
		this.add(new PhenotypeAsset
		{
			id = "white_gray",
			shades_from = "#FAFAFA",
			shades_to = "#B0B0B0"
		});
		this.add(new PhenotypeAsset
		{
			id = "mid_gray",
			shades_from = "#BBBBBB",
			shades_to = "#606060"
		});
		this.add(new PhenotypeAsset
		{
			id = "gray_black",
			shades_from = "#7A7A7A",
			shades_to = "#3E3E3E"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_red",
			shades_from = "#FF9A9A",
			shades_to = "#FF2222"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_orange",
			shades_from = "#FFC191",
			shades_to = "#FF7316"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_yellow",
			shades_from = "#FFEBA3",
			shades_to = "#FFCE1F"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_green",
			shades_from = "#C5FFA0",
			shades_to = "#1CDA2C"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_teal",
			shades_from = "#A3FFE0",
			shades_to = "#0DDCA4"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_blue",
			shades_from = "#A8E3FF",
			shades_to = "#1CA4FF"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_violet",
			shades_from = "#D6B7FF",
			shades_to = "#7142F3"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_purple",
			shades_from = "#F3B9FF",
			shades_to = "#BE4FDD"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_pink",
			shades_from = "#FFB6E3",
			shades_to = "#FF5FC2"
		});
		this.add(new PhenotypeAsset
		{
			id = "bright_salmon",
			shades_from = "#FFAFBC",
			shades_to = "#FF537E"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_red",
			shades_from = "#EC2D2D",
			shades_to = "#5F1414"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_orange",
			shades_from = "#FF7B16",
			shades_to = "#69340B"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_yellow",
			shades_from = "#FFC61A",
			shades_to = "#986411"
		});
		PhenotypeLibrary.default_green = this.add(new PhenotypeAsset
		{
			id = "dark_green",
			shades_from = "#72C727",
			shades_to = "#2A6016"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_teal",
			shades_from = "#1FDEA5",
			shades_to = "#0F6557"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_blue",
			shades_from = "#2182EB",
			shades_to = "#163487"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_violet",
			shades_from = "#6F47DF",
			shades_to = "#321A75"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_purple",
			shades_from = "#A341E1",
			shades_to = "#491C5F"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_pink",
			shades_from = "#F84EE7",
			shades_to = "#652265"
		});
		this.add(new PhenotypeAsset
		{
			id = "dark_salmon",
			shades_from = "#F35688",
			shades_to = "#6F2949"
		});
		this.add(new PhenotypeAsset
		{
			id = "toxic_green",
			shades_from = "#CBFF2E",
			shades_to = "#10F023"
		});
		this.add(new PhenotypeAsset
		{
			id = "aqua",
			shades_from = "#77D8D0",
			shades_to = "#3779A5"
		});
		this.add(new PhenotypeAsset
		{
			id = "swamp",
			shades_from = "#784D85",
			shades_to = "#367F58"
		});
		this.add(new PhenotypeAsset
		{
			id = "jungle",
			shades_from = "#03803D",
			shades_to = "#2EB829"
		});
		this.add(new PhenotypeAsset
		{
			id = "polar",
			shades_from = "#E8EEF5",
			shades_to = "#95C2E7"
		});
		this.add(new PhenotypeAsset
		{
			id = "savanna",
			shades_from = "#FFB52B",
			shades_to = "#D06643"
		});
		this.add(new PhenotypeAsset
		{
			id = "corrupted",
			shades_from = "#7E7987",
			shades_to = "#483162"
		});
		this.add(new PhenotypeAsset
		{
			id = "infernal",
			shades_from = "#FF5100",
			shades_to = "#FF002B"
		});
		this.add(new PhenotypeAsset
		{
			id = "lemon",
			shades_from = "#FFDB27",
			shades_to = "#BDF741"
		});
		this.add(new PhenotypeAsset
		{
			id = "desert",
			shades_from = "#D3C0AB",
			shades_to = "#B68637"
		});
		this.add(new PhenotypeAsset
		{
			id = "crystal",
			shades_from = "#1CFFCE",
			shades_to = "#15A1FF"
		});
		this.add(new PhenotypeAsset
		{
			id = "candy",
			shades_from = "#FF658C",
			shades_to = "#D166FF"
		});
		this.add(new PhenotypeAsset
		{
			id = "soil",
			shades_from = "#533028",
			shades_to = "#160C15"
		});
		this.add(new PhenotypeAsset
		{
			id = "wood",
			shades_from = "#9D582A",
			shades_to = "#4C2828"
		});
		this.add(new PhenotypeAsset
		{
			id = "pink_yellow_mushroom",
			shades_from = "#F12C78",
			shades_to = "#FFAC26"
		});
		this.add(new PhenotypeAsset
		{
			id = "black_blue",
			shades_from = "#34345B",
			shades_to = "#222229"
		});
		this.add(new PhenotypeAsset
		{
			id = "magical",
			shades_from = "#24C695",
			shades_to = "#9D2EA7"
		});
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00023194 File Offset: 0x00021394
	public override void linkAssets()
	{
		base.linkAssets();
		int tIndex = 1;
		foreach (PhenotypeAsset tAsset in this.list)
		{
			this.createShades(tAsset);
			tAsset.phenotype_index = tIndex++;
			this._phenotypes_assets_by_index.Add(tAsset.phenotype_index, tAsset);
		}
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0002320C File Offset: 0x0002140C
	public PhenotypeAsset getAssetByPhenotypeIndex(int pIndex)
	{
		PhenotypeAsset tAsset;
		this._phenotypes_assets_by_index.TryGetValue(pIndex, out tAsset);
		if (tAsset == null)
		{
			tAsset = PhenotypeLibrary.default_asset;
		}
		return tAsset;
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00023234 File Offset: 0x00021434
	public void createShades(PhenotypeAsset pAsset)
	{
		Color tColorFrom = Toolbox.makeColor(pAsset.shades_from);
		Color tColorTo = Toolbox.makeColor(pAsset.shades_to);
		float tStepAmount = 0.33333334f;
		for (int i = 0; i < 4; i++)
		{
			float tAmount = 1f - (float)i * tStepAmount;
			if (tAmount > 1f)
			{
				tAmount = 1f;
			}
			Color tNewColor = Toolbox.blendColor(tColorFrom, tColorTo, tAmount);
			pAsset.colors[i] = tNewColor;
		}
	}

	// Token: 0x0400033B RID: 827
	private Dictionary<int, PhenotypeAsset> _phenotypes_assets_by_index = new Dictionary<int, PhenotypeAsset>();

	// Token: 0x0400033C RID: 828
	public const int PHENOTYPE_SHADES = 4;

	// Token: 0x0400033D RID: 829
	public const int PHENOTYPE_NONE = 0;

	// Token: 0x0400033E RID: 830
	public static PhenotypeAsset default_asset;

	// Token: 0x0400033F RID: 831
	public static PhenotypeAsset default_green;
}
