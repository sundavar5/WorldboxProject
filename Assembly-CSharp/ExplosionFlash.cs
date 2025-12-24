using System;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class ExplosionFlash : BaseEffect
{
	// Token: 0x06001FFD RID: 8189 RVA: 0x00113387 File Offset: 0x00111587
	public void start(Vector3 pVector, float pRadius, float pSpeed = 1f)
	{
		this.speed = pSpeed;
		base.transform.position = new Vector3(pVector.x, pVector.y);
		base.setScale(0.005f * pRadius);
		base.setAlpha(1f);
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x001133C4 File Offset: 0x001115C4
	public override void update(float pElapsed)
	{
		base.setAlpha(this.alpha - pElapsed * this.speed * 0.5f);
		base.setScale(this.scale += pElapsed * this.speed * 0.1f);
		if (this.alpha <= 0f)
		{
			this.kill();
		}
	}

	// Token: 0x0400174C RID: 5964
	private float speed;
}
