using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class Cloud : BaseEffect
{
	// Token: 0x06001FDE RID: 8158 RVA: 0x001126F1 File Offset: 0x001108F1
	internal override void create()
	{
		base.create();
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x001126FC File Offset: 0x001108FC
	internal override void prepare()
	{
		this.sprite_renderer.sprite = Randy.getRandom<Sprite>(this.asset.cached_sprites);
		this.sprite_renderer.flipX = Randy.randomBool();
		this.speed = Randy.randomFloat(this.asset.speed_min, this.asset.speed_max);
		this.effect_texture_width = this.sprite_renderer.sprite.rect.width * 0.08f;
		this.effect_texture_height = this.sprite_renderer.sprite.rect.height * 0.04f;
		this._timer_action_1 = this.asset.interval_action_1;
		this._lifespan = 0f;
		this.alive_time = 0f;
		base.prepare();
		base.setAlpha(0f);
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x001127D5 File Offset: 0x001109D5
	public void setLifespan(float pLifespan)
	{
		this._lifespan = pLifespan;
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x001127DE File Offset: 0x001109DE
	internal void setType(CloudAsset pAsset)
	{
		this.asset = pAsset;
		this.sprite_renderer.color = this.asset.color;
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x00112800 File Offset: 0x00110A00
	internal void setType(string pType)
	{
		CloudAsset tAsset = AssetManager.clouds.get(pType);
		if (tAsset == null)
		{
			return;
		}
		this.setType(tAsset);
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x00112824 File Offset: 0x00110A24
	public void spawn(WorldTile pTile, string pType)
	{
		if (pTile == null)
		{
			this.setType(pType);
			this.prepare();
			return;
		}
		this.prepare(pTile.posV3, pType);
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x00112844 File Offset: 0x00110A44
	internal void prepare(Vector3 pVec, string pType)
	{
		this.setType(pType);
		this.prepare();
		pVec.y -= this.spriteShadow.offset.y;
		base.transform.localPosition = pVec;
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x0011287D File Offset: 0x00110A7D
	internal override void prepare(WorldTile pTile, float pScale = 0.5f)
	{
		this.prepare();
		base.transform.localPosition = new Vector3(pTile.posV3.x, pTile.posV3.y);
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x001128AC File Offset: 0x00110AAC
	public override void update(float pElapsed)
	{
		this.alive_time += pElapsed;
		if (Config.time_scale_asset.sonic)
		{
			this._fade_multiplier = 0.05f;
		}
		else
		{
			this._fade_multiplier = 0.2f;
		}
		if (this.asset.draw_light_area)
		{
			Vector2 tPos = base.transform.localPosition;
			tPos.x += this.asset.draw_light_area_offset_x;
			tPos.y += this.asset.draw_light_area_offset_y;
			World.world.stack_effects.light_blobs.Add(new LightBlobData
			{
				position = tPos,
				radius = this.asset.draw_light_size
			});
		}
		if (!World.world.isPaused())
		{
			base.transform.Translate(this.speed * pElapsed, 0f, 0f);
			if (this.asset.cloud_action_1 != null)
			{
				if (this._timer_action_1 > 0f)
				{
					this._timer_action_1 -= pElapsed;
				}
				else
				{
					this._timer_action_1 = this.asset.interval_action_1;
					this.asset.cloud_action_1(this);
				}
			}
			if (this.asset.cloud_action_2 != null)
			{
				if (this._timer_action_2 > 0f)
				{
					this._timer_action_2 -= pElapsed;
				}
				else
				{
					this._timer_action_2 = this.asset.interval_action_2;
					this.asset.cloud_action_2(this);
				}
			}
		}
		if (base.transform.localPosition.x > (float)MapBox.width || (this._lifespan > 0f && this.alive_time > this._lifespan))
		{
			base.startToDie();
		}
		float mAlpha = this.asset.max_alpha;
		if (World.world.camera != null && World.world.camera.orthographicSize > 0f)
		{
			mAlpha *= World.world.camera.orthographicSize / 100f;
			if (mAlpha > this.asset.max_alpha)
			{
				mAlpha = this.asset.max_alpha;
			}
		}
		int state = this.state;
		if (state == 1)
		{
			if (this.alpha < mAlpha)
			{
				this.alpha += pElapsed * this._fade_multiplier;
				if (this.alpha >= mAlpha)
				{
					this.alpha = mAlpha;
				}
			}
			else if (this.alpha > mAlpha)
			{
				this.alpha -= pElapsed * this._fade_multiplier;
				if (this.alpha <= mAlpha)
				{
					this.alpha = mAlpha;
				}
			}
			else
			{
				this.alpha = mAlpha;
			}
			base.setAlpha(this.alpha);
			return;
		}
		if (state != 2)
		{
			return;
		}
		if (this.alpha > 0f)
		{
			this.alpha -= pElapsed * this._fade_multiplier;
			base.setAlpha(this.alpha);
			return;
		}
		this.alpha = 0f;
		this.controller.killObject(this);
	}

	// Token: 0x04001734 RID: 5940
	public CloudAsset asset;

	// Token: 0x04001735 RID: 5941
	private float speed = 1f;

	// Token: 0x04001736 RID: 5942
	public SpriteShadow spriteShadow;

	// Token: 0x04001737 RID: 5943
	private float _timer_action_1;

	// Token: 0x04001738 RID: 5944
	private float _timer_action_2;

	// Token: 0x04001739 RID: 5945
	internal float alive_time;

	// Token: 0x0400173A RID: 5946
	private float _fade_multiplier = 0.2f;

	// Token: 0x0400173B RID: 5947
	internal float effect_texture_width;

	// Token: 0x0400173C RID: 5948
	internal float effect_texture_height;

	// Token: 0x0400173D RID: 5949
	private float _lifespan;
}
