using System;
using System.IO;
using ai.behaviours;

// Token: 0x020004D6 RID: 1238
public class TesterBehScreenshotFolder : BehaviourActionTester
{
	// Token: 0x060029FE RID: 10750 RVA: 0x0014A5B4 File Offset: 0x001487B4
	public override BehResult execute(AutoTesterBot pObject)
	{
		string tScreenshotFolder = TesterBehScreenshotFolder.getScreenshotFolder(LocalizedTextManager.instance.language);
		if (!Directory.Exists(tScreenshotFolder))
		{
			Directory.CreateDirectory(tScreenshotFolder);
		}
		return BehResult.Continue;
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x0014A5E1 File Offset: 0x001487E1
	internal static string getScreenshotFolder(string pLanguage)
	{
		return "GenAssets/Windows/" + pLanguage;
	}
}
