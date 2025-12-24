using System;

// Token: 0x02000296 RID: 662
[Serializable]
public class SuffixesSettings : StructureSettings
{
	// Token: 0x0600192E RID: 6446 RVA: 0x000EEAAC File Offset: 0x000ECCAC
	public override void create(LanguageStructure pStructure, int pSizeMin, int pSizeMax)
	{
		foreach (WordType tType in LanguageStructureHelpers.word_types)
		{
			this.generate(pStructure, tType, pSizeMin, pSizeMax);
		}
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x000EEADC File Offset: 0x000ECCDC
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

	// Token: 0x06001930 RID: 6448 RVA: 0x000EEB2C File Offset: 0x000ECD2C
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
