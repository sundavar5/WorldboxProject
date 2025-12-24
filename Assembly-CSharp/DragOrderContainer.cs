using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000564 RID: 1380
public class DragOrderContainer : MonoBehaviour
{
	// Token: 0x06002CC4 RID: 11460 RVA: 0x0015E904 File Offset: 0x0015CB04
	private void Awake()
	{
		if (this.scroll_rect == null)
		{
			this.scroll_rect = base.GetComponentInParent<ScrollRectExtended>();
		}
		if (this.scroll_rect == null)
		{
			this.scroll_rect = base.GetComponentInParent<ScrollRect>();
		}
		this.rect_transform = base.GetComponent<RectTransform>();
		this.grid_layout = base.GetComponent<LayoutGroup>();
		this.layout_element = base.gameObject.AddOrGetComponent<LayoutElement>();
		this.layout_element.enabled = false;
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x0015E97A File Offset: 0x0015CB7A
	private void markForUpdate()
	{
		this._marked_for_update = true;
		this._marked_for_update_on_frame = Time.frameCount;
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x0015E98E File Offset: 0x0015CB8E
	private void OnApplicationFocus(bool pHasFocus)
	{
		if (pHasFocus)
		{
			return;
		}
		this.disable();
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x0015E99A File Offset: 0x0015CB9A
	private void OnEnable()
	{
		this.markForUpdate();
		ScrollWindow.addCallbackShow(new ScrollWindowNameAction(this.onWindowClose));
		ScrollWindow.addCallbackHide(new ScrollWindowNameAction(this.onWindowClose));
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x0015E9C4 File Offset: 0x0015CBC4
	private void OnDisable()
	{
		this.disable();
		ScrollWindow.removeCallbackShow(new ScrollWindowNameAction(this.onWindowClose));
		ScrollWindow.removeCallbackHide(new ScrollWindowNameAction(this.onWindowClose));
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x0015E9EE File Offset: 0x0015CBEE
	private void onWindowClose(string pId)
	{
		this.disable();
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x0015E9F8 File Offset: 0x0015CBF8
	private void disable()
	{
		this.grid_layout.enabled = true;
		LayoutRebuilder.MarkLayoutForRebuild(this.rect_transform);
		if (this.dragging_element != null)
		{
			this.dragging_element.stopDrag();
		}
		foreach (DragOrderElement tElement in this._elements)
		{
			if (!tElement.is_target_reached)
			{
				tElement.is_target_reached = true;
				tElement.unsetOnTop();
			}
		}
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x0015EA8C File Offset: 0x0015CC8C
	private void Update()
	{
		if (this._marked_for_update && this._marked_for_update_on_frame != Time.frameCount)
		{
			this._marked_for_update = false;
			this.updateChildrenData();
		}
		this.checkIntersections();
		this.updatePositions();
	}

	// Token: 0x06002CCC RID: 11468 RVA: 0x0015EABC File Offset: 0x0015CCBC
	private void OnDrawGizmos()
	{
		if (!this.debug)
		{
			return;
		}
		foreach (Rect tLocalRect in this._children_rects.Values)
		{
			tLocalRect.min = this.rect_transform.TransformPoint(tLocalRect.min);
			tLocalRect.max = this.rect_transform.TransformPoint(tLocalRect.max);
			DragOrderContainer.drawRect(tLocalRect, Color.green);
		}
	}

	// Token: 0x06002CCD RID: 11469 RVA: 0x0015EB68 File Offset: 0x0015CD68
	private void checkIntersections()
	{
		if (!this.is_anything_dragging)
		{
			return;
		}
		DragOrderElement tElement = this.getIntersectedWith();
		if (tElement == null)
		{
			this._to_ignore_in_intersection = null;
			return;
		}
		if (tElement.main_transform == this._to_ignore_in_intersection)
		{
			return;
		}
		this._to_ignore_in_intersection = tElement.main_transform;
		this.switchElements(this.dragging_element, tElement);
		Action action = this.on_order_changed;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x0015EBD4 File Offset: 0x0015CDD4
	private DragOrderElement getIntersectedWith()
	{
		int tDraggingIndex = this.dragging_element.order_index;
		Vector2 tDraggingPosition = this.dragging_element.main_transform.localPosition;
		Debug.DrawLine(this.rect_transform.TransformPoint(this._children_rects[tDraggingIndex].center), this.rect_transform.TransformPoint(tDraggingPosition));
		if (this.snapping_axis != DragOrderContainer.SnapAxis.No)
		{
			int tFirstIndex = 0;
			int tLastIndex = this._elements.Count - 1;
			Rect tFirstChildRect = this._children_rects[tFirstIndex];
			Rect tLastChildRect = this._children_rects[tLastIndex];
			if (this.snapping_axis == DragOrderContainer.SnapAxis.Horizontal)
			{
				if (tDraggingPosition.x <= tFirstChildRect.xMax)
				{
					return this._elements_dict[tFirstIndex];
				}
				if (tDraggingPosition.x >= tLastChildRect.xMin)
				{
					return this._elements_dict[tLastIndex];
				}
			}
			if (this.snapping_axis == DragOrderContainer.SnapAxis.Vertical)
			{
				if (tDraggingPosition.y >= tFirstChildRect.yMax)
				{
					return this._elements_dict[tFirstIndex];
				}
				if (tDraggingPosition.y <= tLastChildRect.yMin)
				{
					return this._elements_dict[tLastIndex];
				}
			}
		}
		for (int tOrderIndex = 0; tOrderIndex < this._elements.Count; tOrderIndex++)
		{
			if (tOrderIndex != tDraggingIndex && this._children_rects[tOrderIndex].Contains(tDraggingPosition))
			{
				return this._elements_dict[tOrderIndex];
			}
		}
		return null;
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x0015ED44 File Offset: 0x0015CF44
	private void updatePositions()
	{
		if (this.grid_layout.enabled)
		{
			return;
		}
		bool tAnyAnimPlaying = false;
		foreach (DragOrderElement tElem in this._elements)
		{
			if (!(tElem == this.dragging_element))
			{
				tElem.updatePosition();
				if (!tElem.is_target_reached)
				{
					tAnyAnimPlaying = true;
				}
			}
		}
		if (!tAnyAnimPlaying && !this.is_anything_dragging)
		{
			this.grid_layout.enabled = true;
		}
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x0015EDD8 File Offset: 0x0015CFD8
	public void updateChildrenData()
	{
		this.layout_element.minHeight = this.rect_transform.rect.height;
		this.layout_element.minWidth = this.rect_transform.rect.width;
		this._elements.Clear();
		this._elements_dict.Clear();
		this._children_positions.Clear();
		this._children_rects.Clear();
		DragOrderElement[] tElems = this.rect_transform.GetComponentsInChildren<DragOrderElement>();
		int tOrderIndex = 0;
		foreach (DragOrderElement tElem in tElems)
		{
			Vector2 tPosition;
			if (tElem.is_target_reached || this._previous_elements_count != tElems.Length)
			{
				tPosition = tElem.main_transform.localPosition;
			}
			else
			{
				tPosition = tElem.current_destination;
			}
			tElem.order_index = tOrderIndex;
			this._elements.Add(tElem);
			this._elements_dict.Add(tOrderIndex, tElem);
			this._children_positions.Add(tOrderIndex, tPosition);
			Rect tRect = tElem.getRect();
			this._children_rects.Add(tOrderIndex, tRect);
			tElem.current_destination = tPosition;
			tElem.unsetOnTop();
			tOrderIndex++;
		}
		this._previous_elements_count = tElems.Length;
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x0015EF14 File Offset: 0x0015D114
	private void switchElements(DragOrderElement pFirst, DragOrderElement pSecond)
	{
		pFirst.main_transform.SetSiblingIndex(pSecond.main_transform.GetSiblingIndex());
		int tFirstIndex = pFirst.order_index;
		int tSecondIndex = pSecond.order_index;
		bool tIsAscending = tFirstIndex > tSecondIndex;
		pFirst.order_index = tSecondIndex;
		this._elements.Sort((DragOrderElement e1, DragOrderElement e2) => this.sort(e1, e2, tIsAscending));
		int tIndexToCompare = pFirst.order_index;
		foreach (DragOrderElement tElement in this._elements)
		{
			if (!(tElement == pFirst) && (!tIsAscending || tElement.order_index >= tIndexToCompare) && (tIsAscending || tElement.order_index <= tIndexToCompare) && tElement.order_index == tIndexToCompare)
			{
				tElement.order_index += (tIsAscending ? 1 : -1);
				tIndexToCompare = tElement.order_index;
			}
		}
		foreach (DragOrderElement tElement2 in this._elements)
		{
			this._elements_dict[tElement2.order_index] = tElement2;
		}
	}

	// Token: 0x06002CD2 RID: 11474 RVA: 0x0015F070 File Offset: 0x0015D270
	public Vector3 getChildPosition(int pIndex)
	{
		return this._children_positions[pIndex];
	}

	// Token: 0x06002CD3 RID: 11475 RVA: 0x0015F083 File Offset: 0x0015D283
	private int sort(DragOrderElement pFirst, DragOrderElement pSecond, bool pIsAscending)
	{
		return pFirst.order_index.CompareTo(pSecond.order_index) * (pIsAscending ? 1 : -1);
	}

	// Token: 0x06002CD4 RID: 11476 RVA: 0x0015F0A0 File Offset: 0x0015D2A0
	private static void drawRect(Rect pRect, Color pColor)
	{
		Vector3 tMin = pRect.min;
		Vector3 tMax = pRect.max;
		Debug.DrawLine(tMin, new Vector3(tMin.x, tMax.y), pColor);
		Debug.DrawLine(new Vector3(tMin.x, tMax.y), tMax, pColor);
		Debug.DrawLine(tMax, new Vector3(tMax.x, tMin.y), pColor);
		Debug.DrawLine(tMin, new Vector3(tMax.x, tMin.y), pColor);
	}

	// Token: 0x04002250 RID: 8784
	internal static float drag_delay = 0.25f;

	// Token: 0x04002251 RID: 8785
	public MonoBehaviour scroll_rect;

	// Token: 0x04002252 RID: 8786
	public DragOrderContainer.SnapAxis snapping_axis = DragOrderContainer.SnapAxis.No;

	// Token: 0x04002253 RID: 8787
	public bool limit_moving;

	// Token: 0x04002254 RID: 8788
	public bool delay_before_drag = true;

	// Token: 0x04002255 RID: 8789
	public bool debug;

	// Token: 0x04002256 RID: 8790
	public Action on_order_changed;

	// Token: 0x04002257 RID: 8791
	internal DragOrderElement dragging_element;

	// Token: 0x04002258 RID: 8792
	internal bool is_anything_dragging;

	// Token: 0x04002259 RID: 8793
	internal RectTransform rect_transform;

	// Token: 0x0400225A RID: 8794
	internal LayoutGroup grid_layout;

	// Token: 0x0400225B RID: 8795
	internal LayoutElement layout_element;

	// Token: 0x0400225C RID: 8796
	private List<DragOrderElement> _elements = new List<DragOrderElement>();

	// Token: 0x0400225D RID: 8797
	private Dictionary<int, DragOrderElement> _elements_dict = new Dictionary<int, DragOrderElement>();

	// Token: 0x0400225E RID: 8798
	private Dictionary<int, Vector2> _children_positions = new Dictionary<int, Vector2>();

	// Token: 0x0400225F RID: 8799
	private Dictionary<int, Rect> _children_rects = new Dictionary<int, Rect>();

	// Token: 0x04002260 RID: 8800
	private Transform _to_ignore_in_intersection;

	// Token: 0x04002261 RID: 8801
	private int _previous_elements_count;

	// Token: 0x04002262 RID: 8802
	private bool _marked_for_update;

	// Token: 0x04002263 RID: 8803
	private int _marked_for_update_on_frame;

	// Token: 0x04002264 RID: 8804
	private bool _initialized;

	// Token: 0x02000A73 RID: 2675
	public enum SnapAxis
	{
		// Token: 0x04003931 RID: 14641
		Horizontal,
		// Token: 0x04003932 RID: 14642
		Vertical,
		// Token: 0x04003933 RID: 14643
		No
	}
}
