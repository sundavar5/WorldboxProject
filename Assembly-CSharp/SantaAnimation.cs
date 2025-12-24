using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class SantaAnimation : BaseMapObject
{
	// Token: 0x06002133 RID: 8499 RVA: 0x0011B4E4 File Offset: 0x001196E4
	internal override void create()
	{
		base.create();
		this.tStr = new Vector3(this.shakeX, this.shakeY);
		this.shakeTween = base.transform.DOShakePosition(0.5f, this.tStr, 10, 90f, false, false, ShakeRandomnessMode.Full);
		this.santa = base.transform.parent.GetComponent<Santa>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x0011B558 File Offset: 0x00119758
	private void Update()
	{
		if (this.santa.alive)
		{
			this.spriteRenderer.sharedMaterial = this.santa.current_material;
		}
		else
		{
			this.spriteRenderer.sharedMaterial = LibraryMaterials.instance.mat_world_object;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!this.shakeTween.active)
		{
			this.shakeTween = base.transform.DOShakePosition(0.5f, this.tStr, 10, 90f, false, false, ShakeRandomnessMode.Full);
		}
	}

	// Token: 0x0400189B RID: 6299
	public float shakeX = 2f;

	// Token: 0x0400189C RID: 6300
	public float shakeY = 0.3f;

	// Token: 0x0400189D RID: 6301
	private Tween shakeTween;

	// Token: 0x0400189E RID: 6302
	private Vector3 tStr;

	// Token: 0x0400189F RID: 6303
	private Santa santa;

	// Token: 0x040018A0 RID: 6304
	private SpriteRenderer spriteRenderer;
}
