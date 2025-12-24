using System;
using ai.behaviours;

// Token: 0x0200038F RID: 911
public class BehCheckPlot : BehCheckPlotBase
{
	// Token: 0x060021A4 RID: 8612 RVA: 0x0011D0D7 File Offset: 0x0011B2D7
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasPlot())
		{
			return BehResult.Stop;
		}
		if (!base.isBasePlotCheckOk(pActor))
		{
			pActor.leavePlot();
			return BehResult.Stop;
		}
		return base.forceTask(pActor, "progress_plot", true, false);
	}
}
