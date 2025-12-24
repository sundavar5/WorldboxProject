using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class ModLoader : MonoBehaviour
{
	// Token: 0x06000371 RID: 881 RVA: 0x0001F7A7 File Offset: 0x0001D9A7
	public void Update()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (!Config.experimental_mode)
		{
			return;
		}
		if (!this.initialized)
		{
			this.initialized = true;
			this.Initialize();
			base.enabled = false;
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
	internal static List<string> getModsLoaded()
	{
		return ModLoader.modsLoaded;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0001F7DC File Offset: 0x0001D9DC
	public void Initialize()
	{
		string tPath = Path.Combine(Application.streamingAssetsPath, "mods");
		if (!Directory.Exists(tPath))
		{
			Debug.LogError("Can not find mod dlls - there is no 'Mods' folder");
			return;
		}
		using (ListPool<FileInfo> tRandomFiles = new ListPool<FileInfo>(new DirectoryInfo(tPath).GetFiles()))
		{
			tRandomFiles.RemoveAll((FileInfo file) => !file.Name.ToLower().EndsWith(".dll"));
			HashSet<string> tCheckFilenames = new HashSet<string>();
			foreach (FileInfo ptr in tRandomFiles)
			{
				string tFilename = ptr.Name.ToLower();
				tCheckFilenames.Add(tFilename);
			}
			tRandomFiles.RemoveAll(delegate(FileInfo tFileInfo)
			{
				string tFilename2 = tFileInfo.Name.ToLower();
				if (!tFilename2.EndsWith("_memload.dll"))
				{
					return false;
				}
				string tBaseFilename = tFilename2.Replace("_memload.dll", ".dll");
				return tCheckFilenames.Contains(tBaseFilename);
			});
			tRandomFiles.Shuffle<FileInfo>();
			foreach (FileInfo ptr2 in tRandomFiles)
			{
				FileInfo tFileInfo2 = ptr2;
				if (tFileInfo2.Name.ToLower().EndsWith(".dll"))
				{
					string tModPath = tFileInfo2.FullName;
					string directoryName = Path.GetDirectoryName(tModPath);
					string tModName = Path.GetFileNameWithoutExtension(tFileInfo2.Name).Replace("_memload", "");
					string tDLLname = tModName;
					string tMemLoadFilename = tModName + "_memload.dll";
					string tMemLoadPath = Path.Combine(directoryName, tMemLoadFilename);
					bool tMemload;
					if (File.Exists(tMemLoadPath))
					{
						tMemload = true;
						tDLLname = Path.GetFileNameWithoutExtension(tMemLoadFilename);
						tModPath = tMemLoadPath;
						Debug.Log(string.Concat(new string[]
						{
							"[",
							tModName,
							"] Loading ",
							tFileInfo2.Name,
							" into memory"
						}));
					}
					else
					{
						tMemload = false;
						Debug.Log("[" + tModName + "] Loading " + tFileInfo2.Name);
					}
					try
					{
						Assembly tAssembly;
						if (!tMemload)
						{
							tAssembly = Assembly.LoadFile(tModPath);
						}
						else
						{
							byte[] tAssemblyBytes = File.ReadAllBytes(tModPath);
							string tPdbPath = Path.Combine(Path.GetDirectoryName(tModPath), tDLLname + ".pdb");
							if (File.Exists(tPdbPath))
							{
								Debug.Log("[" + tModName + "] .pdb symbol file found");
								try
								{
									byte[] tPdbBytes = File.ReadAllBytes(tPdbPath);
									tAssembly = Assembly.Load(tAssemblyBytes, tPdbBytes);
									goto IL_255;
								}
								catch (Exception ex)
								{
									Debug.LogError("[" + tModName + "] Failed to load with .pdb symbol file");
									Debug.LogError(ex.Message);
									tAssembly = Assembly.Load(tAssemblyBytes);
									goto IL_255;
								}
							}
							tAssembly = Assembly.Load(tAssemblyBytes);
						}
						IL_255:
						string str = "[";
						string str2 = tModName;
						string str3 = "] Assembly: ";
						Assembly assembly = tAssembly;
						Debug.Log(str + str2 + str3 + ((assembly != null) ? assembly.ToString() : null));
						Debug.Log("[" + tModName + "] classes inside the mod:");
						foreach (Type tAssemblyType in tAssembly.GetTypes())
						{
							string str4 = "[";
							string str5 = tModName;
							string str6 = "] ";
							Type type = tAssemblyType;
							Debug.Log(str4 + str5 + str6 + ((type != null) ? type.ToString() : null));
						}
						Debug.Log(string.Concat(new string[]
						{
							"[",
							tModName,
							"] Attempting to load ",
							tModName,
							".WorldBoxMod"
						}));
						Type tModType = tAssembly.GetType(tModName + ".WorldBoxMod");
						if (tModType != null)
						{
							new GameObject(tModName)
							{
								transform = 
								{
									parent = base.transform
								}
							}.AddComponent(tModType);
							ModLoader.modsLoaded.Add(tModName);
							Config.MODDED = true;
							Debug.Log("[" + tModName + "] Was added");
						}
						else
						{
							Debug.LogError(string.Concat(new string[]
							{
								"[",
								tModName,
								"] Missing className: ",
								tModName,
								".WorldBoxMod"
							}));
						}
					}
					catch (Exception ex2)
					{
						Debug.Log("[" + tModName + "] Failed to load mod from path : ");
						Debug.Log("[" + tModName + "] " + tModPath);
						Debug.LogError(ex2.Message);
					}
				}
			}
		}
	}

	// Token: 0x0400030F RID: 783
	private bool initialized;

	// Token: 0x04000310 RID: 784
	private static List<string> modsLoaded = new List<string>();

	// Token: 0x04000311 RID: 785
	private const string MODS_FOLDER = "mods";
}
