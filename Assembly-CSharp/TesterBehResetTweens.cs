using System;
using ai.behaviours;

// Token: 0x020004D2 RID: 1234
public class TesterBehResetTweens : BehaviourActionTester
{
	// Token: 0x060029F6 RID: 10742 RVA: 0x0014A4FC File Offset: 0x001486FC
	public override BehResult execute(AutoTesterBot pObject)
	{
		Tooltip.tweenTime = 0f;
		PremiumUnlockAnimation.scaleTime = 0f;
		PremiumUnlockAnimation.delayTime = 0f;
		PowersTab.scale_time = 0f;
		PowersTab.buttonScaleTime = 0f;
		ButtonAnimation.scaleTime = 0f;
		ButtonResource.scaleTime = 0f;
		UiButtonHoverAnimation.scaleTime = 0f;
		return BehResult.Continue;
	}
}
