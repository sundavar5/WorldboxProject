using System;

// Token: 0x02000165 RID: 357
[Serializable]
public class RarityAsset : Asset, ILocalizedAsset
{
	// Token: 0x06000AF6 RID: 2806 RVA: 0x000A08F1 File Offset: 0x0009EAF1
	public string getLocaleID()
	{
		return this.rarity_trait_string;
	}

	// Token: 0x04000A6C RID: 2668
	public string color_hex;

	// Token: 0x04000A6D RID: 2669
	public string material_path;

	// Token: 0x04000A6E RID: 2670
	public string rarity_trait_string;

	// Token: 0x04000A6F RID: 2671
	[NonSerialized]
	public ContainerItemColor color_container;
}
