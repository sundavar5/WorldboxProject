using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000832 RID: 2098
public class IconRotationAnimation : MonoBehaviour
{
	// Token: 0x06004143 RID: 16707 RVA: 0x001BC854 File Offset: 0x001BAA54
	private void Awake()
	{
		this.initScale = base.transform.localScale;
		this.scaleTo = this.initScale * 1.1f;
		if (this.randomDelay)
		{
			this.delay = Randy.randomFloat(1f, 10f);
		}
	}

	// Token: 0x06004144 RID: 16708 RVA: 0x001BC8A5 File Offset: 0x001BAAA5
	private void checkDestroyTween()
	{
		if (this.curTween != null && this.curTween.active)
		{
			this.curTween.Complete(false);
			this.curTween.Kill(false);
			this.curTween = null;
		}
	}

	// Token: 0x06004145 RID: 16709 RVA: 0x001BC8DC File Offset: 0x001BAADC
	private void rotate1()
	{
		if (base.transform == null)
		{
			return;
		}
		this.curTween = base.transform.DOScale(this.scaleTo, 0.3f).SetDelay(this.delay).SetEase(Ease.InOutBack).OnComplete(new TweenCallback(this.rotate2));
	}

	// Token: 0x06004146 RID: 16710 RVA: 0x001BC938 File Offset: 0x001BAB38
	private void rotate2()
	{
		if (base.transform == null)
		{
			return;
		}
		if (this.randomDelay)
		{
			this.delay = Randy.randomFloat(1f, 10f);
		}
		this.curTween = base.transform.DOScale(this.initScale, 0.3f).SetDelay(0f).SetEase(Ease.InOutBack).OnComplete(new TweenCallback(this.rotate1));
	}

	// Token: 0x06004147 RID: 16711 RVA: 0x001BC9AF File Offset: 0x001BABAF
	private void OnEnable()
	{
		this.checkDestroyTween();
		this.rotate1();
	}

	// Token: 0x06004148 RID: 16712 RVA: 0x001BC9BD File Offset: 0x001BABBD
	private void OnDisable()
	{
		this.checkDestroyTween();
	}

	// Token: 0x06004149 RID: 16713 RVA: 0x001BC9C5 File Offset: 0x001BABC5
	private void OnDestroy()
	{
		this.checkDestroyTween();
	}

	// Token: 0x04002FA1 RID: 12193
	public float delay = 5f;

	// Token: 0x04002FA2 RID: 12194
	public bool randomDelay;

	// Token: 0x04002FA3 RID: 12195
	private Vector3 initScale;

	// Token: 0x04002FA4 RID: 12196
	private Vector3 scaleTo;

	// Token: 0x04002FA5 RID: 12197
	internal Tweener curTween;
}
