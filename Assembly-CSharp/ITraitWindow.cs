using System;
using System.Collections.Generic;

// Token: 0x02000790 RID: 1936
public interface ITraitWindow<TTrait, TTraitButton> : IAugmentationsWindow<ITraitsEditor<TTrait>> where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait>
{
	// Token: 0x06003D73 RID: 15731 RVA: 0x001AE387 File Offset: 0x001AC587
	TraitsContainer<TTrait, TTraitButton> getContainer()
	{
		return this.GetComponentInChildren<TraitsContainer<TTrait, TTraitButton>>(false);
	}

	// Token: 0x06003D74 RID: 15732 RVA: 0x001AE390 File Offset: 0x001AC590
	void reloadTraits(bool pAnimated = true)
	{
		this.getContainer().reloadTraits(pAnimated);
	}

	// Token: 0x06003D75 RID: 15733 RVA: 0x001AE39E File Offset: 0x001AC59E
	ITraitsOwner<TTrait> getTraitsOwner()
	{
		return this.getEditor().getTraitsOwner();
	}

	// Token: 0x06003D76 RID: 15734 RVA: 0x001AE3AB File Offset: 0x001AC5AB
	IReadOnlyCollection<TTrait> getTraits()
	{
		return this.getTraitsOwner().getTraits();
	}

	// Token: 0x06003D77 RID: 15735 RVA: 0x001AE3B8 File Offset: 0x001AC5B8
	void sortTraits(IReadOnlyCollection<TTrait> pTraits)
	{
		this.getTraitsOwner().sortTraits(pTraits);
	}

	// Token: 0x06003D78 RID: 15736 RVA: 0x001AE3C6 File Offset: 0x001AC5C6
	bool hasTraits()
	{
		return this.getTraitsOwner().hasTraits();
	}
}
