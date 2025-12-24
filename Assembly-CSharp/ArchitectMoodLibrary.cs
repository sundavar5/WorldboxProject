using System;

// Token: 0x020000A1 RID: 161
public class ArchitectMoodLibrary : AssetLibrary<ArchitectMood>
{
	// Token: 0x06000557 RID: 1367 RVA: 0x00052B48 File Offset: 0x00050D48
	public override void init()
	{
		base.init();
		this.add(new ArchitectMood
		{
			id = "trickster",
			color_main = "#1cf713",
			color_text = "#1cf713"
		});
		this.add(new ArchitectMood
		{
			id = "benevolent",
			color_main = "#ffe90b",
			color_text = "#ffe90b"
		});
		this.add(new ArchitectMood
		{
			id = "malevolent",
			color_main = "#a00cfc",
			color_text = "#a00cfc"
		});
		this.add(new ArchitectMood
		{
			id = "serene",
			color_main = "#68FFFF",
			color_text = "#68FFFF"
		});
		this.add(new ArchitectMood
		{
			id = "chaotic",
			color_main = "#ff0e0e",
			color_text = "#ff0e0e"
		});
		this.add(new ArchitectMood
		{
			id = "orderly",
			color_main = "#ff870e",
			color_text = "#ff870e"
		});
		this.add(new ArchitectMood
		{
			id = "mysterious",
			color_main = "#f01fb4",
			color_text = "#f01fb4"
		});
		this.add(new ArchitectMood
		{
			id = "apathetic",
			color_main = "#000000",
			color_text = "#AAAAAA"
		});
		this.add(new ArchitectMood
		{
			id = "ethereal",
			color_main = "#73a18e",
			color_text = "#73a18e"
		});
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00052CF0 File Offset: 0x00050EF0
	public override void editorDiagnosticLocales()
	{
		foreach (ArchitectMood tAsset in this.list)
		{
			string tLocaleID = tAsset.getLocaleID();
			this.checkLocale(tAsset, tLocaleID);
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00052D54 File Offset: 0x00050F54
	public override void post_init()
	{
		base.post_init();
		foreach (ArchitectMood tAsset in this.list)
		{
			tAsset.path_icon = "ui/Icons/architect_moods/architect_mood_" + tAsset.id;
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00052DBC File Offset: 0x00050FBC
	public override void editorDiagnostic()
	{
		foreach (ArchitectMood tAsset in this.list)
		{
			if (SpriteTextureLoader.getSprite(tAsset.path_icon) == null)
			{
				BaseAssetLibrary.logAssetError("Missing icon file", tAsset.path_icon);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x040005AB RID: 1451
	public const string DEFAULT_MOOD = "serene";
}
