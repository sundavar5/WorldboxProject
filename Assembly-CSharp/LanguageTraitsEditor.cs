using System;
using System.Collections.Generic;

// Token: 0x02000715 RID: 1813
public class LanguageTraitsEditor : TraitsEditor<LanguageTrait, LanguageTraitButton, LanguageTraitEditorButton, LanguageTraitGroupAsset, LanguageTraitGroupElement>
{
	// Token: 0x1700033B RID: 827
	// (get) Token: 0x060039D5 RID: 14805 RVA: 0x0019B187 File Offset: 0x00199387
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x060039D6 RID: 14806 RVA: 0x0019B18A File Offset: 0x0019938A
	protected override List<LanguageTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.language_trait_groups.list;
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x060039D7 RID: 14807 RVA: 0x0019B196 File Offset: 0x00199396
	protected override List<LanguageTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.language_traits.list;
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x060039D8 RID: 14808 RVA: 0x0019B1A2 File Offset: 0x001993A2
	protected override LanguageTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.language_traits.get("divine_encryption");
		}
	}

	// Token: 0x060039D9 RID: 14809 RVA: 0x0019B1B3 File Offset: 0x001993B3
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_language.checkBySignal(null);
	}
}
