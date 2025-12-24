using System;
using ai.behaviours;

// Token: 0x02000391 RID: 913
public class BehCheckPlotIsOk : BehCheckPlotBase
{
	// Token: 0x060021AB RID: 8619 RVA: 0x0011D1D9 File Offset: 0x0011B3D9
	public override BehResult execute(Actor pActor)
	{
		if (!base.isBasePlotCheckOk(pActor))
		{
			pActor.leavePlot();
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}
}
