using System;

// Token: 0x0200006D RID: 109
public class PersonalityLibrary : AssetLibrary<PersonalityAsset>
{
	// Token: 0x060003CB RID: 971 RVA: 0x000225E4 File Offset: 0x000207E4
	public override void init()
	{
		base.init();
		this.add(new PersonalityAsset
		{
			id = "administrator"
		});
		this.t.base_stats["personality_diplomatic"] = 0.1f;
		this.t.base_stats["personality_administration"] = 0.5f;
		this.t.base_stats["personality_aggression"] = 0.1f;
		this.add(new PersonalityAsset
		{
			id = "militarist"
		});
		this.t.base_stats["personality_diplomatic"] = 0.05f;
		this.t.base_stats["personality_administration"] = 0.1f;
		this.t.base_stats["personality_aggression"] = 0.5f;
		this.add(new PersonalityAsset
		{
			id = "diplomat"
		});
		this.t.base_stats["personality_diplomatic"] = 0.5f;
		this.t.base_stats["personality_aggression"] = 0.05f;
		this.t.base_stats["personality_administration"] = 0.2f;
		this.add(new PersonalityAsset
		{
			id = "balanced"
		});
		this.t.base_stats["personality_administration"] = 0.1f;
		this.t.base_stats["personality_diplomatic"] = 0.1f;
		this.t.base_stats["personality_aggression"] = 0.1f;
		this.add(new PersonalityAsset
		{
			id = "wildcard"
		});
		this.t.base_stats["personality_rationality"] = -1f;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x000227BC File Offset: 0x000209BC
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (PersonalityAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
