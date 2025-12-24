using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class InitLibraries : MonoBehaviour
{
	// Token: 0x060002FF RID: 767 RVA: 0x0001CED4 File Offset: 0x0001B0D4
	private void Awake()
	{
		this.initLibs();
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0001CEDC File Offset: 0x0001B0DC
	public static void initMainLibs()
	{
		if (InitLibraries.initiated_main)
		{
			return;
		}
		InitLibraries.initiated_main = true;
		try
		{
			Config.gv = Application.version;
			Config.iname = Application.installerName;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		try
		{
			if (Application.genuineCheckAvailable)
			{
				Config.gen = new bool?(Application.genuine);
			}
		}
		catch (Exception)
		{
			Config.gen = null;
		}
		LogHandler.init();
		AssetManager.initMain();
		GameProgress.init();
		PlayerConfig.init();
		LocalizedTextManager.init(null);
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0001CF74 File Offset: 0x0001B174
	private void initLibs()
	{
		if (InitLibraries.initiated)
		{
			return;
		}
		InitLibraries.initiated = true;
		LogText.log("InitLibraries " + Config.gv, "initLibs", "st");
		DebugConfig.init();
		InitLibraries.initMainLibs();
		AssetManager.init();
		DebugConfig.checkSonicTimeScales();
		NameGenerator.init();
		LogText.log("InitLibraries", "initLibs", "en");
	}

	// Token: 0x040002AA RID: 682
	public static bool initiated;

	// Token: 0x040002AB RID: 683
	public static bool initiated_main;
}
