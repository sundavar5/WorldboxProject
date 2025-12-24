using System;
using System.Collections.Generic;

// Token: 0x02000220 RID: 544
public interface ITraitsOwner<TTrait> where TTrait : BaseTrait<TTrait>
{
	// Token: 0x060013DD RID: 5085
	bool hasTrait(TTrait pTraitId);

	// Token: 0x060013DE RID: 5086
	bool addTrait(TTrait pTraitId, bool pRemoveOpposites = false);

	// Token: 0x060013DF RID: 5087
	bool removeTrait(TTrait pTrait);

	// Token: 0x060013E0 RID: 5088
	IReadOnlyCollection<TTrait> getTraits();

	// Token: 0x060013E1 RID: 5089
	bool hasTraits();

	// Token: 0x060013E2 RID: 5090
	void sortTraits(IReadOnlyCollection<TTrait> pTraits);

	// Token: 0x060013E3 RID: 5091
	void traitModifiedEvent();

	// Token: 0x060013E4 RID: 5092
	ActorAsset getActorAsset();
}
