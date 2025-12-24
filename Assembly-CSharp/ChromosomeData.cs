using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020000FE RID: 254
[Serializable]
public class ChromosomeData
{
	// Token: 0x0400081A RID: 2074
	public List<string> loci = new List<string>();

	// Token: 0x0400081B RID: 2075
	public List<int> super_loci = new List<int>();

	// Token: 0x0400081C RID: 2076
	public List<int> void_loci = new List<int>();

	// Token: 0x0400081D RID: 2077
	[DefaultValue("chromosome_medium")]
	public string chromosome_type = "chromosome_medium";
}
