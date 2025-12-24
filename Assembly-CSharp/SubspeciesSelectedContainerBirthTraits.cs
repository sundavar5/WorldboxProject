using System;
using System.Collections.Generic;

// Token: 0x0200076B RID: 1899
public class SubspeciesSelectedContainerBirthTraits : SelectedContainerTraits<ActorTrait, ActorTraitButton, SubspeciesBirthTraitsContainer, SubspeciesBirthTraitsEditor>
{
	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06003C41 RID: 15425 RVA: 0x001A333D File Offset: 0x001A153D
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x06003C42 RID: 15426 RVA: 0x001A3340 File Offset: 0x001A1540
	protected override IReadOnlyCollection<ActorTrait> getTraits()
	{
		return SelectedMetas.selected_subspecies.getActorBirthTraits().getTraits();
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x001A3351 File Offset: 0x001A1551
	protected override bool canEditTraits()
	{
		return true;
	}
}
