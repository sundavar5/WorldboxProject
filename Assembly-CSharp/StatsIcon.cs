using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000601 RID: 1537
public class StatsIcon : MonoBehaviour
{
	// Token: 0x06003275 RID: 12917 RVA: 0x0017EF38 File Offset: 0x0017D138
	private void Awake()
	{
		if (this.text == null)
		{
			return;
		}
		this._default_text_scale = this.text.transform.localScale;
		if (base.TryGetComponent<TipButton>(out this._tip_button) && this._tip_button.type == "tip")
		{
			this._tip_button.setHoverAction(new TooltipAction(this.tooltipAction), true);
		}
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.restartCounter));
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x0017EFD0 File Offset: 0x0017D1D0
	private void tooltipAction()
	{
		if (this._tip_button.textOnClick == "" && this._tip_button.textOnClickDescription == "")
		{
			return;
		}
		CustomDataContainer<string> tCustomData = new CustomDataContainer<string>();
		tCustomData["value"] = this._value.ToText();
		if (this._max_value != null)
		{
			tCustomData["max_value"] = this._max_value.Value.ToText();
		}
		TooltipData tData = new TooltipData
		{
			tip_name = this._tip_button.textOnClick,
			tip_description = this._tip_button.textOnClickDescription,
			tip_description_2 = this._tip_button.text_description_2,
			custom_data_string = tCustomData
		};
		Tooltip.show(base.gameObject, "stats_icon", tData);
	}

	// Token: 0x06003277 RID: 12919 RVA: 0x0017F0A1 File Offset: 0x0017D2A1
	public Image getIcon()
	{
		return base.transform.Find("Icon").GetComponent<Image>();
	}

	// Token: 0x06003278 RID: 12920 RVA: 0x0017F0B8 File Offset: 0x0017D2B8
	private void restartCounter()
	{
		if (!this._is_counter_enabled)
		{
			return;
		}
		if (!this.enable_animation)
		{
			return;
		}
		this.setValue(this._value, this._max_value, this._color, this._is_float, this._ending, this._separator, true);
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x0017F0F8 File Offset: 0x0017D2F8
	public void setValue(float pValue)
	{
		this.setValue(pValue, null, "", false, "", '/', false);
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x0017F123 File Offset: 0x0017D323
	public bool areValuesTooClose(float pNewValue)
	{
		return Mathf.Approximately(this.getValue(), pNewValue);
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x0017F138 File Offset: 0x0017D338
	public void setValue(float pValue, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/', bool pFromZero = false)
	{
		this._is_counter_enabled = true;
		float tPreviousValue = pFromZero ? 0f : this._value;
		this._value = pValue;
		this._max_value = pMax;
		this._color = pColor;
		this._ending = pEnding;
		this._is_float = pFloat;
		this._separator = pSeparator;
		Color tColor = this.text.color;
		if (pValue == 0f)
		{
			tColor.a = 0.5f;
		}
		else
		{
			tColor.a = 1f;
		}
		this.text.color = tColor;
		if (!this.enable_animation)
		{
			this.text.text = this.getFinalText();
			return;
		}
		this.checkDestroyTween();
		string tEnding = this.getEnding();
		if (pFloat)
		{
			this._cur_tween = this.text.DOUpCounter(tPreviousValue, this._value, 0.45f, tEnding, pColor);
			return;
		}
		this._cur_tween = this.text.DOUpCounter((int)tPreviousValue, (int)this._value, 0.45f, tEnding, pColor);
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x0017F234 File Offset: 0x0017D434
	private string getEnding()
	{
		string tEnding = "";
		if (this._max_value != null)
		{
			tEnding += this._separator.ToString();
			if (this._is_float)
			{
				float? num = this._max_value % (float)1;
				float num2 = 0f;
				if (!(num.GetValueOrDefault() == num2 & num != null))
				{
					tEnding += this._max_value.Value.ToText();
					goto IL_AC;
				}
			}
			tEnding += Toolbox.formatNumber((long)this._max_value.Value, 4);
		}
		IL_AC:
		if (!string.IsNullOrEmpty(this._ending))
		{
			tEnding += this._ending;
		}
		return tEnding;
	}

	// Token: 0x0600327D RID: 12925 RVA: 0x0017F308 File Offset: 0x0017D508
	private string getFinalText()
	{
		string tShortenedValue;
		if (this._is_float && this._value % 1f != 0f)
		{
			tShortenedValue = this._value.ToText();
		}
		else
		{
			tShortenedValue = Toolbox.formatNumber((long)this._value, 4);
		}
		tShortenedValue += this.getEnding();
		if (this._color != "")
		{
			return Toolbox.coloredText(tShortenedValue, this._color, false);
		}
		return tShortenedValue;
	}

	// Token: 0x0600327E RID: 12926 RVA: 0x0017F37A File Offset: 0x0017D57A
	public float getValue()
	{
		return this._value;
	}

	// Token: 0x0600327F RID: 12927 RVA: 0x0017F382 File Offset: 0x0017D582
	private void OnEnable()
	{
		this.restartCounter();
	}

	// Token: 0x06003280 RID: 12928 RVA: 0x0017F38A File Offset: 0x0017D58A
	private void OnDisable()
	{
		this.checkDestroyTween();
	}

	// Token: 0x06003281 RID: 12929 RVA: 0x0017F392 File Offset: 0x0017D592
	public void checkDestroyTween()
	{
		this._cur_tween.Kill(true);
		this._cur_tween = null;
		Tweener text_scale_anim = this._text_scale_anim;
		if (text_scale_anim != null)
		{
			text_scale_anim.Kill(true);
		}
		this._text_scale_anim = null;
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x0017F3C0 File Offset: 0x0017D5C0
	public void textScaleAnimation()
	{
		this.text.transform.localScale = this._default_text_scale * 1.2f;
		Tweener text_scale_anim = this._text_scale_anim;
		if (text_scale_anim != null)
		{
			text_scale_anim.Kill(true);
		}
		this._text_scale_anim = this.text.transform.DOScale(this._default_text_scale, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x04002622 RID: 9762
	private const float TWEEN_DURATION = 0.45f;

	// Token: 0x04002623 RID: 9763
	private const float SCALE = 1.2f;

	// Token: 0x04002624 RID: 9764
	public Text text;

	// Token: 0x04002625 RID: 9765
	private float _value = -1f;

	// Token: 0x04002626 RID: 9766
	private float? _max_value;

	// Token: 0x04002627 RID: 9767
	private char _separator = '/';

	// Token: 0x04002628 RID: 9768
	private string _ending = "";

	// Token: 0x04002629 RID: 9769
	private string _color = "";

	// Token: 0x0400262A RID: 9770
	private bool _is_float;

	// Token: 0x0400262B RID: 9771
	private Tweener _cur_tween;

	// Token: 0x0400262C RID: 9772
	private Tweener _text_scale_anim;

	// Token: 0x0400262D RID: 9773
	private bool _is_counter_enabled;

	// Token: 0x0400262E RID: 9774
	internal bool enable_animation = true;

	// Token: 0x0400262F RID: 9775
	private TipButton _tip_button;

	// Token: 0x04002630 RID: 9776
	private Vector2 _default_text_scale;
}
