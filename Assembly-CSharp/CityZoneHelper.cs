using System;

// Token: 0x02000324 RID: 804
public class CityZoneHelper
{
	// Token: 0x06001F08 RID: 7944 RVA: 0x0010EA78 File Offset: 0x0010CC78
	public CityZoneHelper()
	{
		this.city_growth = new CityZoneGrowth();
		this.city_abandon = new CityZoneAbandon();
		this.city_place_finder = new CityPlaceFinder();
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x0010EAA1 File Offset: 0x0010CCA1
	public void update(float pElapsed)
	{
		this.city_abandon.checkCities();
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x0010EAAE File Offset: 0x0010CCAE
	public void clear()
	{
		this.city_abandon.clearAll();
		this.city_growth.clearAll();
		this.city_place_finder.clearAll();
	}

	// Token: 0x040016B2 RID: 5810
	public CityZoneGrowth city_growth;

	// Token: 0x040016B3 RID: 5811
	public CityZoneAbandon city_abandon;

	// Token: 0x040016B4 RID: 5812
	public CityPlaceFinder city_place_finder;
}
