using System;
using ai.behaviours;
using UnityEngine.UI;

// Token: 0x020004CB RID: 1227
public class TesterBehRandomMetaSwitch : BehaviourActionTester
{
	// Token: 0x060029EA RID: 10730 RVA: 0x0014A198 File Offset: 0x00148398
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (ScrollWindow.isAnimationActive())
		{
			pObject.wait = 0.1f;
			return BehResult.RepeatStep;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return BehResult.Continue;
		}
		if (!MetaSwitchManager.isSwitcherEnabled())
		{
			return BehResult.Continue;
		}
		BehResult result;
		using (ListPool<MetaSwitchButton> tButtons = new ListPool<MetaSwitchButton>(2))
		{
			tButtons.Add(MetaSwitchManager.getLeftbutton());
			tButtons.Add(MetaSwitchManager.getRightButton());
			tButtons.RemoveAll((MetaSwitchButton pButton) => !pButton.gameObject.activeSelf);
			if (tButtons.Count == 0)
			{
				result = BehResult.Continue;
			}
			else
			{
				MetaSwitchButton random = tButtons.GetRandom<MetaSwitchButton>();
				pObject.wait = 0.2f;
				Button.ButtonClickedEvent onClick = random.button.onClick;
				if (onClick != null)
				{
					onClick.Invoke();
				}
				result = BehResult.Continue;
			}
		}
		return result;
	}
}
