using System;
using System.Collections.Generic;

// Token: 0x02000088 RID: 136
[Serializable]
public class WarTypeAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x06000495 RID: 1173 RVA: 0x000324FD File Offset: 0x000306FD
	public IEnumerable<string> getLocaleIDs()
	{
		yield return this.localized_type;
		yield return this.localized_war_name;
		yield break;
	}

	// Token: 0x040004E8 RID: 1256
	public string name_template = "war_conquest";

	// Token: 0x040004E9 RID: 1257
	public string localized_type = "war_type_conquest";

	// Token: 0x040004EA RID: 1258
	public string localized_war_name = "war_name_conquest";

	// Token: 0x040004EB RID: 1259
	public string path_icon = "war_conquest";

	// Token: 0x040004EC RID: 1260
	public bool kingdom_for_name_attacker = true;

	// Token: 0x040004ED RID: 1261
	public bool forced_war;

	// Token: 0x040004EE RID: 1262
	public bool alliance_join;

	// Token: 0x040004EF RID: 1263
	public bool total_war;

	// Token: 0x040004F0 RID: 1264
	public bool rebellion;

	// Token: 0x040004F1 RID: 1265
	public bool can_end_with_plot;
}
