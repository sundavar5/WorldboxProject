using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Assets.SimpleZip;
using db;
using Ionic.Zlib;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000595 RID: 1429
public class SaveManager : MonoBehaviour
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06002F72 RID: 12146 RVA: 0x00170133 File Offset: 0x0016E333
	private static JsonSerializerSettings _settings
	{
		get
		{
			return JsonHelper.read_settings;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06002F73 RID: 12147 RVA: 0x0017013A File Offset: 0x0016E33A
	private static JsonSerializer _reader
	{
		get
		{
			return JsonHelper.reader;
		}
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x00170141 File Offset: 0x0016E341
	private void Start()
	{
		SaveManager.persistentDataPath = Application.persistentDataPath;
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x0017014D File Offset: 0x0016E34D
	public static void clearCurrentSelectedWorld()
	{
		SaveManager.currentWorkshopMapData = null;
		SaveManager.currentSavePath = string.Empty;
		SaveManager.currentSlot = 0;
	}

	// Token: 0x06002F76 RID: 12150 RVA: 0x00170168 File Offset: 0x0016E368
	public void clickSaveSlot()
	{
		try
		{
			this.saveToCurrentPath();
			ScrollWindow.hideAllEvent(true);
		}
		catch (Exception message)
		{
			Debug.Log("Error during saving");
			Debug.LogError(message);
			ScrollWindow.showWindow("error_happened");
		}
	}

	// Token: 0x06002F77 RID: 12151 RVA: 0x001701B0 File Offset: 0x0016E3B0
	public SavedMap saveToCurrentPath()
	{
		return SaveManager.saveWorldToDirectory(SaveManager.currentSavePath, true, true);
	}

	// Token: 0x06002F78 RID: 12152 RVA: 0x001701BE File Offset: 0x0016E3BE
	public static SavedMap saveWorldToDirectory(string pFolder, bool pCompress = true, bool pCheckFolder = true)
	{
		pFolder = SaveManager.folderPath(pFolder);
		if (pCheckFolder)
		{
			if (!Directory.Exists(pFolder))
			{
				Directory.CreateDirectory(pFolder);
			}
		}
		else
		{
			Directory.CreateDirectory(pFolder);
		}
		SaveManager.saveImagePreview(pFolder);
		return SaveManager.saveMapData(pFolder, pCompress);
	}

	// Token: 0x06002F79 RID: 12153 RVA: 0x001701F0 File Offset: 0x0016E3F0
	public static SavedMap currentWorldToSavedMap()
	{
		World.world.items.diagnostic();
		SavedMap savedMap = new SavedMap();
		savedMap.create();
		return savedMap;
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x0017020C File Offset: 0x0016E40C
	public static void deleteSavePath(string pPath)
	{
		if (Directory.Exists(pPath))
		{
			Directory.Delete(pPath, true);
		}
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x0017021D File Offset: 0x0016E41D
	public static void deleteCurrentSave()
	{
		SaveManager.deleteSavePath(SaveManager.currentSavePath);
		FavoriteWorld.checkFavoriteWorld();
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x06002F7C RID: 12156 RVA: 0x00170234 File Offset: 0x0016E434
	public static SavedMap saveMapData(string pFolder, bool pCompress = true)
	{
		SavedMap tSave = SaveManager.currentWorldToSavedMap();
		pFolder = SaveManager.folderPath(pFolder);
		bool hasError = false;
		string tPathSaveData = "";
		string tTempPath = "";
		string tWhat = "Map";
		SaveManager.saveMetaData(tSave.getMeta(), pFolder);
		SaveManager.saveStatsIn(pFolder);
		try
		{
			if (pCompress)
			{
				tPathSaveData = pFolder + "map.wbox";
				tTempPath = tPathSaveData + ".tmp";
				tSave.toZip(tTempPath);
			}
			else
			{
				tPathSaveData = pFolder + "map.wbax";
				tTempPath = tPathSaveData + ".tmp";
				tSave.toJson(tTempPath);
			}
		}
		catch (IOException e)
		{
			if (Toolbox.IsDiskFull(e))
			{
				WorldTip.showNow("Error saving " + tWhat + " : Disk full!", false, "top", 3f, "#F3961F");
			}
			else
			{
				Debug.Log("Could not save " + tWhat + " due to hard drive / IO Error : ");
				Debug.Log(e);
				WorldTip.showNow("Error saving " + tWhat + " due to IOError! Check console for details", false, "top", 3f, "#F3961F");
			}
			hasError = true;
		}
		catch (Exception message)
		{
			Debug.Log("Could not save " + tWhat + " due to error : ");
			Debug.Log(message);
			WorldTip.showNow("Error saving " + tWhat + "! Check console for errors", false, "top", 3f, "#F3961F");
			hasError = true;
		}
		if (hasError)
		{
			if (File.Exists(tTempPath))
			{
				File.Delete(tTempPath);
			}
		}
		else
		{
			Toolbox.MoveSafely(tTempPath, tPathSaveData);
		}
		return tSave;
	}

	// Token: 0x06002F7D RID: 12157 RVA: 0x001703B8 File Offset: 0x0016E5B8
	public static void saveMetaData(MapMetaData pMetaData, string pPath)
	{
		pMetaData.prepareForSave();
		SaveManager.saveMetaIn(pPath, pMetaData);
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x001703C7 File Offset: 0x0016E5C7
	public static MapMetaData getCurrentMeta()
	{
		return SaveManager.getMetaFor(SaveManager.currentSavePath);
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x001703D3 File Offset: 0x0016E5D3
	public static MapMetaData getMetaFor(int pSlot)
	{
		return SaveManager.getMetaFor(SaveManager.getSlotSavePath(pSlot));
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x001703E0 File Offset: 0x0016E5E0
	public static MapMetaData getMetaFor(string pFolder)
	{
		pFolder = SaveManager.folderPath(pFolder);
		if (!SaveManager.doesSaveExist(pFolder))
		{
			return null;
		}
		string tPath = SaveManager.generateMetaPath(pFolder);
		if (!File.Exists(tPath))
		{
			SavedMap mData = SaveManager.getMapFromPath(pFolder, false);
			if (mData != null)
			{
				mData.check();
				SaveManager.saveMetaData(mData.getMeta(), pFolder);
				Config.scheduleGC("getMetaFor", false);
			}
		}
		if (File.Exists(tPath))
		{
			try
			{
				using (FileStream fs = new FileStream(tPath, FileMode.Open, FileAccess.Read))
				{
					using (StreamReader sr = new StreamReader(fs))
					{
						using (JsonReader reader = new JsonTextReader(sr))
						{
							MapMetaData mapMetaData = SaveManager._reader.Deserialize<MapMetaData>(reader);
							if (mapMetaData == null)
							{
								throw new Exception("Meta data was null");
							}
							return mapMetaData;
						}
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogWarning(message);
				try
				{
					File.Delete(tPath);
				}
				catch (Exception message2)
				{
					Debug.Log(message2);
				}
				return null;
			}
		}
		return null;
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x001704F4 File Offset: 0x0016E6F4
	public static bool loadStatsFrom(string pFolder)
	{
		return SaveManager.doesStatsDBExist(pFolder) && DBManager.loadDBFrom(SaveManager.generateStatsPath(pFolder));
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x0017050B File Offset: 0x0016E70B
	public static bool doesStatsDBExist(string pFolder)
	{
		pFolder = SaveManager.folderPath(pFolder);
		return SaveManager.doesSaveExist(pFolder) && File.Exists(SaveManager.generateStatsPath(pFolder));
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x0017052C File Offset: 0x0016E72C
	public static string getMapCreationTime(string pFolder)
	{
		string tPath = SaveManager.getSavePath(pFolder);
		if (!File.Exists(tPath))
		{
			return "??";
		}
		DateTime tFutureTime = DateTime.UtcNow.AddDays(7.0);
		DateTime tTime = File.GetLastWriteTime(tPath);
		if (tTime.Year < 2017)
		{
			return "GREG";
		}
		if (tTime > tFutureTime)
		{
			return "DREDD";
		}
		CultureInfo tCulture = LocalizedTextManager.getCulture(null);
		string tShortDatePattern = tCulture.DateTimeFormat.ShortDatePattern;
		string tShortTimePattern = tCulture.DateTimeFormat.ShortTimePattern;
		return tTime.ToString(tShortDatePattern + " " + tShortTimePattern, tCulture);
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x001705C8 File Offset: 0x0016E7C8
	public static void saveMetaIn(string pFolder, MapMetaData pMetaData)
	{
		string tPath = SaveManager.generateMetaPath(pFolder);
		string mapMeta = pMetaData.toJson();
		Toolbox.WriteSafely("Map Meta", tPath, ref mapMeta);
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x001705F1 File Offset: 0x0016E7F1
	public static void saveStatsIn(string pFolder)
	{
		DBManager.saveToPath(SaveManager.generateStatsPath(pFolder));
	}

	// Token: 0x06002F86 RID: 12166 RVA: 0x00170600 File Offset: 0x0016E800
	public static bool doesSaveExist(string pFolder)
	{
		return Directory.Exists(pFolder) && (File.Exists(pFolder + "map.wbox") || File.Exists(pFolder + "map.wbax") || File.Exists(pFolder + "map.json"));
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x00170654 File Offset: 0x0016E854
	public static string getSavePath(string pFolder)
	{
		string tFolder = SaveManager.folderPath(pFolder);
		if (!Directory.Exists(tFolder))
		{
			return null;
		}
		string tPathToFile = tFolder + "map.wbox";
		if (File.Exists(tPathToFile))
		{
			return tPathToFile;
		}
		tPathToFile = tFolder + "map.wbax";
		if (File.Exists(tPathToFile))
		{
			return tPathToFile;
		}
		tPathToFile = tFolder + "map.json";
		if (File.Exists(tPathToFile))
		{
			return tPathToFile;
		}
		return null;
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x001706B5 File Offset: 0x0016E8B5
	public static bool slotExists(int pSlot)
	{
		return File.Exists(SaveManager.getSlotPathWbox(pSlot)) || File.Exists(SaveManager.getOldSlotPath(pSlot));
	}

	// Token: 0x06002F89 RID: 12169 RVA: 0x001706D6 File Offset: 0x0016E8D6
	public static bool slotMetaExists(int pSlot = -1)
	{
		return File.Exists(SaveManager.getMetaSlotPath(pSlot));
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x001706E8 File Offset: 0x0016E8E8
	public static void copyDataToClipboard()
	{
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x001706EC File Offset: 0x0016E8EC
	private static void saveImagePreview(string pFolder)
	{
		Texture2D tImagePreviewMain = PreviewHelper.convertMapToTexture();
		Texture2D tImagePreviewSmall = new Texture2D(tImagePreviewMain.width, tImagePreviewMain.height);
		Graphics.CopyTexture(tImagePreviewMain, tImagePreviewSmall);
		byte[] tBytes = tImagePreviewMain.EncodeToPNG();
		Toolbox.WriteSafely("PNG Preview", SaveManager.generatePngPreviewPath(pFolder), tBytes);
		Object.Destroy(tImagePreviewMain);
		TextureScale.Point(tImagePreviewSmall, 32, 32);
		byte[] tBytes2 = tImagePreviewSmall.EncodeToPNG();
		Toolbox.WriteSafely("PNG Small Preview", SaveManager.generatePngSmallPreviewPath(pFolder), tBytes2);
		Object.Destroy(tImagePreviewSmall);
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x00170764 File Offset: 0x0016E964
	public static int getCurrentSlot()
	{
		return SaveManager.currentSlot;
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x0017076B File Offset: 0x0016E96B
	public static string getSlotSavePath(int pSlot)
	{
		return SaveManager.generateMainPath("saves") + SaveManager.folderPath("save" + pSlot.ToString());
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x00170792 File Offset: 0x0016E992
	public static string generateMainPath(string pFolder)
	{
		return SaveManager.folderPath(SaveManager.persistentDataPath) + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x001707A9 File Offset: 0x0016E9A9
	public static string generateAutosavesPath(string pFolder = "")
	{
		return SaveManager.generateMainPath("autosaves") + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x001707C0 File Offset: 0x0016E9C0
	public static string generateWorkshopPath(string pFolder = "")
	{
		return SaveManager.generateMainPath("workshop_upload") + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x001707D7 File Offset: 0x0016E9D7
	public static string generatePngPreviewPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "preview.png";
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x001707E9 File Offset: 0x0016E9E9
	public static string generatePngSmallPreviewPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "preview_small.png";
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x001707FB File Offset: 0x0016E9FB
	public static string generateMetaPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "map.meta";
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x0017080D File Offset: 0x0016EA0D
	public static string generateStatsPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "map_stats.s3db";
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x0017081F File Offset: 0x0016EA1F
	public static string getSlotPathWbox(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.wbox";
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x00170831 File Offset: 0x0016EA31
	public static string getMetaSlotPath(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.meta";
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x00170843 File Offset: 0x0016EA43
	public static string getOldSlotPath(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.json";
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x00170855 File Offset: 0x0016EA55
	public static string getPngSlotPath(int pSlot = -1)
	{
		return SaveManager.getSlotSavePath(pSlot) + "preview.png";
	}

	// Token: 0x06002F99 RID: 12185 RVA: 0x00170867 File Offset: 0x0016EA67
	public static void setCurrentSlot(int pSlotID)
	{
		SaveManager.currentSlot = pSlotID;
		SaveManager.currentSavePath = SaveManager.generateMainPath("saves") + SaveManager.folderPath("save" + pSlotID.ToString());
	}

	// Token: 0x06002F9A RID: 12186 RVA: 0x00170899 File Offset: 0x0016EA99
	public static void setCurrentPath(string pPath)
	{
		SaveManager.currentSavePath = SaveManager.folderPath(pPath);
	}

	// Token: 0x06002F9B RID: 12187 RVA: 0x001708A6 File Offset: 0x0016EAA6
	public static void setCurrentPathAndId(string pPath, int pSlotID)
	{
		SaveManager.currentSlot = pSlotID;
		SaveManager.currentSavePath = SaveManager.folderPath(pPath);
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x001708B9 File Offset: 0x0016EAB9
	public static string getCurrentPreviewPath()
	{
		return SaveManager.currentSavePath + "preview.png";
	}

	// Token: 0x06002F9D RID: 12189 RVA: 0x001708CA File Offset: 0x0016EACA
	public static bool currentSlotExists()
	{
		return SaveManager.doesSaveExist(SaveManager.currentSavePath);
	}

	// Token: 0x06002F9E RID: 12190 RVA: 0x001708D6 File Offset: 0x0016EAD6
	public static bool currentPreviewExists()
	{
		return File.Exists(SaveManager.currentSavePath + "preview.png");
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x001708EC File Offset: 0x0016EAEC
	public static bool currentMetaExists()
	{
		return File.Exists(SaveManager.currentSavePath + "map.meta");
	}

	// Token: 0x06002FA0 RID: 12192 RVA: 0x00170902 File Offset: 0x0016EB02
	internal void loadWorld()
	{
		SaveManager._loading_animation_active = false;
		this.prepareLoading();
		if (SaveManager.currentWorkshopMapData != null)
		{
			this.loadWorld(SaveManager.currentWorkshopMapData.main_path, true);
			SaveManager.currentWorkshopMapData = null;
			return;
		}
		this.loadWorld(SaveManager.currentSavePath, false);
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x0017093C File Offset: 0x0016EB3C
	internal void loadWorld(string pPath, bool pLoadWorkshop = false)
	{
		SmoothLoader.prepare();
		try
		{
			SavedMap tSave = SaveManager.getMapFromPath(pPath, pLoadWorkshop);
			if (tSave == null)
			{
				throw new Exception("Save file not found - has it been deleted?");
			}
			Debug.Log("World Loaded");
			tSave.check();
			Debug.Log("World Laws Loaded");
			Config.scheduleGC("load world", false);
			this.loadData(tSave, pPath);
		}
		catch (Exception e)
		{
			Debug.Log("Error during loading of slot " + pPath);
			try
			{
				MapMetaData tData = SaveManager.getMetaFor(pPath);
				if (tData != null)
				{
					Debug.Log(JsonUtility.ToJson(tData));
				}
				else
				{
					Debug.Log("No meta data");
				}
			}
			catch (Exception message)
			{
				Debug.Log("Failed to load meta data");
				Debug.Log(message);
			}
			Debug.LogError(e);
			ScrollWindow.showWindow("error_happened");
			MapBox.instance.startTheGame(true);
		}
	}

	// Token: 0x06002FA2 RID: 12194 RVA: 0x00170A14 File Offset: 0x0016EC14
	public static void loadMapFromResources(string pPath)
	{
		SmoothLoader.prepare();
		SavedMap tSave = JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress((Resources.Load(pPath) as TextAsset).bytes), SaveManager._settings);
		tSave.check();
		World.world.save_manager.loadData(tSave, null);
	}

	// Token: 0x06002FA3 RID: 12195 RVA: 0x00170A60 File Offset: 0x0016EC60
	public static void loadMapFromBytes(byte[] pMapData)
	{
		SmoothLoader.prepare();
		SavedMap tSave = JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(pMapData), SaveManager._settings);
		tSave.check();
		World.world.save_manager.loadData(tSave, null);
	}

	// Token: 0x06002FA4 RID: 12196 RVA: 0x00170A9C File Offset: 0x0016EC9C
	public static SavedMap getMapFromPath(string pMainPath, bool pLoadWorkshop = false)
	{
		if (pLoadWorkshop)
		{
			return JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(File.ReadAllBytes(SaveManager.folderPath(SaveManager.currentWorkshopMapData.main_path) + "map.wbox")), SaveManager._settings);
		}
		pMainPath = SaveManager.folderPath(pMainPath);
		if (!Directory.Exists(pMainPath))
		{
			Debug.Log("Directory does not exist : " + pMainPath);
			return null;
		}
		string tPathToFile = pMainPath + "map.wbox";
		if (File.Exists(tPathToFile))
		{
			SaveManager.fileInfo(tPathToFile);
			using (FileStream fs = new FileStream(tPathToFile, FileMode.Open, FileAccess.Read))
			{
				using (ZlibStream zs = new ZlibStream(fs, CompressionMode.Decompress))
				{
					using (StreamReader sr = new StreamReader(zs))
					{
						using (JsonReader reader = new JsonTextReader(sr))
						{
							return SaveManager._reader.Deserialize<SavedMap>(reader);
						}
					}
				}
			}
		}
		Debug.Log("Does not exist : " + tPathToFile);
		tPathToFile = pMainPath + "map.wbax";
		if (File.Exists(tPathToFile))
		{
			SaveManager.fileInfo(tPathToFile);
			using (FileStream fs2 = new FileStream(tPathToFile, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr2 = new StreamReader(fs2))
				{
					using (JsonReader reader2 = new JsonTextReader(sr2))
					{
						return SaveManager._reader.Deserialize<SavedMap>(reader2);
					}
				}
			}
		}
		Debug.Log("Does not exist : " + tPathToFile);
		tPathToFile = pMainPath + "map.json";
		if (File.Exists(tPathToFile))
		{
			SaveManager.fileInfo(tPathToFile);
			return JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(File.ReadAllText(tPathToFile)), SaveManager._settings);
		}
		Debug.Log("Does not exist : " + tPathToFile);
		return null;
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x00170C9C File Offset: 0x0016EE9C
	private static void fileInfo(string full_path)
	{
		try
		{
			FileInfo fi = new FileInfo(full_path);
			Debug.Log("Loading          : " + fi.Name);
			Debug.Log("Size             : " + fi.Length.ToString());
			Debug.Log("Creation time    : " + fi.CreationTime.ToString());
			Debug.Log("Last access time : " + fi.LastAccessTime.ToString());
			Debug.Log("Last write time  : " + fi.LastWriteTime.ToString());
			string str = "Folder           : ";
			DirectoryInfo directory = fi.Directory;
			Debug.Log(str + ((directory != null) ? directory.Name : null));
		}
		catch (Exception message)
		{
			Debug.Log("Error when getting file info for");
			Debug.Log(full_path);
			Debug.Log(message);
		}
	}

	// Token: 0x06002FA6 RID: 12198 RVA: 0x00170D84 File Offset: 0x0016EF84
	public void startLoadSlot()
	{
		SaveManager._loading_animation_active = true;
		this.prepareLoading();
		World.world.transition_screen.startTransition(new LoadingScreen.TransitionAction(this.loadWorld));
	}

	// Token: 0x06002FA7 RID: 12199 RVA: 0x00170DAD File Offset: 0x0016EFAD
	private void prepareLoading()
	{
		ScrollWindow.hideAllEvent(true);
		World.world.nameplate_manager.clearAll();
	}

	// Token: 0x06002FA8 RID: 12200 RVA: 0x00170DC4 File Offset: 0x0016EFC4
	private void loadTiles(string pString)
	{
		string[] tSaveAllTiles = pString.Split(',', StringSplitOptions.None);
		int width = Config.ZONE_AMOUNT_X;
		int zone_AMOUNT_Y = Config.ZONE_AMOUNT_Y;
		if (this.data.saveVersion < 7)
		{
			string[] tString = new string[World.world.tiles_list.Length];
			int tSkip = 0;
			int iX = 0;
			int iY = 0;
			for (int i = 0; i < tSaveAllTiles.Length; i++)
			{
				if (iX >= 50 * width)
				{
					iX = 0;
					iY++;
					tSkip += 14 * width;
				}
				int tNewId = i + tSkip;
				if (tNewId <= tString.Length)
				{
					tString[tNewId] = tSaveAllTiles[i];
					iX++;
				}
			}
			tSaveAllTiles = tString;
		}
		for (int j = 0; j < World.world.tiles_list.Length; j++)
		{
			WorldTile tTile = World.world.tiles_list[j];
			if (tTile.data.tile_id >= tSaveAllTiles.Length || tSaveAllTiles[tTile.data.tile_id] == null)
			{
				tTile.setTileType("deep_ocean");
			}
			else
			{
				string[] tSaveTile = tSaveAllTiles[tTile.data.tile_id].Split(':', StringSplitOptions.None);
				string tTileData;
				if (tSaveTile.Length != 2)
				{
					tTileData = null;
				}
				else
				{
					tTileData = tSaveTile[1];
				}
				this._tile_id_main = tSaveTile[0];
				this._tile_id_top = string.Empty;
				this.convertOldTilesToNewOnes();
				if (string.IsNullOrEmpty(this._tile_id_top))
				{
					tTile.setTileType(this._tile_id_main);
				}
				else
				{
					tTile.setTileTypes(this._tile_id_main, AssetManager.top_tiles.get(this._tile_id_top));
				}
				tTile.Height = tTile.Type.height_min;
				if (tTileData != null)
				{
					if (tTileData.Contains("fire"))
					{
						tTile.setFireData(true);
					}
					if (tTileData.Contains("conv0"))
					{
						tTile.data.conwayType = ConwayType.Eater;
						World.world.conway_layer.add(tTile, "conway");
					}
					if (tTileData.Contains("conv1"))
					{
						tTile.data.conwayType = ConwayType.Creator;
						World.world.conway_layer.add(tTile, "conway_inverse");
					}
				}
			}
		}
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x00170FC4 File Offset: 0x0016F1C4
	private void convertPermafrost()
	{
		string tile_id_top = this._tile_id_top;
		if (tile_id_top == "snow_low")
		{
			this._tile_id_top = "permafrost_low";
			return;
		}
		if (tile_id_top == "snow_high")
		{
			this._tile_id_top = "permafrost_high";
			return;
		}
		if (!(tile_id_top == "snow_sand") && !(tile_id_top == "ice") && !(tile_id_top == "snow_hills") && !(tile_id_top == "snow_block"))
		{
			return;
		}
		this._tile_id_top = string.Empty;
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x0017104C File Offset: 0x0016F24C
	private void convertOldTilesToNewOnes()
	{
		if (this._tile_id_main.Contains("road"))
		{
			this._tile_id_main = "soil_low";
			this._tile_id_top = "road";
		}
		string tile_id_main = this._tile_id_main;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(tile_id_main);
		if (num <= 3134936370U)
		{
			if (num <= 1332360248U)
			{
				if (num <= 624473654U)
				{
					if (num != 164218702U)
					{
						if (num != 174734082U)
						{
							if (num != 624473654U)
							{
								goto IL_525;
							}
							if (!(tile_id_main == "hills_frozen"))
							{
								goto IL_525;
							}
							this._tile_id_main = "mountains";
							this._tile_id_top = "snow_block";
							return;
						}
						else
						{
							if (!(tile_id_main == "soil"))
							{
								goto IL_525;
							}
							this._tile_id_main = "soil_low";
							return;
						}
					}
					else
					{
						if (!(tile_id_main == "deep_ocean"))
						{
							goto IL_525;
						}
						return;
					}
				}
				else if (num != 1309421833U)
				{
					if (num != 1328097888U)
					{
						if (num != 1332360248U)
						{
							goto IL_525;
						}
						if (!(tile_id_main == "soil_creep"))
						{
							goto IL_525;
						}
						this._tile_id_main = "soil_low";
						this._tile_id_top = "tumor_low";
						return;
					}
					else
					{
						if (!(tile_id_main == "forest"))
						{
							goto IL_525;
						}
						goto IL_449;
					}
				}
				else
				{
					if (!(tile_id_main == "soil_frozen"))
					{
						goto IL_525;
					}
					this._tile_id_main = "soil_low";
					this._tile_id_top = "permafrost_low";
					return;
				}
			}
			else if (num <= 2043630411U)
			{
				if (num != 1584873562U)
				{
					if (num != 1736598119U)
					{
						if (num != 2043630411U)
						{
							goto IL_525;
						}
						if (!(tile_id_main == "shallow_waters_frozen"))
						{
							goto IL_525;
						}
						this._tile_id_main = "shallow_waters";
						this._tile_id_top = "ice";
						return;
					}
					else
					{
						if (!(tile_id_main == "field"))
						{
							goto IL_525;
						}
						this._tile_id_main = "soil_low";
						this._tile_id_top = "field";
						return;
					}
				}
				else
				{
					if (!(tile_id_main == "forest_soil"))
					{
						goto IL_525;
					}
					this._tile_id_main = "soil_high";
					return;
				}
			}
			else if (num <= 2795768366U)
			{
				if (num != 2740153745U)
				{
					if (num != 2795768366U)
					{
						goto IL_525;
					}
					if (!(tile_id_main == "mountains_frozen"))
					{
						goto IL_525;
					}
					this._tile_id_main = "mountains";
					this._tile_id_top = "snow_block";
					return;
				}
				else
				{
					if (!(tile_id_main == "forest_soil_frozen"))
					{
						goto IL_525;
					}
					this._tile_id_main = "soil_high";
					this._tile_id_top = "permafrost_high";
					return;
				}
			}
			else if (num != 2993663101U)
			{
				if (num != 3134936370U)
				{
					goto IL_525;
				}
				if (!(tile_id_main == "close_ocean"))
				{
					goto IL_525;
				}
				return;
			}
			else if (!(tile_id_main == "grass"))
			{
				goto IL_525;
			}
		}
		else if (num <= 3365615729U)
		{
			if (num <= 3315282872U)
			{
				if (num != 3185372622U)
				{
					if (num != 3189014883U)
					{
						if (num != 3315282872U)
						{
							goto IL_525;
						}
						if (!(tile_id_main == "lava3"))
						{
							goto IL_525;
						}
						return;
					}
					else
					{
						if (!(tile_id_main == "sand"))
						{
							goto IL_525;
						}
						this._tile_id_main = "sand";
						return;
					}
				}
				else
				{
					if (!(tile_id_main == "sand_frozen"))
					{
						goto IL_525;
					}
					this._tile_id_main = "sand";
					this._tile_id_top = "snow_sand";
					return;
				}
			}
			else if (num != 3332060491U)
			{
				if (num != 3348838110U)
				{
					if (num != 3365615729U)
					{
						goto IL_525;
					}
					if (!(tile_id_main == "lava0"))
					{
						goto IL_525;
					}
					return;
				}
				else
				{
					if (!(tile_id_main == "lava1"))
					{
						goto IL_525;
					}
					return;
				}
			}
			else
			{
				if (!(tile_id_main == "lava2"))
				{
					goto IL_525;
				}
				return;
			}
		}
		else if (num <= 3635428898U)
		{
			if (num != 3517725693U)
			{
				if (num != 3632974288U)
				{
					if (num != 3635428898U)
					{
						goto IL_525;
					}
					if (!(tile_id_main == "grass_flowers"))
					{
						goto IL_525;
					}
				}
				else
				{
					if (!(tile_id_main == "shallow_waters"))
					{
						goto IL_525;
					}
					return;
				}
			}
			else
			{
				if (!(tile_id_main == "sand_creep"))
				{
					goto IL_525;
				}
				this._tile_id_main = "sand";
				this._tile_id_top = "tumor_low";
				return;
			}
		}
		else if (num <= 4171135248U)
		{
			if (num != 4022750299U)
			{
				if (num != 4171135248U)
				{
					goto IL_525;
				}
				if (!(tile_id_main == "fuse"))
				{
					goto IL_525;
				}
				this._tile_id_main = "soil_low";
				this._tile_id_top = "fuse";
				return;
			}
			else
			{
				if (!(tile_id_main == "hills"))
				{
					goto IL_525;
				}
				return;
			}
		}
		else if (num != 4227381031U)
		{
			if (num != 4243777795U)
			{
				goto IL_525;
			}
			if (!(tile_id_main == "mountains"))
			{
				goto IL_525;
			}
			return;
		}
		else
		{
			if (!(tile_id_main == "forest_flowers"))
			{
				goto IL_525;
			}
			goto IL_449;
		}
		this._tile_id_main = "soil_low";
		this._tile_id_top = "grass_low";
		return;
		IL_449:
		this._tile_id_main = "soil_high";
		this._tile_id_top = "grass_high";
		return;
		IL_525:
		this._tile_id_main = "soil_low";
	}

	// Token: 0x06002FAB RID: 12203 RVA: 0x0017158C File Offset: 0x0016F78C
	private void loadTileArray(SavedMap pData)
	{
		if (pData.tileAmounts.Length == 0)
		{
			return;
		}
		int maxTiles = pData.width * pData.height * 64 * 64;
		int tTileIndex = 0;
		for (int i = 0; i < pData.tileArray.Length; i++)
		{
			for (int j = 0; j < pData.tileArray[i].Length; j++)
			{
				this._tile_id_top = string.Empty;
				this._tile_id_main = (pData.tileMap[pData.tileArray[i][j]] ?? "deep_ocean");
				if (this._tile_id_main.Contains(":"))
				{
					string[] tSplitID = this._tile_id_main.Split(':', StringSplitOptions.None);
					this._tile_id_main = tSplitID[0];
					this._tile_id_top = tSplitID[1];
				}
				if (pData.saveVersion < 9)
				{
					this.convertOldTilesToNewOnes();
				}
				if (pData.saveVersion < 10)
				{
					this.convertPermafrost();
				}
				for (int k = 0; k < pData.tileAmounts[i][j]; k++)
				{
					WorldTile tTile = World.world.tiles_list[tTileIndex++];
					if (string.IsNullOrEmpty(this._tile_id_top))
					{
						tTile.setTileType(this._tile_id_main);
					}
					else
					{
						tTile.setTileTypes(this._tile_id_main, AssetManager.top_tiles.get(this._tile_id_top));
					}
					tTile.Height = tTile.Type.height_min;
				}
			}
		}
		while (tTileIndex + 1 < maxTiles)
		{
			World.world.tiles_list[tTileIndex++].setTileType("deep_ocean");
		}
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x00171710 File Offset: 0x0016F910
	private void loadConway(List<int> conv0, List<int> conv1)
	{
		if (conv0.Count == 0 && conv1.Count == 0)
		{
			return;
		}
		for (int i = 0; i < conv0.Count; i++)
		{
			World.world.tiles_list[conv0[i]].data.conwayType = ConwayType.Eater;
			World.world.conway_layer.add(World.world.tiles_list[conv0[i]], "conway");
		}
		for (int j = 0; j < conv1.Count; j++)
		{
			World.world.tiles_list[conv1[j]].data.conwayType = ConwayType.Creator;
			World.world.conway_layer.add(World.world.tiles_list[conv1[j]], "conway_inverse");
		}
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x001717D8 File Offset: 0x0016F9D8
	private void loadFrozen(List<int> pTileList)
	{
		if (pTileList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < pTileList.Count; i++)
		{
			int tId = pTileList[i];
			World.world.tiles_list[tId].freeze(10);
		}
	}

	// Token: 0x06002FAE RID: 12206 RVA: 0x0017181C File Offset: 0x0016FA1C
	private void loadFire(List<int> pTileList)
	{
		if (pTileList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < pTileList.Count; i++)
		{
			int tId = pTileList[i];
			World.world.tiles_list[tId].setFireData(true);
		}
	}

	// Token: 0x06002FAF RID: 12207 RVA: 0x00171860 File Offset: 0x0016FA60
	public void loadData(SavedMap pData, string pPath = null)
	{
		this.data = pData;
		Debug.Log("Save Version " + this.data.saveVersion.ToString());
		SaveConverter.convert(this.data);
		World.world.addClearWorld(pData.width, pData.height);
		SmoothLoader.add(delegate
		{
			ScrollWindow.hideAllEvent(true);
		}, "Hiding All Windows", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.setMapSize(pData.width, pData.height);
			World.world.hotkey_tabs_data = pData.hotkey_tabs_data;
			World.world.map_stats = pData.mapStats;
			World.world.map_stats.load();
			World.world.world_laws = pData.worldLaws;
			this.checkWorldLawsLoad();
			AssetManager.gene_library.regenerateBasicDNACodesWithLifeSeed(pData.mapStats.life_dna);
			World.world.era_manager.loadAge();
		}, "Setting Map Size", false, 0.001f, false);
		if (!Config.disable_db)
		{
			bool flag = string.IsNullOrEmpty(pPath) || !SaveManager.doesStatsDBExist(pPath);
			if (!flag)
			{
				SmoothLoader.add(delegate
				{
					if (!SaveManager.loadStatsFrom(pPath))
					{
						DBManager.createDB();
					}
				}, "Loading Stats DB", false, 0.001f, false);
			}
			else
			{
				SmoothLoader.add(delegate
				{
					DBManager.createDB();
				}, "Creating Stats DB", false, 0.001f, false);
			}
			SmoothLoader.add(delegate
			{
				DBTables.checkTablesOK(true);
			}, "Checking Stats DB", false, 0.001f, false);
			DBTables.createOrMigrateTablesLoader(flag);
		}
		WindowPreloader.addWaitForWindowResources();
		if (pData.saveVersion < 8)
		{
			if (pData.saveVersion == 0)
			{
				SmoothLoader.add(new MapLoaderAction(this.loadAncientTiles), "LOADING ANCIENT TILES", false, 0.001f, false);
			}
			SmoothLoader.add(delegate
			{
				this.loadTiles(pData.tileString);
			}, "Loading Very Old Tiles. Like super old. Maybe you should like, re-save your world?", false, 0.001f, false);
		}
		else if (pData.saveVersion > 7)
		{
			SmoothLoader.add(delegate
			{
				this.loadTileArray(pData);
			}, "Loading Tiles", false, 0.001f, false);
			SmoothLoader.add(delegate
			{
				this.loadFrozen(pData.frozen_tiles);
			}, "Loading Frozen", false, 0.001f, false);
			SmoothLoader.add(delegate
			{
				this.loadFire(pData.fire);
			}, "Loading Fires", false, 0.001f, false);
			SmoothLoader.add(delegate
			{
				this.loadConway(pData.conwayEater, pData.conwayCreator);
			}, "Loading Conway", false, 0.001f, false);
		}
		SmoothLoader.add(new MapLoaderAction(this.loadSubspecies), "Loading Subspecies", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadFamilies), "Loading Families", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadLanguages), "Loading Languages", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadReligions), "Loading Religions", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadItems), "Loading Items", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadBooks), "Loading Books", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadCultures), "Loading Cultures", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadClans), "Loading Clans", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadKingdoms), "Loading Kingdoms", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadCities), "Loading Cities", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadWars), "Loading Wars", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadArmies), "Loading Armies", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadAlliances), "Loading Alliances", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadPlots), "Loading Plots", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadActors), "Finish Loading Actors", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.randomDecisionCooldowns), "Set random Decision Cooldowns", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadActorLovers), "Loading Lovers", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadArmyCaptain), "Loading Army Captains", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadPlotAuthors), "Loading Plot Authors", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			SaveConverter.checkOldCityZones(this.data);
		}, "Check Old City Zones", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadBuildings), "Loading Buildings", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.checkSimManagerLists();
		}, "Check Meta List Stuff", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.setHomeBuildings), "Set Home Buildings", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadCivs02), "Loading Civs", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadLeaders), "Loading Leaders", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadDiplomacy), "Loading Diplomacy", false, 0.001f, false);
		World.world.addUnloadResources();
		SmoothLoader.add(delegate
		{
			World.world.map_chunk_manager.allDirty();
		}, "Map Chunk Manager (1/2)", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.map_chunk_manager.update(0f, true);
		}, "Map Chunk Manager (2/2)", false, 0.001f, false);
		SmoothLoader.add(new MapLoaderAction(this.loadBoatStates), "Loading Boats. Ahoy Ahoy", false, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.cleanUpWorld(true);
		}, "Cleaning Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			foreach (Subspecies subspecies in World.world.subspecies)
			{
				subspecies.checkPhenotypeColor();
			}
		}, "Checking Phenotypes", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.redrawTiles();
		}, "Drawing Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.preloadRenderedSprites();
		}, "Preload rendered sprites...", false, 0.2f, false);
		SmoothLoader.add(delegate
		{
			World.world.finishMakingWorld();
		}, "Tidying Up The World", true, 0.001f, false);
		SmoothLoader.add(delegate
		{
			World.world.lastGC();
		}, "Rewriting The World", true, 0.001f, false);
		World.world.addLoadAutoTester();
		World.world.addKillAllUnits();
		World.world.addLoadWorldCallbacks();
		SmoothLoader.add(delegate
		{
			if (this.data.camera_pos_x != 0f && this.data.camera_pos_y != 0f)
			{
				World.world.camera.transform.position = new Vector2(this.data.camera_pos_x, this.data.camera_pos_y);
				MoveCamera.instance.forceZoom(this.data.camera_zoom);
				MoveCamera.instance.skipResetZoom();
			}
			this.data = null;
			World.world.finishingUpLoading();
		}, "Finishing up...", false, 0.2f, false);
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x00171FA4 File Offset: 0x001701A4
	private void checkWorldLawsLoad()
	{
		foreach (WorldLawAsset worldLawAsset in AssetManager.world_laws_library.list)
		{
			OnWorldLoadAction on_world_load = worldLawAsset.on_world_load;
			if (on_world_load != null)
			{
				on_world_load();
			}
		}
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x00172004 File Offset: 0x00170204
	private void loadBoatStates()
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.data.transportID.hasValue())
			{
				Actor tTransport = World.world.units.get(tActor.data.transportID);
				if (tTransport != null)
				{
					tActor.embarkInto(tTransport.getSimpleComponent<Boat>());
					tActor.setTask("sit_inside_boat", true, false, false);
					tActor.ai.update();
				}
			}
		}
	}

	// Token: 0x06002FB2 RID: 12210 RVA: 0x001720A4 File Offset: 0x001702A4
	private void loadDiplomacy()
	{
		World.world.diplomacy.loadFromSave(this.data.relations);
	}

	// Token: 0x06002FB3 RID: 12211 RVA: 0x001720C0 File Offset: 0x001702C0
	private void loadLeaders()
	{
		foreach (City city in World.world.cities)
		{
			city.loadLeader();
		}
	}

	// Token: 0x06002FB4 RID: 12212 RVA: 0x00172110 File Offset: 0x00170310
	private void loadCivs02()
	{
		foreach (Kingdom kingdom in World.world.kingdoms)
		{
			kingdom.load2();
		}
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x00172160 File Offset: 0x00170360
	private void setHomeBuildings()
	{
		foreach (Actor tActor in World.world.units)
		{
			long tID = tActor.data.homeBuildingID;
			if (tID.hasValue())
			{
				Building tBuilding = World.world.buildings.get(tID);
				if (tBuilding != null && tBuilding.isUsable())
				{
					if (tActor.asset.is_boat)
					{
						tBuilding.component_docks.addBoatToDock(tActor);
					}
					else if (tBuilding.component_unit_spawner != null)
					{
						tBuilding.component_unit_spawner.setUnitFromHere(tActor);
					}
					else
					{
						tActor.setHomeBuilding(tBuilding);
					}
				}
			}
		}
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x00172214 File Offset: 0x00170414
	private void loadBuildings()
	{
		World.world.buildings.loadFromSave(this.data.buildings);
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x00172230 File Offset: 0x00170430
	private void loadActors()
	{
		World.world.units.loadFromSave(this.data.actors_data);
		this.data.actors_data.Clear();
	}

	// Token: 0x06002FB8 RID: 12216 RVA: 0x0017225C File Offset: 0x0017045C
	private void randomDecisionCooldowns()
	{
		foreach (Actor actor in World.world.units)
		{
			actor.setupRandomDecisionCooldowns();
		}
	}

	// Token: 0x06002FB9 RID: 12217 RVA: 0x001722AC File Offset: 0x001704AC
	private void loadPlotAuthors()
	{
		foreach (Plot plot in World.world.plots)
		{
			plot.loadAuthors();
		}
	}

	// Token: 0x06002FBA RID: 12218 RVA: 0x001722FC File Offset: 0x001704FC
	private void loadActorLovers()
	{
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.data.lover.hasValue())
			{
				Actor tLover = World.world.units.get(tActor.data.lover);
				if (tLover != null)
				{
					tActor.setLover(tLover);
				}
			}
		}
	}

	// Token: 0x06002FBB RID: 12219 RVA: 0x00172380 File Offset: 0x00170580
	private void loadActorsOld(int startIndex = 0, int pAmount = 0)
	{
	}

	// Token: 0x06002FBC RID: 12220 RVA: 0x00172382 File Offset: 0x00170582
	private void loadCities()
	{
		if (this.data.cities == null)
		{
			return;
		}
		World.world.cities.loadFromSave(this.data.cities);
	}

	// Token: 0x06002FBD RID: 12221 RVA: 0x001723AC File Offset: 0x001705AC
	private void loadWars()
	{
		if (this.data.wars == null)
		{
			return;
		}
		World.world.wars.loadFromSave(this.data.wars);
	}

	// Token: 0x06002FBE RID: 12222 RVA: 0x001723D6 File Offset: 0x001705D6
	private void loadPlots()
	{
		if (this.data.plots == null)
		{
			return;
		}
		World.world.plots.loadFromSave(this.data.plots);
	}

	// Token: 0x06002FBF RID: 12223 RVA: 0x00172400 File Offset: 0x00170600
	private void loadAlliances()
	{
		if (this.data.alliances == null)
		{
			return;
		}
		World.world.alliances.loadFromSave(this.data.alliances);
	}

	// Token: 0x06002FC0 RID: 12224 RVA: 0x0017242A File Offset: 0x0017062A
	private void loadClans()
	{
		if (this.data.clans == null)
		{
			return;
		}
		World.world.clans.loadFromSave(this.data.clans);
	}

	// Token: 0x06002FC1 RID: 12225 RVA: 0x00172454 File Offset: 0x00170654
	private void loadKingdoms()
	{
		if (this.data.kingdoms == null)
		{
			return;
		}
		World.world.kingdoms.loadFromSave(this.data.kingdoms);
	}

	// Token: 0x06002FC2 RID: 12226 RVA: 0x0017247E File Offset: 0x0017067E
	private void loadCultures()
	{
		if (this.data.cultures == null)
		{
			return;
		}
		World.world.cultures.loadFromSave(this.data.cultures);
	}

	// Token: 0x06002FC3 RID: 12227 RVA: 0x001724A8 File Offset: 0x001706A8
	private void loadBooks()
	{
		if (this.data.books == null)
		{
			return;
		}
		World.world.books.loadFromSave(this.data.books);
	}

	// Token: 0x06002FC4 RID: 12228 RVA: 0x001724D2 File Offset: 0x001706D2
	private void loadSubspecies()
	{
		if (this.data.subspecies == null)
		{
			return;
		}
		World.world.subspecies.loadFromSave(this.data.subspecies);
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x001724FC File Offset: 0x001706FC
	private void loadLanguages()
	{
		if (this.data.languages == null)
		{
			return;
		}
		World.world.languages.loadFromSave(this.data.languages);
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x00172526 File Offset: 0x00170726
	private void loadReligions()
	{
		if (this.data.religions == null)
		{
			return;
		}
		World.world.religions.loadFromSave(this.data.religions);
	}

	// Token: 0x06002FC7 RID: 12231 RVA: 0x00172550 File Offset: 0x00170750
	private void loadItems()
	{
		if (this.data.items == null)
		{
			return;
		}
		World.world.items.loadFromSave(this.data.items);
	}

	// Token: 0x06002FC8 RID: 12232 RVA: 0x0017257A File Offset: 0x0017077A
	private void loadFamilies()
	{
		if (this.data.families == null)
		{
			return;
		}
		World.world.families.loadFromSave(this.data.families);
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x001725A4 File Offset: 0x001707A4
	private void loadArmies()
	{
		if (this.data.armies == null)
		{
			return;
		}
		World.world.armies.loadFromSave(this.data.armies);
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x001725D0 File Offset: 0x001707D0
	private void loadArmyCaptain()
	{
		foreach (Army army in World.world.armies)
		{
			army.loadDataCaptains();
		}
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x00172620 File Offset: 0x00170820
	private void loadAncientTiles()
	{
		using (StringBuilderPool tString = new StringBuilderPool())
		{
			for (int i = 0; i < this.data.tiles.Count; i++)
			{
				if (i > 0)
				{
					tString.Append(",");
				}
				WorldTileData tTileData = this.data.tiles[i];
				tString.Append(tTileData.type);
			}
			this.data.tileString = tString.ToString();
			this.data.tiles = new List<WorldTileData>();
		}
	}

	// Token: 0x06002FCC RID: 12236 RVA: 0x001726BC File Offset: 0x001708BC
	public static string folderPath(string pFolder)
	{
		if (string.IsNullOrEmpty(pFolder))
		{
			return string.Empty;
		}
		string sepChar = Path.DirectorySeparatorChar.ToString();
		string altChar = Path.AltDirectorySeparatorChar.ToString();
		if (!pFolder.EndsWith(sepChar) && !pFolder.EndsWith(altChar))
		{
			pFolder += sepChar;
		}
		return pFolder;
	}

	// Token: 0x06002FCD RID: 12237 RVA: 0x00172709 File Offset: 0x00170909
	public static bool isLoadingSaveAnimationActive()
	{
		return SaveManager._loading_animation_active;
	}

	// Token: 0x040023A2 RID: 9122
	public static int currentSlot = 0;

	// Token: 0x040023A3 RID: 9123
	public static string currentSavePath = string.Empty;

	// Token: 0x040023A4 RID: 9124
	public static WorkshopMapData currentWorkshopMapData;

	// Token: 0x040023A5 RID: 9125
	private const string saveslot_base = "saves";

	// Token: 0x040023A6 RID: 9126
	private const string workshop_base = "workshop_upload";

	// Token: 0x040023A7 RID: 9127
	private const string autosaves_base = "autosaves";

	// Token: 0x040023A8 RID: 9128
	internal const string name_main_data_old = "map.json";

	// Token: 0x040023A9 RID: 9129
	internal const string name_main_data = "map.wbox";

	// Token: 0x040023AA RID: 9130
	internal const string name_main_data_non_zip = "map.wbax";

	// Token: 0x040023AB RID: 9131
	internal const string name_meta_data = "map.meta";

	// Token: 0x040023AC RID: 9132
	internal const string name_stats_data = "map_stats.s3db";

	// Token: 0x040023AD RID: 9133
	public const string name_png_preview_main = "preview.png";

	// Token: 0x040023AE RID: 9134
	public const string name_png_preview_small = "preview_small.png";

	// Token: 0x040023AF RID: 9135
	private SavedMap data;

	// Token: 0x040023B0 RID: 9136
	private static string persistentDataPath;

	// Token: 0x040023B1 RID: 9137
	private string _tile_id_main;

	// Token: 0x040023B2 RID: 9138
	private string _tile_id_top;

	// Token: 0x040023B3 RID: 9139
	private static bool _loading_animation_active;
}
