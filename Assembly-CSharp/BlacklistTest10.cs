using System;
using System.Collections.Generic;

// Token: 0x02000510 RID: 1296
public class BlacklistTest10
{
	// Token: 0x06002AB7 RID: 10935 RVA: 0x001543C8 File Offset: 0x001525C8
	public static void init()
	{
		if (BlacklistTest10._initiated)
		{
			return;
		}
		BlacklistTest10._initiated = true;
		BlacklistTools.loadProfanityFilter(BlacklistTest10._profanity, BlacklistTest10._unique);
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x001543E8 File Offset: 0x001525E8
	public static bool checkBlackList(string pName)
	{
		string tName = pName.ToLower();
		BlacklistTest10._unique.Clear();
		BlacklistTest10._unique.UnionWith(tName);
		BlacklistTest10._unique.RemoveWhere((char pChar) => !char.IsLetter(pChar));
		string tClean = BlacklistTools.cleanString(tName);
		bool tDoubleCheck = !(tClean == tName);
		Dictionary<char, string[]> tProfanity = BlacklistTest10._profanity;
		foreach (char tChar in BlacklistTest10._unique)
		{
			if (tProfanity.ContainsKey(tChar))
			{
				for (int i = 0; i < tProfanity[tChar].Length; i++)
				{
					if (tName.Contains(tProfanity[tChar][i]))
					{
						return true;
					}
					if (tDoubleCheck && tClean.Contains(tProfanity[tChar][i]))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04002008 RID: 8200
	private static readonly Dictionary<char, string[]> _profanity = new Dictionary<char, string[]>();

	// Token: 0x04002009 RID: 8201
	private static readonly HashSet<char> _unique = new HashSet<char>();

	// Token: 0x0400200A RID: 8202
	private static bool _initiated = false;
}
