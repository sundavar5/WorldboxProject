using System;
using System.Collections.Generic;

// Token: 0x020005BF RID: 1471
[Serializable]
public class Map
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06003070 RID: 12400 RVA: 0x00176D08 File Offset: 0x00174F08
	public string formattedMapId
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mapId) && this.mapId.Length == 12)
			{
				return string.Concat(new string[]
				{
					"WB-",
					this.mapId.Substring(0, 4),
					"-",
					this.mapId.Substring(4, 4),
					"-",
					this.mapId.Substring(8, 4)
				});
			}
			return this.mapId;
		}
	}

	// Token: 0x040024A1 RID: 9377
	public string mapId;

	// Token: 0x040024A2 RID: 9378
	public string language;

	// Token: 0x040024A3 RID: 9379
	public string timestamp;

	// Token: 0x040024A4 RID: 9380
	public string userId;

	// Token: 0x040024A5 RID: 9381
	public string username;

	// Token: 0x040024A6 RID: 9382
	public string version;

	// Token: 0x040024A7 RID: 9383
	public string mapName;

	// Token: 0x040024A8 RID: 9384
	public string mapDescription;

	// Token: 0x040024A9 RID: 9385
	public int size;

	// Token: 0x040024AA RID: 9386
	public int sortIndex;

	// Token: 0x040024AB RID: 9387
	public List<MapTagType> mapTags;

	// Token: 0x040024AC RID: 9388
	public MapMetaData mapMeta;

	// Token: 0x040024AD RID: 9389
	public OnlineStats onlineStats = new OnlineStats
	{
		downloads = 0,
		plays = 0,
		favs = 0,
		reports = 0
	};
}
