using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class PlayInterstitialAd : MonoBehaviour
{
	// Token: 0x06002976 RID: 10614 RVA: 0x0014837D File Offset: 0x0014657D
	private void Awake()
	{
		PlayInterstitialAd.instance = this;
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x00148385 File Offset: 0x00146585
	private void Update()
	{
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x00148387 File Offset: 0x00146587
	public void initAds()
	{
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x0014838C File Offset: 0x0014658C
	public void realInit()
	{
		PlayInterstitialAd.initiated = true;
		PlayInterstitialAd.adProviders.Add(new IronSourceInterstitialAd());
		PlayInterstitialAd.adProviders.Add(new GoogleInterstitialAd());
		foreach (IWorldBoxAd worldBoxAd in PlayInterstitialAd.adProviders)
		{
			worldBoxAd.adResetCallback = new Action(this.adReset);
			worldBoxAd.adFinishedCallback = new Action(this.adFinished);
			worldBoxAd.adFailedCallback = new Action(this.adFailed);
			worldBoxAd.adStartedCallback = new Action(this.adStarted);
			worldBoxAd.logger = new Action<string>(this.log);
		}
		this.switchProvider();
		this.scheduleAd(8f);
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x00148464 File Offset: 0x00146664
	public void switchProvider()
	{
		if (!this.should_switch)
		{
			return;
		}
		this.should_switch = false;
		using (ListPool<IWorldBoxAd> tAdProviders = new ListPool<IWorldBoxAd>(PlayInterstitialAd.adProviders.Count))
		{
			foreach (IWorldBoxAd tAdProvider in PlayInterstitialAd.adProviders)
			{
				if (tAdProvider.IsInitialized() && tAdProvider != PlayInterstitialAd.adProvider)
				{
					tAdProviders.Add(tAdProvider);
				}
			}
			if (tAdProviders.Count == 0)
			{
				foreach (IWorldBoxAd tAdProvider2 in PlayInterstitialAd.adProviders)
				{
					if (tAdProvider2.IsInitialized())
					{
						tAdProviders.Add(tAdProvider2);
					}
				}
			}
			PlayInterstitialAd.adProvider = tAdProviders.GetRandom<IWorldBoxAd>();
			PlayInterstitialAd.adProvider.Reset();
			this.log("Switched provider: " + PlayInterstitialAd.adProvider.GetProviderName());
		}
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x00148580 File Offset: 0x00146780
	internal bool isReady()
	{
		return false;
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x00148583 File Offset: 0x00146783
	public static bool hasAd()
	{
		return PlayInterstitialAd.adProvider.HasAd();
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x00148590 File Offset: 0x00146790
	internal void showAd()
	{
		this.log("Show interstitial ad");
		this.log("Active ad provider: " + PlayInterstitialAd.adProvider.GetProviderName());
		MonoBehaviour.print("- Show interstitial ad " + this.isReady().ToString());
		PlayInterstitialAd.logEvent("interstitial_ad_show");
		PlayInterstitialAd.adProvider.ShowAd();
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x001485F4 File Offset: 0x001467F4
	public static void forceShowAd()
	{
		try
		{
			PlayInterstitialAd.logEvent("interstitial_ad_force_show");
			if (!PlayInterstitialAd.initiated)
			{
				PlayInterstitialAd.instance.realInit();
				PlayInterstitialAd.adProvider.RequestAd();
			}
			if (PlayInterstitialAd.adProvider.IsReady())
			{
				PlayInterstitialAd.adProvider.ShowAd();
			}
			else
			{
				PlayInterstitialAd.adProvider.RequestAd();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x00148660 File Offset: 0x00146860
	private void scheduleAd(float pTimer = 60f)
	{
		if (this.timeout > 0f)
		{
			return;
		}
		PlayInterstitialAd.adProvider.KillAd();
		this.timeout = pTimer;
		this.switchProvider();
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x00148687 File Offset: 0x00146887
	private static void logEvent(string pEvent)
	{
		Analytics.LogEvent(pEvent, true, true);
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x00148691 File Offset: 0x00146891
	private void log(string pString)
	{
		Debug.Log("<color=yellow>[I] " + pString + "</color>");
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x001486A8 File Offset: 0x001468A8
	private void adReset()
	{
		this.failed = 0;
		this.timeout = 2f;
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x001486BC File Offset: 0x001468BC
	private void adStarted()
	{
		this.failed = 0;
		this.timeout = 8f;
		PlayInterstitialAd.logEvent("interstitial_ad_started");
		PlayInterstitialAd._isShowing = true;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x001486E0 File Offset: 0x001468E0
	private void adFailed()
	{
		this.failed++;
		this.timeout = (float)(8 * this.failed);
		PlayInterstitialAd.logEvent("interstitial_ad_failed");
		PlayInterstitialAd._isShowing = false;
		this.should_switch = (this.failed > 1);
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x0014871E File Offset: 0x0014691E
	private void adFinished()
	{
		this.failed = 0;
		this.timeout = 8f;
		PlayInterstitialAd.logEvent("interstitial_ad_finished");
		PlayInterstitialAd._isShowing = false;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x00148742 File Offset: 0x00146942
	internal static bool isShowing()
	{
		return PlayInterstitialAd._isShowing;
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x00148749 File Offset: 0x00146949
	internal static void setActive(bool pActive = false)
	{
		if (PlayInterstitialAd.instance == null)
		{
			PlayInterstitialAd.instance = GameObject.Find("Services").GetComponentInChildren<PlayInterstitialAd>(true);
		}
		PlayInterstitialAd.instance.gameObject.SetActive(pActive);
	}

	// Token: 0x04001EEA RID: 7914
	public static PlayInterstitialAd instance;

	// Token: 0x04001EEB RID: 7915
	internal static List<IWorldBoxAd> adProviders = new List<IWorldBoxAd>();

	// Token: 0x04001EEC RID: 7916
	public static IWorldBoxAd adProvider;

	// Token: 0x04001EED RID: 7917
	public static bool initiated = false;

	// Token: 0x04001EEE RID: 7918
	public float timeout;

	// Token: 0x04001EEF RID: 7919
	private int failed;

	// Token: 0x04001EF0 RID: 7920
	private bool should_switch = true;

	// Token: 0x04001EF1 RID: 7921
	private const int AD_TIMEOUT = 8;

	// Token: 0x04001EF2 RID: 7922
	private const int LOST_FOCUS_TIMEOUT = 3;

	// Token: 0x04001EF3 RID: 7923
	internal static bool _isShowing = false;
}
