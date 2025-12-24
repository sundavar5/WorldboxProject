using System;

namespace ai.behaviours
{
	// Token: 0x020008F8 RID: 2296
	public enum BehResult
	{
		// Token: 0x040031A5 RID: 12709
		Continue,
		// Token: 0x040031A6 RID: 12710
		Stop,
		// Token: 0x040031A7 RID: 12711
		RepeatStep,
		// Token: 0x040031A8 RID: 12712
		Skip,
		// Token: 0x040031A9 RID: 12713
		StepBack,
		// Token: 0x040031AA RID: 12714
		RestartTask,
		// Token: 0x040031AB RID: 12715
		ActiveTaskReturn,
		// Token: 0x040031AC RID: 12716
		ImmediateRun
	}
}
