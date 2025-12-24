using System;
using System.Collections.Generic;
using LayoutGroupExt;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

// Token: 0x020005D8 RID: 1496
[DisallowMultipleComponent]
public class FlexibleOneRowGrid : MonoBehaviour, ILayoutController
{
	// Token: 0x06003129 RID: 12585 RVA: 0x00178E31 File Offset: 0x00177031
	private void Awake()
	{
		this.init();
	}

	// Token: 0x0600312A RID: 12586 RVA: 0x00178E3C File Offset: 0x0017703C
	private void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		if (this.HasComponent<GridLayoutGroup>())
		{
			this._grid = base.GetComponent<GridLayoutGroup>();
			this._grid_rect = this._grid.GetComponent<RectTransform>();
			return;
		}
		this._grid_extended = base.GetComponent<GridLayoutGroupExtended>();
		this._grid_rect = this._grid_extended.GetComponent<RectTransform>();
		this._is_extended = true;
	}

	// Token: 0x0600312B RID: 12587 RVA: 0x00178EA4 File Offset: 0x001770A4
	public void SetLayoutHorizontal()
	{
		if (!this.debug && !Application.isPlaying)
		{
			return;
		}
		this.init();
		float tCellSize = this._is_extended ? this._grid_extended.cellSize.x : this._grid.cellSize.x;
		float tGridSize = this._grid_rect.rect.width;
		float tChildren = this.calculateChildren();
		float tCurrentWidth = tCellSize * tChildren + (float)this.bonus_spacing_x * (tChildren - 1f);
		float tSpacingX;
		if (tCurrentWidth < tGridSize)
		{
			tSpacingX = (float)this.bonus_spacing_x;
		}
		else
		{
			tCurrentWidth = tCellSize * tChildren;
			tSpacingX = (tGridSize - tCurrentWidth) / (tChildren - 1f);
		}
		if (this._is_extended)
		{
			this._grid_extended.spacing = new Vector2(tSpacingX, 0f);
			return;
		}
		this._grid.spacing = new Vector2(tSpacingX, 0f);
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x00178F80 File Offset: 0x00177180
	public float calculateChildren()
	{
		List<Component> tToIgnoreList = CollectionPool<List<Component>, Component>.Get();
		int tChildren = 0;
		int i = 0;
		int tLen = this._grid_rect.childCount;
		while (i < tLen)
		{
			RectTransform tChild = this._grid_rect.GetChild(i) as RectTransform;
			if (!(tChild == null) && tChild.gameObject.activeInHierarchy)
			{
				if (!tChild.HasComponent<ILayoutIgnorer>())
				{
					tChildren++;
				}
				else
				{
					tChild.GetComponents(typeof(ILayoutIgnorer), tToIgnoreList);
					for (int j = 0; j < tToIgnoreList.Count; j++)
					{
						if (!((ILayoutIgnorer)tToIgnoreList[j]).ignoreLayout)
						{
							tChildren++;
							break;
						}
					}
					tToIgnoreList.Clear();
				}
			}
			i++;
		}
		CollectionPool<List<Component>, Component>.Release(tToIgnoreList);
		return (float)tChildren;
	}

	// Token: 0x0600312D RID: 12589 RVA: 0x0017903C File Offset: 0x0017723C
	public void SetLayoutVertical()
	{
	}

	// Token: 0x04002523 RID: 9507
	public bool debug;

	// Token: 0x04002524 RID: 9508
	public int bonus_spacing_x;

	// Token: 0x04002525 RID: 9509
	private RectTransform _grid_rect;

	// Token: 0x04002526 RID: 9510
	private GridLayoutGroup _grid;

	// Token: 0x04002527 RID: 9511
	private GridLayoutGroupExtended _grid_extended;

	// Token: 0x04002528 RID: 9512
	private bool _is_extended;

	// Token: 0x04002529 RID: 9513
	private bool _initialized;
}
