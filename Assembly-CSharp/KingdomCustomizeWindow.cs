using System;

// Token: 0x020006EE RID: 1774
public class KingdomCustomizeWindow : GenericCustomizeWindow<Kingdom, KingdomData, KingdomBanner>
{
	// Token: 0x17000327 RID: 807
	// (get) Token: 0x060038FE RID: 14590 RVA: 0x00197866 File Offset: 0x00195A66
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x060038FF RID: 14591 RVA: 0x00197869 File Offset: 0x00195A69
	protected override Kingdom meta_object
	{
		get
		{
			return SelectedMetas.selected_kingdom;
		}
	}

	// Token: 0x06003900 RID: 14592 RVA: 0x00197870 File Offset: 0x00195A70
	protected override void onBannerChange()
	{
		this.meta_object.getActorAsset();
		this.image_banner_option_1.sprite = this.meta_object.getElementBackground();
		this.image_banner_option_2.sprite = this.meta_object.getElementIcon();
	}
}
