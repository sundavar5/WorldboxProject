using System;

// Token: 0x020001BE RID: 446
public class BaseAnimatedObject : BaseMapObject
{
	// Token: 0x06000CE7 RID: 3303 RVA: 0x000BA240 File Offset: 0x000B8440
	public virtual void Awake()
	{
		this.sprite_animation = base.gameObject.GetComponent<SpriteAnimation>();
		this._has_sprite_animation = (this.sprite_animation != null);
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x000BA265 File Offset: 0x000B8465
	internal override void create()
	{
		base.create();
		if (this._has_sprite_animation)
		{
			this.sprite_animation.create();
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x000BA280 File Offset: 0x000B8480
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateSpriteAnimation(pElapsed, false);
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000BA291 File Offset: 0x000B8491
	internal void resetAnim()
	{
		if (this._has_sprite_animation)
		{
			this.sprite_animation.resetAnim(0);
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000BA2A7 File Offset: 0x000B84A7
	internal void updateSpriteAnimation(float pElapsed, bool pForce = false)
	{
		if (this._has_sprite_animation)
		{
			this.sprite_animation.update(pElapsed);
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x000BA2BD File Offset: 0x000B84BD
	public override void Dispose()
	{
		this.sprite_animation = null;
		base.Dispose();
	}

	// Token: 0x04000C8C RID: 3212
	internal SpriteAnimation sprite_animation;

	// Token: 0x04000C8D RID: 3213
	private bool _has_sprite_animation;
}
