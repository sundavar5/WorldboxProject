using System;

// Token: 0x02000707 RID: 1799
public class CubeNodeAssetData
{
	// Token: 0x0600398A RID: 14730 RVA: 0x0019958E File Offset: 0x0019778E
	public CubeNodeAssetData(BaseUnlockableAsset pAsset, string pKnowledgeType)
	{
		this.asset = pAsset;
		this.knowledge_type = pKnowledgeType;
	}

	// Token: 0x04002A71 RID: 10865
	public readonly BaseUnlockableAsset asset;

	// Token: 0x04002A72 RID: 10866
	public readonly string knowledge_type;
}
