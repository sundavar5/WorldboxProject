using System;
using System.Collections.Generic;

// Token: 0x02000149 RID: 329
public static class VowelSeparator
{
	// Token: 0x060009F6 RID: 2550 RVA: 0x00092CB0 File Offset: 0x00090EB0
	public static void addRandomConsonants(StringBuilderPool pString, string[] pPartsToInsert)
	{
		if (pString.Length < 2)
		{
			return;
		}
		pString.ToLowerInvariant();
		int tLastWord = pString.LastIndexOfAny(new char[]
		{
			' ',
			','
		}) + 2;
		using (ListPool<int> tPossibleLocations = new ListPool<int>(pString.Length))
		{
			for (int i = tLastWord; i < pString.Length; i++)
			{
				if (VowelSeparator.isVowel(pString[i - 1]) && VowelSeparator.isVowel(pString[i]))
				{
					tPossibleLocations.Add(i);
				}
			}
			if (tPossibleLocations.Count != 0)
			{
				int tRandomLocation = OnomasticsLibrary.GetRandom<int>(tPossibleLocations);
				string tNewPartToInsert = OnomasticsLibrary.GetRandom<string>(pPartsToInsert);
				pString.Insert(tRandomLocation, tNewPartToInsert);
			}
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00092D70 File Offset: 0x00090F70
	public static ListPool<int> findAllVowels(StringBuilderPool pString, int pStart, int pLength)
	{
		ListPool<int> tVowels = new ListPool<int>(pLength);
		for (int i = pStart; i < pStart + pLength; i++)
		{
			if (VowelSeparator.isVowel(pString[i]))
			{
				tVowels.Add(i);
			}
		}
		return tVowels;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00092DA8 File Offset: 0x00090FA8
	public static ListPool<int> findAllSingleVowels(StringBuilderPool pString, int pStart, int pLength)
	{
		pString.ToLowerInvariant();
		ListPool<int> tVowels = new ListPool<int>(pLength);
		for (int i = pStart; i < pStart + pLength; i++)
		{
			if (VowelSeparator.isVowel(pString[i]) && (i <= 0 || !VowelSeparator.isVowel(pString[i - 1])) && (i >= pString.Length - 1 || !VowelSeparator.isVowel(pString[i + 1])))
			{
				tVowels.Add(i);
			}
		}
		return tVowels;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00092E16 File Offset: 0x00091016
	public static bool isVowel(char pChar)
	{
		pChar = char.ToLowerInvariant(pChar);
		return VowelSeparator._vowels.Contains(pChar) || (char.IsLetter(pChar) && VowelSeparator._special_vowels.Contains(pChar));
	}

	// Token: 0x040009CF RID: 2511
	private const string VOWELS = "aeiouy";

	// Token: 0x040009D0 RID: 2512
	private const string VOWELS_SPECIAL = "àáâãäåæèéêëìíîïòóôõöøùúûüýÿāăąēĕėęěĩīĭįĳōŏőœũūŭůűųŷǎǐǒǔǖǘǚǜǟǡǣǫǭǻǽǿȁȃȅȇȉȋȍȏȕȗȧȩȫȭȯȱȳеийоуыэюяѐёєіїѝў";

	// Token: 0x040009D1 RID: 2513
	private static HashSet<char> _vowels = new HashSet<char>("aeiouy".ToCharArray());

	// Token: 0x040009D2 RID: 2514
	private static HashSet<char> _special_vowels = new HashSet<char>("àáâãäåæèéêëìíîïòóôõöøùúûüýÿāăąēĕėęěĩīĭįĳōŏőœũūŭůűųŷǎǐǒǔǖǘǚǜǟǡǣǫǭǻǽǿȁȃȅȇȉȋȍȏȕȗȧȩȫȭȯȱȳеийоуыэюяѐёєіїѝў".ToCharArray());
}
