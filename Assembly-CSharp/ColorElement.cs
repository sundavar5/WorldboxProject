using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005D5 RID: 1493
public class ColorElement : MonoBehaviour
{
	// Token: 0x06003115 RID: 12565 RVA: 0x00178B24 File Offset: 0x00176D24
	public void setColor(Color pOuter, Color pInner)
	{
		this.outer.color = pOuter;
		this.inner.color = pInner;
	}

	// Token: 0x06003116 RID: 12566 RVA: 0x00178B3E File Offset: 0x00176D3E
	public void setSelected(bool pSelected)
	{
		this.selection.enabled = pSelected;
	}

	// Token: 0x06003117 RID: 12567 RVA: 0x00178B4C File Offset: 0x00176D4C
	public void setAction(UnityAction pAction)
	{
		this.button.onClick.AddListener(pAction);
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x00178B60 File Offset: 0x00176D60
	public void showTooltip()
	{
		CustomDataContainer<int> tCustomDataInt = new CustomDataContainer<int>();
		tCustomDataInt["color_count"] = this.asset.color_count();
		tCustomDataInt["color_current"] = this.index + 1;
		TooltipData tData = new TooltipData
		{
			tip_name = this.asset.color_locale,
			custom_data_int = tCustomDataInt
		};
		Tooltip.show(base.gameObject, "color_counter", tData);
	}

	// Token: 0x04002512 RID: 9490
	public Button button;

	// Token: 0x04002513 RID: 9491
	public Image selection;

	// Token: 0x04002514 RID: 9492
	public Image outer;

	// Token: 0x04002515 RID: 9493
	public Image inner;

	// Token: 0x04002516 RID: 9494
	public int index;

	// Token: 0x04002517 RID: 9495
	public MetaCustomizationAsset asset;
}
