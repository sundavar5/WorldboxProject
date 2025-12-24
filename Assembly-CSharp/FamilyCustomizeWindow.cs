using System;

// Token: 0x0200068F RID: 1679
public class FamilyCustomizeWindow : GenericCustomizeWindow<Family, FamilyData, FamilyBanner>
{
	// Token: 0x170002FF RID: 767
	// (get) Token: 0x060035C5 RID: 13765 RVA: 0x00189982 File Offset: 0x00187B82
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x060035C6 RID: 13766 RVA: 0x00189985 File Offset: 0x00187B85
	protected override Family meta_object
	{
		get
		{
			return SelectedMetas.selected_family;
		}
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x0018998C File Offset: 0x00187B8C
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getSpriteBackground();
		this.image_banner_option_2.sprite = this.meta_object.getSpriteFrame();
	}
}
