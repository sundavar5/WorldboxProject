using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000475 RID: 1141
[Obsolete]
public class OptionBool : MonoBehaviour
{
	// Token: 0x06002730 RID: 10032 RVA: 0x0013E272 File Offset: 0x0013C472
	private void Start()
	{
		this.updateSprite();
		if (this.invokeCallbackOnStart)
		{
			if (this.callback != null)
			{
				this.callback.Invoke();
			}
			if (this.boolCallback != null)
			{
				this.boolCallback.Invoke(this.optionEnabled);
			}
		}
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x0013E2B0 File Offset: 0x0013C4B0
	public void checkGameOption(bool pSwitch = false)
	{
		if (pSwitch)
		{
			PlayerConfig.switchOption(this.gameOptionName, this.gameOptionType);
		}
		this.optionEnabled = PlayerConfig.optionEnabled(this.gameOptionName, this.gameOptionType);
		this.updateSprite();
		OptionAsset tAsset = AssetManager.options_library.get(this.gameOptionName);
		ActionOptionAsset action = tAsset.action;
		if (action == null)
		{
			return;
		}
		action(tAsset);
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x0013E310 File Offset: 0x0013C510
	private void OnEnable()
	{
		if (World.world == null)
		{
			return;
		}
		if (this.gameOption)
		{
			this.updateSprite();
			this.checkGameOption(false);
			this.updateSprite();
		}
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x0013E33C File Offset: 0x0013C53C
	public void clickButton()
	{
		if (this.gameOption)
		{
			this.checkGameOption(true);
			PlayerConfig.saveData();
			return;
		}
		this.optionEnabled = !this.optionEnabled;
		this.updateSprite();
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
		if (this.boolCallback != null)
		{
			this.boolCallback.Invoke(this.optionEnabled);
		}
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x0013E39F File Offset: 0x0013C59F
	private void updateSprite()
	{
		if (this.optionEnabled)
		{
			this.icon.sprite = this.spriteOn;
			return;
		}
		this.icon.sprite = this.spriteOff;
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x0013E3CC File Offset: 0x0013C5CC
	public void optionSpriteAnimation()
	{
		Config.sprite_animations_on = !Config.sprite_animations_on;
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x0013E3DB File Offset: 0x0013C5DB
	public void optionShowWORLD()
	{
		World.world.gameObject.SetActive(!World.world.gameObject.activeSelf);
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x0013E3FE File Offset: 0x0013C5FE
	public void optionRemovePremuium()
	{
		Config.hasPremium = false;
		PlayerConfig.instance.data.premium = false;
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
		if (Config.isMobile)
		{
			InAppManager.consumePremium();
		}
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x0013E42C File Offset: 0x0013C62C
	public void clearRewards()
	{
		PlayerConfig.instance.data.rewardedPowers.Clear();
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x0013E44C File Offset: 0x0013C64C
	public void optionShowCanvas()
	{
		World.world.canvas.enabled = false;
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x0013E45E File Offset: 0x0013C65E
	public void optionRenderer()
	{
		this.spriteRenderer.enabled = this.optionEnabled;
		this.updateSprite();
	}

	// Token: 0x04001D6C RID: 7532
	public bool optionEnabled = true;

	// Token: 0x04001D6D RID: 7533
	public bool invokeCallbackOnStart = true;

	// Token: 0x04001D6E RID: 7534
	public SpriteRenderer spriteRenderer;

	// Token: 0x04001D6F RID: 7535
	public Image icon;

	// Token: 0x04001D70 RID: 7536
	private Button button;

	// Token: 0x04001D71 RID: 7537
	public Sprite spriteOn;

	// Token: 0x04001D72 RID: 7538
	public Sprite spriteOff;

	// Token: 0x04001D73 RID: 7539
	public UnityEvent callback;

	// Token: 0x04001D74 RID: 7540
	public UnityEvent<bool> boolCallback;

	// Token: 0x04001D75 RID: 7541
	public bool gameOption;

	// Token: 0x04001D76 RID: 7542
	public OptionType gameOptionType;

	// Token: 0x04001D77 RID: 7543
	public string gameOptionName = "-";
}
