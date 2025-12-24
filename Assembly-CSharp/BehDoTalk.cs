using System;
using ai.behaviours;

// Token: 0x020003D9 RID: 985
public class BehDoTalk : BehaviourActionActor
{
	// Token: 0x0600228B RID: 8843 RVA: 0x00121B48 File Offset: 0x0011FD48
	public BehDoTalk()
	{
		this.socialize = true;
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x00121B58 File Offset: 0x0011FD58
	public override BehResult execute(Actor pActor)
	{
		BaseSimObject beh_actor_target = pActor.beh_actor_target;
		Actor tTarget = (beh_actor_target != null) ? beh_actor_target.a : null;
		if (tTarget == null)
		{
			return BehResult.Stop;
		}
		if (!this.stillCanTalk(tTarget))
		{
			return BehResult.Stop;
		}
		if ((!pActor.hasTelepathicLink() || !tTarget.hasTelepathicLink()) && (float)Toolbox.SquaredDistTile(tTarget.current_tile, pActor.current_tile) > 16f)
		{
			return BehResult.Stop;
		}
		int tTalksAmount;
		pActor.data.get("socialize", out tTalksAmount, 0);
		int tMax = Randy.randomInt(5, 10);
		if (tTalksAmount > tMax)
		{
			return BehResult.Continue;
		}
		this.continueTalk(pActor, tTarget);
		return BehResult.RepeatStep;
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x00121BDE File Offset: 0x0011FDDE
	private bool stillCanTalk(Actor pTarget)
	{
		return pTarget.isAlive() && !pTarget.isLying();
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x00121BF8 File Offset: 0x0011FDF8
	private void continueTalk(Actor pActor, Actor pTarget)
	{
		int tTalksAmount;
		pActor.data.get("socialize", out tTalksAmount, 0);
		pActor.data.set("socialize", ++tTalksAmount);
		bool tNewSprite = false;
		if (Randy.randomChance(0.4f))
		{
			pActor.clearLastTopicSprite();
			tNewSprite = true;
		}
		else if (Randy.randomChance(0.4f))
		{
			pTarget.clearLastTopicSprite();
			tNewSprite = true;
		}
		if (!tNewSprite && pTarget.getTopicSpriteTrait() != null && Randy.randomChance(0.45f))
		{
			pActor.cloneTopicSprite(pTarget.getSocializeTopic());
		}
		pActor.lookTowardsPosition(pTarget.current_position);
		pTarget.lookTowardsPosition(pActor.current_position);
		pTarget.setTask("socialize_receiving", true, false, true);
		float tAngleMax = 10f;
		if (Randy.randomBool())
		{
			pActor.playIdleSound();
		}
		else
		{
			pTarget.playIdleSound();
		}
		pActor.setTargetAngleZ(Randy.randomFloat(-tAngleMax, tAngleMax));
		pTarget.setTargetAngleZ(Randy.randomFloat(-tAngleMax, tAngleMax));
		float tTimer = Randy.randomFloat(1.1f, 3.3f);
		pActor.timer_action = tTimer;
		pTarget.timer_action = tTimer;
		if (pActor.timestamp_tween_session_social == 0.0)
		{
			pActor.timestamp_tween_session_social = BehaviourActionBase<Actor>.world.getCurSessionTime();
			pTarget.timestamp_tween_session_social = BehaviourActionBase<Actor>.world.getCurSessionTime();
		}
	}
}
