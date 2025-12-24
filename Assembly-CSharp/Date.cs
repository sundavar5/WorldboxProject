using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// Token: 0x02000524 RID: 1316
public static class Date
{
	// Token: 0x06002B0E RID: 11022 RVA: 0x00155B69 File Offset: 0x00153D69
	public static string getAgoString(double pTimestamp)
	{
		return Date.formatSeconds(World.world.getWorldTimeElapsedSince(pTimestamp)) + " ago";
	}

	// Token: 0x06002B0F RID: 11023 RVA: 0x00155B88 File Offset: 0x00153D88
	public static string formatSeconds(float pSeconds)
	{
		string tResult;
		if (pSeconds < 60f)
		{
			tResult = ((int)pSeconds).ToText() + "s";
		}
		else
		{
			tResult = ((int)pSeconds / 60).ToText();
			tResult += "m";
		}
		return tResult;
	}

	// Token: 0x06002B10 RID: 11024 RVA: 0x00155BCC File Offset: 0x00153DCC
	public static float getMonthTime()
	{
		int tTotalMonths = Date.getMonthsSince(0.0);
		return (float)World.world.getCurWorldTime() - (float)tTotalMonths * 5f;
	}

	// Token: 0x06002B11 RID: 11025 RVA: 0x00155BFC File Offset: 0x00153DFC
	public static string getYearDate(double pTime)
	{
		return Date.getYear(pTime).ToText();
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x00155C09 File Offset: 0x00153E09
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getYear(double pTime)
	{
		return Date.getYear0(pTime) + 1;
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x00155C13 File Offset: 0x00153E13
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getYear0(double pTime)
	{
		return (int)(pTime / 60.0);
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x00155C24 File Offset: 0x00153E24
	public static int[] getRawDate(double pTime)
	{
		int tDateYear = (int)(pTime / 5.0 / 12.0);
		while (pTime < 0.0)
		{
			pTime += 600000.0;
		}
		double num = pTime / 5.0;
		double tTotalYearTime = num / 12.0;
		int tTotalMonths = (int)num;
		int tTotalYears = (int)tTotalYearTime;
		int tDateMonth = (int)((pTime - (double)((float)tTotalYears * 5f * 12f)) / 5.0);
		int tDateDay = (int)((pTime - (double)((float)tTotalMonths * 5f)) / 5.0 * 30.0);
		tDateYear++;
		tDateMonth++;
		tDateDay++;
		return new int[]
		{
			tDateDay,
			tDateMonth,
			tDateYear
		};
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x00155CE4 File Offset: 0x00153EE4
	public static string getDate(double pTime)
	{
		int[] rawDate = Date.getRawDate(pTime);
		int tDateDay = rawDate[0];
		int tDateMonth = rawDate[1];
		int tDateYear = rawDate[2];
		if (LocalizedTextManager.instance.language == "en")
		{
			using (StringBuilderPool tString = new StringBuilderPool())
			{
				tString.Append(tDateDay);
				tString.Append(Date.GetDaySuffix(tDateDay));
				tString.Append(" of ");
				tString.Append(Date.formatMonth(tDateMonth));
				tString.Append(", ");
				tString.Append(tDateYear.ToText());
				return tString.ToString();
			}
		}
		return Date.formatDate(tDateDay, tDateMonth, tDateYear);
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x00155D94 File Offset: 0x00153F94
	internal static string formatMonth(int pMonth)
	{
		return LocalizedTextManager.getText("month_" + pMonth.ToString(), null, false);
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x00155DB0 File Offset: 0x00153FB0
	internal static string formatDate(int pDay, int pMonth, int pYear)
	{
		CultureInfo culture = LocalizedTextManager.getCulture(null);
		string tLongDatePattern = culture.DateTimeFormat.LongDatePattern;
		if (culture.TwoLetterISOLanguageName == "ar")
		{
			tLongDatePattern = "/ddMMMMyyyy/";
		}
		string tResult = Regex.Replace(tLongDatePattern, "\\bdddd[,\\s]*", "").Trim();
		string tMonthTranslation = LocalizedTextManager.getText("inflected_month_" + pMonth.ToString(), null, false);
		MatchCollection tMatches = null;
		if (tResult.Contains("'"))
		{
			tMatches = Regex.Matches(tResult, "'[^']*'");
			for (int i = 0; i < tMatches.Count; i++)
			{
				tResult = tResult.Replace(tMatches[i].Value, "{{{" + i.ToString() + "}}}");
			}
		}
		tResult = tResult.Replace("MMMM", "[[[1]]]");
		tResult = tResult.Replace("yyyy", "[[[2]]]");
		tResult = tResult.Replace("dd", "[[[3]]]");
		tResult = Regex.Replace(tResult, "\\b[d]\\b", "[[[4]]]");
		tResult = tResult.Replace("MM", "[[[5]]]");
		tResult = tResult.Replace("M", "[[[6]]]");
		tResult = tResult.Replace("[[[1]]]", tMonthTranslation);
		tResult = tResult.Replace("[[[2]]]", pYear.ToText());
		tResult = tResult.Replace("[[[3]]]", (pDay < 10) ? ("0" + pDay.ToString()) : pDay.ToString());
		tResult = tResult.Replace("[[[4]]]", pDay.ToString());
		tResult = tResult.Replace("[[[5]]]", (pMonth < 10) ? ("0" + pMonth.ToString()) : pMonth.ToString());
		tResult = tResult.Replace("[[[6]]]", pMonth.ToString());
		if (tMatches != null && tMatches.Count > 0)
		{
			for (int j = tMatches.Count - 1; j >= 0; j--)
			{
				tResult = tResult.Replace("{{{" + j.ToString() + "}}}", tMatches[j].Value.Trim('\''));
			}
		}
		return tResult;
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x00155FC5 File Offset: 0x001541C5
	public static int getCurrentMonth()
	{
		return Date.getMonth(World.world.getCurWorldTime());
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x00155FD8 File Offset: 0x001541D8
	public static int getMonth(double pTimestamp)
	{
		float tYear = (float)Date.getYear0(pTimestamp);
		return (int)((pTimestamp - (double)(tYear * 12f * 5f)) / 5.0 + 1.0);
	}

	// Token: 0x06002B1A RID: 11034 RVA: 0x00156012 File Offset: 0x00154212
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getCurrentYear()
	{
		return Date.getYear(World.world.getCurWorldTime());
	}

	// Token: 0x06002B1B RID: 11035 RVA: 0x00156023 File Offset: 0x00154223
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getYearsSince(double pFrom)
	{
		return Date.getYear0(World.world.getCurWorldTime() - pFrom);
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x00156036 File Offset: 0x00154236
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getMonthsSince(double pFrom)
	{
		return (int)((World.world.getCurWorldTime() - pFrom) / 5.0);
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x00156050 File Offset: 0x00154250
	private static string GetDaySuffix(int day)
	{
		switch (day)
		{
		case 1:
			break;
		case 2:
			goto IL_34;
		case 3:
			goto IL_3A;
		default:
			switch (day)
			{
			case 21:
				break;
			case 22:
				goto IL_34;
			case 23:
				goto IL_3A;
			default:
				if (day != 31)
				{
					return "th";
				}
				break;
			}
			break;
		}
		return "st";
		IL_34:
		return "nd";
		IL_3A:
		return "rd";
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x001560A2 File Offset: 0x001542A2
	public static bool isMonolithMonth()
	{
		return Date.getCurrentMonth() == 4;
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x001560AF File Offset: 0x001542AF
	public static string getUIStringYearMonthShort()
	{
		return "y:" + Date.getCurrentYear().ToText() + ", m:" + Date.getCurrentMonth().ToText();
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x001560D4 File Offset: 0x001542D4
	public static string getUIStringYearMonth()
	{
		return "y: " + Date.getCurrentYear().ToText() + ", m: " + Date.getCurrentMonth().ToText();
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x001560FC File Offset: 0x001542FC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string TimeNow()
	{
		DateTime dt = DateTime.Now;
		char[] array = new char[8];
		Date.Write2Chars(array, 0, dt.Hour);
		array[2] = ':';
		Date.Write2Chars(array, 3, dt.Minute);
		array[5] = ':';
		Date.Write2Chars(array, 6, dt.Second);
		return new string(array);
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x0015614E File Offset: 0x0015434E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Write2Chars(char[] chars, int offset, int value)
	{
		chars[offset] = Date.Digit(value / 10);
		chars[offset + 1] = Date.Digit(value % 10);
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x0015616A File Offset: 0x0015436A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static char Digit(int value)
	{
		return (char)(value + 48);
	}
}
