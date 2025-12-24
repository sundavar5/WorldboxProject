using System;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class GlowParticles : MonoBehaviour
{
	// Token: 0x06002014 RID: 8212 RVA: 0x00113F59 File Offset: 0x00112159
	private void Awake()
	{
		this.particles = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x00113F67 File Offset: 0x00112167
	private void Update()
	{
		if (this.cooldown > 0f)
		{
			this.cooldown -= Time.deltaTime;
		}
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x00113F88 File Offset: 0x00112188
	public void spawn(float pX, float pY, bool pRemoveCooldown = false)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.particles.particleCount > 50)
		{
			return;
		}
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		if (pRemoveCooldown)
		{
			this.cooldown = 0f;
		}
		if (this.cooldown > 0f)
		{
			return;
		}
		this.cooldown = 0.2f + Randy.randomFloat(0f, 0.3f);
		ParticleSystem.EmitParams tParam = default(ParticleSystem.EmitParams);
		tParam.position = new Vector3(pX, pY);
		this.particles.Emit(tParam, 1);
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x00114010 File Offset: 0x00112210
	public void spawn(Vector3 pPos)
	{
		this.spawn(pPos.x, pPos.y, false);
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x00114025 File Offset: 0x00112225
	public void clear()
	{
		this.particles.Clear();
	}

	// Token: 0x04001757 RID: 5975
	private float cooldown;

	// Token: 0x04001758 RID: 5976
	public ParticleSystem particles;
}
