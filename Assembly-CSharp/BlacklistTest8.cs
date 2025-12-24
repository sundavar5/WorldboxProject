using System;
using System.Collections.Generic;

// Token: 0x0200051A RID: 1306
public class BlacklistTest8
{
	// Token: 0x06002AE0 RID: 10976 RVA: 0x001550DC File Offset: 0x001532DC
	public static void init()
	{
		if (BlacklistTest8._initiated)
		{
			return;
		}
		BlacklistTest8._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest8._profanity, ref BlacklistTest8._min_length, ref BlacklistTest8._max_length);
		for (int i = BlacklistTest8._min_length; i <= BlacklistTest8._max_length; i++)
		{
			BlacklistTest8._char_arrays[i] = new char[i];
		}
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x00155130 File Offset: 0x00153330
	private static int getCharHashCode(char[] pChar)
	{
		return BlacklistTools.getCharHashCode(pChar);
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x00155138 File Offset: 0x00153338
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		for (int i = BlacklistTest8._min_length; i <= BlacklistTest8._max_length; i++)
		{
			char[] tCheck = BlacklistTest8._char_arrays[i];
			HashSet<int> tProfanity = BlacklistTest8._profanity[i];
			for (int j = 0; j < tNameSpan.Length - i + 1; j++)
			{
				tNameSpan.Slice(j, i).CopyTo(tCheck);
				int tHash = BlacklistTest8.getCharHashCode(tCheck);
				if (tProfanity.Contains(tHash))
				{
					return true;
				}
				if (tDoubleCheck && tCleanSpan.Length >= j + i)
				{
					tCleanSpan.Slice(j, i).CopyTo(tCheck);
					tHash = BlacklistTest8.getCharHashCode(tCheck);
					if (tProfanity.Contains(tHash))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04002027 RID: 8231
	private static readonly Dictionary<int, HashSet<int>> _profanity = new Dictionary<int, HashSet<int>>();

	// Token: 0x04002028 RID: 8232
	private static int _min_length = int.MaxValue;

	// Token: 0x04002029 RID: 8233
	private static int _max_length = int.MinValue;

	// Token: 0x0400202A RID: 8234
	private static readonly Dictionary<int, char[]> _char_arrays = new Dictionary<int, char[]>();

	// Token: 0x0400202B RID: 8235
	private static bool _initiated = false;
}
