using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006CA RID: 1738
public class AvatarEffect : MonoBehaviour
{
	// Token: 0x060037A6 RID: 14246 RVA: 0x001915D8 File Offset: 0x0018F7D8
	public void load(StatusAsset pAsset, Actor pActor, UnitAvatarLoader pAvatar)
	{
		this._asset = pAsset;
		this._actor = pActor;
		this._avatar = pAvatar;
		this._animated = pAsset.animated;
		this._rect_transform = base.GetComponent<RectTransform>();
		int tSpriteIndex;
		if (!pAsset.animated)
		{
			if (pAsset.random_frame)
			{
				int tSpritesCount = pAsset.get_sprites_count(pActor, pAsset);
				tSpriteIndex = Randy.randomInt(0, tSpritesCount);
			}
			else
			{
				tSpriteIndex = 0;
			}
		}
		else
		{
			this._time_between_frames = pAsset.animation_speed + Randy.randomFloat(0f, pAsset.animation_speed_random);
			tSpriteIndex = 0;
		}
		this.image.transform.localEulerAngles = this.getSpriteRotation(this._current_frame);
		this.image.sprite = this.getSprite(tSpriteIndex);
	}

	// Token: 0x060037A7 RID: 14247 RVA: 0x0019168C File Offset: 0x0018F88C
	public void update(float pElapsed)
	{
		if (!this._animated)
		{
			return;
		}
		this._elapsed += pElapsed;
		if (this._elapsed < this._time_between_frames)
		{
			return;
		}
		this._elapsed = 0f;
		int tSpritesCount = this._asset.get_sprites_count(this._actor, this._asset);
		this._current_frame = Toolbox.loopIndex(this._current_frame + 1, tSpritesCount);
		Sprite tSprite = this.getSprite(this._current_frame);
		this.image.transform.localPosition = this._initial_position + this.getSpritePosition(this._current_frame);
		this.image.transform.localEulerAngles = this.getSpriteRotation(this._current_frame);
		this.image.sprite = tSprite;
	}

	// Token: 0x060037A8 RID: 14248 RVA: 0x00191758 File Offset: 0x0018F958
	private Sprite getSprite(int pIndex)
	{
		Sprite tResult;
		if (this._asset.has_override_sprite)
		{
			tResult = this._asset.get_override_sprite_ui(this, pIndex);
		}
		else
		{
			tResult = this._asset.sprite_list[pIndex];
		}
		return tResult;
	}

	// Token: 0x060037A9 RID: 14249 RVA: 0x00191798 File Offset: 0x0018F998
	private Vector3 getSpritePosition(int pIndex)
	{
		Vector3 tResult;
		if (this._asset.has_override_sprite)
		{
			tResult = this._asset.get_override_sprite_position_ui(this, pIndex);
		}
		else
		{
			tResult = default(Vector3);
		}
		return tResult;
	}

	// Token: 0x060037AA RID: 14250 RVA: 0x001917D0 File Offset: 0x0018F9D0
	private Vector3 getSpriteRotation(int pIndex)
	{
		Vector3 tResult = default(Vector3);
		if (this._asset.has_override_sprite_rotation_z)
		{
			tResult.z = this._asset.get_override_sprite_rotation_z_ui(this, pIndex);
		}
		else
		{
			tResult.z = this._asset.rotation_z;
		}
		return tResult;
	}

	// Token: 0x060037AB RID: 14251 RVA: 0x00191820 File Offset: 0x0018FA20
	public void setInitialPosition(Vector2 pPosition)
	{
		this._initial_position = pPosition;
	}

	// Token: 0x060037AC RID: 14252 RVA: 0x0019182E File Offset: 0x0018FA2E
	public RectTransform getRectTransform()
	{
		return this._rect_transform;
	}

	// Token: 0x060037AD RID: 14253 RVA: 0x00191836 File Offset: 0x0018FA36
	public UnitAvatarLoader getAvatar()
	{
		return this._avatar;
	}

	// Token: 0x060037AE RID: 14254 RVA: 0x0019183E File Offset: 0x0018FA3E
	public StatusAsset getAsset()
	{
		return this._asset;
	}

	// Token: 0x0400294E RID: 10574
	public Image image;

	// Token: 0x0400294F RID: 10575
	private RectTransform _rect_transform;

	// Token: 0x04002950 RID: 10576
	private Vector3 _initial_position;

	// Token: 0x04002951 RID: 10577
	private StatusAsset _asset;

	// Token: 0x04002952 RID: 10578
	private Actor _actor;

	// Token: 0x04002953 RID: 10579
	private UnitAvatarLoader _avatar;

	// Token: 0x04002954 RID: 10580
	private bool _animated;

	// Token: 0x04002955 RID: 10581
	private float _time_between_frames;

	// Token: 0x04002956 RID: 10582
	private float _elapsed;

	// Token: 0x04002957 RID: 10583
	private int _current_frame;
}
