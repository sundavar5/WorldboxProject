using System;
using System.Collections.Generic;
using System.Reflection;
using Beebyte.Obfuscator;
using strings;
using UnityEngine;

// Token: 0x0200009F RID: 159
[ObfuscateLiterals]
[Serializable]
public class ActorAssetLibrary : BaseLibraryWithUnlockables<ActorAsset>
{
	// Token: 0x06000529 RID: 1321 RVA: 0x00036E04 File Offset: 0x00035004
	public int getHumanoidsAmount()
	{
		return this._humanoids_amount;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00036E0C File Offset: 0x0003500C
	public override void init()
	{
		Debug.Log("INIT ActorStats");
		base.init();
		this.initTemplates();
		this.initCivsClassic();
		this.initAnimalsNormal();
		this.initAnimalsWeird();
		this.initInsects();
		this.initMobsOther();
		this.initCivsNew();
		this.initSpecial();
		this.initAnts();
		this.initCreepMobs();
		this.initBoats();
		Debug.Log("GENERATE ACTOR STATS " + this.list.Count.ToString());
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00036E90 File Offset: 0x00035090
	private void initSpecial()
	{
		this.clone("greg", "$mob$");
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"greg_set"
		});
		this.t.kingdom_id_wild = "greg";
		this.t.kingdom_id_civilization = "miniciv_greg";
		this.t.name_taxonomic_kingdom = "gregalia";
		this.t.name_taxonomic_phylum = "gregata";
		this.t.name_taxonomic_class = "gregia";
		this.t.name_taxonomic_order = "greges";
		this.t.name_taxonomic_family = "gregae";
		this.t.name_taxonomic_genus = "greg";
		this.t.name_taxonomic_species = "greg";
		this.t.collective_term = "group_grex";
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTrait("enhanced_strength");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("big_stomach");
		this.t.addSubspeciesTrait("diet_geophagy");
		this.t.addSubspeciesTrait("diet_xylophagy");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("exoskeleton");
		this.t.addSubspeciesTrait("fenix_born");
		this.t.addSubspeciesTrait("parental_care");
		this.t.addSubspeciesTrait("reproduction_parthenogenesis");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("gestation_extremely_long");
		this.t.addSubspeciesTrait("voracious");
		this.t.addSubspeciesTrait("egg_face");
		this.t.addReligionTrait("echo_of_the_void");
		this.t.addTrait("giant");
		this.t.addTrait("strong");
		this.t.addTrait("regeneration");
		this.addPhenotype("bright_yellow", "default_color");
		this.t.special = true;
		this.t.unit_other = true;
		this.t.name_locale = "Greg";
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.shadow_texture = "unitShadow_6";
		this.t.animation_walk = ActorAnimationSequences.walk_0;
		this.t.animation_swim = ActorAnimationSequences.walk_0;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.architecture_id = "civ_greg";
		this.t.banner_id = "human";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 500f),
			new ValueTuple<string, float>("stamina", 200f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 100f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("speed", 40f)
		});
		this.t.icon = "iconGreg";
		this.t.color_hex = "#24803E";
		this.t.rotating_animation = true;
		this.t.has_soul = true;
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.addResource("worms", 2, true);
		this.t.addResource("evil_beets", 1, false);
		this.clone("living_plants", "$mob_no_genes$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"living_plant_set"
		});
		this.t.use_phenotypes = false;
		this.t.special = true;
		this.t.has_baby_form = false;
		this.t.name_locale = "living_plant";
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.shadow_texture = "unitShadow_6";
		this.t.kingdom_id_wild = "living_plants";
		this.t.base_stats["health"] = 300f;
		this.t.base_stats["speed"] = 10f;
		this.t.base_stats["armor"] = 0f;
		this.t.base_stats["attack_speed"] = 70f;
		this.t.base_stats["damage"] = 30f;
		this.t.base_stats["knockback"] = 1.2f;
		this.t.base_stats["mass"] = 50f;
		this.t.base_stats["mass_2"] = 500f;
		this.t.base_stats["targets"] = 3f;
		this.t.damaged_by_ocean = true;
		this.t.icon = "iconLivingPlants";
		this.t.show_icon_inspect_window = true;
		this.t.show_icon_inspect_window_id = "iconLivingPlants";
		this.t.color_hex = "#115D11";
		this.t.rotating_animation = true;
		this.t.disable_jump_animation = false;
		this.t.inspect_avatar_scale = 1f;
		this.t.base_stats["scale"] = 0.25f;
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.action_on_load = delegate(Actor pActor)
		{
			string tStringData;
			pActor.data.get("special_sprite_id", out tStringData, null);
			if (string.IsNullOrEmpty(tStringData))
			{
				return;
			}
			AssetManager.buildings.get(tStringData).checkSpritesAreLoaded();
		};
		this.t.get_override_sprite = delegate(Actor pActor)
		{
			string tStringData;
			pActor.data.get("special_sprite_id", out tStringData, null);
			int tAnimationIndex;
			pActor.data.get("special_sprite_index", out tAnimationIndex, 0);
			if (string.IsNullOrEmpty(tStringData))
			{
				tStringData = "ui/Icons/iconLivingPlants";
				return SpriteTextureLoader.getSprite(tStringData);
			}
			return AssetManager.buildings.get(tStringData).building_sprites.animation_data[tAnimationIndex].main[0];
		};
		this.addTrait("regeneration");
		this.t.music_theme = "Units_LivingPlants";
		this.t.sound_hit = "event:/SFX/HIT/HitGeneric";
		this.t.show_in_knowledge_window = false;
		this.t.use_items = false;
		this.t.take_items = false;
		this.t.can_edit_equipment = false;
		this.t.use_tool_items = false;
		this.t.addResource("herbs", 1, true);
		this.clone("living_house", "$mob_no_genes$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"living_house_set"
		});
		this.t.use_phenotypes = false;
		this.t.has_baby_form = false;
		this.t.special = true;
		this.t.name_locale = "living_house";
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.shadow_texture = "unitShadow_6";
		this.t.kingdom_id_wild = "living_houses";
		this.t.base_stats["health"] = 500f;
		this.t.base_stats["armor"] = 1f;
		this.t.base_stats["speed"] = 10f;
		this.t.base_stats["attack_speed"] = 60f;
		this.t.base_stats["damage"] = 50f;
		this.t.base_stats["knockback"] = 1.4f;
		this.t.base_stats["mass"] = 50f;
		this.t.base_stats["mass_2"] = 10000f;
		this.t.base_stats["targets"] = 3f;
		this.t.damaged_by_ocean = true;
		this.t.icon = "iconLivingHouse";
		this.t.show_icon_inspect_window = true;
		this.t.show_icon_inspect_window_id = "iconLivingHouse";
		this.t.color_hex = "#24803E";
		this.t.rotating_animation = true;
		this.t.disable_jump_animation = false;
		this.t.inspect_avatar_scale = 1f;
		this.t.base_stats["scale"] = 0.25f;
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.get_override_sprite = this.get("living_plants").get_override_sprite;
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.t.show_in_knowledge_window = false;
		this.t.use_items = false;
		this.t.take_items = false;
		this.t.can_edit_equipment = false;
		this.t.use_tool_items = false;
		this.t.addResource("wood", 1, true);
		this.add(this.t = new ActorAsset
		{
			id = "dragon",
			kingdom_id_wild = "dragons",
			special = true,
			can_be_killed_by_stuff = true,
			can_be_killed_by_life_eraser = true,
			ignore_tile_speed_multiplier = true,
			skip_fight_logic = true,
			job = AssetLibrary<ActorAsset>.a<string>(new string[]
			{
				"dragon_job"
			}),
			can_be_moved_by_powers = true,
			can_be_hurt_by_powers = true,
			update_z = true,
			default_height = 0f,
			effect_damage = true,
			can_flip = true,
			can_turn_into_zombie = true,
			actor_size = ActorSize.S17_Dragon,
			shadow_texture = "unitShadow_7",
			can_be_inspected = true,
			hide_favorite_icon = true,
			icon = "iconDragon",
			inspect_avatar_scale = 1.1f,
			inspect_avatar_offset_y = -22f,
			avatar_prefab = "p_dragon",
			visible_on_minimap = true,
			die_on_blocks = false,
			ignore_blocks = true,
			move_from_block = false,
			run_to_water_when_on_fire = false,
			split_ai_update = false,
			experience_given = 100,
			can_be_surprised = false,
			allow_possession = false,
			show_task_icon = false,
			can_talk_with = false,
			control_can_backstep = false,
			control_can_dash = false,
			control_can_jump = false,
			control_can_kick = false,
			control_can_talk = false,
			control_can_swear = false,
			control_can_steal = false,
			inspect_mind = false
		});
		this.t.setCanTurnIntoZombieAsset("zombie_dragon", false);
		this.t.get_override_sprite = ((Actor pActor) => pActor.getSpriteAnimation().currentSpriteGraphic);
		this.t.get_override_avatar_frames = ((Actor pActor) => PrefabLibrary.instance.dragonAsset.getAsset(DragonState.Fly).frames);
		this.t.allowed_status_tiers = StatusTier.Basic;
		this.t.render_status_effects = false;
		this.t.name_locale = "Dragon";
		this.t.die_in_lava = false;
		this.t.cancel_beh_on_land = false;
		this.t.base_stats["health"] = 1000f;
		this.t.base_stats["damage"] = 100f;
		this.t.base_stats["speed"] = 40f;
		this.t.base_stats["scale"] = 0.3f;
		this.t.base_stats["size"] = 2f;
		this.t.base_stats["mass"] = 4f;
		this.t.base_stats["mass_2"] = 2500f;
		this.t.base_stats["targets"] = 20f;
		this.t.base_stats["lifespan"] = 10000f;
		this.addTrait("regeneration");
		this.addTrait("strong_minded");
		this.addTrait("fire_proof");
		this.addTrait("fire_blood");
		this.t.addResource("dragon_scales", 10, true);
		this.t.addResource("meat", 10, false);
		this.t.addResource("bones", 10, false);
		this.t.music_theme = "Units_Dragon";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		ActorAsset t = this.t;
		t.action_click = (WorldAction)Delegate.Combine(t.action_click, new WorldAction(Dragon.clickToWakeup));
		this.t.action_dead_animation = new DeadAnimation(Dragon.dragonFall);
		ActorAsset t2 = this.t;
		t2.action_death = (WorldAction)Delegate.Combine(t2.action_death, new WorldAction(ActionLibrary.dragonSlayer));
		ActorAsset t3 = this.t;
		t3.action_get_hit = (GetHitAction)Delegate.Combine(t3.action_get_hit, new GetHitAction(Dragon.getHit));
		this.t.check_flip = new WorldAction(Dragon.canFlip);
		this.t.animation_speed_based_on_walk_speed = false;
		this.t.needs_to_be_explored = false;
		this.t.use_tool_items = false;
		this.clone("zombie_dragon", "dragon");
		this.t.special = true;
		this.t.name_locale = "Zombie";
		this.t.avatar_prefab = "p_zombie_dragon";
		this.t.visible_on_minimap = true;
		this.t.die_in_lava = true;
		this.t.show_in_knowledge_window = false;
		this.t.setZombie(false);
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"dragon_job"
		});
		this.removeTrait("fire_proof");
		this.removeTrait("fire_blood");
		this.addTrait("acid_blood");
		this.addTrait("acid_proof");
		this.add(this.t = new ActorAsset
		{
			id = "UFO",
			kingdom_id_wild = "aliens",
			special = true,
			can_be_killed_by_stuff = true,
			can_be_killed_by_life_eraser = true,
			ignore_tile_speed_multiplier = true,
			skip_fight_logic = true,
			job = AssetLibrary<ActorAsset>.a<string>(new string[]
			{
				"ufo_job"
			}),
			flying = true,
			very_high_flyer = true,
			can_be_moved_by_powers = true,
			can_be_hurt_by_powers = true,
			effect_damage = true,
			special_dead_animation = true,
			actor_size = ActorSize.S17_Dragon,
			shadow_texture = "unitShadow_7",
			can_be_inspected = true,
			icon = "iconUfo",
			hide_favorite_icon = true,
			die_by_lightning = true,
			avatar_prefab = "p_ufo",
			visible_on_minimap = true,
			die_on_blocks = false,
			ignore_blocks = true,
			move_from_block = false,
			run_to_water_when_on_fire = false,
			has_skin = false,
			flag_ufo = true,
			split_ai_update = false,
			allow_possession = false,
			show_task_icon = false,
			can_talk_with = false,
			control_can_backstep = false,
			control_can_dash = false,
			control_can_jump = false,
			control_can_kick = false,
			control_can_talk = false,
			control_can_swear = false,
			control_can_steal = false
		});
		this.t.get_override_sprite = ((Actor pActor) => pActor.getSpriteAnimation().currentSpriteGraphic);
		this.t.allowed_status_tiers = StatusTier.Basic;
		this.t.render_status_effects = false;
		this.t.inspect_avatar_scale = 1.45f;
		this.t.inspect_avatar_offset_y = 8f;
		this.t.name_locale = "UFO";
		this.t.name_template_unit = "ufo_name";
		this.t.base_stats["health"] = 1000f;
		this.t.base_stats["armor"] = 5f;
		this.t.base_stats["scale"] = 0.3f;
		this.t.base_stats["speed"] = 20f;
		this.t.base_stats["damage"] = 80f;
		this.t.base_stats["lifespan"] = 0f;
		this.t.base_stats["size"] = 2f;
		ActorAsset t4 = this.t;
		t4.action_death = (WorldAction)Delegate.Combine(t4.action_death, new WorldAction(ActionLibrary.spawnAliens));
		this.t.base_stats["mass"] = 5f;
		this.t.base_stats["mass_2"] = 2500f;
		this.t.action_dead_animation = new DeadAnimation(UFO.ufoFall);
		ActorAsset t5 = this.t;
		t5.action_click = (WorldAction)Delegate.Combine(t5.action_click, new WorldAction(UFO.click));
		ActorAsset t6 = this.t;
		t6.action_get_hit = (GetHitAction)Delegate.Combine(t6.action_get_hit, new GetHitAction(UFO.getHit));
		this.t.prevent_unconscious_rotation = true;
		this.addTrait("strong_minded");
		this.addTrait("fire_proof");
		this.addTrait("light_lamp");
		this.t.music_theme = "Units_UFO";
		this.t.sound_hit = "event:/SFX/HIT/HitMetal";
		this.t.needs_to_be_explored = false;
		this.t.can_be_surprised = false;
		this.t.use_tool_items = false;
		this.t.default_height = 8f;
		ActorAsset actorAsset = new ActorAsset();
		actorAsset.id = "crabzilla";
		actorAsset.kingdom_id_wild = "crabzilla";
		actorAsset.special = true;
		actorAsset.can_be_killed_by_stuff = true;
		actorAsset.ignore_tile_speed_multiplier = true;
		actorAsset.skip_fight_logic = true;
		actorAsset.flying = false;
		actorAsset.can_be_moved_by_powers = false;
		actorAsset.can_be_hurt_by_powers = true;
		actorAsset.update_z = true;
		actorAsset.can_flip = false;
		actorAsset.skip_save = true;
		actorAsset.avatar_prefab = "p_crabzilla";
		actorAsset.visible_on_minimap = true;
		actorAsset.ignore_generic_render = true;
		actorAsset.die_on_blocks = false;
		actorAsset.ignore_blocks = true;
		actorAsset.move_from_block = false;
		actorAsset.run_to_water_when_on_fire = false;
		actorAsset.ignored_by_infinity_coin = true;
		actorAsset.split_ai_update = false;
		actorAsset.has_ai_system = false;
		actorAsset.show_in_knowledge_window = false;
		actorAsset.show_task_icon = false;
		actorAsset.can_talk_with = false;
		actorAsset.control_can_backstep = false;
		actorAsset.control_can_dash = false;
		actorAsset.control_can_jump = false;
		actorAsset.control_can_kick = false;
		actorAsset.control_can_talk = false;
		actorAsset.control_can_swear = false;
		actorAsset.control_can_steal = false;
		actorAsset.show_controllable_tip = false;
		ActorAsset pAsset = actorAsset;
		this.t = actorAsset;
		this.add(pAsset);
		this.t.allowed_status_tiers = StatusTier.None;
		this.t.has_sprite_renderer = false;
		this.t.name_locale = "Crabzilla";
		this.t.icon = "iconCrabzilla";
		this.t.base_stats["scale"] = 0.25f;
		this.t.actor_size = ActorSize.S17_Dragon;
		this.t.base_stats["health"] = 10000f;
		this.t.base_stats["speed"] = 50f;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["size"] = 8f;
		this.t.base_stats["mass_2"] = 99999f;
		this.t.can_level_up = false;
		this.t.shadow = false;
		this.t.hit_fx_alternative_offset = false;
		ActorAsset t7 = this.t;
		t7.action_death = (WorldAction)Delegate.Combine(t7.action_death, new WorldAction(ActionLibrary.clearCrabzilla));
		ActorAsset t8 = this.t;
		t8.action_death = (WorldAction)Delegate.Combine(t8.action_death, new WorldAction(ActionLibrary.startCrabzillaNuke));
		this.t.action_dead_animation = delegate(BaseSimObject pTarget, WorldTile _, float _)
		{
			pTarget.a.dieAndDestroy(AttackType.None);
			return true;
		};
		ActorAsset t9 = this.t;
		t9.action_get_hit = (GetHitAction)Delegate.Combine(t9.action_get_hit, new GetHitAction(Crabzilla.getHit));
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.addTrait("strong_minded");
		this.t.needs_to_be_explored = false;
		this.t.can_be_surprised = false;
		this.t.use_tool_items = false;
		this.add(this.t = new ActorAsset
		{
			id = "god_finger",
			special = true,
			actor_size = ActorSize.S17_Dragon,
			shadow_texture = "unitShadow_7",
			kingdom_id_wild = "godfinger",
			can_be_killed_by_stuff = true,
			ignore_tile_speed_multiplier = true,
			can_be_killed_by_life_eraser = true,
			skip_fight_logic = true,
			can_be_moved_by_powers = true,
			can_be_hurt_by_powers = true,
			update_z = false,
			effect_damage = true,
			skip_save = true,
			avatar_prefab = "p_god_finger",
			visible_on_minimap = true,
			die_on_blocks = false,
			ignore_blocks = true,
			move_from_block = false,
			run_to_water_when_on_fire = false,
			split_ai_update = false,
			has_ai_system = true,
			job = AssetLibrary<ActorAsset>.a<string>(new string[]
			{
				"godfinger_job"
			}),
			flying = true,
			very_high_flyer = true,
			die_by_lightning = true,
			show_in_knowledge_window = false,
			allow_possession = false,
			show_task_icon = false,
			can_talk_with = false,
			control_can_backstep = false,
			control_can_dash = false,
			control_can_jump = false,
			control_can_kick = false,
			control_can_talk = false,
			control_can_swear = false,
			control_can_steal = false,
			show_in_taxonomy_tooltip = false
		});
		this.t.get_override_sprite = ((Actor pActor) => pActor.getSpriteAnimation().currentSpriteGraphic);
		this.t.allowed_status_tiers = StatusTier.Basic;
		this.t.render_status_effects = false;
		this.t.flag_finger = true;
		this.t.name_locale = "God Finger";
		this.t.base_stats["scale"] = 0.3f;
		this.t.base_stats["mass"] = 5f;
		this.t.base_stats["mass_2"] = 99999f;
		this.t.base_stats["speed"] = 50f;
		this.t.base_stats["damage"] = 80f;
		this.t.base_stats["lifespan"] = 10000f;
		this.t.base_stats["size"] = 2f;
		this.t.base_stats["health"] = 100f;
		this.t.base_stats["armor"] = 5f;
		this.t.die_in_lava = false;
		this.addTrait("light_lamp");
		this.addTrait("strong_minded");
		this.addTrait("fire_proof");
		this.t.music_theme = "Units_GodFinger";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.needs_to_be_explored = false;
		ActorAsset t10 = this.t;
		t10.action_dead_animation = (DeadAnimation)Delegate.Combine(t10.action_dead_animation, new DeadAnimation(GodFinger.deathFlip));
		this.t.can_be_surprised = false;
		this.t.use_tool_items = false;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x000386DC File Offset: 0x000368DC
	private void initAnts()
	{
		this.clone("ant_black", "$basic_unit$");
		this.t.allow_possession = false;
		this.t.can_be_cloned = false;
		this.t.show_task_icon = false;
		this.t.kingdom_id_wild = "ants";
		this.t.can_be_killed_by_stuff = true;
		this.t.can_be_killed_by_life_eraser = true;
		this.t.ignore_tile_speed_multiplier = true;
		this.t.skip_fight_logic = true;
		this.t.can_be_moved_by_powers = true;
		this.t.can_be_hurt_by_powers = true;
		this.t.update_z = true;
		this.t.effect_damage = true;
		this.t.can_flip = true;
		this.t.actor_size = ActorSize.S13_Human;
		this.t.color_hex = "#000000";
		this.t.die_on_blocks = false;
		this.t.ignore_blocks = true;
		this.t.move_from_block = false;
		this.t.run_to_water_when_on_fire = false;
		this.t.force_land_creature = true;
		this.t.force_ocean_creature = true;
		this.t.split_ai_update = false;
		this.t.can_be_inspected = true;
		this.t.name_locale = "Black Ant";
		this.t.name_template_unit = "ant_name";
		this.t.unit_other = true;
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ant_black"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0;
		this.t.base_stats["speed"] = 20f;
		this.t.base_stats["mass_2"] = 0.01f;
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.icon = "iconAntBlack";
		this.addTrait("strong_minded");
		this.t.generateFmodPaths("ant_black");
		this.t.can_be_surprised = false;
		this.t.use_tool_items = false;
		this.t.inspect_mind = false;
		this.t.inspect_genealogy = false;
		this.t.can_talk_with = false;
		this.clone("ant_green", "ant_black");
		this.t.name_locale = "Green Ant";
		this.t.icon = "iconAntGreen";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ant_green"
		});
		this.t.color_hex = "#007F0E";
		this.clone("ant_blue", "ant_black");
		this.t.name_locale = "Blue Ant";
		this.t.icon = "iconAntBlue";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ant_blue"
		});
		this.t.color_hex = "#0094FF";
		this.clone("ant_red", "ant_black");
		this.t.name_locale = "Red Ant";
		this.t.icon = "iconAntRed";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ant_red"
		});
		this.t.color_hex = "#FF2511";
		this.clone("sand_spider", "ant_black");
		this.t.name_locale = "Sand Spider";
		this.t.icon = "iconSandSpider";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"sandspider_job"
		});
		this.t.color_hex = "#2D2D2D";
		this.t.base_stats["speed"] = 100f;
		this.clone("worm", "ant_black");
		this.t.unit_other = true;
		this.t.can_be_inspected = false;
		this.t.name_template_unit = "bug_name";
		this.t.name_locale = "Worm";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"worm_job"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0;
		this.t.can_be_moved_by_powers = false;
		this.t.can_be_hurt_by_powers = true;
		this.t.can_be_killed_by_stuff = false;
		this.t.kingdom_id_wild = "nature";
		this.t.base_stats["speed"] = 100f;
		this.t.base_stats["mass_2"] = 0.05f;
		this.t.color_hex = null;
		this.t.shadow = false;
		this.addTrait("fire_proof");
		this.t.music_theme = "Units_Worm";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.clone("printer", "ant_black");
		this.t.kingdom_id_wild = "nature";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"printer_job"
		});
		this.t.can_be_moved_by_powers = false;
		this.t.can_be_hurt_by_powers = true;
		this.t.update_z = true;
		this.t.effect_damage = true;
		this.t.can_flip = true;
		this.t.skip_save = true;
		this.t.die_on_blocks = false;
		this.t.ignore_blocks = true;
		this.t.move_from_block = false;
		this.t.run_to_water_when_on_fire = false;
		this.t.ignored_by_infinity_coin = true;
		this.t.split_ai_update = false;
		this.t.unit_other = true;
		this.t.name_locale = "Printer";
		this.t.base_stats["health"] = 1f;
		this.t.base_stats["speed"] = 10000f;
		this.t.base_stats["mass"] = 10f;
		this.t.animation_walk = ActorAnimationSequences.walk_0_2;
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00038D30 File Offset: 0x00036F30
	private void initCreepMobs()
	{
		this.clone("$creep_mob$", "$mob$");
		this.t.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"advanced_brain",
			"phenotypes"
		});
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"default_set"
		});
		this.t.base_stats["speed"] = 10f;
		this.t.has_advanced_textures = false;
		this.t.can_turn_into_ice_one = false;
		this.t.has_baby_form = false;
		this.t.kingdom_id_civilization = string.Empty;
		this.t.build_order_template_id = string.Empty;
		this.clone("mush_unit", "$creep_mob$");
		this.t.can_edit_equipment = true;
		this.t.take_items = true;
		this.t.use_items = true;
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "ascomycota";
		this.t.name_taxonomic_class = "sordariomycetes";
		this.t.name_taxonomic_order = "hypocreales";
		this.t.name_taxonomic_family = "cordycipitaceae";
		this.t.name_taxonomic_genus = "cordyceps";
		this.t.name_taxonomic_species = "puppetus";
		this.t.collective_term = "group_mycelium";
		this.addPhenotype("dark_green", "default_color");
		this.t.base_stats["mass_2"] = 60f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 300f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 25f),
			new ValueTuple<string, float>("attack_speed", 30f),
			new ValueTuple<string, float>("speed", 30f)
		});
		this.t.unit_other = true;
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.collective_term = "group_mycelium";
		this.t.name_locale = "Mush";
		this.t.body_separate_part_hands = true;
		this.t.kingdom_id_wild = "mush";
		this.t.can_be_killed_by_divine_light = true;
		this.t.icon = "actor_traits/iconMushSpores";
		this.t.color_hex = "#FF49CB";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.addTrait("weightless");
		this.addTrait("mush_spores");
		this.addTrait("regeneration");
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.clone("mush_animal", "mush_unit");
		this.t.name_taxonomic_species = "pippitus";
		this.t.show_in_taxonomy_tooltip = false;
		this.t.icon = "actor_traits/iconMushSpores";
		this.t.unit_other = true;
		this.t.base_stats["health"] = 200f;
		this.t.base_stats["mass_2"] = 45f;
		this.t.body_separate_part_hands = false;
		this.t.use_items = false;
		this.t.take_items = false;
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.clone("tumor_monster_unit", "$mob$");
		this.t.needs_to_be_explored = true;
		this.t.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"advanced_brain"
		});
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"default_set"
		});
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "neoplasia";
		this.t.name_taxonomic_class = "malignomorpha";
		this.t.name_taxonomic_order = "oncovorales";
		this.t.name_taxonomic_family = "tumoridae";
		this.t.name_taxonomic_genus = "neoplasmus";
		this.t.name_taxonomic_species = "carcinomus";
		this.t.collective_term = "group_cancer";
		this.addPhenotype("dark_salmon", "default_color");
		this.t.base_stats["mass_2"] = 75f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 6f)
		});
		this.t.icon = "iconTumor";
		this.t.unit_other = true;
		this.t.name_locale = "Tumor Monster";
		this.t.immune_to_tumor = true;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_ice_one = false;
		this.t.body_separate_part_hands = true;
		this.t.kingdom_id_wild = "tumor";
		this.t.icon = "iconTumorMonster";
		this.t.color_hex = "#FF49CB";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.kingdom_id_civilization = string.Empty;
		this.t.build_order_template_id = string.Empty;
		this.addTrait("weightless");
		this.addTrait("ugly");
		this.addTraitIgnore("bomberman");
		this.addTraitIgnore("pyromaniac");
		this.t.music_theme = "Buildings_Tumor";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.clone("tumor_monster_animal", "tumor_monster_unit");
		this.t.show_in_taxonomy_tooltip = false;
		this.t.show_in_knowledge_window = false;
		this.t.base_asset_id = "tumor_monster_unit";
		this.t.icon = "iconTumorMonster";
		this.t.unit_other = true;
		this.t.body_separate_part_hands = false;
		this.t.use_items = false;
		this.t.take_items = false;
		this.t.mush_id = "mush_animal";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.base_stats["mass_2"] = 55f;
		this.clone("lil_pumpkin", "tumor_monster_animal");
		this.t.show_in_knowledge_window = true;
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"pumpkin_set"
		});
		this.t.show_in_taxonomy_tooltip = true;
		this.t.name_taxonomic_kingdom = "plantae";
		this.t.name_taxonomic_phylum = "angiospermae";
		this.t.name_taxonomic_class = "dicotyledonae";
		this.t.name_taxonomic_order = "cucurbitales";
		this.t.name_taxonomic_family = "cucurbitaceae";
		this.t.name_taxonomic_genus = "worldboxus";
		this.t.name_taxonomic_species = "maximus";
		this.t.collective_term = "group_squash";
		this.t.addSubspeciesTrait("egg_pumpkin");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.clearPhenotypes();
		this.addPhenotype("dark_orange", "default_color");
		this.t.base_stats["mass_2"] = 9f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1000f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 6f)
		});
		this.t.icon = "iconLilPumpkin";
		this.t.unit_other = true;
		this.t.name_locale = "Lil Pumpkin";
		this.t.kingdom_id_wild = "super_pumpkin";
		this.t.immune_to_slowness = true;
		this.t.mush_id = "mush_animal";
		this.t.clearTraits();
		this.addTrait("attractive");
		this.addTrait("fat");
		this.addTrait("bloodlust");
		this.addTrait("thorns");
		this.addTraitIgnore("bomberman");
		this.addTraitIgnore("pyromaniac");
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.base_asset_id = null;
		this.t.addResource("herbs", 1, true);
		this.t.addResource("tea", 1, false);
		this.clone("assimilator", "tumor_monster_animal");
		this.t.show_in_knowledge_window = true;
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"assimilator_set"
		});
		this.t.show_in_taxonomy_tooltip = true;
		this.clearPhenotypes();
		this.addPhenotype("black_blue", "default_color");
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTrait("egg_metal_box");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.name_taxonomic_kingdom = "machina";
		this.t.name_taxonomic_phylum = "cybernetica";
		this.t.name_taxonomic_class = "slowupdata";
		this.t.name_taxonomic_order = "noupdates";
		this.t.name_taxonomic_family = "assimiladae";
		this.t.name_taxonomic_genus = "assimilatus";
		this.t.name_taxonomic_species = "perfectus";
		this.t.collective_term = "group_network";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1000f),
			new ValueTuple<string, float>("stamina", 300f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("armor", 20f),
			new ValueTuple<string, float>("speed", 6f)
		});
		this.t.icon = "iconAssimilator";
		this.t.unit_other = true;
		this.t.name_locale = "Assimilator";
		this.t.inspect_avatar_scale = 2.1f;
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_zombie = false;
		this.t.kingdom_id_wild = "assimilators";
		this.t.base_stats["mass_2"] = 15f;
		this.t.base_stats["damage"] = 0f;
		this.t.body_separate_part_hands = true;
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"shotgun"
		});
		this.addTrait("fire_proof");
		this.addTrait("bubble_defense");
		this.removeTrait("ugly");
		this.removeTrait("weightless");
		this.addTraitIgnore("bomberman");
		this.addTraitIgnore("pyromaniac");
		this.t.sound_hit = "event:/SFX/HIT/HitMetal";
		this.t.use_items = true;
		this.t.base_asset_id = null;
		this.t.addResource("adamantine", 1, true);
		this.t.addResource("common_metals", 1, false);
		this.clone("bioblob", "tumor_monster_animal");
		this.t.show_in_knowledge_window = true;
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"bioblob_set"
		});
		this.t.show_in_taxonomy_tooltip = true;
		this.t.name_taxonomic_kingdom = "protista";
		this.t.name_taxonomic_phylum = "amoebozoa";
		this.t.name_taxonomic_class = "myxogastria";
		this.t.name_taxonomic_order = "physarales";
		this.t.name_taxonomic_family = "blobidae";
		this.t.name_taxonomic_genus = "blobus";
		this.t.name_taxonomic_species = "opticus";
		this.t.collective_term = "group_blob";
		this.t.addSubspeciesTrait("egg_eyeball");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.addTrait("strong_minded");
		this.addTrait("ugly");
		this.addTrait("fragile_health");
		this.clearPhenotypes();
		this.addPhenotype("toxic_green", "default_color");
		this.t.base_stats["mass_2"] = 15f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 20f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("armor", 20f),
			new ValueTuple<string, float>("speed", 6f)
		});
		this.t.icon = "iconBioblob";
		this.t.unit_other = true;
		this.t.name_locale = "Bioblob";
		this.t.kingdom_id_wild = "biomass";
		this.t.immune_to_slowness = true;
		this.t.mush_id = "mush_animal";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.prevent_unconscious_rotation = true;
		this.t.base_asset_id = null;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00039CE3 File Offset: 0x00037EE3
	public void clearPhenotypes()
	{
		this.t.phenotypes_dict = null;
		this.t.phenotypes_list = null;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00039D00 File Offset: 0x00037F00
	public void addPhenotype(string pID, string pType = "default_color")
	{
		if (this.t.phenotypes_dict == null)
		{
			this.t.phenotypes_dict = new Dictionary<string, List<string>>();
			this.t.phenotypes_list = new List<string>();
		}
		if (!this.t.phenotypes_dict.ContainsKey(pType))
		{
			this.t.phenotypes_dict[pType] = new List<string>();
		}
		if (this.t.phenotypes_dict[pType].Contains(pID))
		{
			return;
		}
		this.t.phenotypes_dict[pType].Add(pID);
		this.t.phenotypes_list.Add(pID);
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00039DA8 File Offset: 0x00037FA8
	public void clear()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].units.Clear();
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00039DE1 File Offset: 0x00037FE1
	internal void addTrait(string pTrait)
	{
		this.t.addTrait(pTrait);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00039DEF File Offset: 0x00037FEF
	internal void addTraitIgnore(string pTrait)
	{
		this.t.addTraitIgnore(pTrait);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00039DFD File Offset: 0x00037FFD
	internal void removeTrait(string pTrait)
	{
		this.t.removeTrait(pTrait);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00039E0C File Offset: 0x0003800C
	public override void post_init()
	{
		this.loadAutoTextures(this.list);
		this.generateZombieAssets();
		this.loadShadows();
		base.post_init();
		this.generateFmodPaths();
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.action_dead_animation != null)
			{
				tAsset.special_dead_animation = true;
			}
			if (!string.IsNullOrEmpty(tAsset.base_asset_id))
			{
				ActorAsset tBaseAsset = this.get(tAsset.base_asset_id);
				tAsset.units = tBaseAsset.units;
			}
			if (tAsset.is_humanoid && !tAsset.unit_zombie)
			{
				this._humanoids_amount++;
			}
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00039ED0 File Offset: 0x000380D0
	private void linkSpells()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.spell_ids != null && tAsset.spell_ids.Count != 0)
			{
				tAsset.spells = new SpellHolder();
				tAsset.spells.mergeWith(tAsset.spell_ids);
			}
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00039F50 File Offset: 0x00038150
	public override void linkAssets()
	{
		base.linkAssets();
		this.setupBoolsAvatarPrefabs();
		this.setupBoolSpriteOverrides();
		this.linkArchitectures();
		this.linkSpells();
		this.fillOnlyBoatsList();
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.color_hex != null)
			{
				tAsset.color = new Color32?(Toolbox.makeColor(tAsset.color_hex));
			}
			if (tAsset.check_flip == null)
			{
				tAsset.check_flip = ((BaseSimObject _, WorldTile _) => true);
			}
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0003A010 File Offset: 0x00038210
	private void linkArchitectures()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (!string.IsNullOrEmpty(tAsset.architecture_id))
			{
				tAsset.architecture_asset = AssetManager.architecture_library.get(tAsset.architecture_id);
			}
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0003A080 File Offset: 0x00038280
	public override ActorAsset add(ActorAsset pAsset)
	{
		ActorAsset tNewAsset = base.add(pAsset);
		if (tNewAsset.base_stats == null)
		{
			tNewAsset.base_stats = new BaseStats();
		}
		return tNewAsset;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0003A0A9 File Offset: 0x000382A9
	private void fillOnlyBoatsList()
	{
		this.list_only_boat_assets = this.list.FindAll((ActorAsset pAsset) => pAsset.is_boat);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0003A0DC File Offset: 0x000382DC
	private void setupBoolSpriteOverrides()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.get_override_sprite != null)
			{
				tAsset.has_override_sprite = true;
			}
			if (tAsset.get_override_avatar_frames != null)
			{
				tAsset.has_override_avatar_frames = true;
			}
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0003A148 File Offset: 0x00038348
	private void setupBoolsAvatarPrefabs()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.avatar_prefab != string.Empty)
			{
				tAsset.has_avatar_prefab = true;
			}
		}
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0003A1B0 File Offset: 0x000383B0
	private void loadAutoTextures(IEnumerable<ActorAsset> pAssetsList)
	{
		foreach (ActorAsset tAsset in pAssetsList)
		{
			this.loadTexturesAndSprites(tAsset);
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x0003A1F8 File Offset: 0x000383F8
	private void generateFmodPaths()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (!tAsset.is_boat)
			{
				tAsset.generateFmodPaths(tAsset.id);
			}
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0003A258 File Offset: 0x00038458
	private void loadTexturesAndSprites(ActorAsset pAsset)
	{
		string tPath = "actors/species/";
		string tID = pAsset.id;
		if (pAsset.texture_id != string.Empty)
		{
			tID = pAsset.texture_id;
		}
		if (pAsset.default_animal)
		{
			tPath = tPath + "animals/" + tID + "/";
		}
		else if (pAsset.civ)
		{
			tPath = tPath + "civs/" + tID + "/";
		}
		else if (pAsset.unit_other)
		{
			tPath = tPath + "other/" + tID + "/";
		}
		ActorTextureSubAsset tSubTextureAsset = pAsset.texture_asset;
		if (tSubTextureAsset == null)
		{
			tSubTextureAsset = new ActorTextureSubAsset(tPath, pAsset.has_advanced_textures);
			pAsset.texture_asset = tSubTextureAsset;
			tSubTextureAsset.prevent_unconscious_rotation = pAsset.prevent_unconscious_rotation;
			tSubTextureAsset.render_heads_for_children = pAsset.render_heads_for_babies;
			if (pAsset.shadow)
			{
				tSubTextureAsset.shadow = true;
				tSubTextureAsset.shadow_texture = pAsset.shadow_texture;
				tSubTextureAsset.shadow_texture_egg = pAsset.shadow_texture_egg;
				tSubTextureAsset.shadow_texture_baby = pAsset.shadow_texture_baby;
			}
		}
		if (pAsset.can_turn_into_zombie)
		{
			pAsset.texture_path_zombie_for_auto_loader_main = tPath + "zombie";
			pAsset.texture_path_zombie_for_auto_loader_heads = tPath + "heads_zombie";
		}
		if (pAsset.has_baby_form)
		{
			bool tHasHeadSprite = base.hasSpriteInResources(tSubTextureAsset.texture_path_baby);
			if (!tHasHeadSprite)
			{
				Sprite[] spriteList = SpriteTextureLoader.getSpriteList(tSubTextureAsset.texture_path_baby, false);
				for (int i = 0; i < spriteList.Length; i++)
				{
					if (!(spriteList[i].name != "walk_0_head"))
					{
						tHasHeadSprite = true;
						break;
					}
				}
			}
			if (pAsset.render_heads_for_babies && !tHasHeadSprite)
			{
				Debug.LogError("ActorAssetLibrary: Actor Asset " + pAsset.id + " does not have head sprite for baby, but supposed to render them!");
				return;
			}
		}
		else
		{
			tSubTextureAsset.texture_path_baby = null;
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0003A3F0 File Offset: 0x000385F0
	private void generateZombieAssets()
	{
		using (ListPool<ActorAsset> tListBuffer = new ListPool<ActorAsset>(this.list))
		{
			using (ListPool<ActorAsset> tListWithNewZombieAssets = new ListPool<ActorAsset>(128))
			{
				this.createDefaultZombieAsset();
				foreach (ActorAsset ptr in tListBuffer)
				{
					ActorAsset tOriginalAsset = ptr;
					if (!tOriginalAsset.isTemplateAsset() && tOriginalAsset.zombie_auto_asset && tOriginalAsset.can_turn_into_zombie)
					{
						string tZombieID = tOriginalAsset.getZombieID();
						ActorAsset tGeneratedZombieAsset = this.clone(tZombieID, tOriginalAsset.id);
						tListWithNewZombieAssets.Add(tGeneratedZombieAsset);
						this.setDefaultZombieFields(this.t, tOriginalAsset, tOriginalAsset.default_animal);
						ActorTextureSubAsset tSubTexture = new ActorTextureSubAsset(tOriginalAsset.texture_path_zombie_for_auto_loader_main, this.t.has_advanced_textures);
						this.t.texture_asset = tSubTexture;
						ActorTextureSubAsset tOriginalSubTexture = tOriginalAsset.texture_asset;
						tSubTexture.shadow = tOriginalSubTexture.shadow;
						tSubTexture.shadow_texture = tOriginalSubTexture.shadow_texture;
						tSubTexture.shadow_texture_egg = tOriginalSubTexture.shadow_texture_egg;
						tSubTexture.shadow_texture_baby = tOriginalSubTexture.shadow_texture_baby;
						if (base.hasSpriteInResources(tOriginalAsset.texture_path_zombie_for_auto_loader_main))
						{
							tSubTexture.texture_path_main = tOriginalAsset.texture_path_zombie_for_auto_loader_main;
							tSubTexture.texture_heads = tOriginalAsset.texture_path_zombie_for_auto_loader_heads;
						}
						else
						{
							tSubTexture.texture_path_main = tOriginalAsset.texture_asset.texture_path_main;
							tSubTexture.texture_heads = tOriginalAsset.texture_asset.texture_heads;
							this.t.dynamic_sprite_zombie = true;
						}
						if (tOriginalAsset.animation_swim == null)
						{
							this.t.animation_swim = null;
						}
					}
				}
				this.loadAutoTextures(tListWithNewZombieAssets);
			}
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0003A5DC File Offset: 0x000387DC
	private void loadShadows()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.shadow)
			{
				tAsset.texture_asset.loadShadow();
			}
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0003A63C File Offset: 0x0003883C
	private void createDefaultZombieAsset()
	{
		ActorAsset tBaseZombie = this.clone("zombie", "human");
		tBaseZombie.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"advanced_brain",
			"phenotypes"
		});
		this.setDefaultZombieFields(tBaseZombie, this.get("human"), false);
		this.loadTexturesAndSprites(tBaseZombie);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0003A698 File Offset: 0x00038898
	private void setDefaultZombieFields(ActorAsset pAsset, ActorAsset pDefaultCreatureAsset, bool pAnimal = false)
	{
		pAsset.has_advanced_textures = false;
		pAsset.show_in_knowledge_window = false;
		pAsset.civ = false;
		pAsset.can_have_subspecies = true;
		pAsset.name_locale = "Zombie";
		pAsset.body_separate_part_hands = true;
		pAsset.icon = "iconZombie";
		pAsset.use_items = true;
		pAsset.can_edit_equipment = true;
		pAsset.banner_id = string.Empty;
		pAsset.color_hex = "#24803E";
		pAsset.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"decision"
		});
		pAsset.can_attack_buildings = false;
		pAsset.can_attack_brains = true;
		pAsset.disable_jump_animation = true;
		pAsset.animation_walk = pDefaultCreatureAsset.animation_walk;
		pAsset.animation_swim = pDefaultCreatureAsset.animation_swim;
		pAsset.only_melee_attack = true;
		pAsset.setZombie(pAnimal);
		pAsset.name_taxonomic_species = "zombus";
		pAsset.can_be_surprised = false;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0003A768 File Offset: 0x00038968
	public override void editorDiagnostic()
	{
		this.editorErrorChecks();
		this.editorNameSetChecks();
		this.phenotypeChecks();
		base.editorDiagnostic();
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0003A784 File Offset: 0x00038984
	private void phenotypeChecks()
	{
		using (ListPool<string> tPhenotypes = new ListPool<string>())
		{
			HashSet<string> tUnreachablePhenotypes = new HashSet<string>();
			foreach (ActorAsset tAsset in this.list)
			{
				if (tAsset.use_phenotypes && (tAsset.phenotypes_list == null || tAsset.phenotypes_list.Count == 0))
				{
					BaseAssetLibrary.logAssetError("<b>ActorAssetLibrary</b>: Unit is set to use phenotypes, but no phenotypes are used", tAsset.id);
				}
				if (tAsset.phenotypes_list != null)
				{
					List<string> trait_group_filter_subspecies = tAsset.trait_group_filter_subspecies;
					if (trait_group_filter_subspecies != null && trait_group_filter_subspecies.Contains("phenotypes"))
					{
						tUnreachablePhenotypes.UnionWith(tAsset.phenotypes_list);
					}
					else
					{
						tPhenotypes.AddRange(tAsset.phenotypes_list);
					}
				}
			}
			tUnreachablePhenotypes.RemoveAll(tPhenotypes);
			foreach (string ptr in tPhenotypes)
			{
				string tID = ptr;
				if (!AssetManager.phenotype_library.has(tID))
				{
					BaseAssetLibrary.logAssetError("<b>ActorAssetLibrary</b>: Phenotype <e>" + tID + "</e> not found", tID);
				}
			}
			foreach (PhenotypeAsset tPhenotype in AssetManager.phenotype_library.list)
			{
				if (!tPhenotypes.Contains(tPhenotype.id) && !tUnreachablePhenotypes.Contains(tPhenotype.id))
				{
					BaseAssetLibrary.logAssetError(string.Format("<b>ActorAssetLibrary</b>: Phenotype <e>{0}</e> not findable, because not used by any units", tPhenotype), tPhenotype.id);
				}
			}
			foreach (string tID2 in tUnreachablePhenotypes)
			{
				using (ListPool<string> tActors = new ListPool<string>())
				{
					foreach (ActorAsset tAsset2 in this.list)
					{
						if (tAsset2.phenotypes_list != null && tAsset2.phenotypes_list.Contains(tID2))
						{
							tActors.Add(tAsset2.id);
						}
					}
					BaseAssetLibrary.logAssetError(string.Concat(new string[]
					{
						"<b>ActorAssetLibrary</b>: Phenotype <e>",
						tID2,
						"</e> not reachable, because used by units with hidden phenotypes : <e>",
						string.Join(",", tActors),
						"</e>"
					}), tID2);
				}
			}
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0003AA8C File Offset: 0x00038C8C
	private void editorNameSetChecks()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.canBecomeSapient() && !tAsset.unit_zombie && tAsset.name_template_sets == null)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Name Templates Not Set!!", tAsset.id);
			}
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0003AB00 File Offset: 0x00038D00
	private void editorErrorChecks()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			if (tAsset.can_evolve_into_new_species && string.IsNullOrEmpty(tAsset.evolution_id))
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset missing evolution_id", tAsset.id);
			}
			if (tAsset.kingdom_id_wild != string.Empty && !AssetManager.kingdoms.has(tAsset.kingdom_id_wild))
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset has <e>invalid kingdom_id_wild</e> " + tAsset.kingdom_id_wild, tAsset.id);
			}
			if (tAsset.kingdom_id_civilization != string.Empty)
			{
				if (!AssetManager.kingdoms.has(tAsset.kingdom_id_civilization))
				{
					BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset has <e>invalid kingdom_id_civilization</e> " + tAsset.kingdom_id_civilization, tAsset.id);
				}
				else if (!AssetManager.kingdoms.get(tAsset.kingdom_id_civilization).civ)
				{
					BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset has <e>invalid kingdom_id_civilization</e> that is not a .civ " + tAsset.kingdom_id_civilization, tAsset.id);
				}
			}
			if (tAsset.architecture_id != string.Empty && !AssetManager.architecture_library.has(tAsset.architecture_id))
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset has <e>invalid architecture id</e> " + tAsset.architecture_id, tAsset.id);
			}
			if (!tAsset.zombie_auto_asset && !tAsset.unit_zombie && typeof(SA).GetField(tAsset.id, BindingFlags.Static | BindingFlags.Public) == null)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset does not have <e>SA property</e>", tAsset.id);
			}
			if (tAsset.use_phenotypes && (tAsset.phenotypes_dict == null || tAsset.phenotypes_dict.Count == 0))
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset does not have <e>colors set</e>", tAsset.id);
			}
			if (tAsset.can_have_subspecies && (tAsset.phenotypes_dict == null || tAsset.phenotypes_dict.Count == 0) && tAsset.use_phenotypes)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset can have subspecies, but no default color sets", tAsset.id);
			}
			if (tAsset.can_have_subspecies && tAsset.genome_parts.Count == 0)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset does not have <e>genes set</e>", tAsset.id);
			}
			if (string.IsNullOrEmpty(tAsset.icon))
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset does not have <e>icon set</e>", tAsset.id);
			}
			else if (tAsset.getSpriteIcon() == null)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: Actor Asset <e>sprite is missing</e> on path <e>" + tAsset.icon + "</e>", tAsset.id);
			}
			if (!string.IsNullOrEmpty(tAsset.banner_id) && SpriteTextureLoader.getSpriteList(KingdomBannerLibrary.getFullPathBackground(tAsset.banner_id), false).Length == 0)
			{
				BaseAssetLibrary.logAssetError("ActorAssetLibrary: there's <e>no folder for banners</e> for", tAsset.id + " with banner id " + tAsset.banner_id);
			}
		}
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0003ADC8 File Offset: 0x00038FC8
	public void preloadMainUnitSprites()
	{
		if (!Config.preload_units)
		{
			return;
		}
		foreach (ActorAsset tAsset in this.list)
		{
			if (!tAsset.has_override_sprite && tAsset.has_sprite_renderer)
			{
				tAsset.texture_asset.preloadSprites(tAsset.civ, tAsset.has_baby_form, tAsset);
				if (tAsset.shadow)
				{
					ActorTextureSubAsset tSubAsset = tAsset.texture_asset;
					if (tSubAsset.shadow_size.x < 1f || tSubAsset.shadow_size.y < 1f)
					{
						BaseAssetLibrary.logAssetError(string.Format("ActorAssetLibrary: Shadow size is too small : <e>{0}</e>", tSubAsset.shadow_size), tAsset.id);
					}
					if (tSubAsset.shadow_size_egg.x < 1f || tSubAsset.shadow_size_egg.y < 1f)
					{
						BaseAssetLibrary.logAssetError(string.Format("ActorAssetLibrary: Egg shadow size is too small : <e>{0}</e>", tSubAsset.shadow_size_egg), tAsset.id);
					}
					if (tSubAsset.shadow_size_baby.x < 1f || tSubAsset.shadow_size_baby.y < 1f)
					{
						BaseAssetLibrary.logAssetError(string.Format("ActorAssetLibrary: Baby shadow size is too small : <e>{0}</e>", tSubAsset.shadow_size_baby), tAsset.id);
					}
				}
			}
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0003AF3C File Offset: 0x0003913C
	public override void editorDiagnosticLocales()
	{
		foreach (ActorAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			if (tAsset.can_have_subspecies)
			{
				this.checkLocale(tAsset, tAsset.getCollectiveTermID());
			}
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0003AFAC File Offset: 0x000391AC
	private void initAnimalsNormal()
	{
		this.clone("fox", "$carnivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"fox_set"
		});
		this.t.kingdom_id_wild = "fox";
		this.t.kingdom_id_civilization = "miniciv_fox";
		this.t.base_stats["mass_2"] = 7f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.name_locale = "Fox";
		this.t.setSocialStructure("group_skulk", 12, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "canidae";
		this.t.name_taxonomic_genus = "vulpes";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S8_Fox;
		this.t.shadow_texture = "unitShadow_4";
		this.t.icon = "iconFox";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("bright_orange", "default_color");
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_fox";
		this.t.architecture_id = "civ_fox";
		this.t.banner_id = "civ_fox";
		this.t.clearTraits();
		this.addTrait("genius");
		this.addTrait("fast");
		this.t.addResource("leather", 1, false);
		this.clone("buffalo", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"buffalo_set"
		});
		this.t.kingdom_id_wild = "buffalo";
		this.t.kingdom_id_civilization = "miniciv_buffalo";
		this.t.base_stats["mass_2"] = 650f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 180f),
			new ValueTuple<string, float>("stamina", 200f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 11f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_graminivore");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("dense_dwellings");
		this.t.name_locale = "Buffalo";
		this.t.setSocialStructure("group_herd", 200, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "artiodactyla";
		this.t.name_taxonomic_family = "bovidae";
		this.t.name_taxonomic_genus = "syncerus";
		this.t.name_taxonomic_species = "caffer";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S16_Buffalo;
		this.t.shadow_texture = "unitShadow_7";
		this.t.icon = "iconBuffalo";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("soil", "default_color");
		this.t.color_hex = "#C2974E";
		this.t.max_random_amount = 3;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_buffalo";
		this.t.architecture_id = "civ_buffalo";
		this.t.banner_id = "civ_buffalo";
		this.t.clearTraits();
		this.addTrait("strong");
		this.addTrait("tough");
		this.t.addResource("leather", 2, false);
		this.clone("hyena", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"hyena_set"
		});
		this.t.kingdom_id_wild = "hyena";
		this.t.kingdom_id_civilization = "miniciv_hyena";
		this.t.base_stats["mass_2"] = 63f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 6f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addCultureTrait("city_layout_tile_wobbly_pattern");
		this.t.name_locale = "Hyena";
		this.t.setSocialStructure("group_cackle", 30, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "hyaenidae";
		this.t.name_taxonomic_genus = "crocuta";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S10_Dog;
		this.t.shadow_texture = "unitShadow_5";
		this.t.icon = "iconHyena";
		this.t.color_hex = "#C2974E";
		this.t.max_random_amount = 2;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_hyena";
		this.t.architecture_id = "civ_hyena";
		this.t.banner_id = "civ_hyena";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("savanna", "default_color");
		this.t.addResource("leather", 1, false);
		this.clone("crocodile", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crocodile_set"
		});
		this.t.kingdom_id_wild = "crocodile";
		this.t.kingdom_id_civilization = "miniciv_crocodile";
		this.t.base_stats["mass_2"] = 450f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 180f),
			new ValueTuple<string, float>("stamina", 40f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("lifespan", 70f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("armor", 15f),
			new ValueTuple<string, float>("offspring", 10f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("aquatic");
		this.t.name_locale = "Crocodile";
		this.t.setSocialStructure("group_bask", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "reptilia";
		this.t.name_taxonomic_order = "crocodilia";
		this.t.name_taxonomic_family = "crocodylidae";
		this.t.name_taxonomic_genus = "crocodylus";
		this.t.inspect_avatar_scale = 1.3f;
		this.t.base_stats["mass"] = 20f;
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.shadow_texture = "unitShadow_21";
		this.t.icon = "iconCrocodile";
		this.t.color_hex = "#C2974E";
		this.t.prevent_unconscious_rotation = true;
		this.t.immune_to_slowness = true;
		this.t.max_random_amount = 2;
		this.t.force_land_creature = true;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_crocodile";
		this.t.architecture_id = "civ_crocodile";
		this.t.banner_id = "civ_crocodile";
		this.addTrait("tough");
		this.t.clonePhenotype("$animal_skin$");
		this.addPhenotype("dark_green", "default_color");
		this.t.addResource("leather", 2, false);
		this.clone("monkey", "$herbivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"monkey_set"
		});
		this.t.kingdom_id_wild = "monkey";
		this.t.kingdom_id_civilization = "miniciv_monkey";
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 8f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("nimble");
		this.t.addSubspeciesTrait("shiny_love");
		this.t.name_locale = "Monkey";
		this.t.setSocialStructure("group_troop", 50, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "primates";
		this.t.name_taxonomic_family = "cercopithecidae";
		this.t.name_taxonomic_genus = "macaca";
		this.t.name_taxonomic_species = "mulatta";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S9_Monkey;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconMonkey";
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_monkey";
		this.t.architecture_id = "civ_monkey";
		this.t.banner_id = "civ_monkey";
		this.t.clearTraits();
		this.addTrait("genius");
		this.addTrait("agile");
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("dark_orange", "default_color");
		this.t.default_attack = "rocks";
		this.t.addResource("leather", 1, false);
		this.clone("rhino", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rhino_set"
		});
		this.t.kingdom_id_wild = "rhino";
		this.t.kingdom_id_civilization = "miniciv_rhino";
		this.t.base_stats["mass_2"] = 700f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("armor", 15f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("aggressive");
		this.t.name_locale = "Rhino";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "perissodactyla";
		this.t.name_taxonomic_family = "rhinocerotidae";
		this.t.name_taxonomic_genus = "rhinoceros";
		this.t.collective_term = "group_crash";
		this.t.base_stats["mass"] = 20f;
		this.t.base_stats["targets"] = 3f;
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S16_Buffalo;
		this.t.shadow_texture = "unitShadow_11";
		this.t.icon = "iconRhino";
		this.t.color_hex = "#C2974E";
		this.t.max_random_amount = 1;
		this.t.animal_breeding_close_units_limit = 4;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_rhino";
		this.t.architecture_id = "civ_rhino";
		this.t.banner_id = "civ_rhino";
		this.t.clearTraits();
		this.t.addTrait("strong");
		this.t.addTrait("fat");
		this.t.addTrait("dash");
		this.t.addTrait("hard_skin");
		this.addPhenotype("mid_gray", "default_color");
		this.addPhenotype("skin_medium", "default_color");
		this.t.addResource("meat", 2, false);
		this.t.addResource("leather", 4, false);
		this.clone("frog", "$peaceful_animal$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"frog_set"
		});
		this.t.kingdom_id_wild = "frog";
		this.t.kingdom_id_civilization = "miniciv_frog";
		this.t.base_stats["mass_2"] = 1.5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 20f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("birth_rate", 3f),
			new ValueTuple<string, float>("offspring", 30f)
		});
		this.t.name_locale = "Frog";
		this.t.setSocialStructure("group_army", 100, true, true, FamilyParentsMode.Alpha);
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_bubble");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_insectivore");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "amphibia";
		this.t.name_taxonomic_order = "anura";
		this.t.name_taxonomic_family = "ranidae";
		this.t.name_taxonomic_genus = "rana";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S2_Crab;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconFrog";
		this.t.color_hex = "#C2974E";
		this.t.immune_to_slowness = true;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_frog";
		this.t.architecture_id = "civ_frog";
		this.t.banner_id = "civ_frog";
		this.t.clearTraits();
		this.addTrait("poisonous");
		this.addTrait("weightless");
		this.addPhenotype("bright_green", "default_color");
		this.addPhenotype("infernal", "biome_jungle");
		this.addPhenotype("skin_medium", "biome_savanna");
		this.addPhenotype("pink_yellow_mushroom", "biome_mushroom");
		this.addPhenotype("aqua", "biome_corrupted");
		this.addPhenotype("aqua", "biome_swamp");
		this.addPhenotype("desert", "biome_desert");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("lemon", "biome_lemon");
		this.t.addResource("leather", 1, false);
		this.clone("snake", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"snake_set"
		});
		this.t.kingdom_id_wild = "snake";
		this.t.kingdom_id_civilization = "miniciv_snake";
		this.t.base_stats["mass_2"] = 15f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("birth_rate", 3f),
			new ValueTuple<string, float>("offspring", 20f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("circadian_drift");
		this.addTrait("poison_immune");
		this.addTrait("venomous");
		this.addTrait("weightless");
		this.t.name_locale = "Snake";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "reptilia";
		this.t.name_taxonomic_order = "squamata";
		this.t.name_taxonomic_family = "elapidae";
		this.t.name_taxonomic_genus = "naja";
		this.t.collective_term = "group_den";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S5_Snake;
		this.t.shadow_texture = "unitShadow_4";
		this.t.icon = "iconSnake";
		this.t.color_hex = "#C2974E";
		this.t.immune_to_slowness = true;
		this.t.can_attack_buildings = false;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_snake";
		this.t.architecture_id = "civ_snake";
		this.t.banner_id = "civ_snake";
		this.addPhenotype("dark_green", "default_color");
		this.addPhenotype("dark_orange", "default_color");
		this.addPhenotype("bright_green", "biome_jungle");
		this.addPhenotype("savanna", "biome_savanna");
		this.addPhenotype("aqua", "biome_swamp");
		this.addPhenotype("corrupted", "biome_corrupted");
		this.addPhenotype("desert", "biome_desert");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("lemon", "biome_lemon");
		this.t.default_attack = "bite";
		this.t.addResource("leather", 1, false);
		this.clone("dog", "$peaceful_animal$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"wolf_set"
		});
		this.t.base_stats["mass_2"] = 45f;
		this.t.kingdom_id_wild = "dog";
		this.t.kingdom_id_civilization = "miniciv_dog";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 20f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.name_locale = "Dog";
		this.t.setSocialStructure("group_pack", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "canidae";
		this.t.name_taxonomic_genus = "canis";
		this.t.name_taxonomic_species = "lupus";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S10_Dog;
		this.t.shadow_texture = "unitShadow_5";
		this.t.icon = "iconDog";
		this.t.color_hex = "#393939";
		this.t.default_attack = "jaws";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_dog";
		this.t.architecture_id = "civ_dog";
		this.t.banner_id = "civ_dog";
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("gray_black", "default_color");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("polar", "biome_permafrost");
		this.addPhenotype("skin_black", "biome_corrupted");
		this.addPhenotype("desert", "biome_desert");
		this.t.clearTraits();
		this.addTrait("fast");
		this.addTrait("dash");
		this.t.addResource("leather", 1, false);
		this.clone("wolf", "$carnivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"wolf_set"
		});
		this.t.base_stats["mass_2"] = 55f;
		this.t.kingdom_id_wild = "wolf";
		this.t.kingdom_id_civilization = "miniciv_wolf";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 120f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 20f),
			new ValueTuple<string, float>("damage", 22f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 3f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addClanTrait("combat_instincts");
		this.t.name_locale = "Wolfs";
		this.t.setSocialStructure("group_pack", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "canidae";
		this.t.name_taxonomic_genus = "canis";
		this.t.name_taxonomic_species = "lupus";
		this.t.can_attack_buildings = false;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_7";
		this.t.icon = "iconWolf";
		this.t.color_hex = "#393939";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_wolf";
		this.t.architecture_id = "civ_wolf";
		this.t.banner_id = "civ_wolf";
		this.addTrait("nightchild");
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("gray_black", "default_color");
		this.addPhenotype("dark_red", "biome_infernal");
		this.t.music_theme = "Units_Wolf";
		this.t.addResource("leather", 1, false);
		this.clone("bear", "$carnivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"bear_set"
		});
		this.t.base_stats["mass_2"] = 175f;
		this.t.kingdom_id_wild = "bear";
		this.t.kingdom_id_civilization = "miniciv_bear";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 200f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 35f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 8f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("big_stomach");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.t.addSubspeciesTrait("diet_frugivore");
		this.t.addSubspeciesTrait("winter_slumberers");
		this.t.addSubspeciesTrait("energy_preserver");
		this.t.addSubspeciesTrait("aggressive");
		this.t.name_locale = "Bear";
		this.t.setSocialStructure("group_sleuth", 4, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "ursidae";
		this.t.name_taxonomic_genus = "ursus";
		this.t.base_stats["mass"] = 2f;
		this.t.base_stats["targets"] = 2f;
		this.t.can_attack_buildings = false;
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconBear";
		this.t.color_hex = "#6C522D";
		this.addTrait("strong");
		this.t.default_attack = "claws";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_bear";
		this.t.architecture_id = "civ_bear";
		this.t.banner_id = "civ_bear";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("skin_dark", "default_color");
		this.t.music_theme = "Units_Bear";
		this.t.addResource("leather", 3, false);
		this.clone("piranha", "$animal$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"piranha_set"
		});
		this.t.kingdom_id_wild = "piranha";
		this.t.kingdom_id_civilization = "miniciv_piranha";
		this.t.base_stats["mass_2"] = 10f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 10f),
			new ValueTuple<string, float>("damage", 25f),
			new ValueTuple<string, float>("speed", 13f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("offspring", 20f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_roe");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.t.addSubspeciesTrait("diet_piscivore");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addSubspeciesTrait("aquatic");
		this.t.addSubspeciesTrait("fins");
		this.t.name_locale = "Piranha";
		this.t.setSocialStructure("group_shoal", 30, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "actinopterygii";
		this.t.name_taxonomic_order = "characiformes";
		this.t.name_taxonomic_family = "serrasalmidae";
		this.t.name_taxonomic_genus = "pygocentrus";
		this.t.name_taxonomic_species = "nattereri";
		this.t.can_attack_buildings = false;
		this.t.actor_size = ActorSize.S4_Piranha;
		this.t.icon = "iconPiranha";
		this.t.color_hex = "#3483B6";
		this.t.immune_to_slowness = true;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_piranha";
		this.t.architecture_id = "civ_piranha";
		this.t.banner_id = "civ_piranha";
		ActorAsset t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.checkPiranhaAchievement));
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("aqua", "default_color");
		this.addPhenotype("bright_salmon", "default_color");
		this.t.music_theme = "Units_Piranha";
		this.t.force_land_creature = false;
		this.t.addResource("sushi", 1, true);
		this.clone("rabbit", "$herbivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rabbit_set"
		});
		this.t.kingdom_id_wild = "rabbit";
		this.t.kingdom_id_civilization = "miniciv_rabbit";
		this.t.base_stats["mass_2"] = 4.5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 50f),
			new ValueTuple<string, float>("stamina", 140f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 15f),
			new ValueTuple<string, float>("damage", 5f),
			new ValueTuple<string, float>("armor", 1f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("offspring", 12f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_short");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addClanTrait("we_are_legion");
		this.t.name_locale = "Rabbit";
		this.t.setSocialStructure("group_colony", 30, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "lagomorpha";
		this.t.name_taxonomic_family = "leporidae";
		this.t.name_taxonomic_genus = "oryctolagus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S6_Chicken;
		this.t.shadow_texture = "unitShadow_2";
		this.t.icon = "iconRabbit";
		this.t.color_hex = "#D3D6D1";
		this.addTrait("weightless");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_rabbit";
		this.t.architecture_id = "civ_rabbit";
		this.t.banner_id = "civ_rabbit";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("wood", "default_color");
		this.addTrait("fast");
		this.t.music_theme = "Units_Rabbit";
		this.t.addResource("leather", 1, false);
		this.clone("cat", "$carnivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"cat_set"
		});
		this.t.kingdom_id_wild = "cat";
		this.t.kingdom_id_civilization = "miniciv_cat";
		this.t.base_stats["mass_2"] = 5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 85f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 45f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 3f),
			new ValueTuple<string, float>("offspring", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.name_locale = "Cat";
		this.t.setSocialStructure("group_clowder", 15, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "felidae";
		this.t.name_taxonomic_genus = "felis";
		this.t.name_taxonomic_species = "catus";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S7_Cat;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconCat";
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_cat";
		this.t.architecture_id = "civ_cat";
		this.t.banner_id = "civ_cat";
		this.addTrait("weightless");
		this.t.default_attack = "claws";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("bright_orange", "default_color");
		this.t.music_theme = "Units_Cat";
		this.t.addResource("leather", 1, false);
		this.clone("raccoon", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"raccoon_set"
		});
		this.t.kingdom_id_wild = "raccoon";
		this.t.kingdom_id_civilization = "miniciv_raccoon";
		this.t.base_stats["mass_2"] = 9f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 85f),
			new ValueTuple<string, float>("stamina", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 45f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 3f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.addSubspeciesTrait("nimble");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("shiny_love");
		this.t.addSubspeciesTrait("circadian_drift");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.addClanTrait("silver_tongues");
		this.t.name_locale = "Raccoon";
		this.t.setSocialStructure("group_gaze", 15, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "procyonidae";
		this.t.name_taxonomic_genus = "procyon";
		this.t.name_taxonomic_species = "lotor";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S7_Cat;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconRaccoon";
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "bandit";
		this.t.architecture_id = "civ_bandit";
		this.t.banner_id = "civ_bandit";
		this.t.default_attack = "claws";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("gray_black", "default_color");
		this.addPhenotype("black_blue", "default_color");
		this.t.music_theme = "Units_Cat";
		this.t.disable_jump_animation = true;
		this.t.addResource("leather", 1, false);
		this.clone("seal", "$carnivore$");
		this.t.needs_to_be_explored = false;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"seal_set"
		});
		this.t.kingdom_id_wild = "seal";
		this.t.kingdom_id_civilization = "miniciv_seal";
		this.t.base_stats["mass_2"] = 90f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 30f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 4f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("diet_piscivore");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("fins");
		this.t.addReligionTrait("cast_blood_rain");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addClanTrait("blood_of_sea");
		this.t.addTrait("agile");
		this.t.name_locale = "Seal";
		this.t.setSocialStructure("group_colony", 50, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "carnivora";
		this.t.name_taxonomic_family = "phocidae";
		this.t.name_taxonomic_genus = "phoca";
		this.t.name_taxonomic_species = "vitulina";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S13_Human;
		this.t.shadow_texture = "unitShadow_5";
		this.t.icon = "iconSeal";
		this.t.color_hex = "#C2974E";
		this.t.evolution_id = "civ_seal";
		this.t.can_evolve_into_new_species = true;
		this.t.architecture_id = "civ_piranha";
		this.t.banner_id = "civ_seal";
		this.t.default_attack = "bite";
		this.t.clonePhenotype("$animal_skin$");
		this.addPhenotype("black_blue", "default_color");
		this.addPhenotype("polar", "biome_permafrost");
		this.t.addResource("meat", 1, false);
		this.t.addResource("leather", 2, false);
		this.clone("ostrich", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ostrich_set"
		});
		this.t.kingdom_id_wild = "ostrich";
		this.t.kingdom_id_civilization = "miniciv_ostrich";
		this.t.base_stats["mass_2"] = 117f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 40f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 0f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addTrait("dash");
		this.t.addTrait("fast");
		this.t.name_locale = "Ostrich";
		this.t.setSocialStructure("group_flock", 10, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "aves";
		this.t.name_taxonomic_order = "struthioniformes";
		this.t.name_taxonomic_family = "struthionidae";
		this.t.name_taxonomic_genus = "struthio";
		this.t.name_taxonomic_species = "camelus";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S13_Human;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconOstrich";
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = false;
		this.t.architecture_id = "civ_piranha";
		this.t.banner_id = "civ_druid";
		this.t.default_attack = "bite";
		this.t.clonePhenotype("$animal_skin$");
		this.addPhenotype("black_blue", "default_color");
		this.addPhenotype("wood", "default_color");
		this.t.addResource("leather", 1, false);
		this.clone("unicorn", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"unicorn_set"
		});
		this.t.kingdom_id_wild = "unicorn";
		this.t.kingdom_id_civilization = "miniciv_unicorn";
		this.t.base_stats["mass_2"] = 500f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 500f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 0f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_rainbow");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addCultureTrait("city_layout_royal_checkers");
		this.t.addCultureTrait("fames_crown");
		this.t.addClanTrait("magic_blood");
		this.t.addClanTrait("witchs_vein");
		this.t.addClanTrait("warlocks_vein");
		this.t.addTrait("heart_of_wizard");
		this.t.addTrait("healing_aura");
		this.t.name_locale = "Unicorn";
		this.t.setSocialStructure("group_herd", 15, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "perissodactyla";
		this.t.name_taxonomic_family = "equidae";
		this.t.name_taxonomic_genus = "unicornis";
		this.t.name_taxonomic_species = "fabulosus";
		this.t.skip_fight_logic = false;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S14_Cow;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconUnicorn";
		this.t.color_hex = "#C2974E";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_unicorn";
		this.t.architecture_id = "civ_unicorn";
		this.t.banner_id = "civ_unicorn";
		this.t.default_attack = "bite";
		this.t.clonePhenotype("$animal_skin$");
		this.addPhenotype("skin_pale", "default_color");
		this.addPhenotype("polar", "default_color");
		this.addPhenotype("candy", "default_color");
		this.t.addTrait("blessed");
		this.t.addResource("gems", 1, false);
		this.t.addResource("leather", 2, false);
		this.clone("rat", "$omnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rat_set"
		});
		this.t.kingdom_id_wild = "rat";
		this.t.kingdom_id_civilization = "miniciv_rat";
		this.t.base_stats["mass_2"] = 0.5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 30f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 30f),
			new ValueTuple<string, float>("damage", 8f),
			new ValueTuple<string, float>("armor", 1f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("offspring", 15f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_short");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("nimble");
		this.t.addSubspeciesTrait("shiny_love");
		this.t.addClanTrait("we_are_legion");
		this.t.name_locale = "Rat";
		this.t.setSocialStructure("group_colony", 100, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "rodentia";
		this.t.name_taxonomic_family = "muridae";
		this.t.name_taxonomic_genus = "rattus";
		this.t.actor_size = ActorSize.S3_Rat;
		this.t.shadow_texture = "unitShadow_2";
		this.t.kingdom_id_wild = "rat";
		this.t.shadow = true;
		this.t.source_meat = true;
		this.t.max_random_amount = 5;
		this.t.can_attack_buildings = false;
		this.t.color_hex = "#2D2D2D";
		this.t.icon = "iconRat";
		this.addTrait("contagious");
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("gray_black", "default_color");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("wood", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_rat";
		this.t.architecture_id = "civ_rat";
		this.t.banner_id = "civ_rat";
		this.t.music_theme = "Units_Rat";
		this.t.disable_jump_animation = true;
		this.clone("chicken", "$herbivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"chicken_set"
		});
		this.t.kingdom_id_wild = "chicken";
		this.t.kingdom_id_civilization = "miniciv_chicken";
		this.t.base_stats["mass_2"] = 4f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 35f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("lifespan", 30f),
			new ValueTuple<string, float>("damage", 5f),
			new ValueTuple<string, float>("armor", 1f),
			new ValueTuple<string, float>("birth_rate", 3f),
			new ValueTuple<string, float>("offspring", 12f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addCultureTrait("dense_dwellings");
		this.addTrait("peaceful");
		this.addTrait("weightless");
		this.addTrait("content");
		this.t.name_locale = "Chicken";
		this.t.setSocialStructure("group_flock", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "aves";
		this.t.name_taxonomic_order = "galliformes";
		this.t.name_taxonomic_family = "phasianidae";
		this.t.name_taxonomic_genus = "gallus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S6_Chicken;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconChicken";
		this.t.color_hex = "#DEDAC4";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_chicken";
		this.t.architecture_id = "civ_chicken";
		this.t.banner_id = "civ_chicken";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("dark_orange", "default_color");
		this.t.music_theme = "Units_Chicken";
		this.clone("sheep", "$herbivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"sheep_set"
		});
		this.t.kingdom_id_wild = "sheep";
		this.t.kingdom_id_civilization = "miniciv_sheep";
		this.t.base_stats["mass_2"] = 65f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 90f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 6f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_graminivore");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.name_locale = "Sheep";
		this.t.setSocialStructure("group_flock", 100, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "artiodactyla";
		this.t.name_taxonomic_family = "bovidae";
		this.t.name_taxonomic_genus = "ovis";
		this.t.name_taxonomic_species = "aries";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S11_Sheep;
		this.t.icon = "iconSheep";
		this.t.color_hex = "#D7D7D7";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_sheep";
		this.t.architecture_id = "civ_sheep";
		this.t.banner_id = "civ_sheep";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("white_gray", "default_color");
		this.addPhenotype("skin_mixed", "default_color");
		this.addPhenotype("polar", "default_color");
		this.t.music_theme = "Units_Sheep";
		this.t.addResource("leather", 2, false);
		this.clone("cow", "$herbivore$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"cow_set"
		});
		this.t.kingdom_id_wild = "cow";
		this.t.kingdom_id_civilization = "miniciv_cow";
		this.t.base_stats["mass_2"] = 550f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 120f),
			new ValueTuple<string, float>("stamina", 20f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 6f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_graminivore");
		this.t.name_locale = "Cow";
		this.t.setSocialStructure("group_herd", 50, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "artiodactyla";
		this.t.name_taxonomic_family = "bovidae";
		this.t.name_taxonomic_genus = "bos";
		this.t.name_taxonomic_species = "taurus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S14_Cow;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconCow";
		this.t.color_hex = "#D7D7D7";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_cow";
		this.t.architecture_id = "civ_cow";
		this.t.banner_id = "civ_cow";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("white_gray", "default_color");
		this.t.addResource("leather", 3, false);
		this.clone("penguin", "$animal$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"penguin_set"
		});
		this.t.kingdom_id_wild = "penguin";
		this.t.kingdom_id_civilization = "miniciv_penguin";
		this.t.base_stats["mass_2"] = 35f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 70f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("lifespan", 20f),
			new ValueTuple<string, float>("damage", 7f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_spotted");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_piscivore");
		this.t.addSubspeciesTrait("adaptation_permafrost");
		this.t.addCultureTrait("matriarchy");
		this.t.name_locale = "Penguin";
		this.t.setSocialStructure("group_colony", 200, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "aves";
		this.t.name_taxonomic_order = "sphenisciformes";
		this.t.name_taxonomic_family = "spheniscidae";
		this.t.name_taxonomic_genus = "aptenodytes";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S10_Dog;
		this.t.shadow_texture = "unitShadow_3";
		this.t.icon = "iconPenguin";
		this.t.color_hex = "#D7D7D7";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_penguin";
		this.t.architecture_id = "civ_penguin";
		this.t.banner_id = "civ_penguin";
		this.addTrait("weightless");
		this.addPhenotype("black_blue", "default_color");
		this.clone("armadillo", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"armadillo_set"
		});
		this.t.kingdom_id_wild = "armadillo";
		this.t.kingdom_id_civilization = "miniciv_armadillo";
		this.t.base_stats["mass_2"] = 5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("armor", 20f),
			new ValueTuple<string, float>("speed", 8f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addTrait("hard_skin");
		this.t.addTrait("block");
		this.t.name_locale = "Armadillo";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "cingulata";
		this.t.name_taxonomic_family = "dasypodidae";
		this.t.name_taxonomic_genus = "dasypus";
		this.t.collective_term = "group_roll";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S6_Chicken;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconArmadillo";
		this.t.color_hex = "#D7D7D7";
		this.t.can_evolve_into_new_species = true;
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("dark_orange", "default_color");
		this.addPhenotype("wood", "default_color");
		this.t.evolution_id = "civ_armadillo";
		this.t.architecture_id = "civ_armadillo";
		this.t.banner_id = "civ_armadillo";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.clone("alpaca", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"alpaca_set"
		});
		this.t.kingdom_id_wild = "alpaca";
		this.t.kingdom_id_civilization = "miniciv_alpaca";
		this.t.base_stats["mass_2"] = 67f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 20f),
			new ValueTuple<string, float>("damage", 9f),
			new ValueTuple<string, float>("speed", 8f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("offspring", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addTrait("soft_skin");
		this.t.addReligionTrait("path_of_unity");
		this.t.name_locale = "Alpaca";
		this.t.setSocialStructure("group_herd", 50, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "artiodactyla";
		this.t.name_taxonomic_family = "camelidae";
		this.t.name_taxonomic_genus = "lama";
		this.t.name_taxonomic_species = "pacos";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconAlpaca";
		this.t.color_hex = "#D7D7D7";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("skin_dark", "default_color");
		this.addPhenotype("white_gray", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_alpaca";
		this.t.architecture_id = "civ_alpaca";
		this.t.banner_id = "civ_alpaca";
		this.t.addResource("leather", 2, false);
		this.clone("capybara", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"capybara_set"
		});
		this.t.kingdom_id_wild = "capybara";
		this.t.kingdom_id_civilization = "miniciv_capybara";
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 10f),
			new ValueTuple<string, float>("damage", 9f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addTrait("peaceful");
		this.t.addTrait("content");
		this.t.name_locale = "Capybara";
		this.t.setSocialStructure("group_herd", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "rodentia";
		this.t.name_taxonomic_family = "caviidae";
		this.t.name_taxonomic_genus = "hydrochoerus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconCapybara";
		this.t.color_hex = "#D7D7D7";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("wood", "default_color");
		this.addPhenotype("dark_orange", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_capybara";
		this.t.architecture_id = "civ_capybara";
		this.t.banner_id = "civ_capybara";
		this.t.addResource("leather", 1, false);
		this.clone("goat", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"goat_set"
		});
		this.t.kingdom_id_wild = "goat";
		this.t.kingdom_id_civilization = "miniciv_goat";
		this.t.base_stats["mass_2"] = 30f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 90f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 20f),
			new ValueTuple<string, float>("damage", 11f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 3f)
		});
		this.t.addTrait("dash");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.name_locale = "Goat";
		this.t.setSocialStructure("group_flock", 100, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "artiodactyla";
		this.t.name_taxonomic_family = "bovidae";
		this.t.name_taxonomic_genus = "capra";
		this.t.name_taxonomic_species = "hircus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconGoat";
		this.t.color_hex = "#D7D7D7";
		this.t.clonePhenotype("$animal_fur$");
		this.addPhenotype("gray_black", "default_color");
		this.addPhenotype("white_gray", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_goat";
		this.t.architecture_id = "civ_goat";
		this.t.banner_id = "civ_goat";
		this.t.addResource("leather", 1, false);
		this.clone("scorpion", "$carnivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"scorpion_set"
		});
		this.t.kingdom_id_wild = "scorpion";
		this.t.kingdom_id_civilization = "miniciv_scorpion";
		this.t.base_stats["mass_2"] = 0.25f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 10f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 25f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("adaptation_desert");
		this.t.addSubspeciesTrait("diet_insectivore");
		this.t.addCultureTrait("conscription_female_only");
		this.t.name_locale = "Scorpion";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "arachnida";
		this.t.name_taxonomic_order = "scorpiones";
		this.t.name_taxonomic_family = "scorpionidae";
		this.t.name_taxonomic_genus = "pandinus";
		this.t.collective_term = "group_bed";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconScorpion";
		this.t.color_hex = "#D7D7D7";
		this.t.disable_jump_animation = true;
		this.addPhenotype("dark_red", "default_color");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("bright_yellow", "biome_desert");
		this.addPhenotype("bright_purple", "biome_corrupted");
		this.addPhenotype("soil", "biome_savanna");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_scorpion";
		this.t.architecture_id = "civ_scorpion";
		this.t.banner_id = "civ_scorpion";
		this.clone("turtle", "$animal$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"turtle_set"
		});
		this.t.kingdom_id_wild = "turtle";
		this.t.kingdom_id_civilization = "miniciv_turtle";
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 150f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 400f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("armor", 25f),
			new ValueTuple<string, float>("birth_rate", 3f),
			new ValueTuple<string, float>("offspring", 20f)
		});
		this.t.name_locale = "Turtle";
		this.t.setSocialStructure("group_bale", 10, true, true, FamilyParentsMode.Alpha);
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_colored");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_algivore");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addCultureTrait("matriarchy");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "reptilia";
		this.t.name_taxonomic_order = "testudines";
		this.t.name_taxonomic_family = "emydidae";
		this.t.name_taxonomic_genus = "trachemys";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S7_Cat;
		this.t.shadow_texture = "unitShadow_7";
		this.t.icon = "iconTurtle";
		this.t.color_hex = "#D7D7D7";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_2;
		this.t.immune_to_slowness = true;
		this.t.flag_turtle = true;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_turtle";
		this.t.architecture_id = "civ_turtle";
		this.t.banner_id = "civ_turtle";
		this.t.prevent_unconscious_rotation = true;
		this.addPhenotype("dark_green", "default_color");
		this.addPhenotype("swamp", "default_color");
		this.addPhenotype("corrupted", "default_color");
		this.addPhenotype("desert", "default_color");
		this.addPhenotype("aqua", "default_color");
		this.addTrait("slow");
		this.addTrait("weightless");
		this.addTrait("genius");
		this.t.addResource("bones", 2, false);
		this.clone("crab", "$animal$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crab_set"
		});
		this.t.kingdom_id_wild = "crab";
		this.t.kingdom_id_civilization = "miniciv_crab";
		this.t.base_stats["mass_2"] = 2f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 60f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 10f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("armor", 15f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("birth_rate", 3f),
			new ValueTuple<string, float>("offspring", 30f)
		});
		this.t.name_locale = "Crab";
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("exoskeleton");
		this.t.addSubspeciesTrait("egg_roe");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_algivore");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addTrait("weightless");
		this.t.addTrait("hard_skin");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "malacostraca";
		this.t.name_taxonomic_order = "decapoda";
		this.t.name_taxonomic_family = "portunidae";
		this.t.name_taxonomic_genus = "carcinus";
		this.t.collective_term = "group_cast";
		this.t.disable_jump_animation = true;
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S2_Crab;
		this.t.shadow_texture = "unitShadow_2";
		this.t.icon = "iconCrab";
		this.t.color_hex = "#D7D7D7";
		this.addPhenotype("bright_salmon", "default_color");
		this.addPhenotype("swamp", "biome_swamp");
		this.addPhenotype("corrupted", "biome_corrupted");
		this.addPhenotype("desert", "biome_desert");
		this.addPhenotype("infernal", "biome_infernal");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_crab";
		this.t.architecture_id = "civ_crab";
		this.t.banner_id = "civ_crab";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x000412E0 File Offset: 0x0003F4E0
	private void initAnimalsWeird()
	{
		this.clone("crystal_sword", "$animal$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crystal_sword_set"
		});
		this.t.kingdom_id_wild = "crystal_sword";
		this.t.kingdom_id_civilization = "miniciv_crystal_sword";
		this.t.base_stats["mass_2"] = 10f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 1000f),
			new ValueTuple<string, float>("damage", 50f),
			new ValueTuple<string, float>("armor", 20f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.setSocialStructure("group_guild", 30, true, true, FamilyParentsMode.Alpha);
		this.t.addClanTrait("blood_of_eons");
		this.t.addClanTrait("combat_instincts");
		this.t.addCultureTrait("sword_lovers");
		this.t.addCultureTrait("city_layout_diamond");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_crystal");
		this.t.addSubspeciesTrait("reproduction_budding");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("death_grow_mythril");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_lithotroph");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addReligionTrait("rite_of_infinite_edges");
		this.t.addTrait("deflect_projectile");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "cnidaria";
		this.t.name_taxonomic_class = "anthozoa";
		this.t.name_taxonomic_order = "crystalliformes";
		this.t.name_taxonomic_family = "crystallidae";
		this.t.name_taxonomic_genus = "gladii";
		this.t.name_taxonomic_species = "volans";
		this.t.name_locale = "Crystal Sword";
		this.t.body_separate_part_hands = false;
		this.t.has_skin = false;
		this.t.mush_id = "mush_animal";
		this.t.icon = "iconCrystalSword";
		this.t.color_hex = "#75D0F4";
		this.t.disable_jump_animation = true;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.walk_0_3;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.sound_hit = "event:/SFX/HIT/HitStone";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_crystal_golem";
		this.t.architecture_id = "civ_crystal_golem";
		this.t.banner_id = "civ_crystal_golem";
		this.t.prevent_unconscious_rotation = true;
		this.addPhenotype("crystal", "default_color");
		this.addPhenotype("bright_purple", "default_color");
		this.addPhenotype("bright_pink", "default_color");
		this.addPhenotype("bright_teal", "default_color");
		this.addPhenotype("bright_yellow", "default_color");
		this.addPhenotype("bright_red", "default_color");
		this.addPhenotype("dark_violet", "biome_corrupted");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("bright_green", "biome_swamp");
		this.addTrait("shiny");
		this.addTrait("fire_proof");
		this.addTrait("freeze_proof");
		this.addTrait("light_lamp");
		this.t.addResource("gems", 1, true);
		this.t.addResource("crystal_salt", 1, false);
		this.clone("smore", "$animal$");
		this.t.needs_to_be_explored = true;
		this.t.render_heads_for_babies = false;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"smore_set"
		});
		this.t.kingdom_id_wild = "smore";
		this.t.kingdom_id_civilization = "miniciv_smore";
		this.t.base_stats["mass_2"] = 4f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 300f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("lifespan", 400f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_candy");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("diet_xylophagy");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.setSocialStructure("group_diabetes", 10, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "plantae";
		this.t.name_taxonomic_phylum = "tracheophyta";
		this.t.name_taxonomic_class = "magnoliopsida";
		this.t.name_taxonomic_order = "poales";
		this.t.name_taxonomic_family = "poaceae";
		this.t.name_taxonomic_genus = "saccharum";
		this.t.name_taxonomic_species = "smorex";
		this.t.name_locale = "Smore";
		this.t.body_separate_part_hands = false;
		this.t.has_skin = false;
		this.t.icon = "iconSmore";
		this.t.color_hex = "#F74AA6";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_candy_man";
		this.t.architecture_id = "civ_candy_man";
		this.t.banner_id = "civ_candy_man";
		this.addPhenotype("skin_medium", "default_color");
		this.addPhenotype("skin_dark", "default_color");
		this.addPhenotype("skin_mixed", "default_color");
		this.addPhenotype("swamp", "biome_swamp");
		this.addTrait("flesh_eater");
		this.addTrait("evil");
		this.addTrait("gluttonous");
		this.t.max_random_amount = 3;
		this.t.addResource("candy", 1, true);
		this.t.addResource("evil_beets", 1, false);
		this.clone("acid_blob", "$animal$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"acid_blob_set"
		});
		this.t.kingdom_id_wild = "acid_blob";
		this.t.kingdom_id_civilization = "miniciv_acid_blob";
		this.t.base_stats["mass_2"] = 66f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 120f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("speed", 4f),
			new ValueTuple<string, float>("lifespan", 50f),
			new ValueTuple<string, float>("damage", 35f),
			new ValueTuple<string, float>("offspring", 10f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_blob");
		this.t.addSubspeciesTrait("reproduction_fission");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("unstable_genome");
		this.t.addSubspeciesTrait("adaptation_wasteland");
		this.t.addReligionTrait("cosmic_radiation");
		this.t.addCultureTrait("happiness_from_war");
		this.t.name_locale = "Acid Blob";
		this.t.setSocialStructure("group_legion", 100, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "protista";
		this.t.name_taxonomic_phylum = "amoebozoa";
		this.t.name_taxonomic_class = "myxogastria";
		this.t.name_taxonomic_order = "liceales";
		this.t.name_taxonomic_family = "reticulariaceae";
		this.t.name_taxonomic_genus = "blobicus";
		this.t.name_taxonomic_species = "slimus";
		this.t.body_separate_part_hands = false;
		this.t.has_skin = false;
		this.t.mush_id = "mush_animal";
		this.t.icon = "iconAcidBlob";
		this.t.color_hex = "#008800";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.max_random_amount = 10;
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_acid_gentleman";
		this.t.architecture_id = "civ_acid_gentleman";
		this.t.banner_id = "civ_acid_gentleman";
		this.t.prevent_unconscious_rotation = true;
		this.addTrait("acid_blood");
		this.addTrait("acid_proof");
		this.addTrait("acid_touch");
		this.addTrait("lustful");
		this.addTrait("fat");
		this.addPhenotype("toxic_green", "default_color");
		this.addPhenotype("bright_pink", "biome_corrupted");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("swamp", "biome_swamp");
		this.t.addResource("jam", 1, true);
		this.clone("flower_bud", "$peaceful_animal$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"flower_set"
		});
		this.t.kingdom_id_wild = "flower_bud";
		this.t.kingdom_id_civilization = "miniciv_flower_bud";
		this.t.base_stats["mass_2"] = 5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 50f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("lifespan", 50f),
			new ValueTuple<string, float>("damage", 3f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addTrait("sunblessed");
		this.t.name_locale = "Flower Bud";
		this.t.setSocialStructure("group_platoon", 30, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "plantae";
		this.t.name_taxonomic_phylum = "tracheophyta";
		this.t.name_taxonomic_class = "liliopsida";
		this.t.name_taxonomic_order = "liliales";
		this.t.name_taxonomic_family = "liliaceae";
		this.t.name_taxonomic_genus = "ambulilium";
		this.t.name_taxonomic_species = "mobilens";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S12_Wolf;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconFlowerBud";
		this.t.color_hex = "#D7D7D7";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_liliar";
		this.t.architecture_id = "civ_liliar";
		this.t.banner_id = "civ_liliar";
		this.t.clonePhenotype("$animal_skin$");
		this.addPhenotype("bright_purple", "default_color");
		this.addPhenotype("bright_pink", "default_color");
		this.addPhenotype("bright_blue", "default_color");
		this.addPhenotype("bright_yellow", "default_color");
		this.addPhenotype("bright_red", "default_color");
		this.addPhenotype("jungle", "biome_jungle");
		this.addPhenotype("swamp", "biome_swamp");
		this.addPhenotype("desert", "biome_desert");
		this.addPhenotype("polar", "biome_permafrost");
		this.addPhenotype("bright_orange", "biome_maple");
		this.t.addResource("herbs", 1, true);
		this.clone("lemon_snail", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"lemon_man_set"
		});
		this.t.kingdom_id_wild = "lemon_snail";
		this.t.kingdom_id_civilization = "miniciv_lemon_snail";
		this.t.base_stats["mass_2"] = 5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 50f),
			new ValueTuple<string, float>("stamina", 10f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("lifespan", 55f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 4f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("offspring", 15f)
		});
		this.t.addSubspeciesTrait("reproduction_hermaphroditic");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_colored");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("slow_builders");
		this.t.addReligionTrait("cast_cure");
		this.t.addCultureTrait("city_layout_raindrops");
		this.t.addTrait("slow");
		this.t.addTrait("regeneration");
		this.t.name_locale = "Bitba";
		this.t.setSocialStructure("group_caravan", 10, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "plantae";
		this.t.name_taxonomic_phylum = "tracheophyta";
		this.t.name_taxonomic_class = "magnoliopsida";
		this.t.name_taxonomic_order = "sapindales";
		this.t.name_taxonomic_family = "rutaceae";
		this.t.name_taxonomic_genus = "citruslimax";
		this.t.name_taxonomic_species = "nicedrinkus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S6_Chicken;
		this.t.shadow_texture = "unitShadow_6";
		this.t.disable_jump_animation = true;
		this.t.icon = "iconLemonSnail";
		this.t.color_hex = "#D7D7D7";
		this.addPhenotype("lemon", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_lemon_man";
		this.t.architecture_id = "civ_lemon_man";
		this.t.banner_id = "civ_lemon_man";
		this.t.addResource("lemons", 1, true);
		this.clone("garl", "$herbivore$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"garlic_man_set"
		});
		this.t.kingdom_id_wild = "garl";
		this.t.kingdom_id_civilization = "miniciv_garl";
		this.t.base_stats["mass_2"] = 25f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 110f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("lifespan", 5f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("armor", 3f),
			new ValueTuple<string, float>("speed", 8f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addTrait("poison_immune");
		this.t.addTrait("regeneration");
		this.t.name_locale = "Garl";
		this.t.setSocialStructure("group_pack", 20, true, true, FamilyParentsMode.Alpha);
		this.t.name_taxonomic_kingdom = "plantae";
		this.t.name_taxonomic_phylum = "tracheophyta";
		this.t.name_taxonomic_class = "liliopsida";
		this.t.name_taxonomic_order = "asparagales";
		this.t.name_taxonomic_family = "amaryllidaceae";
		this.t.name_taxonomic_genus = "allium";
		this.t.name_taxonomic_species = "walkus";
		this.t.source_meat = true;
		this.t.actor_size = ActorSize.S6_Chicken;
		this.t.shadow_texture = "unitShadow_6";
		this.t.icon = "iconGarl";
		this.t.color_hex = "#D7D7D7";
		this.addPhenotype("mid_gray", "default_color");
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_garlic_man";
		this.t.architecture_id = "civ_garlic_man";
		this.t.banner_id = "civ_garlic_man";
		this.t.addResource("herbs", 1, true);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00042934 File Offset: 0x00040B34
	private void initInsects()
	{
		this.clone("bee", "$flying_insect$");
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.has_advanced_textures = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.architecture_id = "civ_bee";
		this.t.banner_id = "civ_bee";
		this.addPhenotype("bright_yellow", "default_color");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("city_layout_honeycomb");
		this.t.addCultureTrait("hive_society");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 20f)
		});
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "hymenoptera";
		this.t.name_taxonomic_family = "apidae";
		this.t.name_taxonomic_genus = "apis";
		this.t.name_taxonomic_species = "mellifera";
		this.t.collective_term = "group_colony";
		this.t.name_locale = "Bee";
		this.t.hovering_max = 1f;
		this.t.icon = "iconBee";
		this.t.color_hex = "#23F3FF";
		this.t.addDecision("bee_find_hive");
		this.t.addDecision("bee_create_hive");
		this.t.music_theme = "Units_BeeHive";
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("diet_nectarivore");
		this.t.addSubspeciesTrait("pollinating");
		this.t.addSubspeciesTrait("reproduction_parthenogenesis");
		this.clone("fly", "$flying_insect$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.kingdom_id_wild = "fly";
		this.t.architecture_id = "civ_druid";
		this.t.banner_id = "civ_druid";
		this.addPhenotype("black_blue", "default_color");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 15f)
		});
		this.t.icon = "iconFly";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "diptera";
		this.t.name_taxonomic_family = "muscidae";
		this.t.name_taxonomic_genus = "musca";
		this.t.name_taxonomic_species = "domestica";
		this.t.collective_term = "group_business";
		this.t.name_locale = "Fly";
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("diet_nectarivore");
		this.t.addSubspeciesTrait("egg_bubble");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.clone("butterfly", "$flying_insect$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.architecture_id = "civ_druid";
		this.t.banner_id = "civ_druid";
		this.addPhenotype("bright_yellow", "default_color");
		this.addPhenotype("bright_red", "default_color");
		this.addPhenotype("bright_violet", "default_color");
		this.addPhenotype("bright_pink", "default_color");
		this.addPhenotype("bright_teal", "default_color");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("lifespan", 3f),
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 10f)
		});
		this.t.icon = "iconButterfly";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "lepidoptera";
		this.t.name_taxonomic_family = "nymphalidae";
		this.t.name_taxonomic_genus = "danaus";
		this.t.name_taxonomic_species = "plexippus";
		this.t.collective_term = "group_kaleidoscope";
		this.t.name_locale = "Butterfly";
		this.t.icon = "iconButterfly";
		this.t.max_random_amount = 6;
		this.t.color_hex = "#23F3FF";
		ActorAsset t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.tryToCreatePlants));
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("diet_nectarivore");
		this.t.addSubspeciesTrait("pollinating");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("egg_cocoon");
		this.clone("grasshopper", "$insect$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.architecture_id = "civ_druid";
		this.t.banner_id = "civ_druid";
		this.addPhenotype("bright_green", "default_color");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 10f)
		});
		this.t.icon = "iconGrasshopper";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "orthoptera";
		this.t.name_taxonomic_family = "acrididae";
		this.t.name_taxonomic_genus = "omocestus";
		this.t.name_taxonomic_species = "viridulus";
		this.t.collective_term = "group_cloud";
		this.t.name_locale = "Grasshopper";
		this.t.shadow = false;
		this.clone("beetle", "$insect$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.architecture_id = "civ_beetle";
		this.t.banner_id = "civ_beetle";
		this.t.kingdom_id_wild = "insect";
		this.t.kingdom_id_civilization = "miniciv_insect";
		this.t.can_evolve_into_new_species = true;
		this.t.evolution_id = "civ_beetle";
		this.t.addSubspeciesTrait("diet_xylophagy");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("exoskeleton");
		this.t.addSubspeciesTrait("population_minimal");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_cocoon");
		this.addPhenotype("aqua", "default_color");
		this.addPhenotype("black_blue", "default_color");
		this.addPhenotype("swamp", "default_color");
		this.addPhenotype("soil", "default_color");
		this.t.addTrait("hard_skin");
		this.t.addTrait("slow");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("speed", 5f),
			new ValueTuple<string, float>("offspring", 10f)
		});
		this.t.icon = "iconBeetle";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "coleoptera";
		this.t.name_taxonomic_family = "scarabaeidae";
		this.t.name_taxonomic_genus = "sisyphus";
		this.t.name_taxonomic_species = "schaefferi";
		this.t.collective_term = "group_swarm";
		this.t.name_locale = "Beetle";
		this.t.disable_jump_animation = true;
		this.t.shadow = true;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0004347C File Offset: 0x0004167C
	private void initBoats()
	{
		ActorAsset actorAsset = new ActorAsset();
		actorAsset.id = "$boat$";
		actorAsset.can_be_killed_by_stuff = true;
		actorAsset.can_be_killed_by_life_eraser = true;
		actorAsset.can_attack_buildings = true;
		actorAsset.can_be_moved_by_powers = true;
		actorAsset.can_be_hurt_by_powers = true;
		actorAsset.update_z = true;
		actorAsset.effect_damage = true;
		actorAsset.force_ocean_creature = true;
		actorAsset.shadow = false;
		actorAsset.is_boat = true;
		actorAsset.kingdom_id_wild = "neutral_animals";
		actorAsset.can_be_inspected = true;
		actorAsset.inspect_children = false;
		actorAsset.inspect_sex = false;
		actorAsset.inspect_show_species = false;
		actorAsset.inspect_generation = false;
		actorAsset.inspect_home = true;
		actorAsset.immune_to_injuries = true;
		actorAsset.path_movement_timeout = 0.1f;
		actorAsset.split_ai_update = false;
		actorAsset.show_on_meta_layer = true;
		actorAsset.show_in_knowledge_window = false;
		actorAsset.show_in_taxonomy_tooltip = false;
		actorAsset.force_hide_stamina = true;
		actorAsset.force_hide_mana = true;
		actorAsset.can_talk_with = false;
		actorAsset.control_can_backstep = false;
		actorAsset.control_can_jump = false;
		actorAsset.control_can_kick = false;
		actorAsset.control_can_dash = false;
		actorAsset.control_can_talk = false;
		actorAsset.control_can_swear = false;
		actorAsset.control_can_steal = false;
		actorAsset.needs_to_be_explored = false;
		actorAsset.show_controllable_tip = false;
		ActorAsset pAsset = actorAsset;
		this.t = actorAsset;
		this.add(pAsset);
		this.t.inspect_genealogy = false;
		this.t.need_colored_sprite = true;
		this.t.allowed_status_tiers = StatusTier.Basic;
		this.t.render_status_effects = false;
		this.t.texture_atlas = UnitTextureAtlasID.Boats;
		this.t.name_locale = "Boats";
		this.t.inspect_avatar_scale = 1f;
		this.t.color_hex = "#000000";
		this.t.base_stats["scale"] = 0.25f;
		this.t.base_stats["mass"] = 1000f;
		this.t.base_stats["size"] = 1f;
		this.t.can_edit_traits = false;
		this.addTrait("boat");
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.t.can_be_surprised = false;
		this.t.icon = "iconBoat";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"decision"
		});
		this.t.sound_attack = null;
		this.t.sound_spawn = null;
		this.t.sound_idle = null;
		this.t.sound_death = null;
		this.t.addDecision("boat_check_existence");
		this.t.addDecision("boat_danger_check");
		this.t.addDecision("boat_idle");
		this.t.addDecision("boat_check_limits");
		this.t.prevent_unconscious_rotation = true;
		this.t.animation_speed_based_on_walk_speed = false;
		this.t.get_override_sprite = delegate(Actor pActor)
		{
			Boat tBoat = pActor.getSimpleComponent<Boat>();
			AnimationDataBoat tAnimationData = tBoat.getAnimationDataBoat();
			ActorAnimation tAnimation = tAnimationData.normal;
			if (!pActor.isAlive())
			{
				tAnimation = tAnimationData.broken;
			}
			else if (pActor.position_height != 0f || pActor.isInMagnet())
			{
				tAnimation = tAnimationData.normal;
			}
			else if (!tAnimationData.dict.TryGetValue(tBoat.last_movement_angle, out tAnimation))
			{
				int tBestKey = Toolbox.getClosestAngle(tBoat.last_movement_angle, tAnimationData);
				tAnimationData.dict.TryGetValue(tBestKey, out tAnimation);
			}
			if (tAnimation == null)
			{
				tAnimation = tAnimationData.normal;
			}
			Sprite tMainSprite = tAnimation.frames[0];
			if (tAnimation.frames.Length != 0)
			{
				tMainSprite = AnimationHelper.getSpriteFromList(0, tAnimation.frames, pActor.asset.animation_swim_speed);
			}
			return tMainSprite;
		};
		this.t.use_tool_items = false;
		this.clone("$boat_trading$", "$boat$");
		this.t.default_attack = "boat_cannonball";
		this.t.boat_type = "boat_type_trading";
		this.t.base_stats["health"] = 200f;
		this.t.base_stats["speed"] = 30f;
		this.t.base_stats["mass_2"] = 3000f;
		this.t.base_stats["attack_speed"] = 0.1f;
		this.t.draw_boat_mark = true;
		this.t.cost = new ConstructionCost(10, 0, 0, 10);
		this.t.actor_size = ActorSize.S16_Buffalo;
		this.addTrait("light_lamp");
		this.t.addDecision("boat_trading");
		this.clone("$boat_transport$", "$boat$");
		this.t.default_attack = "boat_cannonball";
		this.t.boat_type = "boat_type_transport";
		this.t.base_stats["health"] = 1000f;
		this.t.base_stats["speed"] = 25f;
		this.t.base_stats["mass_2"] = 2000f;
		this.t.base_stats["attack_speed"] = 0.5f;
		this.t.draw_boat_mark = true;
		this.t.draw_boat_mark_big = true;
		this.t.is_boat_transport = true;
		this.t.cost = new ConstructionCost(5, 0, 2, 20);
		this.t.actor_size = ActorSize.S17_Dragon;
		this.addTrait("light_lamp");
		this.t.addDecision("boat_transport_check");
		this.clone("boat_fishing", "$boat$");
		this.t.skip_fight_logic = true;
		this.t.boat_type = "boat_type_fishing";
		this.t.base_stats["speed"] = 10f;
		this.t.base_stats["health"] = 100f;
		this.t.base_stats["mass_2"] = 100f;
		this.t.cost = new ConstructionCost(5, 0, 0, 5);
		this.t.actor_size = ActorSize.S15_Bear;
		this.t.addDecision("boat_fishing");
		this.clone("boat_transport_human", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_orc", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_elf", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_dwarf", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_acid_gentleman", "$boat_transport$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_transport_alpaca", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_angle", "$boat_transport$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_transport_armadillo", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_bear", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_buffalo", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_candy_man", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_capybara", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_cat", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_chicken", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_cow", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_crab", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_crocodile", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_crystal_golem", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_dog", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_fox", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_frog", "$boat_transport$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_transport_garlic_man", "$boat_transport$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_transport_goat", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_hyena", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_lemon_man", "$boat_transport$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_transport_liliar", "$boat_transport$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_transport_monkey", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_penguin", "$boat_transport$");
		this.t.default_attack = "boat_snowball";
		this.clone("boat_transport_piranha", "$boat_transport$");
		this.t.default_attack = "boat_necro_ball";
		this.clone("boat_transport_rabbit", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_rat", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_rhino", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_scorpion", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_sheep", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_snake", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_turtle", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_wolf", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_white_mage", "$boat_transport$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_transport_snowman", "$boat_transport$");
		this.t.default_attack = "boat_snowball";
		this.clone("boat_transport_necromancer", "$boat_transport$");
		this.t.default_attack = "boat_necro_ball";
		this.clone("boat_transport_evil_mage", "$boat_transport$");
		this.t.default_attack = "boat_fireball";
		this.clone("boat_transport_druid", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_bee", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_beetle", "$boat_transport$");
		this.t.default_attack = "rocks";
		this.clone("boat_transport_seal", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_unicorn", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_ghost", "$boat_transport$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_transport_fairy", "$boat_transport$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_transport_demon", "$boat_transport$");
		this.t.default_attack = "boat_fireball";
		this.clone("boat_transport_cold_one", "$boat_transport$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_transport_bandit", "$boat_transport$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_transport_alien", "$boat_transport$");
		this.t.default_attack = "boat_plasma_ball";
		this.clone("boat_transport_greg", "$boat_transport$");
		this.t.default_attack = "boat_plasma_ball";
		this.clone("boat_trading_human", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_orc", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_elf", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_dwarf", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_acid_gentleman", "$boat_trading$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_trading_alpaca", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_angle", "$boat_trading$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_trading_armadillo", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_bear", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_buffalo", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_candy_man", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_capybara", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_cat", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_chicken", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_cow", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_crab", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_crocodile", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_crystal_golem", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_dog", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_fox", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_frog", "$boat_trading$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_trading_garlic_man", "$boat_trading$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_trading_goat", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_hyena", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_lemon_man", "$boat_trading$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_trading_liliar", "$boat_trading$");
		this.t.default_attack = "boat_acid_ball";
		this.clone("boat_trading_monkey", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_penguin", "$boat_trading$");
		this.t.default_attack = "boat_snowball";
		this.clone("boat_trading_piranha", "$boat_trading$");
		this.t.default_attack = "boat_necro_ball";
		this.clone("boat_trading_rabbit", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_rat", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_rhino", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_scorpion", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_sheep", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_snake", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_turtle", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_wolf", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_white_mage", "$boat_trading$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_trading_snowman", "$boat_trading$");
		this.t.default_attack = "boat_snowball";
		this.clone("boat_trading_necromancer", "$boat_trading$");
		this.t.default_attack = "boat_necro_ball";
		this.clone("boat_trading_evil_mage", "$boat_trading$");
		this.t.default_attack = "boat_fireball";
		this.clone("boat_trading_druid", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_bee", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_beetle", "$boat_trading$");
		this.t.default_attack = "rocks";
		this.clone("boat_trading_seal", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_unicorn", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_ghost", "$boat_trading$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_trading_fairy", "$boat_trading$");
		this.t.default_attack = "boat_arrow";
		this.clone("boat_trading_demon", "$boat_trading$");
		this.t.default_attack = "boat_fireball";
		this.clone("boat_trading_cold_one", "$boat_trading$");
		this.t.default_attack = "boat_freeze_ball";
		this.clone("boat_trading_bandit", "$boat_trading$");
		this.t.default_attack = "boat_cannonball";
		this.clone("boat_trading_alien", "$boat_trading$");
		this.t.default_attack = "boat_plasma_ball";
		this.clone("boat_trading_greg", "$boat_trading$");
		this.t.default_attack = "boat_plasma_ball";
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000447B4 File Offset: 0x000429B4
	private void initCivsClassic()
	{
		this.clone("human", "$civ_advanced_unit$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"human_default_set",
			"human_slavic_set",
			"human_germanic_set",
			"human_rus_set",
			"human_posh_set",
			"human_folk_set",
			"human_pomeranian_set",
			"human_frankish_set",
			"human_rome_set",
			"human_iberian_set",
			"human_monolux_set"
		});
		this.t.addPreferredColors(new string[]
		{
			"blue",
			"navy",
			"teal",
			"cyan"
		});
		this.t.build_order_template_id = "build_order_advanced";
		this.t.music_theme = "Humans_Neutral";
		this.t.kingdom_id_wild = "nomads_human";
		this.t.kingdom_id_civilization = "human";
		this.t.banner_id = "human";
		this.t.architecture_id = "human";
		this.t.name_locale = "Human";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "primates";
		this.t.name_taxonomic_family = "hominidae";
		this.t.name_taxonomic_genus = "homo";
		this.t.name_taxonomic_species = "sapiens";
		this.t.icon = "iconHumans";
		this.t.color_hex = "#005E72";
		this.t.zombie_color_hex = "#00AD2C";
		this.t.disable_jump_animation = true;
		this.t.base_stats["mass_2"] = 65f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("bonus_sex_random", 2f),
			new ValueTuple<string, float>("bad", 2f),
			new ValueTuple<string, float>("lifespan", 70f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("offspring", 5f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 3f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addSubspeciesTrait("nocturnal_dormancy");
		this.t.addClanTrait("divine_dozen");
		this.t.addCultureTrait("city_layout_the_grand_arrangement");
		this.t.addCultureTrait("city_layout_stone_garden");
		this.t.addCultureTrait("roads");
		this.t.addCultureTrait("statue_lovers");
		this.t.addCultureTrait("pep_talks");
		this.t.addCultureTrait("youth_reverence");
		this.t.addCultureTrait("expansionists");
		this.t.addLanguageTrait("nicely_structured_grammar");
		this.t.addReligionTrait("bloodline_bond");
		this.t.addReligionTrait("rite_of_roaring_skies");
		this.t.addReligionTrait("cast_shield");
		this.t.production = new string[]
		{
			"bread",
			"pie"
		};
		this.addPhenotype("skin_light", "default_color");
		this.addPhenotype("skin_dark", "default_color");
		this.addPhenotype("skin_mixed", "default_color");
		this.clone("elf", "$civ_advanced_unit$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"elf_default_set"
		});
		this.t.addPreferredColors(new string[]
		{
			"green",
			"lime",
			"lavender"
		});
		this.t.kingdom_id_wild = "nomads_elf";
		this.t.kingdom_id_civilization = "elf";
		this.t.banner_id = "elf";
		this.t.architecture_id = "elf";
		this.t.build_order_template_id = "build_order_advanced";
		this.t.music_theme = "Elves_Neutral";
		this.t.name_locale = "Elf";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "primates";
		this.t.name_taxonomic_family = "hominidae";
		this.t.name_taxonomic_genus = "elvus";
		this.t.name_taxonomic_species = "elegance";
		this.t.collective_term = "group_quiver";
		this.t.icon = "iconElves";
		this.t.color_hex = "#005D00";
		this.t.zombie_color_hex = "#2C8D98";
		this.t.civ_base_cities = 3;
		this.t.family_limit = 20;
		this.t.base_stats["mass_2"] = 25f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 70f),
			new ValueTuple<string, float>("bonus_sex_random", 1f),
			new ValueTuple<string, float>("stamina", 200f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 5f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 6f)
		});
		this.t.addCultureTrait("bow_lovers");
		this.t.addCultureTrait("spear_lovers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("youth_reverence");
		this.t.addCultureTrait("reading_lovers");
		this.t.addCultureTrait("attentive_readers");
		this.t.addCultureTrait("animal_whisperers");
		this.t.addCultureTrait("true_roots");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("unbroken_chain");
		this.t.addCultureTrait("city_layout_pillars");
		this.t.addClanTrait("blood_pact");
		this.t.addClanTrait("divine_dozen");
		this.t.addClanTrait("witchs_vein");
		this.t.addLanguageTrait("melodic");
		this.t.addLanguageTrait("magic_words");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("death_grow_tree");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_frugivore");
		this.t.addSubspeciesTrait("diet_granivore");
		this.t.addSubspeciesTrait("diet_florivore");
		this.t.addSubspeciesTrait("diet_folivore");
		this.t.addSubspeciesTrait("pure");
		this.t.addKingdomTrait("tax_rate_local_low");
		this.t.addKingdomTrait("tax_rate_tribute_low");
		this.t.addReligionTrait("rite_of_living_harvest");
		this.t.addReligionTrait("rite_of_entanglement");
		this.t.addReligionTrait("cast_grass_seeds");
		this.addTrait("weightless");
		this.addTrait("moonchild");
		this.addTrait("soft_skin");
		this.t.disable_jump_animation = true;
		this.t.production = new string[]
		{
			"bread",
			"jam",
			"sushi",
			"cider"
		};
		this.addPhenotype("skin_light", "default_color");
		this.addPhenotype("skin_mixed", "default_color");
		this.addPhenotype("mid_gray", "biome_corrupted");
		this.addPhenotype("skin_purple", "biome_celestial");
		this.t.addResource("meat", 1, true);
		this.t.addResource("bones", 1, false);
		this.clone("orc", "$civ_advanced_unit$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"orc_default_set"
		});
		this.t.addPreferredColors(new string[]
		{
			"red",
			"orange",
			"brown",
			"maroon",
			"black"
		});
		this.t.kingdom_id_wild = "nomads_orc";
		this.t.kingdom_id_civilization = "orc";
		this.t.banner_id = "orc";
		this.t.architecture_id = "orc";
		this.t.build_order_template_id = "build_order_advanced";
		this.t.music_theme = "Orcs_Neutral";
		this.t.family_limit = 50;
		this.t.name_locale = "Orc";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "primates";
		this.t.name_taxonomic_family = "hominidae";
		this.t.name_taxonomic_genus = "orcus";
		this.t.name_taxonomic_species = "bellicus";
		this.t.collective_term = "group_horde";
		this.t.base_stats["mass_2"] = 85f;
		this.t.icon = "iconOrcs";
		this.t.color_hex = "#2F5225";
		this.t.zombie_color_hex = "#7C5280";
		this.t.civ_base_cities = 4;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 150f),
			new ValueTuple<string, float>("bonus_sex_random", 1f),
			new ValueTuple<string, float>("stamina", 130f),
			new ValueTuple<string, float>("lifespan", 50f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 5f),
			new ValueTuple<string, float>("birth_rate", 5f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 2f)
		});
		this.addTrait("regeneration");
		this.addTrait("savage");
		this.addTrait("nightchild");
		this.addTrait("bloodlust");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("slow_builders");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addSubspeciesTrait("prolonged_rest");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addClanTrait("warlocks_vein");
		this.t.addClanTrait("combat_instincts");
		this.t.addCultureTrait("buildings_spread");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("fast_learners");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addCultureTrait("tiny_legends");
		this.t.addCultureTrait("warriors_ascension");
		this.t.addCultureTrait("shattered_crown");
		this.t.addLanguageTrait("scribble");
		this.t.addLanguageTrait("raging_paragraphs");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addLanguageTrait("foolish_glyphs");
		this.t.addReligionTrait("rite_of_falling_stars");
		this.t.addReligionTrait("zeal_of_conquest");
		this.t.addReligionTrait("cast_fire");
		this.t.production = new string[]
		{
			"bread",
			"burger",
			"tea"
		};
		this.t.disable_jump_animation = true;
		this.addPhenotype("skin_green", "default_color");
		this.addPhenotype("skin_pale", "biome_permafrost");
		this.addPhenotype("mid_gray", "biome_corrupted");
		this.addPhenotype("skin_red", "biome_infernal");
		this.t.addResource("meat", 1, true);
		this.t.addResource("bones", 2, false);
		this.clone("dwarf", "$civ_advanced_unit$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"dwarf_default_set",
			"dwarf_nordic_set"
		});
		this.t.addPreferredColors(new string[]
		{
			"yellow",
			"orange",
			"brown"
		});
		this.t.kingdom_id_wild = "nomads_dwarf";
		this.t.kingdom_id_civilization = "dwarf";
		this.t.banner_id = "dwarf";
		this.t.architecture_id = "dwarf";
		this.t.build_order_template_id = "build_order_advanced";
		this.t.music_theme = "Dwarves_Neutral";
		this.t.name_locale = "Dwarf";
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "chordata";
		this.t.name_taxonomic_class = "mammalia";
		this.t.name_taxonomic_order = "primates";
		this.t.name_taxonomic_family = "hominidae";
		this.t.name_taxonomic_genus = "dworfus";
		this.t.name_taxonomic_species = "fortis";
		this.t.collective_term = "group_beard";
		this.t.base_stats["mass_2"] = 75f;
		this.t.family_limit = 30;
		this.t.item_making_skill = 3;
		this.t.icon = "iconDwarf";
		this.t.color_hex = "#828282";
		this.t.zombie_color_hex = "#7C5280";
		this.t.civ_base_cities = 3;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 150f),
			new ValueTuple<string, float>("bonus_sex_random", 1f),
			new ValueTuple<string, float>("stamina", 40f),
			new ValueTuple<string, float>("lifespan", 220f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 3f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 2f)
		});
		this.addTrait("miner");
		this.addTrait("deflect_projectile");
		this.addTrait("block");
		this.addTrait("fat");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("fast_builders");
		this.t.addSubspeciesTrait("monophasic_sleep");
		this.t.addCultureTrait("roads");
		this.t.addCultureTrait("city_layout_the_grand_arrangement");
		this.t.addCultureTrait("hammer_lovers");
		this.t.addCultureTrait("tower_lovers");
		this.t.addCultureTrait("conscription_male_only");
		this.t.addCultureTrait("elder_reverence");
		this.t.addCultureTrait("gossip_lovers");
		this.t.addCultureTrait("weaponsmith_mastery");
		this.t.addCultureTrait("armorsmith_mastery");
		this.t.addLanguageTrait("powerful_words");
		this.t.addLanguageTrait("ancient_runes");
		this.t.addClanTrait("divine_dozen");
		this.t.addClanTrait("iron_will");
		this.t.addReligionTrait("rite_of_unbroken_shield");
		this.t.addReligionTrait("rite_of_shattered_earth");
		this.t.production = new string[]
		{
			"bread",
			"ale"
		};
		this.t.disable_jump_animation = true;
		this.addPhenotype("skin_light", "default_color");
		this.addPhenotype("skin_medium", "default_color");
		this.addPhenotype("mid_gray", "default_color");
		this.t.addResource("meat", 2, true);
		this.t.addResource("bones", 1, false);
		this.t.addResource("stone", 1, false);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00045D28 File Offset: 0x00043F28
	private void initCivsNew()
	{
		this.clone("civ_cat", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"cat_set"
		});
		this.t.base_stats["mass_2"] = 35f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 17f),
			new ValueTuple<string, float>("offspring", 5f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addSubspeciesTrait("inquisitive_nature");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("true_roots");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("sword_lovers");
		this.t.addClanTrait("silver_tongues");
		this.t.addClanTrait("best_five");
		this.t.addClanTrait("endurance_of_titans");
		this.t.addClanTrait("deathbound");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("cast_silence");
		this.t.addReligionTrait("rite_of_eternal_brew");
		this.t.addReligionTrait("summon_lightning");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.addLanguageTrait("melodic");
		this.addTrait("dodge");
		this.addTrait("battle_reflexes");
		this.t.kingdom_id_civilization = "civ_cat";
		this.t.architecture_id = "civ_cat";
		this.t.banner_id = "civ_cat";
		this.t.cloneTaxonomyFromForSapiens("cat");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("cat");
		this.t.color_hex = "#005E72";
		this.clone("civ_dog", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"wolf_set"
		});
		this.t.base_stats["mass_2"] = 70f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 120f),
			new ValueTuple<string, float>("stamina", 130f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("offspring", 5f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addCultureTrait("expansionists");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("patriarchy");
		this.t.addClanTrait("we_are_legion");
		this.t.addClanTrait("stonefists");
		this.t.addClanTrait("blood_of_giants");
		this.t.addClanTrait("gaia_shield");
		this.t.addClanTrait("blood_pact");
		this.t.addCultureTrait("join_or_die");
		this.t.addReligionTrait("cast_shield");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("hand_of_order");
		this.t.addReligionTrait("rite_of_roaring_skies");
		this.t.addLanguageTrait("melodic");
		this.t.addLanguageTrait("scribble");
		this.t.addTrait("dash");
		this.t.kingdom_id_civilization = "civ_dog";
		this.t.architecture_id = "civ_dog";
		this.t.banner_id = "civ_dog";
		this.t.cloneTaxonomyFromForSapiens("dog");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("dog");
		this.t.color_hex = "#005E72";
		this.clone("civ_chicken", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"chicken_set"
		});
		this.t.base_stats["mass_2"] = 35f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 60f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("offspring", 12f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("metamorphosis_chicken");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addSubspeciesTrait("monophasic_sleep");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addReligionTrait("rite_of_living_harvest");
		this.t.addReligionTrait("cast_silence");
		this.t.addReligionTrait("summon_lightning");
		this.t.addReligionTrait("rite_of_change");
		this.t.addReligionTrait("path_of_unity");
		this.t.kingdom_id_civilization = "civ_chicken";
		this.t.render_heads_for_babies = false;
		this.t.architecture_id = "civ_chicken";
		this.t.banner_id = "civ_chicken";
		this.t.cloneTaxonomyFromForSapiens("chicken");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("chicken");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 1, false);
		this.t.addResource("bones", 1, false);
		this.clone("civ_rabbit", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rabbit_set"
		});
		this.t.base_stats["mass_2"] = 30f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 60f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 12f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 1f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_short");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addClanTrait("we_are_legion");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addReligionTrait("cast_grass_seeds");
		this.t.addReligionTrait("rite_of_eternal_brew");
		this.t.addReligionTrait("summon_tornado");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("spawn_vegetation");
		this.t.kingdom_id_civilization = "civ_rabbit";
		this.t.architecture_id = "civ_rabbit";
		this.t.banner_id = "civ_rabbit";
		this.t.cloneTaxonomyFromForSapiens("rabbit");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("rabbit");
		this.t.color_hex = "#005E72";
		this.clone("civ_monkey", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"monkey_set"
		});
		this.t.base_stats["mass_2"] = 65f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 90f),
			new ValueTuple<string, float>("stamina", 140f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("offspring", 8f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 3f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("good_throwers");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addSubspeciesTrait("polyphasic_sleep");
		this.t.addSubspeciesTrait("shiny_love");
		this.t.addCultureTrait("patriarchy");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("fast_learners");
		this.t.addCultureTrait("buildings_spread");
		this.t.addCultureTrait("expertise_exchange");
		this.t.addCultureTrait("fames_crown");
		this.t.addClanTrait("we_are_legion");
		this.t.addClanTrait("silver_tongues");
		this.t.addClanTrait("bonebreakers");
		this.t.addClanTrait("combat_instincts");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.addReligionTrait("summon_tornado");
		this.t.addReligionTrait("minds_awakening");
		this.t.addReligionTrait("cast_curse");
		this.t.addLanguageTrait("scribble");
		this.t.kingdom_id_civilization = "civ_monkey";
		this.t.architecture_id = "civ_monkey";
		this.t.banner_id = "civ_monkey";
		this.t.cloneTaxonomyFromForSapiens("monkey");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("monkey");
		this.t.color_hex = "#005E72";
		this.clone("civ_fox", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"fox_set"
		});
		this.t.base_stats["mass_2"] = 60f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("lifespan", 120f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 4f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("monophasic_sleep");
		this.t.addSubspeciesTrait("nimble");
		this.t.addLanguageTrait("elegant_words");
		this.t.addLanguageTrait("stylish_writing");
		this.t.addReligionTrait("cast_curse");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.addReligionTrait("summon_lightning");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("path_of_unity");
		this.t.addCultureTrait("reading_lovers");
		this.t.addCultureTrait("fames_crown");
		this.t.kingdom_id_civilization = "civ_fox";
		this.t.architecture_id = "civ_fox";
		this.t.banner_id = "civ_fox";
		this.t.cloneTaxonomyFromForSapiens("fox");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("fox");
		this.t.color_hex = "#005E72";
		this.clone("civ_sheep", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"sheep_set"
		});
		this.t.base_stats["mass_2"] = 80f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 90f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 1f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("monophasic_sleep");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addLanguageTrait("nicely_structured_grammar");
		this.t.addLanguageTrait("melodic");
		this.t.addKingdomTrait("tax_rate_tribute_high");
		this.t.addKingdomTrait("tax_rate_local_high");
		this.t.addCultureTrait("golden_rule");
		this.t.addCultureTrait("city_layout_diamond");
		this.t.addReligionTrait("cast_grass_seeds");
		this.t.kingdom_id_civilization = "civ_sheep";
		this.t.architecture_id = "civ_sheep";
		this.t.banner_id = "civ_sheep";
		this.t.cloneTaxonomyFromForSapiens("sheep");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("sheep");
		this.t.color_hex = "#005E72";
		this.t.addTrait("greedy");
		this.t.addResource("meat", 2, true);
		this.t.addResource("leather", 1, false);
		this.t.addResource("bones", 1, false);
		this.clone("civ_cow", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"cow_set"
		});
		this.t.base_stats["mass_2"] = 175f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 120f),
			new ValueTuple<string, float>("stamina", 20f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 11f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 1f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addSubspeciesTrait("monophasic_sleep");
		this.t.addSubspeciesTrait("prolonged_rest");
		this.t.addLanguageTrait("melodic");
		this.t.addLanguageTrait("stylish_writing");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addTrait("tough");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_cow";
		this.t.architecture_id = "civ_cow";
		this.t.banner_id = "civ_cow";
		this.t.cloneTaxonomyFromForSapiens("cow");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("cow");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, false);
		this.clone("civ_armadillo", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"armadillo_set"
		});
		this.t.base_stats["mass_2"] = 45f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("armor", 20f),
			new ValueTuple<string, float>("speed", 17f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addCultureTrait("armorsmith_mastery");
		this.t.addLanguageTrait("powerful_words");
		this.t.kingdom_id_civilization = "civ_armadillo";
		this.t.architecture_id = "civ_armadillo";
		this.t.banner_id = "civ_armadillo";
		this.t.cloneTaxonomyFromForSapiens("armadillo");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("armadillo");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 1, false);
		this.t.addResource("bones", 2, false);
		this.clone("civ_wolf", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"wolf_set"
		});
		this.t.base_stats["mass_2"] = 85f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 140f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("offspring", 4f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_short");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("metamorphosis_wolf");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("diet_folivore");
		this.t.addCultureTrait("city_layout_claws");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addClanTrait("blood_pact");
		this.t.addClanTrait("iron_will");
		this.t.addClanTrait("combat_instincts");
		this.t.addLanguageTrait("powerful_words");
		this.t.addLanguageTrait("scribble");
		this.t.addReligionTrait("rite_of_restless_dead");
		this.t.kingdom_id_civilization = "civ_wolf";
		this.t.architecture_id = "civ_wolf";
		this.t.banner_id = "civ_wolf";
		this.t.cloneTaxonomyFromForSapiens("wolf");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("wolf");
		this.t.color_hex = "#005E72";
		this.t.addResource("bones", 1, false);
		this.t.addResource("leather", 1, false);
		this.clone("civ_bear", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"bear_set"
		});
		this.t.base_stats["mass_2"] = 140f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 180f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("big_stomach");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("winter_slumberers");
		this.t.addSubspeciesTrait("energy_preserver");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addCultureTrait("city_layout_claws");
		this.t.addCultureTrait("patriarchy");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addCultureTrait("conscription_male_only");
		this.t.addCultureTrait("axe_lovers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("training_potential");
		this.t.addLanguageTrait("foolish_glyphs");
		this.t.addClanTrait("blood_of_giants");
		this.t.addClanTrait("iron_will");
		this.t.addReligionTrait("rite_of_shattered_earth");
		this.t.kingdom_id_civilization = "civ_bear";
		this.t.render_heads_for_babies = false;
		this.t.architecture_id = "civ_bear";
		this.t.banner_id = "civ_bear";
		this.t.cloneTaxonomyFromForSapiens("bear");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("bear");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 1, false);
		this.clone("civ_rhino", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rhino_set"
		});
		this.t.base_stats["mass_2"] = 295f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 230f),
			new ValueTuple<string, float>("stamina", 110f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 35f),
			new ValueTuple<string, float>("armor", 15f),
			new ValueTuple<string, float>("speed", 16f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 2f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("bioproduct_stone");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addClanTrait("bonebreakers");
		this.t.addClanTrait("blood_of_giants");
		this.t.addClanTrait("iron_will");
		this.t.addClanTrait("void_ban");
		this.t.addCultureTrait("patriarchy");
		this.t.addCultureTrait("axe_lovers");
		this.t.addCultureTrait("city_layout_bricks");
		this.t.addLanguageTrait("raging_paragraphs");
		this.t.addLanguageTrait("powerful_words");
		this.t.addReligionTrait("zeal_of_conquest");
		this.t.addTrait("dash");
		this.t.addTrait("block");
		this.t.addTrait("hard_skin");
		this.t.addTrait("tough");
		this.t.addTrait("strong");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_rhino";
		this.t.architecture_id = "civ_rhino";
		this.t.banner_id = "civ_rhino";
		this.t.cloneTaxonomyFromForSapiens("rhino");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("rhino");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, false);
		this.clone("civ_buffalo", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"buffalo_set"
		});
		this.t.base_stats["mass_2"] = 290f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 160f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 16f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("city_layout_monolith_mesh");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addLanguageTrait("raging_paragraphs");
		this.t.addLanguageTrait("powerful_words");
		this.t.addClanTrait("stonefists");
		this.t.addReligionTrait("rite_of_roaring_skies");
		this.t.addTrait("tough");
		this.t.addTrait("dash");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_buffalo";
		this.t.architecture_id = "civ_buffalo";
		this.t.banner_id = "civ_buffalo";
		this.t.cloneTaxonomyFromForSapiens("buffalo");
		this.t.name_taxonomic_genus = "jecodespecus";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("buffalo");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, false);
		this.t.addResource("bones", 1, false);
		this.clone("civ_hyena", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"hyena_set"
		});
		this.t.base_stats["mass_2"] = 60f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 140f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("offspring", 6f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addClanTrait("combat_instincts");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addCultureTrait("axe_lovers");
		this.t.addCultureTrait("city_layout_tile_wobbly_pattern");
		this.t.addLanguageTrait("raging_paragraphs");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.kingdom_id_civilization = "civ_hyena";
		this.t.architecture_id = "civ_hyena";
		this.t.banner_id = "civ_hyena";
		this.t.cloneTaxonomyFromForSapiens("hyena");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("hyena");
		this.t.color_hex = "#005E72";
		this.clone("civ_rat", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"rat_set"
		});
		this.t.base_stats["mass_2"] = 20f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 4f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 16f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 15f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_short");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("nimble");
		this.t.addClanTrait("we_are_legion");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addCultureTrait("hive_society");
		this.t.addCultureTrait("expansionists");
		this.t.addLanguageTrait("scribble");
		this.t.kingdom_id_civilization = "civ_rat";
		this.t.render_heads_for_babies = false;
		this.t.architecture_id = "civ_rat";
		this.t.banner_id = "civ_rat";
		this.t.cloneTaxonomyFromForSapiens("rat");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("rat");
		this.t.color_hex = "#005E72";
		this.clone("civ_alpaca", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"alpaca_set"
		});
		this.t.base_stats["mass_2"] = 70f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 110f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("diplomatic_ascension");
		this.t.addLanguageTrait("elegant_words");
		this.t.addTrait("soft_skin");
		this.t.addReligionTrait("path_of_unity");
		this.t.kingdom_id_civilization = "civ_alpaca";
		this.t.architecture_id = "civ_alpaca";
		this.t.banner_id = "civ_alpaca";
		this.t.cloneTaxonomyFromForSapiens("alpaca");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("alpaca");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, false);
		this.t.addResource("leather", 1, false);
		this.clone("civ_capybara", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"capybara_set"
		});
		this.t.base_stats["mass_2"] = 70f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("offspring", 4f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("telepathic_link");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addSubspeciesTrait("slow_builders");
		this.t.addCultureTrait("serenity_now");
		this.t.addCultureTrait("xenophiles");
		this.t.addLanguageTrait("melodic");
		this.t.kingdom_id_civilization = "civ_capybara";
		this.t.architecture_id = "civ_capybara";
		this.t.banner_id = "civ_capybara";
		this.t.cloneTaxonomyFromForSapiens("capybara");
		this.t.name_taxonomic_genus = "mastefus";
		this.t.name_taxonomic_species = "yourmomus";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("capybara");
		this.t.color_hex = "#005E72";
		this.t.addTrait("peaceful");
		this.t.addTrait("content");
		this.t.addTrait("arcane_reflexes");
		this.clone("civ_goat", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"goat_set"
		});
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 110f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 16f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_herbivore");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("reading_lovers");
		this.t.addCultureTrait("expertise_exchange");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addLanguageTrait("enlightening_script");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_goat";
		this.t.architecture_id = "civ_goat";
		this.t.banner_id = "civ_goat";
		this.t.cloneTaxonomyFromForSapiens("goat");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("goat");
		this.t.color_hex = "#005E72";
		this.clone("civ_scorpion", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"scorpion_set"
		});
		this.t.base_stats["mass_2"] = 25f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("speed", 11f),
			new ValueTuple<string, float>("armor", 25f),
			new ValueTuple<string, float>("offspring", 25f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 5f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_very_long");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_desert");
		this.t.addClanTrait("combat_instincts");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("patriarchy");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addLanguageTrait("elegant_words");
		this.t.addReligionTrait("sands_of_ruin");
		this.t.addReligionTrait("cast_fire");
		this.addTrait("venomous");
		this.addTrait("poison_immune");
		this.t.kingdom_id_civilization = "civ_scorpion";
		this.t.architecture_id = "civ_scorpion";
		this.t.banner_id = "civ_scorpion";
		this.t.cloneTaxonomyFromForSapiens("scorpion");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("scorpion");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, true);
		this.t.addResource("bones", 1, false);
		this.clone("civ_crab", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crab_set"
		});
		this.t.base_stats["mass_2"] = 30f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 20f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("armor", 25f),
			new ValueTuple<string, float>("offspring", 30f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_roe");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("metamorphosis_crab");
		this.t.addSubspeciesTrait("exoskeleton");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_algivore");
		this.t.addSubspeciesTrait("fins");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addCultureTrait("city_layout_pebbles");
		this.t.addLanguageTrait("powerful_words");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addClanTrait("blood_of_sea");
		this.t.addTrait("hard_skin");
		this.t.kingdom_id_civilization = "civ_crab";
		this.t.architecture_id = "civ_crab";
		this.t.banner_id = "civ_crab";
		this.t.cloneTaxonomyFromForSapiens("crab");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("crab");
		this.t.color_hex = "#005E72";
		this.t.addResource("bones", 2, false);
		this.clone("civ_penguin", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"penguin_set"
		});
		this.t.base_stats["mass_2"] = 35f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 110f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 80f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_permafrost");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("xenophiles");
		this.t.addClanTrait("blood_of_sea");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addReligionTrait("rite_of_tempest_call");
		this.addTrait("freeze_proof");
		this.t.kingdom_id_civilization = "civ_penguin";
		this.t.architecture_id = "civ_penguin";
		this.t.banner_id = "civ_penguin";
		this.t.cloneTaxonomyFromForSapiens("penguin");
		this.t.name_taxonomic_genus = "hugovazus";
		this.t.name_taxonomic_species = "pingus";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("penguin");
		this.t.color_hex = "#005E72";
		this.clone("civ_turtle", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"turtle_set"
		});
		this.t.base_stats["mass_2"] = 110f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 180f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 8f),
			new ValueTuple<string, float>("armor", 25f),
			new ValueTuple<string, float>("offspring", 20f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_colored");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("cautious_instincts");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("sword_lovers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addLanguageTrait("eternal_text");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addClanTrait("blood_of_sea");
		this.t.kingdom_id_civilization = "civ_turtle";
		this.t.architecture_id = "civ_turtle";
		this.t.banner_id = "civ_turtle";
		this.t.cloneTaxonomyFromForSapiens("turtle");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("turtle");
		this.t.color_hex = "#005E72";
		this.t.addResource("bones", 1, false);
		this.clone("civ_crocodile", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crocodile_set"
		});
		this.t.base_stats["mass_2"] = 200f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 130f),
			new ValueTuple<string, float>("stamina", 60f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addClanTrait("masters_of_propaganda");
		this.t.addClanTrait("blood_of_sea");
		this.t.addClanTrait("combat_instincts");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("patriarchy");
		this.t.addCultureTrait("warriors_ascension");
		this.t.addCultureTrait("city_layout_parallels");
		this.t.addLanguageTrait("scribble");
		this.t.addReligionTrait("cast_silence");
		this.t.kingdom_id_civilization = "civ_crocodile";
		this.t.render_heads_for_babies = false;
		this.t.architecture_id = "civ_crocodile";
		this.t.banner_id = "civ_crocodile";
		this.t.cloneTaxonomyFromForSapiens("crocodile");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("crocodile");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 1, false);
		this.t.addResource("leather", 1, false);
		this.clone("civ_snake", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"snake_set"
		});
		this.t.base_stats["mass_2"] = 15f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 40f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_shell_plain");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("circadian_drift");
		this.t.addClanTrait("silver_tongues");
		this.t.addClanTrait("blood_of_sea");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addCultureTrait("spear_lovers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.addReligionTrait("cast_curse");
		this.t.addReligionTrait("cast_silence");
		this.addTrait("venomous");
		this.t.kingdom_id_civilization = "civ_snake";
		this.t.architecture_id = "civ_snake";
		this.t.banner_id = "civ_snake";
		this.t.cloneTaxonomyFromForSapiens("snake");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("snake");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 2, true);
		this.t.addResource("leather", 2, false);
		this.clone("civ_frog", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"frog_set"
		});
		this.t.base_stats["mass_2"] = 15f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 90f),
			new ValueTuple<string, float>("mutation", 3f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("offspring", 15f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_bubble");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("dreamweavers");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addLanguageTrait("melodic");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addReligionTrait("cast_blood_rain");
		this.t.addClanTrait("masters_of_propaganda");
		this.t.addClanTrait("blood_of_sea");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_frog";
		this.t.architecture_id = "civ_frog";
		this.t.banner_id = "civ_frog";
		this.t.cloneTaxonomyFromForSapiens("frog");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("frog");
		this.t.color_hex = "#005E72";
		this.t.addResource("meat", 1, true);
		this.t.addResource("leather", 1, false);
		this.clone("civ_piranha", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"piranha_set"
		});
		this.t.base_stats["mass_2"] = 19f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 30f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("speed", 7f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 20f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 1f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_roe");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addLanguageTrait("scribble");
		this.t.addClanTrait("we_are_legion");
		this.t.addClanTrait("combat_instincts");
		this.t.addClanTrait("blood_of_sea");
		this.t.addReligionTrait("cast_blood_rain");
		this.t.addTrait("battle_reflexes");
		this.t.kingdom_id_civilization = "civ_piranha";
		this.t.architecture_id = "civ_piranha";
		this.t.banner_id = "civ_piranha";
		this.t.cloneTaxonomyFromForSapiens("piranha");
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("piranha");
		this.t.color_hex = "#005E72";
		this.t.addResource("sushi", 2, true);
		this.clone("civ_liliar", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"flower_set"
		});
		this.t.base_stats["mass_2"] = 90f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 400f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 5f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 6f),
			new ValueTuple<string, float>("warfare", 1f),
			new ValueTuple<string, float>("stewardship", 6f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("gaia_roots");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addClanTrait("gaia_blood");
		this.t.addClanTrait("gaia_shield");
		this.t.addClanTrait("flesh_weavers");
		this.t.addLanguageTrait("melodic");
		this.t.addLanguageTrait("nicely_structured_grammar");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addTrait("regeneration");
		this.t.kingdom_id_civilization = "civ_liliar";
		this.t.architecture_id = "civ_liliar";
		this.t.banner_id = "civ_liliar";
		this.t.cloneTaxonomyFromForSapiens("flower_bud");
		this.t.name_taxonomic_genus = "luulia";
		this.t.name_taxonomic_species = "jubkoza";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("flower_bud");
		this.t.color_hex = "#005E72";
		this.t.addResource("herbs", 2, true);
		this.clone("civ_garlic_man", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"garlic_man_set"
		});
		this.t.base_stats["mass_2"] = 30f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 250f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 9f),
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addClanTrait("flesh_weavers");
		this.t.addCultureTrait("hive_society");
		this.t.addReligionTrait("cast_cure");
		this.t.addTrait("poison_immune");
		this.t.addTrait("regeneration");
		this.t.render_heads_for_babies = false;
		this.t.kingdom_id_civilization = "civ_garlic_man";
		this.t.architecture_id = "civ_garlic_man";
		this.t.banner_id = "civ_garlic_man";
		this.t.cloneTaxonomyFromForSapiens("garl");
		this.t.name_taxonomic_species = "lumneskatus";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("garl");
		this.t.color_hex = "#005E72";
		this.t.addResource("herbs", 2, true);
		this.clone("civ_lemon_man", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"lemon_man_set"
		});
		this.t.base_stats["mass_2"] = 45f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 4f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 14f),
			new ValueTuple<string, float>("offspring", 5f),
			new ValueTuple<string, float>("diplomacy", 6f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_hermaphroditic");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("population_large");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addCultureTrait("hive_society");
		this.t.addCultureTrait("expertise_exchange");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addLanguageTrait("doomed_glyphs");
		this.t.addReligionTrait("cast_cure");
		this.t.kingdom_id_civilization = "civ_lemon_man";
		this.t.architecture_id = "civ_lemon_man";
		this.t.banner_id = "civ_lemon_man";
		this.t.cloneTaxonomyFromForSapiens("lemon_snail");
		this.t.name_taxonomic_genus = "soursapiens";
		this.t.name_taxonomic_species = "misbehavius";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = "civ_lemon_man";
		this.t.clonePhenotype("lemon_snail");
		this.t.color_hex = "#005E72";
		this.addTrait("poison_immune");
		this.addTrait("paranoid");
		this.addTrait("attractive");
		this.addTrait("lucky");
		this.t.addTrait("regeneration");
		this.t.addResource("lemons", 3, true);
		this.clone("civ_acid_gentleman", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"acid_blob_set"
		});
		this.t.base_stats["mass_2"] = 99f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 80f),
			new ValueTuple<string, float>("stamina", 30f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 10f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 9f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_blob");
		this.t.addSubspeciesTrait("reproduction_fission");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("unstable_genome");
		this.t.addSubspeciesTrait("adaptation_wasteland");
		this.t.addCultureTrait("pep_talks");
		this.t.addCultureTrait("city_layout_madman_labyrinth");
		this.t.addLanguageTrait("elegant_words");
		this.t.kingdom_id_civilization = "civ_acid_gentleman";
		this.t.architecture_id = "civ_acid_gentleman";
		this.t.render_heads_for_babies = false;
		this.t.banner_id = "civ_acid_gentleman";
		this.t.cloneTaxonomyFromForSapiens("acid_blob");
		this.t.name_taxonomic_genus = "gentlemanus";
		this.t.name_taxonomic_species = "jumpus";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.clonePhenotype("acid_blob");
		this.t.color_hex = "#005E72";
		this.t.clonePhenotype("acid_blob");
		this.t.prevent_unconscious_rotation = true;
		this.addTrait("acid_blood");
		this.addTrait("acid_proof");
		this.addTrait("acid_touch");
		this.addTrait("poison_immune");
		this.addTrait("paranoid");
		this.addTrait("attractive");
		this.t.addResource("jam", 1, true);
		this.clone("civ_crystal_golem", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"crystal_golem_set"
		});
		this.t.base_stats["mass_2"] = 455f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 250f),
			new ValueTuple<string, float>("stamina", 60f),
			new ValueTuple<string, float>("lifespan", 1000f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 30f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("armor", 30f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_crystal");
		this.t.addSubspeciesTrait("reproduction_hermaphroditic");
		this.t.addSubspeciesTrait("gestation_extremely_long");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("metamorphosis_sword");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_lithotroph");
		this.t.addSubspeciesTrait("slow_builders");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addClanTrait("endurance_of_titans");
		this.t.addClanTrait("best_five");
		this.t.addClanTrait("blood_of_giants");
		this.t.addClanTrait("iron_will");
		this.t.addCultureTrait("fames_crown");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("sword_lovers");
		this.t.addCultureTrait("city_layout_titan_footprints");
		this.t.addLanguageTrait("words_of_madness");
		this.t.addClanTrait("blood_of_eons");
		this.t.name_locale = "Crystal Golem";
		this.t.cloneTaxonomyFromForSapiens("crystal_sword");
		this.t.name_taxonomic_genus = "bigus";
		this.t.name_taxonomic_species = "crystallus";
		this.t.kingdom_id_civilization = "civ_crystal_golem";
		this.t.architecture_id = "civ_crystal_golem";
		this.t.banner_id = "civ_crystal_golem";
		this.t.icon = "civs/" + this.t.id;
		this.t.has_skin = false;
		this.t.mush_id = "mush_unit";
		this.t.clonePhenotype("crystal_sword");
		this.t.color_hex = "#75D0F4";
		this.t.sound_hit = "event:/SFX/HIT/HitStone";
		this.addTrait("shiny");
		this.addTrait("strong_minded");
		this.t.addResource("gems", 1, true);
		this.t.addResource("stone", 1, false);
		this.t.addResource("crystal_salt", 1, false);
		this.clone("civ_candy_man", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"candy_man_set"
		});
		this.t.base_stats["mass_2"] = 70f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 85f),
			new ValueTuple<string, float>("stamina", 60f),
			new ValueTuple<string, float>("lifespan", 150f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("offspring", 4f),
			new ValueTuple<string, float>("diplomacy", 6f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 5f)
		});
		this.t.addSubspeciesTrait("reproduction_vegetative");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_candy");
		this.t.addSubspeciesTrait("annoying_fireworks");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("diet_cannibalism");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addCultureTrait("xenophobic");
		this.t.addLanguageTrait("words_of_madness");
		this.t.name_locale = "Candy Man";
		this.t.kingdom_id_civilization = "civ_candy_man";
		this.t.architecture_id = "civ_candy_man";
		this.t.banner_id = "civ_candy_man";
		this.t.cloneTaxonomyFromForSapiens("smore");
		this.t.name_taxonomic_genus = "zucker";
		this.t.name_taxonomic_species = "daddies";
		this.t.icon = "civs/" + this.t.id;
		this.t.name_locale = this.t.id;
		this.t.has_skin = false;
		this.t.mush_id = "mush_unit";
		this.t.clonePhenotype("smore");
		this.t.color_hex = "#75D0F4";
		this.t.sound_hit = "event:/SFX/HIT/HitStone";
		this.addTrait("flesh_eater");
		this.addTrait("evil");
		this.addTrait("gluttonous");
		this.addTrait("strong_minded");
		this.t.addResource("candy", 4, true);
		this.t.addResource("evil_beets", 1, false);
		this.clone("civ_beetle", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"insect_set"
		});
		this.t.architecture_id = "civ_beetle";
		this.t.banner_id = "civ_beetle";
		this.t.kingdom_id_civilization = "civ_beetle";
		this.t.default_attack = "rocks";
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_xylophagy");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("fast_builders");
		this.t.addSubspeciesTrait("high_fecundity");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("exoskeleton");
		this.t.addSubspeciesTrait("egg_cocoon");
		this.t.addClanTrait("bonebreakers");
		this.t.addCultureTrait("city_layout_bricks");
		this.t.addCultureTrait("dense_dwellings");
		this.t.clonePhenotype("beetle");
		this.t.addTrait("hard_skin");
		this.t.addTrait("slow");
		this.t.addTrait("strong");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 70f),
			new ValueTuple<string, float>("lifespan", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.icon = "civs/" + this.t.id;
		this.t.cloneTaxonomyFromForSapiens("beetle");
		this.t.name_taxonomic_genus = "hollonus";
		this.t.name_taxonomic_species = "silkus";
		this.t.name_locale = this.t.id;
		this.t.disable_jump_animation = true;
		this.t.render_heads_for_babies = false;
		this.t.shadow = true;
		this.t.base_stats["mass_2"] = 10f;
		this.t.addResource("fertilizer", 1, false);
		this.clone("civ_seal", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"seal_set"
		});
		this.t.architecture_id = "civ_seal";
		this.t.banner_id = "civ_seal";
		this.t.kingdom_id_civilization = "civ_seal";
		this.t.default_attack = "jaws";
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("adaptation_corruption");
		this.t.addSubspeciesTrait("adaptation_desert");
		this.t.addSubspeciesTrait("adaptation_infernal");
		this.t.addSubspeciesTrait("adaptation_permafrost");
		this.t.addSubspeciesTrait("adaptation_swamp");
		this.t.addSubspeciesTrait("adaptation_wasteland");
		this.t.addClanTrait("blood_of_sea");
		this.t.addClanTrait("combat_instincts");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addCultureTrait("city_layout_bricks");
		this.t.addCultureTrait("armorsmith_mastery");
		this.t.addCultureTrait("weaponsmith_mastery");
		this.t.addCultureTrait("craft_shotgun");
		this.t.clonePhenotype("seal");
		this.t.addTrait("agile");
		this.t.addTrait("strong");
		this.t.addTrait("fat");
		this.t.addTrait("backstep");
		this.t.addTrait("deflect_projectile");
		this.t.addTrait("dodge");
		this.t.addTrait("block");
		this.t.addTrait("dash");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 70f),
			new ValueTuple<string, float>("lifespan", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("offspring", 10f),
			new ValueTuple<string, float>("diplomacy", 1f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 4f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.icon = "civs/" + this.t.id;
		this.t.cloneTaxonomyFromForSapiens("seal");
		this.t.name_taxonomic_genus = "phocanavus";
		this.t.name_taxonomic_species = "militaris";
		this.t.name_locale = this.t.id;
		this.t.disable_jump_animation = true;
		this.t.render_heads_for_babies = true;
		this.t.shadow = true;
		this.clone("civ_unicorn", "$animal_civ$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"unicorn_set"
		});
		this.t.architecture_id = "civ_unicorn";
		this.t.banner_id = "civ_unicorn";
		this.t.kingdom_id_civilization = "civ_unicorn";
		this.t.default_attack = "hands";
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("egg_rainbow");
		this.t.addSubspeciesTrait("gestation_moderate");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addCultureTrait("city_layout_royal_checkers");
		this.t.addCultureTrait("fames_crown");
		this.t.addClanTrait("magic_blood");
		this.t.addClanTrait("witchs_vein");
		this.t.addClanTrait("warlocks_vein");
		this.t.addTrait("heart_of_wizard");
		this.t.addTrait("healing_aura");
		this.t.clonePhenotype("unicorn");
		this.t.base_stats["mass_2"] = 300f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 500f),
			new ValueTuple<string, float>("stamina", 120f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("damage", 20f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 0f),
			new ValueTuple<string, float>("offspring", 2f)
		});
		this.t.icon = "civs/" + this.t.id;
		this.t.cloneTaxonomyFromForSapiens("unicorn");
		this.t.name_taxonomic_genus = "pankus";
		this.t.name_taxonomic_species = "veryloudus";
		this.t.name_locale = this.t.id;
		this.t.disable_jump_animation = true;
		this.t.render_heads_for_babies = true;
		this.t.shadow = true;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0004CFFC File Offset: 0x0004B1FC
	private void initMobsOther()
	{
		this.clone("cold_one", "$mob$");
		this.t.render_heads_for_babies = true;
		this.t.is_humanoid = true;
		this.t.can_turn_into_ice_one = false;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"cold_one_set"
		});
		this.t.architecture_id = "civ_cold_one";
		this.t.kingdom_id_wild = "cold_one";
		this.t.kingdom_id_civilization = "miniciv_cold_one";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.setSocialStructure("group_blizzard", 40, true, true, FamilyParentsMode.None);
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("adaptation_permafrost");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_metamorph");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("egg_ice");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("dense_dwellings");
		this.t.addCultureTrait("true_roots");
		this.t.addCultureTrait("craft_ice_weapon");
		this.t.addCultureTrait("city_layout_silk_web");
		this.t.addLanguageTrait("chilly_font");
		this.t.addClanTrait("deathbound");
		this.t.addClanTrait("we_are_legion");
		this.t.addClanTrait("flesh_weavers");
		this.t.addReligionTrait("cast_silence");
		this.t.addReligionTrait("path_of_unity");
		this.t.addReligionTrait("minds_awakening");
		this.t.addReligionTrait("rite_of_shattered_earth");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "cnidaria";
		this.t.name_taxonomic_class = "anthozoa";
		this.t.name_taxonomic_order = "cryonata";
		this.t.name_taxonomic_family = "gelididae";
		this.t.name_taxonomic_genus = "colda";
		this.t.name_taxonomic_species = "asice";
		this.t.base_stats["mass_2"] = 85f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 250f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 1000f),
			new ValueTuple<string, float>("damage", 40f),
			new ValueTuple<string, float>("speed", 30f),
			new ValueTuple<string, float>("armor", 15f),
			new ValueTuple<string, float>("offspring", 4f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Cold One";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ice_hammer"
		});
		this.t.banner_id = "civ_cold_one";
		this.t.icon = "iconWalker";
		this.t.color_hex = "#90D2D4";
		this.t.skeleton_id = "skeleton";
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.t.addDecision("attack_golden_brain");
		this.addPhenotype("bright_blue", "default_color");
		this.addTrait("regeneration");
		this.addTrait("cold_aura");
		this.addTrait("weightless");
		this.addTrait("freeze_proof");
		this.t.music_theme = "Units_ColdOne";
		this.t.sound_hit = "event:/SFX/HIT/HitGeneric";
		this.t.addResource("bones", 1, true);
		this.t.addResource("snow_cucumbers", 2, false);
		this.clone("necromancer", "$mob$");
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"necromancer_set"
		});
		this.t.kingdom_id_wild = "necromancer";
		this.t.kingdom_id_civilization = "miniciv_necromancer";
		this.t.architecture_id = "civ_necromancer";
		this.t.banner_id = "civ_necromancer";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_spores");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("egg_face");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("gift_of_death");
		this.t.addSubspeciesTrait("gift_of_blood");
		this.t.addSubspeciesTrait("adaptation_corruption");
		this.t.addSubspeciesTrait("circadian_drift");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addLanguageTrait("spooky_language");
		this.t.addLanguageTrait("ancient_runes");
		this.t.addLanguageTrait("cursed_font");
		this.t.addLanguageTrait("mortal_tongue");
		this.t.addReligionTrait("shadowroot");
		this.t.addReligionTrait("cast_silence");
		this.t.addReligionTrait("rite_of_change");
		this.t.addReligionTrait("spawn_skeleton");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("true_roots");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addCultureTrait("craft_necro_staff");
		this.t.addClanTrait("deathbound");
		this.t.addClanTrait("flesh_weavers");
		this.t.addClanTrait("blood_of_eons");
		this.t.addClanTrait("witchs_vein");
		this.t.addClanTrait("iron_will");
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "basidiomycota";
		this.t.name_taxonomic_class = "pucciniomycetes";
		this.t.name_taxonomic_order = "pucciniales";
		this.t.name_taxonomic_family = "umbramagusaceae";
		this.t.name_taxonomic_genus = "necromagus";
		this.t.name_taxonomic_species = "boneys";
		this.t.collective_term = "group_mycelium";
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 300f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("lifespan", 550f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 6f),
			new ValueTuple<string, float>("warfare", 8f),
			new ValueTuple<string, float>("stewardship", 6f),
			new ValueTuple<string, float>("intelligence", 7f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Necromancer";
		this.t.body_separate_part_hands = true;
		this.t.icon = "iconNecromancer";
		this.t.color_hex = "#EE3A42";
		this.t.skeleton_id = "skeleton";
		this.t.effect_cast_top = "fx_cast_top_green";
		this.t.effect_cast_ground = "fx_cast_ground_green";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"necromancer_staff"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.t.addDecision("attack_golden_brain");
		this.addPhenotype("skin_mixed", "default_color");
		this.addTrait("regeneration");
		this.addTrait("evil");
		this.addTrait("fragile_health");
		this.t.addResource("mushrooms", 1, true);
		this.t.addResource("bones", 1, false);
		this.clone("druid", "$mob$");
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"druid_set"
		});
		this.t.architecture_id = "civ_druid";
		this.t.kingdom_id_wild = "druid";
		this.t.banner_id = "civ_druid";
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("egg_cocoon");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("bioproduct_mushrooms");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("gift_of_blood");
		this.t.addSubspeciesTrait("gift_of_harmony");
		this.t.addSubspeciesTrait("gift_of_life");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addCultureTrait("true_roots");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("animal_whisperers");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addCultureTrait("city_layout_pillars");
		this.t.addCultureTrait("craft_druid_staff");
		this.t.addCultureTrait("spear_lovers");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("ultimogeniture");
		this.t.addClanTrait("gaia_blood");
		this.t.addLanguageTrait("melodic");
		this.t.addReligionTrait("spawn_vegetation");
		this.t.addReligionTrait("rite_of_entanglement");
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "basidiomycota";
		this.t.name_taxonomic_class = "agaricomycetes";
		this.t.name_taxonomic_order = "agaricales";
		this.t.name_taxonomic_family = "luminomagusaceae";
		this.t.name_taxonomic_genus = "druidus";
		this.t.name_taxonomic_species = "greenus";
		this.t.collective_term = "group_mycelium";
		this.t.base_stats["mass_2"] = 80f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 200f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 12f),
			new ValueTuple<string, float>("speed", 12f),
			new ValueTuple<string, float>("armor", 4f),
			new ValueTuple<string, float>("offspring", 8f),
			new ValueTuple<string, float>("diplomacy", 7f),
			new ValueTuple<string, float>("warfare", 5f),
			new ValueTuple<string, float>("stewardship", 8f),
			new ValueTuple<string, float>("intelligence", 6f)
		});
		this.t.addSubspeciesTrait("death_grow_tree");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.unit_other = true;
		this.t.name_locale = "Druid";
		this.t.body_separate_part_hands = true;
		this.t.kingdom_id_wild = "druid";
		this.t.kingdom_id_civilization = "civ_druid";
		this.t.icon = "iconDruid";
		this.t.color_hex = "#4CDB75";
		this.t.skeleton_id = "skeleton";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"druid_staff"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.addPhenotype("skin_mixed", "default_color");
		this.addTrait("regeneration");
		this.addTrait("flower_prints");
		this.addTrait("healing_aura");
		this.t.addResource("mushrooms", 1, true);
		this.t.addResource("herbs", 2, false);
		this.t.addResource("bones", 1, false);
		this.clone("plague_doctor", "$mob$");
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"plague_doctor_set"
		});
		this.t.kingdom_id_wild = "plague_doctor";
		this.t.kingdom_id_civilization = "miniciv_plague_doctor";
		this.t.architecture_id = "civ_bandit";
		this.t.banner_id = "civ_bandit";
		this.t.build_order_template_id = "build_order_basic_2";
		this.addPhenotype("gray_black", "default_color");
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("population_moderate");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_spores");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("bioproduct_mushrooms");
		this.t.addSubspeciesTrait("egg_face");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("gift_of_harmony");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("conscription_female_only");
		this.t.addCultureTrait("animal_whisperers");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("craft_doctor_staff");
		this.t.addCultureTrait("city_layout_royal_checkers");
		this.t.addClanTrait("flesh_weavers");
		this.t.addClanTrait("gaia_blood");
		this.t.addClanTrait("best_five");
		this.t.addClanTrait("stonefists");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addLanguageTrait("magic_words");
		this.t.addLanguageTrait("cursed_font");
		this.t.addReligionTrait("cast_cure");
		this.t.addReligionTrait("rite_of_change");
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "ascomycota";
		this.t.name_taxonomic_class = "eurotiomycetes";
		this.t.name_taxonomic_order = "eurotiales";
		this.t.name_taxonomic_family = "aspergillaceae";
		this.t.name_taxonomic_genus = "antiplagus";
		this.t.name_taxonomic_species = "medicus";
		this.t.collective_term = "group_mycelium";
		this.t.base_stats["mass_2"] = 80f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 500f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 100f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("armor", 4f),
			new ValueTuple<string, float>("offspring", 6f),
			new ValueTuple<string, float>("diplomacy", 7f),
			new ValueTuple<string, float>("warfare", 5f),
			new ValueTuple<string, float>("stewardship", 8f),
			new ValueTuple<string, float>("intelligence", 6f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Plague Doctor";
		this.t.immune_to_tumor = true;
		this.t.body_separate_part_hands = true;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.addDecision("random_move_towards_civ_building");
		this.t.icon = "iconPlagueDoctor";
		this.t.color_hex = "#EE3A42";
		this.t.skeleton_id = "skeleton";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"plague_doctor_staff"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.addTrait("regeneration");
		this.addTrait("immune");
		this.addTrait("fire_proof");
		this.t.addResource("mushrooms", 1, true);
		this.clone("white_mage", "$mob$");
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"white_mage_set"
		});
		this.t.kingdom_id_wild = "white_mage";
		this.t.kingdom_id_civilization = "miniciv_white_mage";
		this.t.architecture_id = "civ_white_mage";
		this.t.banner_id = "civ_white_mage";
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_budding");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("egg_orb");
		this.t.addSubspeciesTrait("bioproduct_mushrooms");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("metamorphosis_butterfly");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("gift_of_void");
		this.t.addSubspeciesTrait("gift_of_blood");
		this.t.addSubspeciesTrait("gift_of_water");
		this.t.addClanTrait("witchs_vein");
		this.t.addLanguageTrait("ancient_runes");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addLanguageTrait("magic_words");
		this.t.addLanguageTrait("melodic");
		this.t.addCultureTrait("craft_white_staff");
		this.t.addCultureTrait("animal_whisperers");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("city_layout_rings");
		this.t.addCultureTrait("city_layout_the_grand_arrangement");
		this.t.addReligionTrait("cast_cure");
		this.t.addCultureTrait("ancestors_knowledge");
		this.addTrait("regeneration");
		this.addTrait("freeze_proof");
		this.addTrait("wise");
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "basidiomycota";
		this.t.name_taxonomic_class = "agaricomycetes";
		this.t.name_taxonomic_order = "agaricales";
		this.t.name_taxonomic_family = "luminomagusaceae";
		this.t.name_taxonomic_genus = "goodmagus";
		this.t.name_taxonomic_species = "staffus";
		this.t.collective_term = "group_mycelium";
		this.addPhenotype("skin_mixed", "default_color");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 300f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 5f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 3f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 8f),
			new ValueTuple<string, float>("warfare", 4f),
			new ValueTuple<string, float>("stewardship", 7f),
			new ValueTuple<string, float>("intelligence", 7f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "White Mage";
		this.t.body_separate_part_hands = true;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["mass_2"] = 75f;
		this.t.icon = "iconWhiteMage";
		this.t.color_hex = "#EE3A42";
		this.t.skeleton_id = "skeleton";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"white_staff"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.t.addResource("mushrooms", 2, true);
		this.clone("evil_mage", "$mob$");
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"evil_mage_set"
		});
		this.t.kingdom_id_wild = "evil_mage";
		this.t.kingdom_id_civilization = "miniciv_evil_mage";
		this.t.architecture_id = "civ_evil_mage";
		this.t.banner_id = "civ_evil_mage";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.addSubspeciesTrait("photosynthetic_skin");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_budding");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("egg_flames");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("bioproduct_mushrooms");
		this.t.addSubspeciesTrait("fire_elemental_form");
		this.t.addSubspeciesTrait("population_small");
		this.t.addSubspeciesTrait("spicy_kids");
		this.t.addSubspeciesTrait("gift_of_void");
		this.t.addSubspeciesTrait("gift_of_fire");
		this.t.addSubspeciesTrait("gift_of_thunder");
		this.t.addSubspeciesTrait("gift_of_air");
		this.t.addSubspeciesTrait("gift_of_blood");
		this.t.addSubspeciesTrait("adaptation_infernal");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addCultureTrait("craft_evil_staff");
		this.t.addCultureTrait("city_layout_architects_eye");
		this.t.addCultureTrait("city_layout_the_grand_arrangement");
		this.t.addClanTrait("deathbound");
		this.t.addClanTrait("warlocks_vein");
		this.t.addLanguageTrait("scorching_words");
		this.t.addLanguageTrait("ancient_runes");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addLanguageTrait("magic_words");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addReligionTrait("rite_of_infernal_wrath");
		this.t.name_taxonomic_kingdom = "fungi";
		this.t.name_taxonomic_phylum = "basidiomycota";
		this.t.name_taxonomic_class = "pucciniomycetes";
		this.t.name_taxonomic_order = "pucciniales";
		this.t.name_taxonomic_family = "umbramagusaceae";
		this.t.name_taxonomic_genus = "evilmagus";
		this.t.name_taxonomic_species = "burnus";
		this.t.collective_term = "group_mycelium";
		this.addPhenotype("gray_black", "default_color");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 500f),
			new ValueTuple<string, float>("stamina", 60f),
			new ValueTuple<string, float>("lifespan", 450f),
			new ValueTuple<string, float>("mutation", 1f),
			new ValueTuple<string, float>("damage", 1f),
			new ValueTuple<string, float>("armor", 4f),
			new ValueTuple<string, float>("speed", 20f),
			new ValueTuple<string, float>("offspring", 2f),
			new ValueTuple<string, float>("diplomacy", 5f),
			new ValueTuple<string, float>("warfare", 7f),
			new ValueTuple<string, float>("stewardship", 5f),
			new ValueTuple<string, float>("intelligence", 8f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Evil Mage";
		this.t.body_separate_part_hands = true;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["mass_2"] = 75f;
		this.t.icon = "iconEvilMage";
		this.t.color_hex = "#EE3A42";
		this.t.skeleton_id = "skeleton";
		this.t.effect_teleport = "fx_teleport_red";
		this.t.effect_cast_top = "fx_cast_top_red";
		this.t.effect_cast_ground = "fx_cast_ground_red";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"evil_staff"
		});
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.addTrait("evil");
		this.addTrait("fire_proof");
		this.addTrait("regeneration");
		this.addTrait("hotheaded");
		this.t.addResource("mushrooms", 2, true);
		this.t.addResource("bones", 1, false);
		this.clone("skeleton", "$mob$");
		this.t.species_spawn_radius = 60;
		this.t.is_humanoid = true;
		this.t.can_have_subspecies = true;
		this.t.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"phenotypes"
		});
		this.t.kingdom_id_civilization = "miniciv_jumpy_skull";
		this.t.architecture_id = "civ_necromancer";
		this.t.banner_id = "civ_necromancer";
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"skeleton_set"
		});
		this.t.use_phenotypes = false;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.unit_other = true;
		this.t.name_locale = "Skeleton";
		this.t.collective_term = "group_stack";
		this.t.body_separate_part_hands = true;
		this.t.has_skin = false;
		this.t.kingdom_id_wild = "undead";
		this.t.can_be_killed_by_divine_light = true;
		this.t.icon = "iconSkeleton";
		this.t.color_hex = "#ffffff";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"skeleton_job"
		});
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "ossiphora";
		this.t.name_taxonomic_class = "calciata";
		this.t.name_taxonomic_order = "rattlers";
		this.t.name_taxonomic_family = "osteus";
		this.t.name_taxonomic_genus = "bonelords";
		this.t.name_taxonomic_species = "calcius";
		this.t.addSubspeciesNamePrefix("calcius");
		this.t.addSubspeciesNamePrefix("bonelords");
		this.t.addSubspeciesNamePrefix("boney");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 100f)
		});
		this.t.base_stats["mass_2"] = 15f;
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"bow_bronze",
			"bow_steel",
			"bow_iron",
			"sword_steel",
			"spear_steel",
			"sword_iron"
		});
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.addDecision("attack_golden_brain");
		this.t.die_from_dispel = true;
		this.addTrait("weightless");
		this.addTrait("backstep");
		this.addTrait("dodge");
		this.addTrait("dash");
		this.addTrait("block");
		this.t.music_theme = "Units_Skeleton";
		this.t.sound_hit = "event:/SFX/HIT/HitBone";
		this.t.can_be_surprised = false;
		this.t.addResource("bones", 2, true);
		this.clone("jumpy_skull", "$mob$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"jumpy_skull_set"
		});
		this.t.kingdom_id_wild = "jumpy_skull";
		this.t.kingdom_id_civilization = "miniciv_jumpy_skull";
		this.t.architecture_id = "civ_necromancer";
		this.t.banner_id = "civ_necromancer";
		this.t.addSubspeciesTrait("reproduction_soulborne");
		this.t.addSubspeciesTrait("aggressive");
		this.t.addSubspeciesTrait("population_small");
		this.addPhenotype("white_gray", "default_color");
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "infernalia";
		this.t.name_taxonomic_class = "daemonica";
		this.t.name_taxonomic_order = "skullus";
		this.t.name_taxonomic_family = "hoppidae";
		this.t.name_taxonomic_genus = "chere";
		this.t.name_taxonomic_species = "pushka";
		this.t.collective_term = "group_stack";
		this.t.base_stats["mass_2"] = 3.5f;
		this.t.addDecision("check_swearing");
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 1f),
			new ValueTuple<string, float>("lifespan", 500f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 5f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Rude Skull";
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_ice_one = false;
		this.t.body_separate_part_hands = false;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.has_skin = false;
		this.t.can_be_killed_by_divine_light = true;
		this.t.die_from_dispel = true;
		this.t.icon = "iconJumpySkull";
		this.t.color_hex = "#ffffff";
		this.t.disable_jump_animation = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.max_random_amount = 4;
		this.addTrait("weightless");
		this.addTrait("block");
		this.addTrait("paranoid");
		this.addTrait("hotheaded");
		this.t.actor_size = ActorSize.S7_Cat;
		this.t.music_theme = "Units_Skeleton";
		this.t.sound_hit = "event:/SFX/HIT/HitBone";
		this.t.addResource("bones", 2, true);
		this.clone("fire_elemental", "$mob$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"fire_elemental_set"
		});
		this.t.use_phenotypes = false;
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "infernalia";
		this.t.name_taxonomic_class = "daemonica";
		this.t.name_taxonomic_order = "elementales";
		this.t.name_taxonomic_family = "ignisidae";
		this.t.name_taxonomic_genus = "ignis";
		this.t.name_taxonomic_species = "blazarus";
		this.t.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"advanced_brain",
			"phenotypes"
		});
		this.t.collective_term = "group_blaze";
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTrait("fire_elemental_form");
		this.t.addSubspeciesTrait("fenix_born");
		this.t.addSubspeciesTrait("egg_flames");
		this.t.addSubspeciesTrait("gestation_long");
		this.t.addSubspeciesTrait("rapid_aging");
		this.t.addSubspeciesTrait("reproduction_soulborne");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addCultureTrait("matriarchy");
		this.t.addLanguageTrait("scorching_words");
		this.t.base_stats["mass_2"] = 50f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 5f),
			new ValueTuple<string, float>("lifespan", 450f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 10f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Fire Elemental";
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_ice_one = false;
		this.t.can_turn_into_tumor = false;
		this.t.body_separate_part_hands = false;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.use_items = false;
		this.t.take_items = false;
		this.t.can_edit_equipment = false;
		this.t.has_skin = false;
		this.t.has_soul = false;
		this.t.die_from_dispel = true;
		this.t.kingdom_id_wild = "fire_elemental";
		this.t.architecture_id = "civ_demon";
		this.t.banner_id = "civ_demon";
		this.t.die_in_lava = false;
		this.t.default_attack = "fire_hands";
		this.t.allowed_status_tiers = StatusTier.Basic;
		this.t.icon = "iconFireElemental";
		this.t.color_hex = "#ff0000";
		this.t.disable_jump_animation = true;
		this.t.animation_idle = ActorAnimationSequences.idle_0_3;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.walk_0_3;
		this.t.shadow = false;
		this.t.max_random_amount = 4;
		this.addTrait("weightless");
		this.addTrait("light_lamp");
		this.addTrait("fire_proof");
		this.addTrait("fire_blood");
		this.addTrait("burning_feet");
		this.t.music_theme = "Units_Skeleton";
		this.t.sound_hit = null;
		this.t.sound_spawn = null;
		this.t.sound_attack = null;
		this.t.sound_idle = null;
		this.t.sound_death = null;
		this.t.generateFmodPaths("fire_elemental");
		this.t.addResource("peppers", 1, true);
		this.clone("fire_elemental_blob", "fire_elemental");
		this.t.base_asset_id = "fire_elemental";
		this.t.show_in_taxonomy_tooltip = false;
		this.t.show_in_knowledge_window = false;
		this.t.base_stats["speed"] = 3f;
		this.t.base_stats["mass_2"] = 66f;
		this.clone("fire_elemental_horse", "fire_elemental");
		this.t.base_asset_id = "fire_elemental";
		this.t.show_in_taxonomy_tooltip = false;
		this.t.show_in_knowledge_window = false;
		this.t.base_stats["speed"] = 20f;
		this.t.base_stats["mass_2"] = 450f;
		this.clone("fire_elemental_slug", "fire_elemental");
		this.t.base_asset_id = "fire_elemental";
		this.t.show_in_taxonomy_tooltip = false;
		this.t.show_in_knowledge_window = false;
		this.t.prevent_unconscious_rotation = true;
		this.t.base_stats["speed"] = 2f;
		this.t.base_stats["mass_2"] = 30f;
		this.clone("fire_elemental_snake", "fire_elemental");
		this.t.base_asset_id = "fire_elemental";
		this.t.show_in_taxonomy_tooltip = false;
		this.t.show_in_knowledge_window = false;
		this.t.base_stats["speed"] = 5f;
		this.t.base_stats["mass_2"] = 15f;
		this.clone("ghost", "$mob$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"necromancer_set"
		});
		this.t.use_phenotypes = false;
		this.t.unit_other = true;
		this.t.name_locale = "Ghost";
		this.t.body_separate_part_hands = false;
		this.t.has_advanced_textures = true;
		this.t.has_baby_form = false;
		this.t.has_skin = false;
		this.t.shadow = false;
		this.t.can_turn_into_zombie = false;
		this.t.kingdom_id_wild = "undead";
		this.t.kingdom_id_civilization = "civ_ghost";
		this.t.can_be_killed_by_divine_light = true;
		this.t.base_stats["mass_2"] = 0f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("lifespan", 1000f)
		});
		this.t.icon = "iconGhost";
		this.t.color_hex = "#ffffff";
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"skeleton_job"
		});
		this.t.disable_jump_animation = true;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.max_random_amount = 1;
		this.t.can_turn_into_mush = false;
		this.t.mush_id = string.Empty;
		this.t.can_turn_into_tumor = false;
		this.t.tumor_id = string.Empty;
		this.t.has_skin = false;
		this.t.immune_to_injuries = true;
		this.t.die_on_blocks = false;
		this.t.sound_hit = "event:/SFX/HIT/HitGeneric";
		this.t.prevent_unconscious_rotation = true;
		this.t.architecture_id = "civ_ghost";
		this.t.banner_id = "civ_ghost";
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "otherworldlia";
		this.t.name_taxonomic_class = "ectoplasmica";
		this.t.name_taxonomic_order = "soulus";
		this.t.name_taxonomic_family = "transparencia";
		this.t.name_taxonomic_genus = "spectrum";
		this.t.name_taxonomic_species = "umbra";
		this.t.collective_term = "group_cloud";
		this.addTrait("weightless");
		this.addTrait("fire_proof");
		this.addTrait("freeze_proof");
		this.t.addLanguageTrait("spooky_language");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("hovering");
		this.t.addSubspeciesTrait("reproduction_soulborne");
		this.t.resources_given = null;
		this.clone("fire_skull", "$mob$");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"fire_skull_set"
		});
		this.t.kingdom_id_wild = "fire_skull";
		this.t.kingdom_id_civilization = "miniciv_fire_skull";
		this.t.architecture_id = "civ_demon";
		this.t.banner_id = "civ_demon";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.use_phenotypes = false;
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "infernalia";
		this.t.name_taxonomic_class = "daemonica";
		this.t.name_taxonomic_order = "skullus";
		this.t.name_taxonomic_family = "pyropidae";
		this.t.name_taxonomic_genus = "gorit";
		this.t.name_taxonomic_species = "dumkus";
		this.t.collective_term = "group_stack";
		this.t.addLanguageTrait("scorching_words");
		this.t.addSubspeciesTrait("reproduction_soulborne");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("adaptation_infernal");
		this.t.addSubspeciesTrait("egg_flames");
		this.t.base_stats["mass_2"] = 3.5f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 5f),
			new ValueTuple<string, float>("damage", 15f),
			new ValueTuple<string, float>("speed", 5f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Fire Skull";
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_ice_one = false;
		this.t.body_separate_part_hands = false;
		this.t.has_advanced_textures = false;
		this.t.has_baby_form = false;
		this.t.has_skin = false;
		this.t.icon = "iconFireSkull";
		this.t.color_hex = "#EE3A42";
		this.t.disable_jump_animation = true;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.max_random_amount = 4;
		this.addTrait("evil");
		this.addTrait("weightless");
		this.addTrait("fire_blood");
		this.addTrait("fire_proof");
		this.t.addResource("peppers", 1, true);
		this.t.addResource("bones", 2, false);
		this.clone("demon", "$mob$");
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"demon_set"
		});
		this.t.kingdom_id_wild = "demon";
		this.t.kingdom_id_civilization = "miniciv_demon";
		this.t.architecture_id = "civ_demon";
		this.t.banner_id = "civ_demon";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("reproduction_soulborne");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("egg_flames");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("spicy_kids");
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTrait("chaos_driven");
		this.t.addSubspeciesTrait("adaptation_infernal");
		this.t.addSubspeciesTrait("diet_hematophagy");
		this.t.addSubspeciesTrait("circadian_drift");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addCultureTrait("craft_flame_weapon");
		this.t.addLanguageTrait("scorching_words");
		this.t.addReligionTrait("rite_of_the_abyss");
		this.t.addReligionTrait("rite_of_infernal_wrath");
		this.t.addReligionTrait("infernal_rot");
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "infernalia";
		this.t.name_taxonomic_class = "daemonica";
		this.t.name_taxonomic_order = "diabolus";
		this.t.name_taxonomic_family = "maleficidae";
		this.t.name_taxonomic_genus = "daemorior";
		this.t.name_taxonomic_species = "maleficus";
		this.t.setSocialStructure("group_blaze", 40, true, true, FamilyParentsMode.None);
		this.t.base_stats["mass_2"] = 66.6f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("lifespan", 1000f),
			new ValueTuple<string, float>("damage", 35f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("offspring", 6f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Demon";
		this.t.body_separate_part_hands = true;
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"flame_sword"
		});
		this.t.can_be_killed_by_divine_light = true;
		this.t.die_in_lava = false;
		this.t.icon = "iconDemon";
		this.t.color_hex = "#EE3A42";
		this.t.skeleton_id = "skeleton";
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.disable_jump_animation = true;
		this.t.addDecision("attack_golden_brain");
		this.t.actor_size = ActorSize.S14_Cow;
		this.addPhenotype("skin_red", "default_color");
		this.addTrait("regeneration");
		this.addTrait("burning_feet");
		this.addTrait("fire_blood");
		this.addTrait("evil");
		this.t.music_theme = "Units_Demon";
		this.t.addResource("bones", 1, true);
		this.t.addResource("peppers", 2, false);
		this.t.addResource("meat", 1, false);
		this.clone("angle", "$mob$");
		this.t.setUnlockedWithAchievement("achievementAncientWarOfGeometryAndEvil");
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"angle_set"
		});
		this.t.kingdom_id_wild = "angle";
		this.t.kingdom_id_civilization = "miniciv_angle";
		this.t.architecture_id = "civ_angle";
		this.t.banner_id = "civ_angle";
		this.t.build_order_template_id = "build_order_basic_2";
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTrait("egg_orb");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("reproduction_divine");
		this.t.addSubspeciesTrait("pure");
		this.t.addSubspeciesTrait("gift_of_harmony");
		this.t.addSubspeciesTrait("hovering");
		this.t.addClanTrait("gods_chosen");
		this.t.addClanTrait("iron_will");
		this.t.addCultureTrait("city_layout_cross");
		this.t.addCultureTrait("xenophiles");
		this.t.addCultureTrait("fames_crown");
		this.t.addLanguageTrait("font_of_gods");
		this.t.addLanguageTrait("repeated_sentences");
		this.t.addLanguageTrait("eternal_text");
		this.t.addReligionTrait("rite_of_infinite_edges");
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "mathematica";
		this.t.name_taxonomic_class = "geometrica";
		this.t.name_taxonomic_order = "polygones";
		this.t.name_taxonomic_family = "holidae";
		this.t.name_taxonomic_genus = "anglo";
		this.t.name_taxonomic_species = "holliens";
		this.t.setSocialStructure("group_polygon", 40, true, true, FamilyParentsMode.None);
		this.t.base_stats["mass_2"] = 7.77f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 200f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("lifespan", 1000f),
			new ValueTuple<string, float>("damage", 35f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 10f),
			new ValueTuple<string, float>("offspring", 7f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Angle";
		this.t.body_separate_part_hands = true;
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"ice_hammer"
		});
		this.t.die_in_lava = false;
		this.t.icon = "iconAngle";
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_ice_one = false;
		this.t.color_hex = "#EE3A42";
		this.t.animation_idle = ActorAnimationSequences.walk_0_4;
		this.t.animation_walk = ActorAnimationSequences.walk_0_4;
		this.t.animation_swim = null;
		this.t.disable_jump_animation = true;
		this.t.actor_size = ActorSize.S14_Cow;
		this.t.prevent_unconscious_rotation = true;
		this.addPhenotype("bright_yellow", "default_color");
		this.addTrait("regeneration");
		this.addTrait("blessed");
		this.addTrait("light_lamp");
		this.addTrait("psychopath");
		this.t.music_theme = "Units_Demon";
		this.t.can_be_surprised = false;
		this.clone("fairy", "$peaceful_animal$");
		this.t.needs_to_be_explored = true;
		this.t.has_advanced_textures = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"fairy_set"
		});
		this.t.kingdom_id_wild = "fairy";
		this.t.kingdom_id_civilization = "miniciv_fairy";
		this.t.architecture_id = "civ_fairy";
		this.t.banner_id = "civ_fairy";
		this.t.addSubspeciesTrait("gift_of_life");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTrait("hyper_intelligence");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("diet_frugivore");
		this.t.addSubspeciesTrait("diet_florivore");
		this.t.addSubspeciesTrait("bioproduct_gold");
		this.t.addSubspeciesTrait("egg_rainbow");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("metamorphosis_butterfly");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.addSubspeciesTrait("gift_of_harmony");
		this.t.addSubspeciesTrait("gift_of_thunder");
		this.t.addSubspeciesTrait("hovering");
		this.t.addCultureTrait("ultimogeniture");
		this.t.addCultureTrait("tiny_legends");
		this.t.addCultureTrait("fames_crown");
		this.t.addReligionTrait("cast_cure");
		this.t.addLanguageTrait("melodic");
		this.t.addClanTrait("magic_blood");
		this.t.name_taxonomic_kingdom = "mythoria";
		this.t.name_taxonomic_phylum = "arthropoda";
		this.t.name_taxonomic_class = "insecta";
		this.t.name_taxonomic_order = "diptera";
		this.t.name_taxonomic_family = "fabulidae";
		this.t.name_taxonomic_genus = "faerina";
		this.t.name_taxonomic_species = "glitterbug";
		this.t.collective_term = "group_flutter";
		this.addPhenotype("bright_pink", "default_color");
		this.t.base_stats["mass_2"] = 0.01f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 40f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 300f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 8f),
			new ValueTuple<string, float>("speed", 15f),
			new ValueTuple<string, float>("armor", 2f),
			new ValueTuple<string, float>("offspring", 5f)
		});
		this.t.unit_other = true;
		this.t.default_animal = false;
		this.t.name_locale = "Fairy";
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = null;
		this.t.source_meat = false;
		this.t.source_meat_insect = false;
		this.t.actor_size = ActorSize.S3_Rat;
		this.t.shadow_texture = "unitShadow_2";
		this.t.icon = "iconFairy";
		this.t.color_hex = "#23F3FF";
		this.t.disable_jump_animation = true;
		this.t.has_soul = true;
		this.t.move_from_block = true;
		this.t.die_on_blocks = false;
		this.t.prevent_unconscious_rotation = true;
		this.t.animation_speed_based_on_walk_speed = false;
		this.addTrait("weightless");
		this.addTrait("healing_aura");
		this.addTrait("immune");
		this.addTrait("light_lamp");
		this.addTrait("moonchild");
		this.t.music_theme = "Units_Fairy";
		this.clone("bandit", "$mob$");
		this.t.render_heads_for_babies = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"bandit_set"
		});
		this.t.is_humanoid = true;
		this.t.architecture_id = "civ_bandit";
		this.t.kingdom_id_wild = "bandit";
		this.t.kingdom_id_civilization = "miniciv_bandit";
		this.t.banner_id = "civ_bandit";
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addSubspeciesTrait("reproduction_strategy_viviparity");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("nimble");
		this.t.addSubspeciesTrait("shiny_love");
		this.t.addSubspeciesTrait("circadian_drift");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("stomach");
		this.t.addCultureTrait("join_or_die");
		this.t.addCultureTrait("fames_crown");
		this.t.addClanTrait("nitroglycerin_blood");
		this.t.addClanTrait("combat_instincts");
		this.t.addReligionTrait("rite_of_dissent");
		this.t.cloneTaxonomyFromForSapiens("raccoon");
		this.t.name_taxonomic_genus = "banditus";
		this.t.name_taxonomic_species = "nikonis";
		this.t.collective_term = "group_gang";
		this.t.base_stats["mass_2"] = 67f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 100f),
			new ValueTuple<string, float>("lifespan", 60f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 5f),
			new ValueTuple<string, float>("diplomacy", 4f),
			new ValueTuple<string, float>("warfare", 6f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 4f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Bandit";
		this.t.body_separate_part_hands = true;
		this.t.kingdom_id_wild = "bandit";
		this.t.icon = "iconBandit";
		this.t.color_hex = "#4A3F35";
		this.t.skeleton_id = "skeleton";
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"spear_bronze",
			"spear_steel",
			"spear_iron",
			"sword_bronze",
			"sword_steel",
			"sword_iron",
			"bow_bronze",
			"bow_steel",
			"bow_iron"
		});
		this.t.has_soul = true;
		this.addPhenotype("skin_mixed", "default_color");
		this.addTrait("bomberman");
		this.addTrait("thief");
		this.t.disable_jump_animation = true;
		this.t.music_theme = "Units_Bandits";
		this.t.addResource("cider", 1, false);
		this.clone("snowman", "$mob$");
		this.t.render_heads_for_babies = true;
		this.t.needs_to_be_explored = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"snowman_set"
		});
		this.t.kingdom_id_wild = "snowman";
		this.t.kingdom_id_civilization = "miniciv_snowman";
		this.t.architecture_id = "civ_snowman";
		this.t.banner_id = "civ_snowman";
		this.t.build_order_template_id = "build_order_basic_2";
		this.addPhenotype("white_gray", "default_color");
		this.t.addSubspeciesTrait("reproduction_fission");
		this.t.addSubspeciesTrait("genetic_mirror");
		this.t.addSubspeciesTrait("egg_ice");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("genetic_psychosis");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTrait("good_throwers");
		this.t.addSubspeciesTrait("adaptation_permafrost");
		this.t.addLanguageTrait("chilly_font");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addReligionTrait("hand_of_order");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "cnidaria";
		this.t.name_taxonomic_class = "anthozoa";
		this.t.name_taxonomic_order = "cryonata";
		this.t.name_taxonomic_family = "niveidae";
		this.t.name_taxonomic_genus = "snowda";
		this.t.name_taxonomic_species = "frosti";
		this.t.collective_term = "group_melt";
		this.t.base_stats["mass_2"] = 67f;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 100f),
			new ValueTuple<string, float>("stamina", 50f),
			new ValueTuple<string, float>("lifespan", 60f),
			new ValueTuple<string, float>("mutation", 5f),
			new ValueTuple<string, float>("damage", 10f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 2f),
			new ValueTuple<string, float>("warfare", 2f),
			new ValueTuple<string, float>("stewardship", 2f),
			new ValueTuple<string, float>("intelligence", 2f)
		});
		this.t.unit_other = true;
		this.t.name_locale = "Snowman";
		this.t.default_attack = "snowball";
		this.t.icon = "iconSnowman";
		this.t.color_hex = "#FFFFFF";
		this.t.can_turn_into_mush = false;
		this.t.can_turn_into_tumor = false;
		this.t.can_turn_into_zombie = false;
		this.t.can_turn_into_ice_one = false;
		this.addTrait("heliophobia");
		this.addTrait("regeneration");
		this.addTrait("cold_aura");
		this.addTrait("fat");
		this.addTrait("freeze_proof");
		this.t.disable_jump_animation = true;
		this.t.music_theme = "Units_Snowman";
		this.t.addResource("pine_cones", 1, true);
		this.clone("alien", "$mob$");
		this.t.render_heads_for_babies = true;
		this.t.needs_to_be_explored = true;
		this.t.is_humanoid = true;
		this.t.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"alien_set"
		});
		this.t.architecture_id = "civ_alien";
		this.t.kingdom_id_wild = "aliens";
		this.t.kingdom_id_civilization = "civ_aliens";
		this.t.banner_id = "civ_alien";
		this.t.build_order_template_id = "build_order_basic_2";
		this.addPhenotype("bright_green", "default_color");
		this.t.addSubspeciesTrait("prefrontal_cortex");
		this.t.addSubspeciesTrait("advanced_hippocampus");
		this.t.addSubspeciesTrait("amygdala");
		this.t.addSubspeciesTrait("wernicke_area");
		this.t.addSubspeciesTrait("stomach");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTrait("fins");
		this.t.addSubspeciesTrait("hyper_intelligence");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTrait("egg_alien");
		this.t.addSubspeciesTrait("reproduction_strategy_oviparity");
		this.t.addSubspeciesTrait("reproduction_sexual");
		this.t.addClanTrait("best_five");
		this.t.addCultureTrait("city_layout_tile_moonsteps");
		this.t.addCultureTrait("city_layout_iron_weave");
		this.t.addCultureTrait("craft_blaster");
		this.t.addReligionTrait("rite_of_fractured_minds");
		this.t.addReligionTrait("minds_awakening");
		this.t.name_taxonomic_kingdom = "animalia";
		this.t.name_taxonomic_phylum = "tardigrada";
		this.t.name_taxonomic_class = "eutardigrada";
		this.t.name_taxonomic_order = "apochela";
		this.t.name_taxonomic_family = "milnesiidae";
		this.t.name_taxonomic_genus = "abugus";
		this.t.name_taxonomic_species = "abobicus";
		this.t.collective_term = "group_topology";
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("health", 200f),
			new ValueTuple<string, float>("stamina", 150f),
			new ValueTuple<string, float>("lifespan", 60f),
			new ValueTuple<string, float>("mutation", 2f),
			new ValueTuple<string, float>("damage", 18f),
			new ValueTuple<string, float>("speed", 10f),
			new ValueTuple<string, float>("armor", 5f),
			new ValueTuple<string, float>("offspring", 3f),
			new ValueTuple<string, float>("diplomacy", 7f),
			new ValueTuple<string, float>("warfare", 7f),
			new ValueTuple<string, float>("stewardship", 7f),
			new ValueTuple<string, float>("intelligence", 7f)
		});
		this.t.unit_other = true;
		this.t.body_separate_part_hands = true;
		this.t.name_locale = "Alien";
		this.t.base_stats["lifespan"] = 1000f;
		this.t.base_stats["mass_2"] = 32.5f;
		this.t.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"alien_blaster"
		});
		this.t.icon = "iconAlien";
		this.t.color_hex = "#00FF00";
		this.t.can_turn_into_tumor = true;
		this.t.can_turn_into_mush = true;
		this.t.mush_id = "mush_unit";
		this.t.tumor_id = "tumor_monster_unit";
		this.t.has_soul = true;
		this.t.family_banner_frame_generation_inclusion = "families/frame_11";
		this.t.family_banner_frame_only_inclusion = true;
		this.addTrait("regeneration");
		this.addTrait("fat");
		this.addTrait("acid_blood");
		this.addTrait("acid_proof");
		this.addTrait("strong_minded");
		this.t.disable_jump_animation = true;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00051D70 File Offset: 0x0004FF70
	private void initTemplates()
	{
		ActorAsset actorAsset = new ActorAsset();
		actorAsset.id = "$basic_unit$";
		ActorAsset pAsset = actorAsset;
		this.t = actorAsset;
		this.add(pAsset);
		this.t.base_stats["attack_speed"] = 1f;
		this.t.base_stats["accuracy"] = 1f;
		this.t.base_stats["mass"] = 1f;
		this.t.base_stats["knockback"] = 1.5f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["area_of_effect"] = 0.1f;
		this.t.base_stats["size"] = 0.5f;
		this.t.base_stats["range"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 2f;
		this.t.base_stats["scale"] = 0.1f;
		this.t.base_stats["multiplier_supply_timer"] = 1f;
		this.t.base_throwing_range = 7f;
		this.t.affected_by_dust = true;
		this.t.needs_to_be_explored = false;
		this.t.job = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"decision"
		});
		this.clone("$basic_unit_colored$", "$basic_unit$");
		this.t.has_advanced_textures = true;
		this.t.has_baby_form = true;
		this.t.setSimpleCivSettings();
		this.t.kingdom_id_wild = "neutral_animals";
		this.t.can_edit_equipment = true;
		this.t.use_items = true;
		this.t.take_items = true;
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.need_colored_sprite = true;
		this.t.update_z = true;
		this.t.can_be_killed_by_stuff = true;
		this.t.can_be_killed_by_life_eraser = true;
		this.t.can_attack_buildings = true;
		this.t.can_be_moved_by_powers = true;
		this.t.can_be_hurt_by_powers = true;
		this.t.effect_damage = true;
		this.t.can_flip = true;
		this.t.death_animation_angle = true;
		this.t.can_be_inspected = true;
		this.t.can_have_subspecies = true;
		this.t.use_phenotypes = true;
		this.t.addResource("meat", 1, false);
		this.t.addResource("bones", 1, false);
		this.clone("$animal_base$", "$basic_unit_colored$");
		this.t.build_order_template_id = "build_order_basic";
		this.t.has_advanced_textures = false;
		this.t.default_animal = true;
		this.clone("$animal_fur$", "$animal_base$");
		this.addPhenotype("savanna", "biome_savanna");
		this.addPhenotype("dark_teal", "biome_crystal");
		this.addPhenotype("dark_blue", "biome_crystal");
		this.addPhenotype("dark_orange", "biome_savanna");
		this.addPhenotype("swamp", "biome_swamp");
		this.addPhenotype("skin_blue", "biome_swamp");
		this.addPhenotype("corrupted", "biome_corrupted");
		this.addPhenotype("desert", "biome_desert");
		this.addPhenotype("skin_yellow", "biome_desert");
		this.addPhenotype("dark_yellow", "biome_desert");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("lemon", "biome_lemon");
		this.addPhenotype("pink_yellow_mushroom", "biome_mushroom");
		this.addPhenotype("dark_orange", "biome_sand");
		this.addPhenotype("wood", "biome_sand");
		this.addPhenotype("bright_violet", "biome_singularity");
		this.addPhenotype("mid_gray", "biome_garlic");
		this.addPhenotype("dark_orange", "biome_maple");
		this.addPhenotype("polar", "biome_permafrost");
		this.addPhenotype("gray_black", "biome_rocklands");
		this.addPhenotype("bright_purple", "biome_celestial");
		this.addPhenotype("magical", "biome_celestial");
		this.addPhenotype("magical", "biome_mushroom");
		this.addPhenotype("dark_purple", "biome_mushroom");
		this.addPhenotype("skin_pink", "biome_candy");
		this.addPhenotype("dark_pink", "biome_candy");
		this.clone("$animal_skin$", "$animal_base$");
		this.addPhenotype("corrupted", "biome_corrupted");
		this.addPhenotype("infernal", "biome_infernal");
		this.addPhenotype("lemon", "biome_lemon");
		this.clone("$civ_unit$", "$basic_unit_colored$");
		this.t.render_heads_for_babies = true;
		this.t.chromosomes_first = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"chromosome_big",
			"chromosome_medium"
		});
		this.t.setCanTurnIntoZombieAsset("zombie", true);
		this.t.addSubspeciesTrait("prefrontal_cortex");
		this.t.genome_size = 20;
		this.t.civ = true;
		this.t.actor_size = ActorSize.S13_Human;
		this.t.inspect_home = true;
		this.t.body_separate_part_hands = true;
		this.t.has_soul = true;
		this.t.setSocialStructure("group_family", 10, true, false, FamilyParentsMode.Normal);
		this.t.name_taxonomic_species = "sapiens";
		this.t.civ_base_cities = 5;
		this.t.can_turn_into_demon_in_age_of_chaos = true;
		this.t.can_turn_into_mush = true;
		this.t.can_turn_into_ice_one = true;
		this.t.mush_id = "mush_unit";
		this.t.can_turn_into_tumor = true;
		this.t.tumor_id = "tumor_monster_unit";
		this.t.animation_walk = ActorAnimationSequences.walk_0_3;
		this.t.animation_swim = ActorAnimationSequences.swim_0_3;
		this.t.default_attack = "hands";
		this.t.skeleton_id = "skeleton";
		this.t.disable_jump_animation = true;
		this.t.needs_to_be_explored = false;
		this.clone("$civ_advanced_unit$", "$civ_unit$");
		this.t.skin_citizen_male = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"male_1",
			"male_2",
			"male_3",
			"male_4",
			"male_5",
			"male_6",
			"male_7",
			"male_8",
			"male_9",
			"male_10"
		});
		this.t.skin_citizen_female = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"female_1",
			"female_2",
			"female_3",
			"female_4",
			"female_5",
			"female_6",
			"female_7",
			"female_8",
			"female_9",
			"female_10"
		});
		this.t.skin_warrior = AssetLibrary<ActorAsset>.a<string>(new string[]
		{
			"warrior_1",
			"warrior_2",
			"warrior_3",
			"warrior_4",
			"warrior_5",
			"warrior_6",
			"warrior_7",
			"warrior_8",
			"warrior_9",
			"warrior_10"
		});
		this.t.is_humanoid = true;
		this.clone("$mob_no_genes$", "$basic_unit_colored$");
		this.t.inspect_children = false;
		this.t.default_attack = "base_attack";
		this.t.kingdom_id_civilization = string.Empty;
		this.t.build_order_template_id = string.Empty;
		this.t.disable_jump_animation = true;
		this.t.can_have_subspecies = false;
		this.clone("$mob$", "$basic_unit_colored$");
		this.t.default_attack = "base_attack";
		this.t.can_have_subspecies = true;
		this.t.disable_jump_animation = true;
		this.t.can_turn_into_mush = true;
		this.t.can_turn_into_ice_one = true;
		this.t.mush_id = "mush_unit";
		this.t.can_turn_into_tumor = true;
		this.t.tumor_id = "tumor_monster_unit";
		this.t.setCanTurnIntoZombieAsset("zombie", true);
		this.clone("$animal$", "$animal_base$");
		this.t.setCanTurnIntoZombieAsset("zombie_animal", true);
		this.t.can_turn_into_mush = true;
		this.t.mush_id = "mush_animal";
		this.t.can_turn_into_tumor = true;
		this.t.tumor_id = "tumor_monster_animal";
		this.t.source_meat = true;
		this.t.default_attack = "jaws";
		this.clone("$peaceful_animal$", "$animal_base$");
		this.t.setCanTurnIntoZombieAsset("zombie_animal", true);
		this.t.can_turn_into_mush = true;
		this.t.mush_id = "mush_animal";
		this.t.can_turn_into_tumor = true;
		this.t.tumor_id = "tumor_monster_animal";
		this.t.base_stats["damage"] = 1f;
		this.addTrait("peaceful");
		this.clone("$carnivore$", "$animal$");
		this.t.addSubspeciesTrait("diet_carnivore");
		this.clone("$herbivore$", "$animal_base$");
		this.t.can_turn_into_mush = true;
		this.t.mush_id = "mush_animal";
		this.t.setCanTurnIntoZombieAsset("zombie_animal", true);
		this.t.addSubspeciesTrait("diet_herbivore");
		this.clone("$omnivore$", "$animal$");
		this.t.addSubspeciesTrait("diet_omnivore");
		this.clone("$insect$", "$animal_base$");
		this.t.chromosomes_first = AssetLibrary<ActorAsset>.l<string>(new string[]
		{
			"chromosome_tiny"
		});
		this.t.has_baby_form = false;
		this.t.has_advanced_textures = false;
		this.t.setCanTurnIntoZombieAsset("zombie", true);
		this.t.kingdom_id_wild = "insect";
		this.t.kingdom_id_civilization = "miniciv_insect";
		this.t.source_meat_insect = true;
		this.t.actor_size = ActorSize.S0_Bug;
		this.t.shadow_texture = "unitShadow_2";
		this.t.color_hex = "#23F3FF";
		this.t.animation_idle = ActorAnimationSequences.walk_0_2;
		this.t.animation_walk = ActorAnimationSequences.walk_0_2;
		this.t.animation_swim = null;
		this.t.base_stats["speed"] = 5f;
		this.t.base_stats["health"] = 1f;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["mass_2"] = 0.015f;
		this.addTrait("peaceful");
		this.t.max_random_amount = 5;
		this.t.addResource("jam", 1, true);
		this.clone("$flying_insect$", "$insect$");
		this.t.animation_idle = ActorAnimationSequences.walk_0_1;
		this.t.animation_walk = ActorAnimationSequences.walk_0_1;
		this.t.animation_swim = null;
		this.t.disable_jump_animation = true;
		this.t.move_from_block = true;
		this.addTrait("weightless");
		this.t.addSubspeciesTrait("hovering");
		this.clone("$animal_civ$", "$civ_unit$");
		this.t.render_heads_for_babies = true;
		this.t.name_locale = "Greg";
		this.t.icon = "iconHumans";
		this.t.setCanTurnIntoZombieAsset("zombie", true);
		this.t.color_hex = "#005E72";
		this.t.disable_jump_animation = true;
		this.t.addGenome(new ValueTuple<string, float>[]
		{
			new ValueTuple<string, float>("diplomacy", 3f),
			new ValueTuple<string, float>("warfare", 3f),
			new ValueTuple<string, float>("stewardship", 3f),
			new ValueTuple<string, float>("intelligence", 3f)
		});
		this.t.needs_to_be_explored = true;
		this.t.is_humanoid = true;
	}

	// Token: 0x0400058E RID: 1422
	[NonSerialized]
	public List<ActorAsset> list_only_boat_assets;

	// Token: 0x0400058F RID: 1423
	private int _humanoids_amount;

	// Token: 0x04000590 RID: 1424
	private const string TEMPLATE_BASIC_UNIT = "$basic_unit$";

	// Token: 0x04000591 RID: 1425
	private const string TEMPLATE_BASIC_UNIT_COLORED = "$basic_unit_colored$";

	// Token: 0x04000592 RID: 1426
	private const string TEMPLATE_ANIMAL_BASE = "$animal_base$";

	// Token: 0x04000593 RID: 1427
	private const string TEMPLATE_ANIMAL = "$animal$";

	// Token: 0x04000594 RID: 1428
	private const string TEMPLATE_ANIMAL_FUR = "$animal_fur$";

	// Token: 0x04000595 RID: 1429
	private const string TEMPLATE_ANIMAL_SKIN = "$animal_skin$";

	// Token: 0x04000596 RID: 1430
	private const string TEMPLATE_PEACEFUL_ANIMAL = "$peaceful_animal$";

	// Token: 0x04000597 RID: 1431
	private const string TEMPLATE_CARNIVORE = "$carnivore$";

	// Token: 0x04000598 RID: 1432
	private const string TEMPLATE_HERBIVORE = "$herbivore$";

	// Token: 0x04000599 RID: 1433
	private const string TEMPLATE_OMNIVORE = "$omnivore$";

	// Token: 0x0400059A RID: 1434
	private const string TEMPLATE_CIV_UNIT = "$civ_unit$";

	// Token: 0x0400059B RID: 1435
	private const string TEMPLATE_CIV_ADVANCED_UNIT = "$civ_advanced_unit$";

	// Token: 0x0400059C RID: 1436
	private const string TEMPLATE_BOAT = "$boat$";

	// Token: 0x0400059D RID: 1437
	private const string TEMPLATE_BOAT_TRADING = "$boat_trading$";

	// Token: 0x0400059E RID: 1438
	private const string TEMPLATE_BOAT_TRANSPORT = "$boat_transport$";

	// Token: 0x0400059F RID: 1439
	private const string TEMPLATE_MOB_NO_GENES = "$mob_no_genes$";

	// Token: 0x040005A0 RID: 1440
	private const string TEMPLATE_MOB = "$mob$";

	// Token: 0x040005A1 RID: 1441
	private const string TEMPLATE_CREEP_MOB = "$creep_mob$";

	// Token: 0x040005A2 RID: 1442
	private const string TEMPLATE_ANIMAL_CIV = "$animal_civ$";

	// Token: 0x040005A3 RID: 1443
	private const string TEMPLATE_INSECT = "$insect$";

	// Token: 0x040005A4 RID: 1444
	private const string TEMPLATE_FLYING_INSECT = "$flying_insect$";
}
