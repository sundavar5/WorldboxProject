using System;
using System.Collections.Generic;

// Token: 0x02000511 RID: 1297
public class BlacklistTest11
{
	// Token: 0x06002ABB RID: 10939 RVA: 0x00154514 File Offset: 0x00152714
	public static void init()
	{
		if (BlacklistTest11._initiated)
		{
			return;
		}
		BlacklistTest11._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest11._profanity, 3);
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x00154530 File Offset: 0x00152730
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		int tNameLength = tNameSpan.Length;
		Dictionary<string, string[]> tProfanity = BlacklistTest11._profanity;
		for (int i = 0; i < tNameLength - 3 + 1; i++)
		{
			string tCheckString = tNameSpan.Slice(i, 3).ToString();
			string[] tBlacklisted;
			if (tProfanity.TryGetValue(tCheckString, out tBlacklisted))
			{
				for (int j = 0; j < tBlacklisted.Length; j++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[j].AsSpan();
					if (BlacklistTools.contains(tNameSpan.Slice(i), tProfaneWord))
					{
						return true;
					}
				}
			}
		}
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		int tCleanLength = tCleanSpan.Length;
		if (tCleanSpan == tNameSpan || tCleanLength <= 2)
		{
			return false;
		}
		for (int k = 0; k < tCleanLength - 3 + 1; k++)
		{
			string tCheckString2 = tCleanSpan.Slice(k, 3).ToString();
			string[] tBlacklisted2;
			if (tProfanity.TryGetValue(tCheckString2, out tBlacklisted2))
			{
				for (int l = 0; l < tBlacklisted2.Length; l++)
				{
					ReadOnlySpan<char> tProfaneWord2 = tBlacklisted2[l].AsSpan();
					if (BlacklistTools.contains(tCleanSpan.Slice(k), tProfaneWord2))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0400200B RID: 8203
	private static readonly Dictionary<string, string[]> _profanity = new Dictionary<string, string[]>();

	// Token: 0x0400200C RID: 8204
	private const int INDEX_LENGTH = 3;

	// Token: 0x0400200D RID: 8205
	private static bool _initiated = false;
}
