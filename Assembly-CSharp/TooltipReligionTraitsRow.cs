using System;
using System.Collections.Generic;

// Token: 0x0200078C RID: 1932
public class TooltipReligionTraitsRow : TooltipTraitsRow<ReligionTrait>
{
	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06003D69 RID: 15721 RVA: 0x001AE246 File Offset: 0x001AC446
	protected override IReadOnlyCollection<ReligionTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.religion.getTraits();
		}
	}
}
