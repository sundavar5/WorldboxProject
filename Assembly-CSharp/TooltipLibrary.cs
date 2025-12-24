using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000780 RID: 1920
[ObfuscateLiterals]
[Serializable]
public class TooltipLibrary : AssetLibrary<TooltipAsset>
{
	// Token: 0x06003CDD RID: 15581 RVA: 0x001A5B6C File Offset: 0x001A3D6C
	public override void init()
	{
		base.init();
		this.add(new TooltipAsset
		{
			id = "normal",
			callback = new TooltipShowAction(this.showNormal)
		});
		this.add(new TooltipAsset
		{
			id = "neuron",
			callback = new TooltipShowAction(this.showNeuron),
			callback_text_animated = new TooltipShowAction(this.showNeuron)
		});
		this.add(new TooltipAsset
		{
			id = "biome_seed",
			prefab_id = "tooltips/tooltip_biome_seed",
			callback = new TooltipShowAction(this.showBiomeSeed)
		});
		this.add(new TooltipAsset
		{
			id = "unit_spawn",
			prefab_id = "tooltips/tooltip_species_spawn",
			callback = new TooltipShowAction(this.showUnitSpawn)
		});
		this.add(new TooltipAsset
		{
			id = "unit_species",
			prefab_id = "tooltips/tooltip_species_spawn",
			callback = new TooltipShowAction(this.showUnitSpecies)
		});
		this.add(new TooltipAsset
		{
			id = "unit_button",
			prefab_id = "tooltips/tooltip_species_spawn",
			callback = new TooltipShowAction(this.showUnitButton)
		});
		this.add(new TooltipAsset
		{
			id = "onomastics_asset",
			callback = new TooltipShowAction(this.showOnomastics)
		});
		this.add(new TooltipAsset
		{
			id = "trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showTrait)
		});
		this.add(new TooltipAsset
		{
			id = "culture_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showCultureTrait)
		});
		this.add(new TooltipAsset
		{
			id = "language_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showLanguageTrait)
		});
		this.add(new TooltipAsset
		{
			id = "subspecies_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showSubspeciesTrait)
		});
		this.add(new TooltipAsset
		{
			id = "clan_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showClanTrait)
		});
		this.add(new TooltipAsset
		{
			id = "religion_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showReligionTrait)
		});
		this.add(new TooltipAsset
		{
			id = "kingdom_trait",
			prefab_id = "tooltips/tooltip_trait",
			callback = new TooltipShowAction(this.showKingdomTrait)
		});
		this.add(new TooltipAsset
		{
			id = "chromosome",
			callback = new TooltipShowAction(this.showChromosome)
		});
		this.add(new TooltipAsset
		{
			id = "gene",
			callback = new TooltipShowAction(this.showGene),
			callback_text_animated = new TooltipShowAction(this.showGeneDNASequence)
		});
		this.add(new TooltipAsset
		{
			id = "status",
			prefab_id = "tooltips/tooltip_status",
			callback = new TooltipShowAction(this.showStatus)
		});
		this.add(new TooltipAsset
		{
			id = "status_updatable",
			prefab_id = "tooltips/tooltip_status",
			callback = new TooltipShowAction(this.showStatus),
			callback_text_animated = new TooltipShowAction(this.showStatus)
		});
		this.add(new TooltipAsset
		{
			id = "culture",
			prefab_id = "tooltips/tooltip_culture",
			callback = new TooltipShowAction(this.showCulture)
		});
		this.add(new TooltipAsset
		{
			id = "subspecies",
			prefab_id = "tooltips/tooltip_subspecies",
			callback = new TooltipShowAction(this.showSubspecies)
		});
		this.add(new TooltipAsset
		{
			id = "family",
			prefab_id = "tooltips/tooltip_family",
			callback = new TooltipShowAction(this.showFamily)
		});
		this.add(new TooltipAsset
		{
			id = "language",
			prefab_id = "tooltips/tooltip_language",
			callback = new TooltipShowAction(this.showLanguage)
		});
		this.add(new TooltipAsset
		{
			id = "religion",
			prefab_id = "tooltips/tooltip_religion",
			callback = new TooltipShowAction(this.showReligion)
		});
		this.add(new TooltipAsset
		{
			id = "book",
			prefab_id = "tooltips/tooltip_book",
			callback = new TooltipShowAction(this.showBook)
		});
		this.add(new TooltipAsset
		{
			id = "clan",
			prefab_id = "tooltips/tooltip_clan",
			callback = new TooltipShowAction(this.showClan)
		});
		this.add(new TooltipAsset
		{
			id = "army",
			prefab_id = "tooltips/tooltip_army",
			callback = new TooltipShowAction(this.showArmy)
		});
		this.add(new TooltipAsset
		{
			id = "alliance",
			prefab_id = "tooltips/tooltip_alliance",
			callback = new TooltipShowAction(this.showAlliance)
		});
		this.add(new TooltipAsset
		{
			id = "kingdom",
			prefab_id = "tooltips/tooltip_kingdom",
			callback = new TooltipShowAction(this.showKingdom)
		});
		this.add(new TooltipAsset
		{
			id = "kingdom_dead",
			prefab_id = "tooltips/tooltip_kingdom_dead",
			callback = new TooltipShowAction(this.showDeadKingdom)
		});
		this.add(new TooltipAsset
		{
			id = "kingdom_diplo",
			callback = new TooltipShowAction(this.showKingdom),
			prefab_id = "tooltips/tooltip_kingdom_opinion"
		});
		TooltipAsset t = this.t;
		t.callback = (TooltipShowAction)Delegate.Combine(t.callback, new TooltipShowAction(this.opinionListToStatsDiplomacy));
		this.add(new TooltipAsset
		{
			id = "city",
			prefab_id = "tooltips/tooltip_city",
			callback = new TooltipShowAction(this.showCity)
		});
		this.add(new TooltipAsset
		{
			id = "plot",
			prefab_id = "tooltips/tooltip_plot",
			callback = new TooltipShowAction(this.showPlot)
		});
		this.add(new TooltipAsset
		{
			id = "plot_in_editor",
			prefab_id = "tooltips/tooltip_plot_editor",
			callback = new TooltipShowAction(this.showPlotInEditor)
		});
		this.add(new TooltipAsset
		{
			id = "happiness",
			prefab_id = "tooltips/tooltip_happiness",
			callback = new TooltipShowAction(this.showHappiness)
		});
		this.add(new TooltipAsset
		{
			id = "city_capital",
			prefab_id = "tooltips/tooltip_city",
			callback = new TooltipShowAction(this.showCityCapital)
		});
		this.add(new TooltipAsset
		{
			id = "city_home",
			prefab_id = "tooltips/tooltip_city",
			callback = new TooltipShowAction(this.showCityHome)
		});
		this.add(new TooltipAsset
		{
			id = "actor_king",
			prefab_id = "tooltips/tooltip_actor",
			callback = new TooltipShowAction(this.showKing)
		});
		this.add(new TooltipAsset
		{
			id = "actor",
			prefab_id = "tooltips/tooltip_actor",
			callback = new TooltipShowAction(this.showActorNormal)
		});
		this.add(new TooltipAsset
		{
			id = "actor_leader",
			prefab_id = "tooltips/tooltip_actor",
			callback = new TooltipShowAction(this.showLeader)
		});
		this.add(new TooltipAsset
		{
			id = "map_meta",
			callback = new TooltipShowAction(this.showMapMeta)
		});
		this.add(new TooltipAsset
		{
			id = "equipment",
			prefab_id = "tooltips/tooltip_equipment",
			callback = new TooltipShowAction(this.showEquipment)
		});
		this.add(new TooltipAsset
		{
			id = "equipment_in_editor",
			prefab_id = "tooltips/tooltip_equipment_in_editor",
			callback = new TooltipShowAction(this.showEquipmentInEditor)
		});
		this.add(new TooltipAsset
		{
			id = "city_resource",
			callback = new TooltipShowAction(this.showCityResource),
			callback_text_animated = new TooltipShowAction(this.showCityResource)
		});
		this.add(new TooltipAsset
		{
			id = "city_resource_food",
			callback = new TooltipShowAction(this.showCityResourceFood),
			callback_text_animated = new TooltipShowAction(this.showCityResourceFood)
		});
		this.add(new TooltipAsset
		{
			id = "graph_resource",
			callback = new TooltipShowAction(this.showGraphResource)
		});
		this.add(new TooltipAsset
		{
			id = "graph_multi_resource",
			callback = new TooltipShowAction(this.showGraphMultiResource)
		});
		this.add(new TooltipAsset
		{
			id = "gender_data",
			callback = new TooltipShowAction(this.showGenderData)
		});
		this.add(new TooltipAsset
		{
			id = "war",
			prefab_id = "tooltips/tooltip_war",
			callback = new TooltipShowAction(this.showWar)
		});
		TooltipAsset t2 = this.t;
		t2.callback = (TooltipShowAction)Delegate.Combine(t2.callback, new TooltipShowAction(this.showWarSides));
		this.add(new TooltipAsset
		{
			id = "world_law",
			callback = new TooltipShowAction(this.showWorldLaw)
		});
		this.add(new TooltipAsset
		{
			id = "world_age",
			prefab_id = "tooltips/tooltip_world_age",
			callback = new TooltipShowAction(this.showWorldAge)
		});
		this.add(new TooltipAsset
		{
			id = "tip",
			callback = new TooltipShowAction(this.showTip)
		});
		this.add(new TooltipAsset
		{
			id = "tip_zone_mode",
			callback = new TooltipShowAction(this.showTipZoneMode)
		});
		this.add(new TooltipAsset
		{
			id = "stats_icon",
			callback = new TooltipShowAction(this.showTip)
		});
		TooltipAsset t3 = this.t;
		t3.callback = (TooltipShowAction)Delegate.Combine(t3.callback, new TooltipShowAction(this.showStatsData));
		this.add(new TooltipAsset
		{
			id = "debug_kingdom_assets",
			callback = new TooltipShowAction(this.showKingdomAsset)
		});
		this.add(new TooltipAsset
		{
			id = "mass",
			callback = new TooltipShowAction(this.showMass)
		});
		this.add(new TooltipAsset
		{
			id = "past_rulers",
			prefab_id = "tooltips/tooltip_past_rulers",
			callback = new TooltipShowAction(this.showPastRulers)
		});
		this.add(new TooltipAsset
		{
			id = "past_names",
			prefab_id = "tooltips/tooltip_past_rulers",
			callback = new TooltipShowAction(this.showPastNames)
		});
		this.add(new TooltipAsset
		{
			id = "carrying_resources",
			callback = new TooltipShowAction(this.showCarryingResources)
		});
		this.add(new TooltipAsset
		{
			id = "passengers",
			prefab_id = "tooltips/tooltip_passengers",
			callback = new TooltipShowAction(this.showPassengers)
		});
		this.add(new TooltipAsset
		{
			id = "loyalty",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t4 = this.t;
		t4.callback = (TooltipShowAction)Delegate.Combine(t4.callback, new TooltipShowAction(this.showLoyalty));
		TooltipAsset t5 = this.t;
		t5.callback = (TooltipShowAction)Delegate.Combine(t5.callback, new TooltipShowAction(this.opinionListToStatsLoyalty));
		this.add(new TooltipAsset
		{
			id = "taxonomy",
			prefab_id = "tooltips/tooltip_taxonomy",
			callback = new TooltipShowAction(this.showTaxonomy)
		});
		this.add(new TooltipAsset
		{
			id = "achievement",
			prefab_id = "tooltips/tooltip_achievement",
			callback = new TooltipShowAction(this.showAchievement)
		});
		this.add(new TooltipAsset
		{
			id = "color_counter",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t6 = this.t;
		t6.callback = (TooltipShowAction)Delegate.Combine(t6.callback, new TooltipShowAction(this.showColorCounter));
		this.add(new TooltipAsset
		{
			id = "game_language",
			callback = new TooltipShowAction(this.showGameLanguage)
		});
		this.addMetaListButtonTooltips();
		this.initDebug();
	}

	// Token: 0x06003CDE RID: 15582 RVA: 0x001A68E8 File Offset: 0x001A4AE8
	private void showMetaInfo(Tooltip pTooltip, string pAssetId, string pStatisticID)
	{
		MetaTypeAsset tAsset = AssetManager.meta_type_library.get(pAssetId);
		int tCount = 0;
		foreach (NanoObject tObject in tAsset.get_list())
		{
			if (!tObject.isRekt() && !tObject.hasDied())
			{
				tCount++;
			}
		}
		this.setIconValue(pTooltip, "i_total", (float)tCount, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_destroyed", (float)StatsHelper.getStat(pStatisticID), null, "", false, "", '/');
		this.setIconSprite(pTooltip, "i_total", tAsset.icon_list);
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x001A69B8 File Offset: 0x001A4BB8
	private void addMetaListButtonTooltips()
	{
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_subspecies",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t = this.t;
		t.callback = (TooltipShowAction)Delegate.Combine(t.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "subspecies", "world_statistics_subspecies_extinct");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_languages",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t2 = this.t;
		t2.callback = (TooltipShowAction)Delegate.Combine(t2.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "language", "world_statistics_languages_forgotten");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_families",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t3 = this.t;
		t3.callback = (TooltipShowAction)Delegate.Combine(t3.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "family", "world_statistics_families_destroyed");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_cultures",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t4 = this.t;
		t4.callback = (TooltipShowAction)Delegate.Combine(t4.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "culture", "world_statistics_cultures_forgotten");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_religions",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t5 = this.t;
		t5.callback = (TooltipShowAction)Delegate.Combine(t5.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "religion", "world_statistics_religions_forgotten");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_clans",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t6 = this.t;
		t6.callback = (TooltipShowAction)Delegate.Combine(t6.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "clan", "world_statistics_clans_destroyed");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_cities",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t7 = this.t;
		t7.callback = (TooltipShowAction)Delegate.Combine(t7.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "city", "world_statistics_cities_destroyed");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_kingdoms",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t8 = this.t;
		t8.callback = (TooltipShowAction)Delegate.Combine(t8.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "kingdom", "world_statistics_kingdoms_destroyed");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_armies",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t9 = this.t;
		t9.callback = (TooltipShowAction)Delegate.Combine(t9.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "army", "world_statistics_armies_destroyed");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_alliances",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t10 = this.t;
		t10.callback = (TooltipShowAction)Delegate.Combine(t10.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "alliance", "world_statistics_alliances_made");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_wars",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t11 = this.t;
		t11.callback = (TooltipShowAction)Delegate.Combine(t11.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "war", "world_statistics_peaces_made");
		}));
		this.add(new TooltipAsset
		{
			id = "tooltip_meta_list_plots",
			prefab_id = "tooltips/tooltip_meta_list",
			callback = new TooltipShowAction(this.showNormal)
		});
		TooltipAsset t12 = this.t;
		t12.callback = (TooltipShowAction)Delegate.Combine(t12.callback, new TooltipShowAction(delegate(Tooltip pTooltip, string _, TooltipData _)
		{
			this.showMetaInfo(pTooltip, "plot", "world_statistics_plots_succeeded");
		}));
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x001A6E0C File Offset: 0x001A500C
	private void showNormal(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (!string.IsNullOrEmpty(pData.tip_name))
		{
			pTooltip.name.text = pData.tip_name.Localize();
		}
		if (!string.IsNullOrEmpty(pData.tip_description))
		{
			string tLocalizedText = pData.tip_description.Localize();
			pTooltip.setDescription(tLocalizedText, null);
		}
		if (Config.isComputer || Config.isEditor)
		{
			string tBottomDescription = pData.tip_description_2;
			if (string.IsNullOrEmpty(tBottomDescription))
			{
				tBottomDescription = pData.tip_description + "_2";
			}
			if (!string.IsNullOrEmpty(tBottomDescription) && LocalizedTextManager.stringExists(tBottomDescription))
			{
				string tLocalizedHotkeyText = tBottomDescription.Localize();
				tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
				pTooltip.setBottomDescription(tLocalizedHotkeyText, null);
			}
		}
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x001A6EB8 File Offset: 0x001A50B8
	private void showNeuron(Tooltip pTooltip, string pType, TooltipData pData)
	{
		NeuronElement tNeuron = pData.neuron;
		DecisionAsset tDecision = tNeuron.decision;
		Actor actor = tNeuron.actor;
		NeuralLayerAsset tNeuroAsset = tDecision.priority.GetAsset();
		pTooltip.clearTextRows();
		pTooltip.setTitle(tDecision.getLocalizedText(), "neuron", tNeuroAsset.color_hex);
		if (tDecision.unique)
		{
			pTooltip.name.color = RarityLibrary.legendary.color_container.color;
		}
		else
		{
			pTooltip.name.color = RarityLibrary.rare.color_container.color;
		}
		pTooltip.setDescription("neuron_description".Localize(), null);
		bool tEnabled = actor.isDecisionEnabled(tDecision.decision_index);
		pTooltip.addLineText("neuron_state", tEnabled ? LocalizedTextManager.getText("neuron_active", null, false) : LocalizedTextManager.getText("neuron_silenced", null, false), tEnabled ? "#43FF43" : "#FB2C21", false, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineText("neuro_layer", tNeuroAsset.getLocaleID().Localize(), tNeuroAsset.color_hex, false, true, 21);
		pTooltip.addLineText("neuro_layer_priority", tNeuroAsset.getDescriptionID().Localize(), tNeuroAsset.color_hex, false, true, 21);
		pTooltip.addLineText("neuron_firing_rate", tDecision.getFiringRate(), null, false, true, 21);
		pTooltip.addLineText("neuron_cooldown", tNeuron.getSimulatedTimer().ToText() + "s", null, false, true, 21);
		if (actor.isDecisionOnCooldown(tDecision.decision_index, (double)tDecision.cooldown))
		{
			pTooltip.resetBottomDescription();
			pTooltip.addBottomDescription("neuron_on_refractory_period".Localize(), null);
		}
	}

	// Token: 0x06003CE2 RID: 15586 RVA: 0x001A7048 File Offset: 0x001A5248
	private void showBiomeSeed(Tooltip pTooltip, string pType, TooltipData pData)
	{
		GodPower tPower = pData.power;
		string tBiomeId = AssetManager.drops.get(tPower.drop_id).cached_drop_type_low.biome_id;
		BiomeAsset tBiome = AssetManager.biome_library.get(tBiomeId);
		using (ListPool<string> tRows = new ListPool<string>())
		{
			TooltipIconsRow tTraitIcons = pTooltip.transform.FindRecursive("Traits").GetComponent<TooltipIconsRow>();
			bool tShow = false;
			tShow |= this.showBiomeTraits<ActorTrait>(tBiome.spawn_trait_actor, AssetManager.traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<SubspeciesTrait>(tBiome.spawn_trait_subspecies, AssetManager.subspecies_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<SubspeciesTrait>(tBiome.evolution_trait_subspecies, AssetManager.subspecies_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<SubspeciesTrait>(tBiome.spawn_trait_subspecies_always, AssetManager.subspecies_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<CultureTrait>(tBiome.spawn_trait_culture, AssetManager.culture_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<LanguageTrait>(tBiome.spawn_trait_language, AssetManager.language_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<ReligionTrait>(tBiome.spawn_trait_religion, AssetManager.religion_traits, tTraitIcons, pTooltip, pData);
			tShow |= this.showBiomeTraits<ClanTrait>(tBiome.spawn_trait_clan, AssetManager.clan_traits, tTraitIcons, pTooltip, pData);
			tTraitIcons.gameObject.SetActive(tShow);
			if (tShow)
			{
				tTraitIcons.init(pTooltip, pData);
			}
			if (pTooltip.pool_icons == null)
			{
				Transform tIconsParent = pTooltip.transform.FindRecursive("Species");
				StatsIcon tPrefab = Resources.Load<StatsIcon>("ui/PrefabTextIconTooltipBig");
				pTooltip.pool_icons = new ObjectPoolGenericMono<StatsIcon>(tPrefab, tIconsParent);
			}
			tRows.Clear();
			if (tBiome.pot_units_spawn != null)
			{
				foreach (string tActorId in tBiome.pot_units_spawn)
				{
					if (!tRows.Contains(tActorId))
					{
						tRows.Add(tActorId);
						this.showBiomeSeedUnit(tActorId, pTooltip);
					}
				}
			}
			if (WorldLawLibrary.world_law_drop_of_thoughts.isEnabled() && tBiome.pot_sapient_units_spawn != null)
			{
				foreach (string tActorId2 in tBiome.pot_sapient_units_spawn)
				{
					if (!tRows.Contains(tActorId2))
					{
						tRows.Add(tActorId2);
						this.showBiomeSeedUnit(tActorId2, pTooltip);
					}
				}
			}
			this.showNormal(pTooltip, pType, pData);
		}
	}

	// Token: 0x06003CE3 RID: 15587 RVA: 0x001A72E0 File Offset: 0x001A54E0
	private void showBiomeSeedUnit(string pId, Tooltip pTooltip)
	{
		StatsIcon next = pTooltip.pool_icons.getNext();
		Image tIcon = next.getIcon();
		ActorAsset tAsset = AssetManager.actor_library.get(pId);
		tIcon.sprite = tAsset.getSpriteIcon();
		next.text.text = tAsset.getTranslatedName();
		if (tAsset.isAvailable())
		{
			tIcon.color = Toolbox.color_white;
			return;
		}
		tIcon.color = Toolbox.color_black;
	}

	// Token: 0x06003CE4 RID: 15588 RVA: 0x001A7348 File Offset: 0x001A5548
	private bool showBiomeTraits<T>(List<string> pTraits, BaseTraitLibrary<T> pLibrary, TooltipIconsRow pRow, Tooltip pTooltip, TooltipData pData) where T : BaseTrait<T>
	{
		bool flag;
		if (pTraits == null)
		{
			flag = true;
		}
		else
		{
			int count = pTraits.Count;
			flag = false;
		}
		if (flag)
		{
			return false;
		}
		foreach (string tId in pTraits)
		{
			T t = pLibrary.get(tId);
			string tColor = t.isAvailable() ? "#FFFFFF" : "#000000";
			Sprite tIcon = t.getSprite();
			pRow.addIcon(tIcon, tColor);
		}
		return true;
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x001A73D8 File Offset: 0x001A55D8
	private void showUnitSpawn(Tooltip pTooltip, string pType, TooltipData pData)
	{
		GodPower power = pData.power;
		string tActorAssetID = power.getActorAssetID();
		bool tShowStatsOverview = power.show_unit_stats_overview;
		this.showUnitGeneric(pTooltip, pData, tActorAssetID, tShowStatsOverview, true);
		this.checkDebugSpeciesRows(pTooltip, pData, tActorAssetID);
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x001A740C File Offset: 0x001A560C
	private void checkDebugSpeciesRows(Tooltip pTooltip, TooltipData pData, string pActorAssetID)
	{
		ActorAsset tActorAsset = AssetManager.actor_library.get(pActorAssetID);
		this.showDebugRowsIcons<ActorTrait>(pTooltip, pData, "IconsRowActor", tActorAsset.traits, AssetManager.traits);
		this.showDebugRowsIcons<SubspeciesTrait>(pTooltip, pData, "IconsRowSubspecies", tActorAsset.default_subspecies_traits, AssetManager.subspecies_traits);
		this.showDebugRowsIcons<ClanTrait>(pTooltip, pData, "IconsRowClan", tActorAsset.default_clan_traits, AssetManager.clan_traits);
		this.showDebugRowsIcons<LanguageTrait>(pTooltip, pData, "IconsRowLanguage", tActorAsset.default_language_traits, AssetManager.language_traits);
		this.showDebugRowsIcons<CultureTrait>(pTooltip, pData, "IconsRowCulture", tActorAsset.default_culture_traits, AssetManager.culture_traits);
		this.showDebugRowsIcons<ReligionTrait>(pTooltip, pData, "IconsRowReligion", tActorAsset.default_religion_traits, AssetManager.religion_traits);
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x001A74B8 File Offset: 0x001A56B8
	private void showDebugRowsIcons<TTraitType>(Tooltip pTooltip, TooltipData pData, string pRowName, List<string> pTraitsList, BaseTraitLibrary<TTraitType> pTraitLibrary) where TTraitType : BaseTrait<TTraitType>
	{
		TooltipIconsRow tIconsRow = pTooltip.transform.FindRecursive(pRowName).GetComponent<TooltipIconsRow>();
		bool tFill = DebugConfig.isOn(DebugOption.DebugPowerBarTooltipSpeciesTraits);
		if (pTraitsList != null && tFill)
		{
			foreach (string tTraitID in pTraitsList)
			{
				TTraitType tIcon = pTraitLibrary.get(tTraitID);
				tIconsRow.addIcon(tIcon.getSprite(), "#FFFFFF");
			}
		}
		tIconsRow.init(pTooltip, pData);
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x001A7550 File Offset: 0x001A5750
	private void showUnitSpecies(Tooltip pTooltip, string pType, TooltipData pData)
	{
		string tActorAssetID = pData.power.getActorAssetID();
		this.showUnitGeneric(pTooltip, pData, tActorAssetID, true, false);
		this.checkDebugSpeciesRows(pTooltip, pData, tActorAssetID);
	}

	// Token: 0x06003CE9 RID: 15593 RVA: 0x001A7580 File Offset: 0x001A5780
	private void showUnitButton(Tooltip pTooltip, string pType, TooltipData pData)
	{
		string tActorAssetID = pData.actor_asset.id;
		this.showUnitGeneric(pTooltip, pData, tActorAssetID, true, true);
		this.checkDebugSpeciesRows(pTooltip, pData, tActorAssetID);
	}

	// Token: 0x06003CEA RID: 15594 RVA: 0x001A75B0 File Offset: 0x001A57B0
	private void showUnitGeneric(Tooltip pTooltip, TooltipData pData, string pActorAssetId, bool pShowStatsOverview, bool pShowStats = true)
	{
		Transform tTransform = pTooltip.transform.FindRecursive("Stats");
		bool tShowStats = false;
		if (pShowStatsOverview && !string.IsNullOrEmpty(pActorAssetId))
		{
			ActorAsset tAsset = AssetManager.actor_library.get(pActorAssetId);
			if (tAsset != null)
			{
				pTooltip.name.text = tAsset.getLocalizedName();
				pTooltip.setDescription(tAsset.getLocalizedDescription(), null);
				if (!tAsset.isAvailable())
				{
					tTransform.gameObject.SetActive(false);
					return;
				}
				if (pShowStats && DebugConfig.isOn(DebugOption.DebugPowerBarTooltipSpeciesTraits))
				{
					BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tAsset.getStatsForOverview(), false);
				}
				if (tAsset.can_have_subspecies)
				{
					tShowStats = true;
					this.setIconValue(pTooltip, "i_population", (float)tAsset.countPopulation(), null, "", false, "", '/');
					this.setIconValue(pTooltip, "i_subspecies", (float)tAsset.countSubspecies(), null, "", false, "", '/');
					this.setIconValue(pTooltip, "i_families", (float)tAsset.countFamilies(), null, "", false, "", '/');
				}
				if (DebugConfig.isOn(DebugOption.DebugPowerBarTooltipTaxonomy))
				{
					this.showDebugTaxonomy(pTooltip, tAsset);
				}
				if (DebugConfig.isOn(DebugOption.DebugPowerBarTooltipStartingCivMetas))
				{
					this.showDebugTraits(pTooltip, tAsset);
				}
			}
		}
		tTransform.gameObject.SetActive(tShowStats);
		if (!string.IsNullOrEmpty(pData.tip_description))
		{
			string tLocalizedText = LocalizedTextManager.getText(pData.tip_description, null, false);
			tLocalizedText = tLocalizedText.Replace("$lifeissimhours$", 24f.ToText());
			pTooltip.setDescription(tLocalizedText, null);
		}
		if ((Config.isComputer || Config.isEditor) && !string.IsNullOrEmpty(pData.tip_description_2))
		{
			string tLocalizedHotkeyText = LocalizedTextManager.getText(pData.tip_description_2, null, false);
			tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
			pTooltip.setBottomDescription(tLocalizedHotkeyText, null);
		}
	}

	// Token: 0x06003CEB RID: 15595 RVA: 0x001A7788 File Offset: 0x001A5988
	private void showDebugTraits(Tooltip pTooltip, ActorAsset pAsset)
	{
		pTooltip.addLineBreak();
		if (pAsset.default_language_traits != null)
		{
			pTooltip.addLineIntText("language_traits", pAsset.default_language_traits.Count, "#4CCFFF", false);
			using (List<string>.Enumerator enumerator = pAsset.default_language_traits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tID = enumerator.Current;
					pTooltip.addLineText("trait", tID, "#4CCFFF", false, false, 21);
				}
				goto IL_87;
			}
		}
		pTooltip.addLineText("language_traits", "-----", "#4CCFFF", false, false, 21);
		IL_87:
		if (pAsset.default_clan_traits != null)
		{
			pTooltip.addLineIntText("clan_traits", pAsset.default_clan_traits.Count, "#FF637D", false);
			using (List<string>.Enumerator enumerator = pAsset.default_clan_traits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tID2 = enumerator.Current;
					pTooltip.addLineText("trait", tID2, "#4CCFFF", false, false, 21);
				}
				goto IL_108;
			}
		}
		pTooltip.addLineText("clan_traits", "-----", "#FF637D", false, false, 21);
		IL_108:
		if (pAsset.default_culture_traits != null)
		{
			pTooltip.addLineIntText("culture_traits", pAsset.default_culture_traits.Count, "#35CC6E", false);
			using (List<string>.Enumerator enumerator = pAsset.default_culture_traits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tID3 = enumerator.Current;
					pTooltip.addLineText("trait", tID3, "#35CC6E", false, false, 21);
				}
				goto IL_189;
			}
		}
		pTooltip.addLineText("culture_traits", "-----", "#35CC6E", false, false, 21);
		IL_189:
		if (pAsset.default_religion_traits != null)
		{
			pTooltip.addLineIntText("religions_traits", pAsset.default_religion_traits.Count, "#8CFF99", false);
			using (List<string>.Enumerator enumerator = pAsset.default_religion_traits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tID4 = enumerator.Current;
					pTooltip.addLineText("trait", tID4, "#8CFF99", false, false, 21);
				}
				return;
			}
		}
		pTooltip.addLineText("religions_traits", "-----", "#8CFF99", false, false, 21);
	}

	// Token: 0x06003CEC RID: 15596 RVA: 0x001A79D8 File Offset: 0x001A5BD8
	private void showDebugTaxonomy(Tooltip pTooltip, ActorAsset pAsset)
	{
		pTooltip.addLineBreak();
		pTooltip.addLineText("kingdom", pAsset.getTaxonomyRank("taxonomy_kingdom"), ColorStyleLibrary.m.taxonomy_kingdom, false, false, 21);
		pTooltip.addLineText("phylum", pAsset.getTaxonomyRank("taxonomy_phylum"), ColorStyleLibrary.m.taxonomy_phylum, false, false, 21);
		pTooltip.addLineText("class", pAsset.getTaxonomyRank("taxonomy_class"), ColorStyleLibrary.m.taxonomy_class, false, false, 21);
		pTooltip.addLineText("order", pAsset.getTaxonomyRank("taxonomy_order"), ColorStyleLibrary.m.taxonomy_order, false, false, 21);
		pTooltip.addLineText("family", pAsset.getTaxonomyRank("taxonomy_family"), ColorStyleLibrary.m.taxonomy_family, false, false, 21);
		pTooltip.addLineText("genus", pAsset.getTaxonomyRank("taxonomy_genus"), ColorStyleLibrary.m.taxonomy_genus, false, false, 21);
		pTooltip.addLineText("species", pAsset.getTaxonomyRank("taxonomy_species"), ColorStyleLibrary.m.taxonomy_genus, false, false, 21);
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x001A7AE8 File Offset: 0x001A5CE8
	private void showDeadKingdom(Tooltip pTooltip, string pType, TooltipData pData)
	{
		DeadKingdom tKingdom = (DeadKingdom)pData.kingdom;
		pTooltip.setSpeciesIcon(tKingdom.getSpeciesIcon());
		pTooltip.setTitle(tKingdom.name, "kingdom", tKingdom.getColor().color_text);
		this.setIconValue(pTooltip, "i_age", (float)tKingdom.getAge(), null, "#FF637D", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tKingdom.getPopulationPeople(), null, "#FF637D", false, "", '/');
		this.setIconValue(pTooltip, "i_army", (float)tKingdom.countTotalWarriors(), null, "#FF637D", false, "", '/');
		pTooltip.setDescription(tKingdom.getMotto(), null);
		pTooltip.addLineText("founded", tKingdom.getFoundedYear(), null, false, true, 21);
		pTooltip.addLineText("kingdom_died_at", tKingdom.getDiedYear(), "#FF637D", false, true, 21);
		pTooltip.addLineIntText("age", tKingdom.getAge(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("births", tKingdom.getTotalBirths(), null, true, 21);
		pTooltip.addLineIntText("deaths", tKingdom.getTotalDeaths(), null, true, 21);
		pTooltip.addLineIntText("kills", tKingdom.getTotalKills(), null, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineText("species", tKingdom.getActorAsset().getTranslatedName(), null, false, true, 21);
		KingdomBanner[] array = pTooltip.transform.FindAllRecursive<KingdomBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tKingdom);
		}
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x001A7C80 File Offset: 0x001A5E80
	private void showKingdom(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Kingdom tKingdom = pData.kingdom;
		pTooltip.setSpeciesIcon(tKingdom.getSpeciesIcon());
		string tColorHex = tKingdom.getColor().color_text;
		pTooltip.setTitle(tKingdom.name, "kingdom", tKingdom.getColor().color_text);
		pTooltip.transform.FindRecursive("Stats").gameObject.SetActive(true);
		this.setIconValue(pTooltip, "i_age", (float)tKingdom.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tKingdom.getPopulationPeople(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_army", (float)tKingdom.countTotalWarriors(), null, "", false, "", '/');
		pTooltip.setDescription(tKingdom.getMotto(), null);
		string tKingString = "-";
		if (tKingdom.hasKing())
		{
			tKingString = tKingdom.king.getName();
		}
		pTooltip.addLineText("village_statistics_king", tKingString, tColorHex, false, true, 21);
		if (tKingdom.hasKing())
		{
			pTooltip.addLineIntText("ruler_money", tKingdom.king.money, null, true);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineText("villages", tKingdom.cities.Count.ToText() + "/" + tKingdom.getMaxCities().ToText(), null, false, true, 21);
		pTooltip.addLineIntText("adults", tKingdom.countAdults(), null, true);
		pTooltip.addLineIntText("children", tKingdom.countChildren(), null, true);
		pTooltip.addLineIntText("families", tKingdom.countFamilies(), null, true);
		pTooltip.addLineIntText("happy", tKingdom.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("food", tKingdom.countTotalFood(), null, true);
		pTooltip.addLineBreak();
		string tCapitalString = "-";
		if (tKingdom.hasCapital())
		{
			tCapitalString = tKingdom.capital.name;
		}
		pTooltip.addLineText("kingdom_statistics_capital", tCapitalString, tColorHex, false, true, 21);
		if (tKingdom.hasKing() && tKingdom.king.hasClan())
		{
			pTooltip.addLineText("clan", tKingdom.king.clan.data.name, tKingdom.king.clan.getColor().color_text, false, true, 21);
		}
		if (tKingdom.hasCulture())
		{
			pTooltip.addLineText("culture", tKingdom.culture.data.name, tKingdom.culture.getColor().color_text, false, true, 21);
		}
		if (tKingdom.hasLanguage())
		{
			pTooltip.addLineText("language", tKingdom.language.data.name, tKingdom.language.getColor().color_text, false, true, 21);
		}
		if (tKingdom.hasReligion())
		{
			pTooltip.addLineText("religion", tKingdom.religion.data.name, tKingdom.religion.getColor().color_text, false, true, 21);
		}
		Alliance tAlliance = tKingdom.getAlliance();
		if (tAlliance != null)
		{
			int tYears = Date.getYearsSince(tKingdom.data.timestamp_alliance);
			pTooltip.addLineText("alliance", tAlliance.data.name, tAlliance.getColor().color_text, false, true, 21);
			pTooltip.addLineIntText("kingdom_time_in_alliance", tYears, tAlliance.getColor().color_text, true);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("births", tKingdom.getTotalBirths(), null, true, 21);
		pTooltip.addLineIntText("deaths", tKingdom.getTotalDeaths(), null, true, 21);
		pTooltip.addLineIntText("kills", tKingdom.getTotalKills(), null, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineText("species", tKingdom.getActorAsset().getTranslatedName(), null, false, true, 21);
		KingdomBanner[] array = pTooltip.transform.FindAllRecursive<KingdomBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tKingdom);
		}
		TooltipKingdomTraitsRow tTraitsRow = pTooltip.GetComponentInChildren<TooltipKingdomTraitsRow>(true);
		if (tTraitsRow != null)
		{
			tTraitsRow.init(pTooltip, pData);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x001A8094 File Offset: 0x001A6294
	private void showStatus(Tooltip pTooltip, string pType, TooltipData pData)
	{
		StatBar tStatBar = pTooltip.transform.FindRecursive("TimeBar").GetComponent<StatBar>();
		StatusAsset tStatusAsset = pData.status.asset;
		if (!string.IsNullOrEmpty(pData.tip_name))
		{
			pTooltip.name.text = LocalizedTextManager.getText(pData.tip_name, null, false);
		}
		if (!string.IsNullOrEmpty(pData.tip_description))
		{
			pTooltip.setDescription(LocalizedTextManager.getText(pData.tip_description, null, false), null);
		}
		if (tStatusAsset == null)
		{
			return;
		}
		Status tStatus = pData.status;
		tStatBar.setBar((float)((int)tStatus.getRemainingTime()), tStatus.duration, "s", false, false, true, 0.3f);
		pTooltip.clearTextRows();
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tStatusAsset.base_stats, true);
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x001A8154 File Offset: 0x001A6354
	private void showOnomastics(Tooltip pTooltip, string pType, TooltipData pData)
	{
		OnomasticsAsset tAsset = pData.onomastics_asset;
		string localeID = tAsset.getLocaleID();
		string tDescriptionID = tAsset.getDescriptionID();
		string tSubnameID = tAsset.getIDSubname();
		string tTranslatedID = LocalizedTextManager.getText(localeID, null, false);
		pTooltip.setTitle(tTranslatedID, tSubnameID, tAsset.color_text);
		string tTempDescription = "";
		if (tAsset.isGroupType() && !pData.onomastics_data.isGroupEmpty(tAsset.id))
		{
			string tStringGroup = pData.onomastics_data.getGroup(tAsset.id).characters_string;
			tStringGroup = tStringGroup.ToLower();
			tTempDescription = tTempDescription + "[ " + Toolbox.coloredText(tStringGroup, tAsset.color_text, false) + " ]\n\n";
		}
		tTempDescription += LocalizedTextManager.getText(tDescriptionID, null, false);
		pTooltip.setDescription(tTempDescription, null);
		string tDescription2 = tAsset.getDescriptionID2();
		if (!string.IsNullOrEmpty(tDescription2))
		{
			string tTranslatedDescription2 = LocalizedTextManager.getText(tDescription2, null, false);
			pTooltip.setBottomDescription(tTranslatedDescription2, null);
		}
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x001A8238 File Offset: 0x001A6438
	private void showTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ActorTrait tTrait = pData.trait;
		this.showGenericInfoForTrait<ActorTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF2 RID: 15602 RVA: 0x001A825C File Offset: 0x001A645C
	private void showKingdomTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		KingdomTrait tTrait = pData.kingdom_trait;
		this.showGenericInfoForTrait<KingdomTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x001A8280 File Offset: 0x001A6480
	private void showCultureTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		CultureTrait tTrait = pData.culture_trait;
		this.showGenericInfoForTrait<CultureTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x001A82A4 File Offset: 0x001A64A4
	private void showLanguageTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		LanguageTrait tTrait = pData.language_trait;
		this.showGenericInfoForTrait<LanguageTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x001A82C8 File Offset: 0x001A64C8
	private void showSubspeciesTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		SubspeciesTrait tTrait = pData.subspecies_trait;
		this.showGenericInfoForTrait<SubspeciesTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF6 RID: 15606 RVA: 0x001A82EC File Offset: 0x001A64EC
	private void showClanTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ClanTrait tTrait = pData.clan_trait;
		this.showGenericInfoForTrait<ClanTrait>(pTooltip, pData, tTrait, new BaseStats[]
		{
			tTrait.base_stats_male,
			tTrait.base_stats_female
		});
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x001A8324 File Offset: 0x001A6524
	private void showReligionTrait(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ReligionTrait tTrait = pData.religion_trait;
		this.showGenericInfoForTrait<ReligionTrait>(pTooltip, pData, tTrait, Array.Empty<BaseStats>());
	}

	// Token: 0x06003CF8 RID: 15608 RVA: 0x001A8348 File Offset: 0x001A6548
	private void showGenericInfoForTrait<T>(Tooltip pTooltip, TooltipData pData, T pTrait, params BaseStats[] pAdditionalBaseStats) where T : BaseTrait<T>
	{
		this.showTraitOwners<T>(pTooltip, pTrait);
		bool tUnlocked = !pData.is_editor_augmentation_button || pTrait.isAvailable();
		Rarity tRarity = pTrait.rarity;
		string tRarityText = tRarity.getAsset().getLocaleID().Localize();
		string tTraitName = LocalizedTextManager.getText(tUnlocked ? pTrait.getLocaleID() : "achievement_tip_hidden", null, false);
		pTooltip.name.text = tTraitName;
		pTooltip.name.color = tRarity.getRarityColor();
		Text component = pTooltip.transform.Find("Icon and Info/Background/Rarity Type/Rarity Text").GetComponent<Text>();
		component.text = tRarityText;
		component.color = tRarity.getRarityColor();
		Image component2 = pTooltip.transform.Find("Icon and Info/IconBG/Icon").GetComponent<Image>();
		component2.sprite = pTrait.getSprite();
		component2.color = (tUnlocked ? Toolbox.color_white : Toolbox.color_black);
		pTooltip.transform.Find("Icon and Info/IconBG/LegendaryBG").gameObject.SetActive(tRarity == Rarity.R3_Legendary);
		pTooltip.transform.Find("Icon and Info/Background/IconedText").GetComponent<Text>().text = pTrait.getCountRows();
		GameObject tStars = pTooltip.transform.Find("Icon and Info/Background/Rarity Type/Rarity Stars").gameObject;
		int tRarityIndex = (int)tRarity;
		for (int i = 0; i < tStars.transform.childCount; i++)
		{
			Image tImage = tStars.transform.GetChild(i).gameObject.GetComponent<Image>();
			if (i <= tRarityIndex)
			{
				tImage.color = Toolbox.makeColor("#313131");
			}
			else
			{
				tImage.color = Color.black;
			}
		}
		string tTraitDescription = pTrait.getTranslatedDescription();
		if (!string.IsNullOrEmpty(tTraitDescription))
		{
			string tTempDescription = tTraitDescription;
			if (!pTrait.isAvailable() && pTrait.show_for_unlockables_ui)
			{
				if (pTrait.unlocked_with_achievement)
				{
					string tAchievementText = LocalizedTextManager.getText("trait_locked_tooltip_text_achievement", null, false).ColorHex(ColorStyleLibrary.m.color_text_grey, false);
					string tAchievementIdTranslated = "<color=#00ffffff>" + pTrait.getAchievementLocaleID().Localize() + "</color>";
					tAchievementText = tAchievementText.Replace("$achievement_id$", tAchievementIdTranslated);
					if (!pData.is_editor_augmentation_button)
					{
						tTempDescription = tTempDescription + "\n\n" + tAchievementText;
					}
					else
					{
						tTempDescription = tAchievementText;
					}
				}
				else
				{
					tTempDescription = LocalizedTextManager.getText(pTrait.typed_id + "_locked_tooltip_text_exploration", null, false);
				}
			}
			pTooltip.setDescription(tTempDescription, null);
		}
		else
		{
			pTooltip.resetDescription();
		}
		if (tUnlocked)
		{
			string tTraitDescription2 = pTrait.getTranslatedDescription2();
			pTooltip.setBottomDescription(tTraitDescription2, null);
		}
		if (!tUnlocked)
		{
			return;
		}
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, pTrait.base_stats, true);
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, pTrait.base_stats_meta, false);
		if (pAdditionalBaseStats != null)
		{
			foreach (BaseStats tAdditionalStat in pAdditionalBaseStats)
			{
				BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tAdditionalStat, false);
			}
		}
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x001A8658 File Offset: 0x001A6858
	private void showTraitOwners<T>(Tooltip pTooltip, T pTrait) where T : BaseTrait<T>
	{
		Transform tIconsParent = pTooltip.transform.FindRecursive("Species");
		if (pTrait.default_for_actor_assets == null)
		{
			tIconsParent.gameObject.SetActive(false);
			return;
		}
		tIconsParent.gameObject.SetActive(true);
		if (pTooltip.pool_icons == null)
		{
			StatsIcon tPrefab = Resources.Load<StatsIcon>("ui/PrefabTooltipTraitSpecies");
			pTooltip.pool_icons = new ObjectPoolGenericMono<StatsIcon>(tPrefab, tIconsParent);
		}
		foreach (ActorAsset tAsset in pTrait.default_for_actor_assets)
		{
			if (!tAsset.unit_zombie && tAsset.show_in_taxonomy_tooltip)
			{
				Image tIcon = pTooltip.pool_icons.getNext().getIcon();
				tIcon.sprite = tAsset.getSpriteIcon();
				if (tAsset.isAvailable())
				{
					tIcon.color = Toolbox.color_white;
				}
				else
				{
					tIcon.color = Toolbox.color_black;
				}
			}
		}
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x001A8754 File Offset: 0x001A6954
	private void showChromosome(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Chromosome tChromosome = pData.chromosome;
		ChromosomeTypeAsset asset = tChromosome.getAsset();
		string tLocaleKey = asset.getLocaleID();
		pTooltip.name.GetComponent<LocalizedText>().setKeyAndUpdate(tLocaleKey);
		string tDescription = LocalizedTextManager.getText(asset.getDescriptionID(), null, false);
		pTooltip.setDescription(tDescription, null);
		pTooltip.addLineText("genes", tChromosome.countNonEmpty().ToString() + "/" + tChromosome.genes.Count.ToString(), null, false, true, 21);
		pTooltip.addLineBreak();
		BaseStats tStats = BaseStatsHelper.getTotalStatsFrom(tChromosome.getStats(), tChromosome.getStatsMeta());
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tStats, true);
	}

	// Token: 0x06003CFB RID: 15611 RVA: 0x001A8804 File Offset: 0x001A6A04
	private void showGene(Tooltip pTooltip, string pType, TooltipData pData)
	{
		GeneAsset tGene = pData.gene;
		LocusElement tLocus = pData.locus;
		Chromosome tChromosome = pData.chromosome;
		bool tAmplifier = tLocus != null && tLocus.isAmplifier();
		bool flag = tGene.isAvailable();
		if (tChromosome != null)
		{
			tChromosome.isVoidLocus(tLocus.locus_index);
		}
		string tLocalizedName;
		if (!flag)
		{
			tLocalizedName = LocalizedTextManager.getText("achievement_tip_hidden", null, false);
			if (tGene.unlocked_with_achievement)
			{
				string tText = LocalizedTextManager.getText("gene_locked_tooltip_text_achievement", null, false);
				string tAchievementIdTranslated = "<color=#00ffffff>" + tGene.getAchievementLocaleID().Localize() + "</color>";
				tText = tText.Replace("$achievement_id$", tAchievementIdTranslated);
				pTooltip.setDescription(tText, null);
			}
			else
			{
				pTooltip.setDescription(LocalizedTextManager.getText("gene_locked_tooltip_text_exploration", null, false), null);
			}
			pTooltip.transform.FindRecursive("Stats").gameObject.SetActive(false);
		}
		else
		{
			tLocalizedName = LocalizedTextManager.getText(tGene.getLocaleID(), null, false);
			string tDescription = "";
			string tLocaleDescriptionKey = tGene.getDescriptionID();
			if (LocalizedTextManager.stringExists(tLocaleDescriptionKey))
			{
				string tLocalizedDescription = LocalizedTextManager.getText(tLocaleDescriptionKey, null, false);
				tDescription = tDescription + tLocalizedDescription + "\n";
			}
			pTooltip.setDescription(tDescription, null);
		}
		if (tLocus != null)
		{
			string tTextTitle = string.Empty;
			string tTextSubTitle = string.Empty;
			if (tLocus.isAmplifierBad())
			{
				tTextTitle = LocalizedTextManager.getText("amplifier_bad", null, false);
				string tLocalizedDescription2 = LocalizedTextManager.getText("amplifier_bad_description", null, false);
				pTooltip.setDescription(tLocalizedDescription2, null);
			}
			else if (tLocus.isAmplifier())
			{
				tTextTitle = LocalizedTextManager.getText("amplifier", null, false);
				string tLocalizedDescription3 = LocalizedTextManager.getText("amplifier_description", null, false);
				pTooltip.setDescription(tLocalizedDescription3, null);
			}
			else
			{
				tTextTitle = tLocalizedName;
			}
			tTextSubTitle = "locus";
			pTooltip.setTitle(tTextTitle, tTextSubTitle, "#F3961F");
		}
		else
		{
			pTooltip.setTitle(tLocalizedName, "gene", "#F3961F");
		}
		string tBottom = "";
		if (flag && !tGene.is_empty)
		{
			this._base_stats_temp.clear();
			if (tLocus != null && tChromosome != null)
			{
				tChromosome.fillStatsForTooltip(tLocus, this._base_stats_temp);
			}
			else
			{
				this._base_stats_temp.mergeStats(tGene.base_stats, 1f);
			}
			if (tChromosome != null && !tAmplifier)
			{
				tBottom += tChromosome.getSynergyTooltipText(tLocus.locus_index);
			}
			BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, this._base_stats_temp, true);
		}
		if (!tAmplifier)
		{
			tBottom = tBottom + LocalizedTextManager.getText("dna_sequence", null, false) + "\n" + tGene.getSequence();
		}
		pTooltip.setBottomDescription(tBottom, null);
	}

	// Token: 0x06003CFC RID: 15612 RVA: 0x001A8A80 File Offset: 0x001A6C80
	private void showGeneDNASequence(Tooltip pTooltip, string pType, TooltipData pData)
	{
		GeneAsset tGene = pData.gene;
		Chromosome tChromosome = pData.chromosome;
		LocusElement tLocus = pData.locus;
		bool tAmplifier = false;
		if (tLocus != null)
		{
			tAmplifier = tLocus.isAmplifier();
		}
		bool flag = tGene.isAvailable();
		string tBottom = "";
		if (flag && !tAmplifier && tLocus != null && tChromosome != null)
		{
			tBottom += tChromosome.getSynergyTooltipText(tLocus.locus_index);
		}
		if (!tAmplifier)
		{
			tBottom = tBottom + LocalizedTextManager.getText("dna_sequence", null, false) + "\n" + tGene.getSequence();
		}
		pTooltip.setBottomDescription(tBottom, null);
	}

	// Token: 0x06003CFD RID: 15613 RVA: 0x001A8B14 File Offset: 0x001A6D14
	private void showKingdomAsset(Tooltip pTooltip, string pType, TooltipData pData)
	{
		KingdomAsset tAsset = pData.kingdom_asset;
		pTooltip.name.text = tAsset.id;
		string tDescription;
		string tDescription2;
		DebugKingdomButton.getTooltipDescription(tAsset, out tDescription, out tDescription2);
		pTooltip.setDescription(tDescription, null);
		if (!string.IsNullOrEmpty(tDescription2))
		{
			pTooltip.setBottomDescription(tDescription2, null);
		}
		pTooltip.tryShowBoolDebug("civ", tAsset.civ);
		pTooltip.tryShowBoolDebug("nomads", tAsset.nomads);
		pTooltip.tryShowBoolDebug("nature", tAsset.nature);
		pTooltip.tryShowBoolDebug("mobs", tAsset.mobs);
		pTooltip.tryShowBoolDebug("miniciv", tAsset.group_miniciv);
		pTooltip.tryShowBoolDebug("neutral", tAsset.neutral);
		pTooltip.tryShowBoolDebug("brain", tAsset.brain);
		pTooltip.tryShowBoolDebug("always_attack_each_other", tAsset.always_attack_each_other);
		pTooltip.tryShowBoolDebug("units_always_aggressive", tAsset.units_always_looking_for_enemies);
	}

	// Token: 0x06003CFE RID: 15614 RVA: 0x001A8BF4 File Offset: 0x001A6DF4
	private void showTip(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (LocalizedTextManager.stringExists(pData.tip_name))
		{
			pTooltip.name.text = LocalizedTextManager.getText(pData.tip_name, null, false);
		}
		else
		{
			pTooltip.name.text = pData.tip_name;
		}
		if (!string.IsNullOrEmpty(pData.tip_description))
		{
			string tTempDescription = LocalizedTextManager.getText(pData.tip_description, null, false);
			if (tTempDescription.Contains("$favorite_food$"))
			{
				string tFood = "??";
				if (SelectedUnit.unit.hasFavoriteFood())
				{
					tFood = LocalizedTextManager.getText(SelectedUnit.unit.data.favorite_food, null, false);
				}
				tTempDescription = tTempDescription.Replace("$favorite_food$", tFood);
				tTempDescription += "\n";
				tTempDescription += "\n";
				tTempDescription = tTempDescription + LocalizedTextManager.getText("food_consumed", null, false) + ": " + SelectedUnit.unit.data.food_consumed.ToString();
			}
			pTooltip.setDescription(tTempDescription, null);
		}
		if ((Config.isComputer || Config.isEditor) && !string.IsNullOrEmpty(pData.tip_description_2))
		{
			string tLocalizedHotkeyText = LocalizedTextManager.getText(pData.tip_description_2, null, false);
			tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
			pTooltip.setBottomDescription(tLocalizedHotkeyText, null);
		}
	}

	// Token: 0x06003CFF RID: 15615 RVA: 0x001A8D28 File Offset: 0x001A6F28
	private void showTipZoneMode(Tooltip pTooltip, string pType, TooltipData pData)
	{
		OptionAsset tOptionAsset = AssetManager.meta_type_library.getFromPower(pData.tip_name).option_asset;
		string tLocalizedName = pData.tip_name.Localize();
		string tZoneMode = "";
		if (tOptionAsset.multi_toggle)
		{
			tZoneMode = tOptionAsset.getOptionLocaleID();
		}
		pTooltip.setTitle(tLocalizedName, tZoneMode, "#F3961F");
		if (!string.IsNullOrEmpty(pData.tip_description))
		{
			string tTempDescription = LocalizedTextManager.getText(pData.tip_description, null, false);
			string tBordersString = this.getStateText("borders_state_tip", Zones.isBordersEnabled());
			string tNamesString = this.getStateText("map_names_state_tip", Zones.showMapNames());
			tTempDescription = string.Concat(new string[]
			{
				tTempDescription,
				"\n\n",
				tBordersString,
				", ",
				tNamesString
			});
			pTooltip.setDescription(tTempDescription, null);
		}
		if ((Config.isComputer || Config.isEditor) && !string.IsNullOrEmpty(pData.tip_description_2))
		{
			string tLocalizedHotkeyText = LocalizedTextManager.getText(pData.tip_description_2, null, false);
			tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
			pTooltip.setBottomDescription(tLocalizedHotkeyText, null);
		}
	}

	// Token: 0x06003D00 RID: 15616 RVA: 0x001A8E2C File Offset: 0x001A702C
	private string getStateText(string pLocale, bool pState)
	{
		string tStateLocalized = (pState ? "short_on" : "short_off").ColorHex(pState ? "#95DD5D" : "#FF8686", true);
		return LocalizedTextManager.getText(pLocale, null, false).Replace("$state$", tStateLocalized);
	}

	// Token: 0x06003D01 RID: 15617 RVA: 0x001A8E74 File Offset: 0x001A7074
	private void showCarryingResources(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Actor actor = pData.actor;
		pTooltip.name.text = pData.tip_name.Localize();
		foreach (KeyValuePair<string, ResourceContainer> tResSlot in actor.inventory.getResources())
		{
			ResourceAsset tAsset = tResSlot.Value.asset;
			int tAmount = tResSlot.Value.amount;
			pTooltip.addLineIntText(tAsset.getLocaleID(), tAmount, "#43FF43", true);
		}
	}

	// Token: 0x06003D02 RID: 15618 RVA: 0x001A8F14 File Offset: 0x001A7114
	private void showPastNames(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ListPool<NameEntry> tList = pData.past_names;
		pTooltip.name.text = pData.tip_name.Localize();
		if (tList == null)
		{
			return;
		}
		if (tList.Count == 0)
		{
			return;
		}
		MetaCustomizationColorLibrary color_library = AssetManager.meta_customization_library.getAsset(pData.meta_type).color_library;
		ColorLibrary tColorLibrary = (color_library != null) ? color_library() : null;
		foreach (NameEntry ptr in tList)
		{
			NameEntry tName = ptr;
			if (!string.IsNullOrEmpty(tName.name))
			{
				string tPastName = tName.name;
				string tYear = Date.getYearDate(tName.timestamp);
				string tColor = null;
				if (tName.custom)
				{
					tYear = "* " + tYear;
				}
				if (tName.color_id > -1 && tColorLibrary != null)
				{
					tColor = Toolbox.colorToHex(tColorLibrary.list[tName.color_id].getColorText(), true);
					tPastName = Toolbox.coloredText(tPastName, tColor, false);
				}
				pTooltip.addLineText(tPastName, tYear, tColor, false, false, 21);
			}
		}
	}

	// Token: 0x06003D03 RID: 15619 RVA: 0x001A9038 File Offset: 0x001A7238
	private void showPastRulers(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ListPool<LeaderEntry> tList = pData.past_rulers;
		pTooltip.name.text = pData.tip_name.Localize();
		if (tList == null)
		{
			return;
		}
		if (tList.Count == 0)
		{
			return;
		}
		MetaCustomizationColorLibrary color_library = AssetManager.meta_customization_library.getAsset(pData.meta_type).color_library;
		ColorLibrary tColorLibrary = (color_library != null) ? color_library() : null;
		int tLastRulerStart = Date.getCurrentYear();
		for (int i = tList.Count - 1; i >= 0; i--)
		{
			LeaderEntry tEntry = tList[i];
			string tRulerName = tEntry.name;
			bool tDead = false;
			Actor tRuler = World.world.units.get(tEntry.id);
			if (!tRuler.isRekt())
			{
				tRulerName = tRuler.name;
			}
			else
			{
				tDead = true;
			}
			if (string.IsNullOrEmpty(tRulerName))
			{
				tRulerName = LocalizedTextManager.getText("unknown", null, false);
			}
			if (tDead)
			{
				tRulerName = "† " + tRulerName;
			}
			if (tEntry.color_id > -1 && tColorLibrary != null)
			{
				string tRulerColor = Toolbox.colorToHex(tColorLibrary.list[tEntry.color_id].getColorText(), false);
				tRulerName = Toolbox.coloredText(tRulerName, tRulerColor, false);
			}
			int tStartYear = Date.getYear(tEntry.timestamp_ago);
			int tEndYear = Date.getYear(tEntry.timestamp_end);
			if (tEntry.timestamp_end < tEntry.timestamp_ago)
			{
				tEndYear = tLastRulerStart;
			}
			tLastRulerStart = tStartYear;
			int tTotalYears = tEndYear - tStartYear;
			string tLineResult = string.Format("{0}–{1} ({2} {3})", new object[]
			{
				tStartYear,
				tEndYear,
				tTotalYears,
				"y"
			});
			pTooltip.addLineText(tRulerName, tLineResult, null, false, false, 21);
		}
	}

	// Token: 0x06003D04 RID: 15620 RVA: 0x001A91E0 File Offset: 0x001A73E0
	private void showMass(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Actor actor = pData.actor;
		pTooltip.name.text = pData.tip_name.Localize();
		foreach (ResourceContainer tResource in actor.getResourcesFromActor())
		{
			ResourceAsset tAsset = AssetManager.resources.get(tResource.id);
			pTooltip.addLineIntText(tAsset.getLocaleID(), tResource.amount, "#43FF43", true);
		}
	}

	// Token: 0x06003D05 RID: 15621 RVA: 0x001A926C File Offset: 0x001A746C
	private void showPassengers(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Actor actor = pData.actor;
		pTooltip.name.text = LocalizedTextManager.getText("passengers", null, false);
		TooltipIconsRow tPassengersIcons = pTooltip.transform.FindRecursive("Passengers").GetComponent<TooltipIconsRow>();
		Boat tBoat = actor.getSimpleComponent<Boat>();
		this.showBoatPassengers(tBoat, tPassengersIcons, pTooltip, pData);
	}

	// Token: 0x06003D06 RID: 15622 RVA: 0x001A92BC File Offset: 0x001A74BC
	private void showLoyalty(Tooltip pTooltip, string pType, TooltipData pData)
	{
		pTooltip.name.text = LocalizedTextManager.getText("loyalty", null, false);
		int tTotal = pData.city.getLoyalty(true);
		if (tTotal > 0)
		{
			pTooltip.addLineIntText("opinion_total", tTotal, "#43FF43", true);
		}
		else
		{
			pTooltip.addLineIntText("opinion_total", tTotal, "#FB2C21", true);
		}
		foreach (LoyaltyAsset tAsset in LoyaltyCalculator.results.Keys)
		{
			int tResult = LoyaltyCalculator.results[tAsset];
			string tTranslationKey = tAsset.getTranslationKey(tResult);
			pTooltip.addOpinion(new TooltipOpinionInfo(tTranslationKey, tResult));
		}
		Text stats_description = pTooltip.stats_description;
		stats_description.text += "\n------------";
		Text stats_values = pTooltip.stats_values;
		stats_values.text += "\n------------";
		pTooltip.addLineBreak();
		pTooltip.addLineBreak();
	}

	// Token: 0x06003D07 RID: 15623 RVA: 0x001A93C0 File Offset: 0x001A75C0
	private void showArmy(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Army tArmy = pData.army;
		Kingdom tKingdom = tArmy.getKingdom();
		City tCity = tArmy.getCity();
		pTooltip.setTitle(tArmy.name, "army", tArmy.getColor().color_text);
		pTooltip.setSpeciesIcon(tArmy.getActorAsset().getSpriteIcon());
		this.setIconValue(pTooltip, "i_age", (float)tArmy.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tArmy.countUnits(), null, "", false, "", '/');
		if (!tKingdom.isRekt())
		{
			pTooltip.addLineText("kingdom", tKingdom.name, tKingdom.getColor().color_text, false, true, 21);
		}
		if (!tCity.isRekt())
		{
			pTooltip.addLineText("villages", tCity.name, tCity.getColor().color_text, false, true, 21);
		}
		if (tArmy.hasCaptain())
		{
			pTooltip.addLineText("captain", tArmy.getCaptain().getName(), tArmy.getColor().color_text, false, true, 21);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("males", tArmy.countMales(), null, true);
		pTooltip.addLineIntText("females", tArmy.countFemales(), null, true);
		pTooltip.addLineIntText("happy", tArmy.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kills", tArmy.getTotalKills(), null, true, 21);
		pTooltip.addLineIntText("deaths", tArmy.getTotalDeaths(), null, true, 21);
		pTooltip.addLineIntText("renown", tArmy.getRenown(), null, true);
		foreach (KingdomBanner tBanner in pTooltip.transform.FindAllRecursive<KingdomBanner>())
		{
			if (tBanner.gameObject.activeSelf)
			{
				tBanner.load(tArmy.getKingdom());
			}
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D08 RID: 15624 RVA: 0x001A95AC File Offset: 0x001A77AC
	private void showSubspecies(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Subspecies tSubspecies = pData.subspecies;
		pTooltip.setTitle(tSubspecies.name, "subspecies_singular", tSubspecies.getColor().color_text);
		pTooltip.setSpeciesIcon(tSubspecies.getActorAsset().getSpriteIcon());
		this.setIconValue(pTooltip, "i_age", (float)tSubspecies.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tSubspecies.countUnits(), null, "", false, "", '/');
		pTooltip.GetComponentInChildren<TooltipSubspeciesTraitsRow>(true).init(pTooltip, pData);
		pTooltip.addLineIntText("adults", tSubspecies.countAdults(), null, true);
		pTooltip.addLineIntText("children", tSubspecies.countChildren(), null, true);
		pTooltip.addLineIntText("happy", tSubspecies.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kings", tSubspecies.countKings(), null, true);
		pTooltip.addLineIntText("leaders", tSubspecies.countLeaders(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("births", tSubspecies.getTotalBirths(), null, true, 21);
		pTooltip.addLineIntText("deaths", tSubspecies.getTotalDeaths(), null, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("families", tSubspecies.countCurrentFamilies(), null, true);
		foreach (SubspeciesBanner tBanner in pTooltip.transform.FindAllRecursive<SubspeciesBanner>())
		{
			if (tBanner.gameObject.activeSelf)
			{
				tBanner.load(tSubspecies);
			}
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D09 RID: 15625 RVA: 0x001A9738 File Offset: 0x001A7938
	private void showFamily(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Family tFamily = pData.family;
		ActorAsset tAsset = tFamily.getActorAsset();
		pTooltip.setSpeciesIcon(tAsset.getSpriteIcon());
		pTooltip.setTitle(tFamily.name, "family", tFamily.getColor().color_text);
		int tAge = tFamily.getAge();
		this.setIconValue(pTooltip, "i_age", (float)tAge, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tFamily.countUnits(), null, "", false, "", '/');
		pTooltip.addLineIntText("adults", tFamily.countAdults(), null, true);
		pTooltip.addLineIntText("children", tFamily.countChildren(), null, true);
		pTooltip.addLineIntText("happy", tFamily.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("births", tFamily.getTotalBirths(), null, true, 21);
		pTooltip.addLineIntText("deaths", tFamily.getTotalDeaths(), null, true, 21);
		FamilyBanner[] array = pTooltip.transform.FindAllRecursive<FamilyBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tFamily);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D0A RID: 15626 RVA: 0x001A986C File Offset: 0x001A7A6C
	private void showLanguage(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Language tLanguage = pData.language;
		pTooltip.setSpeciesIcon(tLanguage.getActorAsset().getSpriteIcon());
		pTooltip.setTitle(tLanguage.name, "language", tLanguage.getColor().color_text);
		this.setIconValue(pTooltip, "i_age", (float)tLanguage.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tLanguage.countUnits(), null, "", false, "", '/');
		pTooltip.GetComponentInChildren<TooltipLanguageTraitsRow>(true).init(pTooltip, pData);
		if (!string.IsNullOrEmpty(tLanguage.data.creator_city_name))
		{
			pTooltip.addLineText("founded_in", tLanguage.data.creator_city_name, null, false, true, 21);
			pTooltip.addLineBreak();
		}
		pTooltip.addLineIntText("kingdoms", tLanguage.countKingdoms(), null, true);
		pTooltip.addLineIntText("villages", tLanguage.countCities(), null, true);
		pTooltip.addLineIntText("books", tLanguage.books.count(), null, true);
		pTooltip.addLineIntText("books_written", tLanguage.countWrittenBooks(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("adults", tLanguage.countAdults(), null, true);
		pTooltip.addLineIntText("children", tLanguage.countChildren(), null, true);
		pTooltip.addLineIntText("happy", tLanguage.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kings", tLanguage.countKings(), null, true);
		pTooltip.addLineIntText("leaders", tLanguage.countLeaders(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tLanguage.getTotalDeaths(), null, true, 21);
		LanguageBanner[] array = pTooltip.transform.FindAllRecursive<LanguageBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tLanguage);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D0B RID: 15627 RVA: 0x001A9A44 File Offset: 0x001A7C44
	private void showReligion(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Religion tReligion = pData.religion;
		pTooltip.setSpeciesIcon(tReligion.getActorAsset().getSpriteIcon());
		pTooltip.setTitle(tReligion.name, "religion", tReligion.getColor().color_text);
		int tAge = tReligion.getAge();
		this.setIconValue(pTooltip, "i_age", (float)tAge, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tReligion.countUnits(), null, "", false, "", '/');
		if (!string.IsNullOrEmpty(tReligion.data.creator_city_name))
		{
			pTooltip.addLineText("founded_in", tReligion.data.creator_city_name, null, false, true, 21);
			pTooltip.addLineBreak();
		}
		pTooltip.addLineIntText("kingdoms", tReligion.countKingdoms(), null, true);
		pTooltip.addLineIntText("villages", tReligion.countCities(), null, true);
		pTooltip.addLineIntText("books", tReligion.books.count(), null, true);
		pTooltip.addLineIntText("happy", tReligion.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("adults", tReligion.countAdults(), null, true);
		pTooltip.addLineIntText("children", tReligion.countChildren(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kings", tReligion.countKings(), null, true);
		pTooltip.addLineIntText("leaders", tReligion.countLeaders(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tReligion.getTotalDeaths(), null, true, 21);
		ReligionBanner[] array = pTooltip.transform.FindAllRecursive<ReligionBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tReligion);
		}
		pTooltip.GetComponentInChildren<TooltipReligionTraitsRow>(true).init(pTooltip, pData);
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x001A9C10 File Offset: 0x001A7E10
	private void showCulture(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Culture tCulture = pData.culture;
		pTooltip.setSpeciesIcon(tCulture.getActorAsset().getSpriteIcon());
		pTooltip.setTitle(tCulture.name, "culture", tCulture.getColor().color_text);
		this.setIconValue(pTooltip, "i_age", (float)tCulture.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tCulture.countUnits(), null, "", false, "", '/');
		pTooltip.GetComponentInChildren<TooltipCultureTraitsRow>(true).init(pTooltip, pData);
		if (!string.IsNullOrEmpty(tCulture.data.creator_city_name))
		{
			pTooltip.addLineText("founded_in", tCulture.data.creator_city_name, null, false, true, 21);
			pTooltip.addLineBreak();
		}
		pTooltip.addLineIntText("kingdoms", tCulture.countKingdoms(), null, true);
		pTooltip.addLineIntText("villages", tCulture.countCities(), null, true);
		pTooltip.addLineIntText("books", tCulture.books.count(), null, true);
		pTooltip.addLineIntText("happy", tCulture.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("adults", tCulture.countAdults(), null, true);
		pTooltip.addLineIntText("children", tCulture.countChildren(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kings", tCulture.countKings(), null, true);
		pTooltip.addLineIntText("leaders", tCulture.countLeaders(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tCulture.getTotalDeaths(), null, true, 21);
		CultureBanner[] array = pTooltip.transform.FindAllRecursive<CultureBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tCulture);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D0D RID: 15629 RVA: 0x001A9DD4 File Offset: 0x001A7FD4
	private void showPlot(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Plot tPlot = pData.plot;
		ColorAsset color = tPlot.getColor();
		string tColorHex = (color != null) ? color.color_text : null;
		if (string.IsNullOrEmpty(tColorHex))
		{
			tColorHex = "#F3961F";
		}
		pTooltip.setTitle(tPlot.name, "plot", tColorHex);
		int progressPercentage = tPlot.getProgressPercentage();
		int tAge = tPlot.getAge();
		string tProgress = progressPercentage.ToText() + "%";
		string tSupporters = tPlot.getSupporters().ToText() + "/" + tPlot.getMaxSupporters().ToText();
		pTooltip.addDescription(tPlot.getAsset().get_formatted_description(tPlot), null);
		pTooltip.addLineText("started_at", tPlot.getFoundedDate(), null, false, true, 21);
		string tLeaderColor = tColorHex;
		Actor tLeader = tPlot.getAuthor();
		if (tLeader != null)
		{
			tLeaderColor = tLeader.kingdom.getColor().color_text;
		}
		pTooltip.addLineText("started_by", tPlot.data.founder_name, tLeaderColor, false, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("tip_plot_age", tAge, null, true);
		pTooltip.addLineText("tip_plot_progress", tProgress, null, false, true, 21);
		pTooltip.addLineText("tip_plot_members", tSupporters, null, false, true, 21);
		PlotBanner[] componentsInChildren = pTooltip.transform.GetComponentsInChildren<PlotBanner>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].load(tPlot);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D0E RID: 15630 RVA: 0x001A9F34 File Offset: 0x001A8134
	private void showPlotInEditor(Tooltip pTooltip, string pType, TooltipData pData)
	{
		PlotAsset tAsset = pData.plot_asset;
		string tDescriptionLocale;
		string tTitleLocale;
		if (tAsset.isAvailable())
		{
			tDescriptionLocale = tAsset.getDescriptionID2();
			tTitleLocale = tAsset.getLocaleID();
		}
		else
		{
			tDescriptionLocale = "plot_locked_tooltip_text_exploration";
			tTitleLocale = "achievement_tip_hidden";
		}
		string tTitle = LocalizedTextManager.getText(tTitleLocale, null, false);
		pTooltip.setTitle(tTitle, "", "#F3961F");
		string tDescription = LocalizedTextManager.getText(tDescriptionLocale, null, false);
		pTooltip.addDescription(tDescription, null);
		Sprite tSprite = tAsset.getSprite();
		foreach (Image tImage in pTooltip.transform.Find("Headline/icons").GetComponentsInChildren<Image>(true))
		{
			tImage.sprite = tSprite;
			if (tAsset.isAvailable())
			{
				tImage.color = Toolbox.color_white;
			}
			else
			{
				tImage.color = Toolbox.color_black;
			}
		}
	}

	// Token: 0x06003D0F RID: 15631 RVA: 0x001AA00A File Offset: 0x001A820A
	private void showCityHome(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showCity("creature_statistics_home_village", pTooltip, pData);
	}

	// Token: 0x06003D10 RID: 15632 RVA: 0x001AA019 File Offset: 0x001A8219
	private void showCityCapital(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showCity("kingdom_statistics_capital", pTooltip, pData);
	}

	// Token: 0x06003D11 RID: 15633 RVA: 0x001AA028 File Offset: 0x001A8228
	private void showCity(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showCity("village", pTooltip, pData);
	}

	// Token: 0x06003D12 RID: 15634 RVA: 0x001AA038 File Offset: 0x001A8238
	private void showHappiness(Tooltip pTooltip, string pType, TooltipData pData)
	{
		pData.actor = SelectedUnit.unit;
		pTooltip.name.GetComponent<LocalizedText>().setKeyAndUpdate("happiness");
		if (!pData.actor.hasHappinessHistory())
		{
			return;
		}
		using (ListPool<HappinessHistory> tTempListPool = new ListPool<HappinessHistory>(pData.actor.happiness_change_history))
		{
			tTempListPool.Reverse();
			pTooltip.addLineText("happiness_current", pData.actor.getHappiness().ToText() + string.Format(" ({0}%)", pData.actor.getHappinessPercent()), null, false, true, 21);
			pTooltip.addLineBreak();
			for (int i = 0; i < tTempListPool.Count; i++)
			{
				int bonus = tTempListPool[i].bonus;
				HappinessAsset tAsset = tTempListPool[i].asset;
				int tFinalValue = bonus + tAsset.value;
				string tTranslatedText = LocalizedTextManager.getText(tAsset.getLocaleID(), null, false);
				tTranslatedText = Toolbox.coloredString(tTempListPool[i].getAgoString(), ColorStyleLibrary.m.color_text_grey_dark) + ": " + tTranslatedText;
				if (tFinalValue >= 0)
				{
					pTooltip.addLineText(tTranslatedText, tFinalValue.ToString("+##,#;-##,#;0"), "#43FF43", false, false, 21);
				}
				else
				{
					pTooltip.addLineText(tTranslatedText, tFinalValue.ToString("+##,#;-##,#;0"), "#FB2C21", false, false, 21);
				}
			}
		}
	}

	// Token: 0x06003D13 RID: 15635 RVA: 0x001AA1B0 File Offset: 0x001A83B0
	private void showCity(string pTitleID, Tooltip pTooltip, TooltipData pData)
	{
		City tCity = pData.city;
		pTooltip.setSpeciesIcon(tCity.getCurrentSpeciesIcon());
		Kingdom tKingdom = tCity.kingdom;
		string tColorHex = tKingdom.getColor().color_text;
		int tAge = tCity.getAge();
		this.setIconValue(pTooltip, "i_age", (float)tAge, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tCity.getPopulationPeople(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_army", (float)tCity.countWarriors(), null, "", false, "", '/');
		pTooltip.addLineText("books", tCity.countBooks().ToString() + "/" + tCity.countBookSlots().ToString(), null, false, true, 21);
		pTooltip.setTitle(tCity.name, pTitleID, tColorHex);
		string tKingString = "-";
		if (tKingdom.hasKing())
		{
			tKingString = tKingdom.king.getName();
		}
		string tLeaderString = "-";
		if (tCity.hasLeader())
		{
			tLeaderString = tCity.leader.getName();
		}
		pTooltip.addLineText("village_statistics_leader", tLeaderString, tColorHex, false, true, 21);
		if (tCity.hasLeader())
		{
			pTooltip.addLineIntText("ruler_money", tCity.leader.money, null, true);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("adults", tCity.countAdults(), null, true);
		pTooltip.addLineIntText("children", tCity.countChildren(), null, true);
		pTooltip.addLineIntText("families", tCity.countFamilies(), null, true);
		pTooltip.addLineIntText("happy", tCity.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		if (!tCity.kingdom.isNeutral())
		{
			pTooltip.addLineText("kingdom", tCity.kingdom.name, tColorHex, false, true, 21);
		}
		pTooltip.addLineText("village_statistics_king", tKingString, tColorHex, false, true, 21);
		if (tCity.hasLeader() && tCity.leader.hasClan())
		{
			pTooltip.addLineText("clan", tCity.leader.clan.name, tCity.leader.clan.getColor().color_text, false, true, 21);
		}
		if (tCity.hasCulture())
		{
			pTooltip.addLineText("culture", tCity.culture.name, tCity.culture.getColor().color_text, false, true, 21);
		}
		if (tCity.hasReligion())
		{
			pTooltip.addLineText("religion", tCity.religion.name, tCity.religion.getColor().color_text, false, true, 21);
		}
		if (tCity.hasLanguage())
		{
			pTooltip.addLineText("language", tCity.language.name, tCity.language.getColor().color_text, false, true, 21);
		}
		Alliance tAlliance = tKingdom.getAlliance();
		if (tAlliance != null)
		{
			int tYears = Date.getYearsSince(tKingdom.data.timestamp_alliance);
			pTooltip.addLineText("alliance", tAlliance.data.name, tAlliance.getColor().color_text, false, true, 21);
			pTooltip.addLineIntText("kingdom_time_in_alliance", tYears, tAlliance.getColor().color_text, true);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("houses", tCity.getHouseCurrent(), null, true);
		pTooltip.addLineIntText("area", tCity.zones.Count, null, true);
		pTooltip.addLineIntText("loyalty", tCity.getCachedLoyalty(), null, true);
		pTooltip.addLineIntText("books", tCity.countBooks(), null, true);
		pTooltip.GetComponentInChildren<CityBanner>().load(tCity);
		if (DebugConfig.isOn(DebugOption.DebugCityReproduction))
		{
			pTooltip.addLineBreak();
			pTooltip.addLineIntText("males_single", tCity.countSingleMales(), "#4CCFFF", true);
			pTooltip.addLineIntText("females_single", tCity.countSingleFemales(), "#FF637D", true);
			pTooltip.addLineIntText("couples", tCity.countCouples(), null, true);
			pTooltip.addLineText("male/female", tCity.countMales().ToText() + "/" + tCity.countFemales().ToText(), "#FF637D", false, false, 21);
			pTooltip.addLineText("adults/kids", tCity.countAdults().ToText() + " | " + tCity.countChildren().ToText(), null, false, false, 21);
			pTooltip.addLineIntText("pot_par_males", tCity.countPotentialParents(ActorSex.Male), null, false);
			pTooltip.addLineIntText("pot_par_females", tCity.countPotentialParents(ActorSex.Female), null, false);
			pTooltip.addLineBreak();
			pTooltip.addLineIntText("hungry", tCity.countHungry(), "#FF637D", false);
			pTooltip.addLineIntText("starving", tCity.countStarving(), "#FF637D", false);
			pTooltip.addLineIntText("food", tCity.countFoodTotal(), null, true);
			pTooltip.addLineIntText("afteglows", tCity.countUnitsWithStatus("afterglow"), null, false);
			pTooltip.addLineIntText("pregnant", tCity.countUnitsWithStatus("pregnant"), null, false);
			pTooltip.addLineIntText("births", tCity.getTotalBirths(), null, false, 21);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D14 RID: 15636 RVA: 0x001AA6BC File Offset: 0x001A88BC
	private void showActorNormal(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showActor("", pTooltip, pData);
	}

	// Token: 0x06003D15 RID: 15637 RVA: 0x001AA6CB File Offset: 0x001A88CB
	private void showLeader(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showActor("village_statistics_leader", pTooltip, pData);
	}

	// Token: 0x06003D16 RID: 15638 RVA: 0x001AA6DA File Offset: 0x001A88DA
	private void showKing(Tooltip pTooltip, string pType, TooltipData pData)
	{
		this.showActor("village_statistics_king", pTooltip, pData);
	}

	// Token: 0x06003D17 RID: 15639 RVA: 0x001AA6EC File Offset: 0x001A88EC
	private void showActor(string pSubTitle, Tooltip pTooltip, TooltipData pData)
	{
		Actor tActor = pData.actor;
		Image tIconSpecial = pTooltip.transform.FindRecursive("IconSpecial").GetComponent<Image>();
		if (tActor.asset.is_boat)
		{
			tIconSpecial.sprite = tActor.asset.getSpriteIcon();
		}
		else if (tActor.isSexMale())
		{
			tIconSpecial.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
		}
		else
		{
			tIconSpecial.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
		}
		this.setIconValue(pTooltip, "i_age", (float)tActor.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_level", (float)tActor.data.level, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_kills", (float)tActor.data.kills, null, "", false, "", '/');
		pTooltip.GetComponentInChildren<TooltipActorTraitsRow>(true).init(pTooltip, pData);
		pTooltip.GetComponentInChildren<TooltipActorEquipmentsRow>(true).init(pTooltip, pData);
		StatBar component = pTooltip.transform.FindRecursive("HealthBar").GetComponent<StatBar>();
		float tHealthBarCur = (float)tActor.getHealth();
		float tHealthMax = (float)tActor.getMaxHealth();
		component.setBar(tHealthBarCur, tHealthMax, "/" + ((int)tHealthMax).ToText(4), false, false, true, 0.25f);
		this.showActorBars(pTooltip, tActor);
		string tKingdomColorHex = tActor.kingdom.getColor().color_text;
		pTooltip.setTitle(tActor.name, pSubTitle, tKingdomColorHex);
		if (DebugConfig.isOn(DebugOption.DebugTooltipActorAI))
		{
			pTooltip.addLineText("wait_timer", tActor.hasTask() ? tActor.timer_action.ToText() : "-", "#43FF43", false, false, 21);
			pTooltip.addLineText("task", tActor.hasTask() ? tActor.ai.task.id : "-", "#43FF43", false, false, 21);
			pTooltip.addLineText("action", (tActor.ai.action != null) ? tActor.ai.action.id : "-", "#23F3FF", false, false, 21);
			pTooltip.addLineText("job", (tActor.ai.job != null) ? tActor.ai.job.id : "-", "#FB2C21", false, false, 21);
			pTooltip.addLineText("citizen_job", (tActor.citizen_job != null) ? tActor.citizen_job.id : "-", "#8CFF99", false, false, 21);
			pTooltip.addLineIntText("id", tActor.data.id, null, true, 21);
			pTooltip.addLineIntText("hashset", tActor.GetHashCode(), null, false);
			pTooltip.addLineIntText("kingdom_hash", tActor.kingdom.GetHashCode(), null, false);
			pTooltip.addLineIntText("kingdom_id", tActor.kingdom.data.id, null, false, 21);
			pTooltip.addLineText("profession", tActor.profession_asset.id, null, false, false, 21);
		}
		if (tActor.isSapient() && tActor.isKingdomCiv())
		{
			pTooltip.addLineText("kingdom", tActor.kingdom.name, tKingdomColorHex, false, true, 21);
		}
		if (tActor.hasLover())
		{
			pTooltip.addLineText("lover", tActor.lover.name, tActor.lover.kingdom.getColor().color_text, false, true, 21);
		}
		if (tActor.asset.inspect_home)
		{
			string tHomeName = "??";
			if (tActor.city != null)
			{
				tHomeName = tActor.city.name;
			}
			pTooltip.addLineText("creature_statistics_home_village", tHomeName, tKingdomColorHex, false, true, 21);
			if (tActor.hasClan())
			{
				string tClanColorHex = tActor.clan.getColor().color_text;
				pTooltip.addLineText("clan", tActor.clan.data.name, tClanColorHex, false, true, 21);
			}
		}
		if (tActor.hasFamily())
		{
			pTooltip.addLineText("family", tActor.family.name, tActor.family.getColor().color_text, false, true, 21);
		}
		if (tActor.hasCulture())
		{
			pTooltip.addLineText("culture", tActor.culture.name, tActor.culture.getColor().color_text, false, true, 21);
		}
		if (tActor.hasLanguage())
		{
			pTooltip.addLineText("language", tActor.language.name, tActor.language.getColor().color_text, false, true, 21);
		}
		if (tActor.hasArmy())
		{
			pTooltip.addLineText("army", tActor.army.name, tActor.army.getColor().color_text, false, true, 21);
		}
		pTooltip.addLineBreak();
		if (tActor.money > 0)
		{
			pTooltip.addLineIntText("money", tActor.money, null, true);
		}
		if (tActor.loot > 0)
		{
			pTooltip.addLineIntText("loot", tActor.loot, null, true);
		}
		if (tActor.asset.inspect_kills)
		{
			pTooltip.addLineIntText("creature_statistics_kills", tActor.data.kills, null, true);
		}
		if (tActor.asset.inspect_children)
		{
			pTooltip.addLineIntText("creature_statistics_children", tActor.current_children_count, null, true);
		}
		if (tActor.isSapient() && tActor.s_personality != null)
		{
			pTooltip.addLineText("creature_statistics_personality", LocalizedTextManager.getText("personality_" + tActor.s_personality.id, null, false), null, false, true, 21);
		}
		pTooltip.addLineText("task", tActor.hasTask() ? tActor.ai.task.getLocalizedText() : "-", "#43FF43", false, true, 21);
		if (tActor.hasSubspecies())
		{
			pTooltip.addLineBreak();
			pTooltip.addLineText("subspecies", tActor.subspecies.name, tActor.subspecies.getColor().color_text, false, true, 15);
		}
		TooltipIconsRow tResourcesIcons = pTooltip.transform.FindRecursive("Resources").GetComponent<TooltipIconsRow>();
		bool tCarrying = tActor.isCarryingResources();
		tResourcesIcons.gameObject.SetActive(tCarrying);
		if (tCarrying)
		{
			foreach (ResourceContainer tContainer in tActor.inventory.getResources().Values)
			{
				Sprite tIcon = AssetManager.resources.get(tContainer.id).getSpriteIcon();
				int tAmount = tContainer.amount;
				int tLimit = 5;
				for (int i = 0; i < tAmount; i++)
				{
					tResourcesIcons.addIcon(tIcon, "#FFFFFF");
					tLimit--;
					if (tLimit <= 0)
					{
						break;
					}
				}
			}
			tResourcesIcons.init(pTooltip, pData);
		}
		TooltipIconsRow tPassengersIcons = pTooltip.transform.FindRecursive("Passengers").GetComponent<TooltipIconsRow>();
		if (tActor.asset.is_boat)
		{
			Boat tBoat = tActor.getSimpleComponent<Boat>();
			pTooltip.addLineBreak();
			pTooltip.addLineIntText("passengers", tBoat.countPassengers(), null, true);
			this.showBoatPassengers(tBoat, tPassengersIcons, pTooltip, pData);
		}
		else
		{
			tPassengersIcons.gameObject.SetActive(false);
		}
		Sprite tSpeciesIcon;
		if (tActor.asset.is_boat && tActor.hasCity())
		{
			tSpeciesIcon = tActor.city.getSpriteIcon();
		}
		else
		{
			tSpeciesIcon = tActor.asset.getSpriteIcon();
		}
		Image tIconRace = pTooltip.getSpeciesIcon();
		if (tSpeciesIcon != null)
		{
			tIconRace.sprite = tSpeciesIcon;
			tIconRace.gameObject.SetActive(true);
			return;
		}
		tIconRace.gameObject.SetActive(false);
	}

	// Token: 0x06003D18 RID: 15640 RVA: 0x001AAE6C File Offset: 0x001A906C
	private void showBoatPassengers(Boat pBoat, TooltipIconsRow pPassengersIcons, Tooltip pTooltip, TooltipData pData)
	{
		if (!pBoat.hasPassengers())
		{
			pPassengersIcons.gameObject.SetActive(false);
			return;
		}
		pPassengersIcons.gameObject.SetActive(true);
		int tLimit = 60;
		foreach (Actor actor in pBoat.getPassengers())
		{
			Sprite tIcon = actor.asset.getSpriteIcon();
			pPassengersIcons.addIcon(tIcon, "#FFFFFF");
			tLimit--;
			if (tLimit <= 0)
			{
				break;
			}
		}
		pPassengersIcons.init(pTooltip, pData);
	}

	// Token: 0x06003D19 RID: 15641 RVA: 0x001AAF00 File Offset: 0x001A9100
	private void showActorBars(Tooltip pTooltip, Actor pActor)
	{
		bool tShowHappiness = pActor.hasEmotions();
		if (tShowHappiness)
		{
			pTooltip.GetComponentInChildren<HappinessBarIcon>(true).load(pActor);
		}
		this.checkShowProgressBar(pTooltip, "HappinessBarFitter", "%", (float)pActor.getHappinessPercent(), 100f, tShowHappiness);
		bool tShowHunger = pActor.needsFood();
		float tHungerCurrent = (float)pActor.getNutrition() / (float)pActor.getMaxNutrition() * 100f;
		this.checkShowProgressBar(pTooltip, "HungerBarFitter", "%", tHungerCurrent, 100f, tShowHunger);
		bool tShowStamina = !pActor.asset.force_hide_stamina;
		int tStaminaMax = pActor.getMaxStamina();
		float tStaminaCurrent = (float)Mathf.Clamp(pActor.getStamina(), 0, tStaminaMax);
		this.checkShowProgressBar(pTooltip, "StaminaBarFitter", string.Format("/{0}", tStaminaMax), tStaminaCurrent, (float)tStaminaMax, tShowStamina);
		bool tShowMana = !pActor.asset.force_hide_mana;
		int tManaMax = pActor.getMaxMana();
		float tManaCurrent = (float)Mathf.Clamp(pActor.getMana(), 0, tManaMax);
		this.checkShowProgressBar(pTooltip, "ManaBarFitter", string.Format("/{0}", tManaMax), tManaCurrent, (float)tManaMax, tShowMana);
		Transform tBars = pTooltip.transform.FindRecursive("Bars");
		if (!tShowHappiness && !tShowHunger && !tShowStamina && !tShowMana)
		{
			tBars.gameObject.SetActive(false);
			return;
		}
		tBars.gameObject.SetActive(true);
	}

	// Token: 0x06003D1A RID: 15642 RVA: 0x001AB04C File Offset: 0x001A924C
	private void checkShowProgressBar(Tooltip pTooltip, string pBarName, string pEnding, float pCurrentValue, float pMax, bool pShow)
	{
		Transform tBarFitter = pTooltip.transform.FindRecursive(pBarName);
		tBarFitter.gameObject.SetActive(pShow);
		if (!pShow)
		{
			return;
		}
		tBarFitter.GetComponentInChildren<StatBar>(true).setBar(pCurrentValue, pMax, pEnding, false, false, true, 0.25f);
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x001AB094 File Offset: 0x001A9294
	private void showClan(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Clan tClan = pData.clan;
		pTooltip.setSpeciesIcon(tClan.getActorAsset().getSpriteIcon());
		pTooltip.setDescription(tClan.getMotto(), null);
		string tColorHex = tClan.getColor().color_text;
		pTooltip.setTitle(tClan.name, "clan", tColorHex);
		this.setIconValue(pTooltip, "i_age", (float)tClan.getAge(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tClan.countUnits(), null, "", false, "", '/');
		pTooltip.GetComponentInChildren<TooltipClanTraitsRow>(true).init(pTooltip, pData);
		pTooltip.addLineText("clan_members_title", tClan.getTextMaxMembers(), null, false, true, 21);
		if (tClan.getChief() != null)
		{
			if (tClan.getChief().hasKingdom())
			{
				tColorHex = tClan.getChief().kingdom.getColor().color_text;
			}
			pTooltip.addLineText("clan_chief_title", tClan.getChief().getName(), tColorHex, false, true, 21);
			pTooltip.addLineText("species", tClan.getChief().asset.getTranslatedName(), tColorHex, false, true, 21);
			pTooltip.addLineBreak();
		}
		pTooltip.addLineIntText("adults", tClan.countAdults(), null, true);
		pTooltip.addLineIntText("children", tClan.countChildren(), null, true);
		pTooltip.addLineIntText("happy", tClan.countHappyUnits(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kings", tClan.countKings(), null, true);
		pTooltip.addLineIntText("leaders", tClan.countLeaders(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tClan.getTotalDeaths(), null, true, 21);
		ClanBanner[] array = pTooltip.transform.FindAllRecursive<ClanBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tClan);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x001AB278 File Offset: 0x001A9478
	private void showBook(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Book tBook = pData.book;
		BookTypeAsset tAsset = tBook.getAsset();
		int tAge = tBook.getAge();
		string tDescriptionAuthorInfo = LocalizedTextManager.getText("book_author_description", null, false);
		tDescriptionAuthorInfo = tDescriptionAuthorInfo.Replace("$author_name$", tBook.data.author_name);
		tDescriptionAuthorInfo = tDescriptionAuthorInfo.Replace("$author_kingdom$", tBook.data.author_kingdom_name);
		tDescriptionAuthorInfo = tDescriptionAuthorInfo.Replace("$author_city$", tBook.data.author_city_name);
		string tDescriptionBookInfo = tAsset.getDescriptionTranslated();
		pTooltip.setTitle(tBook.name, tAsset.getLocaleID(), tAsset.color_text);
		pTooltip.addLineIntText("age", tAge, null, true);
		pTooltip.addLineText("book_written_in", tBook.getBirthday(), null, false, true, 21);
		pTooltip.addLineIntText("book_times_read", tBook.data.times_read, null, true);
		pTooltip.addLineBreak();
		this.showMetaLineActor(pTooltip, "book_author", tBook.data.author_id, tBook.data.author_name);
		this.showMetaLineClan(pTooltip, "clan", tBook.data.author_clan_id, tBook.data.author_clan_name);
		this.showMetaLineCulture(pTooltip, "culture", tBook.data.culture_id, tBook.data.culture_name);
		this.showMetaLineLanguage(pTooltip, "language", tBook.data.language_id, tBook.data.language_name);
		this.showMetaLineVillage(pTooltip, "village", tBook.data.author_city_id, tBook.data.author_city_name);
		this.showMetaLineVillage(pTooltip, "religion", tBook.data.religion_id, tBook.data.religion_name);
		pTooltip.addLineBreak();
		string tOnReadLocalized = Toolbox.coloredText(LocalizedTextManager.getText("book_action_on_read", null, false), "#FFFFFF", false);
		pTooltip.addLineText(tOnReadLocalized, "", null, false, false, 21);
		TooltipIconsRow tIconsRow = pTooltip.GetComponentInChildren<TooltipIconsRow>(true);
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tBook.getBaseStats(), true);
		if (tBook.getBookTraitActor() != null)
		{
			tIconsRow.addIcon(tBook.getBookTraitActor().getSprite(), "#FFFFFF");
		}
		if (tBook.getBookTraitCulture() != null)
		{
			tIconsRow.addIcon(tBook.getBookTraitCulture().getSprite(), "#FFFFFF");
		}
		if (tBook.getBookTraitLanguage() != null)
		{
			tIconsRow.addIcon(tBook.getBookTraitLanguage().getSprite(), "#FFFFFF");
		}
		if (tBook.getBookTraitReligion() != null)
		{
			tIconsRow.addIcon(tBook.getBookTraitReligion().getSprite(), "#FFFFFF");
		}
		tIconsRow.init(pTooltip, pData);
		if (Config.editor_maxim)
		{
			tDescriptionBookInfo += "\n\n";
			tDescriptionBookInfo += StoryLibrary.getTestText(tBook.getLanguage());
		}
		pTooltip.setDescription(tDescriptionBookInfo, null);
		pTooltip.setBottomDescription(tDescriptionAuthorInfo, null);
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x001AB52C File Offset: 0x001A972C
	private void showMetaLineActor(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		Actor tActor = pID.hasValue() ? World.world.units.get(pID) : null;
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tActor.isRekt())
		{
			Kingdom kingdom = tActor.kingdom;
			tColor = ((kingdom != null) ? kingdom.getColor().color_text : null);
			tValueText = tActor.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D1E RID: 15646 RVA: 0x001AB598 File Offset: 0x001A9798
	private void showMetaLineClan(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		Clan tMetaObject = World.world.clans.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D1F RID: 15647 RVA: 0x001AB5F8 File Offset: 0x001A97F8
	private void showMetaLineCulture(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		Culture tMetaObject = World.world.cultures.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D20 RID: 15648 RVA: 0x001AB658 File Offset: 0x001A9858
	private void showMetaLineLanguage(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		Language tMetaObject = World.world.languages.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D21 RID: 15649 RVA: 0x001AB6B8 File Offset: 0x001A98B8
	private void showMetaLineVillage(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		City tMetaObject = World.world.cities.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D22 RID: 15650 RVA: 0x001AB718 File Offset: 0x001A9918
	private void showMetaLineSubspecies(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		Subspecies tMetaObject = World.world.subspecies.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D23 RID: 15651 RVA: 0x001AB778 File Offset: 0x001A9978
	private void showMetaLineReligion(Tooltip pTooltip, string pTitle, long pID, string pDefaultName)
	{
		if (!pID.hasValue())
		{
			return;
		}
		Religion tMetaObject = World.world.religions.get(pID);
		string tValueText = "† " + pDefaultName;
		string tColor = null;
		if (!tMetaObject.isRekt())
		{
			tColor = tMetaObject.getColor().color_text;
			tValueText = tMetaObject.name;
		}
		pTooltip.addLineText(pTitle, tValueText, tColor, false, true, 21);
	}

	// Token: 0x06003D24 RID: 15652 RVA: 0x001AB7D8 File Offset: 0x001A99D8
	private void showWar(Tooltip pTooltip, string pType, TooltipData pData)
	{
		War tWar = pData.war;
		pTooltip.GetComponentInChildren<WarTooltipBannersContainer>().load(tWar);
		pTooltip.setTitle(tWar.name, tWar.getAsset().localized_war_name, tWar.getAttackersColorTextString());
		pTooltip.addLineIntText("started_at", tWar.getYearStarted(), null, true);
		if (tWar.hasEnded())
		{
			pTooltip.addLineIntText("war_ended_at", tWar.getYearEnded(), null, true);
		}
		pTooltip.addLineIntText("war_duration", tWar.getDuration(), null, true);
		string tWinnerLocale = tWar.data.winner.getLocaleID().Localize();
		switch (tWar.data.winner)
		{
		case WarWinner.Attackers:
			pTooltip.addLineText("war_winner", tWinnerLocale, tWar.getAttackersColorTextString(), false, true, 21);
			break;
		case WarWinner.Defenders:
			pTooltip.addLineText("war_winner", tWinnerLocale, tWar.getDefendersColorTextString(), false, true, 21);
			break;
		case WarWinner.Peace:
			pTooltip.addLineText("war_outcome", tWinnerLocale, null, false, true, 21);
			break;
		case WarWinner.Merged:
			pTooltip.addLineText("war_outcome", tWinnerLocale, null, false, true, 21);
			break;
		}
		pTooltip.addLineBreak();
		Actor tActor = World.world.units.get(tWar.data.started_by_actor_id);
		string tNameToShow = (tActor != null) ? tActor.getName() : tWar.data.started_by_actor_name;
		pTooltip.addLineText("instigator", tNameToShow, null, false, true, 21);
		long tInstigatorKingdomID = tWar.data.started_by_kingdom_id;
		Kingdom tInstigatorKingdom = World.world.kingdoms.get(tInstigatorKingdomID) ?? World.world.kingdoms.db_get(tInstigatorKingdomID);
		if (tInstigatorKingdom != null)
		{
			pTooltip.addLineText("instigator_from", tInstigatorKingdom.name, tInstigatorKingdom.getColor().color_text, false, true, 21);
		}
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("kingdoms", tWar.countKingdoms(), null, true);
		pTooltip.addLineIntText("villages", tWar.countCities(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tWar.getTotalDeaths(), null, true, 21);
		this.setIconValue(pTooltip, "a_army", (float)tWar.countAttackersWarriors(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "a_population", (float)tWar.countAttackersPopulation(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "a_deaths", (float)tWar.getDeadAttackers(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "a_cities", (float)tWar.countAttackersCities(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "d_army", (float)tWar.countDefendersWarriors(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "d_population", (float)tWar.countDefendersPopulation(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "d_deaths", (float)tWar.getDeadDefenders(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "d_cities", (float)tWar.countDefendersCities(), null, "", false, "", '/');
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x001ABB2C File Offset: 0x001A9D2C
	private void showWarSides(Tooltip pTooltip, string pType, TooltipData pData)
	{
		War tWar = pData.war;
		Text tAttackersText = pTooltip.transform.Find("Sides/Attackers/List").GetComponent<Text>();
		Text tDefendersText = pTooltip.transform.Find("Sides/Defenders/List").GetComponent<Text>();
		tAttackersText.text = "";
		tDefendersText.text = "";
		WarWinner winner = tWar.data.winner;
		if (winner != WarWinner.Attackers)
		{
			if (winner == WarWinner.Defenders)
			{
				Text text = tDefendersText;
				text.text = text.text + Toolbox.coloredText("war_winner_won", tWar.getDefendersColorTextString(), true) + "\n\n";
				Text text2 = tAttackersText;
				text2.text = text2.text + Toolbox.coloredText("war_winner_lost", tWar.getAttackersColorTextString(), true) + "\n\n";
			}
		}
		else
		{
			Text text3 = tAttackersText;
			text3.text = text3.text + Toolbox.coloredText("war_winner_won", tWar.getAttackersColorTextString(), true) + "\n\n";
			Text text4 = tDefendersText;
			text4.text = text4.text + Toolbox.coloredText("war_winner_lost", tWar.getDefendersColorTextString(), true) + "\n\n";
		}
		using (ListPool<string> tAttackers = new ListPool<string>())
		{
			using (ListPool<string> tDefenders = new ListPool<string>())
			{
				foreach (Kingdom pKingdom in tWar.getAttackers())
				{
					TooltipLibrary.addParty(pKingdom, tAttackers, false, false);
				}
				foreach (Kingdom pKingdom2 in tWar.getDiedAttackers())
				{
					TooltipLibrary.addParty(pKingdom2, tAttackers, false, true);
				}
				foreach (Kingdom pKingdom3 in tWar.getPastAttackers())
				{
					TooltipLibrary.addParty(pKingdom3, tAttackers, true, false);
				}
				foreach (Kingdom pKingdom4 in tWar.getDefenders())
				{
					TooltipLibrary.addParty(pKingdom4, tDefenders, false, false);
				}
				foreach (Kingdom pKingdom5 in tWar.getDiedDefenders())
				{
					TooltipLibrary.addParty(pKingdom5, tDefenders, false, true);
				}
				foreach (Kingdom pKingdom6 in tWar.getPastDefenders())
				{
					TooltipLibrary.addParty(pKingdom6, tDefenders, true, false);
				}
				if (tAttackers.Count > 13)
				{
					int tMoreAttackers = tAttackers.Count - 12;
					while (tAttackers.Count > 12)
					{
						tAttackers.Pop<string>();
					}
					tAttackers.Add("... and " + tMoreAttackers.ToString() + " more");
				}
				if (tDefenders.Count > 13)
				{
					int tMoreDefenders = tDefenders.Count - 12;
					while (tDefenders.Count > 12)
					{
						tDefenders.Pop<string>();
					}
					tDefenders.Add("... and " + tMoreDefenders.ToString() + " more");
				}
				Text text5 = tAttackersText;
				text5.text += string.Join("\n", tAttackers);
				Text text6 = tDefendersText;
				text6.text += string.Join("\n", tDefenders);
				this.showTabBannerTip(pTooltip, pData);
			}
		}
	}

	// Token: 0x06003D26 RID: 15654 RVA: 0x001ABF34 File Offset: 0x001AA134
	private static void addParty(Kingdom pKingdom, ListPool<string> pPartyList, bool pLeft = false, bool pDied = false)
	{
		string tName = pKingdom.name;
		string tColor = pKingdom.getColor().color_text;
		string tSuffix = "";
		if (pLeft)
		{
			tSuffix = Toolbox.coloredText(" (left)", ColorStyleLibrary.m.color_text_grey_dark, false);
		}
		else if (pDied)
		{
			tSuffix = Toolbox.coloredText(" (died)", ColorStyleLibrary.m.color_text_grey, false);
		}
		else
		{
			pKingdom.hasDied();
		}
		pPartyList.Add(Toolbox.coloredText(tName, tColor, false) + tSuffix);
	}

	// Token: 0x06003D27 RID: 15655 RVA: 0x001ABFAC File Offset: 0x001AA1AC
	private void showAlliance(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Alliance tAlliance = pData.alliance;
		pTooltip.setDescription(tAlliance.getMotto(), null);
		string tColorHex = tAlliance.getColor().color_text;
		pTooltip.setTitle(tAlliance.name, "alliance", tColorHex);
		int tAge = tAlliance.getAge();
		this.setIconValue(pTooltip, "i_age", (float)tAge, null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_population", (float)tAlliance.countPopulation(), null, "", false, "", '/');
		this.setIconValue(pTooltip, "i_army", (float)tAlliance.countWarriors(), null, "", false, "", '/');
		pTooltip.addLineIntText("adults", tAlliance.countAdults(), null, true);
		pTooltip.addLineIntText("children", tAlliance.countChildren(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("tip_alliance_kingdoms", tAlliance.countKingdoms(), null, true);
		pTooltip.addLineIntText("tip_alliance_buildings", tAlliance.countBuildings(), null, true);
		pTooltip.addLineIntText("territory", tAlliance.countZones(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("housed", tAlliance.countHoused(), null, true);
		pTooltip.addLineIntText("homeless", tAlliance.countHomeless(), null, true);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText("deaths", tAlliance.getTotalDeaths(), null, true, 21);
		AllianceBanner[] array = pTooltip.transform.FindAllRecursive<AllianceBanner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].load(tAlliance);
		}
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x001AC148 File Offset: 0x001AA348
	private KingdomOpinion showKingdomOpinion(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Kingdom tKingdom = pData.kingdom;
		pTooltip.name.text = Toolbox.coloredText(tKingdom.name, tKingdom.getColor().color_text, false);
		KingdomOpinion tOpinion = World.world.diplomacy.getRelation(tKingdom, SelectedMetas.selected_kingdom).getOpinion(SelectedMetas.selected_kingdom, tKingdom);
		foreach (OpinionAsset tAsset in tOpinion.results.Keys)
		{
			int tResult = tOpinion.results[tAsset];
			string tDescription = tAsset.getTranslationKey(tResult);
			pTooltip.addOpinion(new TooltipOpinionInfo(tDescription, tResult));
		}
		return tOpinion;
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x001AC20C File Offset: 0x001AA40C
	private string getArrowUp(long pValue)
	{
		if (pValue < 10L)
		{
			return " <size=4>↗</size>";
		}
		if (pValue < 100L)
		{
			return " <size=4>↗↗</size>";
		}
		return " <size=4>↗↗↗</size>";
	}

	// Token: 0x06003D2A RID: 15658 RVA: 0x001AC22B File Offset: 0x001AA42B
	private string getArrowDown(long pValue)
	{
		pValue = (long)Mathf.Abs((float)pValue);
		if (pValue < 10L)
		{
			return " <size=4>↘</size>";
		}
		if (pValue < 100L)
		{
			return " <size=4>↘↘</size>";
		}
		return " <size=4>↘↘↘</size>";
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x001AC254 File Offset: 0x001AA454
	private void showGraphResource(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (pData.nano_object != null)
		{
			NanoObject tObject = pData.nano_object;
			string tObjectColor = Toolbox.colorToHex(tObject.getColor().getColorText(), true);
			string tDescription = "";
			MetaCustomizationAsset tCurrentMetaAsset = AssetManager.meta_customization_library.getAsset(tObject.getMetaType());
			tDescription += LocalizedTextManager.getText(tCurrentMetaAsset.localization_title, null, false);
			tDescription += "\n";
			tDescription += tObject.name;
			pTooltip.name.text = Toolbox.coloredText(tDescription, tObjectColor, false);
		}
		long tYear = pData.custom_data_long["year"];
		pTooltip.addLineLongText("year", tYear, null, true, 21);
		long tAge = (tYear - (long)Date.getCurrentYear()) * -1L;
		pTooltip.addLineLongText("years_ago", tAge, null, true, 21);
		pTooltip.addLineBreak();
		using (ListPool<string> tSortedKeys = new ListPool<string>(pData.custom_data_long.Keys))
		{
			tSortedKeys.Sort((string pA, string pB) => pData.custom_data_long[pB].CompareTo(pData.custom_data_long[pA]));
			foreach (string ptr in tSortedKeys)
			{
				string tCategory = ptr;
				if (!(tCategory == "year") && !tCategory.Contains("_previous"))
				{
					HistoryDataAsset tAsset = AssetManager.history_data_library.get(tCategory);
					long tValue = pData.custom_data_long[tCategory];
					long tPrevious = pData.custom_data_long[tCategory + "_previous"];
					long tChange = tValue - tPrevious;
					if (tChange != 0L)
					{
						string tChangeValue = Toolbox.formatNumber(tChange);
						if (tChange > 0L)
						{
							tChangeValue = "+" + tChangeValue;
						}
						string tChangeColor = (tChange > 0L) ? "#43FF43" : "#FB2C21";
						string tChangeText = Toolbox.coloredText(tChangeValue, tChangeColor, false);
						string tLeftTextMain = tAsset.getLocaleID().Localize();
						if (tChange > 0L)
						{
							tLeftTextMain += this.getArrowUp(tChange).ColorHex("#43FF43", false);
						}
						else
						{
							tLeftTextMain += this.getArrowDown(tChange).ColorHex("#FB2C21", false);
						}
						pTooltip.addLineText(tLeftTextMain, "<size=4>" + tChangeText + "</size> " + tValue.ToText(), tAsset.tooltip_color_hex, false, false, 500);
					}
					else
					{
						pTooltip.addLineLongText(tAsset.getLocaleID(), tValue, tAsset.tooltip_color_hex, true, 21);
					}
				}
			}
		}
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x001AC530 File Offset: 0x001AA730
	private void showGraphMultiResource(Tooltip pTooltip, string pType, TooltipData pData)
	{
		string tResourceCategory = pData.tip_name;
		HistoryDataAsset tAsset = AssetManager.history_data_library.get(tResourceCategory);
		pTooltip.name.text = Toolbox.coloredText(tAsset.getLocaleID(), tAsset.tooltip_color_hex, true);
		long tYear = pData.custom_data_long["year"];
		pTooltip.addLineIntText("year", tYear, null, true, 21);
		long tAge = (tYear - (long)Date.getCurrentYear()) * -1L;
		pTooltip.addLineIntText("years_ago", tAge, null, true, 21);
		pTooltip.addLineBreak();
		using (ListPool<string> tSortedKeys = new ListPool<string>(pData.custom_data_long.Keys))
		{
			tSortedKeys.Sort((string pA, string pB) => pData.custom_data_long[pB].CompareTo(pData.custom_data_long[pA]));
			foreach (string ptr in tSortedKeys)
			{
				string tMetaName = ptr;
				if (!(tMetaName == "year") && !tMetaName.Contains("_previous"))
				{
					long tValue = pData.custom_data_long[tMetaName];
					long tPrevious = pData.custom_data_long[tMetaName + "_previous"];
					long tChange = tValue - tPrevious;
					string tColor = pData.custom_data_string[tMetaName];
					if (tChange != 0L)
					{
						string tChangeValue = Toolbox.formatNumber(tChange);
						if (tChange > 0L)
						{
							tChangeValue = "+" + tChangeValue;
						}
						string tChangeColor = (tChange > 0L) ? "#43FF43" : "#FB2C21";
						string tChangeText = Toolbox.coloredText(tChangeValue, tChangeColor, false);
						pTooltip.addLineText(tMetaName, "<size=4>" + tChangeText + "</size> " + tValue.ToText(), tColor, false, false, 500);
					}
					else
					{
						pTooltip.addLineLongText(tMetaName, tValue, tColor, false, 500);
					}
				}
			}
		}
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x001AC754 File Offset: 0x001AA954
	private void showGenderData(Tooltip pTooltip, string pType, TooltipData pData)
	{
		string tAgeRange = pData.custom_data_string["age_range"];
		pTooltip.name.text = tAgeRange;
		HistoryDataAsset tMaleAsset = AssetManager.history_data_library.get("males");
		HistoryDataAsset tFemaleAsset = AssetManager.history_data_library.get("females");
		pTooltip.addLineText("age_range", tAgeRange, null, false, true, 21);
		pTooltip.addLineBreak();
		pTooltip.addLineIntText(tMaleAsset.getLocaleID(), pData.custom_data_int["males"], tMaleAsset.tooltip_color_hex, true);
		pTooltip.addLineIntText(tFemaleAsset.getLocaleID(), pData.custom_data_int["females"], tFemaleAsset.tooltip_color_hex, true);
	}

	// Token: 0x06003D2E RID: 15662 RVA: 0x001AC7FC File Offset: 0x001AA9FC
	private void showCityResource(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (SelectedMetas.selected_city.isRekt())
		{
			return;
		}
		ResourceAsset tResource = pData.resource;
		pTooltip.name.text = tResource.getTranslatedName();
		pTooltip.clearTextRows();
		pTooltip.addLineIntText("amount", SelectedMetas.selected_city.getResourcesAmount(tResource.id), null, true);
	}

	// Token: 0x06003D2F RID: 15663 RVA: 0x001AC854 File Offset: 0x001AAA54
	private void showCityResourceFood(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (SelectedMetas.selected_city.isRekt())
		{
			return;
		}
		ResourceAsset tResource = pData.resource;
		pTooltip.name.text = tResource.getTranslatedName();
		pTooltip.clearTextRows();
		pTooltip.addLineIntText("amount", SelectedMetas.selected_city.getResourcesAmount(tResource.id), null, true);
		pTooltip.addLineBreak();
		if (tResource.restore_health != 0f)
		{
			pTooltip.addLineText("health", tResource.restore_health.ToText(), null, false, true, 21);
		}
		if (tResource.restore_mana != 0)
		{
			pTooltip.addLineIntText("mana", tResource.restore_mana, null, true);
		}
		if (tResource.restore_stamina != 0)
		{
			pTooltip.addLineIntText("stamina", tResource.restore_stamina, null, true);
		}
		pTooltip.addLineBreak();
		if (tResource.restore_nutrition != 0)
		{
			pTooltip.addLineIntText("nutrition", tResource.restore_nutrition, null, true);
		}
		if (tResource.restore_happiness != 0)
		{
			pTooltip.addLineIntText("happiness", tResource.restore_happiness, null, true);
		}
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x001AC94C File Offset: 0x001AAB4C
	private void showMapMeta(Tooltip pTooltip, string pType, TooltipData pData)
	{
		MapMetaData tMeta = pData.map_meta;
		string tTitleColor = null;
		if (tMeta.saveVersion > Config.WORLD_SAVE_VERSION)
		{
			pTooltip.setBottomDescription(LocalizedTextManager.getText("future_save_version", null, false), "#FB2C21");
			tTitleColor = "#FB2C21";
		}
		pTooltip.name.text = tMeta.mapStats.name;
		pTooltip.name.color = tMeta.mapStats.getArchitectMood().getColorText();
		if (tMeta.modded)
		{
			if (tTitleColor != null)
			{
				pTooltip.addBottomDescription("\n\n", null);
			}
			if (!Config.MODDED)
			{
				pTooltip.addBottomDescription(LocalizedTextManager.getText("modded_world_no_mod_active", null, false), "#FB2C21");
				tTitleColor = "#FB2C21";
			}
			else
			{
				pTooltip.addBottomDescription(LocalizedTextManager.getText("modded_world", null, false), "#45FFFE");
				if (tTitleColor == null)
				{
					tTitleColor = "#45FFFE";
				}
			}
		}
		if (tTitleColor != null)
		{
			pTooltip.name.text = Toolbox.coloredText(tMeta.mapStats.name, tTitleColor, false);
		}
		if (tMeta.mapStats.description != "")
		{
			pTooltip.addDescription(tMeta.mapStats.description, null);
		}
		else
		{
			pTooltip.addDescription("WORLDBOX, HO!", null);
		}
		string tCol = "#95DD5D";
		pTooltip.addLineIntText("world_age", Date.getYear(tMeta.mapStats.world_time), tCol, true);
		pTooltip.addLineIntText("kingdoms", tMeta.kingdoms, tCol, true);
		pTooltip.addLineIntText("cultures", tMeta.cultures, tCol, true);
		pTooltip.addLineIntText("villages", tMeta.cities, tCol, true);
		pTooltip.addLineIntText("mobs", tMeta.mobs, tCol, true);
		pTooltip.addLineIntText("population", tMeta.population, tCol, true);
		if (pTooltip.stats_description.text.Length > 0)
		{
			pTooltip.addLineBreak();
		}
		pTooltip.addLineText("created", tMeta.temp_date_string, null, false, true, 21);
	}

	// Token: 0x06003D31 RID: 15665 RVA: 0x001ACB24 File Offset: 0x001AAD24
	private void showEquipment(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Item tItem = pData.item;
		Component component = pTooltip.transform.Find("Description/IconBG/LegendaryBG");
		Image component2 = pTooltip.transform.Find("Description/IconBG/ItemIcon").GetComponent<Image>();
		Text tEquipmentType = pTooltip.transform.Find("Equipment Type/EquipmentText").GetComponent<Text>();
		EquipmentAsset tItemAsset = tItem.getAsset();
		Sprite tSprite = tItem.getSprite();
		component2.sprite = tSprite;
		Text tDescriptionComponent = pTooltip.transform.Find("Item Description/item_description_text").GetComponent<Text>();
		string tColor = tItem.getQualityColor();
		Component component3 = pTooltip.transform.FindRecursive("Stats");
		bool tUnlocked = tItemAsset.isAvailable();
		string tName = tItem.getName(true);
		pTooltip.name.text = Toolbox.coloredText(tName, tColor, false);
		if (!tUnlocked)
		{
			if (tItemAsset.unlocked_with_achievement)
			{
				string tText = LocalizedTextManager.getText("item_locked_tooltip_text_achievement", null, false);
				string tAchievementIdTranslated = "<color=#00ffffff>" + tItemAsset.getAchievementLocaleID().Localize() + "</color>";
				tText = tText.Replace("$achievement_id$", tAchievementIdTranslated);
				tDescriptionComponent.text = tText;
			}
			else
			{
				tDescriptionComponent.text = LocalizedTextManager.getText("item_locked_tooltip_text_exploration", null, false);
			}
		}
		else
		{
			BaseStatsHelper.showItemMods(pTooltip.stats_description, pTooltip.stats_values, tItem);
			BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tItem.getFullStats(), true);
			pTooltip.addLineBreak();
			pTooltip.addLineText("durability", tItem.getDurabilityString(), null, false, true, 21);
			if (tItem.data.kills > 0)
			{
				pTooltip.addLineBreak();
				pTooltip.addItemText("creature_statistics_kills", (float)tItem.data.kills, false, true, false, "#FF9B1C", false);
			}
			string tDescriptionId = tItemAsset.id + "_description";
			bool tLocaleExists = LocalizedTextManager.stringExists(tDescriptionId);
			tDescriptionComponent.transform.parent.gameObject.SetActive(tLocaleExists);
			if (tLocaleExists)
			{
				tDescriptionComponent.text = LocalizedTextManager.getText(tDescriptionId, null, false);
			}
		}
		component3.gameObject.SetActive(tUnlocked);
		Rarity tItemQuality = tItem.getQuality();
		component.gameObject.SetActive(tItemQuality == Rarity.R3_Legendary);
		pTooltip.description.alignment = TextAnchor.MiddleLeft;
		tEquipmentType.GetComponent<LocalizedText>().setKeyAndUpdate(tItem.getItemKeyType());
		tEquipmentType.color = Toolbox.makeColor(tColor);
		string tCreationDescription = Toolbox.coloredText(tItem.getItemDescription(), "#FFFFFF", false);
		pTooltip.setDescription(tCreationDescription, null);
		this.showTabBannerTip(pTooltip, pData);
	}

	// Token: 0x06003D32 RID: 15666 RVA: 0x001ACD7C File Offset: 0x001AAF7C
	private void showEquipmentInEditor(Tooltip pTooltip, string pType, TooltipData pData)
	{
		EquipmentAsset tItemAsset = pData.item_asset;
		string tHiddenText = LocalizedTextManager.getText("achievement_tip_hidden", null, false);
		if (!tItemAsset.isAvailable())
		{
			pTooltip.name.text = tHiddenText;
			if (tItemAsset.unlocked_with_achievement)
			{
				string tText = LocalizedTextManager.getText("item_locked_tooltip_text_achievement", null, false);
				string tAchievementIdTranslated = "<color=#00ffffff>" + tItemAsset.getAchievementLocaleID().Localize() + "</color>";
				tText = tText.Replace("$achievement_id$", tAchievementIdTranslated);
				pTooltip.setDescription(tText, null);
			}
			else
			{
				pTooltip.setDescription(LocalizedTextManager.getText("item_locked_tooltip_text_exploration", null, false), null);
			}
			pTooltip.transform.FindRecursive("Stats").gameObject.SetActive(false);
			return;
		}
		string tName;
		string tMaterial;
		ItemTools.getTooltipTitle(tItemAsset, out tName, out tMaterial);
		pTooltip.name.text = tMaterial + tName;
		string descriptionID = tItemAsset.getDescriptionID();
		string tDescription = (descriptionID != null) ? descriptionID.Localize() : null;
		if (!string.IsNullOrEmpty(tDescription))
		{
			pTooltip.setDescription(tDescription, null);
		}
		else
		{
			pTooltip.resetDescription();
		}
		if (!string.IsNullOrEmpty(pData.tip_description_2))
		{
			string tLocalizedDescription2 = LocalizedTextManager.getText(pData.tip_description_2, null, false);
			pTooltip.setBottomDescription(tLocalizedDescription2, null);
		}
		BaseStatsHelper.showBaseStats(pTooltip.stats_description, pTooltip.stats_values, tItemAsset.base_stats, true);
	}

	// Token: 0x06003D33 RID: 15667 RVA: 0x001ACEB8 File Offset: 0x001AB0B8
	private void showWorldLaw(Tooltip pTooltip, string pType, TooltipData pData)
	{
		WorldLawAsset tAsset = pData.world_law;
		pTooltip.name.text = LocalizedTextManager.getText(tAsset.getLocaleID(), null, false);
		string tDescription = LocalizedTextManager.getText(tAsset.getDescriptionID(), null, false);
		if (!InputHelpers.mouseSupported)
		{
			if (tAsset.id != "world_law_cursed_world")
			{
				tDescription += "\n\n";
				tDescription += Toolbox.coloredText(LocalizedTextManager.getText("world_laws_tip_mobile_tap", null, false), "#999999", false);
			}
			else if (!tAsset.isEnabled())
			{
				tDescription += "\n\n";
				tDescription += Toolbox.coloredText(LocalizedTextManager.getText("world_laws_tip_mobile_tap_cursed", null, false), "#999999", false);
			}
		}
		pTooltip.setDescription(tDescription, null);
		string tDescriptionSecond = tAsset.getDescriptionID2();
		if (LocalizedTextManager.stringExists(tDescriptionSecond))
		{
			string tDescription2 = LocalizedTextManager.getText(tDescriptionSecond, null, false);
			pTooltip.setBottomDescription(tDescription2, null);
		}
	}

	// Token: 0x06003D34 RID: 15668 RVA: 0x001ACF94 File Offset: 0x001AB194
	private void showWorldAge(Tooltip pTooltip, string pType, TooltipData pData)
	{
		string tAssetId = pData.tip_name;
		WorldAgeAsset tAsset = AssetManager.era_library.get(tAssetId);
		string tTitleLocale = tAsset.getLocaleID();
		string descriptionID = tAsset.getDescriptionID();
		pTooltip.name.text = Toolbox.coloredText(tTitleLocale, Toolbox.colorToHex(tAsset.title_color, true), true);
		string temp_description = LocalizedTextManager.getText(descriptionID, null, false);
		Sprite tSprite = tAsset.getSprite();
		pTooltip.transform.Find("Headline/IconLeft").GetComponent<Image>().sprite = tSprite;
		pTooltip.transform.Find("Headline/IconRight").GetComponent<Image>().sprite = tSprite;
		if (Config.isMobile)
		{
			temp_description += "\n\n";
			temp_description += Toolbox.coloredText(LocalizedTextManager.getText("world_age_tip_mobile_tap", null, false), "#999999", false);
		}
		pTooltip.setDescription(temp_description, null);
	}

	// Token: 0x06003D35 RID: 15669 RVA: 0x001AD068 File Offset: 0x001AB268
	private void showStatsData(Tooltip pTooltip, string pType, TooltipData pData)
	{
		CustomDataContainer<string> custom_data_string = pData.custom_data_string;
		string tValue;
		if (custom_data_string.TryGetValue("value", out tValue))
		{
			pTooltip.addLineText(pData.tip_name, tValue, null, false, true, 21);
		}
		string tMax;
		if (custom_data_string.TryGetValue("max_value", out tMax))
		{
			pTooltip.addLineText("max", tMax, null, false, true, 21);
		}
	}

	// Token: 0x06003D36 RID: 15670 RVA: 0x001AD0BC File Offset: 0x001AB2BC
	private void opinionListToStatsLoyalty(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (pTooltip.opinion_list.Count == 0)
		{
			return;
		}
		pTooltip.opinion_list.Sort(new Comparison<TooltipOpinionInfo>(this.sorter));
		string temp_stat = "";
		string temp_value = "";
		bool positiveFound = false;
		bool tPlusToMinus = false;
		for (int i = 0; i < pTooltip.opinion_list.Count; i++)
		{
			TooltipOpinionInfo tInfo = pTooltip.opinion_list[i];
			if (tInfo.value > 0)
			{
				positiveFound = true;
			}
			if (tInfo.value < 0 && !tPlusToMinus && i > 0 && positiveFound)
			{
				tPlusToMinus = true;
				temp_stat += "\n---";
				temp_value += "\n---";
			}
			if (i > 0)
			{
				temp_stat += "\n";
				temp_value += "\n";
			}
			if (tInfo.value > 0)
			{
				temp_value += Toolbox.coloredText(tInfo.value.ToString("+##,#;-##,#;0"), "#43FF43", false);
				temp_stat += Toolbox.coloredText(LocalizedTextManager.getText(tInfo.translation_key, null, false), "#43FF43", false);
			}
			else
			{
				temp_value += Toolbox.coloredText(tInfo.value.ToString("+##,#;-##,#;0"), "#FB2C21", false);
				temp_stat += Toolbox.coloredText(LocalizedTextManager.getText(tInfo.translation_key, null, false), "#FB2C21", false);
			}
		}
		pTooltip.addStatValues(temp_stat, temp_value);
	}

	// Token: 0x06003D37 RID: 15671 RVA: 0x001AD224 File Offset: 0x001AB424
	private void opinionListToStatsDiplomacy(Tooltip pTooltip, string pType, TooltipData pData)
	{
		KingdomOpinion tKingdomOpinion = this.showKingdomOpinion(pTooltip, pType, pData);
		if (pTooltip.opinion_list.Count == 0)
		{
			pTooltip.stats_container.SetActive(false);
			return;
		}
		pTooltip.opinion_list.Sort(new Comparison<TooltipOpinionInfo>(this.sorter));
		string temp_stat = "";
		string temp_value = "";
		int tTotal = tKingdomOpinion.total;
		if (tTotal >= 0)
		{
			temp_value += Toolbox.coloredText(tTotal.ToText(), "#43FF43", false);
			temp_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null, false), "#43FF43", false);
		}
		else
		{
			temp_value += Toolbox.coloredText(tTotal.ToText(), "#FB2C21", false);
			temp_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null, false), "#FB2C21", false);
		}
		temp_stat += "\n------------";
		temp_value += "\n------------";
		temp_stat += "\n";
		temp_value += "\n";
		bool positiveFound = false;
		bool tPlusToMinus = false;
		for (int i = 0; i < pTooltip.opinion_list.Count; i++)
		{
			TooltipOpinionInfo tInfo = pTooltip.opinion_list[i];
			if (tInfo.value > 0)
			{
				positiveFound = true;
			}
			if (tInfo.value < 0 && !tPlusToMinus && i > 0 && positiveFound)
			{
				tPlusToMinus = true;
				temp_stat += "\n---";
				temp_value += "\n---";
			}
			if (i > 0)
			{
				temp_stat += "\n";
				temp_value += "\n";
			}
			if (tInfo.value > 0)
			{
				temp_value += Toolbox.coloredText(tInfo.value.ToString("+##,#;-##,#;0"), "#43FF43", false);
				temp_stat += Toolbox.coloredText(LocalizedTextManager.getText(tInfo.translation_key, null, false), "#43FF43", false);
			}
			else
			{
				temp_value += Toolbox.coloredText(tInfo.value.ToString("+##,#;-##,#;0"), "#FB2C21", false);
				temp_stat += Toolbox.coloredText(LocalizedTextManager.getText(tInfo.translation_key, null, false), "#FB2C21", false);
			}
		}
		Transform transform = pTooltip.transform.Find("StatsOpinion");
		Text tDescriptionStats = transform.Find("StatsDescription").GetComponent<Text>();
		Text tValuesStats = transform.Find("StatsValues").GetComponent<Text>();
		tDescriptionStats.text = string.Empty;
		tValuesStats.text = string.Empty;
		pTooltip.showOpinion(temp_stat, temp_value, tDescriptionStats, tValuesStats);
		tValuesStats.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		tDescriptionStats.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		pTooltip.stats_container.SetActive(true);
	}

	// Token: 0x06003D38 RID: 15672 RVA: 0x001AD4C8 File Offset: 0x001AB6C8
	private void showTaxonomy(Tooltip pTooltip, string pType, TooltipData pData)
	{
		ActorAsset actorAsset = AssetManager.actor_library.get(pData.subspecies.data.species_id);
		string tTaxonomyRank = pData.tip_name;
		string tTaxonomyRankId = actorAsset.getTaxonomyRank(tTaxonomyRank);
		string tColorHex = ColorStyleLibrary.m.getColorForTaxonomy(tTaxonomyRank);
		pTooltip.name.GetComponent<LocalizedText>().setKeyAndUpdate(tTaxonomyRank);
		pTooltip.name.color = Toolbox.makeColor(tColorHex);
		Text name = pTooltip.name;
		name.text = name.text + "\n" + Toolbox.firstLetterToUpper(tTaxonomyRankId);
		pTooltip.setDescription(LocalizedTextManager.getText("taxonomy_description_tooltip", null, false), null);
		if (pTooltip.pool_icons == null)
		{
			Transform tIconsParent = pTooltip.transform.FindRecursive("Assets");
			StatsIcon tPrefab = Resources.Load<StatsIcon>("ui/PrefabTextIconTooltipBig");
			pTooltip.pool_icons = new ObjectPoolGenericMono<StatsIcon>(tPrefab, tIconsParent);
		}
		foreach (ActorAsset tAsset in AssetManager.actor_library.list)
		{
			if (!tAsset.unit_zombie && tAsset.show_in_taxonomy_tooltip && tAsset.isTaxonomyRank(tTaxonomyRank, tTaxonomyRankId))
			{
				StatsIcon next = pTooltip.pool_icons.getNext();
				Image tIcon = next.getIcon();
				tIcon.sprite = tAsset.getSpriteIcon();
				next.text.text = tAsset.getTranslatedName();
				if (tAsset.isAvailable())
				{
					tIcon.color = Toolbox.color_white;
				}
				else
				{
					tIcon.color = Toolbox.color_black;
				}
			}
		}
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x001AD64C File Offset: 0x001AB84C
	private void showColorCounter(Tooltip pTooltip, string pType, TooltipData pData)
	{
		int tMaxColor = pData.custom_data_int["color_count"];
		pTooltip.setDescription(pData.custom_data_int["color_current"].ToString() + " / " + tMaxColor.ToString(), null);
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x001AD69C File Offset: 0x001AB89C
	private void showGameLanguage(Tooltip pTooltip, string pType, TooltipData pData)
	{
		GameLanguageAsset tAsset = pData.game_language_asset;
		pTooltip.name.text = tAsset.name;
		if (!tAsset.export)
		{
			return;
		}
		if (!tAsset.show_translators)
		{
			return;
		}
		GameLanguageData tLanguageData = tAsset.getLanguageData();
		if (tLanguageData == null)
		{
			return;
		}
		string[] active = tLanguageData.active;
		if (active != null && active.Length != 0)
		{
			pTooltip.resetDescription();
			pTooltip.addDescription("translators_current_translators".Localize() + ":", null);
			pTooltip.description.text = "<b>" + pTooltip.description.text + "</b>";
			foreach (string tActive in tLanguageData.active)
			{
				pTooltip.addDescription("\n" + tActive, null);
			}
		}
		string[] inactive = tLanguageData.inactive;
		if (inactive != null && inactive.Length != 0)
		{
			pTooltip.resetBottomDescription();
			pTooltip.addBottomDescription("translators_past_translators".Localize() + ":", null);
			pTooltip.description_2.text = "<b>" + pTooltip.description_2.text + "</b>";
			foreach (string tInactive in tLanguageData.inactive)
			{
				pTooltip.addBottomDescription("\n" + tInactive, null);
			}
		}
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x001AD7EC File Offset: 0x001AB9EC
	private void showAchievement(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Achievement tAchievement = pData.achievement;
		Image tIconLeft = pTooltip.transform.FindRecursive("IconLeft").GetComponent<Image>();
		Image tIconRight = pTooltip.transform.FindRecursive("IconRight").GetComponent<Image>();
		if (tAchievement.isUnlocked())
		{
			tIconLeft.color = Toolbox.color_white;
			tIconRight.color = Toolbox.color_white;
		}
		else
		{
			tIconLeft.color = Toolbox.color_black;
			tIconRight.color = Toolbox.color_black;
		}
		Sprite tSprite = tAchievement.getIcon();
		if (tSprite != null)
		{
			tIconLeft.sprite = tSprite;
			tIconRight.sprite = tSprite;
		}
		string tNameLocale = tAchievement.getLocaleID();
		pTooltip.name.GetComponent<LocalizedText>().setKeyAndUpdate(tNameLocale);
		string tDescriptionLocale;
		if (tAchievement.hidden && !tAchievement.isUnlocked())
		{
			tDescriptionLocale = "achievement_tip_hidden";
		}
		else
		{
			tDescriptionLocale = tAchievement.getDescriptionID();
		}
		string tDescriptionText = tDescriptionLocale.Localize();
		tDescriptionText = tDescriptionText.Replace("$lifeissimhours$", 24f.ToText());
		pTooltip.setDescription(tDescriptionText, null);
		bool tUnlocked = tAchievement.isUnlocked();
		string tContainerName = tUnlocked ? "unlocked" : "locked";
		string tOppositeContainerName = (!tUnlocked) ? "unlocked" : "locked";
		Transform tIconsParent = pTooltip.transform.FindRecursive(tContainerName);
		tIconsParent.parent.gameObject.SetActive(tAchievement.unlocks_something);
		pTooltip.transform.FindRecursive(tOppositeContainerName).gameObject.SetActive(false);
		if (!tAchievement.unlocks_something)
		{
			return;
		}
		tIconsParent.gameObject.SetActive(true);
		string tTitleID = (pData.achievement.unlock_assets.Count > 1) ? "unlocks_goodies" : "unlocks_goodie";
		pTooltip.setBottomDescription(tTitleID.Localize(), null);
		ObjectPoolGenericMono<StatsIcon> tPool;
		if (!tUnlocked)
		{
			if (pTooltip.pool_icons == null)
			{
				StatsIcon tPrefab = Resources.Load<StatsIcon>("ui/AchievementGoodieTooltipLocked");
				pTooltip.pool_icons = new ObjectPoolGenericMono<StatsIcon>(tPrefab, tIconsParent);
			}
			tPool = pTooltip.pool_icons;
		}
		else
		{
			if (pTooltip.pool_icons_2 == null)
			{
				StatsIcon tPrefab2 = Resources.Load<StatsIcon>("ui/AchievementGoodieTooltipUnlocked");
				pTooltip.pool_icons_2 = new ObjectPoolGenericMono<StatsIcon>(tPrefab2, tIconsParent);
			}
			tPool = pTooltip.pool_icons_2;
		}
		foreach (BaseUnlockableAsset tAsset in tAchievement.unlock_assets)
		{
			tPool.getNext().GetComponent<AchievementGoodie>().load(tAsset, tUnlocked);
		}
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x001ADA48 File Offset: 0x001ABC48
	public int sorter(TooltipOpinionInfo p1, TooltipOpinionInfo p2)
	{
		return p2.value.CompareTo(p1.value);
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x001ADA5C File Offset: 0x001ABC5C
	protected void setIconSprite(Tooltip pTooltip, string pName, string pIconName)
	{
		Transform tTransform = pTooltip.transform.FindRecursive(pName);
		if (tTransform == null)
		{
			Debug.LogError("No icon with this name! " + pName);
			return;
		}
		tTransform.GetComponent<StatsIcon>().getIcon().sprite = SpriteTextureLoader.getSprite("ui/Icons/" + pIconName);
	}

	// Token: 0x06003D3E RID: 15678 RVA: 0x001ADAB0 File Offset: 0x001ABCB0
	protected void setIconValue(Tooltip pTooltip, string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		Transform tTransform = pTooltip.transform.FindRecursive(pName);
		if (tTransform == null)
		{
			Debug.LogError("No icon with this name! " + pName);
			return;
		}
		StatsIcon component = tTransform.GetComponent<StatsIcon>();
		component.enable_animation = false;
		component.setValue(pMainVal, pMax, pColor, pFloat, pEnding, pSeparator, false);
	}

	// Token: 0x06003D3F RID: 15679 RVA: 0x001ADB04 File Offset: 0x001ABD04
	private void showTabBannerTip(Tooltip pTooltip, TooltipData pData)
	{
		if (Config.isComputer || Config.isEditor)
		{
			CustomDataContainer<bool> custom_data_bool = pData.custom_data_bool;
			if (custom_data_bool != null && custom_data_bool["tab_banner"])
			{
				string tLocalizedHotkeyText = LocalizedTextManager.getText("tab_banner_show_window", null, false);
				tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
				pTooltip.setBottomDescription(tLocalizedHotkeyText, null);
			}
		}
	}

	// Token: 0x06003D40 RID: 15680 RVA: 0x001ADB5C File Offset: 0x001ABD5C
	private void initDebug()
	{
		this.add(new TooltipAsset
		{
			id = "debug_asset",
			prefab_id = "tooltips/tooltip_asset_debug",
			callback = new TooltipShowAction(this.showAssetDebug)
		});
		this.add(new TooltipAsset
		{
			id = "debug_collection",
			prefab_id = "tooltips/tooltip_collection_data",
			callback = new TooltipShowAction(this.showCollectionData)
		});
	}

	// Token: 0x06003D41 RID: 15681 RVA: 0x001ADBD1 File Offset: 0x001ABDD1
	private void showAssetDebug(Tooltip pTooltip, string pType, TooltipData pData)
	{
		if (pData.tip_name == "actor")
		{
			this.showActorAssetDebug(pTooltip, pType, pData);
		}
		if (pData.tip_name == "building")
		{
			this.showBuildingAssetDebug(pTooltip, pType, pData);
		}
	}

	// Token: 0x06003D42 RID: 15682 RVA: 0x001ADC0C File Offset: 0x001ABE0C
	private void showActorAssetDebug(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Sprite tIcon = BaseDebugAssetElement<ActorAsset>.selected_asset.getSpriteIcon();
		pTooltip.transform.FindRecursive("IconSpecial").GetComponent<Image>().sprite = tIcon;
		pTooltip.transform.FindRecursive("IconRace").GetComponent<Image>().sprite = tIcon;
		using (ListPool<string> tFields = new ListPool<string>
		{
			"id",
			"icon",
			"has_skin",
			"banner_id",
			"skin_civ_default_male",
			"skin_civ_default_female"
		})
		{
			this.showAssetDebug<ActorAsset>(pTooltip, tFields);
		}
	}

	// Token: 0x06003D43 RID: 15683 RVA: 0x001ADCC8 File Offset: 0x001ABEC8
	private void showBuildingAssetDebug(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Sprite tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHouseTier0");
		pTooltip.transform.FindRecursive("IconSpecial").GetComponent<Image>().sprite = tSprite;
		pTooltip.transform.FindRecursive("IconRace").GetComponent<Image>().sprite = tSprite;
		using (ListPool<string> tFields = new ListPool<string>
		{
			"id",
			"civ_kingdom",
			"can_be_upgraded",
			"upgrade_to",
			"housing_slots",
			"spawn_units_asset",
			"spawn_drop_id"
		})
		{
			this.showAssetDebug<BuildingAsset>(pTooltip, tFields);
		}
	}

	// Token: 0x06003D44 RID: 15684 RVA: 0x001ADD90 File Offset: 0x001ABF90
	private void showAssetDebug<TAsset>(Tooltip pTooltip, ListPool<string> pFields) where TAsset : Asset
	{
		TAsset tAsset = BaseDebugAssetElement<TAsset>.selected_asset;
		pTooltip.name.text = tAsset.id;
		FieldInfoList componentInChildren = pTooltip.GetComponentInChildren<FieldInfoList>();
		componentInChildren.init<TAsset>(pFields);
		componentInChildren.setData(tAsset);
		pTooltip.setDescription("Need description to fix rounded tooltip", null);
	}

	// Token: 0x06003D45 RID: 15685 RVA: 0x001ADDE0 File Offset: 0x001ABFE0
	private void showCollectionData(Tooltip pTooltip, string pType, TooltipData pData)
	{
		Dictionary<string, string> tFieldData = FieldInfoList.selected_field_data;
		if (tFieldData == null)
		{
			pTooltip.setDescription("Nothing to show", null);
			return;
		}
		FieldInfoList tListComponent = pTooltip.GetComponentInChildren<FieldInfoList>();
		tListComponent.checkInitPool();
		foreach (KeyValuePair<string, string> tKeyValue in tFieldData)
		{
			tListComponent.addRow(tKeyValue.Key, tKeyValue.Value);
		}
		pTooltip.setDescription("need description to fix rounded tooltip", null);
	}

	// Token: 0x04002C6D RID: 11373
	private BaseStats _base_stats_temp = new BaseStats();

	// Token: 0x04002C6E RID: 11374
	private const string ARROW_UP_1 = " <size=4>↗</size>";

	// Token: 0x04002C6F RID: 11375
	private const string ARROW_UP_2 = " <size=4>↗↗</size>";

	// Token: 0x04002C70 RID: 11376
	private const string ARROW_UP_3 = " <size=4>↗↗↗</size>";

	// Token: 0x04002C71 RID: 11377
	private const string ARROW_DOWN_1 = " <size=4>↘</size>";

	// Token: 0x04002C72 RID: 11378
	private const string ARROW_DOWN_2 = " <size=4>↘↘</size>";

	// Token: 0x04002C73 RID: 11379
	private const string ARROW_DOWN_3 = " <size=4>↘↘↘</size>";
}
