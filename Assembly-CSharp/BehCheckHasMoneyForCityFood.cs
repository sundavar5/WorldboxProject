using System;
using ai.behaviours;

// Token: 0x020003B5 RID: 949
public class BehCheckHasMoneyForCityFood : BehCityActor
{
	// Token: 0x06002224 RID: 8740 RVA: 0x0011F7AC File Offset: 0x0011D9AC
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.canGetFoodFromCity())
		{
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}
}
