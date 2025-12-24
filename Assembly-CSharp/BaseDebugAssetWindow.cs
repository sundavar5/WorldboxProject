using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200054F RID: 1359
public class BaseDebugAssetWindow<TAsset, TAssetElement> : TabbedWindow where TAsset : Asset where TAssetElement : BaseDebugAssetElement<TAsset>
{
	// Token: 0x06002C50 RID: 11344 RVA: 0x0015C65C File Offset: 0x0015A85C
	protected override void create()
	{
		base.create();
		this.asset = BaseDebugAssetElement<TAsset>.selected_asset;
		this.sorting_tab.addButton("ui/Icons/onomastics/onomastics_vowel_separator", "sort_by_alphabet", new SortButtonAction(this.setDataResorted), delegate
		{
			this._sorted_fields = this._sorting_fields;
			this._sorted_fields.Sort(new Comparison<FieldInfo>(this.sortByName));
			this.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/onomastics/onomastics_consonant_separator", "sort_by_type", new SortButtonAction(this.setDataResorted), delegate
		{
			this._sorted_fields = this._sorting_fields;
			this._sorted_fields.Sort(new Comparison<FieldInfo>(this.sortByType));
			this.checkReverseSort();
		});
		this._default_sort_button = this.sorting_tab.addButton("ui/Icons/actor_traits/iconClumsy", "default_sort", new SortButtonAction(this.setDataResorted), delegate
		{
			this._sorted_fields = this._default_sorting_fields;
			if (this.sorting_tab.getCurrentButton().getState() != SortButtonState.Down && !this._default_reversed)
			{
				return;
			}
			this._default_reversed = !this._default_reversed;
			this._sorted_fields.Reverse();
		});
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x0015C709 File Offset: 0x0015A909
	private void OnEnable()
	{
		this.asset = BaseDebugAssetElement<TAsset>.selected_asset;
		BaseDebugAssetWindow<TAsset, TAssetElement>.current_element = this.asset_debug_element;
		this._initialized = false;
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x0015C728 File Offset: 0x0015A928
	private void Update()
	{
		this.load();
		this.asset_debug_element.update();
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x0015C740 File Offset: 0x0015A940
	private void load()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this.scroll_window.titleText.text = this.asset.id;
		this.asset_debug_element.setData(this.asset);
		this.initSprites();
		this.field_infos.init<TAsset>();
		this.field_infos.setData(this.asset);
		this._sorted_fields = new List<FieldInfo>(this.field_infos.field_infos);
		this._sorting_fields = new List<FieldInfo>(this.field_infos.field_infos);
		this._default_sorting_fields = new List<FieldInfo>(this.field_infos.field_infos);
		this._default_sort_button.click();
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x0015C808 File Offset: 0x0015AA08
	protected virtual void initSprites()
	{
		foreach (object obj in this.sprite_elements_parent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x0015C864 File Offset: 0x0015AA64
	public void clickShowAllSprites()
	{
		GameObject gameObject = this.sprite_elements_parent.gameObject;
		bool tState = !gameObject.activeSelf;
		gameObject.SetActive(tState);
		this.hidden_sprites_placeholder.SetActive(!tState);
		if (tState)
		{
			this.show_sprites_button.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOn");
			return;
		}
		this.show_sprites_button.icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconOff");
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x0015C8D4 File Offset: 0x0015AAD4
	private void setDataResorted()
	{
		this.field_infos.clear();
		Dictionary<string, FieldInfoListItem> tCollectionsData = this.field_infos.fields_collection_data;
		tCollectionsData.Clear();
		for (int i = 0; i < this._sorted_fields.Count; i++)
		{
			FieldInfoListItem tItem = this.field_infos.getFieldData(this._sorted_fields[i], this.asset);
			tCollectionsData.Add(tItem.field_name, tItem);
			this.field_infos.addRow(tItem.field_name, tItem.field_value);
		}
		this.field_infos.setDataSearched(this.field_infos.search_input_field.text);
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x0015C977 File Offset: 0x0015AB77
	private void checkReverseSort()
	{
		if (this.sorting_tab.getCurrentButton().getState() != SortButtonState.Down)
		{
			return;
		}
		this._sorted_fields.Reverse();
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x0015C998 File Offset: 0x0015AB98
	private int sortByName(FieldInfo pObject1, FieldInfo pObject2)
	{
		return string.Compare(pObject1.Name, pObject2.Name, StringComparison.InvariantCulture);
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x0015C9AC File Offset: 0x0015ABAC
	private int sortByType(FieldInfo pObject1, FieldInfo pObject2)
	{
		return string.Compare(pObject1.FieldType.Name, pObject2.FieldType.Name, StringComparison.InvariantCulture);
	}

	// Token: 0x040021FC RID: 8700
	public static TAssetElement current_element;

	// Token: 0x040021FD RID: 8701
	public SpriteElement sprite_element_prefab;

	// Token: 0x040021FE RID: 8702
	public TAssetElement asset_debug_element;

	// Token: 0x040021FF RID: 8703
	public Transform sprite_elements_parent;

	// Token: 0x04002200 RID: 8704
	public SortingTab sorting_tab;

	// Token: 0x04002201 RID: 8705
	public FieldInfoList field_infos;

	// Token: 0x04002202 RID: 8706
	public PowerButton show_sprites_button;

	// Token: 0x04002203 RID: 8707
	public GameObject hidden_sprites_placeholder;

	// Token: 0x04002204 RID: 8708
	private List<FieldInfo> _sorted_fields;

	// Token: 0x04002205 RID: 8709
	private List<FieldInfo> _sorting_fields;

	// Token: 0x04002206 RID: 8710
	private List<FieldInfo> _default_sorting_fields;

	// Token: 0x04002207 RID: 8711
	private bool _default_reversed;

	// Token: 0x04002208 RID: 8712
	protected TAsset asset;

	// Token: 0x04002209 RID: 8713
	private SortButton _default_sort_button;

	// Token: 0x0400220A RID: 8714
	private bool _initialized;
}
