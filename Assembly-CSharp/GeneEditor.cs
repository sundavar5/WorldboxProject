using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006AC RID: 1708
public class GeneEditor : MonoBehaviour
{
	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06003690 RID: 13968 RVA: 0x0018BD36 File Offset: 0x00189F36
	private Subspecies _meta_object
	{
		get
		{
			return SelectedMetas.selected_subspecies;
		}
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x0018BD3D File Offset: 0x00189F3D
	internal void load()
	{
		this.init();
		this.clear();
		this.loadChromosomes(true);
		this.reloadButtons();
		this.recolorGenePoolButtons();
	}

	// Token: 0x06003692 RID: 13970 RVA: 0x0018BD60 File Offset: 0x00189F60
	private void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._window_subspecies = base.GetComponentInParent<SubspeciesWindow>();
		this._pool_elements_chromosomes = new ObjectPoolGenericMono<ChromosomeElement>(this._prefab_chromosome_element, this._transform_chromosomes);
		this._pool_elements_loci = new ObjectPoolGenericMono<LocusElement>(this._prefab_locus_element, this._transform_loci);
		this.loadGeneButtons();
	}

	// Token: 0x06003693 RID: 13971 RVA: 0x0018BDBD File Offset: 0x00189FBD
	private void clear()
	{
		this._pool_elements_chromosomes.clear(true);
		this._pool_elements_loci.clear(true);
		this._selected_chromosome = null;
		this._selected_locus = null;
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x0018BDE5 File Offset: 0x00189FE5
	private void OnEnable()
	{
		this.load();
	}

	// Token: 0x06003695 RID: 13973 RVA: 0x0018BDED File Offset: 0x00189FED
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003696 RID: 13974 RVA: 0x0018BDF5 File Offset: 0x00189FF5
	public void debugRandomizeGenes()
	{
		this._meta_object.addDNAMutationToSeed();
		this._meta_object.generateNucleus();
		this._meta_object.genesChangedEvent();
		this._meta_object.eventGMO();
		this.load();
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x0018BE29 File Offset: 0x0018A029
	public void debugShuffleGenes()
	{
		this._meta_object.unstableGenomeEvent();
		this.load();
	}

	// Token: 0x06003698 RID: 13976 RVA: 0x0018BE3C File Offset: 0x0018A03C
	private void loadChromosomes(bool pSelectFirstChromosome = true)
	{
		foreach (Chromosome tChromosome in this._meta_object.nucleus.chromosomes)
		{
			this._pool_elements_chromosomes.getNext().show(tChromosome, new ChromosomeClickEvent(this.clickChromosome));
		}
		if (pSelectFirstChromosome && this._meta_object.nucleus.chromosomes.Count > 0)
		{
			this.clickChromosome(this._meta_object.nucleus.chromosomes[0]);
		}
	}

	// Token: 0x06003699 RID: 13977 RVA: 0x0018BEE8 File Offset: 0x0018A0E8
	private void recolorGenePoolButtons()
	{
		foreach (GeneButton geneButton in this._dictionary_gene_buttons.Values)
		{
			geneButton.colorChains();
		}
	}

	// Token: 0x0600369A RID: 13978 RVA: 0x0018BF40 File Offset: 0x0018A140
	private void loadGeneButtons()
	{
		foreach (GeneAsset tAsset in AssetManager.gene_library.list)
		{
			if (!tAsset.is_empty)
			{
				GeneButton tGeneButton = Object.Instantiate<GeneButton>(this._prefab_gene_button, this._transform_gene_selector);
				this._dictionary_gene_buttons.Add(tAsset, tGeneButton);
				tGeneButton.load(tAsset);
				tGeneButton.is_editor_button = true;
				tGeneButton.addElementUnlockedAction(new AugmentationUnlockedAction(this.reloadButtons));
				tGeneButton.addGeneClickCallback(new GeneAssetClickEvent(this.clickGeneAssetAction));
				tGeneButton.GetComponent<DraggableLayoutElement>().enabled = tAsset.isAvailable();
			}
		}
	}

	// Token: 0x0600369B RID: 13979 RVA: 0x0018BFFC File Offset: 0x0018A1FC
	public void clickChromosome(Chromosome pChromosome)
	{
		foreach (ChromosomeElement tElement in this._pool_elements_chromosomes.getListTotal())
		{
			if (tElement.gameObject.activeSelf)
			{
				if (tElement.chromosome == pChromosome)
				{
					tElement.image.color = Color.white;
				}
				else
				{
					tElement.image.color = Color.gray;
				}
			}
		}
		this._selected_chromosome = pChromosome;
		this.showGenes(pChromosome);
		this.selectFirstNormalLocus();
	}

	// Token: 0x0600369C RID: 13980 RVA: 0x0018C094 File Offset: 0x0018A294
	private void selectFirstNormalLocus()
	{
		foreach (LocusElement tElement in this._pool_elements_loci.getListTotal())
		{
			if (!tElement.isSpecialLocus())
			{
				this.selectLocus(tElement);
				break;
			}
		}
	}

	// Token: 0x0600369D RID: 13981 RVA: 0x0018C0F0 File Offset: 0x0018A2F0
	internal void selectLocus(LocusElement pElement)
	{
		this._selected_locus = pElement;
	}

	// Token: 0x0600369E RID: 13982 RVA: 0x0018C0FC File Offset: 0x0018A2FC
	private void clickGeneAssetAction(GeneAsset pGeneAsset)
	{
		if (this._selected_locus == null)
		{
			return;
		}
		if (!pGeneAsset.isAvailable())
		{
			return;
		}
		if (pGeneAsset != this._selected_locus.getGeneAsset())
		{
			AchievementLibrary.engineered_evolution.check(null);
		}
		if (!Config.hasPremium)
		{
			ScrollWindow.showWindow("premium_menu");
			return;
		}
		this._selected_chromosome.setGene(pGeneAsset, this._selected_locus.locus_index);
		this.chromosomeUpdatedEvent();
	}

	// Token: 0x0600369F RID: 13983 RVA: 0x0018C16C File Offset: 0x0018A36C
	private void chromosomeUpdatedEvent()
	{
		this._selected_chromosome.setDirty();
		this._selected_chromosome.recalculate();
		this._meta_object.genesChangedEvent();
		this._meta_object.eventGMO();
		this.showGenes(this._selected_chromosome);
		AchievementLibrary.simple_stupid_genetics.check(null);
		AchievementLibrary.fast_living.check(null);
		AchievementLibrary.long_living.check(null);
		AchievementLibrary.master_weaver.check(null);
		this._pool_elements_chromosomes.clear(true);
		this.loadChromosomes(false);
	}

	// Token: 0x060036A0 RID: 13984 RVA: 0x0018C1F4 File Offset: 0x0018A3F4
	public void showGenes(Chromosome pChromosome)
	{
		this._pool_elements_loci.clear(true);
		for (int i = 0; i < pChromosome.genes.Count; i++)
		{
			GeneAsset tGene = pChromosome.genes[i];
			LocusElement next = this._pool_elements_loci.getNext();
			next.show(i, pChromosome, tGene, pChromosome.getLocusType(i), new LocusClickEvent(this.selectLocus));
			next.addElementUnlockedAction(new AugmentationUnlockedAction(this.reloadButtons));
			next.addChromosomeUpdatedEvent(new Action(this.chromosomeUpdatedEvent));
		}
		this._window_subspecies.updateStats();
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x0018C288 File Offset: 0x0018A488
	private void updateTextGenome()
	{
		int tCurrent = this._selected_chromosome.countNonEmpty();
		int tSize = this._selected_chromosome.getAsset().amount_loci;
		this.genome_counter_text.text = tCurrent.ToString() + " / " + tSize.ToString();
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x0018C2D8 File Offset: 0x0018A4D8
	private void Update()
	{
		if (this._meta_object != null && this._selected_chromosome != null)
		{
			this.selection_gene_asset.gameObject.SetActive(this._selected_locus != null);
			this.selection_locus.gameObject.SetActive(this._selected_locus != null);
			if (this._selected_locus != null)
			{
				this.selection_locus.gameObject.transform.position = this._selected_locus.transform.position;
				GeneButton tCurrentGeneButton = this.getCurrentGeneAssetButton();
				this.selection_gene_asset.gameObject.transform.position = tCurrentGeneButton.transform.position;
				if (!Config.isDraggingItem())
				{
					tCurrentGeneButton != null;
				}
			}
		}
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x0018C39C File Offset: 0x0018A59C
	private GeneButton getCurrentGeneAssetButton()
	{
		GeneAsset tAsset = this._selected_locus.getGeneAsset();
		if (tAsset == null)
		{
			return null;
		}
		if (this._dictionary_gene_buttons.ContainsKey(tAsset))
		{
			return this._dictionary_gene_buttons[tAsset];
		}
		return null;
	}

	// Token: 0x060036A4 RID: 13988 RVA: 0x0018C3D8 File Offset: 0x0018A5D8
	private void reloadButtons()
	{
		int tCounterUnlocked = 0;
		int tTotal = 0;
		foreach (GeneButton tB in this._dictionary_gene_buttons.Values)
		{
			bool tUnlocked = tB.getElementAsset().isAvailable();
			tTotal++;
			if (tUnlocked)
			{
				tCounterUnlocked++;
				tB.image.color = Toolbox.color_white;
			}
			else
			{
				tB.image.color = Toolbox.color_black;
			}
			tB.GetComponent<DraggableLayoutElement>().enabled = tUnlocked;
		}
		this._text_unlocked_genes.text = tCounterUnlocked.ToString() + "/" + tTotal.ToString();
		AchievementLibrary.genes_explorer.checkBySignal(null);
	}

	// Token: 0x060036A5 RID: 13989 RVA: 0x0018C4A4 File Offset: 0x0018A6A4
	protected virtual bool hasGene(GeneAsset pTrait)
	{
		return this._selected_chromosome.hasGene(pTrait);
	}

	// Token: 0x04002865 RID: 10341
	[SerializeField]
	private Text _text_unlocked_genes;

	// Token: 0x04002866 RID: 10342
	[SerializeField]
	private Transform _transform_chromosomes;

	// Token: 0x04002867 RID: 10343
	[SerializeField]
	private Transform _transform_loci;

	// Token: 0x04002868 RID: 10344
	[SerializeField]
	private Transform _transform_gene_selector;

	// Token: 0x04002869 RID: 10345
	[SerializeField]
	private ChromosomeElement _prefab_chromosome_element;

	// Token: 0x0400286A RID: 10346
	[SerializeField]
	private LocusElement _prefab_locus_element;

	// Token: 0x0400286B RID: 10347
	[SerializeField]
	private GeneButton _prefab_gene_button;

	// Token: 0x0400286C RID: 10348
	private bool _initialized;

	// Token: 0x0400286D RID: 10349
	private Dictionary<GeneAsset, GeneButton> _dictionary_gene_buttons = new Dictionary<GeneAsset, GeneButton>();

	// Token: 0x0400286E RID: 10350
	private ObjectPoolGenericMono<ChromosomeElement> _pool_elements_chromosomes;

	// Token: 0x0400286F RID: 10351
	private ObjectPoolGenericMono<LocusElement> _pool_elements_loci;

	// Token: 0x04002870 RID: 10352
	private LocusElement _selected_locus;

	// Token: 0x04002871 RID: 10353
	private Chromosome _selected_chromosome;

	// Token: 0x04002872 RID: 10354
	public Image selection_locus;

	// Token: 0x04002873 RID: 10355
	public Image selection_gene_asset;

	// Token: 0x04002874 RID: 10356
	public Text genome_counter_text;

	// Token: 0x04002875 RID: 10357
	private SubspeciesWindow _window_subspecies;
}
