using System;
using System.Collections.Generic;

// Token: 0x02000513 RID: 1299
public class BlacklistTest13
{
	// Token: 0x06002AC3 RID: 10947 RVA: 0x001547C0 File Offset: 0x001529C0
	public static void init()
	{
		if (BlacklistTest13._initiated)
		{
			return;
		}
		BlacklistTest13._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest13._profanity, 3);
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x001547DC File Offset: 0x001529DC
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		Dictionary<string, string[]> tProfanity = BlacklistTest13._profanity;
		for (int i = 0; i < tNameSpan.Length - 3 + 1; i++)
		{
			string tCheckString = tNameSpan.Slice(i, 3).ToString();
			string[] tBlacklisted;
			if (tProfanity.TryGetValue(tCheckString, out tBlacklisted))
			{
				for (int j = 0; j < tBlacklisted.Length; j++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[j].AsSpan();
					if (BlacklistTools.contains(tNameSpan, tProfaneWord, i))
					{
						return true;
					}
				}
			}
		}
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		if (tCleanSpan == tNameSpan || tCleanSpan.Length <= 2)
		{
			return false;
		}
		for (int k = 0; k < tCleanSpan.Length - 3 + 1; k++)
		{
			string tCheckString2 = tCleanSpan.Slice(k, 3).ToString();
			string[] tBlacklisted2;
			if (tProfanity.TryGetValue(tCheckString2, out tBlacklisted2))
			{
				for (int l = 0; l < tBlacklisted2.Length; l++)
				{
					ReadOnlySpan<char> tProfaneWord2 = tBlacklisted2[l].AsSpan();
					if (BlacklistTools.contains(tCleanSpan, tProfaneWord2, k))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04002011 RID: 8209
	private static readonly Dictionary<string, string[]> _profanity = new Dictionary<string, string[]>();

	// Token: 0x04002012 RID: 8210
	private const int INDEX_LENGTH = 3;

	// Token: 0x04002013 RID: 8211
	private static bool _initiated = false;
}
