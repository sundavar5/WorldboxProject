using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B0 RID: 1200
public class RewardPowerWindow : MonoBehaviour
{
	// Token: 0x06002996 RID: 10646 RVA: 0x00148D1C File Offset: 0x00146F1C
	private void OnEnable()
	{
		if (this._description != null && this._auto_gift_key)
		{
			GodPower power_to_unlock = Config.power_to_unlock;
			string tKey;
			if (((power_to_unlock != null) ? power_to_unlock.id : null) == "clock")
			{
				tKey = "unlock_powers_description_clock_hours";
			}
			else
			{
				tKey = "unlock_powers_description_any";
			}
			this._description.setKeyAndUpdate(tKey);
		}
		InitAds.initAdProviders();
		this.updateButtonIcons();
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x00148D84 File Offset: 0x00146F84
	private void updateButtonIcons()
	{
		if (Config.power_to_unlock != null && this._icons.Length != 0)
		{
			PowerButton tButton = PowerButton.get(Config.power_to_unlock.id);
			if (tButton != null)
			{
				Sprite tSprite = tButton.icon.sprite;
				Image[] icons = this._icons;
				for (int i = 0; i < icons.Length; i++)
				{
					icons[i].sprite = tSprite;
				}
			}
		}
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x00148DE4 File Offset: 0x00146FE4
	public void showRewardedAd()
	{
		GodPower power_to_unlock = Config.power_to_unlock;
		if (((power_to_unlock != null) ? power_to_unlock.id : null) == "clock")
		{
			PlayerConfig.instance.data.powerReward = "clock";
			if (!Config.isMobile && !Config.isEditor)
			{
				return;
			}
			RewardedAds.instance.ShowRewardedAd("clock");
			return;
		}
		else
		{
			PlayerConfig.instance.data.powerReward = Config.power_to_unlock.id;
			if (!Config.isMobile && !Config.isEditor)
			{
				return;
			}
			RewardedAds.instance.ShowRewardedAd("power");
			return;
		}
	}

	// Token: 0x04001F0D RID: 7949
	[SerializeField]
	private LocalizedText _description;

	// Token: 0x04001F0E RID: 7950
	[SerializeField]
	private Image[] _icons;

	// Token: 0x04001F0F RID: 7951
	[SerializeField]
	private bool _auto_gift_key;
}
