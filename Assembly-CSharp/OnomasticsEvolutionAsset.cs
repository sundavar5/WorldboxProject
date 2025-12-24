using System;
using System.ComponentModel;

// Token: 0x02000143 RID: 323
[Serializable]
public class OnomasticsEvolutionAsset : Asset
{
	// Token: 0x040009C2 RID: 2498
	public string from;

	// Token: 0x040009C3 RID: 2499
	public string to;

	// Token: 0x040009C4 RID: 2500
	public char[] not_surrounded_by;

	// Token: 0x040009C5 RID: 2501
	[DefaultValue(true)]
	public bool reverse = true;

	// Token: 0x040009C6 RID: 2502
	public OnomasticsReplacerDelegate replacer;
}
