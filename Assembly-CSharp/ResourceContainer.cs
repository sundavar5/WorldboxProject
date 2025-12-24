using System;
using Newtonsoft.Json;

// Token: 0x02000204 RID: 516
[Serializable]
public struct ResourceContainer
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06001237 RID: 4663 RVA: 0x000D45B1 File Offset: 0x000D27B1
	[JsonIgnore]
	public ResourceAsset asset
	{
		get
		{
			return AssetManager.resources.get(this.id);
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x000D45C3 File Offset: 0x000D27C3
	public ResourceContainer(string pID, int pAmount)
	{
		this.id = pID;
		this.amount = pAmount;
	}

	// Token: 0x04001109 RID: 4361
	[JsonProperty]
	public readonly string id;

	// Token: 0x0400110A RID: 4362
	public int amount;
}
