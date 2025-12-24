using System;
using ai.behaviours;

// Token: 0x02000390 RID: 912
public class BehCheckPlotBase : BehaviourActionActor
{
	// Token: 0x060021A6 RID: 8614 RVA: 0x0011D10A File Offset: 0x0011B30A
	public override bool shouldRetry(Actor pActor)
	{
		if (base.shouldRetry(pActor))
		{
			return true;
		}
		if (pActor.hasPlot())
		{
			PlotRetryAction plot_retry_action = pActor.plot.getAsset().getPlotGroup().plot_retry_action;
			if (plot_retry_action != null && plot_retry_action())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x0011D145 File Offset: 0x0011B345
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.uses_plots = true;
		this.uses_clans = true;
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x0011D15B File Offset: 0x0011B35B
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasClan())
		{
			return BehResult.Stop;
		}
		if (!pActor.plot.isActive())
		{
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x0011D178 File Offset: 0x0011B378
	protected bool isBasePlotCheckOk(Actor pActor)
	{
		if (!pActor.hasPlot())
		{
			return false;
		}
		if (!pActor.isKingdomCiv())
		{
			return false;
		}
		Plot tPlot = pActor.plot;
		if (!tPlot.isActive())
		{
			return false;
		}
		PlotAsset tPlotAsset = tPlot.getAsset();
		if (!tPlotAsset.isAllowedByWorldLaws())
		{
			return false;
		}
		PlotCheckerDelegate tChecker = tPlotAsset.check_should_continue;
		return tChecker == null || tChecker(pActor);
	}
}
