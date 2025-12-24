using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020006B9 RID: 1721
[RequireComponent(typeof(ScrollableButton))]
public class GraphCompareMetaSelector : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDraggable
{
	// Token: 0x1700030E RID: 782
	// (get) Token: 0x060036FD RID: 14077 RVA: 0x0018DD67 File Offset: 0x0018BF67
	public bool spawn_particles_on_drag
	{
		get
		{
			return this._spawn_particles_on_drag;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x060036FE RID: 14078 RVA: 0x0018DD6F File Offset: 0x0018BF6F
	private Transform _attach_parent
	{
		get
		{
			return World.world.drag_parent;
		}
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x0018DD7B File Offset: 0x0018BF7B
	private void Awake()
	{
		this._scrollable_button = base.GetComponent<ScrollableButton>();
		this._start_parent = base.transform.parent;
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.showTooltip));
	}

	// Token: 0x06003700 RID: 14080 RVA: 0x0018DDB8 File Offset: 0x0018BFB8
	private void showTooltip()
	{
		IBanner tBanner = base.GetComponent<IBanner>();
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(tBanner))
		{
			tBanner.showTooltip();
		}
	}

	// Token: 0x06003701 RID: 14081 RVA: 0x0018DDE1 File Offset: 0x0018BFE1
	public void addWindow(GraphCompareWindow pWindow)
	{
		this._window = pWindow;
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x0018DDEA File Offset: 0x0018BFEA
	public void addDropzones(params RectTransform[] pDropzones)
	{
		this._dropzones.Clear();
		this._dropzones.AddRange(pDropzones);
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x0018DE03 File Offset: 0x0018C003
	public bool isBeingDragged()
	{
		return this._dragging;
	}

	// Token: 0x06003704 RID: 14084 RVA: 0x0018DE0B File Offset: 0x0018C00B
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		this._dragging = false;
		this._first_position = pEventData.position;
		this._start_parent = base.transform.parent;
		this._start_local_position = base.transform.localPosition;
	}

	// Token: 0x06003705 RID: 14085 RVA: 0x0018DE44 File Offset: 0x0018C044
	public bool checkIfDragging(PointerEventData pEventData)
	{
		if (this._window.countNoosItems() < 5)
		{
			return true;
		}
		Vector2 tMaxLowerLeft = new Vector2(float.MaxValue, 0f);
		Vector2 tMaxLowerRight = new Vector2(float.MinValue, 0f);
		foreach (RectTransform tDropzone in this._dropzones)
		{
			Vector2 tLowerLeft = tDropzone.position;
			tLowerLeft.x -= tDropzone.rect.width * tDropzone.lossyScale.x / 2f;
			tLowerLeft.y -= tDropzone.rect.height * tDropzone.lossyScale.y / 2f;
			Vector2 tLowerRight = tDropzone.position;
			tLowerRight.x += tDropzone.rect.width * tDropzone.lossyScale.x / 2f;
			tLowerRight.y -= tDropzone.rect.height * tDropzone.lossyScale.y / 2f;
			if (tLowerLeft.x < tMaxLowerLeft.x)
			{
				tMaxLowerLeft = tLowerLeft;
			}
			if (tLowerRight.x > tMaxLowerRight.x)
			{
				tMaxLowerRight = tLowerRight;
			}
		}
		if (!Toolbox.isInTriangle(pEventData.position, this._first_position, tMaxLowerLeft, tMaxLowerRight))
		{
			Vector2 tDirection = pEventData.position - this._first_position;
			if (Mathf.Abs(tDirection.x) > Mathf.Abs(tDirection.y))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003706 RID: 14086 RVA: 0x0018E008 File Offset: 0x0018C208
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (Config.isDraggingItem())
		{
			return;
		}
		if (this._dragging)
		{
			return;
		}
		this._dragging = this.checkIfDragging(pEventData);
		if (!this._dragging)
		{
			return;
		}
		Config.setDraggingObject(this);
		pEventData.Use();
		this._scrollable_button.enabled = false;
		GraphCompareMetaObject.disable_raycasts = true;
		base.transform.SetParent(this._attach_parent);
		base.transform.position = pEventData.position;
		this.disableRaycast();
	}

	// Token: 0x06003707 RID: 14087 RVA: 0x0018E087 File Offset: 0x0018C287
	public void OnDrag(PointerEventData pEventData)
	{
		if (!this._dragging)
		{
			return;
		}
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		pEventData.Use();
		base.transform.position = pEventData.position;
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x0018E0B8 File Offset: 0x0018C2B8
	public void OnEndDrag(PointerEventData pEventData)
	{
		this._scrollable_button.OnEndDrag(pEventData);
		if (!this._dragging)
		{
			return;
		}
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		pEventData.Use();
		base.transform.SetParent(this._start_parent);
		base.transform.localPosition = this._start_local_position;
		this.resetDrag();
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x0018E114 File Offset: 0x0018C314
	public void resetDrag()
	{
		if (!this._dragging)
		{
			return;
		}
		Config.clearDraggingObject();
		this._dragging = false;
		this._scrollable_button.enabled = true;
		GraphCompareMetaObject.disable_raycasts = false;
		foreach (Graphic graphic in this._raycastables)
		{
			graphic.raycastTarget = true;
		}
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x0018E18C File Offset: 0x0018C38C
	private void disableRaycast()
	{
		this._raycastables.Clear();
		foreach (Graphic tGraphic in base.GetComponentsInChildren<Graphic>())
		{
			if (tGraphic.raycastTarget)
			{
				this._raycastables.Add(tGraphic);
			}
		}
		foreach (Graphic graphic in this._raycastables)
		{
			graphic.raycastTarget = false;
		}
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x0018E218 File Offset: 0x0018C418
	private void OnDisable()
	{
		this.resetDrag();
	}

	// Token: 0x0600370C RID: 14092 RVA: 0x0018E220 File Offset: 0x0018C420
	public void KillDrag()
	{
		this.OnDisable();
	}

	// Token: 0x0600370E RID: 14094 RVA: 0x0018E258 File Offset: 0x0018C458
	Transform IDraggable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x040028C6 RID: 10438
	[SerializeField]
	private bool _spawn_particles_on_drag = true;

	// Token: 0x040028C7 RID: 10439
	private Vector3 _start_local_position;

	// Token: 0x040028C8 RID: 10440
	private Transform _start_parent;

	// Token: 0x040028C9 RID: 10441
	private ScrollableButton _scrollable_button;

	// Token: 0x040028CA RID: 10442
	private readonly List<Graphic> _raycastables = new List<Graphic>();

	// Token: 0x040028CB RID: 10443
	private Vector2 _first_position = Vector2.zero;

	// Token: 0x040028CC RID: 10444
	private bool _dragging;

	// Token: 0x040028CD RID: 10445
	private readonly List<RectTransform> _dropzones = new List<RectTransform>();

	// Token: 0x040028CE RID: 10446
	private GraphCompareWindow _window;
}
