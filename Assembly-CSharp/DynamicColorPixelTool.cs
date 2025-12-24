using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public static class DynamicColorPixelTool
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x00067D90 File Offset: 0x00065F90
	public static Color32 checkSpecialColors(Color32 pColor, ColorAsset pKingdomColor, bool pCheckForLightColors = false)
	{
		if (Config.EVERYTHING_MAGIC_COLOR)
		{
			return Toolbox.EVERYTHING_MAGIC_COLOR32;
		}
		if (pCheckForLightColors && Toolbox.areColorsEqual(pColor, Toolbox.color_light))
		{
			pColor = Toolbox.color_light_replace;
			return pColor;
		}
		if (pKingdomColor != null)
		{
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_0))
			{
				pColor = pKingdomColor.k_color_0;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_1))
			{
				pColor = pKingdomColor.k_color_1;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_2))
			{
				pColor = pKingdomColor.k_color_2;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_3))
			{
				pColor = pKingdomColor.k_color_3;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_4))
			{
				pColor = pKingdomColor.k_color_4;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_0))
			{
				pColor = pKingdomColor.k2_color_0;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_1))
			{
				pColor = pKingdomColor.k2_color_1;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_2))
			{
				pColor = pKingdomColor.k2_color_2;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_3))
			{
				pColor = pKingdomColor.k2_color_3;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_4))
			{
				pColor = pKingdomColor.k2_color_4;
			}
		}
		if (DynamicColorPixelTool._draw_phenotype)
		{
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_0))
			{
				pColor = DynamicColorPixelTool.phenotype_shade_0;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_1))
			{
				pColor = DynamicColorPixelTool.phenotype_shade_1;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_2))
			{
				pColor = DynamicColorPixelTool.phenotype_shade_2;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_3))
			{
				pColor = DynamicColorPixelTool.phenotype_shade_3;
			}
		}
		return pColor;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00067F18 File Offset: 0x00066118
	public static Color32 checkZombieColors(ActorAsset pAsset, Color32 pColor, int pID, bool pHead = false)
	{
		Color32 tZombieColor = Toolbox.makeColor(pAsset.zombie_color_hex);
		return DynamicColorPixelTool.addNoiseAndBlood(DynamicColorPixelTool.multiplyBlend(pColor, tZombieColor, 1f), pID);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00067F48 File Offset: 0x00066148
	private static Color32 addNoiseAndBlood(Color32 pTargetColor, int pID)
	{
		Random tPixelRandomizer = new Random(pID);
		if (tPixelRandomizer.NextDouble() < 0.5)
		{
			return DynamicColorPixelTool.multiplyBlend(pTargetColor, DynamicColorPixelTool._zombie_blood_color, 0.2f);
		}
		int tNoiseAmount = tPixelRandomizer.Next(0, 20);
		byte b = (byte)Mathf.Clamp((int)pTargetColor.r + tNoiseAmount, 0, 255);
		int tNewG = Mathf.Clamp((int)pTargetColor.g + tNoiseAmount, 0, 255);
		int tNewB = Mathf.Clamp((int)pTargetColor.b + tNoiseAmount, 0, 255);
		return new Color32(b, (byte)tNewG, (byte)tNewB, pTargetColor.a);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00067FD4 File Offset: 0x000661D4
	private static Color32 multiplyBlend(Color32 pBaseColor, Color32 pTargetBlendColor, float pIntensity = 1f)
	{
		float num = (float)pBaseColor.r / 255f;
		float tG = (float)pBaseColor.g / 255f;
		float tB = (float)pBaseColor.b / 255f;
		float tBlendR = Mathf.Lerp(1f, (float)pTargetBlendColor.r / 255f, pIntensity);
		float tBlendG = Mathf.Lerp(1f, (float)pTargetBlendColor.g / 255f, pIntensity);
		float tBlendB = Mathf.Lerp(1f, (float)pTargetBlendColor.b / 255f, pIntensity);
		float num2 = Mathf.Clamp01(num * tBlendR);
		float tNewG = Mathf.Clamp01(tG * tBlendG);
		float tNewB = Mathf.Clamp01(tB * tBlendB);
		return new Color32((byte)(num2 * 255f), (byte)(tNewG * 255f), (byte)(tNewB * 255f), pBaseColor.a);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00068098 File Offset: 0x00066298
	private static Color32 overlayBlend(Color32 pBaseColor, Color32 pTargetBlendColor)
	{
		float tR = (float)pBaseColor.r / 255f;
		float tG = (float)pBaseColor.g / 255f;
		float tB = (float)pBaseColor.b / 255f;
		float tBlendR = (float)pTargetBlendColor.r / 255f;
		float tBlendG = (float)pTargetBlendColor.g / 255f;
		float tBlendB = (float)pTargetBlendColor.b / 255f;
		float num = (tR < 0.5f) ? (2f * tR * tBlendR) : (1f - 2f * (1f - tR) * (1f - tBlendR));
		float tNewG = (tG < 0.5f) ? (2f * tG * tBlendG) : (1f - 2f * (1f - tG) * (1f - tBlendG));
		float tNewB = (tB < 0.5f) ? (2f * tB * tBlendB) : (1f - 2f * (1f - tB) * (1f - tBlendB));
		return new Color32((byte)(num * 255f), (byte)(tNewG * 255f), (byte)(tNewB * 255f), pBaseColor.a);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x000681B1 File Offset: 0x000663B1
	public static void loadPhenotype(int pPhenotypeIndex, int pPhenotypeShadeIndex)
	{
		DynamicColorPixelTool.loadPhenotype(AssetManager.phenotype_library.getAssetByPhenotypeIndex(pPhenotypeIndex), pPhenotypeShadeIndex);
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x000681C4 File Offset: 0x000663C4
	public static void loadPhenotype(PhenotypeAsset pPhenotypeAsset, int pPhenotypeShadeIndex)
	{
		DynamicColorPixelTool._phenotype_color = pPhenotypeAsset.colors[pPhenotypeShadeIndex];
		DynamicColorPixelTool._draw_phenotype = true;
		DynamicColorPixelTool.phenotype_shade_0 = Toolbox.makeDarkerColor(DynamicColorPixelTool._phenotype_color, 1f);
		DynamicColorPixelTool.phenotype_shade_1 = Toolbox.makeDarkerColor(DynamicColorPixelTool._phenotype_color, 0.9f);
		DynamicColorPixelTool.phenotype_shade_2 = Toolbox.makeDarkerColor(DynamicColorPixelTool._phenotype_color, 0.8f);
		DynamicColorPixelTool.phenotype_shade_3 = Toolbox.makeDarkerColor(DynamicColorPixelTool._phenotype_color, 0.7f);
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00068260 File Offset: 0x00066460
	public static void loadSkinColorsPreview(PhenotypeAsset pPhenotype, int pSkinColor)
	{
		DynamicColorPixelTool._draw_phenotype = true;
		DynamicColorPixelTool.phenotype_shade_0 = pPhenotype.colors[0];
		DynamicColorPixelTool.phenotype_shade_1 = pPhenotype.colors[1];
		DynamicColorPixelTool.phenotype_shade_2 = pPhenotype.colors[2];
		DynamicColorPixelTool.phenotype_shade_3 = pPhenotype.colors[3];
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x000682B7 File Offset: 0x000664B7
	public static void resetSkinColors()
	{
		DynamicColorPixelTool._draw_phenotype = false;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x000682BF File Offset: 0x000664BF
	public static void setPlaceholderSkinColor(Color32 pColor)
	{
		DynamicColorPixelTool._phenotype_color = pColor;
	}

	// Token: 0x04000784 RID: 1924
	private static bool _draw_phenotype;

	// Token: 0x04000785 RID: 1925
	private static Color32 _phenotype_color;

	// Token: 0x04000786 RID: 1926
	public static Color32 phenotype_shade_0;

	// Token: 0x04000787 RID: 1927
	public static Color32 phenotype_shade_1;

	// Token: 0x04000788 RID: 1928
	public static Color32 phenotype_shade_2;

	// Token: 0x04000789 RID: 1929
	public static Color32 phenotype_shade_3;

	// Token: 0x0400078A RID: 1930
	private static readonly Color32 _zombie_blood_color = Toolbox.makeColor("#CE566E");
}
