using System;

// Token: 0x02000562 RID: 1378
public class DelayedAction
{
	// Token: 0x06002CBD RID: 11453 RVA: 0x0015E7A4 File Offset: 0x0015C9A4
	public DelayedAction(Action pAction, float pDelay, bool pGameSpeedAffected)
	{
		this.action = pAction;
		this._game_speed_affected = pGameSpeedAffected;
		this._timer = pDelay;
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x0015E7C4 File Offset: 0x0015C9C4
	public bool update(float pElapsed, float pDeltaTime)
	{
		if (this._game_speed_affected)
		{
			this._timer -= pElapsed;
		}
		else
		{
			this._timer -= pDeltaTime;
		}
		if (this._timer > 0f)
		{
			return false;
		}
		this.action();
		return true;
	}

	// Token: 0x0400224B RID: 8779
	public readonly Action action;

	// Token: 0x0400224C RID: 8780
	private bool _game_speed_affected;

	// Token: 0x0400224D RID: 8781
	private float _timer;
}
