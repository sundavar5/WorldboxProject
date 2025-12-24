using System;

// Token: 0x020000E9 RID: 233
public static class NeuralLayerAssetExtensions
{
	// Token: 0x060006DC RID: 1756 RVA: 0x0006794D File Offset: 0x00065B4D
	public static NeuralLayerAsset GetAsset(this NeuroLayer pLayerID)
	{
		return AssetManager.neural_layers.getWithID(pLayerID);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0006795A File Offset: 0x00065B5A
	public static string getDebugString(this NeuroLayer pLayerID)
	{
		return pLayerID.GetAsset().debug_string;
	}
}
