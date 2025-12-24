using System;

// Token: 0x020003E7 RID: 999
public class BehaviourActionCity : BehaviourActionBase<City>
{
	// Token: 0x060022D6 RID: 8918 RVA: 0x0012325B File Offset: 0x0012145B
	public override bool errorsFound(City pCity)
	{
		return !pCity.hasZones() || pCity.getPopulationPeople() == 0 || base.errorsFound(pCity);
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x00123276 File Offset: 0x00121476
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.uses_kingdoms = true;
		this.uses_cities = true;
	}
}
