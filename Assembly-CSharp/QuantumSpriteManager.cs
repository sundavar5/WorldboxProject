using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public static class QuantumSpriteManager
{
	// Token: 0x06001C2E RID: 7214 RVA: 0x001006AC File Offset: 0x000FE8AC
	public static void update()
	{
		if (!QuantumSpriteManager._initiated)
		{
			QuantumSpriteManager._initiated = true;
			QuantumSpriteManager.createSystems();
		}
		Bench.bench("quantum_sprites_scale", "game_total", false);
		QuantumSpriteManager.updateScaleEffect();
		Bench.benchEnd("quantum_sprites_scale", "game_total", false, 0L, false);
		Bench.bench("quantum_sprites", "game_total", false);
		QuantumSpriteManager.updateArrowEffect();
		QuantumSpriteManager.updateEnemyEffect();
		QuantumSpriteManager.updateSystems();
		Bench.benchEnd("quantum_sprites", "game_total", false, 0L, false);
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x0010072C File Offset: 0x000FE92C
	public static void hideAll()
	{
		foreach (QuantumSpriteAsset quantumSpriteAsset in AssetManager.quantum_sprites.list)
		{
			QuantumSpriteGroupSystem group_system = quantumSpriteAsset.group_system;
			if (group_system != null)
			{
				group_system.clearFull();
			}
		}
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x0010078C File Offset: 0x000FE98C
	private static void createSystems()
	{
		foreach (QuantumSpriteAsset tAsset in AssetManager.quantum_sprites.list)
		{
			QuantumSpriteGroupSystem tGroup = new GameObject().AddComponent<QuantumSpriteGroupSystem>();
			tGroup.create(tAsset);
			tAsset.group_system = tGroup;
			tAsset.group_system.turn_off_renderer = tAsset.turn_off_renderer;
			if (Config.preload_quantum_sprites && tAsset.default_amount != 0)
			{
				for (int i = 0; i < tAsset.default_amount; i++)
				{
					tGroup.getNext();
				}
				tGroup.clearFull();
			}
		}
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x00100834 File Offset: 0x000FEA34
	private static void updateSystems()
	{
		bool tLowRes = World.world.quality_changer.isLowRes();
		QuantumSpriteAsset[] tArray = AssetManager.quantum_sprites.getArray();
		int tLen = tArray.Length;
		for (int i = 0; i < tLen; i++)
		{
			QuantumSpriteAsset tAsset = tArray[i];
			Bench.bench(tAsset.id, "quantum_sprites", false);
			tAsset.group_system.prepare();
			if ((tLowRes && !tAsset.render_map) || (!tLowRes && !tAsset.render_gameplay))
			{
				tAsset.group_system.update(0f);
			}
			else
			{
				if (tAsset.debug_option != DebugOption.Nothing)
				{
					if (DebugConfig.isOn(tAsset.debug_option))
					{
						tAsset.draw_call(tAsset);
					}
				}
				else
				{
					tAsset.draw_call(tAsset);
				}
				tAsset.group_system.update(0f);
			}
			Bench.benchEnd(tAsset.id, "quantum_sprites", true, (long)tAsset.group_system.count_active_debug, false);
		}
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x00100928 File Offset: 0x000FEB28
	private static void updateEnemyEffect()
	{
		if (QuantumSpriteManager._enemy_anim_timer > 0f)
		{
			QuantumSpriteManager._enemy_anim_timer -= Time.deltaTime;
			return;
		}
		QuantumSpriteManager._enemy_anim_timer = 0.02f;
		if (QuantumSpriteManager._enemy_anim_timer_positive)
		{
			QuantumSpriteManager.highlight_animation += 1f;
			if (QuantumSpriteManager.highlight_animation > 10f)
			{
				QuantumSpriteManager._enemy_anim_timer_positive = !QuantumSpriteManager._enemy_anim_timer_positive;
				return;
			}
		}
		else
		{
			QuantumSpriteManager.highlight_animation -= 1f;
			if (QuantumSpriteManager.highlight_animation == 0f)
			{
				QuantumSpriteManager._enemy_anim_timer_positive = !QuantumSpriteManager._enemy_anim_timer_positive;
			}
		}
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x001009B6 File Offset: 0x000FEBB6
	private static void updateArrowEffect()
	{
		QuantumSpriteManager.arrow_middle_current += 10f * Time.deltaTime;
		if (QuantumSpriteManager.arrow_middle_current >= 5f)
		{
			QuantumSpriteManager.arrow_middle_current -= 5f;
		}
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x001009EC File Offset: 0x000FEBEC
	private static void updateScaleEffect()
	{
		WorldTile tMouseTile = null;
		if (InputHelpers.mouseSupported)
		{
			tMouseTile = World.world.getMouseTilePos();
		}
		City tMouseCity = null;
		Kingdom tMouseKingdom = null;
		Alliance tMouseAlliance = null;
		if (tMouseTile != null && !World.world.isBusyWithUI())
		{
			tMouseCity = tMouseTile.zone.city;
			City city = tMouseTile.zone.city;
			tMouseKingdom = ((city != null) ? city.kingdom : null);
			tMouseAlliance = ((tMouseKingdom != null) ? tMouseKingdom.getAlliance() : null);
		}
		float tSpeed = 0.1f;
		Kingdom tSelectedKingdom = null;
		if (World.world.isSelectedPower("relations"))
		{
			tSelectedKingdom = SelectedMetas.selected_kingdom;
		}
		foreach (City tCity in World.world.cities)
		{
			Kingdom tKingdom = tCity.kingdom;
			if (!tKingdom.wild)
			{
				bool tIncrease = false;
				if (Zones.isBordersEnabled())
				{
					if (Zones.showCityZones(false))
					{
						if (tCity == tMouseCity || tKingdom == tSelectedKingdom)
						{
							tIncrease = true;
							tCity.setCursorOver();
						}
					}
					else if (Zones.showKingdomZones(false))
					{
						if (tCity.kingdom == tMouseKingdom || tKingdom == tSelectedKingdom)
						{
							tIncrease = true;
							tKingdom.setCursorOver();
						}
					}
					else if (Zones.showAllianceZones(false) && tMouseKingdom != null)
					{
						Kingdom tCheckKingdom = tCity.kingdom;
						if (tCheckKingdom == null)
						{
							continue;
						}
						Alliance tCheckAlliance = tCheckKingdom.getAlliance();
						if (tCheckKingdom.hasAlliance())
						{
							if (tCheckAlliance == tMouseAlliance || tKingdom == tSelectedKingdom)
							{
								tIncrease = true;
								tCity.setCursorOver();
								tKingdom.setCursorOver();
							}
						}
						else if (tCity.kingdom == tMouseKingdom || tKingdom == tSelectedKingdom)
						{
							tIncrease = true;
							tCity.setCursorOver();
							tKingdom.setCursorOver();
						}
					}
				}
				if (tCity.isCursorOver())
				{
					tIncrease = true;
				}
				if (tCity.kingdom.isCursorOver())
				{
					tIncrease = true;
				}
				if (tCity.kingdom.hasAlliance() && tCity.kingdom.getAlliance().isCursorOver())
				{
					tIncrease = true;
				}
				if (tIncrease)
				{
					tCity.mark_scale_effect += tSpeed;
				}
				else
				{
					tCity.mark_scale_effect -= tSpeed;
				}
				tCity.mark_scale_effect = Mathf.Clamp(tCity.mark_scale_effect, 0.5f, 0.75f);
			}
		}
	}

	// Token: 0x0400158B RID: 5515
	public static float arrow_middle_current;

	// Token: 0x0400158C RID: 5516
	public static float highlight_animation;

	// Token: 0x0400158D RID: 5517
	private static float _enemy_anim_timer = 0f;

	// Token: 0x0400158E RID: 5518
	private static bool _enemy_anim_timer_positive = true;

	// Token: 0x0400158F RID: 5519
	private static bool _initiated = false;
}
