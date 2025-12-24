using System;
using ai.behaviours;

// Token: 0x020003B2 RID: 946
public class BehBoatCheckExistence : BehBoat
{
	// Token: 0x0600221C RID: 8732 RVA: 0x0011F55C File Offset: 0x0011D75C
	public override BehResult execute(Actor pActor)
	{
		if (this.boat.actor.getHomeBuilding() == null)
		{
			int tLastCheck;
			pActor.data.get("existence_check", out tLastCheck, 0);
			if (tLastCheck == 0)
			{
				pActor.data.set("existence_check", (int)BehaviourActionBase<Actor>.world.getCurWorldTime());
			}
			else if (Date.getMonthsSince((double)tLastCheck) > 2)
			{
				pActor.getHitFullHealth(AttackType.Explosion);
			}
		}
		else
		{
			pActor.data.removeInt("existence_check");
		}
		return BehResult.Continue;
	}
}
