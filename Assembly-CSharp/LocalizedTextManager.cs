using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000471 RID: 1137
public class LocalizedTextManager
{
	// Token: 0x17000215 RID: 533
	// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0013B75E File Offset: 0x0013995E
	public static Font current_font
	{
		get
		{
			GameLanguageAsset gameLanguageAsset = LocalizedTextManager.current_language;
			Font font;
			if (gameLanguageAsset == null)
			{
				font = null;
			}
			else
			{
				FontGetter font2 = gameLanguageAsset.font;
				font = ((font2 != null) ? font2() : null);
			}
			return font ?? LocalizedTextManager.instance.default_font;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x060026D7 RID: 9943 RVA: 0x0013B78B File Offset: 0x0013998B
	public Font hindi_font
	{
		get
		{
			if (this._hindi_font == null)
			{
				this._hindi_font = (Font)Resources.Load("Fonts/Poppins-Regular", typeof(Font));
			}
			return this._hindi_font;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0013B7BA File Offset: 0x001399BA
	public Font japanese_font
	{
		get
		{
			if (this._japanese_font == null)
			{
				this._japanese_font = (Font)Resources.Load("Fonts/MPLUSRounded1c-Medium", typeof(Font));
			}
			return this._japanese_font;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0013B7E9 File Offset: 0x001399E9
	public Font korean_font
	{
		get
		{
			if (this._korean_font == null)
			{
				this._korean_font = (Font)Resources.Load("Fonts/NanumGothicCoding-Bold", typeof(Font));
			}
			return this._korean_font;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x060026DA RID: 9946 RVA: 0x0013B818 File Offset: 0x00139A18
	public Font persian_font
	{
		get
		{
			if (this._persian_font == null)
			{
				this._persian_font = (Font)Resources.Load("Fonts/Vazirmatn-Bold", typeof(Font));
			}
			return this._persian_font;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060026DB RID: 9947 RVA: 0x0013B847 File Offset: 0x00139A47
	public Font arabic_font
	{
		get
		{
			if (this._arabic_font == null)
			{
				this._arabic_font = (Font)Resources.Load("Fonts/Tajawal-Bold", typeof(Font));
			}
			return this._arabic_font;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060026DC RID: 9948 RVA: 0x0013B876 File Offset: 0x00139A76
	public Font simplified_chinese_font
	{
		get
		{
			if (this._simplified_chinese_font == null)
			{
				this._simplified_chinese_font = (Font)Resources.Load("Fonts/NotoSansCJKsc-Bold", typeof(Font));
			}
			return this._simplified_chinese_font;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060026DD RID: 9949 RVA: 0x0013B8A5 File Offset: 0x00139AA5
	public Font thai_font
	{
		get
		{
			if (this._thai_font == null)
			{
				this._thai_font = (Font)Resources.Load("Fonts/krubbold", typeof(Font));
			}
			return this._thai_font;
		}
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x0013B8D4 File Offset: 0x00139AD4
	public static void init(string pLanguage = null)
	{
		if (LocalizedTextManager.instance != null)
		{
			return;
		}
		LocalizedTextManager.instance = new LocalizedTextManager();
		LocalizedTextManager.instance.create();
		if (pLanguage == null)
		{
			pLanguage = PlayerConfig.dict["language"].stringVal;
		}
		LocalizedTextManager.instance.setLanguage(pLanguage);
	}

	// Token: 0x060026DF RID: 9951 RVA: 0x0013B921 File Offset: 0x00139B21
	private void create()
	{
		this.default_font = (Font)Resources.Load("Fonts/Roboto-Bold", typeof(Font));
		LocalizedTextManager.instance = this;
		this.texts = new List<LocalizedText>();
	}

	// Token: 0x060026E0 RID: 9952 RVA: 0x0013B953 File Offset: 0x00139B53
	public bool contains(string pString)
	{
		return LocalizedTextManager.instance._localized_text.ContainsKey(pString);
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x0013B965 File Offset: 0x00139B65
	public static IEnumerable<string> getKeys()
	{
		return LocalizedTextManager.instance._localized_text.Keys;
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x0013B976 File Offset: 0x00139B76
	public static void addTextField(LocalizedText pText)
	{
		LocalizedTextManager.instance.texts.Add(pText);
		LocalizedTextManager.instance._lang_dirty = true;
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x0013B993 File Offset: 0x00139B93
	public static void removeTextField(LocalizedText pText)
	{
		if (LocalizedTextManager.instance == null)
		{
			return;
		}
		LocalizedTextManager.instance.texts.Remove(pText);
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x0013B9B0 File Offset: 0x00139BB0
	public static void updateTexts()
	{
		Debug.Log("LocalizedTextManager: total texts loaded: " + LocalizedTextManager.instance.texts.Count.ToString());
		foreach (LocalizedText localizedText in LocalizedTextManager.instance.texts)
		{
			localizedText.updateText(true);
		}
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x0013BA2C File Offset: 0x00139C2C
	public static bool stringExists(string pKey)
	{
		return !string.IsNullOrEmpty(pKey) && LocalizedTextManager.instance._localized_text.ContainsKey(pKey);
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x0013BA50 File Offset: 0x00139C50
	public static string getText(string pKey, Text text = null, bool pForceEnglish = false)
	{
		if (LocalizedTextManager.instance.language == "boat")
		{
			return LocalizedTextManager.transformToBoat(pKey);
		}
		if (LocalizedTextManager.instance.language == "keys")
		{
			return LocalizedTextManager.transformToKeys(pKey);
		}
		string tResult;
		if (LocalizedTextManager.instance._localized_text.ContainsKey(pKey))
		{
			tResult = LocalizedTextManager.instance._localized_text[pKey];
		}
		else
		{
			tResult = pKey;
			if (pKey.Contains("_placeholder"))
			{
				tResult = "";
			}
			else if (AssetManager.missing_locale_keys.Add(pKey))
			{
				Debug.LogError("LocalizedTextManager: missing text: " + pKey, text);
				AssetManager.generateMissingLocalesFile();
			}
		}
		if (pKey.StartsWith("world_law_", StringComparison.Ordinal))
		{
			tResult = tResult.Replace("\n\n", "\n");
		}
		return tResult;
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x0013BB18 File Offset: 0x00139D18
	public static string transformToKeys(string pTextKey)
	{
		string tResult = string.Empty;
		if (LocalizedTextManager.instance._localized_text_files.ContainsKey(pTextKey))
		{
			tResult = LocalizedTextManager.instance._localized_text_files[pTextKey];
		}
		return tResult + ": " + pTextKey;
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x0013BB5C File Offset: 0x00139D5C
	public static string transformToBoat(string pTextKey)
	{
		if (LocalizedTextManager._boat_strings.ContainsKey(pTextKey))
		{
			return LocalizedTextManager._boat_strings[pTextKey];
		}
		int tWords = pTextKey.Split(' ', StringSplitOptions.None).Length + 1;
		string tResult = "";
		for (int i = 0; i < tWords; i++)
		{
			if (tResult.Length > 0)
			{
				tResult += " ";
			}
			if (Randy.randomBool())
			{
				if (Randy.randomBool())
				{
					tResult += "Boat";
				}
				else if (Randy.randomBool())
				{
					tResult += "Aye";
				}
				else
				{
					tResult += "Argh";
				}
			}
			else if (Randy.randomBool())
			{
				tResult += "Ahoy";
			}
			else if (Randy.randomBool())
			{
				tResult += "boat";
			}
			else
			{
				tResult += "ye";
			}
		}
		LocalizedTextManager._boat_strings[pTextKey] = tResult;
		return LocalizedTextManager._boat_strings[pTextKey];
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x0013BC4C File Offset: 0x00139E4C
	public void loadLocalizedText(string pLocaleID)
	{
		this.initiated = true;
		LocalizedTextManager.instance._localized_text = new Dictionary<string, string>();
		LocalizedTextManager.instance._localized_text_files = new Dictionary<string, string>();
		string tFolderPath = "locales/" + pLocaleID;
		TextAsset[] tTextAssets;
		try
		{
			tTextAssets = Resources.LoadAll<TextAsset>(tFolderPath);
		}
		catch (Exception)
		{
			tTextAssets = Resources.LoadAll<TextAsset>("locales/en");
		}
		if (tTextAssets == null || tTextAssets.Length == 0)
		{
			tTextAssets = Resources.LoadAll<TextAsset>("locales/en");
		}
		foreach (TextAsset textAsset in tTextAssets)
		{
			string tDataAsJson = textAsset.text;
			string tFileName = textAsset.name;
			Dictionary<string, string> tObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(tDataAsJson);
			foreach (string tKey in tObj.Keys)
			{
				LocalizedTextManager.add(tKey, tObj[tKey], false, tFileName, false);
			}
		}
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x0013BD40 File Offset: 0x00139F40
	public static void add(string pKey, string pTranslation, bool pReplace = false, string pFileName = "", bool pCheckForCharacters = true)
	{
		pKey = pKey.Underscore();
		if (pReplace || !LocalizedTextManager.instance._localized_text.ContainsKey(pKey))
		{
			LocalizedTextManager.instance._localized_text[pKey] = pTranslation;
			LocalizedTextManager.instance._localized_text_files[pKey] = pFileName;
			return;
		}
		if (LocalizedTextManager.instance._localized_text[pKey] == pTranslation)
		{
			Debug.LogWarning("Skipped - already exists - " + pKey + " - exists : " + pTranslation);
			return;
		}
		Debug.LogError("Already exists - " + pKey + " - exists : " + LocalizedTextManager.instance._localized_text[pKey]);
		Debug.LogError("Already exists - " + pKey + " - skipped: " + pTranslation);
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x0013BDF8 File Offset: 0x00139FF8
	public void setLanguage(string pLanguage)
	{
		if (this.language == pLanguage && !this._lang_dirty)
		{
			return;
		}
		this._lang_dirty = false;
		Debug.Log("LOAD LANGUAGE " + pLanguage);
		string tPathCreatures = "locales/" + pLanguage + "/creatures";
		if (pLanguage != "boat" && pLanguage != "keys" && Resources.Load(tPathCreatures) == null)
		{
			pLanguage = PlayerConfig.detectLanguage();
		}
		bool tSave = false;
		if (this.language != "not_set")
		{
			tSave = true;
		}
		this.language = pLanguage;
		LocalizedTextManager.instance.loadLocalizedText(pLanguage);
		try
		{
			RestClient.DefaultRequestHeaders["wb-language"] = (this.language ?? "na");
		}
		catch (Exception)
		{
		}
		LocalizedTextManager.current_language = AssetManager.game_language_library.get(this.language);
		DebugLocales.init();
		LocalizedTextManager.updateTexts();
		if (PlayerConfig.dict["language"].stringVal != pLanguage)
		{
			tSave = true;
		}
		PlayerConfig.dict["language"].stringVal = pLanguage;
		if (tSave)
		{
			PlayerConfig.saveData();
		}
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x0013BF28 File Offset: 0x0013A128
	public static string langToCulture(string pLanguage = null)
	{
		if (pLanguage == null)
		{
			pLanguage = LocalizedTextManager.instance.language;
		}
		string tLang = pLanguage;
		if (tLang == "boat")
		{
			return "";
		}
		if (tLang == "keys")
		{
			return "";
		}
		if (tLang == "lb")
		{
			return "lb-LU";
		}
		if (tLang == "ka")
		{
			return "ka-GE";
		}
		if (tLang == "gr")
		{
			return "el-GR";
		}
		if (tLang == "hr")
		{
			return "hr-HR";
		}
		if (tLang == "by")
		{
			return "be-BY";
		}
		if (tLang == "ch")
		{
			return "zh-Hant";
		}
		if (tLang == "cz")
		{
			return "zh-Hans";
		}
		if (tLang == "fn")
		{
			return "fi-FI";
		}
		if (tLang == "ph")
		{
			return "fil-PH";
		}
		if (tLang == "gr")
		{
			return "fi";
		}
		if (tLang == "br")
		{
			return "pt";
		}
		if (tLang == "ko")
		{
			return "ko-KR";
		}
		if (tLang == "th")
		{
			return "th-TH";
		}
		if (tLang == "ua")
		{
			return "uk";
		}
		if (tLang == "no")
		{
			return "nb-NO";
		}
		if (tLang == "lt")
		{
			return "lt";
		}
		if (tLang == "vn")
		{
			return "vi";
		}
		return tLang;
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x0013C0B0 File Offset: 0x0013A2B0
	public static CultureInfo getCulture(string pLanguage = null)
	{
		string tCultureLang = LocalizedTextManager.langToCulture(pLanguage);
		if (tCultureLang != "")
		{
			try
			{
				return new CultureInfo(tCultureLang);
			}
			catch (CultureNotFoundException)
			{
				return CultureInfo.CurrentCulture;
			}
		}
		return CultureInfo.CurrentCulture;
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x0013C0FC File Offset: 0x0013A2FC
	public static CultureInfo getCurrentCulture()
	{
		return CultureInfo.CurrentCulture;
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x0013C104 File Offset: 0x0013A304
	public static bool cultureSupported()
	{
		string a = LocalizedTextManager.instance.language;
		return !(a == "boat") && !(a == "hi") && !(a == "by") && !(a == "ka") && !(a == "lb");
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x0013C160 File Offset: 0x0013A360
	public static List<string> getAllLanguages()
	{
		if (LocalizedTextManager._all_languages == null)
		{
			LocalizedTextManager._all_languages = new List<string>();
			foreach (TextAsset tLanguage in Resources.LoadAll<TextAsset>("locales"))
			{
				LocalizedTextManager._all_languages.Add(tLanguage.name);
			}
		}
		return LocalizedTextManager._all_languages;
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x0013C1B0 File Offset: 0x0013A3B0
	public static List<string> getAllLanguagesWithChanges()
	{
		if (LocalizedTextManager._changed_languages == null)
		{
			LocalizedTextManager._changed_languages = new List<string>();
			int tMaxLoadedLanguages = 10;
			foreach (TextAsset tLanguageAsset in Resources.LoadAll<TextAsset>("locales"))
			{
				string tLanguage = tLanguageAsset.name;
				string tFolder = TesterBehScreenshotFolder.getScreenshotFolder(tLanguage);
				if (!(tLanguage == "boat") && !(tLanguage == "keys"))
				{
					if (File.Exists(tFolder + "/" + tLanguage + ".json"))
					{
						string tLastJson = File.ReadAllText(tFolder + "/" + tLanguage + ".json", Encoding.UTF8);
						string tLanguageJSON = tLanguageAsset.text;
						Debug.Log(tLanguageJSON.Length.ToString() + " vs " + tLastJson.Length.ToString());
						if (tLastJson == tLanguageJSON)
						{
							Debug.Log("Language " + tLanguage + " has no changes");
							goto IL_13D;
						}
						Debug.Log("Language " + tLanguage + " has changes");
						File.Delete(tFolder + "/" + tLanguage + ".json");
					}
					LocalizedTextManager._changed_languages.Add(tLanguage);
					if (LocalizedTextManager._changed_languages.Count >= tMaxLoadedLanguages)
					{
						break;
					}
				}
				IL_13D:;
			}
		}
		return LocalizedTextManager._changed_languages;
	}

	// Token: 0x04001D29 RID: 7465
	private const string MISSING_LOCALE = "LOC_ER";

	// Token: 0x04001D2A RID: 7466
	private static List<string> _all_languages;

	// Token: 0x04001D2B RID: 7467
	private static List<string> _changed_languages;

	// Token: 0x04001D2C RID: 7468
	public static LocalizedTextManager instance;

	// Token: 0x04001D2D RID: 7469
	private static readonly Dictionary<string, string> _boat_strings = new Dictionary<string, string>();

	// Token: 0x04001D2E RID: 7470
	public static GameLanguageAsset current_language;

	// Token: 0x04001D2F RID: 7471
	private bool _lang_dirty;

	// Token: 0x04001D30 RID: 7472
	private Dictionary<string, string> _localized_text;

	// Token: 0x04001D31 RID: 7473
	private Dictionary<string, string> _localized_text_files;

	// Token: 0x04001D32 RID: 7474
	private Font _default_font;

	// Token: 0x04001D33 RID: 7475
	private Font _hindi_font;

	// Token: 0x04001D34 RID: 7476
	private Font _japanese_font;

	// Token: 0x04001D35 RID: 7477
	private Font _korean_font;

	// Token: 0x04001D36 RID: 7478
	private Font _arabic_font;

	// Token: 0x04001D37 RID: 7479
	private Font _persian_font;

	// Token: 0x04001D38 RID: 7480
	private Font _simplified_chinese_font;

	// Token: 0x04001D39 RID: 7481
	private Font _thai_font;

	// Token: 0x04001D3A RID: 7482
	public Font default_font;

	// Token: 0x04001D3B RID: 7483
	internal bool initiated;

	// Token: 0x04001D3C RID: 7484
	internal string language = "not_set";

	// Token: 0x04001D3D RID: 7485
	internal List<LocalizedText> texts;
}
