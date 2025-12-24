using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public static class AnimationHelper
{
	// Token: 0x060025E3 RID: 9699 RVA: 0x001378DC File Offset: 0x00135ADC
	public static float getTime()
	{
		return AnimationHelper._time_simulation;
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x001378E3 File Offset: 0x00135AE3
	public static void updateTime(float pElapsedScaled, float pElapsedSession)
	{
		AnimationHelper.updateTimeSimulation(pElapsedScaled);
		AnimationHelper.updateTimeSession(pElapsedSession);
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x001378F1 File Offset: 0x00135AF1
	private static void updateTimeSimulation(float pElapsed)
	{
		if (World.world.isPaused())
		{
			return;
		}
		AnimationHelper._time_simulation += pElapsed;
		if (AnimationHelper._time_simulation >= AnimationHelper.animationTimeMax)
		{
			AnimationHelper._time_simulation -= AnimationHelper.animationTimeMax;
		}
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x00137928 File Offset: 0x00135B28
	private static void updateTimeSession(float pElapsed)
	{
		AnimationHelper._time_session += pElapsed;
		if (AnimationHelper._time_session >= AnimationHelper.animationTimeMax)
		{
			AnimationHelper._time_session -= AnimationHelper.animationTimeMax;
		}
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x00137952 File Offset: 0x00135B52
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float getAnimationGlobalTime(float pAnimationSpeed)
	{
		return AnimationHelper._time_simulation * pAnimationSpeed;
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x0013795B File Offset: 0x00135B5B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sprite getSpriteFromListSessionTime(int pHashCodeOffset, IList<Sprite> pFrames, float pAnimationSpeed)
	{
		return AnimationHelper.getSpriteFromList(AnimationHelper._time_session * pAnimationSpeed, pHashCodeOffset, pFrames);
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x0013796B File Offset: 0x00135B6B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sprite getSpriteFromList(int pHashCodeOffset, IList<Sprite> pFrames, float pAnimationSpeed)
	{
		return AnimationHelper.getSpriteFromList(AnimationHelper.getAnimationGlobalTime(pAnimationSpeed), pHashCodeOffset, pFrames);
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x0013797C File Offset: 0x00135B7C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sprite getSpriteFromList(float pTime, int pHashCodeOffset, IList<Sprite> pFrames)
	{
		int tFrameIndex = AnimationHelper.getSpriteIndex(pTime, pHashCodeOffset, pFrames.Count);
		return pFrames[tFrameIndex];
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x0013799E File Offset: 0x00135B9E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getSpriteIndex(float pTime, int pHashCodeOffset, int pFrameCount)
	{
		if (pHashCodeOffset < 0)
		{
			pHashCodeOffset = -pHashCodeOffset;
		}
		return (int)(Mathf.Abs(pTime + (float)(pHashCodeOffset * 100)) % (float)pFrameCount);
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x001379B8 File Offset: 0x00135BB8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int getSpriteIndex(long pHashCodeOffset, int pFrameCount)
	{
		if (pHashCodeOffset < 0L)
		{
			pHashCodeOffset = -pHashCodeOffset;
		}
		return (int)(Mathf.Abs((float)(1L + pHashCodeOffset * 100L)) % (float)pFrameCount);
	}

	// Token: 0x04001CC8 RID: 7368
	private static float animationTimeMax = 100f;

	// Token: 0x04001CC9 RID: 7369
	private static float _time_simulation;

	// Token: 0x04001CCA RID: 7370
	private static float _time_session;
}
