using System;
using System.Runtime.CompilerServices;

// Token: 0x02000171 RID: 369
public class Status : CoreSystemObject<StatusData>
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000A1E54 File Offset: 0x000A0054
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.statuses;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000A1E60 File Offset: 0x000A0060
	public int get_sprites_count
	{
		get
		{
			return this._asset.get_sprites_count(this._sim_object, this._asset);
		}
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x000A1E7E File Offset: 0x000A007E
	public void setDuration(float pDuration)
	{
		this._end_time = World.world.getCurWorldTime() + (double)pDuration;
		this.duration = pDuration;
		this._finished = false;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x000A1EA1 File Offset: 0x000A00A1
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double getRemainingTime()
	{
		return this._end_time - World.world.getCurWorldTime();
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x000A1EB4 File Offset: 0x000A00B4
	protected override void setDefaultValues()
	{
		this._finished = false;
		this._action_timer = 0f;
		this._anim_frame = 0;
		this._anim_time_between_frames = 0f;
		this._anim_timer = 0f;
		this.flip_x = false;
		this._end_time = 0.0;
		this.duration = 0f;
		this._sim_object = null;
		this._has_action = false;
		this._is_animated = false;
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000A1F28 File Offset: 0x000A0128
	public void start(BaseSimObject pSimObject, StatusAsset pAsset)
	{
		this._sim_object = pSimObject;
		this._asset = pAsset;
		this._action_timer = this._asset.action_interval;
		this.setDuration(this._asset.duration);
		if (this._asset.random_frame)
		{
			this._anim_frame = Randy.randomInt(0, this.get_sprites_count);
		}
		if (this._asset.random_flip)
		{
			this.flip_x = Randy.randomBool();
		}
		if (this._asset.sprite_list != null)
		{
			this._anim_time_between_frames = this._asset.animation_speed + Randy.randomFloat(0f, this._asset.animation_speed_random);
			this._anim_timer = this._anim_time_between_frames;
		}
		this._has_action = (this._asset.action != null);
		this._is_animated = (this._asset.animated && this._asset.texture != null);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x000A2018 File Offset: 0x000A0218
	internal void updateAnimationFrame(float pElapsed)
	{
		if (!this._is_animated)
		{
			return;
		}
		this._anim_timer -= pElapsed;
		if (this._anim_timer <= 0f)
		{
			int tFrameCount = this.get_sprites_count;
			this._anim_timer = this._anim_time_between_frames;
			this._anim_frame++;
			if (this._anim_frame >= tFrameCount && this._asset.loop)
			{
				this._anim_frame = 0;
				return;
			}
			if (this._anim_frame >= tFrameCount)
			{
				this._anim_frame = tFrameCount - 1;
			}
		}
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000A209C File Offset: 0x000A029C
	private void updateActionTimer(float pElapsed)
	{
		float tActionInterval = this._asset.action_interval;
		if (this._action_timer > 0f)
		{
			this._action_timer -= pElapsed;
			return;
		}
		this._action_timer = tActionInterval;
		if (this._sim_object.isAlive())
		{
			WorldAction action = this._asset.action;
			if (action == null)
			{
				return;
			}
			action(this._sim_object, this._sim_object.current_tile);
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x000A210C File Offset: 0x000A030C
	public void update(float pElapsed, float pWorldTime)
	{
		if (this._has_action)
		{
			this.updateActionTimer(pElapsed);
		}
		if ((double)pWorldTime >= this._end_time)
		{
			this.finish();
			if (this._sim_object.isAlive())
			{
				WorldAction action_finish = this._asset.action_finish;
				if (action_finish == null)
				{
					return;
				}
				action_finish(this._sim_object, this._sim_object.current_tile);
			}
		}
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x000A216C File Offset: 0x000A036C
	public void finish()
	{
		this._finished = true;
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000B28 RID: 2856 RVA: 0x000A2175 File Offset: 0x000A0375
	public BaseSimObject sim_object
	{
		get
		{
			return this._sim_object;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000B29 RID: 2857 RVA: 0x000A217D File Offset: 0x000A037D
	public bool is_finished
	{
		get
		{
			return this._finished;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000B2A RID: 2858 RVA: 0x000A2185 File Offset: 0x000A0385
	public StatusAsset asset
	{
		get
		{
			return this._asset;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000A218D File Offset: 0x000A038D
	public int anim_frame
	{
		get
		{
			return this._anim_frame;
		}
	}

	// Token: 0x04000ACC RID: 2764
	private float _action_timer;

	// Token: 0x04000ACD RID: 2765
	private StatusAsset _asset;

	// Token: 0x04000ACE RID: 2766
	private bool _finished;

	// Token: 0x04000ACF RID: 2767
	private BaseSimObject _sim_object;

	// Token: 0x04000AD0 RID: 2768
	private int _anim_frame;

	// Token: 0x04000AD1 RID: 2769
	private float _anim_time_between_frames;

	// Token: 0x04000AD2 RID: 2770
	private float _anim_timer;

	// Token: 0x04000AD3 RID: 2771
	public bool flip_x;

	// Token: 0x04000AD4 RID: 2772
	private double _end_time;

	// Token: 0x04000AD5 RID: 2773
	public float duration;

	// Token: 0x04000AD6 RID: 2774
	private bool _has_action;

	// Token: 0x04000AD7 RID: 2775
	private bool _is_animated;
}
