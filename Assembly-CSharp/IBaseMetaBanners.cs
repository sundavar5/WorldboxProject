using System;
using System.Collections.Generic;

// Token: 0x020005DD RID: 1501
public interface IBaseMetaBanners
{
	// Token: 0x06003165 RID: 12645
	void metaBannerShow(MetaBannerElement pAsset);

	// Token: 0x06003166 RID: 12646
	void metaBannerHide(MetaBannerElement pAsset);

	// Token: 0x06003167 RID: 12647
	IReadOnlyCollection<MetaBannerElement> getBanners();

	// Token: 0x06003168 RID: 12648 RVA: 0x0017A3C8 File Offset: 0x001785C8
	void enableClickAnimation()
	{
		foreach (MetaBannerElement metaBannerElement in this.getBanners())
		{
			metaBannerElement.banner.GetComponent<TipButton>().showOnClick = true;
		}
	}
}
