using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public readonly struct ResourceThrowData
{
	// Token: 0x06001741 RID: 5953 RVA: 0x000E6894 File Offset: 0x000E4A94
	public ResourceThrowData(Vector2 pPositionStart, Vector2 pPositionEnd, float pDuration, string pResourceAssetId, int pResourceAmount, long pBuildingTargetId, float pHeight)
	{
		this.position_start = pPositionStart;
		this.position_end = pPositionEnd;
		this.resource_asset_id = pResourceAssetId;
		this.resource_amount = pResourceAmount;
		this.building_target_id = pBuildingTargetId;
		this.height = pHeight;
		this.start_time = World.world.getCurSessionTime();
		this.end_time = this.start_time + (double)pDuration;
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x000E68EE File Offset: 0x000E4AEE
	public bool isFinished()
	{
		return World.world.getCurSessionTime() >= this.end_time;
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x000E6905 File Offset: 0x000E4B05
	public float getRatio()
	{
		return (float)((World.world.getCurSessionTime() - this.start_time) / (this.end_time - this.start_time));
	}

	// Token: 0x040012F9 RID: 4857
	public readonly Vector2 position_start;

	// Token: 0x040012FA RID: 4858
	public readonly Vector2 position_end;

	// Token: 0x040012FB RID: 4859
	public readonly string resource_asset_id;

	// Token: 0x040012FC RID: 4860
	public readonly int resource_amount;

	// Token: 0x040012FD RID: 4861
	public readonly double start_time;

	// Token: 0x040012FE RID: 4862
	public readonly double end_time;

	// Token: 0x040012FF RID: 4863
	public readonly long building_target_id;

	// Token: 0x04001300 RID: 4864
	public readonly float height;
}
