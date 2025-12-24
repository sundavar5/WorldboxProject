using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007C4 RID: 1988
public class WarBanner : BannerGeneric<War, WarData>
{
	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x001B359D File Offset: 0x001B179D
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.War;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x001B35A1 File Offset: 0x001B17A1
	protected override string tooltip_id
	{
		get
		{
			return "war";
		}
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x001B35A8 File Offset: 0x001B17A8
	protected override void setupBanner()
	{
		base.setupBanner();
		this.banner_kingdom1.gameObject.SetActive(false);
		this.banner_kingdom2.gameObject.SetActive(false);
		this.total_war_icon.gameObject.SetActive(false);
		Kingdom tMainAttacker = this.meta_object.getMainAttacker();
		if (!tMainAttacker.isRekt())
		{
			this.banner_kingdom1.gameObject.SetActive(true);
			this.banner_kingdom1.load(tMainAttacker);
		}
		if (this.meta_object.isTotalWar())
		{
			this.total_war_icon.gameObject.SetActive(true);
		}
		else
		{
			Kingdom tMainDefender = this.meta_object.getMainDefender();
			if (!tMainDefender.isRekt())
			{
				this.banner_kingdom2.gameObject.SetActive(true);
				this.banner_kingdom2.load(tMainDefender);
			}
		}
		WarWinner winner = this.meta_object.data.winner;
		if (winner != WarWinner.Attackers)
		{
			if (winner == WarWinner.Defenders)
			{
				this.banner_kingdom1.hasLost();
				if (!this.meta_object.isTotalWar())
				{
					this.banner_kingdom2.hasWon();
				}
			}
		}
		else
		{
			this.banner_kingdom1.hasWon();
			if (!this.meta_object.isTotalWar())
			{
				this.banner_kingdom2.hasLost();
			}
		}
		this.war_icon.sprite = SpriteTextureLoader.getSprite(this.meta_object.getAsset().path_icon);
		if (this.buttons_enabled)
		{
			this.initDiploBanner();
		}
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x001B3704 File Offset: 0x001B1904
	private void initDiploBanner()
	{
		if (this.diplo_banner_initiated)
		{
			return;
		}
		this.diplo_banner_initiated = true;
		this.diplo_banner = true;
		base.GetComponent<Button>().enabled = true;
		base.GetComponent<TipButton>().enabled = true;
		UiButtonHoverAnimation component = base.GetComponent<UiButtonHoverAnimation>();
		component.enabled = true;
		component.scale_size = 1.1f;
		component.default_scale = new Vector3(0.8f, 0.8f, 0.8f);
		this.setupTooltip();
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x001B3777 File Offset: 0x001B1977
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.war = this.meta_object;
		return tooltipData;
	}

	// Token: 0x04002DBC RID: 11708
	public KingdomBanner banner_kingdom1;

	// Token: 0x04002DBD RID: 11709
	public KingdomBanner banner_kingdom2;

	// Token: 0x04002DBE RID: 11710
	public Image war_icon;

	// Token: 0x04002DBF RID: 11711
	public bool diplo_banner;

	// Token: 0x04002DC0 RID: 11712
	public Image total_war_icon;

	// Token: 0x04002DC1 RID: 11713
	private bool diplo_banner_initiated;

	// Token: 0x04002DC2 RID: 11714
	public bool buttons_enabled;
}
