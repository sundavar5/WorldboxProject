using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000046 RID: 70
[Serializable]
public class GodPower : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001C845 File Offset: 0x0001AA45
	[JsonIgnore]
	public bool has_sound_drawing
	{
		get
		{
			return this.sound_drawing != null;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060002E5 RID: 741 RVA: 0x0001C853 File Offset: 0x0001AA53
	[JsonIgnore]
	public bool has_sound_event
	{
		get
		{
			return this.sound_event != null;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060002E6 RID: 742 RVA: 0x0001C861 File Offset: 0x0001AA61
	[JsonIgnore]
	public OptionAsset option_asset
	{
		get
		{
			return AssetManager.options_library.get(this.toggle_name);
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0001C873 File Offset: 0x0001AA73
	public bool isSelected()
	{
		PowerButton selectedButton = PowerButtonSelector.instance.selectedButton;
		return ((selectedButton != null) ? selectedButton.godPower : null) == this;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0001C88E File Offset: 0x0001AA8E
	public bool isAvailable()
	{
		return true;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0001C894 File Offset: 0x0001AA94
	internal static void addPower(GodPower pPower, PowerButton pButton)
	{
		GodPower.god_powers_on_canvas[pPower.id] = pPower;
		if (pPower.requires_premium)
		{
			GodPower.premium_powers.Add(pPower);
			GodPower.premium_buttons.Add(pButton);
		}
		if (!pPower.requires_premium)
		{
			GodPower.powers_rank_0.Add(pButton);
			return;
		}
		switch (pPower.rank)
		{
		case PowerRank.Rank0_free:
		case PowerRank.Rank5_noAwards:
			break;
		case PowerRank.Rank1_common:
			GodPower.powers_rank_1.Add(pButton);
			return;
		case PowerRank.Rank2_normal:
			GodPower.powers_rank_2.Add(pButton);
			return;
		case PowerRank.Rank3_good:
			GodPower.powers_rank_3.Add(pButton);
			return;
		case PowerRank.Rank4_awesome:
			GodPower.powers_rank_4.Add(pButton);
			break;
		default:
			return;
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0001C939 File Offset: 0x0001AB39
	public Sprite getIconSprite()
	{
		return SpriteTextureLoader.getSprite("ui/Icons/" + this.path_icon);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001C950 File Offset: 0x0001AB50
	public static void diagnostic()
	{
		LogText.log("Ranked Powers", "Print", "");
		GodPower.printRankedPowerButtons("rank0", GodPower.powers_rank_0);
		GodPower.printRankedPowerButtons("rank1", GodPower.powers_rank_1);
		GodPower.printRankedPowerButtons("rank2", GodPower.powers_rank_2);
		GodPower.printRankedPowerButtons("rank3", GodPower.powers_rank_3);
		GodPower.printRankedPowerButtons("rank4", GodPower.powers_rank_4);
		GodPower.printRankedPowers("premium powers", GodPower.premium_powers);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001C9CC File Offset: 0x0001ABCC
	private static void printRankedPowerButtons(string pID, List<PowerButton> pList)
	{
		string tIds = "";
		foreach (PowerButton tB in pList)
		{
			tIds = tIds + tB.godPower.id + ", ";
		}
		if (tIds.Length > 2)
		{
			tIds = tIds.Substring(0, tIds.Length - 2);
		}
		LogText.log(pID, tIds, "");
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0001CA58 File Offset: 0x0001AC58
	private static void printRankedPowers(string pID, List<GodPower> pList)
	{
		string tIds = "";
		foreach (GodPower tB in pList)
		{
			tIds = tIds + tB.id + ", ";
		}
		tIds = tIds.Substring(0, tIds.Length - 2);
		LogText.log(pID, tIds, "");
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
	public string getActorAssetID()
	{
		if (this.actor_asset_id != null)
		{
			return this.actor_asset_id;
		}
		string[] array = this.actor_asset_ids;
		if (array != null && array.Length != 0)
		{
			return this.actor_asset_ids[0];
		}
		return null;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0001CB02 File Offset: 0x0001AD02
	public ActorAsset getActorAsset()
	{
		return AssetManager.actor_library.get(this.getActorAssetID());
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0001CB14 File Offset: 0x0001AD14
	public string getLocaleID()
	{
		return this.name.Underscore();
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0001CB21 File Offset: 0x0001AD21
	public string getDescriptionID()
	{
		return this.getLocaleID() + "_description";
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001CB33 File Offset: 0x0001AD33
	public string getTranslatedName()
	{
		return this.getLocaleID().Localize();
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001CB40 File Offset: 0x0001AD40
	public string getTranslatedDescription()
	{
		return this.getDescriptionID().Localize();
	}

	// Token: 0x04000268 RID: 616
	internal static List<PowerButton> powers_rank_0 = new List<PowerButton>();

	// Token: 0x04000269 RID: 617
	internal static List<PowerButton> powers_rank_1 = new List<PowerButton>();

	// Token: 0x0400026A RID: 618
	internal static List<PowerButton> powers_rank_2 = new List<PowerButton>();

	// Token: 0x0400026B RID: 619
	internal static List<PowerButton> powers_rank_3 = new List<PowerButton>();

	// Token: 0x0400026C RID: 620
	internal static List<PowerButton> powers_rank_4 = new List<PowerButton>();

	// Token: 0x0400026D RID: 621
	internal static List<PowerButton> premium_buttons = new List<PowerButton>();

	// Token: 0x0400026E RID: 622
	internal static List<GodPower> premium_powers = new List<GodPower>();

	// Token: 0x0400026F RID: 623
	internal static Dictionary<string, GodPower> god_powers_on_canvas = new Dictionary<string, GodPower>();

	// Token: 0x04000270 RID: 624
	public string name = "DEFAULT NAME";

	// Token: 0x04000271 RID: 625
	public bool requires_premium;

	// Token: 0x04000272 RID: 626
	[DefaultValue(PowerRank.Rank0_free)]
	public PowerRank rank;

	// Token: 0x04000273 RID: 627
	public string path_icon;

	// Token: 0x04000274 RID: 628
	public bool multiple_spawn_tip;

	// Token: 0x04000275 RID: 629
	public bool show_unit_stats_overview;

	// Token: 0x04000276 RID: 630
	public bool show_tool_sizes;

	// Token: 0x04000277 RID: 631
	public bool unselect_when_window;

	// Token: 0x04000278 RID: 632
	public bool make_buildings_transparent;

	// Token: 0x04000279 RID: 633
	[DefaultValue(MetaType.None)]
	public MetaType force_map_mode;

	// Token: 0x0400027A RID: 634
	public bool ignore_cursor_icon;

	// Token: 0x0400027B RID: 635
	public bool hold_action;

	// Token: 0x0400027C RID: 636
	public float click_interval;

	// Token: 0x0400027D RID: 637
	public float particle_interval;

	// Token: 0x0400027E RID: 638
	[DefaultValue(0.95f)]
	public float falling_chance = 0.95f;

	// Token: 0x0400027F RID: 639
	public string sound_drawing;

	// Token: 0x04000280 RID: 640
	public string sound_event;

	// Token: 0x04000281 RID: 641
	public string tile_type;

	// Token: 0x04000282 RID: 642
	[NonSerialized]
	internal TileType cached_tile_type_asset;

	// Token: 0x04000283 RID: 643
	public string top_tile_type;

	// Token: 0x04000284 RID: 644
	[NonSerialized]
	internal TopTileType cached_top_tile_type_asset;

	// Token: 0x04000285 RID: 645
	public string drop_id;

	// Token: 0x04000286 RID: 646
	[NonSerialized]
	internal DropAsset cached_drop_asset;

	// Token: 0x04000287 RID: 647
	public string force_brush;

	// Token: 0x04000288 RID: 648
	public bool terraform;

	// Token: 0x04000289 RID: 649
	public bool draw_lines;

	// Token: 0x0400028A RID: 650
	[DefaultValue(PowerActionType.PowerSpecial)]
	public PowerActionType type;

	// Token: 0x0400028B RID: 651
	[DefaultValue(MouseHoldAnimation.Default)]
	public MouseHoldAnimation mouse_hold_animation;

	// Token: 0x0400028C RID: 652
	public bool highlight;

	// Token: 0x0400028D RID: 653
	public PowerActionWithID click_brush_action;

	// Token: 0x0400028E RID: 654
	public PowerActionWithID click_action;

	// Token: 0x0400028F RID: 655
	public PowerActionWithID click_special_action;

	// Token: 0x04000290 RID: 656
	public PowerAction click_power_brush_action;

	// Token: 0x04000291 RID: 657
	public PowerAction click_power_action;

	// Token: 0x04000292 RID: 658
	public PowerButtonClickAction select_button_action;

	// Token: 0x04000293 RID: 659
	public bool disabled_on_mobile;

	// Token: 0x04000294 RID: 660
	public string toggle_name;

	// Token: 0x04000295 RID: 661
	public bool multi_toggle;

	// Token: 0x04000296 RID: 662
	public PowerToggleAction toggle_action;

	// Token: 0x04000297 RID: 663
	public string actor_asset_id;

	// Token: 0x04000298 RID: 664
	public string[] actor_asset_ids;

	// Token: 0x04000299 RID: 665
	[DefaultValue(6f)]
	public float actor_spawn_height = 6f;

	// Token: 0x0400029A RID: 666
	public bool show_spawn_effect;

	// Token: 0x0400029B RID: 667
	public string printers_print;

	// Token: 0x0400029C RID: 668
	public bool ignore_fast_spawn;

	// Token: 0x0400029D RID: 669
	public bool set_used_camera_drag_on_long_move;

	// Token: 0x0400029E RID: 670
	public bool can_drag_map;

	// Token: 0x0400029F RID: 671
	[DefaultValue(true)]
	public bool tester_enabled = true;

	// Token: 0x040002A0 RID: 672
	[DefaultValue(true)]
	public bool track_activity = true;

	// Token: 0x040002A1 RID: 673
	public bool map_modes_switch;

	// Token: 0x040002A2 RID: 674
	public bool allow_unit_selection;

	// Token: 0x040002A3 RID: 675
	public bool show_close_actor;

	// Token: 0x040002A4 RID: 676
	[DefaultValue(true)]
	public bool activate_on_hotkey_select = true;

	// Token: 0x040002A5 RID: 677
	[DefaultValue(true)]
	public bool surprises_units = true;

	// Token: 0x040002A6 RID: 678
	[NonSerialized]
	public Sprite sprite_icon;
}
