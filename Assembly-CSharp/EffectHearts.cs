using System;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class EffectHearts : BaseEffect
{
	// Token: 0x06001FF3 RID: 8179 RVA: 0x0011304C File Offset: 0x0011124C
	internal override void spawnOnTile(WorldTile pTile)
	{
		float tScale = Randy.randomFloat(0.3f, 0.5f);
		this.prepare(pTile, tScale);
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x00113074 File Offset: 0x00111274
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		float tPosX = base.transform.position.x;
		float tPosY = base.transform.position.y + pElapsed * 3f / Config.time_scale_asset.multiplier;
		base.transform.position = new Vector3(tPosX, tPosY);
	}
}
