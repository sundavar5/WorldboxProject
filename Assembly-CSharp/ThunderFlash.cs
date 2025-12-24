using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class ThunderFlash : BaseEffect
{
	// Token: 0x06002085 RID: 8325 RVA: 0x001168E7 File Offset: 0x00114AE7
	internal void spawnFlash()
	{
		this.prepare(Vector3.zero, 0.3f);
		this.updatePos();
		this._blinks = Randy.randomInt(6, 10);
		this._cur_blinks = this._blinks;
		this.startBlink();
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x00116924 File Offset: 0x00114B24
	private void updatePos()
	{
		float tSpriteWidth = (float)this.sprite_renderer.sprite.texture.width;
		float tSpriteHeight = (float)this.sprite_renderer.sprite.texture.height;
		Vector3 tPosCamera = World.world.camera.transform.position;
		float num = World.world.camera.orthographicSize * 2f;
		float tScaleWidth = num / (float)Screen.height * (float)Screen.width / tSpriteWidth * 1f;
		float tScaleHeight = num / tSpriteHeight * 1f;
		float tModWidth = 4f;
		float tModHeight = 4f;
		base.transform.localPosition = new Vector3(tPosCamera.x, tPosCamera.y + tScaleHeight * tSpriteHeight / 2f);
		base.transform.localScale = new Vector3(tScaleWidth * tModWidth, tScaleHeight * tModHeight);
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x001169FC File Offset: 0x00114BFC
	private void setColor(float pAlpha = 1f)
	{
		this._last_alpha = pAlpha;
		Color tColor = new Color(1f, 1f, 1f, pAlpha);
		this.sprite_renderer.color = tColor;
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x00116A34 File Offset: 0x00114C34
	private void startBlink()
	{
		this._timer_blink = Randy.randomFloat(0f, 0.1f);
		float tAlpha = 0.4f;
		this.setColor(tAlpha);
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x00116A64 File Offset: 0x00114C64
	public override void update(float pElapsed)
	{
		pElapsed = Time.deltaTime;
		base.update(pElapsed);
		this.updatePos();
		if (this._last_alpha > 0f)
		{
			this._last_alpha -= pElapsed * 2f;
			if (this._last_alpha < 0f)
			{
				this._last_alpha = 0f;
			}
		}
		this.setColor(this._last_alpha);
		if (this._timer_blink > 0f)
		{
			this._timer_blink -= pElapsed;
			if (this._timer_blink > 0f)
			{
				return;
			}
			this._cur_blinks--;
			if (this._cur_blinks != 0)
			{
				this.startBlink();
				return;
			}
		}
		if (this._last_alpha <= 0f)
		{
			this.kill();
		}
	}

	// Token: 0x040017B0 RID: 6064
	private float _last_alpha = 1f;

	// Token: 0x040017B1 RID: 6065
	private int _blinks;

	// Token: 0x040017B2 RID: 6066
	private int _cur_blinks = 3;

	// Token: 0x040017B3 RID: 6067
	private float _timer_blink = 0.1f;
}
