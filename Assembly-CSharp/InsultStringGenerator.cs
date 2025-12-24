using System;
using System.Collections.Generic;

// Token: 0x02000468 RID: 1128
public static class InsultStringGenerator
{
	// Token: 0x06002693 RID: 9875 RVA: 0x0013A368 File Offset: 0x00138568
	public static string getRandomText(int pMin = 4, int pMax = 9, bool pUseSameSizeSet = false)
	{
		string result;
		using (StringBuilderPool tLocalNameBuilder = new StringBuilderPool())
		{
			int tRandomSize = Randy.randomInt(pMin, pMax);
			for (int i = 0; i < tRandomSize; i++)
			{
				string tRandomCharacter = pUseSameSizeSet ? InsultStringGenerator._insult_characters_2.GetRandom<string>() : InsultStringGenerator._insult_characters.GetRandom<string>();
				tLocalNameBuilder.Append(tRandomCharacter);
			}
			result = tLocalNameBuilder.ToString();
		}
		return result;
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x0013A3D8 File Offset: 0x001385D8
	public static string getDNASequenceBad()
	{
		string tResult;
		if (InsultStringGenerator._cached_bad_strings.Count < 30)
		{
			using (StringBuilderPool tBuilder = new StringBuilderPool())
			{
				for (int i = 0; i < 6; i++)
				{
					if (i > 0)
					{
						tBuilder.Append(" ");
					}
					tBuilder.Append(InsultStringGenerator.getRandomText(3, 3, true));
				}
				tResult = tBuilder.ToString();
				tResult = Toolbox.coloredString(tResult, "#B159FF");
				InsultStringGenerator._cached_bad_strings.Add(tResult);
				return tResult;
			}
		}
		tResult = InsultStringGenerator._cached_bad_strings.GetRandom<string>();
		return tResult;
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x0013A46C File Offset: 0x0013866C
	public static string getBadConnectionString()
	{
		string tResult;
		if (InsultStringGenerator._cached_bad_connections_string.Count < 30)
		{
			tResult = InsultStringGenerator.getRandomText(7, 7, true);
			tResult = Toolbox.coloredString(tResult, "#B159FF");
			InsultStringGenerator._cached_bad_connections_string.Add(tResult);
		}
		else
		{
			tResult = InsultStringGenerator._cached_bad_connections_string.GetRandom<string>();
		}
		return tResult;
	}

	// Token: 0x04001D09 RID: 7433
	private static string[] _insult_characters_2 = new string[]
	{
		"#",
		"%",
		"&",
		"@"
	};

	// Token: 0x04001D0A RID: 7434
	private static string[] _insult_characters = new string[]
	{
		"!",
		"#",
		"%",
		"&",
		"@"
	};

	// Token: 0x04001D0B RID: 7435
	private static List<string> _cached_bad_strings = new List<string>();

	// Token: 0x04001D0C RID: 7436
	private static List<string> _cached_bad_connections_string = new List<string>();

	// Token: 0x04001D0D RID: 7437
	private const int MAX_HARMFUL_DNA_SEQUENCES = 30;

	// Token: 0x04001D0E RID: 7438
	private const int MAX_BAD_CONNECTION_STRINGS = 30;
}
