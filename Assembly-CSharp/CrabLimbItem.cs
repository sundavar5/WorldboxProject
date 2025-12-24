using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
[RequireComponent(typeof(SpriteRenderer))]
public class CrabLimbItem : MonoBehaviour
{
	// Token: 0x06002311 RID: 8977 RVA: 0x001248B5 File Offset: 0x00122AB5
	private void Awake()
	{
		this._sprite_renderer = base.GetComponent<SpriteRenderer>();
		this._sprite_renderer.sprite = this.high_hp;
		this._shade = this._sprite_renderer.color;
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x001248E8 File Offset: 0x00122AE8
	internal void stateChange(CrabLimbState pState)
	{
		switch (pState)
		{
		case CrabLimbState.HighHP:
			this._sprite_renderer.sprite = this.high_hp;
			break;
		case CrabLimbState.MedHP:
			this._sprite_renderer.sprite = this.med_hp;
			break;
		case CrabLimbState.LowHP:
			this._sprite_renderer.sprite = this.low_hp;
			break;
		}
		this._sprite_renderer.color = this._dmg;
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x00124951 File Offset: 0x00122B51
	internal void flicker(float pProgress)
	{
		this._sprite_renderer.color = Color.Lerp(this._dmg, this._shade, pProgress);
	}

	// Token: 0x04001942 RID: 6466
	public CrabLimb crabLimb;

	// Token: 0x04001943 RID: 6467
	public Sprite high_hp;

	// Token: 0x04001944 RID: 6468
	public Sprite med_hp;

	// Token: 0x04001945 RID: 6469
	public Sprite low_hp;

	// Token: 0x04001946 RID: 6470
	internal SpriteRenderer _sprite_renderer;

	// Token: 0x04001947 RID: 6471
	private Color _shade;

	// Token: 0x04001948 RID: 6472
	private Color _dmg = new Color(1f, 0f, 0f, 1f);
}
