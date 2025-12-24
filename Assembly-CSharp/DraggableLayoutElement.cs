using System;
using System.Collections.Generic;
using LayoutGroupExt;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000567 RID: 1383
[RequireComponent(typeof(CanvasGroup))]
public class DraggableLayoutElement : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, ILayoutIgnorer, IDraggable
{
	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06002D08 RID: 11528 RVA: 0x0015FFD7 File Offset: 0x0015E1D7
	public bool spawn_particles_on_drag
	{
		get
		{
			return this._spawn_particles_on_drag;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06002D09 RID: 11529 RVA: 0x0015FFDF File Offset: 0x0015E1DF
	// (set) Token: 0x06002D0A RID: 11530 RVA: 0x0015FFE7 File Offset: 0x0015E1E7
	public bool ignoreLayout { get; set; }

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06002D0B RID: 11531 RVA: 0x0015FFF0 File Offset: 0x0015E1F0
	private List<RectTransform> _siblings
	{
		get
		{
			return this._parent_layout.m_Children;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0015FFFD File Offset: 0x0015E1FD
	private Vector2[] _sibling_positions
	{
		get
		{
			return this._parent_layout.m_Positions;
		}
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x0016000C File Offset: 0x0015E20C
	private void Start()
	{
		this._rect = base.GetComponent<RectTransform>();
		this._canvas_group = base.GetComponent<CanvasGroup>();
		this._parent = base.transform.parent.GetComponent<RectTransform>();
		this._parent_layout = this._parent.GetComponent<LayoutGroupExtended>();
		if (this._parent_layout == null)
		{
			return;
		}
		this.addToggleComponent<ScrollableButton>();
		this.addToggleComponent<Button>();
		this.addToggleComponent<TipButton>();
		if (this._attach_parent == null)
		{
			this._attach_parent = World.world.drag_parent;
		}
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x00160097 File Offset: 0x0015E297
	private void OnEnable()
	{
		this._cached_parent_position = new Vector3(-1000f, -1000f, -1000f);
		this._target_index = -1;
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x001600BA File Offset: 0x0015E2BA
	public void KillDrag()
	{
		this.OnDisable();
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x001600C2 File Offset: 0x0015E2C2
	private void OnDisable()
	{
		if (!DraggableLayoutElement._any_dragging)
		{
			return;
		}
		if (this._drag_object == null)
		{
			return;
		}
		this.OnEndDrag(new PointerEventData(EventSystem.current));
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x001600EB File Offset: 0x0015E2EB
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		if (!this._touch_drag_delay)
		{
			return;
		}
		this._drag_timer_started_at = Time.time;
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x00160104 File Offset: 0x0015E304
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (Config.isDraggingItem())
		{
			return;
		}
		if (this.isTouchDragDelayed())
		{
			return;
		}
		if (DraggableLayoutElement._any_dragging)
		{
			return;
		}
		DraggableLayoutElement._any_dragging = true;
		this._drag_object = Object.Instantiate<DraggableLayoutElement>(this, this._attach_parent, true);
		this._drag_object.transform.position = pEventData.position;
		this._drag_object.ignoreLayout = true;
		Action<DraggableLayoutElement> action = this._drag_object.start_being_dragged;
		if (action != null)
		{
			action(this);
		}
		this._canvas_group.alpha = 0.2f;
		Config.setDraggingObject(this._drag_object);
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x0016019C File Offset: 0x0015E39C
	public void OnDrag(PointerEventData pEventData)
	{
		if (this.isTouchDragDelayed())
		{
			return;
		}
		if (!DraggableLayoutElement._any_dragging)
		{
			return;
		}
		if (!Config.isDraggingObject(this._drag_object))
		{
			return;
		}
		this._drag_object.transform.position = pEventData.position;
		if (!this.isOverParent(pEventData.position))
		{
			return;
		}
		this.findTarget();
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x001601F8 File Offset: 0x0015E3F8
	public void OnEndDrag(PointerEventData pEventData)
	{
		ScrollRectExtended.SendMessageToAll("OnEndDrag", pEventData);
		if (!DraggableLayoutElement._any_dragging)
		{
			return;
		}
		if (!Config.isDraggingObject(this._drag_object))
		{
			return;
		}
		Config.clearDraggingObject();
		DraggableLayoutElement._any_dragging = false;
		this._drag_timer_started_at = 0f;
		Object.Destroy(this._drag_object.gameObject);
		this._canvas_group.alpha = 1f;
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x0016025C File Offset: 0x0015E45C
	public void Update()
	{
		bool? dragging_cache = this._dragging_cache;
		bool any_dragging = DraggableLayoutElement._any_dragging;
		if (!(dragging_cache.GetValueOrDefault() == any_dragging & dragging_cache != null))
		{
			this._dragging_cache = new bool?(DraggableLayoutElement._any_dragging);
			this._canvas_group.interactable = !DraggableLayoutElement._any_dragging;
			this._canvas_group.blocksRaycasts = !DraggableLayoutElement._any_dragging;
			foreach (MonoBehaviour tToggleElement in this._toggle_elements)
			{
				if (tToggleElement is Selectable)
				{
					(tToggleElement as Selectable).interactable = !DraggableLayoutElement._any_dragging;
				}
				else
				{
					tToggleElement.enabled = !DraggableLayoutElement._any_dragging;
				}
			}
		}
		this.moveToTarget();
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x00160334 File Offset: 0x0015E534
	internal void lockToParent(bool pLock = true)
	{
		this._drag_only_over_parent = pLock;
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x0016033D File Offset: 0x0015E53D
	internal void setDragParent(Transform pParent)
	{
		this._attach_parent = pParent;
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x00160348 File Offset: 0x0015E548
	private void moveToTarget()
	{
		if (this._target_index < 0)
		{
			return;
		}
		int tMyIndex = this._siblings.IndexOf(this._rect);
		int tTargetIndex = this._target_index;
		using (ListPool<int> tNeighbours = this.getNeighbours(tMyIndex))
		{
			if (!tNeighbours.Contains(tTargetIndex))
			{
				tTargetIndex = this.findClosestNeighbour(tTargetIndex, tNeighbours);
			}
			this.swapSiblings(tMyIndex, tTargetIndex);
			if (tTargetIndex == this._target_index)
			{
				this._target_index = -1;
			}
		}
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x001603C8 File Offset: 0x0015E5C8
	private void recalcParent()
	{
		if (this._cached_parent_position == this._parent.position)
		{
			return;
		}
		this._cached_parent_position = this._parent.position;
		this._cached_parent_rect = this._parent.GetWorldRect();
		float tMarginWidth = this._rect.rect.width * 10f;
		float tMarginHeight = this._rect.rect.height * 10f;
		this._cached_parent_rect.x = this._cached_parent_rect.x - tMarginWidth;
		this._cached_parent_rect.y = this._cached_parent_rect.y - tMarginHeight;
		this._cached_parent_rect.width = this._cached_parent_rect.width + tMarginWidth * 2f;
		this._cached_parent_rect.height = this._cached_parent_rect.height + tMarginHeight * 2f;
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x0016049C File Offset: 0x0015E69C
	private void findTarget()
	{
		if (this._drag_object == null)
		{
			return;
		}
		Vector3 tPosition = this._drag_object.transform.position;
		float tClosest = float.MaxValue;
		float tMyDist = float.MaxValue;
		int tTargetIndex = -1;
		int tMyIndex = -1;
		for (int i = 0; i < this._sibling_positions.Length; i++)
		{
			float tDistance = Vector2.Distance(this._sibling_positions[i], tPosition);
			if (this._siblings[i] == this._rect)
			{
				tMyIndex = i;
				tMyDist = tDistance;
			}
			if (tDistance < tClosest)
			{
				tClosest = tDistance;
				tTargetIndex = i;
			}
		}
		if (tTargetIndex == tMyIndex || Mathf.Approximately(tMyDist, tClosest))
		{
			return;
		}
		this._target_index = tTargetIndex;
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x0016054D File Offset: 0x0015E74D
	private bool isOverParent(Vector2 pPosition)
	{
		this.recalcParent();
		return this._cached_parent_rect.Contains(pPosition);
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x00160568 File Offset: 0x0015E768
	private void swapSiblings(int pStartIndex, int pTargetIndex)
	{
		if (pStartIndex >= this._siblings.Count)
		{
			return;
		}
		if (pTargetIndex >= this._siblings.Count)
		{
			return;
		}
		int pStartSiblingIndex = this._siblings[pStartIndex].transform.GetSiblingIndex();
		int pTargetSiblingIndex = this._siblings[pTargetIndex].transform.GetSiblingIndex();
		if (pStartSiblingIndex > pTargetSiblingIndex)
		{
			this._siblings[pTargetIndex].transform.SetSiblingIndex(pStartSiblingIndex);
			base.transform.SetSiblingIndex(pTargetSiblingIndex);
		}
		else
		{
			base.transform.SetSiblingIndex(pTargetSiblingIndex);
			this._siblings[pTargetIndex].transform.SetSiblingIndex(pStartSiblingIndex);
		}
		this._siblings.Swap(pStartIndex, pTargetIndex);
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x0016061C File Offset: 0x0015E81C
	private int findClosestNeighbour(int pIndex, ListPool<int> pNeighbours)
	{
		int tClosestIndex = pIndex;
		Vector2 tTargetPosition = this._sibling_positions[pIndex];
		float tClosestNeighbour = float.MaxValue;
		foreach (int ptr in pNeighbours)
		{
			int tNeighbourIndex = ptr;
			float tDistance = Vector2.Distance(this._sibling_positions[tNeighbourIndex], tTargetPosition);
			if (tDistance < tClosestNeighbour)
			{
				tClosestNeighbour = tDistance;
				tClosestIndex = tNeighbourIndex;
			}
		}
		return tClosestIndex;
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x0016069C File Offset: 0x0015E89C
	private ListPool<int> getNeighbours(int pIndex)
	{
		ListPool<int> tNeighbours = new ListPool<int>(8);
		if (this._sibling_positions.Length < 2)
		{
			return tNeighbours;
		}
		Vector2 tMyPosition = this._sibling_positions[pIndex];
		float tDistance = Vector2.Distance(this._sibling_positions[0], this._sibling_positions[1]) * 1.5f;
		for (int i = 0; i < this._sibling_positions.Length; i++)
		{
			if (i != pIndex && Vector2.Distance(tMyPosition, this._sibling_positions[i]) <= tDistance)
			{
				tNeighbours.Add(i);
			}
		}
		return tNeighbours;
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x00160722 File Offset: 0x0015E922
	private void addToggleComponent<T>() where T : MonoBehaviour
	{
		if (this.HasComponent<T>())
		{
			this._toggle_elements.Add(base.GetComponent<T>());
		}
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x00160742 File Offset: 0x0015E942
	private bool isTouchDragDelayed()
	{
		return this._touch_drag_delay && !InputHelpers.mouseSupported && Time.time - this._drag_timer_started_at < 0.2f;
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x00160791 File Offset: 0x0015E991
	Transform IDraggable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x0400228B RID: 8843
	public const float TOUCH_DELAY = 0.2f;

	// Token: 0x0400228C RID: 8844
	[SerializeField]
	private bool _spawn_particles_on_drag = true;

	// Token: 0x0400228D RID: 8845
	private RectTransform _rect;

	// Token: 0x0400228E RID: 8846
	private CanvasGroup _canvas_group;

	// Token: 0x0400228F RID: 8847
	private LayoutGroupExtended _parent_layout;

	// Token: 0x04002290 RID: 8848
	private RectTransform _parent;

	// Token: 0x04002291 RID: 8849
	private Rect _cached_parent_rect;

	// Token: 0x04002292 RID: 8850
	private Vector3 _cached_parent_position;

	// Token: 0x04002294 RID: 8852
	[SerializeField]
	private Transform _attach_parent;

	// Token: 0x04002295 RID: 8853
	[SerializeField]
	private bool _touch_drag_delay;

	// Token: 0x04002296 RID: 8854
	private DraggableLayoutElement _drag_object;

	// Token: 0x04002297 RID: 8855
	private int _target_index = -1;

	// Token: 0x04002298 RID: 8856
	private List<MonoBehaviour> _toggle_elements = new List<MonoBehaviour>(3);

	// Token: 0x04002299 RID: 8857
	private static bool _any_dragging;

	// Token: 0x0400229A RID: 8858
	private bool? _dragging_cache;

	// Token: 0x0400229B RID: 8859
	[SerializeField]
	private bool _drag_only_over_parent = true;

	// Token: 0x0400229C RID: 8860
	internal Action<DraggableLayoutElement> start_being_dragged;

	// Token: 0x0400229D RID: 8861
	private float _drag_timer_started_at;
}
