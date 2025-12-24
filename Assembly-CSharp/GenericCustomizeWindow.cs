using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005D9 RID: 1497
public class GenericCustomizeWindow<TMetaObject, TData, TBanner> : MonoBehaviour where TMetaObject : MetaObject<TData> where TData : MetaObjectData where TBanner : BannerGeneric<TMetaObject, TData>
{
	// Token: 0x17000290 RID: 656
	// (get) Token: 0x0600312F RID: 12591 RVA: 0x00179048 File Offset: 0x00177248
	protected virtual TMetaObject meta_object
	{
		get
		{
			throw new NotImplementedException("meta_object is not set");
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06003130 RID: 12592 RVA: 0x0017905F File Offset: 0x0017725F
	protected TData data
	{
		get
		{
			return this.meta_object.data;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06003131 RID: 12593 RVA: 0x00179071 File Offset: 0x00177271
	private MetaCustomizationAsset meta_asset
	{
		get
		{
			return AssetManager.meta_customization_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06003132 RID: 12594 RVA: 0x00179083 File Offset: 0x00177283
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException("meta_type is not set");
		}
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x00179090 File Offset: 0x00177290
	private void OnEnable()
	{
		this.loadBanner();
		this.apply();
		int tSelectedColor = this.banner.color;
		this.clickColorElement(this._color_elements[tSelectedColor], tSelectedColor);
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x001790D0 File Offset: 0x001772D0
	public int getChangeValue()
	{
		int tVal = 1;
		if (HotkeyLibrary.many_mod.isHolding())
		{
			tVal = 5;
		}
		return tVal;
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x001790EE File Offset: 0x001772EE
	protected virtual void apply()
	{
		this.updateBanner();
		this.loadBanner();
		this.updateSelection();
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x00179102 File Offset: 0x00177302
	protected virtual void loadBanner()
	{
		this.banner.load(this.meta_object);
	}

	// Token: 0x06003137 RID: 12599 RVA: 0x0017911F File Offset: 0x0017731F
	protected virtual void updateColors()
	{
		this.updateColorsBanner();
	}

	// Token: 0x06003138 RID: 12600 RVA: 0x00179128 File Offset: 0x00177328
	protected virtual void updateColorsBanner()
	{
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.image_banner_option_1.color = tColorAsset.getColorMainSecond();
		if (this.meta_asset.option_2_color_editable)
		{
			this.image_banner_option_2.color = tColorAsset.getColorBanner();
		}
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x00179175 File Offset: 0x00177375
	private void Awake()
	{
		this.create();
	}

	// Token: 0x0600313A RID: 12602 RVA: 0x0017917D File Offset: 0x0017737D
	private void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.setupParts();
		this.setupButtons();
		this.setupBanner();
		this.setupTexts();
		this.setupImages();
	}

	// Token: 0x0600313B RID: 12603 RVA: 0x001791B0 File Offset: 0x001773B0
	protected virtual void setupParts()
	{
		Transform tTitle = base.transform.FindRecursive("Background").Find("Title");
		this.title = tTitle.GetComponent<LocalizedText>();
		this.option_1 = base.transform.FindRecursive("Option 1");
		this.option_2 = base.transform.FindRecursive("Option 2");
		this.colors = base.transform.FindRecursive("Colors");
		this.colors_parent = this.colors.FindRecursive("Colors BG");
		Transform tBanner = base.transform.FindRecursive("Banner");
		this.banner_area = tBanner.FindRecursive("BannerArea");
		this.icon_banner = tBanner.FindRecursive("Icon").GetComponent<Image>();
		this.icon_top = base.transform.FindRecursive("Cat").GetComponent<Image>();
		this.counter_option_1 = this.option_1.FindRecursive("Counter").GetComponent<Text>();
		this.counter_option_2 = this.option_2.FindRecursive("Counter").GetComponent<Text>();
		this.counter_color = this.colors.FindRecursive("Counter").GetComponent<Text>();
		this.title_option_1 = this.option_1.FindRecursive("Title").GetComponent<LocalizedText>();
		this.title_option_2 = this.option_2.FindRecursive("Title").GetComponent<LocalizedText>();
		this.title_color = this.colors.FindRecursive("Title").GetComponent<LocalizedText>();
		this.image_banner_option_1 = this.option_1.FindRecursive("Image").GetComponent<Image>();
		this.image_banner_option_2 = this.option_2.FindRecursive("Image").GetComponent<Image>();
		this.option_1.gameObject.SetActive(this.meta_asset.option_1_editable);
		this.option_2.gameObject.SetActive(this.meta_asset.option_2_editable);
		this.colors.gameObject.SetActive(this.meta_asset.color_editable);
	}

	// Token: 0x0600313C RID: 12604 RVA: 0x001793B8 File Offset: 0x001775B8
	protected virtual void setupButtons()
	{
		this.option_1.FindRecursive("Left").GetComponent<Button>().onClick.AddListener(new UnityAction(this.option1Left));
		this.option_1.FindRecursive("Right").GetComponent<Button>().onClick.AddListener(new UnityAction(this.option1Right));
		this.option_2.FindRecursive("Left").GetComponent<Button>().onClick.AddListener(new UnityAction(this.option2Left));
		this.option_2.FindRecursive("Right").GetComponent<Button>().onClick.AddListener(new UnityAction(this.option2Right));
		base.transform.FindRecursive("Randomize").GetComponent<Button>().onClick.AddListener(new UnityAction(this.randomize));
		ColorElement tPrefab = base.GetComponentInParent<CustomizeWindow>().color_element_prefab;
		for (int i = 0; i < this.meta_asset.color_count(); i++)
		{
			int tIndex = i;
			ColorAsset colorByIndex = this.meta_asset.color_library().getColorByIndex(tIndex);
			Color tOuter = colorByIndex.getColorMainSecond();
			Color tInner = colorByIndex.getColorBorderInsideAlpha32();
			ColorElement tColorElement = Object.Instantiate<ColorElement>(tPrefab, this.colors_parent);
			this._color_elements.Add(tColorElement);
			tColorElement.setColor(tOuter, tInner);
			tColorElement.index = i;
			tColorElement.asset = this.meta_asset;
			tColorElement.setAction(delegate
			{
				this.clickColorElement(tColorElement, tIndex);
			});
			tColorElement.gameObject.GetComponent<TipButton>().setHoverAction(new TooltipAction(tColorElement.showTooltip), false);
		}
	}

	// Token: 0x0600313D RID: 12605 RVA: 0x00179598 File Offset: 0x00177798
	protected virtual void setupBanner()
	{
		TBanner tBannerPrefab = Resources.Load<TBanner>(this.meta_asset.banner_prefab_id);
		if (tBannerPrefab == null)
		{
			Debug.LogWarning("Banner prefab for " + this.meta_asset.banner_prefab_id + " could not be found");
		}
		this.banner = Object.Instantiate<TBanner>(tBannerPrefab);
		this.banner.enable_default_click = false;
		this.banner.transform.localScale = Vector3.one;
		this.banner.transform.SetParent(this.banner_area);
		LayoutElement layoutElement = this.banner.gameObject.AddComponent<LayoutElement>();
		layoutElement.ignoreLayout = true;
		layoutElement.enabled = false;
		this.banner.gameObject.AddComponent<DragSnapElement>().fly_back_parent = this.banner.transform.FindParentWithName("Viewport");
		RectTransform component = this.banner.GetComponent<RectTransform>();
		component.SetAnchor(AnchorPresets.MiddleCenter, 0f, 0f);
		component.localScale = Vector3.one;
	}

	// Token: 0x0600313E RID: 12606 RVA: 0x001796B8 File Offset: 0x001778B8
	protected virtual void setupTexts()
	{
		this.title.setKeyAndUpdate(this.meta_asset.title_locale);
		if (this.meta_asset.option_1_editable)
		{
			this.title_option_1.setKeyAndUpdate(this.meta_asset.option_1_locale);
		}
		if (this.meta_asset.option_2_editable)
		{
			this.title_option_2.setKeyAndUpdate(this.meta_asset.option_2_locale);
		}
		if (this.meta_asset.color_editable)
		{
			this.title_color.setKeyAndUpdate(this.meta_asset.color_locale);
		}
	}

	// Token: 0x0600313F RID: 12607 RVA: 0x00179744 File Offset: 0x00177944
	protected virtual void setupImages()
	{
		this.icon_banner.sprite = SpriteTextureLoader.getSprite("ui/Icons/" + this.meta_asset.icon_banner);
		this.icon_top.sprite = SpriteTextureLoader.getSprite("ui/Icons/" + this.meta_asset.icon_creature);
	}

	// Token: 0x06003140 RID: 12608 RVA: 0x0017979C File Offset: 0x0017799C
	protected virtual void updateCounters()
	{
		if (this.meta_asset.option_1_editable)
		{
			this.counter_option_1.text = (this.meta_asset.option_1_get() + 1).ToString() + "/" + this.meta_asset.option_1_count().ToString();
		}
		if (this.meta_asset.option_2_editable)
		{
			this.counter_option_2.text = (this.meta_asset.option_2_get() + 1).ToString() + "/" + this.meta_asset.option_2_count().ToString();
		}
		if (this.meta_asset.color_editable)
		{
			this.counter_color.text = (this.meta_asset.color_get() + 1).ToString() + "/" + this.meta_asset.color_count().ToString();
		}
	}

	// Token: 0x06003141 RID: 12609 RVA: 0x001798A5 File Offset: 0x00177AA5
	protected virtual void updateBanner()
	{
		this.banner.normalize();
		this.banner.updateColor();
	}

	// Token: 0x06003142 RID: 12610 RVA: 0x001798C7 File Offset: 0x00177AC7
	protected virtual void updateSelection()
	{
		this.updateCounters();
		this.updateColors();
		this.onBannerChange();
	}

	// Token: 0x06003143 RID: 12611 RVA: 0x001798DB File Offset: 0x00177ADB
	protected virtual void onBannerChange()
	{
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x001798E0 File Offset: 0x00177AE0
	public void randomize()
	{
		this.meta_object.generateBanner();
		ColorAsset tColor = this.meta_asset.color_library().list.GetRandom<ColorAsset>();
		this.banner.color = this.meta_asset.color_library().list.IndexOf(tColor);
		this.reselectAllColors();
		this._color_elements[this.banner.color].setSelected(true);
		this.apply();
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x00179970 File Offset: 0x00177B70
	public void option1Left()
	{
		ref TBanner ptr = ref this.banner;
		ptr.option_1 -= this.getChangeValue();
		this.apply();
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x001799AC File Offset: 0x00177BAC
	public void option1Right()
	{
		ref TBanner ptr = ref this.banner;
		ptr.option_1 += this.getChangeValue();
		this.apply();
	}

	// Token: 0x06003147 RID: 12615 RVA: 0x001799E8 File Offset: 0x00177BE8
	public void option2Left()
	{
		ref TBanner ptr = ref this.banner;
		ptr.option_2 -= this.getChangeValue();
		this.apply();
	}

	// Token: 0x06003148 RID: 12616 RVA: 0x00179A24 File Offset: 0x00177C24
	public void option2Right()
	{
		ref TBanner ptr = ref this.banner;
		ptr.option_2 += this.getChangeValue();
		this.apply();
	}

	// Token: 0x06003149 RID: 12617 RVA: 0x00179A5D File Offset: 0x00177C5D
	private void colorSet(int pIndex)
	{
		this.banner.color = pIndex;
		this.apply();
	}

	// Token: 0x0600314A RID: 12618 RVA: 0x00179A78 File Offset: 0x00177C78
	private void reselectAllColors()
	{
		foreach (ColorElement colorElement in this._color_elements)
		{
			colorElement.setSelected(false);
		}
	}

	// Token: 0x0600314B RID: 12619 RVA: 0x00179ACC File Offset: 0x00177CCC
	private void clickColorElement(ColorElement pElement, int pIndex)
	{
		this.reselectAllColors();
		this.colorSet(pIndex);
		pElement.setSelected(true);
	}

	// Token: 0x0400252A RID: 9514
	private bool _created;

	// Token: 0x0400252B RID: 9515
	protected Text counter_option_1;

	// Token: 0x0400252C RID: 9516
	protected Text counter_option_2;

	// Token: 0x0400252D RID: 9517
	protected Text counter_color;

	// Token: 0x0400252E RID: 9518
	protected Image image_banner_option_1;

	// Token: 0x0400252F RID: 9519
	protected Image image_banner_option_2;

	// Token: 0x04002530 RID: 9520
	protected LocalizedText title;

	// Token: 0x04002531 RID: 9521
	protected LocalizedText title_option_1;

	// Token: 0x04002532 RID: 9522
	protected LocalizedText title_option_2;

	// Token: 0x04002533 RID: 9523
	protected LocalizedText title_color;

	// Token: 0x04002534 RID: 9524
	protected Transform banner_area;

	// Token: 0x04002535 RID: 9525
	protected Image icon_banner;

	// Token: 0x04002536 RID: 9526
	protected Image icon_top;

	// Token: 0x04002537 RID: 9527
	protected Transform option_1;

	// Token: 0x04002538 RID: 9528
	protected Transform option_2;

	// Token: 0x04002539 RID: 9529
	protected Transform colors;

	// Token: 0x0400253A RID: 9530
	protected Transform colors_parent;

	// Token: 0x0400253B RID: 9531
	public TBanner banner;

	// Token: 0x0400253C RID: 9532
	private List<ColorElement> _color_elements = new List<ColorElement>();
}
