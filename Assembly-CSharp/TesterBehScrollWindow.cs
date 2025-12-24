using System;
using ai.behaviours;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004DB RID: 1243
public class TesterBehScrollWindow : BehaviourActionTester
{
	// Token: 0x06002A09 RID: 10761 RVA: 0x0014AAC8 File Offset: 0x00148CC8
	public override BehResult execute(AutoTesterBot pObject)
	{
		ScrollWindow tCurrentWindow = ScrollWindow.getCurrentWindow();
		string tWindowId = tCurrentWindow.screen_id;
		if (TesterBehScrollWindow.skipWindows.IndexOf(tWindowId) > -1)
		{
			return BehResult.Continue;
		}
		Transform tScrollbarTransform = tCurrentWindow.transform.FindRecursive("Scrollbar Vertical");
		if (!tScrollbarTransform.gameObject.activeInHierarchy)
		{
			return BehResult.Continue;
		}
		Scrollbar tScrollbar = tScrollbarTransform.gameObject.GetComponent<Scrollbar>();
		float tScrollbarValue = tScrollbar.value;
		float tScrollbarSize = tScrollbar.size;
		if (tScrollbarSize < 0.05f)
		{
			return BehResult.Continue;
		}
		if (tScrollbarSize > 0.95f)
		{
			return BehResult.Continue;
		}
		if (tScrollbarValue > 0.1f)
		{
			tScrollbarValue -= tScrollbarSize;
			if (tScrollbarValue < 0f)
			{
				tScrollbarValue = 0f;
			}
			tScrollbar.value = tScrollbarValue;
			return BehResult.RestartTask;
		}
		return BehResult.Continue;
	}

	// Token: 0x04001F58 RID: 8024
	private static string[] skipWindows = new string[]
	{
		"saves_list",
		"patch_log"
	};
}
