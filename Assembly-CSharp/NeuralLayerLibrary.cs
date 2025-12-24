using System;
using System.Collections.Generic;

// Token: 0x020000E7 RID: 231
public class NeuralLayerLibrary : AssetLibrary<NeuralLayerAsset>
{
	// Token: 0x060006D7 RID: 1751 RVA: 0x00067730 File Offset: 0x00065930
	public override void init()
	{
		base.init();
		this.add(new NeuralLayerAsset
		{
			id = "neuro_layer_0",
			layer = NeuroLayer.Layer_0_Minimal,
			color_hex = "#B0E0E6"
		});
		this.add(new NeuralLayerAsset
		{
			id = "neuro_layer_1",
			layer = NeuroLayer.Layer_1_Low,
			color_hex = "#87CEEB",
			chance_to_go_to_next_layer = 0.3f
		});
		this.add(new NeuralLayerAsset
		{
			id = "neuro_layer_2",
			layer = NeuroLayer.Layer_2_Moderate,
			color_hex = "#FFD700",
			chance_to_go_to_next_layer = 0.2f
		});
		this.add(new NeuralLayerAsset
		{
			id = "neuro_layer_3",
			layer = NeuroLayer.Layer_3_High,
			color_hex = "#FF4500",
			chance_to_go_to_next_layer = 0.1f
		});
		this.add(new NeuralLayerAsset
		{
			id = "neuro_layer_4",
			layer = NeuroLayer.Layer_4_Critical,
			critical = true,
			color_hex = "#FF0000"
		});
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00067838 File Offset: 0x00065A38
	public override void linkAssets()
	{
		base.linkAssets();
		this.layers_array = new NeuralLayerAsset[this.list.Count];
		foreach (NeuralLayerAsset tAsset in this.list)
		{
			this._dict_enum_assets.Add(tAsset.layer, tAsset);
			this.layers_array[(int)tAsset.layer] = tAsset;
		}
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000678C0 File Offset: 0x00065AC0
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (NeuralLayerAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0006792C File Offset: 0x00065B2C
	public NeuralLayerAsset getWithID(NeuroLayer pLayerID)
	{
		return this._dict_enum_assets[pLayerID];
	}

	// Token: 0x0400077B RID: 1915
	private Dictionary<NeuroLayer, NeuralLayerAsset> _dict_enum_assets = new Dictionary<NeuroLayer, NeuralLayerAsset>();

	// Token: 0x0400077C RID: 1916
	public NeuralLayerAsset[] layers_array;
}
