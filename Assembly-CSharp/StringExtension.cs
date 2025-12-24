using System;

// Token: 0x0200056D RID: 1389
public static class StringExtension
{
	// Token: 0x06002D3B RID: 11579 RVA: 0x00160CB8 File Offset: 0x0015EEB8
	public static int[] AllIndexesOf(this string pString, string pValue)
	{
		int tIndex = 0;
		int tLength = pValue.Length;
		int tCount = 0;
		while (tIndex < pString.Length)
		{
			int tFoundIndex = pString.IndexOf(pValue, tIndex, StringComparison.Ordinal);
			if (tFoundIndex == -1)
			{
				break;
			}
			tCount++;
			tIndex = tFoundIndex + tLength;
		}
		int[] tResult = new int[tCount];
		tIndex = 0;
		tCount = 0;
		while (tIndex < pString.Length)
		{
			int tFoundIndex2 = pString.IndexOf(pValue, tIndex, StringComparison.Ordinal);
			if (tFoundIndex2 == -1)
			{
				break;
			}
			tResult[tCount] = tFoundIndex2;
			tCount++;
			tIndex = tFoundIndex2 + tLength;
		}
		return tResult;
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x00160D29 File Offset: 0x0015EF29
	public static char Last(this string pString)
	{
		return pString[pString.Length - 1];
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x00160D39 File Offset: 0x0015EF39
	public static char First(this string pString)
	{
		return pString[0];
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x00160D42 File Offset: 0x0015EF42
	public static string Reverse(this string pString)
	{
		return string.Create<string>(pString.Length, pString, delegate(Span<char> pChars, string pState)
		{
			pState.AsSpan().CopyTo(pChars);
			pChars.Reverse<char>();
		});
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x00160D70 File Offset: 0x0015EF70
	public static string Shuffle(this string pString)
	{
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			using (ListPool<char> tList = new ListPool<char>())
			{
				for (int i = 0; i < pString.Length; i++)
				{
					tList.Add(pString[i]);
				}
				tList.Shuffle<char>();
				for (int j = 0; j < tList.Count; j++)
				{
					char tChar = tList[j];
					tBuilder.Append(tChar);
				}
				string tResult = tBuilder.ToString();
				tResult = char.ToUpper(tResult[0]).ToString() + tResult.Substring(1).ToLower();
				result = tResult;
			}
		}
		return result;
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x00160E3C File Offset: 0x0015F03C
	public static string FirstToUpper(this string pString)
	{
		if (pString.Length == 0)
		{
			return pString;
		}
		string str = pString.Substring(0, 1).ToUpper();
		pString = pString.Substring(1, pString.Length - 1);
		return str + pString;
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x00160E6C File Offset: 0x0015F06C
	public static string ColorHex(this string pString, string pColorHex, bool pLocalize = false)
	{
		return Toolbox.coloredText(pString, pColorHex, pLocalize);
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x00160E76 File Offset: 0x0015F076
	public static string blue(this string pString)
	{
		if (!string.IsNullOrEmpty(pString))
		{
			return pString.ColorHex("#4CCFFF", false);
		}
		return "";
	}

	// Token: 0x06002D43 RID: 11587 RVA: 0x00160E92 File Offset: 0x0015F092
	public static string blue(this object pString)
	{
		if (pString == null)
		{
			return null;
		}
		return pString.ToString().blue();
	}

	// Token: 0x06002D44 RID: 11588 RVA: 0x00160EA4 File Offset: 0x0015F0A4
	public static string red(this string pString)
	{
		if (!string.IsNullOrEmpty(pString))
		{
			return pString.ColorHex("#FF637D", false);
		}
		return "";
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x00160EC0 File Offset: 0x0015F0C0
	public static string red(this object pString)
	{
		if (pString == null)
		{
			return null;
		}
		return pString.ToString().red();
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x00160ED2 File Offset: 0x0015F0D2
	public static string teal(this string pString)
	{
		if (!string.IsNullOrEmpty(pString))
		{
			return pString.ColorHex("#23F3FF", false);
		}
		return "";
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x00160EEE File Offset: 0x0015F0EE
	public static string teal(this object pString)
	{
		if (pString == null)
		{
			return null;
		}
		return pString.ToString().teal();
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x00160F00 File Offset: 0x0015F100
	public static string yellow(this string pString)
	{
		if (!string.IsNullOrEmpty(pString))
		{
			return pString.ColorHex("#FFFF51", false);
		}
		return "";
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x00160F1C File Offset: 0x0015F11C
	public static string yellow(this object pString)
	{
		if (pString == null)
		{
			return null;
		}
		return pString.ToString().yellow();
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x00160F2E File Offset: 0x0015F12E
	public static string Localize(this string pString)
	{
		return LocalizedTextManager.getText(pString.Underscore(), null, false);
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x00160F3D File Offset: 0x0015F13D
	public static string Description(this string pString)
	{
		return pString + "_description";
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x00160F4C File Offset: 0x0015F14C
	public static bool EndsWithAny(this string pString, string[] pTrimString)
	{
		foreach (string tTrimString in pTrimString)
		{
			if (pString.EndsWith(tTrimString))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x00160F79 File Offset: 0x0015F179
	public static string TrimEnd(this string pString, string pTrimString)
	{
		if (pString.EndsWith(pTrimString))
		{
			return pString.Substring(0, pString.Length - pTrimString.Length);
		}
		return pString;
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x00160F9C File Offset: 0x0015F19C
	public unsafe static bool HasUpperCase(this string pString)
	{
		ReadOnlySpan<char> tFastString = pString.AsSpan();
		for (int i = 0; i < tFastString.Length; i++)
		{
			if (char.IsUpper((char)(*tFastString[i])))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x00160FD8 File Offset: 0x0015F1D8
	public unsafe static bool ShouldUnderscore(this string pString)
	{
		ReadOnlySpan<char> tFastString = pString.AsSpan();
		for (int i = 0; i < tFastString.Length; i++)
		{
			if (!char.IsLetterOrDigit((char)(*tFastString[i])) && *tFastString[i] != 95)
			{
				return true;
			}
			if (char.IsWhiteSpace((char)(*tFastString[i])) || char.IsUpper((char)(*tFastString[i])))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x00161040 File Offset: 0x0015F240
	public static string Truncate(this string pString, int pMaxLength)
	{
		if (string.IsNullOrEmpty(pString) || pString.Length <= pMaxLength)
		{
			return pString;
		}
		return pString.Substring(0, pMaxLength);
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x00161060 File Offset: 0x0015F260
	public unsafe static string Underscore(this string pString)
	{
		if (string.IsNullOrEmpty(pString))
		{
			return pString;
		}
		if (!pString.ShouldUnderscore())
		{
			return pString;
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			ReadOnlySpan<char> tFastString = pString.AsSpan();
			bool tLastWasUnderscore = false;
			bool tLastWasCaps = false;
			bool tHasLowerCase = false;
			for (int i = 0; i < tFastString.Length; i++)
			{
				if (char.IsLower((char)(*tFastString[i])))
				{
					tHasLowerCase = true;
					break;
				}
			}
			for (int j = 0; j < tFastString.Length; j++)
			{
				if (char.IsLetter((char)(*tFastString[j])))
				{
					if (char.IsUpper((char)(*tFastString[j])))
					{
						if (j > 0 && !tLastWasUnderscore && (!tLastWasCaps || tHasLowerCase))
						{
							tBuilder.Append('_');
						}
						tBuilder.Append(char.ToLower((char)(*tFastString[j])));
						tLastWasCaps = true;
					}
					else
					{
						tBuilder.Append((char)(*tFastString[j]));
						tLastWasCaps = false;
					}
					tLastWasUnderscore = false;
				}
				else if (char.IsDigit((char)(*tFastString[j])))
				{
					tBuilder.Append((char)(*tFastString[j]));
					tLastWasUnderscore = false;
					tLastWasCaps = false;
				}
				else if (!tLastWasUnderscore)
				{
					tBuilder.Append('_');
					tLastWasUnderscore = true;
					tLastWasCaps = false;
				}
			}
			if (tLastWasUnderscore)
			{
				tBuilder.Remove(tBuilder.Length - 1, 1);
			}
			result = tBuilder.ToString();
		}
		return result;
	}
}
