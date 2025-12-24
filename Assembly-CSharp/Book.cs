using System;

// Token: 0x0200022E RID: 558
public class Book : CoreSystemObject<BookData>
{
	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06001518 RID: 5400 RVA: 0x000DC945 File Offset: 0x000DAB45
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.books;
		}
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000DC954 File Offset: 0x000DAB54
	public void newBook(Actor pByActor, BookTypeAsset pBookType, ActorTrait pTraitActor, CultureTrait pTraitCulture, LanguageTrait pTraitLanguage, ReligionTrait pTraitReligion)
	{
		string tName = NameGenerator.generateNameFromTemplate(pBookType.name_template, pByActor, null, false);
		base.setName(tName, true);
		this.data.book_type = pBookType.id;
		this.data.path_cover = World.world.books.getNewCoverPath();
		this.data.path_icon = pBookType.getNewIconPath();
		this.data.author_name = pByActor.getName();
		this.data.author_id = pByActor.getID();
		BookData data = this.data;
		Clan clan = pByActor.clan;
		data.author_clan_name = ((clan != null) ? clan.name : null);
		BookData data2 = this.data;
		Clan clan2 = pByActor.clan;
		data2.author_clan_id = ((clan2 != null) ? clan2.id : -1L);
		BookData data3 = this.data;
		Kingdom kingdom = pByActor.kingdom;
		data3.author_kingdom_name = ((kingdom != null) ? kingdom.name : null);
		BookData data4 = this.data;
		Kingdom kingdom2 = pByActor.kingdom;
		data4.author_kingdom_id = ((kingdom2 != null) ? kingdom2.id : -1L);
		BookData data5 = this.data;
		City city = pByActor.city;
		data5.author_city_name = ((city != null) ? city.name : null);
		BookData data6 = this.data;
		City city2 = pByActor.city;
		data6.author_city_id = ((city2 != null) ? city2.id : -1L);
		this.data.language_id = pByActor.language.id;
		this.data.language_name = pByActor.language.name;
		this.data.trait_id_actor = ((pTraitActor != null) ? pTraitActor.id : null);
		this.data.trait_id_language = ((pTraitLanguage != null) ? pTraitLanguage.id : null);
		this.data.trait_id_culture = ((pTraitCulture != null) ? pTraitCulture.id : null);
		this.data.trait_id_religion = ((pTraitReligion != null) ? pTraitReligion.id : null);
		pByActor.language.books.setDirty();
		if (pBookType.save_culture)
		{
			this.data.culture_id = pByActor.culture.id;
			this.data.culture_name = pByActor.culture.name;
			pByActor.culture.books.setDirty();
		}
		if (pBookType.save_religion)
		{
			BookData data7 = this.data;
			Religion religion = pByActor.religion;
			data7.religion_id = ((religion != null) ? religion.id : -1L);
			BookData data8 = this.data;
			Religion religion2 = pByActor.religion;
			data8.religion_name = ((religion2 != null) ? religion2.name : null);
			Religion religion3 = pByActor.religion;
			if (religion3 != null)
			{
				religion3.books.setDirty();
			}
		}
		pByActor.language.data.books_written++;
		if (pByActor.hasClan())
		{
			pByActor.clan.data.books_written++;
		}
		this.recalcBaseStats();
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000DCC00 File Offset: 0x000DAE00
	public BaseStats getBaseStats()
	{
		return this._base_stats_read_action;
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000DCC08 File Offset: 0x000DAE08
	private void recalcBaseStats()
	{
		this._base_stats_read_action.clear();
		this._base_stats_read_action.mergeStats(this.getAsset().base_stats, 1f);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000DCC30 File Offset: 0x000DAE30
	public bool isReadyToBeRead()
	{
		return World.world.getWorldTimeElapsedSince(this.data.timestamp_read_last_time) > 10f;
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000DCC51 File Offset: 0x000DAE51
	public override void loadData(BookData pData)
	{
		base.loadData(pData);
		this.recalcBaseStats();
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000DCC60 File Offset: 0x000DAE60
	public Religion getReligion()
	{
		return World.world.religions.get(this.data.religion_id);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000DCC7C File Offset: 0x000DAE7C
	public Language getLanguage()
	{
		return World.world.languages.get(this.data.language_id);
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000DCC98 File Offset: 0x000DAE98
	public Culture getCulture()
	{
		return World.world.cultures.get(this.data.culture_id);
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000DCCB4 File Offset: 0x000DAEB4
	public ActorTrait getBookTraitActor()
	{
		if (string.IsNullOrEmpty(this.data.trait_id_actor))
		{
			return null;
		}
		return AssetManager.traits.get(this.data.trait_id_actor);
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000DCCDF File Offset: 0x000DAEDF
	public LanguageTrait getBookTraitLanguage()
	{
		if (string.IsNullOrEmpty(this.data.trait_id_language))
		{
			return null;
		}
		return AssetManager.language_traits.get(this.data.trait_id_language);
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000DCD0A File Offset: 0x000DAF0A
	public CultureTrait getBookTraitCulture()
	{
		if (string.IsNullOrEmpty(this.data.trait_id_culture))
		{
			return null;
		}
		return AssetManager.culture_traits.get(this.data.trait_id_culture);
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000DCD35 File Offset: 0x000DAF35
	public ReligionTrait getBookTraitReligion()
	{
		if (string.IsNullOrEmpty(this.data.trait_id_religion))
		{
			return null;
		}
		return AssetManager.religion_traits.get(this.data.trait_id_religion);
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x000DCD60 File Offset: 0x000DAF60
	public int getHappiness()
	{
		int tResult = (int)this._base_stats_read_action["happiness"];
		if (this.getLanguage().hasTrait("beautiful_calligraphy"))
		{
			tResult = (int)((float)tResult * LanguageTraitLibrary.getValueFloat("beautiful_calligraphy"));
		}
		return tResult;
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000DCDA4 File Offset: 0x000DAFA4
	public int getExperience()
	{
		int tResult = (int)this._base_stats_read_action["experience"];
		Language tLanguage = this.getLanguage();
		if (tLanguage.hasTrait("scribble"))
		{
			if (tResult > 1)
			{
				tResult = 1;
			}
		}
		else if (tLanguage.hasTrait("nicely_structured_grammar"))
		{
			tResult = (int)((float)tResult * LanguageTraitLibrary.getValueFloat("nicely_structured_grammar"));
		}
		return tResult;
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000DCDFC File Offset: 0x000DAFFC
	public int getMana()
	{
		return (int)this._base_stats_read_action["mana"];
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000DCE0F File Offset: 0x000DB00F
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000DCE17 File Offset: 0x000DB017
	public BookTypeAsset getAsset()
	{
		return AssetManager.book_types.get(this.data.book_type);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000DCE2E File Offset: 0x000DB02E
	public void readIt()
	{
		this.data.timestamp_read_last_time = World.world.getCurWorldTime();
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000DCE48 File Offset: 0x000DB048
	public void increaseReadTimes()
	{
		this.data.times_read++;
		World.world.game_stats.data.booksRead += 1L;
		World.world.map_stats.booksRead += 1L;
		this.readIt();
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000DCEA3 File Offset: 0x000DB0A3
	public override void Dispose()
	{
		base.Dispose();
		this._base_stats_read_action.clear();
	}

	// Token: 0x040011DD RID: 4573
	private BaseStats _base_stats_read_action = new BaseStats();
}
