using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityPools;

// Token: 0x020003A4 RID: 932
public class BehTryNewPlot : BehaviourActionActor
{
	// Token: 0x060021E5 RID: 8677 RVA: 0x0011E0A8 File Offset: 0x0011C2A8
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasPlot())
		{
			return BehResult.Continue;
		}
		if (pActor.isFighting())
		{
			return BehResult.Continue;
		}
		BehResult result;
		using (ListPool<PlotAsset> tTempPotPlots = new ListPool<PlotAsset>())
		{
			this.fillRandomPlots(pActor, tTempPotPlots);
			if (tTempPotPlots.Count == 0)
			{
				result = BehResult.Continue;
			}
			else
			{
				this.startPlotFromTheList(pActor, tTempPotPlots);
				result = BehResult.Continue;
			}
		}
		return result;
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x0011E10C File Offset: 0x0011C30C
	private void fillRandomPlots(Actor pActor, ListPool<PlotAsset> pPotPlots)
	{
		this.fillPlotsToTry(pActor, AssetManager.plots_library.basic_plots, pPotPlots);
		if (pActor.hasReligion() && WorldLawLibrary.world_law_rites.isEnabled())
		{
			this.fillPlotsToTry(pActor, pActor.religion.possible_rites, pPotPlots);
		}
		pPotPlots.Shuffle<PlotAsset>();
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x0011E158 File Offset: 0x0011C358
	private void fillPlotsToTry(Actor pActor, List<PlotAsset> pPlotList, ListPool<PlotAsset> pPotPossiblePlots)
	{
		for (int i = 0; i < pPlotList.Count; i++)
		{
			PlotAsset tAsset = pPlotList[i];
			if (tAsset.checkIsPossible(pActor, true))
			{
				pPotPossiblePlots.AddTimes(tAsset.pot_rate, tAsset);
			}
		}
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x0011E198 File Offset: 0x0011C398
	private void startPlotFromTheList(Actor pActor, ListPool<PlotAsset> pPotList)
	{
		HashSet<PlotAsset> tChecked = UnsafeCollectionPool<HashSet<PlotAsset>, PlotAsset>.Get();
		for (int i = 0; i < pPotList.Count; i++)
		{
			PlotAsset tPlotAsset = pPotList[i];
			if (!tChecked.Contains(tPlotAsset))
			{
				if (BehaviourActionBase<Actor>.world.plots.tryStartPlot(pActor, tPlotAsset, true))
				{
					break;
				}
				tChecked.Add(tPlotAsset);
			}
		}
		UnsafeCollectionPool<HashSet<PlotAsset>, PlotAsset>.Release(tChecked);
	}
}
