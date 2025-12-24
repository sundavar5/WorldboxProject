using System;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class MusicBoxContainerTiles
{
	// Token: 0x0600257D RID: 9597 RVA: 0x00135714 File Offset: 0x00133914
	public void clear()
	{
		this.amount = 0;
		this._last_pan.Set(-1f, -1f);
		this._chunks = 0f;
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x00135740 File Offset: 0x00133940
	public void count(int pAmount, float pWhereFromX, float pWhereFromY)
	{
		this.amount += pAmount;
		this._chunks += 1f;
		this._last_pan.x = this._last_pan.x + pWhereFromX;
		this._last_pan.y = this._last_pan.y + pWhereFromY;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x00135790 File Offset: 0x00133990
	public void calculatePan()
	{
		this._last_pan.x = this._last_pan.x / (this._chunks + 1f);
		this._last_pan.y = this._last_pan.y / (this._chunks + 1f);
		if (this._chunks == 0f)
		{
			this.cur_pan.Set(-1f, -1f);
			return;
		}
		if (this.cur_pan.x == -1f && this.cur_pan.y == -1f)
		{
			this.cur_pan.Set(this._last_pan.x, this._last_pan.y);
			return;
		}
		this.cur_pan = Vector2.MoveTowards(this.cur_pan, this._last_pan, 5f);
	}

	// Token: 0x04001C74 RID: 7284
	public int amount;

	// Token: 0x04001C75 RID: 7285
	public float percent;

	// Token: 0x04001C76 RID: 7286
	public bool enabled;

	// Token: 0x04001C77 RID: 7287
	public Vector2 cur_pan;

	// Token: 0x04001C78 RID: 7288
	private Vector2 _last_pan;

	// Token: 0x04001C79 RID: 7289
	private float _chunks;

	// Token: 0x04001C7A RID: 7290
	public MusicAsset asset;
}
