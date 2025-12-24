using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public static class BlacklistTools
{
	// Token: 0x060005A8 RID: 1448 RVA: 0x000540E4 File Offset: 0x000522E4
	public static string[] getProfanities()
	{
		if (BlacklistTools._profanities != null)
		{
			return BlacklistTools._profanities;
		}
		TextAsset textAsset = Resources.Load("blacklisted_names") as TextAsset;
		string tContent = textAsset.text;
		Resources.UnloadAsset(textAsset);
		string[] tProfanityArr = Regex.Split(tContent, "\r\n?|\n", RegexOptions.Singleline);
		string[] profanities;
		using (ListPool<string> tListPool = new ListPool<string>(tProfanityArr.Length))
		{
			for (int i = 0; i < tProfanityArr.Length; i++)
			{
				string lString = tProfanityArr[i].Trim().ToLower();
				if (lString.Length != 0)
				{
					tListPool.Add(lString);
				}
			}
			BlacklistTools._profanities = tListPool.ToArray<string>();
			profanities = BlacklistTools._profanities;
		}
		return profanities;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00054190 File Offset: 0x00052390
	public static void loadProfanityFilter(Dictionary<char, string[]> pProfanity, HashSet<char> pUnique)
	{
		if (pProfanity != null && pProfanity.Count > 0)
		{
			return;
		}
		try
		{
			Dictionary<char, List<string>> tProfanity = new Dictionary<char, List<string>>();
			foreach (string tString in BlacklistTools.getProfanities())
			{
				pUnique.Clear();
				pUnique.UnionWith(tString);
				pUnique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
				foreach (char tChar in pUnique)
				{
					if (!tProfanity.ContainsKey(tChar))
					{
						tProfanity[tChar] = new List<string>();
					}
					tProfanity[tChar].Add(tString);
				}
			}
			foreach (char tChar2 in tProfanity.Keys)
			{
				pProfanity[tChar2] = tProfanity[tChar2].ToArray();
			}
		}
		catch (Exception message)
		{
			Debug.Log("Error when loading blacklist");
			Debug.LogError(message);
		}
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x000542D4 File Offset: 0x000524D4
	public static void loadProfanityFilter(Dictionary<char, char[][]> pProfanity, HashSet<char> pUnique)
	{
		if (pProfanity != null && pProfanity.Count > 0)
		{
			return;
		}
		try
		{
			Dictionary<char, List<char[]>> tProfanity = new Dictionary<char, List<char[]>>();
			foreach (string tString in BlacklistTools.getProfanities())
			{
				pUnique.Clear();
				pUnique.UnionWith(tString);
				pUnique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
				char[] tCharArray = tString.ToCharArray();
				foreach (char tChar in pUnique)
				{
					if (!tProfanity.ContainsKey(tChar))
					{
						tProfanity[tChar] = new List<char[]>();
					}
					tProfanity[tChar].Add(tCharArray);
				}
			}
			foreach (char tChar2 in tProfanity.Keys)
			{
				pProfanity[tChar2] = tProfanity[tChar2].ToArray();
			}
		}
		catch (Exception message)
		{
			Debug.Log("Error when loading blacklist");
			Debug.LogError(message);
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00054424 File Offset: 0x00052624
	public static void loadProfanityFilter(Dictionary<int, HashSet<int>> pProfanity, ref int pMinLength, ref int pMaxLength)
	{
		if (pProfanity != null && pProfanity.Count > 0)
		{
			return;
		}
		try
		{
			foreach (string tString in BlacklistTools.getProfanities())
			{
				if (tString.Length < pMinLength)
				{
					pMinLength = tString.Length;
				}
				if (tString.Length > pMaxLength)
				{
					pMaxLength = tString.Length;
				}
				if (!pProfanity.ContainsKey(tString.Length))
				{
					pProfanity.Add(tString.Length, new HashSet<int>());
				}
				if (!pProfanity[tString.Length].Add(BlacklistTools.getCharHashCode(tString.ToCharArray())))
				{
					Debug.Log("Duplicate profanity: " + tString);
				}
			}
		}
		catch (Exception message)
		{
			Debug.Log("Error when loading blacklist");
			Debug.LogError(message);
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000544EC File Offset: 0x000526EC
	public static void loadProfanityFilter(Dictionary<string, string[]> pProfanity, int pIndexLength = 3)
	{
		if (pProfanity != null && pProfanity.Count > 0)
		{
			return;
		}
		try
		{
			Dictionary<string, HashSet<string>> tProfanity = new Dictionary<string, HashSet<string>>();
			foreach (string tString in BlacklistTools.getProfanities())
			{
				string tID = tString.Substring(0, pIndexLength);
				if (!tProfanity.ContainsKey(tID))
				{
					tProfanity.Add(tID, new HashSet<string>());
				}
				if (!tProfanity[tID].Add(tString))
				{
					Debug.Log("Duplicate profanity: " + tString);
				}
			}
			foreach (KeyValuePair<string, HashSet<string>> tPair in tProfanity)
			{
				pProfanity.Add(tPair.Key, tPair.Value.ToArray<string>());
			}
		}
		catch (Exception message)
		{
			Debug.Log("Error when loading blacklist");
			Debug.LogError(message);
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x000545DC File Offset: 0x000527DC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getCharHashCode(char[] pChar)
	{
		return ((IStructuralEquatable)pChar).GetHashCode(EqualityComparer<char>.Default);
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000545EC File Offset: 0x000527EC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string cleanString(string pString)
	{
		if (string.IsNullOrEmpty(pString))
		{
			return pString;
		}
		string tString = pString[0].ToString();
		for (int i = 0; i < pString.Length - 1; i++)
		{
			if (!pString[i].Equals(pString[i + 1]))
			{
				tString += pString[i + 1].ToString();
			}
		}
		return tString;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0005465C File Offset: 0x0005285C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static string cleanStringAsSpan(string pString)
	{
		if (string.IsNullOrEmpty(pString))
		{
			return pString;
		}
		ReadOnlySpan<char> tSpan = pString.AsSpan();
		int length2 = tSpan.Length;
		Span<char> buffer = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)length2) * 2)], length2);
		int length = 0;
		*buffer[length++] = (char)(*tSpan[0]);
		for (int i = 1; i < tSpan.Length; i++)
		{
			if (*tSpan[i] != *tSpan[i - 1])
			{
				*buffer[length++] = (char)(*tSpan[i]);
			}
		}
		return new string(buffer.Slice(0, length));
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00054700 File Offset: 0x00052900
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static ReadOnlySpan<char> cleanSpan(ReadOnlySpan<char> pSpan)
	{
		if (pSpan.Length == 0)
		{
			return pSpan;
		}
		Span<char> buffer = new char[pSpan.Length];
		int length = 0;
		*buffer[length++] = (char)(*pSpan[0]);
		for (int i = 1; i < pSpan.Length; i++)
		{
			if (*pSpan[i] != *pSpan[i - 1])
			{
				*buffer[length++] = (char)(*pSpan[i]);
			}
		}
		return buffer.Slice(0, length);
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00054790 File Offset: 0x00052990
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool contains(ReadOnlySpan<char> pText, ReadOnlySpan<char> pSearchPattern)
	{
		if (pSearchPattern.Length == 0)
		{
			return true;
		}
		if (pSearchPattern.Length > pText.Length)
		{
			return false;
		}
		char tFirstChar = (char)(*pSearchPattern[0]);
		for (int i = 0; i <= pText.Length - pSearchPattern.Length; i++)
		{
			if (*pText[i] == (ushort)tFirstChar)
			{
				bool tMatch = true;
				for (int j = 1; j < pSearchPattern.Length; j++)
				{
					if (*pText[i + j] != *pSearchPattern[j])
					{
						tMatch = false;
						break;
					}
				}
				if (tMatch)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00054820 File Offset: 0x00052A20
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool contains(ReadOnlySpan<char> pText, ReadOnlySpan<char> pSearchPattern, int pStartIndex)
	{
		if (pSearchPattern.Length == 0)
		{
			return true;
		}
		if (pSearchPattern.Length > pText.Length)
		{
			return false;
		}
		char tFirstChar = (char)(*pSearchPattern[0]);
		for (int i = pStartIndex; i <= pText.Length - pSearchPattern.Length; i++)
		{
			if (*pText[i] == (ushort)tFirstChar)
			{
				bool tMatch = true;
				for (int j = 1; j < pSearchPattern.Length; j++)
				{
					if (*pText[i + j] != *pSearchPattern[j])
					{
						tMatch = false;
						break;
					}
				}
				if (tMatch)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x000548B0 File Offset: 0x00052AB0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool contains(ref ReadOnlySpan<char> pText, ref ReadOnlySpan<char> pSearchPattern)
	{
		if (pSearchPattern.Length == 0)
		{
			return true;
		}
		if (pSearchPattern.Length > pText.Length)
		{
			return false;
		}
		char tFirstChar = (char)(*pSearchPattern[0]);
		for (int i = 0; i <= pText.Length - pSearchPattern.Length; i++)
		{
			if (*pText[i] == (ushort)tFirstChar)
			{
				bool tMatch = true;
				for (int j = 1; j < pSearchPattern.Length; j++)
				{
					if (*pText[i + j] != *pSearchPattern[j])
					{
						tMatch = false;
						break;
					}
				}
				if (tMatch)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00054934 File Offset: 0x00052B34
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool contains2(ref ReadOnlySpan<char> pText, ref ReadOnlySpan<char> pSearchPattern)
	{
		int tSearchLength = pSearchPattern.Length;
		if (tSearchLength == 0)
		{
			return true;
		}
		int tTextLength = pText.Length;
		if (tSearchLength > tTextLength)
		{
			return false;
		}
		int i = 0;
		int length = tTextLength - tSearchLength;
		while (i <= length)
		{
			if (pText.Slice(i, tSearchLength).SequenceEqual(pSearchPattern))
			{
				return true;
			}
			i++;
		}
		return false;
	}

	// Token: 0x040005D6 RID: 1494
	private static string[] _profanities;
}
