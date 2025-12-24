using System;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class CursorSpeed
{
	// Token: 0x06001687 RID: 5767 RVA: 0x000E3480 File Offset: 0x000E1680
	public void update()
	{
		if (Input.GetMouseButton(0))
		{
			this._lastFramePos.Set(this._curFramePos.x, this._curFramePos.y);
			Vector3 tMousePos = Input.mousePosition;
			this._curFramePos.Set(tMousePos.x, tMousePos.y);
			this.difference = Toolbox.DistVec2Float(this._curFramePos, this._lastFramePos) / 2f;
			if (this.difference > this.speed)
			{
				this.speed = this.difference;
			}
		}
		this.speed = this.speed * 0.95f - 1f;
		if (this.speed < 0f)
		{
			this.speed = 0f;
		}
		this.fmod_speed = (float)((int)this.speed);
		if (this.fmod_speed > 100f)
		{
			this.fmod_speed = 100f;
		}
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x000E3560 File Offset: 0x000E1760
	public void debug(DebugTool pTool)
	{
		pTool.setText("difference", this.difference, 0f, false, 0L, false, false, "");
		pTool.setText("speed", this.speed, 0f, false, 0L, false, false, "");
		pTool.setText("fmod_speed", this.fmod_speed, 0f, false, 0L, false, false, "");
	}

	// Token: 0x040012A9 RID: 4777
	private Vector2 _lastFramePos;

	// Token: 0x040012AA RID: 4778
	private Vector2 _curFramePos;

	// Token: 0x040012AB RID: 4779
	private float difference;

	// Token: 0x040012AC RID: 4780
	public float speed;

	// Token: 0x040012AD RID: 4781
	public float fmod_speed;
}
