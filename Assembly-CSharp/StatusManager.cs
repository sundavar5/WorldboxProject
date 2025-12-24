using System;
using System.Collections.Generic;

// Token: 0x0200017E RID: 382
public class StatusManager : CoreSystemManager<Status, StatusData>
{
	// Token: 0x06000B79 RID: 2937 RVA: 0x000A57E9 File Offset: 0x000A39E9
	public StatusManager()
	{
		this.type_id = "statuses";
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x000A57FC File Offset: 0x000A39FC
	public Status newStatus(BaseSimObject pSimObject, StatusAsset pAsset, float pOverrideTimer)
	{
		Status tStatus = base.newObject();
		tStatus.start(pSimObject, pAsset);
		if (pOverrideTimer > 0f)
		{
			tStatus.setDuration(pOverrideTimer);
		}
		return tStatus;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x000A5828 File Offset: 0x000A3A28
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateStatuses(pElapsed);
		this.checkDead();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x000A583E File Offset: 0x000A3A3E
	public override void removeObject(Status pObject)
	{
		base.removeObject(pObject);
		pObject.sim_object.removeFinishedStatusEffect(pObject);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x000A5854 File Offset: 0x000A3A54
	private void updateStatuses(float pElapsed)
	{
		float tWorldTime = (float)World.world.getCurWorldTime();
		bool tPaused = World.world.isPaused();
		List<Status> tList = this.list;
		for (int i = 0; i < tList.Count; i++)
		{
			Status tStatus = tList[i];
			if (!tStatus.is_finished)
			{
				if (!tPaused)
				{
					tStatus.update(pElapsed, tWorldTime);
				}
				if (!tPaused || tStatus.asset.is_animated_in_pause)
				{
					tStatus.updateAnimationFrame(pElapsed);
				}
			}
		}
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x000A58C8 File Offset: 0x000A3AC8
	private void checkDead()
	{
		for (int i = this.list.Count - 1; i >= 0; i--)
		{
			Status Status = this.list[i];
			if (Status.is_finished)
			{
				this.removeObject(Status);
			}
		}
	}
}
