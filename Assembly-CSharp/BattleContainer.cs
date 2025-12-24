using System;

// Token: 0x020001C5 RID: 453
public class BattleContainer
{
	// Token: 0x06000D53 RID: 3411 RVA: 0x000BB965 File Offset: 0x000B9B65
	public int getDeathsTotal()
	{
		return this._deaths_civs + this._deaths_mobs;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x000BB974 File Offset: 0x000B9B74
	public void increaseDeaths(Actor pActor)
	{
		if (pActor.isSapient())
		{
			this._deaths_civs++;
			return;
		}
		this._deaths_mobs++;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x000BB99B File Offset: 0x000B9B9B
	public bool isRendered()
	{
		return this._deaths_civs > 3 || this._deaths_mobs > 6;
	}

	// Token: 0x04000CAA RID: 3242
	public float timer = 1f;

	// Token: 0x04000CAB RID: 3243
	public float timeout = 1f;

	// Token: 0x04000CAC RID: 3244
	public float timer_animation = 0.05f;

	// Token: 0x04000CAD RID: 3245
	public int frame;

	// Token: 0x04000CAE RID: 3246
	private int _deaths_civs;

	// Token: 0x04000CAF RID: 3247
	private int _deaths_mobs;

	// Token: 0x04000CB0 RID: 3248
	public WorldTile tile;
}
