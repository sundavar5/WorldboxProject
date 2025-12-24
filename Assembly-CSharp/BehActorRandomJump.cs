using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class BehActorRandomJump : BehaviourActionActor
{
	// Token: 0x0600219C RID: 8604 RVA: 0x0011CFB8 File Offset: 0x0011B1B8
	public override BehResult execute(Actor pActor)
	{
		float tPowerSide = Randy.randomFloat(1f, 5f);
		float tPowerHeight = Randy.randomFloat(1f, 2f);
		Vector2 tStart = pActor.current_position;
		float tRandomAngle = Randy.randomFloat(-180f, 180f);
		Vector2 tTarget = tStart + Toolbox.rotateVector(tStart, tRandomAngle) * tPowerSide;
		pActor.calculateForce(tStart.x, tStart.y, tTarget.x, tTarget.y, tPowerSide, tPowerHeight, false);
		pActor.punchTargetAnimation(tStart, false, false, -60f);
		if (pActor.is_visible)
		{
			Vector2 tStartVisual = pActor.current_position;
			BaseEffect tEffect = EffectsLibrary.spawnAt("fx_dodge", tStartVisual, pActor.actor_scale);
			if (tEffect != null)
			{
				tEffect.transform.rotation = Toolbox.getEulerAngle(tStart, tTarget);
			}
		}
		return BehResult.Continue;
	}
}
