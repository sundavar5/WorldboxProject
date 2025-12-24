using System;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class SelectedMeta<TMeta, TMetaData> : SelectedNano<TMeta> where TMeta : MetaObject<TMetaData>, IFavoriteable where TMetaData : MetaObjectData
{
	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06003B7D RID: 15229 RVA: 0x001A0D56 File Offset: 0x0019EF56
	protected virtual MetaType meta_type { get; }

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06003B7E RID: 15230 RVA: 0x001A0D5E File Offset: 0x0019EF5E
	protected string window_id
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type).window_name;
		}
	}

	// Token: 0x06003B7F RID: 15231 RVA: 0x001A0D75 File Offset: 0x0019EF75
	protected override void updateElements(TMeta pNano)
	{
		if (pNano.isRekt())
		{
			return;
		}
		base.updateElements(pNano);
		this.checkShowBanner();
	}

	// Token: 0x06003B80 RID: 15232 RVA: 0x001A0D92 File Offset: 0x0019EF92
	protected override void showStatsGeneral(TMeta pMeta)
	{
		this.setName(pMeta);
		this.setTitleIcons(pMeta);
		this.showGeneralIcons(pMeta);
	}

	// Token: 0x06003B81 RID: 15233 RVA: 0x001A0DA9 File Offset: 0x0019EFA9
	protected virtual void setName(TMeta pMeta)
	{
		this.name_field.text = pMeta.name;
		this.name_field.color = pMeta.getColor().getColorText();
	}

	// Token: 0x06003B82 RID: 15234 RVA: 0x001A0DDC File Offset: 0x0019EFDC
	protected virtual void setTitleIcons(TMeta pMeta)
	{
		Sprite tSprite = pMeta.getSpriteIcon();
		this.icon_right.sprite = tSprite;
	}

	// Token: 0x06003B83 RID: 15235 RVA: 0x001A0E01 File Offset: 0x0019F001
	protected virtual void checkShowBanner()
	{
		this.banner.load(this.nano_object);
	}

	// Token: 0x06003B84 RID: 15236 RVA: 0x001A0E1C File Offset: 0x0019F01C
	protected void showGeneralIcons(TMeta pMeta)
	{
		StatsIconContainer[] stats_icons = this.stats_icons;
		for (int i = 0; i < stats_icons.Length; i++)
		{
			stats_icons[i].showGeneralIcons<TMeta, TMetaData>(pMeta);
		}
	}

	// Token: 0x06003B85 RID: 15237 RVA: 0x001A0E47 File Offset: 0x0019F047
	public void openInfoTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Info");
	}

	// Token: 0x06003B86 RID: 15238 RVA: 0x001A0E68 File Offset: 0x0019F068
	public void openTraitsEditorTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Traits");
	}

	// Token: 0x06003B87 RID: 15239 RVA: 0x001A0E89 File Offset: 0x0019F089
	public void openFamiliesTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Families");
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x001A0EAA File Offset: 0x0019F0AA
	public void openInterestingPeopleTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Interesting People");
	}

	// Token: 0x06003B89 RID: 15241 RVA: 0x001A0ECB File Offset: 0x0019F0CB
	public void openPyramidTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Pyramid");
	}

	// Token: 0x06003B8A RID: 15242 RVA: 0x001A0EEC File Offset: 0x0019F0EC
	public void openStatisticsTab()
	{
		ScrollWindow.showWindow(this.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Statistics");
	}

	// Token: 0x04002BAA RID: 11178
	[SerializeField]
	protected BannerGeneric<TMeta, TMetaData> banner;
}
