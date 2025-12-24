using System;
using System.Collections.Generic;

// Token: 0x02000517 RID: 1303
public class BlacklistTest5
{
	// Token: 0x06002AD3 RID: 10963 RVA: 0x00154D00 File Offset: 0x00152F00
	public static void init()
	{
		if (BlacklistTest5._initiated)
		{
			return;
		}
		BlacklistTest5._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest5._profanity, BlacklistTest5._unique);
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x00154D20 File Offset: 0x00152F20
	internal static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		BlacklistTest5._unique.Clear();
		BlacklistTest5._unique.UnionWith(tName);
		BlacklistTest5._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		Dictionary<char, char[][]> tProfanity = BlacklistTest5._profanity;
		foreach (char tChar in BlacklistTest5._unique)
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

	// Token: 0x0400201D RID: 8221
	private static readonly Dictionary<char, char[][]> _profanity = new Dictionary<char, char[][]>();

	// Token: 0x0400201E RID: 8222
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x0400201F RID: 8223
	private static bool _initiated = false;
}
