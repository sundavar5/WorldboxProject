using System;
using System.Collections.Generic;

// Token: 0x0200078E RID: 1934
public class TooltipSubspeciesTraitsRow : TooltipTraitsRow<SubspeciesTrait>
{
	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06003D6E RID: 15726 RVA: 0x001AE2C6 File Offset: 0x001AC4C6
	protected override IReadOnlyCollection<SubspeciesTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.subspecies.getTraits();
		}
	}
}
