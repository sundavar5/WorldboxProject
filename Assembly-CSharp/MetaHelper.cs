using System;

// Token: 0x02000222 RID: 546
public static class MetaHelper
{
	// Token: 0x060013E6 RID: 5094 RVA: 0x000D9918 File Offset: 0x000D7B18
	public static void addRandomTrait<TTrait>(ITraitsOwner<TTrait> pMetaObject, BaseTraitLibrary<TTrait> pLibrary) where TTrait : BaseTrait<TTrait>
	{
		int tMin = 1;
		int tMax = 3;
		if (WorldLawLibrary.world_law_glitched_noosphere.isEnabled())
		{
			tMin = 3;
			tMax = 6;
		}
		int tAmount = Randy.randomInt(tMin, tMax);
		for (int i = 0; i < tAmount; i++)
		{
			TTrait tTrait = pLibrary.getRandomSpawnTrait();
			if (tTrait.isAvailable())
			{
				pMetaObject.addTrait(tTrait, true);
			}
		}
	}
}
