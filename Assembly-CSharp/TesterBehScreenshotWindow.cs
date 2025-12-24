using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class TesterBehScreenshotWindow : BehaviourActionTester
{
	// Token: 0x06002A07 RID: 10759 RVA: 0x0014A9D4 File Offset: 0x00148BD4
	public override BehResult execute(AutoTesterBot pObject)
	{
		ScrollWindow currentWindow = ScrollWindow.getCurrentWindow();
		string tWindowId = currentWindow.screen_id;
		string tContentPosition = ((int)currentWindow.transform.FindRecursive("Content").gameObject.GetComponent<RectTransform>().localPosition.y).ToString("D4");
		string tFolder = TesterBehScreenshotFolder.getScreenshotFolder(LocalizedTextManager.instance.language);
		Console.WriteLine(string.Concat(new string[]
		{
			"[",
			Date.TimeNow(),
			"] Screenshotting window: ",
			tWindowId,
			" to ",
			tFolder,
			"/",
			tWindowId,
			"_",
			tContentPosition,
			".png"
		}));
		ScreenCapture.CaptureScreenshot(string.Concat(new string[]
		{
			tFolder,
			"/",
			tWindowId,
			"_",
			tContentPosition,
			"_000.png"
		}));
		return BehResult.Continue;
	}
}
