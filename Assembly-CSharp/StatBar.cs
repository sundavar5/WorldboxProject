using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007EB RID: 2027
public class StatBar : MonoBehaviour
{
	// Token: 0x06003FB9 RID: 16313 RVA: 0x001B600A File Offset: 0x001B420A
	private void OnEnable()
	{
		this.restartBar();
	}

	// Token: 0x06003FBA RID: 16314 RVA: 0x001B6012 File Offset: 0x001B4212
	private void Start()
	{
		this.restartBar();
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.restartBar));
	}

	// Token: 0x06003FBB RID: 16315 RVA: 0x001B603B File Offset: 0x001B423B
	private void restartBar()
	{
		this.setBar(this._val, this._max, this._ending, true, this._float, true, 0.3f);
	}

	// Token: 0x06003FBC RID: 16316 RVA: 0x001B6064 File Offset: 0x001B4264
	private void resetSize()
	{
		float tPercent = Mathf.Floor(0.01f * this.mask.rect.width);
		this.bar.sizeDelta = new Vector2(tPercent, this.bar.sizeDelta.y);
		this.textField.text = "0";
	}

	// Token: 0x06003FBD RID: 16317 RVA: 0x001B60C1 File Offset: 0x001B42C1
	public void resetTween()
	{
		this.checkDestroyTween(false);
		this._val = 0f;
		this.resetSize();
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x001B60DB File Offset: 0x001B42DB
	private void OnRectTransformDimensionsChange()
	{
		this.restartBar();
	}

	// Token: 0x06003FBF RID: 16319 RVA: 0x001B60E4 File Offset: 0x001B42E4
	public void setBar(float pVal, float pMax, string pEnding, bool pReset = true, bool pFloat = false, bool pUpdateText = true, float pSpeed = 0.3f)
	{
		if (pMax == 0f)
		{
			this.resetTween();
			this.textField.text = "-";
			return;
		}
		if (!pReset && pVal == this._val && pMax == this._max && pEnding == this._ending)
		{
			return;
		}
		if (pReset)
		{
			this.resetTween();
		}
		this._max = pMax;
		this._ending = pEnding;
		this._float = pFloat;
		this.checkDestroyTween(false);
		if (pReset)
		{
			this._bar_tween = this.bar.DOSizeDelta(new Vector2(0f, this.bar.sizeDelta.y), 0.005f, false).OnComplete(delegate
			{
				float tPercent2 = Mathf.Floor(pVal / pMax * this.mask.rect.width);
				this._bar_tween = this.bar.DOSizeDelta(new Vector2(tPercent2, this.bar.sizeDelta.y), pSpeed, false);
			});
		}
		else
		{
			float tPercent = Mathf.Floor(pVal / pMax * this.mask.rect.width);
			this._bar_tween = this.bar.DOSizeDelta(new Vector2(tPercent, this.bar.sizeDelta.y), pSpeed, false);
		}
		if (pUpdateText)
		{
			if (pFloat)
			{
				this._text_tween = this.textField.DOUpCounter(this._val, pVal, pSpeed, pEnding, "");
			}
			else
			{
				this._text_tween = this.textField.DOUpCounter((int)this._val, (int)pVal, pSpeed, pEnding, "");
			}
		}
		this._val = pVal;
		StatBarUpdated bar_updated_action = this._bar_updated_action;
		if (bar_updated_action == null)
		{
			return;
		}
		bar_updated_action(pVal, pMax);
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x001B62B6 File Offset: 0x001B44B6
	private void OnDisable()
	{
		this.checkDestroyTween(true);
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x001B62C0 File Offset: 0x001B44C0
	private void checkDestroyTween(bool pComplete = true)
	{
		if (this._bar_tween.IsActive())
		{
			this._bar_tween.Complete(pComplete);
			this._bar_tween.Kill(pComplete);
			this._bar_tween = null;
		}
		if (this._text_tween.IsActive())
		{
			this._text_tween.Complete(pComplete);
			this._text_tween.Kill(pComplete);
			this._text_tween = null;
		}
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x001B6325 File Offset: 0x001B4525
	public void addCallback(StatBarUpdated pAction)
	{
		this._bar_updated_action = (StatBarUpdated)Delegate.Combine(this._bar_updated_action, pAction);
	}

	// Token: 0x06003FC3 RID: 16323 RVA: 0x001B633E File Offset: 0x001B453E
	public void removeCallback(StatBarUpdated pAction)
	{
		this._bar_updated_action = (StatBarUpdated)Delegate.Remove(this._bar_updated_action, pAction);
	}

	// Token: 0x04002E36 RID: 11830
	public Text textField;

	// Token: 0x04002E37 RID: 11831
	public RectTransform mask;

	// Token: 0x04002E38 RID: 11832
	public RectTransform bar;

	// Token: 0x04002E39 RID: 11833
	private Tweener _bar_tween;

	// Token: 0x04002E3A RID: 11834
	private Tweener _text_tween;

	// Token: 0x04002E3B RID: 11835
	private float _val;

	// Token: 0x04002E3C RID: 11836
	private float _max = 100f;

	// Token: 0x04002E3D RID: 11837
	private string _ending;

	// Token: 0x04002E3E RID: 11838
	private bool _float;

	// Token: 0x04002E3F RID: 11839
	public StatBarUpdated _bar_updated_action;
}
