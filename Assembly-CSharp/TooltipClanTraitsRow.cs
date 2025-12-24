using System;
using System.Collections.Generic;

// Token: 0x02000786 RID: 1926
public class TooltipClanTraitsRow : TooltipTraitsRow<ClanTrait>
{
	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06003D5A RID: 15706 RVA: 0x001AE0AA File Offset: 0x001AC2AA
	protected override IReadOnlyCollection<ClanTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.clan.getTraits();
		}
	}
}
