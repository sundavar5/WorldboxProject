using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005FC RID: 1532
public class SelectedPowerTabTopContainer : MonoBehaviour
{
	// Token: 0x0600325B RID: 12891 RVA: 0x0017E7BE File Offset: 0x0017C9BE
	private void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x0017E7CC File Offset: 0x0017C9CC
	private void Update()
	{
		if (SelectedUnit.isSet())
		{
			Actor tActor = SelectedUnit.unit;
			if (!tActor.isRekt())
			{
				Sprite tSpriteIcon = tActor.asset.getSpriteIcon();
				this._icon_unit.sprite = tSpriteIcon;
			}
		}
		else if (SelectedObjects.isNanoObjectSet())
		{
			NanoObject tNanoObject = SelectedObjects.getSelectedNanoObject();
			if (!tNanoObject.isRekt())
			{
				MetaTypeAsset tMetaTypeAsset = tNanoObject.getMetaTypeAsset();
				if (tMetaTypeAsset.set_icon_for_cancel_button)
				{
					Sprite tSpriteIcon2 = tMetaTypeAsset.getIconSprite();
					this._icon_unit.sprite = tSpriteIcon2;
				}
			}
		}
		if (this.goDown != this._going_down)
		{
			this._going_down = this.goDown;
			this._timer = 0f;
			if (this.goDown)
			{
				this._timer = 0.95f;
			}
		}
		if (this.goUp != this._going_up)
		{
			this._going_up = this.goUp;
			this._timer = -1f;
		}
		if (this._timer < 1f)
		{
			this._timer += Time.deltaTime / 2f;
			this._timer = Mathf.Clamp(this._timer, 0f, 1f);
		}
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x0017E8DE File Offset: 0x0017CADE
	public void cancelSelection()
	{
		SelectedUnit.clear();
		SelectedObjects.unselectNanoObject();
		PowersTab.unselect();
	}

	// Token: 0x0400260F RID: 9743
	[SerializeField]
	private Image _icon_unit;

	// Token: 0x04002610 RID: 9744
	public bool goUp;

	// Token: 0x04002611 RID: 9745
	public bool goDown;

	// Token: 0x04002612 RID: 9746
	private bool _going_down;

	// Token: 0x04002613 RID: 9747
	private bool _going_up;

	// Token: 0x04002614 RID: 9748
	private RectTransform _rect;

	// Token: 0x04002615 RID: 9749
	private float _timer;
}
