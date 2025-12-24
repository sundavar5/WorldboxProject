using System;
using System.Collections.Generic;
using ai.behaviours;
using life.taxi;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class QuantumSpriteLibrary : AssetLibrary<QuantumSpriteAsset>
{
	// Token: 0x06000A6D RID: 2669 RVA: 0x00096700 File Offset: 0x00094900
	public override void init()
	{
		base.init();
		this.initDebugQuantumSpriteAssets();
		QuantumSpriteAsset quantumSpriteAsset = new QuantumSpriteAsset();
		quantumSpriteAsset.id = "square_selection";
		quantumSpriteAsset.id_prefab = "p_gameSprite";
		quantumSpriteAsset.base_scale = 0.1f;
		quantumSpriteAsset.arrow_animation = true;
		quantumSpriteAsset.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawSquareSelection);
		quantumSpriteAsset.render_gameplay = true;
		quantumSpriteAsset.turn_off_renderer = true;
		quantumSpriteAsset.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("MapOverlay");
		};
		quantumSpriteAsset.add_camera_zoom_multiplier_min = 0;
		quantumSpriteAsset.add_camera_zoom_multiplier_max = 100;
		quantumSpriteAsset.color = new Color(1f, 1f, 1f, 0.95f);
		this.add(quantumSpriteAsset);
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_unit_cursor_destination",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursor),
			render_gameplay = true,
			color = new Color(1f, 1f, 1f, 0.95f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_unit_cursor_destination_selected",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorSelected),
			render_gameplay = true,
			color = new Color(0.3f, 1f, 1f, 0.7f)
		});
		QuantumSpriteAsset quantumSpriteAsset2 = new QuantumSpriteAsset();
		quantumSpriteAsset2.id = "debug_raycasts_controlled";
		quantumSpriteAsset2.id_prefab = "p_mapSprite";
		quantumSpriteAsset2.base_scale = 0.3f;
		quantumSpriteAsset2.render_gameplay = true;
		quantumSpriteAsset2.debug_option = DebugOption.ControlledUnitsAttackRaycast;
		quantumSpriteAsset2.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorSelectedRaycasts);
		quantumSpriteAsset2.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		this.add(quantumSpriteAsset2);
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_unit_cursor_lover",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorLover),
			render_gameplay = true,
			color = new Color(1f, 0.4f, 0.77f, 0.95f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_unit_cursor_family",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.05f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorFamily),
			render_gameplay = true,
			color = new Color(1f, 1f, 0.28f, 0.7f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_unit_cursor_house",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.05f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorHouse),
			render_gameplay = true,
			color = new Color(0.2f, 0.72f, 0f, 0.95f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "cursor_arrow_parents",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.05f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorParents),
			render_gameplay = true,
			color = new Color(0.5f, 0.83f, 1f, 0.95f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "cursor_arrow_kids",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.05f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorKids),
			render_gameplay = true,
			color = new Color(0.63f, 0.16f, 0.92f, 0.95f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "cursor_arrow_attack_target",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.05f,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsUnitCursorAttackTarget),
			render_gameplay = true,
			color = new Color(1f, 0f, 0f, 0.95f)
		});
		QuantumSpriteAsset quantumSpriteAsset3 = new QuantumSpriteAsset();
		quantumSpriteAsset3.id = "draw_walls";
		quantumSpriteAsset3.id_prefab = "p_mapSprite";
		quantumSpriteAsset3.add_camera_zoom_multiplier = false;
		quantumSpriteAsset3.turn_off_renderer = true;
		quantumSpriteAsset3.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWalls);
		quantumSpriteAsset3.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset3.render_gameplay = true;
		quantumSpriteAsset3.default_amount = 500;
		this.add(quantumSpriteAsset3);
		QuantumSpriteAsset quantumSpriteAsset4 = new QuantumSpriteAsset();
		quantumSpriteAsset4.id = "draw_light_walls_light_blobs";
		quantumSpriteAsset4.id_prefab = "p_mapSprite";
		quantumSpriteAsset4.add_camera_zoom_multiplier = false;
		quantumSpriteAsset4.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWallLightBlobs);
		quantumSpriteAsset4.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset4.render_gameplay = true;
		quantumSpriteAsset4.render_map = true;
		this.add(quantumSpriteAsset4);
		QuantumSpriteAsset quantumSpriteAsset5 = new QuantumSpriteAsset();
		quantumSpriteAsset5.id = "draw_lava_light_blobs";
		quantumSpriteAsset5.id_prefab = "p_mapSprite";
		quantumSpriteAsset5.add_camera_zoom_multiplier = false;
		quantumSpriteAsset5.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawLavaLightBlobs);
		quantumSpriteAsset5.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset5.render_gameplay = true;
		quantumSpriteAsset5.render_map = true;
		this.add(quantumSpriteAsset5);
		QuantumSpriteAsset quantumSpriteAsset6 = new QuantumSpriteAsset();
		quantumSpriteAsset6.id = "draw_units";
		quantumSpriteAsset6.id_prefab = "p_mapSprite";
		quantumSpriteAsset6.add_camera_zoom_multiplier = false;
		quantumSpriteAsset6.turn_off_renderer = true;
		quantumSpriteAsset6.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnits);
		quantumSpriteAsset6.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset6.render_gameplay = true;
		quantumSpriteAsset6.default_amount = 1000;
		this.add(quantumSpriteAsset6);
		QuantumSpriteAsset quantumSpriteAsset7 = new QuantumSpriteAsset();
		quantumSpriteAsset7.id = "draw_healthbars";
		quantumSpriteAsset7.id_prefab = "p_mapSprite";
		quantumSpriteAsset7.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawHealthbars);
		quantumSpriteAsset7.turn_off_renderer = true;
		quantumSpriteAsset7.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("MapOverlay");
		};
		quantumSpriteAsset7.render_gameplay = true;
		quantumSpriteAsset7.add_camera_zoom_multiplier_min = 0;
		quantumSpriteAsset7.add_camera_zoom_multiplier_max = 100;
		quantumSpriteAsset7.default_amount = 100;
		this.add(quantumSpriteAsset7);
		QuantumSpriteAsset quantumSpriteAsset8 = new QuantumSpriteAsset();
		quantumSpriteAsset8.id = "draw_units_avatars";
		quantumSpriteAsset8.id_prefab = "p_mapSprite";
		quantumSpriteAsset8.add_camera_zoom_multiplier = false;
		quantumSpriteAsset8.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitsAvatars);
		quantumSpriteAsset8.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset8.render_gameplay = true;
		quantumSpriteAsset8.render_map = true;
		this.add(quantumSpriteAsset8);
		QuantumSpriteAsset quantumSpriteAsset9 = new QuantumSpriteAsset();
		quantumSpriteAsset9.id = "unit_items";
		quantumSpriteAsset9.id_prefab = "p_mapSprite";
		quantumSpriteAsset9.base_scale = 0.15f;
		quantumSpriteAsset9.add_camera_zoom_multiplier = false;
		quantumSpriteAsset9.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitItems);
		quantumSpriteAsset9.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_world_object);
		};
		quantumSpriteAsset9.render_gameplay = true;
		quantumSpriteAsset9.default_amount = 200;
		this.add(quantumSpriteAsset9);
		QuantumSpriteAsset quantumSpriteAsset10 = new QuantumSpriteAsset();
		quantumSpriteAsset10.id = "draw_unit_hit_effect";
		quantumSpriteAsset10.id_prefab = "p_mapSprite";
		quantumSpriteAsset10.add_camera_zoom_multiplier = false;
		quantumSpriteAsset10.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitsEffectDamage);
		quantumSpriteAsset10.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_damaged);
		};
		quantumSpriteAsset10.render_gameplay = true;
		quantumSpriteAsset10.render_map = true;
		this.add(quantumSpriteAsset10);
		QuantumSpriteAsset quantumSpriteAsset11 = new QuantumSpriteAsset();
		quantumSpriteAsset11.id = "draw_parabolic_unload";
		quantumSpriteAsset11.id_prefab = "p_mapSprite";
		quantumSpriteAsset11.add_camera_zoom_multiplier = false;
		quantumSpriteAsset11.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawParabolicUnload);
		quantumSpriteAsset11.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset11.render_gameplay = true;
		quantumSpriteAsset11.render_map = true;
		this.add(quantumSpriteAsset11);
		QuantumSpriteAsset quantumSpriteAsset12 = new QuantumSpriteAsset();
		quantumSpriteAsset12.id = "draw_unit_highlight_effect";
		quantumSpriteAsset12.id_prefab = "p_mapSprite";
		quantumSpriteAsset12.add_camera_zoom_multiplier = false;
		quantumSpriteAsset12.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitsEffectHighlight);
		quantumSpriteAsset12.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_highlighted);
		};
		quantumSpriteAsset12.render_gameplay = true;
		quantumSpriteAsset12.render_map = true;
		this.add(quantumSpriteAsset12);
		QuantumSpriteAsset quantumSpriteAsset13 = new QuantumSpriteAsset();
		quantumSpriteAsset13.id = "draw_buildings";
		quantumSpriteAsset13.id_prefab = "p_mapSprite";
		quantumSpriteAsset13.add_camera_zoom_multiplier = false;
		quantumSpriteAsset13.turn_off_renderer = true;
		quantumSpriteAsset13.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawBuildings);
		quantumSpriteAsset13.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset13.render_gameplay = true;
		quantumSpriteAsset13.default_amount = 2000;
		this.add(quantumSpriteAsset13);
		QuantumSpriteAsset quantumSpriteAsset14 = new QuantumSpriteAsset();
		quantumSpriteAsset14.id = "draw_building_stockpiles";
		quantumSpriteAsset14.id_prefab = "p_mapSprite";
		quantumSpriteAsset14.add_camera_zoom_multiplier = false;
		quantumSpriteAsset14.turn_off_renderer = true;
		quantumSpriteAsset14.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawStockpileResources);
		quantumSpriteAsset14.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset14.render_gameplay = true;
		quantumSpriteAsset14.default_amount = 100;
		this.add(quantumSpriteAsset14);
		QuantumSpriteAsset quantumSpriteAsset15 = new QuantumSpriteAsset();
		quantumSpriteAsset15.id = "projectiles";
		quantumSpriteAsset15.id_prefab = "p_mapSprite";
		quantumSpriteAsset15.render_gameplay = true;
		quantumSpriteAsset15.turn_off_renderer = true;
		quantumSpriteAsset15.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset15.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawProjectiles);
		quantumSpriteAsset15.default_amount = 100;
		this.add(quantumSpriteAsset15);
		this.add(new QuantumSpriteAsset
		{
			id = "projectile_shadows",
			id_prefab = "p_shadow",
			turn_off_renderer = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawProjectileShadows),
			default_amount = 100
		});
		this.add(new QuantumSpriteAsset
		{
			id = "throwing_items_shadows",
			id_prefab = "p_shadow",
			turn_off_renderer = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawThrowingItemsShadows),
			default_amount = 100
		});
		this.add(new QuantumSpriteAsset
		{
			id = "shadows_buildings",
			id_prefab = "p_shadow",
			turn_off_renderer = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawShadowsBuildings),
			default_amount = 500
		});
		this.add(new QuantumSpriteAsset
		{
			id = "shadows_unit",
			id_prefab = "p_shadow",
			turn_off_renderer = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawShadowsUnit)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "unit_banners",
			id_prefab = "p_unitBanner",
			turn_off_renderer = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitBanners)
		});
		QuantumSpriteAsset quantumSpriteAsset16 = new QuantumSpriteAsset();
		quantumSpriteAsset16.id = "selected_units";
		quantumSpriteAsset16.id_prefab = "p_gameSprite";
		quantumSpriteAsset16.render_gameplay = true;
		quantumSpriteAsset16.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
		};
		quantumSpriteAsset16.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawSelectedUnits);
		this.add(quantumSpriteAsset16);
		QuantumSpriteAsset quantumSpriteAsset17 = new QuantumSpriteAsset();
		quantumSpriteAsset17.id = "square_selection_to_select";
		quantumSpriteAsset17.id_prefab = "p_gameSprite";
		quantumSpriteAsset17.render_gameplay = true;
		quantumSpriteAsset17.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
		};
		quantumSpriteAsset17.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitsToBeSelectedBySquareTool);
		this.add(quantumSpriteAsset17);
		QuantumSpriteAsset quantumSpriteAsset18 = new QuantumSpriteAsset();
		quantumSpriteAsset18.id = "favorites_game";
		quantumSpriteAsset18.id_prefab = "p_gameSprite";
		quantumSpriteAsset18.render_gameplay = true;
		quantumSpriteAsset18.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconFavoriteStar"));
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.sprite_renderer.sortingOrder = 1;
		};
		quantumSpriteAsset18.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawFavoritesGame);
		this.add(quantumSpriteAsset18);
		QuantumSpriteAsset quantumSpriteAsset19 = new QuantumSpriteAsset();
		quantumSpriteAsset19.id = "favorites_items";
		quantumSpriteAsset19.id_prefab = "p_gameSprite";
		quantumSpriteAsset19.base_scale = 0.3f;
		quantumSpriteAsset19.render_map = true;
		quantumSpriteAsset19.selected_city_scale = true;
		quantumSpriteAsset19.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconFavoriteWeapon"));
		};
		quantumSpriteAsset19.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawFavoriteItemsMap);
		this.add(quantumSpriteAsset19);
		QuantumSpriteAsset quantumSpriteAsset20 = new QuantumSpriteAsset();
		quantumSpriteAsset20.id = "unit_metas";
		quantumSpriteAsset20.id_prefab = "p_gameSprite";
		quantumSpriteAsset20.turn_off_renderer = true;
		quantumSpriteAsset20.base_scale = 0.3f;
		quantumSpriteAsset20.render_map = false;
		quantumSpriteAsset20.render_gameplay = true;
		quantumSpriteAsset20.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSprite(SpriteTextureLoader.getSprite("effects/unit_meta"));
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset20.draw_call = new QuantumSpriteUpdater(this.drawUnitMetas);
		this.add(quantumSpriteAsset20);
		QuantumSpriteAsset quantumSpriteAsset21 = new QuantumSpriteAsset();
		quantumSpriteAsset21.id = "happiness_icons";
		quantumSpriteAsset21.id_prefab = "p_gameSprite";
		quantumSpriteAsset21.turn_off_renderer = true;
		quantumSpriteAsset21.base_scale = 0.03f;
		quantumSpriteAsset21.render_gameplay = true;
		quantumSpriteAsset21.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset21.draw_call = new QuantumSpriteUpdater(this.drawUnitHappinessIcons);
		this.add(quantumSpriteAsset21);
		QuantumSpriteAsset quantumSpriteAsset22 = new QuantumSpriteAsset();
		quantumSpriteAsset22.id = "task_icons";
		quantumSpriteAsset22.id_prefab = "p_gameSprite";
		quantumSpriteAsset22.turn_off_renderer = true;
		quantumSpriteAsset22.base_scale = 0.04f;
		quantumSpriteAsset22.render_gameplay = true;
		quantumSpriteAsset22.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
		};
		quantumSpriteAsset22.draw_call = new QuantumSpriteUpdater(this.drawUnitTaskIcons);
		this.add(quantumSpriteAsset22);
		QuantumSpriteAsset quantumSpriteAsset23 = new QuantumSpriteAsset();
		quantumSpriteAsset23.id = "family_species_icons";
		quantumSpriteAsset23.id_prefab = "p_mapSprite";
		quantumSpriteAsset23.base_scale = 0.3f;
		quantumSpriteAsset23.add_camera_zoom_multiplier = false;
		quantumSpriteAsset23.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawFamilySpeciesIcons);
		quantumSpriteAsset23.color = new Color(1f, 1f, 1f, 0.8f);
		quantumSpriteAsset23.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 1;
		};
		quantumSpriteAsset23.render_map = true;
		this.add(quantumSpriteAsset23);
		QuantumSpriteAsset quantumSpriteAsset24 = new QuantumSpriteAsset();
		quantumSpriteAsset24.id = "favorites_map";
		quantumSpriteAsset24.id_prefab = "p_gameSprite";
		quantumSpriteAsset24.base_scale = 0.3f;
		quantumSpriteAsset24.render_map = true;
		quantumSpriteAsset24.selected_city_scale = true;
		quantumSpriteAsset24.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconFavoriteStar_Map"));
		};
		quantumSpriteAsset24.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawFavoritesMap);
		this.add(quantumSpriteAsset24);
		this.add(new QuantumSpriteAsset
		{
			id = "status_effects",
			id_prefab = "p_gameSprite",
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawStatusEffects),
			default_amount = 10
		});
		this.add(new QuantumSpriteAsset
		{
			id = "wars_lines",
			id_prefab = "p_mapArrow_arrows",
			line_width = 5,
			line_height = 7,
			arrow_animation = true,
			render_map = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWars)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "wars_icons",
			id_prefab = "p_mapSprite",
			render_map = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWarsIcons)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "plots",
			id_prefab = "p_plot",
			base_scale = 0.3f,
			render_map = true,
			render_gameplay = true,
			selected_city_scale = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawPlots),
			default_amount = 10
		});
		this.add(new QuantumSpriteAsset
		{
			id = "plot_removals",
			id_prefab = "p_plot",
			base_scale = 0.3f,
			render_map = true,
			render_gameplay = true,
			selected_city_scale = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawPlotRemovals),
			default_amount = 10
		});
		QuantumSpriteAsset quantumSpriteAsset25 = new QuantumSpriteAsset();
		quantumSpriteAsset25.id = "kings";
		quantumSpriteAsset25.id_prefab = "p_mapSprite";
		quantumSpriteAsset25.base_scale = 0.3f;
		quantumSpriteAsset25.render_map = true;
		quantumSpriteAsset25.selected_city_scale = true;
		quantumSpriteAsset25.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawKings);
		quantumSpriteAsset25.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset25.default_amount = 10;
		this.add(quantumSpriteAsset25);
		QuantumSpriteAsset quantumSpriteAsset26 = new QuantumSpriteAsset();
		quantumSpriteAsset26.id = "leaders";
		quantumSpriteAsset26.id_prefab = "p_mapSprite";
		quantumSpriteAsset26.render_map = true;
		quantumSpriteAsset26.selected_city_scale = true;
		quantumSpriteAsset26.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawLeaders);
		quantumSpriteAsset26.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset26.default_amount = 10;
		this.add(quantumSpriteAsset26);
		QuantumSpriteAsset quantumSpriteAsset27 = new QuantumSpriteAsset();
		quantumSpriteAsset27.id = "armies";
		quantumSpriteAsset27.id_prefab = "p_mapArmy";
		quantumSpriteAsset27.base_scale = 0.3f;
		quantumSpriteAsset27.render_map = true;
		quantumSpriteAsset27.selected_city_scale = true;
		quantumSpriteAsset27.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			((QuantumSpriteWithText)pQSprite).initText();
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset27.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArmies);
		quantumSpriteAsset27.default_amount = 10;
		this.add(quantumSpriteAsset27);
		this.add(new QuantumSpriteAsset
		{
			id = "magnet_units",
			id_prefab = "p_mapSprite",
			render_map = true,
			render_gameplay = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawMagnetUnits),
			default_amount = 10
		});
		QuantumSpriteAsset quantumSpriteAsset28 = new QuantumSpriteAsset();
		quantumSpriteAsset28.id = "boats_big";
		quantumSpriteAsset28.id_prefab = "p_mapSprite";
		quantumSpriteAsset28.base_scale = 0.3f;
		quantumSpriteAsset28.render_map = true;
		quantumSpriteAsset28.selected_city_scale = true;
		quantumSpriteAsset28.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawBoatIcons);
		quantumSpriteAsset28.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset28.default_amount = 10;
		this.add(quantumSpriteAsset28);
		QuantumSpriteAsset quantumSpriteAsset29 = new QuantumSpriteAsset();
		quantumSpriteAsset29.id = "boats_small";
		quantumSpriteAsset29.id_prefab = "p_mapSprite";
		quantumSpriteAsset29.render_map = true;
		quantumSpriteAsset29.selected_city_scale = true;
		quantumSpriteAsset29.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawBoatIcons);
		quantumSpriteAsset29.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset29.default_amount = 10;
		this.add(quantumSpriteAsset29);
		QuantumSpriteAsset quantumSpriteAsset30 = new QuantumSpriteAsset();
		quantumSpriteAsset30.id = "battles";
		quantumSpriteAsset30.id_prefab = "p_mapBattle";
		quantumSpriteAsset30.base_scale = 0.6f;
		quantumSpriteAsset30.flag_battle = true;
		quantumSpriteAsset30.path_icon = "civ/map_mark_battle_animation";
		quantumSpriteAsset30.render_map = true;
		quantumSpriteAsset30.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawBattles);
		quantumSpriteAsset30.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_minis);
		};
		quantumSpriteAsset30.default_amount = 10;
		this.add(quantumSpriteAsset30);
		this.add(new QuantumSpriteAsset
		{
			id = "arrows_army_targets",
			id_prefab = "p_mapArrow_stroke",
			render_map = true,
			arrow_animation = true,
			base_scale = 0.3f,
			selected_city_scale = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawArrowsArmyAttackTargets)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "highlight_cursor_zones",
			id_prefab = "p_mapZone",
			base_scale = 1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawCursorZones),
			render_map = true,
			add_camera_zoom_multiplier = false,
			color = new Color(1f, 1f, 1f, 0.2f),
			color_2 = new Color(1f, 0.1f, 0.1f, 0.2f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "selected_kingdom",
			id_prefab = "p_mapZone",
			base_scale = 1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawSelectedKingdomZones),
			render_map = true,
			add_camera_zoom_multiplier = false,
			color = new Color(1f, 1f, 1f, 0.4f),
			color_2 = new Color(1f, 0.1f, 0.1f, 0.2f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "whisper_of_war",
			id_prefab = "p_mapZone",
			base_scale = 1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWhisperOfWar),
			render_map = true,
			add_camera_zoom_multiplier = false,
			color = new Color(1f, 1f, 1f, 0.4f),
			color_2 = new Color(1f, 0.1f, 0.1f, 0.2f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "whisper_of_war_line",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.5f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawWhisperOfWarLine),
			render_map = true,
			render_gameplay = true,
			color = new Color(0.4f, 0.4f, 1f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "unity_line",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.5f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnityLine),
			render_map = true,
			render_gameplay = true,
			color = new Color(0.4f, 0.4f, 1f, 0.9f)
		});
		QuantumSpriteAsset quantumSpriteAsset31 = new QuantumSpriteAsset();
		quantumSpriteAsset31.id = "capturing_zones";
		quantumSpriteAsset31.id_prefab = "p_mapZone_lines";
		quantumSpriteAsset31.base_scale = 1f;
		quantumSpriteAsset31.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawCapturingZones);
		quantumSpriteAsset31.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 0;
		};
		quantumSpriteAsset31.render_map = true;
		quantumSpriteAsset31.add_camera_zoom_multiplier = false;
		this.add(quantumSpriteAsset31);
		this.add(new QuantumSpriteAsset
		{
			id = "ate_item",
			id_prefab = "p_mapSprite",
			base_scale = 0.15f,
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawJustAte),
			render_gameplay = true
		});
		QuantumSpriteAsset quantumSpriteAsset32 = new QuantumSpriteAsset();
		quantumSpriteAsset32.id = "socialize";
		quantumSpriteAsset32.id_prefab = "p_mapSprite";
		quantumSpriteAsset32.base_scale = 0.15f;
		quantumSpriteAsset32.add_camera_zoom_multiplier = false;
		quantumSpriteAsset32.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawSocialize);
		quantumSpriteAsset32.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSharedMat(LibraryMaterials.instance.mat_socialize);
		};
		quantumSpriteAsset32.render_gameplay = true;
		this.add(quantumSpriteAsset32);
		this.add(new QuantumSpriteAsset
		{
			id = "cursor_power",
			id_prefab = "p_mapSprite",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawCursorSprite),
			add_camera_zoom_multiplier_min = 0,
			add_camera_zoom_multiplier_max = 100,
			render_gameplay = true,
			render_map = true
		});
		this.add(new QuantumSpriteAsset
		{
			id = "controlled_unit_attack_recharge",
			id_prefab = "p_attack_recharge",
			base_scale = 0.03f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawCursorAttackRecharge),
			add_camera_zoom_multiplier_min = 0,
			add_camera_zoom_multiplier_max = 100,
			render_gameplay = true,
			render_map = true
		});
		this.add(new QuantumSpriteAsset
		{
			id = "cursor_target_subspecies",
			id_prefab = "p_mapArrow_dna",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawCursorTargetSubspecies),
			arrow_animation = false,
			add_camera_zoom_multiplier_min = 0,
			add_camera_zoom_multiplier_max = 100,
			line_height = 6,
			line_width = 45,
			render_gameplay = true,
			render_map = true
		});
		this.add(new QuantumSpriteAsset
		{
			id = "buildings_light_windows",
			id_prefab = "p_windowLight",
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawBuildingsLightWindows),
			render_gameplay = true,
			render_map = true
		});
		QuantumSpriteLibrary.light_areas = this.add(new QuantumSpriteAsset
		{
			id = "light_areas",
			id_prefab = "p_lightArea",
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawLightAreas),
			render_gameplay = true,
			render_map = true
		});
		QuantumSpriteAsset quantumSpriteAsset33 = new QuantumSpriteAsset();
		quantumSpriteAsset33.id = "fire_sprites";
		quantumSpriteAsset33.id_prefab = "p_mapSprite";
		quantumSpriteAsset33.base_scale = 0.15f;
		quantumSpriteAsset33.add_camera_zoom_multiplier = false;
		quantumSpriteAsset33.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawFires);
		quantumSpriteAsset33.sound_idle = "event:/SFX/STATUS/StatusBurningBuilding";
		quantumSpriteAsset33.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.setScale(pAsset.base_scale);
			pQSprite.sprite_renderer.sprite = QuantumSpriteLibrary._fire_sprites_1.GetRandom<Sprite>();
		};
		quantumSpriteAsset33.render_gameplay = true;
		this.add(quantumSpriteAsset33);
		QuantumSpriteAsset quantumSpriteAsset34 = new QuantumSpriteAsset();
		quantumSpriteAsset34.id = "unexplored_augmentations";
		quantumSpriteAsset34.id_prefab = "p_gameSprite";
		quantumSpriteAsset34.base_scale = 0.1f;
		quantumSpriteAsset34.add_camera_zoom_multiplier = false;
		quantumSpriteAsset34.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("Objects");
			pQSprite.sprite_renderer.sortingOrder = 1;
		};
		quantumSpriteAsset34.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnexploredAugmentationSprite);
		quantumSpriteAsset34.color = new Color(1f, 1f, 1f, 0.8f);
		quantumSpriteAsset34.render_gameplay = true;
		this.add(quantumSpriteAsset34);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0009823C File Offset: 0x0009643C
	private void drawUnitHappinessIcons(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("icons_happiness"))
		{
			return;
		}
		float tOffset = 18f;
		if (PlayerConfig.optionBoolEnabled("icons_tasks"))
		{
			tOffset += 11f;
		}
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.hasEmotions() && !tActor.isInsideSomething())
			{
				float tScale = tActor.current_scale.y * 0.5f;
				Vector3 tVisualPos = tActor.current_position;
				tVisualPos.z = tOffset;
				tVisualPos.y += tOffset * tActor.current_scale.y;
				Sprite tHappinessSprite = HappinessHelper.getSpriteBasedOnHappinessValue(tActor.getHappiness());
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVisualPos, null, null, null, null, 1f, false, tScale).setSprite(tHappinessSprite);
			}
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00098314 File Offset: 0x00096514
	private void drawUnitTaskIcons(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("icons_tasks"))
		{
			return;
		}
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		float tOffset = 17.5f;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tActor.isInsideSomething() && tActor.asset.show_task_icon && tActor.ai != null)
			{
				BehaviourTaskActor tTask = tActor.ai.task;
				if (tTask != null && tTask.show_icon)
				{
					float tScale = tActor.current_scale.y * 0.5f;
					Vector3 tVisualPos = tActor.current_position;
					tVisualPos.z = tOffset;
					tVisualPos.y += tOffset * tActor.current_scale.y;
					QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVisualPos, null, null, null, null, 1f, false, tScale).setSprite(tTask.getSprite());
				}
			}
		}
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x000983FC File Offset: 0x000965FC
	private void drawUnitMetas(QuantumSpriteAsset pAsset)
	{
		bool tShowMetas = PlayerConfig.optionBoolEnabled("unit_metas");
		bool tIsNanoSet = SelectedObjects.isNanoObjectSet();
		if (tIsNanoSet)
		{
			tShowMetas = true;
		}
		if (!tShowMetas)
		{
			this._last_meta_type_metas = MetaType.None;
			return;
		}
		this._metas_fall_offset_timer += World.world.delta_time * 1f;
		if (this._metas_fall_offset_timer > 1f)
		{
			this._metas_fall_offset_timer = 1f;
		}
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		MetaType tMetaType = Zones.getCurrentMapBorderMode(false);
		if (tMetaType.isNone())
		{
			return;
		}
		bool tOnlyFavoritedMeta = PlayerConfig.optionBoolEnabled("only_favorited_meta");
		NanoObject tSelectedNano = SelectedObjects.getSelectedNanoObject();
		if (tIsNanoSet)
		{
			tMetaType = tSelectedNano.getMetaType();
		}
		if (this._last_meta_type_metas != tMetaType)
		{
			this._metas_fall_offset_timer = 0f;
		}
		this._last_meta_type_metas = tMetaType;
		float tPositionFallingDown = (1f - iTween.easeOutBounce(0f, 1f, this._metas_fall_offset_timer)) * 5f;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			IMetaObject tMetaObject = tActor.getMetaObjectOfType(tMetaType) as IMetaObject;
			if (tMetaObject != null && (!tIsNanoSet || tSelectedNano == tMetaObject) && (!tOnlyFavoritedMeta || tMetaObject.isFavorite()))
			{
				ColorAsset tColorAsset = tMetaObject.getColor();
				if (tColorAsset == null)
				{
					Debug.LogError("[drawUnitMetas] Forgot to set color asset for ? " + tMetaType.ToString());
				}
				else
				{
					ref Color tColor = ref tColorAsset.getColorTextRef();
					QuantumSprite next = pAsset.group_system.getNext();
					Vector3 tScale = tActor.current_scale;
					Vector3 tVisualPos = tActor.current_position;
					tVisualPos.y += tPositionFallingDown;
					tVisualPos.z = -0.02f;
					next.setPosOnly(ref tVisualPos);
					next.setScale(ref tScale);
					next.setColor(ref tColor);
				}
			}
		}
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x000985B1 File Offset: 0x000967B1
	private static void showLightAt(Vector2 pPos, Color pColor, float pScale = 1f)
	{
		QuantumSprite next = QuantumSpriteLibrary.light_areas.group_system.getNext();
		next.set(ref pPos, pScale);
		next.setColor(ref QuantumSpriteLibrary.light_areas.color);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x000985DC File Offset: 0x000967DC
	private static Color getColorForLight()
	{
		Color tColorLight = Color.white;
		if (MapBox.isRenderMiniMap())
		{
			tColorLight.a = World.world_era.era_effect_light_alpha_minimap;
			if (!World.world.zone_calculator.isModeNone())
			{
				tColorLight.a = 0.4f;
			}
		}
		else
		{
			tColorLight.a = World.world_era.era_effect_light_alpha_game;
		}
		return tColorLight;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00098638 File Offset: 0x00096838
	private static void drawLightAreas(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("night_lights"))
		{
			return;
		}
		if (!World.world.era_manager.shouldShowLights())
		{
			return;
		}
		Color tColorLightImportant = Color.white;
		Color tColorLight = QuantumSpriteLibrary.getColorForLight();
		if (World.world.heat_ray_fx.isReady())
		{
			QuantumSpriteLibrary.showLightAt(World.world.heat_ray_fx.getPosForLight(), tColorLightImportant, 1.5f);
		}
		List<LightBlobData> tListBlobs = World.world.stack_effects.light_blobs;
		if (tListBlobs.Count > 0)
		{
			for (int i = 0; i < tListBlobs.Count; i++)
			{
				LightBlobData tData = tListBlobs[i];
				QuantumSpriteLibrary.showLightAt(tData.position, tColorLightImportant, tData.radius);
			}
		}
		if (MapBox.isRenderGameplay())
		{
			Actor[] tArr = QuantumSpriteLibrary.visible_units;
			int tLen = QuantumSpriteLibrary.visible_units_count;
			for (int j = 0; j < tLen; j++)
			{
				QuantumSpriteLibrary.checkUnitLight(tArr[j], tColorLight);
			}
		}
		else
		{
			List<Actor> tList = World.world.units.getSimpleList();
			for (int k = 0; k < tList.Count; k++)
			{
				QuantumSpriteLibrary.checkUnitLight(tList[k], tColorLight);
			}
		}
		if (World.world.quality_changer.shouldRenderBuildings())
		{
			int tLen2 = World.world.buildings.countVisibleBuildings();
			Building[] tBuildings = World.world.buildings.getVisibleBuildings();
			for (int l = 0; l < tLen2; l++)
			{
				QuantumSpriteLibrary.checkBuildingLights(tBuildings[l], tColorLight);
			}
		}
		else
		{
			List<Building> tBuildingList = World.world.buildings.getSimpleList();
			for (int m = 0; m < tBuildingList.Count; m++)
			{
				QuantumSpriteLibrary.checkBuildingLights(tBuildingList[m], tColorLight);
			}
		}
		if (MapBox.isRenderGameplay())
		{
			if (WorldBehaviourActionFire.hasFires())
			{
				List<TileZone> tListZones = World.world.zone_camera.getVisibleZones();
				for (int iZone = 0; iZone < tListZones.Count; iZone++)
				{
					TileZone tZone = tListZones[iZone];
					if (WorldBehaviourActionFire.hasFires(tZone))
					{
						WorldTile[] tTiles = tZone.tiles;
						int tCount = tTiles.Length;
						for (int n = 0; n < tCount; n++)
						{
							WorldTile tTile = tTiles[n];
							if (tTile.isOnFire())
							{
								QuantumSpriteLibrary.showLightAt(tTile.pos, tColorLight, 0.2f);
							}
						}
					}
				}
			}
		}
		else if (WorldBehaviourActionFire.hasFires())
		{
			foreach (TileZone tZone2 in World.world.zone_calculator.zones)
			{
				if (WorldBehaviourActionFire.hasFires(tZone2))
				{
					WorldTile[] tTiles2 = tZone2.tiles;
					int tCount2 = tTiles2.Length;
					for (int i2 = 0; i2 < tCount2; i2++)
					{
						WorldTile tTile2 = tTiles2[i2];
						if (tTile2.isOnFire())
						{
							QuantumSpriteLibrary.showLightAt(tTile2.pos, tColorLight, 0.2f);
						}
					}
				}
			}
		}
		if ((Config.isComputer || Config.isEditor) && PlayerConfig.optionBoolEnabled("cursor_lights"))
		{
			QuantumSpriteLibrary.showLightAt(World.world.getMousePos(), tColorLightImportant, 0.4f);
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0009894C File Offset: 0x00096B4C
	private static void checkBuildingLights(Building pBuilding, Color pColor)
	{
		if (pBuilding.hasAnyStatusEffect())
		{
			foreach (Status tEffect in pBuilding.getStatuses())
			{
				if (tEffect.asset.draw_light_area)
				{
					QuantumSpriteLibrary.showLightAt(pBuilding.current_position, pColor, tEffect.asset.draw_light_size);
				}
			}
		}
		if (!pBuilding.asset.draw_light_area)
		{
			return;
		}
		if (!pBuilding.isUsable())
		{
			return;
		}
		if (pBuilding.isAbandoned())
		{
			return;
		}
		if (pBuilding.asset.hasHousingSlots() && !pBuilding.hasResidents())
		{
			return;
		}
		Vector3 tPos = pBuilding.current_position;
		tPos.x += pBuilding.asset.draw_light_area_offset_x;
		tPos.y += pBuilding.asset.draw_light_area_offset_y;
		QuantumSpriteLibrary.showLightAt(tPos, pColor, pBuilding.asset.draw_light_size);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x00098A4C File Offset: 0x00096C4C
	private static void checkUnitLight(Actor pActor, Color pColor)
	{
		if (pActor.a.has_tag_generate_light)
		{
			Vector2 tPos = pActor.current_position;
			tPos.y += pActor.getHeight();
			QuantumSpriteLibrary.showLightAt(tPos, pColor, 0.3f);
			return;
		}
		if (pActor.hasAnyStatusEffect())
		{
			foreach (Status tEffect in pActor.getStatuses())
			{
				if (tEffect.asset.draw_light_area)
				{
					QuantumSpriteLibrary.showLightAt(pActor.current_position, pColor, tEffect.asset.draw_light_size);
				}
			}
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00098AF8 File Offset: 0x00096CF8
	private static void drawBuildingsLightWindows(QuantumSpriteAsset pAsset)
	{
		if (!World.world.quality_changer.shouldRenderBuildings())
		{
			return;
		}
		if (!World.world.era_manager.shouldShowLights())
		{
			return;
		}
		if (!PlayerConfig.optionBoolEnabled("night_lights"))
		{
			return;
		}
		Color tColorLight = Color.white;
		if (Randy.randomBool())
		{
			tColorLight.a = 0.95f;
		}
		else
		{
			tColorLight.a = 1f;
		}
		int tLen = World.world.buildings.countVisibleBuildings();
		Building[] tBuildings = World.world.buildings.getVisibleBuildings();
		for (int i = 0; i < tLen; i++)
		{
			Building tBuilding = tBuildings[i];
			if (tBuilding.asset.city_building && tBuilding.isUsable() && !tBuilding.isAbandoned() && (!tBuilding.asset.hasHousingSlots() || tBuilding.hasResidents()))
			{
				Sprite tSprite = DynamicSprites.getBuildingLight(tBuilding);
				if (!(tSprite == null))
				{
					Vector3 tPos = tBuilding.cur_transform_position;
					tPos.z = -0.19f;
					QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tBuilding.getCurrentScale().y).setSprite(tSprite);
				}
			}
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00098C18 File Offset: 0x00096E18
	private static void drawFamilySpeciesIcons(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("map_species_families"))
		{
			return;
		}
		foreach (Family tFamily in World.world.families)
		{
			if (tFamily.isAlive())
			{
				ActorAsset tAsset = tFamily.getActorAsset();
				if (tFamily.units.Count != 0)
				{
					Actor tActor = tFamily.units[0];
					if (!tActor.isRekt() && tActor.current_zone.visible)
					{
						Sprite tSprite = tAsset.getSpriteIcon();
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV3, null, null, null, null, 1f, false, -1f).setSprite(tSprite);
					}
				}
			}
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00098CEC File Offset: 0x00096EEC
	private static void drawCursorTargetSubspecies(QuantumSpriteAsset pAsset)
	{
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.selected_buttons.selectedButton == null)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		if (MoveCamera.inSpectatorMode())
		{
			return;
		}
		if (Input.GetMouseButton(0))
		{
			return;
		}
		if (Input.GetMouseButton(1))
		{
			return;
		}
		if (Input.GetMouseButton(2))
		{
			return;
		}
		WorldTile tTile = World.world.getMouseTilePosCachedFrame();
		if (tTile == null)
		{
			return;
		}
		GodPower tPower = World.world.getSelectedPowerAsset();
		if (tPower.type != PowerActionType.PowerSpawnActor)
		{
			return;
		}
		ActorAsset tActorAsset = tPower.getActorAsset();
		if (tActorAsset == null)
		{
			return;
		}
		if (!tActorAsset.can_have_subspecies)
		{
			return;
		}
		Actor tClosestActor;
		if (World.world.subspecies.getNearbySpecies(tActorAsset, tTile, out tClosestActor, false, false) == null)
		{
			return;
		}
		if (!tClosestActor.is_visible)
		{
			return;
		}
		Vector3 tPos = tClosestActor.getHeadOffsetPositionForFunRendering();
		QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, World.world.getMousePos(), tPos, ref Toolbox.color_white, null);
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00098DD8 File Offset: 0x00096FD8
	private static void drawCursorSprite(QuantumSpriteAsset pAsset)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.selected_buttons.selectedButton == null)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		if (MoveCamera.inSpectatorMode())
		{
			return;
		}
		if (Input.GetMouseButton(0))
		{
			return;
		}
		if (Input.GetMouseButton(1))
		{
			return;
		}
		if (Input.GetMouseButton(2))
		{
			return;
		}
		if (World.world.getSelectedPowerAsset().ignore_cursor_icon)
		{
			return;
		}
		float tCameraScaleZoomMultiplier = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
		Vector2 tPos = World.world.getMousePos();
		tPos.x += -0.3f * tCameraScaleZoomMultiplier;
		tPos.y += -0.3f * tCameraScaleZoomMultiplier;
		QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
		quantumSprite.setSprite(World.world.selected_buttons.selectedButton.icon.sprite);
		tPos.x += 0.3f * tCameraScaleZoomMultiplier;
		tPos.y += 0.3f * tCameraScaleZoomMultiplier;
		Color tColor = Toolbox.color_black;
		quantumSprite.setSprite(World.world.selected_buttons.selectedButton.icon.sprite);
		tColor.a = 0.3f;
		quantumSprite.setColor(ref tColor);
		quantumSprite.sprite_renderer.sortingOrder = 9;
		QuantumSprite quantumSprite2 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
		quantumSprite2.setSprite(World.world.selected_buttons.selectedButton.icon.sprite);
		quantumSprite2.sprite_renderer.sortingOrder = 10;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00098F70 File Offset: 0x00097170
	private static void drawCursorAttackRecharge(QuantumSpriteAsset pAsset)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (!ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = ControllableUnit.getControllableUnit();
		if (tActor.asset.id == "crabzilla")
		{
			return;
		}
		if (tActor.isAttackReady())
		{
			return;
		}
		float tRatio = tActor.getAttackCooldownRatio();
		float tCameraScaleZoomMultiplier = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
		Vector2 tPos = World.world.getMousePos();
		tPos.x += 2.5f * tCameraScaleZoomMultiplier;
		tPos.y -= 2.5f * tCameraScaleZoomMultiplier;
		CircleIconShaderMod component = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f).GetComponent<CircleIconShaderMod>();
		component.sprite_renderer_with_mat.sprite = QuantumSpriteLibrary._sprite_attack_reload;
		component.setShaderVal(tRatio);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00099034 File Offset: 0x00097234
	private static void drawUnexploredAugmentationSprite(QuantumSpriteAsset pQAsset)
	{
		if (!PowerLibrary.inspect_unit.isSelected())
		{
			return;
		}
		if (WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			return;
		}
		Sprite tSprite = AnimationHelper.getSpriteFromListSessionTime(0, QuantumSpriteLibrary._unexplored_sprites, SimGlobals.m.unexplored_sprite_animation_speed);
		for (int i = 0; i < QuantumSpriteLibrary.visible_units_alive_count; i++)
		{
			Actor tActor = QuantumSpriteLibrary.visible_units_alive[i];
			if (QuantumSpriteLibrary.checkShouldDrawUnexploredSpriteFor(tActor))
			{
				Vector3 tPos = tActor.getHeadOffsetPositionForFunRendering();
				QuantumSpriteLibrary.drawQuantumSprite(pQAsset, tPos, null, null, null, null, 1f, false, tActor.current_scale.y).setSprite(tSprite);
			}
		}
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x000990BC File Offset: 0x000972BC
	private static bool checkShouldDrawUnexploredSpriteFor(Actor pActor)
	{
		if (pActor.asset.is_boat)
		{
			return false;
		}
		ActorAsset tActorAsset = pActor.asset;
		if (!tActorAsset.isAvailable() && tActorAsset.needs_to_be_explored)
		{
			return true;
		}
		if (pActor.hasEquipment())
		{
			foreach (Item item in pActor.equipment.getItems())
			{
				EquipmentAsset tItemAsset = item.getAsset();
				if (!tItemAsset.isAvailable() && !tItemAsset.unlocked_with_achievement && tItemAsset.needs_to_be_explored)
				{
					return true;
				}
			}
		}
		return QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.traits) || (pActor.hasClan() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.clan.getTraits())) || (pActor.hasCulture() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.culture.getTraits())) || (pActor.hasLanguage() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.language.getTraits())) || (pActor.hasReligion() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.religion.getTraits())) || (pActor.hasSubspecies() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.subspecies.getTraits())) || (pActor.hasKingdom() && QuantumSpriteLibrary.checkAssetsForUnexplored(pActor.kingdom.getTraits()));
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00099218 File Offset: 0x00097418
	private static bool checkAssetsForUnexplored(IReadOnlyCollection<BaseUnlockableAsset> pAssets)
	{
		foreach (BaseUnlockableAsset tAsset in pAssets)
		{
			if (!tAsset.isAvailable() && !tAsset.unlocked_with_achievement && tAsset.needs_to_be_explored)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00099278 File Offset: 0x00097478
	private static void drawBuildingsOld(QuantumSpriteAsset pAsset)
	{
		int tLen = World.world.buildings.countVisibleBuildings();
		Building[] tBuildings = World.world.buildings.getVisibleBuildings();
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tLen);
		for (int i = 0; i < tLen; i++)
		{
			Building building = tBuildings[i];
			QuantumSprite tQSprite = tQSprites[i];
			Sprite tSprite = building.checkSpriteToRender();
			Vector3 tScale = building.getCurrentScale();
			Vector3 tVisualPos = building.cur_transform_position;
			Vector3 tRotation = building.current_rotation;
			Material tMaterial = building.material;
			bool tFlipX = building.flip_x;
			Color tColor = building.kingdom.asset.color_building;
			tQSprite.setSprite(tSprite);
			tQSprite.setScale(ref tScale);
			tQSprite.setSharedMat(tMaterial);
			tQSprite.setPosOnly(ref tVisualPos);
			tQSprite.setRotation(ref tRotation);
			tQSprite.setFlipX(tFlipX);
			tQSprite.setColor(ref tColor);
		}
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0009934C File Offset: 0x0009754C
	private static void drawBuildingsCache(QuantumSpriteAsset pAsset)
	{
		BuildingRenderData render_data = World.world.buildings.render_data;
		int tLen = World.world.buildings.countVisibleBuildings();
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tLen);
		QuantumSpriteCacheData tCacheData = pAsset.group_system.getCacheData(tLen);
		Sprite[] tR_sprites = render_data.colored_sprites;
		Material[] tR_materials = render_data.materials;
		Vector3[] tR_scales = render_data.scales;
		Vector3[] tR_positions = render_data.positions;
		Vector3[] tR_rotations = render_data.rotations;
		bool[] tR_flips = render_data.flip_x_states;
		Color[] tR_colors = render_data.colors;
		Sprite[] tC_sprites = tCacheData.sprites;
		Material[] tC_materials = tCacheData.materials;
		Vector3[] tC_scales = tCacheData.scales;
		Vector3[] tC_positions = tCacheData.positions;
		Vector3[] tC_rotations = tCacheData.rotations;
		bool[] tC_flips = tCacheData.flip_x_states;
		Color[] tC_colors = tCacheData.colors;
		for (int i = 0; i < tLen; i++)
		{
			Sprite tToRender = tR_sprites[i];
			if (tC_sprites[i] != tToRender)
			{
				tC_sprites[i] = tToRender;
				tQSprites[i].sprite_renderer.sprite = tToRender;
			}
		}
		for (int j = 0; j < tLen; j++)
		{
			Material tMaterial = tR_materials[j];
			if (tC_materials[j] != tMaterial)
			{
				tC_materials[j] = tMaterial;
				tQSprites[j].sprite_renderer.sharedMaterial = tMaterial;
			}
		}
		for (int k = 0; k < tLen; k++)
		{
			ref Vector3 tScale = ref tR_scales[k];
			ref Vector3 tCacheScale = ref tC_scales[k];
			if (tScale.x != tCacheScale.x || tScale.y != tCacheScale.y || tScale.z != tCacheScale.z)
			{
				tCacheScale = tScale;
				tQSprites[k].m_transform.localScale = tScale;
			}
		}
		for (int l = 0; l < tLen; l++)
		{
			ref Vector3 tPos = ref tR_positions[l];
			ref Vector3 tCachedPos = ref tC_positions[l];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tQSprites[l].m_transform.position = tPos;
			}
		}
		for (int m = 0; m < tLen; m++)
		{
			ref Vector3 tRotation = ref tR_rotations[m];
			ref Vector3 tCachedRotation = ref tC_rotations[m];
			if (tRotation.x != tCachedRotation.x || tRotation.y != tCachedRotation.y || tRotation.z != tCachedRotation.z)
			{
				tCachedRotation = tRotation;
				tQSprites[m].m_transform.eulerAngles = tRotation;
			}
		}
		for (int n = 0; n < tLen; n++)
		{
			ref bool tFlipX = ref tR_flips[n];
			ref bool tFlipXCache = ref tC_flips[n];
			if (tFlipX != tFlipXCache)
			{
				tFlipXCache = tFlipX;
				tQSprites[n].sprite_renderer.flipX = tFlipX;
			}
		}
		for (int i2 = 0; i2 < tLen; i2++)
		{
			ref Color tColor = ref tR_colors[i2];
			ref Color tCacheColor = ref tC_colors[i2];
			if (tColor.r != tCacheColor.r || tColor.g != tCacheColor.g || tColor.b != tCacheColor.b || tColor.a != tCacheColor.a)
			{
				tCacheColor = tColor;
				tQSprites[i2].sprite_renderer.color = tColor;
			}
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x000996AC File Offset: 0x000978AC
	private static void drawBuildings(QuantumSpriteAsset pAsset)
	{
		BuildingRenderData render_data = World.world.buildings.render_data;
		int tLen = World.world.buildings.countVisibleBuildings();
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tLen);
		Sprite[] tR_sprites = render_data.colored_sprites;
		Material[] tR_materials = render_data.materials;
		Vector3[] tR_scales = render_data.scales;
		Vector3[] tR_positions = render_data.positions;
		Vector3[] tR_rotations = render_data.rotations;
		bool[] tR_flips = render_data.flip_x_states;
		Color[] tR_colors = render_data.colors;
		for (int i = 0; i < tLen; i++)
		{
			QuantumSprite quantumSprite = tQSprites[i];
			Sprite tToRender = tR_sprites[i];
			quantumSprite.setSprite(tToRender);
			Material tMaterial = tR_materials[i];
			quantumSprite.setSharedMat(tMaterial);
			ref Vector3 tScale = ref tR_scales[i];
			quantumSprite.setScale(ref tScale);
			ref Vector3 tPos = ref tR_positions[i];
			quantumSprite.setPosOnly(ref tPos);
			ref Vector3 tRotation = ref tR_rotations[i];
			quantumSprite.setRotation(ref tRotation);
			ref bool tFlipX = ref tR_flips[i];
			quantumSprite.setFlipX(tFlipX);
			ref Color tColor = ref tR_colors[i];
			quantumSprite.setColor(ref tColor);
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000997B0 File Offset: 0x000979B0
	private static void drawParabolicUnload(QuantumSpriteAsset pAsset)
	{
		List<ResourceThrowData> tList = World.world.resource_throw_manager.getList();
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tList.Count);
		for (int i = tList.Count - 1; i >= 0; i--)
		{
			ResourceThrowData tEffectData = tList[i];
			QuantumSprite quantumSprite = tQSprites[i];
			float tRatio = tEffectData.getRatio();
			Vector3 tParabolicPos = Toolbox.Parabola(tEffectData.position_start, tEffectData.position_end, tEffectData.height, tRatio);
			tParabolicPos.z = 4f;
			float tScale = 0.1f;
			Sprite tSprite = AssetManager.resources.get(tEffectData.resource_asset_id).getGameplaySprite();
			quantumSprite.setSprite(tSprite);
			quantumSprite.setPosOnly(ref tParabolicPos);
			quantumSprite.setScale(tScale);
			quantumSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tRatio * 360f));
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00099894 File Offset: 0x00097A94
	private static void drawUnitsEffectDamage(QuantumSpriteAsset pAsset)
	{
		List<ActorDamageEffectData> tList = World.world.stack_effects.actor_effect_hit;
		for (int i = tList.Count - 1; i >= 0; i--)
		{
			ActorDamageEffectData tEffectData = tList[i];
			float tSince = World.world.getRealTimeElapsedSince(tEffectData.timestamp);
			Actor tActor = tEffectData.actor;
			if (tSince > 0.3f || !tActor.isAlive() || !tActor.is_visible)
			{
				tList.RemoveAt(i);
			}
			else
			{
				QuantumSprite next = pAsset.group_system.getNext();
				Vector3 tAngle = tActor.updateRotation();
				Vector3 tScale = tActor.current_scale;
				Vector3 tVisualPos = tActor.cur_transform_position;
				Sprite tSprite = tActor.checkSpriteToRender();
				Color tColor = Color.white;
				tColor.a = 1f - tSince / 0.3f;
				next.setSprite(tSprite);
				next.setPosOnly(ref tVisualPos);
				next.setScale(ref tScale);
				next.setRotation(ref tAngle);
				next.setColor(ref tColor);
			}
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00099980 File Offset: 0x00097B80
	private static void drawUnitsEffectHighlight(QuantumSpriteAsset pAsset)
	{
		List<ActorHighlightEffectData> tList = World.world.stack_effects.actor_effect_highlight;
		for (int i = tList.Count - 1; i >= 0; i--)
		{
			ActorHighlightEffectData tEffectData = tList[i];
			float tSince = World.world.getRealTimeElapsedSince(tEffectData.timestamp);
			Actor tActor = tEffectData.actor;
			if (tSince > 0.3f || !tActor.isAlive() || !tActor.is_visible)
			{
				tList.RemoveAt(i);
			}
			else
			{
				QuantumSprite next = pAsset.group_system.getNext();
				Vector3 tAngle = tActor.updateRotation();
				Vector3 tScale = tActor.current_scale;
				Vector3 tVisualPos = tActor.cur_transform_position;
				Sprite tSprite = tActor.checkSpriteToRender();
				Color tColor = Color.white;
				tColor.a = 1f - tSince / 0.3f;
				next.setSprite(tSprite);
				next.setPosOnly(ref tVisualPos);
				next.setScale(ref tScale);
				next.setRotation(ref tAngle);
				next.setColor(ref tColor);
			}
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00099A6C File Offset: 0x00097C6C
	private static void drawSquareSelection(QuantumSpriteAsset pAsset)
	{
		if (!World.world.player_control.square_selection_started)
		{
			return;
		}
		float tCameraScaleZoomMultiplier = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
		Color tColorSelection = World.world.getArchitectColor();
		Vector2 tStart = World.world.player_control.square_selection_position_current;
		Vector2 tEnd = World.world.getMousePos();
		float tWidth = tEnd.x - tStart.x;
		float tHeight = tEnd.y - tStart.y;
		float tLineSize = 0.1f * tCameraScaleZoomMultiplier;
		Color tColorMain = tColorSelection;
		tColorMain.a = 0.3f;
		QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tStart, null, null, null, null, 1f, false, -1f);
		quantumSprite.setSprite(QuantumSpriteLibrary._sprite_pixel);
		quantumSprite.transform.localScale = new Vector3(tWidth, tHeight);
		quantumSprite.setColor(ref tColorMain);
		QuantumSprite quantumSprite2 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tStart, null, null, null, null, 1f, false, -1f);
		quantumSprite2.setSprite(QuantumSpriteLibrary._sprite_pixel);
		quantumSprite2.transform.localScale = new Vector3(tWidth, tLineSize);
		quantumSprite2.setColor(ref tColorSelection);
		QuantumSprite quantumSprite3 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tStart, null, null, null, null, 1f, false, -1f);
		quantumSprite3.setSprite(QuantumSpriteLibrary._sprite_pixel);
		quantumSprite3.transform.localScale = new Vector3(tLineSize, tHeight);
		quantumSprite3.setColor(ref tColorSelection);
		QuantumSprite quantumSprite4 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tEnd, null, null, null, null, 1f, false, -1f);
		quantumSprite4.setSprite(QuantumSpriteLibrary._sprite_pixel);
		quantumSprite4.transform.localScale = new Vector3(-tWidth, tLineSize);
		quantumSprite4.setColor(ref tColorSelection);
		QuantumSprite quantumSprite5 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tEnd, null, null, null, null, 1f, false, -1f);
		quantumSprite5.setSprite(QuantumSpriteLibrary._sprite_pixel);
		quantumSprite5.transform.localScale = new Vector3(tLineSize, -tHeight);
		quantumSprite5.setColor(ref tColorSelection);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00099C38 File Offset: 0x00097E38
	private static void drawArrowsUnitCursor(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_destination"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		bool current_tile = tActor.current_tile != null;
		WorldTile tTile2 = tActor.tile_target;
		if (current_tile && tTile2 != null)
		{
			QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tTile2.posV3, ref pAsset.color, null);
		}
		if (tActor.has_attack_target && tActor.isEnemyTargetAlive())
		{
			QuantumSpriteAsset tAsset2 = AssetManager.quantum_sprites.get("debug_arrows_units_attack_targets");
			QuantumSpriteLibrary.drawArrowQuantumSprite(tAsset2, tActor.current_position, tActor.attack_target.current_position, ref tAsset2.color, null);
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00099CEC File Offset: 0x00097EEC
	private static void drawArrowsUnitCursorSelectedRaycasts(QuantumSpriteAsset pAsset)
	{
		if (!ControllableUnit.isControllingUnit())
		{
			return;
		}
		if (!Input.GetKey(KeyCode.LeftShift))
		{
			return;
		}
		foreach (Actor tActor in ControllableUnit.getCotrolledUnits())
		{
			if (tActor.isRekt())
			{
				break;
			}
			if (tActor.isInMagnet())
			{
				break;
			}
			Vector2 current_position = tActor.current_position;
			Vector2 tMousePosTarget = World.world.getMousePos();
			Color tColorRed = Color.red;
			Color tColorWhite = Color.white;
			Color tColorDark = Color.black;
			List<WorldTile> tRaycastResult = PathfinderTools.raycast(current_position, tMousePosTarget, 0.99f);
			bool tHitSomebodyGenerally = false;
			for (int i = 0; i < tRaycastResult.Count; i++)
			{
				WorldTile tTile = tRaycastResult[i];
				float tScale = 0.05f + (float)i * 0.1f * 0.05f;
				bool tHitNow = false;
				if (i > 0 && tTile.countUnits() > 0)
				{
					tHitSomebodyGenerally = true;
					tHitNow = true;
				}
				float tAngle = Toolbox.getAngleDegrees((float)tTile.x, (float)tTile.y, tMousePosTarget.x, tMousePosTarget.y) - 45f;
				QuantumSprite tQSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tTile.posV3, null, null, null, null, 1f, false, tScale);
				tQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconAttack"));
				tQSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tAngle));
				if (tHitNow)
				{
					tQSprite.setColor(ref tColorRed);
				}
				else if (tHitSomebodyGenerally)
				{
					tQSprite.setColor(ref tColorDark);
				}
				else
				{
					tQSprite.setColor(ref tColorWhite);
				}
			}
		}
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00099EA4 File Offset: 0x000980A4
	private static void drawArrowsUnitCursorSelected(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_destination"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		float tTimeSinceOrder = World.world.getRealTimeElapsedSince(QuantumSpriteLibrary.last_order_timestamp);
		if (tTimeSinceOrder > 2f)
		{
			return;
		}
		float tColorModifier = 1f - tTimeSinceOrder / 2f;
		Color tColorMain = World.world.getArchitectColor();
		int tCurrent = 0;
		foreach (Actor tActor in SelectedUnit.getAllSelectedList())
		{
			if (tActor.isRekt())
			{
				break;
			}
			if (tActor.isInMagnet())
			{
				break;
			}
			if (SelectedUnit.isMainSelected(tActor))
			{
				tColorMain.a = tColorModifier;
			}
			else
			{
				tColorMain.a = tColorModifier * 0.4f;
			}
			bool current_tile = tActor.current_tile != null;
			WorldTile tTile2 = tActor.tile_target;
			if (current_tile && tTile2 != null)
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tTile2.posV3, ref tColorMain, null);
			}
			tCurrent++;
			if (tCurrent > 20)
			{
				break;
			}
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00099FBC File Offset: 0x000981BC
	private static void drawArrowsUnitCursorLover(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_lover"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		if (!tActor.hasLover())
		{
			return;
		}
		Vector3 tPos = tActor.current_position;
		Vector3 tPos2 = tActor.lover.current_position;
		QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0009A02C File Offset: 0x0009822C
	private static void drawArrowsUnitCursorHouse(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_house"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		if (!tActor.hasHouse())
		{
			return;
		}
		Vector3 tPos = tActor.current_position;
		Vector3 tPos2 = tActor.getHomeBuilding().current_position;
		QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0009A09C File Offset: 0x0009829C
	private static void drawArrowsUnitCursorFamily(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_family"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		if (!tActor.hasFamily())
		{
			return;
		}
		Vector3 tPos = tActor.current_position;
		foreach (Actor tFamilyMember in tActor.family.units)
		{
			if (tFamilyMember != tActor && !tFamilyMember.isRekt())
			{
				Vector3 tPos2 = tFamilyMember.current_position;
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0009A15C File Offset: 0x0009835C
	private static void drawArrowsUnitCursorParents(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_parents"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		Vector3 tPos = tActor.current_position;
		foreach (Actor actor in tActor.getParents())
		{
			Vector3 tPos2 = actor.current_position;
			QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
		}
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0009A1F8 File Offset: 0x000983F8
	private static void drawArrowsUnitCursorKids(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_kids"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		Vector3 tPos = tActor.current_position;
		foreach (Actor actor in tActor.getChildren(false))
		{
			Vector3 tPos2 = actor.current_position;
			QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
		}
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0009A294 File Offset: 0x00098494
	private static void drawArrowsUnitCursorAttackTarget(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("cursor_arrow_attack_target"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		if (!tActor.has_attack_target)
		{
			return;
		}
		if (tActor.attack_target.isRekt())
		{
			return;
		}
		BaseSimObject tTarget = tActor.attack_target;
		Vector3 tPos = tActor.current_position;
		Vector3 tPos2 = tTarget.current_position;
		QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPos2, ref pAsset.color, null);
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0009A314 File Offset: 0x00098514
	private static void drawWalls(QuantumSpriteAsset pAsset)
	{
		GodPower selectedPowerAsset = World.world.getSelectedPowerAsset();
		bool tTransparentBuildings = selectedPowerAsset != null && selectedPowerAsset.make_buildings_transparent;
		Material tDefaultMat = LibraryMaterials.instance.mat_world_object;
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_order, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_evil, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_ancient, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_wild, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_iron, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_green, pAsset, tTransparentBuildings, tDefaultMat);
		QuantumSpriteLibrary.drawWallType(TopTileLibrary.wall_light, pAsset, tTransparentBuildings, World.world.library_materials.mat_world_object_lit);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0009A3AC File Offset: 0x000985AC
	private static void drawWallLightBlobs(QuantumSpriteAsset pAsset)
	{
		if (!World.world.era_manager.shouldShowLights())
		{
			return;
		}
		List<WorldTile> tTiles = TopTileLibrary.wall_light.getCurrentTiles();
		if (tTiles.Count == 0)
		{
			return;
		}
		for (int i = 0; i < tTiles.Count; i++)
		{
			WorldTile tTile = tTiles[i];
			if (tTile.zone.visible)
			{
				World.world.stack_effects.light_blobs.Add(new LightBlobData
				{
					position = tTile.posV3,
					radius = 0.3f
				});
			}
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0009A440 File Offset: 0x00098640
	private static void drawLavaLightBlobs(QuantumSpriteAsset pAsset)
	{
		if (!World.world.era_manager.shouldShowLights())
		{
			return;
		}
		List<TileZone> tListZones = World.world.zone_camera.getVisibleZones();
		for (int iZone = 0; iZone < tListZones.Count; iZone++)
		{
			TileZone tZone = tListZones[iZone];
			if (tZone.hasLava())
			{
				if (tZone.countLava() < 5)
				{
					using (IEnumerator<WorldTile> enumerator = tZone.loopLava().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							WorldTile tTile = enumerator.Current;
							World.world.stack_effects.light_blobs.Add(new LightBlobData
							{
								position = tTile.posV3,
								radius = 0.2f
							});
						}
						goto IL_ED;
					}
				}
				World.world.stack_effects.light_blobs.Add(new LightBlobData
				{
					position = tZone.centerTile.posV3,
					radius = 1f
				});
			}
			IL_ED:;
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0009A55C File Offset: 0x0009875C
	private static void drawWallType(TopTileType pTileTypeAsset, QuantumSpriteAsset pAsset, bool pTransparentBuildings, Material pMaterial)
	{
		List<WorldTile> tTiles = pTileTypeAsset.getCurrentTiles();
		if (tTiles.Count == 0)
		{
			return;
		}
		float tGlobalBuildingScaleX = World.world.quality_changer.getTweenBuildingsValue() * 0.25f;
		float tGlobalBuildingScaleY = tGlobalBuildingScaleX;
		float zRange = 0.1f;
		for (int i = 0; i < tTiles.Count; i++)
		{
			WorldTile tTile = tTiles[i];
			if (tTile.zone.visible)
			{
				Sprite tSprite = WallHelper.getSprite(tTile, pTileTypeAsset);
				QuantumSprite next = pAsset.group_system.getNext();
				next.setSprite(tSprite);
				Vector3 tPos = tTile.posV3;
				tPos.z = Mathf.Repeat(tPos.x * 0.0001f, zRange);
				next.setPosOnly(ref tPos);
				next.setScale(tGlobalBuildingScaleX, tGlobalBuildingScaleY);
				next.setSharedMat(pMaterial);
			}
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0009A61C File Offset: 0x0009881C
	private static void drawUnitsAvatars(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = World.world.units.visible_units_avatars.array;
		int tLen = World.world.units.visible_units_avatars.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tActor.asset.ignore_generic_render)
			{
				Transform tAvatar = tActor.avatar.transform;
				if (!tActor.is_visible)
				{
					tAvatar.position = Globals.POINT_IN_VOID;
				}
				else
				{
					Vector3 tRotation = tActor.updateRotation();
					Vector3 tScale = tActor.current_scale;
					Vector3 tVisualPos = tActor.updatePos();
					tAvatar.position = tVisualPos;
					tAvatar.localScale = tScale;
					tAvatar.eulerAngles = tRotation;
				}
			}
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0009A6C8 File Offset: 0x000988C8
	private static void drawHealthbars(QuantumSpriteAsset pAsset)
	{
		bool tShowForSelectedUnits = SelectedUnit.isSet();
		bool tShowForAllUnits = HotkeyLibrary.isHoldingAlt();
		if (!tShowForAllUnits && !tShowForSelectedUnits)
		{
			return;
		}
		if (tShowForAllUnits)
		{
			tShowForSelectedUnits = false;
		}
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		if (Zones.getCurrentMapBorderMode(false).isNone())
		{
			return;
		}
		ref Color tColor = ref ColorStyleLibrary.m.health_bar_background;
		ref Color tColorGreen = ref ColorStyleLibrary.m.health_bar_main_green;
		ref Color tColorRed = ref ColorStyleLibrary.m.health_bar_main_red;
		float tCameraMod = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset) * 1.6f;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tShowForSelectedUnits || SelectedUnit.isSelected(tActor))
			{
				float tHealthRatio = tActor.getHealthRatio();
				if (tHealthRatio < 1f)
				{
					float tActorScale = 0.1f;
					float tWidthXScale = 9f * tActorScale * tCameraMod;
					float tHeightXScale = 1.5f * tActorScale * tCameraMod;
					Vector3 tPos = default(Vector3);
					tPos.x = tActor.cur_transform_position.x - tWidthXScale / 2f;
					tPos.y = tActor.cur_transform_position.y + 13f * tActorScale;
					if (tHealthRatio < 1f)
					{
						QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
						quantumSprite.setSprite(QuantumSpriteLibrary._sprite_pixel);
						quantumSprite.transform.localScale = new Vector3(tWidthXScale, tHeightXScale);
						quantumSprite.setColor(ref tColor);
					}
					ref Color tBarColor = ref tColorGreen;
					if (tActor.getHealthRatio() < 0.4f)
					{
						tBarColor = ref tColorRed;
					}
					tPos.z += 0.01f;
					QuantumSprite quantumSprite2 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
					quantumSprite2.setSprite(QuantumSpriteLibrary._sprite_pixel);
					quantumSprite2.transform.localScale = new Vector3(tWidthXScale * tHealthRatio, tHeightXScale);
					quantumSprite2.setColor(ref tBarColor);
				}
			}
		}
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0009A888 File Offset: 0x00098A88
	private static void drawUnits(QuantumSpriteAsset pAsset)
	{
		ActorRenderData tRenderData = World.world.units.render_data;
		int tVisibleUnits = QuantumSpriteLibrary.visible_units_count;
		if (tVisibleUnits == 0)
		{
			return;
		}
		bool[] tR_hasNormalRender = tRenderData.has_normal_render;
		Sprite[] tR_mainSprites = tRenderData.main_sprite_colored;
		Vector3[] tR_positions = tRenderData.positions;
		Vector3[] tR_scales = tRenderData.scales;
		Vector3[] tR_rotations = tRenderData.rotations;
		Color[] tR_colors = tRenderData.colors;
		if (QuantumSpriteLibrary._q_render_indexes_units.Length < tVisibleUnits)
		{
			QuantumSpriteLibrary._q_render_indexes_units = Toolbox.checkArraySize<int>(QuantumSpriteLibrary._q_render_indexes_units, tVisibleUnits);
		}
		int[] tUnitIndexesToRender = QuantumSpriteLibrary._q_render_indexes_units;
		int tCounterIndexes = 0;
		for (int i = 0; i < tVisibleUnits; i++)
		{
			if (tR_hasNormalRender[i])
			{
				tUnitIndexesToRender[tCounterIndexes++] = i;
			}
		}
		if (tCounterIndexes == 0)
		{
			return;
		}
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tCounterIndexes);
		QuantumSpriteCacheData cacheData = pAsset.group_system.getCacheData(tCounterIndexes);
		Sprite[] tC_mainSprites = cacheData.sprites;
		Vector3[] tC_positions = cacheData.positions;
		Vector3[] tC_scales = cacheData.scales;
		Vector3[] tC_rotations = cacheData.rotations;
		Color[] tC_colors = cacheData.colors;
		for (int j = 0; j < tCounterIndexes; j++)
		{
			int tRenderIndex = tUnitIndexesToRender[j];
			Sprite tMainSprite = tR_mainSprites[tRenderIndex];
			if (tC_mainSprites[j] != tMainSprite)
			{
				tC_mainSprites[j] = tMainSprite;
				tQSprites[j].sprite_renderer.sprite = tMainSprite;
			}
		}
		for (int k = 0; k < tCounterIndexes; k++)
		{
			int tRenderIndex2 = tUnitIndexesToRender[k];
			Transform tTransform = tQSprites[k].m_transform;
			ref Vector3 tPos = ref tR_positions[tRenderIndex2];
			ref Vector3 tCachedPos = ref tC_positions[k];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tTransform.position = tPos;
			}
			ref Vector3 tScale = ref tR_scales[tRenderIndex2];
			ref Vector3 tCachedScale = ref tC_scales[k];
			if (tScale.x != tCachedScale.x || tScale.y != tCachedScale.y || tScale.z != tCachedScale.z)
			{
				tCachedScale = tScale;
				tTransform.localScale = tScale;
			}
			ref Vector3 tRotation = ref tR_rotations[tRenderIndex2];
			ref Vector3 tCachedRotation = ref tC_rotations[k];
			if (tRotation.x != tCachedRotation.x || tRotation.y != tCachedRotation.y || tRotation.z != tCachedRotation.z)
			{
				tCachedRotation = tRotation;
				tTransform.eulerAngles = tRotation;
			}
			ref Color tColor = ref tR_colors[tRenderIndex2];
			ref Color tCachedColor = ref tC_colors[k];
			if (tColor.r != tCachedColor.r || tColor.g != tCachedColor.g || tColor.b != tCachedColor.b)
			{
				tCachedColor = tColor;
				tQSprites[k].sprite_renderer.color = tColor;
			}
		}
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0009AB64 File Offset: 0x00098D64
	private static void drawUnitItems(QuantumSpriteAsset pAsset)
	{
		ActorRenderData tRenderData = World.world.units.render_data;
		int tVisibleUnits = QuantumSpriteLibrary.visible_units_count;
		if (tVisibleUnits == 0)
		{
			return;
		}
		bool[] tR_hasItem = tRenderData.has_item;
		Vector3[] tR_scale = tRenderData.item_scale;
		Vector3[] tR_position = tRenderData.item_pos;
		Vector3[] tR_rotations = tRenderData.rotations;
		Sprite[] tR_sprites = tRenderData.item_sprites;
		if (QuantumSpriteLibrary._q_render_indexes_unit_items.Length < tVisibleUnits)
		{
			QuantumSpriteLibrary._q_render_indexes_unit_items = Toolbox.checkArraySize<int>(QuantumSpriteLibrary._q_render_indexes_unit_items, tVisibleUnits);
		}
		int[] tUnitIndexesToRender = QuantumSpriteLibrary._q_render_indexes_unit_items;
		int tCounterIndexes = 0;
		for (int i = 0; i < tVisibleUnits; i++)
		{
			if (tR_hasItem[i])
			{
				tUnitIndexesToRender[tCounterIndexes++] = i;
			}
		}
		if (tCounterIndexes == 0)
		{
			return;
		}
		QuantumSpriteCacheData cacheData = pAsset.group_system.getCacheData(tCounterIndexes);
		Vector3[] tC_scale = cacheData.scales;
		Vector3[] tC_position = cacheData.positions;
		Vector3[] tC_rotations = cacheData.rotations;
		Sprite[] tC_sprites = cacheData.sprites;
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tCounterIndexes);
		for (int j = 0; j < tCounterIndexes; j++)
		{
			int tRenderIndex = tUnitIndexesToRender[j];
			ref Vector3 tScale = ref tR_scale[tRenderIndex];
			ref Vector3 tCachedScale = ref tC_scale[j];
			if (tScale.x != tCachedScale.x || tScale.y != tCachedScale.y || tScale.z != tCachedScale.z)
			{
				tCachedScale = tScale;
				tQSprites[j].m_transform.localScale = tScale;
			}
			ref Vector3 tPos = ref tR_position[tRenderIndex];
			ref Vector3 tCachedPos = ref tC_position[j];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tQSprites[j].m_transform.position = tPos;
			}
			ref Vector3 tRotation = ref tR_rotations[tRenderIndex];
			ref Vector3 tCachedRotation = ref tC_rotations[j];
			if (tRotation.x != tCachedRotation.x || tRotation.y != tCachedRotation.y || tRotation.z != tCachedRotation.z)
			{
				tCachedRotation = tRotation;
				tQSprites[j].m_transform.eulerAngles = tRotation;
			}
		}
		for (int k = 0; k < tCounterIndexes; k++)
		{
			int tRenderIndex2 = tUnitIndexesToRender[k];
			Sprite tItemSprite = tR_sprites[tRenderIndex2];
			if (tC_sprites[k] != tItemSprite)
			{
				tC_sprites[k] = tItemSprite;
				tQSprites[k].sprite_renderer.sprite = tItemSprite;
			}
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0009ADD4 File Offset: 0x00098FD4
	private static void drawFires(QuantumSpriteAsset pAsset)
	{
		if (!WorldBehaviourActionFire.hasFires())
		{
			return;
		}
		float pAnimationSpeed = 10f;
		int tFireTilesCount = 0;
		if (QuantumSpriteLibrary._q_render_indexes_sprites_fire.Length < World.world.tile_manager.tiles_count)
		{
			QuantumSpriteLibrary._q_render_indexes_sprites_fire = new int[World.world.tile_manager.tiles_count];
		}
		int[] tFireTilesToRender = QuantumSpriteLibrary._q_render_indexes_sprites_fire;
		float tTime = AnimationHelper.getAnimationGlobalTime(pAnimationSpeed);
		Sprite[][] tFireSpriteSets = QuantumSpriteLibrary._fire_sprites_sets;
		int[] tFireZones = WorldBehaviourActionFire.getFires();
		int[] tRandomSeeds = World.world.tile_manager.random_seeds;
		int[] tFireSets = World.world.tile_manager.fire_animation_set;
		List<TileZone> tZones = World.world.zone_camera.getVisibleZones();
		Vector3[] tPositions = World.world.tile_manager.positions_vector3;
		bool[] tTileFires = World.world.tile_manager.fires;
		for (int iZone = 0; iZone < tZones.Count; iZone++)
		{
			TileZone tZone = tZones[iZone];
			if (tFireZones[tZone.id] != 0)
			{
				WorldTile[] tTiles = tZone.tiles;
				int tCount = tTiles.Length;
				for (int i = 0; i < tCount; i++)
				{
					int tTileID = tTiles[i].tile_id;
					if (tTileFires[tTileID])
					{
						tFireTilesToRender[tFireTilesCount++] = tTileID;
					}
				}
			}
		}
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tFireTilesCount);
		QuantumSpriteCacheData cacheData = pAsset.group_system.getCacheData(tFireTilesCount);
		Vector3[] tC_positions = cacheData.positions;
		int[] tC_indexes = cacheData.indexes;
		int[] tC_indexes_2 = cacheData.indexes_2;
		for (int j = 0; j < tFireTilesCount; j++)
		{
			int tTileID2 = tFireTilesToRender[j];
			int tFireSet = tFireSets[tTileID2];
			Sprite[] tCurSprites = tFireSpriteSets[tFireSet];
			Vector3 tPos = tPositions[tTileID2];
			ref Vector3 tCachedPos = ref tC_positions[j];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tQSprites[j].m_transform.position = tPos;
			}
			int tFrameIndex = (int)(tTime + (float)(tRandomSeeds[tTileID2] * 100)) % tCurSprites.Length;
			if (tC_indexes[j] != tFrameIndex || tC_indexes_2[j] != tFireSet)
			{
				tC_indexes[j] = tFrameIndex;
				tC_indexes_2[j] = tFireSet;
				Sprite tSprite = tCurSprites[tFrameIndex];
				tQSprites[j].sprite_renderer.sprite = tSprite;
			}
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0009B000 File Offset: 0x00099200
	private static void drawSocialize(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("talk_bubbles"))
		{
			return;
		}
		float tMax = 1f;
		double tCurTime = World.world.getCurSessionTime();
		Actor[] tArr = World.world.units.visible_units_socialize.array;
		int tLen = World.world.units.visible_units_socialize.count;
		tLen = Math.Min(tLen, 1000);
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tActor.hasTrait("mute"))
			{
				CommunicationAsset normal = CommunicationLibrary.normal;
				float tDiff = (float)(tCurTime - tActor.timestamp_tween_session_social);
				if (tDiff > tMax)
				{
					tDiff = 1f;
				}
				Vector3 headOffsetPositionForFunRendering = tActor.getHeadOffsetPositionForFunRendering();
				float tTween = iTween.easeOutCubic(0f, 1f, tDiff);
				float tOffsetX = Randy.randomFloat(-0.03f, 0.03f);
				float tOffsetY = Randy.randomFloat(-0.03f, 0.03f);
				Vector2 tScale = tActor.current_scale;
				float tX = headOffsetPositionForFunRendering.x + tOffsetX * tScale.x;
				float tY = headOffsetPositionForFunRendering.y + tOffsetY * tScale.y;
				Vector2 tPos = new Vector2(tX, tY);
				tScale.y *= tTween;
				QuantumSprite tQBubble = pAsset.group_system.getNext();
				tQBubble.set(ref tPos, tScale.y);
				Sprite tSpeechSprite = normal.getSpriteBubble();
				tQBubble.setSprite(tSpeechSprite);
				if (normal.show_topic)
				{
					Vector3 tPosTopic = tPos;
					tPosTopic.x += -1.65f * tActor.current_scale.x;
					tPosTopic.y += 10.04f * tActor.current_scale.y;
					tPosTopic.z = tPos.y + 3f * tActor.current_scale.y;
					QuantumSprite next = pAsset.group_system.getNext();
					next.set(ref tPosTopic, tScale.y * 0.35f);
					Sprite tTopicSprite = tActor.getSocializeTopic();
					next.setSprite(tTopicSprite);
				}
			}
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0009B200 File Offset: 0x00099400
	private static void drawJustAte(QuantumSpriteAsset pAsset)
	{
		float tMax = 1f;
		double tCurTime = World.world.getCurSessionTime();
		Actor[] tArr = World.world.units.visible_units_just_ate.array;
		int tLen = World.world.units.visible_units_just_ate.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			float tDiff = (float)(tCurTime - tActor.timestamp_session_ate_food);
			if (tDiff > tMax)
			{
				tActor.timestamp_session_ate_food = 0.0;
			}
			else
			{
				float tMod = tDiff / tMax;
				float tTween = iTween.easeOutCubic(0f, 1f, tMod);
				Vector3 tVec = tActor.current_position;
				tVec.y += 1f + tTween * 2f;
				float tScale = tTween;
				if (tScale > 0.5f)
				{
					tScale = 0.5f;
				}
				QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVec, null, null, null, null, tScale, false, -1f);
				ResourceAsset tAsset = AssetManager.resources.get(tActor.ate_last_item_id);
				quantumSprite.setSprite(tAsset.getSpriteIcon());
				quantumSprite.transform.eulerAngles = new Vector3(0f, 0f, tTween * 360f);
				float tAlpha = 1f;
				if ((double)tMod > 0.6)
				{
					tAlpha = (1f - tMod) / 0.4f;
				}
				Color tColor = new Color(tAlpha, tAlpha, tAlpha, tAlpha);
				quantumSprite.setColor(ref tColor);
			}
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0009B370 File Offset: 0x00099570
	private static void drawCapturingZones(QuantumSpriteAsset pAsset)
	{
		if (!Zones.showKingdomZones(false) && !Zones.showCityZones(false) && !Zones.showAllianceZones(false))
		{
			return;
		}
		using (ListPool<TileZone> tZonesToDraw = new ListPool<TileZone>())
		{
			foreach (City tCity in World.world.cities)
			{
				if (!tCity.being_captured_by.isRekt() && tCity.hasZones())
				{
					float tZonesToShow = (float)tCity.last_visual_capture_ticks / 100f * (float)tCity.zones.Count;
					if (tZonesToShow > (float)tCity.zones.Count)
					{
						tZonesToShow = (float)tCity.zones.Count;
					}
					CapturingZonesCalculator.getListToDraw(tCity, (int)tZonesToShow, tZonesToDraw);
					for (int i = 0; i < tZonesToDraw.Count; i++)
					{
						TileZone tZone = tZonesToDraw[i];
						GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tZone.centerTile, null, null, null, null);
						Color tColor = tCity.being_captured_by.getColor().getColorBorderOut_capture();
						groupSpriteObject.setColor(ref tColor);
					}
				}
			}
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0009B49C File Offset: 0x0009969C
	private static void drawUnityLine(QuantumSpriteAsset pAsset)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (!World.world.isSelectedPower("unity"))
		{
			return;
		}
		Kingdom tKingdom = Config.unity_A;
		if (tKingdom == null)
		{
			return;
		}
		Vector2 tMousePos = World.world.getMousePos();
		foreach (City tCity in tKingdom.getCities())
		{
			Color tColor = tKingdom.getColor().getColorMainSecond();
			QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tCity.getTile(false).posV, tMousePos, ref tColor, null);
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0009B548 File Offset: 0x00099748
	private static void drawWhisperOfWarLine(QuantumSpriteAsset pAsset)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (!World.world.isSelectedPower("whisper_of_war"))
		{
			return;
		}
		Kingdom tKingdom = Config.whisper_A;
		if (tKingdom == null)
		{
			return;
		}
		Vector2 tMousePos = World.world.getMousePos();
		foreach (City tCity in tKingdom.getCities())
		{
			Color tColor = tKingdom.getColor().getColorMainSecond();
			QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tCity.getTile(false).posV, tMousePos, ref tColor, null);
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0009B5F4 File Offset: 0x000997F4
	private static void drawWhisperOfWar(QuantumSpriteAsset pAsset)
	{
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (!World.world.isSelectedPower("whisper_of_war"))
		{
			return;
		}
		WorldTile mouseTilePosCachedFrame = World.world.getMouseTilePosCachedFrame();
		City tMouseCity = (mouseTilePosCachedFrame != null) ? mouseTilePosCachedFrame.zone.city : null;
		Kingdom tKingdomToUse = null;
		if (Config.whisper_A == null)
		{
			if (tMouseCity == null)
			{
				return;
			}
			tKingdomToUse = tMouseCity.kingdom;
		}
		else
		{
			tKingdomToUse = Config.whisper_A;
		}
		foreach (City tCity in tKingdomToUse.getCities())
		{
			QuantumSpriteLibrary.colorZones(pAsset, tCity.zones, pAsset.color);
		}
		QuantumSpriteLibrary.colorEnemies(pAsset, tKingdomToUse);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0009B6AC File Offset: 0x000998AC
	private static void drawSelectedKingdomZones(QuantumSpriteAsset pAsset)
	{
		if (!World.world.isSelectedPower("relations"))
		{
			return;
		}
		if (SelectedMetas.selected_kingdom == null)
		{
			return;
		}
		foreach (City tCity in SelectedMetas.selected_kingdom.getCities())
		{
			QuantumSpriteLibrary.colorZones(pAsset, tCity.zones, pAsset.color);
		}
		QuantumSpriteLibrary.colorEnemies(pAsset, SelectedMetas.selected_kingdom);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0009B730 File Offset: 0x00099930
	private static void drawCursorZones(QuantumSpriteAsset pAsset)
	{
		if (World.world.isBusyWithUI())
		{
			return;
		}
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (!Zones.showMapBorders())
		{
			return;
		}
		WorldTile tMouseTile = World.world.getMouseTilePosCachedFrame();
		if (tMouseTile == null)
		{
			return;
		}
		MetaTypeAsset tCurrentMetaZoneType = World.world.getCachedMapMetaAsset();
		if (tCurrentMetaZoneType != null)
		{
			tCurrentMetaZoneType.check_cursor_highlight(tCurrentMetaZoneType, tMouseTile, pAsset);
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0009B788 File Offset: 0x00099988
	public static void colorEnemies(QuantumSpriteAsset pAsset, Kingdom pKingdom)
	{
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.isEnemy(pKingdom))
			{
				foreach (City tCity in tKingdom.getCities())
				{
					Color tColor = pAsset.color_2;
					tColor.a = 0.1f + QuantumSpriteManager.highlight_animation / 30f;
					QuantumSpriteLibrary.colorZones(pAsset, tCity.zones, tColor);
				}
			}
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0009B840 File Offset: 0x00099A40
	public static void colorZones(QuantumSpriteAsset pAsset, List<TileZone> pZones, Color pColor)
	{
		for (int i = 0; i < pZones.Count; i++)
		{
			TileZone tZone = pZones[i];
			if (tZone.visible)
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tZone.centerTile.posV, null, null, null, null, 1f, false, -1f).setColor(ref pColor);
			}
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0009B898 File Offset: 0x00099A98
	public static void colorZones(QuantumSpriteAsset pAsset, ListPool<TileZone> pZones, Color pColor)
	{
		for (int i = 0; i < pZones.Count; i++)
		{
			TileZone tZone = pZones[i];
			if (tZone.visible)
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tZone.centerTile.posV, null, null, null, null, 1f, false, -1f).setColor(ref pColor);
			}
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0009B8F0 File Offset: 0x00099AF0
	private static void drawArrowsArmyAttackTargets(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_armies"))
		{
			return;
		}
		if (!PlayerConfig.optionBoolEnabled("army_targets"))
		{
			return;
		}
		WorldTile tMouseTile = World.world.getMouseTilePosCachedFrame();
		City tMouseCity = null;
		if (tMouseTile != null && DebugConfig.isOn(DebugOption.ArrowsOnlyForCursorCities))
		{
			tMouseCity = tMouseTile.zone.city;
		}
		foreach (City tCity in World.world.cities)
		{
			if (tCity.target_attack_city != null && (!Zones.showCityZones(false) || tMouseCity == null || tCity == tMouseCity) && tCity.hasArmy() && tCity.army.hasCaptain())
			{
				Actor captain = tCity.army.getCaptain();
				WorldTile tTile = captain.current_tile;
				WorldTile tTile2 = captain.beh_tile_target;
				if (tTile != null && tTile2 != null)
				{
					Color tColor = tCity.kingdom.getColor().getColorMainSecond();
					QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tTile.posV3, tTile2.posV3, ref tColor, tCity);
				}
			}
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0009B9F4 File Offset: 0x00099BF4
	private static void drawWarsIcons(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_wars"))
		{
			return;
		}
		QuantumSpriteLibrary.drawWarIconInList(QuantumSpriteLibrary._wars_pos_sword_main, "ui/Icons/iconAttack", pAsset, 0.2f);
		QuantumSpriteLibrary.drawWarIconInList(QuantumSpriteLibrary._wars_pos_shields_main, "ui/Icons/iconShield", pAsset, 0.2f);
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0009BA30 File Offset: 0x00099C30
	private static void drawWarIconInList(List<Vector3> pList, string pPath, QuantumSpriteAsset pAsset, float pSize)
	{
		if (pList.Count == 0)
		{
			return;
		}
		foreach (Vector3 tVec in pList)
		{
			float tScale = pSize * tVec.z * 1.5f;
			pAsset.base_scale = tScale;
			QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVec, null, null, null, null, 1f, false, -1f);
			quantumSprite.setSprite(SpriteTextureLoader.getSprite(pPath));
			quantumSprite.sprite_renderer.sortingOrder = 1;
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0009BAC4 File Offset: 0x00099CC4
	private static void drawProjectileShadows(QuantumSpriteAsset pAsset)
	{
		if (!Config.shadows_active)
		{
			return;
		}
		foreach (Projectile tProjectile in World.world.projectiles.list)
		{
			ProjectileAsset tAsset = tProjectile.asset;
			if (!string.IsNullOrEmpty(tAsset.texture_shadow))
			{
				Vector3 tShadowPos = tProjectile.getCurrentPosition();
				float tAngle = tProjectile.getAngleForShadow();
				QuantumSprite next = pAsset.group_system.getNext();
				Sprite tSprite = SpriteTextureLoader.getSprite(tAsset.texture_shadow);
				next.setSprite(tSprite);
				next.set(ref tShadowPos, tProjectile.getCurrentScale());
				next.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tAngle));
			}
		}
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0009BB9C File Offset: 0x00099D9C
	private static void drawProjectiles(QuantumSpriteAsset pAsset)
	{
		foreach (Projectile tProjectile in World.world.projectiles.list)
		{
			ProjectileAsset tAsset = tProjectile.asset;
			Color tColor = new Color(1f, 1f, 1f, tProjectile.getAlpha());
			Vector3 tCurPos = tProjectile.getTransformedPositionWithHeight();
			tCurPos.z = tProjectile.getCurrentHeight();
			QuantumSprite tQSprite = pAsset.group_system.getNext();
			if (tAsset.animated)
			{
				Sprite tSprite = AnimationHelper.getSpriteFromList(tProjectile.GetHashCode(), tAsset.frames, tAsset.animation_speed);
				tQSprite.setSprite(tSprite);
			}
			else
			{
				Sprite tSprite2 = tAsset.frames[0];
				tQSprite.setSprite(tSprite2);
			}
			tQSprite.set(ref tCurPos, tProjectile.getCurrentScale());
			tQSprite.transform.rotation = tProjectile.rotation;
			tQSprite.setColor(ref tColor);
		}
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0009BCAC File Offset: 0x00099EAC
	private static void drawThrowingItemsShadows(QuantumSpriteAsset pAsset)
	{
		if (!Config.shadows_active)
		{
			return;
		}
		List<ResourceThrowData> tList = World.world.resource_throw_manager.getList();
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tList.Count);
		for (int i = 0; i < tList.Count; i++)
		{
			ResourceThrowData tData = tList[i];
			QuantumSprite quantumSprite = tQSprites[i];
			float tRatio = tData.getRatio();
			Vector3 tPos = Vector2.Lerp(tData.position_start, tData.position_end, tRatio);
			tPos.z = 4f;
			float tScale = 0.1f;
			Sprite tSprite = AssetManager.resources.get(tData.resource_asset_id).getGameplaySprite();
			quantumSprite.setSprite(tSprite);
			quantumSprite.set(ref tPos, tScale);
			quantumSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tRatio * 360f));
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0009BD8C File Offset: 0x00099F8C
	private static void drawShadowsBuildings(QuantumSpriteAsset pAsset)
	{
		if (!World.world.quality_changer.shouldRenderBuildingShadows())
		{
			return;
		}
		int tVisibleBuildings = World.world.buildings.countVisibleBuildings();
		if (tVisibleBuildings == 0)
		{
			return;
		}
		BuildingRenderData render_data = World.world.buildings.render_data;
		bool[] tR_shadows = render_data.shadows;
		Vector3[] tR_positions = render_data.positions;
		Vector3[] tR_scales = render_data.scales;
		Sprite[] tR_sprites = render_data.shadow_sprites;
		if (QuantumSpriteLibrary._q_render_indexes_shadows_buildings.Length < tVisibleBuildings)
		{
			QuantumSpriteLibrary._q_render_indexes_shadows_buildings = Toolbox.checkArraySize<int>(QuantumSpriteLibrary._q_render_indexes_shadows_buildings, tVisibleBuildings);
		}
		int[] tShadowIndexesToRender = QuantumSpriteLibrary._q_render_indexes_shadows_buildings;
		int tCounterIndexes = 0;
		for (int i = 0; i < tVisibleBuildings; i++)
		{
			if (tR_shadows[i])
			{
				tShadowIndexesToRender[tCounterIndexes++] = i;
			}
		}
		if (tCounterIndexes == 0)
		{
			return;
		}
		QuantumSpriteCacheData cacheData = pAsset.group_system.getCacheData(tCounterIndexes);
		Vector3[] tC_positions = cacheData.positions;
		Vector3[] tC_scales = cacheData.scales;
		Sprite[] tC_sprites = cacheData.sprites;
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tCounterIndexes);
		for (int j = 0; j < tCounterIndexes; j++)
		{
			QuantumSprite tQSprite = tQSprites[j];
			int tRenderIndex = tShadowIndexesToRender[j];
			ref Vector3 tPos = ref tR_positions[tRenderIndex];
			ref Vector3 tCachedPos = ref tC_positions[j];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tQSprite.m_transform.position = tPos;
			}
			ref Vector3 tScale = ref tR_scales[tRenderIndex];
			ref Vector3 tCachedScale = ref tC_scales[j];
			if (tScale.x != tCachedScale.x || tScale.y != tCachedScale.y || tScale.z != tCachedScale.z)
			{
				tCachedScale = tScale;
				tQSprite.m_transform.localScale = tScale;
			}
			Sprite tShadowSprite = tR_sprites[tRenderIndex];
			if (tC_sprites[j] != tShadowSprite)
			{
				tC_sprites[j] = tShadowSprite;
				tQSprite.sprite_renderer.sprite = tShadowSprite;
			}
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0009BF80 File Offset: 0x0009A180
	private static void drawShadowsUnit(QuantumSpriteAsset pAsset)
	{
		if (!World.world.quality_changer.shouldRenderUnitShadows())
		{
			return;
		}
		ActorRenderData tRenderData = World.world.units.render_data;
		int tVisibleUnits = QuantumSpriteLibrary.visible_units_count;
		if (tVisibleUnits == 0)
		{
			return;
		}
		bool[] tR_shadows = tRenderData.shadows;
		Vector3[] tR_positions = tRenderData.shadow_position;
		Vector3[] tR_scales = tRenderData.shadow_scales;
		Sprite[] tR_shadowSprites = tRenderData.shadow_sprites;
		if (QuantumSpriteLibrary._q_render_indexes_shadows_units.Length < tVisibleUnits)
		{
			QuantumSpriteLibrary._q_render_indexes_shadows_units = Toolbox.checkArraySize<int>(QuantumSpriteLibrary._q_render_indexes_shadows_units, tVisibleUnits);
		}
		int[] tUnitIndexesToRender = QuantumSpriteLibrary._q_render_indexes_shadows_units;
		int tCounterIndexes = 0;
		for (int i = 0; i < tVisibleUnits; i++)
		{
			if (tR_shadows[i])
			{
				tUnitIndexesToRender[tCounterIndexes++] = i;
			}
		}
		if (tCounterIndexes == 0)
		{
			return;
		}
		QuantumSprite[] tQSprites = pAsset.group_system.getFastActiveList(tCounterIndexes);
		QuantumSpriteCacheData cacheData = pAsset.group_system.getCacheData(tCounterIndexes);
		Vector3[] tC_positions = cacheData.positions;
		Vector3[] tC_scales = cacheData.shadow_scales;
		Sprite[] tC_shadowSprites = cacheData.sprites;
		for (int j = 0; j < tCounterIndexes; j++)
		{
			int tRenderIndex = tUnitIndexesToRender[j];
			ref Vector3 tPos = ref tR_positions[tRenderIndex];
			ref Vector3 tCachedPos = ref tC_positions[j];
			if (tPos.x != tCachedPos.x || tPos.y != tCachedPos.y || tPos.z != tCachedPos.z)
			{
				tCachedPos = tPos;
				tQSprites[j].m_transform.position = tPos;
			}
			ref Vector3 tScale = ref tR_scales[tRenderIndex];
			ref Vector3 tCachedScale = ref tC_scales[j];
			if (tScale.x != tCachedScale.x || tScale.y != tCachedScale.y || tScale.z != tCachedScale.z)
			{
				tCachedScale = tScale;
				tQSprites[j].m_transform.localScale = tScale;
			}
			Sprite tShadowSprite = tR_shadowSprites[tRenderIndex];
			if (tC_shadowSprites[j] != tShadowSprite)
			{
				tC_shadowSprites[j] = tShadowSprite;
				tQSprites[j].sprite_renderer.sprite = tShadowSprite;
			}
		}
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0009C170 File Offset: 0x0009A370
	private static void drawUnitBanners(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = World.world.units.visible_units_with_banner.array;
		int tLen = World.world.units.visible_units_with_banner.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			Vector3 tPos = tActor.getHeadOffsetPositionForFunRendering();
			QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tActor.current_scale.y);
			Color tColor = tActor.kingdom.getColor().getColorText();
			quantumSprite.setColor(ref tColor);
			quantumSprite.checkRotation(tPos, tActor, -0.01f);
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0009C204 File Offset: 0x0009A404
	private static void drawFavoriteItemsMap(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_favorite_items"))
		{
			return;
		}
		foreach (Item tItem in World.world.items)
		{
			if (tItem.isFavorite())
			{
				Actor tActor = tItem.getActor();
				if (!tActor.isRekt() && tActor.current_zone.visible)
				{
					Vector3 tPos = tActor.current_position;
					tPos.y += 1f;
					GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, tActor.kingdom, tActor.city, null, 1f, false, -1f);
					Sprite tSprite = tItem.getSprite();
					groupSpriteObject.setSprite(tSprite);
				}
			}
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0009C2CC File Offset: 0x0009A4CC
	private static void drawFavoritesMap(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_favorites"))
		{
			return;
		}
		Actor[] tArr = World.world.units.visible_units_with_favorite.array;
		int tLen = World.world.units.visible_units_with_favorite.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			Vector3 tPos = tActor.current_position;
			tPos.y -= 3f;
			QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, tActor.kingdom, tActor.city, null, 1f, false, -1f);
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0009C360 File Offset: 0x0009A560
	private static void drawUnitsToBeSelectedBySquareTool(QuantumSpriteAsset pAsset)
	{
		if (!World.world.player_control.square_selection_started)
		{
			return;
		}
		using (ListPool<Actor> tList = World.world.player_control.getUnitsToBeSelected())
		{
			if (tList != null && tList.Count != 0)
			{
				Sprite tSprite = AnimationHelper.getSpriteFromListSessionTime(0, QuantumSpriteLibrary._unit_selection_effect, 10f);
				Color tColor = World.world.getArchitectColor();
				tColor.a = 0.7f;
				foreach (Actor ptr in tList)
				{
					Actor actor = ptr;
					Vector3 tPos = actor.current_position;
					float tScale = actor.current_scale.y;
					QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tScale);
					quantumSprite.setSprite(tSprite);
					quantumSprite.setColor(ref tColor);
				}
			}
		}
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0009C454 File Offset: 0x0009A654
	private static void drawSelectedUnits(QuantumSpriteAsset pAsset)
	{
		if (!SelectedUnit.isSet())
		{
			return;
		}
		Sprite tSprite = AnimationHelper.getSpriteFromListSessionTime(0, QuantumSpriteLibrary._unit_selection_effect, 10f);
		Sprite tSpriteMain = AnimationHelper.getSpriteFromListSessionTime(0, QuantumSpriteLibrary._unit_selection_effect_main, 10f);
		Color tColor = World.world.getArchitectColor();
		tColor.a = 0.8f;
		Color tColorMain = World.world.getArchitectColor();
		foreach (Actor actor in SelectedUnit.getAllSelected())
		{
			Vector3 tPos = actor.current_position;
			float tScale = actor.current_scale.y;
			if (SelectedUnit.isMainSelected(actor))
			{
				QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tScale * 1.1f);
				quantumSprite.setSprite(tSpriteMain);
				quantumSprite.setColor(ref tColorMain);
			}
			else
			{
				QuantumSprite quantumSprite2 = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tScale);
				quantumSprite2.setSprite(tSprite);
				quantumSprite2.setColor(ref tColor);
			}
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0009C558 File Offset: 0x0009A758
	private static void drawFavoritesGame(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_favorites"))
		{
			return;
		}
		float tOffset = 20f;
		if (PlayerConfig.optionBoolEnabled("icons_tasks"))
		{
			tOffset += 11.5f;
		}
		if (PlayerConfig.optionBoolEnabled("icons_happiness"))
		{
			tOffset += 11.5f;
		}
		Actor[] tArr = World.world.units.visible_units_with_favorite.array;
		int tLen = World.world.units.visible_units_with_favorite.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tActor.isInMagnet())
			{
				tActor.updatePos();
				float tX = tActor.cur_transform_position.x;
				float tY = tActor.cur_transform_position.y + tOffset * tActor.current_scale.y;
				Vector3 tPos = new Vector3(tX, tY);
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, tActor.current_scale.y);
			}
		}
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0009C644 File Offset: 0x0009A844
	private static void drawStatusEffects(QuantumSpriteAsset pAsset)
	{
		Actor[] tUnitsWithStatus = World.world.units.visible_units_with_status.array;
		int tVisibleUnitsCounter = World.world.units.visible_units_with_status.count;
		for (int i = 0; i < tVisibleUnitsCounter; i++)
		{
			QuantumSpriteLibrary.drawStatusEffectFor(tUnitsWithStatus[i], pAsset);
		}
		int tLenBuildings = World.world.buildings.countVisibleBuildings();
		Building[] tBuildings = World.world.buildings.getVisibleBuildings();
		for (int j = 0; j < tLenBuildings; j++)
		{
			Building tBuilding = tBuildings[j];
			if (tBuilding.hasAnyStatusEffectToRender())
			{
				QuantumSpriteLibrary.drawStatusEffectFor(tBuilding, pAsset);
			}
		}
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0009C6DC File Offset: 0x0009A8DC
	private static void drawStatusEffectFor(BaseSimObject pSimObject, QuantumSpriteAsset pAsset)
	{
		foreach (Status tEffect in pSimObject.getStatuses())
		{
			StatusAsset tAssetAsset = tEffect.asset;
			if (tAssetAsset.need_visual_render)
			{
				Vector3 tPos = pSimObject.cur_transform_position;
				if (pSimObject.isActor())
				{
					tPos.x += tAssetAsset.offset_x * pSimObject.a.getScaleMod();
					tPos.y += tAssetAsset.offset_y * pSimObject.a.getScaleMod();
				}
				if (tAssetAsset.has_override_sprite_position)
				{
					Vector3 tOverridePos = tAssetAsset.get_override_sprite_position(pSimObject, tEffect.anim_frame);
					tPos += tOverridePos;
				}
				if (!pSimObject.isActor() || tEffect.asset.render_check(pSimObject.a.asset))
				{
					QuantumSprite tQSprite = pAsset.group_system.getNext();
					tQSprite.setScale(pSimObject.current_scale.y * tAssetAsset.scale);
					Sprite tSprite;
					if (tAssetAsset.has_override_sprite)
					{
						tSprite = tAssetAsset.get_override_sprite(pSimObject, tEffect.anim_frame);
					}
					else
					{
						tSprite = tAssetAsset.sprite_list[tEffect.anim_frame];
					}
					tQSprite.setSprite(tSprite);
					tQSprite.setPosOnly(ref tPos);
					if (tAssetAsset.use_parent_rotation)
					{
						tQSprite.setFlipX(false);
						tQSprite.checkRotation(tPos, pSimObject, tAssetAsset.position_z);
					}
					else
					{
						if (pSimObject.isActor() && tAssetAsset.can_be_flipped)
						{
							tQSprite.setFlipX(pSimObject.a.flip);
						}
						else
						{
							tQSprite.setFlipX(false);
						}
						Vector3 tZeroRotation = new Vector3(0f, 0f, 0f);
						tQSprite.setRotation(ref tZeroRotation);
					}
					if (tAssetAsset.rotation_z != 0f)
					{
						Vector3 tRotation = pSimObject.current_rotation;
						if (tAssetAsset.has_override_sprite_rotation_z)
						{
							tRotation.z += tAssetAsset.get_override_sprite_rotation_z(pSimObject, tEffect.anim_frame);
						}
						else
						{
							tRotation.z += tAssetAsset.rotation_z;
						}
						tQSprite.setRotation(ref tRotation);
					}
					tQSprite.setSharedMat(tAssetAsset.material);
				}
			}
		}
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0009C918 File Offset: 0x0009AB18
	private static void drawWars(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_wars"))
		{
			return;
		}
		QuantumSpriteLibrary._wars_pos_sword_main.Clear();
		QuantumSpriteLibrary._wars_pos_shields_main.Clear();
		if (World.world.wars.Count == 0)
		{
			return;
		}
		Kingdom tCursorKingdom = null;
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.isCursorOver())
			{
				tCursorKingdom = tKingdom;
				break;
			}
		}
		float tAlpha = 1f;
		foreach (War tWar in World.world.wars)
		{
			bool tHighlightNeeded = false;
			if (!tWar.hasEnded() && !tWar.isTotalWar())
			{
				if (tCursorKingdom != null && tWar.hasKingdom(tCursorKingdom))
				{
					tHighlightNeeded = true;
				}
				if (tCursorKingdom != null)
				{
					if (tHighlightNeeded)
					{
						pAsset.base_scale = 1f;
						tAlpha = 1f;
					}
					else
					{
						pAsset.base_scale = 0.2f;
						tAlpha = 0.1f;
					}
				}
				else
				{
					pAsset.base_scale = 0.5f;
				}
				Kingdom tMainAttacker = tWar.main_attacker;
				Kingdom tMainDefender = tWar.main_defender;
				if (!tMainAttacker.isRekt() && !tMainDefender.isRekt() && tMainAttacker.hasCapital() && tMainDefender.hasCapital() && tMainAttacker.capital.isValidTargetForWar() && tMainDefender.capital.isValidTargetForWar())
				{
					Vector3 tPosMainAttacker = tMainAttacker.capital.city_center;
					Vector3 tPosMainDefender = tMainDefender.capital.city_center;
					tPosMainAttacker.y -= 20f;
					tPosMainDefender.y -= 20f;
					tPosMainAttacker.z = pAsset.base_scale;
					tPosMainDefender.z = pAsset.base_scale;
					QuantumSpriteLibrary._wars_pos_sword_main.Add(tPosMainAttacker);
					QuantumSpriteLibrary._wars_pos_shields_main.Add(tPosMainDefender);
					pAsset.base_scale *= 0.6f;
					QuantumSpriteArrows tQSprite = QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPosMainAttacker, tPosMainDefender, ref Toolbox.color_white, null);
					Color tColor = tMainAttacker.getColor().getColorMainSecond();
					tColor.a = tAlpha;
					if (tQSprite != null)
					{
						tQSprite.spriteArrowMiddle.color = tColor;
						tQSprite.spriteArrowMiddle.sortingOrder = -1;
					}
				}
			}
		}
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
	private static void drawPlots(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_plots"))
		{
			return;
		}
		foreach (Plot tPlot in World.world.plots)
		{
			if (tPlot.isActive())
			{
				QuantumSpriteLibrary.drawPlotIcon(pAsset, tPlot);
			}
		}
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0009CC10 File Offset: 0x0009AE10
	private static void drawPlotIcon(QuantumSpriteAsset pAsset, Plot pPlot)
	{
		foreach (Actor tActor in pPlot.units)
		{
			if (!tActor.isRekt() && tActor.current_zone.visible)
			{
				Vector3 tPos = tActor.current_position;
				City tActorCity = tActor.city;
				float tOffsetMap = 5.5f;
				tOffsetMap *= QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
				if (tActorCity != null)
				{
					tOffsetMap *= tActorCity.mark_scale_effect;
				}
				tPos.y += tOffsetMap;
				QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, tActorCity, null, pPlot.transition_animation, false, -1f);
				Sprite tSprite = pPlot.getSprite();
				quantumSprite.setSprite(tSprite);
				CircleIconShaderMod component = quantumSprite.GetComponent<CircleIconShaderMod>();
				component.sprite_renderer_with_mat.sprite = tSprite;
				component.setShaderVal(pPlot.getProgressMod());
			}
		}
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0009CD04 File Offset: 0x0009AF04
	private static void drawPlotRemovals(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_plots"))
		{
			return;
		}
		List<PlotIconData> tList = World.world.stack_effects.plot_removals;
		if (tList.Count > 0)
		{
			for (int i = tList.Count - 1; i >= 0; i--)
			{
				PlotIconData tIconData = tList[i];
				Actor tActor = tIconData.actor;
				float tSince = World.world.getRealTimeElapsedSince(tIconData.timestamp);
				if (tSince > 1f || !tActor.isAlive())
				{
					tList.RemoveAt(i);
				}
				else
				{
					Vector3 tPos = tActor.current_position;
					City tActorCity = tActor.city;
					float tOffsetMap = 5.5f;
					tOffsetMap *= QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
					if (tActorCity != null)
					{
						tOffsetMap *= tActorCity.mark_scale_effect;
					}
					tPos.y += tOffsetMap;
					float tTimeLeft = tSince / 1f;
					float tScale = Mathf.Lerp(1.3f, 0f, tTimeLeft);
					QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, tActorCity, null, tScale, false, -1f);
					Sprite tSprite = SpriteTextureLoader.getSprite(tIconData.sprite);
					quantumSprite.setSprite(tSprite);
					CircleIconShaderMod component = quantumSprite.GetComponent<CircleIconShaderMod>();
					component.sprite_renderer_with_mat.sprite = tSprite;
					component.setShaderVal(1f);
				}
			}
		}
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0009CE38 File Offset: 0x0009B038
	private static void drawKings(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("map_kings_leaders"))
		{
			return;
		}
		int tShownNew = 0;
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tShownNew > 2)
			{
				break;
			}
			Actor tActor = tKingdom.king;
			if (!tActor.isRekt() && !tActor.isInMagnet() && tActor.current_zone.visible)
			{
				Vector3 tVec = tActor.current_position;
				tVec.y -= 3f;
				Sprite tSpriteToRender;
				if (tActor.has_attack_target)
				{
					tSpriteToRender = QuantumSpriteLibrary._king_sprite_angry;
				}
				else if (tActor.hasPlot())
				{
					tSpriteToRender = QuantumSpriteLibrary._king_sprite_surprised;
				}
				else if (!tKingdom.hasEnemies())
				{
					tSpriteToRender = QuantumSpriteLibrary._king_sprite_happy;
				}
				else
				{
					tSpriteToRender = QuantumSpriteLibrary._king_sprite_normal;
				}
				if (!pAsset.group_system.is_within_active_index)
				{
					tShownNew++;
				}
				GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVec, null, tKingdom, tActor.city, null, 1f, false, -1f);
				Sprite tColoredSprite = DynamicSprites.getIcon(tSpriteToRender, tKingdom.getColor());
				groupSpriteObject.setSprite(tColoredSprite);
			}
		}
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0009CF68 File Offset: 0x0009B168
	private static void drawLeaders(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("map_kings_leaders"))
		{
			return;
		}
		int tShownNew = 0;
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tShownNew > 2)
			{
				break;
			}
			foreach (City tCity in tKingdom.getCities())
			{
				Actor tActor = tCity.leader;
				if (!tActor.isRekt() && !tActor.isInMagnet() && !tActor.isKing() && tActor.current_zone.visible)
				{
					Vector3 tVec = tActor.current_position;
					tVec.y -= 3f;
					Sprite tSpriteToRender;
					if (tActor.has_attack_target)
					{
						tSpriteToRender = QuantumSpriteLibrary._leader_sprite_angry;
					}
					else if (tActor.hasPlot())
					{
						tSpriteToRender = QuantumSpriteLibrary._leader_sprite_surprised;
					}
					else if (tKingdom.hasEnemies())
					{
						tSpriteToRender = QuantumSpriteLibrary._leader_sprite_normal;
					}
					else if (tActor.isHappy())
					{
						tSpriteToRender = QuantumSpriteLibrary._leader_sprite_happy;
					}
					else
					{
						tSpriteToRender = QuantumSpriteLibrary._leader_sprite_sad;
					}
					if (!pAsset.group_system.is_within_active_index)
					{
						tShownNew++;
					}
					GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVec, null, tKingdom, tCity, null, 1f, false, -1f);
					Sprite tColoredSprite = DynamicSprites.getIcon(tSpriteToRender, tKingdom.getColor());
					groupSpriteObject.setSprite(tColoredSprite);
				}
			}
		}
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0009D110 File Offset: 0x0009B310
	private static void drawBattles(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_battles"))
		{
			return;
		}
		HashSet<BattleContainer> tValues = BattleKeeperManager.get();
		if (tValues.Count == 0)
		{
			return;
		}
		foreach (BattleContainer tCont in tValues)
		{
			if (tCont.isRendered())
			{
				GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tCont.tile, null, null, null, tCont);
				Sprite tSprite = SpriteTextureLoader.getSpriteList(pAsset.path_icon, false)[tCont.frame];
				groupSpriteObject.setSprite(tSprite);
			}
		}
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0009D1A8 File Offset: 0x0009B3A8
	private static void drawBoatIcons(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_boats"))
		{
			return;
		}
		foreach (ActorAsset tBoatAsset in AssetManager.actor_library.list_only_boat_assets)
		{
			QuantumSpriteLibrary.drawBoatIcons(pAsset, tBoatAsset.id);
		}
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0009D214 File Offset: 0x0009B414
	private static void drawBoatIcons(QuantumSpriteAsset pAsset, string pActorAssetID)
	{
		HashSet<Actor> tActorSet = AssetManager.actor_library.get(pActorAssetID).units;
		if (tActorSet.Count == 0)
		{
			return;
		}
		int tShownNew = 0;
		foreach (Actor tBoat in tActorSet)
		{
			if (tShownNew > 2)
			{
				break;
			}
			if (!tBoat.isRekt() && tBoat.current_zone.visible && tBoat.asset.draw_boat_mark && tBoat.isKingdomCiv() && (!(pAsset.id == "boats_big") || tBoat.asset.draw_boat_mark_big) && (!(pAsset.id == "boats_small") || !tBoat.asset.draw_boat_mark_big) && !tBoat.isInMagnet())
			{
				bool color = tBoat.kingdom.getColor() != null;
				if (!pAsset.group_system.is_within_active_index)
				{
					tShownNew++;
				}
				QuantumSprite tQSprite;
				if (color)
				{
					Vector3 pPos = tBoat.current_position;
					WorldTile pTileTarget = null;
					City city = tBoat.city;
					tQSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, pPos, pTileTarget, tBoat.kingdom, city, null, 1f, false, -1f);
				}
				else
				{
					Vector3 pPos2 = tBoat.current_position;
					WorldTile pTileTarget2 = null;
					City city = tBoat.city;
					tQSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, pPos2, pTileTarget2, tBoat.kingdom, city, null, 1f, false, -1f);
				}
				Sprite tColoredSprite;
				if (tBoat.asset.draw_boat_mark_big)
				{
					tColoredSprite = DynamicSprites.getIcon(QuantumSpriteLibrary._boat_sprite_big, tBoat.kingdom.getColor());
				}
				else
				{
					tColoredSprite = DynamicSprites.getIcon(QuantumSpriteLibrary._boat_sprite_small, tBoat.kingdom.getColor());
				}
				tQSprite.setSprite(tColoredSprite);
			}
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
	private static void drawMagnetUnits(QuantumSpriteAsset pAsset)
	{
		if (!World.world.magnet.hasUnits())
		{
			return;
		}
		List<Actor> tList = World.world.magnet.magnet_units;
		for (int i = 0; i < tList.Count; i++)
		{
			Actor tUnit = tList[i];
			if (!tUnit.isRekt())
			{
				QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tUnit.current_position, null, tUnit.kingdom, null, null, 1f, false, -1f);
				quantumSprite.setScale(tUnit.current_scale.y);
				quantumSprite.transform.rotation = Quaternion.Euler(0f, 0f, World.world.magnet.moving_angle);
				quantumSprite.setSprite(tUnit.getSpriteToRender());
			}
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0009D4A0 File Offset: 0x0009B6A0
	private static void drawArmies(QuantumSpriteAsset pAsset)
	{
		if (!PlayerConfig.optionBoolEnabled("marks_armies"))
		{
			return;
		}
		int tShownNew = 0;
		if (Zones.showArmyZones(false) && Zones.showMapNames())
		{
			return;
		}
		for (int i = 0; i < World.world.armies.list.Count; i++)
		{
			if (tShownNew > 2)
			{
				return;
			}
			Army tArmy = World.world.armies.list[i];
			if (tArmy.hasCaptain())
			{
				Actor tCaptain = tArmy.getCaptain();
				if (!tCaptain.isInMagnet() && tCaptain.current_zone.visible && tCaptain.isKingdomCiv())
				{
					Kingdom tKingdom = tCaptain.kingdom;
					QuantumSpriteWithText tQSprite = (QuantumSpriteWithText)QuantumSpriteLibrary.drawQuantumSprite(pAsset, tCaptain.current_position, null, tKingdom, tCaptain.city, null, 1f, false, -1f);
					if (DebugConfig.isOn(DebugOption.ShowAmountNearArmy))
					{
						tQSprite.text.gameObject.SetActive(true);
						tQSprite.text.text = (tArmy.countUnits().ToString() ?? "");
						tQSprite.text.GetComponent<Renderer>().sortingLayerID = tQSprite.sprite_renderer.sortingLayerID;
						tQSprite.text.GetComponent<Renderer>().sortingOrder = tQSprite.sprite_renderer.sortingOrder;
					}
					else
					{
						tQSprite.text.gameObject.SetActive(false);
					}
					if (!pAsset.group_system.is_within_active_index)
					{
						tShownNew++;
					}
					Sprite tColoredSprite = DynamicSprites.getIcon(QuantumSpriteLibrary._flag_sprite, tKingdom.getColor());
					tQSprite.setSprite(tColoredSprite);
				}
			}
		}
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0009D638 File Offset: 0x0009B838
	private static QuantumSpriteArrows drawArrowQuantumSprite(QuantumSpriteAsset pAsset, Vector3 pStart, Vector3 pEnd, ref Color pColor, City pCity = null)
	{
		if (pStart.x == pEnd.x && pStart.y == pEnd.y)
		{
			return null;
		}
		float tDist = Toolbox.Dist(pStart.x, pStart.y, pEnd.x, pEnd.y);
		float tScale = pAsset.base_scale * QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
		if (pCity != null)
		{
			tScale *= pCity.mark_scale_effect;
		}
		tDist /= tScale;
		if (tDist < (float)pAsset.line_width)
		{
			return null;
		}
		float tAnimatedPos = QuantumSpriteManager.arrow_middle_current;
		if (!pAsset.arrow_animation)
		{
			tAnimatedPos = 0f;
		}
		QuantumSpriteArrows tQSprite = (QuantumSpriteArrows)pAsset.group_system.getNext();
		tQSprite.spriteArrowEnd.enabled = pAsset.render_arrow_end;
		tQSprite.spriteArrowStart.enabled = pAsset.render_arrow_start;
		if (tDist < (float)(pAsset.line_width + 2))
		{
			tQSprite.spriteArrowEnd.enabled = false;
		}
		if (tQSprite.spriteArrowEnd.enabled)
		{
			tQSprite.spriteArrowEnd.color = pColor;
			tQSprite.spriteArrowEnd.transform.localPosition = new Vector3(tDist, 0f, 0f);
		}
		if (tQSprite.spriteArrowStart.enabled)
		{
			tQSprite.spriteArrowStart.color = pColor;
		}
		tQSprite.spriteArrowMiddle.color = pColor;
		Vector3 tPos = pStart;
		tPos.z = (float)pAsset.group_system.countActive() * 0.001f;
		tQSprite.transform.position = tPos;
		float tAngle = Toolbox.getAngleDegrees(pStart.x, pStart.y, pEnd.x, pEnd.y);
		tQSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tAngle));
		float tSizeMiddle = tDist - tAnimatedPos;
		if (tQSprite.spriteArrowEnd.enabled)
		{
			tSizeMiddle -= 5f;
		}
		tQSprite.spriteArrowMiddle.size = new Vector2(tSizeMiddle, (float)pAsset.line_height);
		tQSprite.spriteArrowMiddle.transform.localPosition = new Vector3(tAnimatedPos, 0f, 0f);
		tQSprite.transform.localScale = new Vector3(tScale, tScale, 1f);
		return tQSprite;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0009D850 File Offset: 0x0009BA50
	private static QuantumSprite drawQuantumSprite(QuantumSpriteAsset pAsset, Vector3 pPos, WorldTile pTileTarget = null, Kingdom pKingdom = null, City pCity = null, BattleContainer pBattle = null, float pModScale = 1f, bool pSetColor = false, float pForceScaleTo = -1f)
	{
		QuantumSprite tQSprite = pAsset.group_system.getNext();
		if (pSetColor)
		{
			tQSprite.setColor(ref Toolbox.color_white);
		}
		float tScale;
		if (pForceScaleTo == -1f)
		{
			tScale = pAsset.base_scale * pModScale;
			if (pAsset.flag_battle)
			{
				tScale = tScale * pBattle.timer * 0.2f;
			}
			if (pAsset.add_camera_zoom_multiplier)
			{
				tScale *= QuantumSpriteLibrary.getCameraScaleZoomMultiplier(pAsset);
			}
			if (pAsset.selected_city_scale)
			{
				if (pCity != null)
				{
					tScale *= pCity.mark_scale_effect;
				}
				else
				{
					tScale *= 0.5f;
				}
			}
		}
		else
		{
			tScale = pForceScaleTo;
		}
		tQSprite.set(ref pPos, tScale);
		return tQSprite;
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0009D8E4 File Offset: 0x0009BAE4
	private static QuantumSprite drawQuantumSprite(QuantumSpriteAsset pAsset, WorldTile pTile, WorldTile pTileTarget, Kingdom pKingdom = null, City pCity = null, BattleContainer pBattle = null)
	{
		if (pTile == null)
		{
			return null;
		}
		return QuantumSpriteLibrary.drawQuantumSprite(pAsset, pTile.posV3, pTileTarget, pKingdom, pCity, pBattle, 1f, false, -1f);
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0009D913 File Offset: 0x0009BB13
	private static float getCameraScaleZoomMultiplier(QuantumSpriteAsset pAsset)
	{
		return Mathf.Clamp(MoveCamera.instance.main_camera.orthographicSize / 30f, (float)pAsset.add_camera_zoom_multiplier_min, (float)pAsset.add_camera_zoom_multiplier_max);
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0009D93D File Offset: 0x0009BB3D
	private static Actor[] visible_units
	{
		get
		{
			return World.world.units.visible_units.array;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0009D953 File Offset: 0x0009BB53
	private static int visible_units_count
	{
		get
		{
			return World.world.units.visible_units.count;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0009D969 File Offset: 0x0009BB69
	private static Actor[] visible_units_alive
	{
		get
		{
			return World.world.units.visible_units_alive.array;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0009D97F File Offset: 0x0009BB7F
	private static int visible_units_alive_count
	{
		get
		{
			return World.world.units.visible_units_alive.count;
		}
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0009D998 File Offset: 0x0009BB98
	public void initDebugQuantumSpriteAssets()
	{
		QuantumSpriteAsset quantumSpriteAsset = new QuantumSpriteAsset();
		quantumSpriteAsset.id = "draw_money";
		quantumSpriteAsset.id_prefab = "p_mapSprite";
		quantumSpriteAsset.add_camera_zoom_multiplier = false;
		quantumSpriteAsset.debug_option = DebugOption.ShowMoneyIcons;
		quantumSpriteAsset.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawMoney);
		quantumSpriteAsset.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsTop");
			pQSprite.sprite_renderer.sprite = SpriteTextureLoader.getSprite("ui/Icons/iconResGold");
		};
		quantumSpriteAsset.render_gameplay = true;
		quantumSpriteAsset.default_amount = 10;
		this.add(quantumSpriteAsset);
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_settlers",
			id_prefab = "p_mapArrow_stroke",
			render_map = true,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsSettlers),
			debug_option = DebugOption.CivDrawSettleTarget
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_land_claim",
			id_prefab = "p_mapArrow_stroke",
			render_map = true,
			arrow_animation = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawClaimZone),
			debug_option = DebugOption.CivDrawCityClaimZone
		});
		this.add(new QuantumSpriteAsset
		{
			base_scale = 0.35f,
			id = "debug_kingdom_attack_targets",
			id_prefab = "p_mapArrow_stroke",
			render_arrow_end = true,
			render_arrow_start = true,
			arrow_animation = true,
			render_map = true,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsKingdomAttackTarget),
			debug_option = DebugOption.KingdomDrawAttackTarget
		});
		QuantumSpriteAsset quantumSpriteAsset2 = new QuantumSpriteAsset();
		quantumSpriteAsset2.id = "debug_unit_attack_range";
		quantumSpriteAsset2.id_prefab = "p_mapSprite";
		quantumSpriteAsset2.base_scale = 0.1f;
		quantumSpriteAsset2.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitAttackRange);
		quantumSpriteAsset2.debug_option = DebugOption.CursorUnitAttackRange;
		quantumSpriteAsset2.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			Sprite tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconWhiteCircle");
			pQSprite.setSprite(tSprite);
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 10;
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset2.render_gameplay = true;
		quantumSpriteAsset2.color = new Color(1f, 1f, 1f, 0.3f);
		this.add(quantumSpriteAsset2);
		QuantumSpriteAsset quantumSpriteAsset3 = new QuantumSpriteAsset();
		quantumSpriteAsset3.id = "debug_unit_attack_size";
		quantumSpriteAsset3.id_prefab = "p_mapSprite";
		quantumSpriteAsset3.base_scale = 0.1f;
		quantumSpriteAsset3.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawUnitSize);
		quantumSpriteAsset3.debug_option = DebugOption.CursorUnitSize;
		quantumSpriteAsset3.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			Sprite tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconWhiteCircle");
			pQSprite.setSprite(tSprite);
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 10;
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset3.render_gameplay = true;
		quantumSpriteAsset3.color = new Color(0.2f, 0.2f, 1f, 0.4f);
		this.add(quantumSpriteAsset3);
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_attack_targets",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitAttackTargets),
			debug_option = DebugOption.ArrowsUnitsAttackTargets,
			arrow_animation = true,
			render_gameplay = true,
			color = new Color(1f, 0f, 0f, 0.7f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_actor_targets",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitBehTarget),
			debug_option = DebugOption.ArrowUnitsBehActorTarget,
			arrow_animation = true,
			render_gameplay = true,
			color = new Color(1f, 1f, 0f, 0.7f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_navigation_targets",
			id_prefab = "p_mapArrow_stroke",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitNavigationTargets),
			debug_option = DebugOption.ArrowsUnitsNavigationTargets,
			arrow_animation = true,
			render_gameplay = true,
			color = new Color(0.9f, 0.9f, 0.9f, 0.5f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_height",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitHeight),
			debug_option = DebugOption.ArrowsUnitsHeight,
			render_gameplay = true,
			color = new Color(0f, 1f, 0f, 0.5f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_navigation_path",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitNavigationPath),
			debug_option = DebugOption.ArrowsUnitsPaths,
			render_gameplay = true,
			color = new Color(0f, 0f, 0f, 0.5f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_next_step_tile",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitNextStepTile),
			debug_option = DebugOption.ArrowsUnitsNextStepTile,
			render_gameplay = true,
			color = new Color(0.4f, 1f, 1f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_next_position",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitNextStepPosition),
			debug_option = DebugOption.ArrowsUnitsNextStepPosition,
			render_gameplay = true,
			color = new Color(0.4f, 0.4f, 1f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_arrows_units_current_position",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsUnitCurrentPosition),
			debug_option = DebugOption.ArrowsUnitsCurrentPosition,
			render_gameplay = true,
			color = new Color(0f, 1f, 0f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_boat_passenger_lines",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsBoatPassengers),
			debug_option = DebugOption.BoatPassengerLines,
			render_gameplay = true,
			color = new Color(1f, 1f, 0f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_boat_taxi_request",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsPassengerTaxiRequestTargets),
			debug_option = DebugOption.ActorGizmosBoatTaxiRequestTargets,
			render_gameplay = true,
			color = new Color(0f, 1f, 0f, 0.9f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_building_residents",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsBuildingResidents),
			debug_option = DebugOption.BuildingResidents,
			render_gameplay = true,
			color = new Color(1f, 1f, 0f, 0.3f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_lovers",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.08f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawArrowsLovers),
			debug_option = DebugOption.Lovers,
			render_gameplay = true,
			color = new Color(1f, 0f, 0f, 0.5f)
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_favorite_foods",
			id_prefab = "p_mapSprite",
			base_scale = 0.2f,
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawFavoriteFoods),
			debug_option = DebugOption.RenderFavoriteFoods,
			render_gameplay = true
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_show_kingdom_icons",
			id_prefab = "p_mapSprite",
			base_scale = 0.1f,
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawKingdomIcons),
			debug_option = DebugOption.ShowKingdomIcons,
			render_gameplay = true
		});
		this.add(new QuantumSpriteAsset
		{
			id = "debug_holding_items",
			id_prefab = "p_mapSprite",
			base_scale = 0.1f,
			add_camera_zoom_multiplier = false,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawHoldingFoods),
			debug_option = DebugOption.RenderHoldingResources,
			render_gameplay = true
		});
		QuantumSpriteAsset quantumSpriteAsset4 = new QuantumSpriteAsset();
		quantumSpriteAsset4.id = "debug_show_zones_mush";
		quantumSpriteAsset4.id_prefab = "p_mapZone";
		quantumSpriteAsset4.base_scale = 1f;
		quantumSpriteAsset4.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawMushInfection);
		quantumSpriteAsset4.debug_option = DebugOption.ShowMushInfection;
		quantumSpriteAsset4.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset4.render_map = true;
		quantumSpriteAsset4.add_camera_zoom_multiplier = false;
		quantumSpriteAsset4.color = Toolbox.makeColor("#FF5E6A", 0.2f);
		this.add(quantumSpriteAsset4);
		this.add(new QuantumSpriteAsset
		{
			id = "debug_show_highlighted_zones",
			id_prefab = "p_mapZone",
			base_scale = 1f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawDebugHighlightedZones),
			render_map = true,
			render_gameplay = true,
			add_camera_zoom_multiplier = false
		});
		QuantumSpriteAsset quantumSpriteAsset5 = new QuantumSpriteAsset();
		quantumSpriteAsset5.id = "debug_show_godfinger_tiles";
		quantumSpriteAsset5.id_prefab = "p_mapZone";
		quantumSpriteAsset5.base_scale = 0.15f;
		quantumSpriteAsset5.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawGodFingerTiles);
		quantumSpriteAsset5.debug_option = DebugOption.ShowGodFingerTargetting;
		quantumSpriteAsset5.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 0;
		};
		quantumSpriteAsset5.render_map = true;
		quantumSpriteAsset5.render_gameplay = true;
		quantumSpriteAsset5.add_camera_zoom_multiplier = false;
		this.add(quantumSpriteAsset5);
		QuantumSpriteAsset quantumSpriteAsset6 = new QuantumSpriteAsset();
		quantumSpriteAsset6.id = "debug_show_dragon_attack_tiles";
		quantumSpriteAsset6.id_prefab = "p_mapZone";
		quantumSpriteAsset6.base_scale = 0.15f;
		quantumSpriteAsset6.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawDragonAttackTiles);
		quantumSpriteAsset6.debug_option = DebugOption.ShowDragonTargetting;
		quantumSpriteAsset6.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 0;
		};
		quantumSpriteAsset6.render_map = true;
		quantumSpriteAsset6.render_gameplay = true;
		quantumSpriteAsset6.add_camera_zoom_multiplier = false;
		this.add(quantumSpriteAsset6);
		QuantumSpriteAsset quantumSpriteAsset7 = new QuantumSpriteAsset();
		quantumSpriteAsset7.id = "debug_show_swim_targets";
		quantumSpriteAsset7.id_prefab = "p_mapZone";
		quantumSpriteAsset7.base_scale = 0.15f;
		quantumSpriteAsset7.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.drawSwimTargets);
		quantumSpriteAsset7.debug_option = DebugOption.ShowSwimToIslandLogic;
		quantumSpriteAsset7.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.sprite_renderer.sortingLayerID = SortingLayer.NameToID("EffectsBack");
			pQSprite.sprite_renderer.sortingOrder = 0;
		};
		quantumSpriteAsset7.render_map = true;
		quantumSpriteAsset7.render_gameplay = true;
		quantumSpriteAsset7.add_camera_zoom_multiplier = false;
		this.add(quantumSpriteAsset7);
		QuantumSpriteAsset quantumSpriteAsset8 = new QuantumSpriteAsset();
		quantumSpriteAsset8.id = "debug_show_zones_zombie_infection";
		quantumSpriteAsset8.id_prefab = "p_mapZone";
		quantumSpriteAsset8.base_scale = 1f;
		quantumSpriteAsset8.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawZombieInfection);
		quantumSpriteAsset8.debug_option = DebugOption.ShowZombieInfection;
		quantumSpriteAsset8.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset8.render_map = true;
		quantumSpriteAsset8.add_camera_zoom_multiplier = false;
		quantumSpriteAsset8.color = Toolbox.makeColor("#3FC668", 0.2f);
		this.add(quantumSpriteAsset8);
		QuantumSpriteAsset quantumSpriteAsset9 = new QuantumSpriteAsset();
		quantumSpriteAsset9.id = "debug_show_zones_plague";
		quantumSpriteAsset9.id_prefab = "p_mapZone";
		quantumSpriteAsset9.base_scale = 1f;
		quantumSpriteAsset9.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawPlagueInfection);
		quantumSpriteAsset9.debug_option = DebugOption.ShowPlagueInfection;
		quantumSpriteAsset9.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset9.render_map = true;
		quantumSpriteAsset9.add_camera_zoom_multiplier = false;
		quantumSpriteAsset9.color = Toolbox.makeColor("#C444FF", 0.2f);
		this.add(quantumSpriteAsset9);
		QuantumSpriteAsset quantumSpriteAsset10 = new QuantumSpriteAsset();
		quantumSpriteAsset10.id = "debug_show_zones_curse";
		quantumSpriteAsset10.id_prefab = "p_mapZone";
		quantumSpriteAsset10.base_scale = 1f;
		quantumSpriteAsset10.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawCurseInfection);
		quantumSpriteAsset10.debug_option = DebugOption.ShowCursed;
		quantumSpriteAsset10.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset10.render_map = true;
		quantumSpriteAsset10.add_camera_zoom_multiplier = false;
		quantumSpriteAsset10.color = Toolbox.makeColor("#852EAD", 0.2f);
		this.add(quantumSpriteAsset10);
		QuantumSpriteAsset quantumSpriteAsset11 = new QuantumSpriteAsset();
		quantumSpriteAsset11.id = "debug_dead_units";
		quantumSpriteAsset11.id_prefab = "p_mapZone";
		quantumSpriteAsset11.base_scale = 0.2f;
		quantumSpriteAsset11.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawDeadUnits);
		quantumSpriteAsset11.debug_option = DebugOption.DeadUnits;
		quantumSpriteAsset11.create_object = delegate(QuantumSpriteAsset _, QuantumSprite pQSprite)
		{
			pQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconSkulls"));
		};
		quantumSpriteAsset11.render_map = true;
		quantumSpriteAsset11.render_gameplay = true;
		quantumSpriteAsset11.color = Toolbox.makeColor("#FFFFFF", 0.1f);
		this.add(quantumSpriteAsset11);
		this.add(new QuantumSpriteAsset
		{
			id = "debug_draw_bad_links",
			id_prefab = "p_mapArrow_line",
			base_scale = 0.4f,
			draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawBadLinks),
			debug_option = DebugOption.DrawBadLinksDiag,
			render_arrow_end = true,
			render_arrow_start = true,
			render_map = true,
			render_gameplay = true,
			color = Toolbox.makeColor("#D300B0", 0.8f)
		});
		QuantumSpriteAsset quantumSpriteAsset12 = new QuantumSpriteAsset();
		quantumSpriteAsset12.id = "debug_cursor_city_zone_range";
		quantumSpriteAsset12.id_prefab = "p_mapZone";
		quantumSpriteAsset12.base_scale = 1f;
		quantumSpriteAsset12.add_camera_zoom_multiplier = false;
		quantumSpriteAsset12.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugCityZoneRange);
		quantumSpriteAsset12.debug_option = DebugOption.CursorCityZoneRange;
		quantumSpriteAsset12.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset12.render_map = true;
		quantumSpriteAsset12.render_gameplay = true;
		quantumSpriteAsset12.color = Toolbox.makeColor("#00FF00", 0.5f);
		this.add(quantumSpriteAsset12);
		QuantumSpriteAsset quantumSpriteAsset13 = new QuantumSpriteAsset();
		quantumSpriteAsset13.id = "debug_enemy_finder";
		quantumSpriteAsset13.id_prefab = "p_mapSprite";
		quantumSpriteAsset13.base_scale = 0.2f;
		quantumSpriteAsset13.debug_option = DebugOption.CursorEnemyFinderChunks;
		quantumSpriteAsset13.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugEnemyFinder);
		quantumSpriteAsset13.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			Color.white.a = 0.8f;
			pQSprite.setSprite(SpriteTextureLoader.getSprite("ui/Icons/iconAccuracy"));
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset13.render_map = true;
		quantumSpriteAsset13.render_gameplay = true;
		this.add(quantumSpriteAsset13);
		QuantumSpriteAsset quantumSpriteAsset14 = new QuantumSpriteAsset();
		quantumSpriteAsset14.id = "debug_show_population";
		quantumSpriteAsset14.id_prefab = "p_mapZone";
		quantumSpriteAsset14.base_scale = 1f;
		quantumSpriteAsset14.draw_call = new QuantumSpriteUpdater(QuantumSpriteLibrary.debugDrawPopulation);
		quantumSpriteAsset14.debug_option = DebugOption.ShowPopulationTotal;
		quantumSpriteAsset14.create_object = delegate(QuantumSpriteAsset pAsset, QuantumSprite pQSprite)
		{
			pQSprite.setColor(ref pAsset.color);
		};
		quantumSpriteAsset14.render_map = true;
		quantumSpriteAsset14.add_camera_zoom_multiplier = false;
		quantumSpriteAsset14.color = Toolbox.makeColor("#FFFFFF", 0.1f);
		this.add(quantumSpriteAsset14);
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0009E938 File Offset: 0x0009CB38
	private static void drawMoney(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.isAlive() && (tActor.data.money != 0 || tActor.data.loot != 0))
			{
				Vector3 tPos = tActor.current_position;
				tPos.y += 1f;
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0009E9D8 File Offset: 0x0009CBD8
	private static void debugDrawArrowsSettlers(QuantumSpriteAsset pAsset)
	{
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0009E9DC File Offset: 0x0009CBDC
	private static void debugDrawClaimZone(QuantumSpriteAsset pAsset)
	{
		WorldTile tMouseTile = World.world.getMouseTilePos();
		City tMouseCity = null;
		if (tMouseTile != null && DebugConfig.isOn(DebugOption.ArrowsOnlyForCursorCities))
		{
			tMouseCity = tMouseTile.zone.city;
		}
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.hasKing() && tKingdom.king.isTask("claim_land"))
			{
				QuantumSpriteLibrary.checkDrawClaimLand(pAsset, tKingdom.king);
			}
			foreach (City tCity in tKingdom.getCities())
			{
				if ((tMouseCity == null || tCity == tMouseCity) && tCity.hasLeader() && tCity.leader.isTask("claim_land"))
				{
					QuantumSpriteLibrary.checkDrawClaimLand(pAsset, tCity.leader);
				}
			}
		}
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
	private static void checkDrawClaimLand(QuantumSpriteAsset pAsset, Actor pActor)
	{
		if (pActor.city.isRekt())
		{
			return;
		}
		WorldTile tTile = pActor.current_tile;
		WorldTile tTile2 = pActor.beh_tile_target;
		if (tTile == null || tTile2 == null)
		{
			return;
		}
		QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tTile.posV3, tTile2.posV3, ref Toolbox.color_yellow, null);
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0009EB30 File Offset: 0x0009CD30
	private static void debugDrawArrowsKingdomAttackTarget(QuantumSpriteAsset pAsset)
	{
		WorldTile tMouseTile = World.world.getMouseTilePos();
		City tMouseCity = null;
		if (tMouseTile != null && DebugConfig.isOn(DebugOption.ArrowsOnlyForCursorCities))
		{
			tMouseCity = tMouseTile.zone.city;
		}
		foreach (Kingdom kingdom in World.world.kingdoms)
		{
			foreach (City tCity in kingdom.getCities())
			{
				if (tCity.target_attack_city != null && (!Zones.showCityZones(false) || tMouseCity == null || tCity == tMouseCity))
				{
					WorldTile tTile = tCity.getTile(false);
					WorldTile tTile2 = tCity.target_attack_zone.centerTile;
					if (tTile != null && tTile2 != null)
					{
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tTile.posV3, tTile2.posV3, ref Toolbox.color_red, null);
					}
				}
			}
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0009EC30 File Offset: 0x0009CE30
	private static void drawUnitAttackRange(QuantumSpriteAsset pAsset)
	{
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor == null)
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		float tScale = tActor.getAttackRange() / 13f;
		QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_position, null, null, null, null, 1f, false, tScale).transform.position = tActor.current_position;
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0009EC98 File Offset: 0x0009CE98
	private static void drawUnitSize(QuantumSpriteAsset pAsset)
	{
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor == null)
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		float tScale = tActor.stats["size"] / 13f;
		QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_position, null, null, null, null, 1f, false, tScale).transform.position = tActor.current_position;
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0009ED00 File Offset: 0x0009CF00
	private static void debugDrawArrowsUnitAttackTargets(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units;
		int tLen = QuantumSpriteLibrary.visible_units_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.has_attack_target && (!tOnlyFav || tActor.isFavorite()) && tActor.isEnemyTargetAlive())
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.attack_target.current_position, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0009ED7C File Offset: 0x0009CF7C
	private static void debugDrawArrowsUnitBehTarget(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units;
		int tLen = QuantumSpriteLibrary.visible_units_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.beh_actor_target != null && (!tOnlyFav || tActor.isFavorite()) && tActor.beh_actor_target != null)
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.beh_actor_target.current_position, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0009EDF8 File Offset: 0x0009CFF8
	private static void debugDrawArrowsUnitNavigationTargets(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units;
		int tLen = QuantumSpriteLibrary.visible_units_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.tile_target != null && (!tOnlyFav || tActor.isFavorite()))
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.tile_target.posV3, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0009EE68 File Offset: 0x0009D068
	private static void debugDrawArrowsUnitHeight(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units;
		int tLen = QuantumSpriteLibrary.visible_units_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tOnlyFav || tActor.isFavorite())
			{
				Vector3 tPos = tActor.current_position;
				Vector3 tPosHeight = tActor.current_position;
				tPosHeight.y += tActor.getHeight();
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tPos, tPosHeight, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0009EEE8 File Offset: 0x0009D0E8
	private static void debugDrawArrowsUnitNavigationPath(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.current_path.Count != 0 && (!tOnlyFav || tActor.isFavorite()))
			{
				WorldTile tLastTile = null;
				foreach (WorldTile tTile in tActor.current_path)
				{
					if (tLastTile == null)
					{
						tLastTile = tTile;
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_tile.posV3, tLastTile.posV3, ref pAsset.color, null);
					}
					else
					{
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tLastTile.posV3, tTile.posV3, ref pAsset.color, null);
						tLastTile = tTile;
					}
				}
			}
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0009EFD0 File Offset: 0x0009D1D0
	private static void debugDrawArrowsUnitNextStepTile(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.debug_next_step_tile != null && (!DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly) || tActor.isFavorite()))
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.debug_next_step_tile.posV3, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0009F038 File Offset: 0x0009D238
	private static void debugDrawArrowsUnitNextStepPosition(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.is_moving && (!tOnlyFav || tActor.isFavorite()))
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.next_step_position, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
	private static void debugDrawArrowsUnitCurrentPosition(QuantumSpriteAsset pAsset)
	{
		bool tOnlyFav = DebugConfig.isOn(DebugOption.ArrowsUnitsFavoritesOnly);
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (!tOnlyFav || tActor.isFavorite())
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tActor.current_tile.posV3, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0009F10C File Offset: 0x0009D30C
	private static void debugDrawArrowsBoatPassengers(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tBoat = tArr[i];
			if (tBoat.asset.is_boat && tBoat.asset.is_boat_transport)
			{
				TaxiRequest tRequest = tBoat.getSimpleComponent<Boat>().taxi_request;
				if (tRequest != null)
				{
					foreach (Actor tRequestActor in tRequest.getActors())
					{
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tRequestActor.current_position, tBoat.current_tile.posV3, ref pAsset.color, null);
					}
				}
			}
		}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0009F1D0 File Offset: 0x0009D3D0
	private static void debugDrawArrowsPassengerTaxiRequestTargets(QuantumSpriteAsset pAsset)
	{
		Color tColorTarget = Color.cyan;
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tBoat = tArr[i];
			if (tBoat.asset.is_boat && tBoat.asset.is_boat_transport)
			{
				TaxiRequest tRequest = tBoat.getSimpleComponent<Boat>().taxi_request;
				if (tRequest != null)
				{
					foreach (Actor tRequestActor in tRequest.getActors())
					{
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tRequestActor.current_position, tRequest.getTileStart().posV3, ref pAsset.color, null);
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tRequestActor.current_position, tRequest.getTileTarget().posV3, ref tColorTarget, null);
					}
				}
			}
		}
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0009F2C4 File Offset: 0x0009D4C4
	private static void debugDrawArrowsBuildingResidents(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			Building tBuilding = tActor.getHomeBuilding();
			if (tBuilding != null)
			{
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tActor.current_position, tBuilding.current_tile.posV3, ref pAsset.color, null);
			}
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0009F320 File Offset: 0x0009D520
	private static void debugDrawArrowsLovers(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.hasLover() && tActor.data.created_time >= tActor.lover.data.created_time)
			{
				Actor tLover = tActor.lover;
				Vector3 tStartPos = tActor.current_position;
				tStartPos.y += 0.5f;
				Color tColor = pAsset.color;
				if (tActor.kingdom != tLover.kingdom)
				{
					tColor.a = 0.1f;
				}
				else if (tActor.city != tLover.city)
				{
					tColor.a = 0.2f;
				}
				else
				{
					tColor.a = 0.5f;
				}
				if (tActor.isKingdomCiv())
				{
					tColor.r = 1f;
					tColor.g = 0f;
					tColor.b = 0f;
				}
				else
				{
					tColor.r = 1f;
					tColor.g = 1f;
					tColor.b = 0f;
				}
				QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tStartPos, tLover.current_position, ref tColor, null);
			}
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0009F458 File Offset: 0x0009D658
	private static void debugDrawFavoriteFoods(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.hasFavoriteFood())
			{
				GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_position, null, null, null, null, 1f, false, -1f);
				ResourceAsset tAsset = tActor.favorite_food_asset;
				groupSpriteObject.setSprite(tAsset.getSpriteIcon());
			}
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0009F4C0 File Offset: 0x0009D6C0
	private static void debugDrawKingdomIcons(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.kingdom.asset.show_icon)
			{
				Vector3 tPos = tActor.current_position;
				tPos.y += 1f;
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f).setSprite(tActor.kingdom.asset.getSprite());
			}
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0009F544 File Offset: 0x0009D744
	private static void debugDrawHoldingFoods(QuantumSpriteAsset pAsset)
	{
		Actor[] tArr = QuantumSpriteLibrary.visible_units_alive;
		int tLen = QuantumSpriteLibrary.visible_units_alive_count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.isCarryingResources())
			{
				string tId = tActor.inventory.getItemIDToRender();
				if (!string.IsNullOrEmpty(tId))
				{
					Vector3 tVec = tActor.current_position;
					tVec.y += 2f;
					GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tVec, null, null, null, null, 1f, false, -1f);
					ResourceAsset tAssetResource = AssetManager.resources.get(tId);
					groupSpriteObject.setSprite(tAssetResource.getSpriteIcon());
				}
			}
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0009F5DC File Offset: 0x0009D7DC
	private static void debugDrawMushInfection(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.hasTrait("mush_spores"))
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0009F660 File Offset: 0x0009D860
	private static void drawDebugHighlightedZones(QuantumSpriteAsset pAsset)
	{
		if (DebugHighlight.hashset.Count == 0)
		{
			return;
		}
		foreach (DebugHighlightContainer tObj in DebugHighlight.hashset)
		{
			QuantumSprite tQSprite = null;
			if (tObj.zone != null)
			{
				tQSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
			else if (tObj.chunk != null)
			{
				tQSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj.chunk.tiles[0].zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
			Color tColor = tObj.color;
			tColor.a = tObj.timer / tObj.interval * tObj.color.a;
			tQSprite.setColor(ref tColor);
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0009F75C File Offset: 0x0009D95C
	private static void debugDrawGodFingerTiles(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.kingdoms_wild.get("godfinger").units)
		{
			if (tActor.isAlive())
			{
				GodFinger tFinger = tActor.getActorComponent<GodFinger>();
				Color tColor = tFinger.debug_color;
				tColor.a = 0.9f;
				foreach (WorldTile tTile in tFinger.target_tiles)
				{
					QuantumSpriteLibrary.drawQuantumSprite(pAsset, tTile.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
				}
				GodFinger.debug_trail(tFinger);
			}
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0009F84C File Offset: 0x0009DA4C
	private static void debugDrawDragonAttackTiles(QuantumSpriteAsset pAsset)
	{
		Kingdom tKingdom = World.world.kingdoms_wild.get("dragons");
		Kingdom tKingdom2 = World.world.kingdoms_wild.get("undead");
		if (tKingdom == null && tKingdom2 == null)
		{
			return;
		}
		if (tKingdom != null && tKingdom.units.Count > 0)
		{
			QuantumSpriteLibrary.debugDrawDragonAttackTiles(pAsset, tKingdom.units);
		}
		if (tKingdom2 != null && tKingdom2.units.Count > 0)
		{
			QuantumSpriteLibrary.debugDrawDragonAttackTiles(pAsset, tKingdom2.units);
		}
		foreach (WorldTile tObj in Toolbox.temp_list_tiles)
		{
			GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj.posV, null, null, null, null, 1f, false, -1f);
			Color tColor = Toolbox.color_mushSpores;
			tColor.a = 0.4f;
			groupSpriteObject.setColor(ref tColor);
		}
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0009F938 File Offset: 0x0009DB38
	private static void debugDrawDragonAttackTiles(QuantumSpriteAsset pAsset, List<Actor> pUnits)
	{
		foreach (Actor tActor in pUnits)
		{
			if (tActor.isAlive())
			{
				Dragon tDragonComponent = tActor.getActorComponent<Dragon>();
				if (!(tDragonComponent == null))
				{
					Color tColor = Toolbox.color_infected;
					float tAlpha = 0.1f + (float)tDragonComponent._landAttackCache * 0.1f;
					tColor.a = Mathf.Min(tAlpha, 0.8f);
					foreach (WorldTile tObj in tDragonComponent.getLandAttackTiles())
					{
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
					}
					tColor = Toolbox.color_phenotype_green_0;
					tAlpha = 0.1f + (float)tDragonComponent._slideAttackTilesFlipCache * 0.1f;
					tColor.a = Mathf.Min(tAlpha, 0.8f);
					foreach (WorldTile tObj2 in tDragonComponent._slideAttackTilesFlip)
					{
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj2.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
					}
					tColor = Toolbox.color_magenta_1;
					tAlpha = 0.1f + (float)tDragonComponent._slideAttackTilesNoFlipCache * 0.1f;
					tColor.a = Mathf.Min(tAlpha, 0.8f);
					foreach (WorldTile tObj3 in tDragonComponent._slideAttackTilesNoFlip)
					{
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObj3.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
					}
					tColor = Toolbox.color_red;
					if (tActor.tile_target != null)
					{
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.tile_target.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
					}
					tColor = Toolbox.color_heal;
					if (tActor.beh_tile_target != null)
					{
						QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.beh_tile_target.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
					}
				}
			}
		}
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0009FBF4 File Offset: 0x0009DDF4
	private static void drawSwimTargets(QuantumSpriteAsset pAsset)
	{
		Color tColor = Toolbox.color_infected;
		tColor.a = 0.8f;
		foreach (KeyValuePair<int, MapRegion> tBestReg in BehGoToStablePlace.bestRegions)
		{
			List<WorldTile> tTiles = tBestReg.Value.tiles;
			for (int i = 0; i < tTiles.Count; i++)
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tTiles[i].posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
			}
		}
		if (BehGoToStablePlace.best_tile == null)
		{
			return;
		}
		tColor = Toolbox.color_red;
		QuantumSpriteLibrary.drawQuantumSprite(pAsset, BehGoToStablePlace.best_tile.posV, null, null, null, null, 1f, false, -1f).setColor(ref tColor);
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0009FCD0 File Offset: 0x0009DED0
	private static void debugDrawZombieInfection(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.hasTrait("infected") || tActor.hasTrait("zombie"))
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0009FD60 File Offset: 0x0009DF60
	private static void debugDrawPlagueInfection(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.hasTrait("plague"))
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0009FDE4 File Offset: 0x0009DFE4
	private static void debugDrawCurseInfection(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.hasStatus("cursed"))
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0009FE68 File Offset: 0x0009E068
	private static void debugDrawDeadUnits(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (!tActor.isAlive())
			{
				QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_position, null, null, null, null, 1f, false, -1f);
			}
		}
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0009FEDC File Offset: 0x0009E0DC
	private static void debugDrawCitizenJobs(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.citizen_job != null && (DebugConfig.isOn(DebugOption.DrawCitizenJobIconsAll) || DebugConfig.isOn(tActor.citizen_job.debug_option)))
			{
				GroupSpriteObject groupSpriteObject = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_position, null, null, null, null, 1f, false, -1f);
				Sprite tSprite = SpriteTextureLoader.getSprite(tActor.citizen_job.path_icon);
				groupSpriteObject.setSprite(tSprite);
			}
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0009FF80 File Offset: 0x0009E180
	private static void debugDrawBadLinks(QuantumSpriteAsset pAsset)
	{
		MapChunk[] chunks = World.world.map_chunk_manager.chunks;
		for (int i = 0; i < chunks.Length; i++)
		{
			foreach (MapRegion tRegionMain in chunks[i].regions)
			{
				foreach (MapRegion tNeighbour in tRegionMain.neighbours)
				{
					if (Toolbox.Dist(tNeighbour.chunk.x, tNeighbour.chunk.y, tRegionMain.chunk.x, tRegionMain.chunk.y) >= 1.5f)
					{
						Vector3 tStart = tRegionMain.tiles[0].posV;
						Vector3 tEnd = tNeighbour.tiles[0].posV;
						QuantumSpriteLibrary.drawArrowQuantumSprite(pAsset, tStart, tEnd, ref pAsset.color, null);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x000A00B0 File Offset: 0x0009E2B0
	private static void debugCityZoneRange(QuantumSpriteAsset pAsset)
	{
		WorldTile tMouseTile = World.world.getMouseTilePos();
		if (tMouseTile == null)
		{
			return;
		}
		City tCity = tMouseTile.zone.city;
		if (tCity.isRekt())
		{
			return;
		}
		HashSet<TileZone> tZoneSet = new HashSet<TileZone>();
		Bench.bench("debugCityZoneRange", "meh", false);
		World.world.city_zone_helper.city_growth.getZoneToClaim(null, tCity, true, tZoneSet, 0);
		Debug.Log("bench city growth: " + Bench.benchEnd("debugCityZoneRange", "meh", false, 0L, false).ToString());
		foreach (TileZone tZone in tZoneSet)
		{
			QuantumSpriteLibrary.drawQuantumSprite(pAsset, tZone.centerTile.posV, null, null, null, null, 1f, false, -1f);
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x000A019C File Offset: 0x0009E39C
	private static void debugEnemyFinder(QuantumSpriteAsset pAsset)
	{
		if (World.world.getMouseTilePos() == null)
		{
			return;
		}
		Actor tActor = World.world.getActorNearCursor();
		if (tActor == null)
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		EnemyFinderData tEnemyData = EnemiesFinder.findEnemiesFrom(tActor.current_tile, tActor.kingdom, -1);
		if (tEnemyData.isEmpty())
		{
			return;
		}
		Vector2 tOffset = new Vector2(0f, 1f);
		foreach (BaseSimObject tObject in tEnemyData.list)
		{
			QuantumSpriteLibrary.drawQuantumSprite(pAsset, tObject.current_position + tOffset, null, null, null, null, 0.2f, false, -1f);
		}
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x000A0264 File Offset: 0x0009E464
	private static void debugDrawPopulation(QuantumSpriteAsset pAsset)
	{
		foreach (Actor tActor in World.world.units)
		{
			QuantumSpriteLibrary.drawQuantumSprite(pAsset, tActor.current_tile.zone.centerTile.posV, null, null, null, null, 1f, false, -1f);
		}
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x000A02DC File Offset: 0x0009E4DC
	private static void drawStockpileResources(QuantumSpriteAsset pAsset)
	{
		List<Building> tList = World.world.buildings.visible_stockpiles;
		if (tList.Count == 0)
		{
			return;
		}
		if (QuantumSpriteLibrary._array_stockpile_slots == null)
		{
			QuantumSpriteLibrary._array_stockpile_slots = new Vector2[35];
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int tIndex = i * 7 + j;
					QuantumSpriteLibrary._array_stockpile_slots[tIndex] = new Vector2((float)j, (float)i);
				}
			}
			QuantumSpriteLibrary._array_stockpile_slots.Shuffle<Vector2>();
		}
		foreach (Building tBuilding in tList)
		{
			if (tBuilding.is_visible && tBuilding.isUsable() && !tBuilding.isUnderConstruction())
			{
				QuantumSpriteLibrary.drawStockpileResourcesForBuilding(pAsset, tBuilding);
			}
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x000A03B0 File Offset: 0x0009E5B0
	private static void drawStockpileResourcesForBuilding(QuantumSpriteAsset pAsset, Building pBuilding)
	{
		float tGlobalBuildingScale = World.world.quality_changer.getTweenBuildingsValue();
		Color tColor = Toolbox.color_white;
		if (!pBuilding.hasCity())
		{
			tColor = Toolbox.color_abandoned_building;
		}
		Vector3 tTopLeftCorner = pBuilding.cur_transform_position;
		tTopLeftCorner.x += pBuilding.asset.stockpile_top_left_offset.x * tGlobalBuildingScale;
		tTopLeftCorner.y += pBuilding.asset.stockpile_top_left_offset.y * tGlobalBuildingScale;
		tTopLeftCorner.z = 0f;
		using (ListPool<SlotDrawAmount> tTempList = new ListPool<SlotDrawAmount>())
		{
			foreach (CityStorageSlot tSlot in pBuilding.resources.getSlots())
			{
				if (tSlot.amount != 0)
				{
					tTempList.Add(new SlotDrawAmount
					{
						resource_id = tSlot.id,
						amount = tSlot.amount / tSlot.asset.stack_size + 1
					});
				}
			}
			int tCurrentSlotIndex = 0;
			int tCurrentItemIndex = 0;
			while (tCurrentSlotIndex < 35 && tTempList.Count > 0)
			{
				SlotDrawAmount tResSlot = tTempList[tCurrentItemIndex];
				if (tResSlot.amount <= 0)
				{
					tTempList.RemoveAt(tCurrentItemIndex);
					if (tCurrentItemIndex >= tTempList.Count)
					{
						tCurrentItemIndex = 0;
					}
				}
				else
				{
					int tCurrentAmountLeft = tResSlot.amount;
					if (tCurrentAmountLeft <= 0)
					{
						break;
					}
					int tCurrentRow = (int)QuantumSpriteLibrary._array_stockpile_slots[tCurrentSlotIndex].x;
					int tCurrentCurrentColumn = (int)QuantumSpriteLibrary._array_stockpile_slots[tCurrentSlotIndex].y;
					ResourceAsset asset = tResSlot.asset;
					int tRenderAmountCurrent = tCurrentAmountLeft;
					Sprite tSprite = asset.getGameplaySprite();
					int tItemsInStackToDraw = Mathf.Clamp(tRenderAmountCurrent, 1, 7);
					if (tCurrentCurrentColumn % 2 != 0)
					{
						tItemsInStackToDraw--;
					}
					tResSlot.amount -= tItemsInStackToDraw;
					tTempList[tCurrentItemIndex] = tResSlot;
					for (int i = 0; i < tItemsInStackToDraw; i++)
					{
						QuantumSpriteLibrary.drawResourceIconOnStockpile(pAsset, tTopLeftCorner, tSprite, i, tCurrentRow, tCurrentCurrentColumn, ref tColor);
					}
					tCurrentSlotIndex++;
					tCurrentItemIndex++;
					if (tCurrentItemIndex >= tTempList.Count)
					{
						tCurrentItemIndex = 0;
					}
					if (tCurrentSlotIndex >= 35)
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x000A05F8 File Offset: 0x0009E7F8
	private static void drawResourceIconOnStockpile(QuantumSpriteAsset pAsset, Vector3 pMainPosition, Sprite pSprite, int pIndex, int pRow, int pColumn, ref Color pColor)
	{
		Vector3 tPos = pMainPosition;
		tPos.x += 0.58f * (float)pRow;
		tPos.y -= 0.5f * (float)pColumn;
		if (pColumn % 2 != 0)
		{
			tPos.x += 0.29f;
		}
		tPos.y += 0.4f * (float)pIndex;
		tPos.z += 0.5f * (float)pIndex;
		QuantumSprite quantumSprite = QuantumSpriteLibrary.drawQuantumSprite(pAsset, tPos, null, null, null, null, 1f, false, -1f);
		quantumSprite.setSprite(pSprite);
		quantumSprite.setColor(ref pColor);
	}

	// Token: 0x04000A3B RID: 2619
	public static QuantumSpriteAsset light_areas;

	// Token: 0x04000A3C RID: 2620
	private static readonly Sprite _sprite_pixel = SpriteTextureLoader.getSprite("effects/pixel_corner");

	// Token: 0x04000A3D RID: 2621
	private static readonly Sprite _sprite_attack_reload = SpriteTextureLoader.getSprite("ui/Icons/iconAttackReload");

	// Token: 0x04000A3E RID: 2622
	private static readonly Sprite _boat_sprite_small = SpriteTextureLoader.getSprite("civ/icons/minimap_boat_small");

	// Token: 0x04000A3F RID: 2623
	private static readonly Sprite _boat_sprite_big = SpriteTextureLoader.getSprite("civ/icons/minimap_boat_big");

	// Token: 0x04000A40 RID: 2624
	private static readonly Sprite[] _unexplored_sprites = SpriteTextureLoader.getSpriteList("effects/fx_unexplored", false);

	// Token: 0x04000A41 RID: 2625
	private static readonly Sprite[] _unit_selection_effect = SpriteTextureLoader.getSpriteList("effects/unit_selected_effect", false);

	// Token: 0x04000A42 RID: 2626
	private static readonly Sprite[] _unit_selection_effect_main = SpriteTextureLoader.getSpriteList("effects/unit_selected_effect_main", false);

	// Token: 0x04000A43 RID: 2627
	private static readonly Sprite[] _fire_sprites_1 = SpriteTextureLoader.getSpriteList("effects/fx_status_burning_t", false);

	// Token: 0x04000A44 RID: 2628
	private static readonly Sprite[] _fire_sprites_2 = SpriteTextureLoader.getSpriteList("effects/fx_status_burning_t_2", false);

	// Token: 0x04000A45 RID: 2629
	private static readonly Sprite[] _fire_sprites_3 = SpriteTextureLoader.getSpriteList("effects/fx_status_burning_t_3", false);

	// Token: 0x04000A46 RID: 2630
	private static readonly Sprite[][] _fire_sprites_sets = new Sprite[][]
	{
		QuantumSpriteLibrary._fire_sprites_1,
		QuantumSpriteLibrary._fire_sprites_2,
		QuantumSpriteLibrary._fire_sprites_3
	};

	// Token: 0x04000A47 RID: 2631
	private static readonly Sprite _king_sprite_normal = SpriteTextureLoader.getSprite("civ/icons/minimap_king_normal");

	// Token: 0x04000A48 RID: 2632
	private static readonly Sprite _king_sprite_angry = SpriteTextureLoader.getSprite("civ/icons/minimap_king_angry");

	// Token: 0x04000A49 RID: 2633
	private static readonly Sprite _king_sprite_surprised = SpriteTextureLoader.getSprite("civ/icons/minimap_king_surprised");

	// Token: 0x04000A4A RID: 2634
	private static readonly Sprite _king_sprite_happy = SpriteTextureLoader.getSprite("civ/icons/minimap_king_happy");

	// Token: 0x04000A4B RID: 2635
	private static readonly Sprite _king_sprite_sad = SpriteTextureLoader.getSprite("civ/icons/minimap_king_sad");

	// Token: 0x04000A4C RID: 2636
	private static readonly Sprite _leader_sprite_normal = SpriteTextureLoader.getSprite("civ/icons/minimap_leader_normal");

	// Token: 0x04000A4D RID: 2637
	private static readonly Sprite _leader_sprite_angry = SpriteTextureLoader.getSprite("civ/icons/minimap_leader_angry");

	// Token: 0x04000A4E RID: 2638
	private static readonly Sprite _leader_sprite_surprised = SpriteTextureLoader.getSprite("civ/icons/minimap_leader_surprised");

	// Token: 0x04000A4F RID: 2639
	private static readonly Sprite _leader_sprite_happy = SpriteTextureLoader.getSprite("civ/icons/minimap_leader_happy");

	// Token: 0x04000A50 RID: 2640
	private static readonly Sprite _leader_sprite_sad = SpriteTextureLoader.getSprite("civ/icons/minimap_leader_sad");

	// Token: 0x04000A51 RID: 2641
	private static readonly Sprite _flag_sprite = SpriteTextureLoader.getSprite("civ/icons/minimap_flag");

	// Token: 0x04000A52 RID: 2642
	public static double last_order_timestamp;

	// Token: 0x04000A53 RID: 2643
	private static int[] _q_render_indexes_units = new int[8192];

	// Token: 0x04000A54 RID: 2644
	private static int[] _q_render_indexes_shadows_units = new int[8192];

	// Token: 0x04000A55 RID: 2645
	private static int[] _q_render_indexes_shadows_buildings = new int[8192];

	// Token: 0x04000A56 RID: 2646
	private static int[] _q_render_indexes_sprites_fire = new int[4096];

	// Token: 0x04000A57 RID: 2647
	private static int[] _q_render_indexes_unit_items = new int[8192];

	// Token: 0x04000A58 RID: 2648
	private static readonly List<Vector3> _wars_pos_sword_main = new List<Vector3>();

	// Token: 0x04000A59 RID: 2649
	private static readonly List<Vector3> _wars_pos_shields_main = new List<Vector3>();

	// Token: 0x04000A5A RID: 2650
	private float _metas_fall_offset_timer;

	// Token: 0x04000A5B RID: 2651
	private MetaType _last_meta_type_metas;

	// Token: 0x04000A5C RID: 2652
	private const float STOCKPILE_ITEM_OFFSET = 0.4f;

	// Token: 0x04000A5D RID: 2653
	private const int STOCKPILE_MAX_STACKS = 7;

	// Token: 0x04000A5E RID: 2654
	private const int STOCKPILE_MAX_ROWS = 5;

	// Token: 0x04000A5F RID: 2655
	private const int STOCKPILE_MAX_COLUMNS = 7;

	// Token: 0x04000A60 RID: 2656
	private const float STOCKPILE_ROW_OFFSET = 0.58f;

	// Token: 0x04000A61 RID: 2657
	private const float STOCKPILE_COLUMN_OFFSET = 0.5f;

	// Token: 0x04000A62 RID: 2658
	private const float STOCKPILE_OFFSET_Z = 0.5f;

	// Token: 0x04000A63 RID: 2659
	private static Vector2[] _array_stockpile_slots;

	// Token: 0x04000A64 RID: 2660
	private const int MAX_SLOTS = 35;
}
