using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using strings;
using UnityEngine;

// Token: 0x02000070 RID: 112
[ObfuscateLiterals]
public class PowerLibrary : AssetLibrary<GodPower>
{
	// Token: 0x060003D5 RID: 981 RVA: 0x000232B8 File Offset: 0x000214B8
	public override void init()
	{
		base.init();
		this.addCivsClassic();
		this.addCivsAnimals();
		this.addMobs();
		this.addSpecial();
		this.addTerraformTiles();
		this.addDestruction();
		this.addClouds();
		this.addPrinters();
		this.addDrops();
		this.addWaypoints();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00023308 File Offset: 0x00021508
	private void addWaypoints()
	{
		this.clone("desire_alien_mold", "$template_drops$");
		this.t.name = "Alien Mold Desire";
		this.t.drop_id = this.t.id;
		this.clone("desire_computer", "$template_drops$");
		this.t.name = "Evil Computer Desire";
		this.t.drop_id = this.t.id;
		this.clone("desire_golden_egg", "$template_drops$");
		this.t.name = "Golden Egg Desire";
		this.t.drop_id = this.t.id;
		this.clone("desire_harp", "$template_drops$");
		this.t.name = "Ethereal Harp Desire";
		this.t.drop_id = this.t.id;
		this.clone("waypoint_alien_mold", "$template_drop_building$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Alien Mold";
		this.t.drop_id = this.t.id;
		this.clone("waypoint_computer", "$template_drop_building$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Evil Computer";
		this.t.drop_id = this.t.id;
		this.clone("waypoint_golden_egg", "$template_drop_building$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Golden Egg";
		this.t.drop_id = this.t.id;
		this.clone("waypoint_harp", "$template_drop_building$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Ethereal Harp";
		this.t.drop_id = this.t.id;
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00023530 File Offset: 0x00021730
	private void addTerraformTiles()
	{
		this.add(new GodPower
		{
			id = "$template_terraform_tiles$",
			draw_lines = true,
			terraform = true,
			type = PowerActionType.PowerDrawTile,
			mouse_hold_animation = MouseHoldAnimation.Draw,
			rank = PowerRank.Rank0_free,
			show_tool_sizes = true,
			unselect_when_window = true,
			hold_action = true,
			click_interval = 0f
		});
		this.t.click_action = new PowerActionWithID(this.cleanBurnedTile);
		GodPower t = this.t;
		t.click_action = (PowerActionWithID)Delegate.Combine(t.click_action, new PowerActionWithID(this.stopFire));
		GodPower t2 = this.t;
		t2.click_brush_action = (PowerActionWithID)Delegate.Combine(t2.click_brush_action, new PowerActionWithID(this.fmodDrawingSound));
		GodPower t3 = this.t;
		t3.click_brush_action = (PowerActionWithID)Delegate.Combine(t3.click_brush_action, new PowerActionWithID(this.loopWithCurrentBrush));
		GodPower t4 = this.t;
		t4.click_brush_action = (PowerActionWithID)Delegate.Combine(t4.click_brush_action, new PowerActionWithID(this.drawingCursorEffect));
		this.clone("$template_draw$", "$template_terraform_tiles$");
		GodPower t5 = this.t;
		t5.click_action = (PowerActionWithID)Delegate.Combine(t5.click_action, new PowerActionWithID(this.drawTiles));
		this.clone("$template_wall$", "$template_draw$");
		this.t.make_buildings_transparent = true;
		this.t.force_brush = "sqr_0";
		this.t.show_tool_sizes = false;
		this.t.click_action = new PowerActionWithID(this.cleanBurnedTile);
		GodPower t6 = this.t;
		t6.click_action = (PowerActionWithID)Delegate.Combine(t6.click_action, new PowerActionWithID(this.stopFire));
		GodPower t7 = this.t;
		t7.click_action = (PowerActionWithID)Delegate.Combine(t7.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t8 = this.t;
		t8.click_action = (PowerActionWithID)Delegate.Combine(t8.click_action, new PowerActionWithID(this.drawLifeEraser));
		GodPower t9 = this.t;
		t9.click_action = (PowerActionWithID)Delegate.Combine(t9.click_action, new PowerActionWithID(this.drawTiles));
		this.t.sound_drawing = "event:/SFX/POWERS/Mountains";
		this.clone("$template_eraser$", "$template_terraform_tiles$");
		this.t.click_action = new PowerActionWithID(this.flashPixel);
		this.clone("fuse", "$template_draw$");
		this.t.name = "Fuse";
		this.t.top_tile_type = "fuse";
		GodPower t10 = this.t;
		t10.click_action = (PowerActionWithID)Delegate.Combine(t10.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t11 = this.t;
		t11.click_action = (PowerActionWithID)Delegate.Combine(t11.click_action, new PowerActionWithID(this.flashPixel));
		this.t.sound_drawing = "event:/SFX/POWERS/Fuse";
		this.clone("tile_deep_ocean", "$template_draw$");
		this.t.name = "Deep Ocean";
		this.t.tile_type = "pit_deep_ocean";
		this.t.path_icon = "iconTileDeepOcean";
		this.t.sound_drawing = "event:/SFX/POWERS/Pit";
		GodPower t12 = this.t;
		t12.click_action = (PowerActionWithID)Delegate.Combine(t12.click_action, new PowerActionWithID(this.destroyBuildings));
		this.clone("tile_close_ocean", "$template_draw$");
		this.t.name = "Close Ocean";
		this.t.tile_type = "pit_close_ocean";
		this.t.path_icon = "iconTileCloseOcean";
		this.t.sound_drawing = "event:/SFX/POWERS/Pit";
		GodPower t13 = this.t;
		t13.click_action = (PowerActionWithID)Delegate.Combine(t13.click_action, new PowerActionWithID(this.destroyBuildings));
		this.clone("tile_shallow_waters", "$template_draw$");
		this.t.name = "Shallow Waters";
		this.t.tile_type = "pit_shallow_waters";
		this.t.path_icon = "iconTileShallowWater";
		this.t.sound_drawing = "event:/SFX/POWERS/Pit";
		GodPower t14 = this.t;
		t14.click_action = (PowerActionWithID)Delegate.Combine(t14.click_action, new PowerActionWithID(this.destroyBuildings));
		this.clone("tile_sand", "$template_draw$");
		this.t.name = "Sand";
		this.t.tile_type = "sand";
		this.t.path_icon = "iconTileSand";
		this.t.sound_drawing = "event:/SFX/POWERS/Sand";
		this.clone("tile_soil", "$template_draw$");
		this.t.name = "Soil";
		this.t.tile_type = "soil_low";
		this.t.path_icon = "iconTileSoil";
		this.t.sound_drawing = "event:/SFX/POWERS/SoilLow";
		this.clone("tile_high_soil", "$template_draw$");
		this.t.name = "Soil High";
		this.t.tile_type = "soil_high";
		this.t.path_icon = "iconTileHighSoil";
		this.t.sound_drawing = "event:/SFX/POWERS/SoilHigh";
		this.clone("tile_hills", "$template_draw$");
		this.t.name = "Hills";
		this.t.tile_type = "hills";
		this.t.path_icon = "iconTileHills";
		GodPower t15 = this.t;
		t15.click_action = (PowerActionWithID)Delegate.Combine(t15.click_action, new PowerActionWithID(this.destroyBuildings));
		this.t.sound_drawing = "event:/SFX/POWERS/Hills";
		this.clone("tile_mountains", "$template_draw$");
		this.t.name = "Mountains";
		this.t.tile_type = "mountains";
		this.t.path_icon = "iconTileMountains";
		GodPower t16 = this.t;
		t16.click_action = (PowerActionWithID)Delegate.Combine(t16.click_action, new PowerActionWithID(this.destroyBuildings));
		this.t.sound_drawing = "event:/SFX/POWERS/Mountains";
		this.clone("tile_summit", "$template_draw$");
		this.t.name = "Summit";
		this.t.tile_type = "summit";
		this.t.path_icon = "iconTileSummit";
		GodPower t17 = this.t;
		t17.click_action = (PowerActionWithID)Delegate.Combine(t17.click_action, new PowerActionWithID(this.destroyBuildings));
		this.t.sound_drawing = "event:/SFX/POWERS/Mountains";
		this.clone("wall_order", "$template_wall$");
		this.t.name = "Stone Wall";
		this.t.top_tile_type = "wall_order";
		this.t.path_icon = "iconWallOrder";
		this.clone("wall_evil", "$template_wall$");
		this.t.name = "Wall of Evil";
		this.t.top_tile_type = "wall_evil";
		this.t.path_icon = "iconWallEvil";
		this.clone("wall_ancient", "$template_wall$");
		this.t.name = "Ancient Wall";
		this.t.top_tile_type = "wall_ancient";
		this.t.path_icon = "iconWallAncient";
		this.clone("wall_wild", "$template_wall$");
		this.t.name = "Wooden Wall";
		this.t.top_tile_type = "wall_wild";
		this.t.path_icon = "iconWallWild";
		this.clone("wall_green", "$template_wall$");
		this.t.name = "Green Wall";
		this.t.top_tile_type = "wall_green";
		this.t.path_icon = "iconWallGreen";
		this.clone("wall_iron", "$template_wall$");
		this.t.name = "Iron Wall";
		this.t.top_tile_type = "wall_iron";
		this.t.path_icon = "iconWallIron";
		this.clone("wall_light", "$template_wall$");
		this.t.name = "Wall of Light";
		this.t.top_tile_type = "wall_light";
		this.t.path_icon = "iconWallLight";
		this.clone("shovel_plus", "$template_terraform_tiles$");
		this.t.name = "Shovel Plus";
		this.t.path_icon = "iconShovelPlus";
		GodPower t18 = this.t;
		t18.click_action = (PowerActionWithID)Delegate.Combine(t18.click_action, new PowerActionWithID(this.drawShovelPlus));
		GodPower t19 = this.t;
		t19.click_action = (PowerActionWithID)Delegate.Combine(t19.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t20 = this.t;
		t20.click_action = (PowerActionWithID)Delegate.Combine(t20.click_action, new PowerActionWithID(this.flashPixel));
		this.t.sound_drawing = "event:/SFX/POWERS/ShovelPlus";
		this.clone("shovel_minus", "$template_terraform_tiles$");
		this.t.name = "Shovel Minus";
		this.t.path_icon = "iconShovelMinus";
		GodPower t21 = this.t;
		t21.click_action = (PowerActionWithID)Delegate.Combine(t21.click_action, new PowerActionWithID(this.drawShovelMinus));
		GodPower t22 = this.t;
		t22.click_action = (PowerActionWithID)Delegate.Combine(t22.click_action, new PowerActionWithID(this.destroyBuildings));
		GodPower t23 = this.t;
		t23.click_action = (PowerActionWithID)Delegate.Combine(t23.click_action, new PowerActionWithID(this.flashPixel));
		this.t.sound_drawing = "event:/SFX/POWERS/ShovelMinus";
		this.clone("vortex", "$template_terraform_tiles$");
		this.t.name = "Vortex";
		this.t.path_icon = "iconVertex2";
		this.t.click_action = new PowerActionWithID(this.stopFire);
		this.t.sound_drawing = "event:/SFX/POWERS/Vortex";
		GodPower t24 = this.t;
		t24.click_brush_action = (PowerActionWithID)Delegate.Combine(t24.click_brush_action, new PowerActionWithID(this.useVortex));
		this.clone("grey_goo", "$template_eraser$");
		this.t.name = "Grey Goo";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		GodPower t25 = this.t;
		t25.click_action = (PowerActionWithID)Delegate.Combine(t25.click_action, new PowerActionWithID(this.drawGreyGoo));
		GodPower t26 = this.t;
		t26.click_action = (PowerActionWithID)Delegate.Combine(t26.click_action, new PowerActionWithID(this.stopFire));
		this.t.sound_drawing = "event:/SFX/POWERS/GreyGoo";
		this.t.tester_enabled = false;
		this.clone("conway", "$template_eraser$");
		this.t.name = "Conway game of Life1";
		GodPower t27 = this.t;
		t27.click_action = (PowerActionWithID)Delegate.Combine(t27.click_action, new PowerActionWithID(this.drawConway));
		GodPower t28 = this.t;
		t28.click_action = (PowerActionWithID)Delegate.Combine(t28.click_action, new PowerActionWithID(this.stopFire));
		this.t.sound_drawing = "event:/SFX/POWERS/Conway";
		this.clone("conway_inverse", "$template_eraser$");
		this.t.name = "Conway game of Life2";
		GodPower t29 = this.t;
		t29.click_action = (PowerActionWithID)Delegate.Combine(t29.click_action, new PowerActionWithID(this.drawConwayInverse));
		GodPower t30 = this.t;
		t30.click_action = (PowerActionWithID)Delegate.Combine(t30.click_action, new PowerActionWithID(this.stopFire));
		this.t.sound_drawing = "event:/SFX/POWERS/Conway";
		this.clone("finger", "$template_eraser$");
		this.t.name = "Finger";
		this.t.path_icon = "iconTileFinger";
		GodPower t31 = this.t;
		t31.click_action = (PowerActionWithID)Delegate.Combine(t31.click_action, new PowerActionWithID(this.drawFinger));
		GodPower t32 = this.t;
		t32.click_action = (PowerActionWithID)Delegate.Combine(t32.click_action, new PowerActionWithID(this.stopFire));
		GodPower t33 = this.t;
		t33.click_action = (PowerActionWithID)Delegate.Combine(t33.click_action, new PowerActionWithID(this.cleanBurnedTile));
		this.t.sound_drawing = "event:/SFX/POWERS/Finger";
		this.clone("life_eraser", "$template_eraser$");
		GodPower t34 = this.t;
		t34.click_action = (PowerActionWithID)Delegate.Combine(t34.click_action, new PowerActionWithID(this.drawLifeEraser));
		this.t.name = "Life Eraser";
		this.t.sound_drawing = "event:/SFX/POWERS/LifeEraser";
		this.clone("demolish", "$template_eraser$");
		GodPower t35 = this.t;
		t35.click_action = (PowerActionWithID)Delegate.Combine(t35.click_action, new PowerActionWithID(this.drawDemolish));
		this.t.name = "Demolish";
		this.t.sound_drawing = "event:/SFX/POWERS/Demolish";
		this.clone("scissors", "$template_eraser$");
		this.t.path_icon = "iconScissors";
		this.t.force_map_mode = MetaType.City;
		GodPower t36 = this.t;
		t36.click_action = (PowerActionWithID)Delegate.Combine(t36.click_action, new PowerActionWithID(this.drawScissors));
		this.t.name = "Scissors";
		this.t.sound_drawing = "event:/SFX/POWERS/Demolish";
		this.clone("border_brush", "$template_eraser$");
		this.t.path_icon = "iconBorderBrush";
		this.t.force_map_mode = MetaType.City;
		GodPower t37 = this.t;
		t37.click_action = (PowerActionWithID)Delegate.Combine(t37.click_action, new PowerActionWithID(this.drawBorderBrush));
		this.t.name = "Border Brush";
		this.t.sound_drawing = "event:/SFX/POWERS/Demolish";
		this.clone("sponge", "$template_eraser$");
		this.t.path_icon = "iconSponge";
		GodPower t38 = this.t;
		t38.click_brush_action = (PowerActionWithID)Delegate.Combine(t38.click_brush_action, new PowerActionWithID(this.removeClouds));
		GodPower t39 = this.t;
		t39.click_brush_action = (PowerActionWithID)Delegate.Combine(t39.click_brush_action, new PowerActionWithID(this.removeTornadoes));
		GodPower t40 = this.t;
		t40.click_action = (PowerActionWithID)Delegate.Combine(t40.click_action, new PowerActionWithID(this.removeBuildingsBySponge));
		GodPower t41 = this.t;
		t41.click_action = (PowerActionWithID)Delegate.Combine(t41.click_action, new PowerActionWithID(this.removeGoo));
		GodPower t42 = this.t;
		t42.click_action = (PowerActionWithID)Delegate.Combine(t42.click_action, new PowerActionWithID(this.cleanBurnedTile));
		GodPower t43 = this.t;
		t43.click_action = (PowerActionWithID)Delegate.Combine(t43.click_action, new PowerActionWithID(this.stopFire));
		this.t.name = "Sponge";
		this.t.sound_drawing = "event:/SFX/POWERS/Sponge";
		this.clone("sickle", "$template_eraser$");
		this.t.path_icon = "iconSickle";
		GodPower t44 = this.t;
		t44.click_action = (PowerActionWithID)Delegate.Combine(t44.click_action, new PowerActionWithID(this.drawSickle));
		this.t.name = "Sickle";
		this.t.sound_event = "event:/SFX/POWERS/Sickle";
		this.t.sound_drawing = "event:/SFX/POWERS/Sickle";
		this.clone("spade", "$template_eraser$");
		this.t.path_icon = "iconSpade";
		GodPower t45 = this.t;
		t45.click_action = (PowerActionWithID)Delegate.Combine(t45.click_action, new PowerActionWithID(this.drawSpade));
		this.t.name = "Spade";
		this.t.sound_drawing = "event:/SFX/POWERS/Spade";
		this.clone("axe", "$template_eraser$");
		this.t.path_icon = "iconAxe";
		GodPower t46 = this.t;
		t46.click_action = (PowerActionWithID)Delegate.Combine(t46.click_action, new PowerActionWithID(this.drawAxe));
		this.t.name = "Axe";
		this.t.sound_drawing = "event:/SFX/POWERS/Axe";
		this.clone("bucket", "$template_eraser$");
		this.t.path_icon = "iconBucket";
		GodPower t47 = this.t;
		t47.click_action = (PowerActionWithID)Delegate.Combine(t47.click_action, new PowerActionWithID(this.drawBucket));
		this.t.name = "Bucket";
		this.t.sound_drawing = "event:/SFX/POWERS/Bucket";
		this.clone("pickaxe", "$template_eraser$");
		this.t.path_icon = "iconPickaxe";
		GodPower t48 = this.t;
		t48.click_action = (PowerActionWithID)Delegate.Combine(t48.click_action, new PowerActionWithID(this.drawPickaxe));
		this.t.name = "Pickaxe";
		this.t.sound_drawing = "event:/SFX/POWERS/Pickaxe";
		this.clone("divine_light", "$template_eraser$");
		this.t.path_icon = "iconDivineLight";
		GodPower t49 = this.t;
		t49.click_brush_action = (PowerActionWithID)Delegate.Combine(t49.click_brush_action, new PowerActionWithID(this.divineLightFX));
		this.t.click_action = new PowerActionWithID(this.drawDivineLight);
		this.t.name = "Divine Light";
		this.t.show_tool_sizes = true;
		this.t.sound_drawing = "event:/SFX/POWERS/DivineLight";
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00024768 File Offset: 0x00022968
	private void addDrops()
	{
		this.add(new GodPower
		{
			id = "$template_drops$",
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			type = PowerActionType.PowerSpawnDrops,
			mouse_hold_animation = MouseHoldAnimation.Sprinkle
		});
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t = this.t;
		t.click_power_brush_action = (PowerAction)Delegate.Combine(t.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		GodPower t2 = this.t;
		t2.click_power_action = (PowerAction)Delegate.Combine(t2.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.t.surprises_units = false;
		this.clone("paint", "$template_drops$");
		this.t.name = "Paint";
		this.t.force_map_mode = MetaType.City;
		this.t.drop_id = this.t.id;
		this.clone("dust_white", "$template_drops$");
		this.t.name = "White Dust";
		this.t.drop_id = this.t.id;
		this.clone("dust_black", "$template_drops$");
		this.t.name = "Black Dust";
		this.t.drop_id = this.t.id;
		this.clone("dust_red", "$template_drops$");
		this.t.name = "Red Dust";
		this.t.drop_id = this.t.id;
		this.clone("dust_blue", "$template_drops$");
		this.t.name = "Blue Dust";
		this.t.drop_id = this.t.id;
		this.clone("dust_gold", "$template_drops$");
		this.t.name = "Gold Dust";
		this.t.drop_id = this.t.id;
		this.clone("dust_purple", "$template_drops$");
		this.t.name = "Purple Dust";
		this.t.drop_id = this.t.id;
		this.clone("$template_explosive_tiles$", "$template_drops$");
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t3 = this.t;
		t3.click_power_brush_action = (PowerAction)Delegate.Combine(t3.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.clone("tnt", "$template_explosive_tiles$");
		this.t.name = "tnt";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/Tnt";
		this.clone("tnt_timed", "$template_explosive_tiles$");
		this.t.name = "tnt_timed";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/TntTimed";
		this.clone("water_bomb", "$template_explosive_tiles$");
		this.t.name = "Water Bomb";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/WaterBomb";
		this.clone("landmine", "$template_explosive_tiles$");
		this.t.name = "Landmine";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/LandMine";
		this.clone("fireworks", "$template_explosive_tiles$");
		this.t.name = "Fireworks";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/Fireworks";
		this.clone("inspiration", "$template_drops$");
		this.t.force_map_mode = MetaType.City;
		this.t.name = "Inspiration";
		this.t.drop_id = this.t.id;
		this.t.path_icon = "iconInspiration";
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Inspiration";
		this.clone("discord", "$template_drops$");
		this.t.force_map_mode = MetaType.Alliance;
		this.t.name = "Discord";
		this.t.drop_id = this.t.id;
		this.t.path_icon = "iconDiscord";
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Inspiration";
		this.clone("friendship", "$template_drops$");
		this.t.force_map_mode = MetaType.Kingdom;
		this.t.name = "Friendship";
		this.t.path_icon = "iconFriendship";
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Friendship";
		this.clone("spite", "$template_drops$");
		this.t.force_map_mode = MetaType.Kingdom;
		this.t.name = "Spite";
		this.t.path_icon = "iconSprite";
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Spite";
		this.clone("madness", "$template_drops$");
		this.t.name = "Madness";
		this.t.falling_chance = 0.01f;
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/Madness";
		this.clone("blessing", "$template_drops$");
		this.t.name = "Blessing";
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Blessing";
		this.clone("shield", "$template_drops$");
		this.t.name = "Shield";
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Shield";
		this.clone("curse", "$template_drops$");
		this.t.name = "Curse";
		this.t.rank = PowerRank.Rank0_free;
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.sound_drawing = "event:/SFX/POWERS/Curse";
		this.clone("zombie_infection", "$template_drops$");
		this.t.name = "Zombie Infection";
		this.t.falling_chance = 0.01f;
		this.t.rank = PowerRank.Rank3_good;
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.sound_drawing = "event:/SFX/POWERS/ZombieInfection";
		this.clone("mush_spores", "$template_drops$");
		this.t.name = "Mush Spores";
		this.t.falling_chance = 0.01f;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.sound_drawing = "event:/SFX/POWERS/MushSpores";
		this.clone("coffee", "$template_drops$");
		this.t.name = "Coffee";
		this.t.falling_chance = 0.01f;
		this.t.rank = PowerRank.Rank1_common;
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.sound_drawing = "event:/SFX/POWERS/Coffee";
		this.clone("powerup", "$template_drops$");
		this.t.name = "Powerup";
		this.t.falling_chance = 0.01f;
		this.t.rank = PowerRank.Rank1_common;
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.sound_drawing = "event:/SFX/POWERS/Powerup";
		this.clone("plague", "$template_drops$");
		this.t.name = "Plague";
		this.t.drop_id = this.t.id;
		this.t.falling_chance = 0.01f;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.sound_drawing = "event:/SFX/POWERS/Plague";
		this.clone("living_plants", "$template_drops$");
		this.t.name = "Living Plants";
		this.t.drop_id = this.t.id;
		this.t.actor_asset_id = "living_plants";
		this.t.falling_chance = 0.01f;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.sound_drawing = "event:/SFX/POWERS/LivingPlants";
		this.t.surprises_units = true;
		this.clone("living_house", "$template_drops$");
		this.t.name = "Living Houses";
		this.t.drop_id = this.t.id;
		this.t.actor_asset_id = "living_house";
		this.t.falling_chance = 0.01f;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.sound_drawing = "event:/SFX/POWERS/LivingHouses";
		this.t.surprises_units = true;
		this.clone("$template_bombs$", "$template_drops$");
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t4 = this.t;
		t4.click_power_brush_action = (PowerAction)Delegate.Combine(t4.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.clone("bomb", "$template_bombs$");
		this.t.name = "Bomb";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/Bomb";
		this.t.surprises_units = true;
		this.clone("grenade", "bomb");
		this.t.name = "Grenade";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/Grenade";
		this.clone("napalm_bomb", "bomb");
		this.t.name = "Napalm Bomb";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/NapalmBomb";
		this.clone("atomic_bomb", "$template_bombs$");
		this.t.name = "Atomic Bomb";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.sound_drawing = "event:/SFX/POWERS/AtomicBomb";
		this.t.surprises_units = true;
		this.clone("antimatter_bomb", "$template_bombs$");
		this.t.name = "Antimatter Bomb";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/AntimatterBomb";
		this.t.surprises_units = true;
		this.clone("czar_bomba", "$template_bombs$");
		this.t.name = "Tsar Bomba";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.sound_drawing = "event:/SFX/POWERS/TsarBomb";
		this.t.surprises_units = true;
		this.clone("crab_bomb", "bomb");
		this.t.name = "Crab Bomb";
		this.t.drop_id = this.t.id;
		this.t.sound_drawing = "event:/SFX/POWERS/CrabBomb";
		this.clone("rain", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Rain";
		this.t.falling_chance = 0.02f;
		this.t.sound_drawing = "event:/SFX/POWERS/Rain";
		this.clone("blood_rain", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Blood Rain";
		this.t.falling_chance = 0.02f;
		this.t.sound_drawing = "event:/SFX/POWERS/BloodRain";
		this.t.surprises_units = true;
		this.clone("clone_rain", "$template_drops$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.drop_id = this.t.id;
		this.t.name = "Clone Rain";
		this.t.falling_chance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t5 = this.t;
		t5.click_power_brush_action = (PowerAction)Delegate.Combine(t5.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/BloodRain";
		GodPower t6 = this.t;
		t6.click_power_action = (PowerAction)Delegate.Combine(t6.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.clone("dispel", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Dispel";
		this.t.falling_chance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t7 = this.t;
		t7.click_power_brush_action = (PowerAction)Delegate.Combine(t7.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/BloodRain";
		GodPower t8 = this.t;
		t8.click_power_action = (PowerAction)Delegate.Combine(t8.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.clone("sleep", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Sleep";
		this.t.falling_chance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t9 = this.t;
		t9.click_power_brush_action = (PowerAction)Delegate.Combine(t9.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/BloodRain";
		GodPower t10 = this.t;
		t10.click_power_action = (PowerAction)Delegate.Combine(t10.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.clone("jazz", "$template_drops$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.drop_id = this.t.id;
		this.t.name = "Smooth Jazz";
		this.t.falling_chance = 0.02f;
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t11 = this.t;
		t11.click_power_brush_action = (PowerAction)Delegate.Combine(t11.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/BloodRain";
		GodPower t12 = this.t;
		t12.click_power_action = (PowerAction)Delegate.Combine(t12.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.clone("fire", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Fire";
		this.t.falling_chance = 0.01f;
		this.t.particle_interval = 0.3f;
		this.t.sound_drawing = "event:/SFX/POWERS/Fire";
		this.t.surprises_units = true;
		this.clone("acid", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Acid";
		this.t.falling_chance = 0.02f;
		this.t.sound_drawing = "event:/SFX/POWERS/Acid";
		this.t.surprises_units = true;
		this.clone("lava", "$template_drops$");
		this.t.drop_id = this.t.id;
		this.t.name = "Lava";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.falling_chance = 0.03f;
		this.t.sound_drawing = "event:/SFX/POWERS/Lava";
		this.t.surprises_units = true;
		this.add(new GodPower
		{
			id = "$template_seeds$",
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			type = PowerActionType.PowerSpawnSeeds,
			mouse_hold_animation = MouseHoldAnimation.Sprinkle
		});
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t13 = this.t;
		t13.click_power_brush_action = (PowerAction)Delegate.Combine(t13.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		GodPower t14 = this.t;
		t14.click_power_action = (PowerAction)Delegate.Combine(t14.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.t.surprises_units = false;
		this.clone("seeds_grass", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Grass Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_savanna", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Savanna Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsSavanna";
		this.clone("seeds_enchanted", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Enchanted Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsEnchanted";
		this.clone("seeds_corrupted", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Corrupted Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsCorrupted";
		this.clone("seeds_mushroom", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Mushroom Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsMushroom";
		this.clone("seeds_swamp", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Swamp Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsSwamp";
		this.clone("seeds_infernal", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Infernal Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsInfernal";
		this.clone("seeds_jungle", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Jungle Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsJungle";
		this.clone("seeds_desert", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Desert Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsDesert";
		this.clone("seeds_lemon", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Lemon Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsLemon";
		this.clone("seeds_permafrost", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Permafrost Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsPermafrost";
		this.clone("seeds_candy", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Candy Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsCandy";
		this.clone("seeds_crystal", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Crystal Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsCrystal";
		this.clone("seeds_birch", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Birch Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_maple", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Maple Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_rocklands", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Rocklands Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_garlic", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Garlic Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_flower", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Flower Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_celestial", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Celestial Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_singularity", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Singularity Swamp Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_clover", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Clover Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("seeds_paradox", "$template_seeds$");
		this.t.drop_id = this.t.id;
		this.t.name = "Paradox Seeds";
		this.t.sound_drawing = "event:/SFX/POWERS/SeedsGrass";
		this.clone("$template_plants$", "$template_seeds$");
		this.t.type = PowerActionType.PowerSpawnDrops;
		this.clone("fruit_bush", "$template_plants$");
		this.t.type = PowerActionType.PowerSpawnDrops;
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t15 = this.t;
		t15.click_power_brush_action = (PowerAction)Delegate.Combine(t15.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.drop_id = this.t.id;
		this.t.name = "Fruit Bush";
		this.t.sound_drawing = "event:/SFX/POWERS/FruitBush";
		this.clone("fertilizer_plants", "$template_plants$");
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t16 = this.t;
		t16.click_power_brush_action = (PowerAction)Delegate.Combine(t16.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.drop_id = this.t.id;
		this.t.name = "Plants Fertilizer";
		this.t.sound_drawing = "event:/SFX/POWERS/FertilizerPlants";
		this.clone("fertilizer_trees", "$template_plants$");
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t17 = this.t;
		t17.click_power_brush_action = (PowerAction)Delegate.Combine(t17.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.drop_id = this.t.id;
		this.t.name = "Trees Fertilizer";
		this.t.sound_drawing = "event:/SFX/POWERS/FertilizerTrees";
		this.add(new GodPower
		{
			id = "$template_drop_building$",
			unselect_when_window = true,
			type = PowerActionType.PowerSpawnBuilding,
			mouse_hold_animation = MouseHoldAnimation.Sprinkle,
			force_brush = "circ_1",
			set_used_camera_drag_on_long_move = true
		});
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		GodPower t18 = this.t;
		t18.click_power_action = (PowerAction)Delegate.Combine(t18.click_power_action, new PowerAction(this.flashPixel));
		this.clone("$template_minerals$", "$template_drops$");
		this.t.falling_chance = 1f;
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsRandom);
		GodPower t19 = this.t;
		t19.click_power_brush_action = (PowerAction)Delegate.Combine(t19.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.clone("stone", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Stone";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("metals", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Ore Deposit";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("gold", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Gold";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("silver", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Silver";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("mythril", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Mythril";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("adamantine", "$template_minerals$");
		this.t.drop_id = this.t.id;
		this.t.name = "Adamantine";
		this.t.sound_drawing = "event:/SFX/POWERS/Minerals";
		this.clone("tumor", "$template_drop_building$");
		this.t.name = "Tumor";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("biomass", "$template_drop_building$");
		this.t.name = "Biomass";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("super_pumpkin", "$template_drop_building$");
		this.t.name = "Super Pumpkin";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("cybercore", "$template_drop_building$");
		this.t.name = "Cybercore";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("geyser", "$template_drop_building$");
		this.t.name = "Geyser";
		this.t.drop_id = this.t.id;
		this.clone("geyser_acid", "$template_drop_building$");
		this.t.name = "Acid Geyser";
		this.t.drop_id = this.t.id;
		this.clone("volcano", "$template_drop_building$");
		this.t.name = "Volcano";
		this.t.drop_id = this.t.id;
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.clone("golden_brain", "$template_drop_building$");
		this.t.name = "Golden Brain";
		this.t.drop_id = this.t.id;
		this.clone("monolith", "$template_drop_building$");
		this.t.name = "Monolith";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank5_noAwards;
		this.t.drop_id = this.t.id;
		this.clone("corrupted_brain", "$template_drop_building$");
		this.t.name = "Corrupted Brain";
		this.t.drop_id = this.t.id;
		this.clone("ice_tower", "$template_drop_building$");
		this.t.name = "Ice Tower";
		this.t.requires_premium = true;
		this.t.drop_id = this.t.id;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("beehive", "$template_drop_building$");
		this.t.name = "Beehive";
		this.t.drop_id = this.t.id;
		this.t.rank = PowerRank.Rank1_common;
		this.clone("flame_tower", "$template_drop_building$");
		this.t.name = "Flame Tower";
		this.t.requires_premium = true;
		this.t.drop_id = this.t.id;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("angle_tower", "$template_drop_building$");
		this.t.name = "Angle Tower";
		this.t.requires_premium = true;
		this.t.drop_id = this.t.id;
		this.t.rank = PowerRank.Rank5_noAwards;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000269A8 File Offset: 0x00024BA8
	private void addPrinters()
	{
		this.add(new GodPower
		{
			id = "$template_printer$",
			name = "Printer",
			unselect_when_window = true,
			actor_spawn_height = 3f,
			show_spawn_effect = true,
			actor_asset_id = "printer"
		});
		this.t.click_action = new PowerActionWithID(this.spawnPrinter);
		this.clone("printer_hexagon", "$template_printer$");
		this.t.printers_print = "hexagon";
		this.clone("printer_skull", "$template_printer$");
		this.t.printers_print = "skull";
		this.clone("printer_squares", "$template_printer$");
		this.t.printers_print = "squares";
		this.clone("printer_yinyang", "$template_printer$");
		this.t.printers_print = "yinyang";
		this.clone("printer_island1", "$template_printer$");
		this.t.printers_print = "island1";
		this.clone("printer_star", "$template_printer$");
		this.t.printers_print = "star";
		this.clone("printer_heart", "$template_printer$");
		this.t.printers_print = "heart";
		this.clone("printer_diamond", "$template_printer$");
		this.t.printers_print = "diamond";
		this.clone("printer_alien_drawing", "$template_printer$");
		this.t.printers_print = "aliendrawing";
		this.clone("printer_crater", "$template_printer$");
		this.t.printers_print = "crater";
		this.clone("printer_labyrinth", "$template_printer$");
		this.t.printers_print = "labyrinth";
		this.clone("printer_spiral", "$template_printer$");
		this.t.printers_print = "spiral";
		this.clone("printer_star_fort", "$template_printer$");
		this.t.printers_print = "starfort";
		this.clone("printer_code", "$template_printer$");
		this.t.printers_print = "code";
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00026BE0 File Offset: 0x00024DE0
	private void addClouds()
	{
		this.clone("cloud", "$template_spawn_special$");
		this.t.name = "Cloud of Life";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudOfLife);
		this.clone("cloud_rain", "$template_spawn_special$");
		this.t.name = "Rain Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudRain);
		this.clone("cloud_fire", "$template_spawn_special$");
		this.t.name = "Fire Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudFire);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.clone("cloud_lightning", "$template_spawn_special$");
		this.t.name = "Thunder Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudLightning);
		this.clone("cloud_ash", "$template_spawn_special$");
		this.t.name = "Ash Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudAsh);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.clone("cloud_magic", "$template_spawn_special$");
		this.t.name = "Magic Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudMagic);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.clone("cloud_rage", "$template_spawn_special$");
		this.t.name = "Rage Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudRage);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.clone("cloud_acid", "$template_spawn_special$");
		this.t.name = "Acid Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudAcid);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.clone("cloud_lava", "$template_spawn_special$");
		this.t.name = "Lava Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudLava);
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.clone("cloud_snow", "$template_spawn_special$");
		this.t.name = "Snow Cloud";
		this.t.multiple_spawn_tip = true;
		this.t.click_action = new PowerActionWithID(this.spawnCloudSnow);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00026F28 File Offset: 0x00025128
	private void addDestruction()
	{
		this.add(new GodPower
		{
			id = "$template_spawn_special$",
			name = "$template_spawn_special$",
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true
		});
		this.clone("force", "$template_spawn_special$");
		this.t.name = "Force";
		GodPower t = this.t;
		t.click_action = (PowerActionWithID)Delegate.Combine(t.click_action, new PowerActionWithID(this.spawnForce));
		this.clone("finger_flick", "$template_spawn_special$");
		this.t.show_close_actor = true;
		this.t.name = "finger_flick";
		GodPower t2 = this.t;
		t2.click_action = (PowerActionWithID)Delegate.Combine(t2.click_action, new PowerActionWithID(this.fingerFlick));
		this.add(new GodPower
		{
			id = "infinity_coin",
			name = "Infinity Coin",
			multiple_spawn_tip = true,
			set_used_camera_drag_on_long_move = true
		});
		GodPower t3 = this.t;
		t3.click_action = (PowerActionWithID)Delegate.Combine(t3.click_action, new PowerActionWithID(this.spawnInfinityCoin));
		this.add(new GodPower
		{
			id = "heatray",
			name = "Heatray",
			requires_premium = true,
			rank = PowerRank.Rank2_normal,
			force_brush = "circ_10",
			show_tool_sizes = false,
			unselect_when_window = true,
			hold_action = true
		});
		GodPower t4 = this.t;
		t4.click_brush_action = (PowerActionWithID)Delegate.Combine(t4.click_brush_action, new PowerActionWithID(this.heatrayFX));
		GodPower t5 = this.t;
		t5.click_action = (PowerActionWithID)Delegate.Combine(t5.click_action, new PowerActionWithID(this.drawHeatray));
		GodPower t6 = this.t;
		t6.click_action = (PowerActionWithID)Delegate.Combine(t6.click_action, new PowerActionWithID(this.flashPixel));
		this.add(new GodPower
		{
			id = "meteorite",
			name = "Meteorite",
			requires_premium = true,
			rank = PowerRank.Rank3_good,
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true,
			show_spawn_effect = true,
			multiple_spawn_tip = true
		});
		GodPower t7 = this.t;
		t7.click_action = (PowerActionWithID)Delegate.Combine(t7.click_action, new PowerActionWithID(this.spawnMeteorite));
		this.add(new GodPower
		{
			id = "bowling_ball",
			name = "Bowling Ball",
			requires_premium = true,
			rank = PowerRank.Rank2_normal,
			unselect_when_window = true,
			show_spawn_effect = true,
			hold_action = true,
			sound_drawing = "event:/SFX/POWERS/DivineMagnet",
			multiple_spawn_tip = true
		});
		GodPower t8 = this.t;
		t8.click_brush_action = (PowerActionWithID)Delegate.Combine(t8.click_brush_action, new PowerActionWithID(this.prepareBoulder));
		GodPower t9 = this.t;
		t9.click_brush_action = (PowerActionWithID)Delegate.Combine(t9.click_brush_action, new PowerActionWithID(this.fmodDrawingSound));
		this.add(new GodPower
		{
			id = "robot_santa",
			name = "Robot Santa",
			requires_premium = false,
			rank = PowerRank.Rank0_free,
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true,
			show_spawn_effect = true,
			multiple_spawn_tip = true
		});
		GodPower t10 = this.t;
		t10.click_action = (PowerActionWithID)Delegate.Combine(t10.click_action, new PowerActionWithID(this.spawnSanta));
		this.add(new GodPower
		{
			id = "lightning",
			name = "Lightning",
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true
		});
		GodPower t11 = this.t;
		t11.click_action = (PowerActionWithID)Delegate.Combine(t11.click_action, new PowerActionWithID(this.spawnLightning));
		this.add(new GodPower
		{
			id = "earthquake",
			name = "Earthquake",
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true
		});
		GodPower t12 = this.t;
		t12.click_action = (PowerActionWithID)Delegate.Combine(t12.click_action, new PowerActionWithID(this.spawnEarthquake));
		this.add(new GodPower
		{
			id = "tornado",
			name = "Tornado",
			requires_premium = true,
			rank = PowerRank.Rank3_good,
			unselect_when_window = true,
			set_used_camera_drag_on_long_move = true,
			show_spawn_effect = true,
			multiple_spawn_tip = true
		});
		GodPower t13 = this.t;
		t13.click_action = (PowerActionWithID)Delegate.Combine(t13.click_action, new PowerActionWithID(this.spawnTornado));
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000273CC File Offset: 0x000255CC
	private void addSpecial()
	{
		this.add(new GodPower
		{
			id = "temperature_plus",
			name = "Temperature",
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true
		});
		this.t.click_action = new PowerActionWithID(this.drawTemperaturePlus);
		GodPower t = this.t;
		t.click_action = (PowerActionWithID)Delegate.Combine(t.click_action, new PowerActionWithID(this.flashPixel));
		this.t.click_brush_action = new PowerActionWithID(this.loopWithCurrentBrush);
		GodPower t2 = this.t;
		t2.click_brush_action = (PowerActionWithID)Delegate.Combine(t2.click_brush_action, new PowerActionWithID(this.fmodDrawingSound));
		this.t.sound_drawing = "event:/SFX/POWERS/IncreaseTemperature";
		this.clone("temperature_minus", "temperature_plus");
		this.t.click_action = new PowerActionWithID(PowerLibrary.drawTemperatureMinus);
		GodPower t3 = this.t;
		t3.click_action = (PowerActionWithID)Delegate.Combine(t3.click_action, new PowerActionWithID(this.flashPixel));
		this.t.sound_drawing = "event:/SFX/POWERS/DecreaseTemperature";
		this.add(new GodPower
		{
			id = "magnet",
			name = "Magnet",
			show_tool_sizes = true,
			hold_action = true,
			highlight = true,
			sound_drawing = "event:/SFX/POWERS/DivineMagnet",
			unselect_when_window = true
		});
		this.t.click_brush_action = new PowerActionWithID(this.useMagnet);
		GodPower t4 = this.t;
		t4.click_brush_action = (PowerActionWithID)Delegate.Combine(t4.click_brush_action, new PowerActionWithID(this.flashBrushPixelsDuringClick));
		GodPower t5 = this.t;
		t5.click_brush_action = (PowerActionWithID)Delegate.Combine(t5.click_brush_action, new PowerActionWithID(this.fmodDrawingSound));
		this.add(new GodPower
		{
			id = "hide_ui",
			name = "hide_ui",
			path_icon = "iconHideUI",
			rank = PowerRank.Rank0_free
		});
		this.t.select_button_action = new PowerButtonClickAction(this.clickHideUI);
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		PowerLibrary.traits_gamma_rain_edit = this.add(new GodPower
		{
			id = "traits_gamma_rain_edit",
			name = "Gamma Rain",
			path_icon = "iconRainGammaEdit",
			requires_premium = true,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.select_button_action = new PowerButtonClickAction(this.clickTraitEditorRainButton);
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		PowerLibrary.traits_delta_rain_edit = this.add(new GodPower
		{
			id = "traits_delta_rain_edit",
			name = "Delta Rain",
			path_icon = "iconRainDeltaEdit",
			requires_premium = true,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.select_button_action = new PowerButtonClickAction(this.clickTraitEditorRainButton);
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		PowerLibrary.traits_omega_rain_edit = this.add(new GodPower
		{
			id = "traits_omega_rain_edit",
			name = "Omega Rain",
			path_icon = "iconRainOmegaEdit",
			requires_premium = true,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.select_button_action = new PowerButtonClickAction(this.clickTraitEditorRainButton);
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		this.add(new GodPower
		{
			id = "traits_gamma_rain",
			name = "Gamma Rain",
			path_icon = "iconRainGammaEdit",
			requires_premium = true,
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.drop_id = "gamma_rain";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t6 = this.t;
		t6.click_power_brush_action = (PowerAction)Delegate.Combine(t6.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/GammaRain";
		GodPower t7 = this.t;
		t7.click_power_action = (PowerAction)Delegate.Combine(t7.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.add(new GodPower
		{
			id = "traits_delta_rain",
			name = "Delta Rain",
			path_icon = "iconRainDeltaEdit",
			requires_premium = true,
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.drop_id = "delta_rain";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t8 = this.t;
		t8.click_power_brush_action = (PowerAction)Delegate.Combine(t8.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/DeltaRain";
		GodPower t9 = this.t;
		t9.click_power_action = (PowerAction)Delegate.Combine(t9.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.add(new GodPower
		{
			id = "traits_omega_rain",
			name = "Omega Rain",
			path_icon = "iconRainOmegaEdit",
			requires_premium = true,
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.drop_id = "omega_rain";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t10 = this.t;
		t10.click_power_brush_action = (PowerAction)Delegate.Combine(t10.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/OmegaRain";
		GodPower t11 = this.t;
		t11.click_power_action = (PowerAction)Delegate.Combine(t11.click_power_action, new PowerAction(this.fmodDrawingSound));
		PowerLibrary.equipment_rain_edit = this.add(new GodPower
		{
			id = "equipment_rain_edit",
			name = "Loot Rain",
			path_icon = "iconRainGammaEdit",
			requires_premium = true,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.select_button_action = new PowerButtonClickAction(this.clickEquipmentEditorRainButton);
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		this.add(new GodPower
		{
			id = "equipment_rain",
			name = "Loot Rain",
			path_icon = "iconRainLootEdit",
			requires_premium = true,
			hold_action = true,
			show_tool_sizes = true,
			unselect_when_window = true,
			falling_chance = 0.05f,
			rank = PowerRank.Rank5_noAwards
		});
		this.t.drop_id = "loot_rain";
		this.t.click_power_action = new PowerAction(this.spawnDrops);
		this.t.click_power_brush_action = new PowerAction(this.loopWithCurrentBrushPowerForDropsFull);
		GodPower t12 = this.t;
		t12.click_power_brush_action = (PowerAction)Delegate.Combine(t12.click_power_brush_action, new PowerAction(this.flashBrushPixelsDuringClick));
		this.t.sound_drawing = "event:/SFX/POWERS/GammaRain";
		GodPower t13 = this.t;
		t13.click_power_action = (PowerAction)Delegate.Combine(t13.click_power_action, new PowerAction(this.fmodDrawingSound));
		this.add(new GodPower
		{
			id = "city_select",
			name = "Select City",
			force_map_mode = MetaType.City,
			path_icon = "iconCityInspect",
			can_drag_map = true
		});
		this.t.tester_enabled = false;
		this.t.track_activity = false;
		GodPower t14 = this.t;
		t14.click_action = (PowerActionWithID)Delegate.Combine(t14.click_action, new PowerActionWithID(ActionLibrary.inspectCity));
		this.add(new GodPower
		{
			id = "relations",
			name = "Relations",
			force_map_mode = MetaType.Kingdom,
			path_icon = "iconDiplomacy",
			can_drag_map = true
		});
		GodPower t15 = this.t;
		t15.select_button_action = (PowerButtonClickAction)Delegate.Combine(t15.select_button_action, new PowerButtonClickAction(ActionLibrary.selectRelations));
		GodPower t16 = this.t;
		t16.click_special_action = (PowerActionWithID)Delegate.Combine(t16.click_special_action, new PowerActionWithID(ActionLibrary.clickRelations));
		this.t.tester_enabled = false;
		this.add(new GodPower
		{
			id = "whisper_of_war",
			name = "Whisper of War",
			force_map_mode = MetaType.Kingdom,
			path_icon = "iconWhisperOfWar",
			can_drag_map = true
		});
		GodPower t17 = this.t;
		t17.select_button_action = (PowerButtonClickAction)Delegate.Combine(t17.select_button_action, new PowerButtonClickAction(ActionLibrary.selectWhisperOfWar));
		GodPower t18 = this.t;
		t18.click_special_action = (PowerActionWithID)Delegate.Combine(t18.click_special_action, new PowerActionWithID(ActionLibrary.clickWhisperOfWar));
		this.t.tester_enabled = false;
		this.add(new GodPower
		{
			id = "unity",
			name = "unity",
			force_map_mode = MetaType.Alliance,
			path_icon = "iconUnity",
			can_drag_map = true
		});
		GodPower t19 = this.t;
		t19.select_button_action = (PowerButtonClickAction)Delegate.Combine(t19.select_button_action, new PowerButtonClickAction(ActionLibrary.selectUnity));
		GodPower t20 = this.t;
		t20.click_special_action = (PowerActionWithID)Delegate.Combine(t20.click_special_action, new PowerActionWithID(ActionLibrary.clickUnity));
		this.t.tester_enabled = false;
		PowerLibrary.inspect_unit = this.add(new GodPower
		{
			id = "inspect",
			name = "inspect",
			can_drag_map = true,
			set_used_camera_drag_on_long_move = true
		});
		this.t.tester_enabled = false;
		GodPower t21 = this.t;
		t21.click_action = (PowerActionWithID)Delegate.Combine(t21.click_action, new PowerActionWithID(ActionLibrary.inspectUnit));
		this.t.allow_unit_selection = true;
		this.add(new GodPower
		{
			id = "map_names",
			name = "Map Names",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_names";
		GodPower t22 = this.t;
		t22.toggle_action = (PowerToggleAction)Delegate.Combine(t22.toggle_action, new PowerToggleAction(this.toggleMultiOption));
		this.add(new GodPower
		{
			id = "map_layers",
			name = "map_layers",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_layers";
		GodPower t23 = this.t;
		t23.toggle_action = (PowerToggleAction)Delegate.Combine(t23.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "map_species_families",
			name = "map_species_families",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_species_families";
		GodPower t24 = this.t;
		t24.toggle_action = (PowerToggleAction)Delegate.Combine(t24.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "city_layer",
			name = "city_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_city_layer";
		GodPower t25 = this.t;
		t25.toggle_action = (PowerToggleAction)Delegate.Combine(t25.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "culture_layer",
			name = "culture_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_culture_layer";
		GodPower t26 = this.t;
		t26.toggle_action = (PowerToggleAction)Delegate.Combine(t26.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "subspecies_layer",
			name = "subspecies_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_subspecies_layer";
		GodPower t27 = this.t;
		t27.toggle_action = (PowerToggleAction)Delegate.Combine(t27.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "family_layer",
			name = "family_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_family_layer";
		GodPower t28 = this.t;
		t28.toggle_action = (PowerToggleAction)Delegate.Combine(t28.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "language_layer",
			name = "language_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_language_layer";
		GodPower t29 = this.t;
		t29.toggle_action = (PowerToggleAction)Delegate.Combine(t29.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "religion_layer",
			name = "religion_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_religion_layer";
		GodPower t30 = this.t;
		t30.toggle_action = (PowerToggleAction)Delegate.Combine(t30.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "clan_layer",
			name = "clan_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_clan_layer";
		GodPower t31 = this.t;
		t31.toggle_action = (PowerToggleAction)Delegate.Combine(t31.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "kingdom_layer",
			name = "kingdom_layer",
			unselect_when_window = true,
			multi_toggle = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_kingdom_layer";
		GodPower t32 = this.t;
		t32.toggle_action = (PowerToggleAction)Delegate.Combine(t32.toggle_action, new PowerToggleAction(this.toggleOptionZone));
		this.add(new GodPower
		{
			id = "alliance_layer",
			name = "alliance_layer",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_alliance_layer";
		GodPower t33 = this.t;
		t33.toggle_action = (PowerToggleAction)Delegate.Combine(t33.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "army_layer",
			name = "army_layer",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.map_modes_switch = true;
		this.t.toggle_name = "map_army_layer";
		GodPower t34 = this.t;
		t34.toggle_action = (PowerToggleAction)Delegate.Combine(t34.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "map_kings_leaders",
			name = "map_kings_leaders",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "map_kings_leaders";
		GodPower t35 = this.t;
		t35.toggle_action = (PowerToggleAction)Delegate.Combine(t35.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_favorites",
			name = "marks_favorites",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_favorites";
		GodPower t36 = this.t;
		t36.toggle_action = (PowerToggleAction)Delegate.Combine(t36.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_favorite_items",
			name = "marks_favorite_items",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_favorite_items";
		GodPower t37 = this.t;
		t37.toggle_action = (PowerToggleAction)Delegate.Combine(t37.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_armies",
			name = "Show Armies",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_armies";
		GodPower t38 = this.t;
		t38.toggle_action = (PowerToggleAction)Delegate.Combine(t38.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_battles",
			name = "Show Battles",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_battles";
		GodPower t39 = this.t;
		t39.toggle_action = (PowerToggleAction)Delegate.Combine(t39.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_plots",
			name = "Plot Icons",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_plots";
		GodPower t40 = this.t;
		t40.toggle_action = (PowerToggleAction)Delegate.Combine(t40.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_wars",
			name = "War Icons",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_wars";
		GodPower t41 = this.t;
		t41.toggle_action = (PowerToggleAction)Delegate.Combine(t41.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "highlight_kingdom_enemies",
			name = "Highlight Kingdom Enemies",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "highlight_kingdom_enemies";
		GodPower t42 = this.t;
		t42.toggle_action = (PowerToggleAction)Delegate.Combine(t42.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "only_favorited_meta",
			name = "only_favorited_meta",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "only_favorited_meta";
		GodPower t43 = this.t;
		t43.toggle_action = (PowerToggleAction)Delegate.Combine(t43.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "unit_metas",
			name = "unit_metas",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "unit_metas";
		GodPower t44 = this.t;
		t44.toggle_action = (PowerToggleAction)Delegate.Combine(t44.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "money_flow",
			name = "money_flow",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "money_flow";
		GodPower t45 = this.t;
		t45.toggle_action = (PowerToggleAction)Delegate.Combine(t45.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "meta_conversions",
			name = "meta_conversions",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "meta_conversions";
		GodPower t46 = this.t;
		t46.toggle_action = (PowerToggleAction)Delegate.Combine(t46.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "talk_bubbles",
			name = "talk_bubbles",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "talk_bubbles";
		GodPower t47 = this.t;
		t47.toggle_action = (PowerToggleAction)Delegate.Combine(t47.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "icons_happiness",
			name = "icons_happiness",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "icons_happiness";
		GodPower t48 = this.t;
		t48.toggle_action = (PowerToggleAction)Delegate.Combine(t48.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "icons_tasks",
			name = "icons_tasks",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "icons_tasks";
		GodPower t49 = this.t;
		t49.toggle_action = (PowerToggleAction)Delegate.Combine(t49.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "army_targets",
			name = "Army Targets",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "army_targets";
		GodPower t50 = this.t;
		t50.toggle_action = (PowerToggleAction)Delegate.Combine(t50.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "tooltip_zones",
			name = "Tooltip Zones",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "tooltip_zones";
		GodPower t51 = this.t;
		t51.toggle_action = (PowerToggleAction)Delegate.Combine(t51.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "tooltip_units",
			name = "Tooltip Units",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "tooltip_units";
		GodPower t52 = this.t;
		t52.toggle_action = (PowerToggleAction)Delegate.Combine(t52.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_destination",
			name = "cursor_arrow_destination",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_destination";
		GodPower t53 = this.t;
		t53.toggle_action = (PowerToggleAction)Delegate.Combine(t53.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_lover",
			name = "cursor_arrow_lover",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_lover";
		GodPower t54 = this.t;
		t54.toggle_action = (PowerToggleAction)Delegate.Combine(t54.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_house",
			name = "cursor_arrow_house",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_house";
		GodPower t55 = this.t;
		t55.toggle_action = (PowerToggleAction)Delegate.Combine(t55.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_family",
			name = "cursor_arrow_family",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_family";
		GodPower t56 = this.t;
		t56.toggle_action = (PowerToggleAction)Delegate.Combine(t56.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_parents",
			name = "cursor_arrow_parents",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_parents";
		GodPower t57 = this.t;
		t57.toggle_action = (PowerToggleAction)Delegate.Combine(t57.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_kids",
			name = "cursor_arrow_kids",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_kids";
		GodPower t58 = this.t;
		t58.toggle_action = (PowerToggleAction)Delegate.Combine(t58.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "cursor_arrow_attack_target",
			name = "cursor_arrow_attack_target",
			unselect_when_window = true
		});
		this.t.disabled_on_mobile = true;
		this.t.tester_enabled = false;
		this.t.toggle_name = "cursor_arrow_attack_target";
		GodPower t59 = this.t;
		t59.toggle_action = (PowerToggleAction)Delegate.Combine(t59.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "marks_boats",
			name = "marks_boats",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "marks_boats";
		GodPower t60 = this.t;
		t60.toggle_action = (PowerToggleAction)Delegate.Combine(t60.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "history_log",
			name = "History Log",
			unselect_when_window = true
		});
		this.t.tester_enabled = false;
		this.t.toggle_name = "history_log";
		GodPower t61 = this.t;
		t61.toggle_action = (PowerToggleAction)Delegate.Combine(t61.toggle_action, new PowerToggleAction(this.toggleOption));
		this.add(new GodPower
		{
			id = "pause",
			name = "Pause",
			unselect_when_window = true,
			can_drag_map = true
		});
		this.t.tester_enabled = false;
		this.t.activate_on_hotkey_select = false;
		this.add(new GodPower
		{
			id = "clock",
			name = "Clock",
			unselect_when_window = true,
			requires_premium = true,
			ignore_cursor_icon = true,
			rank = PowerRank.Rank0_free,
			can_drag_map = true
		});
		this.t.tester_enabled = false;
		this.t.allow_unit_selection = true;
		this.add(new GodPower
		{
			id = "follow_unit",
			name = "follow_unit",
			unselect_when_window = true,
			can_drag_map = true
		});
		this.t.tester_enabled = false;
		this.t.allow_unit_selection = true;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0002914C File Offset: 0x0002734C
	private void addCivsAnimals()
	{
		this.clone("civ_cat", "$template_spawn_actor$");
		this.t.name = "civ_cat";
		this.t.actor_asset_id = "civ_cat";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_dog", "$template_spawn_actor$");
		this.t.name = "civ_dog";
		this.t.actor_asset_id = "civ_dog";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_chicken", "$template_spawn_actor$");
		this.t.name = "civ_chicken";
		this.t.actor_asset_id = "civ_chicken";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_rabbit", "$template_spawn_actor$");
		this.t.name = "civ_rabbit";
		this.t.actor_asset_id = "civ_rabbit";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_monkey", "$template_spawn_actor$");
		this.t.name = "civ_monkey";
		this.t.actor_asset_id = "civ_monkey";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_fox", "$template_spawn_actor$");
		this.t.name = "civ_fox";
		this.t.actor_asset_id = "civ_fox";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_sheep", "$template_spawn_actor$");
		this.t.name = "civ_sheep";
		this.t.actor_asset_id = "civ_sheep";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_cow", "$template_spawn_actor$");
		this.t.name = "civ_cow";
		this.t.actor_asset_id = "civ_cow";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_armadillo", "$template_spawn_actor$");
		this.t.name = "civ_armadillo";
		this.t.actor_asset_id = "civ_armadillo";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_wolf", "$template_spawn_actor$");
		this.t.name = "civ_wolf";
		this.t.actor_asset_id = "civ_wolf";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_bear", "$template_spawn_actor$");
		this.t.name = "civ_bear";
		this.t.actor_asset_id = "civ_bear";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_rhino", "$template_spawn_actor$");
		this.t.name = "civ_rhino";
		this.t.actor_asset_id = "civ_rhino";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_buffalo", "$template_spawn_actor$");
		this.t.name = "civ_buffalo";
		this.t.actor_asset_id = "civ_buffalo";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_hyena", "$template_spawn_actor$");
		this.t.name = "civ_hyena";
		this.t.actor_asset_id = "civ_hyena";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_rat", "$template_spawn_actor$");
		this.t.name = "civ_rat";
		this.t.actor_asset_id = "civ_rat";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_alpaca", "$template_spawn_actor$");
		this.t.name = "civ_alpaca";
		this.t.actor_asset_id = "civ_alpaca";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_capybara", "$template_spawn_actor$");
		this.t.name = "civ_capybara";
		this.t.actor_asset_id = "civ_capybara";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_goat", "$template_spawn_actor$");
		this.t.name = "civ_goat";
		this.t.actor_asset_id = "civ_goat";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_crab", "$template_spawn_actor$");
		this.t.name = "civ_crab";
		this.t.actor_asset_id = "civ_crab";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_penguin", "$template_spawn_actor$");
		this.t.name = "civ_penguin";
		this.t.actor_asset_id = "civ_penguin";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_turtle", "$template_spawn_actor$");
		this.t.name = "civ_turtle";
		this.t.actor_asset_id = "civ_turtle";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_crocodile", "$template_spawn_actor$");
		this.t.name = "civ_crocodile";
		this.t.actor_asset_id = "civ_crocodile";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_snake", "$template_spawn_actor$");
		this.t.name = "civ_snake";
		this.t.actor_asset_id = "civ_snake";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_frog", "$template_spawn_actor$");
		this.t.name = "civ_frog";
		this.t.actor_asset_id = "civ_frog";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_piranha", "$template_spawn_actor$");
		this.t.name = "civ_piranha";
		this.t.actor_asset_id = "civ_piranha";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_scorpion", "$template_spawn_actor$");
		this.t.name = "civ_scorpion";
		this.t.actor_asset_id = "civ_scorpion";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_candy_man", "$template_spawn_actor$");
		this.t.name = "civ_candy_man";
		this.t.actor_asset_id = "civ_candy_man";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_crystal_golem", "$template_spawn_actor$");
		this.t.name = "civ_crystal_golem";
		this.t.actor_asset_id = "civ_crystal_golem";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_liliar", "$template_spawn_actor$");
		this.t.name = "civ_liliar";
		this.t.actor_asset_id = "civ_liliar";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_garlic_man", "$template_spawn_actor$");
		this.t.name = "civ_garlic_man";
		this.t.actor_asset_id = "civ_garlic_man";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_lemon_man", "$template_spawn_actor$");
		this.t.name = "civ_lemon_man";
		this.t.actor_asset_id = "civ_lemon_man";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_acid_gentleman", "$template_spawn_actor$");
		this.t.name = "civ_acid_gentleman";
		this.t.actor_asset_id = "civ_acid_gentleman";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_beetle", "$template_spawn_actor$");
		this.t.name = "civ_beetle";
		this.t.actor_asset_id = "civ_beetle";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_seal", "$template_spawn_actor$");
		this.t.name = "civ_seal";
		this.t.actor_asset_id = "civ_seal";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.clone("civ_unicorn", "$template_spawn_actor$");
		this.t.name = "civ_unicorn";
		this.t.actor_asset_id = "civ_unicorn";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00029B54 File Offset: 0x00027D54
	private void addMobs()
	{
		this.clone("cold_one", "$template_spawn_actor$");
		this.t.name = "Cold Ones";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "cold_one";
		this.clone("demon", "$template_spawn_actor$");
		this.t.name = "Demon";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "demon";
		this.clone("angle", "$template_spawn_actor$");
		this.t.name = "Angle";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "angle";
		this.clone("tumor_monster_unit", "$template_spawn_actor$");
		this.t.name = "Tumor Monster";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_ids = AssetLibrary<GodPower>.a<string>(new string[]
		{
			"tumor_monster_unit",
			"tumor_monster_animal"
		});
		this.clone("mush_unit", "$template_spawn_actor$");
		this.t.name = "Mush";
		this.t.requires_premium = false;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_ids = AssetLibrary<GodPower>.a<string>(new string[]
		{
			"mush_unit",
			"mush_animal"
		});
		this.clone("bioblob", "$template_spawn_actor$");
		this.t.name = "Bioblob";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "bioblob";
		this.clone("lil_pumpkin", "$template_spawn_actor$");
		this.t.name = "Lil Pumpkin";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "lil_pumpkin";
		this.clone("assimilator", "$template_spawn_actor$");
		this.t.name = "Assimilator";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "assimilator";
		this.clone("necromancer", "$template_spawn_actor$");
		this.t.name = "Necromancer";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "necromancer";
		this.clone("druid", "$template_spawn_actor$");
		this.t.name = "Druid";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "druid";
		this.clone("plague_doctor", "$template_spawn_actor$");
		this.t.name = "Plague Doctor";
		this.t.actor_asset_id = "plague_doctor";
		this.clone("evil_mage", "$template_spawn_actor$");
		this.t.name = "Evil Mage";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "evil_mage";
		this.clone("white_mage", "$template_spawn_actor$");
		this.t.name = "White Mage";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_id = "white_mage";
		this.clone("bandit", "$template_spawn_actor$");
		this.t.name = "Bandits";
		this.t.actor_asset_id = "bandit";
		this.clone("snowman", "$template_spawn_actor$");
		this.t.name = "Snowman";
		this.t.actor_asset_id = "snowman";
		this.clone("zombie", "$template_spawn_actor$");
		this.t.name = "Zombie";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.t.actor_asset_ids = AssetLibrary<GodPower>.a<string>(new string[]
		{
			"zombie_human",
			"zombie_orc",
			"zombie_dwarf",
			"zombie_elf"
		});
		this.clone("skeleton", "$template_spawn_actor$");
		this.t.name = "Skeleton";
		this.t.rank = PowerRank.Rank0_free;
		this.t.actor_asset_id = "skeleton";
		this.clone("sheep", "$template_spawn_actor$");
		this.t.name = "Sheeps";
		this.t.actor_asset_id = "sheep";
		this.clone("rhino", "$template_spawn_actor$");
		this.t.name = "Rhino";
		this.t.actor_asset_id = "rhino";
		this.clone("monkey", "$template_spawn_actor$");
		this.t.name = "Monkey";
		this.t.actor_asset_id = "monkey";
		this.clone("buffalo", "$template_spawn_actor$");
		this.t.name = "Buffalo";
		this.t.actor_asset_id = "buffalo";
		this.clone("fox", "$template_spawn_actor$");
		this.t.name = "Fox";
		this.t.actor_asset_id = "fox";
		this.clone("hyena", "$template_spawn_actor$");
		this.t.name = "Hyena";
		this.t.actor_asset_id = "hyena";
		this.clone("dog", "$template_spawn_actor$");
		this.t.name = "Dog";
		this.t.actor_asset_id = "dog";
		this.clone("cow", "$template_spawn_actor$");
		this.t.name = "Cow";
		this.t.actor_asset_id = "cow";
		this.clone("frog", "$template_spawn_actor$");
		this.t.name = "Frog";
		this.t.actor_asset_id = "frog";
		this.clone("crocodile", "$template_spawn_actor$");
		this.t.name = "Crocodile";
		this.t.actor_asset_id = "crocodile";
		this.clone("snake", "$template_spawn_actor$");
		this.t.name = "Snake";
		this.t.actor_asset_id = "snake";
		this.clone("turtle", "$template_spawn_actor$");
		this.t.name = "Turtle";
		this.t.actor_asset_id = "turtle";
		this.clone("penguin", "$template_spawn_actor$");
		this.t.name = "Penguin";
		this.t.actor_asset_id = "penguin";
		this.clone("crab", "$template_spawn_actor$");
		this.t.name = "Crab";
		this.t.actor_asset_id = "crab";
		this.clone("rabbit", "$template_spawn_actor$");
		this.t.name = "Rabbit";
		this.t.actor_asset_id = "rabbit";
		this.clone("cat", "$template_spawn_actor$");
		this.t.name = "Cat";
		this.t.actor_asset_id = "cat";
		this.clone("chicken", "$template_spawn_actor$");
		this.t.name = "Chicken";
		this.t.actor_asset_id = "chicken";
		this.clone("wolf", "$template_spawn_actor$");
		this.t.name = "Wolfs";
		this.t.actor_asset_id = "wolf";
		this.clone("armadillo", "$template_spawn_actor$");
		this.t.name = "Armadillo";
		this.t.actor_asset_id = "armadillo";
		this.clone("raccoon", "$template_spawn_actor$");
		this.t.name = "Raccoon";
		this.t.actor_asset_id = "raccoon";
		this.clone("seal", "$template_spawn_actor$");
		this.t.name = "Seal";
		this.t.actor_asset_id = "seal";
		this.clone("ostrich", "$template_spawn_actor$");
		this.t.name = "Ostrich";
		this.t.actor_asset_id = "ostrich";
		this.clone("unicorn", "$template_spawn_actor$");
		this.t.name = "Unicorn";
		this.t.actor_asset_id = "unicorn";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("alpaca", "$template_spawn_actor$");
		this.t.name = "Alpaca";
		this.t.actor_asset_id = "alpaca";
		this.clone("capybara", "$template_spawn_actor$");
		this.t.name = "Capybara";
		this.t.actor_asset_id = "capybara";
		this.clone("scorpion", "$template_spawn_actor$");
		this.t.name = "Scorpion";
		this.t.actor_asset_id = "scorpion";
		this.clone("flower_bud", "$template_spawn_actor$");
		this.t.name = "Flower Bud";
		this.t.actor_asset_id = "flower_bud";
		this.clone("lemon_snail", "$template_spawn_actor$");
		this.t.name = "Bitba";
		this.t.actor_asset_id = "lemon_snail";
		this.clone("garl", "$template_spawn_actor$");
		this.t.name = "Garl";
		this.t.actor_asset_id = "garl";
		this.clone("bear", "$template_spawn_actor$");
		this.t.name = "Bear";
		this.t.actor_asset_id = "bear";
		this.clone("piranha", "$template_spawn_actor$");
		this.t.name = "Piranha";
		this.t.actor_asset_id = "piranha";
		this.clone("worm", "$template_spawn_actor$");
		this.t.name = "Worm";
		this.t.actor_asset_id = "worm";
		this.clone("crystal_sword", "$template_spawn_actor$");
		this.t.name = "Crystal Sword";
		this.t.actor_asset_id = "crystal_sword";
		this.clone("jumpy_skull", "$template_spawn_actor$");
		this.t.name = "Rude Skull";
		this.t.actor_asset_id = "jumpy_skull";
		this.clone("fire_skull", "$template_spawn_actor$");
		this.t.name = "Fire Skull";
		this.t.actor_asset_id = "fire_skull";
		this.clone("fire_elemental", "$template_spawn_actor$");
		this.t.name = "Fire Elemental";
		this.t.actor_asset_ids = SA.fire_elementals;
		this.clone("ghost", "$template_spawn_actor$");
		this.t.name = "Ghost";
		this.t.actor_asset_id = "ghost";
		this.clone("alien", "$template_spawn_actor$");
		this.t.name = "Alien";
		this.t.actor_asset_id = "alien";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank3_good;
		this.clone("greg", "$template_spawn_actor$");
		this.t.name = "Greg";
		this.t.actor_asset_id = "greg";
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank5_noAwards;
		this.clone("smore", "$template_spawn_actor$");
		this.t.name = "Smore";
		this.t.actor_asset_id = "smore";
		this.clone("sand_spider", "$template_spawn_actor$");
		this.t.name = "Sand Spider";
		this.t.actor_asset_id = "sand_spider";
		this.t.hold_action = true;
		this.clone("goat", "$template_spawn_actor$");
		this.t.name = "Goat";
		this.t.actor_asset_id = "goat";
		this.clone("acid_blob", "$template_spawn_actor$");
		this.t.name = "Acid Blob";
		this.t.actor_asset_id = "acid_blob";
		this.clone("god_finger", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank2_normal;
		this.t.name = "God Finger";
		this.t.actor_asset_id = "god_finger";
		this.clone("UFO", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.name = "UFO";
		this.t.actor_asset_id = "UFO";
		this.clone("dragon", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.name = "Dragon";
		this.t.actor_asset_id = "dragon";
		this.clone("fairy", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank2_normal;
		this.t.name = "Fairy";
		this.t.actor_asset_id = "fairy";
		this.t.requires_premium = true;
		this.clone("butterfly", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Butterfly";
		this.t.actor_asset_id = "butterfly";
		this.clone("bee", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Bee";
		this.t.actor_asset_id = "bee";
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("grasshopper", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Grasshopper";
		this.t.actor_asset_id = "grasshopper";
		this.clone("fly", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Fly";
		this.t.actor_asset_id = "fly";
		this.clone("beetle", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Beetle";
		this.t.actor_asset_id = "beetle";
		this.clone("rat", "$template_spawn_actor$");
		this.t.name = "Rat";
		this.t.actor_asset_id = "rat";
		this.clone("ant_blue", "$template_spawn_actor$");
		this.t.name = "Blue Ant";
		this.t.actor_asset_id = "ant_blue";
		this.clone("ant_green", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Green Ant";
		this.t.actor_asset_id = "ant_green";
		this.clone("ant_black", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Black Ant";
		this.t.actor_asset_id = "ant_black";
		this.clone("ant_red", "$template_spawn_actor$");
		this.t.requires_premium = true;
		this.t.rank = PowerRank.Rank1_common;
		this.t.name = "Red Ant";
		this.t.actor_asset_id = "ant_red";
		this.clone("crabzilla", "$template_spawn_actor$");
		this.t.name = "Crabzilla";
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requires_premium = true;
		this.t.actor_asset_id = "crabzilla";
		this.t.actor_spawn_height = 0f;
		this.t.ignore_fast_spawn = true;
		this.t.tester_enabled = false;
		this.t.multiple_spawn_tip = false;
		this.t.click_action = new PowerActionWithID(this.spawnCrabzilla);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0002AD04 File Offset: 0x00028F04
	private void addCivsClassic()
	{
		this.add(new GodPower
		{
			id = "$template_spawn_actor$",
			type = PowerActionType.PowerSpawnActor,
			rank = PowerRank.Rank0_free,
			unselect_when_window = true,
			show_spawn_effect = true,
			actor_spawn_height = 3f,
			multiple_spawn_tip = true,
			show_unit_stats_overview = true,
			set_used_camera_drag_on_long_move = true
		});
		this.t.click_action = new PowerActionWithID(this.spawnUnit);
		this.clone("human", "$template_spawn_actor$");
		this.t.name = "Human";
		this.t.actor_asset_id = "human";
		this.clone("orc", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requires_premium = true;
		this.t.name = "Orc";
		this.t.actor_asset_id = "orc";
		this.clone("elf", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requires_premium = true;
		this.t.name = "Elf";
		this.t.actor_asset_id = "elf";
		this.clone("dwarf", "$template_spawn_actor$");
		this.t.rank = PowerRank.Rank4_awesome;
		this.t.requires_premium = true;
		this.t.name = "Dwarf";
		this.t.actor_asset_id = "dwarf";
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0002AE88 File Offset: 0x00029088
	public override void linkAssets()
	{
		foreach (GodPower tPower in this.list)
		{
			if (!string.IsNullOrEmpty(tPower.drop_id))
			{
				tPower.cached_drop_asset = AssetManager.drops.get(tPower.drop_id);
			}
			if (!string.IsNullOrEmpty(tPower.tile_type))
			{
				tPower.cached_tile_type_asset = AssetManager.tiles.get(tPower.tile_type);
			}
			if (!string.IsNullOrEmpty(tPower.top_tile_type))
			{
				tPower.cached_top_tile_type_asset = AssetManager.top_tiles.get(tPower.top_tile_type);
			}
			if (tPower.actor_asset_id != null)
			{
				ActorAsset tActorAsset = AssetManager.actor_library.get(tPower.actor_asset_id);
				if (tActorAsset.power_id == null)
				{
					tActorAsset.power_id = tPower.id;
				}
			}
			string[] actor_asset_ids = tPower.actor_asset_ids;
			if (actor_asset_ids != null && actor_asset_ids.Length != 0)
			{
				foreach (string tActorAssetID in tPower.actor_asset_ids)
				{
					ActorAsset tActorAsset2 = AssetManager.actor_library.get(tActorAssetID);
					if (tActorAsset2.power_id == null)
					{
						tActorAsset2.power_id = tPower.id;
					}
				}
			}
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0002AFD4 File Offset: 0x000291D4
	private void traceRanks(PowerButton pTarget)
	{
		string rank0 = "";
		string rank = "";
		string rank2 = "";
		string rank3 = "";
		string rank4 = "";
		for (int i = 0; i < this.list.Count; i++)
		{
			GodPower tPower = this.list[i];
			switch (tPower.rank)
			{
			case PowerRank.Rank0_free:
				rank0 = rank0 + tPower.name + ", ";
				break;
			case PowerRank.Rank1_common:
				rank = rank + tPower.name + ", ";
				break;
			case PowerRank.Rank2_normal:
				rank2 = rank2 + tPower.name + ", ";
				break;
			case PowerRank.Rank3_good:
				rank3 = rank3 + tPower.name + ", ";
				break;
			case PowerRank.Rank4_awesome:
				rank4 = rank4 + tPower.name + ", ";
				break;
			}
		}
		Debug.Log("rank 0: " + rank0);
		Debug.Log("rank 1: " + rank);
		Debug.Log("rank 2: " + rank2);
		Debug.Log("rank 3: " + rank3);
		Debug.Log("rank 4: " + rank4);
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0002B110 File Offset: 0x00029310
	private bool spawnDrops(WorldTile tTile, GodPower pPower)
	{
		BrushData tBrush = Config.current_brush_data;
		bool tSpawnPixel = false;
		if (tBrush.size == 0 && tBrush.fast_spawn)
		{
			if (World.world.player_control.timer_spawn_pixels <= 0f)
			{
				World.world.player_control.timer_spawn_pixels = 0.5f;
				tSpawnPixel = true;
			}
		}
		else if (tBrush.fast_spawn && Randy.randomBool())
		{
			if (World.world.player_control.timer_spawn_pixels <= 0f)
			{
				World.world.player_control.timer_spawn_pixels = 0.3f;
				tSpawnPixel = true;
			}
		}
		else
		{
			tSpawnPixel = Randy.randomChance(pPower.falling_chance);
		}
		if (World.world.player_control.first_click)
		{
			World.world.player_control.first_click = false;
			tSpawnPixel = true;
			World.world.player_control.timer_spawn_pixels = 0.3f;
		}
		if (tSpawnPixel)
		{
			World.world.drop_manager.spawn(tTile, pPower.cached_drop_asset, -1f, -1f, true, -1L).soundOn = true;
		}
		return true;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0002B210 File Offset: 0x00029410
	private bool spawnPrinter(WorldTile pTile, string pPower)
	{
		GodPower tPower = this.get(pPower);
		EffectsLibrary.spawn("fx_spawn", pTile, null, null, 0f, -1f, -1f, null);
		World.world.units.spawnNewUnitByPlayer("printer", pTile, true, false, 6f, null).data.set("template", tPower.printers_print);
		AchievementLibrary.print_heart.check(tPower);
		return true;
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0002B283 File Offset: 0x00029483
	private bool useMagnet(WorldTile pTile, string pPower)
	{
		World.world.magnet.magnetAction(false, pTile);
		return true;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0002B297 File Offset: 0x00029497
	private bool spawnCloudSnow(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_snow");
		return true;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0002B2A6 File Offset: 0x000294A6
	private bool spawnCloudLava(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_lava");
		return true;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0002B2B5 File Offset: 0x000294B5
	private bool spawnCloudAcid(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_acid");
		return true;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0002B2C4 File Offset: 0x000294C4
	private bool spawnCloudOfLife(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_normal");
		return true;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0002B2D3 File Offset: 0x000294D3
	private bool spawnCloudRain(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_rain");
		return true;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0002B2E2 File Offset: 0x000294E2
	private bool spawnCloudFire(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_fire");
		return true;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0002B2F1 File Offset: 0x000294F1
	private bool spawnCloudLightning(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_lightning");
		return true;
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0002B300 File Offset: 0x00029500
	private bool spawnCloudMagic(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_magic");
		return true;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0002B30F File Offset: 0x0002950F
	private bool spawnCloudRage(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_rage");
		return true;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0002B31E File Offset: 0x0002951E
	private bool spawnCloudAsh(WorldTile pTile, string pPower)
	{
		this.spawnCloud(pTile, "cloud_ash");
		return true;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0002B330 File Offset: 0x00029530
	private void spawnCloud(WorldTile pTile, string pCloudID)
	{
		EffectsLibrary.spawn("fx_cloud", pTile, pCloudID, null, 0f, -1f, -1f, null);
		MusicBox.playSound("event:/SFX/UNIQUE/SpawnCloud", (float)pTile.pos.x, (float)pTile.pos.y, false, false);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0002B388 File Offset: 0x00029588
	private bool spawnCrabzilla(WorldTile pTile, string pPower)
	{
		World.world.player_control.already_used_power = false;
		World.world.selected_buttons.unselectAll();
		((SpawnEffect)EffectsLibrary.spawn("fx_spawn_big", pTile, null, null, 0f, -1f, -1f, null)).setEvent("crabzilla", pTile);
		return true;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0002B3E2 File Offset: 0x000295E2
	private bool spawnLightning(WorldTile pTile, string pPower)
	{
		MapBox.spawnLightningBig(pTile, 0.25f, null);
		return true;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0002B3F4 File Offset: 0x000295F4
	private bool spawnForce(WorldTile pTile, string pPower)
	{
		MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile, false, false);
		World.world.applyForceOnTile(pTile, 10, 3f, true, 0, null, null, null, true);
		EffectsLibrary.spawnExplosionWave(pTile.posV3, 10f, 1f);
		return true;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0002B43C File Offset: 0x0002963C
	private bool fingerFlick(WorldTile pTile, string pPower)
	{
		Actor tUnit = World.world.getActorNearCursor();
		if (tUnit == null)
		{
			return false;
		}
		Vector2 tCursorPos = World.world.getMousePos();
		Vector2 tUnitPos = tUnit.current_position;
		float tForceDirection = Randy.randomFloat(2.5f, 5f);
		float tForceHeight = Randy.randomFloat(2.5f, 3f);
		tUnit.calculateForce(tUnitPos.x, tUnitPos.y, tCursorPos.x, tCursorPos.y, tForceDirection, tForceHeight, true);
		tUnit.addStatusEffect("flicked", 0f, true);
		tUnit.makeStunned(5f);
		return true;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0002B4CC File Offset: 0x000296CC
	private bool spawnInfinityCoin(WorldTile pTile, string pPower)
	{
		EffectsLibrary.spawn("fx_infinity_coin", pTile, null, null, 0f, -1f, -1f, null);
		return true;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0002B4ED File Offset: 0x000296ED
	private bool spawnEarthquake(WorldTile pTile, string pPower)
	{
		Earthquake.startQuake(pTile, EarthquakeType.RandomPower);
		return true;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0002B4F7 File Offset: 0x000296F7
	private bool spawnMeteorite(WorldTile pTile, string pPower)
	{
		Meteorite.spawnMeteorite(pTile, null);
		return true;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0002B501 File Offset: 0x00029701
	private bool spawnTornado(WorldTile pTile, string pPower)
	{
		EffectsLibrary.spawnAtTile("fx_tornado", pTile, 0.5f);
		return true;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0002B518 File Offset: 0x00029718
	private bool prepareBoulder(WorldTile pTile, string pPower)
	{
		Touch tTouch = default(Touch);
		Vector2 tPosition;
		if (InputHelpers.mouseSupported)
		{
			tPosition = World.world.getMousePos();
		}
		else
		{
			if (!World.world.player_control.getTouchPos(out tTouch, true))
			{
				return false;
			}
			tPosition = World.world.camera.ScreenToWorldPoint(tTouch.position);
		}
		Boulder.chargeBoulder(tPosition, tTouch);
		return true;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0002B580 File Offset: 0x00029780
	private bool spawnSanta(WorldTile pTile, string pPower)
	{
		EffectsLibrary.spawn("fx_santa", pTile, "santa", null, 0f, -1f, -1f, null);
		return true;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0002B5A8 File Offset: 0x000297A8
	private void toggleOptionZone(string pPower)
	{
		GodPower tPower = AssetManager.powers.get(pPower);
		MetaTypeAsset tMetaTypeAsset = AssetManager.meta_type_library.getFromPower(tPower);
		if (InputHelpers.GetMouseButtonUp(1))
		{
			tMetaTypeAsset.toggleOptionZone(tPower, -1, true);
			return;
		}
		tMetaTypeAsset.toggleOptionZone(tPower, 1, true);
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0002B5E8 File Offset: 0x000297E8
	internal void toggleMultiOption(string pPower)
	{
		GodPower godPower = AssetManager.powers.get(pPower);
		string pOption = godPower.toggle_name;
		OptionAsset tOption = AssetManager.options_library.get(pOption);
		int tDirection;
		if (InputHelpers.GetMouseButtonUp(1))
		{
			tDirection = -1;
		}
		else
		{
			tDirection = 1;
		}
		PlayerOptionData tData = tOption.data;
		if (tData.boolVal)
		{
			tData.intVal += tDirection;
			if (tData.intVal > tOption.max_value)
			{
				tData.intVal = 0;
				tData.boolVal = false;
			}
			if (tData.intVal < 0)
			{
				tData.intVal = tOption.max_value;
			}
		}
		else
		{
			tData.boolVal = true;
		}
		PlayerConfig.saveData();
		string tLocalizedName = godPower.getTranslatedName();
		string tLocalizedDescription = godPower.getTranslatedDescription();
		string tZoneMode = tOption.getTranslatedOption();
		if (tData.boolVal)
		{
			WorldTip.instance.showToolbarText(tLocalizedName + " - " + tZoneMode, tLocalizedDescription, true);
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0002B6BC File Offset: 0x000298BC
	private void toggleOption(string pPower)
	{
		GodPower tPower = AssetManager.powers.get(pPower);
		WorldTip.instance.showToolbarText(tPower, true);
		PlayerOptionData tData = PlayerConfig.dict[tPower.toggle_name];
		tData.boolVal = !tData.boolVal;
		if (tPower.map_modes_switch)
		{
			if (tData.boolVal)
			{
				PowerLibrary.disableAllOtherMapModes(pPower);
			}
			else
			{
				WorldTip.instance.startHide();
			}
		}
		PlayerConfig.saveData();
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x0002B728 File Offset: 0x00029928
	internal static void disableAllOtherMapModes(string pMainPower)
	{
		for (int i = 0; i < AssetManager.powers.list.Count; i++)
		{
			GodPower tPower = AssetManager.powers.list[i];
			if (tPower.map_modes_switch && !(tPower.id == pMainPower))
			{
				PlayerConfig.dict[tPower.toggle_name].boolVal = false;
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0002B78C File Offset: 0x0002998C
	private bool useVortex(WorldTile pTile, string pPower)
	{
		if (pTile.isTemporaryFrozen())
		{
			pTile.unfreeze(99);
		}
		VortexAction.moveTiles(pTile, Config.current_brush_data);
		return true;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0002B7AC File Offset: 0x000299AC
	private bool drawTiles(WorldTile pTile, string pPowerID)
	{
		GodPower godPower = this.get(pPowerID);
		TileType tTypeTile = godPower.cached_tile_type_asset;
		TopTileType tTypeTopTile = godPower.cached_top_tile_type_asset;
		World.world.flash_effects.flashPixel(pTile, 25, ColorType.White);
		if (tTypeTopTile != null && tTypeTopTile.wall && pTile.Type.id != tTypeTopTile.id)
		{
			World.world.game_stats.data.wallsPlaced += 1L;
			AchievementLibrary.segregator.check(null);
		}
		MapAction.terraformTile(pTile, tTypeTile, tTypeTopTile, TerraformLibrary.draw, false);
		return true;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0002B83B File Offset: 0x00029A3B
	private bool flashPixel(WorldTile pTile, string pPowerID = null)
	{
		World.world.flash_effects.flashPixel(pTile, 10, ColorType.White);
		return true;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0002B851 File Offset: 0x00029A51
	private bool flashPixel(WorldTile pTile, GodPower pPower)
	{
		World.world.flash_effects.flashPixel(pTile, 10, ColorType.White);
		return true;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0002B868 File Offset: 0x00029A68
	private bool drawTemperaturePlus(WorldTile pTile, string pPower)
	{
		if (pTile.isTemporaryFrozen() && Randy.randomBool())
		{
			pTile.unfreeze(1);
		}
		WorldBehaviourUnitTemperatures.checkTile(pTile, 5);
		if (pTile.Type.lava)
		{
			LavaHelper.heatUpLava(pTile);
		}
		if (pTile.hasBuilding() && pTile.building.asset.spawn_drops)
		{
			pTile.building.data.removeFlag("stop_spawn_drops");
		}
		return true;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0002B8D5 File Offset: 0x00029AD5
	public bool clickHideUI(string pPowerId)
	{
		if (ScrollWindow.isWindowActive())
		{
			return true;
		}
		Config.ui_main_hidden = true;
		return true;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0002B8E7 File Offset: 0x00029AE7
	public bool clickTraitEditorRainButton(string pPowerId)
	{
		Config.selected_trait_editor = pPowerId;
		ScrollWindow.showWindow("trait_rain_editor");
		return true;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0002B8FA File Offset: 0x00029AFA
	public bool clickEquipmentEditorRainButton(string pPowerId)
	{
		ScrollWindow.showWindow("equipment_rain_editor");
		return true;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0002B908 File Offset: 0x00029B08
	public static bool drawTemperatureMinus(WorldTile pTile, string pPower)
	{
		if (pTile.Type.lava)
		{
			LavaHelper.coolDownLava(pTile);
		}
		if (pTile.isOnFire())
		{
			pTile.stopFire();
		}
		if (pTile.canBeFrozen() && Randy.randomBool())
		{
			if (pTile.health > 0)
			{
				pTile.health--;
			}
			else
			{
				pTile.freeze(1);
			}
		}
		WorldBehaviourUnitTemperatures.checkTile(pTile, -5);
		if (pTile.hasBuilding())
		{
			ActionLibrary.addFrozenEffectOnTarget(null, pTile.building, null);
		}
		if (pTile.hasBuilding() && pTile.building.asset.spawn_drops)
		{
			pTile.building.data.addFlag("stop_spawn_drops");
		}
		return true;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0002B9B5 File Offset: 0x00029BB5
	private bool drawShovelPlus(WorldTile pTile, string pPower)
	{
		if (pTile.health > 0)
		{
			pTile.health--;
		}
		else
		{
			MapAction.increaseTile(pTile, false, "destroy");
		}
		return false;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0002B9DD File Offset: 0x00029BDD
	private bool drawShovelMinus(WorldTile pTile, string pPower)
	{
		if (pTile.health > 0)
		{
			pTile.health--;
		}
		else
		{
			MapAction.decreaseTile(pTile, false, "destroy");
		}
		return false;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0002BA05 File Offset: 0x00029C05
	private bool drawGreyGoo(WorldTile pTile, string pPower)
	{
		World.world.grey_goo_layer.add(pTile);
		return false;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0002BA18 File Offset: 0x00029C18
	private bool drawConway(WorldTile pTile, string pPower)
	{
		if (Randy.randomBool())
		{
			World.world.conway_layer.add(pTile, "conway");
		}
		return false;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0002BA37 File Offset: 0x00029C37
	private bool drawConwayInverse(WorldTile pTile, string pPower)
	{
		if (Randy.randomBool())
		{
			World.world.conway_layer.add(pTile, "conway_inverse");
		}
		return false;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0002BA58 File Offset: 0x00029C58
	private bool drawFinger(WorldTile pTile, string pPower)
	{
		TileType tType = World.world.player_control.first_pressed_type;
		TopTileType tTopTile = World.world.player_control.first_pressed_top_type;
		if (tTopTile != null && !tTopTile.allowed_to_be_finger_copied)
		{
			tTopTile = null;
		}
		if (tType.ground && (tTopTile == null || tTopTile.ground))
		{
			MapAction.terraformTile(pTile, tType, tTopTile, TerraformLibrary.draw, false);
		}
		else
		{
			this.destroyBuildings(pTile, pPower);
			MapAction.terraformTile(pTile, tType, tTopTile, TerraformLibrary.destroy_no_flash, false);
		}
		if (pTile.Type.grey_goo)
		{
			World.world.grey_goo_layer.add(pTile);
		}
		if (tTopTile != null && tTopTile.biome_id == "biome_grass")
		{
			AchievementLibrary.touch_the_grass.check(null);
		}
		return false;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0002BB0C File Offset: 0x00029D0C
	private bool drawBorderBrush(WorldTile pTile, string pPower)
	{
		WorldTile tFirstTile = World.world.player_control.first_pressed_tile;
		if (tFirstTile == null)
		{
			return false;
		}
		City tCity = tFirstTile.zone_city;
		if (tCity == null)
		{
			return false;
		}
		tCity.addZone(pTile.zone);
		World.world.city_zone_helper.city_place_finder.setDirty();
		tCity.setAbandonedZonesDirty();
		return false;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0002BB64 File Offset: 0x00029D64
	private bool spawnUnit(WorldTile pTile, string pPowerID)
	{
		GodPower tPower = this.get(pPowerID);
		MusicBox.playSound("event:/SFX/UNIQUE/SpawnWhoosh", (float)pTile.pos.x, (float)pTile.pos.y, false, false);
		if (tPower.id == "sheep" && pTile.Type.lava)
		{
			AchievementLibrary.sacrifice.check(null);
		}
		EffectsLibrary.spawn("fx_spawn", pTile, null, null, 0f, -1f, -1f, null);
		string[] actor_asset_ids = tPower.actor_asset_ids;
		string tStatsID;
		if (actor_asset_ids != null && actor_asset_ids.Length != 0)
		{
			tStatsID = tPower.actor_asset_ids.GetRandom<string>();
		}
		else
		{
			tStatsID = tPower.actor_asset_id;
		}
		Actor tUnit = World.world.units.spawnNewUnitByPlayer(tStatsID, pTile, true, true, tPower.actor_spawn_height, null);
		AchievementLibrary.back_to_beta_testing.check(tUnit);
		return true;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0002BC3C File Offset: 0x00029E3C
	private bool divineLightFX(WorldTile pCenterTile, string pPowerID)
	{
		World.world.fx_divine_light.playOn(pCenterTile);
		return true;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0002BC4F File Offset: 0x00029E4F
	private bool drawDivineLight(WorldTile pCenterTile, string pPowerID)
	{
		pCenterTile.doUnits(delegate(Actor pActor)
		{
			this.clearBadTraitsFrom(pActor);
			if (pActor.asset.can_be_killed_by_divine_light)
			{
				pActor.getHit((float)pActor.getMaxHealthPercent(0.4f), true, AttackType.Divine, null, true, false, true);
			}
			else
			{
				pActor.startColorEffect(ActorColorEffect.White);
			}
			pActor.finishStatusEffect("ash_fever");
			pActor.finishAngryStatus();
			if (!pActor.isInLiquid())
			{
				pActor.cancelAllBeh();
			}
			if (pActor.hasPlot())
			{
				World.world.plots.cancelPlot(pActor.plot);
			}
		});
		return true;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0002BC64 File Offset: 0x00029E64
	private void clearBadTraitsFrom(Actor pActor)
	{
		using (ListPool<ActorTrait> tTraitsToRemove = new ListPool<ActorTrait>())
		{
			foreach (ActorTrait tTrait in pActor.getTraits())
			{
				if (tTrait.can_be_removed_by_divine_light)
				{
					tTraitsToRemove.Add(tTrait);
				}
			}
			if (tTraitsToRemove.Count > 0)
			{
				pActor.removeTraits(tTraitsToRemove);
				pActor.setStatsDirty();
				pActor.changeHappiness("just_felt_the_divine", 0);
			}
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0002BCFC File Offset: 0x00029EFC
	private bool cleanBurnedTile(WorldTile pTile, string pPowerID)
	{
		pTile.removeBurn();
		return true;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0002BD08 File Offset: 0x00029F08
	private bool removeTornadoes(WorldTile pTile, string pPowerID)
	{
		bool result;
		using (ListPool<BaseEffect> tEffects = new ListPool<BaseEffect>(World.world.stack_effects.get("fx_tornado").getList()))
		{
			if (tEffects.Count == 0)
			{
				result = false;
			}
			else
			{
				float tCheckRadius = (float)(2 * (Config.current_brush_data.size + 1));
				tCheckRadius *= tCheckRadius;
				foreach (BaseEffect ptr in tEffects)
				{
					BaseEffect tEffect = ptr;
					if (tEffect.active)
					{
						Vector3 tPos = tEffect.transform.localPosition;
						if (Toolbox.SquaredDist(tPos.x, tPos.y, (float)pTile.x, (float)pTile.y) <= tCheckRadius)
						{
							((TornadoEffect)tEffect).die();
						}
					}
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0002BDF8 File Offset: 0x00029FF8
	private bool drawPickaxe(WorldTile pTile, string pPowerID)
	{
		if (pTile.hasBuilding() && pTile.building.asset.building_type == BuildingType.Building_Mineral)
		{
			pTile.building.startDestroyBuilding();
		}
		if (pTile.Type.can_be_removed_with_pickaxe)
		{
			MapAction.decreaseTile(pTile, false, "remove");
		}
		return true;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0002BE45 File Offset: 0x0002A045
	private bool drawBucket(WorldTile pTile, string pPowerID)
	{
		MapAction.removeLiquid(pTile);
		if (pTile.Type.lava)
		{
			MapAction.decreaseTile(pTile, false, "flash");
		}
		if (pTile.Type.can_be_removed_with_bucket)
		{
			MapAction.decreaseTile(pTile, false, "flash");
		}
		return true;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0002BE80 File Offset: 0x0002A080
	private bool drawAxe(WorldTile pTile, string pPowerID)
	{
		if (pTile.hasBuilding())
		{
			Building tBuilding = pTile.building;
			BuildingAsset tAsset = tBuilding.asset;
			if (tAsset.building_type == BuildingType.Building_Tree && !tBuilding.chopped)
			{
				if (tAsset.resources_given != null && pTile.hasCity())
				{
					foreach (ResourceContainer tContainer in tAsset.resources_given)
					{
						pTile.zone_city.addResourcesToRandomStockpile(tContainer.id, tContainer.amount);
					}
				}
				tBuilding.chopTree();
			}
		}
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 0, 0f, false))
		{
			if (!(tActor.kingdom.name != "living_plants"))
			{
				tActor.a.getHitFullHealth(AttackType.Divine);
			}
		}
		if (pTile.Type.can_be_removed_with_axe)
		{
			MapAction.decreaseTile(pTile, false, "remove");
		}
		return true;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
	private bool drawSpade(WorldTile pTile, string pPowerID)
	{
		if (pTile.Type.can_be_removed_with_spade)
		{
			MapAction.removeGreens(pTile);
		}
		return true;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0002BFBC File Offset: 0x0002A1BC
	private bool drawSickle(WorldTile pTile, string pPowerID)
	{
		if (pTile.hasBuilding())
		{
			BuildingType building_type = pTile.building.asset.building_type;
			if (building_type == BuildingType.Building_Fruits || building_type - BuildingType.Building_Wheat <= 1)
			{
				pTile.building.startDestroyBuilding();
			}
		}
		if (pTile.Type.can_be_removed_with_sickle)
		{
			MapAction.decreaseTile(pTile, false, "remove");
		}
		return true;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0002C014 File Offset: 0x0002A214
	private bool drawDemolish(WorldTile pTile, string pPowerID)
	{
		if (pTile.hasBuilding() && pTile.building.asset.can_be_demolished)
		{
			pTile.building.startDestroyBuilding();
		}
		if (pTile.Type.can_be_removed_with_demolish)
		{
			MapAction.decreaseTile(pTile, false, "flash");
		}
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 0, 0f, false))
		{
			if (!(tActor.kingdom.name != "living_houses"))
			{
				tActor.a.getHitFullHealth(AttackType.Divine);
			}
		}
		return true;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
	private bool drawScissors(WorldTile pTile, string pPowerID)
	{
		if (pTile.zone.hasCity())
		{
			pTile.zone.city.removeZone(pTile.zone);
		}
		return true;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0002C0EA File Offset: 0x0002A2EA
	private bool drawLifeEraser(WorldTile pTile, string pPowerID)
	{
		MapAction.removeLifeFromTile(pTile);
		return true;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0002C0F3 File Offset: 0x0002A2F3
	private bool drawHeatray(WorldTile pTile, string pPowerID)
	{
		if (World.world.heat_ray_fx.isReady())
		{
			World.world.heat.addTile(pTile, Randy.randomInt(1, 3));
		}
		return true;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0002C120 File Offset: 0x0002A320
	[ClickActionCaller]
	private bool heatrayFX(WorldTile pTile, string pPowerID)
	{
		if (World.world.heat_ray_fx.isReady())
		{
			MusicBox.inst.playDrawingSound("event:/SFX/POWERS/HeatRayMelts", (float)pTile.x, (float)pTile.y);
		}
		World.world.heat_ray_fx.play(pTile.pos, 10);
		this.loopWithBrush(pTile, pPowerID);
		return true;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0002C184 File Offset: 0x0002A384
	[ClickActionCaller]
	private bool loopWithCurrentBrush(WorldTile pCenterTile, string pPowerID)
	{
		GodPower tPower = this.get(pPowerID);
		this.loopWithBrush(pCenterTile, tPower);
		if (tPower.surprises_units)
		{
			ActionLibrary.suprisedByArchitector(null, pCenterTile);
		}
		return true;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0002C1B3 File Offset: 0x0002A3B3
	[ClickActionCaller]
	private bool drawingCursorEffect(WorldTile pTile, string pPowerID)
	{
		EffectsLibrary.spawnAt("fx_spark", pTile.posV3, 0.2f);
		return true;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0002C1CC File Offset: 0x0002A3CC
	private bool flashBrushPixelsDuringClick(WorldTile pCenterTile, string pPower)
	{
		BrushData tBrushData = Config.current_brush_data;
		World.world.highlightTilesBrush(pCenterTile, tBrushData, new PowerAction(this.flashPixel), null);
		return true;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0002C1FC File Offset: 0x0002A3FC
	private bool flashBrushPixelsDuringClick(WorldTile pCenterTile, GodPower pPower)
	{
		BrushData tBrushData = Config.current_brush_data;
		World.world.highlightTilesBrush(pCenterTile, tBrushData, new PowerAction(this.flashPixel), pPower);
		return true;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0002C22C File Offset: 0x0002A42C
	[ClickPowerActionCaller]
	private bool loopWithCurrentBrushPowerForDropsFull(WorldTile pCenterTile, GodPower pPower)
	{
		BrushData tBrushData = Config.current_brush_data;
		WorldBehaviourTileEffects.checkTileForEffectKill(pCenterTile, tBrushData.size);
		World.world.loopWithBrushPowerForDropsFull(pCenterTile, tBrushData, pPower.click_power_action, pPower);
		return true;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0002C260 File Offset: 0x0002A460
	[ClickPowerActionCaller]
	private bool loopWithCurrentBrushPowerForDropsRandom(WorldTile pCenterTile, GodPower pPower)
	{
		BrushData tBrushData = Config.current_brush_data;
		WorldBehaviourTileEffects.checkTileForEffectKill(pCenterTile, tBrushData.size);
		World.world.loopWithBrushPowerForDropsRandom(pCenterTile, tBrushData, pPower.click_power_action, pPower);
		return true;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0002C294 File Offset: 0x0002A494
	[ClickActionCaller]
	private bool loopWithBrush(WorldTile pCenterTile, string pPowerID)
	{
		GodPower tPower = this.get(pPowerID);
		return this.loopWithBrush(pCenterTile, tPower);
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
	[ClickActionCaller]
	private bool loopWithBrush(WorldTile pCenterTile, GodPower pPower)
	{
		string tBrushId = Config.current_brush;
		if (!string.IsNullOrEmpty(pPower.force_brush))
		{
			tBrushId = pPower.force_brush;
		}
		BrushData tBrush = Brush.get(tBrushId);
		WorldBehaviourTileEffects.checkTileForEffectKill(pCenterTile, tBrush.size);
		World.world.loopWithBrush(pCenterTile, tBrush, pPower.click_action, pPower.id);
		return true;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0002C307 File Offset: 0x0002A507
	private bool stopFire(WorldTile pTile, string pPowerID)
	{
		pTile.stopFire();
		if (pTile.hasBuilding() && pTile.building.hasStatus("burning"))
		{
			pTile.building.stopFire();
		}
		return true;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0002C335 File Offset: 0x0002A535
	private bool fmodDrawingSound(WorldTile pTile, GodPower pPower)
	{
		if (pPower.has_sound_drawing)
		{
			MusicBox.inst.playDrawingSound(pPower.sound_drawing, (float)pTile.x, (float)pTile.y);
		}
		return true;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0002C360 File Offset: 0x0002A560
	private bool fmodDrawingSound(WorldTile pTile, string pPowerID)
	{
		GodPower tPower = this.get(pPowerID);
		this.fmodDrawingSound(pTile, tPower);
		return true;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0002C37F File Offset: 0x0002A57F
	private bool destroyBuildings(WorldTile pTile, string pPowerID)
	{
		if (!pTile.hasBuilding())
		{
			return false;
		}
		pTile.building.startDestroyBuilding();
		return true;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0002C398 File Offset: 0x0002A598
	private bool removeClouds(WorldTile pTile, string pPowerID)
	{
		List<BaseEffect> tList = World.world.stack_effects.get("fx_cloud").getList();
		float tCheckRadius = (float)(10 * (Config.current_brush_data.size + 1));
		tCheckRadius *= tCheckRadius;
		for (int i = 0; i < tList.Count; i++)
		{
			BaseEffect tCloud = tList[i];
			if (tCloud.active)
			{
				Vector3 tPos = tCloud.transform.localPosition;
				if (Toolbox.SquaredDist(tPos.x, tPos.y, (float)pTile.x, (float)pTile.y) <= tCheckRadius)
				{
					((Cloud)tCloud).startToDie();
				}
			}
		}
		return true;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0002C432 File Offset: 0x0002A632
	private bool removeGoo(WorldTile pTile, string pPowerID)
	{
		if (pTile.Type.grey_goo)
		{
			MapAction.decreaseTile(pTile, false, "flash");
		}
		return true;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0002C450 File Offset: 0x0002A650
	private bool removeBuildingsBySponge(WorldTile pTile, string pPowerID)
	{
		if (!pTile.hasBuilding())
		{
			return false;
		}
		bool tRemoveBuilding = false;
		if (pTile.building.isRuin() || pTile.building.asset.removed_by_sponge)
		{
			tRemoveBuilding = true;
		}
		if (tRemoveBuilding)
		{
			pTile.building.startDestroyBuilding();
		}
		return true;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0002C49C File Offset: 0x0002A69C
	public override void editorDiagnosticLocales()
	{
		foreach (GodPower tAsset in this.list)
		{
			if (tAsset.show_tool_sizes && !string.IsNullOrEmpty(tAsset.force_brush))
			{
				BaseAssetLibrary.logAssetError("<e>PowerLibrary</e>: <b>show_tool_sizes</b> is enabled - but <b>force_brush</b> is set to <b>" + tAsset.force_brush + "</b> - making the tool sizes useless", tAsset.id);
			}
			if (tAsset.show_tool_sizes && tAsset.click_brush_action == null && tAsset.click_power_brush_action == null)
			{
				BaseAssetLibrary.logAssetError("<e>PowerLibrary</e>: <b>show_tool_sizes</b> is enabled - but <b>click_brush_action</b> and <b>click_power_brush_action</b> are not set - making the tool sizes useless", tAsset.id);
			}
		}
		this.localeChecks();
		this.callbackChecks();
		base.editorDiagnosticLocales();
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0002C558 File Offset: 0x0002A758
	private void localeChecks()
	{
		foreach (GodPower tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0002C5C0 File Offset: 0x0002A7C0
	private void callbackChecks()
	{
		foreach (GodPower tAsset in this.list)
		{
			if (tAsset.click_action != null)
			{
				Delegate[] invocationList;
				if (tAsset.click_brush_action != null)
				{
					bool tFound = false;
					invocationList = tAsset.click_brush_action.GetInvocationList();
					for (int i = 0; i < invocationList.Length; i++)
					{
						if (invocationList[i].Method.GetCustomAttributes(typeof(ClickActionCallerAttribute), true).Length != 0)
						{
							tFound = true;
						}
					}
					if (!tFound)
					{
						string tClickActionNames = tAsset.click_action.AsString<PowerActionWithID>();
						string tClickBrushActionNames = tAsset.click_brush_action.AsString<PowerActionWithID>();
						BaseAssetLibrary.logAssetError(string.Concat(new string[]
						{
							"<e>PowerLibrary</e>: <b>click_brush_action</b> (",
							tClickBrushActionNames,
							") overrides <b>click_action</b> (",
							tClickActionNames,
							") - either add <b>loopWithBrush</b> which will call them - or mark a similar caller method with [ClickActionCaller] attribute"
						}), tAsset.id);
					}
				}
				invocationList = tAsset.click_action.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Method.GetCustomAttributes(typeof(ClickActionCallerAttribute), true).Length != 0)
					{
						BaseAssetLibrary.logAssetError("<e>PowerLibrary</e>: <b>click_action</b> (" + tAsset.click_action.AsString<PowerActionWithID>() + ") has [ClickActionCaller] attribute - it should be used only in <b>click_brush_action</b>", tAsset.id);
					}
				}
			}
			if (tAsset.click_power_action != null)
			{
				Delegate[] invocationList;
				if (tAsset.click_power_brush_action != null)
				{
					bool tFound2 = false;
					invocationList = tAsset.click_power_brush_action.GetInvocationList();
					for (int i = 0; i < invocationList.Length; i++)
					{
						if (invocationList[i].Method.GetCustomAttributes(typeof(ClickPowerActionCallerAttribute), true).Length != 0)
						{
							tFound2 = true;
						}
					}
					if (!tFound2)
					{
						string tClickPowerActionNames = tAsset.click_power_action.AsString<PowerAction>();
						string tClickPowerBrushActionNames = tAsset.click_power_brush_action.AsString<PowerAction>();
						BaseAssetLibrary.logAssetError(string.Concat(new string[]
						{
							"<e>PowerLibrary</e>: <b>click_power_brush_action</b> (",
							tClickPowerBrushActionNames,
							") overrides <b>click_power_action</b> (",
							tClickPowerActionNames,
							") - either add <b>loopWithCurrentBrushPower</b> which will call them - or mark a similar caller method with [ClickPowerActionCaller] attribute"
						}), tAsset.id);
					}
				}
				invocationList = tAsset.click_power_action.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Method.GetCustomAttributes(typeof(ClickPowerActionCallerAttribute), true).Length != 0)
					{
						BaseAssetLibrary.logAssetError("<e>PowerLibrary</e>: <b>click_power_action</b> (" + tAsset.click_power_action.AsString<PowerAction>() + ") has [ClickPowerActionCaller] attribute - it should be used only in <b>click_power_brush_action</b>", tAsset.id);
					}
				}
			}
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0002C834 File Offset: 0x0002AA34
	public string addToGameplayReport(string pWhat)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhat + "\n";
		foreach (GodPower godPower in this.list)
		{
			string tName = godPower.getTranslatedName();
			string tDescription = godPower.getTranslatedDescription();
			string tLineInfo = "\n" + tName;
			tLineInfo += "\n";
			if (!string.IsNullOrEmpty(tDescription))
			{
				tLineInfo = tLineInfo + "1: " + tDescription;
			}
			tResult += tLineInfo;
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x04000340 RID: 832
	private const string TEMPLATE_EXPLOSIVE_TILES = "$template_explosive_tiles$";

	// Token: 0x04000341 RID: 833
	private const string TEMPLATE_BOMBS = "$template_bombs$";

	// Token: 0x04000342 RID: 834
	private const string TEMPLATE_DROPS = "$template_drops$";

	// Token: 0x04000343 RID: 835
	private const string TEMPLATE_SEEDS = "$template_seeds$";

	// Token: 0x04000344 RID: 836
	private const string TEMPLATE_PLANTS = "$template_plants$";

	// Token: 0x04000345 RID: 837
	private const string TEMPLATE_DROP_MINERALS = "$template_minerals$";

	// Token: 0x04000346 RID: 838
	private const string TEMPLATE_DROP_BUILDING = "$template_drop_building$";

	// Token: 0x04000347 RID: 839
	private const string TEMPLATE_PRINTER = "$template_printer$";

	// Token: 0x04000348 RID: 840
	private const string TEMPLATE_SPAWN_SPECIAL = "$template_spawn_special$";

	// Token: 0x04000349 RID: 841
	private const string TEMPLATE_SPAWN_ACTOR = "$template_spawn_actor$";

	// Token: 0x0400034A RID: 842
	private const string TEMPLATE_TERRAFORM_TILES = "$template_terraform_tiles$";

	// Token: 0x0400034B RID: 843
	private const string TEMPLATE_WALL = "$template_wall$";

	// Token: 0x0400034C RID: 844
	private const string TEMPLATE_DRAW = "$template_draw$";

	// Token: 0x0400034D RID: 845
	private const string TEMPLATE_ERASER = "$template_eraser$";

	// Token: 0x0400034E RID: 846
	public static GodPower traits_gamma_rain_edit;

	// Token: 0x0400034F RID: 847
	public static GodPower traits_delta_rain_edit;

	// Token: 0x04000350 RID: 848
	public static GodPower traits_omega_rain_edit;

	// Token: 0x04000351 RID: 849
	public static GodPower equipment_rain_edit;

	// Token: 0x04000352 RID: 850
	public static GodPower inspect_unit;
}
