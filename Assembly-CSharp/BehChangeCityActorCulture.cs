using System;
using ai.behaviours;

// Token: 0x020003CE RID: 974
public class BehChangeCityActorCulture : BehCityActor
{
	// Token: 0x06002265 RID: 8805 RVA: 0x00120E43 File Offset: 0x0011F043
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasCulture())
		{
			pActor.city.setCulture(pActor.culture);
		}
		return BehResult.Continue;
	}
}
