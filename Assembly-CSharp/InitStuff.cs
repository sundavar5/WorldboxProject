using System;
using System.Threading.Tasks;
using db;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Proyecto26;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class InitStuff : MonoBehaviour
{
	// Token: 0x0600267C RID: 9852 RVA: 0x00139D56 File Offset: 0x00137F56
	private void Start()
	{
		ThreadHelper.Initialize();
		InitStuff.initDB();
		InitStuff.initSteam();
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x00139D68 File Offset: 0x00137F68
	public static void initRest()
	{
		if (InitStuff.restinitiated)
		{
			return;
		}
		InitStuff.restinitiated = true;
		try
		{
			RestClient.DefaultRequestHeaders["salt"] = (global::RequestHelper.salt ?? "na");
			RestClient.DefaultRequestHeaders["wb-version"] = (Application.version ?? "na");
			RestClient.DefaultRequestHeaders["wb-identifier"] = (Application.identifier ?? "na");
			RestClient.DefaultRequestHeaders["wb-platform"] = (Application.platform.ToString() ?? "na");
			RestClient.DefaultRequestHeaders["wb-language"] = (LocalizedTextManager.instance.language ?? "na");
			RestClient.DefaultRequestHeaders["wb-prem"] = (Config.hasPremium ? "y" : "n");
			RestClient.DefaultRequestHeaders["wb-build"] = (Config.versionCodeText ?? "na");
			RestClient.DefaultRequestHeaders["wb-gen"] = ((Config.gen != null) ? (Config.gen.Value ? "y" : "n") : "na");
			RestClient.DefaultRequestHeaders["wb-git"] = (Config.gitCodeText ?? "na");
		}
		catch (Exception ex)
		{
			Debug.Log("RestClient initialization Error");
			Debug.Log(ex.Message);
		}
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x00139EF4 File Offset: 0x001380F4
	public static void initOnlineServices()
	{
		if (InitStuff.initiated)
		{
			return;
		}
		InitStuff.initiated = true;
		if (Config.isEditor)
		{
			return;
		}
		try
		{
			if (Config.firebaseEnabled)
			{
				InitStuff.initFirebase();
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Firebase Init Error");
			Debug.Log(ex.Message);
		}
		try
		{
			VersionCheck.checkVersion();
		}
		catch (Exception ex2)
		{
			Debug.Log("Version Error");
			Debug.Log(ex2.Message);
		}
		InitStuff.initRichPresence();
	}

	// Token: 0x0600267F RID: 9855 RVA: 0x00139F7C File Offset: 0x0013817C
	private void Update()
	{
		if (this.checkInitTimeOut > 0f)
		{
			this.checkInitTimeOut -= Time.fixedDeltaTime;
			if (this.checkInitTimeOut < 0f)
			{
				if (Config.firebaseEnabled)
				{
					if (Config.firebaseChecked)
					{
						goto IL_5D;
					}
					try
					{
						InitStuff.checkFirebase();
						goto IL_5D;
					}
					catch (Exception ex)
					{
						Debug.Log("Firebase Check Error");
						Debug.Log(ex.Message);
						goto IL_5D;
					}
				}
				Config.firebaseChecked = true;
			}
		}
		IL_5D:
		if (!Config.firebaseChecked)
		{
			return;
		}
		if (!InitStuff.restinitiated)
		{
			InitStuff.initRest();
		}
		if (this.servicesInitTimeOut > 0f)
		{
			this.servicesInitTimeOut -= Time.fixedDeltaTime;
			if (this.servicesInitTimeOut < 0f)
			{
				InitStuff.initOnlineServices();
			}
		}
		if (Config.firebaseInitiating)
		{
			return;
		}
		if (this.adsInitTimeOut > 0f)
		{
			this.adsInitTimeOut -= Time.fixedDeltaTime;
			if (this.adsInitTimeOut < 0f)
			{
				InitAds.initAdProviders();
			}
		}
		if (this.elapsedSeconds <= InitStuff.targetSeconds)
		{
			this.elapsedSeconds += Time.deltaTime;
			return;
		}
		this.elapsedSeconds = 0f;
		try
		{
			if (Config.hasPremium || InitStuff.targetSeconds != 900f)
			{
				VersionCheck.checkVersion();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x0013A0C8 File Offset: 0x001382C8
	private static void checkFirebase()
	{
		Debug.Log("Firebase Starting Check");
		Config.firebaseChecked = false;
		FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(delegate(Task<DependencyStatus> pTask)
		{
			Debug.Log("Firebase check continuing on thread");
			DependencyStatus dependencyStatus = pTask.Result;
			ThreadHelper.ExecuteInUpdate(delegate
			{
				Debug.Log("Firebase check status");
				if (dependencyStatus != DependencyStatus.Available)
				{
					Debug.Log("Firebase is not available");
					Debug.Log(dependencyStatus);
				}
				else
				{
					Debug.Log("Firebase is available");
					Debug.Log(dependencyStatus);
				}
				Config.firebaseChecked = true;
			});
		});
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x0013A104 File Offset: 0x00138304
	private static void initFirebase()
	{
		Debug.Log("Firebase init");
		Config.firebaseInitiating = true;
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(delegate(Task<DependencyStatus> pTask)
		{
			Debug.Log("Firebase init continuing on thread");
			DependencyStatus dependencyStatus = pTask.Result;
			Debug.Log(dependencyStatus);
			ThreadHelper.ExecuteInUpdate(delegate
			{
				Debug.Log("Firebase task status");
				if (dependencyStatus == DependencyStatus.Available)
				{
					try
					{
						Config.firebaseInitiating = false;
						Config.firebase_available = true;
						FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
						Debug.Log("Firebase loaded");
						FirebaseAnalytics.LogEvent("data", "installerName", Config.iname ?? "");
						return;
					}
					catch (Exception ex)
					{
						Debug.Log("Firebase Error");
						Debug.Log(ex.Message);
						Config.authEnabled = false;
						Config.firebase_available = false;
						Config.firebaseInitiating = false;
						return;
					}
				}
				Debug.Log(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				Config.authEnabled = false;
				Config.firebase_available = false;
			});
		});
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x0013A140 File Offset: 0x00138340
	private static void initDB()
	{
		GameObject gameObject = new GameObject("DB");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<DBManager>();
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x0013A17C File Offset: 0x0013837C
	private static void initRichPresence()
	{
		if (Config.disable_steam && Config.disable_discord)
		{
			return;
		}
		GameObject gameObject = new GameObject("PowerTracker");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<PowerTracker>();
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x0013A1D0 File Offset: 0x001383D0
	internal static void initSteam()
	{
		if (Config.disable_steam)
		{
			return;
		}
		GameObject gameObject = new GameObject("Steam");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<SteamSDK>();
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
		SteamAchievements.InitAchievements();
	}

	// Token: 0x04001D02 RID: 7426
	private static bool initiated = false;

	// Token: 0x04001D03 RID: 7427
	private static bool restinitiated = false;

	// Token: 0x04001D04 RID: 7428
	private float elapsedSeconds;

	// Token: 0x04001D05 RID: 7429
	public static float targetSeconds = 900f;

	// Token: 0x04001D06 RID: 7430
	private float checkInitTimeOut = 1f;

	// Token: 0x04001D07 RID: 7431
	private float servicesInitTimeOut = 3f;

	// Token: 0x04001D08 RID: 7432
	private float adsInitTimeOut = 8f;
}
