using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B1 RID: 1201
public class RewardUI : MonoBehaviour
{
	// Token: 0x0600299A RID: 10650 RVA: 0x00148E80 File Offset: 0x00147080
	internal void setRewardInfo(List<PowerButton> pButtons)
	{
		this.rewardPowers = pButtons;
		this.nextReward();
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x00148E8F File Offset: 0x0014708F
	internal bool hasRewards()
	{
		return this.rewardPowers != null && this.rewardPowers.Count > 0;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x00148EAC File Offset: 0x001470AC
	internal PowerButton popLowestReward()
	{
		int lowestRank = 10000;
		int lowestIndex = 0;
		int i = 0;
		foreach (PowerButton item in this.rewardPowers)
		{
			if (item.godPower.rank < (PowerRank)lowestRank)
			{
				lowestIndex = i;
				lowestRank = (int)item.godPower.rank;
			}
			i++;
		}
		PowerButton result = this.rewardPowers[lowestIndex];
		this.rewardPowers.RemoveAt(lowestIndex);
		return result;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x00148F40 File Offset: 0x00147140
	internal void nextReward()
	{
		if (!this.hasRewards())
		{
			return;
		}
		PowerButton tButton = this.popLowestReward();
		this.powerSprite.sprite = tButton.icon.sprite;
		this.text.GetComponent<LocalizedText>().setKeyAndUpdate(tButton.godPower.getLocaleID());
		this.text_description.gameObject.SetActive(true);
		this.text_description.GetComponent<LocalizedText>().setKeyAndUpdate(tButton.godPower.getDescriptionID());
		if (tButton.godPower.id == "clock")
		{
			this.window_title.GetComponent<LocalizedText>().key = "free_hourglass_title";
			this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_hourglass_unlocked";
			this.rewardAnimation.quickReward = true;
		}
		else
		{
			this.window_title.GetComponent<LocalizedText>().key = "free_power";
			this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_power_unlocked";
			this.rewardAnimation.quickReward = false;
		}
		PlayerConfig.instance.data.lastReward = tButton.godPower.id;
		this.window_title.GetComponent<LocalizedText>().updateText(true);
		this.free_power_unlocked.GetComponent<LocalizedText>().updateText(true);
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x0014907C File Offset: 0x0014727C
	public void bottomButtonClick()
	{
		if (this.rewardAnimation.state != RewardAnimationState.Open)
		{
			if (this.rewardAnimation.state == RewardAnimationState.Idle)
			{
				this.rewardAnimation.clickAnimation();
			}
			return;
		}
		if (this.hasRewards())
		{
			this.rewardAnimation.resetAnim();
			this.nextReward();
			return;
		}
		base.GetComponent<ButtonEvent>().hideRewardWindowAndHighlightPower();
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x001490D8 File Offset: 0x001472D8
	internal void setRewardInfo(string pSpritePath, string pText)
	{
		this.powerSprite.sprite = SpriteTextureLoader.getSprite("ui/Icons/" + pSpritePath);
		this.text.GetComponent<LocalizedText>().key = pText;
		this.text.GetComponent<LocalizedText>().updateText(true);
		this.text_description.gameObject.SetActive(false);
		this.window_title.GetComponent<LocalizedText>().key = "free_saveslots_title";
		this.window_title.GetComponent<LocalizedText>().updateText(true);
		this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_saveslots_unlocked";
		this.free_power_unlocked.GetComponent<LocalizedText>().updateText(true);
		this.rewardAnimation.quickReward = true;
	}

	// Token: 0x04001F10 RID: 7952
	public Image powerSprite;

	// Token: 0x04001F11 RID: 7953
	public Text text;

	// Token: 0x04001F12 RID: 7954
	public Text text_description;

	// Token: 0x04001F13 RID: 7955
	public Text window_title;

	// Token: 0x04001F14 RID: 7956
	public Text free_power_unlocked;

	// Token: 0x04001F15 RID: 7957
	public List<PowerButton> rewardPowers;

	// Token: 0x04001F16 RID: 7958
	public RewardAnimation rewardAnimation;
}
