using System;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class HeatRayEffect : BaseAnimatedObject
{
	// Token: 0x06002030 RID: 8240 RVA: 0x00114881 File Offset: 0x00112A81
	public override void Awake()
	{
		base.Awake();
		this.ray.transform.localScale = new Vector3(1f, 0f, 1f);
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x001148B0 File Offset: 0x00112AB0
	private void Update()
	{
		this.update(World.world.elapsed);
		this.ray.update(World.world.elapsed);
		this.heat.update(World.world.elapsed);
		if (this.ticksActive > 0)
		{
			this.ticksActive--;
			return;
		}
		this.active = false;
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x00114916 File Offset: 0x00112B16
	internal bool isReady()
	{
		return this.touchedGround;
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0011491E File Offset: 0x00112B1E
	public Vector2 getPosForLight()
	{
		return this.heat.transform.position;
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x00114938 File Offset: 0x00112B38
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		Vector3 tVec = this.ray.transform.position;
		tVec.z = base.transform.position.y;
		this.ray.transform.position = tVec;
		tVec = this.heat.transform.position;
		this.heat.transform.position = tVec;
		if (this.active)
		{
			if (this.rayScaleY < 2000f)
			{
				this.rayScaleY += pElapsed * 7000f;
				if (this.rayScaleY >= 2000f)
				{
					this.rayScaleY = 2000f;
					this.touchedGround = true;
				}
				this.ray.transform.localScale = new Vector3(this.rayWidth, this.rayScaleY, 1f);
			}
		}
		else
		{
			this.touchedGround = false;
			if (this.rayScaleY > 0f)
			{
				this.rayScaleY -= pElapsed * 4000f;
				if (this.rayScaleY < 0f)
				{
					this.rayScaleY = 0f;
					base.gameObject.SetActive(false);
				}
				this.ray.transform.localScale = new Vector3(this.rayWidth, this.rayScaleY, 1f);
			}
		}
		this.heat.gameObject.SetActive(this.touchedGround);
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x00114AA4 File Offset: 0x00112CA4
	internal void play(Vector2 pPos, int pSize)
	{
		if (pSize >= 10)
		{
			this.rayWidth = 1f;
		}
		else
		{
			this.rayWidth = 0.4f;
		}
		base.transform.localPosition = new Vector3(pPos.x, pPos.y);
		this.active = true;
		this.ticksActive = 4;
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001767 RID: 5991
	public SpriteAnimation ray;

	// Token: 0x04001768 RID: 5992
	public SpriteAnimation heat;

	// Token: 0x04001769 RID: 5993
	private bool active;

	// Token: 0x0400176A RID: 5994
	private int ticksActive;

	// Token: 0x0400176B RID: 5995
	private bool touchedGround;

	// Token: 0x0400176C RID: 5996
	private float rayScaleY;

	// Token: 0x0400176D RID: 5997
	private float rayWidth = 1f;
}
