using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005CD RID: 1485
public class ButtonResource : MonoBehaviour
{
	// Token: 0x060030DE RID: 12510 RVA: 0x00177C50 File Offset: 0x00175E50
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x060030DF RID: 12511 RVA: 0x00177C9D File Offset: 0x00175E9D
	internal void load(ResourceAsset pAsset, int pAmount)
	{
		this.asset = pAsset;
		if (this.asset == null)
		{
			return;
		}
		base.GetComponent<Image>().sprite = pAsset.getSpriteIcon();
		this.textAmount.text = (pAmount.ToString() ?? "");
	}

	// Token: 0x060030E0 RID: 12512 RVA: 0x00177CDB File Offset: 0x00175EDB
	private void showHoverTooltip()
	{
		if (!Config.tooltips_active)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x00177CEC File Offset: 0x00175EEC
	private void showTooltip()
	{
		string tTooltipId = this.asset.tooltip;
		Tooltip.show(this, tTooltipId, new TooltipData
		{
			resource = this.asset
		});
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		base.transform.DOKill(false);
		base.transform.DOScale(0.8f, ButtonResource.scaleTime).SetEase(Ease.InBack);
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x00177D66 File Offset: 0x00175F66
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x040024E6 RID: 9446
	public Text textAmount;

	// Token: 0x040024E7 RID: 9447
	public ResourceAsset asset;

	// Token: 0x040024E8 RID: 9448
	public static float scaleTime = 0.1f;
}
