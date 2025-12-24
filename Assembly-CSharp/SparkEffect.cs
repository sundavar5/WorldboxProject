using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class SparkEffect : BaseEffect
{
	// Token: 0x06002066 RID: 8294 RVA: 0x00115EE0 File Offset: 0x001140E0
	internal override void prepare(Vector2 pVector, float pScale = 1f)
	{
		base.prepare(pVector, pScale);
		base.setAlpha(1f);
		this.sprite_animation.setFrames(this._sprite_sets.GetRandom<SpriteSet>().sprites);
		this._speed = 10f + Randy.randomFloat(-5f, 5f);
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x00115F36 File Offset: 0x00114136
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		base.transform.position += new Vector3(0f, this._speed * Time.deltaTime, 0f);
	}

	// Token: 0x04001790 RID: 6032
	private const float BASE_ALPHA = 1f;

	// Token: 0x04001791 RID: 6033
	private const float BASE_SPEED = 10f;

	// Token: 0x04001792 RID: 6034
	private const float RANDOM_OFFSET = 5f;

	// Token: 0x04001793 RID: 6035
	[SerializeField]
	private List<SpriteSet> _sprite_sets;

	// Token: 0x04001794 RID: 6036
	private float _speed = 10f;
}
