using System;
using System.Collections.Generic;

// Token: 0x02000787 RID: 1927
public class TooltipCultureTraitsRow : TooltipTraitsRow<CultureTrait>
{
	// Token: 0x17000397 RID: 919
	// (get) Token: 0x06003D5C RID: 15708 RVA: 0x001AE0C4 File Offset: 0x001AC2C4
	protected override IReadOnlyCollection<CultureTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.culture.getTraits();
		}
	}
}
