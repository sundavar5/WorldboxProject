using System;

// Token: 0x02000781 RID: 1921
public readonly struct TooltipModContainerInfo
{
	// Token: 0x06003D53 RID: 15699 RVA: 0x001ADF63 File Offset: 0x001AC163
	public TooltipModContainerInfo(ItemAsset pAsset, int pPluses, string pStringPluses)
	{
		this.asset = pAsset;
		this.pluses = pPluses;
		this.string_pluses = pStringPluses;
	}

	// Token: 0x04002C74 RID: 11380
	public readonly ItemAsset asset;

	// Token: 0x04002C75 RID: 11381
	public readonly int pluses;

	// Token: 0x04002C76 RID: 11382
	public readonly string string_pluses;
}
