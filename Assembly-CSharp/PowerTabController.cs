using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F3 RID: 1523
public class PowerTabController : MonoBehaviour
{
	// Token: 0x060031BF RID: 12735 RVA: 0x0017BCF8 File Offset: 0x00179EF8
	private void Awake()
	{
		PowerTabController.instance = this;
		this._buttons = new List<Button>
		{
			this.t_drawing,
			this.t_kingdoms,
			this.t_creatures,
			this.t_nature,
			this.t_bombs,
			this.t_other
		};
	}

	// Token: 0x060031C0 RID: 12736 RVA: 0x0017BD60 File Offset: 0x00179F60
	private void Update()
	{
		PowersTab tActiveTab = PowersTab.getActiveTab();
		PowerTabAsset tAsset = tActiveTab.getAsset();
		if (tAsset.on_update_check_active != null && !tAsset.on_update_check_active(tAsset))
		{
			tActiveTab.hideTab();
			PowerTabController.showMainTab();
			World.world.selected_buttons.unselectTabs();
			return;
		}
		tActiveTab.update();
	}

	// Token: 0x060031C1 RID: 12737 RVA: 0x0017BDB1 File Offset: 0x00179FB1
	internal static float currentScrollPosition()
	{
		return PowerTabController.instance.scrollRect.horizontalNormalizedPosition;
	}

	// Token: 0x060031C2 RID: 12738 RVA: 0x0017BDC2 File Offset: 0x00179FC2
	public static void loadScrollPosition(float pPosition)
	{
	}

	// Token: 0x060031C3 RID: 12739 RVA: 0x0017BDC4 File Offset: 0x00179FC4
	internal void resetToStartScrollPosition()
	{
		this.scrollRect.horizontalNormalizedPosition = 0f;
	}

