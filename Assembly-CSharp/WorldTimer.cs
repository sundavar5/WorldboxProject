using System;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class WorldTimer
{
	// Token: 0x060028AF RID: 10415 RVA: 0x00146280 File Offset: 0x00144480
	public WorldTimer(float pInterval, Action pCallback)
	{
		this.interval = pInterval;
		this.callback = pCallback;
		this.timer = this.interval;
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x001462A2 File Offset: 0x001444A2
	public void setTime(float pNewTime)
	{
		this.timer = pNewTime;
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x001462AB File Offset: 0x001444AB
	internal void setInterval(float pInterval)
	{
		this.interval = pInterval;
	}

	// Token: 0x060028B2 RID: 10418 RVA: 0x001462B4 File Offset: 0x001444B4
	public WorldTimer(float pInterval, bool pStopWatch)
	{
		this.isStopWatch = pStopWatch;
		this.interval = pInterval;
		this.timer = 0f;
		this.isActive = false;
	}

	// Token: 0x060028B3 RID: 10419 RVA: 0x001462DC File Offset: 0x001444DC
	public void startTimer(float pRate = -1f)
	{
		if (pRate != -1f)
		{
			this.interval = pRate;
		}
		this.timer = this.interval;
	}

	// Token: 0x060028B4 RID: 10420 RVA: 0x001462F9 File Offset: 0x001444F9
	public void stop()
	{
		this.isActive = false;
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x00146304 File Offset: 0x00144504
	public void update(float pElapsed = -1f)
	{
		if (pElapsed == -1f)
		{
			pElapsed = Time.deltaTime;
		}
		if (this.isStopWatch)
		{
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				this.isActive = true;
				return;
			}
			this.isActive = false;
			return;
		}
		else
		{
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				return;
			}
			this.timer = this.interval;
			this.callback();
			return;
		}
	}

	// Token: 0x04001E8F RID: 7823
	public bool isActive;

	// Token: 0x04001E90 RID: 7824
	private bool isStopWatch;

	// Token: 0x04001E91 RID: 7825
	private Action callback;

	// Token: 0x04001E92 RID: 7826
	private float interval;

	// Token: 0x04001E93 RID: 7827
	internal float timer;
}
