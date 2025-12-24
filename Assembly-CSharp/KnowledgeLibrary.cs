using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000128 RID: 296
public class KnowledgeLibrary : AssetLibrary<KnowledgeAsset>
{
	// Token: 0x060008FE RID: 2302 RVA: 0x00081430 File Offset: 0x0007F630
	public override void init()
	{
		base.init();
		KnowledgeAsset knowledgeAsset = new KnowledgeAsset();
		knowledgeAsset.id = "units";
		knowledgeAsset.path_icon = "ui/Icons/iconInterestingPeople";
		knowledgeAsset.path_icon_easter_egg = "ui/Icons/iconBre";
		knowledgeAsset.button_prefab_path = "ui/RunningIcon";
		knowledgeAsset.window_id = "list_favorite_units";
		knowledgeAsset.get_library = (() => AssetManager.actor_library);
		knowledgeAsset.get_asset_sprite = new SpriteGetter(this.getActorSprite);
		knowledgeAsset.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			RunningIcon tIcon = pButton.GetComponent<RunningIcon>();
			Sprite tSprite = this.units.get_asset_sprite(pAsset);
			tIcon.setIcon(tSprite);
			this.checkButtonColor(tIcon.getIconImage(), pAsset);
		};
		knowledgeAsset.tip_button_loader = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			ActorAsset tAsset = pAsset as ActorAsset;
			pButton.GetComponent<TipButton>().setHoverAction(delegate
			{
				TooltipData tData = new TooltipData
				{
					actor_asset = tAsset
				};
				Tooltip.show(pButton, "unit_button", tData);
			}, true);
		};
		knowledgeAsset.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			ActorAsset tAsset = (ActorAsset)pAsset;
			TooltipData tData = new TooltipData
			{
				actor_asset = tAsset
			};
			Tooltip.show(pButton, "unit_button", tData);
			return tData;
		};
		this.units = this.add(knowledgeAsset);
		KnowledgeAsset knowledgeAsset2 = new KnowledgeAsset();
		knowledgeAsset2.id = "items";
		knowledgeAsset2.path_icon = "ui/Icons/iconEquipmentEditor";
		knowledgeAsset2.path_icon_easter_egg = "ui/Icons/civs/civ_armadillo";
		knowledgeAsset2.button_prefab_path = "ui/EquipmentButton";
		knowledgeAsset2.window_id = "equipment_rain_editor";
		knowledgeAsset2.get_library = (() => AssetManager.items);
		knowledgeAsset2.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset2.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.augmentationButtonLoader<EquipmentButton, EquipmentAsset>(pButton, pAsset);
		};
		knowledgeAsset2.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			EquipmentAsset tAsset = (EquipmentAsset)pAsset;
			TooltipData tData = new TooltipData
			{
				item_asset = tAsset
			};
			Tooltip.show(pButton, "equipment_in_editor", tData);
			return tData;
		};
		this.items = this.add(knowledgeAsset2);
		KnowledgeAsset knowledgeAsset3 = new KnowledgeAsset();
		knowledgeAsset3.id = "genes";
		knowledgeAsset3.path_icon = "ui/Icons/iconGene";
		knowledgeAsset3.path_icon_easter_egg = "ui/Icons/iconGreg";
		knowledgeAsset3.button_prefab_path = "ui/genetic_elements/GeneButton";
		knowledgeAsset3.window_id = "list_subspecies";
		knowledgeAsset3.get_library = (() => AssetManager.gene_library);
		knowledgeAsset3.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset3.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.augmentationButtonLoader<GeneButton, GeneAsset>(pButton, pAsset);
		};
		knowledgeAsset3.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			GeneAsset tAsset = (GeneAsset)pAsset;
			TooltipData tData = new TooltipData
			{
				gene = tAsset
			};
			Tooltip.show(pButton, "gene", tData);
			return tData;
		};
		this.genes = this.add(knowledgeAsset3);
		KnowledgeAsset knowledgeAsset4 = new KnowledgeAsset();
		knowledgeAsset4.id = "traits";
		knowledgeAsset4.path_icon = "ui/Icons/iconEditTrait";
		knowledgeAsset4.path_icon_easter_egg = "ui/Icons/iconZombie";
		knowledgeAsset4.button_prefab_path = "ui/unit_window_elements/ActorTraitButton";
		knowledgeAsset4.window_id = "trait_rain_editor";
		knowledgeAsset4.get_library = (() => AssetManager.traits);
		knowledgeAsset4.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset4.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<ActorTraitButton, ActorTrait>(pButton, pAsset);
		};
		knowledgeAsset4.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			ActorTrait tAsset = (ActorTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		knowledgeAsset4.click_icon_action = delegate(KnowledgeAsset pAsset)
		{
			Config.selected_trait_editor = PowerLibrary.traits_delta_rain_edit.id;
			ScrollWindow.showWindow(pAsset.window_id);
		};
		this.traits = this.add(knowledgeAsset4);
		KnowledgeAsset knowledgeAsset5 = new KnowledgeAsset();
		knowledgeAsset5.id = "subspecies_traits";
		knowledgeAsset5.path_icon = "ui/Icons/iconSpecies";
		knowledgeAsset5.path_icon_easter_egg = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_budding";
		knowledgeAsset5.button_prefab_path = "ui/subspecies_window_elements/SubspeciesTraitButton";
		knowledgeAsset5.window_id = "list_subspecies";
		knowledgeAsset5.get_library = (() => AssetManager.subspecies_traits);
		knowledgeAsset5.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset5.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<SubspeciesTraitButton, SubspeciesTrait>(pButton, pAsset);
		};
		knowledgeAsset5.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			SubspeciesTrait tAsset = (SubspeciesTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				subspecies_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.subspecies_traits = this.add(knowledgeAsset5);
		KnowledgeAsset knowledgeAsset6 = new KnowledgeAsset();
		knowledgeAsset6.id = "culture_traits";
		knowledgeAsset6.path_icon = "ui/Icons/iconCulture";
		knowledgeAsset6.path_icon_easter_egg = "ui/Icons/iconEvilMage";
		knowledgeAsset6.button_prefab_path = "ui/culture_window_elements/CultureTraitButton";
		knowledgeAsset6.window_id = "list_cultures";
		knowledgeAsset6.get_library = (() => AssetManager.culture_traits);
		knowledgeAsset6.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset6.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<CultureTraitButton, CultureTrait>(pButton, pAsset);
		};
		knowledgeAsset6.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			CultureTrait tAsset = (CultureTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				culture_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.culture_traits = this.add(knowledgeAsset6);
		KnowledgeAsset knowledgeAsset7 = new KnowledgeAsset();
		knowledgeAsset7.id = "language_traits";
		knowledgeAsset7.path_icon = "ui/Icons/iconLanguage";
		knowledgeAsset7.path_icon_easter_egg = "ui/Icons/iconShrug";
		knowledgeAsset7.button_prefab_path = "ui/language_window_elements/LanguageTraitButton";
		knowledgeAsset7.window_id = "list_languages";
		knowledgeAsset7.get_library = (() => AssetManager.language_traits);
		knowledgeAsset7.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset7.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<LanguageTraitButton, LanguageTrait>(pButton, pAsset);
		};
		knowledgeAsset7.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			LanguageTrait tAsset = (LanguageTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				language_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.language_traits = this.add(knowledgeAsset7);
		KnowledgeAsset knowledgeAsset8 = new KnowledgeAsset();
		knowledgeAsset8.id = "clan_traits";
		knowledgeAsset8.path_icon = "ui/Icons/iconClan";
		knowledgeAsset8.path_icon_easter_egg = "ui/Icons/iconLivingPlants";
		knowledgeAsset8.button_prefab_path = "ui/clan_window_elements/ClanTraitButton";
		knowledgeAsset8.window_id = "list_clans";
		knowledgeAsset8.get_library = (() => AssetManager.clan_traits);
		knowledgeAsset8.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset8.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<ClanTraitButton, ClanTrait>(pButton, pAsset);
		};
		knowledgeAsset8.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			ClanTrait tAsset = (ClanTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				clan_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.clan_traits = this.add(knowledgeAsset8);
		KnowledgeAsset knowledgeAsset9 = new KnowledgeAsset();
		knowledgeAsset9.id = "religion_traits";
		knowledgeAsset9.path_icon = "ui/Icons/iconReligion";
		knowledgeAsset9.path_icon_easter_egg = "ui/Icons/iconWhiteMage";
		knowledgeAsset9.button_prefab_path = "ui/religion_window_elements/ReligionTraitButton";
		knowledgeAsset9.window_id = "list_religions";
		knowledgeAsset9.get_library = (() => AssetManager.religion_traits);
		knowledgeAsset9.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset9.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<ReligionTraitButton, ReligionTrait>(pButton, pAsset);
		};
		knowledgeAsset9.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			ReligionTrait tAsset = (ReligionTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				religion_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.religion_traits = this.add(knowledgeAsset9);
		KnowledgeAsset knowledgeAsset10 = new KnowledgeAsset();
		knowledgeAsset10.id = "kingdom_traits";
		knowledgeAsset10.show_in_knowledge_window = false;
		knowledgeAsset10.path_icon = "ui/Icons/iconKingdom";
		knowledgeAsset10.path_icon_easter_egg = "ui/Icons/iconWhiteMage";
		knowledgeAsset10.button_prefab_path = "ui/kingdom_window_elements/KingdomTraitButton";
		knowledgeAsset10.window_id = "list_kingdoms";
		knowledgeAsset10.get_library = (() => AssetManager.kingdoms_traits);
		knowledgeAsset10.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset10.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.traitButtonLoader<KingdomTraitButton, KingdomTrait>(pButton, pAsset);
		};
		knowledgeAsset10.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			KingdomTrait tAsset = (KingdomTrait)pAsset;
			TooltipData tData = new TooltipData
			{
				kingdom_trait = tAsset,
				is_editor_augmentation_button = true
			};
			Tooltip.show(pButton, tAsset.typed_id, tData);
			return tData;
		};
		this.kingdom_traits = this.add(knowledgeAsset10);
		KnowledgeAsset knowledgeAsset11 = new KnowledgeAsset();
		knowledgeAsset11.id = "plots";
		knowledgeAsset11.path_icon = "ui/Icons/iconPlot";
		knowledgeAsset11.path_icon_easter_egg = "ui/Icons/actor_traits/iconGenius";
		knowledgeAsset11.button_prefab_path = "ui/PlotButton";
		knowledgeAsset11.window_id = "list_plots";
		knowledgeAsset11.get_library = (() => AssetManager.plots_library);
		knowledgeAsset11.get_asset_sprite = new SpriteGetter(this.getAugmentationSprite);
		knowledgeAsset11.load_button = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			this.augmentationButtonLoader<PlotButton, PlotAsset>(pButton, pAsset);
		};
		knowledgeAsset11.show_tooltip = delegate(Transform pButton, BaseUnlockableAsset pAsset)
		{
			PlotAsset tAsset = (PlotAsset)pAsset;
			TooltipData tData = new TooltipData
			{
				plot_asset = tAsset
			};
			Tooltip.show(pButton, "plot_in_editor", tData);
			return tData;
		};
		this.plots = this.add(knowledgeAsset11);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00081C66 File Offset: 0x0007FE66
	private Sprite getActorSprite(BaseUnlockableAsset pAsset)
	{
		return ((ActorAsset)pAsset).getSpriteIcon();
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00081C73 File Offset: 0x0007FE73
	private Sprite getAugmentationSprite(BaseUnlockableAsset pAsset)
	{
		return ((BaseAugmentationAsset)pAsset).getSprite();
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00081C80 File Offset: 0x0007FE80
	private TButton augmentationButtonLoader<TButton, TAsset>(Transform pButton, BaseUnlockableAsset pAsset) where TButton : AugmentationButton<TAsset> where TAsset : BaseAugmentationAsset
	{
		TButton tButton = pButton.GetComponent<TButton>();
		tButton.load(pAsset as TAsset);
		tButton.locked_bg.enabled = false;
		this.checkButtonColor(tButton.image, pAsset);
		tButton.is_editor_button = true;
		return tButton;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00081CDC File Offset: 0x0007FEDC
	private TButton traitButtonLoader<TButton, TAsset>(Transform pButton, BaseUnlockableAsset pAsset) where TButton : TraitButton<TAsset> where TAsset : BaseTrait<TAsset>
	{
		TButton tButton = this.augmentationButtonLoader<TButton, TAsset>(pButton, pAsset);
		this.checkButtonColor(tButton.image, pAsset);
		return tButton;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00081D05 File Offset: 0x0007FF05
	private void checkButtonColor(Image pImage, BaseUnlockableAsset pAsset)
	{
		if (pAsset.isUnlockedByPlayer())
		{
			pImage.color = Toolbox.color_white;
			return;
		}
		pImage.color = Toolbox.color_black;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00081D28 File Offset: 0x0007FF28
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (KnowledgeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}

	// Token: 0x04000939 RID: 2361
	private KnowledgeAsset units;

	// Token: 0x0400093A RID: 2362
	private KnowledgeAsset items;

	// Token: 0x0400093B RID: 2363
	private KnowledgeAsset genes;

	// Token: 0x0400093C RID: 2364
	private KnowledgeAsset traits;

	// Token: 0x0400093D RID: 2365
	private KnowledgeAsset subspecies_traits;

	// Token: 0x0400093E RID: 2366
	private KnowledgeAsset culture_traits;

	// Token: 0x0400093F RID: 2367
	private KnowledgeAsset language_traits;

	// Token: 0x04000940 RID: 2368
	private KnowledgeAsset clan_traits;

	// Token: 0x04000941 RID: 2369
	private KnowledgeAsset religion_traits;

	// Token: 0x04000942 RID: 2370
	private KnowledgeAsset kingdom_traits;

	// Token: 0x04000943 RID: 2371
	private KnowledgeAsset plots;
}
