using System;
using Beebyte.Obfuscator;
using com.unity3d.mediation;
using Unity.Services.LevelPlay;

// Token: 0x020004AC RID: 1196
[ObfuscateLiterals]
public class IronSourceRewardAd : IWorldBoxAd
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06002953 RID: 10579 RVA: 0x00147F03 File Offset: 0x00146103
	// (set) Token: 0x06002954 RID: 10580 RVA: 0x00147F0B File Offset: 0x0014610B
	public Action<string> logger { get; set; }

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06002955 RID: 10581 RVA: 0x00147F14 File Offset: 0x00146114
	// (set) Token: 0x06002956 RID: 10582 RVA: 0x00147F1C File Offset: 0x0014611C
	public Action adResetCallback { get; set; }

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06002957 RID: 10583 RVA: 0x00147F25 File Offset: 0x00146125
	// (set) Token: 0x06002958 RID: 10584 RVA: 0x00147F2D File Offset: 0x0014612D
	public Action adFailedCallback { get; set; }

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06002959 RID: 10585 RVA: 0x00147F36 File Offset: 0x00146136
	// (set) Token: 0x0600295A RID: 10586 RVA: 0x00147F3E File Offset: 0x0014613E
	public Action adFinishedCallback { get; set; }

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x0600295B RID: 10587 RVA: 0x00147F47 File Offset: 0x00146147
	// (set) Token: 0x0600295C RID: 10588 RVA: 0x00147F4F File Offset: 0x0014614F
	public Action adStartedCallback { get; set; }

	// Token: 0x0600295D RID: 10589 RVA: 0x00147F58 File Offset: 0x00146158
	public void Reset()
	{
		IronSourceRewardAd.failed = 0;
		IronSourceRewardAd.loaded = 0;
		IronSourceRewardAd.lastError = "";
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x00147F70 File Offset: 0x00146170
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
		if (this.HasAd() && !IronSourceRewardAd.started)
		{
			return;
		}
		this.KillAd();
		IronSourceRewardAd.started = false;
		if (!IronSourceRewardAd.initialized)
		{
			IronSourceRewardAd.initialized = true;
			IronSourceRewardAd._rewarded_ad = new Unity.Services.LevelPlay.LevelPlayRewardedAd("none");
			IronSourceRewardAd._rewarded_ad.OnAdLoaded += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleRewardedAdAvailable);
			IronSourceRewardAd._rewarded_ad.OnAdLoadFailed += new Action<com.unity3d.mediation.LevelPlayAdError>(this.HandleRewardedAdUnavailable);
			IronSourceRewardAd._rewarded_ad.OnAdDisplayed += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleRewardBasedVideoOpened);
			IronSourceRewardAd._rewarded_ad.OnAdDisplayFailed += new Action<com.unity3d.mediation.LevelPlayAdDisplayInfoError>(this.HandleRewardedAdFailedToShow);
			IronSourceRewardAd._rewarded_ad.OnAdRewarded += new Action<com.unity3d.mediation.LevelPlayAdInfo, com.unity3d.mediation.LevelPlayReward>(this.HandleRewardBasedVideoRewarded);
			IronSourceRewardAd._rewarded_ad.OnAdClosed += new Action<com.unity3d.mediation.LevelPlayAdInfo>(this.HandleRewardBasedVideoClosed);
			IronSourceRewardAd._rewarded_ad.OnAdLoadFailed += new Action<com.unity3d.mediation.LevelPlayAdError>(this.HandleRewardedAdFailedToLoad);
		}
		this.log("Requesting Ad");
		IronSourceRewardAd._rewarded_ad.LoadAd();
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x0014807F File Offset: 0x0014627F
	public void HandleRewardedAdAvailable(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.loaded++;
			IronSourceRewardAd.failed = 0;
			this.log("Ad Available");
		});
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x00148092 File Offset: 0x00146292
	public void HandleRewardedAdUnavailable(Unity.Services.LevelPlay.LevelPlayAdError pError)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			this.log("Ad Unavailable");
		});
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x001480A5 File Offset: 0x001462A5
	public void HandleRewardBasedVideoOpened(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd.loaded++;
			IronSourceRewardAd.failed = 0;
			this.log("Ad Opened");
			RewardedAds.debug += "h3_";
			if (this.adStartedCallback != null)
			{
				this.adStartedCallback();
			}
		});
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x001480B8 File Offset: 0x001462B8
	public void HandleRewardedAdFailedToShow(Unity.Services.LevelPlay.LevelPlayAdDisplayInfoError pAdError)
	{
		string tLoadError = "Failed to show ad";
		if (pAdError != null)
		{
			tLoadError = pAdError.ToString();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd.loaded = 0;
			IronSourceRewardAd.failed++;
			if (IronSourceRewardAd.lastError != tLoadError)
			{
				this.log("<color=red>Ad Failed to Show: " + tLoadError + "</color>");
				IronSourceRewardAd.lastError = tLoadError;
			}
			else
			{
				this.log("<color=red>Ad Failed to Show</color>");
			}
			RewardedAds.debug += "h4_";
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
		});
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00148100 File Offset: 0x00146300
	public void HandleRewardedAdFailedToLoad(Unity.Services.LevelPlay.LevelPlayAdError pAdError)
	{
		string tLoadError = "Failed to load ad";
		if (pAdError != null)
		{
			tLoadError = pAdError.ToString();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd.loaded = 0;
			IronSourceRewardAd.failed++;
			if (IronSourceRewardAd.lastError != tLoadError)
			{
				this.log("<color=red>Ad Failed to Load: " + tLoadError + "</color>");
				IronSourceRewardAd.lastError = tLoadError;
			}
			else
			{
				this.log("<color=red>Ad Failed to Load</color>");
			}
			RewardedAds.debug += "h4_";
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
		});
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x00148145 File Offset: 0x00146345
	public void HandleRewardBasedVideoRewarded(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo, Unity.Services.LevelPlay.LevelPlayReward pReward)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd.loaded++;
			IronSourceRewardAd.failed = 0;
			this.log("Ad Rewarded");
			if (World.world != null)
			{
				this.log("is worldbox on focus " + World.world.has_focus.ToString());
			}
			RewardedAds.instance.handleRewards();
			RewardedAds.debug += "h5_";
		});
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x00148158 File Offset: 0x00146358
	public void HandleRewardBasedVideoClosed(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd.loaded++;
			IronSourceRewardAd.failed = 0;
			this.log("Ad Closed");
			RewardedAds.debug += "h6_";
			this.KillAd();
			if (this.adFinishedCallback != null)
			{
				this.adFinishedCallback();
			}
		});
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x0014816B File Offset: 0x0014636B
	public void KillAd()
	{
		bool flag = IronSourceRewardAd.started;
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x00148173 File Offset: 0x00146373
	public bool IsReady()
	{
		if (!this.IsInitialized())
		{
			return false;
		}
		Unity.Services.LevelPlay.LevelPlayRewardedAd rewarded_ad = IronSourceRewardAd._rewarded_ad;
		return rewarded_ad != null && rewarded_ad.IsAdReady();
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x0014818F File Offset: 0x0014638F
	public void ShowAd()
	{
		if (this.IsReady())
		{
			IronSourceRewardAd.started = true;
			IronSourceRewardAd._rewarded_ad.ShowAd(null);
		}
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x001481AA File Offset: 0x001463AA
	public bool HasAd()
	{
		return this.IsInitialized() && this.IsReady();
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x001481BC File Offset: 0x001463BC
	public string GetProviderName()
	{
		return "IronSource Rewarded Ad";
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x001481C3 File Offset: 0x001463C3
	public string GetColor()
	{
		return IronSourceMobileAdsLoader.GetColor();
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x001481CA File Offset: 0x001463CA
	private void log(string pLog)
	{
		this.logger(this.GetColor() + " " + pLog);
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x001481E8 File Offset: 0x001463E8
	public bool IsInitialized()
	{
		return IronSourceMobileAdsLoader.initialized;
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x001481EF File Offset: 0x001463EF
	public void showAdInfo(Unity.Services.LevelPlay.LevelPlayAdInfo pAdInfo)
	{
	}

	// Token: 0x04001EDE RID: 7902
	private const string LEVELPLAY_REWARDED = "none";

	// Token: 0x04001EE4 RID: 7908
	private static int loaded = 0;

	// Token: 0x04001EE5 RID: 7909
	private static int failed = 0;

	// Token: 0x04001EE6 RID: 7910
	private static bool initialized = false;

	// Token: 0x04001EE7 RID: 7911
	private static Unity.Services.LevelPlay.LevelPlayRewardedAd _rewarded_ad;

	// Token: 0x04001EE8 RID: 7912
	private static bool started = false;

	// Token: 0x04001EE9 RID: 7913
	private static string lastError = "";
}
