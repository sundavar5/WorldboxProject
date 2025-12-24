using System;
using ai.behaviours;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B9 RID: 1209
public class TesterBehClickRandomButton : BehaviourActionTester
{
	// Token: 0x060029C5 RID: 10693 RVA: 0x001498BC File Offset: 0x00147ABC
	public TesterBehClickRandomButton(Type pButtonType = null)
	{
		this._type = pButtonType;
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x001498CC File Offset: 0x00147ACC
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (ScrollWindow.isAnimationActive())
		{
			return BehResult.RepeatStep;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return BehResult.Stop;
		}
		ScrollWindow tCurrentWindow = ScrollWindow.getCurrentWindow();
		if (tCurrentWindow == null)
		{
			return BehResult.Stop;
		}
		Component[] tTypeComponents = tCurrentWindow.GetComponentsInChildren(this._type);
		if (tTypeComponents.Length == 0)
		{
			return BehResult.Stop;
		}
		Component tRandomComponent = Randy.getRandom<Component>(tTypeComponents);
		if (tRandomComponent == null)
		{
			return BehResult.Stop;
		}
		Button tButton;
		if (!tRandomComponent.TryGetComponent<Button>(out tButton))
		{
			return BehResult.Stop;
		}
		pObject.wait = 0.5f;
		Button.ButtonClickedEvent onClick = tButton.onClick;
		if (onClick != null)
		{
			onClick.Invoke();
		}
		return BehResult.Continue;
	}

	// Token: 0x04001F2C RID: 7980
	private Type _type;
}
