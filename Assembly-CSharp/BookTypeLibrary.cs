using System;

// Token: 0x020000B6 RID: 182
public class BookTypeLibrary : AssetLibrary<BookTypeAsset>
{
	// Token: 0x060005C8 RID: 1480 RVA: 0x00054A24 File Offset: 0x00052C24
	public override void init()
	{
		base.init();
		BookTypeAsset bookTypeAsset = new BookTypeAsset();
		bookTypeAsset.id = "family_story";
		bookTypeAsset.name_template = "book_name_love_story";
		bookTypeAsset.color_text = "#FFA94D";
		bookTypeAsset.writing_rate = 3;
		bookTypeAsset.path_icons = "family_story/";
		bookTypeAsset.requirement_check = ((Actor a, BookTypeAsset _) => a.hasFamily());
		this.add(bookTypeAsset);
		this.t.base_stats["mana"] = 10f;
		this.t.base_stats["happiness"] = 5f;
		this.t.base_stats["experience"] = 5f;
		BookTypeAsset bookTypeAsset2 = new BookTypeAsset();
		bookTypeAsset2.id = "love_story";
		bookTypeAsset2.name_template = "book_name_love_story";
		bookTypeAsset2.color_text = "#FF6B6B";
		bookTypeAsset2.writing_rate = 3;
		bookTypeAsset2.path_icons = "love_story/";
		bookTypeAsset2.requirement_check = ((Actor a, BookTypeAsset _) => a.hasFamily());
		this.add(bookTypeAsset2);
		this.t.base_stats["mana"] = 10f;
		this.t.base_stats["happiness"] = 10f;
		this.t.base_stats["experience"] = 5f;
		BookTypeAsset bookTypeAsset3 = new BookTypeAsset();
		bookTypeAsset3.id = "friendship_story";
		bookTypeAsset3.name_template = "book_name_love_story";
		bookTypeAsset3.color_text = "#74C0FC";
		bookTypeAsset3.writing_rate = 3;
		bookTypeAsset3.path_icons = "friendship_story/";
		bookTypeAsset3.requirement_check = ((Actor a, BookTypeAsset _) => a.hasFamily());
		this.add(bookTypeAsset3);
		this.t.base_stats["mana"] = 10f;
		this.t.base_stats["happiness"] = 5f;
		this.t.base_stats["experience"] = 5f;
		BookTypeAsset bookTypeAsset4 = new BookTypeAsset();
		bookTypeAsset4.id = "bad_story_about_king";
		bookTypeAsset4.name_template = "book_name_bad_story";
		bookTypeAsset4.color_text = "#FFD700";
		bookTypeAsset4.writing_rate = 3;
		bookTypeAsset4.path_icons = "bad_story/";
		bookTypeAsset4.requirement_check = delegate(Actor a, BookTypeAsset _)
		{
			if (!a.kingdom.hasKing())
			{
				return false;
			}
			if (a.isKing())
			{
				return false;
			}
			Actor tKing = a.kingdom.king;
			return tKing.hasFamily() && tKing.hasClan();
		};
		this.add(bookTypeAsset4);
		this.t.base_stats["happiness"] = -10f;
		this.t.base_stats["experience"] = 10f;
		BookTypeAsset bookTypeAsset5 = new BookTypeAsset();
		bookTypeAsset5.id = "fable";
		bookTypeAsset5.name_template = "book_name_fable";
		bookTypeAsset5.color_text = "#9ACD32";
		bookTypeAsset5.writing_rate = 3;
		bookTypeAsset5.path_icons = "fable/";
		bookTypeAsset5.requirement_check = ((Actor a, BookTypeAsset _) => a.hasWeapon() && a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader());
		this.add(bookTypeAsset5);
		this.t.base_stats["mana"] = 20f;
		this.t.base_stats["happiness"] = 10f;
		this.t.base_stats["experience"] = 30f;
		BookTypeAsset bookTypeAsset6 = new BookTypeAsset();
		bookTypeAsset6.id = "warfare_manual";
		bookTypeAsset6.name_template = "book_name_warfare_manual";
		bookTypeAsset6.color_text = "#E8590C";
		bookTypeAsset6.path_icons = "warfare_manual/";
		bookTypeAsset6.rate_calc = ((Actor a, BookTypeAsset _) => (int)a.stats["warfare"]);
		bookTypeAsset6.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader() && a.city.countWarriors() != 0);
		this.add(bookTypeAsset6);
		this.t.base_stats["warfare"] = 1f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 5f;
		BookTypeAsset bookTypeAsset7 = new BookTypeAsset();
		bookTypeAsset7.id = "economy_manual";
		bookTypeAsset7.name_template = "book_name_economy_manual";
		bookTypeAsset7.path_icons = "economy_manual/";
		bookTypeAsset7.color_text = "#EAC645";
		bookTypeAsset7.rate_calc = ((Actor a, BookTypeAsset b) => (int)a.stats["stewardship"]);
		bookTypeAsset7.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader());
		this.add(bookTypeAsset7);
		this.t.base_stats["happiness"] = -10f;
		this.t.base_stats["stewardship"] = 1f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 10f;
		BookTypeAsset bookTypeAsset8 = new BookTypeAsset();
		bookTypeAsset8.id = "stewardship_manual";
		bookTypeAsset8.name_template = "book_name_stewardship_manual";
		bookTypeAsset8.color_text = "#D4A373";
		bookTypeAsset8.path_icons = "stewardship_manual/";
		bookTypeAsset8.rate_calc = ((Actor a, BookTypeAsset b) => (int)a.stats["stewardship"]);
		bookTypeAsset8.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader());
		this.add(bookTypeAsset8);
		this.t.base_stats["happiness"] = -5f;
		this.t.base_stats["stewardship"] = 1f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 10f;
		BookTypeAsset bookTypeAsset9 = new BookTypeAsset();
		bookTypeAsset9.id = "diplomacy_manual";
		bookTypeAsset9.name_template = "book_name_diplomacy_manual";
		bookTypeAsset9.color_text = "#66D9E8";
		bookTypeAsset9.path_icons = "diplomacy_manual/";
		bookTypeAsset9.rate_calc = ((Actor a, BookTypeAsset _) => (int)a.stats["diplomacy"]);
		bookTypeAsset9.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader());
		this.add(bookTypeAsset9);
		this.t.base_stats["happiness"] = -10f;
		this.t.base_stats["diplomacy"] = 1f;
		this.t.base_stats["experience"] = 10f;
		BookTypeAsset bookTypeAsset10 = new BookTypeAsset();
		bookTypeAsset10.id = "mathbook";
		bookTypeAsset10.name_template = "book_name_math";
		bookTypeAsset10.color_text = "#3BC9DB";
		bookTypeAsset10.path_icons = "mathbook/";
		bookTypeAsset10.rate_calc = ((Actor a, BookTypeAsset _) => (int)a.stats["intelligence"]);
		bookTypeAsset10.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader() && a.hasCulture());
		this.add(bookTypeAsset10);
		this.t.base_stats["happiness"] = -20f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 30f;
		BookTypeAsset bookTypeAsset11 = new BookTypeAsset();
		bookTypeAsset11.id = "biology_book";
		bookTypeAsset11.name_template = "book_name_biology";
		bookTypeAsset11.path_icons = "biology_book/";
		bookTypeAsset11.color_text = "#80C980";
		bookTypeAsset11.rate_calc = ((Actor a, BookTypeAsset _) => (int)a.stats["intelligence"]);
		bookTypeAsset11.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader() && a.hasSubspecies());
		this.add(bookTypeAsset11);
		this.t.base_stats["happiness"] = 10f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 30f;
		BookTypeAsset bookTypeAsset12 = new BookTypeAsset();
		bookTypeAsset12.id = "history_book";
		bookTypeAsset12.name_template = "book_name_history";
		bookTypeAsset12.path_icons = "history_book/";
		bookTypeAsset12.color_text = "#C49A6C";
		bookTypeAsset12.rate_calc = ((Actor a, BookTypeAsset _) => (int)a.stats["intelligence"]);
		bookTypeAsset12.requirement_check = ((Actor a, BookTypeAsset _) => a.kingdom.hasKing() && a.kingdom.king.hasClan() && a.hasCity() && a.city.hasLeader() && a.hasSubspecies());
		this.add(bookTypeAsset12);
		this.t.base_stats["happiness"] = 5f;
		this.t.base_stats["warfare"] = 1f;
		this.t.base_stats["diplomacy"] = 1f;
		this.t.base_stats["intelligence"] = 1f;
		this.t.base_stats["experience"] = 30f;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x000553B0 File Offset: 0x000535B0
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		foreach (BookTypeAsset tAsset in this.list)
		{
			base.checkSpriteExists("path_icons", tAsset.getFullIconPath(), tAsset);
		}
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00055418 File Offset: 0x00053618
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (BookTypeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getDescriptionID());
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
