using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A6 RID: 1702
[RequireComponent(typeof(CanvasGroup))]
public class ChainElement : TraitButton<GeneAsset>
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x0600366A RID: 13930 RVA: 0x0018B915 File Offset: 0x00189B15
	protected GeneAsset gene
	{
		get
		{
			return this.augmentation_asset;
		}
	}

	// Token: 0x0600366B RID: 13931 RVA: 0x0018B91D File Offset: 0x00189B1D
	public GeneAsset getGeneAsset()
	{
		return this.gene;
	}

	// Token: 0x0600366C RID: 13932 RVA: 0x0018B925 File Offset: 0x00189B25
	public override void load(GeneAsset pAsset)
	{
		base.load(pAsset);
		base.gameObject.name = this.gene.id;
		this.colorChains();
	}

	// Token: 0x0600366D RID: 13933 RVA: 0x0018B94C File Offset: 0x00189B4C
	public void colorChains()
	{
		if (!this.gene.show_genepool_nucleobases)
		{
			this.hideChains();
			return;
		}
		this.showChain(this.chain_left, true, this.gene.genetic_code_left, null);
		this.showChain(this.chain_right, true, this.gene.genetic_code_right, null);
		this.showChain(this.chain_up, true, this.gene.genetic_code_up, null);
		this.showChain(this.chain_down, true, this.gene.genetic_code_down, null);
	}

	// Token: 0x0600366E RID: 13934 RVA: 0x0018B9F1 File Offset: 0x00189BF1
	protected void hideChains()
	{
		this.hideChain(this.chain_left);
		this.hideChain(this.chain_right);
		this.hideChain(this.chain_up);
		this.hideChain(this.chain_down);
	}

	// Token: 0x0600366F RID: 13935 RVA: 0x0018BA23 File Offset: 0x00189C23
	protected void showChain(Image pChainImage, bool pShow, char pGeneticCode, Color? pColor = null)
	{
		pChainImage.gameObject.SetActive(pShow);
		if (pColor != null)
		{
			this.colorChain(pChainImage, pColor.Value);
			return;
		}
		if (pShow)
		{
			this.colorChain(pChainImage, NucleobaseHelper.getColor(pGeneticCode, false));
		}
	}

	// Token: 0x06003670 RID: 13936 RVA: 0x0018BA5A File Offset: 0x00189C5A
	protected void hideChain(Image pChain)
	{
		pChain.gameObject.SetActive(false);
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x0018BA68 File Offset: 0x00189C68
	protected void colorChain(Image pChain, Color pColor)
	{
		pChain.color = pColor;
	}

	// Token: 0x04002859 RID: 10329
	public Image chain_left;

	// Token: 0x0400285A RID: 10330
	public Image chain_right;

	// Token: 0x0400285B RID: 10331
	public Image chain_up;

	// Token: 0x0400285C RID: 10332
	public Image chain_down;

	// Token: 0x0400285D RID: 10333
	internal int locus_index = -1;
}
