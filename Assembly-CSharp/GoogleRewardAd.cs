using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
[ObfuscateLiterals]
public class GoogleRewardAd : IWorldBoxAd
{
	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060028F2 RID: 10482 RVA: 0x00147362 File Offset: 0x00145562
	// (set) Token: 0x060028F3 RID: 10483 RVA: 0x0014736A File Offset: 0x0014556A
	public Action<string> logger { get; set; }

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060028F4 RID: 10484 RVA: 0x00147373 File Offset: 0x00145573
	// (set) Token: 0x060028F5 RID: 10485 RVA: 0x0014737B File Offset: 0x0014557B
	public Action adResetCallback { get; set; }

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00147384 File Offset: 0x00145584
	// (set) Token: 0x060028F7 RID: 10487 RVA: 0x0014738C File Offset: 0x0014558C
	public Action adFailedCallback { get; set; }

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00147395 File Offset: 0x00145595
	// (set) Token: 0x060028F9 RID: 10489 RVA: 0x0014739D File Offset: 0x0014559D
	public Action adFinishedCallback { get; set; }

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060028FA RID: 10490 RVA: 0x001473A6 File Offset: 0x001455A6
	// (set) Token: 0x060028FB RID: 10491 RVA: 0x001473AE File Offset: 0x001455AE
	public Action adStartedCallback { get; set; }

