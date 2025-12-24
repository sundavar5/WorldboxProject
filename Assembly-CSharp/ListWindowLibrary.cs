using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class ListWindowLibrary : AssetLibrary<ListWindowAsset>
{
	// Token: 0x0600091B RID: 2331 RVA: 0x00081E50 File Offset: 0x00080050
	public override void init()
	{
		ListWindowAsset listWindowAsset = new ListWindowAsset();
		listWindowAsset.id = "list_alliances";
		listWindowAsset.meta_type = MetaType.Alliance;
		listWindowAsset.no_items_locale = "list_empty_alliances";
		listWindowAsset.art_path = "ui/illustrations/art_alliances";
		listWindowAsset.icon_path = "ui/Icons/iconAllianceList";
		listWindowAsset.set_list_component = ((Transform pTransform) => pTransform.AddComponent<AllianceListComponent>());
		this.add(listWindowAsset);
		ListWindowAsset listWindowAsset2 = new ListWindowAsset();
		listWindowAsset2.id = "list_clans";
		listWindowAsset2.meta_type = MetaType.Clan;
		listWindowAsset2.no_items_locale = "list_empty_clans";
		listWindowAsset2.art_path = "ui/illustrations/art_clans";
		listWindowAsset2.icon_path = "ui/Icons/iconClanList";
		listWindowAsset2.set_list_component = ((Transform pTransform) => pTransform.AddComponent<ClanListComponent>());
		this.add(listWindowAsset2);
		ListWindowAsset listWindowAsset3 = new ListWindowAsset();
		listWindowAsset3.id = "list_cultures";
		listWindowAsset3.meta_type = MetaType.Culture;
		listWindowAsset3.no_items_locale = "list_empty_cultures";
		listWindowAsset3.art_path = "ui/illustrations/art_cultures";
		listWindowAsset3.icon_path = "ui/Icons/iconCultureList";
		listWindowAsset3.set_list_component = ((Transform pTransform) => pTransform.AddComponent<CultureListComponent>());
		this.add(listWindowAsset3);
		ListWindowAsset listWindowAsset4 = new ListWindowAsset();
		listWindowAsset4.id = "list_cities";
		listWindowAsset4.meta_type = MetaType.City;
		listWindowAsset4.no_items_locale = "list_empty_villages";
		listWindowAsset4.art_path = "ui/illustrations/art_cities";
		listWindowAsset4.icon_path = "ui/Icons/iconCityList";
		listWindowAsset4.set_list_component = ((Transform pTransform) => pTransform.AddComponent<CityListComponent>());
		this.add(listWindowAsset4);
		ListWindowAsset listWindowAsset5 = new ListWindowAsset();
		listWindowAsset5.id = "list_families";
		listWindowAsset5.meta_type = MetaType.Family;
		listWindowAsset5.no_items_locale = "list_empty_families";
		listWindowAsset5.art_path = "ui/illustrations/art_families";
		listWindowAsset5.icon_path = "ui/Icons/iconFamilyList";
		listWindowAsset5.set_list_component = ((Transform pTransform) => pTransform.AddComponent<FamilyListComponent>());
		this.add(listWindowAsset5);
		ListWindowAsset listWindowAsset6 = new ListWindowAsset();
		listWindowAsset6.id = "list_favorite_items";
		listWindowAsset6.meta_type = MetaType.Item;
		listWindowAsset6.no_items_locale = "list_empty_favorites_items";
		listWindowAsset6.art_path = "ui/illustrations/art_favorite_items";
		listWindowAsset6.icon_path = "ui/Icons/iconFavoriteItemsList";
		listWindowAsset6.set_list_component = ((Transform pTransform) => pTransform.AddComponent<FavoriteItemListComponent>());
		this.add(listWindowAsset6);
		ListWindowAsset listWindowAsset7 = new ListWindowAsset();
		listWindowAsset7.id = "list_favorite_units";
		listWindowAsset7.meta_type = MetaType.Unit;
		listWindowAsset7.no_items_locale = "list_empty_favorite_units";
		listWindowAsset7.art_path = "ui/illustrations/art_favorite_units";
		listWindowAsset7.icon_path = "ui/Icons/iconFavoritesList";
		listWindowAsset7.set_list_component = ((Transform pTransform) => pTransform.AddComponent<WindowFavorites>());
		this.add(listWindowAsset7);
		ListWindowAsset listWindowAsset8 = new ListWindowAsset();
		listWindowAsset8.id = "list_kingdoms";
		listWindowAsset8.meta_type = MetaType.Kingdom;
		listWindowAsset8.no_items_locale = "list_empty_kingdoms";
		listWindowAsset8.art_path = "ui/illustrations/art_kingdoms";
		listWindowAsset8.icon_path = "ui/Icons/iconKingdomList";
		listWindowAsset8.set_list_component = ((Transform pTransform) => pTransform.AddComponent<KingdomListComponent>());
		this.add(listWindowAsset8);
		ListWindowAsset listWindowAsset9 = new ListWindowAsset();
		listWindowAsset9.id = "list_languages";
		listWindowAsset9.meta_type = MetaType.Language;
		listWindowAsset9.no_items_locale = "list_empty_languages";
		listWindowAsset9.art_path = "ui/illustrations/art_languages";
		listWindowAsset9.icon_path = "ui/Icons/iconLanguageList";
		listWindowAsset9.set_list_component = ((Transform pTransform) => pTransform.AddComponent<LanguageListComponent>());
		this.add(listWindowAsset9);
		ListWindowAsset listWindowAsset10 = new ListWindowAsset();
		listWindowAsset10.id = "list_plots";
		listWindowAsset10.meta_type = MetaType.Plot;
		listWindowAsset10.no_items_locale = "list_empty_plots";
		listWindowAsset10.art_path = "ui/illustrations/art_plots";
		listWindowAsset10.icon_path = "ui/Icons/iconPlotList";
		listWindowAsset10.set_list_component = ((Transform pTransform) => pTransform.AddComponent<PlotListComponent>());
		this.add(listWindowAsset10);
		ListWindowAsset listWindowAsset11 = new ListWindowAsset();
		listWindowAsset11.id = "list_religions";
		listWindowAsset11.meta_type = MetaType.Religion;
		listWindowAsset11.no_items_locale = "list_empty_religions";
		listWindowAsset11.art_path = "ui/illustrations/art_religions";
		listWindowAsset11.icon_path = "ui/Icons/iconReligionList";
		listWindowAsset11.set_list_component = ((Transform pTransform) => pTransform.AddComponent<ReligionListComponent>());
		this.add(listWindowAsset11);
		ListWindowAsset listWindowAsset12 = new ListWindowAsset();
		listWindowAsset12.id = "list_subspecies";
		listWindowAsset12.meta_type = MetaType.Subspecies;
		listWindowAsset12.no_items_locale = "list_empty_subspecies";
		listWindowAsset12.art_path = "ui/illustrations/art_subspecies";
		listWindowAsset12.icon_path = "ui/Icons/iconSubspeciesList";
		listWindowAsset12.set_list_component = ((Transform pTransform) => pTransform.AddComponent<SubspeciesListComponent>());
		this.add(listWindowAsset12);
		ListWindowAsset listWindowAsset13 = new ListWindowAsset();
		listWindowAsset13.id = "list_wars";
		listWindowAsset13.meta_type = MetaType.War;
		listWindowAsset13.no_items_locale = "list_empty_wars";
		listWindowAsset13.no_dead_items_locale = "empty_past_wars_list";
		listWindowAsset13.art_path = "ui/illustrations/art_wars";
		listWindowAsset13.icon_path = "ui/Icons/iconWarList";
		listWindowAsset13.set_list_component = ((Transform pTransform) => pTransform.AddComponent<WarListComponent>());
		this.add(listWindowAsset13);
		ListWindowAsset listWindowAsset14 = new ListWindowAsset();
		listWindowAsset14.id = "list_armies";
		listWindowAsset14.meta_type = MetaType.Army;
		listWindowAsset14.no_items_locale = "list_empty_armies";
		listWindowAsset14.art_path = "ui/illustrations/art_armies";
		listWindowAsset14.icon_path = "ui/Icons/iconArmyList";
		listWindowAsset14.set_list_component = ((Transform pTransform) => pTransform.AddComponent<ArmyListComponent>());
		this.add(listWindowAsset14);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x000823E8 File Offset: 0x000805E8
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (ListWindowAsset tAsset in this.list)
		{
			this._dict.Add(tAsset.meta_type, tAsset);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0008244C File Offset: 0x0008064C
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (ListWindowAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x000824DC File Offset: 0x000806DC
	public ListWindowAsset getByMetaType(MetaType pType)
	{
		return this._dict[pType];
	}

	// Token: 0x0400094A RID: 2378
	private Dictionary<MetaType, ListWindowAsset> _dict = new Dictionary<MetaType, ListWindowAsset>();
}
