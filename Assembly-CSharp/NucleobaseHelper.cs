using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
public static class NucleobaseHelper
{
	// Token: 0x060007EE RID: 2030 RVA: 0x0006FE44 File Offset: 0x0006E044
	public static Color getColor(char pChar, bool pDark = false)
	{
		Color tColor;
		if (pDark)
		{
			if (pChar <= 'C')
			{
				if (pChar == 'A')
				{
					return NucleobaseHelper.color_adenine_dark;
				}
				if (pChar == 'C')
				{
					return NucleobaseHelper.color_cytosine_dark;
				}
			}
			else
			{
				if (pChar == 'G')
				{
					return NucleobaseHelper.color_guanine_dark;
				}
				if (pChar == 'T')
				{
					return NucleobaseHelper.color_thymine_dark;
				}
			}
			tColor = Color.black;
		}
		else
		{
			if (pChar <= 'C')
			{
				if (pChar == 'A')
				{
					return NucleobaseHelper.color_adenine;
				}
				if (pChar == 'C')
				{
					return NucleobaseHelper.color_cytosine;
				}
			}
			else
			{
				if (pChar == 'G')
				{
					return NucleobaseHelper.color_guanine;
				}
				if (pChar == 'T')
				{
					return NucleobaseHelper.color_thymine;
				}
			}
			tColor = Color.black;
		}
		return tColor;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0006FEE0 File Offset: 0x0006E0E0
	public static string getColorHex(char pChar, bool pDark = false)
	{
		string tResult = string.Empty;
		if (pDark)
		{
			if (pChar <= 'C')
			{
				if (pChar != 'A')
				{
					if (pChar == 'C')
					{
						tResult = "#3A8FFF88";
					}
				}
				else
				{
					tResult = "#70FF7088";
				}
			}
			else if (pChar != 'G')
			{
				if (pChar == 'T')
				{
					tResult = "#FF3A3A88";
				}
			}
			else
			{
				tResult = "#FFDF4288";
			}
		}
		else if (pChar <= 'C')
		{
			if (pChar != 'A')
			{
				if (pChar == 'C')
				{
					tResult = "#3A8FFF";
				}
			}
			else
			{
				tResult = "#70FF70";
			}
		}
		else if (pChar != 'G')
		{
			if (pChar == 'T')
			{
				tResult = "#FF3A3A";
			}
		}
		else
		{
			tResult = "#FFDF42";
		}
		return tResult;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0006FF70 File Offset: 0x0006E170
	private static string getNucleobaseFullID(char pChar)
	{
		string tNucleobaseID = string.Empty;
		if (pChar <= 'C')
		{
			if (pChar != 'A')
			{
				if (pChar == 'C')
				{
					tNucleobaseID = "nucleo_cytosine";
				}
			}
			else
			{
				tNucleobaseID = "nucleo_adenine";
			}
		}
		else if (pChar != 'G')
		{
			if (pChar == 'T')
			{
				tNucleobaseID = "nucleo_thymine";
			}
		}
		else
		{
			tNucleobaseID = "nucleo_guanine";
		}
		return tNucleobaseID;
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0006FFC0 File Offset: 0x0006E1C0
	public static string getColoredNucleobaseFull(char pChar)
	{
		string tColor = NucleobaseHelper.getColorHex(pChar, false);
		string tNucleobaseNameTranslated = NucleobaseHelper.getFullNucleobaseName(pChar);
		return string.Concat(new string[]
		{
			"<color=",
			tColor,
			">",
			tNucleobaseNameTranslated,
			"</color>"
		});
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00070007 File Offset: 0x0006E207
	public static string getFullNucleobaseName(char pChar)
	{
		return LocalizedTextManager.getText(NucleobaseHelper.getNucleobaseFullID(pChar), null, false);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00070018 File Offset: 0x0006E218
	public static string getColoredSequence(string pGeneticCode)
	{
		string tResult = string.Empty;
		foreach (char tShortNucleobaseID in pGeneticCode)
		{
			string tColor = NucleobaseHelper.getColorHex(tShortNucleobaseID, false);
			tResult += string.Format("<color={0}>{1}</color>", tColor, tShortNucleobaseID);
		}
		return tResult;
	}

	// Token: 0x04000842 RID: 2114
	private const char SHORT_ADENINE = 'A';

	// Token: 0x04000843 RID: 2115
	private const char SHORT_CYTOSINE = 'C';

	// Token: 0x04000844 RID: 2116
	private const char SHORT_GUANINE = 'G';

	// Token: 0x04000845 RID: 2117
	private const char SHORT_THYMINE = 'T';

	// Token: 0x04000846 RID: 2118
	private const string NOT_CONNECTED_TRANSPARENCY = "88";

	// Token: 0x04000847 RID: 2119
	private const string COLOR_HEX_ADENINE = "#70FF70";

	// Token: 0x04000848 RID: 2120
	private const string COLOR_HEX_CYTOSINE = "#3A8FFF";

	// Token: 0x04000849 RID: 2121
	private const string COLOR_HEX_GUANINE = "#FFDF42";

	// Token: 0x0400084A RID: 2122
	private const string COLOR_HEX_THYMINE = "#FF3A3A";

	// Token: 0x0400084B RID: 2123
	private const string COLOR_HEX_ADENINE_DARK = "#70FF7088";

	// Token: 0x0400084C RID: 2124
	private const string COLOR_HEX_CYTOSINE_DARK = "#3A8FFF88";

	// Token: 0x0400084D RID: 2125
	private const string COLOR_HEX_GUANINE_DARK = "#FFDF4288";

	// Token: 0x0400084E RID: 2126
	private const string COLOR_HEX_THYMINE_DARK = "#FF3A3A88";

	// Token: 0x0400084F RID: 2127
	private static readonly Color color_adenine = Toolbox.makeColor("#70FF70");

	// Token: 0x04000850 RID: 2128
	private static readonly Color color_cytosine = Toolbox.makeColor("#3A8FFF");

	// Token: 0x04000851 RID: 2129
	private static readonly Color color_guanine = Toolbox.makeColor("#FFDF42");

	// Token: 0x04000852 RID: 2130
	private static readonly Color color_thymine = Toolbox.makeColor("#FF3A3A");

	// Token: 0x04000853 RID: 2131
	private static readonly Color color_adenine_dark = Toolbox.makeColor("#70FF7088");

	// Token: 0x04000854 RID: 2132
	private static readonly Color color_cytosine_dark = Toolbox.makeColor("#3A8FFF88");

	// Token: 0x04000855 RID: 2133
	private static readonly Color color_guanine_dark = Toolbox.makeColor("#FFDF4288");

	// Token: 0x04000856 RID: 2134
	private static readonly Color color_thymine_dark = Toolbox.makeColor("#FF3A3A88");

	// Token: 0x04000857 RID: 2135
	public static readonly Color color_bad = Color.black;

	// Token: 0x04000858 RID: 2136
	public const string COLOR_HEX_BAD = "#B159FF";
}
