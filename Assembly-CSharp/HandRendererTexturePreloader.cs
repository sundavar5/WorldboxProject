using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000167 RID: 359
public static class HandRendererTexturePreloader
{
	// Token: 0x06000AFC RID: 2812 RVA: 0x000A0B18 File Offset: 0x0009ED18
	public static void launch()
	{
		AssetManager.items.loadSprites();
		AssetManager.unit_hand_tools.loadSprites();
		AssetManager.resources.loadSprites();
		HandRendererTexturePreloader.preloadItemsIntoAtlas();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x000A0B40 File Offset: 0x0009ED40
	private static void preloadItemsIntoAtlas()
	{
		foreach (UnitHandToolAsset unitHandToolAsset in AssetManager.unit_hand_tools.list)
		{
			bool tUseColors = unitHandToolAsset.is_colored;
			HandRendererTexturePreloader.preloadSpritesUnitHands(unitHandToolAsset.getSprites(), tUseColors);
		}
		foreach (EquipmentAsset equipmentAsset in AssetManager.items.list)
		{
			bool tUseColors2 = equipmentAsset.is_colored;
			HandRendererTexturePreloader.preloadSpritesUnitHands(equipmentAsset.getSprites(), tUseColors2);
		}
		foreach (ResourceAsset resourceAsset in AssetManager.resources.list)
		{
			bool tUseColors3 = resourceAsset.is_colored;
			HandRendererTexturePreloader.preloadSpritesUnitHands(resourceAsset.getSprites(), tUseColors3);
		}
		Debug.Log("Total Preloaded Hand Renderer Sprites : " + HandRendererTexturePreloader._preloaded_items_counter.ToString() + " with colors " + ColorAsset.getAllColorsList().Count.ToString());
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x000A0C78 File Offset: 0x0009EE78
	private static void preloadSpritesUnitHands(Sprite[] pSprites, bool pUseColors)
	{
		if (pSprites == null)
		{
			return;
		}
		if (pUseColors)
		{
			using (List<ColorAsset>.Enumerator enumerator = ColorAsset.getAllColorsList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ColorAsset tColorAsset = enumerator.Current;
					for (int i = 0; i < pSprites.Length; i++)
					{
						DynamicSprites.preloadItemSprite(pSprites[i], tColorAsset);
						HandRendererTexturePreloader._preloaded_items_counter++;
					}
				}
				return;
			}
		}
		for (int i = 0; i < pSprites.Length; i++)
		{
			DynamicSprites.preloadItemSprite(pSprites[i], null);
			HandRendererTexturePreloader._preloaded_items_counter++;
		}
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000A0D14 File Offset: 0x0009EF14
	public static int getTotal()
	{
		return HandRendererTexturePreloader._preloaded_items_counter;
	}

	// Token: 0x04000A74 RID: 2676
	private static int _preloaded_items_counter;
}
