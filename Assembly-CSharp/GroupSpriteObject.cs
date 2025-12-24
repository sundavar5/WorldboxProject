using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class GroupSpriteObject : MonoBehaviour
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x0600201A RID: 8218 RVA: 0x0011403A File Offset: 0x0011223A
	public bool has_sprite_renderer
	{
		get
		{
			return this._has_sprite_renderer;
		}
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x00114042 File Offset: 0x00112242
	private void Awake()
	{
		this.create();
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x0011404A File Offset: 0x0011224A
	protected void create()
	{
		this.m_transform = base.gameObject.transform;
		this.sprite_renderer = base.gameObject.GetComponent<SpriteRenderer>();
		if (this.sprite_renderer != null)
		{
			this._has_sprite_renderer = true;
		}
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x00114084 File Offset: 0x00112284
	public void checkRotation(Vector3 pPos, BaseSimObject pSimObject, float pZ)
	{
		pPos.z = pZ;
		Vector3 tPivot = default(Vector3);
		ref Vector3 tAngle = ref pSimObject.current_rotation;
		if (tAngle.y != 0f || tAngle.z != 0f)
		{
			tPivot.Set(pSimObject.cur_transform_position.x, pSimObject.cur_transform_position.y, 0f);
			pPos = Toolbox.RotatePointAroundPivot(ref pPos, ref tPivot, ref tAngle);
			pPos.z = pZ;
		}
		this.setPosOnly(ref pPos);
		this.setLocalEulerAngles(pSimObject.current_rotation);
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x0011410C File Offset: 0x0011230C
	public void setPosOnly(Vector2 pPosition)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x00114164 File Offset: 0x00112364
	public void setPosOnly(ref Vector2 pPosition)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y || this._last_pos_v3.z != 0f)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x001141D8 File Offset: 0x001123D8
	public void setPosOnly(ref Vector3 pPosition)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y || this._last_pos_v3.z != pPosition.z)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0011424C File Offset: 0x0011244C
	public void setRotation(ref Vector3 pVec)
	{
		if (this._last_angles_v3.y != pVec.y || this._last_angles_v3.z != pVec.z)
		{
			this._last_angles_v3 = pVec;
			this.m_transform.eulerAngles = pVec;
		}
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0011429C File Offset: 0x0011249C
	public void setLocalEulerAngles(Vector3 pVec)
	{
		if (this._last_angles_v3.y != pVec.y || this._last_angles_v3.z != pVec.z)
		{
			this._last_angles_v3 = pVec;
			this.m_transform.localEulerAngles = pVec;
		}
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x001142D8 File Offset: 0x001124D8
	public void setSprite(Sprite pSprite)
	{
		int tHashCode = pSprite.GetHashCode();
		if (this._last_sprite_hash_code != tHashCode)
		{
			this.sprite_renderer.sprite = pSprite;
			this._last_sprite_hash_code = tHashCode;
		}
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x00114308 File Offset: 0x00112508
	public void setFlipX(bool pFlipX)
	{
		if (this._last_flip_x == pFlipX)
		{
			return;
		}
		this._last_flip_x = pFlipX;
		this.sprite_renderer.flipX = pFlipX;
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x00114328 File Offset: 0x00112528
	public void setSharedMat(Material pMaterial)
	{
		int tHashCode = pMaterial.GetHashCode();
		if (this._last_sprite_material != tHashCode)
		{
			this.sprite_renderer.sharedMaterial = pMaterial;
			this._last_sprite_material = tHashCode;
		}
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x00114358 File Offset: 0x00112558
	public void setColor(ref Color pColor)
	{
		if (this._last_color.r != pColor.r || this._last_color.g != pColor.g || this._last_color.b != pColor.b || this._last_color.a != pColor.a)
		{
			this.sprite_renderer.color = pColor;
			this._last_color = pColor;
		}
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x001143CE File Offset: 0x001125CE
	public void setScale(float pScale)
	{
		if (this._last_scale_v3.y != pScale)
		{
			this._last_scale_v2 = new Vector2(pScale, pScale);
			this._last_scale_v3 = this._last_scale_v2;
			this.m_transform.localScale = this._last_scale_v3;
		}
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x00114410 File Offset: 0x00112610
	public void setScale(float pScaleX, float pScaleY)
	{
		if (this._last_scale_v2.y != pScaleY || this._last_scale_v2.x != pScaleX)
		{
			this._last_scale_v2 = new Vector2(pScaleX, pScaleY);
			this._last_scale_v3 = this._last_scale_v2;
			this.m_transform.localScale = this._last_scale_v3;
		}
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x00114468 File Offset: 0x00112668
	public void setScale(ref Vector3 pScaleVec)
	{
		if (this._last_scale_v3 != pScaleVec)
		{
			this._last_scale_v3 = pScaleVec;
			this.m_transform.localScale = pScaleVec;
		}
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0011449C File Offset: 0x0011269C
	public void set(ref Vector2 pPosition, ref Vector3 pScale)
	{
		if (this._last_pos_v2.x != pPosition.x || this._last_pos_v2.y != pPosition.y)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
		if (this._last_scale_v2.y != pScale.y || this._last_scale_v2.x != pScale.x)
		{
			this._last_scale_v2 = pScale;
			this._last_scale_v3 = this._last_scale_v2;
			this.m_transform.localScale = pScale;
		}
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x00114558 File Offset: 0x00112758
	public void set(ref Vector2 pPosition, float pScale)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
		if (this._last_scale_v2.x != pScale)
		{
			this.setScale(pScale);
		}
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x001145D0 File Offset: 0x001127D0
	public void set(ref Vector3 pPosition, float pScale)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y || this._last_pos_v3.z != pPosition.z)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
		if (this._last_scale_v2.x != pScale)
		{
			this.setScale(pScale);
		}
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x0011465C File Offset: 0x0011285C
	public void set(ref Vector3 pPosition, ref Vector2 pScale)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y || this._last_pos_v3.z != pPosition.z)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
		if (this._last_scale_v2.y != pScale.y || this._last_scale_v2.x != pScale.x)
		{
			this._last_scale_v2 = pScale;
			this._last_scale_v3 = this._last_scale_v2;
			this.m_transform.localScale = pScale;
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x0011472C File Offset: 0x0011292C
	public void set(ref Vector3 pPosition, ref Vector3 pScale)
	{
		if (this._last_pos_v3.x != pPosition.x || this._last_pos_v3.y != pPosition.y)
		{
			this._last_pos_v2 = pPosition;
			this._last_pos_v3 = pPosition;
			this.m_transform.localPosition = this._last_pos_v3;
		}
		if (this._last_scale_v2.y != pScale.y || this._last_scale_v2.x != pScale.x)
		{
			this._last_scale_v2 = pScale;
			this._last_scale_v3 = this._last_scale_v2;
			this.m_transform.localScale = pScale;
		}
	}

	// Token: 0x04001759 RID: 5977
	internal EventInstance fmod_instance;

	// Token: 0x0400175A RID: 5978
	internal Transform m_transform;

	// Token: 0x0400175B RID: 5979
	internal SpriteRenderer sprite_renderer;

	// Token: 0x0400175C RID: 5980
	private bool _has_sprite_renderer;

	// Token: 0x0400175D RID: 5981
	private Vector2 _last_pos_v2 = new Vector2(-1f, -1f);

	// Token: 0x0400175E RID: 5982
	private Vector2 _last_scale_v2 = new Vector2(-1f, -1f);

	// Token: 0x0400175F RID: 5983
	private Vector3 _last_pos_v3 = new Vector3(-1f, -1f, -1f);

	// Token: 0x04001760 RID: 5984
	private Vector3 _last_scale_v3 = new Vector3(-1f, -1f, -1f);

	// Token: 0x04001761 RID: 5985
	private Vector3 _last_angles_v3 = new Vector3(-1f, -1f, -1f);

	// Token: 0x04001762 RID: 5986
	private Color _last_color;

	// Token: 0x04001763 RID: 5987
	private bool _last_flip_x;

	// Token: 0x04001764 RID: 5988
	private int _last_sprite_hash_code = -1;

	// Token: 0x04001765 RID: 5989
	private int _last_sprite_material = -1;

	// Token: 0x04001766 RID: 5990
	public int last_id;
}
