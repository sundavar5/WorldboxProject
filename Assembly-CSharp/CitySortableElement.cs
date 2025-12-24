using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200065C RID: 1628
public class CitySortableElement : CityElement, ILayoutController
{
	// Token: 0x060034C4 RID: 13508 RVA: 0x00186A3C File Offset: 0x00184C3C
	protected override void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
		base.Awake();
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x00186A50 File Offset: 0x00184C50
	protected virtual void onListChange()
	{
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x00186A54 File Offset: 0x00184C54
	public void SetLayoutVertical()
	{
		if (this._rect == null)
		{
			return;
		}
		using (ListPool<RectTransform> tChildren = this._rect.getLayoutChildren())
		{
			if (!tChildren.SequenceEqual(this._rect_children))
			{
				this._rect_children.Clear();
				this._rect_children.AddRange(tChildren);
				this.onListChange();
			}
		}
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x00186AC4 File Offset: 0x00184CC4
	public void SetLayoutHorizontal()
	{
	}

	// Token: 0x040027B5 RID: 10165
	private RectTransform _rect;

	// Token: 0x040027B6 RID: 10166
	private List<RectTransform> _rect_children = new List<RectTransform>();
}
