using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200072F RID: 1839
public class PopulationPyramidItem : MonoBehaviour
{
	// Token: 0x06003A90 RID: 14992 RVA: 0x0019E33B File Offset: 0x0019C53B
	private void Awake()
	{
		this.resetBar();
	}

	// Token: 0x06003A91 RID: 14993 RVA: 0x0019E343 File Offset: 0x0019C543
	private void Start()
	{
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.animateBar));
	}

	// Token: 0x06003A92 RID: 14994 RVA: 0x0019E368 File Offset: 0x0019C568
	internal void setCount(int pCount, int pMax)
	{
		this._count_text.text = pCount.ToString();
		Color tColor = this._count_text.color;
		if (pCount == 0)
		{
			tColor.a = 0.5f;
		}
		else
		{
			tColor.a = 1f;
		}
		this._count_text.color = tColor;
		this._count = pCount;
		this._max_count = pMax;
		this.animateBar();
	}

	// Token: 0x06003A93 RID: 14995 RVA: 0x0019E3D0 File Offset: 0x0019C5D0
	internal int getCount()
	{
		return this._count;
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x0019E3D8 File Offset: 0x0019C5D8
	private void resetBar()
	{
		this.checkDestroyTween();
		this._bar.sizeDelta = new Vector2(0.1f, this._bar.sizeDelta.y);
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x0019E408 File Offset: 0x0019C608
	internal void setOpacity(float pOpacity)
	{
		Color tColor = this._bar_image.color;
		tColor.a = pOpacity;
		this._bar_image.color = tColor;
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x0019E438 File Offset: 0x0019C638
	internal void animateBar()
	{
		this.resetBar();
		this._percent = (float)this._count / (float)this._max_count;
		if (this._count > 0)
		{
			this._calc_percent = 4f + Mathf.Floor(this._percent * this._bar_width);
		}
		else
		{
			this._calc_percent = 0f;
		}
		this._cur_tween = this._bar.DOSizeDelta(new Vector2(this._calc_percent, this._bar.sizeDelta.y), 0.3f, false);
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x0019E4C6 File Offset: 0x0019C6C6
	private void OnDisable()
	{
		this.checkDestroyTween();
	}

	// Token: 0x06003A98 RID: 15000 RVA: 0x0019E4CE File Offset: 0x0019C6CE
	private void checkDestroyTween()
	{
		if (this._cur_tween.IsActive())
		{
			this._cur_tween.Complete(false);
			this._cur_tween.Kill(false);
		}
		this._cur_tween = null;
	}

	// Token: 0x04002B3A RID: 11066
	[SerializeField]
	private RectTransform _mask;

	// Token: 0x04002B3B RID: 11067
	[SerializeField]
	private RectTransform _bar;

	// Token: 0x04002B3C RID: 11068
	[SerializeField]
	private Image _bar_image;

	// Token: 0x04002B3D RID: 11069
	[SerializeField]
	private Text _count_text;

	// Token: 0x04002B3E RID: 11070
	[SerializeField]
	private float _bar_width = 80f;

	// Token: 0x04002B3F RID: 11071
	[SerializeField]
	private int _count;

	// Token: 0x04002B40 RID: 11072
	[SerializeField]
	private int _max_count;

	// Token: 0x04002B41 RID: 11073
	[SerializeField]
	private float _percent;

	// Token: 0x04002B42 RID: 11074
	[SerializeField]
	private float _calc_percent;

	// Token: 0x04002B43 RID: 11075
	private Tweener _cur_tween;
}
