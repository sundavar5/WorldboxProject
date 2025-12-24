using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005F8 RID: 1528
[Skip]
[SelectionBase]
[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public class ScrollRectExtended : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x060031E5 RID: 12773 RVA: 0x0017C8A6 File Offset: 0x0017AAA6
	// (set) Token: 0x060031E6 RID: 12774 RVA: 0x0017C8AE File Offset: 0x0017AAAE
	public RectTransform content
	{
		get
		{
			return this.m_Content;
		}
		set
		{
			this.m_Content = value;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0017C8B7 File Offset: 0x0017AAB7
	// (set) Token: 0x060031E8 RID: 12776 RVA: 0x0017C8BF File Offset: 0x0017AABF
	public bool horizontal
	{
		get
		{
			return this.m_Horizontal;
		}
		set
		{
			this.m_Horizontal = value;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x060031E9 RID: 12777 RVA: 0x0017C8C8 File Offset: 0x0017AAC8
	// (set) Token: 0x060031EA RID: 12778 RVA: 0x0017C8D0 File Offset: 0x0017AAD0
	public bool vertical
	{
		get
		{
			return this.m_Vertical;
		}
		set
		{
			this.m_Vertical = value;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x060031EB RID: 12779 RVA: 0x0017C8D9 File Offset: 0x0017AAD9
	// (set) Token: 0x060031EC RID: 12780 RVA: 0x0017C8E1 File Offset: 0x0017AAE1
	public ScrollRectExtended.MovementType movementType
	{
		get
		{
			return this.m_MovementType;
		}
		set
		{
			this.m_MovementType = value;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x060031ED RID: 12781 RVA: 0x0017C8EA File Offset: 0x0017AAEA
	// (set) Token: 0x060031EE RID: 12782 RVA: 0x0017C8F2 File Offset: 0x0017AAF2
	public float elasticity
	{
		get
		{
			return this.m_Elasticity;
		}
		set
		{
			this.m_Elasticity = value;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x060031EF RID: 12783 RVA: 0x0017C8FB File Offset: 0x0017AAFB
	// (set) Token: 0x060031F0 RID: 12784 RVA: 0x0017C903 File Offset: 0x0017AB03
	public bool inertia
	{
		get
		{
			return this.m_Inertia;
		}
		set
		{
			this.m_Inertia = value;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0017C90C File Offset: 0x0017AB0C
	// (set) Token: 0x060031F2 RID: 12786 RVA: 0x0017C914 File Offset: 0x0017AB14
	public float decelerationRate
	{
		get
		{
			return this.m_DecelerationRate;
		}
		set
		{
			this.m_DecelerationRate = value;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x060031F3 RID: 12787 RVA: 0x0017C91D File Offset: 0x0017AB1D
	// (set) Token: 0x060031F4 RID: 12788 RVA: 0x0017C925 File Offset: 0x0017AB25
	public float scrollSensitivity
	{
		get
		{
			return this.m_ScrollSensitivity;
		}
		set
		{
			this.m_ScrollSensitivity = value;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x060031F5 RID: 12789 RVA: 0x0017C92E File Offset: 0x0017AB2E
	// (set) Token: 0x060031F6 RID: 12790 RVA: 0x0017C936 File Offset: 0x0017AB36
	public RectTransform viewport
	{
		get
		{
			return this.m_Viewport;
		}
		set
		{
			this.m_Viewport = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x060031F7 RID: 12791 RVA: 0x0017C945 File Offset: 0x0017AB45
	// (set) Token: 0x060031F8 RID: 12792 RVA: 0x0017C94D File Offset: 0x0017AB4D
	public float scrollFactor
	{
		get
		{
			return this.m_ScrollFactor;
		}
		set
		{
			this.m_ScrollFactor = value;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x060031F9 RID: 12793 RVA: 0x0017C956 File Offset: 0x0017AB56
	// (set) Token: 0x060031FA RID: 12794 RVA: 0x0017C960 File Offset: 0x0017AB60
	public Scrollbar horizontalScrollbar
	{
		get
		{
			return this.m_HorizontalScrollbar;
		}
		set
		{
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			this.m_HorizontalScrollbar = value;
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			this.SetDirtyCaching();
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x060031FB RID: 12795 RVA: 0x0017C9CC File Offset: 0x0017ABCC
	// (set) Token: 0x060031FC RID: 12796 RVA: 0x0017C9D4 File Offset: 0x0017ABD4
	public Scrollbar verticalScrollbar
	{
		get
		{
			return this.m_VerticalScrollbar;
		}
		set
		{
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_VerticalScrollbar = value;
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.SetDirtyCaching();
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x060031FD RID: 12797 RVA: 0x0017CA40 File Offset: 0x0017AC40
	// (set) Token: 0x060031FE RID: 12798 RVA: 0x0017CA48 File Offset: 0x0017AC48
	public ScrollRectExtended.ScrollbarVisibility horizontalScrollbarVisibility
	{
		get
		{
			return this.m_HorizontalScrollbarVisibility;
		}
		set
		{
			this.m_HorizontalScrollbarVisibility = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x060031FF RID: 12799 RVA: 0x0017CA57 File Offset: 0x0017AC57
	// (set) Token: 0x06003200 RID: 12800 RVA: 0x0017CA5F File Offset: 0x0017AC5F
	public ScrollRectExtended.ScrollbarVisibility verticalScrollbarVisibility
	{
		get
		{
			return this.m_VerticalScrollbarVisibility;
		}
		set
		{
			this.m_VerticalScrollbarVisibility = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06003201 RID: 12801 RVA: 0x0017CA6E File Offset: 0x0017AC6E
	// (set) Token: 0x06003202 RID: 12802 RVA: 0x0017CA76 File Offset: 0x0017AC76
	public float horizontalScrollbarSpacing
	{
		get
		{
			return this.m_HorizontalScrollbarSpacing;
		}
		set
		{
			this.m_HorizontalScrollbarSpacing = value;
			this.SetDirty();
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06003203 RID: 12803 RVA: 0x0017CA85 File Offset: 0x0017AC85
	// (set) Token: 0x06003204 RID: 12804 RVA: 0x0017CA8D File Offset: 0x0017AC8D
	public float verticalScrollbarSpacing
	{
		get
		{
			return this.m_VerticalScrollbarSpacing;
		}
		set
		{
			this.m_VerticalScrollbarSpacing = value;
			this.SetDirty();
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06003205 RID: 12805 RVA: 0x0017CA9C File Offset: 0x0017AC9C
	// (set) Token: 0x06003206 RID: 12806 RVA: 0x0017CAA4 File Offset: 0x0017ACA4
	public ScrollRectExtended.ScrollRectEvent onValueChanged
	{
		get
		{
			return this.m_OnValueChanged;
		}
		set
		{
			this.m_OnValueChanged = value;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06003207 RID: 12807 RVA: 0x0017CAB0 File Offset: 0x0017ACB0
	protected RectTransform viewRect
	{
		get
		{
			if (this.m_ViewRect == null)
			{
				this.m_ViewRect = this.m_Viewport;
			}
			if (this.m_ViewRect == null)
			{
				this.m_ViewRect = (RectTransform)base.transform;
			}
			return this.m_ViewRect;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06003208 RID: 12808 RVA: 0x0017CAFC File Offset: 0x0017ACFC
	// (set) Token: 0x06003209 RID: 12809 RVA: 0x0017CB04 File Offset: 0x0017AD04
	public Vector2 velocity
	{
		get
		{
			return this.m_Velocity;
		}
		set
		{
			this.m_Velocity = value;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x0600320A RID: 12810 RVA: 0x0017CB0D File Offset: 0x0017AD0D
	internal RectTransform rectTransform
	{
		get
		{
			if (this.m_Rect == null)
			{
				this.m_Rect = base.GetComponent<RectTransform>();
			}
			return this.m_Rect;
		}
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x0017CB30 File Offset: 0x0017AD30
	protected ScrollRectExtended()
	{
		this.flexibleWidth = -1f;
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x0017CBEF File Offset: 0x0017ADEF
	public virtual void Rebuild(CanvasUpdate executing)
	{
		if (executing == CanvasUpdate.Prelayout)
		{
			this.UpdateCachedData();
		}
		if (executing == CanvasUpdate.PostLayout)
		{
			this.UpdateBounds();
			this.UpdateScrollbars(Vector2.zero);
			this.UpdatePrevData();
			this.m_HasRebuiltLayout = true;
		}
	}

	// Token: 0x0600320D RID: 12813 RVA: 0x0017CC1C File Offset: 0x0017AE1C
	public virtual void LayoutComplete()
	{
	}

	// Token: 0x0600320E RID: 12814 RVA: 0x0017CC1E File Offset: 0x0017AE1E
	public virtual void GraphicUpdateComplete()
	{
	}

	// Token: 0x0600320F RID: 12815 RVA: 0x0017CC20 File Offset: 0x0017AE20
	private void UpdateCachedData()
	{
		Transform transform = base.transform;
		this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
		this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
		bool flag = this.viewRect.parent == transform;
		bool hScrollbarIsChild = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
		bool vScrollbarIsChild = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
		bool allAreChildren = flag && hScrollbarIsChild && vScrollbarIsChild;
		this.m_HSliderExpand = (allAreChildren && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRectExtended.ScrollbarVisibility.AutoHideAndExpandViewport);
		this.m_VSliderExpand = (allAreChildren && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRectExtended.ScrollbarVisibility.AutoHideAndExpandViewport);
		this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
		this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
	}

	// Token: 0x06003210 RID: 12816 RVA: 0x0017CD80 File Offset: 0x0017AF80
	protected override void OnEnable()
	{
		base.OnEnable();
		ScrollRectExtended.instances.Add(this);
		if (this.m_HorizontalScrollbar)
		{
			this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
		}
		if (this.m_VerticalScrollbar)
		{
			this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
		}
		CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x0017CDF8 File Offset: 0x0017AFF8
	protected override void OnDisable()
	{
		CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
		ScrollRectExtended.instances.Remove(this);
		if (this.m_HorizontalScrollbar)
		{
			this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
		}
		if (this.m_VerticalScrollbar)
		{
			this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
		}
		this.m_HasRebuiltLayout = false;
		this.m_Tracker.Clear();
		this.m_Velocity = Vector2.zero;
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		base.OnDisable();
	}

	// Token: 0x06003212 RID: 12818 RVA: 0x0017CE97 File Offset: 0x0017B097
	public override bool IsActive()
	{
		return base.IsActive() && this.m_Content != null;
	}

	// Token: 0x06003213 RID: 12819 RVA: 0x0017CEAF File Offset: 0x0017B0AF
	private void EnsureLayoutHasRebuilt()
	{
		if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
		{
			Canvas.ForceUpdateCanvases();
		}
	}

	// Token: 0x06003214 RID: 12820 RVA: 0x0017CEC5 File Offset: 0x0017B0C5
	public virtual void StopMovement()
	{
		this.m_Velocity = Vector2.zero;
	}

	// Token: 0x06003215 RID: 12821 RVA: 0x0017CED4 File Offset: 0x0017B0D4
	public virtual void OnScroll(PointerEventData data)
	{
		if (!this.IsActive())
		{
			return;
		}
		CanvasMain.addTooltipShowTimeout(0.06f);
		if (AssetManager.hotkey_library != null && AssetManager.hotkey_library.isHoldingAnyModKey())
		{
			return;
		}
		this.EnsureLayoutHasRebuilt();
		this.UpdateBounds();
		Vector2 delta = data.scrollDelta;
		delta.y *= -1f;
		if (this.vertical && !this.horizontal)
		{
			if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
			{
				delta.y = delta.x;
			}
			delta.x = 0f;
		}
		if (this.horizontal && !this.vertical)
		{
			if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
			{
				delta.x = delta.y;
			}
			delta.y = 0f;
		}
		Vector2 position = this.m_Content.anchoredPosition;
		position += delta * this.m_ScrollSensitivity;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Clamped)
		{
			position += this.CalculateOffset(position - this.m_Content.anchoredPosition);
		}
		this.SetContentAnchoredPosition(position);
		this.UpdateBounds();
	}

	// Token: 0x06003216 RID: 12822 RVA: 0x0017D000 File Offset: 0x0017B200
	public virtual void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		this.m_Velocity = Vector2.zero;
	}

	// Token: 0x06003217 RID: 12823 RVA: 0x0017D018 File Offset: 0x0017B218
	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		this.UpdateBounds();
		this.m_PointerStartLocalCursor = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
		this.m_ContentStartPosition = this.m_Content.anchoredPosition;
		this.m_Dragging = true;
		this._check_timer = this._check_timer_interval;
		eventData.Use();
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x0017D090 File Offset: 0x0017B290
	public virtual void OnEndDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		this.m_Dragging = false;
		eventData.Use();
	}

	// Token: 0x06003219 RID: 12825 RVA: 0x0017D0A8 File Offset: 0x0017B2A8
	public virtual void OnDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		Vector2 localCursor;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out localCursor))
		{
			return;
		}
		this.UpdateBounds();
		Vector2 pointerDelta = localCursor - this.m_PointerStartLocalCursor;
		pointerDelta *= this.m_ScrollFactor;
		Vector2 position = this.m_ContentStartPosition + pointerDelta;
		Vector2 offset = this.CalculateOffset(position - this.m_Content.anchoredPosition);
		position += offset;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Elastic)
		{
			if (offset.x != 0f)
			{
				position.x -= ScrollRectExtended.RubberDelta(offset.x, this.m_ViewBounds.size.x);
			}
			if (offset.y != 0f)
			{
				position.y -= ScrollRectExtended.RubberDelta(offset.y, this.m_ViewBounds.size.y);
			}
		}
		this.SetContentAnchoredPosition(position);
		eventData.Use();
	}

	// Token: 0x0600321A RID: 12826 RVA: 0x0017D1B4 File Offset: 0x0017B3B4
	protected virtual void SetContentAnchoredPosition(Vector2 position)
	{
		if (!this.m_Horizontal)
		{
			position.x = this.m_Content.anchoredPosition.x;
		}
		if (!this.m_Vertical)
		{
			position.y = this.m_Content.anchoredPosition.y;
		}
		if (position != this.m_Content.anchoredPosition)
		{
			this.m_Content.anchoredPosition = position;
			this.UpdateBounds();
		}
	}

	// Token: 0x0600321B RID: 12827 RVA: 0x0017D224 File Offset: 0x0017B424
	private void fixDragHack()
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (this.m_Dragging && !Input.GetMouseButton(0))
		{
			if (this._check_timer > 0f)
			{
				this._check_timer -= Time.fixedDeltaTime;
				return;
			}
			this.m_Dragging = false;
		}
	}

	// Token: 0x0600321C RID: 12828 RVA: 0x0017D270 File Offset: 0x0017B470
	protected virtual void LateUpdate()
	{
		if (!this.m_Content)
		{
			return;
		}
		this.fixDragHack();
		this.EnsureLayoutHasRebuilt();
		this.UpdateScrollbarVisibility();
		this.UpdateBounds();
		float deltaTime = Time.unscaledDeltaTime;
		Vector2 offset = this.CalculateOffset(Vector2.zero);
		if (!this.m_Dragging && (offset != Vector2.zero || this.m_Velocity != Vector2.zero))
		{
			Vector2 position = this.m_Content.anchoredPosition;
			for (int axis = 0; axis < 2; axis++)
			{
				if (this.m_MovementType == ScrollRectExtended.MovementType.Elastic && offset[axis] != 0f)
				{
					float speed = this.m_Velocity[axis];
					position[axis] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[axis], this.m_Content.anchoredPosition[axis] + offset[axis], ref speed, this.m_Elasticity, float.PositiveInfinity, deltaTime);
					this.m_Velocity[axis] = speed;
				}
				else if (this.m_Inertia)
				{
					ref Vector2 ptr = ref this.m_Velocity;
					int index = axis;
					ptr[index] *= Mathf.Pow(this.m_DecelerationRate, deltaTime);
					if (Mathf.Abs(this.m_Velocity[axis]) < 1f)
					{
						this.m_Velocity[axis] = 0f;
					}
					ptr = ref position;
					index = axis;
					ptr[index] += this.m_Velocity[axis] * deltaTime;
				}
				else
				{
					this.m_Velocity[axis] = 0f;
				}
			}
			if (this.m_Velocity != Vector2.zero)
			{
				if (this.m_MovementType == ScrollRectExtended.MovementType.Clamped)
				{
					offset = this.CalculateOffset(position - this.m_Content.anchoredPosition);
					position += offset;
				}
				this.SetContentAnchoredPosition(position);
			}
		}
		if (this.m_Dragging && this.m_Inertia)
		{
			Vector3 newVelocity = (this.m_Content.anchoredPosition - this.m_PrevPosition) / deltaTime;
			newVelocity *= this.velocityMod;
			this.m_Velocity = Vector3.Lerp(this.m_Velocity, newVelocity, deltaTime * 10f);
		}
		if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
		{
			this.UpdateScrollbars(offset);
			this.m_OnValueChanged.Invoke(this.normalizedPosition);
			this.UpdatePrevData();
		}
	}

	// Token: 0x0600321D RID: 12829 RVA: 0x0017D524 File Offset: 0x0017B724
	private void UpdatePrevData()
	{
		if (this.m_Content == null)
		{
			this.m_PrevPosition = Vector2.zero;
		}
		else
		{
			this.m_PrevPosition = this.m_Content.anchoredPosition;
		}
		this.m_PrevViewBounds = this.m_ViewBounds;
		this.m_PrevContentBounds = this.m_ContentBounds;
	}

	// Token: 0x0600321E RID: 12830 RVA: 0x0017D578 File Offset: 0x0017B778
	private void UpdateScrollbars(Vector2 offset)
	{
		if (this.m_HorizontalScrollbar)
		{
			if (this.m_ContentBounds.size.x > 0f)
			{
				this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
			}
			else
			{
				this.m_HorizontalScrollbar.size = 1f;
			}
			this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
		}
		if (this.m_VerticalScrollbar)
		{
			if (this.m_ContentBounds.size.y > 0f)
			{
				this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
			}
			else
			{
				this.m_VerticalScrollbar.size = 1f;
			}
			this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x0600321F RID: 12831 RVA: 0x0017D68D File Offset: 0x0017B88D
	// (set) Token: 0x06003220 RID: 12832 RVA: 0x0017D6A0 File Offset: 0x0017B8A0
	public Vector2 normalizedPosition
	{
		get
		{
			return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
		}
		set
		{
			this.SetNormalizedPosition(value.x, 0);
			this.SetNormalizedPosition(value.y, 1);
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06003221 RID: 12833 RVA: 0x0017D6BC File Offset: 0x0017B8BC
	// (set) Token: 0x06003222 RID: 12834 RVA: 0x0017D75C File Offset: 0x0017B95C
	public float horizontalNormalizedPosition
	{
		get
		{
			this.UpdateBounds();
			if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x)
			{
				return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
			}
			return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
		}
		set
		{
			this.SetNormalizedPosition(value, 0);
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06003223 RID: 12835 RVA: 0x0017D768 File Offset: 0x0017B968
	// (set) Token: 0x06003224 RID: 12836 RVA: 0x0017D808 File Offset: 0x0017BA08
	public float verticalNormalizedPosition
	{
		get
		{
			this.UpdateBounds();
			if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y)
			{
				return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
			}
			return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
		}
		set
		{
			this.SetNormalizedPosition(value, 1);
		}
	}

	// Token: 0x06003225 RID: 12837 RVA: 0x0017D812 File Offset: 0x0017BA12
	private void SetHorizontalNormalizedPosition(float value)
	{
		this.SetNormalizedPosition(value, 0);
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x0017D81C File Offset: 0x0017BA1C
	private void SetVerticalNormalizedPosition(float value)
	{
		this.SetNormalizedPosition(value, 1);
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x0017D828 File Offset: 0x0017BA28
	private void SetNormalizedPosition(float value, int axis)
	{
		this.EnsureLayoutHasRebuilt();
		this.UpdateBounds();
		float hiddenLength = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
		float contentBoundsMinPosition = this.m_ViewBounds.min[axis] - value * hiddenLength;
		float newLocalPosition = this.m_Content.localPosition[axis] + contentBoundsMinPosition - this.m_ContentBounds.min[axis];
		Vector3 localPosition = this.m_Content.localPosition;
		if (Mathf.Abs(localPosition[axis] - newLocalPosition) > 0.01f)
		{
			localPosition[axis] = newLocalPosition;
			this.m_Content.localPosition = localPosition;
			this.m_Velocity[axis] = 0f;
			this.UpdateBounds();
		}
	}

	// Token: 0x06003228 RID: 12840 RVA: 0x0017D903 File Offset: 0x0017BB03
	private static float RubberDelta(float overStretching, float viewSize)
	{
		return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
	}

	// Token: 0x06003229 RID: 12841 RVA: 0x0017D92E File Offset: 0x0017BB2E
	protected override void OnRectTransformDimensionsChange()
	{
		this.SetDirty();
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x0600322A RID: 12842 RVA: 0x0017D936 File Offset: 0x0017BB36
	private bool hScrollingNeeded
	{
		get
		{
			return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x0600322B RID: 12843 RVA: 0x0017D969 File Offset: 0x0017BB69
	private bool vScrollingNeeded
	{
		get
		{
			return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
		}
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x0017D99C File Offset: 0x0017BB9C
	public virtual void CalculateLayoutInputHorizontal()
	{
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x0017D99E File Offset: 0x0017BB9E
	public virtual void CalculateLayoutInputVertical()
	{
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x0600322E RID: 12846 RVA: 0x0017D9A0 File Offset: 0x0017BBA0
	public virtual float minWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x0600322F RID: 12847 RVA: 0x0017D9A7 File Offset: 0x0017BBA7
	public virtual float preferredWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06003230 RID: 12848 RVA: 0x0017D9AE File Offset: 0x0017BBAE
	// (set) Token: 0x06003231 RID: 12849 RVA: 0x0017D9B6 File Offset: 0x0017BBB6
	public virtual float flexibleWidth { get; private set; }

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06003232 RID: 12850 RVA: 0x0017D9BF File Offset: 0x0017BBBF
	public virtual float minHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06003233 RID: 12851 RVA: 0x0017D9C6 File Offset: 0x0017BBC6
	public virtual float preferredHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06003234 RID: 12852 RVA: 0x0017D9CD File Offset: 0x0017BBCD
	public virtual float flexibleHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06003235 RID: 12853 RVA: 0x0017D9D4 File Offset: 0x0017BBD4
	public virtual int layoutPriority
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x0017D9D8 File Offset: 0x0017BBD8
	public virtual void SetLayoutHorizontal()
	{
		this.m_Tracker.Clear();
		if (this.m_HSliderExpand || this.m_VSliderExpand)
		{
			this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			this.viewRect.anchorMin = Vector2.zero;
			this.viewRect.anchorMax = Vector2.one;
			this.viewRect.sizeDelta = Vector2.zero;
			this.viewRect.anchoredPosition = Vector2.zero;
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_VSliderExpand && this.vScrollingNeeded)
		{
			this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_HSliderExpand && this.hScrollingNeeded)
		{
			this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
		{
			this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
		}
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x0017DC34 File Offset: 0x0017BE34
	public virtual void SetLayoutVertical()
	{
		this.UpdateScrollbarLayout();
		this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
		this.m_ContentBounds = this.GetBounds();
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x0017DC90 File Offset: 0x0017BE90
	private void UpdateScrollbarVisibility()
	{
		if (this.m_VerticalScrollbar && this.m_VerticalScrollbarVisibility != ScrollRectExtended.ScrollbarVisibility.Permanent && this.m_VerticalScrollbar.gameObject.activeSelf != this.vScrollingNeeded)
		{
			this.m_VerticalScrollbar.gameObject.SetActive(this.vScrollingNeeded);
		}
		if (this.m_HorizontalScrollbar && this.m_HorizontalScrollbarVisibility != ScrollRectExtended.ScrollbarVisibility.Permanent && this.m_HorizontalScrollbar.gameObject.activeSelf != this.hScrollingNeeded)
		{
			this.m_HorizontalScrollbar.gameObject.SetActive(this.hScrollingNeeded);
		}
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x0017DD24 File Offset: 0x0017BF24
	private void UpdateScrollbarLayout()
	{
		if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
		{
			this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
			this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
			this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
			this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
			if (this.vScrollingNeeded)
			{
				this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
			}
			else
			{
				this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
			}
		}
		if (this.m_HSliderExpand && this.m_VerticalScrollbar)
		{
			this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
			this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
			this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
			this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
			if (this.hScrollingNeeded)
			{
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				return;
			}
			this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
		}
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x0017DF2C File Offset: 0x0017C12C
	private void UpdateBounds()
	{
		this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
		this.m_ContentBounds = this.GetBounds();
		if (this.m_Content == null)
		{
			return;
		}
		Vector3 contentSize = this.m_ContentBounds.size;
		Vector3 contentPos = this.m_ContentBounds.center;
		Vector3 excess = this.m_ViewBounds.size - contentSize;
		if (excess.x > 0f)
		{
			contentPos.x -= excess.x * (this.m_Content.pivot.x - 0.5f);
			contentSize.x = this.m_ViewBounds.size.x;
		}
		if (excess.y > 0f)
		{
			contentPos.y -= excess.y * (this.m_Content.pivot.y - 0.5f);
			contentSize.y = this.m_ViewBounds.size.y;
		}
		this.m_ContentBounds.size = contentSize;
		this.m_ContentBounds.center = contentPos;
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x0017E06C File Offset: 0x0017C26C
	private Bounds GetBounds()
	{
		if (this.m_Content == null)
		{
			return default(Bounds);
		}
		Vector3 vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		Matrix4x4 toLocal = this.viewRect.worldToLocalMatrix;
		this.m_Content.GetWorldCorners(this.m_Corners);
		for (int i = 0; i < 4; i++)
		{
			Vector3 lhs = toLocal.MultiplyPoint3x4(this.m_Corners[i]);
			vMin = Vector3.Min(lhs, vMin);
			vMax = Vector3.Max(lhs, vMax);
		}
		Bounds bounds = new Bounds(vMin, Vector3.zero);
		bounds.Encapsulate(vMax);
		return bounds;
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x0017E124 File Offset: 0x0017C324
	private Vector2 CalculateOffset(Vector2 delta)
	{
		Vector2 offset = Vector2.zero;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Unrestricted)
		{
			return offset;
		}
		Vector2 min = this.m_ContentBounds.min;
		Vector2 max = this.m_ContentBounds.max;
		if (this.m_Horizontal)
		{
			min.x += delta.x;
			max.x += delta.x;
			if (min.x > this.m_ViewBounds.min.x)
			{
				offset.x = this.m_ViewBounds.min.x - min.x;
			}
			else if (max.x < this.m_ViewBounds.max.x)
			{
				offset.x = this.m_ViewBounds.max.x - max.x;
			}
		}
		if (this.m_Vertical)
		{
			min.y += delta.y;
			max.y += delta.y;
			if (max.y < this.m_ViewBounds.max.y)
			{
				offset.y = this.m_ViewBounds.max.y - max.y;
			}
			else if (min.y > this.m_ViewBounds.min.y)
			{
				offset.y = this.m_ViewBounds.min.y - min.y;
			}
		}
		return offset;
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x0017E29A File Offset: 0x0017C49A
	protected void SetDirty()
	{
		if (!this.IsActive())
		{
			return;
		}
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x0017E2B0 File Offset: 0x0017C4B0
	protected void SetDirtyCaching()
	{
		if (!this.IsActive())
		{
			return;
		}
		CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x0017E2CC File Offset: 0x0017C4CC
	public bool isHorizontalScrollAvailable()
	{
		float width = this.content.rect.width;
		float tViewportWidth = this.viewport.rect.width;
		return width > tViewportWidth;
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x0017E304 File Offset: 0x0017C504
	public bool isVerticalScrollAvailable()
	{
		float height = this.content.rect.height;
		float tViewportHeight = this.viewport.rect.height;
		return height > tViewportHeight;
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x0017E33B File Offset: 0x0017C53B
	public bool isDragged()
	{
		return this.m_Dragging;
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x0017E344 File Offset: 0x0017C544
	public static bool isAnyDragged()
	{
		for (int i = 0; i < ScrollRectExtended.instances.Count; i++)
		{
			if (ScrollRectExtended.instances[i].isDragged())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x0017E37C File Offset: 0x0017C57C
	public static void SendMessageToAll(string pMessage, PointerEventData pEventData)
	{
		for (int i = 0; i < ScrollRectExtended.instances.Count; i++)
		{
			ScrollRectExtended.instances[i].SendMessage(pMessage, pEventData);
		}
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x0017E3BC File Offset: 0x0017C5BC
	Transform ICanvasElement.get_transform()
	{
		return base.transform;
	}

	// Token: 0x040025D6 RID: 9686
	[SerializeField]
	private RectTransform m_Content;

	// Token: 0x040025D7 RID: 9687
	[SerializeField]
	private bool m_Horizontal = true;

	// Token: 0x040025D8 RID: 9688
	[SerializeField]
	private bool m_Vertical = true;

	// Token: 0x040025D9 RID: 9689
	[SerializeField]
	private ScrollRectExtended.MovementType m_MovementType = ScrollRectExtended.MovementType.Elastic;

	// Token: 0x040025DA RID: 9690
	[SerializeField]
	private float m_Elasticity = 0.1f;

	// Token: 0x040025DB RID: 9691
	[SerializeField]
	private bool m_Inertia = true;

	// Token: 0x040025DC RID: 9692
	[SerializeField]
	private float m_DecelerationRate = 0.135f;

	// Token: 0x040025DD RID: 9693
	[SerializeField]
	private float m_ScrollSensitivity = 1f;

	// Token: 0x040025DE RID: 9694
	[SerializeField]
	private RectTransform m_Viewport;

	// Token: 0x040025DF RID: 9695
	[SerializeField]
	private float m_ScrollFactor = 1f;

	// Token: 0x040025E0 RID: 9696
	public float velocityMod = 1f;

	// Token: 0x040025E1 RID: 9697
	private Scrollbar m_HorizontalScrollbar;

	// Token: 0x040025E2 RID: 9698
	[SerializeField]
	private Scrollbar m_VerticalScrollbar;

	// Token: 0x040025E3 RID: 9699
	[SerializeField]
	private ScrollRectExtended.ScrollbarVisibility m_HorizontalScrollbarVisibility;

	// Token: 0x040025E4 RID: 9700
	[SerializeField]
	private ScrollRectExtended.ScrollbarVisibility m_VerticalScrollbarVisibility;

	// Token: 0x040025E5 RID: 9701
	[SerializeField]
	private float m_HorizontalScrollbarSpacing;

	// Token: 0x040025E6 RID: 9702
	[SerializeField]
	private float m_VerticalScrollbarSpacing;

	// Token: 0x040025E7 RID: 9703
	[SerializeField]
	private ScrollRectExtended.ScrollRectEvent m_OnValueChanged = new ScrollRectExtended.ScrollRectEvent();

	// Token: 0x040025E8 RID: 9704
	private Vector2 m_PointerStartLocalCursor = Vector2.zero;

	// Token: 0x040025E9 RID: 9705
	private Vector2 m_ContentStartPosition = Vector2.zero;

	// Token: 0x040025EA RID: 9706
	private RectTransform m_ViewRect;

	// Token: 0x040025EB RID: 9707
	private Bounds m_ContentBounds;

	// Token: 0x040025EC RID: 9708
	private Bounds m_ViewBounds;

	// Token: 0x040025ED RID: 9709
	private Vector2 m_Velocity;

	// Token: 0x040025EE RID: 9710
	private bool m_Dragging;

	// Token: 0x040025EF RID: 9711
	private Vector2 m_PrevPosition = Vector2.zero;

	// Token: 0x040025F0 RID: 9712
	private Bounds m_PrevContentBounds;

	// Token: 0x040025F1 RID: 9713
	private Bounds m_PrevViewBounds;

	// Token: 0x040025F2 RID: 9714
	[NonSerialized]
	private bool m_HasRebuiltLayout;

	// Token: 0x040025F3 RID: 9715
	private bool m_HSliderExpand;

	// Token: 0x040025F4 RID: 9716
	private bool m_VSliderExpand;

	// Token: 0x040025F5 RID: 9717
	private float m_HSliderHeight;

	// Token: 0x040025F6 RID: 9718
	private float m_VSliderWidth;

	// Token: 0x040025F7 RID: 9719
	[NonSerialized]
	private RectTransform m_Rect;

	// Token: 0x040025F8 RID: 9720
	private RectTransform m_HorizontalScrollbarRect;

	// Token: 0x040025F9 RID: 9721
	private RectTransform m_VerticalScrollbarRect;

	// Token: 0x040025FA RID: 9722
	private DrivenRectTransformTracker m_Tracker;

	// Token: 0x040025FB RID: 9723
	public static List<ScrollRectExtended> instances = new List<ScrollRectExtended>();

	// Token: 0x040025FC RID: 9724
	private float _check_timer = 0.05f;

	// Token: 0x040025FD RID: 9725
	private float _check_timer_interval = 0.05f;

	// Token: 0x040025FF RID: 9727
	private readonly Vector3[] m_Corners = new Vector3[4];

	// Token: 0x02000A99 RID: 2713
	public enum MovementType
	{
		// Token: 0x04003AA3 RID: 15011
		Unrestricted,
		// Token: 0x04003AA4 RID: 15012
		Elastic,
		// Token: 0x04003AA5 RID: 15013
		Clamped
	}

	// Token: 0x02000A9A RID: 2714
	public enum ScrollbarVisibility
	{
		// Token: 0x04003AA7 RID: 15015
		Permanent,
		// Token: 0x04003AA8 RID: 15016
		AutoHide,
		// Token: 0x04003AA9 RID: 15017
		AutoHideAndExpandViewport
	}

	// Token: 0x02000A9B RID: 2715
	[Serializable]
	public class ScrollRectEvent : UnityEvent<Vector2>
	{
	}
}
