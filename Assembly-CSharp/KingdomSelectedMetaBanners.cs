using System;

// Token: 0x020006F7 RID: 1783
public class KingdomSelectedMetaBanners : KingdomMetaBanners, ISelectedTabBanners<Kingdom>
{
	// Token: 0x06003936 RID: 14646 RVA: 0x001981C4 File Offset: 0x001963C4
	public void update(Kingdom pKingdom)
	{
		this.meta_object = pKingdom;
		this.clear();
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			if (tBannerAsset.check())
			{
				base.metaBannerShow(tBannerAsset);
			}
		}
	}

	// Token: 0x06003937 RID: 14647 RVA: 0x00198234 File Offset: 0x00196434
	protected override void OnEnable()
	{
	}

	// Token: 0x06003938 RID: 14648 RVA: 0x00198236 File Offset: 0x00196436
	public int countVisibleBanners()
	{
		return this.visible_banners;
	}
}
