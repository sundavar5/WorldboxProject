using System;

// Token: 0x0200023D RID: 573
public static class BuildingTweenExtension
{
	// Token: 0x060015DD RID: 5597 RVA: 0x000E0A0C File Offset: 0x000DEC0C
	internal static void checkTweens(this Building pBuilding)
	{
		BuildingAnimationState animation_state = pBuilding.animation_state;
		if (animation_state == BuildingAnimationState.OnRuin)
		{
			pBuilding.setScaleTween(1f, 0.1f, 0f, new Action(pBuilding.completeMakingRuin), new EasingFunction(iTween.easeInCubic), 0);
			return;
		}
		if (animation_state != BuildingAnimationState.OnRemove)
		{
			return;
		}
		EasingFunction tEase = new EasingFunction(iTween.easeInBack);
		if (pBuilding.chopped)
		{
			tEase = new EasingFunction(iTween.easeInCubic);
			pBuilding.scale_helper.scale_use_x = true;
		}
		pBuilding.setScaleTween(1f, 0.5f, 0f, new Action(pBuilding.removeBuildingFinal), tEase, 1);
		if (pBuilding.asset.city_building)
		{
			pBuilding.startShake(0.5f, 0.1f, 0.1f);
		}
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000E0ACC File Offset: 0x000DECCC
	internal static void setScaleTween(this Building pBuilding, float pFrom = 0f, float pDuration = 0.2f, float pTarget = 1f, Action pActionOnComplete = null, EasingFunction pEase = null, int pPriority = 0)
	{
		BuildingTweenScaleHelper tHelper = pBuilding.scale_helper;
		if (tHelper.active && tHelper.scale_final_action != null && tHelper.scale_last_priority >= (float)pPriority)
		{
			return;
		}
		if (pEase == null)
		{
			pEase = new EasingFunction(iTween.easeOutBack);
		}
		tHelper.active = true;
		tHelper.scale_start = pFrom;
		tHelper.scale_target = pTarget;
		tHelper.scale_time = World.world.getCurSessionTime() + (double)pDuration;
		tHelper.scale_duration = pDuration;
		tHelper.scale_final_action = pActionOnComplete;
		tHelper.scale_ease = pEase;
		if (tHelper.scale_use_x)
		{
			pBuilding.current_scale.x = pBuilding.asset.scale_base.x * pFrom;
		}
		else
		{
			pBuilding.current_scale.y = pBuilding.asset.scale_base.y * pFrom;
		}
		pBuilding.batch.c_scale.Add(pBuilding);
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
	public static void checkFinalAction(this Building pBuilding)
	{
		Action scale_final_action = pBuilding.scale_helper.scale_final_action;
		if (scale_final_action != null)
		{
			scale_final_action();
		}
		pBuilding.scale_helper.scale_final_action = null;
		Action angle_final_action = pBuilding.scale_helper.angle_final_action;
		if (angle_final_action != null)
		{
			angle_final_action();
		}
		pBuilding.scale_helper.angle_final_action = null;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x000E0BF8 File Offset: 0x000DEDF8
	internal static void finishScaleTween(this Building pBuilding)
	{
		pBuilding.setAnimationState(BuildingAnimationState.Normal);
		BuildingTweenScaleHelper tHelper = pBuilding.scale_helper;
		tHelper.scale_time = World.world.getCurSessionTime() + (double)tHelper.scale_duration;
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x000E0C2C File Offset: 0x000DEE2C
	internal static void updateAngle(this Building pBuilding, float pElapsed)
	{
		if (pBuilding.current_rotation.z == pBuilding.scale_helper.angle_target)
		{
			return;
		}
		BuildingTweenScaleHelper tHelper = pBuilding.scale_helper;
		tHelper.angle_time += pElapsed;
		if (tHelper.angle_time >= 1f)
		{
			tHelper.angle_time = 1f;
			pBuilding.batch.c_angle.Remove(pBuilding);
			pBuilding.batch.actions_to_run.Add(new Action(pBuilding.checkFinalAction));
		}
		float tAngle = iTween.easeInExpo(0f, 1f, tHelper.angle_time);
		pBuilding.current_rotation.Set(0f, 0f, tAngle * pBuilding.scale_helper.angle_target);
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x000E0CE4 File Offset: 0x000DEEE4
	internal static void updateScale(this Building pBuilding)
	{
		if (!pBuilding.scale_helper.active)
		{
			return;
		}
		BuildingTweenScaleHelper tHelper = pBuilding.scale_helper;
		double tTimeLeft = tHelper.scale_time - World.world.getCurSessionTime();
		float tScale;
		if (tTimeLeft <= 0.0)
		{
			tHelper.scale_time = World.world.getCurSessionTime() + (double)tHelper.scale_duration;
			tHelper.active = false;
			pBuilding.batch.actions_to_run.Add(new Action(pBuilding.checkFinalAction));
			pBuilding.batch.c_scale.Remove(pBuilding);
			tScale = tHelper.scale_target;
		}
		else
		{
			float tTimeSpent = (float)(((double)tHelper.scale_duration - tTimeLeft) / (double)tHelper.scale_duration);
			tScale = tHelper.scale_ease(tHelper.scale_start, tHelper.scale_target, tTimeSpent);
		}
		if (tHelper.scale_use_x)
		{
			pBuilding.current_scale.x = pBuilding.asset.scale_base.x * tScale;
			return;
		}
		pBuilding.current_scale.y = pBuilding.asset.scale_base.y * tScale;
	}
}
