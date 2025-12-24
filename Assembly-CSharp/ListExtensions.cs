using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;

// Token: 0x0200046D RID: 1133
public static class ListExtensions
{
	// Token: 0x17000214 RID: 532
	// (get) Token: 0x060026A8 RID: 9896 RVA: 0x0013A980 File Offset: 0x00138B80
	private static Random rnd
	{
		get
		{
			return Randy.rnd;
		}
	}

	// Token: 0x060026A9 RID: 9897 RVA: 0x0013A987 File Offset: 0x00138B87
	public static string ToJson(this List<string> list)
	{
		if (list.Count == 0)
		{
			return "[]";
		}
		return "['" + string.Join("','", list) + "']";
	}

	// Token: 0x060026AA RID: 9898 RVA: 0x0013A9B4 File Offset: 0x00138BB4
	public static void ShuffleHalf<T>(this List<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int tCount = list.Count;
		int tHalfLength = tCount / 2 + 1;
		int i = 0;
		while (i < tHalfLength && i < tCount)
		{
			list.Swap(i, ListExtensions.rnd.Next(i, tCount));
			i += 2;
		}
	}

	// Token: 0x060026AB RID: 9899 RVA: 0x0013A9FC File Offset: 0x00138BFC
	public static void ShuffleN<T>(this List<T> list, int pItems)
	{
		if (list.Count < 2)
		{
			return;
		}
		int tCount = (list.Count < pItems) ? list.Count : pItems;
		for (int i = 0; i < tCount; i++)
		{
			list.Swap(i, ListExtensions.rnd.Next(i, tCount));
		}
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x0013AA48 File Offset: 0x00138C48
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Shuffle<T>(this List<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int tCount = list.Count;
		for (int i = 0; i < tCount; i++)
		{
			list.Swap(i, ListExtensions.rnd.Next(i, tCount));
		}
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x0013AA85 File Offset: 0x00138C85
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this List<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(0, ListExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x0013AAA9 File Offset: 0x00138CA9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this List<T> list, int nItem)
	{
		if (list.Count < 2)
		{
			return;
		}
		if (list.Count < nItem + 1)
		{
			return;
		}
		list.Swap(nItem, ListExtensions.rnd.Next(nItem, list.Count));
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x0013AAD9 File Offset: 0x00138CD9
	public static void ShuffleLast<T>(this List<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(list.Count - 1, ListExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x060026B0 RID: 9904 RVA: 0x0013AB04 File Offset: 0x00138D04
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Pop<T>(this List<T> list)
	{
		T result = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return result;
	}

	// Token: 0x060026B1 RID: 9905 RVA: 0x0013AB22 File Offset: 0x00138D22
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Shift<T>(this List<T> list)
	{
		T result = list[0];
		list.RemoveAt(0);
		return result;
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x0013AB32 File Offset: 0x00138D32
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T First<T>(this List<T> list)
	{
		return list[0];
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x0013AB3B File Offset: 0x00138D3B
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Last<T>(this List<T> list)
	{
		return list[list.Count - 1];
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x0013AB4C File Offset: 0x00138D4C
	public static void ShuffleRandomOne<T>(this List<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int i = Randy.randomInt(0, list.Count - 1);
		list.Swap(i, ListExtensions.rnd.Next(i, list.Count));
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x0013AB8C File Offset: 0x00138D8C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(this List<T> list, int i, int j)
	{
		T temp = list[i];
		list[i] = list[j];
		list[j] = temp;
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x0013ABB7 File Offset: 0x00138DB7
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandom<T>(this List<T> list)
	{
		return list[ListExtensions.rnd.Next(0, list.Count)];
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x0013ABD0 File Offset: 0x00138DD0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void RemoveAtSwapBack<T>(this List<T> list, T pObject)
	{
		int tIndex = list.IndexOf(pObject);
		if (tIndex == -1)
		{
			return;
		}
		int tCount = list.Count - 1;
		list[tIndex] = list[tCount];
		list[tCount] = pObject;
		list.RemoveAt(tCount);
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x0013AC10 File Offset: 0x00138E10
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Any<T>(this List<T> list)
	{
		return list != null && list.Count > 0;
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x0013AC20 File Offset: 0x00138E20
	[Pure]
	public static bool SetEquals<T>(this List<T> pList, IEnumerable<T> pOther)
	{
		if (pList == null || pOther == null)
		{
			return false;
		}
		HashSet<T> hashSet = CollectionPool<HashSet<T>, T>.Get();
		HashSet<T> tOther = CollectionPool<HashSet<T>, T>.Get();
		hashSet.UnionWith(pList);
		tOther.UnionWith(pOther);
		bool tEquals = hashSet.SetEquals(tOther);
		tOther.Clear();
		hashSet.Clear();
		CollectionPool<HashSet<T>, T>.Release(hashSet);
		CollectionPool<HashSet<T>, T>.Release(tOther);
		return tEquals;
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x0013AC6E File Offset: 0x00138E6E
	public static string ToLineString<T>(this List<T> pList, string pSeparator = ",")
	{
		if (pList == null)
		{
			return string.Empty;
		}
		return string.Join<T>(pSeparator, pList);
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x0013AC80 File Offset: 0x00138E80
	public static void PrintToConsole<T>(this List<T> pList)
	{
		if (pList == null)
		{
			return;
		}
		Debug.Log(pList.ToLineString(","));
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x0013AC98 File Offset: 0x00138E98
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void AddTimes<T>(this List<T> pList, int pAmount, T pObject)
	{
		for (int i = 0; i < pAmount; i++)
		{
			pList.Add(pObject);
		}
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x0013ACB8 File Offset: 0x00138EB8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T LoopNext<T>(this List<T> pList, T pObject)
	{
		int tIndex = pList.IndexOf(pObject);
		if (tIndex == -1)
		{
			return pObject;
		}
		tIndex++;
		if (tIndex >= pList.Count)
		{
			tIndex = 0;
		}
		return pList[tIndex];
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x0013ACEC File Offset: 0x00138EEC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Span<T> AsSpan<T>(this List<T> pList)
	{
		ListAccessHelper.ListDataHelper<T> listDataHelper = *UnsafeUtility.As<List<T>, ListAccessHelper.ListDataHelper<T>>(ref pList);
		int tSize = listDataHelper._size;
		T[] tItems = listDataHelper._items;
		if (tSize > tItems.Length)
		{
			throw new InvalidOperationException("Concurrent operations are not supported.");
		}
		return new Span<T>(tItems, 0, tSize);
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x0013AD28 File Offset: 0x00138F28
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> pList)
	{
		ListAccessHelper.ListDataHelper<T> listDataHelper = *UnsafeUtility.As<List<T>, ListAccessHelper.ListDataHelper<T>>(ref pList);
		int tSize = listDataHelper._size;
		T[] tItems = listDataHelper._items;
		if (tSize > tItems.Length)
		{
			throw new InvalidOperationException("Concurrent operations are not supported.");
		}
		return new ReadOnlySpan<T>(tItems, 0, tSize);
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x0013AD63 File Offset: 0x00138F63
	public static string AsString<T>(this List<T> pList)
	{
		return pList.ToArray().AsString<T>();
	}
}
