using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020006AE RID: 1710
public class LocusElement : ChainElement, IDropHandler, IEventSystemHandler
{
	// Token: 0x060036AB RID: 13995 RVA: 0x0018C4F2 File Offset: 0x0018A6F2
	protected override void create()
	{
		base.create();
		this.is_editor_button = false;
		this._animation_amplifier = this.effect_amplifier.GetComponent<SpriteAnimation>();
		this._animation_amplifier_bad = this.effect_locus_amplifier_bad.GetComponent<SpriteAnimation>();
	}

	// Token: 0x060036AC RID: 13996 RVA: 0x0018C524 File Offset: 0x0018A724
	protected override void Update()
	{
		base.Update();
		if (this.isAmplifier())
		{
			if (this._animation_amplifier.isActiveAndEnabled)
			{
				this._animation_amplifier.update(Time.deltaTime);
			}
			if (this._animation_amplifier_bad.isActiveAndEnabled)
			{
				this._animation_amplifier_bad.update(Time.deltaTime);
			}
		}
	}

	// Token: 0x060036AD RID: 13997 RVA: 0x0018C579 File Offset: 0x0018A779
	private void click()
	{
		if (!base.gene.can_drop_and_grab)
		{
			return;
		}
		this._locus_click_event(this);
		this.checkSprite();
		if (!InputHelpers.mouseSupported)
		{
			base.GetComponent<TipButton>().hoverAction();
		}
	}

