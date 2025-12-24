using System;
using ai.behaviours;

// Token: 0x0200038D RID: 909
public class BehCheckEnemyNotNear : BehaviourActionActor
{
	// Token: 0x060021A0 RID: 8608 RVA: 0x0011D0AC File Offset: 0x0011B2AC
	public override BehResult execute(Actor pActor)
	{
		if (Finder.isEnemyNearOnSameIsland(pActor, 1))
		{
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}
}
