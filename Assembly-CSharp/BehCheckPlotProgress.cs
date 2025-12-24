using System;
using ai.behaviours;

// Token: 0x02000392 RID: 914
public class BehCheckPlotProgress : BehCheckPlotBase
{
	// Token: 0x060021AD RID: 8621 RVA: 0x0011D1F8 File Offset: 0x0011B3F8
	public override BehResult execute(Actor pActor)
	{
		if (!base.isBasePlotCheckOk(pActor))
		{
			pActor.leavePlot();
			return BehResult.Stop;
		}
		Plot plot = pActor.plot;
		plot.updateProgressTarget(pActor, pActor.stats["intelligence"]);
		if (!plot.isActive())
		{
			pActor.leavePlot();
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}
}
