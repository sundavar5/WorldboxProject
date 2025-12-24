using System;
using System.Collections.Generic;

// Token: 0x020006F6 RID: 1782
public class KingdomSelectedContainerTraits : SelectedContainerTraits<KingdomTrait, KingdomTraitButton, KingdomTraitsContainer, KingdomTraitsEditor>
{
	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06003932 RID: 14642 RVA: 0x001981A8 File Offset: 0x001963A8
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x06003933 RID: 14643 RVA: 0x001981AB File Offset: 0x001963AB
	protected override IReadOnlyCollection<KingdomTrait> getTraits()
	{
		return SelectedMetas.selected_kingdom.getTraits();
	}

	// Token: 0x06003934 RID: 14644 RVA: 0x001981B7 File Offset: 0x001963B7
	protected override bool canEditTraits()
	{
		return true;
	}
}
