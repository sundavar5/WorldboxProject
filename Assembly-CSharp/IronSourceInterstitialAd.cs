using System;
using Beebyte.Obfuscator;
using com.unity3d.mediation;
using Unity.Services.LevelPlay;

// Token: 0x020004AA RID: 1194
[ObfuscateLiterals]
public class IronSourceInterstitialAd : IWorldBoxAd
{
	// Token: 0x17000233 RID: 563
	// (get) Token: 0x0600292A RID: 10538 RVA: 0x001479FE File Offset: 0x00145BFE
	// (set) Token: 0x0600292B RID: 10539 RVA: 0x00147A06 File Offset: 0x00145C06
	public Action<string> logger { get; set; }

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x0600292C RID: 10540 RVA: 0x00147A0F File Offset: 0x00145C0F
	// (set) Token: 0x0600292D RID: 10541 RVA: 0x00147A17 File Offset: 0x00145C17
	public Action adResetCallback { get; set; }

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x0600292E RID: 10542 RVA: 0x00147A20 File Offset: 0x00145C20
	// (set) Token: 0x0600292F RID: 10543 RVA: 0x00147A28 File Offset: 0x00145C28
	public Action adFailedCallback { get; set; }

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06002930 RID: 10544 RVA: 0x00147A31 File Offset: 0x00145C31
	// (set) Token: 0x06002931 RID: 10545 RVA: 0x00147A39 File Offset: 0x00145C39
	public Action adFinishedCallback { get; set; }

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06002932 RID: 10546 RVA: 0x00147A42 File Offset: 0x00145C42
	// (set) Token: 0x06002933 RID: 10547 RVA: 0x00147A4A File Offset: 0x00145C4A
	public Action adStartedCallback { get; set; }

