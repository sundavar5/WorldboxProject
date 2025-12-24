using System;

// Token: 0x02000338 RID: 824
public class EffectDivineLight : BaseAnimatedObject
{
	// Token: 0x06001FEA RID: 8170 RVA: 0x00112C51 File Offset: 0x00110E51
	public override void Awake()
	{
		base.Awake();
		this.setState(DivineLightState.SpawnFirstStage);
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x00112C60 File Offset: 0x00110E60
	private void setState(DivineLightState pState)
	{
		this.state = pState;
		switch (this.state)
		{
		case DivineLightState.SpawnFirstStage:
			this.raySpawn.gameObject.SetActive(true);
			this.rayIdle.gameObject.SetActive(false);
			this.baseSpawn.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(false);
			return;
		case DivineLightState.SpawnSecondStage:
			this.raySpawn.gameObject.SetActive(false);
			this.rayIdle.gameObject.SetActive(true);
			this.baseSpawn.gameObject.SetActive(true);
			this.baseIdle.gameObject.SetActive(false);
			return;
		case DivineLightState.Idle:
			this.raySpawn.gameObject.SetActive(false);
			this.rayIdle.gameObject.SetActive(true);
			this.baseSpawn.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(true);
			return;
		case DivineLightState.Hide:
			this.raySpawn.gameObject.SetActive(true);
			this.rayIdle.gameObject.SetActive(false);
			this.baseSpawn.gameObject.SetActive(true);
			this.baseIdle.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x00112DA5 File Offset: 0x00110FA5
	private void stopEffet()
	{
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x00112DA7 File Offset: 0x00110FA7
	private void useEffect()
	{
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x00112DAC File Offset: 0x00110FAC
	private void Update()
	{
		if (this.isOn)
		{
			this.raySpawn.playType = AnimPlayType.Forward;
			this.baseSpawn.playType = AnimPlayType.Forward;
			if (this.raySpawn.isLastFrame())
			{
				this.raySpawn.gameObject.SetActive(false);
				this.rayIdle.gameObject.SetActive(true);
			}
			else
			{
				this.raySpawn.gameObject.SetActive(true);
				this.rayIdle.gameObject.SetActive(false);
			}
			if (this.baseSpawn.isLastFrame())
			{
				this.baseSpawn.gameObject.SetActive(false);
				this.baseIdle.gameObject.SetActive(true);
			}
			else
			{
				this.baseSpawn.gameObject.SetActive(true);
				this.baseIdle.gameObject.SetActive(false);
			}
		}
		else
		{
			this.raySpawn.playType = AnimPlayType.Backward;
			this.baseSpawn.playType = AnimPlayType.Backward;
			this.rayIdle.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(false);
			if (this.raySpawn.isFirstFrame())
			{
				this.raySpawn.gameObject.SetActive(false);
			}
			else
			{
				this.raySpawn.gameObject.SetActive(true);
			}
			if (this.baseSpawn.isFirstFrame())
			{
				this.baseSpawn.gameObject.SetActive(false);
			}
			else
			{
				this.baseSpawn.gameObject.SetActive(true);
			}
		}
		if (this.baseSpawn.gameObject.activeSelf)
		{
			this.baseSpawn.update(World.world.delta_time);
		}
		if (this.baseIdle.gameObject.activeSelf)
		{
			this.baseIdle.update(World.world.delta_time);
		}
		if (this.raySpawn.gameObject.activeSelf)
		{
			this.raySpawn.update(World.world.delta_time);
		}
		if (this.rayIdle.gameObject.activeSelf)
		{
			this.rayIdle.update(World.world.delta_time);
		}
		this.isOn = false;
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x00112FC9 File Offset: 0x001111C9
	public void playOn(WorldTile pTile)
	{
		base.gameObject.transform.localPosition = pTile.posV3;
		this.isOn = true;
	}

	// Token: 0x04001744 RID: 5956
	public SpriteAnimation raySpawn;

	// Token: 0x04001745 RID: 5957
	public SpriteAnimation rayIdle;

	// Token: 0x04001746 RID: 5958
	public SpriteAnimation baseSpawn;

	// Token: 0x04001747 RID: 5959
	public SpriteAnimation baseIdle;

	// Token: 0x04001748 RID: 5960
	public bool isOn;

	// Token: 0x04001749 RID: 5961
	private DivineLightState state;
}
