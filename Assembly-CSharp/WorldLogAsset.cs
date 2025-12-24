using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B9 RID: 441
[Serializable]
public class WorldLogAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x06000CC8 RID: 3272 RVA: 0x000B92F4 File Offset: 0x000B74F4
	public string getLocaleID()
	{
		return this.locale_id ?? this.id;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x000B9306 File Offset: 0x000B7506
	public string getLocaleID(int pIndex)
	{
		return string.Format("{0}_{1}", this.getLocaleID(), pIndex);
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x000B931E File Offset: 0x000B751E
	public IEnumerable<string> getLocaleIDs()
	{
		if (this.random_ids == 0)
		{
			yield return this.getLocaleID();
		}
		else
		{
			int num;
			for (int i = 1; i <= this.random_ids; i = num + 1)
			{
				yield return this.getLocaleID(i);
				num = i;
			}
		}
		yield break;
	}

	// Token: 0x04000C5B RID: 3163
	public string group;

	// Token: 0x04000C5C RID: 3164
	public string locale_id;

	// Token: 0x04000C5D RID: 3165
	public string path_icon;

	// Token: 0x04000C5E RID: 3166
	public Color color = Toolbox.color_log_neutral;

	// Token: 0x04000C5F RID: 3167
	public int random_ids;

	// Token: 0x04000C60 RID: 3168
	public WorldLogTextFormatter text_replacer;
}
