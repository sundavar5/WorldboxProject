using System;

// Token: 0x0200015B RID: 347
public class PowerTabLibrary : AssetLibrary<PowerTabAsset>
{
	// Token: 0x06000A55 RID: 2645 RVA: 0x00095A1C File Offset: 0x00093C1C
	public override void init()
	{
		this.addMainTabs();
		this.addSelectionTabs();
		PowerTabAsset powerTabAsset = new PowerTabAsset();
		powerTabAsset.id = "selected_unit";
		powerTabAsset.meta_type = MetaType.Unit;
		powerTabAsset.window_id = "unit";
		powerTabAsset.on_main_tab_select = delegate(PowerTabAsset _)
		{
			if (SelectedUnit.isSet())
			{
				SelectedUnit.clear();
			}
		};
		powerTabAsset.on_main_info_click = delegate(PowerTabAsset _)
		{
			ActionLibrary.openUnitWindow(SelectedUnit.unit);
			ScrollWindow.getCurrentWindow().tabs.showTab("MainTab");
		};
		powerTabAsset.on_update_check_active = ((PowerTabAsset _) => SelectedUnit.isSet());
		powerTabAsset.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		powerTabAsset.get_power_tab = (() => PowerTabController.instance.tab_selected_unit);
		this.add(powerTabAsset);
		PowerTabAsset powerTabAsset2 = new PowerTabAsset();
		powerTabAsset2.id = "multiple_units";
		powerTabAsset2.on_update_check_active = ((PowerTabAsset _) => SelectedUnit.isSet());
		powerTabAsset2.on_main_tab_select = delegate(PowerTabAsset _)
		{
			if (SelectedUnit.isSet())
			{
				SelectedUnit.clear();
			}
		};
		powerTabAsset2.on_main_info_click = delegate(PowerTabAsset _)
		{
			ActionLibrary.openUnitWindow(SelectedUnit.unit);
			ScrollWindow.getCurrentWindow().tabs.showTab("MainTab");
		};
		powerTabAsset2.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextAmount);
		powerTabAsset2.get_power_tab = (() => PowerTabController.instance.tab_multiple_units);
		this.add(powerTabAsset2);
		this.add(new PowerTabAsset
		{
			id = "selected_building"
		});
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00095BDC File Offset: 0x00093DDC
	private void addMainTabs()
	{
		this.add(new PowerTabAsset
		{
			id = "main",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "creation",
			locale_key = "tab_world_creation",
			icon_path = "ui/Icons/power_tabs/icon_tab_drawings",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "noosphere",
			locale_key = "tab_noosphere",
			icon_path = "ui/Icons/power_tabs/icon_tab_noosphere",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "units",
			locale_key = "tab_world_creatures",
			icon_path = "ui/Icons/power_tabs/icon_tab_creatures",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "nature",
			locale_key = "tab_nature",
			icon_path = "ui/Icons/power_tabs/icon_tab_nature",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "destruction",
			locale_key = "tab_explosions",
			icon_path = "ui/Icons/power_tabs/icon_tab_bombs",
			tab_type_main = true
		});
		this.add(new PowerTabAsset
		{
			id = "other",
			locale_key = "tab_other",
			icon_path = "ui/Icons/power_tabs/icon_tab_other",
			tab_type_main = true
		});
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00095D40 File Offset: 0x00093F40
	private void addSelectionTabs()
	{
		PowerTabAsset powerTabAsset = new PowerTabAsset();
		powerTabAsset.id = "selected_army";
		powerTabAsset.meta_type = MetaType.Army;
		powerTabAsset.window_id = "army";
		powerTabAsset.get_power_tab = (() => PowerTabController.instance.tab_selected_army);
		powerTabAsset.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset);
		PowerTabAsset powerTabAsset2 = new PowerTabAsset();
		powerTabAsset2.id = "selected_family";
		powerTabAsset2.meta_type = MetaType.Family;
		powerTabAsset2.window_id = "family";
		powerTabAsset2.get_power_tab = (() => PowerTabController.instance.tab_selected_family);
		powerTabAsset2.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset2.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset2.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset2.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset2);
		PowerTabAsset powerTabAsset3 = new PowerTabAsset();
		powerTabAsset3.id = "selected_subspecies";
		powerTabAsset3.meta_type = MetaType.Subspecies;
		powerTabAsset3.window_id = "subspecies";
		powerTabAsset3.get_power_tab = (() => PowerTabController.instance.tab_selected_subspecies);
		powerTabAsset3.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset3.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset3.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset3.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset3);
		PowerTabAsset powerTabAsset4 = new PowerTabAsset();
		powerTabAsset4.id = "selected_language";
		powerTabAsset4.meta_type = MetaType.Language;
		powerTabAsset4.window_id = "language";
		powerTabAsset4.get_power_tab = (() => PowerTabController.instance.tab_selected_language);
		powerTabAsset4.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset4.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset4.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset4.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset4);
		PowerTabAsset powerTabAsset5 = new PowerTabAsset();
		powerTabAsset5.id = "selected_culture";
		powerTabAsset5.meta_type = MetaType.Culture;
		powerTabAsset5.window_id = "culture";
		powerTabAsset5.get_power_tab = (() => PowerTabController.instance.tab_selected_culture);
		powerTabAsset5.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset5.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset5.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset5.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset5);
		PowerTabAsset powerTabAsset6 = new PowerTabAsset();
		powerTabAsset6.id = "selected_religion";
		powerTabAsset6.meta_type = MetaType.Religion;
		powerTabAsset6.window_id = "religion";
		powerTabAsset6.get_power_tab = (() => PowerTabController.instance.tab_selected_religion);
		powerTabAsset6.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset6.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset6.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset6.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset6);
		PowerTabAsset powerTabAsset7 = new PowerTabAsset();
		powerTabAsset7.id = "selected_clan";
		powerTabAsset7.meta_type = MetaType.Clan;
		powerTabAsset7.window_id = "clan";
		powerTabAsset7.get_power_tab = (() => PowerTabController.instance.tab_selected_clan);
		powerTabAsset7.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset7.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset7.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset7.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset7);
		PowerTabAsset powerTabAsset8 = new PowerTabAsset();
		powerTabAsset8.id = "selected_city";
		powerTabAsset8.meta_type = MetaType.City;
		powerTabAsset8.window_id = "city";
		powerTabAsset8.get_power_tab = (() => PowerTabController.instance.tab_selected_city);
		powerTabAsset8.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset8.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset8.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset8.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset8);
		PowerTabAsset powerTabAsset9 = new PowerTabAsset();
		powerTabAsset9.id = "selected_kingdom";
		powerTabAsset9.meta_type = MetaType.Kingdom;
		powerTabAsset9.window_id = "kingdom";
		powerTabAsset9.get_power_tab = (() => PowerTabController.instance.tab_selected_kingdom);
		powerTabAsset9.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset9.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset9.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset9.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset9);
		PowerTabAsset powerTabAsset10 = new PowerTabAsset();
		powerTabAsset10.id = "selected_alliance";
		powerTabAsset10.meta_type = MetaType.Alliance;
		powerTabAsset10.window_id = "alliance";
		powerTabAsset10.get_power_tab = (() => PowerTabController.instance.tab_selected_alliance);
		powerTabAsset10.on_update_check_active = new PowerTabActionCheck(this.defaultOnUpdateCheckActive);
		powerTabAsset10.on_main_tab_select = new PowerTabAction(this.defaultMainTabSelect);
		powerTabAsset10.on_main_info_click = new PowerTabAction(this.defaultMainInfoClick);
		powerTabAsset10.get_localized_worldtip = new PowerTabWorldtipAction(this.getWorldTipTextMetaName);
		this.add(powerTabAsset10);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0009632B File Offset: 0x0009452B
	private void defaultMainTabSelect(PowerTabAsset pAsset)
	{
		SelectedObjects.unselectNanoObject();
		pAsset.meta_type.getAsset().window_action_clear();
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00096347 File Offset: 0x00094547
	private bool defaultOnUpdateCheckActive(PowerTabAsset pAsset)
	{
		return SelectedObjects.isNanoObjectSet();
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0009634E File Offset: 0x0009454E
	private void defaultMainInfoClick(PowerTabAsset pAsset)
	{
		ScrollWindow.showWindow(pAsset.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("MainTab");
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00096370 File Offset: 0x00094570
	private string getWorldTipTextMetaName(PowerTabAsset pAsset)
	{
		NanoObject tNano = SelectedObjects.getSelectedNanoObject();
		string text = LocalizedTextManager.getText("show_tab_" + pAsset.id, null, false);
		string tColor = tNano.getColor().color_text;
		string tName = tNano.name.ColorHex(tColor, false);
		return text.Replace("$name$", tName);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x000963C0 File Offset: 0x000945C0
	private string getWorldTipTextAmount(PowerTabAsset pAsset)
	{
		int tAmount = SelectedUnit.countSelected();
		return LocalizedTextManager.getText("show_tab_" + pAsset.id, null, false).Replace("$amount$", tAmount.ToString());
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x000963FC File Offset: 0x000945FC
	public override void editorDiagnosticLocales()
	{
		foreach (PowerTabAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
		base.editorDiagnosticLocales();
	}
}
