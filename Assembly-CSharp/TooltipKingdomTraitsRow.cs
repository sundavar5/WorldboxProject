using System;
using System.Collections.Generic;

// Token: 0x0200078A RID: 1930
public class TooltipKingdomTraitsRow : TooltipTraitsRow<KingdomTrait>
{
	// Token: 0x17000398 RID: 920
	// (get) Token: 0x06003D65 RID: 15717 RVA: 0x001AE212 File Offset: 0x001AC412
	protected override IReadOnlyCollection<KingdomTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.kingdom.getTraits();
		}
	}
}
