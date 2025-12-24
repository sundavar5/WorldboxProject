using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x02000169 RID: 361
public static class PreloadHelpers
{
	// Token: 0x06000B01 RID: 2817 RVA: 0x000A0D4C File Offset: 0x0009EF4C
	public static void init()
	{
		if (PreloadHelpers._initiated)
		{
			return;
		}
		PreloadHelpers._initiated = true;
		WindowPreloader.addWindowPreloadResources();
		SmoothLoader.add(delegate
		{
			HandRendererTexturePreloader.launch();
		}, "Preload hand item textures...", false, 0.2f, false);
		SmoothLoader.add(delegate
		{
			AssetManager.actor_library.preloadMainUnitSprites();
			Debug.Log("Loaded unit related sprites: " + ActorTextureSubAsset.all_preloaded_sprites_units.Count.ToString());
		}, "Preload main unit textures...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			AssetManager.subspecies_traits.preloadMainUnitSprites();
			Debug.Log("Loaded unit related sprites: " + ActorTextureSubAsset.all_preloaded_sprites_units.Count.ToString());
		}, "Preload unit trait textures...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadBuildingSpriteLists();
		}, "Preload building sprite lists...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadBuildingSprites();
		}, "Preload building sprites...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadBoatAnimations();
		}, "Preload boat animations...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadActorAnimations();
		}, "Preload unit animations...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadPixelBagsUnits();
		}, "Preload pixel bags units...", false, 0.1f, false);
		SmoothLoader.add(delegate
		{
			PreloadHelpers.preloadPixelBagsBuildings();
		}, "Preload pixel bags buildings...", false, 0.1f, false);
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000A0F1C File Offset: 0x0009F11C
	private static void preloadBuildingSpriteLists()
	{
		if (!Config.preload_buildings)
		{
			return;
		}
		foreach (BuildingAsset buildingAsset in AssetManager.buildings.list)
		{
			buildingAsset.loadBuildingSpriteList();
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x000A0F7C File Offset: 0x0009F17C
	private static void preloadBuildingSprites()
	{
		if (!Config.preload_buildings)
		{
			return;
		}
		foreach (BuildingAsset buildingAsset in AssetManager.buildings.list)
		{
			buildingAsset.loadBuildingSprites();
		}
		AssetManager.dynamic_sprites_library.checkDirty();
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
	private static void preloadBoatAnimations()
	{
		if (!Config.preload_units)
		{
			return;
		}
		foreach (ActorAsset actorAsset in AssetManager.actor_library.list_only_boat_assets)
		{
			ActorAnimationLoader.loadAnimationBoat(actorAsset.boat_texture_id);
		}
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x000A1048 File Offset: 0x0009F248
	private static void preloadActorAnimations()
	{
		if (!Config.preload_units)
		{
			return;
		}
		foreach (ActorAsset tActorAsset in AssetManager.actor_library.list)
		{
			if (!tActorAsset.isTemplateAsset() && !tActorAsset.is_boat && !tActorAsset.ignore_generic_render && tActorAsset.texture_asset != null)
			{
				foreach (KeyValuePair<string, Sprite[]> tPair in tActorAsset.texture_asset.dict_mains)
				{
					ActorAnimationLoader.getAnimationContainer(tPair.Key, tActorAsset, null, null);
				}
			}
		}
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x000A1110 File Offset: 0x0009F310
	private static void preloadPixelBagsUnits()
	{
		if (!Config.preload_units)
		{
			return;
		}
		foreach (Sprite pSpriteSource in ActorTextureSubAsset.all_preloaded_sprites_units)
		{
			PixelBagManager.preloadPixelBagUnit(pSpriteSource);
		}
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x000A1168 File Offset: 0x0009F368
	private static void preloadPixelBagsBuildings()
	{
		if (!Config.preload_buildings)
		{
			return;
		}
		foreach (Sprite pSpriteSource in PreloadHelpers.all_preloaded_sprites_buildings)
		{
			PixelBagManager.preloadPixelBagUnit(pSpriteSource);
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x000A11C0 File Offset: 0x0009F3C0
	private static void createPreloadReport()
	{
		string tPath = "GenAssets/wbdiag/preloaded_assets_report.txt";
		using (StringBuilderPool tResult = new StringBuilderPool())
		{
			int tTotalPreloadedGraphicalStuff = 0;
			tTotalPreloadedGraphicalStuff += ActorTextureSubAsset.all_preloaded_sprites_units.Count;
			tTotalPreloadedGraphicalStuff += PixelBagManager.total;
			tTotalPreloadedGraphicalStuff += SpriteTextureLoader.total_sprites;
			tTotalPreloadedGraphicalStuff += SpriteTextureLoader.total_sprites_list;
			tTotalPreloadedGraphicalStuff += SpriteTextureLoader.total_sprites_list_single_sprites;
			tTotalPreloadedGraphicalStuff += ActorAnimationLoader.count_units;
			tTotalPreloadedGraphicalStuff += ActorAnimationLoader.count_heads;
			tTotalPreloadedGraphicalStuff += ActorAnimationLoader.count_boats;
			tTotalPreloadedGraphicalStuff += ActorTextureSubAsset.getTotal();
			tTotalPreloadedGraphicalStuff += PreloadHelpers.total_building_sprite_containers;
			tTotalPreloadedGraphicalStuff += PreloadHelpers.total_building_sprites;
			tTotalPreloadedGraphicalStuff += HandRendererTexturePreloader.getTotal();
			int tTotalAssets = 0;
			foreach (BaseAssetLibrary tLibrary in AssetManager.getList())
			{
				tTotalAssets += tLibrary.total_items;
			}
			tResult.AppendLine("# Preloaded Assets Report");
			tResult.AppendLine();
			tResult.AppendLine("Total Preloaded Graphical Stuff: " + tTotalPreloadedGraphicalStuff.ToString());
			tResult.AppendLine("Total Preloaded Assets: " + tTotalAssets.ToString());
			tResult.AppendLine();
			tResult.AppendLine("[Sprites] Preloaded Sprites: " + ActorTextureSubAsset.all_preloaded_sprites_units.Count.ToString());
			tResult.AppendLine("[Sprites] Preloaded Building Sprites: " + PreloadHelpers.total_building_sprites.ToString());
			tResult.AppendLine("[Sprites] Hand Renderer Sprites: " + HandRendererTexturePreloader.getTotal().ToString());
			tResult.AppendLine("[Objects] Preloaded Building Sprite Containers: " + PreloadHelpers.total_building_sprite_containers.ToString());
			tResult.AppendLine("[Objects] Preloaded Pixel Bags: " + PixelBagManager.total.ToString());
			tResult.AppendLine("[Objects] Texture Sub Assets: " + ActorTextureSubAsset.getTotal().ToString());
			tResult.AppendLine();
			tResult.AppendLine("[SpriteTextureLoader] Total Single Sprites: " + SpriteTextureLoader.total_sprites.ToString());
			tResult.AppendLine("[SpriteTextureLoader] Total Sprites Lists: " + SpriteTextureLoader.total_sprites_list.ToString());
			tResult.AppendLine("[SpriteTextureLoader] Total Sprites Lists Single Sprites: " + SpriteTextureLoader.total_sprites_list_single_sprites.ToString());
			tResult.AppendLine();
			tResult.AppendLine("[ActorAnimationLoader] Total Units: " + ActorAnimationLoader.count_units.ToString());
			tResult.AppendLine("[ActorAnimationLoader] Total Heads: " + ActorAnimationLoader.count_heads.ToString());
			tResult.AppendLine("[ActorAnimationLoader] Total Boats: " + ActorAnimationLoader.count_boats.ToString());
			tResult.AppendLine();
			foreach (BaseAssetLibrary tLibrary2 in AssetManager.getList())
			{
				tResult.AppendLine(tLibrary2.id + ": " + tLibrary2.total_items.ToString());
			}
			File.WriteAllTextAsync(tPath, tResult.ToString(), default(CancellationToken));
		}
	}

	// Token: 0x04000A75 RID: 2677
	private static bool _initiated = false;

	// Token: 0x04000A76 RID: 2678
	public static int total_building_sprite_containers = 0;

	// Token: 0x04000A77 RID: 2679
	public static int total_building_sprites = 0;

	// Token: 0x04000A78 RID: 2680
	public static List<Sprite> all_preloaded_sprites_buildings = new List<Sprite>();
}
