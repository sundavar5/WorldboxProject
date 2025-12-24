using System;

namespace ai.behaviours
{
	// Token: 0x0200091A RID: 2330
	public class BehCheckForBabiesFromSexualReproduction : BehaviourActionActor
	{
		// Token: 0x060045C8 RID: 17864 RVA: 0x001D3A64 File Offset: 0x001D1C64
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasLover())
			{
				return BehResult.Stop;
			}
			pActor.addAfterglowStatus();
			pActor.lover.addAfterglowStatus();
			pActor.changeHappiness("just_kissed", 0);
			pActor.lover.changeHappiness("just_kissed", 0);
			if (BabyHelper.isMetaLimitsReached(pActor))
			{
				return BehResult.Stop;
			}
			RateCounter counter_reproduction_acts = pActor.subspecies.counter_reproduction_acts;
			if (counter_reproduction_acts != null)
			{
				counter_reproduction_acts.registerEvent();
			}
			this.checkForBabies(pActor, pActor.lover);
			return BehResult.Continue;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x001D3ADC File Offset: 0x001D1CDC
		private void checkForBabies(Actor pParentA, Actor pParentB)
		{
			this.checkFamily(pParentA, pParentB);
			Subspecies tSubspecies = pParentA.subspecies;
			Actor tMotherTarget = null;
			ReproductiveStrategy tReproductiveStrategy = tSubspecies.getReproductionStrategy();
			if (tSubspecies.hasTraitReproductionSexual())
			{
				if (pParentA.isSexFemale())
				{
					tMotherTarget = pParentA;
				}
				else if (pParentB.isSexFemale())
				{
					tMotherTarget = pParentB;
				}
			}
			else if (tSubspecies.hasTraitReproductionSexualHermaphroditic())
			{
				if (Randy.randomBool())
				{
					tMotherTarget = pParentA;
				}
				else
				{
					tMotherTarget = pParentB;
				}
			}
			if (tMotherTarget == null)
			{
				return;
			}
			if (!BabyHelper.canMakeBabies(tMotherTarget))
			{
				return;
			}
			if (tMotherTarget.isSexMale() && tMotherTarget.subspecies.hasTraitReproductionSexual())
			{
				return;
			}
			float tMaturationTime = pParentA.getMaturationTimeSeconds();
			switch (tReproductiveStrategy)
			{
			case ReproductiveStrategy.Egg:
			case ReproductiveStrategy.SpawnUnitImmediate:
				BabyMaker.makeBabiesViaSexual(tMotherTarget, pParentA, pParentB);
				tMotherTarget.subspecies.counterReproduction();
				return;
			case ReproductiveStrategy.Pregnancy:
				BabyHelper.babyMakingStart(tMotherTarget);
				tMotherTarget.addStatusEffect("pregnant", tMaturationTime, true);
				tMotherTarget.subspecies.counterReproduction();
				return;
			default:
				return;
			}
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001D3BA8 File Offset: 0x001D1DA8
		private void checkFamily(Actor pActor, Actor pLover)
		{
			bool tNeedNewFamily = false;
			if (pActor.hasFamily())
			{
				if (pActor.family != pActor.lover.family)
				{
					tNeedNewFamily = true;
				}
			}
			else
			{
				tNeedNewFamily = true;
			}
			if (tNeedNewFamily)
			{
				BehaviourActionBase<Actor>.world.families.newFamily(pActor, pActor.current_tile, pLover);
			}
		}
	}
}
