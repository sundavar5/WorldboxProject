using System;
using System.Collections.Generic;

// Token: 0x02000514 RID: 1300
public class BlacklistTest2
{
	// Token: 0x06002AC7 RID: 10951 RVA: 0x0015490D File Offset: 0x00152B0D
	public static void init()
	{
		if (BlacklistTest2._initiated)
		{
			return;
		}
		BlacklistTest2._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest2._profanity, BlacklistTest2._unique);
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x0015492C File Offset: 0x00152B2C
	internal static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		BlacklistTest2._unique.Clear();
		BlacklistTest2._unique.UnionWith(tName);
		BlacklistTest2._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		Dictionary<char, string[]> tProfanity = BlacklistTest2._profanity;
		string tClean = BlacklistTools.cleanStringAsSpan(tName);
		bool tDoubleCheck = !(tClean == tName);
		ReadOnlySpan<char> tNameSpan = tName.AsSpan();
		ReadOnlySpan<char> tCleanSpan = tDoubleCheck ? tClean.AsSpan() : null;
		foreach (char tChar in BlacklistTest2._unique)
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

	// Token: 0x04002014 RID: 8212
	private static readonly Dictionary<char, string[]> _profanity = new Dictionary<char, string[]>();

	// Token: 0x04002015 RID: 8213
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x04002016 RID: 8214
	private static bool _initiated = false;
}
