using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007F3 RID: 2035
[DisallowMultipleComponent]
public class TipButton : MonoBehaviour
{
	// Token: 0x06003FE2 RID: 16354 RVA: 0x001B6737 File Offset: 0x001B4937
	private void Awake()
	{
		if (this.hoverAction == null)
		{
			this.setHoverAction(new TooltipAction(this.showTooltipDefault), true);
		}
		this._default_scale = base.gameObject.transform.localScale;
	}

	// Token: 0x06003FE3 RID: 16355 RVA: 0x001B676C File Offset: 0x001B496C
	private void Start()
	{
		Button tB;
		if (base.TryGetComponent<Button>(out tB))
		{
			if (this.showOnClick)
			{
				tB.onClick.AddListener(new UnityAction(this.showTooltipOnClick));
			}
			tB.OnHover(new UnityAction(this.showHoverTooltip));
			tB.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
			return;
		}
		Slider tS = base.GetComponent<Slider>();
		if (tS != null)
		{
			tS.OnHover(new UnityAction(this.showHoverTooltip));
			tS.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
		}
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x001B67FB File Offset: 0x001B49FB
	public void setDefaultScale(Vector3 pScale)
	{
		this._default_scale = pScale;
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x001B6804 File Offset: 0x001B4A04
	public void setHoverAction(TooltipAction pAction, bool pAddAnimation = true)
	{
		this.hoverAction = pAction;
		if (pAddAnimation)
		{
			this.hoverAction = (TooltipAction)Delegate.Combine(this.hoverAction, new TooltipAction(this.clickAnimation));
		}
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x001B6832 File Offset: 0x001B4A32
	private void showTooltipOnClick()
	{
		if (this.clickAction != null)
		{
			this.clickAction();
			return;
		}
		TooltipAction tooltipAction = this.hoverAction;
		if (tooltipAction == null)
		{
			return;
		}
		tooltipAction();
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x001B6858 File Offset: 0x001B4A58
	private void showHoverTooltip()
	{
		if (!Config.tooltips_active)
		{
			return;
		}
		TooltipAction tooltipAction = this.hoverAction;
		if (tooltipAction == null)
		{
			return;
		}
		tooltipAction();
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x001B6874 File Offset: 0x001B4A74
	public void showTooltipDefault()
	{
		if (Config.isMobile)
		{
			if (!string.IsNullOrEmpty(this.text_override_non_steam))
			{
				this.textOnClick = this.text_override_non_steam;
			}
			if (!string.IsNullOrEmpty(this.description_override_non_steam))
			{
				this.textOnClickDescription = this.description_override_non_steam;
			}
		}
		if (this.textOnClick == "" && this.textOnClickDescription == "")
		{
			return;
		}
		TooltipData tData = new TooltipData
		{
			tip_name = this.textOnClick,
			tip_description = this.textOnClickDescription,
			tip_description_2 = this.text_description_2
		};
		Tooltip.show(base.gameObject, this.type, tData);
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x001B691C File Offset: 0x001B4B1C
	public void clickAnimation()
	{
		float tToIncrease = this._default_click_scale_increase;
		if (this.override_click_scale_animation)
		{
			tToIncrease = this.overridden_click_scale_animation;
		}
		float tIncrease = this._default_scale.x * tToIncrease;
		base.transform.localScale = new Vector3(tIncrease, tIncrease, tIncrease);
		this._scale_anim.Kill(false);
		this._scale_anim = base.transform.DOScale(this._default_scale, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x06003FEA RID: 16362 RVA: 0x001B698F File Offset: 0x001B4B8F
	private void OnEnable()
	{
		this.resetAnimation();
	}

	// Token: 0x06003FEB RID: 16363 RVA: 0x001B6997 File Offset: 0x001B4B97
	private void resetAnimation()
	{
		Tweener scale_anim = this._scale_anim;
		if (scale_anim != null)
		{
			scale_anim.Kill(false);
		}
		base.transform.localScale = this._default_scale;
	}

	// Token: 0x06003FEC RID: 16364 RVA: 0x001B69BC File Offset: 0x001B4BBC
	private void OnDestroy()
	{
		this._scale_anim.Kill(false);
	}

	// Token: 0x04002E4E RID: 11854
	private Vector3 _default_scale;

	// Token: 0x04002E4F RID: 11855
	private float _default_click_scale_increase = 1.1f;

	// Token: 0x04002E50 RID: 11856
	public const float SCALE_DURATION = 0.1f;

	// Token: 0x04002E51 RID: 11857
	public string textOnClick;

	// Token: 0x04002E52 RID: 11858
	public string textOnClickDescription;

	// Token: 0x04002E53 RID: 11859
	public string text_description_2;

	// Token: 0x04002E54 RID: 11860
	public string text_override_non_steam = string.Empty;

	// Token: 0x04002E55 RID: 11861
	public string description_override_non_steam = string.Empty;

	// Token: 0x04002E56 RID: 11862
	public TooltipAction hoverAction;

	// Token: 0x04002E57 RID: 11863
	public TooltipAction clickAction;

	// Token: 0x04002E58 RID: 11864
	public bool return_if_same_object;

	// Token: 0x04002E59 RID: 11865
	public string type = "tip";

	// Token: 0x04002E5A RID: 11866
	public bool showOnClick = true;

	// Token: 0x04002E5B RID: 11867
	public bool override_click_scale_animation;

	// Token: 0x04002E5C RID: 11868
	public float overridden_click_scale_animation = 1f;

	// Token: 0x04002E5D RID: 11869
	private Tweener _scale_anim;
}
