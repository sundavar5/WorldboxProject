using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200017D RID: 381
[ObfuscateLiterals]
public class StatusLibrary : AssetLibrary<StatusAsset>
{
	// Token: 0x06000B56 RID: 2902 RVA: 0x000A22D4 File Offset: 0x000A04D4
	public override void init()
	{
		base.init();
		this.add(new StatusAsset
		{
			id = "handsome_migrant",
			locale_id = "status_title_handsome_migrant",
			locale_description = "status_description_handsome_migrant",
			duration = 360f,
			path_icon = "ui/Icons/iconStatisticsGoodLookingMigrants"
		});
		this.add(new StatusAsset
		{
			id = "recovery_plot",
			locale_id = "status_title_recovery_plot",
			locale_description = "status_description_recovery_plot",
			duration = 60f,
			path_icon = "ui/Icons/iconRecoveryPlot"
		});
		this.add(new StatusAsset
		{
			id = "voices_in_my_head",
			locale_id = "status_title_voices_in_my_head",
			locale_description = "status_description_voices_in_my_head",
			duration = 180f,
			path_icon = "ui/Icons/iconVoicesInMyHead"
		});
		this.t.base_stats["diplomacy"] = -5f;
		this.t.base_stats["personality_rationality"] = -0.3f;
		this.t.base_stats["opinion"] = -5f;
		this.add(new StatusAsset
		{
			id = "recovery_spell",
			locale_id = "status_title_recovery_spell",
			locale_description = "status_description_recovery_spell",
			duration = 5f,
			path_icon = "ui/Icons/iconRecoverySpell"
		});
		this.add(new StatusAsset
		{
			id = "recovery_social",
			locale_id = "status_title_recovery_social",
			locale_description = "status_description_recovery_social",
			duration = 60f,
			path_icon = "ui/Icons/iconRecoverySocial"
		});
		this.add(new StatusAsset
		{
			id = "recovery_combat_action",
			locale_id = "status_title_recovery_combat_action",
			locale_description = "status_description_recovery_combat_action",
			duration = 1f,
			path_icon = "ui/Icons/iconRecoveryCombatAction"
		});
		StatusAsset statusAsset = new StatusAsset();
		statusAsset.id = "starving";
		statusAsset.locale_id = "status_title_starvation";
		statusAsset.locale_description = "status_description_starvation";
		statusAsset.duration = 60f;
		statusAsset.path_icon = "ui/Icons/iconHungry";
		statusAsset.action_on_receive = ((BaseSimObject pActor, WorldTile _) => pActor.a.changeHappiness("starving", 0));
		statusAsset.action_interval = 5f;
		statusAsset.action = delegate(BaseSimObject pObject, WorldTile pTile)
		{
			Actor tActor = pObject.a;
			if (!tActor.isAlive())
			{
				return false;
			}
			if (!tActor.hasCity())
			{
				return false;
			}
			if (tActor.isFighting())
			{
				return false;
			}
			City tCity = tActor.city;
			if (tActor.hasTask() && tActor.ai.task.diet)
			{
				return false;
			}
			if (!tCity.hasSuitableFood(tActor.subspecies))
			{
				tActor.cancelAllBeh();
				return false;
			}
			tActor.setTask("try_to_eat_city_food", true, false, false);
			return true;
		};
		this.add(statusAsset);
		this.t.tier = StatusTier.Advanced;
		StatusAsset statusAsset2 = new StatusAsset();
		statusAsset2.id = "drowning";
		statusAsset2.locale_id = "status_title_drowning";
		statusAsset2.locale_description = "status_description_drowning";
		statusAsset2.duration = 1f;
		statusAsset2.path_icon = "ui/Icons/iconDrowning";
		statusAsset2.action_interval = 0.5f;
		statusAsset2.action = delegate(BaseSimObject pObject, WorldTile pTile)
		{
			Actor tActor = pObject.a;
			if (!tActor.isAlive())
			{
				return false;
			}
			tActor.getHit(1f, true, AttackType.Drowning, null, true, false, true);
			EffectsLibrary.spawnAt("fx_drowning", tActor.current_position, 0.1f);
			return true;
		};
		statusAsset2.action_death = delegate(BaseSimObject pObject, WorldTile pTile)
		{
			Actor tActor = pObject.a;
			EffectsLibrary.spawnAt("fx_drowning", tActor.current_position, 0.1f);
			return true;
		};
		this.add(statusAsset2);
		this.t.tier = StatusTier.Advanced;
		this.t.base_stats.addTag("ignore_fights");
		this.add(new StatusAsset
		{
			id = "sleeping",
			render_priority = 8,
			locale_id = "status_title_sleeping",
			locale_description = "status_description_sleeping",
			duration = 60f,
			animated = true,
			is_animated_in_pause = true,
			can_be_flipped = false,
			use_parent_rotation = false,
			removed_on_damage = true,
			texture = "fx_status_sleeping_t",
			path_icon = "ui/Icons/iconSleep",
			action_finish = delegate(BaseSimObject pActor, WorldTile _)
			{
				Actor a = pActor.a;
				string tRandomStatus = this._pot_dreams.GetRandom<string>();
				if (!string.IsNullOrEmpty(tRandomStatus))
				{
					pActor.a.addStatusEffect(tRandomStatus, 0f, true);
				}
				a.restoreStaminaPercent(0.2f);
				return pActor.a.changeHappiness("just_slept", 0);
			}
		});
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"tantrum",
			"surprised"
		});
		this.t.tier = StatusTier.Advanced;
		this.t.base_stats.addTag("unconscious");
		StatusAsset statusAsset3 = new StatusAsset();
		statusAsset3.id = "laughing";
		statusAsset3.render_priority = 4;
		statusAsset3.locale_id = "status_title_laughing";
		statusAsset3.locale_description = "status_description_laughing";
		statusAsset3.duration = 60f;
		statusAsset3.animated = true;
		statusAsset3.is_animated_in_pause = true;
		statusAsset3.can_be_flipped = false;
		statusAsset3.use_parent_rotation = false;
		statusAsset3.removed_on_damage = true;
		statusAsset3.texture = "fx_status_laughing_t";
		statusAsset3.path_icon = "ui/Icons/iconLaughing";
		statusAsset3.action_on_receive = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.playIdleSound();
			return true;
		};
		this.add(statusAsset3);
		this.t.tier = StatusTier.Advanced;
		StatusAsset statusAsset4 = new StatusAsset();
		statusAsset4.id = "singing";
		statusAsset4.render_priority = 4;
		statusAsset4.locale_id = "status_title_singing";
		statusAsset4.locale_description = "status_description_singing";
		statusAsset4.duration = 60f;
		statusAsset4.animated = true;
		statusAsset4.is_animated_in_pause = true;
		statusAsset4.can_be_flipped = false;
		statusAsset4.use_parent_rotation = false;
		statusAsset4.removed_on_damage = true;
		statusAsset4.texture = "fx_status_singing_t";
		statusAsset4.path_icon = "ui/Icons/iconSinging";
		statusAsset4.action_on_receive = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.playIdleSound();
			return true;
		};
		this.add(statusAsset4);
		this.t.tier = StatusTier.Advanced;
		StatusAsset statusAsset5 = new StatusAsset();
		statusAsset5.id = "swearing";
		statusAsset5.render_priority = 4;
		statusAsset5.locale_id = "status_title_swearing";
		statusAsset5.locale_description = "status_description_swearing";
		statusAsset5.duration = 60f;
		statusAsset5.animated = true;
		statusAsset5.is_animated_in_pause = true;
		statusAsset5.can_be_flipped = false;
		statusAsset5.use_parent_rotation = false;
		statusAsset5.removed_on_damage = true;
		statusAsset5.texture = "fx_status_swearing_t";
		statusAsset5.path_icon = "ui/Icons/iconSwearing";
		statusAsset5.action_on_receive = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.playIdleSound();
			return true;
		};
		this.add(statusAsset5);
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"peaceful"
		});
		this.t.tier = StatusTier.Advanced;
		this.t.base_stats.addTag("moody");
		StatusAsset statusAsset6 = new StatusAsset();
		statusAsset6.id = "crying";
		statusAsset6.render_priority = 4;
		statusAsset6.locale_id = "status_title_crying";
		statusAsset6.locale_description = "status_description_crying";
		statusAsset6.duration = 60f;
		statusAsset6.animated = true;
		statusAsset6.is_animated_in_pause = true;
		statusAsset6.can_be_flipped = false;
		statusAsset6.use_parent_rotation = false;
		statusAsset6.removed_on_damage = true;
		statusAsset6.texture = "fx_status_crying_t";
		statusAsset6.path_icon = "ui/Icons/iconCrying";
		statusAsset6.action_on_receive = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.playIdleSound();
			return true;
		};
		this.add(statusAsset6);
		this.t.tier = StatusTier.Advanced;
		this.t.base_stats.addTag("moody");
		StatusAsset statusAsset7 = new StatusAsset();
		statusAsset7.id = "possessed";
		statusAsset7.locale_id = "status_title_possessed";
		statusAsset7.locale_description = "status_description_possessed";
		statusAsset7.duration = 10f;
		statusAsset7.animated = true;
		statusAsset7.path_icon = "ui/Icons/iconPossessed";
		statusAsset7.action_on_receive = ((BaseSimObject pActor, WorldTile _) => pActor.a.changeHappiness("just_possessed", 0));
		statusAsset7.action_interval = 0f;
		statusAsset7.action = new WorldAction(this.possessedAction);
		this.add(statusAsset7);
		this.add(new StatusAsset
		{
			id = "possessed_follower",
			locale_id = "status_title_possessed_follower",
			locale_description = "status_description_possessed_follower",
			duration = 200f,
			affects_mind = true,
			animated = true,
			is_animated_in_pause = true,
			texture = "fx_status_possessed_follower_t",
			path_icon = "ui/Icons/iconPossessed",
			decision_id = "possessed_following"
		});
		StatusAsset statusAsset8 = new StatusAsset();
		statusAsset8.id = "strange_urge";
		statusAsset8.locale_id = "status_title_strange_urge";
		statusAsset8.locale_description = "status_description_strange_urge";
		statusAsset8.duration = 100f;
		statusAsset8.animated = true;
		statusAsset8.path_icon = "ui/Icons/iconStrangeUrge";
		statusAsset8.action_on_receive = ((BaseSimObject pActor, WorldTile _) => !Randy.randomChance(0.7f) && pActor.a.changeHappiness("strange_urge", 0));
		this.add(statusAsset8);
		StatusAsset statusAsset9 = new StatusAsset();
		statusAsset9.id = "tantrum";
		statusAsset9.locale_id = "status_title_tantrum";
		statusAsset9.locale_description = "status_description_tantrum";
		statusAsset9.duration = 120f;
		statusAsset9.affects_mind = true;
		statusAsset9.path_icon = "ui/Icons/iconTantrum";
		statusAsset9.action_finish = ((BaseSimObject pActor, WorldTile _) => pActor.a.changeHappiness("just_had_tantrum", 0));
		statusAsset9.decision_id = "do_tantrum";
		this.add(statusAsset9);
		StatusAsset statusAsset10 = new StatusAsset();
		statusAsset10.id = "egg";
		statusAsset10.locale_id = "status_egg";
		statusAsset10.locale_description = "status_description_egg";
		statusAsset10.duration = 5f;
		statusAsset10.animated = false;
		statusAsset10.path_icon = "ui/Icons/iconEgg";
		statusAsset10.action_finish = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.makeStunned(5f);
			if (pActor.a.isRendered())
			{
				EffectsLibrary.spawn("fx_spawn", pActor.current_tile, null, null, 0f, pActor.current_position.x, pActor.current_position.y, null);
			}
			return true;
		};
		this.add(statusAsset10);
		this.t.base_stats.addTag("immovable");
		this.t.base_stats.addTag("frozen_ai");
		this.t.base_stats["armor"] = 10f;
		StatusAsset statusAsset11 = new StatusAsset();
		statusAsset11.id = "cursed";
		statusAsset11.locale_id = "status_title_cursed";
		statusAsset11.locale_description = "status_description_cursed";
		statusAsset11.duration = 300f;
		statusAsset11.animated = false;
		statusAsset11.path_icon = "ui/Icons/iconCursed";
		statusAsset11.can_be_cured = true;
		statusAsset11.action_on_receive = ((BaseSimObject pActor, WorldTile _) => pActor.a.changeHappiness("just_cursed", 0));
		this.add(statusAsset11);
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"evil"
		});
		StatusAsset t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.turnIntoSkeleton));
		this.t.base_stats["loyalty_traits"] = -100f;
		this.t.base_stats["multiplier_damage"] = -0.5f;
		this.t.base_stats["multiplier_health"] = -0.5f;
		this.t.base_stats["multiplier_speed"] = -0.2f;
		this.t.base_stats["multiplier_diplomacy"] = -0.9f;
		this.t.base_stats["lifespan"] = -10f;
		this.t.base_stats["multiplier_offspring"] = -2f;
		this.add(new StatusAsset
		{
			id = "spell_silence",
			locale_id = "status_spell_silence",
			locale_description = "status_description_spell_silence",
			duration = 60f,
			animated = false,
			path_icon = "ui/Icons/iconSpellSilence"
		});
		this.add(new StatusAsset
		{
			id = "spell_boost",
			locale_id = "status_spell_boost",
			locale_description = "status_description_spell_boost",
			duration = 180f,
			animated = false,
			path_icon = "ui/Icons/iconSpellBoost"
		});
		this.t.base_stats["mana"] = 100f;
		this.t.base_stats["skill_spell"] = 0.2f;
		StatusAsset statusAsset12 = new StatusAsset();
		statusAsset12.id = "inspired";
		statusAsset12.locale_id = "status_title_inspired";
		statusAsset12.locale_description = "status_description_inspired";
		statusAsset12.duration = 60f;
		statusAsset12.animated = false;
		statusAsset12.path_icon = "ui/Icons/iconInspired";
		statusAsset12.action_finish = ((BaseSimObject pActor, WorldTile _) => pActor.a.changeHappiness("just_inspired", 0));
		this.add(statusAsset12);
		this.t.base_stats["multiplier_speed"] = 0.3f;
		this.t.base_stats["multiplier_crit"] = 0.3f;
		this.t.base_stats["multiplier_attack_speed"] = 0.3f;
		this.add(new StatusAsset
		{
			id = "confused",
			locale_id = "status_title_confused",
			locale_description = "status_description_confused",
			duration = 10f,
			animated = false,
			affects_mind = true,
			path_icon = "ui/Icons/iconConfused",
			decision_id = "status_confused"
		});
		this.t.opposite_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"egg"
		});
		this.add(new StatusAsset
		{
			id = "soul_harvested",
			locale_id = "status_title_soul_harvested",
			locale_description = "status_description_soul_harvested",
			duration = 666f,
			animated = false,
			path_icon = "ui/Icons/iconSoulHarvested",
			decision_id = "status_soul_harvested"
		});
		StatusAsset statusAsset13 = new StatusAsset();
		statusAsset13.id = "magnetized";
		statusAsset13.locale_id = "status_title_magnetized";
		statusAsset13.locale_description = "status_description_magnetized";
		statusAsset13.duration = 4f;
		statusAsset13.animated = false;
		statusAsset13.path_icon = "ui/Icons/iconMagnetized";
		statusAsset13.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("just_magnetised", 0));
		this.add(statusAsset13);
		this.add(new StatusAsset
		{
			id = "rage",
			locale_id = "status_title_rage",
			locale_description = "status_description_rage",
			duration = 300f,
			animated = false,
			affects_mind = true,
			path_icon = "ui/Icons/iconRage"
		});
		this.t.base_stats["multiplier_damage"] = 1f;
		StatusAsset statusAsset14 = new StatusAsset();
		statusAsset14.id = "surprised";
		statusAsset14.render_priority = 9;
		statusAsset14.locale_id = "status_title_surprised";
		statusAsset14.locale_description = "status_description_surprised";
		statusAsset14.duration = 2f;
		statusAsset14.texture = "fx_status_surprised_t";
		statusAsset14.animated = true;
		statusAsset14.is_animated_in_pause = true;
		statusAsset14.loop = false;
		statusAsset14.use_parent_rotation = false;
		statusAsset14.path_icon = "ui/Icons/iconSurprised";
		statusAsset14.offset_y = 1f;
		statusAsset14.offset_y_ui = 0.65f;
		statusAsset14.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("just_surprised", 0));
		this.add(statusAsset14);
		this.add(new StatusAsset
		{
			id = "on_guard",
			render_priority = 8,
			locale_id = "status_title_on_guard",
			locale_description = "status_description_on_guard",
			duration = 60f,
			use_parent_rotation = false,
			path_icon = "ui/Icons/iconOnGuard"
		});
		StatusAsset statusAsset15 = new StatusAsset();
		statusAsset15.id = "angry";
		statusAsset15.locale_id = "status_title_angry";
		statusAsset15.locale_description = "status_description_angry";
		statusAsset15.duration = 60f;
		statusAsset15.use_parent_rotation = false;
		statusAsset15.path_icon = "ui/Icons/iconAngry";
		statusAsset15.animated = true;
		statusAsset15.is_animated_in_pause = true;
		statusAsset15.texture = "fx_status_angry_t";
		statusAsset15.action_finish = delegate(BaseSimObject pActor, WorldTile _)
		{
			pActor.a.finishAngryStatus();
			return true;
		};
		this.add(statusAsset15);
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"peaceful"
		});
		this.add(new StatusAsset
		{
			id = "just_ate",
			locale_id = "status_title_just_ate",
			locale_description = "status_description_just_ate",
			duration = 200f,
			path_icon = "ui/Icons/iconHunger",
			decision_id = "try_to_poop"
		});
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"starving"
		});
		this.add(new StatusAsset
		{
			id = "festive_spirit",
			locale_id = "status_title_festive_spirit",
			locale_description = "status_description_festive_spirit",
			duration = 100f,
			path_icon = "ui/Icons/iconFireworks",
			decision_id = "try_to_launch_fireworks"
		});
		this.add(new StatusAsset
		{
			id = "being_suspicious",
			locale_id = "status_title_being_suspicious",
			locale_description = "status_description_being_suspicious",
			duration = 20f,
			path_icon = "ui/Icons/iconSuspicious",
			decision_id = "run_away_being_sus"
		});
		StatusAsset statusAsset16 = new StatusAsset();
		statusAsset16.id = "had_good_dream";
		statusAsset16.locale_id = "status_title_had_good_dream";
		statusAsset16.locale_description = "status_description_had_good_dream";
		statusAsset16.duration = 90f;
		statusAsset16.path_icon = "ui/Icons/iconDreamGood";
		statusAsset16.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("had_good_dream", 0));
		this.add(statusAsset16);
		StatusAsset statusAsset17 = new StatusAsset();
		statusAsset17.id = "had_bad_dream";
		statusAsset17.locale_id = "status_title_had_bad_dream";
		statusAsset17.locale_description = "status_description_had_bad_dream";
		statusAsset17.duration = 90f;
		statusAsset17.path_icon = "ui/Icons/iconDreamBad";
		statusAsset17.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("had_bad_dream", 0));
		this.add(statusAsset17);
		StatusAsset statusAsset18 = new StatusAsset();
		statusAsset18.id = "had_nightmare";
		statusAsset18.locale_id = "status_title_had_nightmare";
		statusAsset18.locale_description = "status_description_had_nightmare";
		statusAsset18.duration = 90f;
		statusAsset18.path_icon = "ui/Icons/iconDreamNightmare";
		statusAsset18.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("had_nightmare", 0));
		this.add(statusAsset18);
		this.add(new StatusAsset
		{
			id = "stunned",
			render_priority = 10,
			locale_id = "status_title_stunned",
			locale_description = "status_description_stunned",
			duration = 4f,
			texture = "fx_status_stunned_t",
			animated = true,
			is_animated_in_pause = true,
			use_parent_rotation = false,
			path_icon = "ui/Icons/iconStunned"
		});
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"sleeping",
			"tantrum",
			"surprised",
			"singing",
			"laughing",
			"being_suspicious"
		});
		this.t.tier = StatusTier.Advanced;
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"tough"
		});
		this.t.opposite_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"egg"
		});
		this.t.base_stats.addTag("unconscious");
		this.add(new StatusAsset
		{
			id = "afterglow",
			locale_id = "status_title_afterglow",
			locale_description = "status_description_afterglow",
			duration = 90f,
			animated = true,
			is_animated_in_pause = true,
			use_parent_rotation = false,
			path_icon = "ui/Icons/iconAfterglow",
			offset_y = 1f
		});
		StatusAsset statusAsset19 = new StatusAsset();
		statusAsset19.id = "fell_in_love";
		statusAsset19.locale_id = "status_title_fell_in_love";
		statusAsset19.locale_description = "status_description_fell_in_love";
		statusAsset19.duration = 180f;
		statusAsset19.use_parent_rotation = false;
		statusAsset19.path_icon = "ui/Icons/iconLovers";
		statusAsset19.offset_y = 1f;
		statusAsset19.action_interval = 2f;
		statusAsset19.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("fallen_in_love", 0));
		statusAsset19.action = delegate(BaseSimObject pTarget, WorldTile _)
		{
			EffectsLibrary.spawnAt("fx_hearts", pTarget.current_position, pTarget.current_scale.y);
			return true;
		};
		this.add(statusAsset19);
		this.add(new StatusAsset
		{
			id = "pregnant",
			locale_id = "status_title_pregnant",
			locale_description = "status_description_pregnant",
			duration = 45f,
			path_icon = "ui/Icons/iconPregnant",
			animated = true,
			is_animated_in_pause = true,
			use_parent_rotation = false,
			texture = "fx_status_pregnant_t",
			action_finish = new WorldAction(this.actionPregnancyFinish)
		});
		this.t.base_stats["multiplier_speed"] = -0.2f;
		this.add(new StatusAsset
		{
			id = "pregnant_parthenogenesis",
			locale_id = "status_title_pregnant",
			locale_description = "status_description_pregnant",
			duration = 45f,
			path_icon = "ui/Icons/iconPregnant",
			animated = true,
			is_animated_in_pause = true,
			use_parent_rotation = false,
			texture = "fx_status_pregnant_t",
			action_finish = new WorldAction(this.actionPregnancyParthenogenesisFinish)
		});
		this.t.base_stats["multiplier_speed"] = -0.2f;
		this.add(new StatusAsset
		{
			id = "powerup",
			locale_id = "status_title_powerup",
			locale_description = "status_description_powerup",
			duration = 300f,
			animated = true,
			path_icon = "ui/Icons/iconPowerup"
		});
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.05f;
		this.t.base_stats["armor"] = 1f;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.t.base_stats["multiplier_crit"] = 0.5f;
		this.t.base_stats["scale"] = 0.1f;
		StatusAsset statusAsset20 = new StatusAsset();
		statusAsset20.id = "enchanted";
		statusAsset20.locale_id = "status_title_enchanted";
		statusAsset20.locale_description = "status_description_enchanted";
		statusAsset20.duration = 120f;
		statusAsset20.path_icon = "ui/Icons/iconEnchanted";
		statusAsset20.action_on_receive = ((BaseSimObject pSelf, WorldTile _) => pSelf.a.changeHappiness("just_enchanted", 0));
		this.add(statusAsset20);
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.05f;
		this.t.base_stats["multiplier_damage"] = 0.77f;
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["multiplier_diplomacy"] = 0.1f;
		this.t.base_stats["multiplier_crit"] = 0.1f;
		this.t.base_stats["lifespan"] = 10f;
		this.add(new StatusAsset
		{
			id = "slowness",
			locale_id = "status_title_slowness",
			locale_description = "status_description_slowness",
			texture = "fx_status_slowness_t",
			duration = 30f,
			animated = true,
			path_icon = "ui/Icons/iconSlowness"
		});
		this.t.base_stats["speed"] = -100f;
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"caffeinated"
		});
		this.add(new StatusAsset
		{
			id = "motivated",
			locale_id = "status_title_motivated",
			locale_description = "status_description_motivated",
			texture = "fx_status_motivated_t",
			duration = 120f,
			animated = true,
			is_animated_in_pause = true,
			path_icon = "ui/Icons/iconMotivated"
		});
		this.t.base_stats["speed"] = 10f;
		this.t.base_stats["attack_speed"] = 2f;
		this.add(new StatusAsset
		{
			id = "cough",
			locale_id = "status_title_cough",
			locale_description = "status_description_cough",
			duration = 200f,
			path_icon = "ui/Icons/iconCough",
			can_be_cured = true
		});
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"immune"
		});
		this.t.base_stats["lifespan"] = -15f;
		this.t.base_stats["multiplier_speed"] = -0.1f;
		this.t.base_stats["multiplier_health"] = -0.1f;
		this.t.tier = StatusTier.Advanced;
		this.add(new StatusAsset
		{
			id = "ash_fever",
			locale_id = "status_title_ash_fever",
			locale_description = "status_description_ash_fever",
			duration = 500f,
			path_icon = "ui/Icons/iconAshFever",
			action = new WorldAction(StatusLibrary.ashFeverEffect),
			action_interval = 10f,
			can_be_cured = true
		});
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"immune"
		});
		this.t.tier = StatusTier.Advanced;
		this.t.base_stats["diplomacy"] = -5f;
		this.t.base_stats["intelligence"] = -5f;
		this.t.base_stats["stewardship"] = -5f;
		this.t.base_stats["warfare"] = 5f;
		this.t.base_stats["multiplier_speed"] = -0.1f;
		this.t.base_stats["multiplier_health"] = -0.6f;
		this.t.base_stats["lifespan"] = -45f;
		this.add(new StatusAsset
		{
			id = "caffeinated",
			locale_id = "status_title_caffeinated",
			locale_description = "status_description_caffeinated",
			texture = "fx_status_caffeinated_t",
			duration = 60f,
			animated = true,
			is_animated_in_pause = true,
			path_icon = "ui/Icons/iconCoffee"
		});
		this.t.base_stats["intelligence"] = 222f;
		this.t.base_stats["speed"] = 200f;
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"frozen"
		});
		this.add(new StatusAsset
		{
			id = "frozen",
			locale_id = "status_title_frozen",
			locale_description = "status_description_frozen",
			texture = "fx_status_frozen_t",
			duration = 15f,
			allow_timer_reset = false,
			random_frame = true,
			sound_idle = "event:/SFX/STATUS/StatusFrozen",
			path_icon = "ui/Icons/iconFrozen"
		});
		this.t.base_stats["mass"] = 100f;
		this.t.base_stats["armor"] = -20f;
		this.t.base_stats["speed"] = -10000f;
		this.t.base_stats.addTag("immovable");
		this.t.base_stats.addTag("frozen_ai");
		this.t.base_stats.addTag("stop_idle_animation");
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"burning",
			"tantrum"
		});
		this.t.opposite_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"shield"
		});
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"freeze_proof"
		});
		this.t.opposite_tags = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"immunity_cold"
		});
		this.t.tier = StatusTier.Advanced;
		this.add(new StatusAsset
		{
			id = "shield",
			locale_id = "status_title_shield",
			locale_description = "status_description_shield",
			texture = "fx_status_shield_t",
			duration = 60f,
			animated = true,
			is_animated_in_pause = true,
			sound_idle = "event:/SFX/STATUS/StatusShield",
			path_icon = "ui/Icons/iconBubbleShield"
		});
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.1f;
		this.t.base_stats["mass"] = 100f;
		this.t.base_stats["armor"] = 90f;
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"burning"
		});
		this.t.opposite_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"frozen"
		});
		StatusAsset t2 = this.t;
		t2.action_get_hit = (GetHitAction)Delegate.Combine(t2.action_get_hit, new GetHitAction(StatusLibrary.spawnShieldHitEffect));
		this.t.tier = StatusTier.Advanced;
		this.add(new StatusAsset
		{
			id = "burning",
			locale_id = "status_title_burning",
			locale_description = "status_description_burning",
			texture = "fx_status_burning_t",
			duration = 30f,
			render_priority = 100,
			allow_timer_reset = false,
			action = new WorldAction(StatusLibrary.burningEffect),
			action_interval = 2f,
			animated = true,
			animation_speed = 0.1f,
			animation_speed_random = 0.08f,
			random_frame = true,
			random_flip = true,
			cancel_actor_job = true,
			material_id = "mat_world_object_lit",
			draw_light_area = true,
			draw_light_size = 0.2f,
			sound_idle = "event:/SFX/STATUS/StatusBurningBuilding",
			path_icon = "ui/Icons/iconFire",
			decision_id = "run_to_water_when_on_fire"
		});
		this.t.opposite_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"shield",
			"tantrum"
		});
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"fire_proof"
		});
		this.t.opposite_tags = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"immunity_fire"
		});
		this.t.remove_status = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"frozen"
		});
		this.t.tier = StatusTier.Advanced;
		this.add(new StatusAsset
		{
			id = "poisoned",
			locale_id = "status_title_poisoned",
			locale_description = "status_description_poisoned",
			duration = 90f,
			allow_timer_reset = false,
			action = new WorldAction(StatusLibrary.poisonedEffect),
			action_interval = 1f,
			path_icon = "ui/Icons/iconPoisoned"
		});
		this.t.tier = StatusTier.Advanced;
		this.t.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
		{
			"poison_immune"
		});
		this.add(new StatusAsset
		{
			id = "invincible",
			locale_id = "status_title_invincible",
			locale_description = "status_description_invincible",
			duration = 5f,
			path_icon = "ui/Icons/iconInvincible"
		});
		this.add(new StatusAsset
		{
			id = "dodge",
			locale_id = "status_title_dodge",
			locale_description = "status_description_dodge",
			duration = 1f,
			path_icon = "ui/Icons/skills/iconSkillDodge"
		});
		this.add(new StatusAsset
		{
			id = "dash",
			locale_id = "status_title_dash",
			locale_description = "status_description_dash",
			duration = 2f,
			path_icon = "ui/Icons/skills/iconSkillDash"
		});
		this.t.base_stats["multiplier_speed"] = 1.55f;
		this.add(new StatusAsset
		{
			id = "taking_roots",
			locale_id = "status_title_taking_roots",
			locale_description = "status_description_taking_roots",
			sound_idle = "event:/SFX/NATURE/TreeFall",
			duration = 120f,
			path_icon = "ui/Icons/iconTakingRoots",
			texture = "fx_status_taking_roots_t",
			animated = true,
			use_parent_rotation = false
		});
		this.t.base_stats["mass"] = 100f;
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["speed"] = -10000f;
		this.t.base_stats["multiplier_speed"] = -0.3f;
		this.t.base_stats["multiplier_damage"] = -0.3f;
		this.t.base_stats["multiplier_health"] = -0.3f;
		this.t.base_stats.addTag("immovable");
		this.t.base_stats.addTag("stop_idle_animation");
		this.t.base_stats.addTag("frozen_ai");
		this.t.tier = StatusTier.Advanced;
		StatusAsset t3 = this.t;
		t3.action_finish = (WorldAction)Delegate.Combine(t3.action_finish, new WorldAction(this.actionTakingRootsFinish));
		this.add(new StatusAsset
		{
			id = "uprooting",
			locale_id = "status_title_uprooting",
			locale_description = "status_description_uprooting",
			sound_idle = "event:/SFX/CIVILIZATIONS/CropsGrow",
			duration = 120f,
			path_icon = "ui/Icons/iconUprooting",
			texture = "fx_status_uprooting_t",
			animated = true,
			use_parent_rotation = false,
			offset_y_ui = 0.1f
		});
		this.t.base_stats["armor"] = 20f;
		this.t.base_stats["speed"] = -10000f;
		this.t.base_stats["multiplier_speed"] = -0.3f;
		this.t.base_stats["multiplier_damage"] = -0.3f;
		this.t.base_stats["multiplier_health"] = -0.3f;
		this.t.base_stats.addTag("immovable");
		this.t.base_stats.addTag("frozen_ai");
		this.t.base_stats.addTag("stop_idle_animation");
		this.t.tier = StatusTier.Advanced;
		StatusAsset statusAsset21 = new StatusAsset();
		statusAsset21.id = "budding";
		statusAsset21.locale_id = "status_title_budding";
		statusAsset21.locale_description = "status_description_budding";
		statusAsset21.duration = 60f;
		statusAsset21.path_icon = "ui/Icons/iconStatusBudding";
		statusAsset21.animated = true;
		statusAsset21.animation_speed = 0f;
		statusAsset21.scale = 0.7f;
		statusAsset21.position_z = -0.01f;
		statusAsset21.rotation_z = -30f;
		statusAsset21.use_parent_rotation = true;
		statusAsset21.get_override_sprite = delegate(BaseSimObject pActor, int pIndex)
		{
			Sprite tFrame = pActor.a.animation_container.walking.frames[0];
			return pActor.a.calculateColoredSprite(tFrame, false);
		};
		statusAsset21.get_override_sprite_position = delegate(BaseSimObject pActor, int pIndex)
		{
			AnimationFrameData tData = pActor.a.getAnimationFrameData();
			ref Vector3 tCurrentScale = ref pActor.a.current_scale;
			Vector3 tResult;
			if (tData.show_head)
			{
				ref Vector2 tPosHead = ref tData.pos_head_new;
				float tScaleMod = pActor.a.getScaleMod();
				tResult = new Vector2(-0.4f * tScaleMod + tPosHead.x * tCurrentScale.x, -0.25f * tScaleMod + tPosHead.y * tCurrentScale.y);
			}
			else
			{
				float tSize = Mathf.Max((float)pActor.a.asset.actor_size, 3f) / 13f;
				float tHeight;
				if (pActor.a.isInLiquid())
				{
					tHeight = 1f;
				}
				else
				{
					tHeight = 5f;
				}
				tResult = new Vector2(0f, tHeight * tSize * tCurrentScale.y);
			}
			return tResult;
		};
		statusAsset21.get_override_sprite_rotation_z = delegate(BaseSimObject pActor, int pIndex)
		{
			float tRotation;
			if (pActor.a.has_rendered_sprite_head)
			{
				tRotation = -30f;
			}
			else
			{
				tRotation = 0f;
			}
			return tRotation;
		};
		statusAsset21.get_override_sprite_ui = delegate(AvatarEffect pEffect, int pIndex)
		{
			UnitAvatarLoader avatar = pEffect.getAvatar();
			ActorAvatarData tData = avatar.getData();
			AnimationContainerUnit tContainer = avatar.getAnimationContainer();
			Sprite tSprite = tContainer.walking.frames[0];
			return tData.getColoredSprite(tSprite, tContainer);
		};
		statusAsset21.get_override_sprite_position_ui = delegate(AvatarEffect pEffect, int pIndex)
		{
			UnitAvatarLoader tAvatar = pEffect.getAvatar();
			ActorAvatarData tAvatarData = tAvatar.getData();
			Vector2 tPos = default(Vector2);
			int tIndex = tAvatar.getActualSpriteIndex();
			if (tAvatarData.hasRenderedSpriteHead())
			{
				AnimationContainerUnit tContainer = tAvatar.getAnimationContainer();
				string tState = (tAvatarData.is_touching_liquid && tContainer.has_swimming) ? tContainer.swimming.frames[tIndex].name : tContainer.walking.frames[tIndex].name;
				ref Vector2 tPosHead = ref tContainer.dict_frame_data[tState].pos_head;
				ref float tScale = ref pEffect.getAsset().scale;
				tPos = new Vector2(-0.4f + tPosHead.x * tScale, -0.25f + tPosHead.y * tScale);
			}
			else
			{
				float tSize = Mathf.Max((float)tAvatarData.asset.actor_size, 3f) / 13f;
				float tHeight;
				if (tAvatarData.is_touching_liquid)
				{
					tHeight = 1f;
				}
				else
				{
					tHeight = 5f;
				}
				tPos = new Vector2(0f, tHeight * tSize);
			}
			return tPos;
		};
		statusAsset21.get_override_sprite_rotation_z_ui = delegate(AvatarEffect pEffect, int pIndex)
		{
			float tRotation;
			if (pEffect.getAvatar().getData().hasRenderedSpriteHead())
			{
				tRotation = -30f;
			}
			else
			{
				tRotation = 0f;
			}
			return tRotation;
		};
		statusAsset21.get_sprites_count = ((BaseSimObject _, StatusAsset _) => 1);
		this.add(statusAsset21);
		this.t.render_check = ((ActorAsset pAsset) => pAsset.render_budding);
		StatusAsset t4 = this.t;
		t4.action_finish = (WorldAction)Delegate.Combine(t4.action_finish, new WorldAction(this.actionBuddingFinish));
		StatusAsset statusAsset22 = new StatusAsset();
		statusAsset22.id = "flicked";
		statusAsset22.locale_id = "status_title_flicked";
		statusAsset22.locale_description = "status_description_flicked";
		statusAsset22.duration = 3f;
		statusAsset22.path_icon = "ui/Icons/iconFingerFlick";
		statusAsset22.action_death = delegate(BaseSimObject pActor, WorldTile pTile)
		{
			if (pActor.a.getActorAsset().id != "beetle")
			{
				return false;
			}
			if (!pActor.current_tile.Type.lava)
			{
				return false;
			}
			AchievementLibrary.flick_it.check(null);
			return true;
		};
		this.add(statusAsset22);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x000A4877 File Offset: 0x000A2A77
	public override void linkAssets()
	{
		base.linkAssets();
		this.setupBools();
		this.setupCachedSprites();
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x000A488C File Offset: 0x000A2A8C
	private void setupCachedSprites()
	{
		foreach (StatusAsset tAsset in this.list)
		{
			if (tAsset.texture != null && tAsset.sprite_list == null)
			{
				tAsset.sprite_list = SpriteTextureLoader.getSpriteList("effects/" + tAsset.texture, false);
			}
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x000A4904 File Offset: 0x000A2B04
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		foreach (StatusAsset tAsset in this.list)
		{
			if (!string.IsNullOrEmpty(tAsset.texture))
			{
				base.checkSpriteExists("texture", "effects/" + tAsset.texture, tAsset);
			}
			base.checkSpriteExists("path_icon", tAsset.path_icon, tAsset);
		}
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x000A4994 File Offset: 0x000A2B94
	public override void editorDiagnosticLocales()
	{
		foreach (StatusAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x000A4A00 File Offset: 0x000A2C00
	private void setupBools()
	{
		foreach (StatusAsset tAsset in this.list)
		{
			if (tAsset.get_override_sprite != null)
			{
				tAsset.has_override_sprite = true;
				tAsset.need_visual_render = true;
			}
			if (tAsset.get_override_sprite_position != null)
			{
				tAsset.has_override_sprite_position = true;
			}
			if (tAsset.get_override_sprite_rotation_z != null)
			{
				tAsset.has_override_sprite_rotation_z = true;
			}
			if (tAsset.texture != null)
			{
				tAsset.need_visual_render = true;
			}
		}
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x000A4A90 File Offset: 0x000A2C90
	private bool actionPregnancyFinish(BaseSimObject pTarget, WorldTile pTile)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		BabyMaker.makeBabyFromPregnancy(pTarget.a);
		return true;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x000A4AA8 File Offset: 0x000A2CA8
	private bool actionPregnancyParthenogenesisFinish(BaseSimObject pTarget, WorldTile pTile)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		BabyMaker.makeBabyViaParthenogenesis(pTarget.a);
		return true;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x000A4AC0 File Offset: 0x000A2CC0
	private bool actionTakingRootsFinish(BaseSimObject pTarget, WorldTile pTile)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		BabyMaker.makeBabyViaVegetative(pTarget.a);
		return true;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x000A4AD9 File Offset: 0x000A2CD9
	private bool actionBuddingFinish(BaseSimObject pTarget, WorldTile pTile)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		Actor a = pTarget.a;
		BabyMaker.makeBabyViaBudding(pTarget as Actor).applyRandomForce(1.5f, 2f);
		a.applyRandomForce(1.5f, 2f);
		return true;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x000A4B18 File Offset: 0x000A2D18
	public static bool ashFeverEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		int tDamage = pTarget.getMaxHealthPercent(0.01f);
		pTarget.getHit((float)tDamage, true, AttackType.AshFever, null, true, false, true);
		return true;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x000A4B40 File Offset: 0x000A2D40
	public static bool burningEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isActor() && pTarget.a.asset.has_skin && Randy.randomBool())
		{
			pTarget.a.addInjuryTrait("skin_burns");
		}
		int tDamage = pTarget.getMaxHealthPercent(0.1f);
		if (pTarget.isBuilding() && pTarget.b.isRuin())
		{
			tDamage = (int)((float)tDamage * 0.25f + 1f);
		}
		pTarget.getHit((float)tDamage, true, AttackType.Fire, null, true, false, true);
		if (MapBox.isRenderGameplay() && Randy.randomChance(0.1f))
		{
			World.world.particles_fire.spawn(pTarget.current_position.x, pTarget.current_position.y, true);
		}
		return true;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x000A4BF8 File Offset: 0x000A2DF8
	public static bool spawnShieldHitEffect(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
	{
		if (!pSelf.isAlive())
		{
			return false;
		}
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_shield_hit", pSelf.current_position, 1f);
		if (tEffect == null)
		{
			return false;
		}
		tEffect.attachTo(pSelf.a);
		return true;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x000A4C40 File Offset: 0x000A2E40
	public static bool poisonedEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		float tDamage = Mathf.Max((float)pTarget.getHealth() * 0.01f, 1f);
		if (Randy.randomBool() && pTarget.getHealth() > 1)
		{
			pTarget.getHit(tDamage, true, AttackType.Poison, null, true, false, true);
		}
		pTarget.a.spawnParticle(Toolbox.color_infected);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		return true;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000A4CAC File Offset: 0x000A2EAC
	public void linkMaterials()
	{
		foreach (StatusAsset tAsset in this.list)
		{
			Material tMaterial = LibraryMaterials.instance.dict[tAsset.material_id];
			tAsset.material = tMaterial;
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000A4D18 File Offset: 0x000A2F18
	public string addToGameplayReport(string pWhat)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhat + "\n";
		foreach (StatusAsset statusAsset in this.list)
		{
			string tName = statusAsset.getLocaleID().Localize();
			string tDescription = statusAsset.getDescriptionID().Localize();
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

	// Token: 0x06000B66 RID: 2918 RVA: 0x000A4DD8 File Offset: 0x000A2FD8
	private bool possessedAction(BaseSimObject pTarget, WorldTile _)
	{
		Actor tActor = pTarget.a;
		if (tActor.asset.id == "crabzilla")
		{
			return false;
		}
		if (tActor.is_unconscious)
		{
			return false;
		}
		this.checkPossessedMovement(tActor);
		this.checkPossessedAttackLeft(tActor);
		this.checkPossessedAttackRight(tActor);
		this.checkPossessedFlip(tActor);
		this.checkBonusActions(tActor);
		return true;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000A4E34 File Offset: 0x000A3034
	private void checkBonusActions(Actor pActor)
	{
		if (pActor.is_immovable)
		{
			return;
		}
		if (ControllableUnit.isActionPressedJump())
		{
			this.checkJump(pActor);
		}
		if (ControllableUnit.isActionPressedTalk())
		{
			this.checkTalk(pActor);
		}
		if (ControllableUnit.isActionPressedDash())
		{
			this.checkDash(pActor);
		}
		if (ControllableUnit.isActionPressedBackstep())
		{
			this.checkBackstep(pActor);
		}
		if (ControllableUnit.isActionPressedSteal())
		{
			this.checkSteal(pActor);
		}
		if (ControllableUnit.isActionPressedSwear())
		{
			this.checkSwear(pActor);
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000A4EA0 File Offset: 0x000A30A0
	private void checkTalk(Actor pActor)
	{
		if (pActor.under_forces)
		{
			return;
		}
		if (!pActor.asset.control_can_talk)
		{
			return;
		}
		if (pActor.hasTrait("mute"))
		{
			return;
		}
		if (!pActor.isAttackReady())
		{
			return;
		}
		Vector2 tPosition = pActor.getPossessionControlTargetPosition();
		pActor.spawnSlashTalk(tPosition);
		pActor.punchTargetAnimation(tPosition, false, false, -20f);
		pActor.lookTowardsPosition(tPosition);
		pActor.setActionTimeout(1f);
		Actor tActorTarget = this.getActorTargetRaycast(pActor);
		if (tActorTarget == null)
		{
			tActorTarget = this.getActorTargetNearCursor(pActor);
		}
		if (tActorTarget == null)
		{
			return;
		}
		if (tActorTarget.is_unconscious)
		{
			return;
		}
		if (!tActorTarget.asset.can_talk_with)
		{
			return;
		}
		if (tActorTarget.hasStatus("possessed"))
		{
			return;
		}
		if (ControllableUnit.isControllingUnit(tActorTarget))
		{
			return;
		}
		if (Randy.randomChance(0.3f))
		{
			tActorTarget.getSurprised(pActor.current_tile, true);
		}
		tActorTarget.cancelAllBeh();
		tActorTarget.tryToConvertActorToMetaFromActor(pActor, false);
		tActorTarget.addStatusEffect("possessed_follower", 0f, true);
		tActorTarget.lookTowardsPosition(pActor.current_position);
		tActorTarget.setTask("socialize_receiving", true, false, true);
		pActor.clearLastTopicSprite();
		tActorTarget.clearLastTopicSprite();
		float tTalkTime = 2f + Randy.randomFloat(0f, 3f);
		tActorTarget.makeWait(tTalkTime);
		pActor.setTask("socialize_receiving", true, false, true);
		pActor.makeWait(tTalkTime);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x000A4FE4 File Offset: 0x000A31E4
	private void checkSteal(Actor pActor)
	{
		if (pActor.under_forces)
		{
			return;
		}
		if (!pActor.asset.control_can_steal)
		{
			return;
		}
		if (!pActor.isAttackReady())
		{
			return;
		}
		Vector2 tPosition = pActor.getPossessionControlTargetPosition();
		pActor.spawnSlashSteal(tPosition);
		pActor.punchTargetAnimation(tPosition, false, false, 40f);
		pActor.lookTowardsPosition(tPosition);
		pActor.setActionTimeout(1f);
		Actor tActorTarget = this.getActorTargetRaycast(pActor);
		if (tActorTarget == null)
		{
			return;
		}
		if (Vector2.Distance(pActor.current_position, tActorTarget.current_position) < 2f)
		{
			pActor.stealActionFrom(tActorTarget, 5f, 1f, true, true);
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x000A507C File Offset: 0x000A327C
	private void checkSwear(Actor pActor)
	{
		if (!pActor.asset.control_can_swear)
		{
			return;
		}
		if (pActor.hasTrait("mute"))
		{
			return;
		}
		if (!pActor.isAttackReady())
		{
			return;
		}
		Vector2 tPosition = pActor.getPossessionControlTargetPosition();
		pActor.addStatusEffect("swearing", 2f, false);
		pActor.punchTargetAnimation(tPosition, false, false, -40f);
		pActor.spawnSlashYell(tPosition);
		pActor.lookTowardsPosition(tPosition);
		pActor.setActionTimeout(1f);
		Actor tActorTarget = this.getActorTargetRaycast(pActor);
		if (tActorTarget == null)
		{
			tActorTarget = this.getActorTargetNearCursor(pActor);
		}
		if (tActorTarget == null)
		{
			return;
		}
		tActorTarget.tryToGetSurprised(pActor.current_tile, true);
		tActorTarget.addAggro(pActor);
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x000A5120 File Offset: 0x000A3320
	private void checkBackstep(Actor pActor)
	{
		if (!pActor.asset.control_can_backstep)
		{
			return;
		}
		CombatActionAsset combat_action_backstep = CombatActionLibrary.combat_action_backstep;
		Vector2 tPosition = pActor.getPossessionControlTargetPositionMovementVector();
		combat_action_backstep.action_actor_target_position(pActor, tPosition, null);
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000A5158 File Offset: 0x000A3358
	private void checkDash(Actor pActor)
	{
		if (!pActor.asset.control_can_dash)
		{
			return;
		}
		CombatActionAsset combat_action_dash = CombatActionLibrary.combat_action_dash;
		Vector2 tPosition = pActor.getPossessionControlTargetPositionMovementVector();
		combat_action_dash.action_actor_target_position(pActor, tPosition, null);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x000A5190 File Offset: 0x000A3390
	private void checkJump(Actor pActor)
	{
		if (pActor.under_forces)
		{
			return;
		}
		if (!pActor.asset.control_can_jump)
		{
			return;
		}
		float num = SimGlobals.m.gravity * 0.5f;
		float tMassUnit = pActor.stats["mass_2"] * pActor.actor_scale;
		float tForceAmount = num / tMassUnit;
		tForceAmount = Mathf.Clamp(tForceAmount, 1.5f, 2.5f);
		if (pActor.hasTrait("weightless"))
		{
			tForceAmount += tForceAmount * 0.5f;
		}
		float tForceHeight = tForceAmount;
		Vector2 tTargetPosition = pActor.current_position - ControllableUnit.getMovementVector();
		Vector2 tCurPosition = pActor.current_position;
		EffectsLibrary.spawnAt("fx_jump", tCurPosition, 0.1f);
		pActor.calculateForce(tCurPosition.x, tCurPosition.y, tTargetPosition.x, tTargetPosition.y, tForceAmount, tForceHeight, true);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000A5258 File Offset: 0x000A3458
	private void checkPossessedAttackLeft(Actor pActor)
	{
		if (!ControllableUnit.isAttackPressedLeft())
		{
			return;
		}
		if (Config.joyControls && !TouchPossessionController.isSelectedActionAttack())
		{
			return;
		}
		if (pActor.asset.skip_fight_logic)
		{
			return;
		}
		if (!pActor.isAttackReady())
		{
			return;
		}
		Actor tActorTarget = this.getActorTargetAttack(pActor, 0f);
		float tAttackRange = pActor.getAttackRange();
		Vector3 tAttackPos = this.getAttackPos(pActor, 0f);
		if (tActorTarget != null && pActor.hasMeleeAttack())
		{
			Vector3 tTargetPos = tActorTarget.current_position;
			float tDistToActor = Vector2.Distance(pActor.current_position, tTargetPos);
			if (tDistToActor > tAttackRange)
			{
				tDistToActor = tAttackRange;
			}
			tAttackPos = Toolbox.getNewPointVec2(pActor.current_position, tAttackPos, tDistToActor, true);
		}
		Kingdom tKingdomPossessed = World.world.kingdoms_wild.get("possessed");
		pActor.tryToAttack(null, false, null, tAttackPos, tKingdomPossessed, (tActorTarget != null) ? tActorTarget.current_tile : null, tAttackRange * 0.2f);
		pActor.setPossessionAttackHappened();
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x000A5344 File Offset: 0x000A3544
	private void checkPossessedAttackRight(Actor pActor)
	{
		if (!ControllableUnit.isAttackJustPressedRight())
		{
			return;
		}
		if (!pActor.asset.control_can_kick)
		{
			return;
		}
		if (pActor.asset.skip_fight_logic)
		{
			return;
		}
		if (!pActor.isAttackReady())
		{
			return;
		}
		Vector2 tPosition = pActor.getPossessionControlTargetPosition();
		pActor.punchTargetAnimation(tPosition, false, false, -20f);
		pActor.setActionTimeout(1f);
		pActor.spawnSlashKick(tPosition);
		pActor.lookTowardsPosition(tPosition);
		pActor.setPossessionAttackHappened();
		Actor tActorToHit = this.getActorTargetAttack(pActor, 2f);
		if (tActorToHit == null)
		{
			return;
		}
		if (Vector2.Distance(pActor.current_position, tActorToHit.current_position) < 2f)
		{
			tActorToHit.makeStunned(5f);
			Vector2 tStartPos = pActor.current_position;
			tStartPos.y += pActor.current_scale.y;
			Vector2 tDirection = tPosition;
			float tForceDirection = Randy.randomFloat(1.5f, 3f);
			float tForceHeight = Randy.randomFloat(1.5f, 3f);
			if (pActor.under_forces || pActor.hasStatus("dash"))
			{
				tForceDirection *= 2f;
				tForceHeight *= 1.5f;
			}
			tActorToHit.calculateForce(tDirection.x, tDirection.y, tStartPos.x, tStartPos.y, tForceDirection, tForceHeight, true);
			tActorToHit.addAggro(pActor);
		}
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x000A5484 File Offset: 0x000A3684
	private Vector2 getAttackPos(Actor pActorFor, float pRange = 0f)
	{
		Vector2 tActorPos = pActorFor.current_position;
		Vector2 tMousePosTarget = this.mouse_pos;
		Vector2 tAttackPos = tMousePosTarget;
		float tRangeAttack = (pRange != 0f) ? pRange : pActorFor.getAttackRange();
		if (pActorFor.hasMeleeAttack())
		{
			tAttackPos = Toolbox.getNewPointVec2(tActorPos, tMousePosTarget, tRangeAttack, true);
		}
		return tAttackPos;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x000A54C8 File Offset: 0x000A36C8
	private Actor getActorTargetAttack(Actor pActorFor, float pRange = 0f)
	{
		float tRangeAttack = (pRange != 0f) ? pRange : pActorFor.getAttackRange();
		Vector2 tAttackPos = this.getAttackPos(pActorFor, tRangeAttack);
		WorldTile tTileTarget = World.world.GetTile((int)tAttackPos.x, (int)tAttackPos.y);
		if (tTileTarget == null)
		{
			tTileTarget = pActorFor.current_tile;
		}
		float tBestDist = float.MaxValue;
		Actor tBestObject = null;
		float tRangeAttackSquared = tRangeAttack * tRangeAttack;
		foreach (Actor tActor in Finder.getUnitsFromChunk(tTileTarget, 0, tRangeAttack, true))
		{
			if (pActorFor != tActor)
			{
				float tDist = Toolbox.SquaredDistVec2Float(tAttackPos, tActor.current_position);
				if (tDist <= tRangeAttackSquared && tDist < tBestDist)
				{
					tBestObject = tActor;
					tBestDist = tDist;
				}
			}
		}
		return tBestObject;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000A558C File Offset: 0x000A378C
	private Actor getActorTargetRaycast(Actor pActorFor)
	{
		Vector2 tActorPos = pActorFor.current_position;
		Vector2 tMousePosTarget = this.mouse_pos;
		List<WorldTile> list = PathfinderTools.raycast(tActorPos, tMousePosTarget, 0.99f);
		float tDistance = float.MaxValue;
		Actor tActorTarget = null;
		Action<Actor> <>9__0;
		foreach (WorldTile tTile in list)
		{
			if (tTile.hasUnits())
			{
				WorldTile worldTile = tTile;
				Action<Actor> pAction;
				if ((pAction = <>9__0) == null)
				{
					pAction = (<>9__0 = delegate(Actor tActor)
					{
						if (tActor == pActorFor)
						{
							return;
						}
						float tDist = Toolbox.SquaredDistVec2Float(tActorPos, tActor.current_position);
						if (tDist < tDistance)
						{
							tDistance = tDist;
							tActorTarget = tActor;
						}
					});
				}
				worldTile.doUnits(pAction);
				if (tActorTarget != null)
				{
					break;
				}
			}
		}
		if (tActorTarget == null || tActorTarget == pActorFor)
		{
			return null;
		}
		return tActorTarget;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x000A5674 File Offset: 0x000A3874
	private Actor getActorTargetNearCursor(Actor pActorFor)
	{
		Actor tActorToHit = World.world.getActorNearCursor();
		if (tActorToHit == null || tActorToHit == pActorFor)
		{
			return null;
		}
		return tActorToHit;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x000A5698 File Offset: 0x000A3898
	private void checkPossessedMovement(Actor pActor)
	{
		if (!ControllableUnit.isMovementActionActive())
		{
			pActor.setPossessedMovement(false);
			return;
		}
		if (pActor.is_immovable)
		{
			return;
		}
		Vector2 tMovementVector = ControllableUnit.getMovementVector();
		if (tMovementVector == Vector2.zero)
		{
			return;
		}
		pActor.setPossessedMovement(true);
		Vector2 tUnitPos = pActor.current_position;
		pActor.next_step_position_possession = tUnitPos + tMovementVector * 2f;
		tMovementVector += tUnitPos;
		float tSpeedDelta = pActor.updatePossessedMovementTowards(World.world.elapsed, tMovementVector);
		if (!pActor.isInAir() && !pActor.under_forces && tSpeedDelta >= 0.03f)
		{
			BaseEffect tEffect = EffectsLibrary.spawnAt("fx_walk", tUnitPos, 0.1f);
			if (tEffect != null)
			{
				tEffect.GetComponent<SpriteRenderer>().flipX = !pActor.flip;
			}
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000A5756 File Offset: 0x000A3956
	private void checkPossessedFlip(Actor pActor)
	{
		pActor.updateMovementPossessedFlip();
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000A575E File Offset: 0x000A395E
	public Vector2 mouse_pos
	{
		get
		{
			return ControllableUnit.getClickVector();
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000A5765 File Offset: 0x000A3965
	public StatusLibrary()
	{
		string[] array = new string[23];
		array[0] = "had_bad_dream";
		array[1] = "had_good_dream";
		array[2] = "had_nightmare";
		this._pot_dreams = array;
		base..ctor();
	}

	// Token: 0x04000B17 RID: 2839
	public const float DURATION_STRANGE_URGE = 100f;

	// Token: 0x04000B18 RID: 2840
	private string[] _pot_dreams;
}