	// Token: 0x060028FC RID: 10492 RVA: 0x001473B8 File Offset: 0x001455B8
	public void Reset()
	{
		this.log("reset to " + GoogleRewardAd.default_current.ToString());
		GoogleRewardAd.current = GoogleRewardAd.default_current;
		GoogleRewardAd.failed = 0;
		GoogleRewardAd.loaded = 0;
		GoogleRewardAd.lastError = "";
		GoogleRewardAd.lastID = "";
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x0014740C File Offset: 0x0014560C
	private string getRewardAdUnitID()
	{
		this.log("[prerew] " + GoogleRewardAd.current.ToString());
		if (GoogleRewardAd.failed > 1 && GoogleRewardAd.loaded == 0)
		{
			GoogleRewardAd.failed = 0;
			GoogleRewardAd.current++;
			GoogleRewardAd.current = Mathf.Clamp(GoogleRewardAd.current, 0, 3);
			this.log("Level " + GoogleRewardAd.current.ToString());
		}
		else if (GoogleRewardAd.loaded > 2 && GoogleRewardAd.current > 0)
		{
			GoogleRewardAd.current--;
			GoogleRewardAd.current = Mathf.Clamp(GoogleRewardAd.current, 0, 3);
			this.log("Level " + GoogleRewardAd.current.ToString());
		}
		return "unexpected_platform";
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x001474D0 File Offset: 0x001456D0
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
		if (this.rewardBasedVideo != null && !this.started)
		{
			return;
		}
		this.KillAd();
		this.started = false;
		GoogleRewardAd._admob_id = this.getRewardAdUnitID();
		AdRequest request;
		if (Config.testAds)
		{
			this.log("Requesting Test Ad");
			request = new AdRequest();
			List<string> testDeviceIds = new List<string>
			{
				"38469EF1320047F75C548E8477B3583B",
				"6b80482efcca7c0f3f07a95f8be98fe6"
			};
			MobileAds.SetRequestConfiguration(new RequestConfiguration
			{
				TestDeviceIds = testDeviceIds
			});
		}
		else
		{
			this.log("Requesting Ad");
			request = new AdRequest();
		}
		RewardedAd.Load(GoogleRewardAd._admob_id, request, delegate(RewardedAd ad, LoadAdError error)
		{
			ThreadHelper.ExecuteInUpdate(delegate
			{
				if (error != null || ad == null)
				{
					this.log("Callback error");
					this.HandleRewardBasedVideoFailedToLoad(error);
					return;
				}
				this.HandleRewardBasedVideoLoaded();
				this.rewardBasedVideo = ad;
				this.rewardBasedVideo.OnAdFullScreenContentOpened += this.HandleRewardBasedVideoOpened;
				this.rewardBasedVideo.OnAdFullScreenContentFailed += this.HandleRewardedAdFailedToShow;
				this.rewardBasedVideo.OnAdPaid += this.HandleOnPaidEvent;
				this.rewardBasedVideo.OnAdFullScreenContentClosed += this.HandleRewardBasedVideoClosed;
			});
		});
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x00147585 File Offset: 0x00145785
	public void HandleRewardBasedVideoLoaded()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded++;
			GoogleRewardAd.failed = 0;
			this.log("Ad Loaded");
			RewardedAds.debug += "h1_";
		});
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x00147598 File Offset: 0x00145798
	public void HandleRewardBasedVideoFailedToLoad(LoadAdError pLoadAdError)
	{
		string tLoadError = "Failed to load ad";
		if (pLoadAdError != null)
		{
			tLoadError = pLoadAdError.GetMessage();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded = 0;
			GoogleRewardAd.failed++;
			if (GoogleRewardAd.lastError != tLoadError || GoogleRewardAd._admob_id != GoogleRewardAd.lastID)
			{
				this.log(GoogleRewardAd.current.ToString() + " " + GoogleRewardAd._admob_id);
				this.log("<color=red>Ad Failed to Load: " + tLoadError + "</color>");
				GoogleRewardAd.lastError = tLoadError;
				GoogleRewardAd.lastID = GoogleRewardAd._admob_id;
			}
			else
			{
				this.log("<color=red>Ad Failed to Load</color>");
			}
			this.started = true;
			RewardedAds.debug += "h2_";
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
			if (tLoadError.Contains("floor") || tLoadError.Contains("fill") || tLoadError.Contains("configured"))
			{
				GoogleRewardAd.failed++;
				if (GoogleRewardAd.current < 3 && this.adResetCallback != null)
				{
					this.adResetCallback();
				}
			}
		});
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x001475DD File Offset: 0x001457DD
	public void HandleRewardBasedVideoOpened()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded++;
			GoogleRewardAd.failed = 0;
			this.log("Ad Opened");
			this.started = true;
			RewardedAds.debug += "h3_";
			if (this.adStartedCallback != null)
			{
				this.adStartedCallback();
			}
		});
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x001475F0 File Offset: 0x001457F0
	public void HandleRewardedAdFailedToShow(AdError pAdError)
	{
		string tLoadError = "Failed to show ad";
		if (pAdError != null)
		{
			tLoadError = pAdError.GetMessage();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded = 0;
			GoogleRewardAd.failed++;
			if (GoogleRewardAd.lastError != tLoadError)
			{
				this.log("<color=red>Ad Failed to Show: " + tLoadError + "</color>");
				GoogleRewardAd.lastError = tLoadError;
			}
			else
			{
				this.log("<color=red>Ad Failed to Show</color>");
			}
			this.started = true;
			RewardedAds.debug += "h4_";
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
		});
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x00147635 File Offset: 0x00145835
	public void HandleRewardBasedVideoRewarded(Reward pAdReward)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded++;
			GoogleRewardAd.failed = 0;
			this.log("Ad Rewarded");
			this.started = true;
			if (World.world != null)
			{
				this.log("is worldbox on focus " + World.world.has_focus.ToString());
			}
			RewardedAds.instance.handleRewards();
			RewardedAds.debug += "h5_";
		});
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x00147648 File Offset: 0x00145848
	public void HandleRewardBasedVideoClosed()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded++;
			GoogleRewardAd.failed = 0;
			this.log("Ad Closed");
			this.started = true;
			RewardedAds.debug += "h6_";
			this.KillAd();
			if (this.adFinishedCallback != null)
			{
				this.adFinishedCallback();
			}
		});
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x0014765C File Offset: 0x0014585C
	public void HandleOnPaidEvent(AdValue pAdValue)
	{
		string tLog1 = "Rewarded interstitial ad has received a paid event. " + pAdValue.ToString();
		string tLog2 = string.Concat(new string[]
		{
			"Values: ",
			pAdValue.Precision.ToString(),
			" ",
			pAdValue.Value.ToString(),
			" ",
			pAdValue.CurrencyCode
		});
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleRewardAd.loaded++;
			GoogleRewardAd.failed = 0;
			this.log(tLog1);
			this.log(tLog2);
			this.started = true;
			RewardedAds.debug += "h7_";
		});
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x001476F8 File Offset: 0x001458F8
	public void KillAd()
	{
		if (this.rewardBasedVideo == null)
		{
			return;
		}
		if (!this.started)
		{
			return;
		}
		this.rewardBasedVideo.OnAdFullScreenContentOpened -= this.HandleRewardBasedVideoOpened;
		this.rewardBasedVideo.OnAdFullScreenContentFailed -= this.HandleRewardedAdFailedToShow;
		this.rewardBasedVideo.OnAdPaid -= this.HandleOnPaidEvent;
		this.rewardBasedVideo.OnAdFullScreenContentClosed -= this.HandleRewardBasedVideoClosed;
		this.rewardBasedVideo.Destroy();
		this.rewardBasedVideo = null;
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x00147785 File Offset: 0x00145985
	public bool IsReady()
	{
		return this.rewardBasedVideo != null && this.rewardBasedVideo.CanShowAd();
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x0014779C File Offset: 0x0014599C
	public void ShowAd()
	{
		if (this.IsReady())
		{
			this.started = true;
			this.rewardBasedVideo.Show(new Action<Reward>(this.HandleRewardBasedVideoRewarded));
		}
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x001477C4 File Offset: 0x001459C4
	public bool HasAd()
	{
		return this.IsInitialized() && this.rewardBasedVideo != null;
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x001477D9 File Offset: 0x001459D9
	public string GetProviderName()
	{
		return "AdMob Rewarded Ad";
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x001477E0 File Offset: 0x001459E0
	public string GetColor()
	{
		return GoogleMobileAdsLoader.GetColor();
	}

	// Token: 0x0600290C RID: 10508 RVA: 0x001477E7 File Offset: 0x001459E7
	private void log(string pLog)
	{
		this.logger(this.GetColor() + " " + pLog);
	}

	// Token: 0x0600290D RID: 10509 RVA: 0x00147805 File Offset: 0x00145A05
	public bool IsInitialized()
	{
		return GoogleMobileAdsLoader.initialized;
	}

	// Token: 0x04001EC1 RID: 7873
	private RewardedAd rewardBasedVideo;

	// Token: 0x04001EC6 RID: 7878
	private static int loaded = 0;

	// Token: 0x04001EC7 RID: 7879
	private static int failed = 0;

	// Token: 0x04001EC8 RID: 7880
	internal static int default_current = 2;

	// Token: 0x04001EC9 RID: 7881
	private static int current = 2;

	// Token: 0x04001ECA RID: 7882
	private const int max_current = 3;

	// Token: 0x04001ECB RID: 7883
	private static string _admob_id = string.Empty;

	// Token: 0x04001ECC RID: 7884
	private bool started;

	// Token: 0x04001ECD RID: 7885
	private static string lastError = "";

	// Token: 0x04001ECE RID: 7886
	private static string lastID = "";
}
