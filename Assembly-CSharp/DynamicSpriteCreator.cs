using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EC RID: 236
public static class DynamicSpriteCreator
{
	// Token: 0x060006F1 RID: 1777 RVA: 0x000682E0 File Offset: 0x000664E0
	public static Sprite createNewItemSprite(DynamicSpritesAsset pAsset, Sprite pSource, ColorAsset pKingdomColor)
	{
		UnitSpriteConstructorAtlas tAtlas = pAsset.getAtlas();
		Rect tTextureRectBody = pSource.rect;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		tAtlas.checkBounds(tWidth, tHeight);
		int tTextureWidth = tAtlas.texture.width;
		int height = tAtlas.texture.height;
		Color32[] tPartPixels = pSource.texture.GetPixels32();
		int tBodyTextureWidth = pSource.texture.width;
		int xx = 0;
		while ((float)xx < tTextureRectBody.width)
		{
			int yy = 0;
			while ((float)yy < tTextureRectBody.height)
			{
				int num = xx + (int)tTextureRectBody.x;
				int tPixelY = yy + (int)tTextureRectBody.y;
				int tPixelID = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tPartPixels[tPixelID];
				if (tColor.a != 0)
				{
					tColor = DynamicColorPixelTool.checkSpecialColors(tColor, pKingdomColor, true);
					int pX = xx + tAtlas.last_x;
					int pY = yy + tAtlas.last_y;
					if (pX < 0)
					{
						pX = 0;
					}
					if (pY < 0)
					{
						pY = 0;
					}
					tPixelID = pX + pY * tTextureWidth;
					tAtlas.pixels[tPixelID] = tColor;
				}
				yy++;
			}
			xx++;
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00068414 File Offset: 0x00066614
	private static Sprite createFinalSprite(UnitSpriteConstructorAtlas pAtlasTexture, Sprite pMain, int pWidth, int pHeight, int pResizeX = 0, int pResizeY = 0)
	{
		Rect tRect = new Rect((float)pAtlasTexture.last_x, (float)pAtlasTexture.last_y, (float)pWidth, (float)pHeight);
		Vector2 tPivot = new Vector2((pMain.pivot.x + (float)pResizeX) / (float)pWidth, pMain.pivot.y / (float)pHeight);
		Sprite sprite = Sprite.Create(pAtlasTexture.texture, tRect, tPivot, 1f);
		sprite.name = "gen_" + pMain.name;
		pAtlasTexture.last_x += pWidth + 1;
		return sprite;
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0006849C File Offset: 0x0006669C
	private static Sprite createNewSpriteBuildingShadow(DynamicSpritesAsset pDynamicSpritesAsset, BuildingAsset tAsset, Sprite pSource, bool pIsContructionSprite)
	{
		UnitSpriteConstructorAtlas tAtlas = pDynamicSpritesAsset.getAtlas();
		Rect tTextureRectBody = pSource.rect;
		int tBonusWidth = 3;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		int tSpriteX = (int)tTextureRectBody.x;
		int tSpriteY = (int)tTextureRectBody.y;
		tAtlas.checkBounds(tWidth + tBonusWidth, tHeight);
		int tDrawTextureWidth = tAtlas.texture.width;
		int height = tAtlas.texture.height;
		Color32[] tPartPixels = pSource.texture.GetPixels32();
		Vector2 tShadowBound;
		float tDistortion;
		if (pIsContructionSprite)
		{
			tShadowBound = BuildingLibrary.shadow_under_construction_bound;
			tDistortion = BuildingLibrary.shadow_under_construction_distortion;
		}
		else
		{
			tShadowBound = tAsset.shadow_bound;
			tDistortion = tAsset.shadow_distortion;
		}
		int tBoundX = (int)(tShadowBound.x * (float)tWidth);
		int tBoundY = (int)((float)tHeight * tShadowBound.y);
		List<Vector2Int> tListAdds = new List<Vector2Int>();
		Color32 tColorBlack = Color.black;
		int tBodyTextureWidth = pSource.texture.width;
		for (int xx = 0; xx < tWidth; xx++)
		{
			for (int yy = 0; yy < tHeight; yy++)
			{
				int num = xx + tSpriteX;
				int tPixelY = yy + tSpriteY;
				int tPixelID = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tPartPixels[tPixelID];
				if (tColor.a != 0)
				{
					tColor = tColorBlack;
					if (xx >= tBoundX)
					{
						int pX = xx + tAtlas.last_x;
						int pY = yy + tAtlas.last_y;
						if (yy > tBoundY)
						{
							pY = (int)((float)yy * tDistortion) + tAtlas.last_y;
						}
						if (pX < 0)
						{
							pX = 0;
						}
						if (pY < 0)
						{
							pY = 0;
						}
						tListAdds.Add(new Vector2Int(pX, pY));
						tPixelID = pX + pY * tDrawTextureWidth;
						tAtlas.pixels[tPixelID] = tColor;
					}
				}
			}
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		tWidth += tBonusWidth;
		foreach (Vector2Int tPix in tListAdds)
		{
			int num2 = tPix.x + 1;
			int tPy = tPix.y;
			int pixelId = num2 + tPy * tDrawTextureWidth;
			tAtlas.pixels[pixelId] = tColorBlack;
			int num3 = tPix.x + 2;
			tPy = tPix.y;
			pixelId = num3 + tPy * tDrawTextureWidth;
			tAtlas.pixels[pixelId] = tColorBlack;
			int num4 = tPix.x + 1;
			tPy = tPix.y + 1;
			pixelId = num4 + tPy * tDrawTextureWidth;
			tAtlas.pixels[pixelId] = tColorBlack;
		}
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0006870C File Offset: 0x0006690C
	public static Sprite createNewUnitShadow(DynamicSpritesAsset pAsset, Sprite pSource)
	{
		UnitSpriteConstructorAtlas tAtlas = pAsset.getAtlas();
		Rect tTextureRectBody = pSource.rect;
		int tBonusWidth = 1;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		int tSpriteX = (int)tTextureRectBody.x;
		int tSpriteY = (int)tTextureRectBody.y;
		tAtlas.checkBounds(tWidth + tBonusWidth, tHeight);
		int tDrawTextureWidth = tAtlas.texture.width;
		int height = tAtlas.texture.height;
		Color32[] tPartPixels = pSource.texture.GetPixels32();
		int tBodyTextureWidth = pSource.texture.width;
		for (int xx = 0; xx < tWidth; xx++)
		{
			for (int yy = 0; yy < tHeight; yy++)
			{
				int num = xx + tSpriteX;
				int tPixelY = yy + tSpriteY;
				int tPixelId = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tPartPixels[tPixelId];
				if (tColor.a != 0)
				{
					int pX = xx + tAtlas.last_x;
					int pY = yy + tAtlas.last_y;
					if (pX < 0)
					{
						pX = 0;
					}
					if (pY < 0)
					{
						pY = 0;
					}
					tPixelId = pX + pY * tDrawTextureWidth;
					tAtlas.pixels[tPixelId] = tColor;
				}
			}
		}
		tWidth += tBonusWidth;
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00068830 File Offset: 0x00066A30
	public static void createBuildingShadow(BuildingAsset pAsset, Sprite pSprite, bool pIsContructionSprite)
	{
		DynamicSpritesAsset building_shadows = DynamicSpritesLibrary.building_shadows;
		int tId = pSprite.GetHashCode();
		Sprite tResult = DynamicSpriteCreator.createNewSpriteBuildingShadow(building_shadows, pAsset, pSprite, pIsContructionSprite);
		building_shadows.addSprite((long)tId, tResult);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0006885C File Offset: 0x00066A5C
	public static Sprite createNewIcon(DynamicSpritesAsset pAsset, Sprite pSource, ColorAsset pKingdomColor, PhenotypeAsset pPhenotype = null)
	{
		UnitSpriteConstructorAtlas tAtlas = pAsset.getAtlas();
		if (pPhenotype != null)
		{
			DynamicColorPixelTool.loadSkinColorsPreview(pPhenotype, 0);
		}
		Rect tTextureRectBody = pSource.rect;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		tAtlas.checkBounds(tWidth, tHeight);
		int tTextureWidth = tAtlas.texture.width;
		int height = tAtlas.texture.height;
		Color32[] tPartPixels = pSource.texture.GetPixels32();
		int tBodyTextureWidth = pSource.texture.width;
		int xx = 0;
		while ((float)xx < tTextureRectBody.width)
		{
			int yy = 0;
			while ((float)yy < tTextureRectBody.height)
			{
				int num = xx + (int)tTextureRectBody.x;
				int tPixelY = yy + (int)tTextureRectBody.y;
				int tPixelID = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tPartPixels[tPixelID];
				if (tColor.a != 0)
				{
					tColor = DynamicColorPixelTool.checkSpecialColors(tColor, pKingdomColor, true);
					int pX = xx + tAtlas.last_x;
					int pY = yy + tAtlas.last_y;
					if (pX < 0)
					{
						pX = 0;
					}
					if (pY < 0)
					{
						pY = 0;
					}
					tPixelID = pX + pY * tTextureWidth;
					tAtlas.pixels[tPixelID] = tColor;
				}
				yy++;
			}
			xx++;
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00068998 File Offset: 0x00066B98
	public static Sprite createNewSpriteBuilding(DynamicSpritesAsset pAssetAtlas, long pID, Sprite pSource, ColorAsset pKingdomColor)
	{
		UnitSpriteConstructorAtlas tAtlas = pAssetAtlas.getAtlas();
		Rect tTextureRectBody = pSource.rect;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		tAtlas.checkBounds(tWidth, tHeight);
		int tTextureWidth = tAtlas.texture.width;
		int height = tAtlas.texture.height;
		Color32[] tPartPixels = pSource.texture.GetPixels32();
		DynamicSpriteCreator._light_colors.Clear();
		int tBodyTextureWidth = pSource.texture.width;
		int xx = 0;
		while ((float)xx < tTextureRectBody.width)
		{
			int yy = 0;
			while ((float)yy < tTextureRectBody.height)
			{
				int num = xx + (int)tTextureRectBody.x;
				int tPixelY = yy + (int)tTextureRectBody.y;
				int pixelId = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tPartPixels[pixelId];
				if (tColor.a != 0)
				{
					if (Toolbox.areColorsEqual(tColor, Toolbox.color_light))
					{
						DynamicSpriteCreator._light_colors.Add(new Vector2Int(xx, yy));
					}
					tColor = DynamicColorPixelTool.checkSpecialColors(tColor, pKingdomColor, true);
					int pX = xx + tAtlas.last_x;
					int pY = yy + tAtlas.last_y;
					if (pX < 0)
					{
						pX = 0;
					}
					if (pY < 0)
					{
						pY = 0;
					}
					pixelId = pX + pY * tTextureWidth;
					tAtlas.pixels[pixelId] = tColor;
				}
				yy++;
			}
			xx++;
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		Sprite result = DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
		if (DynamicSpriteCreator._light_colors.Count > 0)
		{
			DynamicSpriteCreator.checkBuildingLightSprite(DynamicSpritesLibrary.building_lights, (long)pSource.GetHashCode(), pSource);
		}
		return result;
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00068B14 File Offset: 0x00066D14
	private static void checkBuildingLightSprite(DynamicSpritesAsset pQuantumAsset, long pHashcodeMainSprite, Sprite pSprite)
	{
		if (pQuantumAsset.getSprite(pHashcodeMainSprite) == null)
		{
			Sprite tResult = DynamicSpriteCreator.createNewSpriteBuildingLight(pQuantumAsset, pSprite);
			pQuantumAsset.addSprite(pHashcodeMainSprite, tResult);
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00068B3C File Offset: 0x00066D3C
	public static Sprite createNewSpriteBuildingLight(DynamicSpritesAsset pAsset, Sprite pSource)
	{
		UnitSpriteConstructorAtlas tAtlas = pAsset.getAtlas();
		Rect tTextureRectBody = pSource.rect;
		int tWidth = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		tAtlas.checkBounds(tWidth, tHeight);
		int tBodyTextureWidth = pSource.texture.width;
		for (int i = 0; i < DynamicSpriteCreator._light_colors.Count; i++)
		{
			Vector2Int tColorCoords = DynamicSpriteCreator._light_colors[i];
			DynamicSpriteCreator.drawLightPixel(tAtlas, tColorCoords.x, tColorCoords.y, tWidth, tHeight, tBodyTextureWidth, Toolbox.color_light_100);
			DynamicSpriteCreator.drawLightPixel(tAtlas, tColorCoords.x, tColorCoords.y - 1, tWidth, tHeight, tBodyTextureWidth, Toolbox.color_light_10);
			DynamicSpriteCreator.drawLightPixel(tAtlas, tColorCoords.x - 1, tColorCoords.y, tWidth, tHeight, tBodyTextureWidth, Toolbox.color_light_10);
			DynamicSpriteCreator.drawLightPixel(tAtlas, tColorCoords.x + 1, tColorCoords.y, tWidth, tHeight, tBodyTextureWidth, Toolbox.color_light_10);
			DynamicSpriteCreator.drawLightPixel(tAtlas, tColorCoords.x, tColorCoords.y + 1, tWidth, tHeight, tBodyTextureWidth, Toolbox.color_light_10);
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSource, tWidth, tHeight, 0, 0);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00068C58 File Offset: 0x00066E58
	private static void drawLightPixel(UnitSpriteConstructorAtlas pAtlas, int pColorCoordsX, int pColorCoordsY, int pWidth, int pHeight, int pBodyTextureWidth, Color32 pColor)
	{
		int pX = pColorCoordsX + pAtlas.last_x;
		int pY = pColorCoordsY + pAtlas.last_y;
		if (pX < 0)
		{
			pX = 0;
		}
		if (pY < 0)
		{
			pY = 0;
		}
		int tPixelID = pX + pY * pAtlas.texture.width;
		if (pAtlas.pixels[tPixelID].a >= pColor.a)
		{
			return;
		}
		pAtlas.pixels[tPixelID] = pColor;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00068CBC File Offset: 0x00066EBC
	public static Sprite createNewSpriteForDebug(Sprite pSpriteSource, ColorAsset pKingdomColor)
	{
		Rect tTextureRectBody = pSpriteSource.rect;
		int width = (int)tTextureRectBody.width;
		int tHeight = (int)tTextureRectBody.height;
		Color32[] tMainPixels = pSpriteSource.texture.GetPixels32();
		Texture2D tTexture = new Texture2D(width, tHeight);
		tTexture.filterMode = FilterMode.Point;
		tTexture.wrapMode = TextureWrapMode.Clamp;
		Color32[] tNewPixels = tTexture.GetPixels32();
		int tBodyTextureWidth = pSpriteSource.texture.width;
		int xx = 0;
		while ((float)xx < tTextureRectBody.width)
		{
			int yy = 0;
			while ((float)yy < tTextureRectBody.height)
			{
				int num = xx + (int)tTextureRectBody.x;
				int tPixelY = yy + (int)tTextureRectBody.y;
				int tPixelID = num + tPixelY * tBodyTextureWidth;
				Color32 tColor = tMainPixels[tPixelID];
				if (tColor.a == 0)
				{
					tNewPixels[tPixelID] = tColor;
				}
				else
				{
					tColor = DynamicColorPixelTool.checkSpecialColors(tColor, pKingdomColor, true);
					tNewPixels[tPixelID] = tColor;
				}
				yy++;
			}
			xx++;
		}
		tTexture.SetPixels32(tNewPixels);
		tTexture.Apply();
		Sprite sprite = Sprite.Create(tTexture, tTextureRectBody, pSpriteSource.pivot, 1f);
		sprite.name = "gen_" + pSpriteSource.name;
		return sprite;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00068DD4 File Offset: 0x00066FD4
	public static Sprite createNewSpriteUnit(AnimationFrameData pFrameData, Sprite pSourceBody, Sprite pSourceHead, ColorAsset pKingdomColor, ActorAsset pAsset, int pPhenotypeIndex, int pPhenotypeShade, UnitTextureAtlasID pAtlasID)
	{
		UnitSpriteConstructorAtlas tAtlas = null;
		if (pAtlasID != UnitTextureAtlasID.Units)
		{
			if (pAtlasID == UnitTextureAtlasID.Boats)
			{
				tAtlas = DynamicSpritesLibrary.boats.getAtlas();
			}
		}
		else
		{
			tAtlas = DynamicSpritesLibrary.units.getAtlas();
		}
		PixelBag pixelBag = PixelBagManager.getPixelBag(pSourceBody, true, false);
		int tWidth = pixelBag.texture_rect_width;
		int tHeight = pixelBag.texture_rect_height;
		int tAdditionalXHead = 0;
		int tAdditionalYHead = 0;
		DynamicColorPixelTool.setPlaceholderSkinColor(DynamicSpriteCreator._placeholder_color_skin);
		DynamicColorPixelTool.resetSkinColors();
		if (pPhenotypeIndex != 0)
		{
			DynamicColorPixelTool.loadPhenotype(pPhenotypeIndex, pPhenotypeShade);
		}
		if (pSourceHead != null && pFrameData != null)
		{
			Rect tTextureRectHead = pSourceHead.rect;
			Vector2 tHeadPos = pFrameData.pos_head_new;
			int tDiffY = (int)tHeadPos.y + (int)tTextureRectHead.height - tHeight;
			if (tDiffY > 0)
			{
				tAdditionalYHead = tDiffY;
			}
			int tDiffX = (int)tHeadPos.x + (int)tTextureRectHead.width - tWidth;
			if (tDiffX > 0)
			{
				tAdditionalXHead = tDiffX;
			}
			else if (tHeadPos.x < 0f)
			{
				tAdditionalXHead = -(int)tHeadPos.x;
			}
		}
		int tResizeX = tAdditionalXHead;
		int tResizeY = tAdditionalYHead;
		tWidth += tResizeX;
		tHeight += tResizeY;
		tAtlas.checkBounds(tWidth, tHeight);
		DynamicSpriteCreator.fillDebugColor(tWidth, tHeight, tAtlas);
		bool tDynamicZombie = pAsset.dynamic_sprite_zombie;
		int tPartX = tResizeX + tAtlas.last_x;
		int tPartY = tAtlas.last_y;
		DynamicSpriteCreator.drawPixelsAll(pixelBag, tAtlas, pKingdomColor, tPartX, tPartY, tDynamicZombie, pAsset, false);
		if (pSourceHead != null && pFrameData != null)
		{
			PixelBag pixelBag2 = PixelBagManager.getPixelBag(pSourceHead, true, false);
			Vector2 pos_head_new = pFrameData.pos_head_new;
			Vector2 tPivotHead = pSourceHead.pivot;
			int tPartHeadX = (int)pos_head_new.x - (int)tPivotHead.x;
			int tPartHeadY = (int)pos_head_new.y - (int)tPivotHead.y;
			tPartX += tPartHeadX;
			tPartY += tPartHeadY;
			DynamicSpriteCreator.drawPixelsAll(pixelBag2, tAtlas, pKingdomColor, tPartX, tPartY, tDynamicZombie, pAsset, true);
		}
		DynamicSpriteCreator.setAtlasDirty(tAtlas);
		return DynamicSpriteCreator.createFinalSprite(tAtlas, pSourceBody, tWidth, tHeight, tResizeX, 0);
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00068F64 File Offset: 0x00067164
	private static void fillDebugColor(int pWidth, int pHeight, UnitSpriteConstructorAtlas pAtlas)
	{
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00068F68 File Offset: 0x00067168
	private static void drawPixelsAll(PixelBag pBag, UnitSpriteConstructorAtlas pAtlas, ColorAsset pKingdomColor, int pPartX, int pPartY, bool pDynamicZombie, ActorAsset pActorAsset, bool pHead = false)
	{
		Color32[] pixels = pAtlas.pixels;
		int tAtlasWidth = pAtlas.texture.width;
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k1_0, pKingdomColor.k_color_0, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k1_1, pKingdomColor.k_color_1, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k1_2, pKingdomColor.k_color_2, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k1_3, pKingdomColor.k_color_3, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k1_4, pKingdomColor.k_color_4, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k2_0, pKingdomColor.k2_color_0, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k2_1, pKingdomColor.k2_color_1, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k2_2, pKingdomColor.k2_color_2, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k2_3, pKingdomColor.k2_color_3, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_k2_4, pKingdomColor.k2_color_4, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_light, Toolbox.color_light_replace, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_normal, Toolbox.color_magenta_1, pPartX, pPartY, pDynamicZombie, pActorAsset, true, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_phenotype_shade_0, DynamicColorPixelTool.phenotype_shade_0, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_phenotype_shade_1, DynamicColorPixelTool.phenotype_shade_1, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_phenotype_shade_2, DynamicColorPixelTool.phenotype_shade_2, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
		DynamicSpriteCreator.drawPixels(pixels, tAtlasWidth, pBag.arr_pixels_phenotype_shade_3, DynamicColorPixelTool.phenotype_shade_3, pPartX, pPartY, pDynamicZombie, pActorAsset, false, pHead);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00069150 File Offset: 0x00067350
	private static void drawPixels(Color32[] pPixels, int pAtlasWidth, Pixel[] pListSourcePixels, Color32 pNewColor, int pPartX, int pPartY, bool pDrawDynamicZombie, ActorAsset pActorAsset, bool pUseNormal = false, bool pHead = false)
	{
		if (pListSourcePixels == null)
		{
			return;
		}
		foreach (Pixel tSourcePixel in pListSourcePixels)
		{
			Color32 tColorToSet = pNewColor;
			int tX = tSourcePixel.x + pPartX;
			int tY = tSourcePixel.y + pPartY;
			if (tX < 0)
			{
				tX = 0;
			}
			if (tY < 0)
			{
				tY = 0;
			}
			int tPixelID = tX + tY * pAtlasWidth;
			if (pUseNormal)
			{
				tColorToSet = tSourcePixel.color;
			}
			if (pDrawDynamicZombie)
			{
				tColorToSet = DynamicColorPixelTool.checkZombieColors(pActorAsset, tColorToSet, tPixelID / 3 + tX, pHead);
			}
			pPixels[tPixelID] = tColorToSet;
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x000691D0 File Offset: 0x000673D0
	public static Sprite getSpriteUnit(AnimationFrameData pFrameData, Sprite pMainSprite, Actor pActor, ColorAsset pKingdomColor, int pPhenotypeIndex, int pPhenotypeShade, UnitTextureAtlasID pTextureAtlasID)
	{
		long t_kingdomID = 0L;
		long t_phenotypeShadeID = 0L;
		long t_phenotypeIndex = (long)pPhenotypeIndex;
		long t_headID = 0L;
		long t_bodyID = (long)DynamicSpriteCreator.getBodySpriteSmallID(pMainSprite);
		if (pActor.has_rendered_sprite_head)
		{
			int tHeadId;
			ActorAnimationLoader.int_ids_heads.TryGetValue(pActor.cached_sprite_head, out tHeadId);
			if (tHeadId == 0)
			{
				int tNewID = ActorAnimationLoader.int_ids_heads.Count + 1;
				ActorAnimationLoader.int_ids_heads.Add(pActor.cached_sprite_head, tNewID);
				tHeadId = tNewID;
			}
			t_headID = (long)tHeadId;
		}
		if (t_phenotypeIndex != 0L)
		{
			t_phenotypeShadeID = (long)(pPhenotypeShade + 1);
		}
		if (pKingdomColor != null)
		{
			t_kingdomID = (long)(pKingdomColor.index_id + 1);
		}
		long tId = t_kingdomID * 1000000000000L + t_headID * 1000000000L + t_bodyID * 1000000L + t_phenotypeIndex * 1000L + t_phenotypeShadeID;
		if (DynamicSpriteCreator.debug_actor == pActor)
		{
			AssetManager.dynamic_sprites_library.setDebugActor(tId, t_kingdomID, t_headID, t_bodyID, t_phenotypeIndex, t_phenotypeShadeID);
		}
		DynamicSpritesAsset tAsset = DynamicSpritesLibrary.units;
		Sprite tResult = tAsset.getSprite(tId);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewSpriteUnit(pFrameData, pMainSprite, pActor.cached_sprite_head, pKingdomColor, pActor.asset, pPhenotypeIndex, pPhenotypeShade, pTextureAtlasID);
			tAsset.addSprite(tId, tResult);
		}
		return tResult;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x000692D4 File Offset: 0x000674D4
	public static void setAtlasDirty(UnitSpriteConstructorAtlas pAtlas)
	{
		AssetManager.dynamic_sprites_library.setDirty();
		pAtlas.dirty = true;
		if (!pAtlas.isBigSpriteSheetAtlas())
		{
			pAtlas.checkDirty();
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000692F8 File Offset: 0x000674F8
	public static int getBodySpriteSmallID(Sprite pSprite)
	{
		int tResult;
		if (!DynamicSpriteCreator._int_ids_body.TryGetValue(pSprite, out tResult))
		{
			tResult = DynamicSpriteCreator._int_ids_body.Count + 1;
			DynamicSpriteCreator._int_ids_body.Add(pSprite, tResult);
		}
		return tResult;
	}

	// Token: 0x0400078B RID: 1931
	public static Actor debug_actor;

	// Token: 0x0400078C RID: 1932
	private static Dictionary<Sprite, int> _int_ids_body = new Dictionary<Sprite, int>();

	// Token: 0x0400078D RID: 1933
	private static readonly Color32 _placeholder_color_skin = Toolbox.makeColor("#00FF00");

	// Token: 0x0400078E RID: 1934
	private static readonly List<Vector2Int> _light_colors = new List<Vector2Int>();
}
