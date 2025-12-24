using System;
using System.Text.RegularExpressions;

// Token: 0x02000290 RID: 656
[Serializable]
public class WordAsset : Asset
{
	// Token: 0x0600191A RID: 6426 RVA: 0x000EE278 File Offset: 0x000EC478
	public string getLocaleID()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000EE27F File Offset: 0x000EC47F
	public string getDescriptionID()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000EE286 File Offset: 0x000EC486
	public string getDescriptionID2()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000EE290 File Offset: 0x000EC490
	public string getWordInLanguage(LanguageStructure pStructure, LinguisticsAsset pLinguisticsAsset, int pSeed)
	{
		string text;
		using (StringBuilderPool tSB = new StringBuilderPool())
		{
			foreach (char tChar in this.getWordPattern(pStructure, pSeed))
			{
				string text2;
				if (tChar != 'E')
				{
					if (tChar != 'M')
					{
						if (tChar == 'S')
						{
							text2 = pStructure.syllables_start.GetRandom<string>();
						}
						else
						{
							text2 = "";
						}
					}
					else
					{
						text2 = pStructure.syllables_mid.GetRandom<string>();
					}
				}
				else
				{
					text2 = pStructure.syllables_ends.GetRandom<string>();
				}
				string tSyllableType = text2;
				tSB.Append(tSyllableType);
			}
			string tWord = tSB.ToString();
			if (pLinguisticsAsset.word_type != WordType.None)
			{
				int tWordType = (int)pLinguisticsAsset.word_type;
				PrefixesSettings tPrefixes = pStructure.settings_prefixes;
				SuffixesSettings tSuffixes = pStructure.settings_suffixes;
				if (tPrefixes.enabled[tWordType])
				{
					tWord = tPrefixes.sets[tWordType].GetRandom<string>() + tPrefixes.separator[tWordType] + tWord;
				}
				if (tSuffixes.enabled[tWordType])
				{
					tWord = tWord + tSuffixes.separator[tWordType] + tSuffixes.sets[tWordType].GetRandom<string>();
				}
			}
			text = tWord;
		}
		return text;
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000EE3B8 File Offset: 0x000EC5B8
	private string getWordPattern(LanguageStructure pStructure, int pSeed)
	{
		return this.selectWeightedPattern(pStructure.word_patterns, pStructure.word_weights);
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x000EE3CC File Offset: 0x000EC5CC
	private string selectWeightedPattern(string[] pPattern, float[] pWeight)
	{
		float tRoll = Randy.random();
		float tCumulative = 0f;
		for (int i = 0; i < pPattern.Length; i++)
		{
			tCumulative += pWeight[i];
			if (tRoll < tCumulative)
			{
				return pPattern[i];
			}
		}
		return pPattern.Last<string>();
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x000EE408 File Offset: 0x000EC608
	private string fixWordBoundaries(string pWord)
	{
		if (string.IsNullOrEmpty(pWord))
		{
			return pWord;
		}
		return Regex.Replace(Regex.Replace(Regex.Replace(pWord, "([bcdfghjklmnpqrstvwxyz])\\1{2,}", "$1$1"), "([aeiou])\\1+", delegate(Match m)
		{
			string doubled = m.Value;
			if (doubled == "ee" || doubled == "oo" || doubled == "aa")
			{
				return doubled.Substring(0, 2);
			}
			return m.Groups[1].Value;
		}), "([bdgkpt])([bdgkpt])", "$1").Replace("tst", "st").Replace("ndn", "nd").Replace("ckc", "ck");
	}
}
