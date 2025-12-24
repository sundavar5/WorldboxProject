using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200060C RID: 1548
public class UiMover : MonoBehaviour
{
	// Token: 0x060032C1 RID: 12993 RVA: 0x00180561 File Offset: 0x0017E761
	private void Awake()
	{
		if (this.initInitPos)
		{
			this.initPos = base.gameObject.transform.localPosition;
		}
	}

	// Token: 0x060032C2 RID: 12994 RVA: 0x00180584 File Offset: 0x0017E784
	public void setVisible(bool pVisible, bool pNow = false, TweenCallback pCompleteCallback = null)
	{
		this.visible = pVisible;
		if (!pNow)
		{
			if (this.visible)
			{
				if (!this.onVisible)
				{
					this.onVisible = true;
					this.moveTween(this.initPos, pCompleteCallback);
					return;
				}
			}
			else if (this.onVisible)
			{
				this.onVisible = false;
				this.moveTween(this.hidePos, pCompleteCallback);
			}
			return;
		}
		if (pVisible)
		{
			base.gameObject.transform.localPosition = this.initPos;
			return;
		}
		base.gameObject.transform.localPosition = this.hidePos;
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x00180610 File Offset: 0x0017E810
	protected void moveTween(Vector3 pVecPos, TweenCallback pCompleteCallback = null)
	{
		float tTime = 0.35f;
		this._tweener.Kill(true);
		this._tweener = base.transform.DOLocalMove(pVecPos, tTime, false).SetDelay(0.02f).SetEase(Ease.InOutCubic).OnComplete(pCompleteCallback);
	}

	// Token: 0x0400265B RID: 9819
	public bool onVisible;

	// Token: 0x0400265C RID: 9820
	public Vector3 initPos;

	// Token: 0x0400265D RID: 9821
	public Vector3 hidePos;

	// Token: 0x0400265E RID: 9822
	public bool visible;

	// Token: 0x0400265F RID: 9823
	public bool initInitPos = true;

	// Token: 0x04002660 RID: 9824
	private Tweener _tweener;
}
