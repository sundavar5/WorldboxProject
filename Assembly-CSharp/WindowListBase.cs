using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007FA RID: 2042
public class WindowListBase<TListElement, TMetaObject, TData> : MonoBehaviour, IShouldRefreshWindow where TListElement : WindowListElementBase<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06004008 RID: 16392 RVA: 0x001B70B4 File Offset: 0x001B52B4
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06004009 RID: 16393 RVA: 0x001B70C6 File Offset: 0x001B52C6
	private MetaTypeAsset _meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x0600400A RID: 16394 RVA: 0x001B70D8 File Offset: 0x001B52D8
	private void checkCreate()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.create();
	}

	// Token: 0x0600400B RID: 16395 RVA: 0x001B70F0 File Offset: 0x001B52F0
	protected virtual void create()
	{
		this.pool_elements = new ObjectPoolGenericMono<TListElement>(this.element_prefab, this.transformContent);
		this._scroll_window = base.gameObject.GetComponent<ScrollWindow>();
		this._element_width = this.element_prefab.transform.GetComponent<RectTransform>().sizeDelta.x;
		this._element_height = this.element_prefab.transform.GetComponent<RectTransform>().sizeDelta.y;
		this._scroll_rect = base.gameObject.GetComponentInChildren<ScrollRect>();
		this.addEmptyPoolSystem();
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x001B7186 File Offset: 0x001B5386
	private void OnRenderObject()
	{
		this.autolayout_done = true;
	}

	// Token: 0x0600400D RID: 16397 RVA: 0x001B7190 File Offset: 0x001B5390
	private void LateUpdate()
	{
		if (!this.autolayout_done)
		{
			return;
		}
		IReadOnlyList<BaseEmptyListMono> tList = this.pool_empty_elements.getListTotal();
		int tFirstVisible = int.MaxValue;
		int tLastVisible = int.MinValue;
		float tScrollRectBottom = this._scroll_rect.content.localPosition.y;
		float tScrollRectTop = tScrollRectBottom + this._scroll_rect.viewport.rect.height;
		for (int i = 0; i < tList.Count; i++)
		{
			BaseEmptyListMono tEmptyMono = tList[i];
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
				BaseEmptyListMono tEmptyMono2 = tList[j];
				this.releaseElement(tEmptyMono2);
			}
		}
		for (int k = tFirstPadding; k <= tLastPadding; k++)
		{
			BaseEmptyListMono tEmptyMono3 = tList[k];
			if (tEmptyMono3.gameObject.activeSelf && !tEmptyMono3.hasElement())
			{
				this.makeElementVisible(tEmptyMono3);
			}
		}
		if (WindowListBase<TListElement, TMetaObject, TData>._debug)
		{
			this.debugUpdateElementNames(tList, tScrollRectTop, tScrollRectBottom);
		}
	}

	// Token: 0x0600400E RID: 16398 RVA: 0x001B72FC File Offset: 0x001B54FC
	private void makeElementVisible(BaseEmptyListMono pEmptyMono)
	{
		TListElement tElement = this.pool_elements.getNext();
		tElement.show((TMetaObject)((object)pEmptyMono.meta_object));
		tElement.transform.SetParent(pEmptyMono.transform);
		tElement.transform.localPosition = Vector3.zero;
		pEmptyMono.assignElement(tElement);
	}

	// Token: 0x0600400F RID: 16399 RVA: 0x001B7364 File Offset: 0x001B5564
	private bool IsVisibleInScrollRect(RectTransform pRectTransform, ScrollRect pScrollRect, float pScrollRectTop, float pScrollRectBottom)
	{
		Vector2 tLocal = pRectTransform.localPosition;
		tLocal *= -1f;
		float tHeight = pRectTransform.sizeDelta.y * 0.6f;
		return tLocal.y <= pScrollRectTop + tHeight && tLocal.y >= pScrollRectBottom - tHeight;
	}

	// Token: 0x06004010 RID: 16400 RVA: 0x001B73B8 File Offset: 0x001B55B8
	private void addEmptyPoolSystem()
	{
		BaseEmptyListMono tEmptyObject = Resources.Load<BaseEmptyListMono>("ui/list_element_empty");
		this.pool_empty_elements = new ObjectPoolGenericMono<BaseEmptyListMono>(tEmptyObject, this.transformContent);
	}

	// Token: 0x06004011 RID: 16401 RVA: 0x001B73E2 File Offset: 0x001B55E2
	private void showElement(TMetaObject pObject)
	{
		this.pool_empty_elements.getNext().assignObject(pObject);
	}

	// Token: 0x06004012 RID: 16402 RVA: 0x001B73FA File Offset: 0x001B55FA
	protected virtual IEnumerable<TMetaObject> getObjects()
	{
		return this._meta_type_asset.get_list().Cast<TMetaObject>();
	}

	// Token: 0x06004013 RID: 16403 RVA: 0x001B7411 File Offset: 0x001B5611
	private void OnEnable()
	{
		this.checkCreate();
		this.show();
	}

	// Token: 0x06004014 RID: 16404 RVA: 0x001B7420 File Offset: 0x001B5620
	private void show()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.clear();
		if (this.isEmpty())
		{
			this.noItems.SetActive(true);
		}
		else
		{
			this.noItems.SetActive(false);
			this.showElements();
		}
		this.pool_empty_elements.disableInactive();
		ScrollWindow.checkElements();
	}

	// Token: 0x06004015 RID: 16405 RVA: 0x001B7474 File Offset: 0x001B5674
	private ListPool<TMetaObject> getElements()
	{
		this.meta_list.Clear();
		this.meta_list.AddRange(this.getObjects());
		this.meta_list.Sort(this.current_sort);
		SortButton currentButton = this.sorting_tab.getCurrentButton();
		if (currentButton != null && currentButton.getState() == SortButtonState.Down)
		{
			this.meta_list.Reverse();
		}
		return new ListPool<TMetaObject>(this.meta_list);
	}

	// Token: 0x06004016 RID: 16406 RVA: 0x001B74E0 File Offset: 0x001B56E0
	private void showElements()
	{
		using (ListPool<TMetaObject> tList = this.getElements())
		{
			for (int i = 0; i < tList.Count; i++)
			{
				TMetaObject tObject = tList[i];
				this.showElement(tObject);
			}
		}
	}

	// Token: 0x06004017 RID: 16407 RVA: 0x001B7530 File Offset: 0x001B5730
	private bool isEmpty()
	{
		IEnumerable<TMetaObject> tList = this.getObjects();
		return tList == null || tList.Count<TMetaObject>() == 0;
	}

	// Token: 0x06004018 RID: 16408 RVA: 0x001B7554 File Offset: 0x001B5754
	private void clear()
	{
		IReadOnlyList<BaseEmptyListMono> tList = this.pool_empty_elements.getListTotal();
		for (int ii = 0; ii < tList.Count; ii++)
		{
			BaseEmptyListMono tEmptyMono = tList[ii];
			this.releaseElement(tEmptyMono);
			tEmptyMono.clearObject();
		}
		this.pool_empty_elements.clear(true);
		this.pool_elements.resetParent();
		this.meta_list.Clear();
	}

	// Token: 0x06004019 RID: 16409 RVA: 0x001B75B8 File Offset: 0x001B57B8
	private void releaseElement(BaseEmptyListMono pEmptyMono)
	{
		if (!pEmptyMono.hasElement())
		{
			return;
		}
		TListElement tElement = (TListElement)((object)pEmptyMono.element);
		pEmptyMono.clearElement();
		this.pool_elements.release(tElement, true);
	}

	// Token: 0x0600401A RID: 16410 RVA: 0x001B75F0 File Offset: 0x001B57F0
	private void debugUpdateElementNames(IReadOnlyList<BaseEmptyListMono> pList, float pScrollRectTop, float pScrollRectBottom)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			BaseEmptyListMono tEmptyMono = pList[i];
			bool tIsVisible = this.IsVisibleInScrollRect(tEmptyMono.rect_transform, this._scroll_rect, pScrollRectTop, pScrollRectBottom);
			tEmptyMono.debugUpdateName(tIsVisible);
		}
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x001B7632 File Offset: 0x001B5832
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x0600401C RID: 16412 RVA: 0x001B763C File Offset: 0x001B583C
	public virtual bool checkRefreshWindow()
	{
		using (List<TMetaObject>.Enumerator enumerator = this.meta_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isRekt())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04002E77 RID: 11895
	public GameObject noItems;

	// Token: 0x04002E78 RID: 11896
	protected ObjectPoolGenericMono<TListElement> pool_elements;

	// Token: 0x04002E79 RID: 11897
	protected ObjectPoolGenericMono<BaseEmptyListMono> pool_empty_elements;

	// Token: 0x04002E7A RID: 11898
	public Transform transformContent;

	// Token: 0x04002E7B RID: 11899
	public TListElement element_prefab;

	// Token: 0x04002E7C RID: 11900
	public SortingTab sorting_tab;

	// Token: 0x04002E7D RID: 11901
	private bool _created;

	// Token: 0x04002E7E RID: 11902
	protected Comparison<TMetaObject> current_sort;

	// Token: 0x04002E7F RID: 11903
	public readonly List<TMetaObject> meta_list = new List<TMetaObject>();

	// Token: 0x04002E80 RID: 11904
	private ScrollWindow _scroll_window;

	// Token: 0x04002E81 RID: 11905
	private float _element_width;

	// Token: 0x04002E82 RID: 11906
	private float _element_height;

	// Token: 0x04002E83 RID: 11907
	private ScrollRect _scroll_rect;

	// Token: 0x04002E84 RID: 11908
	private bool autolayout_done;

	// Token: 0x04002E85 RID: 11909
	private const int PADDING_ELEMENTS = 3;

	// Token: 0x04002E86 RID: 11910
	private static readonly bool _debug;
}
