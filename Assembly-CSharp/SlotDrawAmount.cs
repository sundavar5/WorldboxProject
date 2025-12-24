using System;

// Token: 0x02000162 RID: 354
internal struct SlotDrawAmount
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000A0868 File Offset: 0x0009EA68
	public ResourceAsset asset
	{
		get
		{
			return AssetManager.resources.get(this.resource_id);
		}
	}

	// Token: 0x04000A65 RID: 2661
	public string resource_id;

	// Token: 0x04000A66 RID: 2662
	public int amount;
}
