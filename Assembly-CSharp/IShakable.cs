using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020005DF RID: 1503
public interface IShakable
{
	// Token: 0x17000294 RID: 660
	// (get) Token: 0x0600316A RID: 12650
	float shake_duration { get; }

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x0600316B RID: 12651
	float shake_strength { get; }

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x0600316C RID: 12652
	// (set) Token: 0x0600316D RID: 12653
	Tweener shake_tween { get; set; }

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x0600316E RID: 12654
	Transform transform { get; }

	// Token: 0x0600316F RID: 12655 RVA: 0x0017A420 File Offset: 0x00178620
	void shake()
	{
		this.killShakeTween();
		this.shake_tween = this.transform.DOShakePosition(this.shake_duration, this.shake_strength, 10, 90f, false, true, ShakeRandomnessMode.Full);
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x0017A45A File Offset: 0x0017865A
	void killShakeTween()
	{
		this.shake_tween.Kill(true);
	}
}
