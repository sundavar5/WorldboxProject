using System;
using System.Collections.Generic;

// Token: 0x02000512 RID: 1298
public class BlacklistTest12
{
	// Token: 0x06002ABF RID: 10943 RVA: 0x00154674 File Offset: 0x00152874
	public static void init()
	{
		if (BlacklistTest12._initiated)
		{
			return;
		}
		BlacklistTest12._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest12._profanity, 3);
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x00154690 File Offset: 0x00152890
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		Dictionary<string, string[]> tProfanity = BlacklistTest12._profanity;
		for (int i = 0; i < tNameSpan.Length - 3 + 1; i++)
		{
			string tCheckString = tNameSpan.Slice(i, 3).ToString();
			string[] tBlacklisted;
			if (tProfanity.TryGetValue(tCheckString, out tBlacklisted))
			{
				for (int j = 0; j < tBlacklisted.Length; j++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[j].AsSpan();
					if (BlacklistTools.contains(ref tNameSpan, ref tProfaneWord))
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
					if (BlacklistTools.contains(ref tCleanSpan, ref tProfaneWord2))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0400200E RID: 8206
	private static readonly Dictionary<string, string[]> _profanity = new Dictionary<string, string[]>();

	// Token: 0x0400200F RID: 8207
	private const int INDEX_LENGTH = 3;

	// Token: 0x04002010 RID: 8208
	private static bool _initiated = false;
}
