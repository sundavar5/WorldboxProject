using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000658 RID: 1624
public class CityLoyaltyElement : MonoBehaviour
{
	// Token: 0x060034A0 RID: 13472 RVA: 0x001864DA File Offset: 0x001846DA
	public void setCity(City pCity)
	{
		this._city = pCity;
	}

	// Token: 0x060034A1 RID: 13473 RVA: 0x001864E4 File Offset: 0x001846E4
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x00186531 File Offset: 0x00184731
	private void showHoverTooltip()
	{
		if (!Config.tooltips_active)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x060034A3 RID: 13475 RVA: 0x00186541 File Offset: 0x00184741
	private void showTooltip()
	{
		this._tooltip_data = new TooltipData
		{
			city = this._city
		};
		Tooltip.show(base.gameObject, "loyalty", this._tooltip_data);
	}

	// Token: 0x040027A8 RID: 10152
	private City _city;

	// Token: 0x040027A9 RID: 10153
	private TooltipData _tooltip_data;
}
