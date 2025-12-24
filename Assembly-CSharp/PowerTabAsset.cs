using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
[Serializable]
public class PowerTabAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x06000A50 RID: 2640 RVA: 0x00095990 File Offset: 0x00093B90
	public void tryToShowPowerTab()
	{
		PowerTabGetter powerTabGetter = this.get_power_tab;
		PowersTab tPowerTab = (powerTabGetter != null) ? powerTabGetter() : null;
		if (tPowerTab == null)
		{
			Debug.LogError("PowerTabAsset: tryToShowPowerTab: get_power_tab returned null for " + this.meta_type.ToString());
			return;
		}
		tPowerTab.tryToShowTab();
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x000959E0 File Offset: 0x00093BE0
	public string getLocaleID()
	{
		return this.locale_key;
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000959E8 File Offset: 0x00093BE8
	public string getDescriptionID()
	{
		if (this.locale_key == null)
		{
			return null;
		}
		return this.locale_key + "_info";
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00095A04 File Offset: 0x00093C04
	public Sprite getIcon()
	{
		return SpriteTextureLoader.getSprite(this.icon_path);
	}

	// Token: 0x040009F9 RID: 2553
	[NonSerialized]
	public PowersTab gameplay_tab;

	// Token: 0x040009FA RID: 2554
	public bool tab_type_selected;

	// Token: 0x040009FB RID: 2555
	public bool tab_type_main;

	// Token: 0x040009FC RID: 2556
	public PowerTabGetter get_power_tab;

	// Token: 0x040009FD RID: 2557
	public string window_id;

	// Token: 0x040009FE RID: 2558
	public MetaType meta_type;

	// Token: 0x040009FF RID: 2559
	public PowerTabAction on_main_tab_select;

	// Token: 0x04000A00 RID: 2560
	public PowerTabAction on_main_info_click;

	// Token: 0x04000A01 RID: 2561
	public PowerTabActionCheck on_update_check_active;

	// Token: 0x04000A02 RID: 2562
	public PowerTabWorldtipAction get_localized_worldtip;

	// Token: 0x04000A03 RID: 2563
	public string icon_path;

	// Token: 0x04000A04 RID: 2564
	public string locale_key;

	// Token: 0x04000A05 RID: 2565
	[NonSerialized]
	public float last_scroll_position;
}
