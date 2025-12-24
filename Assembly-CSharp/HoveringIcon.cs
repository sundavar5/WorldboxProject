using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005DC RID: 1500
public class HoveringIcon : MonoBehaviour
{
	// Token: 0x0600315D RID: 12637 RVA: 0x0017A21F File Offset: 0x0017841F
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x0600315E RID: 12638 RVA: 0x0017A239 File Offset: 0x00178439
	internal void clear()
	{
		this._tweener.Kill(false);
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x0017A247 File Offset: 0x00178447
	internal void init()
	{
		this._original_pos = base.transform.localPosition;
		this._random_timer = Randy.randomFloat(1f * this.timer_mod, 1.5f * this.timer_mod);
		this.startAnimation();
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x0017A283 File Offset: 0x00178483
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x0017A28C File Offset: 0x0017848C
	private void startAnimation()
	{
		this._tweener.Kill(false);
		base.transform.localPosition = new Vector3(this._original_pos.x, this._original_pos.y = this._original_pos.y + Randy.randomFloat(this.min, this.max));
		if (Randy.randomBool())
		{
			this.moveStageOne();
			return;
		}
		this.moveStageTwo();
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x0017A2F7 File Offset: 0x001784F7
	private void moveStageTwo()
	{
		this._tweener = base.transform.DOLocalMove(this._original_pos, this._random_timer, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.moveStageOne));
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x0017A330 File Offset: 0x00178530
	private void moveStageOne()
	{
		Vector3 tVec = new Vector3(this._original_pos.x, this._original_pos.y, 1f);
		tVec.y += 3f;
		this._tweener = base.transform.DOLocalMove(tVec, this._random_timer, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.moveStageTwo));
	}

	// Token: 0x04002546 RID: 9542
	private Vector3 _original_pos;

	// Token: 0x04002547 RID: 9543
	private float _random_timer;

	// Token: 0x04002548 RID: 9544
	public Image image;

	// Token: 0x04002549 RID: 9545
	public float min = -2f;

	// Token: 0x0400254A RID: 9546
	public float max = 2f;

	// Token: 0x0400254B RID: 9547
	public float timer_mod = 1f;

	// Token: 0x0400254C RID: 9548
	private Tweener _tweener;

	// Token: 0x0400254D RID: 9549
	internal RectTransform rect;
}
