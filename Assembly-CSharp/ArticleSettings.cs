using System;

// Token: 0x02000293 RID: 659
public class ArticleSettings : StructureSettings
{
	// Token: 0x06001924 RID: 6436 RVA: 0x000EE7C0 File Offset: 0x000EC9C0
	public override void create(LanguageStructure pStructure, int pSizeMin, int pSizeMax)
	{
		foreach (WordType tType in LanguageStructureHelpers.word_types)
		{
			this.generate(pStructure, tType, pSizeMin, pSizeMax);
		}
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x000EE7F0 File Offset: 0x000EC9F0
	public void generate(LanguageStructure pStructure, WordType pWord, int pSizeMin, int pSizeMax)
	{
		bool tEnabled = Randy.randomBool();
		this.enabled[(int)pWord] = tEnabled;
		if (tEnabled)
		{
			this.sets[(int)pWord] = this.generateSets(pStructure, Randy.randomInt(pSizeMin, pSizeMax));
			this.separator[(int)pWord] = LanguageStructureHelpers.possible_article_separators.GetRandom<string>();
		}
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x000EE83C File Offset: 0x000ECA3C
	private string[] generateSets(LanguageStructure pStructure, int pAmount)
	{
		string[] tResultArticles = new string[pAmount];
		for (int i = 0; i < pAmount; i++)
		{
			string tWord;
			switch (Randy.randomInt(0, 5))
			{
			case 0:
				tWord = pStructure.sets_consonants.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>() + pStructure.sets_consonants.GetRandom<string>();
				break;
			case 1:
				tWord = pStructure.sets_onset_2.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			case 2:
				tWord = pStructure.sets_consonants.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			case 3:
				tWord = pStructure.sets_vowels.GetRandom<string>() + pStructure.sets_consonants.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			default:
				tWord = (pStructure.sets_vowels.GetRandom<string>() ?? "");
				break;
			}
			tResultArticles[i] = tWord;
		}
		return tResultArticles;
	}
}
