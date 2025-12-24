using System;
using System.Collections.Generic;
using Firebase.Analytics;

// Token: 0x02000445 RID: 1093
public class Analytics
{
	// Token: 0x060025D9 RID: 9689 RVA: 0x001376D4 File Offset: 0x001358D4
	public static void trackWindow(string pName)
	{
		if (Config.isEditor)
		{
			return;
		}
		if (Config.isComputer)
		{
			return;
		}
		string tName = Analytics.slugify(pName);
		if (Config.firebase_available)
		{
			FirebaseAnalytics.LogEvent("open_window", "window_id", tName);
			Analytics.logScreen("ScrollWindow", tName);
		}
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x0013771A File Offset: 0x0013591A
	public static void hideWindow()
	{
		Analytics.logScreen("GamePlay", "gameplay");
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x0013772B File Offset: 0x0013592B
	public static void worldLoaded()
	{
		Analytics.logScreen("GamePlay", "gameplay");
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x0013773C File Offset: 0x0013593C
	public static void worldLoading()
	{
		Analytics.logScreen("LoadingScreen", "loading");
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x00137750 File Offset: 0x00135950
	private static void logScreen(string pClass, string pName)
	{
		if (Config.firebase_available)
		{
			Parameter[] tParams = new Parameter[]
			{
				new Parameter(FirebaseAnalytics.ParameterScreenClass, pClass),
				new Parameter(FirebaseAnalytics.ParameterScreenName, pName)
			};
			FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventScreenView, tParams);
		}
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x00137794 File Offset: 0x00135994
	public static void LogEvent(string pName, bool pFirebase = true, bool pFacebook = true)
	{
		if (Config.isEditor)
		{
			return;
		}
		if (Config.isComputer)
		{
			return;
		}
		MapBox world = World.world;
		bool flag;
		if (world == null)
		{
			flag = false;
		}
		else
		{
			AutoTesterBot auto_tester = world.auto_tester;
			bool? flag2 = (auto_tester != null) ? new bool?(auto_tester.active) : null;
			bool flag3 = true;
			flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
		}
		if (flag)
		{
			return;
		}
		string tName = Analytics.slugify(pName);
		if (Config.firebase_available && pFirebase)
		{
			FirebaseAnalytics.LogEvent(tName);
		}
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x0013780C File Offset: 0x00135A0C
	public static void LogEvent(string pName, string parameterName, string parameterValue)
	{
		if (Config.isEditor)
		{
			return;
		}
		if (Config.isComputer)
		{
			return;
		}
		MapBox world = World.world;
		bool flag;
		if (world == null)
		{
			flag = false;
		}
		else
		{
			AutoTesterBot auto_tester = world.auto_tester;
			bool? flag2 = (auto_tester != null) ? new bool?(auto_tester.active) : null;
			bool flag3 = true;
			flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
		}
		if (flag)
		{
			return;
		}
		string tName = Analytics.slugify(pName);
		if (Config.firebase_available)
		{
			FirebaseAnalytics.LogEvent(tName, parameterName, parameterValue);
		}
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x00137884 File Offset: 0x00135A84
	public static string slugify(string pPhrase)
	{
		string tSlug;
		if (!Analytics._event_slugs.TryGetValue(pPhrase, out tSlug))
		{
			tSlug = pPhrase.Trim().Replace(" ", "_").ToLower();
			Analytics._event_slugs[pPhrase] = tSlug;
		}
		return tSlug;
	}

	// Token: 0x04001CC7 RID: 7367
	private static Dictionary<string, string> _event_slugs = new Dictionary<string, string>();
}
