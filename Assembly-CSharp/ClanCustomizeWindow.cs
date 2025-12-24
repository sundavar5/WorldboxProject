using System;

// Token: 0x02000664 RID: 1636
public class ClanCustomizeWindow : GenericCustomizeWindow<Clan, ClanData, ClanBanner>
{
	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06003504 RID: 13572 RVA: 0x00187B8A File Offset: 0x00185D8A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06003505 RID: 13573 RVA: 0x00187B8D File Offset: 0x00185D8D
	protected override Clan meta_object
	{
		get
		{
			return SelectedMetas.selected_clan;
		}
	}

	// Token: 0x06003506 RID: 13574 RVA: 0x00187B94 File Offset: 0x00185D94
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getBackgroundSprite();
		this.image_banner_option_2.sprite = this.meta_object.getIconSprite();
	}
}
