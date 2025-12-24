using System;
using System.Collections.Generic;

// Token: 0x02000791 RID: 1937
public interface ITraitsContainer<TTrait, TTraitButton> where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait>
{
	// Token: 0x06003D79 RID: 15737
	IReadOnlyCollection<TTraitButton> getTraitButtons();
}
