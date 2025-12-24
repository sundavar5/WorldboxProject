using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A3 RID: 1187
public class AdLoadingButton : MonoBehaviour
{
	// Token: 0x060028C7 RID: 10439 RVA: 0x00146B4A File Offset: 0x00144D4A
	private void Awake()
	{
		this.button_image = this.button.GetComponent<Image>();
		this.button_localized_text = this.button_text.GetComponent<LocalizedText>();
		this.state = AdLoadingButtonState.None;
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x00146B78 File Offset: 0x00144D78
	private void Update()
	{
		AdLoadingButtonState tState;
		if (Config.isEditor && Config.editor_test_rewards_from_ads)
		{
			tState = AdLoadingButtonState.AdReady;
			this.state = tState;
			this.toggleState();
			return;
		}
		if (RewardedAds.isReady())
		{
			tState = AdLoadingButtonState.AdReady;
		}
		else if (!Config.adsInitialized)
		{
			tState = AdLoadingButtonState.Initializing;
		}
		else
		{
			tState = AdLoadingButtonState.AdLoading;
			RewardedAds.trimTimeout();
		}
		if (tState == this.state)
		{
			return;
		}
		this.state = tState;
		this.toggleState();
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x00146BDC File Offset: 0x00144DDC
	private void toggleState()
	{
		switch (this.state)
		{
		case AdLoadingButtonState.Initializing:
			this.button.interactable = false;
			this.button_localized_text.setKeyAndUpdate("waiting_for_ad");
			this.button_image.sprite = this.spriteOff;
			return;
		case AdLoadingButtonState.AdLoading:
			this.button.interactable = false;
			this.button_localized_text.setKeyAndUpdate("loading_ads");
			this.button_image.sprite = this.spriteOff;
			return;
		case AdLoadingButtonState.AdReady:
			this.button.interactable = true;
			this.button_localized_text.setKeyAndUpdate("watch_ad");
			this.button_image.sprite = this.spriteOn;
			return;
		default:
			return;
		}
	}

	// Token: 0x04001EA9 RID: 7849
	public Text button_text;

	// Token: 0x04001EAA RID: 7850
	public LocalizedText button_localized_text;

	// Token: 0x04001EAB RID: 7851
	public Button button;

	// Token: 0x04001EAC RID: 7852
	private Image button_image;

	// Token: 0x04001EAD RID: 7853
	public Sprite spriteOn;

	// Token: 0x04001EAE RID: 7854
	public Sprite spriteOff;

	// Token: 0x04001EAF RID: 7855
	private AdLoadingButtonState state;
}
