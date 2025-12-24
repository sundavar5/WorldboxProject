using System;
using UnityEngine;

// Token: 0x02000684 RID: 1668
public class EffectParticlesCursor : MonoBehaviour
{
	// Token: 0x06003592 RID: 13714 RVA: 0x001891C5 File Offset: 0x001873C5
	private void Awake()
	{
		this._sprite_animation = base.GetComponent<SpriteAnimationSimple>();
	}

	// Token: 0x06003593 RID: 13715 RVA: 0x001891D3 File Offset: 0x001873D3
	public void launch()
	{
		this._sprite_animation.resetAnim();
		this._speed = 50f + Randy.randomFloat(-10f, 10f);
	}

	// Token: 0x06003594 RID: 13716 RVA: 0x001891FC File Offset: 0x001873FC
	public void update()
	{
		this._sprite_animation.update(Time.deltaTime);
		base.transform.position += new Vector3(0f, this._speed * Time.deltaTime, 0f);
	}

	// Token: 0x06003595 RID: 13717 RVA: 0x0018924A File Offset: 0x0018744A
	public SpriteAnimationSimple getAnimation()
	{
		return this._sprite_animation;
	}

	// Token: 0x06003596 RID: 13718 RVA: 0x00189252 File Offset: 0x00187452
	public void setFrames(Sprite[] pFrames)
	{
		this._sprite_animation.setFrames(pFrames);
	}

	// Token: 0x040027F1 RID: 10225
	private SpriteAnimationSimple _sprite_animation;

	// Token: 0x040027F2 RID: 10226
	private float _speed = 50f;
}
