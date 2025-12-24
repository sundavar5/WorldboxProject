using System;
using ai.behaviours;

// Token: 0x020003BF RID: 959
public class BehFamilyCheckMembers : BehaviourActionActor
{
	// Token: 0x0600223C RID: 8764 RVA: 0x0012037C File Offset: 0x0011E57C
	public override BehResult execute(Actor pActor)
	{
		if (pActor.family.countUnits() > 1)
		{
			return BehResult.Stop;
		}
		pActor.setFamily(null);
		return BehResult.Continue;
	}
}
