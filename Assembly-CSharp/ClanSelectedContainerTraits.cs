using System;
using System.Collections.Generic;

// Token: 0x02000669 RID: 1641
public class ClanSelectedContainerTraits : SelectedContainerTraits<ClanTrait, ClanTraitButton, ClanTraitsContainer, ClanTraitsEditor>
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06003518 RID: 13592 RVA: 0x00187E66 File Offset: 0x00186066
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x00187E69 File Offset: 0x00186069
	protected override IReadOnlyCollection<ClanTrait> getTraits()
	{
		return SelectedMetas.selected_clan.getTraits();
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x00187E75 File Offset: 0x00186075
	protected override bool canEditTraits()
	{
		return true;
	}
}
