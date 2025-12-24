using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020005D7 RID: 1495
public class CustomButtonSwitch : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06003123 RID: 12579 RVA: 0x00178D50 File Offset: 0x00176F50
	private void Start()
	{
		this.anim = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x06003124 RID: 12580 RVA: 0x00178D64 File Offset: 0x00176F64
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Action action = this.click_decrease;
			if (action != null)
			{
				action();
			}
			SoundBox.click();
			this.newClickAnimation();
			return;
		}
		Action action2 = this.click_increase;
		if (action2 != null)
		{
			action2();
		}
		SoundBox.click();
		this.newClickAnimation();
	}

	// Token: 0x06003125 RID: 12581 RVA: 0x00178DB3 File Offset: 0x00176FB3
	private void Awake()
	{
		this.defaultScale = base.transform.localScale;
		this.clickedScale = this.defaultScale * 1.1f;
	}

	// Token: 0x06003126 RID: 12582 RVA: 0x00178DDC File Offset: 0x00176FDC
	public void newClickAnimation()
	{
		base.transform.DOKill(false);
		base.transform.localScale = this.clickedScale;
		base.transform.DOScale(this.defaultScale, 0.3f).SetEase(Ease.InOutBack);
	}

	// Token: 0x06003127 RID: 12583 RVA: 0x00178E1A File Offset: 0x0017701A
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x0400251E RID: 9502
	public Action click_increase;

	// Token: 0x0400251F RID: 9503
	public Action click_decrease;

	// Token: 0x04002520 RID: 9504
	private Animator anim;

	// Token: 0x04002521 RID: 9505
	private Vector3 defaultScale;

	// Token: 0x04002522 RID: 9506
	private Vector3 clickedScale;
}
