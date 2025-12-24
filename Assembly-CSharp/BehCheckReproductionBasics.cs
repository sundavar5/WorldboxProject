using System;
using ai.behaviours;

// Token: 0x020003AD RID: 941
public class BehCheckReproductionBasics : BehaviourActionActor
{
	// Token: 0x06002212 RID: 8722 RVA: 0x0011F3A8 File Offset: 0x0011D5A8
	public override BehResult execute(Actor pActor)
	{
		if (pActor.isSapient())
		{
			if (!WorldLawLibrary.world_law_civ_babies.isEnabled())
			{
				return BehResult.Stop;
			}
		}
		else if (!WorldLawLibrary.world_law_animals_babies.isEnabled())
		{
			return BehResult.Stop;
		}
		RateCounter counter_reproduction_basics_ = pActor.subspecies.counter_reproduction_basics_1;
		if (counter_reproduction_basics_ != null)
		{
			counter_reproduction_basics_.registerEvent();
		}
		if (!pActor.canBreed())
		{
			return BehResult.Stop;
		}
		RateCounter counter_reproduction_basics_2 = pActor.subspecies.counter_reproduction_basics_2;
		if (counter_reproduction_basics_2 != null)
		{
			counter_reproduction_basics_2.registerEvent();
		}
		if (!BabyHelper.canMakeBabies(pActor))
		{
			return BehResult.Stop;
		}
		if (BabyHelper.isMetaLimitsReached(pActor))
		{
			return BehResult.Stop;
		}
		RateCounter counter_reproduction_basics_3 = pActor.subspecies.counter_reproduction_basics_3;
		if (counter_reproduction_basics_3 != null)
		{
			counter_reproduction_basics_3.registerEvent();
		}
		if (!pActor.isImportantPerson() && !pActor.isPlacePrivateForBreeding())
		{
			return BehResult.Stop;
		}
		RateCounter counter_reproduction_basics_4 = pActor.subspecies.counter_reproduction_basics_4;
		if (counter_reproduction_basics_4 != null)
		{
			counter_reproduction_basics_4.registerEvent();
		}
		return BehResult.Continue;
	}
}
