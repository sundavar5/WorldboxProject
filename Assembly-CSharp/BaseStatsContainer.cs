using System;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class BaseStatsContainer
{
	// Token: 0x060001D4 RID: 468 RVA: 0x0000F02C File Offset: 0x0000D22C
	public void normalize()
	{
		BaseStatAsset tAsset = this.asset;
		if (tAsset.normalize)
		{
			this.value = Mathf.Clamp(this.value, tAsset.normalize_min, tAsset.normalize_max);
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000F065 File Offset: 0x0000D265
	[JsonIgnore]
	public BaseStatAsset asset
	{
		get
		{
			return AssetManager.base_stats_library.get(this.id);
		}
	}

	// Token: 0x04000158 RID: 344
	public string id;

	// Token: 0x04000159 RID: 345
	public float value;
}
