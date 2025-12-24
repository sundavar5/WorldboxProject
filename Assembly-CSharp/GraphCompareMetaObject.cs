using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020006B8 RID: 1720
public class GraphCompareMetaObject : MonoBehaviour, IDropHandler, IEventSystemHandler
{
	// Token: 0x060036ED RID: 14061 RVA: 0x0018D8D7 File Offset: 0x0018BAD7
	public void Awake()
	{
		this.init();
	}

	// Token: 0x060036EE RID: 14062 RVA: 0x0018D8DF File Offset: 0x0018BADF
	private void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._graph_window = base.GetComponentInParent<GraphCompareWindow>();
		this._graph_controller = this._graph_window.graph_controller;
		this._pool_drop_banners = this._graph_window.getDropBannerPool();
	}

	// Token: 0x060036EF RID: 14063 RVA: 0x0018D91F File Offset: 0x0018BB1F
	public void OnEnable()
	{
		if (this.current_item == null)
		{
			this.empty_drop_icon.SetActive(true);
			this.meta_title.setKeyAndUpdate("graph_drop_to_compare");
			this.meta_name.gameObject.SetActive(false);
		}
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x0018D956 File Offset: 0x0018BB56
	public void Update()
	{
		if (this._disable_raycasts == GraphCompareMetaObject.disable_raycasts)
		{
			return;
		}
		this._disable_raycasts = GraphCompareMetaObject.disable_raycasts;
		if (GraphCompareMetaObject.disable_raycasts)
		{
			this.disableRaycastChildren();
			return;
		}
		this.enableRaycastChildren();
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x0018D988 File Offset: 0x0018BB88
	public void disableRaycastChildren()
	{
		this._raycast_children.Clear();
		foreach (Graphic tGraphic in base.GetComponentsInChildren<Graphic>())
		{
			if (!(tGraphic.gameObject == base.gameObject) && tGraphic.raycastTarget)
			{
				this._raycast_children.Add(tGraphic);
				tGraphic.raycastTarget = false;
			}
		}
	}

	// Token: 0x060036F2 RID: 14066 RVA: 0x0018D9E8 File Offset: 0x0018BBE8
	public void enableRaycastChildren()
	{
		foreach (Graphic graphic in this._raycast_children)
		{
			graphic.raycastTarget = true;
		}
		this._raycast_children.Clear();
	}

	// Token: 0x060036F3 RID: 14067 RVA: 0x0018DA44 File Offset: 0x0018BC44
	public void OnDrop(PointerEventData pEventData)
	{
		if (pEventData.pointerDrag == null)
		{
			return;
		}
		BannerBase tBannerBase = pEventData.pointerDrag.GetComponent<BannerBase>();
		if (tBannerBase == null)
		{
			return;
		}
		GraphCompareMetaSelector tSelector = pEventData.pointerDrag.GetComponent<GraphCompareMetaSelector>();
		if (tSelector == null)
		{
			return;
		}
		if (!tSelector.isBeingDragged())
		{
			return;
		}
		tSelector.OnEndDrag(pEventData);
		SoundBox.click();
		this.setObjectAndUpdate(tBannerBase.GetNanoObject());
		pEventData.Use();
	}

	// Token: 0x060036F4 RID: 14068 RVA: 0x0018DAB3 File Offset: 0x0018BCB3
	public void empty()
	{
		this.init();
		this.clearObject();
		this.empty_drop_icon.SetActive(true);
	}

	// Token: 0x060036F5 RID: 14069 RVA: 0x0018DACD File Offset: 0x0018BCCD
	public void clear()
	{
		this.init();
		Config.selected_objects_graph.Remove(this.current_item);
		this.clearObject();
		this.empty_drop_icon.SetActive(true);
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x0018DAF7 File Offset: 0x0018BCF7
	public void clearAndSetObject(NanoObject pObject)
	{
		this.clear();
		this.setObject(pObject);
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x0018DB08 File Offset: 0x0018BD08
	public void setObject(NanoObject pObject)
	{
		if (pObject.isRekt())
		{
			return;
		}
		this.empty_drop_icon.SetActive(false);
		this.current_item = pObject;
		this._current_banner = this._graph_window.setupBanner(this.current_item, base.transform, this._pool_drop_banners);
		this._current_banner.jump(0.1f, false);
		this._current_banner.GetComponent<Button>().onClick.AddListener(new UnityAction(this.removeOnClick));
		if (!Config.selected_objects_graph.Contains(this.current_item))
		{
			Config.selected_objects_graph.Add(this.current_item);
		}
		Color tColor = this.current_item.getColor().getColorText();
		this.meta_title.text.color = tColor;
		MetaCustomizationAsset tCurrentMetaAsset = AssetManager.meta_customization_library.getAsset(this.current_item.getMetaType());
		this.meta_title.setKeyAndUpdate(tCurrentMetaAsset.localization_title);
		this.meta_name.gameObject.SetActive(true);
		this.meta_name.text = this.current_item.name;
		this.meta_name.color = tColor;
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x0018DC24 File Offset: 0x0018BE24
	private void setObjectAndUpdate(NanoObject pObject)
	{
		string tActiveCategory = this._graph_controller.getActiveCategory();
		this.clearAndSetObject(pObject);
		this._graph_window.loadNoosItems(true);
		this._graph_controller.resetAndUpdateGraph();
		this._graph_controller.tryEnableCategory(tActiveCategory);
	}

	// Token: 0x060036F9 RID: 14073 RVA: 0x0018DC67 File Offset: 0x0018BE67
	private void removeOnClick()
	{
		SoundBox.click();
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this._current_banner))
		{
			this._current_banner.showTooltip();
			return;
		}
		this.setObjectAndUpdate(null);
	}

	// Token: 0x060036FA RID: 14074 RVA: 0x0018DC98 File Offset: 0x0018BE98
	private void clearObject()
	{
		if (this.current_item == null)
		{
			return;
		}
		this.releaseChild();
		this.current_item = null;
		this.meta_title.text.color = Toolbox.color_text_default;
		this.meta_name.color = Toolbox.color_text_default;
		this.meta_name.gameObject.SetActive(false);
	}

	// Token: 0x060036FB RID: 14075 RVA: 0x0018DCF4 File Offset: 0x0018BEF4
	private void releaseChild()
	{
		if (this._current_banner == null)
		{
			return;
		}
		this._current_banner.GetComponent<Button>().onClick.RemoveListener(new UnityAction(this.removeOnClick));
		this._pool_drop_banners.resetParent(this._current_banner);
		this._pool_drop_banners.release(this._current_banner);
		this._current_banner = null;
	}

	// Token: 0x040028BA RID: 10426
	private GraphCompareWindow _graph_window;

	// Token: 0x040028BB RID: 10427
	private GraphController _graph_controller;

	// Token: 0x040028BC RID: 10428
	private MultiBannerPool _pool_drop_banners;

	// Token: 0x040028BD RID: 10429
	public NanoObject current_item;

	// Token: 0x040028BE RID: 10430
	public GameObject empty_drop_icon;

	// Token: 0x040028BF RID: 10431
	public LocalizedText meta_title;

	// Token: 0x040028C0 RID: 10432
	public Text meta_name;

	// Token: 0x040028C1 RID: 10433
	private IBanner _current_banner;

	// Token: 0x040028C2 RID: 10434
	public static bool disable_raycasts;

	// Token: 0x040028C3 RID: 10435
	private bool _disable_raycasts;

	// Token: 0x040028C4 RID: 10436
	private List<Graphic> _raycast_children = new List<Graphic>();

	// Token: 0x040028C5 RID: 10437
	private bool _initialized;
}
