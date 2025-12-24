using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class SoundController : MonoBehaviour
{
	// Token: 0x060025D1 RID: 9681 RVA: 0x001372E8 File Offset: 0x001354E8
	private void Awake()
	{
		this.s = base.GetComponent<AudioSource>();
		this._camera = Camera.main.GetComponent<MoveCamera>();
		this.originVolume = this.s.volume;
		if (this.ambientSound)
		{
			this.s.spatialBlend = 1f;
			this.s.dopplerLevel = 0f;
			this.s.rolloffMode = AudioRolloffMode.Logarithmic;
		}
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x00137358 File Offset: 0x00135558
	internal void play(float pX = 0f, float pY = 0f)
	{
		float tVolumeMod = 1f;
		if (this.ambientSound)
		{
			this.sfxPos = new Vector3(pX, pY, 0f);
			this.sfxPosCamera = this._camera.main_camera.WorldToViewportPoint(this.sfxPos);
			if (this.sfxPosCamera.x > 0f && this.sfxPosCamera.x < 1f && this.sfxPosCamera.y > 0f)
			{
				float y = this.sfxPosCamera.y;
			}
			if (pX != 0f && pY != 0f)
			{
				tVolumeMod = 1f - this._camera.main_camera.orthographicSize / this._camera.orthographic_size_max * 0.7f;
				tVolumeMod = Mathf.Clamp01(tVolumeMod);
			}
		}
		if (this.clips != null && this.clips.Count > 0)
		{
			this.s.clip = Randy.getRandom<AudioClip>(this.clips);
		}
		base.gameObject.SetActive(true);
		this.s.volume = this.originVolume * tVolumeMod;
		this.s.pitch = this.originPitch + Randy.randomFloat(-this.randomizePitch, this.randomizePitch);
		this.s.transform.position = new Vector3(pX, pY);
		this.s.Play();
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x001374C0 File Offset: 0x001356C0
	internal void update(float pElapsed)
	{
		if (this.timeout > 0f)
		{
			this.timeout -= pElapsed;
		}
		if (this.ambientSound)
		{
			this.sfxPosCamera = this._camera.main_camera.WorldToViewportPoint(this.sfxPos);
			if (this.sfxPosCamera.x <= 0f || this.sfxPosCamera.x >= 1f || this.sfxPosCamera.y <= 0f || this.sfxPosCamera.y >= 1f)
			{
			}
			float tVolumeMod = 1f - this._camera.main_camera.orthographicSize / this._camera.orthographic_size_max * 0.7f;
			tVolumeMod = Mathf.Clamp01(tVolumeMod);
			this.s.volume = this.originVolume * tVolumeMod;
		}
	}

	// Token: 0x04001CB9 RID: 7353
	public List<AudioClip> clips;

	// Token: 0x04001CBA RID: 7354
	public bool soundEnabled = true;

	// Token: 0x04001CBB RID: 7355
	public bool ambientSound;

	// Token: 0x04001CBC RID: 7356
	internal int curCopies;

	// Token: 0x04001CBD RID: 7357
	public int copies;

	// Token: 0x04001CBE RID: 7358
	public float randomizePitch;

	// Token: 0x04001CBF RID: 7359
	internal float timeout;

	// Token: 0x04001CC0 RID: 7360
	public float timeoutInterval;

	// Token: 0x04001CC1 RID: 7361
	public float originPitch = 1f;

	// Token: 0x04001CC2 RID: 7362
	internal AudioSource s;

	// Token: 0x04001CC3 RID: 7363
	private MoveCamera _camera;

	// Token: 0x04001CC4 RID: 7364
	private float originVolume = 1f;

	// Token: 0x04001CC5 RID: 7365
	private Vector3 sfxPos;

	// Token: 0x04001CC6 RID: 7366
	private Vector3 sfxPosCamera;
}
