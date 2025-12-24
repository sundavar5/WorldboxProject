using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000566 RID: 1382
[RequireComponent(typeof(LayoutElement))]
public class DragSnapElement : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IScrollHandler, IDraggable, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x0015F9F1 File Offset: 0x0015DBF1
	public bool spawn_particles_on_drag
	{
		get
		{
			return this._spawn_particles_on_drag;
		}
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x0015F9FC File Offset: 0x0015DBFC
	private void Start()
	{
		this._layout_element = base.GetComponent<LayoutElement>();
		this._button = base.GetComponent<Button>();
		this._start_parent = base.transform.parent;
		this._start_local_position = base.transform.localPosition;
		this._initial_ignore_layout = this._layout_element.ignoreLayout;
		if (this.attach_parent == null)
		{
			this.attach_parent = World.world.drag_parent;
		}
		if (this.fly_back_parent == null)
		{
			this.fly_back_parent = (base.transform.FindParentWithName(new string[]
			{
				"Content",
				"Viewport"
			}) ?? this.attach_parent);
		}
		ScrollableButton tScrollableButton;
		if (base.gameObject.TryGetComponent<ScrollableButton>(out tScrollableButton))
		{
			tScrollableButton.enabled = false;
			this._scroll_rect_extended = base.gameObject.GetComponentInParent<ScrollRectExtended>();
			if (this._scroll_rect_extended == null)
			{
				this._scroll_rect = base.gameObject.GetComponentInParent<ScrollRect>();
			}
		}
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x0015FAF7 File Offset: 0x0015DCF7
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		if (!this._touch_drag_delay)
		{
			return;
		}
		this._drag_timer_started_at = Time.time;
	}

	// Token: 0x06002CF4 RID: 11508 RVA: 0x0015FB10 File Offset: 0x0015DD10
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (this._is_dragging)
		{
			return;
		}
		if (Config.isDraggingItem())
		{
			return;
		}
		if (this.isTouchDragDelayed())
		{
			return;
		}
		this._tweener.Kill(false);
		Config.setDraggingObject(this);
		this._is_dragging = true;
		base.transform.SetParent(this.attach_parent);
		this._button.enabled = false;
		this._layout_element.enabled = true;
		this.updatePosition(pEventData.position);
	}

	// Token: 0x06002CF5 RID: 11509 RVA: 0x0015FB8A File Offset: 0x0015DD8A
	public void OnDrag(PointerEventData pEventData)
	{
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		if (this.isTouchDragDelayed())
		{
			return;
		}
		this.updatePosition(pEventData.position);
	}

	// Token: 0x06002CF6 RID: 11510 RVA: 0x0015FBB0 File Offset: 0x0015DDB0
	public float getDragMod()
	{
		if (!this._is_dragging)
		{
			return 0f;
		}
		Vector3 vector = this._start_parent.TransformPoint(this._start_local_position);
		float tMod = Mathf.Clamp01((vector - base.transform.position).magnitude / this.limit_max_drag_distance);
		if (vector.y > base.transform.position.y)
		{
			tMod = -tMod;
		}
		return tMod;
	}

	// Token: 0x06002CF7 RID: 11511 RVA: 0x0015FC20 File Offset: 0x0015DE20
	private void updatePosition(Vector3 pTargetPosition)
	{
		Vector3 tStartPosition = this._start_parent.TransformPoint(this._start_local_position);
		Vector3 tDirection = pTargetPosition - tStartPosition;
		if (tDirection.magnitude > this.limit_max_drag_distance)
		{
			base.transform.position = tStartPosition + tDirection.normalized * this.limit_max_drag_distance;
			return;
		}
		base.transform.position = pTargetPosition;
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x0015FC88 File Offset: 0x0015DE88
	public void OnEndDrag(PointerEventData pEventData)
	{
		if (!Config.isDraggingItem())
		{
			return;
		}
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		Config.clearDraggingObject();
		this._is_dragging = false;
		this._drag_timer_started_at = 0f;
		this._layout_element.ignoreLayout = true;
		base.transform.SetParent(this.fly_back_parent);
		Tweener tweener = this._tweener;
		if (tweener != null)
		{
			tweener.Kill(false);
		}
		Vector3 tStartPosition = this._start_parent.TransformPoint(this._start_local_position);
		float tDistance = (tStartPosition - base.transform.position).magnitude;
		float tSpeed = this.dragSpeed(tDistance);
		this._tweener = DOTween.To(() => base.transform.position, delegate(Vector3 pVector)
		{
			base.transform.position = pVector;
		}, tStartPosition, tSpeed).SetEase(this.ease).OnComplete(new TweenCallback(this.resetElement));
		Tooltip.blockTooltips(tSpeed * 0.7f);
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x0015FD70 File Offset: 0x0015DF70
	public void resetElement()
	{
		if (this._hovered)
		{
			this._button.TriggerHover();
		}
		if (this._start_parent == null || (this.fly_back_parent != this._start_parent && this._start_parent == base.transform.parent))
		{
			return;
		}
		base.transform.SetParent(this._start_parent);
		base.transform.localPosition = this._start_local_position;
		this._button.enabled = true;
		this._layout_element.ignoreLayout = this._initial_ignore_layout;
		this._layout_element.enabled = false;
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x0015FE18 File Offset: 0x0015E018
	public float dragSpeed(float pDistance)
	{
		float tLerped = (Mathf.Clamp(pDistance, this.snapback_min_distance, this.snapback_max_distance) - this.snapback_min_distance) / (this.snapback_max_distance - this.snapback_min_distance);
		return Mathf.Lerp(this.snapback_speed_min_distance, this.snapback_speed_max_distance, tLerped);
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x0015FE5F File Offset: 0x0015E05F
	public void onWindowClose(string pId)
	{
		this._tweener.Kill(true);
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x0015FE6D File Offset: 0x0015E06D
	public void OnScroll(PointerEventData pEventData)
	{
		this.sendMessage("OnScroll", pEventData);
	}

	// Token: 0x06002CFD RID: 11517 RVA: 0x0015FE7B File Offset: 0x0015E07B
	public void OnPointerEnter(PointerEventData eventData)
	{
		this._hovered = true;
	}

	// Token: 0x06002CFE RID: 11518 RVA: 0x0015FE84 File Offset: 0x0015E084
	public void OnPointerExit(PointerEventData eventData)
	{
		this._hovered = false;
	}

	// Token: 0x06002CFF RID: 11519 RVA: 0x0015FE8D File Offset: 0x0015E08D
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

	// Token: 0x06002D00 RID: 11520 RVA: 0x0015FEB4 File Offset: 0x0015E0B4
	public void OnEnable()
	{
		ScrollWindow.addCallbackHide(new ScrollWindowNameAction(this.onWindowClose));
	}

	// Token: 0x06002D01 RID: 11521 RVA: 0x0015FEC7 File Offset: 0x0015E0C7
	public void OnDisable()
	{
		ScrollWindow.removeCallbackHide(new ScrollWindowNameAction(this.onWindowClose));
		this.KillDrag();
		if (this._tweener.IsActive())
		{
			Debug.LogError("OnDisable kill called, shouldn't happen", this);
			this._tweener.Kill(false);
		}
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x0015FF04 File Offset: 0x0015E104
	public void KillDrag()
	{
		if (this._is_dragging)
		{
			this.OnEndDrag(new PointerEventData(EventSystem.current));
			this._tweener.Kill(true);
		}
	}

	// Token: 0x06002D03 RID: 11523 RVA: 0x0015FF2A File Offset: 0x0015E12A
	private bool isTouchDragDelayed()
	{
		return this._touch_drag_delay && !InputHelpers.mouseSupported && Time.time - this._drag_timer_started_at < 0.2f;
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x0015FFB4 File Offset: 0x0015E1B4
	Transform IDraggable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04002275 RID: 8821
	[SerializeField]
	private bool _spawn_particles_on_drag = true;

	// Token: 0x04002276 RID: 8822
	[SerializeField]
	private bool _touch_drag_delay;

	// Token: 0x04002277 RID: 8823
	private float _drag_timer_started_at;

	// Token: 0x04002278 RID: 8824
	private Tweener _tweener;

	// Token: 0x04002279 RID: 8825
	private LayoutElement _layout_element;

	// Token: 0x0400227A RID: 8826
	private Button _button;

	// Token: 0x0400227B RID: 8827
	private Vector3 _start_local_position;

	// Token: 0x0400227C RID: 8828
	private Transform _start_parent;

	// Token: 0x0400227D RID: 8829
	public float limit_max_drag_distance = 77f;

	// Token: 0x0400227E RID: 8830
	public float snapback_max_distance = 77f;

	// Token: 0x0400227F RID: 8831
	public float snapback_speed_max_distance = 0.35f;

	// Token: 0x04002280 RID: 8832
	public float snapback_min_distance = 22f;

	// Token: 0x04002281 RID: 8833
	public float snapback_speed_min_distance = 0.9f;

	// Token: 0x04002282 RID: 8834
	public Transform attach_parent;

	// Token: 0x04002283 RID: 8835
	public Transform fly_back_parent;

	// Token: 0x04002284 RID: 8836
	public Ease ease = Ease.OutElastic;

	// Token: 0x04002285 RID: 8837
	public float speed = 0.4f;

	// Token: 0x04002286 RID: 8838
	private ScrollRect _scroll_rect;

	// Token: 0x04002287 RID: 8839
	private ScrollRectExtended _scroll_rect_extended;

	// Token: 0x04002288 RID: 8840
	private bool _initial_ignore_layout;

	// Token: 0x04002289 RID: 8841
	private bool _hovered;

	// Token: 0x0400228A RID: 8842
	private bool _is_dragging;
}
