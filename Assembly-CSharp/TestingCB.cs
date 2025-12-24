using System;
using Beebyte.Obfuscator;

// Token: 0x020005B5 RID: 1461
[ObfuscateLiterals]
internal static class TestingCB
{
	// Token: 0x0600303F RID: 12351 RVA: 0x001758D0 File Offset: 0x00173AD0
	internal static void init()
	{
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.premiumChecker));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.premiumPossible));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.purpleTextures));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.fireworks));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.tutorial));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.aye));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.language));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.openWindow));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.deleteFile));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.nextCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.valCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.enableSigCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.adChecks));
	}

	// Token: 0x06003040 RID: 12352 RVA: 0x00175A7D File Offset: 0x00173C7D
	private static void premiumChecker(string pVersionCheck)
	{
		if (pVersionCheck.Contains("no_valid"))
		{
			Config.removePremium();
		}
		if (pVersionCheck.Contains("give_prem"))
		{
			Config.givePremium();
		}
	}

	// Token: 0x06003041 RID: 12353 RVA: 0x00175AA3 File Offset: 0x00173CA3
	private static void premiumPossible(string pVersionCheck)
	{
		if (pVersionCheck.Contains("dprchk"))
		{
			Config.pCheck(false);
		}
		if (pVersionCheck.Contains("eprchk"))
		{
			Config.pCheck(true);
		}
	}

	// Token: 0x06003042 RID: 12354 RVA: 0x00175ACB File Offset: 0x00173CCB
	private static void purpleTextures(string pVersionCheck)
	{
		if (pVersionCheck.Contains("everything_magic"))
		{
			Config.magicCheck(true);
		}
		if (pVersionCheck.Contains("nothing_magic"))
		{
			Config.magicCheck(false);
		}
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x00175AF3 File Offset: 0x00173CF3
	private static void fireworks(string pVersionCheck)
	{
		if (pVersionCheck.Contains("fireworks"))
		{
			Config.fireworksCheck(true);
		}
		if (pVersionCheck.Contains("firenope"))
		{
			Config.fireworksCheck(false);
		}
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x00175B1B File Offset: 0x00173D1B
	private static void tutorial(string pVersionCheck)
	{
		if (pVersionCheck.Contains("showtut"))
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
		if (pVersionCheck.Contains("bear"))
		{
			Tutorial.restartTutorial();
		}
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x00175B57 File Offset: 0x00173D57
	private static void aye(string pVersionCheck)
	{
		if (pVersionCheck.Contains("aye"))
		{
			MapBox.aye();
		}
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x00175B6C File Offset: 0x00173D6C
	private static void language(string pVersionCheck)
	{
		if (pVersionCheck.Contains("lang_"))
		{
			string tLang = TestingCB.extractVal(pVersionCheck, "lang_", false);
			LocalizedTextManager.instance.setLanguage(tLang);
		}
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x00175B9E File Offset: 0x00173D9E
	private static void openWindow(string pVersionCheck)
	{
		if (pVersionCheck.Contains("window_"))
		{
			ScrollWindow.get(TestingCB.extractVal(pVersionCheck, "window_", true)).forceShow();
		}
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x00175BC3 File Offset: 0x00173DC3
	private static void deleteFile(string pVersionCheck)
	{
		if (pVersionCheck.Contains("del_"))
		{
			CustomTextureAtlas.delete(TestingCB.extractVal(pVersionCheck, "del_", false));
		}
	}

	// Token: 0x06003049 RID: 12361 RVA: 0x00175BE4 File Offset: 0x00173DE4
	private static void nextCheck(string pVersionCheck)
	{
		if (pVersionCheck.Contains("nxtc_"))
		{
			int tSec = int.Parse(TestingCB.extractVal(pVersionCheck, "nxtc_", false));
			if (tSec > 0)
			{
				InitStuff.targetSeconds = (float)tSec;
				return;
			}
		}
		else
		{
			InitStuff.targetSeconds = 900f;
		}
	}

	// Token: 0x0600304A RID: 12362 RVA: 0x00175C26 File Offset: 0x00173E26
	private static void valCheck(string pVersionCheck)
	{
		if (pVersionCheck.Contains("evalchk"))
		{
			Config.valCheck(true);
		}
		if (pVersionCheck.Contains("dvalchk"))
		{
			Config.valCheck(false);
		}
	}

	// Token: 0x0600304B RID: 12363 RVA: 0x00175C4E File Offset: 0x00173E4E
	private static void enableSigCheck(string pVersionCheck)
	{
	}

	// Token: 0x0600304C RID: 12364 RVA: 0x00175C50 File Offset: 0x00173E50
	private static void adChecks(string pVersionCheck)
	{
	}

	// Token: 0x0600304D RID: 12365 RVA: 0x00175C54 File Offset: 0x00173E54
	public static string extractVal(string pVersionCheck, string pSplitValue, bool pLast = false)
	{
		string[] tSplit = pVersionCheck.Split(new string[]
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
}
