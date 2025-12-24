using System;

// Token: 0x020000FD RID: 253
[Serializable]
public class ChromosomeTypeAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x060007C4 RID: 1988 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
	public string getLocaleID()
	{
		return this.id;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0006EBC0 File Offset: 0x0006CDC0
	public string getDescriptionID()
	{
		return this.id + "_description";
	}

	// Token: 0x04000813 RID: 2067
	public int amount_loci;

	// Token: 0x04000814 RID: 2068
	public int amount_loci_min_amplifier;

	// Token: 0x04000815 RID: 2069
	public int amount_loci_max_amplifier;

	// Token: 0x04000816 RID: 2070
	public int amount_loci_min_empty;

	// Token: 0x04000817 RID: 2071
	public int amount_loci_max_empty;

	// Token: 0x04000818 RID: 2072
	public string name;

	// Token: 0x04000819 RID: 2073
	public string description;
}
