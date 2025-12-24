using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class ConstantineConventer
{
	// Token: 0x06002532 RID: 9522 RVA: 0x0013370C File Offset: 0x0013190C
	public static void init()
	{
		if (!ConstantineConventer.enabled)
		{
			return;
		}
		string[] array = Resources.Load<TextAsset>("texts/fmod_sheet").text.Split('\n', StringSplitOptions.None);
		Debug.Log(array[0]);
		List<string> tNewList = new List<string>();
		string tResult = "";
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string tEventString = array2[i].Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
			string[] array3 = tEventString.Split('/', StringSplitOptions.None);
			string tId = array3[array3.Length - 1];
			string tNewString = "\tpublic const string ";
			tNewString += tId;
			tNewString += " = ";
			tNewString += "\"";
			tNewString += tEventString;
			tNewString += "\"";
			tNewString += ";";
			tNewList.Add(tNewString);
			tResult = tResult + tNewString + "\n";
		}
		File.WriteAllText(Application.dataPath + "/Resources/texts/fmod_sheet_converted.txt", tResult);
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x00133828 File Offset: 0x00131A28
	public static void init2()
	{
		string[] array = Resources.Load<TextAsset>("texts/fmod_sheet").text.Split('\n', StringSplitOptions.None);
		Debug.Log(array[0]);
		List<string> tNewList = new List<string>();
		string tResult = "";
		string tCurrentTag = "";
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string tCleanString = array2[i].Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
			if (tCleanString.Contains("$"))
			{
				tCleanString = tCleanString.Replace("$ ", "");
				tCleanString = tCleanString.Replace("$", "");
				tCurrentTag = tCleanString;
			}
			else if (!tCleanString.Contains("WB_SFX_"))
			{
				tResult += "\n";
			}
			else
			{
				string tNewString = "\tpublic const string ";
				tNewString += tCleanString;
				tNewString += " = ";
				tNewString = tNewString + tCurrentTag + " + ";
				tNewString += "\"";
				tNewString += tCleanString;
				tNewString += "\"";
				tNewString += ";";
				tNewList.Add(tNewString);
				tResult = tResult + tNewString + "\n";
			}
		}
		File.WriteAllText(Application.dataPath + "/Resources/texts/fmod_sheet_converted.txt", tResult);
	}

	// Token: 0x04001AC6 RID: 6854
	private static bool enabled;
}
