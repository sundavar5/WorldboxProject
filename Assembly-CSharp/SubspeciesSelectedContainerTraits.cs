using System;
using System.Collections.Generic;

// Token: 0x0200076C RID: 1900
public class SubspeciesSelectedContainerTraits : SelectedContainerTraits<SubspeciesTrait, SubspeciesTraitButton, SubspeciesTraitsContainer, SubspeciesTraitsEditor>
{
	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06003C45 RID: 15429 RVA: 0x001A335C File Offset: 0x001A155C
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x06003C46 RID: 15430 RVA: 0x001A335F File Offset: 0x001A155F
	protected override IReadOnlyCollection<SubspeciesTrait> getTraits()
	{
		return SelectedMetas.selected_subspecies.getTraits();
	}

	// Token: 0x06003C47 RID: 15431 RVA: 0x001A336B File Offset: 0x001A156B
	protected override bool canEditTraits()
	{
		return true;
	}
}
