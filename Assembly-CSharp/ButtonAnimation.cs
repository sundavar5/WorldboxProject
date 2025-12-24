using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public class ButtonAnimation : MonoBehaviour
{
	// Token: 0x060030AE RID: 12462 RVA: 0x001775BC File Offset: 0x001757BC
	private IEnumerator newAnim()
	{
		base.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		yield return CoroutineHelper.wait_for_0_01_s;
		base.gameObject.transform.DOScale(1f, ButtonAnimation.scaleTime).SetEase(Ease.InOutBack);
		yield break;
	}

	// Token: 0x060030AF RID: 12463 RVA: 0x001775CB File Offset: 0x001757CB
	public void clickAnimation()
	{
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.newAnim());
		}
	}

	// Token: 0x040024E0 RID: 9440
	public static float scaleTime = 0.1f;
}
