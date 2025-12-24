using System;

// Token: 0x0200055C RID: 1372
public struct AvatarsCombineDataElement
{
	// Token: 0x06002CA0 RID: 11424 RVA: 0x0015E0EC File Offset: 0x0015C2EC
	public AvatarsCombineDataElement(int pOrderIndex, int pTotalAmount)
	{
		this.order_index = pOrderIndex;
		this.total_amount = pTotalAmount;
	}

	// Token: 0x0400222F RID: 8751
	public readonly int order_index;

	// Token: 0x04002230 RID: 8752
	public readonly int total_amount;
}
