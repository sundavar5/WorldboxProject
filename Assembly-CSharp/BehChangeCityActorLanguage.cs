using System;
using ai.behaviours;

// Token: 0x020003CF RID: 975
public class BehChangeCityActorLanguage : BehCityActor
{
	// Token: 0x06002267 RID: 8807 RVA: 0x00120E67 File Offset: 0x0011F067
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasLanguage())
		{
			pActor.city.setLanguage(pActor.language);
		}
		return BehResult.Continue;
	}
}
