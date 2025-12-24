using System;
using System.Collections.Generic;

// Token: 0x0200051B RID: 1307
public class BlacklistTest9
{
	// Token: 0x06002AE5 RID: 10981 RVA: 0x0015525D File Offset: 0x0015345D
	public static void init()
	{
		if (BlacklistTest9._initiated)
		{
			return;
		}
		BlacklistTest9._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest9._profanity, 3);
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x00155278 File Offset: 0x00153478
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		Dictionary<string, string[]> tProfanity = BlacklistTest9._profanity;
		for (int i = 0; i < tNameSpan.Length - 3 + 1; i++)
		{
			string tCheckString = tNameSpan.Slice(i, 3).ToString();
			string[] tBlacklisted;
			if (tProfanity.TryGetValue(tCheckString, out tBlacklisted))
			{
				for (int j = 0; j < tBlacklisted.Length; j++)
				{
					ReadOnlySpan<char> tProfaneWord = tBlacklisted[j].AsSpan();
					if (BlacklistTools.contains(tNameSpan, tProfaneWord))
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
					if (BlacklistTools.contains(tCleanSpan, tProfaneWord2))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0400202C RID: 8236
	private static readonly Dictionary<string, string[]> _profanity = new Dictionary<string, string[]>();

	// Token: 0x0400202D RID: 8237
	private const int INDEX_LENGTH = 3;

	// Token: 0x0400202E RID: 8238
	private static bool _initiated = false;
}
