using System;
using System.Collections.Generic;

// Token: 0x020000B0 RID: 176
public class Blacklist
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x00053E4E File Offset: 0x0005204E
	public static void init()
	{
		if (Blacklist._initiated)
		{
			return;
		}
		Blacklist._initiated = true;
		BlacklistTools.loadProfanityFilter(Blacklist._profanity, 3);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00053E6C File Offset: 0x0005206C
	internal unsafe static bool checkBlackList(StringBuilderPool pName)
	{
		pName.ToLowerInvariant();
		int length = pName.Length;
		Span<char> tNewSpan = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)length) * 2)], length);
		pName.CopyTo(0, tNewSpan, pName.Length);
		ReadOnlySpan<char> tNameSpan = tNewSpan;
		Dictionary<string, string[]> tProfanity = Blacklist._profanity;
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

	// Token: 0x060005A5 RID: 1445 RVA: 0x00053FB0 File Offset: 0x000521B0
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		Dictionary<string, string[]> tProfanity = Blacklist._profanity;
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

	// Token: 0x040005D3 RID: 1491
	private static readonly Dictionary<string, string[]> _profanity = new Dictionary<string, string[]>();

	// Token: 0x040005D4 RID: 1492
	private const int INDEX_LENGTH = 3;

	// Token: 0x040005D5 RID: 1493
	private static bool _initiated = false;
}
