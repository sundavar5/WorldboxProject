using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000021 RID: 33
public static class AssetModLoader
{
	// Token: 0x060001B3 RID: 435 RVA: 0x0000E660 File Offset: 0x0000C860
	public static void load()
	{
		AssetModLoader.path_log = Application.streamingAssetsPath + "/mod_loading_logs.log";
		File.WriteAllText(AssetModLoader.path_log, "");
		string mainPath = Application.streamingAssetsPath + "/mods/";
		List<string> mainDirs = AssetModLoader.getDirectories(mainPath);
		AssetModLoader.log("# HELLO");
		AssetModLoader.log("# GOTTA LOAD MODS FAST");
		AssetModLoader.log("# LOADING MODS NOW");
		AssetModLoader.log("########");
		AssetModLoader.log("");
		AssetModLoader.log("# MAIN PATH: " + mainPath);
		AssetModLoader.log("# TOTAL MODS: " + mainDirs.Count.ToString());
		AssetModLoader.log("");
		for (int i = 0; i < mainDirs.Count; i++)
		{
			string text = mainDirs[i];
			AssetModLoader.log("---------START------------------------------------------------------------------------------------");
			AssetModLoader.log("## LOADING MOD N " + (i + 1).ToString());
			AssetModLoader.log(text);
			AssetModLoader.loadMod(text);
			AssetModLoader.log("---------FINISH-----------------------------------------------------------------------------------");
			AssetModLoader.log("");
			AssetModLoader.log("");
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000E778 File Offset: 0x0000C978
	private static void loadMod(string pPath)
	{
		string[] array = pPath.Split(new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		});
		string tFolder = array[array.Length - 1];
		AssetModLoader.log("# CHECKING MOD... " + tFolder);
		foreach (string pPath2 in AssetModLoader.getDirectories(pPath))
		{
			AssetModLoader.checkModAssets(pPath2);
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000E7FC File Offset: 0x0000C9FC
	private static void checkModAssets(string pPath)
	{
		List<string> tDirs = AssetModLoader.getDirectories(pPath);
		string[] array = pPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.None);
		AssetModLoader.log("");
		string tFolder = array[array.Length - 1];
		AssetModLoader.log("## CHECKING MOD FOLDER... " + tFolder);
		AssetModLoader.log("## SUB FOLDERS FOUND: " + tDirs.Count.ToString());
		AssetModLoader.log("");
		foreach (string pPath2 in tDirs)
		{
			AssetModLoader.checkModFolder(pPath2, tFolder);
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
	private static void checkModFolder(string pPath, string pType)
	{
		List<string> tFiles = AssetModLoader.getFiles(pPath);
		string[] tSplit = pPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.None);
		AssetModLoader.log("");
		AssetModLoader.log("# CHECKING PATH... " + tSplit[tSplit.Length - 1]);
		AssetModLoader.log("FILES: " + tFiles.Count.ToString());
		AssetModLoader.log("");
		foreach (string tPath in tFiles)
		{
			AssetModLoader.log(tPath);
			if (tPath.Contains("json"))
			{
				AssetModLoader.loadFileJson(tPath, pType);
			}
			if (tPath.Contains("png"))
			{
				AssetModLoader.loadTexture(tPath);
			}
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000E97C File Offset: 0x0000CB7C
	private static void loadTexture(string pPath)
	{
		string[] array = pPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.None);
		string tFile = array[array.Length - 1];
		AssetModLoader.log("# LOAD TEXTURE: " + tFile);
		byte[] tPNGBytes = File.ReadAllBytes(pPath);
		string tTextureID = "@wb_" + tFile;
		AssetModLoader.log("ADDING TEXTURE... " + tTextureID);
		SpriteTextureLoader.addSprite(tTextureID, tPNGBytes);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
	private static void loadFileJson(string pPath, string pType)
	{
		string[] array = pPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.None);
		string tFile = array[array.Length - 1];
		AssetModLoader.log("# LOAD ASSET: " + tFile);
		string tStringData = File.ReadAllText(pPath);
		if (!(pType == "actors"))
		{
			if (pType == "buildings")
			{
				AssetModLoader.loadAssetBuilding(tStringData);
				return;
			}
			if (!(pType == "kingdoms"))
			{
				if (!(pType == "powers"))
				{
					pType == "traits";
					return;
				}
				AssetModLoader.loadAssetPowers(tStringData);
			}
		}
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000EA60 File Offset: 0x0000CC60
	private static void loadAssetActor(string pData)
	{
		ActorAsset tAsset = JsonUtility.FromJson<ActorAsset>(pData);
		AssetManager.actor_library.add(tAsset);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000EA80 File Offset: 0x0000CC80
	private static void loadAssetBuilding(string pData)
	{
		BuildingAsset tAsset = JsonUtility.FromJson<BuildingAsset>(pData);
		AssetManager.buildings.add(tAsset);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
	private static void loadAssetKingdom(string pData)
	{
		KingdomAsset tAsset = JsonUtility.FromJson<KingdomAsset>(pData);
		AssetManager.kingdoms.add(tAsset);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
	private static void loadAssetPowers(string pData)
	{
		GodPower tAsset = JsonUtility.FromJson<GodPower>(pData);
		AssetManager.powers.add(tAsset);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
	private static void loadAssetTraits(string pData)
	{
		ActorTrait tAsset = JsonUtility.FromJson<ActorTrait>(pData);
		AssetManager.traits.add(tAsset);
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000EB00 File Offset: 0x0000CD00
	private static void log(string pLog)
	{
		File.AppendAllText(AssetModLoader.path_log, pLog + "\n");
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000EB18 File Offset: 0x0000CD18
	private static List<string> getDirectories(string pPath)
	{
		List<string> tList = new List<string>();
		foreach (string tStr in Directory.GetDirectories(pPath))
		{
			if (!tStr.Contains(".meta"))
			{
				tList.Add(tStr);
			}
		}
		return tList;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000EB5C File Offset: 0x0000CD5C
	private static List<string> getFiles(string pPath)
	{
		List<string> tList = new List<string>();
		foreach (string tStr in Directory.GetFiles(pPath))
		{
			if (!tStr.Contains(".meta"))
			{
				tList.Add(tStr);
			}
		}
		return tList;
	}

	// Token: 0x04000150 RID: 336
	private static string path_log;
}
