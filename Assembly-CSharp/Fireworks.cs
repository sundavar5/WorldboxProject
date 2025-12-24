using System;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class Fireworks : BaseEffect
{
	// Token: 0x06002011 RID: 8209 RVA: 0x00113E4C File Offset: 0x0011204C
	internal override void spawnOnTile(WorldTile pTile)
	{
		float tScale = Randy.randomFloat(0.3f, 1f);
		this.prepare(pTile, tScale);
		if (Randy.randomBool())
		{
			this.loadSprites("effects/fireworks1");
		}
		else
		{
			this.loadSprites("effects/fireworks2");
		}
		this.sprite_renderer.flipX = Randy.randomBool();
		Color tColor = default(Color);
		tColor.a = 1f;
		tColor.r = Randy.randomFloat(0f, 1f);
		tColor.b = Randy.randomFloat(0f, 1f);
		tColor.g = Randy.randomFloat(0f, 1f);
		this.sprite_renderer.color = tColor;
		float tRotation = Randy.randomFloat(-15f, 15f);
		base.transform.localEulerAngles = new Vector3(0f, 0f, tRotation);
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x00113F30 File Offset: 0x00112130
	private void loadSprites(string pPath)
	{
		Sprite[] tSprites = SpriteTextureLoader.getSpriteList(pPath, false);
		this.sprite_animation.frames = tSprites;
	}
}
