using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000130 RID: 304
[Serializable]
public class GameLanguageAsset : Asset
{
	// Token: 0x06000926 RID: 2342 RVA: 0x0008251B File Offset: 0x0008071B
	public IEnumerable<string> getGroups()
	{
		return this.translations.Keys;
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x00082528 File Offset: 0x00080728
	[JsonIgnore]
	public Dictionary<string, Dictionary<string, string>> translations
	{
		get
		{
			if (this._translations == null)
			{
				this._translations = new Dictionary<string, Dictionary<string, string>>();
				foreach (TextAsset textAsset in Resources.LoadAll<TextAsset>("locales/" + this.id))
				{
					string tDataAsJson = textAsset.text;
					string tFileName = textAsset.name;
					Dictionary<string, string> tObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(tDataAsJson);
					this._translations[tFileName] = tObj;
				}
			}
			return this._translations;
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00082598 File Offset: 0x00080798
	public GameLanguageData getLanguageData()
	{
		if (GameLanguageAsset._language_data == null)
		{
			TextAsset tTextAsset = Resources.Load<TextAsset>("texts/tooltip_translators");
			if (tTextAsset == null)
			{
				Debug.LogError("No tooltip translators found for language: " + this.id);
				return null;
			}
			GameLanguageAsset._language_data = JsonConvert.DeserializeObject<Dictionary<string, GameLanguageData>>(tTextAsset.text);
		}
		GameLanguageData tLanguageData;
		GameLanguageAsset._language_data.TryGetValue(this.id, out tLanguageData);
		return tLanguageData;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x000825FB File Offset: 0x000807FB
	public bool isRTL()
	{
		return this.is_rtl;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00082603 File Offset: 0x00080803
	public bool isHanzi()
	{
		return this.is_hanzi;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0008260B File Offset: 0x0008080B
	public bool isHindi()
	{
		return this.is_hindi;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00082613 File Offset: 0x00080813
	public bool hasForcedStyle()
	{
		return this.force_style != null;
	}

	// Token: 0x04000950 RID: 2384
	public string name;

	// Token: 0x04000951 RID: 2385
	public bool main;

	// Token: 0x04000952 RID: 2386
	public bool export = true;

	// Token: 0x04000953 RID: 2387
	public bool is_rtl;

	// Token: 0x04000954 RID: 2388
	public bool is_hanzi;

	// Token: 0x04000955 RID: 2389
	public bool is_hindi;

	// Token: 0x04000956 RID: 2390
	public bool debug_only;

	// Token: 0x04000957 RID: 2391
	public string path_icon;

	// Token: 0x04000958 RID: 2392
	public bool show_translators = true;

	// Token: 0x04000959 RID: 2393
	public FontGetter font = () => LocalizedTextManager.instance.default_font;

	// Token: 0x0400095A RID: 2394
	public ForcedFontStyle force_style;

	// Token: 0x0400095B RID: 2395
	private Dictionary<string, Dictionary<string, string>> _translations;

	// Token: 0x0400095C RID: 2396
	private static Dictionary<string, GameLanguageData> _language_data;
}
