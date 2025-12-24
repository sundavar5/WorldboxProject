using System;

// Token: 0x0200023E RID: 574
public class BuildingTweenScaleHelper
{
	// Token: 0x060015E3 RID: 5603 RVA: 0x000E0DEE File Offset: 0x000DEFEE
	public void doRotateTween(float pTargetAngle, float pDuration, Action pAction)
	{
		this.angle_target = pTargetAngle;
		this.angle_duration = pDuration;
		this.angle_final_action = pAction;
		this.angle_time = 0f;
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x000E0E10 File Offset: 0x000DF010
	public void reset()
	{
		this.active = false;
		this.scale_start = 0f;
		this.scale_target = 1f;
		this.scale_time = 0.0;
		this.scale_duration = 1f;
		this.scale_last_priority = 0f;
		this.scale_use_x = false;
		this.scale_final_action = null;
		this.scale_ease = null;
		this.angle_target = 0f;
		this.angle_duration = 0f;
		this.angle_time = 0f;
		this.angle_final_action = null;
	}

	// Token: 0x0400124F RID: 4687
	internal bool active;

	// Token: 0x04001250 RID: 4688
	internal float scale_start;

	// Token: 0x04001251 RID: 4689
	internal float scale_target = 1f;

	// Token: 0x04001252 RID: 4690
	internal double scale_time;

	// Token: 0x04001253 RID: 4691
	internal float scale_duration = 1f;

	// Token: 0x04001254 RID: 4692
	internal float scale_last_priority;

	// Token: 0x04001255 RID: 4693
	internal bool scale_use_x;

	// Token: 0x04001256 RID: 4694
	internal Action scale_final_action;

	// Token: 0x04001257 RID: 4695
	internal EasingFunction scale_ease;

	// Token: 0x04001258 RID: 4696
	public float angle_target;

	// Token: 0x04001259 RID: 4697
	public float angle_duration;

	// Token: 0x0400125A RID: 4698
	public float angle_time;

	// Token: 0x0400125B RID: 4699
	internal Action angle_final_action;
}
