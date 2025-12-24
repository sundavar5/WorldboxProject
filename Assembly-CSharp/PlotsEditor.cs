using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200072C RID: 1836
public class PlotsEditor : AugmentationsEditor<PlotAsset, PlotButton, PlotEditorButton, PlotCategoryAsset, PlotGroupElement, IPlotsWindow, IPlotsEditor>, IPlotsEditor, IAugmentationsEditor
{
	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06003A78 RID: 14968 RVA: 0x0019DD89 File Offset: 0x0019BF89
	protected override List<PlotCategoryAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.plot_category_library.list;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06003A79 RID: 14969 RVA: 0x0019DD95 File Offset: 0x0019BF95
	protected override PlotAsset edited_marker_augmentation
	{
		get
		{
			return null;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06003A7A RID: 14970 RVA: 0x0019DD98 File Offset: 0x0019BF98
	protected override List<PlotAsset> all_augmentations_list
	{
		get
		{
			return AssetManager.plots_library.list;
		}
	}

	// Token: 0x06003A7B RID: 14971 RVA: 0x0019DDA4 File Offset: 0x0019BFA4
	protected override void metaAugmentationClick(PlotEditorButton pButton)
	{
		if (!base.isAugmentationAvailable(pButton.augmentation_button))
		{
			return;
		}
		PlotButton tButton = pButton.augmentation_button;
		bool flag = this.hasAugmentation(tButton);
		if (this.getCurrentActor().hasPlot())
		{
			this.removeAugmentation(tButton);
		}
		if (!flag)
		{
			this.addAugmentation(tButton);
		}
		base.metaAugmentationClick(pButton);
	}

	// Token: 0x06003A7C RID: 14972 RVA: 0x0019DDF4 File Offset: 0x0019BFF4
	protected override void createButton(PlotAsset pElement, PlotGroupElement pGroup)
	{
		if (!pElement.show_in_meta_editor)
		{
			return;
		}
		PlotEditorButton tEditorButton = Object.Instantiate<PlotEditorButton>(this.prefab_editor_augmentation, pGroup.augmentation_buttons_transform);
		tEditorButton.augmentation_button.is_editor_button = true;
		tEditorButton.augmentation_button.load(pElement);
		this.all_augmentation_buttons.Add(tEditorButton);
		pGroup.augmentation_buttons.Add(tEditorButton);
		tEditorButton.augmentation_button.button.onClick.RemoveAllListeners();
		tEditorButton.augmentation_button.button.onClick.AddListener(delegate()
		{
			this.editorButtonClick(tEditorButton);
		});
		tEditorButton.gameObject.SetActive(true);
	}

	// Token: 0x06003A7D RID: 14973 RVA: 0x0019DEC4 File Offset: 0x0019C0C4
	protected override bool hasAugmentation(PlotButton pButton)
	{
		Actor tActor = this.getCurrentActor();
		return tActor.hasPlot() && !(tActor.plot.getAsset().id != pButton.getElementId());
	}

	// Token: 0x06003A7E RID: 14974 RVA: 0x0019DF04 File Offset: 0x0019C104
	protected override bool addAugmentation(PlotButton pButton)
	{
		Actor tActor = this.getCurrentActor();
		tActor.addStatusEffect("voices_in_my_head", 0f, true);
		if (tActor.hasPlot())
		{
			World.world.plots.cancelPlot(tActor.plot);
			tActor.plot = null;
		}
		PlotAsset tAsset = pButton.getElementAsset();
		if (!tAsset.canBeDoneByRole(tActor))
		{
			WorldTip.showNowTop("plot_force_not_possible", true);
			return false;
		}
		if (tAsset.check_can_be_forced != null && !tAsset.check_can_be_forced(tActor))
		{
			WorldTip.showNowTop("plot_force_bad_conditions", true);
			return false;
		}
		if (!World.world.plots.tryStartPlot(tActor, tAsset, true))
		{
			WorldTip.showNowTop("plot_force_failed", true);
			return false;
		}
		WorldTip.showNowTop("plot_force_started", true);
		tActor.cancelAllBeh();
		tActor.setTask("check_plot", true, false, false);
		return true;
	}

	// Token: 0x06003A7F RID: 14975 RVA: 0x0019DFCF File Offset: 0x0019C1CF
	protected override bool removeAugmentation(PlotButton pButton)
	{
		this.getCurrentActor().setPlot(null);
		return true;
	}

	// Token: 0x06003A80 RID: 14976 RVA: 0x0019DFDE File Offset: 0x0019C1DE
	protected override void showActiveButtons()
	{
	}

	// Token: 0x06003A81 RID: 14977 RVA: 0x0019DFE0 File Offset: 0x0019C1E0
	protected override void startSignal()
	{
		AchievementLibrary.plots_explorer.checkBySignal(null);
	}

	// Token: 0x06003A82 RID: 14978 RVA: 0x0019DFED File Offset: 0x0019C1ED
	protected override bool isAugmentationExists(string pId)
	{
		return AssetManager.plots_library.has(pId);
	}

	// Token: 0x06003A83 RID: 14979 RVA: 0x0019DFFA File Offset: 0x0019C1FA
	private Actor getCurrentActor()
	{
		return SelectedUnit.unit;
	}
}
