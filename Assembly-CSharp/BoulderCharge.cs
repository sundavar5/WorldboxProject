using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class BoulderCharge : BaseEffect
{
	// Token: 0x06001FD3 RID: 8147 RVA: 0x00112358 File Offset: 0x00110558
	internal override void prepare(Vector2 pVector, float pScale = 1f)
	{
		base.prepare(pVector, pScale);
		this._direction = Boulder.chargeVector();
		this._direction.x = this._direction.x + Randy.randomFloat(-20f, 20f);
		this._direction.y = this._direction.y + Randy.randomFloat(-20f, 20f);
		base.setAlpha(1f);
		this.sprite_animation.setFrames(this._sprite_sets.GetRandom<SpriteSet>().sprites);
		this.sprite_animation.timeBetweenFrames = 0.2f / this._direction.magnitude;
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x001123F8 File Offset: 0x001105F8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		base.transform.position += new Vector3((this._direction.x + Randy.randomFloat(-20f, 20f)) * Time.deltaTime, this._direction.y * Time.deltaTime, 0f);
		base.setAlpha(this.alpha - 0.001f);
	}

	// Token: 0x04001728 RID: 5928
	private const float BASE_ALPHA = 1f;

	// Token: 0x04001729 RID: 5929
	private const float ALPHA_CHANGE = 0.001f;

	// Token: 0x0400172A RID: 5930
	private const float RANDOM_OFFSET = 20f;

	// Token: 0x0400172B RID: 5931
	private const float BASE_TIME_BETWEEN_FRAMES = 0.2f;

	// Token: 0x0400172C RID: 5932
	[SerializeField]
	private List<SpriteSet> _sprite_sets;

	// Token: 0x0400172D RID: 5933
	private Vector2 _direction;
}
