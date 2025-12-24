using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class BaseEffect : BaseAnimatedObject
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00111ABA File Offset: 0x0010FCBA
	public double timestamp_spawned
	{
		get
		{
			return this._timestamp_spawned;
		}
	}

	// Token: 0x06001FAC RID: 8108 RVA: 0x00111AC2 File Offset: 0x0010FCC2
	public override void Awake()
	{
		this.sprite_renderer = base.GetComponent<SpriteRenderer>();
		base.Awake();
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x00111AD6 File Offset: 0x0010FCD6
	public void activate()
	{
		this.active = true;
		base.gameObject.SetActive(true);
		this.state = 1;
		this._timestamp_spawned = (double)Time.time;
		this.clear();
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x00111B04 File Offset: 0x0010FD04
	internal void attachTo(Actor pActor)
	{
		this.attachedToActor = pActor;
		this.updateAttached();
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x00111B13 File Offset: 0x0010FD13
	internal void makeParentController()
	{
		base.transform.SetParent(this.controller.transform, true);
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x00111B2C File Offset: 0x0010FD2C
	internal virtual void prepare(WorldTile pTile, float pScale = 0.5f)
	{
		this.state = 1;
		base.transform.localEulerAngles = Vector3.zero;
		this.current_position = new Vector3((float)pTile.pos.x + 0.5f, (float)pTile.pos.y);
		base.transform.localPosition = this.current_position;
		this.setScale(pScale);
		this.setAlpha(1f);
		base.resetAnim();
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x00111BB2 File Offset: 0x0010FDB2
	public void setScale(float pScale)
	{
		this.scale = pScale;
		if (this.scale < 0f)
		{
			this.scale = 0f;
		}
		base.transform.localScale = new Vector3(pScale, pScale);
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x00111BE8 File Offset: 0x0010FDE8
	internal virtual void prepare(Vector2 pVector, float pScale = 1f)
	{
		this.state = 1;
		base.transform.rotation = Quaternion.identity;
		base.transform.localPosition = pVector;
		this.setScale(pScale);
		this.setAlpha(1f);
		base.resetAnim();
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x00111C38 File Offset: 0x0010FE38
	protected void setAlpha(float pVal)
	{
		this.alpha = pVal;
		Color tColor = this.sprite_renderer.color;
		tColor.a = this.alpha;
		this.sprite_renderer.color = tColor;
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x00111C71 File Offset: 0x0010FE71
	internal virtual void prepare()
	{
		base.transform.position = new Vector3((float)Randy.randomInt(-50, 30), (float)Randy.randomInt(0, MapBox.height + 25));
		this.state = 1;
		this.setAlpha(0f);
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x00111CAE File Offset: 0x0010FEAE
	internal virtual void spawnOnTile(WorldTile pTile)
	{
		this.tile = pTile;
		base.transform.localPosition = new Vector3(pTile.posV3.x, pTile.posV3.y);
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x00111CDD File Offset: 0x0010FEDD
	internal void startToDie()
	{
		this.state = 2;
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x00111CE6 File Offset: 0x0010FEE6
	public virtual void kill()
	{
		this.state = 3;
		this.controller.killObject(this);
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x00111CFC File Offset: 0x0010FEFC
	public void deactivate()
	{
		if (this.fmod_instance.isValid())
		{
			this.fmod_instance.stop(STOP_MODE.ALLOWFADEOUT);
			this.fmod_instance.release();
		}
		this.active = false;
		base.transform.SetParent(base.transform);
		base.gameObject.SetActive(false);
		this.clear();
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00111D59 File Offset: 0x0010FF59
	public void clear()
	{
		this.tile = null;
		this.attachedToActor = null;
		this.callback = null;
		this.callbackOnFrame = -1;
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x00111D78 File Offset: 0x0010FF78
	public override void update(float pElapsed)
	{
		if (this.controller.asset.draw_light_area)
		{
			Vector2 tPos = base.transform.position;
			tPos.x += this.controller.asset.draw_light_area_offset_x;
			tPos.y += this.controller.asset.draw_light_area_offset_y;
			World.world.stack_effects.light_blobs.Add(new LightBlobData
			{
				position = tPos,
				radius = this.controller.asset.draw_light_size
			});
		}
		if (World.world.isPaused() && DebugConfig.isOn(DebugOption.PauseEffects))
		{
			return;
		}
		if (this.attachedToActor != null)
		{
			this.updateAttached();
		}
		base.update(pElapsed);
		if (this.callbackOnFrame != -1 && this.sprite_animation.currentFrameIndex == this.callbackOnFrame)
		{
			this.callback();
			this.clear();
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x00111E78 File Offset: 0x00110078
	private void updateAttached()
	{
		if (!this.attachedToActor.isAlive())
		{
			this.kill();
			return;
		}
		this.sprite_renderer.flipX = this.attachedToActor.a.flip;
		base.transform.localScale = this.attachedToActor.current_scale;
		base.transform.localPosition = this.attachedToActor.cur_transform_position;
		base.transform.eulerAngles = this.attachedToActor.current_rotation;
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x00111EF6 File Offset: 0x001100F6
	public void setCallback(int pFrame, BaseCallback pCallback)
	{
		this.callbackOnFrame = pFrame;
		this.callback = pCallback;
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x00111F06 File Offset: 0x00110106
	public bool isKilled()
	{
		return this.state == 3;
	}

	// Token: 0x0400170B RID: 5899
	private const int MAP_MARGIN_TOP = 25;

	// Token: 0x0400170C RID: 5900
	private const int MAP_OFFSET_BOTTOM_MIN = -50;

	// Token: 0x0400170D RID: 5901
	private const int MAP_OFFSET_BOTTOM_MAX = 30;

	// Token: 0x0400170E RID: 5902
	internal bool active;

	// Token: 0x0400170F RID: 5903
	internal int effectIndex;

	// Token: 0x04001710 RID: 5904
	public const int STATE_START = 1;

	// Token: 0x04001711 RID: 5905
	public const int STATE_ON_DEATH = 2;

	// Token: 0x04001712 RID: 5906
	public const int STATE_KILLED = 3;

	// Token: 0x04001713 RID: 5907
	protected float scale;

	// Token: 0x04001714 RID: 5908
	protected float alpha;

	// Token: 0x04001715 RID: 5909
	public WorldTile tile;

	// Token: 0x04001716 RID: 5910
	internal BaseEffectController controller;

	// Token: 0x04001717 RID: 5911
	internal int state;

	// Token: 0x04001718 RID: 5912
	private double _timestamp_spawned;

	// Token: 0x04001719 RID: 5913
	internal SpriteRenderer sprite_renderer;

	// Token: 0x0400171A RID: 5914
	internal BaseCallback callback;

	// Token: 0x0400171B RID: 5915
	internal int callbackOnFrame = -1;

	// Token: 0x0400171C RID: 5916
	internal EventInstance fmod_instance;

	// Token: 0x0400171D RID: 5917
	internal Actor attachedToActor;
}
