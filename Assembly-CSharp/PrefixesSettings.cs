using System;

// Token: 0x02000294 RID: 660
[Serializable]
public class PrefixesSettings : StructureSettings
{
	// Token: 0x06001928 RID: 6440 RVA: 0x000EE938 File Offset: 0x000ECB38
	public override void create(LanguageStructure pStructure, int pSizeMin, int pSizeMax)
	{
		foreach (WordType tType in LanguageStructureHelpers.word_types)
		{
			this.generate(pStructure, tType, pSizeMin, pSizeMax);
		}
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x000EE968 File Offset: 0x000ECB68
	public void generate(LanguageStructure pStructure, WordType pWord, int pSizeMin, int pSizeMax)
	{
		int tSize = Randy.randomInt(pSizeMin, pSizeMax);
		bool tEnabled = tSize != 0 && Randy.randomBool();
		this.enabled[(int)pWord] = tEnabled;
		if (tEnabled)
		{
			this.sets[(int)pWord] = this.generateSets(pStructure, tSize);
			this.separator[(int)pWord] = "";
		}
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x000EE9B8 File Offset: 0x000ECBB8
	private string[] generateSets(LanguageStructure pStructure, int pAmount)
	{
		string[] tResultArticles = new string[pAmount];
		for (int i = 0; i < pAmount; i++)
		{
			string tWord;
			switch (Randy.randomInt(0, 5))
			{
			case 0:
				tWord = pStructure.sets_consonants.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			case 1:
				tWord = pStructure.sets_onset_2.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			case 2:
				tWord = pStructure.sets_consonants.GetRandom<string>() + pStructure.sets_vowels.GetRandom<string>();
				break;
			default:
				tWord = pStructure.sets_vowels.GetRandom<string>() + pStructure.sets_consonants.GetRandom<string>();
				break;
			}
			tResultArticles[i] = tWord;
		}
		return tResultArticles;
	}
}
