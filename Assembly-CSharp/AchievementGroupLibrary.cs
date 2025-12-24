using System;
using Beebyte.Obfuscator;

// Token: 0x02000017 RID: 23
[ObfuscateLiterals]
[Serializable]
public class AchievementGroupLibrary : AssetLibrary<AchievementGroupAsset>
{
	// Token: 0x06000105 RID: 261 RVA: 0x00009D34 File Offset: 0x00007F34
	public override void init()
	{
		base.init();
		this.add(new AchievementGroupAsset
		{
			id = "creation",
			color = "#68B3FF"
		});
		this.add(new AchievementGroupAsset
		{
			id = "worlds",
			color = "#BAFFC2"
		});
		this.add(new AchievementGroupAsset
		{
			id = "civilizations",
			color = "#BAF0F4"
		});
		this.add(new AchievementGroupAsset
		{
			id = "creatures",
			color = "#42FF61"
		});
		this.add(new AchievementGroupAsset
		{
			id = "destruction",
			color = "#FF6B86"
		});
		this.add(new AchievementGroupAsset
		{
			id = "nature",
			color = "#BAFFC2"
		});
		this.add(new AchievementGroupAsset
		{
			id = "experiments",
			color = "#FF8F44"
		});
		this.add(new AchievementGroupAsset
		{
			id = "collection",
			color = "#46DCE3"
		});
		this.add(new AchievementGroupAsset
		{
			id = "exploration",
			color = "#EFCB00"
		});
		this.add(new AchievementGroupAsset
		{
			id = "forbidden",
			color = "#C98CFF"
		});
		this.add(new AchievementGroupAsset
		{
			id = "miscellaneous",
			color = "#B4C4C0"
		});
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00009EC0 File Offset: 0x000080C0
	public override void linkAssets()
	{
		foreach (Achievement tAchievement in AssetManager.achievements.list)
		{
			this.dict[tAchievement.group].achievements_list.Add(tAchievement);
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00009F2C File Offset: 0x0000812C
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (AchievementGroupAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
