using System;
using System.Collections.Generic;

// Token: 0x02000519 RID: 1305
public class BlacklistTest7
{
	// Token: 0x06002ADB RID: 10971 RVA: 0x00154FB4 File Offset: 0x001531B4
	public static void init()
	{
		if (BlacklistTest7._initiated)
		{
			return;
		}
		BlacklistTest7._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest7._profanity, ref BlacklistTest7._min_length, ref BlacklistTest7._max_length);
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x00154FD8 File Offset: 0x001531D8
	private static int getCharHashCode(char[] pChar)
	{
		return BlacklistTools.getCharHashCode(pChar);
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x00154FE0 File Offset: 0x001531E0
	internal static bool checkBlackList(string pName)
	{
		ReadOnlySpan<char> tNameSpan = pName.ToLower().AsSpan();
		ReadOnlySpan<char> tCleanSpan = BlacklistTools.cleanSpan(tNameSpan);
		bool tDoubleCheck = !(tCleanSpan == tNameSpan);
		for (int i = BlacklistTest7._min_length; i <= BlacklistTest7._max_length; i++)
		{
			HashSet<int> tProfanity = BlacklistTest7._profanity[i];
			for (int j = 0; j < tNameSpan.Length - i + 1; j++)
			{
				int tHash = BlacklistTest7.getCharHashCode(tNameSpan.Slice(j, i).ToArray());
				if (tProfanity.Contains(tHash))
				{
					return true;
				}
				if (tDoubleCheck && tCleanSpan.Length >= j + i)
				{
					tHash = BlacklistTest7.getCharHashCode(tCleanSpan.Slice(j, i).ToArray());
					if (tProfanity.Contains(tHash))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04002023 RID: 8227
	private static readonly Dictionary<int, HashSet<int>> _profanity = new Dictionary<int, HashSet<int>>();

	// Token: 0x04002024 RID: 8228
	private static int _min_length = int.MaxValue;

	// Token: 0x04002025 RID: 8229
	private static int _max_length = int.MinValue;

	// Token: 0x04002026 RID: 8230
	private static bool _initiated = false;
}
