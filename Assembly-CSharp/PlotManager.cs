using System;
using System.Collections.Generic;

// Token: 0x020002D0 RID: 720
public class PlotManager : MetaSystemManager<Plot, PlotData>
{
	// Token: 0x06001AB6 RID: 6838 RVA: 0x000F9C77 File Offset: 0x000F7E77
	public PlotManager()
	{
		this.type_id = "plot";
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x000F9C8A File Offset: 0x000F7E8A
	public override Plot loadObject(PlotData pData)
	{
		if (AssetManager.plots_library.get(pData.plot_type_id) == null)
		{
			return null;
		}
		return base.loadObject(pData);
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x000F9CA7 File Offset: 0x000F7EA7
	public override void startCollectHistoryData()
	{
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x000F9CA9 File Offset: 0x000F7EA9
	public override void clearLastYearStats()
	{
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x000F9CAB File Offset: 0x000F7EAB
	public void cancelPlot(Plot pPlot)
	{
		pPlot.finishPlot(PlotState.Cancelled, null);
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x000F9CB8 File Offset: 0x000F7EB8
	public bool tryStartPlot(Actor pActor, PlotAsset pPlotAsset, bool pForced = true)
	{
		bool tStarted;
		if (pPlotAsset.try_to_start_advanced != null)
		{
			tStarted = pPlotAsset.try_to_start_advanced(pActor, pPlotAsset, pForced);
		}
		else
		{
			World.world.plots.newPlot(pActor, pPlotAsset, pForced);
			tStarted = true;
		}
		return tStarted;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x000F9CF4 File Offset: 0x000F7EF4
	public Plot newPlot(Actor pAuthor, PlotAsset pAsset, bool pForced)
	{
		World.world.game_stats.data.plotsStarted += 1L;
		World.world.map_stats.plotsStarted += 1L;
		Plot plot = base.newObject();
		plot.newPlot(pAuthor, pAsset, pForced);
		plot.data.founder_name = pAuthor.getName();
		if (!pForced)
		{
			pAuthor.spendMoney(pAsset.money_cost);
		}
		return plot;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x000F9D65 File Offset: 0x000F7F65
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateProgress(pElapsed);
		this.updateAnimations(pElapsed);
		this.checkForgottenPlots();
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x000F9D84 File Offset: 0x000F7F84
	private void updateProgress(float pElapsed)
	{
		if (World.world.isPaused())
		{
			return;
		}
		foreach (Plot plot in this)
		{
			plot.updateProgress(pElapsed);
		}
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x000F9DD8 File Offset: 0x000F7FD8
	private void updateAnimations(float pElapsed)
	{
		foreach (Plot plot in this)
		{
			plot.updateAnimations(pElapsed);
		}
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x000F9E20 File Offset: 0x000F8020
	private void checkForgottenPlots()
	{
		foreach (Plot tPlot in this)
		{
			if (tPlot.last_update_progress != 0.0 && tPlot.isActive() && (World.world.getWorldTimeElapsedSince(tPlot.last_update_progress) > SimGlobals.m.forgotten_plot_time || tPlot.isAuthorDead()))
			{
				this.cancelPlot(tPlot);
			}
		}
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x000F9EA8 File Offset: 0x000F80A8
	public override void removeObject(Plot pPlot)
	{
		base.removeObject(pPlot);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x000F9EB4 File Offset: 0x000F80B4
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Plot tPlot = tUnit.plot;
			if (tPlot != null && tPlot.isDirtyUnits())
			{
				tPlot.listUnit(tUnit);
			}
		}
		foreach (Plot tPlot2 in this)
		{
			if (tPlot2.isActive() && tPlot2.isDirtyUnits() && tPlot2.units.Count == 0)
			{
				this.cancelPlot(tPlot2);
			}
		}
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x000F9F68 File Offset: 0x000F8168
	public bool isPlotTypeAlreadyRunning(Actor pActor, PlotAsset pPlotAsset)
	{
		foreach (Plot tPlot in this)
		{
			if (tPlot.isActive() && tPlot.isSameType(pPlotAsset))
			{
				return true;
			}
		}
		return false;
	}
}
