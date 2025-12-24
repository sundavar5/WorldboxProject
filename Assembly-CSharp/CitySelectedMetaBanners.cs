using System;

// Token: 0x0200065A RID: 1626
public class CitySelectedMetaBanners : CityMetaBanners, ISelectedTabBanners<City>
{
	// Token: 0x060034BC RID: 13500 RVA: 0x00186990 File Offset: 0x00184B90
	public void update(City pCity)
	{
		this.meta_object = pCity;
		this.clear();
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			if (tBannerAsset.check())
			{
				base.metaBannerShow(tBannerAsset);
			}
		}
	}

	// Token: 0x060034BD RID: 13501 RVA: 0x00186A00 File Offset: 0x00184C00
	protected override void OnEnable()
	{
	}

	// Token: 0x060034BE RID: 13502 RVA: 0x00186A02 File Offset: 0x00184C02
	public int countVisibleBanners()
	{
		return this.visible_banners;
	}
}
