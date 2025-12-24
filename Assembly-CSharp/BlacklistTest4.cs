using System;
using System.Collections.Generic;

// Token: 0x02000516 RID: 1302
public class BlacklistTest4
{
	// Token: 0x06002ACF RID: 10959 RVA: 0x00154BB0 File Offset: 0x00152DB0
	public static void init()
	{
		if (BlacklistTest4._initiated)
		{
			return;
		}
		BlacklistTest4._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest4._profanity, BlacklistTest4._unique);
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x00154BD0 File Offset: 0x00152DD0
	internal static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		BlacklistTest4._unique.Clear();
		BlacklistTest4._unique.UnionWith(tName);
		BlacklistTest4._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		Dictionary<char, char[][]> tProfanity = BlacklistTest4._profanity;
		foreach (char tChar in BlacklistTest4._unique)
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

	// Token: 0x0400201A RID: 8218
	private static readonly Dictionary<char, char[][]> _profanity = new Dictionary<char, char[][]>();

	// Token: 0x0400201B RID: 8219
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x0400201C RID: 8220
	private static bool _initiated = false;
}
