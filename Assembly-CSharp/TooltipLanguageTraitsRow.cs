using System;
using System.Collections.Generic;

// Token: 0x0200078B RID: 1931
public class TooltipLanguageTraitsRow : TooltipTraitsRow<LanguageTrait>
{
	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06003D67 RID: 15719 RVA: 0x001AE22C File Offset: 0x001AC42C
	protected override IReadOnlyCollection<LanguageTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.language.getTraits();
		}
	}
}
