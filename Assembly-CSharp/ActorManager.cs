using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ai;
using ai.behaviours;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class ActorManager : SimSystemManager<Actor, ActorData>
{
	// Token: 0x060011C2 RID: 4546 RVA: 0x000D1994 File Offset: 0x000CFB94
	public ActorManager()
	{
		this.type_id = "unit";
		this._job_manager = new JobManagerActors("actors");
		this._unit_visible_lists.Add(this.visible_units);
		this._unit_visible_lists.Add(this.visible_units_avatars);
		this._unit_visible_lists.Add(this.visible_units_alive);
		this._unit_visible_lists.Add(this.visible_units_with_status);
		this._unit_visible_lists.Add(this.visible_units_with_favorite);
		this._unit_visible_lists.Add(this.visible_units_with_banner);
		this._unit_visible_lists.Add(this.visible_units_just_ate);
		this._unit_visible_lists.Add(this.visible_units_socialize);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000D1AF4 File Offset: 0x000CFCF4
	public void prepareForMetaChecks()
	{
		this.units_only_wild.Clear();
		this.units_only_alive.Clear();
		this.units_only_dying.Clear();
		this.units_only_civ.Clear();
		this.have_dying_units = false;
		List<Actor> tActorList = base.getSimpleList();
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			if (tUnit.isAlive())
			{
				if (tUnit.kingdom.wild)
				{
					this.units_only_wild.Add(tUnit);
				}
				else
				{
					this.units_only_civ.Add(tUnit);
				}
				this.units_only_alive.Add(tUnit);
			}
			else
			{
				this.units_only_dying.Add(tUnit);
				this.have_dying_units = true;
			}
		}
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x000D1BA4 File Offset: 0x000CFDA4
	public void calculateVisibleActors()
	{
		Bench.bench("actors_prepare_lists", "game_total", false);
		this.clearLists();
		this.prepareLists();
		Bench.benchEnd("actors_prepare_lists", "game_total", false, 0L, false);
		Bench.bench("actors_fill_visible", "game_total", false);
		this.fillVisibleObjects();
		Bench.benchEnd("actors_fill_visible", "game_total", false, 0L, false);
		Bench.bench("actors_precalc_render_data_parallel", "game_total", false);
		this.precalculateRenderDataParallel();
		Bench.benchEnd("actors_precalc_render_data_parallel", "game_total", false, 0L, false);
		Bench.bench("actors_precalc_render_data_normal", "game_total", false);
		this.precalculateRenderDataNormal();
		Bench.benchEnd("actors_precalc_render_data_normal", "game_total", false, 0L, false);
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000D1C64 File Offset: 0x000CFE64
	private void precalculateRenderDataParallel()
	{
		int tDebugItemScale = DebugConfig.isOn(DebugOption.RenderBigItems) ? 10 : 1;
		bool tShouldRenderUnitShadows = World.world.quality_changer.shouldRenderUnitShadows();
		int tTotalVisibleObjects = this.visible_units.count;
		Actor[] tArray = this.visible_units.array;
		int tDynamicBatchSize = 256;
		int tTotalBatches = ParallelHelper.calcTotalBatches(tTotalVisibleObjects, tDynamicBatchSize);
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, tTotalVisibleObjects);
			for (int tIndex = num; tIndex < tIndexEnd; tIndex++)
			{
				Actor tActor = tArray[tIndex];
				Vector3 tActorScale = tActor.current_scale;
				Vector3 tActorRotation = tActor.updateRotation();
				Vector3 tCurrentActorPos = tActor.updatePos();
				bool tHasRenderedItem = tActor.checkHasRenderedItem();
				bool tHasNormalRender = !tActor.asset.ignore_generic_render;
				Sprite tItemSpriteFinal;
				if (tHasRenderedItem)
				{
					Sprite tItemSpriteMain = tActor.getRenderedItemSprite();
					IHandRenderer cachedHandRendererAsset = tActor.getCachedHandRendererAsset();
					int tColorAssetID = -900000;
					if (cachedHandRendererAsset.is_colored)
					{
						tColorAssetID = tActor.kingdom.getColor().GetHashCode();
					}
					tItemSpriteFinal = DynamicSprites.getCachedAtlasItemSprite(DynamicSprites.getItemSpriteID(tItemSpriteMain, tColorAssetID), tItemSpriteMain);
				}
				else
				{
					tItemSpriteFinal = null;
				}
				this.render_data.positions[tIndex] = tCurrentActorPos;
				this.render_data.scales[tIndex] = tActorScale;
				this.render_data.rotations[tIndex] = tActorRotation;
				this.render_data.flip_x_states[tIndex] = tActor.flip;
				this.render_data.colors[tIndex] = tActor.color;
				this.render_data.has_normal_render[tIndex] = tHasNormalRender;
				this.render_data.has_item[tIndex] = tHasRenderedItem;
				this.render_data.item_sprites[tIndex] = tItemSpriteFinal;
				AnimationFrameData tFrameData = tActor.getAnimationFrameData();
				bool tHaveShadow = false;
				if (tShouldRenderUnitShadows && tActor.show_shadow)
				{
					ActorTextureSubAsset tSubAsset;
					if (tActor.hasSubspecies() && tActor.subspecies.has_mutation_reskin)
					{
						tSubAsset = tActor.subspecies.mutation_skin_asset.texture_asset;
					}
					else
					{
						tSubAsset = tActor.asset.texture_asset;
					}
					tHaveShadow = tSubAsset.shadow;
					if (tSubAsset.shadow)
					{
						Vector2 tSizeShadow;
						if (tActor.isEgg())
						{
							this.render_data.shadow_sprites[tIndex] = tSubAsset.shadow_sprite_egg;
							tSizeShadow = tSubAsset.shadow_size_egg;
						}
						else if (tActor.isBaby())
						{
							this.render_data.shadow_sprites[tIndex] = tSubAsset.shadow_sprite_baby;
							tSizeShadow = tSubAsset.shadow_size_baby;
						}
						else
						{
							this.render_data.shadow_sprites[tIndex] = tSubAsset.shadow_sprite;
							tSizeShadow = tSubAsset.shadow_size;
						}
						tSizeShadow *= tActorScale;
						int tFlip = tActor.flip ? 1 : -1;
						float tOffsetX = tSizeShadow.x / 2f;
						float tOffsetY = tSizeShadow.y * 0.6f;
						float tAbsAngle = Mathf.Abs(tActorRotation.z);
						Vector2 tShadowPosition = tActor.current_shadow_position;
						tShadowPosition.x += tOffsetX * (tActorRotation.z * (float)tFlip) / 90f;
						tShadowPosition.y -= tOffsetY * tAbsAngle / 90f;
						this.render_data.shadow_position[tIndex] = tShadowPosition;
						if (tFrameData != null && tFrameData.size_unit != default(Vector2))
						{
							float tScaleWidthLay = (tFrameData.size_unit * tActorScale).y / tSizeShadow.x * tActorScale.x;
							float tScaleX = Mathf.Lerp(tActorScale.x, tScaleWidthLay, tAbsAngle / 90f);
							this.render_data.shadow_scales[tIndex] = new Vector2(tScaleX, tActorScale.y);
						}
						else
						{
							this.render_data.shadow_scales[tIndex] = tActorScale;
						}
					}
				}
				this.render_data.shadows[tIndex] = tHaveShadow;
				if (tHasNormalRender)
				{
					if (tActor.canParallelSetColoredSprite())
					{
						Sprite tMainSprite = tActor.calculateMainSprite();
						this.render_data.main_sprites[tIndex] = tMainSprite;
						if (tActor.hasColoredSprite())
						{
							if (!tActor.isColoredSpriteNeedsCheck(tMainSprite))
							{
								this.render_data.main_sprite_colored[tIndex] = tActor.getLastColoredSprite();
							}
							else
							{
								this.render_data.main_sprite_colored[tIndex] = null;
							}
						}
						else
						{
							this.render_data.main_sprite_colored[tIndex] = tMainSprite;
						}
					}
					else
					{
						this.render_data.main_sprites[tIndex] = null;
						this.render_data.main_sprite_colored[tIndex] = null;
					}
					if (tHasRenderedItem)
					{
						this.render_data.item_scale[tIndex] = tActorScale * (float)tDebugItemScale;
						float tFrameDataPosX = 0f;
						float tFrameDataPosY = 0f;
						if (tFrameData != null)
						{
							tFrameDataPosX = tFrameData.pos_item.x;
							tFrameDataPosY = tFrameData.pos_item.y;
						}
						float tX = tCurrentActorPos.x + tFrameDataPosX * tActorScale.x;
						float tY = tCurrentActorPos.y + tFrameDataPosY * tActorScale.y;
						float tZ = -0.01f + tFrameDataPosY * tActorScale.y;
						Vector3 tItemPosition = new Vector3(tX, tY);
						Vector3 tAngle = tActorRotation;
						if (tAngle.y != 0f || tAngle.z != 0f)
						{
							Vector3 t_pivot = new Vector3(tCurrentActorPos.x, tCurrentActorPos.y, 0f);
							tItemPosition = Toolbox.RotatePointAroundPivot(ref tItemPosition, ref t_pivot, ref tAngle);
						}
						tItemPosition.z = tZ;
						this.render_data.item_pos[tIndex] = tItemPosition;
					}
				}
			}
		});
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000D1D04 File Offset: 0x000CFF04
	private void precalculateRenderDataNormal()
	{
		ActorRenderData tRenderData = this.render_data;
		int tTotalVisibleObjects = this.visible_units.count;
		Actor[] tArray = this.visible_units.array;
		for (int i = 0; i < tTotalVisibleObjects; i++)
		{
			Actor tActor = tArray[i];
			if (tRenderData.has_normal_render[i] && tRenderData.main_sprite_colored[i] == null)
			{
				Sprite tMainSprite = tRenderData.main_sprites[i];
				if (tMainSprite == null)
				{
					tMainSprite = tActor.calculateMainSprite();
				}
				tRenderData.main_sprite_colored[i] = tActor.calculateColoredSprite(tMainSprite, true);
			}
		}
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000D1D80 File Offset: 0x000CFF80
	private void fillVisibleObjects()
	{
		base.prepareArray();
		Actor[] tUnits = base.getSimpleArray();
		int tCountTotal = this.Count;
		bool tRenderNormalUnits = MapBox.isRenderGameplay();
		int tCountVisible = 0;
		int tCountVisibleAlive = 0;
		Actor[] tVisible = this.visible_units.array;
		Actor[] tVisibleAlive = this.visible_units_alive.array;
		for (int i = 0; i < tCountTotal; i++)
		{
			Actor tActor = tUnits[i];
			ActorAsset tActorAsset = tActor.asset;
			TileZone tZone = tActor.current_tile.zone;
			if (tActorAsset.has_avatar_prefab)
			{
				Actor[] array = this.visible_units_avatars.array;
				ActorVisibleDataArray actorVisibleDataArray = this.visible_units_avatars;
				int count = actorVisibleDataArray.count;
				actorVisibleDataArray.count = count + 1;
				array[count] = tActor;
			}
			if (tActor.isFavorite() && !tActorAsset.hide_favorite_icon && tZone.visible && !ControllableUnit.isControllingUnit(tActor))
			{
				Actor[] array2 = this.visible_units_with_favorite.array;
				ActorVisibleDataArray actorVisibleDataArray2 = this.visible_units_with_favorite;
				int count = actorVisibleDataArray2.count;
				actorVisibleDataArray2.count = count + 1;
				array2[count] = tActor;
			}
			if (tZone.visible && tRenderNormalUnits && tActor.is_visible)
			{
				tVisible[tCountVisible++] = tActor;
				if (tActor.isAlive())
				{
					tVisibleAlive[tCountVisibleAlive++] = tActor;
					if (tActor.is_army_captain)
					{
						Actor[] array3 = this.visible_units_with_banner.array;
						ActorVisibleDataArray actorVisibleDataArray3 = this.visible_units_with_banner;
						int count = actorVisibleDataArray3.count;
						actorVisibleDataArray3.count = count + 1;
						array3[count] = tActor;
					}
					if (tActorAsset.render_status_effects && tActor.hasAnyStatusEffectToRender())
					{
						Actor[] array4 = this.visible_units_with_status.array;
						ActorVisibleDataArray actorVisibleDataArray4 = this.visible_units_with_status;
						int count = actorVisibleDataArray4.count;
						actorVisibleDataArray4.count = count + 1;
						array4[count] = tActor;
					}
					if (tActor.timestamp_session_ate_food > 0.0)
					{
						Actor[] array5 = this.visible_units_just_ate.array;
						ActorVisibleDataArray actorVisibleDataArray5 = this.visible_units_just_ate;
						int count = actorVisibleDataArray5.count;
						actorVisibleDataArray5.count = count + 1;
						array5[count] = tActor;
					}
					BehaviourActionActor tActorAction = tActor.ai.action;
					if (tActorAction != null && tActorAction.socialize)
					{
						Actor[] array6 = this.visible_units_socialize.array;
						ActorVisibleDataArray actorVisibleDataArray6 = this.visible_units_socialize;
						int count = actorVisibleDataArray6.count;
						actorVisibleDataArray6.count = count + 1;
						array6[count] = tActor;
					}
					else if (tActor.is_forced_socialize_icon && !tActor.is_moving && !tActor.isLying() && tActor.isAttackReady() && Date.getMonthsSince(tActor.is_forced_socialize_timestamp) < 1)
					{
						Actor[] array7 = this.visible_units_socialize.array;
						ActorVisibleDataArray actorVisibleDataArray7 = this.visible_units_socialize;
						int count = actorVisibleDataArray7.count;
						actorVisibleDataArray7.count = count + 1;
						array7[count] = tActor;
					}
				}
			}
		}
		this.visible_units.count = tCountVisible;
		this.visible_units_alive.count = tCountVisibleAlive;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000D2010 File Offset: 0x000D0210
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		Bench.bench("actors", "game_total", false);
		base.checkContainer();
		this._job_manager.updateBase(pElapsed);
		base.checkContainer();
		Bench.benchEnd("actors", "game_total", false, 0L, false);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x000D2064 File Offset: 0x000D0264
	private void checkOverrideUnitShooting()
	{
		if (!DebugConfig.isOn(DebugOption.OverrideUnitShooting))
		{
			return;
		}
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Vector2 tCursorPos = World.world.getMousePos();
		WorldTile tCursorTile = World.world.getMouseTilePos();
		Actor tCursorActor = World.world.getActorNearCursor();
		if (tCursorTile == null)
		{
			return;
		}
		foreach (Actor tActor in this)
		{
			if (tActor != tCursorActor && tActor.isAlive() && tActor.hasRangeAttack())
			{
				BaseSimObject pInitiator = tActor;
				Kingdom kingdom = tActor.kingdom;
				Vector3 pInitiatorPosition = tActor.current_position;
				AttackData tAttackData = new AttackData(pInitiator, tCursorTile, tCursorPos, pInitiatorPosition, null, kingdom, AttackType.Weapon, false, true, false, tActor.getWeaponAsset().projectile, null, 0f);
				CombatActionLibrary.combat_attack_range.action(tAttackData);
			}
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x000D2150 File Offset: 0x000D0350
	protected override void destroyObject(Actor pActor)
	{
		base.destroyObject(pActor);
		if (pActor.hasKingdom())
		{
			pActor.setKingdom(null);
		}
		if (pActor.hasSubspecies())
		{
			pActor.setSubspecies(null);
		}
		if (pActor.tile_target != null)
		{
			pActor.clearTileTarget();
		}
		pActor.asset.units.Remove(pActor);
		this.removeObject(pActor);
		this._job_manager.removeObject(pActor, pActor.batch);
		if (pActor.avatar != null)
		{
			Object.Destroy(pActor.avatar);
			pActor.avatar = null;
		}
		if (pActor.idle_loop_sound != null)
		{
			pActor.idle_loop_sound.stop();
		}
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000D21EE File Offset: 0x000D03EE
	internal override void scheduleDestroyOnPlay(Actor pObject)
	{
		this.triggerActionsOnRemove(pObject);
		base.scheduleDestroyOnPlay(pObject);
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000D2200 File Offset: 0x000D0400
	private void triggerActionsOnRemove(Actor pActor)
	{
		foreach (ActorTrait tTrait in pActor.traits)
		{
			WorldActionTrait action_on_object_remove = tTrait.action_on_object_remove;
			if (action_on_object_remove != null)
			{
				action_on_object_remove(pActor, tTrait);
			}
		}
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000D2260 File Offset: 0x000D0460
	public override void loadFromSave(List<ActorData> pList)
	{
		base.loadFromSave(pList);
		base.checkContainer();
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000D2270 File Offset: 0x000D0470
	public Actor evolutionEvent(Actor pTargetActor, bool pWithBiomeEffect, bool pAscension)
	{
		Subspecies tOriginalSubspecies = pTargetActor.subspecies;
		bool tNewSubspecies = false;
		Subspecies tEvolvedSubspecies = null;
		string tNewActorAssetId = pTargetActor.asset.id;
		if (tOriginalSubspecies.hasEvolvedIntoForm() && !pAscension)
		{
			tEvolvedSubspecies = tOriginalSubspecies.getEvolvedInto();
			if (tEvolvedSubspecies != null)
			{
				tNewActorAssetId = tEvolvedSubspecies.getActorAsset().id;
			}
		}
		if (tEvolvedSubspecies == null)
		{
			bool tNewAsset = false;
			if (pTargetActor.asset.can_evolve_into_new_species)
			{
				tNewAsset = (tOriginalSubspecies.isSapient() || Randy.randomBool());
				if (tNewAsset)
				{
					tNewActorAssetId = pTargetActor.asset.evolution_id;
				}
			}
			if (!tNewAsset)
			{
				Subspecies subspecies = World.world.subspecies.newSpecies(pTargetActor.asset, pTargetActor.current_tile, true);
				tNewSubspecies = true;
				subspecies.mutateFrom(tOriginalSubspecies);
				tEvolvedSubspecies = subspecies;
			}
		}
		if (tEvolvedSubspecies == null)
		{
			tEvolvedSubspecies = World.world.subspecies.newSpecies(AssetManager.actor_library.get(tNewActorAssetId), pTargetActor.current_tile, false);
			tNewSubspecies = true;
			tEvolvedSubspecies.mutateFrom(tOriginalSubspecies);
		}
		if (tNewSubspecies)
		{
			tEvolvedSubspecies.addTrait("uplifted", false);
			tEvolvedSubspecies.makeSapient();
			tEvolvedSubspecies.data.biome_variant = tOriginalSubspecies.data.biome_variant;
		}
		ActorAsset tNewActorAsset = AssetManager.actor_library.get(tNewActorAssetId);
		pTargetActor.setAsset(tNewActorAsset);
		pTargetActor.setSubspecies(tEvolvedSubspecies);
		tEvolvedSubspecies.data.parent_subspecies = tOriginalSubspecies.id;
		if (pAscension)
		{
			string tName = tEvolvedSubspecies.name;
			if (!tName.Contains("Ascentus"))
			{
				tName += " Ascentus";
				tEvolvedSubspecies.setName(tName, true);
			}
		}
		else
		{
			tOriginalSubspecies.setEvolutionSubspecies(tEvolvedSubspecies);
		}
		if (pWithBiomeEffect && Randy.randomChance(0.1f))
		{
			BiomeAsset tBiome = pTargetActor.current_tile.getBiome();
			if (tBiome != null && tBiome.evolution_trait_subspecies != null && tBiome.evolution_trait_subspecies.Count > 0)
			{
				SubspeciesTrait tTrait = AssetManager.subspecies_traits.get(tBiome.evolution_trait_subspecies.GetRandom<string>());
				if (tTrait != null)
				{
					tEvolvedSubspecies.addTrait(tTrait, false);
				}
			}
		}
		pTargetActor.afterEvolutionEvents();
		return pTargetActor;
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x000D243C File Offset: 0x000D063C
	public bool cloneUnit(Actor pCloneFrom, WorldTile pTileTarget = null)
	{
		if (pCloneFrom == null)
		{
			return false;
		}
		if (!pCloneFrom.asset.can_be_cloned)
		{
			return false;
		}
		pCloneFrom.prepareForSave();
		ActorData tOriginalData = pCloneFrom.data;
		string tName = pCloneFrom.getName();
		ActorData tNewBabyData = new ActorData();
		ActorTool.copyImportantData(tOriginalData, tNewBabyData, true);
		tNewBabyData.created_time = World.world.getCurWorldTime();
		tNewBabyData.id = World.world.map_stats.getNextId("unit");
		tNewBabyData.name = tName;
		tNewBabyData.custom_name = tOriginalData.custom_name;
		tNewBabyData.age_overgrowth = tOriginalData.getAge();
		tNewBabyData.parent_id_1 = tOriginalData.id;
		pCloneFrom.increaseBirths();
		if (pTileTarget == null)
		{
			pTileTarget = pCloneFrom.current_tile.neighboursAll.GetRandom<WorldTile>();
		}
		tNewBabyData.x = pTileTarget.x;
		tNewBabyData.y = pTileTarget.y;
		Actor tCloneActor = World.world.units.loadObject(tNewBabyData);
		tCloneActor.created_time_unscaled = (double)Time.time;
		tCloneActor.addTrait("fragile_health", false);
		foreach (ActorTrait tTrait in pCloneFrom.getTraits())
		{
			tCloneActor.addTrait(tTrait, false);
		}
		tCloneActor.addTrait("miracle_born", false);
		if (!pCloneFrom.hasFamily() && pCloneFrom.asset.create_family_at_spawn)
		{
			World.world.families.newFamily(pCloneFrom, pCloneFrom.current_tile, null);
		}
		tCloneActor.data.cloneCustomDataFrom(pCloneFrom.data);
		tCloneActor.setReligion(pCloneFrom.religion);
		tCloneActor.setClan(pCloneFrom.clan);
		tCloneActor.setCulture(pCloneFrom.culture);
		tCloneActor.setSubspecies(pCloneFrom.subspecies);
		tCloneActor.joinLanguage(pCloneFrom.language);
		tCloneActor.setFamily(pCloneFrom.family);
		tCloneActor.setHealth(tOriginalData.health, false);
		tCloneActor.setMana(tOriginalData.mana, false);
		tCloneActor.setStamina(tOriginalData.stamina, false);
		tCloneActor.setHappiness(tOriginalData.happiness, false);
		tCloneActor.setNutrition(tOriginalData.nutrition, false);
		tCloneActor.addTrait("clone", false);
		if (tOriginalData.saved_items != null)
		{
			foreach (long tID in tOriginalData.saved_items)
			{
				Item tItem = World.world.items.get(tID);
				if (tItem != null)
				{
					Item tNewItem = World.world.items.generateItem(tItem.getAsset(), null, null, 1, pCloneFrom, 0, false);
					tNewItem.data.modifiers.Clear();
					tNewItem.data.modifiers.AddRange(tItem.data.modifiers);
					tNewItem.data.modifiers.Remove("eternal");
					tNewItem.initItem();
					tCloneActor.equipment.setItem(tNewItem, tCloneActor);
				}
			}
		}
		tCloneActor.applyRandomForce(1.5f, 2f);
		if (tCloneActor.isRendered())
		{
			EffectsLibrary.spawn("fx_spawn", pTileTarget, null, null, 0f, -1f, -1f, null);
		}
		if (tCloneActor.asset.has_sound_spawn)
		{
			MusicBox.playSound(tCloneActor.asset.sound_spawn, pTileTarget, false, false);
		}
		return true;
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000D2790 File Offset: 0x000D0990
	public Actor createNewUnit(string pStatsID, WorldTile pTile, bool pMiracleSpawn = false, float pSpawnHeight = 0f, Subspecies pSubspecies = null, Subspecies pSubspeciesMutateFrom = null, bool pSpawnWithItems = true, bool pAdultAge = false, bool pGiveOwnerlessItems = false, bool pSapientSubspecies = false)
	{
		ActorAsset tAsset = AssetManager.actor_library.get(pStatsID);
		if (tAsset == null)
		{
			return null;
		}
		Actor tActor = base.newObject();
		tActor.setAsset(tAsset);
		if (!pSubspecies.isRekt())
		{
			tActor.setSubspecies(pSubspecies);
		}
		else
		{
			Actor tClosestSameActor;
			this.checkNewSpecies(tAsset, pTile, tActor, out tClosestSameActor, false, pSapientSubspecies, pSubspeciesMutateFrom);
			if (pMiracleSpawn && tClosestSameActor != null)
			{
				if (tClosestSameActor.hasCulture())
				{
					tActor.setCulture(tClosestSameActor.culture);
				}
				if (tClosestSameActor.hasReligion())
				{
					tActor.setReligion(tClosestSameActor.religion);
				}
				if (tClosestSameActor.hasLanguage())
				{
					tActor.setLanguage(tClosestSameActor.language);
				}
			}
		}
		this.addRandomTraitFromBiomeToActor(tActor, pTile);
		this.finalizeActor(pStatsID, tActor, pTile, pSpawnHeight);
		if (pMiracleSpawn || pAdultAge)
		{
			if (pMiracleSpawn)
			{
				tActor.addTrait("miracle_born", false);
			}
			if (tActor.hasSubspecies())
			{
				tActor.data.age_overgrowth = (int)Math.Ceiling((double)tActor.subspecies.age_breeding);
			}
			else
			{
				tActor.data.age_overgrowth = tAsset.age_spawn;
			}
			if (HotkeyLibrary.isHoldingAlt())
			{
				tActor.data.age_overgrowth = 0;
			}
		}
		tActor.newCreature();
		if (pSpawnWithItems)
		{
			tActor.generateDefaultSpawnWeapons(pGiveOwnerlessItems);
		}
		tActor.clearSprites();
		return tActor;
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000D28B4 File Offset: 0x000D0AB4
	private void finalizeActor(string pStats, Actor pActor, WorldTile pTile, float pZHeight = 0f)
	{
		ActorAsset tAsset = AssetManager.actor_library.get(pStats);
		pActor.setAsset(tAsset);
		ActorData tData = pActor.data;
		pActor.spawnOn(pTile, pZHeight);
		if (tData.subspecies.hasValue())
		{
			pActor.setSubspecies(World.world.subspecies.get(tData.subspecies));
		}
		if (tData.family.hasValue())
		{
			pActor.setFamily(World.world.families.get(tData.family));
		}
		if (tData.language.hasValue())
		{
			pActor.setLanguage(World.world.languages.get(tData.language));
		}
		if (tData.plot.hasValue())
		{
			pActor.setPlot(World.world.plots.get(tData.plot));
		}
		if (tData.religion.hasValue())
		{
			pActor.setReligion(World.world.religions.get(tData.religion));
		}
		if (tData.clan.hasValue())
		{
			pActor.setClan(World.world.clans.get(tData.clan));
		}
		if (tData.culture.hasValue())
		{
			pActor.setCulture(World.world.cultures.get(tData.culture));
		}
		if (tData.army.hasValue())
		{
			pActor.setArmy(World.world.armies.get(tData.army));
		}
		pActor.create();
		pActor.checkDefaultKingdom();
		pActor.checkDefaultProfession();
		pActor.updateStats();
		if (pActor.asset.can_be_killed_by_stuff)
		{
			pActor.batch.c_main_tile_action.Add(pActor);
		}
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000D2A5C File Offset: 0x000D0C5C
	public Actor createBabyActorFromData(ActorData pData, WorldTile pTile, City pCity)
	{
		ActorAsset tStats = AssetManager.actor_library.get(pData.asset_id);
		Actor tActor = base.loadObject(pData);
		tActor.setData(pData);
		tActor.created_time_unscaled = (double)Time.time;
		this.finalizeActor(tStats.id, tActor, pTile, 0f);
		return tActor;
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000D2AAC File Offset: 0x000D0CAC
	public Actor spawnNewUnitByPlayer(string pStatsID, WorldTile pTile, bool pSpawnSound = false, bool pMiracleSpawn = false, float pSpawnHeight = 6f, Subspecies pSubspecies = null)
	{
		Actor tActor = this.spawnNewUnit(pStatsID, pTile, pSpawnSound, pMiracleSpawn, pSpawnHeight, pSubspecies, true, false);
		if (tActor.current_zone.hasCity() && tActor.isSapient())
		{
			City tCity = tActor.current_zone.city;
			if (!tCity.isNeutral() && tCity.isPossibleToJoin(tActor))
			{
				tActor.joinCity(tCity);
			}
		}
		return tActor;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000D2B08 File Offset: 0x000D0D08
	public Actor spawnNewUnit(string pActorAssetID, WorldTile pTile, bool pSpawnSound = false, bool pMiracleSpawn = false, float pSpawnHeight = 6f, Subspecies pSubspecies = null, bool pGiveOwnerlessItems = false, bool pAdultAge = false)
	{
		Actor tUnit = this.createNewUnit(pActorAssetID, pTile, pMiracleSpawn, pSpawnHeight, pSubspecies, null, true, pAdultAge, pGiveOwnerlessItems, false);
		if (pSpawnSound && tUnit.asset.has_sound_spawn)
		{
			MusicBox.playSound(tUnit.asset.sound_spawn, (float)pTile.pos.x, (float)pTile.pos.y, false, false);
		}
		if (tUnit.kingdom == null)
		{
			Kingdom tNomadKingdom = World.world.kingdoms_wild.get(tUnit.asset.kingdom_id_wild);
			tUnit.setKingdom(tNomadKingdom);
		}
		tUnit.setStatsDirty();
		tUnit.setNutrition(SimGlobals.m.nutrition_level_on_spawn, true);
		return tUnit;
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000D2BB0 File Offset: 0x000D0DB0
	private void checkNewSpecies(ActorAsset pAsset, WorldTile pTile, Actor pActor, out Actor pClosestActor, bool pGlobalSearch = false, bool pLookForSapientSubspecies = false, Subspecies pSubspeciesMutateFrom = null)
	{
		pClosestActor = null;
		if (!pAsset.can_have_subspecies)
		{
			return;
		}
		Subspecies tSubspecies = null;
		if (pGlobalSearch)
		{
			foreach (Subspecies tExistingSubspecies in World.world.subspecies)
			{
				if (tExistingSubspecies.isSpecies(pAsset.id))
				{
					tSubspecies = tExistingSubspecies;
					break;
				}
			}
		}
		if (tSubspecies == null)
		{
			Actor tClosestActor;
			tSubspecies = World.world.subspecies.getNearbySpecies(pAsset, pTile, out tClosestActor, pLookForSapientSubspecies, true);
			pClosestActor = tClosestActor;
		}
		if (tSubspecies == null)
		{
			tSubspecies = World.world.subspecies.newSpecies(pAsset, pTile, false);
			if (pSubspeciesMutateFrom != null)
			{
				tSubspecies.mutateFrom(pSubspeciesMutateFrom);
			}
			tSubspecies.forceRecalcBaseStats();
		}
		pActor.setSubspecies(tSubspecies);
		pActor.event_full_stats = true;
		pActor.setStatsDirty();
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x000D2C7C File Offset: 0x000D0E7C
	public ActorTrait addRandomTraitFromBiomeToActor(Actor pActor, WorldTile pTile)
	{
		if (!pTile.Type.is_biome)
		{
			return null;
		}
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		List<string> spawn_trait_actor = tBiomeAsset.spawn_trait_actor;
		if (spawn_trait_actor != null && spawn_trait_actor.Count > 0 && Randy.randomBool())
		{
			string tRandomTraitID = tBiomeAsset.spawn_trait_actor.GetRandom<string>();
			ActorTrait tTrait = AssetManager.traits.get(tRandomTraitID);
			pActor.addTrait(tTrait, false);
			return tTrait;
		}
		return null;
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x000D2CE8 File Offset: 0x000D0EE8
	public override Actor loadObject(ActorData pData)
	{
		if (this.dict.ContainsKey(pData.id))
		{
			Debug.Log("Trying to load unit with same ID, that already is loaded. " + pData.id.ToString());
			return null;
		}
		WorldTile tTile = World.world.GetTile(pData.x, pData.y);
		if (tTile == null)
		{
			return null;
		}
		ActorAsset tAsset = AssetManager.actor_library.get(pData.asset_id);
		if (tAsset == null)
		{
			return null;
		}
		int tSavedHealth = pData.health;
		int tSavedNutrition = pData.nutrition;
		int tSavedStamina = pData.stamina;
		int tSavedMana = pData.mana;
		Actor tActor = base.loadObject(pData);
		tActor.setData(pData);
		this.finalizeActor(tAsset.id, tActor, tTile, 0f);
		if (tActor.canUseItems())
		{
			tActor.equipment.load(pData.saved_items, tActor);
		}
		if (tActor.isSapient())
		{
			tActor.reloadInventory();
		}
		tActor.loadFromSave();
		tActor.updateStats();
		tActor.setHealth(tSavedHealth, true);
		tActor.setNutrition(tSavedNutrition, true);
		tActor.setStamina(tSavedStamina, true);
		tActor.setMana(tSavedMana, true);
		if (tActor.asset.can_have_subspecies && !tActor.hasSubspecies())
		{
			Actor tClosestSameActor;
			this.checkNewSpecies(tActor.asset, tActor.current_tile, tActor, out tClosestSameActor, true, false, null);
		}
		tActor.makeWait(Randy.randomFloat(0.1f, 2f));
		return tActor;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x000D2E48 File Offset: 0x000D1048
	protected override void addObject(Actor pObject)
	{
		base.addObject(pObject);
		this._job_manager.addNewObject(pObject);
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000D2E60 File Offset: 0x000D1060
	private void clearLists()
	{
		for (int i = 0; i < this._unit_visible_lists.Count; i++)
		{
			this._unit_visible_lists[i].count = 0;
		}
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000D2E98 File Offset: 0x000D1098
	private void prepareLists()
	{
		for (int i = 0; i < this._unit_visible_lists.Count; i++)
		{
			this._unit_visible_lists[i].prepare(this.Count);
		}
		this.render_data.checkSize(this.Count);
		base.checkContainer();
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000D2EE9 File Offset: 0x000D10E9
	public override void clear()
	{
		this._job_manager.clear();
		this.cached_sleeping_units.Clear();
		this.clearLists();
		base.checkContainer();
		base.scheduleDestroyAllOnWorldClear();
		base.checkObjectsToDestroy();
		base.clear();
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000D2F20 File Offset: 0x000D1120
	public void debugJobManager(DebugTool pTool)
	{
		this._job_manager.debug(pTool);
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x000D2F2E File Offset: 0x000D112E
	public JobManagerActors getJobManager()
	{
		return this._job_manager;
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x000D2F38 File Offset: 0x000D1138
	public void checkSleepingUnits()
	{
		if (World.world.getWorldTimeElapsedSince(this._timestamp_sleeping_units) < 10f)
		{
			return;
		}
		this.cached_sleeping_units.Clear();
		this._timestamp_sleeping_units = World.world.getCurWorldTime();
		foreach (Status tStatus in World.world.statuses.list.LoopRandom<Status>())
		{
			if (!tStatus.is_finished && !(tStatus.asset.id != "sleeping"))
			{
				Actor tActor = tStatus.sim_object.a;
				if (tActor.isAlive())
				{
					this.cached_sleeping_units.Add(tActor);
					if (this.cached_sleeping_units.Count > 10)
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x04000FCA RID: 4042
	private JobManagerActors _job_manager;

	// Token: 0x04000FCB RID: 4043
	public readonly ActorRenderData render_data = new ActorRenderData(4096);

	// Token: 0x04000FCC RID: 4044
	public readonly ActorVisibleDataArray visible_units_avatars = new ActorVisibleDataArray();

	// Token: 0x04000FCD RID: 4045
	public readonly ActorVisibleDataArray visible_units = new ActorVisibleDataArray();

	// Token: 0x04000FCE RID: 4046
	public readonly ActorVisibleDataArray visible_units_alive = new ActorVisibleDataArray();

	// Token: 0x04000FCF RID: 4047
	public readonly ActorVisibleDataArray visible_units_with_status = new ActorVisibleDataArray();

	// Token: 0x04000FD0 RID: 4048
	public readonly ActorVisibleDataArray visible_units_with_favorite = new ActorVisibleDataArray();

	// Token: 0x04000FD1 RID: 4049
	public readonly ActorVisibleDataArray visible_units_with_banner = new ActorVisibleDataArray();

	// Token: 0x04000FD2 RID: 4050
	public readonly ActorVisibleDataArray visible_units_just_ate = new ActorVisibleDataArray();

	// Token: 0x04000FD3 RID: 4051
	public readonly ActorVisibleDataArray visible_units_socialize = new ActorVisibleDataArray();

	// Token: 0x04000FD4 RID: 4052
	private double _timestamp_sleeping_units;

	// Token: 0x04000FD5 RID: 4053
	public readonly List<Actor> cached_sleeping_units = new List<Actor>();

	// Token: 0x04000FD6 RID: 4054
	private readonly List<ActorVisibleDataArray> _unit_visible_lists = new List<ActorVisibleDataArray>();

	// Token: 0x04000FD7 RID: 4055
	public bool have_dying_units;

	// Token: 0x04000FD8 RID: 4056
	public readonly List<Actor> units_only_wild = new List<Actor>();

	// Token: 0x04000FD9 RID: 4057
	public readonly List<Actor> units_only_civ = new List<Actor>();

	// Token: 0x04000FDA RID: 4058
	public readonly List<Actor> units_only_alive = new List<Actor>();

	// Token: 0x04000FDB RID: 4059
	public readonly List<Actor> units_only_dying = new List<Actor>();
}
