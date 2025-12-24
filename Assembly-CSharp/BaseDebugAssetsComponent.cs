using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000550 RID: 1360
public class BaseDebugAssetsComponent<TAsset, TAssetElement, TAssetElementPlace> : MonoBehaviour where TAsset : Asset where TAssetElement : BaseDebugAssetElement<TAsset> where TAssetElementPlace : BaseAssetElementPlace<TAsset, TAssetElement>
{
	// Token: 0x06002C5E RID: 11358 RVA: 0x0015CA77 File Offset: 0x0015AC77
	protected virtual List<TAsset> getAssetsList()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x0015CA7E File Offset: 0x0015AC7E
	protected virtual List<TAsset> getListCivsSort()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x0015CA85 File Offset: 0x0015AC85
	private void OnEnable()
	{
		this.refresh();
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x0015CA8D File Offset: 0x0015AC8D
	private void Start()
	{
		this._scroll_rect_transform = this.scroll_rect.GetComponent<RectTransform>();
		this.search_input_field.onValueChanged.AddListener(new UnityAction<string>(this.setDataSearched));
		this.init();
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x0015CAC4 File Offset: 0x0015ACC4
	protected virtual void init()
	{
		this.list_assets_sorted = new List<TAsset>(this.getAssetsList());
		this.list_assets_sorting = new List<TAsset>(this.getAssetsList());
		this.list_assets_sorting_default = new List<TAsset>(this.getAssetsList());
		foreach (object obj in base.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.list_places = new List<TAssetElementPlace>();
		foreach (TAsset tAsset in this.getAssetsList())
		{
			TAssetElementPlace tPlace = Object.Instantiate<TAssetElementPlace>(this.place_prefab, base.transform);
			this.list_places.Add(tPlace);
			tPlace.setData(tAsset, this.element_prefab);
		}
		this.sorting_tab.addButton("ui/Icons/iconHumans", "sort_by_civs", new SortButtonAction(this.setDataResorted), delegate
		{
			this.list_assets_sorted = this.getListCivsSort();
		});
		this.sorting_tab.addButton("ui/Icons/actor_traits/iconClumsy", "default_sort", new SortButtonAction(this.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting_default;
			if (this.sorting_tab.getCurrentButton().getState() != SortButtonState.Down && !this.default_sort_reversed)
			{
				return;
			}
			this.default_sort_reversed = !this.default_sort_reversed;
			this.list_assets_sorted.Reverse();
		}).click();
		this._initialized = true;
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x0015CC34 File Offset: 0x0015AE34
	private void Update()
	{
		if (!this._initialized)
		{
			return;
		}
		this._scroll_world_rect = this._scroll_rect_transform.GetWorldRect();
		foreach (TAssetElementPlace tPlace in this.list_places)
		{
			if (tPlace.game_object_cache.activeSelf)
			{
				if (tPlace.element != null)
				{
					tPlace.element.update();
				}
				this.checkVisible(tPlace);
			}
		}
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x0015CCE0 File Offset: 0x0015AEE0
	private void checkVisible(TAssetElementPlace pPlace)
	{
		if (!pPlace.gameObject.activeSelf)
		{
			return;
		}
		bool tIsVisible = this.isElementVisible(pPlace);
		if (!tIsVisible && pPlace.has_element)
		{
			pPlace.clear();
			return;
		}
		if (tIsVisible && !pPlace.has_element)
		{
			TAsset tAsset = this.list_assets_sorted[pPlace.rect_transform.GetSiblingIndex()];
			pPlace.setData(tAsset, this.element_prefab);
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x0015CD62 File Offset: 0x0015AF62
	public void refresh()
	{
		if (!this._initialized)
		{
			return;
		}
		this.setDataResorted();
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x0015CD73 File Offset: 0x0015AF73
	public bool isElementVisible(TAssetElementPlace pPlace)
	{
		return this._scroll_world_rect.Overlaps(pPlace.rect_transform.GetWorldRect());
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x0015CD90 File Offset: 0x0015AF90
	protected void setDataResorted()
	{
		int tLastIndex = this.list_assets_sorted.Count - 1;
		for (int i = 0; i < this.list_places.Count; i++)
		{
			TAssetElementPlace tPlace = this.list_places[i];
			if (i > tLastIndex)
			{
				tPlace.game_object_cache.SetActive(false);
				tPlace.allowed_for_search = false;
			}
			else
			{
				tPlace.game_object_cache.SetActive(true);
				tPlace.allowed_for_search = true;
				if (this.isElementVisible(tPlace) && tPlace.has_element)
				{
					TAsset tAsset = this.list_assets_sorted[i];
					tPlace.element.setData(tAsset);
				}
			}
		}
		this.setDataSearched(this.search_input_field.text);
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x0015CE5E File Offset: 0x0015B05E
	protected void checkReverseSort()
	{
		if (this.sorting_tab.getCurrentButton().getState() != SortButtonState.Down)
		{
			return;
		}
		this.list_assets_sorted.Reverse();
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x0015CE80 File Offset: 0x0015B080
	private void setDataSearched(string pValue)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		pValue = pValue.ToLower();
		if (string.IsNullOrEmpty(pValue))
		{
			foreach (TAssetElementPlace tPlace in this.list_places)
			{
				if (tPlace.allowed_for_search)
				{
					tPlace.game_object_cache.SetActive(true);
				}
			}
			return;
		}
		for (int i = 0; i < this.list_assets_sorted.Count; i++)
		{
			TAssetElementPlace tPlace2 = this.list_places[i];
			if (tPlace2.allowed_for_search)
			{
				bool tContains = this.list_assets_sorted[i].id.ToLower().Contains(pValue);
				tPlace2.game_object_cache.SetActive(tContains);
			}
		}
	}

	// Token: 0x0400220B RID: 8715
	public TAssetElementPlace place_prefab;

	// Token: 0x0400220C RID: 8716
	public TAssetElement element_prefab;

	// Token: 0x0400220D RID: 8717
	public ScrollRect scroll_rect;

	// Token: 0x0400220E RID: 8718
	private RectTransform _scroll_rect_transform;

	// Token: 0x0400220F RID: 8719
	private Rect _scroll_world_rect;

	// Token: 0x04002210 RID: 8720
	public InputField search_input_field;

	// Token: 0x04002211 RID: 8721
	public SortingTab sorting_tab;

	// Token: 0x04002212 RID: 8722
	protected List<TAsset> list_assets_sorted;

	// Token: 0x04002213 RID: 8723
	protected List<TAsset> list_assets_sorting;

	// Token: 0x04002214 RID: 8724
	protected List<TAsset> list_assets_sorting_default;

	// Token: 0x04002215 RID: 8725
	protected bool default_sort_reversed;

	// Token: 0x04002216 RID: 8726
	protected List<TAssetElementPlace> list_places;

	// Token: 0x04002217 RID: 8727
	private bool _initialized;
}
