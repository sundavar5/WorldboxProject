using System;
using System.IO;
using UnityEngine;

// Token: 0x0200053F RID: 1343
public class LogText
{
	// Token: 0x06002BE6 RID: 11238 RVA: 0x0015AD28 File Offset: 0x00158F28
	public static void log(string pEvent, string pInfo = "", string pState = "")
	{
		if (!Globals.DIAGNOSTIC)
		{
			return;
		}
		if (!LogText.created)
		{
			LogText.created = true;
			File.WriteAllText(LogText.getPath(), "");
		}
		if (pState == "st")
		{
			LogText.offset++;
		}
		else if (pState == "en")
		{
			LogText.offset--;
		}
		string tOffsetStr = "";
		for (int i = 0; i < LogText.offset; i++)
		{
			tOffsetStr += " ";
		}
		if (pState == "en")
		{
			tOffsetStr += " ";
		}
		else if (pState == "")
		{
			tOffsetStr += " ";
		}
		if (pState == "en")
		{
			pState = "x";
		}
		else if (pState == "st")
		{
			pState = "!";
		}
		if (pInfo != "")
		{
			pEvent = tOffsetStr + pEvent + " :: " + pInfo;
			if (pState != "")
			{
				pEvent = pEvent + " - " + pState;
			}
		}
		File.AppendAllText(LogText.getPath(), pEvent + "\n");
	}

	// Token: 0x06002BE7 RID: 11239 RVA: 0x0015AE59 File Offset: 0x00159059
	public static string getPath()
	{
		return Application.persistentDataPath + LogText.dataName;
	}

	// Token: 0x040021BD RID: 8637
	private static string dataName = "/wb_runtime.log";

	// Token: 0x040021BE RID: 8638
	private static bool created = false;

	// Token: 0x040021BF RID: 8639
	internal static int offset = 0;
}
