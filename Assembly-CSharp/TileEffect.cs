using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class TileEffect : BaseEffect
{
	// Token: 0x0600208B RID: 8331 RVA: 0x00116B48 File Offset: 0x00114D48
	public void load(TileEffectAsset pAsset)
	{
		Sprite[] tFrames = pAsset.getSprites();
		this.sprite_animation.setFrames(tFrames);
		this.sprite_animation.resetAnim(0);
		this.sprite_animation.timeBetweenFrames = pAsset.time_between_frames;
	}
}
