using System;
using UnityEngine;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097A RID: 2426
	public class CondActorNotJustLanded : BehaviourActorCondition
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x001E1DE4 File Offset: 0x001DFFE4
		public override bool check(Actor pActor)
		{
			int tJustLanded;
			pActor.data.get("justLanded", out tJustLanded, 0);
			bool result = tJustLanded <= 0;
			tJustLanded--;
			pActor.data.set("justLanded", Mathf.Max(tJustLanded, 0));
			return result;
		}
	}
}
