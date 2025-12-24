using System;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class NapalmFlash : BaseEffect
{
	// Token: 0x06002055 RID: 8277 RVA: 0x0011571E File Offset: 0x0011391E
	internal void spawnFlash(WorldTile pTile)
	{
		this.tile = pTile;
		this.bombSpawned = false;
		this.killing = false;
		this.prepare(pTile, 0.1f);
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x00115741 File Offset: 0x00113941
	public static bool napalmEffect(WorldTile pTile, string pPowerID)
	{
		pTile.startFire(true);
		return true;
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x0011574C File Offset: 0x0011394C
	private void Update()
	{
		if (base.transform.localScale.x < 1f && !this.killing)
		{
			Vector3 tVec = base.transform.localScale;
			tVec.x += World.world.elapsed * 0.7f;
			if (tVec.x >= 0.6f && !this.bombSpawned)
			{
				this.bombSpawned = true;
				World.world.loopWithBrush(this.tile, Brush.get(12, "circ_"), new PowerActionWithID(NapalmFlash.napalmEffect), null);
			}
			if (tVec.x >= 0.7f)
			{
				tVec.x = 0.7f;
				this.killing = true;
			}
			tVec.y = tVec.x;
			base.transform.localScale = tVec;
			return;
		}
		if (this.killing)
		{
			Vector3 tVec2 = base.transform.localScale;
			tVec2.x -= World.world.elapsed * 1.5f;
			tVec2.y = tVec2.x;
			if (tVec2.x <= 0f)
			{
				tVec2.x = 0f;
				this.kill();
			}
			base.transform.localScale = tVec2;
		}
	}

	// Token: 0x04001781 RID: 6017
	private bool killing;

	// Token: 0x04001782 RID: 6018
	private bool bombSpawned;
}
