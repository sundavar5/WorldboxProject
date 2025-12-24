using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
public class RewardedAds : MonoBehaviour
{
	// Token: 0x1700023D RID: 573
	// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00149242 File Offset: 0x00147442
	// (set) Token: 0x060029A4 RID: 10660 RVA: 0x00149249 File Offset: 0x00147449
	public static string debug
	{
		get
		{
			return RewardedAds._debug;
		}
		set
		{
			RewardedAds._debug = ((value != null && value.Length > 50) ? value.Substring(value.Length - 50, 50) : value);
		}
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x00149271 File Offset: 0x00147471
	private void Awake()
	{
		RewardedAds.instance = this;
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x0014927C File Offset: 0x0014747C
	public void switchProvider()
	{
		if (!this.should_switch)
		{
			return;
		}
		this.should_switch = false;
		using (ListPool<IWorldBoxAd> tAdProviders = new ListPool<IWorldBoxAd>(RewardedAds.adProviders.Count))
		{
			foreach (IWorldBoxAd tAdProvider in RewardedAds.adProviders)
			{
				if (tAdProvider.IsInitialized() && tAdProvider != RewardedAds.adProvider)
				{
					tAdProviders.Add(tAdProvider);
				}
			}
			if (tAdProviders.Count == 0)
			{
				foreach (IWorldBoxAd tAdProvider2 in RewardedAds.adProviders)
				{
					if (tAdProvider2.IsInitialized())
					{
						tAdProviders.Add(tAdProvider2);
					}
				}
			}
			RewardedAds.adProvider = tAdProviders.GetRandom<IWorldBoxAd>();
			RewardedAds.adProvider.Reset();
			this.log("Switched provider: " + RewardedAds.adProvider.GetProviderName());
		}
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x00149398 File Offset: 0x00147598
	public void unloadAd()
	{
		RewardedAds.debug += "u_";
		RewardedAds.adProvider.KillAd();
		RewardedAds.debug += "u2_";
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x001493CC File Offset: 0x001475CC
	private void RequestRewardBasedVideo()
	{
		RewardedAds.debug += "h8_";
		this.timeout = 18f;
		this.unloadAd();
		this.switchProvider();
		RewardedAds.adProvider.RequestAd();
		RewardedAds.debug += "h9_";
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x00149422 File Offset: 0x00147622
	private static void logEvent(string pEvent)
	{
		Analytics.LogEvent(pEvent, true, true);
		if (!string.IsNullOrEmpty(RewardedAds.adType))
		{
			Analytics.LogEvent(pEvent + "_" + RewardedAds.adType, true, true);
		}
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x0014944F File Offset: 0x0014764F
	private void log(string pString)
	{
		Debug.Log("<color=cyan>[R] " + pString + "</color>");
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x00149466 File Offset: 0x00147666
	private void adReset()
	{
		this.failed = 0;
		this.timeout = 2f;
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x0014947A File Offset: 0x0014767A
	private void adStarted()
	{
		this.failed = 0;
		this.timeout = 2f;
		RewardedAds.logEvent("ad_reward_started");
		RewardedAds._isShowing = true;
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x0014949E File Offset: 0x0014769E
	private void adFailed()
	{
		this.failed++;
		this.timeout = (float)(8 * this.failed);
		RewardedAds.logEvent("ad_reward_failed");
		RewardedAds._isShowing = false;
		this.should_switch = (this.failed > 1);
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x001494DC File Offset: 0x001476DC
	private void adFinished()
	{
		this.failed = 0;
		this.timeout = 2f;
		RewardedAds.logEvent("ad_reward_finished");
		RewardedAds._isShowing = false;
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x00149500 File Offset: 0x00147700
	private PowerButton generateRandomReward()
	{
		return null;
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x00149503 File Offset: 0x00147703
	private bool hasRewardAvailable()
	{
		return false;
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x00149506 File Offset: 0x00147706
	private void generateReward()
	{
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x00149508 File Offset: 0x00147708
	internal static bool isReady()
	{
		return Config.adsInitialized && RewardedAds.initiated && RewardedAds.adProvider != null && RewardedAds.adProvider.IsReady();
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x0014952F File Offset: 0x0014772F
	public static bool hasAd()
	{
		return Config.adsInitialized && RewardedAds.initiated && RewardedAds.adProvider != null && RewardedAds.adProvider.HasAd();
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x00149556 File Offset: 0x00147756
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool isShowing()
	{
		return RewardedAds._isShowing;
	}

	// Token: 0x060029B5 RID: 10677 RVA: 0x00149560 File Offset: 0x00147760
	public void ShowRewardedAd(string pAdType = "")
	{
		RewardedAds.adType = pAdType;
		RewardedAds.debug += "h10_";
		if (RewardedAds.isReady())
		{
			this.log("Show rewarded video");
			this.log("Active ad provider: " + RewardedAds.adProvider.GetProviderName());
			RewardedAds.logEvent("ad_reward_start");
			RewardedAds.adProvider.ShowAd();
			return;
		}
		ScrollWindow.get("ad_loading_error").clickShow(false, false);
		RewardedAds.logEvent("ad_reward_loading_error");
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x001495E4 File Offset: 0x001477E4
	public static void trimTimeout()
	{
		if (RewardedAds.instance == null)
		{
			return;
		}
		if (RewardedAds.instance.timeout > 2f && RewardedAds.instance.failed > 0)
		{
			RewardedAds.instance.timeout = 2f;
			RewardedAds.instance.failed = 0;
		}
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x00149637 File Offset: 0x00147837
	public void handleRewards()
	{
	}

	// Token: 0x04001F1A RID: 7962
	internal static RewardedAds instance;

	// Token: 0x04001F1B RID: 7963
	internal static List<IWorldBoxAd> adProviders = new List<IWorldBoxAd>();

	// Token: 0x04001F1C RID: 7964
	internal static IWorldBoxAd adProvider;

	// Token: 0x04001F1D RID: 7965
	public static bool initiated = false;

	// Token: 0x04001F1E RID: 7966
	private float timeout;

	// Token: 0x04001F1F RID: 7967
	private int failed;

	// Token: 0x04001F20 RID: 7968
	private bool should_switch = true;

	// Token: 0x04001F21 RID: 7969
	private const int AD_TIMEOUT = 8;

	// Token: 0x04001F22 RID: 7970
	private const int AD_REQUEST_TIMEOUT = 10;

	// Token: 0x04001F23 RID: 7971
	private const int LOST_FOCUS_TIMEOUT = 3;

	// Token: 0x04001F24 RID: 7972
	private static string adType;

	// Token: 0x04001F25 RID: 7973
	public static string _debug = "";

	// Token: 0x04001F26 RID: 7974
	private static List<PowerButton> rewardPowers = new List<PowerButton>();

	// Token: 0x04001F27 RID: 7975
	private static List<PowerButton> unlockButtons = new List<PowerButton>();

	// Token: 0x04001F28 RID: 7976
	internal static bool _isShowing = false;
}
