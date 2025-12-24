using System;
using System.Collections.Generic;

// Token: 0x02000518 RID: 1304
public class BlacklistTest6
{
	// Token: 0x06002AD7 RID: 10967 RVA: 0x00154E50 File Offset: 0x00153050
	public static void init()
	{
		if (BlacklistTest6._initiated)
		{
			return;
		}
		BlacklistTest6._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest6._profanity, BlacklistTest6._unique);
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x00154E70 File Offset: 0x00153070
	internal static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		BlacklistTest6._unique.Clear();
		BlacklistTest6._unique.UnionWith(tName);
		BlacklistTest6._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		string tClean = BlacklistTools.cleanStringAsSpan(tName);
		bool tDoubleCheck = !(tClean == tName);
		Dictionary<char, char[][]> tProfanity = BlacklistTest6._profanity;
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		ReadOnlySpan<char> tCleanSpan = tDoubleCheck ? tClean.AsSpan() : null;
		foreach (char tChar in BlacklistTest6._unique)
		{
			char[][] tBlacklisted;
			if (tProfanity.TryGetValue(tChar, out tBlacklisted))
			{
				for (int i = 0; i < tBlacklisted.Length; i++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[i].AsSpan<char>();
					if (BlacklistTools.contains(tNameSpan, tProfaneWord))
					{
						return true;
					}
					if (tDoubleCheck && BlacklistTools.contains(tCleanSpan, tProfaneWord))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04002020 RID: 8224
	private static readonly Dictionary<char, char[][]> _profanity = new Dictionary<char, char[][]>();

	// Token: 0x04002021 RID: 8225
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x04002022 RID: 8226
	private static bool _initiated = false;
}
