using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class GameLanguageLibrary : AssetLibrary<GameLanguageAsset>
{
	// Token: 0x0600092E RID: 2350 RVA: 0x0008265C File Offset: 0x0008085C
	public override void init()
	{
		base.init();
		this.add("en", "English");
		this.t.main = true;
		this.add("de", "Deutsch");
		this.add("pl", "Polski");
		this.add("cz", "中文(简体)");
		this.t.is_hanzi = true;
		this.t.force_style = new ForcedFontStyle(FontStyle.Normal, true);
		this.t.font = (() => LocalizedTextManager.instance.simplified_chinese_font);
		this.add("ch", "中文(繁體)");
		this.t.is_hanzi = true;
		this.t.force_style = new ForcedFontStyle(FontStyle.Normal, true);
		this.add("ko", "한국어");
		this.t.font = (() => LocalizedTextManager.instance.korean_font);
		this.add("ja", "日本語");
		this.t.is_hanzi = true;
		this.t.force_style = new ForcedFontStyle(FontStyle.Normal, true);
		this.t.font = (() => LocalizedTextManager.instance.japanese_font);
		this.add("th", "ภาษาไทย");
		this.t.font = (() => LocalizedTextManager.instance.thai_font);
		this.add("vn", "Tiếng Việt");
		this.add("ru", "Русский");
		this.t.show_translators = false;
		this.add("ua", "Українська");
		this.add("by", "Беларуская");
		this.add("es", "Español");
		this.add("br", "Português do Brasil");
		this.add("pt", "Português de Portugal");
		this.add("fr", "Français");
		this.add("it", "Italiano");
		this.add("ro", "Română");
		this.add("hr", "Hrvatski");
		this.add("tr", "Türkçe");
		this.add("gr", "Ελληνικά");
		this.add("ka", "ქართული");
		this.t.font = (() => LocalizedTextManager.instance.arabic_font);
		this.add("sk", "Slovenčina");
		this.add("cs", "Čeština");
		this.add("hu", "Magyar");
		this.add("nl", "Nederlands");
		this.add("id", "Bahasa Indonesia");
		this.add("sv", "Svenska");
		this.add("no", "Norsk");
		this.add("da", "Dansk");
		this.add("fa", "فارسی");
		this.t.is_rtl = true;
		this.t.font = (() => LocalizedTextManager.instance.persian_font);
		this.add("ar", "العربية");
		this.t.is_rtl = true;
		this.t.font = (() => LocalizedTextManager.instance.arabic_font);
		this.add("he", "עִברִית");
		this.t.is_rtl = true;
		this.add("fn", "Suomi");
		this.add("ph", "Tagalog");
		this.add("hi", "हिन्दी");
		this.t.is_hindi = true;
		this.t.font = (() => LocalizedTextManager.instance.hindi_font);
		this.t.force_style = new ForcedFontStyle(FontStyle.Bold, false);
		this.add("lb", "Lëtzebuergesch");
		this.add("lt", "Lithuanian");
		this.add(new GameLanguageAsset
		{
			id = "boat",
			name = "Boat",
			export = false,
			path_icon = "ui/Icons/iconSeaborn"
		});
		this.add(new GameLanguageAsset
		{
			id = "keys",
			name = "KEYS",
			export = false,
			debug_only = true,
			path_icon = "ui/Icons/iconDebug"
		});
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00082B68 File Offset: 0x00080D68
	public GameLanguageAsset add(string pID, string pName)
	{
		return this.add(new GameLanguageAsset
		{
			id = pID,
			name = pName
		});
	}
}
