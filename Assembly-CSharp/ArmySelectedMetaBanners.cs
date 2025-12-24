using System;

// Token: 0x02000635 RID: 1589
public class ArmySelectedMetaBanners : ArmyMetaBanners, ISelectedTabBanners<Army>
{
	// Token: 0x060033CB RID: 13259 RVA: 0x00184008 File Offset: 0x00182208
	public void update(Army pArmy)
	{
		this.meta_object = pArmy;
		this.clear();
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			if (tBannerAsset.check())
			{
				base.metaBannerShow(tBannerAsset);
			}
		}
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x00184078 File Offset: 0x00182278
	protected override void OnEnable()
	{
	}

	// Token: 0x060033CD RID: 13261 RVA: 0x0018407A File Offset: 0x0018227A
	public int countVisibleBanners()
	{
		return base.visible_banners;
	}
}
