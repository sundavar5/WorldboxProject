using System;

// Token: 0x0200020E RID: 526
public class AllianceCustomizeWindow : GenericCustomizeWindow<Alliance, AllianceData, AllianceBanner>
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060012ED RID: 4845 RVA: 0x000D5F73 File Offset: 0x000D4173
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060012EE RID: 4846 RVA: 0x000D5F77 File Offset: 0x000D4177
	protected override Alliance meta_object
	{
		get
		{
			return SelectedMetas.selected_alliance;
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x000D5F7E File Offset: 0x000D417E
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getBackgroundSprite();
		this.image_banner_option_2.sprite = this.meta_object.getIconSprite();
	}
}
