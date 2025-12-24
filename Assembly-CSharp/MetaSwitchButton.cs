using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000835 RID: 2101
public class MetaSwitchButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06004158 RID: 16728 RVA: 0x001BCDB8 File Offset: 0x001BAFB8
	public void init(MetaSwitchManager.Direction pDirection, SwitchWindowsAction pAction)
	{
		this._direction = pDirection;
		this._pool = new MultiBannerPool(this.banner_parent);
		this.button.onClick.AddListener(delegate()
		{
			pAction(this._direction);
		});
	}

	// Token: 0x06004159 RID: 16729 RVA: 0x001BCE0D File Offset: 0x001BB00D
	public void setBanner(IBanner pBanner)
	{
		this._banner = pBanner;
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x001BCE16 File Offset: 0x001BB016
	public MultiBannerPool getPool()
	{
		return this._pool;
	}

	// Token: 0x0600415B RID: 16731 RVA: 0x001BCE1E File Offset: 0x001BB01E
	public void clear()
	{
		this._pool.clear();
	}

	// Token: 0x0600415C RID: 16732 RVA: 0x001BCE2B File Offset: 0x001BB02B
	public void OnPointerEnter(PointerEventData pEventData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x0600415D RID: 16733 RVA: 0x001BCE3B File Offset: 0x001BB03B
	public void OnPointerExit(PointerEventData pEventData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		Tooltip.hideTooltip();
	}

	// Token: 0x0600415E RID: 16734 RVA: 0x001BCE4A File Offset: 0x001BB04A
	private void showTooltip()
	{
		if (!MetaSwitchManager.isSwitcherEnabled())
		{
			return;
		}
		this._banner.meta_type_asset.stat_hover(this._banner.GetNanoObject().getID(), this);
	}

	// Token: 0x04002FB1 RID: 12209
	public Button button;

	// Token: 0x04002FB2 RID: 12210
	public Text meta_name;

	// Token: 0x04002FB3 RID: 12211
	public Transform banner_parent;

	// Token: 0x04002FB4 RID: 12212
	private IBanner _banner;

	// Token: 0x04002FB5 RID: 12213
	private MultiBannerPool _pool;

	// Token: 0x04002FB6 RID: 12214
	private MetaSwitchManager.Direction _direction;
}
