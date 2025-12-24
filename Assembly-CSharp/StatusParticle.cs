using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class StatusParticle : BaseEffect
{
	// Token: 0x06002082 RID: 8322 RVA: 0x00116894 File Offset: 0x00114A94
	public void spawnParticle(Vector3 pVector, Color pColor, float pScale = 0.25f)
	{
		base.prepare(pVector, pScale);
		base.GetComponent<SpriteRenderer>().color = pColor;
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x001168AF File Offset: 0x00114AAF
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		base.setScale(this.scale - pElapsed * 0.2f);
		if (this.scale <= 0f)
		{
			this.kill();
		}
	}
}
