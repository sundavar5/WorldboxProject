using System;
using System.Collections;
using System.Collections.Generic;
using db;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000719 RID: 1817
public class WorldLogWindow : MonoBehaviour
{
	// Token: 0x060039EA RID: 14826 RVA: 0x0019B548 File Offset: 0x00199748
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<WorldLogElement>(this._element_prefab_log, this._transform_content);
		this._pool_empty = new ObjectPoolGenericMono<EmptyLogElement>(this._element_prefab_empty, this._transform_content);
		using (List<HistoryGroupAsset>.Enumerator enumerator = AssetManager.history_groups.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				HistoryGroupAsset tAsset = enumerator.Current;
				Object.Instantiate<ToggleButton>(this._prefab, this._grid.transform).init(tAsset.icon_path, tAsset.getLocaleID(), delegate(ToggleButton pButton)
				{
					if (pButton.is_on)
					{
						this._selected_groups.Add(tAsset.id);
						return;
					}
					this._selected_groups.Remove(tAsset.id);
				}, new ToggleButtonAction(this.showSorted));
			}
		}
		int tWidth = 198 / AssetManager.history_groups.list.Count;
		this._grid.cellSize = new Vector2((float)tWidth, this._grid.cellSize.y);
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x0019B65C File Offset: 0x0019985C
	private void OnEnable()
	{
		this.clear();
		this._messages = DBGetter.getWorldLogMessages();
		bool tHasItems = this._messages.Any<WorldLogMessage>();
		this._no_items.SetActive(!tHasItems);
		this._grid.gameObject.SetActive(tHasItems);
		if (!tHasItems)
		{
			return;
		}
		this.showSorted();
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x0019B6B0 File Offset: 0x001998B0
	private void OnRenderObject()
	{
		this._autolayout_done = true;
	}

	// Token: 0x060039ED RID: 14829 RVA: 0x0019B6BC File Offset: 0x001998BC
	private void LateUpdate()
	{
		if (!this._autolayout_done)
		{
			return;
		}
		IReadOnlyList<EmptyLogElement> tList = this._pool_empty.getListTotal();
		int tFirstVisible = int.MaxValue;
		int tLastVisible = int.MinValue;
		float tScrollRectBottom = this._scroll_rect.content.localPosition.y;
		float tScrollRectTop = tScrollRectBottom + this._scroll_rect.viewport.rect.height;
		for (int i = 0; i < tList.Count; i++)
		{
			EmptyLogElement tEmptyMono = tList[i];
			if (tEmptyMono.gameObject.activeSelf)
			{
				if (this.IsVisibleInScrollRect(tEmptyMono.rect_transform, this._scroll_rect, tScrollRectTop, tScrollRectBottom))
				{
					if (tFirstVisible == 2147483647)
					{
						tFirstVisible = i;
					}
					tLastVisible = i;
				}
				else if (tLastVisible > -2147483648)
				{
					break;
				}
			}
		}
		if (tLastVisible == 2147483647)
		{
			return;
		}
		if (tFirstVisible == -2147483648)
		{
			return;
		}
		int tFirstPadding = Math.Max(0, tFirstVisible - 3);
		int tLastPadding = Math.Min(tList.Count - 1, tLastVisible + 3);
		for (int j = 0; j < tList.Count; j++)
		{
			if (j < tFirstPadding || j > tLastPadding)
			{
				EmptyLogElement tEmptyMono2 = tList[j];
				WorldLogElement tElement = tEmptyMono2.getElement();
				if (!(tElement == null))
				{
					this._pool.release(tElement, true);
					tEmptyMono2.setElement(null);
				}
			}
		}
		for (int k = tFirstPadding; k <= tLastPadding; k++)
		{
			EmptyLogElement tEmptyMono3 = tList[k];
			if (tEmptyMono3.gameObject.activeSelf)
			{
				WorldLogElement tElement2 = tEmptyMono3.getElement();
				if (!(tElement2 != null))
				{
					tElement2 = this._pool.getNext();
					tEmptyMono3.setElement(tElement2);
				}
			}
		}
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x0019B850 File Offset: 0x00199A50
	private bool IsVisibleInScrollRect(RectTransform pRectTransform, ScrollRect pScrollRect, float pScrollRectTop, float pScrollRectBottom)
	{
		Vector2 tLocal = pRectTransform.localPosition;
		tLocal *= -1f;
		float tHeight = pRectTransform.sizeDelta.y * 0.6f;
		return tLocal.y <= pScrollRectTop + tHeight + base.transform.localPosition.y && tLocal.y >= pScrollRectBottom - tHeight + base.transform.localPosition.y;
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x0019B8C5 File Offset: 0x00199AC5
	private void showSorted()
	{
		base.StartCoroutine(this.showSortedRoutine());
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x0019B8D4 File Offset: 0x00199AD4
	private IEnumerator showSortedRoutine()
	{
		WorldLogWindow.<showSortedRoutine>d__19 <showSortedRoutine>d__ = new WorldLogWindow.<showSortedRoutine>d__19(0);
		<showSortedRoutine>d__.<>4__this = this;
		return <showSortedRoutine>d__;
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x0019B8E4 File Offset: 0x00199AE4
	private int sortByTime(WorldLogMessage p1, WorldLogMessage p2)
	{
		return p2.timestamp.CompareTo(p1.timestamp);
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x0019B905 File Offset: 0x00199B05
	private void OnDisable()
	{
		ListPool<WorldLogMessage> messages = this._messages;
		if (messages != null)
		{
			messages.Dispose();
		}
		this._messages = null;
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x0019B91F File Offset: 0x00199B1F
	private void clear()
	{
		this._pool.clear(true);
		this._pool_empty.clear(true);
	}

	// Token: 0x04002AD9 RID: 10969
	private const int PADDING_ELEMENTS = 3;

	// Token: 0x04002ADA RID: 10970
	[SerializeField]
	private WorldLogElement _element_prefab_log;

	// Token: 0x04002ADB RID: 10971
	[SerializeField]
	private EmptyLogElement _element_prefab_empty;

	// Token: 0x04002ADC RID: 10972
	[SerializeField]
	private Transform _transform_content;

	// Token: 0x04002ADD RID: 10973
	[SerializeField]
	private GameObject _no_items;

	// Token: 0x04002ADE RID: 10974
	[SerializeField]
	private GridLayoutGroup _grid;

	// Token: 0x04002ADF RID: 10975
	[SerializeField]
	private ScrollRect _scroll_rect;

	// Token: 0x04002AE0 RID: 10976
	[SerializeField]
	private ToggleButton _prefab;

	// Token: 0x04002AE1 RID: 10977
	private ObjectPoolGenericMono<WorldLogElement> _pool;

	// Token: 0x04002AE2 RID: 10978
	private ObjectPoolGenericMono<EmptyLogElement> _pool_empty;

	// Token: 0x04002AE3 RID: 10979
	private HashSet<string> _selected_groups = new HashSet<string>();

	// Token: 0x04002AE4 RID: 10980
	private ListPool<WorldLogMessage> _messages;

	// Token: 0x04002AE5 RID: 10981
	private bool _autolayout_done;
}
