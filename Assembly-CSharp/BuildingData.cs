using System;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x02000235 RID: 565
[Serializable]
public class BuildingData : BaseObjectData
{
	// Token: 0x17000132 RID: 306
	// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000DF91B File Offset: 0x000DDB1B
	[Preserve]
	[Obsolete("use .id instead", true)]
	public long objectID
	{
		set
		{
			if (!value.hasValue())
			{
				return;
			}
			if (base.id.hasValue())
			{
				return;
			}
			base.id = value;
		}
	}

	// Token: 0x17000133 RID: 307
	// (set) Token: 0x060015B9 RID: 5561 RVA: 0x000DF93B File Offset: 0x000DDB3B
	[Preserve]
	[Obsolete("use .asset_id instead", true)]
	public string templateID
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.asset_id))
			{
				return;
			}
			this.asset_id = value;
		}
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000DF95B File Offset: 0x000DDB5B
	public override void Dispose()
	{
		base.Dispose();
		CityResources cityResources = this.resources;
		if (cityResources != null)
		{
			cityResources.Dispose();
		}
		this.resources = null;
		StorageBooks storageBooks = this.books;
		if (storageBooks != null)
		{
			storageBooks.Dispose();
		}
		this.books = null;
	}

	// Token: 0x04001226 RID: 4646
	[DefaultValue(BuildingState.Normal)]
	public BuildingState state = BuildingState.Normal;

	// Token: 0x04001227 RID: 4647
	public int mainX;

	// Token: 0x04001228 RID: 4648
	public int mainY;

	// Token: 0x04001229 RID: 4649
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string asset_id;

	// Token: 0x0400122A RID: 4650
	[DefaultValue(-1L)]
	public long cityID = -1L;

	// Token: 0x0400122B RID: 4651
	public float grow_time;

	// Token: 0x0400122C RID: 4652
	public CityResources resources;

	// Token: 0x0400122D RID: 4653
	public StorageBooks books;

	// Token: 0x0400122E RID: 4654
	[DefaultValue(-1)]
	public int frameID = -1;
}
