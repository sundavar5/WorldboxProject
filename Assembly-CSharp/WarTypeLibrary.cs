using System;

// Token: 0x02000089 RID: 137
public class WarTypeLibrary : AssetLibrary<WarTypeAsset>
{
	// Token: 0x06000497 RID: 1175 RVA: 0x00032548 File Offset: 0x00030748
	public override void init()
	{
		base.init();
		WarTypeLibrary.normal = this.add(new WarTypeAsset
		{
			id = "normal",
			name_template = "war_conquest",
			localized_type = "war_type_conquest",
			localized_war_name = "war_name_conquest",
			path_icon = "wars/war_conquest",
			kingdom_for_name_attacker = true,
			alliance_join = true,
			can_end_with_plot = true
		});
		WarTypeLibrary.spite = this.add(new WarTypeAsset
		{
			id = "spite",
			name_template = "war_spite",
			localized_type = "war_type_spite",
			localized_war_name = "war_name_spite",
			path_icon = "wars/war_spite",
			kingdom_for_name_attacker = true,
			forced_war = true,
			total_war = true,
			alliance_join = false
		});
		WarTypeLibrary.inspire = this.add(new WarTypeAsset
		{
			id = "inspire",
			name_template = "war_inspire",
			localized_type = "war_type_inspire",
			localized_war_name = "war_name_inspire",
			path_icon = "wars/war_rebellion",
			kingdom_for_name_attacker = false,
			alliance_join = false,
			rebellion = true,
			can_end_with_plot = true
		});
		WarTypeLibrary.rebellion = this.add(new WarTypeAsset
		{
			id = "rebellion",
			name_template = "war_rebellion",
			localized_type = "war_type_rebellion",
			localized_war_name = "war_name_rebellion",
			path_icon = "wars/war_rebellion",
			kingdom_for_name_attacker = false,
			alliance_join = false,
			rebellion = true,
			can_end_with_plot = true
		});
		WarTypeLibrary.whisper_of_war = this.add(new WarTypeAsset
		{
			id = "whisper_of_war",
			name_template = "war_whisper",
			localized_type = "war_type_whisper",
			localized_war_name = "war_name_whisper",
			path_icon = "wars/war_whisper",
			kingdom_for_name_attacker = true,
			alliance_join = true
		});
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00032738 File Offset: 0x00030938
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (WarTypeAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
	}

	// Token: 0x040004F2 RID: 1266
	public static WarTypeAsset normal;

	// Token: 0x040004F3 RID: 1267
	public static WarTypeAsset spite;

	// Token: 0x040004F4 RID: 1268
	public static WarTypeAsset inspire;

	// Token: 0x040004F5 RID: 1269
	public static WarTypeAsset rebellion;

	// Token: 0x040004F6 RID: 1270
	public static WarTypeAsset whisper_of_war;

	// Token: 0x040004F7 RID: 1271
	public static WarTypeAsset clash;
}
