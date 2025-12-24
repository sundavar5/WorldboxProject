using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000799 RID: 1945
public class TraitsGrid : MonoBehaviour, ILayoutController
{
	// Token: 0x06003DAF RID: 15791 RVA: 0x001AECD3 File Offset: 0x001ACED3
	private void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x06003DB0 RID: 15792 RVA: 0x001AECE4 File Offset: 0x001ACEE4
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
				OnChange onChange = this.on_change;
				if (onChange != null)
				{
					onChange();
				}
			}
		}
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x001AED60 File Offset: 0x001ACF60
	public void SetLayoutHorizontal()
	{
	}

	// Token: 0x04002C89 RID: 11401
	public OnChange on_change;

	// Token: 0x04002C8A RID: 11402
	private RectTransform _rect;

	// Token: 0x04002C8B RID: 11403
	private List<RectTransform> _rect_children = new List<RectTransform>();
}
