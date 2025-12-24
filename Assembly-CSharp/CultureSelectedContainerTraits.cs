using System;
using System.Collections.Generic;

// Token: 0x02000676 RID: 1654
public class CultureSelectedContainerTraits : SelectedContainerTraits<CultureTrait, CultureTraitButton, CultureTraitsContainer, CultureTraitsEditor>
{
	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06003552 RID: 13650 RVA: 0x00188703 File Offset: 0x00186903
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x00188706 File Offset: 0x00186906
	protected override IReadOnlyCollection<CultureTrait> getTraits()
	{
		return SelectedMetas.selected_culture.getTraits();
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x00188712 File Offset: 0x00186912
	protected override bool canEditTraits()
	{
		return true;
	}
}
