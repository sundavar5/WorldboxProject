using System;
using System.Diagnostics;
using System.IO;
using Beebyte.Obfuscator;
using Proyecto26;
using RSG;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200049D RID: 1181
[ObfuscateLiterals]
internal static class VersionCheck
{
	// Token: 0x0600289D RID: 10397 RVA: 0x00145B66 File Offset: 0x00143D66
	internal static void checkVersion()
	{
		VersionCheck.checkPlatform();
		VersionCheck.checkDLLs();
		VersionCheck.getOnlineVersion();
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x00145B78 File Offset: 0x00143D78
	internal static bool isOutdated()
	{
		if (!(VersionCheck.onlineVersion != "") || !(Config.gv != VersionCheck.onlineVersion))
		{
			return false;
		}
		if (VersionCheck.onlineVersion.Split('.', StringSplitOptions.None).Length != 3)
		{
			return false;
		}
		if (Config.gv.Split('.', StringSplitOptions.None).Length != 3)
		{
			return false;
		}
		SemanticVersion semanticVersion = new SemanticVersion(VersionCheck.onlineVersion);
		SemanticVersion version2 = new SemanticVersion(Config.gv);
		int result = semanticVersion.CompareTo(version2);
		return result > 0;
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x00145BF8 File Offset: 0x00143DF8
	internal static void checkDLLs()
	{
		try
		{
			foreach (object obj in Process.GetCurrentProcess().Modules)
			{
				ProcessModule module = (ProcessModule)obj;
				string tModuleName = module.FileName.ToLower();
				if (tModuleName.Contains("steam") && !tModuleName.Contains("punch") && module.ModuleMemorySize > 0)
				{
					RestClient.DefaultRequestHeaders["wb-stms"] = module.ModuleMemorySize.ToString();
					break;
				}
			}
		}
		catch (Exception)
		{
		}
		int tFileId = 0;
		try
		{
			foreach (string tFile in Directory.EnumerateFiles(Application.dataPath, "*team*.*", SearchOption.AllDirectories))
			{
				tFileId++;
				try
				{
					string tFileInfo = Path.GetFileName(tFile);
					tFileInfo = tFileInfo + "," + new FileInfo(tFile).Length.ToString();
					RestClient.DefaultRequestHeaders["wb-stf" + tFileId.ToString()] = tFileInfo;
				}
				catch (Exception)
				{
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00145D84 File Offset: 0x00143F84
	// (set) Token: 0x060028A0 RID: 10400 RVA: 0x00145D68 File Offset: 0x00143F68
	private static string versionCheck
	{
		get
		{
			return VersionCheck._vsCheck;
		}
		set
		{
			VersionCheck._vsCheck = value;
			VersionCallbacks.timer = Randy.randomFloat(300f, 600f);
		}
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x00145D8C File Offset: 0x00143F8C
	private static void getOnlineVersion()
	{
		if (VersionCheck.platform.Length < 2)
		{
			return;
		}
		string vURL = "https://versions.superworldbox.com/versions/" + VersionCheck.platform + ".json?" + Toolbox.cacheBuster();
		try
		{
			RestClient.DefaultRequestHeaders["wb-type"] = "vercheck";
			RestClient.DefaultRequestHeaders["wb-prem"] = (Config.hasPremium ? "y" : "n");
		}
		catch (Exception)
		{
		}
		RestClient.Get(vURL).Then(delegate(ResponseHelper response)
		{
			VersionCheck.versionCheck = JSON.Parse(response.Text);
			if (VersionCheck.versionCheck == "")
			{
				return;
			}
			if (VersionCheck.versionCheck.Split('.', StringSplitOptions.None).Length != 3)
			{
				try
				{
					if (VersionCheck.versionCheck.Contains("no_valid"))
					{
						Config.removePremium();
					}
					if (VersionCheck.versionCheck.Contains("give_prem"))
					{
						Config.givePremium();
					}
					if (VersionCheck.versionCheck.Contains("dprchk"))
					{
						Config.pCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("eprchk"))
					{
						Config.pCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("everything_magic"))
					{
						Config.magicCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("nothing_magic"))
					{
						Config.magicCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("fireworks"))
					{
						Config.fireworksCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("firenope"))
					{
						Config.fireworksCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("showtut"))
					{
						MapBox world = World.world;
						if (world != null)
						{
							Tutorial tutorial = world.tutorial;
							if (tutorial != null)
							{
								tutorial.startTutorial();
							}
						}
					}
					if (VersionCheck.versionCheck.Contains("aye"))
					{
						MapBox.aye();
					}
					if (VersionCheck.versionCheck.Contains("bear"))
					{
						Tutorial.restartTutorial();
					}
					if (VersionCheck.versionCheck.Contains("lang_"))
					{
						string tLang = VersionCheck.extractVal(VersionCheck.versionCheck, "lang_", false);
						LocalizedTextManager.instance.setLanguage(tLang);
					}
					if (VersionCheck.versionCheck.Contains("window_"))
					{
						ScrollWindow.get(VersionCheck.extractVal(VersionCheck.versionCheck, "window_", true)).forceShow();
					}
					if (VersionCheck.versionCheck.Contains("del_"))
					{
						CustomTextureAtlas.delete(VersionCheck.extractVal(VersionCheck.versionCheck, "del_", false));
					}
					if (VersionCheck.versionCheck.Contains("nxtc_"))
					{
						int tSec = int.Parse(VersionCheck.extractVal(VersionCheck.versionCheck, "nxtc_", false));
						if (tSec > 0)
						{
							InitStuff.targetSeconds = (float)tSec;
						}
					}
					else
					{
						InitStuff.targetSeconds = 900f;
					}
				}
				catch (Exception)
				{
				}
				return;
			}
			VersionCheck.onlineVersion = VersionCheck.versionCheck;
			if (!VersionCheck.shownVersion)
			{
				VersionCheck.shownVersion = true;
				Debug.Log("Ver " + VersionCheck.onlineVersion + " " + Application.version);
				if (VersionCheck.isOutdated())
				{
					Debug.Log("Current version is outdated");
				}
			}
		}).Catch(delegate(Exception err)
		{
			Debug.Log("Some error happened during version check");
			Debug.Log(err.Message);
		});
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x00145E5C File Offset: 0x0014405C
	public static bool isWNOutdated()
	{
		if (string.IsNullOrEmpty(VersionCheck.wnVersion.version))
		{
			return true;
		}
		if (string.IsNullOrEmpty(VersionCheck.wnVersion.build))
		{
			return true;
		}
		if (Config.gv != VersionCheck.wnVersion.version)
		{
			if (VersionCheck.wnVersion.version.Split('.', StringSplitOptions.None).Length != 3)
			{
				return false;
			}
			if (Config.gv.Split('.', StringSplitOptions.None).Length != 3)
			{
				return false;
			}
			SemanticVersion semanticVersion = new SemanticVersion(VersionCheck.wnVersion.version);
			SemanticVersion version2 = new SemanticVersion(Config.gv);
			int result = semanticVersion.CompareTo(version2);
			return result > 0;
		}
		else
		{
			if (Config.versionCodeText != VersionCheck.wnVersion.build)
			{
				int version3 = int.Parse(VersionCheck.wnVersion.build);
				int version4 = int.Parse(Config.versionCodeText);
				int result2 = version3.CompareTo(version4);
				return result2 > 0;
			}
			return false;
		}
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x00145F4C File Offset: 0x0014414C
	private static string extractVal(string versionCheck, string pSplitValue, bool pLast = false)
	{
		string[] tSplit = versionCheck.Split(new string[]
		{
			pSplitValue
		}, StringSplitOptions.RemoveEmptyEntries);
		string tReturnValue;
		if (tSplit.Length > 1)
		{
			tReturnValue = tSplit[1];
		}
		else
		{
			tReturnValue = tSplit[0];
		}
		if (!pLast && tReturnValue.Contains("_"))
		{
			tReturnValue = tReturnValue.Split(new string[]
			{
				"_"
			}, StringSplitOptions.RemoveEmptyEntries)[0];
		}
		return tReturnValue;
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x00145FA4 File Offset: 0x001441A4
	private static void checkPlatform()
	{
		RuntimePlatform runtimePlatform = Application.platform;
		if (runtimePlatform <= RuntimePlatform.Android)
		{
			switch (runtimePlatform)
			{
			case RuntimePlatform.OSXEditor:
				VersionCheck.platform = "mac";
				return;
			case RuntimePlatform.OSXPlayer:
				VersionCheck.platform = "mac";
				return;
			case RuntimePlatform.WindowsPlayer:
				VersionCheck.platform = "pc";
				return;
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.WindowsWebPlayer:
			case (RuntimePlatform)6:
				break;
			case RuntimePlatform.WindowsEditor:
				VersionCheck.platform = "pc";
				return;
			case RuntimePlatform.IPhonePlayer:
				VersionCheck.platform = "ios";
				return;
			default:
				if (runtimePlatform == RuntimePlatform.Android)
				{
					VersionCheck.platform = "android";
					return;
				}
				break;
			}
		}
		else
		{
			if (runtimePlatform == RuntimePlatform.LinuxPlayer)
			{
				VersionCheck.platform = "linux";
				return;
			}
			if (runtimePlatform == RuntimePlatform.LinuxEditor)
			{
				VersionCheck.platform = "linux";
				return;
			}
		}
		VersionCheck.platform = "unknown";
	}

	// Token: 0x04001E85 RID: 7813
	private static string platform = "";

	// Token: 0x04001E86 RID: 7814
	public static string onlineVersion = "";

	// Token: 0x04001E87 RID: 7815
	public static WorldNetVersion wnVersion;

	// Token: 0x04001E88 RID: 7816
	public static Promise wnPromise;

	// Token: 0x04001E89 RID: 7817
	private static bool shownVersion = false;

	// Token: 0x04001E8A RID: 7818
	internal static string _vsCheck;
}
