using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007D0 RID: 2000
public class ComponentListBase<TListElement, TMetaObject, TData, TComponent> : MonoBehaviour, IComponentList, IShouldRefreshWindow where TListElement : WindowListElementBase<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData where TComponent : ComponentListBase<TListElement, TMetaObject, TData, TComponent>
{
	// Token: 0x170003AA RID: 938
	// (get) Token: 0x06003F0F RID: 16143 RVA: 0x001B4787 File Offset: 0x001B2987
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06003F10 RID: 16144 RVA: 0x001B478E File Offset: 0x001B298E
	private MetaTypeAsset _meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x001B47A0 File Offset: 0x001B29A0
	protected virtual IEnumerable<TMetaObject> getObjectsList()
	{
		return this.get_objects_delegate((TComponent)((object)this));
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06003F12 RID: 16146 RVA: 0x001B47B3 File Offset: 0x001B29B3
	protected virtual bool change_asset_sort_order
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x001B47B6 File Offset: 0x001B29B6
	protected ObjectPoolGenericMono<BaseEmptyListMono> getPoolEmpty()
	{
		return this._pool_empty_elements;
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x001B47BE File Offset: 0x001B29BE
	private void checkCreate()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.create();
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x001B47D8 File Offset: 0x001B29D8
	protected virtual void create()
	{
		this._pool_elements = new ObjectPoolGenericMono<TListElement>(this.element_prefab, this.list_transform);
		this._element_height = this.element_prefab.transform.GetComponent<RectTransform>().sizeDelta.y;
		this.addEmptyPoolSystem();
		this.showSortingTabs();
	}

	// Token: 0x06003F16 RID: 16150 RVA: 0x001B482D File Offset: 0x001B2A2D
	protected virtual void setupSortingTabs()
	{
	}

	// Token: 0x06003F17 RID: 16151 RVA: 0x001B482F File Offset: 0x001B2A2F
	protected virtual void showSortingTabs()
	{
		this.sorting_tab.clearButtons();
		this.setupSortingTabs();
		this.sorting_tab.enableFirstIfNone();
	}

	// Token: 0x06003F18 RID: 16152 RVA: 0x001B484D File Offset: 0x001B2A4D
	private void OnRenderObject()
	{
		this.autolayout_done = true;
	}

	// Token: 0x06003F19 RID: 16153 RVA: 0x001B4858 File Offset: 0x001B2A58
	private void LateUpdate()
	{
		if (!this.autolayout_done)
		{
			return;
		}
		IReadOnlyList<BaseEmptyListMono> tList = this._pool_empty_elements.getListTotal();
		int tFirstVisible = int.MaxValue;
		int tLastVisible = int.MinValue;
		float tScrollRectBottom = this.scroll_rect.content.localPosition.y;
		float tScrollRectTop = tScrollRectBottom + this.scroll_rect.viewport.rect.height;
		for (int i = 0; i < tList.Count; i++)
		{
			BaseEmptyListMono tEmptyMono = tList[i];
			if (tEmptyMono.gameObject.activeSelf)
			{
				if (this.IsVisibleInScrollRect(tEmptyMono.rect_transform, this.scroll_rect, tScrollRectTop, tScrollRectBottom))
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
		if (ComponentListBase<TListElement, TMetaObject, TData, TComponent>._debug)
		{
			this.debugUpdateElementNames(tList, tScrollRectTop, tScrollRectBottom);
		}
	}

	// Token: 0x06003F1A RID: 16154 RVA: 0x001B49C4 File Offset: 0x001B2BC4
	private void makeElementVisible(BaseEmptyListMono pEmptyMono)
	{
		TListElement tElement = this._pool_elements.getNext();
		tElement.show((TMetaObject)((object)pEmptyMono.meta_object));
		tElement.transform.SetParent(pEmptyMono.transform);
		tElement.transform.localPosition = Vector3.zero;
		pEmptyMono.assignElement(tElement);
	}

	// Token: 0x06003F1B RID: 16155 RVA: 0x001B4A2C File Offset: 0x001B2C2C
	private bool IsVisibleInScrollRect(RectTransform pRectTransform, ScrollRect pScrollRect, float pScrollRectTop, float pScrollRectBottom)
	{
		Vector2 tLocal = pRectTransform.localPosition;
		tLocal *= -1f;
		float tHeight = pRectTransform.sizeDelta.y * 0.6f;
		return tLocal.y <= pScrollRectTop + tHeight + base.transform.localPosition.y && tLocal.y >= pScrollRectBottom - tHeight + base.transform.localPosition.y;
	}

	// Token: 0x06003F1C RID: 16156 RVA: 0x001B4AA4 File Offset: 0x001B2CA4
	private void addEmptyPoolSystem()
	{
		BaseEmptyListMono tEmptyObject = Resources.Load<BaseEmptyListMono>("ui/list_element_empty");
		tEmptyObject = Object.Instantiate<BaseEmptyListMono>(tEmptyObject, this.list_transform);
		tEmptyObject.gameObject.SetActive(false);
		LayoutElement tLayoutElement;
		if (this._element_height > 0f && tEmptyObject.TryGetComponent<LayoutElement>(out tLayoutElement))
		{
			tLayoutElement.minHeight = this._element_height;
		}
		this._pool_empty_elements = new ObjectPoolGenericMono<BaseEmptyListMono>(tEmptyObject, this.list_transform);
	}

	// Token: 0x06003F1D RID: 16157 RVA: 0x001B4B0A File Offset: 0x001B2D0A
	private void showElement(TMetaObject pObject)
	{
		this._pool_empty_elements.getNext().assignObject(pObject);
	}

	// Token: 0x06003F1E RID: 16158 RVA: 0x001B4B22 File Offset: 0x001B2D22
	protected static IEnumerable<TMetaObject> getObjects(ComponentListBase<TListElement, TMetaObject, TData, TComponent> pComponentList)
	{
		IEnumerable<TMetaObject> tList = pComponentList._meta_type_asset.get_list().Cast<TMetaObject>();
		foreach (TMetaObject tMeta in pComponentList.getFiltered(tList))
		{
			yield return tMeta;
		}
		IEnumerator<TMetaObject> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06003F1F RID: 16159 RVA: 0x001B4B32 File Offset: 0x001B2D32
	protected virtual IEnumerable<TMetaObject> getFiltered(IEnumerable<TMetaObject> pList)
	{
		switch (this.getCurrentFilter())
		{
		case ListItemsFilter.Favorites:
		{
			foreach (TMetaObject tMeta in pList)
			{
				if (tMeta.isFavorite())
				{
					yield return tMeta;
				}
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		case ListItemsFilter.Dead:
		{
			foreach (TMetaObject tMeta2 in pList)
			{
				if (tMeta2.hasDied())
				{
					yield return tMeta2;
				}
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		case ListItemsFilter.OnlyAlive:
		{
			foreach (TMetaObject tMeta3 in pList)
			{
				if (!tMeta3.hasDied())
				{
					yield return tMeta3;
				}
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		default:
		{
			foreach (TMetaObject tMeta4 in pList)
			{
				yield return tMeta4;
			}
			IEnumerator<TMetaObject> enumerator = null;
			break;
		}
		}
		yield break;
		yield break;
	}

	// Token: 0x06003F20 RID: 16160 RVA: 0x001B4B49 File Offset: 0x001B2D49
	private void OnEnable()
	{
		this.checkCreate();
		this.showSortingTabs();
		this.show();
	}

	// Token: 0x06003F21 RID: 16161 RVA: 0x001B4B60 File Offset: 0x001B2D60
	protected virtual void show()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.clear();
		this.latest_counted = 0;
		if (this.isEmpty())
		{
			if (this.no_items != null)
			{
				this.no_items.SetActive(true);
			}
		}
		else
		{
			if (this.no_items != null)
			{
				this.no_items.SetActive(false);
			}
			this.showElements();
			this.latest_counted = this._pool_empty_elements.countActive();
		}
		if (this._title_counter != null)
		{
			this._title_counter.text = this.latest_counted.ToString();
		}
		if (this._favorites_counter != null)
		{
			this._favorites_counter.text = this.latest_counted.ToString();
		}
		if (this._dead_counter != null)
		{
			this._dead_counter.text = this.latest_counted.ToString();
		}
		this._pool_empty_elements.disableInactive();
		ScrollWindow.checkElements();
	}

	// Token: 0x06003F22 RID: 16162 RVA: 0x001B4C54 File Offset: 0x001B2E54
	public ListPool<NanoObject> getElements()
	{
		this.meta_list.Clear();
		this.meta_list.AddRange(this.getObjectsList());
		this.meta_list.Sort((NanoObject a, NanoObject b) => this.current_sort(a as TMetaObject, b as TMetaObject));
		SortButton currentButton = this.sorting_tab.getCurrentButton();
		if (currentButton != null && currentButton.getState() == SortButtonState.Down)
		{
			this.meta_list.Reverse();
		}
		return new ListPool<NanoObject>(this.meta_list);
	}

	// Token: 0x06003F23 RID: 16163 RVA: 0x001B4CC8 File Offset: 0x001B2EC8
	protected void showElements()
	{
		using (ListPool<NanoObject> tTempList = this.getElements())
		{
			for (int i = 0; i < tTempList.Count; i++)
			{
				NanoObject tObject = tTempList[i];
				this.showElement(tObject as TMetaObject);
			}
			if (this.change_asset_sort_order)
			{
				this._meta_type_asset.setListGetter(new MetaTypeListPoolAction(this.getElements));
			}
		}
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x001B4D44 File Offset: 0x001B2F44
	public virtual bool isEmpty()
	{
		IEnumerable<TMetaObject> tList = this.getObjectsList();
		return tList == null || !tList.Any<TMetaObject>();
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x001B4D68 File Offset: 0x001B2F68
	public virtual void clear()
	{
		IReadOnlyList<BaseEmptyListMono> tList = this._pool_empty_elements.getListTotal();
		for (int ii = 0; ii < tList.Count; ii++)
		{
			BaseEmptyListMono tEmptyMono = tList[ii];
			this.releaseElement(tEmptyMono);
			tEmptyMono.clearObject();
		}
		this._pool_empty_elements.clear(true);
		this._pool_elements.resetParent();
		this.meta_list.Clear();
		this._meta_type_asset.setListGetter(null);
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x001B4DD8 File Offset: 0x001B2FD8
	private void releaseElement(BaseEmptyListMono pEmptyMono)
	{
		if (!pEmptyMono.hasElement())
		{
			return;
		}
		TListElement tElement = (TListElement)((object)pEmptyMono.element);
		pEmptyMono.clearElement();
		this._pool_elements.release(tElement, true);
	}

	// Token: 0x06003F27 RID: 16167 RVA: 0x001B4E10 File Offset: 0x001B3010
	private void debugUpdateElementNames(IReadOnlyList<BaseEmptyListMono> pList, float pScrollRectTop, float pScrollRectBottom)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			BaseEmptyListMono tEmptyMono = pList[i];
			bool tIsVisible = this.IsVisibleInScrollRect(tEmptyMono.rect_transform, this.scroll_rect, pScrollRectTop, pScrollRectBottom);
			tEmptyMono.debugUpdateName(tIsVisible);
		}
	}

	// Token: 0x06003F28 RID: 16168 RVA: 0x001B4E52 File Offset: 0x001B3052
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003F29 RID: 16169 RVA: 0x001B4E5A File Offset: 0x001B305A
	public void setShowFavoritesOnly()
	{
		this._show_items = ListItemsFilter.Favorites;
	}

	// Token: 0x06003F2A RID: 16170 RVA: 0x001B4E63 File Offset: 0x001B3063
	public void setShowAll()
	{
		this._show_items = ListItemsFilter.All;
	}

	// Token: 0x06003F2B RID: 16171 RVA: 0x001B4E6C File Offset: 0x001B306C
	public void setShowDeadOnly()
	{
		this._show_items = ListItemsFilter.Dead;
	}

	// Token: 0x06003F2C RID: 16172 RVA: 0x001B4E75 File Offset: 0x001B3075
	public void setShowAliveOnly()
	{
		this._show_items = ListItemsFilter.OnlyAlive;
	}

	// Token: 0x06003F2D RID: 16173 RVA: 0x001B4E7E File Offset: 0x001B307E
	public virtual void setDefault()
	{
	}

	// Token: 0x06003F2E RID: 16174 RVA: 0x001B4E80 File Offset: 0x001B3080
	public ListItemsFilter getCurrentFilter()
	{
		return this._show_items;
	}

	// Token: 0x06003F2F RID: 16175 RVA: 0x001B4E88 File Offset: 0x001B3088
	public void init(GameObject pNoItems, SortingTab pSortingTab, GameObject pListElementPrefab, Transform pListTransform, ScrollRect pScrollRect, Text pTitleCounter, Text pFavoritesCounter, Text pDeadCounter)
	{
		this.no_items = pNoItems;
		this.sorting_tab = pSortingTab;
		this.element_prefab = pListElementPrefab.GetComponent<TListElement>();
		this.list_transform = pListTransform;
		this.scroll_rect = pScrollRect;
		this._title_counter = pTitleCounter;
		this._favorites_counter = pFavoritesCounter;
		this._dead_counter = pDeadCounter;
	}

	// Token: 0x06003F30 RID: 16176 RVA: 0x001B4ED8 File Offset: 0x001B30D8
	public virtual bool checkRefreshWindow()
	{
		using (List<NanoObject>.Enumerator enumerator = this.meta_list.GetEnumerator())
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

	// Token: 0x06003F31 RID: 16177 RVA: 0x001B4F34 File Offset: 0x001B3134
	protected void genericMetaSortByAge(Comparison<TMetaObject> pAction)
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconAge", "sort_by_age", new SortButtonAction(this.show), delegate
		{
			this.current_sort = pAction;
		});
	}

	// Token: 0x06003F32 RID: 16178 RVA: 0x001B4F84 File Offset: 0x001B3184
	protected void genericMetaSortByRenown(Comparison<TMetaObject> pAction)
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconRenown", "sort_by_renown", new SortButtonAction(this.show), delegate
		{
			this.current_sort = pAction;
		});
	}

	// Token: 0x06003F33 RID: 16179 RVA: 0x001B4FD4 File Offset: 0x001B31D4
	protected void genericMetaSortByPopulation(Comparison<TMetaObject> pAction)
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconPopulation", "sort_by_members", new SortButtonAction(this.show), delegate
		{
			this.current_sort = pAction;
		});
	}

	// Token: 0x06003F34 RID: 16180 RVA: 0x001B5024 File Offset: 0x001B3224
	protected void genericMetaSortByKills(Comparison<TMetaObject> pAction)
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconKills", "sort_by_kills", new SortButtonAction(this.show), delegate
		{
			this.current_sort = pAction;
		});
	}

	// Token: 0x06003F35 RID: 16181 RVA: 0x001B5074 File Offset: 0x001B3274
	protected void genericMetaSortByDeath(Comparison<TMetaObject> pAction)
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconDead", "sort_by_dead", new SortButtonAction(this.show), delegate
		{
			this.current_sort = pAction;
		});
	}

	// Token: 0x06003F36 RID: 16182 RVA: 0x001B50C4 File Offset: 0x001B32C4
	protected int sortByRenown(IMetaObject p1, IMetaObject p2)
	{
		return p2.getRenown().CompareTo(p1.getRenown());
	}

	// Token: 0x06003F37 RID: 16183 RVA: 0x001B50E8 File Offset: 0x001B32E8
	protected int sortByAge(IMetaObject p1, IMetaObject p2)
	{
		return -p2.getMetaData().created_time.CompareTo(p1.getMetaData().created_time);
	}

	// Token: 0x06003F38 RID: 16184 RVA: 0x001B5114 File Offset: 0x001B3314
	public static int sortByPopulation(IMetaObject p1, IMetaObject p2)
	{
		return p2.getPopulationPeople().CompareTo(p1.getPopulationPeople());
	}

	// Token: 0x06003F39 RID: 16185 RVA: 0x001B5138 File Offset: 0x001B3338
	public static int sortByKills(IMetaObject p1, IMetaObject p2)
	{
		return p2.getTotalKills().CompareTo(p1.getTotalKills());
	}

	// Token: 0x06003F3A RID: 16186 RVA: 0x001B515C File Offset: 0x001B335C
	public static int sortByDeaths(IMetaObject p1, IMetaObject p2)
	{
		return p2.getTotalDeaths().CompareTo(p1.getTotalDeaths());
	}

	// Token: 0x04002DE3 RID: 11747
	public GameObject no_items;

	// Token: 0x04002DE4 RID: 11748
	public SortingTab sorting_tab;

	// Token: 0x04002DE5 RID: 11749
	public TListElement element_prefab;

	// Token: 0x04002DE6 RID: 11750
	public Transform list_transform;

	// Token: 0x04002DE7 RID: 11751
	public ScrollRect scroll_rect;

	// Token: 0x04002DE8 RID: 11752
	[SerializeField]
	private Text _title_counter;

	// Token: 0x04002DE9 RID: 11753
	[SerializeField]
	private Text _favorites_counter;

	// Token: 0x04002DEA RID: 11754
	[SerializeField]
	private Text _dead_counter;

	// Token: 0x04002DEB RID: 11755
	private ListItemsFilter _show_items;

	// Token: 0x04002DEC RID: 11756
	public GetListOfObjectsFunc<TListElement, TMetaObject, TData, TComponent> get_objects_delegate = new GetListOfObjectsFunc<TListElement, TMetaObject, TData, TComponent>(ComponentListBase<TListElement, TMetaObject, TData, TComponent>.getObjects);

	// Token: 0x04002DED RID: 11757
	private ObjectPoolGenericMono<TListElement> _pool_elements;

	// Token: 0x04002DEE RID: 11758
	private ObjectPoolGenericMono<BaseEmptyListMono> _pool_empty_elements;

	// Token: 0x04002DEF RID: 11759
	protected Comparison<TMetaObject> current_sort;

	// Token: 0x04002DF0 RID: 11760
	public readonly List<NanoObject> meta_list = new List<NanoObject>();

	// Token: 0x04002DF1 RID: 11761
	private bool autolayout_done;

	// Token: 0x04002DF2 RID: 11762
	private const int PADDING_ELEMENTS = 3;

	// Token: 0x04002DF3 RID: 11763
	private static readonly bool _debug;

	// Token: 0x04002DF4 RID: 11764
	private bool _created;

	// Token: 0x04002DF5 RID: 11765
	protected int latest_counted;

	// Token: 0x04002DF6 RID: 11766
	private float _element_height;
}
