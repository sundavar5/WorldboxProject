using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class WorldAgesParticles : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x00003524 File Offset: 0x00001724
	private void Awake()
	{
		this.setSystem("Rain", out this._system_rain, out this._mat_rain);
		this.setSystem("Snow", out this._system_snow, out this._mat_snow);
		this.setSystem("Magic", out this._system_magic, out this._mat_magic);
		this.setSystem("Ash", out this._system_ash, out this._mat_ash);
		this.setSystem("Sun Blobs", out this._system_sun_blobs, out this._mat_sun_blobs);
		this.setSystem("Sun Rays", out this._system_sun_rays, out this._mat_sun_ray);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000035BC File Offset: 0x000017BC
	private void setSystem(string pID, out ParticleSystem pSystem, out Material pMat)
	{
		pSystem = base.transform.Find(pID).GetComponent<ParticleSystem>();
		pMat = pSystem.GetComponent<Renderer>().material;
		pSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
		Color tColor = pMat.color;
		tColor.a = 0f;
		pMat.color = tColor;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00003610 File Offset: 0x00001810
	private void Update()
	{
		if (World.world == null)
		{
			return;
		}
		if (World.world_era == null)
		{
			return;
		}
		this._camera = World.world.camera;
		this.updateParticles(this._system_rain, this._mat_rain, World.world_era.particles_rain);
		this.updateParticles(this._system_snow, this._mat_snow, World.world_era.particles_snow);
		this.updateParticles(this._system_magic, this._mat_magic, World.world_era.particles_magic);
		this.updateParticles(this._system_ash, this._mat_ash, World.world_era.particles_ash);
		this.updateParticles(this._system_sun_blobs, this._mat_sun_blobs, World.world_era.particles_sun);
		this.updateParticles(this._system_sun_rays, this._mat_sun_ray, World.world_era.particles_sun);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000036EC File Offset: 0x000018EC
	private void updateParticles(ParticleSystem pSystem, Material pMaterial, bool pEnabled)
	{
		if (!WorldAgesParticles.effects_enabled)
		{
			pEnabled = false;
		}
		Color tColor = pMaterial.color;
		bool tPlay = MapBox.isRenderGameplay() && pEnabled;
		if (tColor.a != 0f && !tPlay && !pSystem.isPlaying)
		{
			return;
		}
		int tWidth = MapBox.width;
		int tHeight = MapBox.height;
		Vector3 tPos = new Vector3((float)(tWidth / 2), (float)(tHeight / 2));
		pSystem.transform.localPosition = tPos;
		pSystem.shape.scale = new Vector3((float)tWidth * 1.5f, (float)tHeight * 1.5f, 1f);
		if (!tPlay)
		{
			if (tColor.a > 0f)
			{
				tColor.a -= World.world.delta_time * 0.1f;
			}
		}
		else if (tColor.a < 1f)
		{
			tColor.a += World.world.delta_time * 0.1f;
			if (tColor.a > 1f)
			{
				tColor.a = 1f;
			}
		}
		if (tColor.a <= 0f)
		{
			tColor.a = 0f;
			pSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
		else if (!pSystem.isPlaying)
		{
			pSystem.Play();
		}
		pMaterial.color = tColor;
	}

	// Token: 0x04000018 RID: 24
	public static bool effects_enabled = true;

	// Token: 0x04000019 RID: 25
	private ParticleSystem _system_rain;

	// Token: 0x0400001A RID: 26
	private Material _mat_rain;

	// Token: 0x0400001B RID: 27
	private ParticleSystem _system_snow;

	// Token: 0x0400001C RID: 28
	private Material _mat_snow;

	// Token: 0x0400001D RID: 29
	private ParticleSystem _system_magic;

	// Token: 0x0400001E RID: 30
	private Material _mat_magic;

	// Token: 0x0400001F RID: 31
	private ParticleSystem _system_ash;

	// Token: 0x04000020 RID: 32
	private Material _mat_ash;

	// Token: 0x04000021 RID: 33
	private ParticleSystem _system_sun_blobs;

	// Token: 0x04000022 RID: 34
	private Material _mat_sun_blobs;

	// Token: 0x04000023 RID: 35
	private ParticleSystem _system_sun_rays;

	// Token: 0x04000024 RID: 36
	private Material _mat_sun_ray;

	// Token: 0x04000025 RID: 37
	private Camera _camera;
}
