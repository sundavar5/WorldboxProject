using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class MagnetThrow
{
	// Token: 0x06000DCF RID: 3535 RVA: 0x000BE70A File Offset: 0x000BC90A
	public void initializeMouseTracking()
	{
		this._last_mouse_position = World.world.getMousePos();
		this._velocity_samples.Clear();
		this._throw_momentum = Vector2.zero;
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x000BE734 File Offset: 0x000BC934
	public void trackMouseMovement(int pMagnetState)
	{
		if (pMagnetState != 1)
		{
			return;
		}
		Vector2 tCurrentMousePos = World.world.getMousePos();
		Vector2 tMouseDelta = tCurrentMousePos - this._last_mouse_position;
		this._mouse_velocity = tMouseDelta * 60f;
		this._velocity_samples.Add(this._mouse_velocity);
		if (this._velocity_samples.Count > 5)
		{
			this._velocity_samples.RemoveAt(0);
		}
		Vector2 tTargetMomentum = tMouseDelta * 0.7f;
		this._throw_momentum = Vector2.Lerp(this._throw_momentum, tTargetMomentum, Time.deltaTime * 10f);
		this._last_mouse_position = tCurrentMousePos;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x000BE7CC File Offset: 0x000BC9CC
	public Vector2 calculateThrowForce()
	{
		Vector2 tAverageVelocity = Vector2.zero;
		if (this._velocity_samples.Count > 0)
		{
			foreach (Vector2 sample in this._velocity_samples)
			{
				tAverageVelocity += sample;
			}
			tAverageVelocity /= (float)this._velocity_samples.Count;
		}
		Vector2 tThrowForce = tAverageVelocity * 5f * Time.deltaTime;
		float tForceMagnitude = tThrowForce.magnitude;
		if (tForceMagnitude > 10f)
		{
			tThrowForce = tThrowForce.normalized * 10f;
		}
		else if (tForceMagnitude < 0.1f && tForceMagnitude > 0.1f)
		{
			tThrowForce = tThrowForce.normalized * 0.1f;
		}
		return tThrowForce;
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x000BE8A8 File Offset: 0x000BCAA8
	public void clear()
	{
		this._velocity_samples.Clear();
		this._throw_momentum = Vector2.zero;
	}

	// Token: 0x04000E2B RID: 3627
	private Vector2 _mouse_velocity = Vector2.zero;

	// Token: 0x04000E2C RID: 3628
	private Vector2 _last_mouse_position;

	// Token: 0x04000E2D RID: 3629
	private readonly List<Vector2> _velocity_samples = new List<Vector2>();

	// Token: 0x04000E2E RID: 3630
	private const int MAX_VELOCITY_SAMPLES = 5;

	// Token: 0x04000E2F RID: 3631
	private const float THROW_FORCE_MULTIPLIER = 5f;

	// Token: 0x04000E30 RID: 3632
	public const float MIN_THROW_FORCE = 0.1f;

	// Token: 0x04000E31 RID: 3633
	private const float MAX_THROW_FORCE = 10f;

	// Token: 0x04000E32 RID: 3634
	private Vector2 _throw_momentum = Vector2.zero;

	// Token: 0x04000E33 RID: 3635
	private const float MOMENTUM_DECAY = 0.85f;

	// Token: 0x04000E34 RID: 3636
	private const float MOMENTUM_BUILD_RATE = 0.7f;
}
