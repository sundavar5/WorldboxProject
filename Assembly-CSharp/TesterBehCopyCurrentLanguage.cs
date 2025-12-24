using System;
using System.IO;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class TesterBehCopyCurrentLanguage : BehaviourActionTester
{
	// Token: 0x060029C9 RID: 10697 RVA: 0x00149984 File Offset: 0x00147B84
	public override BehResult execute(AutoTesterBot pObject)
	{
		string tCurrentLanguage = LocalizedTextManager.instance.language;
		string tFolder = TesterBehScreenshotFolder.getScreenshotFolder(tCurrentLanguage);
		string tPath = "locales/" + tCurrentLanguage;
		string tLanguageJSON = (Resources.Load(tPath) as TextAsset).text;
		Console.WriteLine(string.Concat(new string[]
		{
			"[",
			Date.TimeNow(),
			"] Copying language: ",
			tPath,
			" to ",
			tFolder,
			"/",
			tCurrentLanguage,
			".json"
		}));
		File.WriteAllText(tFolder + "/" + tCurrentLanguage + ".json", tLanguageJSON);
		return BehResult.Continue;
	}
}
