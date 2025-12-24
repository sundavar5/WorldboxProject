using System;
using Newtonsoft.Json;

// Token: 0x020000E6 RID: 230
[Serializable]
public class NeuralLayerAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x000676E0 File Offset: 0x000658E0
	public string getLocaleID()
	{
		return this.id;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x000676E8 File Offset: 0x000658E8
	public string getDescriptionID()
	{
		return this.id + "_priority";
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000676FA File Offset: 0x000658FA
	[JsonIgnore]
	public string debug_string
	{
		get
		{
			return string.Format("{0} - {1} - ({2})", this.getLocaleID().Localize(), this.getDescriptionID().Localize(), this.layer);
		}
	}

	// Token: 0x04000777 RID: 1911
	public NeuroLayer layer;

	// Token: 0x04000778 RID: 1912
	public string color_hex;

	// Token: 0x04000779 RID: 1913
	public bool critical;

	// Token: 0x0400077A RID: 1914
	public float chance_to_go_to_next_layer;
}
