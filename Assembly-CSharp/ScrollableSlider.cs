using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005FA RID: 1530
[DisallowMultipleComponent]
[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(SliderExtended))]
public class ScrollableSlider : MonoBehaviour, IScrollHandler, IEventSystemHandler
{
	// Token: 0x0600324E RID: 12878 RVA: 0x0017E4FC File Offset: 0x0017C6FC
	protected void Start()
	{
		this._scroll_rect_extended = base.gameObject.GetComponentInParent<ScrollRectExtended>();
		if (this._scroll_rect_extended == null)
		{
			this._scroll_rect = base.gameObject.GetComponentInParent<ScrollRect>();
		}
		if (this._scroll_rect == null && this._scroll_rect_extended == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x0017E55C File Offset: 0x0017C75C
	public void OnScroll(PointerEventData pEventData)
	{
		ScrollRect scroll_rect = this._scroll_rect;
		if (scroll_rect != null)
		{
			scroll_rect.SendMessage("OnScroll", pEventData);
		}
		ScrollRectExtended scroll_rect_extended = this._scroll_rect_extended;
		if (scroll_rect_extended == null)
		{
			return;
		}
		scroll_rect_extended.SendMessage("OnScroll", pEventData);
	}

	// Token: 0x04002605 RID: 9733
	private ScrollRect _scroll_rect;

	// Token: 0x04002606 RID: 9734
	private ScrollRectExtended _scroll_rect_extended;
}
