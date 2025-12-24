using System;
using System.Collections.Generic;

// Token: 0x02000016 RID: 22
[Serializable]
public class AchievementGroupAsset : BaseCategoryAsset, ILocalizedAsset
{
	// Token: 0x06000103 RID: 259 RVA: 0x00009D0D File Offset: 0x00007F0D
	public override string getLocaleID()
	{
		return "achievement_group_" + this.id;
	}

	// Token: 0x040000BF RID: 191
	[NonSerialized]
	public List<Achievement> achievements_list = new List<Achievement>();
}
