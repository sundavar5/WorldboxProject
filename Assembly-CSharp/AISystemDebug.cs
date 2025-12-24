using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000371 RID: 881
public static class AISystemDebug
{
	// Token: 0x0600213E RID: 8510 RVA: 0x0011B9FB File Offset: 0x00119BFB
	public static void clear()
	{
		AISystemDebug.debug_list_actions.Clear();
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x0011BA07 File Offset: 0x00119C07
	public static void debugLog(string pString)
	{
		AISystemDebug.debug_list_actions.Add(pString);
		if (AISystemDebug.debug_list_actions.Count > 1000)
		{
			AISystemDebug.debug_list_actions.RemoveAt(0);
		}
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x0011BA30 File Offset: 0x00119C30
	public static void log()
	{
		string tResult = "";
		foreach (string tAction in AISystemDebug.debug_list_actions)
		{
			tResult = tResult + tAction + "\n";
		}
		File.WriteAllText(AISystemDebug.getPath(), tResult);
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x0011BA9C File Offset: 0x00119C9C
	public static string getPath()
	{
		return Application.persistentDataPath + AISystemDebug.dataName;
	}

	// Token: 0x040018AB RID: 6315
	private static string dataName = "/ai_system.log";

	// Token: 0x040018AC RID: 6316
	private static List<string> debug_list_actions = new List<string>();
}