	// Token: 0x060036AE RID: 13998 RVA: 0x0018C5B2 File Offset: 0x0018A7B2
	private void clearLocus()
	{
		this._locus_click_event(null);
		this.checkSprite();
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x0018C5C8 File Offset: 0x0018A7C8
	private void checkSprite()
	{
		bool flag = this.isEmptyLocus();
		bool tIsAmplifier = this.isAmplifier();
		bool tLocusBgVisible = !flag && !tIsAmplifier;
		bool tNextToBad = this._chromosome.isNextToBad(this._locus_x, this._locus_y);
		if (tIsAmplifier)
		{
			if (tNextToBad)
			{
				this.effect_amplifier.gameObject.SetActive(false);
				this.effect_locus_amplifier_bad.gameObject.SetActive(true);
			}
			else
			{
				this.effect_amplifier.gameObject.SetActive(true);
				this.effect_locus_amplifier_bad.gameObject.SetActive(false);
			}
		}
		else
		{
			this.effect_amplifier.gameObject.SetActive(false);
			this.effect_locus_amplifier_bad.gameObject.SetActive(false);
		}
		if (this.shouldBeBadLocus())
		{
			this.sprite_background.sprite = this.sprite_locus_bg_bad;
		}
		else if (this.shouldBeGoldenLocus())
		{
			this.sprite_background.sprite = this.sprite_locus_bg_synergy;
		}
		else
		{
			this.sprite_background.sprite = this.sprite_locus_bg_normal;
		}
		this.sprite_background.gameObject.SetActive(tLocusBgVisible);
		this.checkChainsColors();
		if (flag || tIsAmplifier)
		{
			this._gene_button.gameObject.SetActive(false);
		}
		else
		{
			this._gene_button.gameObject.SetActive(true);
			this._gene_button.load(base.gene);
			this._gene_button.is_editor_button = true;
			this._gene_button.locusChild(new UnityAction(this.click), this.locus_index);
		}
		if (this.isAmplifier())
		{
			base.transform.localScale = new Vector3(this._super_size, this._super_size, this._super_size);
		}
		else
		{
			base.transform.localScale = new Vector3(this._normal_size, this._normal_size, this._normal_size);
		}
		base.GetComponent<TipButton>().setDefaultScale(base.transform.localScale);
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x0018C799 File Offset: 0x0018A999
	private bool shouldBeBadChainSide(int pX, int pY, int pOffsetX, int pOffsetY)
	{
		return this.shouldBeBadChain(pX, pY, pX + pOffsetX, pY + pOffsetY);
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x0018C7AC File Offset: 0x0018A9AC
	private bool shouldBeBadChain(int pX, int pY, int pToX, int pToY)
	{
		if (base.gene.is_bad)
		{
			return true;
		}
		GeneAsset tSideAsset = this._chromosome.getGeneAt(pToX, pToY);
		return (tSideAsset != null && tSideAsset.is_bad) || this._chromosome.hasAmplifierBad(pX, pY) || this._chromosome.hasAmplifierBad(pToX, pToY);
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x0018C808 File Offset: 0x0018AA08
	private void checkChainsColors()
	{
		int tX = this._locus_x;
		int tY = this._locus_y;
		Chromosome tChromosome = this._chromosome;
		GeneAsset tGeneLeft = tChromosome.getGeneLeft(tX, tY);
		GeneAsset tGeneRight = tChromosome.getGeneRight(tX, tY);
		GeneAsset tGeneUp = tChromosome.getGeneUp(tX, tY);
		GeneAsset tGeneDown = tChromosome.getGeneDown(tX, tY);
		bool tCanConnectLeft = !tChromosome.hasBoundLeft(tX, tY);
		bool tCanConnectRight = !tChromosome.hasBoundRight(tX, tY);
		bool tCanConnectUp = !tChromosome.hasBoundUp(tX, tY);
		bool tCanConnectDown = !tChromosome.hasBoundDown(tX, tY);
		bool tSynergyLeft = tChromosome.hasSynergyConnectionLeft(tX, tY);
		bool tSynergyRight = tChromosome.hasSynergyConnectionRight(tX, tY);
		bool tSynergyUp = tChromosome.hasSynergyConnectionUp(tX, tY);
		bool tSynergyDown = tChromosome.hasSynergyConnectionDown(tX, tY);
		if (!tSynergyLeft)
		{
			base.hideChain(this.chain_left);
		}
		else if (this.shouldBeBadChain(tX, tY, tX - 1, tY))
		{
			base.showChain(this.chain_left, true, base.gene.genetic_code_left, new Color?(NucleobaseHelper.color_bad));
		}
		else if (tChromosome.isForcedSynergyLeft(tX, tY))
		{
			base.showChain(this.chain_left, true, base.gene.genetic_code_left, null);
		}
		else
		{
			base.showChain(this.chain_left, true, tGeneLeft.genetic_code_right, null);
		}
		if (!tSynergyRight)
		{
			base.hideChain(this.chain_right);
		}
		else if (this.shouldBeBadChain(tX, tY, tX + 1, tY))
		{
			base.showChain(this.chain_right, true, base.gene.genetic_code_right, new Color?(NucleobaseHelper.color_bad));
		}
		else if (tChromosome.isForcedSynergyRight(tX, tY))
		{
			base.showChain(this.chain_right, true, base.gene.genetic_code_right, null);
		}
		else
		{
			base.showChain(this.chain_right, true, tGeneRight.genetic_code_left, null);
		}
		if (!tSynergyUp)
		{
			base.hideChain(this.chain_up);
		}
		else if (this.shouldBeBadChain(tX, tY, tX, tY - 1))
		{
			base.showChain(this.chain_up, true, base.gene.genetic_code_up, new Color?(NucleobaseHelper.color_bad));
		}
		else if (tChromosome.isForcedSynergyUp(tX, tY))
		{
			base.showChain(this.chain_up, true, base.gene.genetic_code_up, null);
		}
		else
		{
			base.showChain(this.chain_up, true, tGeneUp.genetic_code_down, null);
		}
		if (!tSynergyDown)
		{
			base.hideChain(this.chain_down);
		}
		else if (this.shouldBeBadChain(tX, tY, tX, tY + 1))
		{
			base.showChain(this.chain_down, true, base.gene.genetic_code_down, new Color?(NucleobaseHelper.color_bad));
		}
		else if (tChromosome.isForcedSynergyDown(tX, tY))
		{
			base.showChain(this.chain_down, true, base.gene.genetic_code_down, null);
		}
		else
		{
			base.showChain(this.chain_down, true, tGeneDown.genetic_code_up, null);
		}
		this.showDot(this._dot_left, tCanConnectLeft && !tSynergyLeft, base.gene.genetic_code_left);
		this.showDot(this._dot_right, tCanConnectRight && !tSynergyRight, base.gene.genetic_code_right);
		this.showDot(this._dot_up, tCanConnectUp && !tSynergyUp, base.gene.genetic_code_up);
		this.showDot(this._dot_down, tCanConnectDown && !tSynergyDown, base.gene.genetic_code_down);
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x0018CB78 File Offset: 0x0018AD78
	public override void load(GeneAsset pAsset)
	{
		throw new NotImplementedException("Use show instead");
	}

	// Token: 0x060036B4 RID: 14004 RVA: 0x0018CB84 File Offset: 0x0018AD84
	internal override void load(string pElementID)
	{
		throw new NotImplementedException("Use show instead");
	}

	// Token: 0x060036B5 RID: 14005 RVA: 0x0018CB90 File Offset: 0x0018AD90
	public void show(int pLocusIndex, Chromosome pChromosome, GeneAsset pGene, LocusType pLocusType, LocusClickEvent pLocusClickEvent)
	{
		base.load(pGene);
		this.clearActions();
		this._chromosome = pChromosome;
		this.locus_index = pLocusIndex;
		ValueTuple<int, int> xyfromIndex = this._chromosome.getXYFromIndex(pLocusIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		this._locus_x = tX;
		this._locus_y = tY;
		this._locus_click_event = pLocusClickEvent;
		this.locus_type = pLocusType;
		base.gameObject.name = "Locus " + base.gene.id;
		base.colorChains();
		this.checkSprite();
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x0018CC1B File Offset: 0x0018AE1B
	protected override void clearActions()
	{
		base.clearActions();
		this._chromosome_updated_event = null;
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x0018CC2C File Offset: 0x0018AE2C
	public bool shouldBeBadLocus()
	{
		bool is_bad = base.gene.is_bad;
		bool tNextToBad = this._chromosome.isNextToBad(this._locus_x, this._locus_y);
		return is_bad || tNextToBad;
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x0018CC5E File Offset: 0x0018AE5E
	public bool shouldBeGoldenLocus()
	{
		return this.isAmplifier() || base.gene.synergy_sides_always || this._chromosome.hasFullSynergy(this.locus_index);
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x0018CC8F File Offset: 0x0018AE8F
	public bool isAmplifier()
	{
		return this.locus_type == LocusType.Amplifier;
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x0018CC9A File Offset: 0x0018AE9A
	public bool isAmplifierBad()
	{
		return this._chromosome.hasAmplifierBad(this._locus_x, this._locus_y);
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x0018CCB3 File Offset: 0x0018AEB3
	public bool isEmptyLocus()
	{
		return this.locus_type == LocusType.Empty;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x0018CCBE File Offset: 0x0018AEBE
	protected override void fillTooltipData(GeneAsset pElement)
	{
		Tooltip.show(this, "gene", this.tooltipDataBuilder());
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x0018CCD1 File Offset: 0x0018AED1
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			gene = base.gene,
			locus = this,
			chromosome = this._chromosome
		};
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x0018CCF7 File Offset: 0x0018AEF7
	public bool canAddGene()
	{
		return this._chromosome.canAddToLocus(this.locus_index);
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x0018CD0A File Offset: 0x0018AF0A
	public bool isSpecialLocus()
	{
		return this._chromosome.isSpecialLocus(this.locus_index);
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x0018CD20 File Offset: 0x0018AF20
	public void OnDrop(PointerEventData pEventData)
	{
		if (pEventData.pointerDrag == null)
		{
			return;
		}
		if (this.isAmplifier())
		{
			return;
		}
		if (!Config.hasPremium)
		{
			ScrollWindow.showWindow("premium_menu");
			return;
		}
		GeneButton tGeneButton = pEventData.pointerDrag.GetComponent<GeneButton>();
		if (tGeneButton == null)
		{
			return;
		}
		GeneAsset tGeneAsset = tGeneButton.getElementAsset();
		if (!tGeneAsset.can_drop_and_grab)
		{
			return;
		}
		if (tGeneButton.locus_index > -1)
		{
			GeneAsset tOldAsset = this._chromosome.getGene(this.locus_index);
			this._chromosome.setGene(tOldAsset, tGeneButton.locus_index);
		}
		GeneAsset tOldGene = base.getGeneAsset();
		this._chromosome.setGene(tGeneAsset, this.locus_index);
		this._chromosome_updated_event();
		SelectedMetas.selected_subspecies.eventGMO();
		if (tGeneAsset != tOldGene)
		{
			AchievementLibrary.engineered_evolution.check(null);
		}
		this.fillTooltipData(base.gene);
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x0018CDF4 File Offset: 0x0018AFF4
	public void addChromosomeUpdatedEvent(Action pChromosomeUpdatedEvent)
	{
		this._chromosome_updated_event = pChromosomeUpdatedEvent;
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x0018CDFD File Offset: 0x0018AFFD
	protected void showDot(LocusDot pChainDot, bool pShow, char pGeneticCode)
	{
		pChainDot.gameObject.SetActive(pShow);
		if (pShow)
		{
			pChainDot.colorDot(pGeneticCode);
		}
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x0018CE15 File Offset: 0x0018B015
	protected override void startSignal()
	{
		AchievementLibrary.genes_explorer.checkBySignal(null);
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x0018CE22 File Offset: 0x0018B022
	protected override bool unlockElement()
	{
		bool result = base.unlockElement();
		base.isElementUnlocked();
		return result;
	}

	// Token: 0x04002877 RID: 10359
	private Chromosome _chromosome;

	// Token: 0x04002878 RID: 10360
	private LocusClickEvent _locus_click_event;

	// Token: 0x04002879 RID: 10361
	private Action _chromosome_updated_event;

	// Token: 0x0400287A RID: 10362
	private LocusType locus_type;

	// Token: 0x0400287B RID: 10363
	public Image sprite_background;

	// Token: 0x0400287C RID: 10364
	public Image effect_amplifier;

	// Token: 0x0400287D RID: 10365
	public Image effect_locus_amplifier_bad;

	// Token: 0x0400287E RID: 10366
	public Sprite sprite_locus_bg_normal;

	// Token: 0x0400287F RID: 10367
	public Sprite sprite_locus_bg_synergy;

	// Token: 0x04002880 RID: 10368
	public Sprite sprite_locus_bg_bad;

	// Token: 0x04002881 RID: 10369
	[SerializeField]
	private LocusDot _dot_left;

	// Token: 0x04002882 RID: 10370
	[SerializeField]
	private LocusDot _dot_right;

	// Token: 0x04002883 RID: 10371
	[SerializeField]
	private LocusDot _dot_up;

	// Token: 0x04002884 RID: 10372
	[SerializeField]
	private LocusDot _dot_down;

	// Token: 0x04002885 RID: 10373
	private float _normal_size = 0.8f;

	// Token: 0x04002886 RID: 10374
	private float _super_size = 0.8f;

	// Token: 0x04002887 RID: 10375
	private int _locus_x;

	// Token: 0x04002888 RID: 10376
	private int _locus_y;

	// Token: 0x04002889 RID: 10377
	private SpriteAnimation _animation_amplifier;

	// Token: 0x0400288A RID: 10378
	private SpriteAnimation _animation_amplifier_bad;

	// Token: 0x0400288B RID: 10379
	[SerializeField]
	private GeneButton _gene_button;
}
