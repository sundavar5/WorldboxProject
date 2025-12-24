using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

// Token: 0x0200057C RID: 1404
public static class Randy
{
	// Token: 0x06002E6C RID: 11884 RVA: 0x00166697 File Offset: 0x00164897
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void OnBeforeSplashScreen()
	{
		Randy.fullReset();
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x0016669E File Offset: 0x0016489E
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void OnBeforeSceneLoad()
	{
		Randy.fullReset();
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x001666A5 File Offset: 0x001648A5
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void OnAfterSceneLoad()
	{
		Randy.fullReset();
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x001666AC File Offset: 0x001648AC
	internal static void fullReset()
	{
		int year = DateTime.Now.Year;
		int tMonth = DateTime.Now.Month;
		int tDay = DateTime.Now.Day;
		int tHour = DateTime.Now.Hour;
		Randy._seed = (long)(year * 100000 + tMonth * 1000 + tDay * 10 + tHour / 3);
		Randy.nextSeed();
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x00166712 File Offset: 0x00164912
	internal static void nextSeed()
	{
		Randy._seed += 543L;
		Randy.resetSeed(Randy._seed);
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x00166730 File Offset: 0x00164930
	public static void resetSeed(long pLongValue)
	{
		if (pLongValue == 0L)
		{
			pLongValue = 1L;
		}
		int seed = (int)pLongValue;
		uint tUintValue = (uint)pLongValue;
		UnityEngine.Random.InitState(seed);
		Randy.rnd = new System.Random(seed);
		Randy.rand = new Unity.Mathematics.Random(tUintValue);
		Randy.rand.NextBool();
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x00166770 File Offset: 0x00164970
	public static void resetSeed(int pIntValue)
	{
		if (pIntValue == 0)
		{
			pIntValue = 1;
		}
		UnityEngine.Random.InitState(pIntValue);
		uint tUintValue = (uint)pIntValue;
		Randy.rnd = new System.Random(pIntValue);
		Randy.rand = new Unity.Mathematics.Random(tUintValue);
		Randy.rand.NextBool();
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x001667AC File Offset: 0x001649AC
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int randomInt(int pMinInclusive, int pMaxExclusive)
	{
		return Randy.rand.NextInt(pMinInclusive, pMaxExclusive);
	}

	// Token: 0x06002E74 RID: 11892 RVA: 0x001667BA File Offset: 0x001649BA
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool randomBool()
	{
		return Randy.rand.NextBool();
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x001667C8 File Offset: 0x001649C8
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool randomChance(float pVal)
	{
		float tRandomVal = Randy.random();
		return pVal >= tRandomVal;
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x001667E2 File Offset: 0x001649E2
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float random()
	{
		return Randy.rand.NextFloat();
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x001667EE File Offset: 0x001649EE
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float randomFloat(float pMinInclusive, float pMaxExclusive)
	{
		return Randy.rand.NextFloat(pMinInclusive, pMaxExclusive);
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x001667FC File Offset: 0x001649FC
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 randomPointOnCircle(float pRadiusMinInclusive, float pRadiusMaxExclusive)
	{
		return Randy.getRandomPointInUnitCircle().normalized * Randy.randomFloat(pRadiusMinInclusive, pRadiusMaxExclusive);
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x00166824 File Offset: 0x00164A24
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 getRandomPointInUnitCircle()
	{
		float radius = Mathf.Sqrt(Randy.random());
		float angle = Randy.random() * 2f * 3.1415927f;
		return new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x00166863 File Offset: 0x00164A63
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T getRandom<T>(T[] pArray)
	{
		return pArray.GetRandom<T>();
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x0016686C File Offset: 0x00164A6C
	[Pure]
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T getRandom<T>(List<T> pList)
	{
		if (pList.Count == 0)
		{
			return default(T);
		}
		return pList.GetRandom<T>();
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x00166894 File Offset: 0x00164A94
	[Pure]
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T getRandom<T>(ListPool<T> pList)
	{
		if (pList.Count == 0)
		{
			return default(T);
		}
		return pList.GetRandom<T>();
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x001668BC File Offset: 0x00164ABC
	[Pure]
	public static T RandomEnumValue<T>() where T : Enum
	{
		Array tValues = Enum.GetValues(typeof(T));
		return (T)((object)tValues.GetValue(Randy.randomInt(0, tValues.Length)));
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x001668F0 File Offset: 0x00164AF0
	[Pure]
	public static Color getRandomColor()
	{
		return new Color(Randy.random(), Randy.random(), Randy.random(), 1f);
	}

	// Token: 0x06002E7F RID: 11903 RVA: 0x0016690B File Offset: 0x00164B0B
	[Pure]
	public static Color ColorHSV()
	{
		return Randy.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x0016693C File Offset: 0x00164B3C
	[Pure]
	public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
	{
		float h = Mathf.Lerp(hueMin, hueMax, Randy.debug_rand.NextFloat());
		float s = Mathf.Lerp(saturationMin, saturationMax, Randy.debug_rand.NextFloat());
		float v = Mathf.Lerp(valueMin, valueMax, Randy.debug_rand.NextFloat());
		Color result = Color.HSVToRGB(h, s, v, true);
		result.a = Mathf.Lerp(alphaMin, alphaMax, Randy.debug_rand.NextFloat());
		return result;
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x001669A4 File Offset: 0x00164BA4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this List<T> list)
	{
		return new RandomListEnumerator<T>(list);
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x001669B1 File Offset: 0x00164BB1
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this List<T> list, int pMax)
	{
		return new RandomListEnumerator<T>(list, list.Count, pMax);
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x001669C5 File Offset: 0x00164BC5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this T[] array)
	{
		return new RandomArrayEnumerator<T>(array);
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x001669D2 File Offset: 0x00164BD2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this T[] array, int pLength)
	{
		return new RandomArrayEnumerator<T>(array, pLength);
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x001669E0 File Offset: 0x00164BE0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this T[] array, int pLength, int pMax)
	{
		return new RandomArrayEnumerator<T>(array, pLength, pMax);
	}

	// Token: 0x06002E86 RID: 11910 RVA: 0x001669EF File Offset: 0x00164BEF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this ListPool<T> list)
	{
		return new RandomArrayEnumerator<T>(list.GetRawBuffer(), list.Count);
	}

	// Token: 0x06002E87 RID: 11911 RVA: 0x00166A07 File Offset: 0x00164C07
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this ListPool<T> list, int pMax)
	{
		return new RandomArrayEnumerator<T>(list.GetRawBuffer(), list.Count, pMax);
	}

	// Token: 0x06002E88 RID: 11912 RVA: 0x00166A20 File Offset: 0x00164C20
	public static IEnumerable<T> LoopRandom<T>(this IEnumerable<T> pEnumerable)
	{
		ListPool<T> tPool = null;
		List<T> tList = pEnumerable as List<T>;
		IEnumerable<T> tEnumerable;
		if (tList == null)
		{
			T[] tArray = pEnumerable as T[];
			if (tArray == null)
			{
				ListPool<T> tListPool = pEnumerable as ListPool<T>;
				if (tListPool == null)
				{
					tPool = new ListPool<T>(pEnumerable);
					tEnumerable = tPool.LoopRandom<T>();
				}
				else
				{
					tEnumerable = tListPool.LoopRandom<T>();
				}
			}
			else
			{
				tEnumerable = tArray.LoopRandom(tArray.Length);
			}
		}
		else
		{
			tEnumerable = tList.LoopRandom<T>();
		}
		try
		{
			foreach (T tItem in tEnumerable)
			{
				yield return tItem;
			}
			IEnumerator<T> enumerator = null;
		}
		finally
		{
			ListPool<T> listPool = tPool;
			if (listPool != null)
			{
				listPool.Dispose();
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x06002E89 RID: 11913 RVA: 0x00166A30 File Offset: 0x00164C30
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> LoopRandom<T>(this IEnumerable<T> pEnumerable, int pMax)
	{
		ListPool<T> tPool = null;
		List<T> tList = pEnumerable as List<T>;
		IEnumerable<T> tEnumerable;
		if (tList == null)
		{
			T[] tArray = pEnumerable as T[];
			if (tArray == null)
			{
				ListPool<T> tListPool = pEnumerable as ListPool<T>;
				if (tListPool == null)
				{
					tPool = new ListPool<T>(pEnumerable);
					tEnumerable = tPool.LoopRandom(pMax);
				}
				else
				{
					tEnumerable = tListPool.LoopRandom(pMax);
				}
			}
			else
			{
				tEnumerable = tArray.LoopRandom(tArray.Length, pMax);
			}
		}
		else
		{
			tEnumerable = tList.LoopRandom(pMax);
		}
		try
		{
			foreach (T tItem in tEnumerable)
			{
				yield return tItem;
			}
			IEnumerator<T> enumerator = null;
		}
		finally
		{
			ListPool<T> listPool = tPool;
			if (listPool != null)
			{
				listPool.Dispose();
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x040022D1 RID: 8913
	internal static System.Random rnd = new System.Random(123);

	// Token: 0x040022D2 RID: 8914
	internal static Unity.Mathematics.Random rand = new Unity.Mathematics.Random(123U);

	// Token: 0x040022D3 RID: 8915
	internal static Unity.Mathematics.Random debug_rand = new Unity.Mathematics.Random(123U);

	// Token: 0x040022D4 RID: 8916
	private static long _seed = 1L;
}