	// Token: 0x060031C4 RID: 12740 RVA: 0x0017BDD8 File Offset: 0x00179FD8
	public Button getTabForTabGroup(string pGroupName)
	{
		foreach (Button tButton in this._buttons)
		{
			if (tButton.onClick.GetPersistentTarget(0).name == pGroupName)
			{
				return tButton;
			}
		}
		return null;
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x0017BE44 File Offset: 0x0017A044
	public Button getNext(string pActiveTab)
	{
		this._buttons.Sort((Button a, Button b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
		for (int i = 0; i < this._buttons.Count; i++)
		{
			if (this._buttons[i].onClick.GetPersistentTarget(0).name == pActiveTab && i < this._buttons.Count - 1)
			{
				return this._buttons[i + 1];
			}
		}
		return this._buttons.First<Button>();
	}

	// Token: 0x060031C6 RID: 12742 RVA: 0x0017BEE0 File Offset: 0x0017A0E0
	public Button getPrev(string pActiveTab)
	{
		this._buttons.Sort((Button a, Button b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
		for (int i = 0; i < this._buttons.Count; i++)
		{
			if (this._buttons[i].onClick.GetPersistentTarget(0).name == pActiveTab && i > 0)
			{
				return this._buttons[i - 1];
			}
		}
		return this._buttons.Last<Button>();
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x0017BF6F File Offset: 0x0017A16F
	public static void showMainTab()
	{
		PowerTabController.instance.tab_main.showTab(null);
	}

	// Token: 0x060031C8 RID: 12744 RVA: 0x0017BF81 File Offset: 0x0017A181
	public static void showTabSelectedUnit()
	{
		SelectedTabsHistory.addToHistory(SelectedUnit.unit);
		PowerTabAsset asset = PowerTabController.getAsset("selected_unit");
		asset.tryToShowPowerTab();
		PowerTabController.showWorldTipSelected(asset.id, true);
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x0017BFA8 File Offset: 0x0017A1A8
	public static void showTabMultipleUnits()
	{
		PowerTabAsset asset = PowerTabController.getAsset("multiple_units");
		asset.tryToShowPowerTab();
		PowerTabController.showWorldTipSelected(asset.id, true);
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x0017BFC5 File Offset: 0x0017A1C5
	public static void showTabSelectedMeta(MetaTypeAsset pMetaTypeAsset)
	{
		PowerTabAsset asset = PowerTabController.getAsset(pMetaTypeAsset.power_tab_id);
		asset.tryToShowPowerTab();
		MetaTypeAsset.last_meta_type = pMetaTypeAsset.id;
		PowerTabController.showWorldTipSelected(asset.id, true);
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x0017BFF0 File Offset: 0x0017A1F0
	public static void showWorldTipSelected(string pPowerTabId, bool pBar = true)
	{
		string tCurrentMeta = SelectedObjects.getSelectedNanoObject().getTypeID();
		if (PowerTabController.prev_selected_meta_id == tCurrentMeta)
		{
			return;
		}
		PowerTabController.prev_selected_meta_id = tCurrentMeta;
		PowerTabAsset tAsset = AssetManager.power_tab_library.get(pPowerTabId);
		string tRow = tAsset.get_localized_worldtip(tAsset);
		if (pBar)
		{
			WorldTip.instance.showToolbarText(tRow);
			return;
		}
		WorldTip.showNowTop(tRow, false);
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x0017C04B File Offset: 0x0017A24B
	public static PowerTabAsset getAsset(string pID)
	{
		return AssetManager.power_tab_library.get(pID);
	}

	// Token: 0x0400259E RID: 9630
	public PowersTab tab_main;

	// Token: 0x0400259F RID: 9631
	public PowersTab tab_selected_unit;

	// Token: 0x040025A0 RID: 9632
	public PowersTab tab_multiple_units;

	// Token: 0x040025A1 RID: 9633
	public PowersTab tab_selected_kingdom;

	// Token: 0x040025A2 RID: 9634
	public PowersTab tab_selected_subspecies;

	// Token: 0x040025A3 RID: 9635
	public PowersTab tab_selected_alliance;

	// Token: 0x040025A4 RID: 9636
	public PowersTab tab_selected_army;

	// Token: 0x040025A5 RID: 9637
	public PowersTab tab_selected_family;

	// Token: 0x040025A6 RID: 9638
	public PowersTab tab_selected_language;

	// Token: 0x040025A7 RID: 9639
	public PowersTab tab_selected_culture;

	// Token: 0x040025A8 RID: 9640
	public PowersTab tab_selected_religion;

	// Token: 0x040025A9 RID: 9641
	public PowersTab tab_selected_clan;

	// Token: 0x040025AA RID: 9642
	public PowersTab tab_selected_city;

	// Token: 0x040025AB RID: 9643
	[Space]
	public Button t_main;

	// Token: 0x040025AC RID: 9644
	public Button t_drawing;

	// Token: 0x040025AD RID: 9645
	public Button t_kingdoms;

	// Token: 0x040025AE RID: 9646
	public Button t_creatures;

	// Token: 0x040025AF RID: 9647
	public Button t_bombs;

	// Token: 0x040025B0 RID: 9648
	public Button t_nature;

	// Token: 0x040025B1 RID: 9649
	public Button t_other;

	// Token: 0x040025B2 RID: 9650
	public Transform copyTarget;

	// Token: 0x040025B3 RID: 9651
	[Space]
	internal static PowerTabController instance;

	// Token: 0x040025B4 RID: 9652
	public ToolbarArrow arrowLeft;

	// Token: 0x040025B5 RID: 9653
	public ToolbarArrow arrowRight;

	// Token: 0x040025B6 RID: 9654
	public RectTransform rect;

	// Token: 0x040025B7 RID: 9655
	public ScrollRectExtended scrollRect;

	// Token: 0x040025B8 RID: 9656
	private List<Button> _buttons;

	// Token: 0x040025B9 RID: 9657
	public static string prev_selected_meta_id;
}
