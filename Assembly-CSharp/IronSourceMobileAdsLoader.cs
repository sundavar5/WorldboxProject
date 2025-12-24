using System;
using Beebyte.Obfuscator;
using com.unity3d.mediation;
using Unity.Services.LevelPlay;
using UnityEngine;

// Token: 0x020004AB RID: 1195
[ObfuscateLiterals]
public class IronSourceMobileAdsLoader : MonoBehaviour
{
	// Token: 0x0600294B RID: 10571 RVA: 0x00147D74 File Offset: 0x00145F74
	public static void initAds()
	{
		if (IronSourceMobileAdsLoader.instance != null)
		{
			return;
		}
		GameObject gameObject = new GameObject("IronSourceMobileAdsLoader");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
		IronSourceMobileAdsLoader.instance = gameObject.AddComponent<IronSourceMobileAdsLoader>();
	}

	// Token: 0x0600294C RID: 10572 RVA: 0x00147DCC File Offset: 0x00145FCC
	internal void Start()
	{
		if (DebugConfig.isOn(DebugOption.TestAds))
		{
			Config.testAds = true;
		}
		if (!Config.isMobile || Config.hasPremium)
		{
			return;
		}
		try
		{
			IronSourceMobileAdsLoader.log("Initializing");
			com.unity3d.mediation.LevelPlayAdFormat[] tLegacyAdFormats = new com.unity3d.mediation.LevelPlayAdFormat[]
			{
				com.unity3d.mediation.LevelPlayAdFormat.REWARDED
			};
			Unity.Services.LevelPlay.LevelPlay.OnInitSuccess += new Action<com.unity3d.mediation.LevelPlayConfiguration>(this.SdkInitializationCompletedEvent);
			Unity.Services.LevelPlay.LevelPlay.OnInitFailed += new Action<com.unity3d.mediation.LevelPlayInitError>(this.SdkInitializationFailedEvent);
			Unity.Services.LevelPlay.LevelPlay.Init("unexpected_platform", null, tLegacyAdFormats);
			IronSourceMobileAdsLoader.log("Version " + Unity.Services.LevelPlay.LevelPlay.UnityVersion);
		}
		catch (Exception message)
		{
			IronSourceMobileAdsLoader.log("Could not initialize ads");
			Debug.Log(message);
		}
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x00147E74 File Offset: 0x00146074
	private void OnApplicationPause(bool isPaused)
	{
		IronSourceMobileAdsLoader.log("OnApplicationPause = " + isPaused.ToString());
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x00147E8C File Offset: 0x0014608C
	private void SdkInitializationCompletedEvent(Unity.Services.LevelPlay.LevelPlayConfiguration pConfig)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceMobileAdsLoader.log("Initialized");
			IronSourceMobileAdsLoader.initialized = true;
			Config.adsInitialized = true;
		});
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x00147EB2 File Offset: 0x001460B2
	private void SdkInitializationFailedEvent(Unity.Services.LevelPlay.LevelPlayInitError pConfig)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceMobileAdsLoader.log("Failed to initialize ads");
			IronSourceMobileAdsLoader.initialized = false;
		});
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x00147ED8 File Offset: 0x001460D8
	private static void log(string pLog)
	{
		Debug.Log(IronSourceMobileAdsLoader.GetColor() + " <color=#abe0c3>" + pLog + "</color>");
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x00147EF4 File Offset: 0x001460F4
	public static string GetColor()
	{
		return "[<color=#abe0c3>IS</color>]";
	}

	// Token: 0x04001EDB RID: 7899
	private const string APP_KEY = "unexpected_platform";

	// Token: 0x04001EDC RID: 7900
	private static IronSourceMobileAdsLoader instance;

	// Token: 0x04001EDD RID: 7901
	internal static bool initialized;
}
