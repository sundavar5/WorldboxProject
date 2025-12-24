using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005F9 RID: 1529
[DisallowMultipleComponent]
public class ScrollableButton : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IScrollHandler
{
	// Token: 0x06003246 RID: 12870 RVA: 0x0017E3C4 File Offset: 0x0017C5C4
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
		this._has_button = base.gameObject.TryGetComponent<Button>(out this._button);
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x0017E43B File Offset: 0x0017C63B
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (this._scroll_wheel_only)
		{
			return;
		}
		this.sendMessage("OnBeginDrag", pEventData);
		if (this._has_button)
		{
			this._button.interactable = false;
		}
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x0017E466 File Offset: 0x0017C666
	public void OnDrag(PointerEventData pEventData)
	{
		if (this._scroll_wheel_only)
		{
			return;
		}
		this.sendMessage("OnDrag", pEventData);
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x0017E47D File Offset: 0x0017C67D
	public void OnEndDrag(PointerEventData pEventData)
	{
		if (this._scroll_wheel_only)
		{
			return;
		}
		this.sendMessage("OnEndDrag", pEventData);
		if (this._has_button)
		{
			this._button.interactable = true;
		}
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x0017E4A8 File Offset: 0x0017C6A8
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		if (this._scroll_wheel_only)
		{
			return;
		}
		this.sendMessage("OnInitializePotentialDrag", pEventData);
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x0017E4BF File Offset: 0x0017C6BF
	public void OnScroll(PointerEventData pEventData)
	{
		this.sendMessage("OnScroll", pEventData);
	}

	// Token: 0x0600324C RID: 12876 RVA: 0x0017E4CD File Offset: 0x0017C6CD
	private void sendMessage(string pMethodName, PointerEventData pEventData)
	{
		ScrollRect scroll_rect = this._scroll_rect;
		if (scroll_rect != null)
		{
			scroll_rect.SendMessage(pMethodName, pEventData);
		}
		ScrollRectExtended scroll_rect_extended = this._scroll_rect_extended;
		if (scroll_rect_extended == null)
		{
			return;
		}
		scroll_rect_extended.SendMessage(pMethodName, pEventData);
	}

	// Token: 0x04002600 RID: 9728
	private ScrollRect _scroll_rect;

	// Token: 0x04002601 RID: 9729
	private ScrollRectExtended _scroll_rect_extended;

	// Token: 0x04002602 RID: 9730
	private Button _button;

	// Token: 0x04002603 RID: 9731
	private bool _has_button;

	// Token: 0x04002604 RID: 9732
	[SerializeField]
	private bool _scroll_wheel_only;
}
