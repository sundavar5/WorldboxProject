using System;
using System.Collections.Generic;

// Token: 0x0200050F RID: 1295
public class BlacklistTest
{
	// Token: 0x06002AB3 RID: 10931 RVA: 0x0015427E File Offset: 0x0015247E
	public static void init()
	{
		if (BlacklistTest._initiated)
		{
			return;
		}
		BlacklistTest._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest._profanity, BlacklistTest._unique);
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x001542A0 File Offset: 0x001524A0
	public static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		BlacklistTest._unique.Clear();
		BlacklistTest._unique.UnionWith(tName);
		BlacklistTest._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		Dictionary<char, string[]> tProfanity = BlacklistTest._profanity;
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		foreach (char tChar in BlacklistTest._unique)
		{
			string[] tBlacklisted;
			if (tProfanity.TryGetValue(tChar, out tBlacklisted))
			{
				for (int i = 0; i < tBlacklisted.Length; i++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[i].AsSpan();
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

	// Token: 0x04002005 RID: 8197
	private static readonly Dictionary<char, string[]> _profanity = new Dictionary<char, string[]>();

	// Token: 0x04002006 RID: 8198
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x04002007 RID: 8199
	private static bool _initiated = false;
}
