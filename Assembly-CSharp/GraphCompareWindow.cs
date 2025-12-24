using System;
using System.Collections;
using System.Collections.Generic;
using db;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006BA RID: 1722
public class GraphCompareWindow : MonoBehaviour
{
	// Token: 0x0600370F RID: 14095 RVA: 0x0018E260 File Offset: 0x0018C460
	private void Awake()
	{
		foreach (object obj in this._noos_list_container)
		{
			Transform tChild = (Transform)obj;
			if (tChild.gameObject.name.StartsWith("MetaContainer"))
			{
				Object.Destroy(tChild.gameObject);
			}
		}
		this._pool_drag_objects = new ObjectPoolGenericMono<RectTransform>(this._meta_drag_object, this._noos_list_container);
		this._pool_banners = new MultiBannerPool(this._pool_banner_container);
		this._pool_drop_banners = new MultiBannerPool(this._pool_drop_banner_container);
		this._noos_button.onClick.AddListener(delegate()
		{
			this.nextNoos();
		});
	}

	// Token: 0x06003710 RID: 14096 RVA: 0x0018E32C File Offset: 0x0018C52C
	internal MultiBannerPool getDropBannerPool()
	{
		return this._pool_drop_banners;
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x0018E334 File Offset: 0x0018C534
	private void OnEnable()
	{
		ScrollWindow.addCallbackHide(new ScrollWindowNameAction(this.resetPoolsAndParents));
		this.loadNoos();
		if (!this.hasAny())
		{
			return;
		}
		if (Config.selected_objects_graph.Count == 0)
		{
			base.StartCoroutine(this.displayRandom());
			return;
		}
		base.StartCoroutine(this.displaySelected(true));
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x0018E389 File Offset: 0x0018C589
	private IEnumerator selectNoosCoroutine()
	{
		if (Config.selected_objects_graph.Count == 0)
		{
			yield break;
		}
		this.selectNoos(Config.selected_objects_graph.First());
		SoundBox.click();
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x06003713 RID: 14099 RVA: 0x0018E398 File Offset: 0x0018C598
	private IEnumerator updateGraph()
	{
		if (Config.selected_objects_graph.Count == 0)
		{
			yield break;
		}
		string tActiveCategory = this.graph_controller.getActiveCategory();
		this.graph_controller.resetAndUpdateGraph();
		this.graph_controller.tryEnableCategory(tActiveCategory);
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x06003714 RID: 14100 RVA: 0x0018E3A7 File Offset: 0x0018C5A7
	private IEnumerator displaySelected(bool pUpdate = true)
	{
		GraphCompareWindow.<displaySelected>d__24 <displaySelected>d__ = new GraphCompareWindow.<displaySelected>d__24(0);
		<displaySelected>d__.<>4__this = this;
		<displaySelected>d__.pUpdate = pUpdate;
		return <displaySelected>d__;
	}

	// Token: 0x06003715 RID: 14101 RVA: 0x0018E3BD File Offset: 0x0018C5BD
	private void OnDisable()
	{
		this.clearNoosItems();
		this.clearAsset();
	}

	// Token: 0x06003716 RID: 14102 RVA: 0x0018E3CB File Offset: 0x0018C5CB
	private void clearAsset()
	{
		this._current_asset = null;
	}

	// Token: 0x06003717 RID: 14103 RVA: 0x0018E3D4 File Offset: 0x0018C5D4
	private void loadNoos()
	{
		this._noos_list.Clear();
		foreach (HistoryMetaDataAsset iAsset in AssetManager.history_meta_data_library.list)
		{
			MetaTypeAsset tAsset = AssetManager.meta_type_library.get(iAsset.id);
			if (tAsset.has_any())
			{
				this._noos_list.Add(tAsset);
			}
		}
		this.showItems(this.hasAny());
	}

	// Token: 0x06003718 RID: 14104 RVA: 0x0018E468 File Offset: 0x0018C668
	private bool hasAny()
	{
		return this._noos_list.Count > 0;
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x0018E478 File Offset: 0x0018C678
	private void showItems(bool pShow)
	{
		Transform tContent = base.transform.FindRecursive("Content");
		for (int i = 0; i < tContent.childCount; i++)
		{
			tContent.GetChild(i).gameObject.SetActive(pShow);
		}
		this._empty_list_message.SetActive(!pShow);
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x0018E4C8 File Offset: 0x0018C6C8
	private void updateNoosIcon(MetaTypeAsset pAsset)
	{
		Sprite tImage = SpriteTextureLoader.getSprite("ui/Icons/" + pAsset.icon_list);
		this._noos_icon.sprite = tImage;
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x0018E4F7 File Offset: 0x0018C6F7
	public void clearNoosItems()
	{
		this._noos_items.Clear();
		this._pool_banners.clear();
		this._pool_drag_objects.clear(true);
	}

	// Token: 0x0600371C RID: 14108 RVA: 0x0018E51C File Offset: 0x0018C71C
	private void resetNoosList()
	{
		this._noos_list_container.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		this._noos_list_container.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		this.clearNoosItems();
	}

	// Token: 0x0600371D RID: 14109 RVA: 0x0018E570 File Offset: 0x0018C770
	private void resetPoolsAndParents(string pID)
	{
		if (pID != "chart_comparer")
		{
			return;
		}
		base.StopAllCoroutines();
		this.clearNoosItems();
		this.meta_object_1.empty();
		this.meta_object_2.empty();
		this.meta_object_3.empty();
		ScrollWindow.removeCallbackHide(new ScrollWindowNameAction(this.resetPoolsAndParents));
	}

	// Token: 0x0600371E RID: 14110 RVA: 0x0018E5C9 File Offset: 0x0018C7C9
	public IEnumerator loadNoosItemsCoroutine(bool pSilent = false)
	{
		GraphCompareWindow.<loadNoosItemsCoroutine>d__34 <loadNoosItemsCoroutine>d__ = new GraphCompareWindow.<loadNoosItemsCoroutine>d__34(0);
		<loadNoosItemsCoroutine>d__.<>4__this = this;
		<loadNoosItemsCoroutine>d__.pSilent = pSilent;
		return <loadNoosItemsCoroutine>d__;
	}

	// Token: 0x0600371F RID: 14111 RVA: 0x0018E5DF File Offset: 0x0018C7DF
	public int countNoosItems()
	{
		return this._noos_items.Count;
	}

	// Token: 0x06003720 RID: 14112 RVA: 0x0018E5EC File Offset: 0x0018C7EC
	public static int sortByUnits(NanoObject pNanoObject1, NanoObject pNanoObject2)
	{
		return ((IMetaObject)pNanoObject2).countUnits().CompareTo(((IMetaObject)pNanoObject1).countUnits());
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x0018E618 File Offset: 0x0018C818
	private void nextNoos()
	{
		int tIndex = this._noos_list.IndexOf(this._current_asset);
		tIndex = Toolbox.loopIndex(tIndex + 1, this._noos_list.Count);
		this.selectNoos(this._noos_list[tIndex]);
	}

	// Token: 0x06003722 RID: 14114 RVA: 0x0018E660 File Offset: 0x0018C860
	private void selectNoos(NanoObject pObject)
	{
		MetaTypeAsset tAsset = AssetManager.meta_type_library.get(pObject.getType());
		this.selectNoos(tAsset);
	}

	// Token: 0x06003723 RID: 14115 RVA: 0x0018E685 File Offset: 0x0018C885
	private void selectNoos(MetaTypeAsset pAsset)
	{
		if (this._current_asset == pAsset)
		{
			return;
		}
		this.clearNoosItems();
		this._current_asset = pAsset;
		this.updateNoosIcon(this._current_asset);
		this.loadNoosItems(false);
	}

	// Token: 0x06003724 RID: 14116 RVA: 0x0018E6B4 File Offset: 0x0018C8B4
	public IBanner setupBanner(NanoObject pObject, Transform pBannerArea, MultiBannerPool pBannerPool)
	{
		IBanner tBanner = pBannerPool.getNext(pObject);
		tBanner.load(pObject);
		tBanner.transform.localScale = new Vector3(1f, 1f, 1f);
		tBanner.transform.SetParent(pBannerArea);
		UiButtonHoverAnimation component = tBanner.GetComponent<UiButtonHoverAnimation>();
		component.enabled = false;
		component.scale_size = 1f;
		component.default_scale = new Vector3(1f, 1f, 1f);
		tBanner.GetComponent<TipButton>().setDefaultScale(pBannerArea.localScale);
		if (!tBanner.HasComponent<LayoutElement>())
		{
			tBanner.AddComponent<LayoutElement>().ignoreLayout = true;
		}
		RectTransform component2 = tBanner.GetComponent<RectTransform>();
		component2.SetAnchor(AnchorPresets.MiddleCenter, 0f, 0f);
		component2.localScale = new Vector3(1f, 1f, 1f);
		component2.anchoredPosition = new Vector2(0f, 0f);
		return tBanner;
	}

	// Token: 0x06003725 RID: 14117 RVA: 0x0018E798 File Offset: 0x0018C998
	private IBanner setupDragBanner(NanoObject pObject, Transform pBannerArea, MultiBannerPool pBannerPool)
	{
		IBanner tBanner = this.setupBanner(pObject, pBannerArea, pBannerPool);
		if (!tBanner.HasComponent<GraphCompareMetaSelector>())
		{
			GraphCompareMetaSelector graphCompareMetaSelector = tBanner.AddComponent<GraphCompareMetaSelector>();
			graphCompareMetaSelector.addWindow(this);
			graphCompareMetaSelector.addDropzones(new RectTransform[]
			{
				this.meta_object_1.GetComponent<RectTransform>(),
				this.meta_object_2.GetComponent<RectTransform>(),
				this.meta_object_3.GetComponent<RectTransform>()
			});
		}
		return tBanner;
	}

	// Token: 0x06003726 RID: 14118 RVA: 0x0018E7FC File Offset: 0x0018C9FC
	private ListPool<NanoObject> getPossibleItems()
	{
		ListPool<NanoObject> tPossibleItems = new ListPool<NanoObject>();
		foreach (MetaTypeAsset metaTypeAsset in this._noos_list)
		{
			foreach (NanoObject tItem in metaTypeAsset.get_list())
			{
				tPossibleItems.Add(tItem);
			}
		}
		return tPossibleItems;
	}

	// Token: 0x06003727 RID: 14119 RVA: 0x0018E890 File Offset: 0x0018CA90
	internal void loadNoosItems(bool pSilent = false)
	{
		if (this._load_noos_items != null)
		{
			base.StopCoroutine(this._load_noos_items);
		}
		this._load_noos_items = base.StartCoroutine(this.loadNoosItemsCoroutine(pSilent));
	}

	// Token: 0x06003728 RID: 14120 RVA: 0x0018E8BC File Offset: 0x0018CABC
	private void selectRandom()
	{
		using (ListPool<NanoObject> tPossibleItems = this.getPossibleItems())
		{
			Config.selected_objects_graph.Clear();
			int tMax = Mathf.Min(tPossibleItems.Count, 3);
			foreach (NanoObject tItem in tPossibleItems.LoopRandom(tMax))
			{
				Config.selected_objects_graph.Add(tItem);
			}
			if (tPossibleItems.Count <= 7)
			{
				this._stop_randomizer = true;
			}
		}
	}

	// Token: 0x06003729 RID: 14121 RVA: 0x0018E954 File Offset: 0x0018CB54
	public void randomizeSelection()
	{
		if (this._is_randomizing)
		{
			this._stop_randomizer = true;
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.displayRandom());
	}

	// Token: 0x0600372A RID: 14122 RVA: 0x0018E979 File Offset: 0x0018CB79
	private IEnumerator displayRandom()
	{
		this._is_randomizing = true;
		foreach (CanvasGroup canvasGroup in this._block_during_random)
		{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}
		int i = 0;
		while (i < 10 && !this._stop_randomizer)
		{
			this.selectRandom();
			yield return this.displaySelected(false);
			yield return this.randomizeCategories();
			yield return this.updateGraph();
			yield return this.randomNoosItems();
			this.updateNoosIcon(this._noos_list.GetRandom<MetaTypeAsset>());
			int j = i;
			i = j + 1;
		}
		yield return this.randomizeCategories();
		yield return this.randomizeTimescale();
		this.clearAsset();
		yield return this.selectNoosCoroutine();
		foreach (CanvasGroup canvasGroup2 in this._block_during_random)
		{
			canvasGroup2.interactable = true;
			canvasGroup2.blocksRaycasts = true;
		}
		this._stop_randomizer = false;
		this._is_randomizing = false;
		yield break;
	}

	// Token: 0x0600372B RID: 14123 RVA: 0x0018E988 File Offset: 0x0018CB88
	private IEnumerator randomizeCategories()
	{
		this.graph_controller.pickRandomCategory();
		SoundBox.click();
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x0600372C RID: 14124 RVA: 0x0018E997 File Offset: 0x0018CB97
	private IEnumerator randomizeTimescale()
	{
		if (!this.graph_controller.randomTimeScale())
		{
			yield break;
		}
		SoundBox.click();
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x0600372D RID: 14125 RVA: 0x0018E9A6 File Offset: 0x0018CBA6
	public IEnumerator randomNoosItems()
	{
		GraphCompareWindow.<randomNoosItems>d__52 <randomNoosItems>d__ = new GraphCompareWindow.<randomNoosItems>d__52(0);
		<randomNoosItems>d__.<>4__this = this;
		return <randomNoosItems>d__;
	}

	// Token: 0x040028CF RID: 10447
	public GraphCompareMetaObject meta_object_1;

	// Token: 0x040028D0 RID: 10448
	public GraphCompareMetaObject meta_object_2;

	// Token: 0x040028D1 RID: 10449
	public GraphCompareMetaObject meta_object_3;

	// Token: 0x040028D2 RID: 10450
	public GraphController graph_controller;

	// Token: 0x040028D3 RID: 10451
	[SerializeField]
	private GameObject _empty_list_message;

	// Token: 0x040028D4 RID: 10452
	[SerializeField]
	private RectTransform _meta_drag_object;

	// Token: 0x040028D5 RID: 10453
	private ObjectPoolGenericMono<RectTransform> _pool_drag_objects;

	// Token: 0x040028D6 RID: 10454
	private MultiBannerPool _pool_banners;

	// Token: 0x040028D7 RID: 10455
	private MultiBannerPool _pool_drop_banners;

	// Token: 0x040028D8 RID: 10456
	[SerializeField]
	private Button _noos_button;

	// Token: 0x040028D9 RID: 10457
	[SerializeField]
	private Image _noos_icon;

	// Token: 0x040028DA RID: 10458
	[SerializeField]
	private Transform _noos_list_container;

	// Token: 0x040028DB RID: 10459
	[SerializeField]
	private Transform _pool_banner_container;

	// Token: 0x040028DC RID: 10460
	[SerializeField]
	private Transform _pool_drop_banner_container;

	// Token: 0x040028DD RID: 10461
	private MetaTypeAsset _current_asset;

	// Token: 0x040028DE RID: 10462
	private List<MetaTypeAsset> _noos_list = new List<MetaTypeAsset>();

	// Token: 0x040028DF RID: 10463
	private List<NanoObject> _noos_items = new List<NanoObject>();

	// Token: 0x040028E0 RID: 10464
	private Coroutine _load_noos_items;

	// Token: 0x040028E1 RID: 10465
	private const int VISIBLE_ITEMS = 6;

	// Token: 0x040028E2 RID: 10466
	[SerializeField]
	private CanvasGroup[] _block_during_random;

	// Token: 0x040028E3 RID: 10467
	private bool _is_randomizing;

	// Token: 0x040028E4 RID: 10468
	private bool _stop_randomizer;
}
