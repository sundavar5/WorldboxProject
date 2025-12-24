using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006EB RID: 1771
public class KingdomBanner : BannerGeneric<Kingdom, KingdomData>
{
	// Token: 0x17000325 RID: 805
	// (get) Token: 0x060038E9 RID: 14569 RVA: 0x00197605 File Offset: 0x00195805
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x060038EA RID: 14570 RVA: 0x00197608 File Offset: 0x00195808
	protected override string tooltip_id
	{
		get
		{
			return "kingdom";
		}
	}

	// Token: 0x060038EB RID: 14571 RVA: 0x0019760F File Offset: 0x0019580F
	protected override void setupTooltip()
	{
		base.setupTooltip();
	}

	// Token: 0x060038EC RID: 14572 RVA: 0x00197618 File Offset: 0x00195818
	protected override void setupBanner()
	{
		base.setupBanner();
		this.dead_image.gameObject.SetActive(false);
		this.left_image.gameObject.SetActive(false);
		this.winner_image.gameObject.SetActive(false);
		this.loser_image.gameObject.SetActive(false);
		this.part_background.sprite = this.meta_object.getElementBackground();
		this.part_icon.sprite = this.meta_object.getElementIcon();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.part_background.color = tColorAsset.getColorMainSecond();
		this.part_icon.color = tColorAsset.getColorBanner();
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x001976C9 File Offset: 0x001958C9
	public override void load(NanoObject pObject)
	{
		base.load(pObject);
		if (this.meta_object.hasDied())
		{
			this.showAsDead();
		}
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x001976E5 File Offset: 0x001958E5
	private void showAsDead()
	{
		this.dead_image.gameObject.SetActive(true);
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x001976F8 File Offset: 0x001958F8
	public void hasLeftWar()
	{
		this.left_image.gameObject.SetActive(true);
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x0019770B File Offset: 0x0019590B
	public void hasWon()
	{
		this.winner_image.gameObject.SetActive(true);
	}

	// Token: 0x060038F1 RID: 14577 RVA: 0x0019771E File Offset: 0x0019591E
	public void hasLost()
	{
		this.loser_image.gameObject.SetActive(true);
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x00197734 File Offset: 0x00195934
	protected override void tooltipAction()
	{
		if (this.meta_object == null)
		{
			return;
		}
		string tType = this.meta_object.hasDied() ? "kingdom_dead" : "kingdom";
		string tTipName = string.Empty;
		if (this.diplo_banner)
		{
			tType = "kingdom_diplo";
			tTipName = "kingdom_diplo";
		}
		TooltipData tData = this.getTooltipData();
		tData.tip_name = tTipName;
		Tooltip.show(this, tType, tData);
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x00197794 File Offset: 0x00195994
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.kingdom = this.meta_object;
		return tooltipData;
	}

	// Token: 0x04002A23 RID: 10787
	public bool diplo_banner;

	// Token: 0x04002A24 RID: 10788
	[SerializeField]
	protected Image background;

	// Token: 0x04002A25 RID: 10789
	[SerializeField]
	protected Image icon;

	// Token: 0x04002A26 RID: 10790
	[SerializeField]
	protected Image dead_image;

	// Token: 0x04002A27 RID: 10791
	[SerializeField]
	protected Image left_image;

	// Token: 0x04002A28 RID: 10792
	[SerializeField]
	protected Image winner_image;

	// Token: 0x04002A29 RID: 10793
	[SerializeField]
	protected Image loser_image;

	// Token: 0x04002A2A RID: 10794
	private Color _bgcolor;

	// Token: 0x04002A2B RID: 10795
	private Color _iconcolor;
}
