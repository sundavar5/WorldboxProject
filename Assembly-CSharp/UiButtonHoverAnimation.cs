using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000607 RID: 1543
[DisallowMultipleComponent]
public class UiButtonHoverAnimation : MonoBehaviour
{
	// Token: 0x060032A0 RID: 12960 RVA: 0x0017FD48 File Offset: 0x0017DF48
	private void Awake()
	{
		this.button = base.GetComponent<Button>();
		this.default_scale = base.gameObject.transform.localScale;
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x0017FD6C File Offset: 0x0017DF6C
	private void Start()
	{
		this.button.OnHover(new UnityAction(this.startAnim));
	}

	// Token: 0x060032A2 RID: 12962 RVA: 0x0017FD88 File Offset: 0x0017DF88
	private void startAnim()
	{
		float tIncrease = this.default_scale.x * this.scale_size;
		base.transform.localScale = new Vector3(tIncrease, tIncrease, tIncrease);
		base.transform.DOKill(false);
		base.transform.DOScale(this.default_scale, UiButtonHoverAnimation.scaleTime).SetEase(Ease.InBack);
	}

	// Token: 0x060032A3 RID: 12963 RVA: 0x0017FDE6 File Offset: 0x0017DFE6
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x0400263E RID: 9790
	private Button button;

	// Token: 0x0400263F RID: 9791
	public Vector3 default_scale;

	// Token: 0x04002640 RID: 9792
	public float scale_size = 1.1f;

	// Token: 0x04002641 RID: 9793
	public static float scaleTime = 0.1f;
}
