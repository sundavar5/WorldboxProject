using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public static class DynamicSprites
{
	// Token: 0x06000704 RID: 1796 RVA: 0x00069358 File Offset: 0x00067558
	public static Sprite getIconWithColors(Sprite pSprite, PhenotypeAsset pPhenotype, ColorAsset pKingdomColor)
	{
		DynamicSpritesAsset tAsset = DynamicSpritesLibrary.icons;
		long tId = (long)(pSprite.GetHashCode() * 10000 + ((pPhenotype != null) ? pPhenotype.GetHashCode() : 0) * 100 + ((pKingdomColor != null) ? pKingdomColor.GetHashCode() : 0));
		Sprite tResult = tAsset.getSprite(tId);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewIcon(tAsset, pSprite, pKingdomColor, pPhenotype);
			tAsset.addSprite(tId, tResult);
		}
		return tResult;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x000693B4 File Offset: 0x000675B4
	public static Sprite getRecoloredBuilding(Sprite pBuildingSprite, ColorAsset pColor, DynamicSpritesAsset pAtlasAsset)
	{
		long tId = DynamicSprites.getBuildingSpriteID(pBuildingSprite.GetHashCode(), pColor);
		Sprite tResult = pAtlasAsset.getSprite(tId);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewSpriteBuilding(pAtlasAsset, tId, pBuildingSprite, pColor);
			pAtlasAsset.addSprite(tId, tResult);
		}
		return tResult;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x000693EC File Offset: 0x000675EC
	private static long getBuildingSpriteID(int pBaseSpriteID, ColorAsset pColor)
	{
		long t_kingdomID;
		if (pColor == null)
		{
			t_kingdomID = -1000000L;
		}
		else
		{
			t_kingdomID = (long)(pColor.index_id + 1);
		}
		return (t_kingdomID + 1L) * 10000000L + (long)pBaseSpriteID;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00069420 File Offset: 0x00067620
	public static Sprite getBuildingLight(Building pBuilding)
	{
		DynamicSpritesAsset building_lights = DynamicSpritesLibrary.building_lights;
		int tID = pBuilding.last_main_sprite.GetHashCode();
		return building_lights.getSprite((long)tID);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00069448 File Offset: 0x00067648
	public static Sprite getIcon(Sprite pSprite, ColorAsset pColorAsset)
	{
		DynamicSpritesAsset tAsset = DynamicSpritesLibrary.icons;
		long tId = (long)(pSprite.GetHashCode() * 10000 + pColorAsset.GetHashCode());
		Sprite tResult = tAsset.getSprite(tId);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewIcon(tAsset, pSprite, pColorAsset, null);
			tAsset.addSprite(tId, tResult);
		}
		return tResult;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00069490 File Offset: 0x00067690
	public static Sprite getShadowBuilding(BuildingAsset pAsset, Sprite pSprite)
	{
		if (!pAsset.shadow)
		{
			return null;
		}
		int tId = pSprite.GetHashCode();
		return DynamicSpritesLibrary.building_shadows.getSprite((long)tId);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x000694BC File Offset: 0x000676BC
	public static Sprite getShadowUnit(Sprite pSprite, int pHashCode)
	{
		DynamicSpritesAsset tAsset = DynamicSpritesLibrary.units_shadows;
		Sprite tResult = tAsset.getSprite((long)pHashCode);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewUnitShadow(tAsset, pSprite);
			tAsset.addSprite((long)pHashCode, tResult);
		}
		return tResult;
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x000694F0 File Offset: 0x000676F0
	public static void preloadItemSprite(Sprite pSprite, ColorAsset pColorAsset = null)
	{
		long tId = DynamicSprites.getItemSpriteID(pSprite, pColorAsset);
		DynamicSpritesAsset items = DynamicSpritesLibrary.items;
		Sprite tNewSprite = DynamicSpriteCreator.createNewItemSprite(items, pSprite, pColorAsset);
		items.addSprite(tId, tNewSprite);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0006951C File Offset: 0x0006771C
	public static long getItemSpriteID(Sprite pSprite, ColorAsset pColor)
	{
		int tHashCodeColor;
		if (pColor != null)
		{
			tHashCodeColor = pColor.GetHashCode();
		}
		else
		{
			tHashCodeColor = -900000;
		}
		return DynamicSprites.getItemSpriteID(pSprite, tHashCodeColor);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00069542 File Offset: 0x00067742
	public static long getItemSpriteID(Sprite pSprite, int pColorID = -900000)
	{
		return (long)(pSprite.GetHashCode() * 10000 + pColorID);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00069554 File Offset: 0x00067754
	public static Sprite getCachedAtlasItemSprite(long pID, Sprite pSpriteSource)
	{
		Sprite tResult = DynamicSpritesLibrary.items.getSprite(pID);
		if (tResult == null)
		{
			Debug.LogError("[getCachedAtlasItemSprite]Dynamic sprite not found: " + pID.ToString() + " " + ((pSpriteSource != null) ? pSpriteSource.ToString() : null));
			return pSpriteSource;
		}
		return tResult;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0006959C File Offset: 0x0006779C
	public static Sprite getCachedAtlasItemSprite(long pID, Sprite pSpriteSource, ColorAsset pColorAsset)
	{
		Sprite tResult = DynamicSpritesLibrary.items.getSprite(pID);
		if (tResult == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"[getCachedAtlasItemSprite]Dynamic sprite not found: ",
				pID.ToString(),
				" ",
				(pSpriteSource != null) ? pSpriteSource.ToString() : null,
				" ",
				(pColorAsset != null) ? (pColorAsset.index_id.ToString() + " " + pColorAsset.color_main) : "null"
			}));
			return pSpriteSource;
		}
		return tResult;
	}

	// Token: 0x0400078F RID: 1935
	public const int NO_COLOR_ID = -900000;
}
