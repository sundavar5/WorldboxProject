using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
[Serializable]
public class MetaTypeAsset : Asset
{
	// Token: 0x060019F7 RID: 6647 RVA: 0x000F1EDC File Offset: 0x000F00DC
	public bool hasDecisions()
	{
		string[] array = this.decision_ids;
		return array != null && array.Length != 0;
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000F1EEE File Offset: 0x000F00EE
	public int getZoneOptionState()
	{
		if (!Zones.getForcedMapMode().isNone() && Zones.isPowerForcedMapModeEnabled())
		{
			return 0;
		}
		return this.option_asset.current_int_value;
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000F1F10 File Offset: 0x000F0110
	internal void toggleOptionZone(GodPower pPower, int pDirection = 1, bool pDisable = true)
	{
		PlayerOptionData tData = this.option_asset.data;
		if (tData.boolVal)
		{
			tData.intVal += pDirection;
			if (tData.intVal > this.option_asset.max_value)
			{
				tData.intVal = 0;
				if (pDisable)
				{
					tData.boolVal = false;
				}
			}
			if (tData.intVal < 0)
			{
				tData.intVal = this.option_asset.max_value;
			}
		}
		else
		{
			tData.boolVal = true;
		}
		if (pPower.map_modes_switch)
		{
			if (tData.boolVal)
			{
				PowerLibrary.disableAllOtherMapModes(pPower.id);
			}
			else
			{
				WorldTip.instance.startHide();
			}
		}
		PlayerConfig.saveData();
		string tLocalizedName = pPower.getTranslatedName();
		string tLocalizedDescription = pPower.getTranslatedDescription();
		string tZoneMode = this.option_asset.getTranslatedOption();
		if (tData.boolVal)
		{
			WorldTip.instance.showToolbarText(tLocalizedName + " - " + tZoneMode, tLocalizedDescription, true);
		}
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000F1FEC File Offset: 0x000F01EC
	public bool isMetaZoneOptionSelectedFluid()
	{
		return this.getZoneOptionState() == this.dynamic_zone_option;
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x000F1FFC File Offset: 0x000F01FC
	public bool isActive(bool pOnlyOption = false)
	{
		if (pOnlyOption)
		{
			return this.isOptionActive();
		}
		return this.isOptionActive() || Zones.isPowerForceMapMode(this.map_mode);
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000F201D File Offset: 0x000F021D
	public bool isOptionActive()
	{
		return PlayerConfig.optionBoolEnabled(this.option_id);
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x000F202A File Offset: 0x000F022A
	public ListPool<NanoObject> getSortedList()
	{
		if (this.get_sorted_list != null)
		{
			return this.get_sorted_list();
		}
		if (this.custom_sorted_list != null)
		{
			return this.custom_sorted_list();
		}
		return new ListPool<NanoObject>(this.get_list());
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x000F2064 File Offset: 0x000F0264
	public bool hasRanks()
	{
		return this.ranks != null;
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000F206F File Offset: 0x000F026F
	public void setListGetter(MetaTypeListPoolAction pListAction)
	{
		this.get_sorted_list = pListAction;
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x000F2078 File Offset: 0x000F0278
	public void selectAndInspect(NanoObject pNewNanoObject, bool pFromNameplate = false, bool pCheckNameplate = true, bool pClearAction = false)
	{
		int tCurrentFrame = Time.frameCount;
		if (MetaTypeAsset._last_call_frame == tCurrentFrame)
		{
			return;
		}
		MetaTypeAsset._last_call_frame = tCurrentFrame;
		if (pCheckNameplate && World.world.nameplate_manager.isOverNameplate() && !pFromNameplate)
		{
			return;
		}
		NanoObject nanoObject = this.get_selected();
		string tPrevSelectedMetaType = MetaTypeAsset.last_meta_type;
		this.set_selected(pNewNanoObject);
		SelectedObjects.setNanoObject(pNewNanoObject);
		bool tMetaIsRekt = nanoObject.isRekt();
		bool tNanoTheSame = nanoObject == pNewNanoObject;
		bool tMetaTypeTheSame = tPrevSelectedMetaType == MetaTypeAsset.last_meta_type;
		bool tTabTheSame = this.power_tab_id == PowersTab.getActiveTab().getAsset().id;
		if (HotkeyLibrary.isHoldingAnyMod())
		{
			ScrollWindow.showWindow(this.window_name);
			return;
		}
		if (!tMetaIsRekt && tNanoTheSame && tMetaTypeTheSame && tTabTheSame && !pClearAction)
		{
			ScrollWindow.showWindow(this.window_name);
			return;
		}
		SelectedTabsHistory.addToHistory(pNewNanoObject);
		if (this.selected_tab_action != null)
		{
			this.selected_tab_action();
			return;
		}
		if (this.selected_tab_action_meta != null)
		{
			SelectedUnit.clear();
			this.selected_tab_action_meta(this);
			return;
		}
		if (!pClearAction)
		{
			ScrollWindow.showWindow(this.window_name);
			return;
		}
		SelectedTabsHistory.showPreviousTab();
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x000F2184 File Offset: 0x000F0384
	public Sprite getIconSprite()
	{
		return SpriteTextureLoader.getSprite(this.icon_single_path);
	}

	// Token: 0x0400140A RID: 5130
	public MetaType map_mode;

	// Token: 0x0400140B RID: 5131
	public string option_id = string.Empty;

	// Token: 0x0400140C RID: 5132
	public string power_option_zone_id = string.Empty;

	// Token: 0x0400140D RID: 5133
	public string power_tab_id = string.Empty;

	// Token: 0x0400140E RID: 5134
	public string window_name;

	// Token: 0x0400140F RID: 5135
	public bool force_zone_when_selected;

	// Token: 0x04001410 RID: 5136
	public int[] ranks;

	// Token: 0x04001411 RID: 5137
	public string[] reports;

	// Token: 0x04001412 RID: 5138
	public MetaCheckUnitWindowAction check_unit_has_meta;

	// Token: 0x04001413 RID: 5139
	public MetaUnitSetMetaForWindow set_unit_set_meta_for_meta_for_window;

	// Token: 0x04001414 RID: 5140
	public MetaZoneDrawAction draw_zones;

	// Token: 0x04001415 RID: 5141
	public MetaZoneClickAction click_action_zone;

	// Token: 0x04001416 RID: 5142
	public MetaZoneHighlightAction check_cursor_highlight;

	// Token: 0x04001417 RID: 5143
	public MetaZoneGetMeta tile_get_metaobject;

	// Token: 0x04001418 RID: 5144
	public MetaZoneGetMetaSimple tile_get_metaobject_0;

	// Token: 0x04001419 RID: 5145
	public MetaZoneGetMetaSimple tile_get_metaobject_1;

	// Token: 0x0400141A RID: 5146
	public MetaZoneGetMetaSimple tile_get_metaobject_2;

	// Token: 0x0400141B RID: 5147
	public MetaZoneTooltipAction check_tile_has_meta;

	// Token: 0x0400141C RID: 5148
	public MetaZoneTooltipAction check_cursor_tooltip;

	// Token: 0x0400141D RID: 5149
	public MetaTooltipShowAction cursor_tooltip_action;

	// Token: 0x0400141E RID: 5150
	public MetaZoneDynamicAction dynamic_zones;

	// Token: 0x0400141F RID: 5151
	public MetaTypeAction window_action_clear;

	// Token: 0x04001420 RID: 5152
	public MetaTypeAction selected_tab_action;

	// Token: 0x04001421 RID: 5153
	public MetaTypeActionAsset selected_tab_action_meta;

	// Token: 0x04001422 RID: 5154
	public MetaTypeHistoryAction window_history_action_update;

	// Token: 0x04001423 RID: 5155
	public MetaTypeHistoryAction window_history_action_restore;

	// Token: 0x04001424 RID: 5156
	public int dynamic_zone_option = 2;

	// Token: 0x04001425 RID: 5157
	public bool has_dynamic_zones;

	// Token: 0x04001426 RID: 5158
	public string icon_list;

	// Token: 0x04001427 RID: 5159
	public string icon_single_path;

	// Token: 0x04001428 RID: 5160
	public MetaTypeListAction get_list;

	// Token: 0x04001429 RID: 5161
	public MetaTypeListPoolAction get_sorted_list;

	// Token: 0x0400142A RID: 5162
	public MetaTypeListHasAction has_any;

	// Token: 0x0400142B RID: 5163
	public MetaSelectedGetter get_selected;

	// Token: 0x0400142C RID: 5164
	public MetaSelectedSetter set_selected;

	// Token: 0x0400142D RID: 5165
	public MetaGetter get;

	// Token: 0x0400142E RID: 5166
	public MetaStatAction stat_hover;

	// Token: 0x0400142F RID: 5167
	public MetaStatAction stat_click;

	// Token: 0x04001430 RID: 5168
	public MetaTypeListPoolAction custom_sorted_list;

	// Token: 0x04001431 RID: 5169
	public string[] decision_ids;

	// Token: 0x04001432 RID: 5170
	[NonSerialized]
	public DecisionAsset[] decisions_assets;

	// Token: 0x04001433 RID: 5171
	[NonSerialized]
	public OptionAsset option_asset;

	// Token: 0x04001434 RID: 5172
	public bool unit_amount_alpha;

	// Token: 0x04001435 RID: 5173
	public bool set_icon_for_cancel_button;

	// Token: 0x04001436 RID: 5174
	private static int _last_call_frame = -1;

	// Token: 0x04001437 RID: 5175
	public static string last_meta_type;
}
