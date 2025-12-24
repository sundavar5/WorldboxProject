using System;
using ai.behaviours;

// Token: 0x020003D0 RID: 976
public class BehChangeCityActorReligion : BehCityActor
{
	// Token: 0x06002269 RID: 8809 RVA: 0x00120E8B File Offset: 0x0011F08B
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasReligion())
		{
			pActor.city.setReligion(pActor.religion);
		}
		return BehResult.Continue;
	}
}
