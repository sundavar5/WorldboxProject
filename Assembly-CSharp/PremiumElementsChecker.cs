using System;
using UnityEngine;

// Token: 0x0200083A RID: 2106
public class PremiumElementsChecker : MonoBehaviour
{
	// Token: 0x060041C6 RID: 16838 RVA: 0x001BF156 File Offset: 0x001BD356
	private void Awake()
	{
		PremiumElementsChecker.instance = this;
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x001BF15E File Offset: 0x001BD35E
	internal static bool goodForInterstitialAd()
	{
		return DebugConfig.isOn(DebugOption.TestAds);
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x001BF16B File Offset: 0x001BD36B
	public static void setInterstitialAdTimer(int howLong = 80)
	{
		if (DebugConfig.isOn(DebugOption.TestAds))
		{
			howLong = 15;
		}
		if (howLong > 100)
		{
			howLong = 100;
		}
		PremiumElementsChecker.instance.insterAdTimer = (float)howLong;
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x001BF18E File Offset: 0x001BD38E
	private void Update()
	{
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x001BF190 File Offset: 0x001BD390
	public static void checkElements()
	{
		if (Config.hasPremium)
		{
			if (PremiumElementsChecker.instance.premiumButtonCorner != null)
			{
				PremiumElementsChecker.instance.premiumButtonCorner.SetActive(false);
			}
			if (PremiumElementsChecker.instance.adsButton != null)
			{
				PremiumElementsChecker.instance.adsButton.SetActive(false);
			}
		}
		else if (PremiumElementsChecker.instance.premiumButtonCorner != null)
		{
			PremiumElementsChecker.instance.premiumButtonCorner.SetActive(true);
		}
		foreach (PowerButton powerButton in PowerButton.power_buttons)
		{
			powerButton.checkLockIcon();
		}
	}

	// Token: 0x060041CB RID: 16843 RVA: 0x001BF250 File Offset: 0x001BD450
	public static void toggleActive(bool pState)
	{
		if (PremiumElementsChecker.instance.premiumButtonCorner != null)
		{
			PremiumElementsChecker.instance.premiumButtonCorner.SetActive(pState);
		}
		if (PremiumElementsChecker.instance.adsButton != null)
		{
			PremiumElementsChecker.instance.adsButton.SetActive(pState);
		}
	}

	// Token: 0x04003010 RID: 12304
	public GameObject premiumButtonCorner;

	// Token: 0x04003011 RID: 12305
	public GameObject adsButton;

	// Token: 0x04003012 RID: 12306
	private static PremiumElementsChecker instance;

	// Token: 0x04003013 RID: 12307
	internal float insterAdTimer = 25f;
}
