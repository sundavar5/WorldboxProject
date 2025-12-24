using System;
using ai.behaviours;

// Token: 0x02000393 RID: 915
public class BehCheckStartCivilization : BehaviourActionActor
{
	// Token: 0x060021AF RID: 8623 RVA: 0x0011D24C File Offset: 0x0011B44C
	public override BehResult execute(Actor pActor)
	{
		pActor.buildCityAndStartCivilization();
		return BehResult.Continue;
	}
}
