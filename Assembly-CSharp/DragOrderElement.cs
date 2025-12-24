using System;
using LayoutGroupExt;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000565 RID: 1381
public class DragOrderElement : MonoBehaviour, IDraggable, IEndDragHandler, IEventSystemHandler
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x0015F181 File Offset: 0x0015D381
	public bool spawn_particles_on_drag
	{
		get
		{
			return this._spawn_particles_on_drag;
		}
	}

	// Token: 0x06002CD8 RID: 11480 RVA: 0x0015F18C File Offset: 0x0015D38C
	private void Start()
	{
		if (this.main_transform == null)
		{
			this.main_transform = base.GetComponent<RectTransform>();
		}
		this._parent_canvas_sorting_order = this.main_transform.gameObject.GetComponentInParent<Canvas>().sortingOrder;
		this._canvas = this.main_transform.gameObject.AddComponent<Canvas>();
		this._canvas.sortingOrder = this._parent_canvas_sorting_order;
		this._canvas.overrideSorting = false;
		this._raycaster = this.main_transform.gameObject.AddComponent<GraphicRaycaster>();
		this._raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.All;
		this._raycaster.blockingMask = -1;
		this._raycaster.ignoreReversedGraphics = true;
		this._button = base.GetComponent<Button>();
		this._button.onClick.AddListener(delegate()
		{
			DragOrderContainer container = this._container;
			if (container == null)
			{
				return;
			}
			container.updateChildrenData();
		});
		this.is_target_reached = true;
		this.checkContainer();
	}

	// Token: 0x06002CD9 RID: 11481 RVA: 0x0015F275 File Offset: 0x0015D475
	private void checkContainer()
	{
		if (this._container != null)
		{
			return;
		}
		this._container = this.main_transform.GetComponentInParent<DragOrderContainer>();
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x0015F298 File Offset: 0x0015D498
	private void checkParent()
	{
		Transform tNewParent = this.main_transform.parent;
		if (this._current_parent == tNewParent)
		{
			return;
		}
		this._current_parent = tNewParent;
		this.checkContainer();
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x0015F2D0 File Offset: 0x0015D4D0
	private void Update()
	{
		if (!base.enabled)
		{
			return;
		}
		this.checkParent();
		if (this._container == null)
		{
			return;
		}
		this.checkDrag();
		if (this._container.grid_layout.enabled)
		{
			return;
		}
		if (this._container.dragging_element == this)
		{
			this.moveDraggingTab();
			return;
		}
		if (this.is_target_reached)
		{
			return;
		}
		if (Vector3.Distance(this.main_transform.localPosition, this.current_destination) < 0.1f)
		{
			this.is_target_reached = true;
			this.unsetOnTop();
			return;
		}
		this.main_transform.localPosition = Vector3.Lerp(this.main_transform.localPosition, this.current_destination, Time.deltaTime * 10f);
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x0015F398 File Offset: 0x0015D598
	private void setOnTop()
	{
		this._canvas.overrideSorting = true;
		this._canvas.sortingOrder = 24;
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x0015F3B3 File Offset: 0x0015D5B3
	internal void unsetOnTop()
	{
		if (!this._canvas.overrideSorting)
		{
			return;
		}
		this._canvas.sortingOrder = this._parent_canvas_sorting_order;
		this._canvas.overrideSorting = false;
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x0015F3E0 File Offset: 0x0015D5E0
	public void updatePosition()
	{
		Vector2 tDestination = this.getChildPositionInContainer();
		if (this.main_transform.localPosition == tDestination)
		{
			return;
		}
		if (this.current_destination == tDestination)
		{
			return;
		}
		this.current_destination = tDestination;
		this.is_target_reached = false;
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x0015F430 File Offset: 0x0015D630
	private void moveDraggingTab()
	{
		if (!this._container.is_anything_dragging)
		{
			this.endDrag();
			return;
		}
		if (!InputHelpers.GetMouseButton(this._mouse_button))
		{
			this.endDrag();
			return;
		}
		Vector3 tNewPosition = Input.mousePosition;
		DragOrderContainer.SnapAxis snapping_axis = this._container.snapping_axis;
		if (snapping_axis != DragOrderContainer.SnapAxis.Horizontal)
		{
			if (snapping_axis == DragOrderContainer.SnapAxis.Vertical)
			{
				tNewPosition.x = this.main_transform.position.x;
			}
		}
		else
		{
			tNewPosition.y = this.main_transform.position.y;
		}
		if (!this._container.limit_moving)
		{
			this.main_transform.position = tNewPosition;
			return;
		}
		Rect tContainerRect = this._container.rect_transform.GetWorldRect();
		Vector2 tCellSize;
		Vector2 vector;
		this.getGridValues(this._container.grid_layout, out tCellSize, out vector);
		tCellSize *= 0.5f;
		tNewPosition.x = Mathf.Min(tNewPosition.x, tContainerRect.xMax - tCellSize.x);
		tNewPosition.x = Mathf.Max(tNewPosition.x, tContainerRect.xMin + tCellSize.x);
		tNewPosition.y = Mathf.Min(tNewPosition.y, tContainerRect.yMax - tCellSize.y);
		tNewPosition.y = Mathf.Max(tNewPosition.y, tContainerRect.yMin + tCellSize.y);
		this.main_transform.position = tNewPosition;
	}

	// Token: 0x06002CE0 RID: 11488 RVA: 0x0015F588 File Offset: 0x0015D788
	private void checkDrag()
	{
		this.checkDragBegin();
		this.checkDragEnd();
	}

	// Token: 0x06002CE1 RID: 11489 RVA: 0x0015F598 File Offset: 0x0015D798
	private void checkDragBegin()
	{
		if (Config.isDraggingItem())
		{
			return;
		}
		if (this._container.is_anything_dragging)
		{
			return;
		}
		if (!this.can_be_dragged)
		{
			return;
		}
		if (this._container.delay_before_drag && !this.isMouseOver())
		{
			this._drag_initialized = false;
			return;
		}
		if (InputHelpers.GetAnyMouseButtonDown() && this.isMouseOver())
		{
			this._mouse_button = InputHelpers.GetAnyMouseButtonDownIndex();
			this._drag_started_at = Time.time;
			if (!this._container.delay_before_drag)
			{
				this._drag_started_at -= DragOrderContainer.drag_delay;
				this._prev_mouse_position = Input.mousePosition;
			}
			this._drag_initialized = true;
		}
		if (!this._drag_initialized)
		{
			return;
		}
		if (!InputHelpers.GetMouseButton(this._mouse_button))
		{
			this._drag_initialized = false;
			return;
		}
		if (!this._container.delay_before_drag && !DragOrderElement.shouldStartDrag(Input.mousePosition, this._prev_mouse_position))
		{
			return;
		}
		if (Time.time - this._drag_started_at < DragOrderContainer.drag_delay)
		{
			return;
		}
		this.startDrag();
	}

	// Token: 0x06002CE2 RID: 11490 RVA: 0x0015F699 File Offset: 0x0015D899
	private void checkDragEnd()
	{
		if (!InputHelpers.GetMouseButtonUp(this._mouse_button))
		{
			return;
		}
		if (!this._container.is_anything_dragging)
		{
			return;
		}
		this._drag_initialized = false;
		this.endDrag();
	}

	// Token: 0x06002CE3 RID: 11491 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
	public void OnEndDrag(PointerEventData pData)
	{
		if (!this._container.is_anything_dragging)
		{
			return;
		}
		this._drag_initialized = false;
		this.endDrag();
	}

	// Token: 0x06002CE4 RID: 11492 RVA: 0x0015F6E4 File Offset: 0x0015D8E4
	private void startDrag()
	{
		this._drag_started_at = Time.realtimeSinceStartup;
		this._container.dragging_element = this;
		Config.setDraggingObject(this);
		this._container.is_anything_dragging = true;
		this._container.grid_layout.enabled = false;
		this._container.layout_element.enabled = true;
		this._button.interactable = false;
		if (this._container.scroll_rect != null)
		{
			this._container.scroll_rect.enabled = false;
		}
		this._container.updateChildrenData();
		this.setOnTop();
	}

	// Token: 0x06002CE5 RID: 11493 RVA: 0x0015F77D File Offset: 0x0015D97D
	public void stopDrag()
	{
		this.endDrag();
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x0015F788 File Offset: 0x0015D988
	private void endDrag()
	{
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		this._button.interactable = true;
		this._mouse_button = -1;
		if (this._container.scroll_rect != null)
		{
			this._container.scroll_rect.enabled = true;
		}
		Vector3 tTo = this.getChildPositionInContainer();
		this.current_destination = tTo;
		this.is_target_reached = false;
		if (this._container.dragging_element != this)
		{
			return;
		}
		this._container.layout_element.enabled = false;
		this._drag_initialized = false;
		this._container.dragging_element = null;
		Config.clearDraggingObject();
		this._container.is_anything_dragging = false;
	}

	// Token: 0x06002CE7 RID: 11495 RVA: 0x0015F838 File Offset: 0x0015DA38
	private Vector3 getChildPositionInContainer()
	{
		return this._container.getChildPosition(this.order_index);
	}

	// Token: 0x06002CE8 RID: 11496 RVA: 0x0015F84C File Offset: 0x0015DA4C
	private bool isMouseOver()
	{
		Vector2 tPoint = this._container.rect_transform.InverseTransformPoint(Input.mousePosition);
		return this.getRect().Contains(tPoint);
	}

	// Token: 0x06002CE9 RID: 11497 RVA: 0x0015F884 File Offset: 0x0015DA84
	public Rect getRect()
	{
		Vector2 tCellSize;
		Vector2 tSpacing;
		this.getGridValues(this._container.grid_layout, out tCellSize, out tSpacing);
		Vector2 position = this.main_transform.localPosition - tCellSize * this.main_transform.pivot - tSpacing / 2f;
		Vector2 tRectSize = tCellSize + tSpacing;
		return new Rect(position, tRectSize);
	}

	// Token: 0x06002CEA RID: 11498 RVA: 0x0015F8EC File Offset: 0x0015DAEC
	private void getGridValues(LayoutGroup pLayoutGroup, out Vector2 pCellSize, out Vector2 pSpacing)
	{
		GridLayoutGroup tGrid = pLayoutGroup as GridLayoutGroup;
		if (tGrid != null)
		{
			pCellSize = tGrid.cellSize;
			pSpacing = tGrid.spacing;
			return;
		}
		GridLayoutGroupExtended tGrid2 = pLayoutGroup as GridLayoutGroupExtended;
		if (tGrid2 == null)
		{
			pCellSize = Vector2.zero;
			pSpacing = Vector2.zero;
			return;
		}
		pCellSize = tGrid2.cellSize;
		pSpacing = tGrid2.spacing;
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x0015F958 File Offset: 0x0015DB58
	private static bool shouldStartDrag(Vector2 pPressPos, Vector2 pCurrentPos)
	{
		float tThreshold = (float)EventSystem.current.pixelDragThreshold;
		return (pPressPos - pCurrentPos).sqrMagnitude >= tThreshold * tThreshold;
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x0015F988 File Offset: 0x0015DB88
	private void OnDisable()
	{
		DragOrderContainer container = this._container;
		if (container == null || !container.is_anything_dragging)
		{
			return;
		}
		this.OnEndDrag(new PointerEventData(EventSystem.current));
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x0015F9B2 File Offset: 0x0015DBB2
	public void KillDrag()
	{
		this.OnDisable();
	}

	// Token: 0x06002CEF RID: 11503 RVA: 0x0015F9D7 File Offset: 0x0015DBD7
	Transform IDraggable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04002265 RID: 8805
	[SerializeField]
	private bool _spawn_particles_on_drag = true;

	// Token: 0x04002266 RID: 8806
	public RectTransform main_transform;

	// Token: 0x04002267 RID: 8807
	public bool can_be_dragged = true;

	// Token: 0x04002268 RID: 8808
	private DragOrderContainer _container;

	// Token: 0x04002269 RID: 8809
	private int _parent_canvas_sorting_order;

	// Token: 0x0400226A RID: 8810
	private Canvas _canvas;

	// Token: 0x0400226B RID: 8811
	private GraphicRaycaster _raycaster;

	// Token: 0x0400226C RID: 8812
	private Button _button;

	// Token: 0x0400226D RID: 8813
	private Transform _current_parent;

	// Token: 0x0400226E RID: 8814
	internal Vector2 current_destination;

	// Token: 0x0400226F RID: 8815
	internal bool is_target_reached;

	// Token: 0x04002270 RID: 8816
	internal int order_index;

	// Token: 0x04002271 RID: 8817
	private bool _drag_initialized;

	// Token: 0x04002272 RID: 8818
	private float _drag_started_at;

	// Token: 0x04002273 RID: 8819
	private int _mouse_button = -1;

	// Token: 0x04002274 RID: 8820
	private Vector3 _prev_mouse_position;
}
