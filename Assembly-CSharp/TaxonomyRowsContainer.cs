using System;
using UnityEngine.Events;

// Token: 0x020002E3 RID: 739
public class TaxonomyRowsContainer : StatsRowsContainer
{
	// Token: 0x06001BDD RID: 7133 RVA: 0x000FE130 File Offset: 0x000FC330
	protected override void showStats()
	{
		this.showTaxonomicRank("taxonomy_kingdom");
		this.showTaxonomicRank("taxonomy_phylum");
		this.showTaxonomicRank("taxonomy_class");
		this.showTaxonomicRank("taxonomy_order");
		this.showTaxonomicRank("taxonomy_family");
		this.showTaxonomicRank("taxonomy_genus");
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		StatsWindow.tryToShowMetaSpecies("species", tSubspecies.data.species_id, this);
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000FE19C File Offset: 0x000FC39C
	private void showTaxonomicRank(string pTaxonomyRank)
	{
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		string tColorHex = ColorStyleLibrary.m.getColorForTaxonomy(pTaxonomyRank);
		string tTaxonomyRankId = AssetManager.actor_library.get(tSubspecies.data.species_id).getTaxonomyRank(pTaxonomyRank);
		if (string.IsNullOrEmpty(tTaxonomyRankId))
		{
			return;
		}
		tTaxonomyRankId = Toolbox.firstLetterToUpper(tTaxonomyRankId);
		KeyValueField tField = base.showStatRow(pTaxonomyRank, tTaxonomyRankId, tColorHex, MetaType.None, -1L, true, null, null, null, true);
		tField.on_hover_value = delegate()
		{
			this.showTooltipTaxonomy(pTaxonomyRank, tSubspecies, tField);
		};
		tField.on_hover_value_out = new UnityAction(Tooltip.hideTooltip);
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000FE25C File Offset: 0x000FC45C
	private void showTooltipTaxonomy(string pRankType, Subspecies pSpecies, KeyValueField pField)
	{
		TooltipData tData = new TooltipData
		{
			subspecies = pSpecies,
			tip_name = pRankType
		};
		Tooltip.show(pField, "taxonomy", tData);
	}
}
