using System;
using System.Collections.Generic;

// Token: 0x0200064F RID: 1615
public class CitiesMetaContainer : ListMetaContainer<CityListElement, City, CityData>
{
	// Token: 0x06003473 RID: 13427 RVA: 0x00185D46 File Offset: 0x00183F46
	protected override IEnumerable<City> getMetaList()
	{
		return base.getMeta().getCities();
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x00185D53 File Offset: 0x00183F53
	protected override Comparison<City> getSorting()
	{
		return new Comparison<City>(ComponentListBase<CityListElement, City, CityData, CityListComponent>.sortByPopulation);
	}
}
