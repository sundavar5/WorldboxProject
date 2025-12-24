using System;

// Token: 0x02000767 RID: 1895
public class SubspeciesCustomizeWindow : GenericCustomizeWindow<Subspecies, SubspeciesData, SubspeciesBanner>
{
	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06003C2C RID: 15404 RVA: 0x001A301A File Offset: 0x001A121A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06003C2D RID: 15405 RVA: 0x001A301D File Offset: 0x001A121D
	protected override Subspecies meta_object
	{
		get
		{
			return SelectedMetas.selected_subspecies;
		}
	}

	// Token: 0x06003C2E RID: 15406 RVA: 0x001A3024 File Offset: 0x001A1224
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getSpriteBackground();
		this.image_banner_option_2.sprite = this.meta_object.getSpriteIcon();
	}

	// Token: 0x06003C2F RID: 15407 RVA: 0x001A3052 File Offset: 0x001A1252
	protected override void updateColorsBanner()
	{
	}
}
