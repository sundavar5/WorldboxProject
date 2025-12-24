using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000297 RID: 663
public class ListWindow : MonoBehaviour
{
	// Token: 0x06001932 RID: 6450 RVA: 0x000EEBF4 File Offset: 0x000ECDF4
	private void Awake()
	{
		this._asset = AssetManager.list_window_library.getByMetaType(this._meta_type);
		this._list_transform.gameObject.SetActive(false);
		IComponentList tComponent = this._asset.set_list_component(this._list_transform);
		this.initComponent(tComponent);
		this.initTabsCallbacks(tComponent);
		this._tab_list.tab_action.AddListener(delegate(WindowMetaTab _)
		{
			this._no_items_description.setKeyAndUpdate(this._asset.no_items_locale);
		});
		this._no_items_description.setKeyAndUpdate(this._asset.no_items_locale);
		if (this._statistics != null)
		{
			this._statistics.meta_type = this._meta_type;
		}
		this._list_transform.gameObject.SetActive(true);
		this._art.sprite = SpriteTextureLoader.getSprite(this._asset.art_path);
		Sprite tIcon = SpriteTextureLoader.getSprite(this._asset.icon_path);
		this._tab_list_icon.sprite = tIcon;
		this._title_icon_left.sprite = tIcon;
		this._title_icon_right.sprite = tIcon;
		this._no_items_icon_left.sprite = tIcon;
		this._no_items_icon_right.sprite = tIcon;
		if (this._breakdown != null)
		{
			this._breakdown.setMetaType(this._meta_type);
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x000EED38 File Offset: 0x000ECF38
	protected virtual void initComponent(IComponentList pComponent)
	{
		pComponent.init(this._no_items, this._sorting_tab, this._list_element_prefab, this._list_transform, this._scroll_rect, this._title_counter, this._favorites_counter, this._dead_counter);
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x000EED7C File Offset: 0x000ECF7C
	protected virtual void initTabsCallbacks(IComponentList pComponent)
	{
		bool tHasFavorites = this._tab_favorite != null && this._tab_favorite.gameObject.activeSelf;
		bool tHasDead = this._tab_dead != null && this._tab_dead.gameObject.activeSelf;
		if (tHasFavorites || tHasDead)
		{
			this.setTabCallbacks(this._tab_list, new Action(pComponent.setShowAll), new Action(pComponent.setDefault));
			if (tHasFavorites)
			{
				this.setTabCallbacks(this._tab_favorite, new Action(pComponent.setShowFavoritesOnly), new Action(pComponent.setDefault));
				this._tab_favorite.tab_action.AddListener(delegate(WindowMetaTab _)
				{
					this._no_items_description.setKeyAndUpdate("empty_favorites_list");
				});
			}
			if (tHasDead)
			{
				this.setTabCallbacks(this._tab_list, new Action(pComponent.setShowAliveOnly), new Action(pComponent.setDefault));
				this.setTabCallbacks(this._tab_dead, new Action(pComponent.setShowDeadOnly), new Action(pComponent.setDefault));
				this._tab_dead.tab_action.AddListener(delegate(WindowMetaTab _)
				{
					this._no_items_description.setKeyAndUpdate(this._asset.no_dead_items_locale);
				});
				return;
			}
		}
		else
		{
			this._tab_list.tab_action.AddListener(delegate(WindowMetaTab _)
			{
				this._tabs_container.showTab(this._tab_list);
			});
		}
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000EEECC File Offset: 0x000ED0CC
	protected void setTabCallbacks(WindowMetaTab pTab, Action pCallback, Action pDefaultCallback = null)
	{
		pTab.tab_action.RemoveAllListeners();
		if (pDefaultCallback != null)
		{
			pTab.tab_action.AddListener(delegate(WindowMetaTab _)
			{
				pDefaultCallback();
			});
		}
		pTab.tab_action.AddListener(delegate(WindowMetaTab _)
		{
			pCallback();
		});
		pTab.tab_action.AddListener(delegate(WindowMetaTab p)
		{
			this._tabs_container.showTab(p);
		});
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x000EEF4C File Offset: 0x000ED14C
	protected LocalizedText getNoItems()
	{
		return this._no_items_description;
	}

	// Token: 0x040013BC RID: 5052
	[SerializeField]
	private Transform _content_list;

	// Token: 0x040013BD RID: 5053
	[SerializeField]
	private WindowMetaTab _tab_list;

	// Token: 0x040013BE RID: 5054
	[SerializeField]
	private WindowMetaTab _tab_favorite;

	// Token: 0x040013BF RID: 5055
	[SerializeField]
	private WindowMetaTab _tab_dead;

	// Token: 0x040013C0 RID: 5056
	[SerializeField]
	private WindowMetaTabButtonsContainer _tabs_container;

	// Token: 0x040013C1 RID: 5057
	[SerializeField]
	private Image _art;

	// Token: 0x040013C2 RID: 5058
	[SerializeField]
	private Image _tab_list_icon;

	// Token: 0x040013C3 RID: 5059
	[SerializeField]
	private Image _title_icon_left;

	// Token: 0x040013C4 RID: 5060
	[SerializeField]
	private Image _title_icon_right;

	// Token: 0x040013C5 RID: 5061
	[SerializeField]
	private Image _no_items_icon_left;

	// Token: 0x040013C6 RID: 5062
	[SerializeField]
	private Image _no_items_icon_right;

	// Token: 0x040013C7 RID: 5063
	[SerializeField]
	private TipButton _tip_button_favorite;

	// Token: 0x040013C8 RID: 5064
	[SerializeField]
	private LocalizedText _no_items_description;

	// Token: 0x040013C9 RID: 5065
	[SerializeField]
	private ListWindowStatistics _statistics;

	// Token: 0x040013CA RID: 5066
	[SerializeField]
	private MetaRepresentationTotal _breakdown;

	// Token: 0x040013CB RID: 5067
	[SerializeField]
	private GameObject _no_items;

	// Token: 0x040013CC RID: 5068
	[SerializeField]
	private SortingTab _sorting_tab;

	// Token: 0x040013CD RID: 5069
	[SerializeField]
	private Transform _list_transform;

	// Token: 0x040013CE RID: 5070
	[SerializeField]
	private ScrollRect _scroll_rect;

	// Token: 0x040013CF RID: 5071
	[SerializeField]
	private Text _title_counter;

	// Token: 0x040013D0 RID: 5072
	[SerializeField]
	private Text _favorites_counter;

	// Token: 0x040013D1 RID: 5073
	[SerializeField]
	private Text _dead_counter;

	// Token: 0x040013D2 RID: 5074
	[SerializeField]
	private GameObject _list_element_prefab;

	// Token: 0x040013D3 RID: 5075
	[SerializeField]
	private MetaType _meta_type;

	// Token: 0x040013D4 RID: 5076
	private ListWindowAsset _asset;
}
