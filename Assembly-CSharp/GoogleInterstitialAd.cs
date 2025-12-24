using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
[ObfuscateLiterals]
public class GoogleInterstitialAd : IWorldBoxAd
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060028CB RID: 10443 RVA: 0x00146C96 File Offset: 0x00144E96
	// (set) Token: 0x060028CC RID: 10444 RVA: 0x00146C9E File Offset: 0x00144E9E
	public Action<string> logger { get; set; }

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060028CD RID: 10445 RVA: 0x00146CA7 File Offset: 0x00144EA7
	// (set) Token: 0x060028CE RID: 10446 RVA: 0x00146CAF File Offset: 0x00144EAF
	public Action adResetCallback { get; set; }

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060028CF RID: 10447 RVA: 0x00146CB8 File Offset: 0x00144EB8
	// (set) Token: 0x060028D0 RID: 10448 RVA: 0x00146CC0 File Offset: 0x00144EC0
	public Action adFailedCallback { get; set; }

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060028D1 RID: 10449 RVA: 0x00146CC9 File Offset: 0x00144EC9
	// (set) Token: 0x060028D2 RID: 10450 RVA: 0x00146CD1 File Offset: 0x00144ED1
	public Action adFinishedCallback { get; set; }

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060028D3 RID: 10451 RVA: 0x00146CDA File Offset: 0x00144EDA
	// (set) Token: 0x060028D4 RID: 10452 RVA: 0x00146CE2 File Offset: 0x00144EE2
	public Action adStartedCallback { get; set; }

	// Token: 0x060028D5 RID: 10453 RVA: 0x00146CEC File Offset: 0x00144EEC
	public void Reset()
	{
		this.log("reset to " + GoogleInterstitialAd.default_current.ToString());
		GoogleInterstitialAd._current = GoogleInterstitialAd.default_current;
		GoogleInterstitialAd._failed = 0;
		GoogleInterstitialAd._loaded = 0;
		GoogleInterstitialAd._last_error = "";
		GoogleInterstitialAd._last_id = "";
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x00146D40 File Offset: 0x00144F40
	private string getInterstitialAdUnitID()
	{
		this.log("[preint] " + GoogleInterstitialAd._current.ToString());
		if (GoogleInterstitialAd._failed > 1 && GoogleInterstitialAd._loaded == 0)
		{
			GoogleInterstitialAd._failed = 0;
			GoogleInterstitialAd._current++;
			GoogleInterstitialAd._current = Mathf.Clamp(GoogleInterstitialAd._current, 0, 3);
			this.log("Level " + GoogleInterstitialAd._current.ToString());
		}
		else if (GoogleInterstitialAd._loaded > 2 && GoogleInterstitialAd._current > 0)
		{
			GoogleInterstitialAd._current--;
			GoogleInterstitialAd._current = Mathf.Clamp(GoogleInterstitialAd._current, 0, 3);
			this.log("Level " + GoogleInterstitialAd._current.ToString());
		}
		return "unexpected_platform";
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x00146E04 File Offset: 0x00145004
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
		GoogleInterstitialAd._admob_id = this.getInterstitialAdUnitID();
		this.KillAd();
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
		InterstitialAd.Load(GoogleInterstitialAd._admob_id, request, delegate(InterstitialAd ad, LoadAdError error)
		{
			ThreadHelper.ExecuteInUpdate(delegate
			{
				if (error != null || ad == null)
				{
					this.log("Callback error");
					this.HandleOnAdFailedToLoad(error);
					return;
				}
				this.HandleOnAdLoaded();
				this.interstitial = ad;
				this.interstitial.OnAdFullScreenContentOpened += this.HandleOnAdOpened;
				this.interstitial.OnAdFullScreenContentClosed += this.HandleOnAdClosed;
				this.interstitial.OnAdFullScreenContentFailed += this.HandleOnAdFailed;
				this.interstitial.OnAdPaid += this.HandleOnPaidEvent;
			});
		});
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x00146EA1 File Offset: 0x001450A1
	public void HandleOnAdLoaded()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleInterstitialAd._loaded++;
			GoogleInterstitialAd._failed = 0;
			this.log("Ad Loaded");
		});
	}

	// Token: 0x060028D9 RID: 10457 RVA: 0x00146EB4 File Offset: 0x001450B4
	public void HandleOnAdFailedToLoad(LoadAdError pLoadAdError = null)
	{
		string tLoadError = "Failed to load ad";
		if (pLoadAdError != null)
		{
			tLoadError = pLoadAdError.GetMessage();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleInterstitialAd._loaded = 0;
			GoogleInterstitialAd._failed++;
			if (GoogleInterstitialAd._last_error != tLoadError || GoogleInterstitialAd._admob_id != GoogleInterstitialAd._last_id)
			{
				this.log(GoogleInterstitialAd._current.ToString() + " " + GoogleInterstitialAd._admob_id);
				this.log("<color=red>Failed Load: " + tLoadError + "</color>");
				GoogleInterstitialAd._last_error = tLoadError;
				GoogleInterstitialAd._last_id = GoogleInterstitialAd._admob_id;
			}
			else
			{
				this.log("<color=red>Failed Load</color>");
			}
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
			if (tLoadError.Contains("floor") || tLoadError.Contains("fill") || tLoadError.Contains("configured"))
			{
				GoogleInterstitialAd._failed++;
				if (GoogleInterstitialAd._current < 3 && this.adResetCallback != null)
				{
					this.adResetCallback();
				}
			}
		});
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x00146EFC File Offset: 0x001450FC
	public void HandleOnAdFailed(AdError pLoadAdError)
	{
		string tLoadError = "Failed to show ad";
		if (pLoadAdError != null)
		{
			tLoadError = pLoadAdError.GetMessage();
		}
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleInterstitialAd._loaded = 0;
			GoogleInterstitialAd._failed++;
			if (GoogleInterstitialAd._last_error != tLoadError || GoogleInterstitialAd._admob_id != GoogleInterstitialAd._last_id)
			{
				this.log(GoogleInterstitialAd._current.ToString() + " " + GoogleInterstitialAd._admob_id);
				this.log("<color=red>Ad Failed: " + tLoadError + "</color>");
				GoogleInterstitialAd._last_error = tLoadError;
				GoogleInterstitialAd._last_id = GoogleInterstitialAd._admob_id;
			}
			else
			{
				this.log("<color=red>Ad Failed</color>");
			}
			this.KillAd();
			if (this.adFailedCallback != null)
			{
				this.adFailedCallback();
			}
			if (tLoadError.Contains("floor") || tLoadError.Contains("fill") || tLoadError.Contains("configured"))
			{
				GoogleInterstitialAd._failed++;
				if (GoogleInterstitialAd._current < 3 && this.adResetCallback != null)
				{
					this.adResetCallback();
				}
			}
		});
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x00146F41 File Offset: 0x00145141
	public void HandleOnAdOpened()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleInterstitialAd._loaded++;
			GoogleInterstitialAd._failed = 0;
			this.log("Ad Opened");
			if (this.adStartedCallback != null)
			{
				this.adStartedCallback();
			}
		});
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x00146F54 File Offset: 0x00145154
	public void HandleOnAdClosed()
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			GoogleInterstitialAd._loaded++;
			GoogleInterstitialAd._failed = 0;
			this.log("Ad Closed");
			this.KillAd();
			if (this.adFinishedCallback != null)
			{
				this.adFinishedCallback();
			}
		});
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x00146F68 File Offset: 0x00145168
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
			GoogleInterstitialAd._loaded++;
			GoogleInterstitialAd._failed = 0;
			this.log(tLog1);
			this.log(tLog2);
		});
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x00147004 File Offset: 0x00145204
	public void KillAd()
	{
		if (this.interstitial == null)
		{
			return;
		}
		this.interstitial.OnAdFullScreenContentOpened -= this.HandleOnAdOpened;
		this.interstitial.OnAdFullScreenContentClosed -= this.HandleOnAdClosed;
		this.interstitial.OnAdPaid -= this.HandleOnPaidEvent;
		this.interstitial.Destroy();
		this.interstitial = null;
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x00147071 File Offset: 0x00145271
	public bool IsReady()
	{
		return this.interstitial != null && this.interstitial.CanShowAd();
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x00147088 File Offset: 0x00145288
	public void ShowAd()
	{
		if (this.IsReady())
		{
			this.interstitial.Show();
		}
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x0014709D File Offset: 0x0014529D
	public bool HasAd()
	{
		return this.IsInitialized() && this.interstitial != null;
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x001470B2 File Offset: 0x001452B2
	public string GetProviderName()
	{
		return "AdMob Interstitial Ad";
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x001470B9 File Offset: 0x001452B9
	public string GetColor()
	{
		return GoogleMobileAdsLoader.GetColor();
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x001470C0 File Offset: 0x001452C0
	private void log(string pLog)
	{
		this.logger(this.GetColor() + " " + pLog);
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x001470DE File Offset: 0x001452DE
	public bool IsInitialized()
	{
		return GoogleMobileAdsLoader.initialized;
	}

	// Token: 0x04001EB1 RID: 7857
	private InterstitialAd interstitial;

	// Token: 0x04001EB6 RID: 7862
	private static string _last_error = "";

	// Token: 0x04001EB7 RID: 7863
	private static string _last_id = "";

	// Token: 0x04001EB8 RID: 7864
	private static int _loaded = 0;

	// Token: 0x04001EB9 RID: 7865
	private static int _failed = 0;

	// Token: 0x04001EBA RID: 7866
	internal static int default_current = 2;

	// Token: 0x04001EBB RID: 7867
	private static int _current = 2;

	// Token: 0x04001EBC RID: 7868
	private const int max_current = 3;

	// Token: 0x04001EBD RID: 7869
	private static string _admob_id = string.Empty;
}
