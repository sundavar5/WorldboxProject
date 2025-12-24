using System;

// Token: 0x02000289 RID: 649
public static class LanguageStructureHelpers
{
	// Token: 0x0400139E RID: 5022
	public const int AMOUNT_TYPES = 7;

	// Token: 0x0400139F RID: 5023
	public static readonly WordType[] word_types = new WordType[]
	{
		WordType.Name,
		WordType.Place,
		WordType.Concept,
		WordType.Action,
		WordType.Object,
		WordType.Creature,
		WordType.Pronoun
	};

	// Token: 0x040013A0 RID: 5024
	public static readonly string[] possible_word_patterns = new string[]
	{
		"S",
		"S",
		"S",
		"SE",
		"SE",
		"SE",
		"SE",
		"SE",
		"SE",
		"SME",
		"SME",
		"SMME"
	};

	// Token: 0x040013A1 RID: 5025
	public static readonly string[] possible_article_separators = new string[]
	{
		"",
		" ",
		" ",
		" ",
		" ",
		" ",
		" ",
		" ",
		" ",
		"-",
		"'",
		"’"
	};
}
