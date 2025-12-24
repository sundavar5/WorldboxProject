using System;
using Beebyte.Obfuscator;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
[ObfuscateLiterals]
public class GoogleMobileAdsLoader : MonoBehaviour
{
	// Token: 0x060028EC RID: 10476 RVA: 0x001471DC File Offset: 0x001453DC
	public static void initAds()
	{
		if (GoogleMobileAdsLoader.instance != null)
		{
			return;
		}
		if (!GoogleMobileAdsLoader.shouldLoad())
		{
			return;
		}
		GameObject gameObject = new GameObject("GoogleMobileAdsLoader");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
		GoogleMobileAdsLoader.instance = gameObject.AddComponent<GoogleMobileAdsLoader>();
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x0014723C File Offset: 0x0014543C
	public void Start()
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
			string tRegion = PreciseLocale.GetRegion();
			if (tRegion.ToLower().Contains("us") || tRegion.ToLower().Contains("gb"))
			{
				GoogleInterstitialAd.default_current = 1;
				GoogleRewardAd.default_current = 1;
			}
			string tCurrency = PreciseLocale.GetCurrencyCode();
			if (tCurrency == "USD" || tCurrency == "GBP")
			{
				GoogleInterstitialAd.default_current = 1;
				GoogleRewardAd.default_current = 1;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			GoogleMobileAdsLoader.log("Initializing");
			MobileAds.DisableMediationInitialization();
			MobileAds.Initialize(delegate(InitializationStatus _)
			{
				ThreadHelper.ExecuteInUpdate(delegate
				{
					GoogleMobileAdsLoader.log("Initialized");
					GoogleMobileAdsLoader.initialized = true;
					Config.adsInitialized = true;
				});
			});
		}
		catch (Exception message)
		{
			GoogleMobileAdsLoader.log("Could not initialize ads");
			Debug.Log(message);
		}
	}

	// Token: 0x060028EE RID: 10478 RVA: 0x00147334 File Offset: 0x00145534
	private static void log(string pLog)
	{
		Debug.Log(GoogleMobileAdsLoader.GetColor() + " <color=#fbbc04>" + pLog + "</color>");
	}

	// Token: 0x060028EF RID: 10479 RVA: 0x00147350 File Offset: 0x00145550
	public static string GetColor()
	{
		return "[<color=#ea4335>A</color><color=#fbbc04>D</color><color=#4285f4>M</color>]";
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x00147357 File Offset: 0x00145557
	public static bool shouldLoad()
	{
		return false;
	}

	// Token: 0x04001EBE RID: 7870
	private static GoogleMobileAdsLoader instance;

	// Token: 0x04001EBF RID: 7871
	internal static bool initialized;
}
