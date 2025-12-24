using System;
using System.Collections.Generic;

// Token: 0x02000515 RID: 1301
public class BlacklistTest3
{
	// Token: 0x06002ACB RID: 10955 RVA: 0x00154A68 File Offset: 0x00152C68
	public static void init()
	{
		if (BlacklistTest3._initiated)
		{
			return;
		}
		BlacklistTest3._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest3._profanity, BlacklistTest3._unique);
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x00154A88 File Offset: 0x00152C88
	internal static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		BlacklistTest3._unique.Clear();
		BlacklistTest3._unique.UnionWith(tName);
		BlacklistTest3._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		Dictionary<char, string[]> tProfanity = BlacklistTest3._profanity;
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		foreach (char tChar in BlacklistTest3._unique)
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

	// Token: 0x04002017 RID: 8215
	private static readonly Dictionary<char, string[]> _profanity = new Dictionary<char, string[]>();

	// Token: 0x04002018 RID: 8216
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x04002019 RID: 8217
	private static bool _initiated = false;
}
