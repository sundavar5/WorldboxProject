using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200061E RID: 1566
public class AchievementButton : MonoBehaviour
{
	// Token: 0x06003357 RID: 13143 RVA: 0x00182BF0 File Offset: 0x00180DF0
	public void Load(Achievement pAchievement)
	{
		this._achievement = pAchievement;
		Sprite tNewSprite = this._achievement.getIcon();
		if (tNewSprite != null)
		{
			this._icon.sprite = tNewSprite;
			if (!AchievementLibrary.isUnlocked(this._achievement))
			{
				this._icon.color = Color.black;
				this._background_default.SetActive(true);
				this._background_completed.GetComponent<Image>().enabled = false;
				this._icon_medal.SetActive(false);
			}
		}
		if (pAchievement.unlocks_something)
		{
			this._background_legendary.gameObject.SetActive(true);
		}
		else
		{
			this._background_legendary.gameObject.SetActive(false);
		}
		base.name = this._achievement.id;
	}

	// Token: 0x06003358 RID: 13144 RVA: 0x00182CA8 File Offset: 0x00180EA8
	private void Start()
	{
		base.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06003359 RID: 13145 RVA: 0x00182D14 File Offset: 0x00180F14
	private void showHoverTooltip()
	{
		if (!Config.tooltips_active)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x00182D24 File Offset: 0x00180F24
	private void showTooltip()
	{
		Tooltip.show(this, "achievement", new TooltipData
		{
			achievement = this._achievement
		});
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		base.transform.DOKill(false);
		base.transform.DOScale(0.8f, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x040026EE RID: 9966
	private Achievement _achievement;

	// Token: 0x040026EF RID: 9967
	[SerializeField]
	private Image _icon;

	// Token: 0x040026F0 RID: 9968
	[SerializeField]
	private Image _background_completed;

	// Token: 0x040026F1 RID: 9969
	[SerializeField]
	private Image _background_legendary;

	// Token: 0x040026F2 RID: 9970
	[SerializeField]
	private GameObject _background_default;

	// Token: 0x040026F3 RID: 9971
	[SerializeField]
	private GameObject _icon_medal;
}
