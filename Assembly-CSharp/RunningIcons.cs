using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020007E4 RID: 2020
public class RunningIcons : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06003F96 RID: 16278 RVA: 0x001B59E0 File Offset: 0x001B3BE0
	public void addIcon(RunningIcon pIcon)
	{
		this._icons.Add(pIcon);
	}

	// Token: 0x06003F97 RID: 16279 RVA: 0x001B59F0 File Offset: 0x001B3BF0
	public void init(RunningIconCallback pNextItemAction, RunningIconCallback pPrevItemAction)
	{
		this._initialized = true;
		this._next_item_action = pNextItemAction;
		this._prev_item_action = pPrevItemAction;
		foreach (object obj in base.transform)
		{
			Transform tTransform = (Transform)obj;
			if (this._first_position_x > tTransform.localPosition.x)
			{
				this._first_position_x = tTransform.localPosition.x;
			}
			if (this._last_position_x < tTransform.localPosition.x)
			{
				this._last_position_x = tTransform.localPosition.x;
			}
		}
		float tSecondItem = float.MaxValue;
		foreach (object obj2 in base.transform)
		{
			Transform tTransform2 = (Transform)obj2;
			if (tTransform2.localPosition.x != this._first_position_x && tSecondItem > tTransform2.localPosition.x)
			{
				tSecondItem = tTransform2.localPosition.x;
			}
		}
		this._step = tSecondItem - this._first_position_x;
		this._last_position_x += this._step;
		this._last_index = base.transform.childCount - 1;
		this.toggle(true);
	}

	// Token: 0x06003F98 RID: 16280 RVA: 0x001B5B54 File Offset: 0x001B3D54
	private void OnEnable()
	{
		this.reset();
	}

	// Token: 0x06003F99 RID: 16281 RVA: 0x001B5B5C File Offset: 0x001B3D5C
	private void reset()
	{
		if (this._icons == null)
		{
			return;
		}
		foreach (RunningIcon tIcon in this._icons)
		{
			this._next_item_action(tIcon.transform);
		}
	}

	// Token: 0x06003F9A RID: 16282 RVA: 0x001B5BC4 File Offset: 0x001B3DC4
	private void Update()
	{
		if (!this._initialized)
		{
			return;
		}
		if (!this._state)
		{
			return;
		}
		this.moveBy(this.speed, this._direction, 0);
	}

	// Token: 0x06003F9B RID: 16283 RVA: 0x001B5BEC File Offset: 0x001B3DEC
	public void moveBy(float pSpeed, RunningIcons.Direction pDirection, int pCounter = 0)
	{
		int tStartIndex = 0;
		int tEndIndex = this._last_index;
		float tStep = this._step;
		if (pDirection == RunningIcons.Direction.Left)
		{
			tStartIndex = this._last_index;
			tEndIndex = 0;
			pSpeed *= -1f;
			tStep *= -1f;
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			Vector3 tPosition = transform.localPosition;
			tPosition.x += pSpeed;
			transform.localPosition = tPosition;
		}
		for (;;)
		{
			Transform tTransform = base.transform.GetChild(tEndIndex);
			Vector3 tPosition2 = tTransform.localPosition;
			if (!this.endReached(tPosition2.x, pDirection))
			{
				break;
			}
			Transform tSibling = base.transform.GetChild(tStartIndex);
			tPosition2.x = tSibling.localPosition.x - tStep;
			tTransform.localPosition = tPosition2;
			tTransform.SetSiblingIndex(tStartIndex);
			if (pDirection == RunningIcons.Direction.Left)
			{
				this._prev_item_action(tTransform);
			}
			else
			{
				this._next_item_action(tTransform);
			}
		}
	}

	// Token: 0x06003F9C RID: 16284 RVA: 0x001B5D04 File Offset: 0x001B3F04
	public void toggle(bool pState)
	{
		this._state = pState;
	}

	// Token: 0x06003F9D RID: 16285 RVA: 0x001B5D0D File Offset: 0x001B3F0D
	public bool getState()
	{
		return this._state;
	}

	// Token: 0x06003F9E RID: 16286 RVA: 0x001B5D15 File Offset: 0x001B3F15
	private bool endReached(float pPosition, RunningIcons.Direction pDirection)
	{
		if (pDirection == RunningIcons.Direction.Right)
		{
			return pPosition >= this._last_position_x;
		}
		return pPosition <= this._first_position_x;
	}

	// Token: 0x06003F9F RID: 16287 RVA: 0x001B5D34 File Offset: 0x001B3F34
	public void OnSelect(BaseEventData pEventData)
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		this.toggle(false);
	}

	// Token: 0x06003FA0 RID: 16288 RVA: 0x001B5D45 File Offset: 0x001B3F45
	public void OnDeselect(BaseEventData pEventData)
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		this.toggle(true);
	}

	// Token: 0x04002E1C RID: 11804
	[SerializeField]
	private RunningIcons.Direction _direction;

	// Token: 0x04002E1D RID: 11805
	[SerializeField]
	private float speed;

	// Token: 0x04002E1E RID: 11806
	private IconGetter _get_next_icon;

	// Token: 0x04002E1F RID: 11807
	private RunningIconCallback _next_item_action;

	// Token: 0x04002E20 RID: 11808
	private RunningIconCallback _prev_item_action;

	// Token: 0x04002E21 RID: 11809
	private float _first_position_x = float.MaxValue;

	// Token: 0x04002E22 RID: 11810
	private float _last_position_x = float.MinValue;

	// Token: 0x04002E23 RID: 11811
	private int _last_index;

	// Token: 0x04002E24 RID: 11812
	private List<RunningIcon> _icons = new List<RunningIcon>();

	// Token: 0x04002E25 RID: 11813
	private bool _state;

	// Token: 0x04002E26 RID: 11814
	private bool _initialized;

	// Token: 0x04002E27 RID: 11815
	private float _step;

	// Token: 0x02000B15 RID: 2837
	public enum Direction
	{
		// Token: 0x04003C72 RID: 15474
		Left,
		// Token: 0x04003C73 RID: 15475
		Right
	}
}
