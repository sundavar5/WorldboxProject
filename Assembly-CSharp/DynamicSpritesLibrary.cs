using System;

// Token: 0x020000F0 RID: 240
public class DynamicSpritesLibrary : AssetLibrary<DynamicSpritesAsset>
{
	// Token: 0x0600071D RID: 1821 RVA: 0x00069758 File Offset: 0x00067958
	public override void init()
	{
		base.init();
		DynamicSpritesLibrary.units = this.add(new DynamicSpritesAsset
		{
			id = "units",
			atlas_id = UnitTextureAtlasID.Units
		});
		DynamicSpritesLibrary.units_shadows = this.add(new DynamicSpritesAsset
		{
			id = "units_shadows",
			atlas_id = UnitTextureAtlasID.UnitsShadows
		});
		DynamicSpritesLibrary.boats = this.add(new DynamicSpritesAsset
		{
			id = "boats",
			atlas_id = UnitTextureAtlasID.Boats
		});
		this.add(new DynamicSpritesAsset
		{
			id = "buildings",
			export_folder_path = "buildings",
			atlas_id = UnitTextureAtlasID.Buildings,
			buildings = true
		});
		this.add(new DynamicSpritesAsset
		{
			id = "buildings_trees",
			export_folder_path = "buildings_trees",
			check_wobbly_setting = true,
			atlas_id = UnitTextureAtlasID.BuildingsTrees,
			buildings = true
		});
		this.add(new DynamicSpritesAsset
		{
			id = "buildings_wobbly",
			export_folder_path = "buildings_wobbly",
			check_wobbly_setting = true,
			atlas_id = UnitTextureAtlasID.BuildingsWobbly,
			buildings = true
		});
		this.add(new DynamicSpritesAsset
		{
			id = "buildings_trees_big",
			export_folder_path = "buildings_trees_big",
			check_wobbly_setting = true,
			atlas_id = UnitTextureAtlasID.BuildingsTreesBig,
			buildings = true
		});
		DynamicSpritesLibrary.building_lights = this.add(new DynamicSpritesAsset
		{
			id = "building_lights",
			atlas_id = UnitTextureAtlasID.BuildingsLights
		});
		DynamicSpritesLibrary.building_shadows = this.add(new DynamicSpritesAsset
		{
			id = "building_shadows",
			atlas_id = UnitTextureAtlasID.BuildingsShadows
		});
		DynamicSpritesLibrary.icons = this.add(new DynamicSpritesAsset
		{
			id = "icons",
			big_atlas = false,
			atlas_id = UnitTextureAtlasID.Icons
		});
		DynamicSpritesLibrary.items = this.add(new DynamicSpritesAsset
		{
			id = "items",
			atlas_id = UnitTextureAtlasID.Items
		});
		DynamicSpritesLibrary.zombies = this.add(new DynamicSpritesAsset
		{
			id = "zombies",
			atlas_id = UnitTextureAtlasID.Zombies
		});
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0006995A File Offset: 0x00067B5A
	public override DynamicSpritesAsset add(DynamicSpritesAsset pAsset)
	{
		base.add(pAsset);
		return pAsset;
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00069968 File Offset: 0x00067B68
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (DynamicSpritesAsset dynamicSpritesAsset in this.list)
		{
			dynamicSpritesAsset.create();
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000699C0 File Offset: 0x00067BC0
	public void clear()
	{
		foreach (DynamicSpritesAsset dynamicSpritesAsset in this.list)
		{
			dynamicSpritesAsset.clear();
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00069A10 File Offset: 0x00067C10
	public void debug(DebugTool pTool, Actor pActor)
	{
		DynamicSpriteCreator.debug_actor = pActor;
		pTool.setText("preloaded_sprites_units:", ActorTextureSubAsset.all_preloaded_sprites_units.Count, 0f, false, 0L, false, false, "");
		pTool.setText("pixel_bags:", PixelBagManager.total, 0f, false, 0L, false, false, "");
		pTool.setText("total_sprite_textures:", SpriteTextureLoader.total_sprites, 0f, false, 0L, false, false, "");
		pTool.setText("total_sprites_lists:", SpriteTextureLoader.total_sprites_list, 0f, false, 0L, false, false, "");
		pTool.setText("total_sprites_lists_single_sprites:", SpriteTextureLoader.total_sprites_list_single_sprites, 0f, false, 0L, false, false, "");
		pTool.setText("total_texture_sub_assets:", ActorTextureSubAsset.getTotal(), 0f, false, 0L, false, false, "");
		pTool.setText("hand_renderer_textures:", HandRendererTexturePreloader.getTotal(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("[ActorAnimationLoader] units:", ActorAnimationLoader.count_units, 0f, false, 0L, false, false, "");
		pTool.setText("[ActorAnimationLoader] heads:", ActorAnimationLoader.count_heads, 0f, false, 0L, false, false, "");
		pTool.setText("[ActorAnimationLoader] boats:", ActorAnimationLoader.count_boats, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		foreach (DynamicSpritesAsset tAsset in AssetManager.dynamic_sprites_library.list)
		{
			pTool.setText("sprites " + tAsset.id + ":", tAsset.countSprites(), 0f, false, 0L, false, false, "");
			pTool.setText("textures " + tAsset.id + ":", tAsset.countTextures(), 0f, false, 0L, false, false, "");
		}
		pTool.setText("units:", DynamicSpritesLibrary.units.getAtlas().debug(), 0f, false, 0L, false, false, "");
		pTool.setText("boats:", DynamicSpritesLibrary.boats.getAtlas().debug(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("_debug_id:", DynamicSpritesLibrary._debug_id, 0f, false, 0L, false, false, "");
		pTool.setText("_debug_head_id:", DynamicSpritesLibrary._debug_head_id, 0f, false, 0L, false, false, "");
		pTool.setText("_debug_main_body_sprite:", DynamicSpritesLibrary._debug_main_body_sprite, 0f, false, 0L, false, false, "");
		pTool.setText("_debug_phenotype_index:", DynamicSpritesLibrary._debug_phenotype_index, 0f, false, 0L, false, false, "");
		pTool.setText("_debug_kingdom_color_id:", DynamicSpritesLibrary._debug_kingdom_color_id, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00069D58 File Offset: 0x00067F58
	public void setDirty()
	{
		this._dirty = true;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00069D64 File Offset: 0x00067F64
	public void checkDirty()
	{
		if (!this._dirty)
		{
			return;
		}
		this._dirty = false;
		foreach (DynamicSpritesAsset dynamicSpritesAsset in AssetManager.dynamic_sprites_library.list)
		{
			dynamicSpritesAsset.checkAtlasDirty();
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00069DC8 File Offset: 0x00067FC8
	public void setDebugActor(long pID, long pKingdomColorID, long pHeadID, long pMainBodySpriteID, long pPhenotypeIndex, long pShadeID)
	{
		DynamicSpritesLibrary._debug_id = pID;
		DynamicSpritesLibrary._debug_kingdom_color_id = pKingdomColorID;
		DynamicSpritesLibrary._debug_head_id = pHeadID;
		DynamicSpritesLibrary._debug_main_body_sprite = pMainBodySpriteID;
		DynamicSpritesLibrary._debug_phenotype_index = pPhenotypeIndex;
		DynamicSpritesLibrary._debug_shade_id = pShadeID;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00069DF1 File Offset: 0x00067FF1
	public void export()
	{
	}

	// Token: 0x0400079C RID: 1948
	public static DynamicSpritesAsset units;

	// Token: 0x0400079D RID: 1949
	public static DynamicSpritesAsset boats;

	// Token: 0x0400079E RID: 1950
	public static DynamicSpritesAsset units_shadows;

	// Token: 0x0400079F RID: 1951
	public static DynamicSpritesAsset building_lights;

	// Token: 0x040007A0 RID: 1952
	public static DynamicSpritesAsset building_shadows;

	// Token: 0x040007A1 RID: 1953
	public static DynamicSpritesAsset icons;

	// Token: 0x040007A2 RID: 1954
	public static DynamicSpritesAsset items;

	// Token: 0x040007A3 RID: 1955
	public static DynamicSpritesAsset zombies;

	// Token: 0x040007A4 RID: 1956
	private bool _dirty;

	// Token: 0x040007A5 RID: 1957
	public static long _debug_id;

	// Token: 0x040007A6 RID: 1958
	private static long _debug_kingdom_color_id;

	// Token: 0x040007A7 RID: 1959
	private static long _debug_head_id;

	// Token: 0x040007A8 RID: 1960
	private static long _debug_main_body_sprite;

	// Token: 0x040007A9 RID: 1961
	private static long _debug_phenotype_index;

	// Token: 0x040007AA RID: 1962
	private static long _debug_shade_id;
}
