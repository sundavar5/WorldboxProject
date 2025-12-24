using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class Smoke : BaseEffect
{
	// Token: 0x06002064 RID: 8292 RVA: 0x00115DC0 File Offset: 0x00113FC0
	private void Update()
	{
		if (this.timer_scale > 0f)
		{
			this.timer_scale -= World.world.elapsed;
			return;
		}
		this.timer_scale = 0.01f;
		base.setAlpha(this.alpha - 0.01f);
		if (this.alpha <= 0f)
		{
			this.controller.killObject(this);
			return;
		}
		if (base.transform.localScale.x < 4f)
		{
			base.transform.localScale = new Vector3(base.transform.localScale.x + 0.03f, base.transform.localScale.y + 0.03f);
		}
		base.transform.localPosition = new Vector3(base.transform.localPosition.x + World.world.wind_direction.x * 0.5f, base.transform.localPosition.y + World.world.wind_direction.y * 0.5f);
	}

	// Token: 0x0400178F RID: 6031
	private float timer_scale;
}
