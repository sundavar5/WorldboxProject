using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.UnityConverters.Math;
using UnityEngine;

// Token: 0x02000022 RID: 34
public abstract class BaseAssetLibrary
{
	// Token: 0x060001C1 RID: 449 RVA: 0x0000EB9D File Offset: 0x0000CD9D
	public virtual void init()
	{
		string version = Application.version;
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
	private static JsonSerializer _json_serializer
	{
		get
		{
			if (BaseAssetLibrary._json_serializer_internal == null)
			{
				BaseAssetLibrary._json_serializer_internal = JsonSerializer.Create(new JsonSerializerSettings
				{
					DefaultValueHandling = DefaultValueHandling.Ignore,
					Formatting = Formatting.Indented,
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					Culture = CultureInfo.InvariantCulture,
					ContractResolver = new OrderedContractResolver(),
					Converters = 
					{
						new DelegateConverter(),
						new StringEnumConverter(),
						new Color32Converter(),
						new ColorConverter(),
						new Vector2Converter(),
						new Vector2IntConverter(),
						new Vector3Converter(),
						new Vector3IntConverter(),
						new Vector4Converter()
					}
				});
			}
			return BaseAssetLibrary._json_serializer_internal;
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000EC90 File Offset: 0x0000CE90
	public void exportAssets()
	{
		if (this.id.StartsWith("beh"))
		{
			return;
		}
		if (this.id.StartsWith("debug"))
		{
			return;
		}
		string tPath = "GenAssets/wbassets" + "/" + this.id + ".json";
		try
		{
			using (FileStream fs = new FileStream(tPath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (StreamWriter sw = new StreamWriter(fs)
				{
					NewLine = "\n"
				})
				{
					using (JsonTextWriter writer = new JsonTextWriter(sw)
					{
						Formatting = Formatting.Indented,
						Indentation = 4,
						IndentChar = ' '
					})
					{
						BaseAssetLibrary._json_serializer.Serialize(writer, this);
					}
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Failed to export assets to " + tPath + ": " + e.Message);
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
	public void importAssets()
	{
		string tPath = "GenAssets/wbassets" + "/" + this.id + ".json";
		if (!File.Exists(tPath))
		{
			Debug.LogError("File not found: " + tPath);
			return;
		}
		using (FileStream fs = new FileStream(tPath, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			using (StreamReader sr = new StreamReader(fs))
			{
				using (JsonTextReader reader = new JsonTextReader(sr))
				{
					BaseAssetLibrary._json_serializer.Populate(reader, this);
				}
			}
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000EE50 File Offset: 0x0000D050
	public virtual void post_init()
	{
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000EE52 File Offset: 0x0000D052
	public virtual void linkAssets()
	{
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000EE54 File Offset: 0x0000D054
	public virtual void editorDiagnostic()
	{
		this.editorDiagnosticLocales();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000EE5C File Offset: 0x0000D05C
	public virtual void editorDiagnosticLocales()
	{
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000EE5E File Offset: 0x0000D05E
	public virtual void checkLocale(Asset pAsset, string pLocaleID)
	{
		if (!string.IsNullOrEmpty(pLocaleID) && !LocalizedTextManager.stringExists(pLocaleID))
		{
			BaseAssetLibrary.logAssetError("<e>" + pAsset.id + "</e>: Missing translation key", pLocaleID);
			AssetManager.missing_locale_keys.Add(pLocaleID);
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000EE97 File Offset: 0x0000D097
	internal bool hasSpriteInResources(string pPath)
	{
		return !(SpriteTextureLoader.getSprite(pPath) == null);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000EEAC File Offset: 0x0000D0AC
	internal bool hasSpriteInResourcesDebug(string pPath)
	{
		string tPath = Path.Combine(Path.Combine("Assets/Resources", pPath).Split(new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		}));
		return File.Exists(tPath + ".png") || (Directory.Exists(tPath) && Directory.GetFiles(tPath, "*.png", SearchOption.TopDirectoryOnly).Length != 0);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000EF14 File Offset: 0x0000D114
	protected void logErrorOpposites(string pMainTraitID, string pOppositeTraitID)
	{
		Debug.LogError(string.Concat(new string[]
		{
			"<color=#FF3232>[",
			pMainTraitID,
			"]</color> has opposite <color=#FFF832>[",
			pOppositeTraitID,
			"]</color>, but <color=#FFF832>[",
			pOppositeTraitID,
			"]</color> doesn't have opposite <color=#FF3232>[",
			pMainTraitID,
			"]</color>"
		}));
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000EF6C File Offset: 0x0000D16C
	private static string formatLog(string pMessage, string pRightPart = null)
	{
		if (pMessage.Contains("<"))
		{
			pMessage = pMessage.Replace("<e>", "<b><color=#FFFFFF>");
			pMessage = pMessage.Replace("</e>", "</color></b>");
		}
		string tStringLog = "<color=#D2B7FF>" + pMessage.Trim() + "</color>";
		if (!string.IsNullOrEmpty(pRightPart))
		{
			tStringLog = tStringLog + " : <b><color=#FFF832>" + pRightPart.Trim() + "</color></b>";
		}
		return tStringLog;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
	public static void logAssetLog(string pMessage, string pRightPart = null)
	{
		Debug.Log(BaseAssetLibrary.formatLog(pMessage, pRightPart));
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000EFEE File Offset: 0x0000D1EE
	public static void logAssetError(string pMessage, string pRightPart = null)
	{
		Debug.LogError(BaseAssetLibrary.formatLog(pMessage, pRightPart));
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000EFFC File Offset: 0x0000D1FC
	public virtual IEnumerable<Asset> getList()
	{
		yield break;
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000F005 File Offset: 0x0000D205
	public virtual int total_items
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x04000151 RID: 337
	private const string ERROR_COLOR_WHITE = "#FFFFFF";

	// Token: 0x04000152 RID: 338
	private const string ERROR_COLOR_RED = "#FF3232";

	// Token: 0x04000153 RID: 339
	private const string ERROR_COLOR_YELLOW = "#FFF832";

	// Token: 0x04000154 RID: 340
	private const string ERROR_COLOR_MAIN = "#D2B7FF";

	// Token: 0x04000155 RID: 341
	[JsonProperty(Order = -1)]
	public string id = "ASSET_LIBRARY";

	// Token: 0x04000156 RID: 342
	protected static int _latest_hash = 1;

	// Token: 0x04000157 RID: 343
	private static JsonSerializer _json_serializer_internal = null;
}
