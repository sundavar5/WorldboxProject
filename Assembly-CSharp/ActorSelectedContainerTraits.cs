using System;
using System.Collections.Generic;

// Token: 0x020006D1 RID: 1745
public class ActorSelectedContainerTraits : SelectedContainerTraits<ActorTrait, ActorTraitButton, ActorTraitsContainer, ActorTraitsEditor>
{
	// Token: 0x1700031A RID: 794
	// (get) Token: 0x060037FF RID: 14335 RVA: 0x0019312A File Offset: 0x0019132A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Unit;
		}
	}

	// Token: 0x06003800 RID: 14336 RVA: 0x0019312E File Offset: 0x0019132E
	protected override IReadOnlyCollection<ActorTrait> getTraits()
	{
		return SelectedUnit.unit.getTraits();
	}

	// Token: 0x06003801 RID: 14337 RVA: 0x0019313A File Offset: 0x0019133A
	protected override bool canEditTraits()
	{
		return SelectedUnit.unit.asset.can_edit_traits;
	}
}
