using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class BookManager : CoreSystemManager<Book, BookData>
{
	// Token: 0x0600152F RID: 5423 RVA: 0x000DCF4B File Offset: 0x000DB14B
	public BookManager()
	{
		this.type_id = "book";
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000DCF60 File Offset: 0x000DB160
	public Book generateNewBook(Actor pActor)
	{
		City tCity = pActor.getCity();
		Building tBuilding = tCity.getBuildingWithBookSlot();
		if (tBuilding == null)
		{
			return null;
		}
		Book tBook = this.newBook(pActor);
		if (tBook == null)
		{
			return null;
		}
		World.world.game_stats.data.booksWritten += 1L;
		World.world.map_stats.booksWritten += 1L;
		pActor.changeHappiness("wrote_book", 0);
		tBuilding.addBook(tBook);
		tCity.setStatusDirty();
		return tBook;
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x000DCFDD File Offset: 0x000DB1DD
	public string getNewCoverPath()
	{
		if (BookManager._cached_covers == null)
		{
			BookManager._cached_covers = SpriteTextureLoader.getSpriteList("books/book_covers/", false);
		}
		return BookManager._cached_covers.GetRandom<Sprite>().name;
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000DD008 File Offset: 0x000DB208
	private BookTypeAsset getPossibleBookType(Actor pActor)
	{
		BookTypeAsset result;
		using (ListPool<BookTypeAsset> tPool = new ListPool<BookTypeAsset>(AssetManager.book_types.list.Count * 5))
		{
			for (int iIndex = 0; iIndex < AssetManager.book_types.list.Count; iIndex++)
			{
				BookTypeAsset tBookType = AssetManager.book_types.list[iIndex];
				if (tBookType.requirement_check == null || tBookType.requirement_check(pActor, tBookType))
				{
					int tRate = tBookType.writing_rate;
					if (tBookType.rate_calc != null)
					{
						tRate = tBookType.rate_calc(pActor, tBookType);
					}
					tRate = Mathf.Min(tRate, 10);
					for (int i = 0; i < tRate; i++)
					{
						tPool.Add(tBookType);
					}
				}
			}
			if (tPool.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tPool.GetRandom<BookTypeAsset>();
			}
		}
		return result;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000DD0E0 File Offset: 0x000DB2E0
	public Book newBook(Actor pActor)
	{
		BookTypeAsset tBookType = this.getPossibleBookType(pActor);
		if (tBookType == null)
		{
			return null;
		}
		Book book = base.newObject();
		ActorTrait tTraitActor = this.getBookTrait(pActor);
		Language language = pActor.language;
		LanguageTrait tTraitLanguage = (language != null) ? language.getTraitForBook() : null;
		Religion religion = pActor.religion;
		ReligionTrait tTraitReligion = (religion != null) ? religion.getTraitForBook() : null;
		Culture culture = pActor.culture;
		CultureTrait tTraitCulture = (culture != null) ? culture.getTraitForBook() : null;
		book.newBook(pActor, tBookType, tTraitActor, tTraitCulture, tTraitLanguage, tTraitReligion);
		return book;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000DD150 File Offset: 0x000DB350
	private ActorTrait getBookTrait(Actor pActor)
	{
		IReadOnlyCollection<ActorTrait> tTraits = pActor.getTraits();
		ActorTrait result;
		using (ListPool<ActorTrait> tList = new ListPool<ActorTrait>(tTraits.Count))
		{
			foreach (ActorTrait tTrait in tTraits)
			{
				if (tTrait.group_id == "mind")
				{
					tList.Add(tTrait);
				}
			}
			if (tList.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tList.GetRandom<ActorTrait>();
			}
		}
		return result;
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000DD1EC File Offset: 0x000DB3EC
	public void copyBook(Book pBook)
	{
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000DD1F0 File Offset: 0x000DB3F0
	public void burnBook(Book pBook)
	{
		Language language = pBook.getLanguage();
		if (language != null)
		{
			language.books.setDirty();
		}
		Culture culture = pBook.getCulture();
		if (culture != null)
		{
			culture.books.setDirty();
		}
		Religion religion = pBook.getReligion();
		if (religion != null)
		{
			religion.books.setDirty();
		}
		this.removeObject(pBook);
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000DD246 File Offset: 0x000DB446
	public override void removeObject(Book pObject)
	{
		World.world.game_stats.data.booksBurnt += 1L;
		World.world.map_stats.booksBurnt += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x040011F6 RID: 4598
	public const string COVER_PATH = "books/book_covers/";

	// Token: 0x040011F7 RID: 4599
	public const string ICON_PATH = "books/book_icons/";

	// Token: 0x040011F8 RID: 4600
	private static Sprite[] _cached_covers;
}
