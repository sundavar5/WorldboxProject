using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class GodFinger : BaseActorComponent
{
	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06002117 RID: 8471 RVA: 0x0011A7B7 File Offset: 0x001189B7
	internal bool is_drawing
	{
		get
		{
			if (this.actor.ai.hasTask())
			{
				BehFinger behFinger = this.actor.ai.action as BehFinger;
				return behFinger != null && behFinger.drawing_action;
			}
			return false;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06002118 RID: 8472 RVA: 0x0011A7ED File Offset: 0x001189ED
	internal bool drawing_over_water
	{
		get
		{
			return this.finger_target == FingerTarget.Water;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06002119 RID: 8473 RVA: 0x0011A7F8 File Offset: 0x001189F8
	internal bool drawing_over_ground
	{
		get
		{
			return this.finger_target == FingerTarget.Ground;
		}
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x0011A804 File Offset: 0x00118A04
	internal override void create(Actor pActor)
	{
		base.create(pActor);
		this.debug_color = GodFinger._random_colors.GetRandom<Color>();
		base.gameObject.name = "GF " + this.actor.getID().ToString();
		this.fingerTip = base.transform.Find("Tip").gameObject.GetComponent<SpriteAnimation>();
		this.fingerTip.gameObject.SetActive(false);
		this.actor.target_angle = Vector3.zero;
		this.actor.setFlying(true);
		this.actor.position_height = 8f;
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x0011A8AD File Offset: 0x00118AAD
	internal void lightAction()
	{
		AchievementLibrary.god_finger_lightning.check(null);
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x0011A8BC File Offset: 0x00118ABC
	public override void update(float pElapsed)
	{
		if (!this.actor.isAlive())
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		bool tIsDrawing = this.is_drawing;
		bool tGonnaDraw = !tIsDrawing && this.flying_target < 2f;
		if (tIsDrawing)
		{
			this.actor.target_angle.z = Mathf.Clamp(this.actor.target_angle.z, 25f, 35f);
			if (this.actor.target_angle.z < this._rotate_wiggle)
			{
				Actor actor = this.actor;
				actor.target_angle.z = actor.target_angle.z + 100f * pElapsed;
			}
			else if (this.actor.target_angle.z > this._rotate_wiggle)
			{
				Actor actor2 = this.actor;
				actor2.target_angle.z = actor2.target_angle.z - 100f * pElapsed;
			}
			else
			{
				this._rotate_wiggle = (float)Randy.randomInt(25, 35);
			}
			this.actor.rotation_cooldown = 300f;
		}
		else if (tGonnaDraw)
		{
			if (this.actor.target_angle.z < 30f)
			{
				Actor actor3 = this.actor;
				actor3.target_angle.z = actor3.target_angle.z + 100f * pElapsed;
			}
			this.actor.rotation_cooldown = 300f;
		}
		else
		{
			this.actor.rotation_cooldown = 0f;
		}
		if (this.flying_target != this.actor.position_height)
		{
			this.actor.position_height = Mathf.MoveTowards(this.actor.position_height, this.flying_target, pElapsed * 8f);
		}
		this.fingerTip.gameObject.SetActive(tIsDrawing);
		if (tIsDrawing)
		{
			this.fingerTip.update(pElapsed);
			if (!this.isInMapBounds(this.actor.current_position))
			{
				return;
			}
			this.drawOnTile(this.actor.current_tile);
		}
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x0011AA9C File Offset: 0x00118C9C
	private bool isInMapBounds(Vector3 pPos)
	{
		return pPos.x > 0f && pPos.y > 0f && pPos.x < (float)MapBox.width && pPos.y < (float)MapBox.height;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x0011AAD8 File Offset: 0x00118CD8
	public void drawOnTile(WorldTile pTile)
	{
		World.world.conway_layer.checkKillRange(pTile.pos, 2);
		string current_brush = Config.current_brush;
		Config.current_brush = this.brush;
		if (this.god_power.click_power_action != null || this.god_power.click_power_brush_action != null)
		{
			if (this.god_power.click_power_brush_action != null)
			{
				this.god_power.click_power_brush_action(pTile, this.god_power);
			}
			else if (this.god_power.click_power_action != null)
			{
				this.god_power.click_power_action(pTile, this.god_power);
			}
		}
		if (this.god_power.click_action != null || this.god_power.click_brush_action != null)
		{
			if (this.god_power.click_brush_action != null)
			{
				this.god_power.click_brush_action(pTile, this.god_power.id);
			}
			else if (this.god_power.click_action != null)
			{
				this.god_power.click_action(pTile, this.god_power.id);
			}
		}
		World.world.loopWithBrush(pTile, Config.current_brush_data, new PowerActionWithID(this.clearTargets), "god_finger");
		World.world.loopWithBrush(pTile, Brush.get(2, "circ_"), new PowerActionWithID(this.fingerTile), "god_finger");
		Config.current_brush = current_brush;
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x0011AC2E File Offset: 0x00118E2E
	public bool clearTargets(WorldTile pTile, string pPowerID)
	{
		this.target_tiles.Remove(pTile);
		return true;
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x0011AC3E File Offset: 0x00118E3E
	public bool fingerTile(WorldTile pTile, string pPowerID)
	{
		pTile.doUnits(delegate(Actor pActor)
		{
			if (pActor.asset.flag_finger)
			{
				return;
			}
			if (!pActor.asset.can_be_killed_by_stuff)
			{
				return;
			}
			pActor.getHitFullHealth(AttackType.Gravity);
		});
		return true;
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x0011AC66 File Offset: 0x00118E66
	public override void Dispose()
	{
		this.target_tiles.Clear();
		this.finger_target = FingerTarget.None;
		base.Dispose();
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x0011AC80 File Offset: 0x00118E80
	internal static bool deathFlip(BaseSimObject pTarget, WorldTile pTile, float pElapsed)
	{
		Actor tActor = pTarget.a;
		if (tActor.isFalling())
		{
			tActor.updateFall();
			return true;
		}
		if (tActor.target_angle.z < 90f)
		{
			tActor.target_angle.z = Mathf.Lerp(tActor.target_angle.z, 90f, pElapsed * 4f);
			if (tActor.target_angle.z > 90f)
			{
				tActor.target_angle.z = 90f;
			}
			if (!tActor.is_visible)
			{
				tActor.updateRotation();
			}
			if (Mathf.Abs(tActor.current_rotation.z) >= 89f)
			{
				tActor.dieAndDestroy(AttackType.None);
				return true;
			}
		}
		tActor.updateDeadBlackAnimation(pElapsed);
		return true;
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x0011AD38 File Offset: 0x00118F38
	public static void debug_trail(GodFinger pFinger)
	{
		if (!DebugConfig.isOn(DebugOption.ShowGodFingerTargetting))
		{
			return;
		}
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		if (!pFinger.actor.is_visible)
		{
			return;
		}
		AiSystemActor tAI = pFinger.actor.ai;
		if (!tAI.hasTask())
		{
			return;
		}
		Color tColor = Toolbox.color_white;
		string id = tAI.task.id;
		if (!(id == "godfinger_move"))
		{
			if (!(id == "godfinger_find_target"))
			{
				if (!(id == "godfinger_random_fun_move"))
				{
					if (!(id == "godfinger_circle_move"))
					{
						if (!(id == "godfinger_circle_move_big"))
						{
							if (!(id == "godfinger_circle_move_small"))
							{
								return;
							}
							tColor = Toolbox.color_fire;
						}
						else
						{
							tColor = Toolbox.color_yellow;
						}
					}
					else
					{
						tColor = Toolbox.color_purple;
					}
				}
				else
				{
					tColor = Toolbox.color_green;
				}
			}
			else
			{
				tColor = Toolbox.color_red;
			}
		}
		else
		{
			tColor = Toolbox.color_blue;
		}
		BaseEffect tEffects = EffectsLibrary.spawn("fx_weapon_particle", null, null, null, 0f, pFinger.fingerTip.transform.position.x, pFinger.fingerTip.transform.position.y, null);
		if (tEffects != null)
		{
			((StatusParticle)tEffects).spawnParticle(tEffects.transform.position, tColor, 0.25f);
		}
	}

	// Token: 0x04001882 RID: 6274
	public const float FLYING_SPEED = 8f;

	// Token: 0x04001883 RID: 6275
	public const int MAX_TARGET_TILES = 1200;

	// Token: 0x04001884 RID: 6276
	internal GodPower god_power;

	// Token: 0x04001885 RID: 6277
	internal string brush;

	// Token: 0x04001886 RID: 6278
	internal float flying_target = 8f;

	// Token: 0x04001887 RID: 6279
	private float _rotate_wiggle = 30f;

	// Token: 0x04001888 RID: 6280
	internal static string[] power_over_water = Toolbox.splitStringIntoArray(new string[]
	{
		"tile_high_soil#10",
		"tile_soil#10",
		"tile_hills",
		"tile_mountains",
		"tile_summit",
		"shovel_plus"
	});

	// Token: 0x04001889 RID: 6281
	internal static string[] power_over_ground = Toolbox.splitStringIntoArray(new string[]
	{
		"seeds_candy",
		"seeds_corrupted",
		"seeds_crystal",
		"seeds_desert",
		"seeds_enchanted",
		"seeds_grass",
		"seeds_infernal",
		"seeds_jungle",
		"seeds_lemon",
		"seeds_mushroom",
		"seeds_permafrost",
		"seeds_savanna",
		"seeds_swamp",
		"seeds_birch",
		"seeds_maple",
		"seeds_flower",
		"seeds_garlic",
		"seeds_rocklands",
		"seeds_celestial",
		"seeds_singularity",
		"seeds_clover",
		"seeds_paradox",
		"fertilizer_plants#4",
		"fertilizer_trees#4"
	});

	// Token: 0x0400188A RID: 6282
	internal HashSet<WorldTile> target_tiles = new HashSet<WorldTile>(1800);

	// Token: 0x0400188B RID: 6283
	internal FingerTarget finger_target;

	// Token: 0x0400188C RID: 6284
	private SpriteAnimation fingerTip;

	// Token: 0x0400188D RID: 6285
	internal Color debug_color;

	// Token: 0x0400188E RID: 6286
	private static Color[] _random_colors = new Color[]
	{
		Toolbox.color_green,
		Toolbox.color_red,
		Toolbox.color_blue,
		Toolbox.color_yellow,
		Toolbox.color_purple,
		Toolbox.color_fire,
		Toolbox.color_phenotype_green_0,
		Toolbox.color_phenotype_green_1,
		Toolbox.color_phenotype_green_2,
		Toolbox.color_phenotype_green_3,
		Toolbox.color_magenta_0,
		Toolbox.color_magenta_1,
		Toolbox.color_magenta_2,
		Toolbox.color_magenta_3,
		Toolbox.color_magenta_4,
		Toolbox.color_teal_0,
		Toolbox.color_teal_1,
		Toolbox.color_teal_2,
		Toolbox.color_teal_3,
		Toolbox.color_teal_4
	};
}
