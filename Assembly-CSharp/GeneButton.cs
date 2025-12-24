using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020006A8 RID: 1704
public class GeneButton : ChainElement
{
	// Token: 0x0600367B RID: 13947 RVA: 0x0018BBD9 File Offset: 0x00189DD9
	protected override void create()
	{
		base.create();
		this.button.onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x0600367C RID: 13948 RVA: 0x0018BBFD File Offset: 0x00189DFD
	private void click()
	{
		GeneAssetClickEvent gene_asset_click_event = this._gene_asset_click_event;
		if (gene_asset_click_event != null)
		{
			gene_asset_click_event(base.gene);
		}
		if (!InputHelpers.mouseSupported)
		{
			base.GetComponent<TipButton>().hoverAction();
		}
	}

	// Token: 0x0600367D RID: 13949 RVA: 0x0018BC30 File Offset: 0x00189E30
	protected override void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		base.onStartDrag(pOriginalElement);
		this._petri_bg.SetActive(false);
		base.colorChains();
		bool tShowLocked = !this.augmentation_asset.isUnlocked();
		this.locked_bg.gameObject.SetActive(tShowLocked);
	}

	// Token: 0x0600367E RID: 13950 RVA: 0x0018BC78 File Offset: 0x00189E78
	internal void locusChild(UnityAction pAction, int pLocusIndex)
	{
		base.hideChains();
		this.button.onClick.RemoveListener(new UnityAction(this.click));
		this.button.onClick.RemoveListener(pAction);
		this.button.onClick.AddListener(pAction);
		this.locus_index = pLocusIndex;
		this.disableTooltip();
	}

	// Token: 0x0600367F RID: 13951 RVA: 0x0018BCD6 File Offset: 0x00189ED6
	protected override void fillTooltipData(GeneAsset pElement)
	{
		Tooltip.show(this, "gene", this.tooltipDataBuilder());
	}

	// Token: 0x06003680 RID: 13952 RVA: 0x0018BCE9 File Offset: 0x00189EE9
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			gene = base.gene
		};
	}

	// Token: 0x06003681 RID: 13953 RVA: 0x0018BCFC File Offset: 0x00189EFC
	public void addGeneClickCallback(GeneAssetClickEvent pAction)
	{
		this._gene_asset_click_event = (GeneAssetClickEvent)Delegate.Combine(this._gene_asset_click_event, pAction);
	}

	// Token: 0x06003682 RID: 13954 RVA: 0x0018BD15 File Offset: 0x00189F15
	public void removeGeneClickCallback(GeneAssetClickEvent pAction)
	{
		this._gene_asset_click_event = (GeneAssetClickEvent)Delegate.Remove(this._gene_asset_click_event, pAction);
	}

	// Token: 0x04002863 RID: 10339
	[SerializeField]
	private GameObject _petri_bg;

	// Token: 0x04002864 RID: 10340
	private GeneAssetClickEvent _gene_asset_click_event;
}
