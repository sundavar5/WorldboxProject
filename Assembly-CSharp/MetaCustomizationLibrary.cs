using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class MetaCustomizationLibrary : AssetLibrary<MetaCustomizationAsset>
{
	// Token: 0x0600195D RID: 6493 RVA: 0x000EF0E8 File Offset: 0x000ED2E8
	public override void init()
	{
		base.init();
		MetaCustomizationAsset metaCustomizationAsset = new MetaCustomizationAsset();
		metaCustomizationAsset.id = "religion";
		metaCustomizationAsset.meta_type = MetaType.Religion;
		metaCustomizationAsset.banner_prefab_id = "ui/PrefabBannerReligion";
		metaCustomizationAsset.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			ReligionBanner religionBanner = Object.Instantiate<ReligionBanner>(Resources.Load<ReligionBanner>(pAsset.banner_prefab_id), pParent);
			religionBanner.enable_default_click = false;
			religionBanner.load(pNanoObject as Religion);
			return religionBanner;
		};
		metaCustomizationAsset.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<ReligionCustomizeWindow>();
		};
		metaCustomizationAsset.customize_window_id = "religion_customize";
		metaCustomizationAsset.option_1_get = (() => SelectedMetas.selected_religion.data.banner_background_id);
		metaCustomizationAsset.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_religion.data.banner_background_id = pValue;
		};
		metaCustomizationAsset.option_2_get = (() => SelectedMetas.selected_religion.data.banner_icon_id);
		metaCustomizationAsset.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_religion.data.banner_icon_id = pValue;
		};
		metaCustomizationAsset.color_get = (() => SelectedMetas.selected_religion.data.color_id);
		metaCustomizationAsset.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_religion.data.setColorID(pValue);
		};
		metaCustomizationAsset.color_library = (() => AssetManager.religion_colors_library);
		metaCustomizationAsset.option_1_count = (() => AssetManager.religion_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset.option_2_count = (() => AssetManager.religion_banners_library.getCurrentAsset().icons.Count);
		metaCustomizationAsset.title_locale = "customize_religion";
		metaCustomizationAsset.option_1_locale = "religion_background";
		metaCustomizationAsset.option_2_locale = "religion_element";
		metaCustomizationAsset.color_locale = "religion_color";
		metaCustomizationAsset.icon_banner = "iconReligion";
		metaCustomizationAsset.icon_creature = "iconLivingPlants";
		this.add(metaCustomizationAsset);
		MetaCustomizationAsset metaCustomizationAsset2 = new MetaCustomizationAsset();
		metaCustomizationAsset2.id = "culture";
		metaCustomizationAsset2.meta_type = MetaType.Culture;
		metaCustomizationAsset2.banner_prefab_id = "ui/PrefabBannerCulture";
		metaCustomizationAsset2.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			CultureBanner cultureBanner = Object.Instantiate<CultureBanner>(Resources.Load<CultureBanner>(pAsset.banner_prefab_id), pParent);
			cultureBanner.enable_default_click = false;
			cultureBanner.load(pNanoObject as Culture);
			return cultureBanner;
		};
		metaCustomizationAsset2.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<CultureCustomizeWindow>();
		};
		metaCustomizationAsset2.customize_window_id = "culture_customize";
		metaCustomizationAsset2.option_1_get = (() => SelectedMetas.selected_culture.data.banner_decor_id);
		metaCustomizationAsset2.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_culture.data.banner_decor_id = pValue;
		};
		metaCustomizationAsset2.option_2_get = (() => SelectedMetas.selected_culture.data.banner_element_id);
		metaCustomizationAsset2.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_culture.data.banner_element_id = pValue;
		};
		metaCustomizationAsset2.color_get = (() => SelectedMetas.selected_culture.data.color_id);
		metaCustomizationAsset2.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_culture.data.setColorID(pValue);
		};
		metaCustomizationAsset2.color_library = (() => AssetManager.culture_colors_library);
		metaCustomizationAsset2.option_1_count = (() => AssetManager.culture_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset2.option_2_count = (() => AssetManager.culture_banners_library.getCurrentAsset().icons.Count);
		metaCustomizationAsset2.title_locale = "customize_culture";
		metaCustomizationAsset2.option_1_locale = "culture_decor";
		metaCustomizationAsset2.option_2_locale = "culture_element";
		metaCustomizationAsset2.color_locale = "culture_color";
		metaCustomizationAsset2.icon_banner = "iconCulture";
		metaCustomizationAsset2.icon_creature = "iconSuperPumpkin";
		this.add(metaCustomizationAsset2);
		MetaCustomizationAsset metaCustomizationAsset3 = new MetaCustomizationAsset();
		metaCustomizationAsset3.id = "family";
		metaCustomizationAsset3.meta_type = MetaType.Family;
		metaCustomizationAsset3.banner_prefab_id = "ui/PrefabBannerFamily";
		metaCustomizationAsset3.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			FamilyBanner familyBanner = Object.Instantiate<FamilyBanner>(Resources.Load<FamilyBanner>(pAsset.banner_prefab_id), pParent);
			familyBanner.enable_default_click = false;
			familyBanner.load(pNanoObject as Family);
			return familyBanner;
		};
		metaCustomizationAsset3.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<FamilyCustomizeWindow>();
		};
		metaCustomizationAsset3.customize_window_id = "family_customize";
		metaCustomizationAsset3.option_1_get = (() => SelectedMetas.selected_family.data.banner_background_id);
		metaCustomizationAsset3.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_family.data.banner_background_id = pValue;
		};
		metaCustomizationAsset3.option_2_get = (() => SelectedMetas.selected_family.data.banner_frame_id);
		metaCustomizationAsset3.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_family.data.banner_frame_id = pValue;
		};
		metaCustomizationAsset3.option_2_color_editable = false;
		metaCustomizationAsset3.color_get = (() => SelectedMetas.selected_family.data.color_id);
		metaCustomizationAsset3.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_family.data.setColorID(pValue);
		};
		metaCustomizationAsset3.color_library = (() => AssetManager.families_colors_library);
		metaCustomizationAsset3.option_1_count = (() => AssetManager.family_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset3.option_2_count = (() => AssetManager.family_banners_library.getCurrentAsset().frames.Count);
		metaCustomizationAsset3.title_locale = "customize_family";
		metaCustomizationAsset3.option_1_locale = "family_background";
		metaCustomizationAsset3.option_2_locale = "family_frame";
		metaCustomizationAsset3.color_locale = "family_color";
		metaCustomizationAsset3.icon_banner = "iconFamily";
		metaCustomizationAsset3.icon_creature = "iconLivingPlants";
		this.add(metaCustomizationAsset3);
		MetaCustomizationAsset metaCustomizationAsset4 = new MetaCustomizationAsset();
		metaCustomizationAsset4.id = "language";
		metaCustomizationAsset4.meta_type = MetaType.Language;
		metaCustomizationAsset4.banner_prefab_id = "ui/PrefabBannerLanguage";
		metaCustomizationAsset4.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			LanguageBanner languageBanner = Object.Instantiate<LanguageBanner>(Resources.Load<LanguageBanner>(pAsset.banner_prefab_id), pParent);
			languageBanner.enable_default_click = false;
			languageBanner.load(pNanoObject as Language);
			return languageBanner;
		};
		metaCustomizationAsset4.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<LanguageCustomizeWindow>();
		};
		metaCustomizationAsset4.customize_window_id = "language_customize";
		metaCustomizationAsset4.option_1_get = (() => SelectedMetas.selected_language.data.banner_background_id);
		metaCustomizationAsset4.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_language.data.banner_background_id = pValue;
		};
		metaCustomizationAsset4.option_2_get = (() => SelectedMetas.selected_language.data.banner_icon_id);
		metaCustomizationAsset4.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_language.data.banner_icon_id = pValue;
		};
		metaCustomizationAsset4.color_get = (() => SelectedMetas.selected_language.data.color_id);
		metaCustomizationAsset4.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_language.data.setColorID(pValue);
		};
		metaCustomizationAsset4.color_library = (() => AssetManager.languages_colors_library);
		metaCustomizationAsset4.option_1_count = (() => AssetManager.language_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset4.option_2_count = (() => AssetManager.language_banners_library.getCurrentAsset().icons.Count);
		metaCustomizationAsset4.title_locale = "customize_language";
		metaCustomizationAsset4.option_1_locale = "language_background";
		metaCustomizationAsset4.option_2_locale = "language_element";
		metaCustomizationAsset4.color_locale = "language_color";
		metaCustomizationAsset4.icon_banner = "iconLanguage";
		metaCustomizationAsset4.icon_creature = "iconLivingPlants";
		this.add(metaCustomizationAsset4);
		MetaCustomizationAsset metaCustomizationAsset5 = new MetaCustomizationAsset();
		metaCustomizationAsset5.id = "subspecies";
		metaCustomizationAsset5.meta_type = MetaType.Subspecies;
		metaCustomizationAsset5.banner_prefab_id = "ui/PrefabBannerSubspecies";
		metaCustomizationAsset5.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			SubspeciesBanner subspeciesBanner = Object.Instantiate<SubspeciesBanner>(Resources.Load<SubspeciesBanner>(pAsset.banner_prefab_id), pParent);
			subspeciesBanner.enable_default_click = false;
			subspeciesBanner.load(pNanoObject as Subspecies);
			return subspeciesBanner;
		};
		metaCustomizationAsset5.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<SubspeciesCustomizeWindow>();
		};
		metaCustomizationAsset5.customize_window_id = "subspecies_customize";
		metaCustomizationAsset5.option_1_get = (() => SelectedMetas.selected_subspecies.data.banner_background_id);
		metaCustomizationAsset5.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_subspecies.data.banner_background_id = pValue;
		};
		metaCustomizationAsset5.color_get = (() => SelectedMetas.selected_subspecies.data.color_id);
		metaCustomizationAsset5.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_subspecies.data.setColorID(pValue);
		};
		metaCustomizationAsset5.color_library = (() => AssetManager.subspecies_colors_library);
		metaCustomizationAsset5.option_1_count = (() => AssetManager.subspecies_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset5.option_2_editable = false;
		metaCustomizationAsset5.title_locale = "customize_subspecies";
		metaCustomizationAsset5.option_1_locale = "subspecies_background";
		metaCustomizationAsset5.color_locale = "subspecies_color";
		metaCustomizationAsset5.icon_banner = "iconSubspeciesList";
		metaCustomizationAsset5.icon_creature = "iconLivingPlants";
		this.add(metaCustomizationAsset5);
		MetaCustomizationAsset metaCustomizationAsset6 = new MetaCustomizationAsset();
		metaCustomizationAsset6.id = "kingdom";
		metaCustomizationAsset6.meta_type = MetaType.Kingdom;
		metaCustomizationAsset6.banner_prefab_id = "ui/PrefabBannerKingdom";
		metaCustomizationAsset6.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			KingdomBanner kingdomBanner = Object.Instantiate<KingdomBanner>(Resources.Load<KingdomBanner>(pAsset.banner_prefab_id), pParent);
			kingdomBanner.enable_default_click = false;
			kingdomBanner.load(pNanoObject as Kingdom);
			return kingdomBanner;
		};
		metaCustomizationAsset6.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<KingdomCustomizeWindow>();
		};
		metaCustomizationAsset6.customize_window_id = "kingdom_customize";
		metaCustomizationAsset6.option_1_get = (() => SelectedMetas.selected_kingdom.data.banner_background_id);
		metaCustomizationAsset6.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_kingdom.data.banner_background_id = pValue;
		};
		metaCustomizationAsset6.option_2_get = (() => SelectedMetas.selected_kingdom.data.banner_icon_id);
		metaCustomizationAsset6.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_kingdom.data.banner_icon_id = pValue;
		};
		metaCustomizationAsset6.color_get = (() => SelectedMetas.selected_kingdom.data.color_id);
		metaCustomizationAsset6.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_kingdom.data.setColorID(pValue);
		};
		metaCustomizationAsset6.color_library = (() => AssetManager.kingdom_colors_library);
		metaCustomizationAsset6.option_1_count = (() => AssetManager.kingdom_banners_library.get(SelectedMetas.selected_kingdom.getActorAsset().banner_id).backgrounds.Count);
		metaCustomizationAsset6.option_2_count = (() => AssetManager.kingdom_banners_library.get(SelectedMetas.selected_kingdom.getActorAsset().banner_id).icons.Count);
		metaCustomizationAsset6.title_locale = "customize_kingdom";
		metaCustomizationAsset6.option_1_locale = "banner_design";
		metaCustomizationAsset6.option_2_locale = "banner_emblem";
		metaCustomizationAsset6.color_locale = "kingdom_color";
		metaCustomizationAsset6.icon_banner = "iconCrown";
		metaCustomizationAsset6.icon_creature = "iconBiomass";
		this.add(metaCustomizationAsset6);
		MetaCustomizationAsset metaCustomizationAsset7 = new MetaCustomizationAsset();
		metaCustomizationAsset7.id = "city";
		metaCustomizationAsset7.meta_type = MetaType.City;
		metaCustomizationAsset7.localization_title = "village";
		metaCustomizationAsset7.option_1_editable = false;
		metaCustomizationAsset7.option_2_editable = false;
		metaCustomizationAsset7.banner_prefab_id = "ui/PrefabBannerCity";
		metaCustomizationAsset7.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			CityBanner cityBanner = Object.Instantiate<CityBanner>(Resources.Load<CityBanner>(pAsset.banner_prefab_id), pParent);
			cityBanner.enable_default_click = false;
			cityBanner.load(pNanoObject as City);
			return cityBanner;
		};
		metaCustomizationAsset7.customize_window_id = "kingdom_customize";
		this.add(metaCustomizationAsset7);
		MetaCustomizationAsset metaCustomizationAsset8 = new MetaCustomizationAsset();
		metaCustomizationAsset8.id = "army";
		metaCustomizationAsset8.meta_type = MetaType.Army;
		metaCustomizationAsset8.localization_title = "army";
		metaCustomizationAsset8.option_1_editable = false;
		metaCustomizationAsset8.option_2_editable = false;
		metaCustomizationAsset8.banner_prefab_id = "ui/PrefabBannerArmy";
		metaCustomizationAsset8.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			ArmyBanner armyBanner = Object.Instantiate<ArmyBanner>(Resources.Load<ArmyBanner>(pAsset.banner_prefab_id), pParent);
			armyBanner.enable_default_click = false;
			armyBanner.load(pNanoObject as Army);
			return armyBanner;
		};
		metaCustomizationAsset8.color_library = (() => AssetManager.kingdom_colors_library);
		metaCustomizationAsset8.customize_window_id = "kingdom_customize";
		this.add(metaCustomizationAsset8);
		MetaCustomizationAsset metaCustomizationAsset9 = new MetaCustomizationAsset();
		metaCustomizationAsset9.id = "clan";
		metaCustomizationAsset9.meta_type = MetaType.Clan;
		metaCustomizationAsset9.banner_prefab_id = "ui/PrefabBannerClan";
		metaCustomizationAsset9.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			ClanBanner clanBanner = Object.Instantiate<ClanBanner>(Resources.Load<ClanBanner>(pAsset.banner_prefab_id), pParent);
			clanBanner.enable_default_click = false;
			clanBanner.load(pNanoObject as Clan);
			return clanBanner;
		};
		metaCustomizationAsset9.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<ClanCustomizeWindow>();
		};
		metaCustomizationAsset9.customize_window_id = "clan_customize";
		metaCustomizationAsset9.option_1_get = (() => SelectedMetas.selected_clan.data.banner_background_id);
		metaCustomizationAsset9.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_clan.data.banner_background_id = pValue;
		};
		metaCustomizationAsset9.option_2_get = (() => SelectedMetas.selected_clan.data.banner_icon_id);
		metaCustomizationAsset9.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_clan.data.banner_icon_id = pValue;
		};
		metaCustomizationAsset9.color_get = (() => SelectedMetas.selected_clan.data.color_id);
		metaCustomizationAsset9.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_clan.data.setColorID(pValue);
		};
		metaCustomizationAsset9.color_library = (() => AssetManager.clan_colors_library);
		metaCustomizationAsset9.option_1_count = (() => AssetManager.clan_banners_library.getCurrentAsset().backgrounds.Count);
		metaCustomizationAsset9.option_2_count = (() => AssetManager.clan_banners_library.getCurrentAsset().icons.Count);
		metaCustomizationAsset9.title_locale = "customize_clan";
		metaCustomizationAsset9.option_1_locale = "clan_background";
		metaCustomizationAsset9.option_2_locale = "clan_icon";
		metaCustomizationAsset9.color_locale = "clan_color";
		metaCustomizationAsset9.icon_banner = "iconClan";
		metaCustomizationAsset9.icon_creature = "iconSuperPumpkin";
		this.add(metaCustomizationAsset9);
		MetaCustomizationAsset metaCustomizationAsset10 = new MetaCustomizationAsset();
		metaCustomizationAsset10.id = "alliance";
		metaCustomizationAsset10.meta_type = MetaType.Alliance;
		metaCustomizationAsset10.banner_prefab_id = "ui/PrefabBannerAlliance";
		metaCustomizationAsset10.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			AllianceBanner allianceBanner = Object.Instantiate<AllianceBanner>(Resources.Load<AllianceBanner>(pAsset.banner_prefab_id), pParent);
			allianceBanner.enable_default_click = false;
			allianceBanner.load(pNanoObject as Alliance);
			return allianceBanner;
		};
		metaCustomizationAsset10.customize_component = delegate(GameObject pGameObject)
		{
			pGameObject.AddComponent<AllianceCustomizeWindow>();
		};
		metaCustomizationAsset10.customize_window_id = "alliance_customize";
		metaCustomizationAsset10.option_1_get = (() => SelectedMetas.selected_alliance.data.banner_background_id);
		metaCustomizationAsset10.option_1_set = delegate(int pValue)
		{
			SelectedMetas.selected_alliance.data.banner_background_id = pValue;
		};
		metaCustomizationAsset10.option_2_get = (() => SelectedMetas.selected_alliance.data.banner_icon_id);
		metaCustomizationAsset10.option_2_set = delegate(int pValue)
		{
			SelectedMetas.selected_alliance.data.banner_icon_id = pValue;
		};
		metaCustomizationAsset10.color_get = (() => SelectedMetas.selected_alliance.data.color_id);
		metaCustomizationAsset10.color_set = delegate(int pValue)
		{
			SelectedMetas.selected_alliance.data.setColorID(pValue);
		};
		metaCustomizationAsset10.color_library = (() => AssetManager.kingdom_colors_library);
		metaCustomizationAsset10.option_1_count = (() => World.world.alliances.getBackgroundsList().Length);
		metaCustomizationAsset10.option_2_count = (() => World.world.alliances.getIconsList().Length);
		metaCustomizationAsset10.title_locale = "customize_alliance";
		metaCustomizationAsset10.option_1_locale = "alliance_background";
		metaCustomizationAsset10.option_2_locale = "alliance_icon";
		metaCustomizationAsset10.color_locale = "alliance_color";
		metaCustomizationAsset10.icon_banner = "iconAlliance";
		metaCustomizationAsset10.icon_creature = "iconSuperPumpkin";
		this.add(metaCustomizationAsset10);
		MetaCustomizationAsset metaCustomizationAsset11 = new MetaCustomizationAsset();
		metaCustomizationAsset11.id = "plot";
		metaCustomizationAsset11.meta_type = MetaType.Plot;
		metaCustomizationAsset11.editable = false;
		metaCustomizationAsset11.banner_prefab_id = "ui/PrefabBannerPlot";
		metaCustomizationAsset11.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			PlotBanner plotBanner = Object.Instantiate<PlotBanner>(Resources.Load<PlotBanner>(pAsset.banner_prefab_id), pParent);
			plotBanner.load(pNanoObject);
			return plotBanner;
		};
		this.add(metaCustomizationAsset11);
		MetaCustomizationAsset metaCustomizationAsset12 = new MetaCustomizationAsset();
		metaCustomizationAsset12.id = "war";
		metaCustomizationAsset12.meta_type = MetaType.War;
		metaCustomizationAsset12.editable = false;
		metaCustomizationAsset12.banner_prefab_id = "ui/PrefabBannerWar";
		metaCustomizationAsset12.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			WarBanner warBanner = Object.Instantiate<WarBanner>(Resources.Load<WarBanner>(pAsset.banner_prefab_id), pParent);
			warBanner.load(pNanoObject);
			return warBanner;
		};
		this.add(metaCustomizationAsset12);
		MetaCustomizationAsset metaCustomizationAsset13 = new MetaCustomizationAsset();
		metaCustomizationAsset13.id = "unit";
		metaCustomizationAsset13.meta_type = MetaType.Unit;
		metaCustomizationAsset13.editable = false;
		metaCustomizationAsset13.banner_prefab_id = "ui/UnitAvatarElement";
		metaCustomizationAsset13.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			UiUnitAvatarElement uiUnitAvatarElement = Object.Instantiate<UiUnitAvatarElement>(Resources.Load<UiUnitAvatarElement>(pAsset.banner_prefab_id), pParent);
			uiUnitAvatarElement.load(pNanoObject);
			uiUnitAvatarElement.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			return uiUnitAvatarElement;
		};
		this.add(metaCustomizationAsset13);
		MetaCustomizationAsset metaCustomizationAsset14 = new MetaCustomizationAsset();
		metaCustomizationAsset14.id = "item";
		metaCustomizationAsset14.meta_type = MetaType.Item;
		metaCustomizationAsset14.editable = false;
		metaCustomizationAsset14.banner_prefab_id = "ui/EquipmentButton";
		metaCustomizationAsset14.get_banner = delegate(MetaCustomizationAsset pAsset, NanoObject pNanoObject, Transform pParent)
		{
			EquipmentButton equipmentButton = Object.Instantiate<EquipmentButton>(Resources.Load<EquipmentButton>(pAsset.banner_prefab_id), pParent);
			equipmentButton.load(pNanoObject);
			return equipmentButton;
		};
		this.add(metaCustomizationAsset14);
		this.add(new MetaCustomizationAsset
		{
			id = "world",
			meta_type = MetaType.World,
			editable = false,
			option_1_editable = false,
			option_2_editable = false,
			color_editable = false
		});
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000F0395 File Offset: 0x000EE595
	public MetaCustomizationAsset getAsset(MetaType pType)
	{
		return this.get(pType.AsString());
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000F03A4 File Offset: 0x000EE5A4
	public override void post_init()
	{
		base.post_init();
		foreach (MetaCustomizationAsset tAsset in this.list)
		{
			if (tAsset.localization_title == null)
			{
				tAsset.localization_title = tAsset.id;
			}
		}
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000F040C File Offset: 0x000EE60C
	public override void linkAssets()
	{
		base.linkAssets();
		using (List<MetaCustomizationAsset>.Enumerator enumerator = this.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MetaCustomizationAsset tAsset = enumerator.Current;
				if (tAsset.color_count == null)
				{
					tAsset.color_count = (() => tAsset.color_library().list.Count);
				}
			}
		}
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x000F0490 File Offset: 0x000EE690
	public override void editorDiagnostic()
	{
		foreach (MetaCustomizationAsset tAsset in this.list)
		{
			if (tAsset.editable)
			{
				if (tAsset.color_count == null)
				{
					BaseAssetLibrary.logAssetError("Missing <e>color_count</e>", tAsset.id);
				}
				if (tAsset.option_1_editable && tAsset.option_1_count == null)
				{
					BaseAssetLibrary.logAssetError("Missing <e>option_1_count</e>", tAsset.id);
				}
				if (tAsset.option_2_editable && tAsset.option_2_count == null)
				{
					BaseAssetLibrary.logAssetError("Missing <e>option_2_count</e>", tAsset.id);
				}
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000F0544 File Offset: 0x000EE744
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MetaCustomizationAsset tAsset in this.list)
		{
			if (tAsset.editable)
			{
				foreach (string tLocaleID in tAsset.getLocaleIDs())
				{
					this.checkLocale(tAsset, tLocaleID);
				}
			}
		}
	}
}
