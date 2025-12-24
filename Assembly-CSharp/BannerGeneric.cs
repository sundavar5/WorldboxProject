using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005C8 RID: 1480
public abstract class BannerGeneric<TMetaObject, TData> : BannerBase where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06003094 RID: 12436 RVA: 0x001770FE File Offset: 0x001752FE
	protected TData data
	{
		get
		{
			return this.meta_object.data;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06003095 RID: 12437 RVA: 0x00177110 File Offset: 0x00175310
	protected virtual string tooltip_id
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06003096 RID: 12438 RVA: 0x00177117 File Offset: 0x00175317
	private void Start()
	{
		this.create();
	}

	// Token: 0x06003097 RID: 12439 RVA: 0x0017711F File Offset: 0x0017531F
	private void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.setupParts();
		this.setupClick();
		this.setupTooltip();
	}

	// Token: 0x06003098 RID: 12440 RVA: 0x00177144 File Offset: 0x00175344
	protected virtual void setupClick()
	{
		if (!this.enable_default_click && !this._enable_customize_click && !this.enable_tab_show_click)
		{
			return;
		}
		Button tButton = base.GetComponentInChildren<Button>();
		if (tButton == null)
		{
			return;
		}
		if (this.enable_default_click)
		{
			tButton.onClick.AddListener(new UnityAction(this.clickAction));
			return;
		}
		if (this._enable_customize_click)
		{
			tButton.onClick.AddListener(new UnityAction(this.clickCustomize));
			return;
		}
		if (this.enable_tab_show_click)
		{
			tButton.onClick.AddListener(new UnityAction(this.clickShowTab));
		}
	}

	// Token: 0x06003099 RID: 12441 RVA: 0x001771DC File Offset: 0x001753DC
	protected virtual void clickAction()
	{
		if (this.meta_object.hasDied())
		{
			return;
		}
		if (!InputHelpers.mouseSupported)
		{
			this.switchOnDoubleTap();
			return;
		}
		this.showMetaWindow();
	}

	// Token: 0x0600309A RID: 12442 RVA: 0x00177205 File Offset: 0x00175405
	private void switchOnDoubleTap()
	{
		if (!Tooltip.isShowingFor(this))
		{
			this.tooltipAction();
			return;
		}
		this.showMetaWindow();
	}

	// Token: 0x0600309B RID: 12443 RVA: 0x0017721C File Offset: 0x0017541C
	private void showMetaWindow()
	{
		base.meta_type_asset.set_selected(this.meta_object);
		string tWindowId = base.meta_type_asset.window_name;
		if (ScrollWindow.isCurrentWindow(tWindowId))
		{
			ScrollWindow.get(tWindowId).showSameWindow();
			return;
		}
		ScrollWindow.showWindow(tWindowId);
	}

	// Token: 0x0600309C RID: 12444 RVA: 0x0017726C File Offset: 0x0017546C
	protected virtual void clickCustomize()
	{
		string tCustomizeWindowId = base.meta_asset.customize_window_id;
		if (tCustomizeWindowId == string.Empty)
		{
			Debug.LogError("var " + tCustomizeWindowId + " is not set!");
			return;
		}
		ScrollWindow.showWindow(tCustomizeWindowId);
	}

	// Token: 0x0600309D RID: 12445 RVA: 0x001772AE File Offset: 0x001754AE
	private void clickShowTab()
	{
		if (HotkeyLibrary.isHoldingControlForSelection())
		{
			this.clickAction();
			return;
		}
		base.meta_type_asset.selectAndInspect(this.meta_object, false, true, false);
		Tooltip.blockTooltips(0.5f);
		Tooltip.hideTooltipNow();
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x001772E6 File Offset: 0x001754E6
	protected virtual void setupParts()
	{
		this.loadPartBackground();
		this.loadPartFrame();
		this.loadPartIcon();
	}

	// Token: 0x0600309F RID: 12447 RVA: 0x001772FA File Offset: 0x001754FA
	protected virtual void loadPartFrame()
	{
		Transform transform = base.transform.FindRecursive("Frame");
		this.part_frame = ((transform != null) ? transform.GetComponent<Image>() : null);
	}

	// Token: 0x060030A0 RID: 12448 RVA: 0x0017731E File Offset: 0x0017551E
	protected virtual void loadPartBackground()
	{
		Transform transform = base.transform.FindRecursive("Background");
		this.part_background = ((transform != null) ? transform.GetComponent<Image>() : null);
	}

	// Token: 0x060030A1 RID: 12449 RVA: 0x00177342 File Offset: 0x00175542
	protected virtual void loadPartIcon()
	{
		Transform transform = base.transform.FindRecursive("Icon");
		this.part_icon = ((transform != null) ? transform.GetComponent<Image>() : null);
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x00177366 File Offset: 0x00175566
	protected virtual void setupBanner()
	{
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x00177368 File Offset: 0x00175568
	public override void load(NanoObject pObject)
	{
		this.setMetaObject(pObject);
		this.create();
		this.setupBanner();
	}

	// Token: 0x060030A4 RID: 12452 RVA: 0x0017737D File Offset: 0x0017557D
	public override NanoObject GetNanoObject()
	{
		return this.meta_object;
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x0017738C File Offset: 0x0017558C
	protected virtual void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(delegate
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.tooltipAction();
		}, true);
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x001773B7 File Offset: 0x001755B7
	protected virtual void tooltipAction()
	{
		Tooltip.show(this, this.tooltip_id, this.getTooltipData());
	}

	// Token: 0x060030A7 RID: 12455 RVA: 0x001773CC File Offset: 0x001755CC
	protected virtual TooltipData getTooltipData()
	{
		CustomDataContainer<bool> tCustomData = new CustomDataContainer<bool>();
		tCustomData["tab_banner"] = this.enable_tab_show_click;
		return new TooltipData
		{
			custom_data_bool = tCustomData
		};
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x001773FC File Offset: 0x001755FC
	public override void showTooltip()
	{
		this.tooltipAction();
	}

	// Token: 0x060030A9 RID: 12457 RVA: 0x00177404 File Offset: 0x00175604
	protected void setMetaObject(NanoObject pObject)
	{
		this.meta_object = (TMetaObject)((object)pObject);
	}

	// Token: 0x060030AA RID: 12458 RVA: 0x00177414 File Offset: 0x00175614
	internal virtual void normalize()
	{
		if (base.meta_asset.option_1_editable)
		{
			int tOption1Count = base.meta_asset.option_1_count();
			if (base.option_1 < 0)
			{
				base.option_1 += tOption1Count;
			}
			if (base.option_1 > tOption1Count - 1)
			{
				base.option_1 -= tOption1Count;
			}
			base.option_1 = Mathf.Clamp(base.option_1, 0, tOption1Count - 1);
		}
		if (base.meta_asset.option_2_editable)
		{
			int tOption2Count = base.meta_asset.option_2_count();
			if (base.option_2 < 0)
			{
				base.option_2 += tOption2Count;
			}
			if (base.option_2 > tOption2Count - 1)
			{
				base.option_2 -= tOption2Count;
			}
			base.option_2 = Mathf.Clamp(base.option_2, 0, tOption2Count - 1);
		}
		if (base.meta_asset.color_editable)
		{
			int tColorCount = base.meta_asset.color_count();
			if (base.color < 0)
			{
				base.color += tColorCount;
			}
			if (base.color > tColorCount - 1)
			{
				base.color -= tColorCount;
			}
			base.color = Mathf.Clamp(base.color, 0, tColorCount - 1);
		}
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x0017754C File Offset: 0x0017574C
	internal virtual void updateColor()
	{
		ColorAsset tColor = base.meta_asset.color_library().list[base.color];
		if (this.meta_object.updateColor(tColor))
		{
			base.meta_asset.on_new_color();
		}
	}

	// Token: 0x040024D8 RID: 9432
	protected TMetaObject meta_object;

	// Token: 0x040024D9 RID: 9433
	private bool _created;

	// Token: 0x040024DA RID: 9434
	protected Image part_background;

	// Token: 0x040024DB RID: 9435
	protected Image part_icon;

	// Token: 0x040024DC RID: 9436
	protected Image part_frame;

	// Token: 0x040024DD RID: 9437
	public bool enable_default_click = true;

	// Token: 0x040024DE RID: 9438
	[SerializeField]
	private bool _enable_customize_click;

	// Token: 0x040024DF RID: 9439
	public bool enable_tab_show_click;
}
