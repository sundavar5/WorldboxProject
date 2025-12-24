using System;
using System.Collections.Generic;

// Token: 0x0200073F RID: 1855
public class ReligionSelectedContainerTraits : SelectedContainerTraits<ReligionTrait, ReligionTraitButton, ReligionTraitsContainer, ReligionTraitsEditor>
{
	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x0019F8FC File Offset: 0x0019DAFC
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x06003AE9 RID: 15081 RVA: 0x0019F8FF File Offset: 0x0019DAFF
	protected override IReadOnlyCollection<ReligionTrait> getTraits()
	{
		return SelectedMetas.selected_religion.getTraits();
	}

	// Token: 0x06003AEA RID: 15082 RVA: 0x0019F90B File Offset: 0x0019DB0B
	protected override bool canEditTraits()
	{
		return true;
	}
}
