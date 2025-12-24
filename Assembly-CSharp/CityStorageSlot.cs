using System;
using Newtonsoft.Json;

// Token: 0x0200031F RID: 799
[Serializable]
public class CityStorageSlot
{
	// Token: 0x06001EE5 RID: 7909 RVA: 0x0010DC84 File Offset: 0x0010BE84
	public CityStorageSlot(string pID)
	{
		this.create(pID);
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x0010DC93 File Offset: 0x0010BE93
	public void create(string pID)
	{
		this.id = pID;
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0010DC9C File Offset: 0x0010BE9C
	[JsonIgnore]
	public ResourceAsset asset
	{
		get
		{
			return AssetManager.resources.get(this.id);
		}
	}

	// Token: 0x0400169F RID: 5791
	public string id;

	// Token: 0x040016A0 RID: 5792
	public int amount;
}
