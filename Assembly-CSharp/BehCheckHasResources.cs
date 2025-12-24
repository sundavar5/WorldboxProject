using System;
using ai.behaviours;

// Token: 0x0200038E RID: 910
public class BehCheckHasResources : BehaviourActionActor
{
	// Token: 0x060021A2 RID: 8610 RVA: 0x0011D0C2 File Offset: 0x0011B2C2
	public override BehResult execute(Actor pObject)
	{
		if (pObject.isCarryingResources())
		{
			return BehResult.Continue;
		}
		return BehResult.Stop;
	}
}
