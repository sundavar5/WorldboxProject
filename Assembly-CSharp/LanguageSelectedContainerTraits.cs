using System;
using System.Collections.Generic;

// Token: 0x0200070F RID: 1807
public class LanguageSelectedContainerTraits : SelectedContainerTraits<LanguageTrait, LanguageTraitButton, LanguageTraitsContainer, LanguageTraitsEditor>
{
	// Token: 0x17000339 RID: 825
	// (get) Token: 0x060039C4 RID: 14788 RVA: 0x0019B0B5 File Offset: 0x001992B5
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x060039C5 RID: 14789 RVA: 0x0019B0B8 File Offset: 0x001992B8
	protected override IReadOnlyCollection<LanguageTrait> getTraits()
	{
		return SelectedMetas.selected_language.getTraits();
	}

	// Token: 0x060039C6 RID: 14790 RVA: 0x0019B0C4 File Offset: 0x001992C4
	protected override bool canEditTraits()
	{
		return true;
	}
}
