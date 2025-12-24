using System;

// Token: 0x0200073B RID: 1851
public class ReligionCustomizeWindow : GenericCustomizeWindow<Religion, ReligionData, ReligionBanner>
{
	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x0019F6ED File Offset: 0x0019D8ED
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06003ADA RID: 15066 RVA: 0x0019F6F0 File Offset: 0x0019D8F0
	protected override Religion meta_object
	{
		get
		{
			return SelectedMetas.selected_religion;
		}
	}

	// Token: 0x06003ADB RID: 15067 RVA: 0x0019F6F7 File Offset: 0x0019D8F7
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getBackgroundSprite();
		this.image_banner_option_2.sprite = this.meta_object.getIconSprite();
	}
}
