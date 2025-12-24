using System;
using ai.behaviours;

// Token: 0x0200039D RID: 925
public class BehFinishReading : BehCitizenActionCity
{
	// Token: 0x060021C6 RID: 8646 RVA: 0x0011D8F4 File Offset: 0x0011BAF4
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.uses_books = true;
		this.uses_religions = true;
		this.uses_languages = true;
		this.uses_cultures = true;
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x0011D918 File Offset: 0x0011BB18
	public override BehResult execute(Actor pActor)
	{
		Book tBook = pActor.beh_book_target;
		if (tBook == null || !tBook.isAlive())
		{
			return BehResult.Stop;
		}
		this.checkBookTrait(pActor, tBook);
		this.checkBookValueBonuses(pActor, tBook);
		this.checkBookAttributes(pActor, tBook);
		this.checkSpecialBookRewards(pActor, tBook);
		this.tryToConvertActorToMetaFromBook(pActor, tBook);
		this.checkBookAssetAction(pActor, tBook);
		this.tryToGetMetaTraitsFromBook(pActor, tBook);
		tBook.increaseReadTimes();
		return BehResult.Continue;
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x0011D978 File Offset: 0x0011BB78
	private void checkBookAttributes(Actor pActor, Book pBook)
	{
		foreach (BaseStatsContainer pContainer in pBook.getBaseStats().getList())
		{
			if (pContainer.asset.actor_data_attribute)
			{
				ActorData data = pActor.data;
				string id = pContainer.id;
				data[id] += pContainer.value;
			}
		}
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x0011D9FC File Offset: 0x0011BBFC
	private void checkBookAssetAction(Actor pActor, Book pBook)
	{
		BookTypeAsset tAsset = pBook.getAsset();
		BookReadAction read_action = tAsset.read_action;
		if (read_action == null)
		{
			return;
		}
		read_action(pActor, tAsset);
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x0011DA24 File Offset: 0x0011BC24
	private void checkSpecialBookRewards(Actor pActor, Book pBook)
	{
		foreach (LanguageTrait tTrait in pBook.getLanguage().getTraits())
		{
			BookTraitAction read_book_trait_action = tTrait.read_book_trait_action;
			if (read_book_trait_action != null)
			{
				read_book_trait_action(pActor, tTrait, pBook);
			}
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x0011DA84 File Offset: 0x0011BC84
	private void checkBookValueBonuses(Actor pActor, Book pBook)
	{
		int tHappiness = pBook.getHappiness();
		int tExperience = pBook.getExperience();
		int tMana = pBook.getMana();
		if (pActor.hasCulture())
		{
			if (pActor.culture.hasTrait("reading_lovers") && tHappiness < 0)
			{
				tHappiness *= -1;
			}
			if (pActor.culture.hasTrait("attentive_readers"))
			{
				tExperience *= (int)((float)tExperience * CultureTraitLibrary.getValueFloat("attentive_readers"));
			}
		}
		pActor.changeHappiness("just_read_book", tHappiness);
		pActor.addExperience(tExperience);
		pActor.addMana(tMana);
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x0011DB08 File Offset: 0x0011BD08
	private void checkBookTrait(Actor pActor, Book pBook)
	{
		if (Randy.randomBool())
		{
			ActorTrait tTrait = pBook.getBookTraitActor();
			if (tTrait != null)
			{
				pActor.addTrait(tTrait, false);
			}
		}
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x0011DB2F File Offset: 0x0011BD2F
	private void tryToConvertActorToMetaFromBook(Actor pActor, Book pBook)
	{
		this.tryToConvertActorToBookCulture(pActor, pBook);
		this.tryToConvertActorToBookLanguage(pActor, pBook);
		this.tryToConvertActorToBookReligion(pActor, pBook);
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x0011DB49 File Offset: 0x0011BD49
	private void tryToGetMetaTraitsFromBook(Actor pActor, Book pBook)
	{
		if (!pActor.isKing() && !pActor.isCityLeader())
		{
			return;
		}
		this.tryToGetMetaTraitFromBookCulture(pActor, pBook);
		this.tryToGetMetaTraitFromBookLanguage(pActor, pBook);
		this.tryToGetMetaTraitFromBookReligion(pActor, pBook);
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x0011DB74 File Offset: 0x0011BD74
	private void tryToGetMetaTraitFromBookCulture(Actor pActor, Book pBook)
	{
		if (!pActor.hasCulture())
		{
			return;
		}
		CultureTrait tTrait = pBook.getBookTraitCulture();
		if (tTrait == null)
		{
			return;
		}
		if (Randy.randomBool())
		{
			pActor.culture.addTrait(tTrait, false);
		}
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x0011DBAC File Offset: 0x0011BDAC
	private void tryToGetMetaTraitFromBookLanguage(Actor pActor, Book pBook)
	{
		if (!pActor.hasLanguage())
		{
			return;
		}
		LanguageTrait tTrait = pBook.getBookTraitLanguage();
		if (tTrait == null)
		{
			return;
		}
		if (Randy.randomBool())
		{
			pActor.language.addTrait(tTrait, false);
		}
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x0011DBE4 File Offset: 0x0011BDE4
	private void tryToGetMetaTraitFromBookReligion(Actor pActor, Book pBook)
	{
		if (!pActor.hasReligion())
		{
			return;
		}
		ReligionTrait tTrait = pBook.getBookTraitReligion();
		if (tTrait == null)
		{
			return;
		}
		if (Randy.randomBool())
		{
			pActor.religion.addTrait(tTrait, false);
		}
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x0011DC1C File Offset: 0x0011BE1C
	private void tryToConvertActorToBookReligion(Actor pActor, Book pBook)
	{
		Religion tBookReligion = pBook.getReligion();
		if (tBookReligion == null)
		{
			return;
		}
		if (pActor.religion == tBookReligion)
		{
			return;
		}
		using (ListPool<Religion> tListPool = new ListPool<Religion>(6))
		{
			if (pActor.hasReligion())
			{
				tListPool.AddTimes(3, pActor.religion);
				if (this.hasStylishWritingActor(pActor))
				{
					tListPool.AddTimes(this.getStylishWritingValue(), pActor.religion);
				}
			}
			tListPool.AddTimes(3, tBookReligion);
			if (this.hasStylishWritingBook(pBook))
			{
				tListPool.AddTimes(this.getStylishWritingValue(), tBookReligion);
			}
			Religion tNewReligion = tListPool.GetRandom<Religion>();
			if (tNewReligion != pActor.religion)
			{
				pActor.tryToConvertToReligion(tNewReligion);
			}
		}
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x0011DCC8 File Offset: 0x0011BEC8
	private void tryToConvertActorToBookLanguage(Actor pActor, Book pBook)
	{
		Language tBookLanguage = pBook.getLanguage();
		if (tBookLanguage == null)
		{
			return;
		}
		if (pActor.language == tBookLanguage)
		{
			return;
		}
		using (ListPool<Language> tPotLanguages = new ListPool<Language>())
		{
			if (pActor.hasLanguage())
			{
				tPotLanguages.AddTimes(3, pActor.language);
				if (this.hasStylishWritingActor(pActor))
				{
					tPotLanguages.AddTimes(this.getStylishWritingValue(), pActor.language);
				}
			}
			tPotLanguages.AddTimes(3, tBookLanguage);
			if (this.hasStylishWritingBook(pBook))
			{
				tPotLanguages.AddTimes(this.getStylishWritingValue(), tBookLanguage);
			}
			Language tNewLanguage = tPotLanguages.GetRandom<Language>();
			if (tNewLanguage != pActor.language)
			{
				pActor.tryToConvertToLanguage(tNewLanguage);
			}
		}
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x0011DD74 File Offset: 0x0011BF74
	private void tryToConvertActorToBookCulture(Actor pActor, Book pBook)
	{
		Culture tCultureBook = pBook.getCulture();
		if (tCultureBook == null)
		{
			return;
		}
		Culture tCultureActor = pActor.culture;
		if (tCultureActor == tCultureBook)
		{
			return;
		}
		using (ListPool<Culture> tListPool = new ListPool<Culture>())
		{
			if (pActor.hasCulture())
			{
				tListPool.AddTimes(3, tCultureActor);
				if (this.hasStylishWritingActor(pActor))
				{
					tListPool.AddTimes(this.getStylishWritingValue(), tCultureActor);
				}
			}
			tListPool.AddTimes(3, tCultureBook);
			if (this.hasStylishWritingBook(pBook))
			{
				tListPool.AddTimes(this.getStylishWritingValue(), tCultureBook);
			}
			Culture tNewCulture = tListPool.GetRandom<Culture>();
			if (tNewCulture != tCultureActor)
			{
				pActor.tryToConvertToCulture(tNewCulture);
			}
		}
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x0011DE14 File Offset: 0x0011C014
	private bool hasStylishWritingActor(Actor pActor)
	{
		return pActor.hasLanguage() && pActor.language.hasTrait("stylish_writing");
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x0011DE33 File Offset: 0x0011C033
	private bool hasStylishWritingBook(Book pBook)
	{
		return pBook.getLanguage().hasTrait("stylish_writing");
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x0011DE4A File Offset: 0x0011C04A
	private int getStylishWritingValue()
	{
		return LanguageTraitLibrary.getValue("stylish_writing");
	}
}
