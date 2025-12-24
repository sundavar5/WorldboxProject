using System;

// Token: 0x02000672 RID: 1650
public class CultureCustomizeWindow : GenericCustomizeWindow<Culture, CultureData, CultureBanner>
{
	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06003542 RID: 13634 RVA: 0x0018849A File Offset: 0x0018669A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06003543 RID: 13635 RVA: 0x0018849D File Offset: 0x0018669D
	protected override Culture meta_object
	{
		get
		{
			return SelectedMetas.selected_culture;
		}
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x001884A4 File Offset: 0x001866A4
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getDecorSprite();
		this.image_banner_option_2.sprite = this.meta_object.getElementSprite();
	}
}
