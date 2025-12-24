using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020007E1 RID: 2017
public class RunningIcon : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IScrollHandler, IPointerClickHandler, IDraggable
{
	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06003F80 RID: 16256 RVA: 0x001B57D7 File Offset: 0x001B39D7
	public bool spawn_particles_on_drag
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06003F81 RID: 16257 RVA: 0x001B57DA File Offset: 0x001B39DA
	public void Awake()
	{
		this._parent = base.GetComponentInParent<RunningIcons>();
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x001B57E8 File Offset: 0x001B39E8
	public Image getIconImage()
	{
		return this._icon;
	}

	// Token: 0x06003F83 RID: 16259 RVA: 0x001B57F0 File Offset: 0x001B39F0
	public void setIcon(Sprite pIcon)
	{
		this._icon.sprite = pIcon;
	}

	// Token: 0x06003F84 RID: 16260 RVA: 0x001B57FE File Offset: 0x001B39FE
	public void setIconColor(Color pColor)
	{
		this._icon.color = pColor;
	}

	// Token: 0x06003F85 RID: 16261 RVA: 0x001B580C File Offset: 0x001B3A0C
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (Config.isDraggingItem())
		{
			return;
		}
		Config.setDraggingObject(this);
		this._last_position = pEventData.position;
		this._parent.toggle(false);
	}

	// Token: 0x06003F86 RID: 16262 RVA: 0x001B5834 File Offset: 0x001B3A34
	public void OnDrag(PointerEventData pEventData)
	{
		if (!Config.isDraggingObject(this))
		{
			return;
		}
		this._parent.toggle(false);
		Vector2 tDelta = pEventData.position - this._last_position;
		this._last_position = pEventData.position;
		if (tDelta.x == 0f)
		{
			return;
		}
		float tDist = tDelta.x / CanvasMain.instance.canvas_ui.scaleFactor;
		if (tDist < 0f)
		{
			this._parent.moveBy(Mathf.Abs(tDist), RunningIcons.Direction.Left, 0);
			return;
		}
		this._parent.moveBy(Mathf.Abs(tDist), RunningIcons.Direction.Right, 0);
	}

	// Token: 0x06003F87 RID: 16263 RVA: 0x001B58C8 File Offset: 0x001B3AC8
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
		this._parent.toggle(true);
	}

	// Token: 0x06003F88 RID: 16264 RVA: 0x001B58EC File Offset: 0x001B3AEC
	public void OnScroll(PointerEventData pEventData)
	{
		if (pEventData.scrollDelta.y < 0f)
		{
			this._parent.moveBy(Mathf.Abs(pEventData.scrollDelta.y * 20f), RunningIcons.Direction.Left, 0);
			return;
		}
		this._parent.moveBy(Mathf.Abs(pEventData.scrollDelta.y * 20f), RunningIcons.Direction.Right, 0);
	}

	// Token: 0x06003F89 RID: 16265 RVA: 0x001B5954 File Offset: 0x001B3B54
	public void OnPointerClick(PointerEventData pEventData)
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		base.GetComponent<Button>().onClick.Invoke();
		if (EventSystem.current.currentSelectedGameObject == this._parent.gameObject)
		{
			this._parent.toggle(false);
		}
		EventSystem.current.SetSelectedGameObject(this._parent.gameObject);
	}

	// Token: 0x06003F8A RID: 16266 RVA: 0x001B59B6 File Offset: 0x001B3BB6
	private void OnDisable()
	{
		this.KillDrag();
	}

	// Token: 0x06003F8B RID: 16267 RVA: 0x001B59BE File Offset: 0x001B3BBE
	public void KillDrag()
	{
		this.OnEndDrag(new PointerEventData(EventSystem.current));
	}

	// Token: 0x06003F8D RID: 16269 RVA: 0x001B59D8 File Offset: 0x001B3BD8
	Transform IDraggable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04002E19 RID: 11801
	[SerializeField]
	private Image _icon;

	// Token: 0x04002E1A RID: 11802
	private RunningIcons _parent;

	// Token: 0x04002E1B RID: 11803
	private Vector2 _last_position;
}
