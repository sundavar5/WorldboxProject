using System;
using System.Collections.Generic;

// Token: 0x02000140 RID: 320
public static class ConsonantSeparator
{
	// Token: 0x060009BA RID: 2490 RVA: 0x0008FEE4 File Offset: 0x0008E0E4
	public static void addRandomVowels(StringBuilderPool pString, string[] pPartsToInsert)
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
				if (ConsonantSeparator.isConsonant(pString[i - 1]) && ConsonantSeparator.isConsonant(pString[i]))
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

	// Token: 0x060009BB RID: 2491 RVA: 0x0008FFA4 File Offset: 0x0008E1A4
	public static ListPool<int> findAllConsonants(StringBuilderPool pString, int pStart, int pLength)
	{
		ListPool<int> tConsonants = new ListPool<int>(pLength);
		for (int i = pStart; i < pStart + pLength; i++)
		{
			if (ConsonantSeparator.isConsonant(pString[i]))
			{
				tConsonants.Add(i);
			}
		}
		return tConsonants;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0008FFDC File Offset: 0x0008E1DC
	public static ListPool<int> findAllSingleConsonants(StringBuilderPool pString, int pStart, int pLength)
	{
		ListPool<int> tConsonants = new ListPool<int>(pLength);
		for (int i = pStart; i < pStart + pLength; i++)
		{
			if (ConsonantSeparator.isConsonant(pString[i]) && (i <= 0 || !ConsonantSeparator.isConsonant(pString[i - 1])) && (i >= pString.Length - 1 || !ConsonantSeparator.isConsonant(pString[i + 1])))
			{
				tConsonants.Add(i);
			}
		}
		return tConsonants;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00090043 File Offset: 0x0008E243
	public static bool isConsonant(char pChar)
	{
		pChar = char.ToLowerInvariant(pChar);
		return ConsonantSeparator._consonants.Contains(pChar) || (char.IsLetter(pChar) && !VowelSeparator.isVowel(pChar));
	}

	// Token: 0x040009AB RID: 2475
	private static HashSet<char> _consonants = new HashSet<char>
	{
		'b',
		'c',
		'd',
		'f',
		'g',
		'h',
		'j',
		'k',
		'l',
		'm',
		'n',
		'p',
		'q',
		'r',
		's',
		't',
		'v',
		'w',
		'x',
		'z'
	};
}
