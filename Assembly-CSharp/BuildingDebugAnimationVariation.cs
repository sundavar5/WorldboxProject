using System;
using UnityEngine.UI;

// Token: 0x02000554 RID: 1364
public class BuildingDebugAnimationVariation : DebugAnimationVariation
{
	// Token: 0x06002C7B RID: 11387 RVA: 0x0015D584 File Offset: 0x0015B784
	public void update(float pElapsed)
	{
		this.sprite_animation.update(pElapsed);
		this.shadow_animation.update(pElapsed);
	}

	// Token: 0x06002C7C RID: 11388 RVA: 0x0015D59E File Offset: 0x0015B79E
	public void toggleAnimation(bool pState)
	{
		if (pState)
		{
			this.sprite_animation.isOn = true;
			this.shadow_animation.isOn = true;
			return;
		}
		this.sprite_animation.stopAnimations();
		this.shadow_animation.stopAnimations();
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x0015D5D2 File Offset: 0x0015B7D2
	public void setFrame(int pIndex)
	{
		this.sprite_animation.currentFrameIndex = pIndex;
		this.sprite_animation.updateFrame();
		this.shadow_animation.currentFrameIndex = pIndex;
		this.shadow_animation.updateFrame();
	}

	// Token: 0x0400221D RID: 8733
	public Image shadow;

	// Token: 0x0400221E RID: 8734
	public SpriteAnimation shadow_animation;
}
