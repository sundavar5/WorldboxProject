using System;
using System.Collections.Generic;

// Token: 0x0200064E RID: 1614
public class CitiesBannersContainer : BannersMetaContainer<CityBanner, City, CityData>
{
	// Token: 0x06003471 RID: 13425 RVA: 0x00185D36 File Offset: 0x00183F36
	protected override IEnumerable<City> getMetaList(IMetaObject pMeta)
	{
		return pMeta.getCities();
	}
}
