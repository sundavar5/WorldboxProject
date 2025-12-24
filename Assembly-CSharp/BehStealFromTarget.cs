using System;
using ai.behaviours;

// Token: 0x020003C3 RID: 963
public class BehStealFromTarget : BehaviourActionActor
{
	// Token: 0x06002247 RID: 8775 RVA: 0x00120524 File Offset: 0x0011E724
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.null_check_actor_target = true;
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x00120534 File Offset: 0x0011E734
	public override BehResult execute(Actor pActor)
	{
		Actor tTarget = pActor.beh_actor_target.a;
		if (tTarget == null || !tTarget.isAlive() || tTarget.isInsideSomething())
		{
			return BehResult.Stop;
		}
		if (pActor.distanceToActorTile(tTarget) > 2f)
		{
			return BehResult.Stop;
		}
		bool tSteal = false;
		float tWaitTimerHimself = 0.5f;
		float tStunnedTimer = 1f;
		bool tAddAggro = false;
		if (tTarget.canSeeTileBasedOnDirection(pActor.current_tile))
		{
			if (Randy.randomChance(0.4f))
			{
				tSteal = true;
				tStunnedTimer = 1f;
				tWaitTimerHimself = 0.9f;
				tAddAggro = true;
			}
		}
		else if (Randy.randomChance(0.7f))
		{
			tSteal = true;
			tStunnedTimer = 5f;
			tWaitTimerHimself = 1f;
		}
		else
		{
			pActor.makeWait(1f);
		}
		if (tSteal)
		{
			pActor.spawnSlashTalk(tTarget.current_position);
			pActor.punchTargetAnimation(tTarget.current_position, false, false, -20f);
			pActor.stealActionFrom(tTarget, tStunnedTimer, tWaitTimerHimself, tAddAggro, false);
			return BehResult.Continue;
		}
		return BehResult.Stop;
	}
}
