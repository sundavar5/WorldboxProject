using System;
using ai;
using ai.behaviours;

// Token: 0x020003DA RID: 986
public class BehFinishTalk : BehaviourActionActor
{
	// Token: 0x0600228F RID: 8847 RVA: 0x00121D2F File Offset: 0x0011FF2F
	public BehFinishTalk()
	{
		this.socialize = true;
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x00121D40 File Offset: 0x0011FF40
	public override BehResult execute(Actor pActor)
	{
		BaseSimObject beh_actor_target = pActor.beh_actor_target;
		Actor tTarget = (beh_actor_target != null) ? beh_actor_target.a : null;
		if (tTarget == null)
		{
			return BehResult.Stop;
		}
		if (!this.stillCanTalk(tTarget))
		{
			return BehResult.Stop;
		}
		this.finishTalk(pActor, tTarget);
		return BehResult.Continue;
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x00121D79 File Offset: 0x0011FF79
	private bool stillCanTalk(Actor pTarget)
	{
		return pTarget.isAlive() && !pTarget.isLying();
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x00121D90 File Offset: 0x0011FF90
	private void finishTalk(Actor pActor, Actor pTarget)
	{
		pActor.resetSocialize();
		pTarget.resetSocialize();
		bool flag = Randy.randomChance(0.7f);
		int tBonusValue;
		if (flag)
		{
			tBonusValue = 10;
		}
		else
		{
			tBonusValue = -15;
		}
		pActor.changeHappiness("just_talked", tBonusValue);
		pTarget.changeHappiness("just_talked", tBonusValue);
		pActor.addStatusEffect("recovery_social", 0f, true);
		pTarget.addStatusEffect("recovery_social", 0f, true);
		if (flag)
		{
			ActorTool.checkFallInLove(pActor, pTarget);
		}
		if (flag)
		{
			ActorTool.checkBecomingBestFriends(pActor, pTarget);
		}
		this.checkMetaSpread(pActor, pTarget);
		if (pActor.hasCulture() && pActor.culture.hasTrait("youth_reverence") && this.throwDiceForGift(pActor, pTarget) && pActor.isAdult() && pTarget.getAge() < pActor.getAge())
		{
			this.makeGift(pActor, pTarget);
		}
		if (pActor.hasCulture() && pActor.culture.hasTrait("elder_reverence") && this.throwDiceForGift(pActor, pTarget) && pActor.isAdult() && pTarget.getAge() > pActor.getAge())
		{
			this.makeGift(pActor, pTarget);
		}
		this.checkPassLearningAttributes(pActor, pTarget);
		float tTimer = Randy.randomFloat(1.1f, 3.3f);
		pActor.timer_action = tTimer;
		pTarget.timer_action = tTimer;
	}

	// Token: 0x06002293 RID: 8851 RVA: 0x00121EC4 File Offset: 0x001200C4
	private void checkAttribue(Actor pActor, Actor pTarget, string pAttributeID)
	{
		if (!Randy.randomChance(0.3f))
		{
			return;
		}
		if (pActor.stats[pAttributeID] > pTarget.stats[pAttributeID])
		{
			BaseStats stats = pTarget.stats;
			float num = stats[pAttributeID];
			stats[pAttributeID] = num + 1f;
			return;
		}
		if (pActor.stats[pAttributeID] < pTarget.stats[pAttributeID])
		{
			BaseStats stats2 = pActor.stats;
			float num = stats2[pAttributeID];
			stats2[pAttributeID] = num + 1f;
		}
	}

	// Token: 0x06002294 RID: 8852 RVA: 0x00121F4D File Offset: 0x0012014D
	private void checkPassLearningAttributes(Actor pActor, Actor pTarget)
	{
		this.checkAttribue(pActor, pTarget, "intelligence");
		this.checkAttribue(pActor, pTarget, "warfare");
		this.checkAttribue(pActor, pTarget, "diplomacy");
		this.checkAttribue(pActor, pTarget, "stewardship");
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x00121F83 File Offset: 0x00120183
	private void checkMetaSpread(Actor pActor, Actor pTarget)
	{
		if (!pActor.hasSubspecies())
		{
			return;
		}
		if (!pTarget.hasSubspecies())
		{
			return;
		}
		this.tryToSpreadCulture(pActor, pTarget);
		this.tryToSpreadLanguage(pActor, pTarget);
		this.tryToSpreadReligion(pActor, pTarget);
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x00121FB0 File Offset: 0x001201B0
	private void tryToSpreadCulture(Actor pActor, Actor pTarget)
	{
		if (!pActor.subspecies.has_advanced_memory)
		{
			return;
		}
		if (!pTarget.subspecies.has_advanced_memory)
		{
			return;
		}
		Culture tCultureToSet = this.decideCulture(pActor, pTarget);
		if (tCultureToSet != null)
		{
			pActor.tryToConvertToCulture(tCultureToSet);
			pTarget.tryToConvertToCulture(tCultureToSet);
			if (tCultureToSet.hasTrait("pep_talks") && Randy.randomChance(0.5f))
			{
				pActor.addStatusEffect("inspired", 0f, true);
				pTarget.addStatusEffect("inspired", 0f, true);
			}
			if (tCultureToSet.hasTrait("expertise_exchange"))
			{
				pActor.addExperience(CultureTraitLibrary.getValue("expertise_exchange"));
				pTarget.addExperience(CultureTraitLibrary.getValue("expertise_exchange"));
			}
			if (tCultureToSet.hasTrait("gossip_lovers"))
			{
				pActor.changeHappiness("just_talked_gossip", 0);
				pTarget.changeHappiness("just_talked_gossip", 0);
			}
		}
	}

	// Token: 0x06002297 RID: 8855 RVA: 0x0012208C File Offset: 0x0012028C
	private void tryToSpreadLanguage(Actor pActor, Actor pTarget)
	{
		if (!pActor.subspecies.has_advanced_communication)
		{
			return;
		}
		if (!pTarget.subspecies.has_advanced_communication)
		{
			return;
		}
		Language tLanguageToSet = this.decideLanguage(pActor, pTarget);
		if (tLanguageToSet != null)
		{
			pActor.tryToConvertToLanguage(tLanguageToSet);
			pTarget.tryToConvertToLanguage(tLanguageToSet);
		}
	}

	// Token: 0x06002298 RID: 8856 RVA: 0x001220D4 File Offset: 0x001202D4
	private void tryToSpreadReligion(Actor pActor, Actor pTarget)
	{
		if (!pActor.subspecies.has_advanced_memory)
		{
			return;
		}
		if (!pTarget.subspecies.has_advanced_memory)
		{
			return;
		}
		Religion tReligionToSet = this.decideReligion(pActor, pTarget);
		if (tReligionToSet != null)
		{
			pActor.tryToConvertToReligion(tReligionToSet);
			pTarget.tryToConvertToReligion(tReligionToSet);
		}
	}

	// Token: 0x06002299 RID: 8857 RVA: 0x0012211C File Offset: 0x0012031C
	private Religion decideReligion(Actor pActor1, Actor pActor2)
	{
		Religion tReligion = pActor1.religion;
		Religion tReligion2 = pActor2.religion;
		if (tReligion == null && tReligion2 == null)
		{
			return null;
		}
		if (tReligion == null)
		{
			return tReligion2;
		}
		if (tReligion2 == null)
		{
			return tReligion;
		}
		Religion random;
		using (ListPool<Religion> tPotReligions = new ListPool<Religion>())
		{
			tPotReligions.Add(tReligion);
			tPotReligions.Add(tReligion2);
			if (pActor1.hasCity() && pActor1.religion == pActor1.city.getReligion())
			{
				tPotReligions.Add(pActor1.religion);
			}
			if (pActor1.kingdom.hasReligion() && pActor1.religion == pActor1.kingdom.getReligion())
			{
				tPotReligions.Add(pActor1.religion);
			}
			if (pActor2.hasCity() && pActor2.religion == pActor2.city.getReligion())
			{
				tPotReligions.Add(pActor2.religion);
			}
			if (pActor2.kingdom.hasReligion() && pActor2.religion == pActor2.kingdom.getReligion())
			{
				tPotReligions.Add(pActor2.religion);
			}
			random = tPotReligions.GetRandom<Religion>();
		}
		return random;
	}

	// Token: 0x0600229A RID: 8858 RVA: 0x00122228 File Offset: 0x00120428
	private Language decideLanguage(Actor pActor1, Actor pActor2)
	{
		Language tLanguage = pActor1.language;
		Language tLanguage2 = pActor2.language;
		if (tLanguage == null && tLanguage2 == null)
		{
			return null;
		}
		if (tLanguage == null)
		{
			return tLanguage2;
		}
		if (tLanguage2 == null)
		{
			return tLanguage;
		}
		Language random;
		using (ListPool<Language> tPotLanguages = new ListPool<Language>())
		{
			int tAmount_ = 3;
			int tAmount_2 = 3;
			if (pActor1.hasLanguage() && pActor1.language.hasTrait("melodic"))
			{
				tAmount_ += LanguageTraitLibrary.getValue("melodic");
			}
			if (pActor2.hasLanguage() && pActor2.language.hasTrait("melodic"))
			{
				tAmount_2 += LanguageTraitLibrary.getValue("melodic");
			}
			if (pActor1.hasCity() && pActor1.language == pActor1.city.getLanguage())
			{
				tAmount_++;
			}
			if (pActor1.kingdom.hasLanguage() && pActor1.language == pActor1.kingdom.getLanguage())
			{
				tAmount_++;
			}
			if (pActor2.hasCity() && pActor2.language == pActor2.city.getLanguage())
			{
				tAmount_2++;
			}
			if (pActor2.kingdom.hasLanguage() && pActor2.language == pActor2.kingdom.getLanguage())
			{
				tAmount_2++;
			}
			tPotLanguages.AddTimes(tAmount_, tLanguage);
			tPotLanguages.AddTimes(tAmount_2, tLanguage2);
			random = tPotLanguages.GetRandom<Language>();
		}
		return random;
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x00122374 File Offset: 0x00120574
	private Culture decideCulture(Actor pActor1, Actor pActor2)
	{
		Culture tCulture = pActor1.culture;
		Culture tCulture2 = pActor2.culture;
		if (tCulture == null && tCulture2 == null)
		{
			return null;
		}
		if (tCulture == null)
		{
			return tCulture2;
		}
		if (tCulture2 == null)
		{
			return tCulture;
		}
		Culture random;
		using (ListPool<Culture> tPotCultures = new ListPool<Culture>())
		{
			int tAmount_ = 3;
			int tAmount_2 = 3;
			if (pActor1.hasLanguage() && pActor1.language.hasTrait("melodic"))
			{
				tAmount_ += LanguageTraitLibrary.getValue("melodic");
			}
			if (pActor2.hasLanguage() && pActor2.language.hasTrait("melodic"))
			{
				tAmount_2 += LanguageTraitLibrary.getValue("melodic");
			}
			if (pActor1.hasCity() && pActor1.culture == pActor1.city.getCulture())
			{
				tAmount_++;
			}
			if (pActor1.kingdom.hasCulture() && pActor1.culture == pActor1.kingdom.getCulture())
			{
				tAmount_++;
			}
			if (pActor2.hasCity() && pActor2.culture == pActor2.city.getCulture())
			{
				tAmount_2++;
			}
			if (pActor2.kingdom.hasCulture() && pActor2.culture == pActor2.kingdom.getCulture())
			{
				tAmount_2++;
			}
			tPotCultures.AddTimes(tAmount_, tCulture);
			tPotCultures.AddTimes(tAmount_2, tCulture2);
			random = tPotCultures.GetRandom<Culture>();
		}
		return random;
	}

	// Token: 0x0600229C RID: 8860 RVA: 0x001224C0 File Offset: 0x001206C0
	private bool throwDiceForGift(Actor pActor, Actor pTarget)
	{
		bool flag = pActor.isRelatedTo(pTarget) || pActor.isImportantTo(pTarget);
		float tChance = 0.2f;
		if (flag)
		{
			tChance += 0.3f;
		}
		return Randy.randomChance(tChance);
	}

	// Token: 0x0600229D RID: 8861 RVA: 0x001224F8 File Offset: 0x001206F8
	private void makeGift(Actor pActor, Actor pTarget)
	{
		bool tItemGift = pTarget.tryToAcceptGift(pActor);
		int tRandomMoney = pActor.getMoneyForGift();
		if (tRandomMoney > 0)
		{
			pTarget.addMoney(tRandomMoney);
		}
		if (tRandomMoney > 0 || tItemGift)
		{
			pActor.changeHappiness("just_gave_gift", 0);
			pTarget.changeHappiness("just_received_gift", 0);
		}
	}
}