	// Token: 0x06002934 RID: 10548 RVA: 0x00147A53 File Offset: 0x00145C53
	public void Reset()
	{
		IronSourceInterstitialAd.failed = 0;
		IronSourceInterstitialAd.loaded = 0;
		IronSourceInterstitialAd.lastError = "";
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x00147A6C File Offset: 0x00145C6C
	public void RequestAd()
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (Config.hasPremium)
		{
			return;
		}
		if (!IronSourceMobileAdsLoader.initialized)
		{
			return;
		}
		if (!IronSourceInterstitialAd._initialized)
		{
			IronSourceInterstitialAd._initialized = true;
			IronSourceInterstitialAd._interstitial_ad = new Unity.Services.LevelPlay.LevelPlayInterstitialAd("none");
			IronSourceInterstitialAd._interstitial_ad.OnAdLoaded += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleOnAdReady);
			IronSourceInterstitialAd._interstitial_ad.OnAdLoadFailed += new Action<com.unity3d.mediation.LevelPlayAdError>(this.HandleOnAdFailedToLoad);
			IronSourceInterstitialAd._interstitial_ad.OnAdDisplayed += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleOnAdOpened);
			IronSourceInterstitialAd._interstitial_ad.OnAdDisplayFailed += new Action<com.unity3d.mediation.LevelPlayAdDisplayInfoError>(this.HandleOnAdFailedToShow);
			IronSourceInterstitialAd._interstitial_ad.OnAdClosed += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleOnAdClosed);
		}
		this.KillAd();
		this.log("Requesting Ad");
		IronSourceInterstitialAd._interstitial_ad.LoadAd();
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x00147B39 File Offset: 0x00145D39
	public void HandleOnAdLoaded()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded++;
			IronSourceInterstitialAd.failed = 0;
			this.log("Ad Loaded");
		});
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x00147B4C File Offset: 0x00145D4C
	public void HandleOnAdFailedToLoad(Unity.Services.LevelPlay.LevelPlayAdError pLoadAdError)
	{
		string tLoadError = "Failed to load ad";
		if (pLoadAdError != null)
		{
			tLoadError = pLoadAdError.ToString();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded = 0;
			IronSourceInterstitialAd.failed++;
			if (IronSourceInterstitialAd.lastError != tLoadError)
			{
				this.log("<color=red>Ad Failed to Load: " + tLoadError + "</color>");
				IronSourceInterstitialAd.lastError = tLoadError;
			}
			else
			{
				this.log("<color=red>Ad Failed to Load</color>");
			}
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
		});
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x00147B94 File Offset: 0x00145D94
	public void HandleOnAdFailedToShow(Unity.Services.LevelPlay.LevelPlayAdDisplayInfoError pLoadAdInfoError)
	{
		Unity.Services.LevelPlay.LevelPlayAdInfo displayLevelPlayAdInfo = pLoadAdInfoError.DisplayLevelPlayAdInfo;
		Unity.Services.LevelPlay.LevelPlayAdError tLoadAdError = pLoadAdInfoError.LevelPlayError;
		string tLoadError = "Failed to show ad";
		if (tLoadAdError != null)
		{
			tLoadError = tLoadAdError.ToString();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded = 0;
			IronSourceInterstitialAd.failed++;
			if (IronSourceInterstitialAd.lastError != tLoadError)
			{
				this.log("<color=red>Ad Failed to Show: " + tLoadError + "</color>");
				IronSourceInterstitialAd.lastError = tLoadError;
			}
			else
			{
				this.log("<color=red>Ad Failed to Show</color>");
			}
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
		});
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x00147BE7 File Offset: 0x00145DE7
	public void HandleOnAdReady(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded++;
			IronSourceInterstitialAd.failed = 0;
			this.log("Ad Ready");
		});
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x00147BFA File Offset: 0x00145DFA
	public void HandleOnAdOpened(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded++;
			IronSourceInterstitialAd.failed = 0;
			this.log("Ad Opened");
			if (this.adStartedCallback != null)
			{
				this.adStartedCallback();
			}
		});
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x00147C0D File Offset: 0x00145E0D
	public void HandleOnAdClosed(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceInterstitialAd.loaded++;
			IronSourceInterstitialAd.failed = 0;
			this.log("Ad Closed");
			this.KillAd();
			if (this.adFinishedCallback != null)
			{
				this.adFinishedCallback();
			}
		});
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x00147C20 File Offset: 0x00145E20
	public void KillAd()
	{
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x00147C22 File Offset: 0x00145E22
	public bool IsReady()
	{
		if (!IronSourceMobileAdsLoader.initialized)
		{
			return false;
		}
		Unity.Services.LevelPlay.LevelPlayInterstitialAd interstitial_ad = IronSourceInterstitialAd._interstitial_ad;
		return interstitial_ad != null && interstitial_ad.IsAdReady();
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x00147C3D File Offset: 0x00145E3D
	public void ShowAd()
	{
		if (!this.IsReady())
		{
			return;
		}
		IronSourceInterstitialAd._interstitial_ad.ShowAd(null);
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x00147C53 File Offset: 0x00145E53
	public bool HasAd()
	{
		if (!this.IsInitialized())
		{
			return false;
		}
		Unity.Services.LevelPlay.LevelPlayInterstitialAd interstitial_ad = IronSourceInterstitialAd._interstitial_ad;
		return interstitial_ad != null && interstitial_ad.IsAdReady();
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x00147C6F File Offset: 0x00145E6F
	public string GetProviderName()
	{
		return "IronSource Interstitial Ad";
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x00147C76 File Offset: 0x00145E76
	public string GetColor()
	{
		return IronSourceMobileAdsLoader.GetColor();
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x00147C7D File Offset: 0x00145E7D
	private void log(string pLog)
	{
		this.logger(this.GetColor() + " " + pLog);
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x00147C9B File Offset: 0x00145E9B
	public bool IsInitialized()
	{
		return IronSourceMobileAdsLoader.initialized;
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x00147CA2 File Offset: 0x00145EA2
	public void showAdInfo(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
	}

	// Token: 0x04001ED0 RID: 7888
	private const string LEVELPLAY_INTERSTITIAL = "none";

	// Token: 0x04001ED6 RID: 7894
	private static string lastError = "";

	// Token: 0x04001ED7 RID: 7895
	private static int loaded = 0;

	// Token: 0x04001ED8 RID: 7896
	private static int failed = 0;

	// Token: 0x04001ED9 RID: 7897
	private static bool _initialized = false;

	// Token: 0x04001EDA RID: 7898
	private static Unity.Services.LevelPlay.LevelPlayInterstitialAd _interstitial_ad;
}
