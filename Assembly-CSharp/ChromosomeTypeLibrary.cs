using System;

// Token: 0x020000FF RID: 255
public class ChromosomeTypeLibrary : AssetLibrary<ChromosomeTypeAsset>
{
	// Token: 0x060007C8 RID: 1992 RVA: 0x0006EC10 File Offset: 0x0006CE10
	public override void init()
	{
		base.init();
		this.add(new ChromosomeTypeAsset
		{
			id = "chromosome_big",
			amount_loci_min_amplifier = 1,
			amount_loci_max_amplifier = 4,
			amount_loci_min_empty = 4,
			amount_loci_max_empty = 8,
			amount_loci = 30,
			name = "chromosome_big",
			description = "some chromosome"
		});
		this.add(new ChromosomeTypeAsset
		{
			id = "chromosome_medium",
			amount_loci_min_amplifier = 1,
			amount_loci_max_amplifier = 3,
			amount_loci_min_empty = 3,
			amount_loci_max_empty = 5,
			amount_loci = 24,
			name = "chromosome_medium",
			description = "some chromosome"
		});
		this.add(new ChromosomeTypeAsset
		{
			id = "chromosome_small",
			amount_loci_min_amplifier = 1,
			amount_loci_max_amplifier = 3,
			amount_loci_min_empty = 2,
			amount_loci_max_empty = 4,
			amount_loci = 18,
			name = "chromosome_small",
			description = "some chromosome"
		});
		this.add(new ChromosomeTypeAsset
		{
			id = "chromosome_tiny",
			amount_loci_min_amplifier = 0,
			amount_loci_max_amplifier = 2,
			amount_loci_min_empty = 1,
			amount_loci_max_empty = 3,
			amount_loci = 12,
			name = "chromosome_tiny",
			description = "some chromosome"
		});
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0006ED68 File Offset: 0x0006CF68
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (ChromosomeTypeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x0400081E RID: 2078
	public const int LOCI_PER_ROW = 6;
}
