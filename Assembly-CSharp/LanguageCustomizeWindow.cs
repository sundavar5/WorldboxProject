using System;

// Token: 0x0200070B RID: 1803
public class LanguageCustomizeWindow : GenericCustomizeWindow<Language, LanguageData, LanguageBanner>
{
	// Token: 0x17000335 RID: 821
	// (get) Token: 0x060039B5 RID: 14773 RVA: 0x0019AEA1 File Offset: 0x001990A1
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x060039B6 RID: 14774 RVA: 0x0019AEA4 File Offset: 0x001990A4
	protected override Language meta_object
	{
		get
		{
			return SelectedMetas.selected_language;
		}
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x0019AEAB File Offset: 0x001990AB
	protected override void onBannerChange()
	{
		this.image_banner_option_1.sprite = this.meta_object.getBackgroundSprite();
		this.image_banner_option_2.sprite = this.meta_object.getIconSprite();
	}
}
