using System;
using System.Collections.Generic;

// Token: 0x02000785 RID: 1925
public class TooltipActorTraitsRow : TooltipTraitsRow<ActorTrait>
{
	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06003D58 RID: 15704 RVA: 0x001AE090 File Offset: 0x001AC290
	protected override IReadOnlyCollection<ActorTrait> traits_hashset
	{
		get
		{
			return this.tooltip_data.actor.getTraits();
		}
	}
}
