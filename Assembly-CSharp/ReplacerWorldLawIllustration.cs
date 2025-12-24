using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A3 RID: 1699
public class ReplacerWorldLawIllustration : MonoBehaviour
{
	// Token: 0x06003662 RID: 13922 RVA: 0x0018B7FC File Offset: 0x001899FC
	private void Awake()
	{
		this._target_image = base.GetComponent<Image>();
	}

	// Token: 0x06003663 RID: 13923 RVA: 0x0018B80A File Offset: 0x00189A0A
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			this._target_image.sprite = this.image_cursed;
			return;
		}
		this._target_image.sprite = this.image_normal;
	}

	// Token: 0x0400284F RID: 10319
	private Image _target_image;

	// Token: 0x04002850 RID: 10320
	public Sprite image_normal;

	// Token: 0x04002851 RID: 10321
	public Sprite image_cursed;
}
