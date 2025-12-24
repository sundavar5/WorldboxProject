using System;
using UnityEngine.UI;

// Token: 0x02000671 RID: 1649
public class CultureBanner : BannerGeneric<Culture, CultureData>
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x0600353C RID: 13628 RVA: 0x001883D6 File Offset: 0x001865D6
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x0600353D RID: 13629 RVA: 0x001883D9 File Offset: 0x001865D9
	protected override string tooltip_id
	{
		get
		{
			return "culture";
		}
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x001883E0 File Offset: 0x001865E0
	protected override void loadPartBackground()
	{
		this.part_background = base.transform.FindRecursive("Decor").GetComponent<Image>();
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x001883FD File Offset: 0x001865FD
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.culture = this.meta_object;
		return tooltipData;
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x00188414 File Offset: 0x00186614
	protected override void setupBanner()
	{
		base.setupBanner();
		this.part_icon.sprite = this.meta_object.getElementSprite();
		this.part_background.sprite = this.meta_object.getDecorSprite();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.part_icon.color = tColorAsset.getColorBanner();
		this.part_background.color = tColorAsset.getColorMainSecond();
		this.part_frame.color = tColorAsset.getColorMainSecond();
	}
}
