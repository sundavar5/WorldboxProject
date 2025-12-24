using System;
using ai.behaviours;

// Token: 0x020004CC RID: 1228
public class TesterBehRandomWindowTab : BehaviourActionTester
{
	// Token: 0x060029EC RID: 10732 RVA: 0x0014A268 File Offset: 0x00148468
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (ScrollWindow.isAnimationActive())
		{
			pObject.wait = 0.1f;
			return BehResult.RepeatStep;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return BehResult.Stop;
		}
		if (ScrollWindow.getCurrentWindow() == null)
		{
			return BehResult.Stop;
		}
		string tDirection = Randy.randomBool() ? "window_tab_previous" : "window_tab_next";
		HotkeyAsset tAsset = AssetManager.hotkey_library.get(tDirection);
		if (tAsset == null)
		{
			return BehResult.Stop;
		}
		pObject.wait = 0.1f;
		HotkeyAction just_pressed_action = tAsset.just_pressed_action;
		if (just_pressed_action != null)
		{
			just_pressed_action(tAsset);
		}
		return BehResult.Continue;
	}
}
