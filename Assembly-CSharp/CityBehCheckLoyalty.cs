using System;
using ai.behaviours;

// Token: 0x020003E3 RID: 995
public class CityBehCheckLoyalty : BehaviourActionCity
{
	// Token: 0x060022BA RID: 8890 RVA: 0x00122E9A File Offset: 0x0012109A
	public override BehResult execute(City pCity)
	{
		CityBehCheckLoyalty.check(pCity);
		return BehResult.Continue;
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x00122EA3 File Offset: 0x001210A3
	public static void check(City pCity)
	{
		pCity.getLoyalty(true);
	}
}
