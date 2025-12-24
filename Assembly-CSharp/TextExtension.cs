using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000008 RID: 8
public static class TextExtension
{
	// Token: 0x0600000B RID: 11 RVA: 0x00003170 File Offset: 0x00001370
	public static void SetHindiText(this Text text, string value)
	{
		if (TextExtension.krutiDev == null)
		{
			TextExtension.krutiDev = (Resources.Load("CD_Kruti_Dev_010") as Font);
		}
		bool isColor = value.IndexOf("</color>", StringComparison.Ordinal) > -1;
		if (isColor)
		{
			TextExtension.colors.Clear();
			value = value.Replace("</color>", "END_COLOR");
			int i = 0;
			foreach (object item in Regex.Matches(value, "<color.*?>"))
			{
				TextExtension.colors.Add(item.ToString());
				value = value.Replace(item.ToString(), "COLOR_" + i++.ToString());
			}
		}
		if (value.IndexOf("'", StringComparison.Ordinal) > -1)
		{
			value = value.Replace("'", "SINGLE_QUOTE");
		}
		value = HindiCorrector.GetCorrectedHindiText(value);
		if (value.IndexOf("SINGLE_QUOTE", StringComparison.Ordinal) > -1)
		{
			value = value.Replace("SINGLE_QUOTE", "'");
		}
		if (isColor)
		{
			value = value.Replace("END_COLOR", "</color>");
			int j = 0;
			foreach (string item2 in TextExtension.colors)
			{
				value = value.Replace("COLOR_" + j++.ToString(), item2);
			}
		}
		text.font = TextExtension.krutiDev;
		text.text = value;
	}

	// Token: 0x0400000B RID: 11
	private static Font krutiDev;

	// Token: 0x0400000C RID: 12
	private static List<string> colors = new List<string>();
}
