using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class EffectFlyingY : BaseEffect
{
	// Token: 0x06001FF1 RID: 8177 RVA: 0x00112FF0 File Offset: 0x001111F0
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		Vector3 position = base.transform.position;
		float tPosX = position.x;
		float tPosY = position.y + pElapsed * 1f / Config.time_scale_asset.multiplier;
		base.transform.position = new Vector3(tPosX, tPosY);
	}
}
