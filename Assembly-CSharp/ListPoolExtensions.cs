using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Pool;

// Token: 0x02000574 RID: 1396
public static class ListPoolExtensions
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x001651F8 File Offset: 0x001633F8
	private static Random rnd
	{
		get
		{
			return Randy.rnd;
		}
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x001651FF File Offset: 0x001633FF
	public static string ToJson(this global::ListPool<string> list)
	{
		if (list.Count == 0)
		{
			return "[]";
		}
		return "['" + string.Join("','", list) + "']";
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x0016522C File Offset: 0x0016342C
	public static void ShuffleHalf<T>(this global::ListPool<T> list)
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
			list.Swap(i, ListPoolExtensions.rnd.Next(i, tCount));
			i += 2;
		}
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x00165274 File Offset: 0x00163474
	public static void ShuffleN<T>(this global::ListPool<T> list, int pItems)
	{
		if (list.Count < 2)
		{
			return;
		}
		int tCount = (list.Count < pItems) ? list.Count : pItems;
		for (int i = 0; i < tCount; i++)
		{
			list.Swap(i, ListPoolExtensions.rnd.Next(i, tCount));
		}
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x001652C0 File Offset: 0x001634C0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Shuffle<T>(this global::ListPool<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int tCount = list.Count;
		for (int i = 0; i < tCount; i++)
		{
			list.Swap(i, ListPoolExtensions.rnd.Next(i, tCount));
		}
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x001652FD File Offset: 0x001634FD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this global::ListPool<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(0, ListPoolExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x00165321 File Offset: 0x00163521
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this global::ListPool<T> list, int nItem)
	{
		if (list.Count < 2)
		{
			return;
		}
		if (list.Count < nItem + 1)
		{
			return;
		}
		list.Swap(nItem, ListPoolExtensions.rnd.Next(nItem, list.Count));
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x00165351 File Offset: 0x00163551
	public static void ShuffleLast<T>(this global::ListPool<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(list.Count - 1, ListPoolExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x0016537C File Offset: 0x0016357C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Pop<T>(this global::ListPool<T> list)
	{
		T result = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return result;
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x0016539A File Offset: 0x0016359A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Shift<T>(this global::ListPool<T> list)
	{
		T result = list[0];
		list.RemoveAt(0);
		return result;
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x001653AA File Offset: 0x001635AA
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T First<T>(this global::ListPool<T> list)
	{
		return list[0];
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x001653B3 File Offset: 0x001635B3
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Last<T>(this global::ListPool<T> list)
	{
		return list[list.Count - 1];
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x001653C4 File Offset: 0x001635C4
	public static void ShuffleRandomOne<T>(this global::ListPool<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int i = Randy.randomInt(0, list.Count - 1);
		list.Swap(i, ListPoolExtensions.rnd.Next(i, list.Count));
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x00165404 File Offset: 0x00163604
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(this global::ListPool<T> list, int i, int j)
	{
		T[] _items = list.GetRawBuffer();
		T tTemp = _items[i];
		_items[i] = _items[j];
		_items[j] = tTemp;
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x00165436 File Offset: 0x00163636
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandom<T>(this global::ListPool<T> list)
	{
		return list[ListPoolExtensions.rnd.Next(0, list.Count)];
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x00165450 File Offset: 0x00163650
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void RemoveAtSwapBack<T>(this global::ListPool<T> list, T pObject)
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

	// Token: 0x06002DC6 RID: 11718 RVA: 0x00165490 File Offset: 0x00163690
	[Pure]
	public static T[] ToArray<T>(this global::ListPool<T> list)
	{
		T[] tArray = new T[list.Count];
		list.CopyTo(tArray, 0);
		return tArray;
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x001654B2 File Offset: 0x001636B2
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Any<T>(this global::ListPool<T> list)
	{
		return list != null && list.Count > 0;
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x001654C4 File Offset: 0x001636C4
	[Pure]
	public static bool SetEquals<T>(this global::ListPool<T> pList, IEnumerable<T> pOther)
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

	// Token: 0x06002DC9 RID: 11721 RVA: 0x00165514 File Offset: 0x00163714
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void AddTimes<T>(this global::ListPool<T> pList, int pAmount, T pObject)
	{
		for (int i = 0; i < pAmount; i++)
		{
			pList.Add(pObject);
		}
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x00165534 File Offset: 0x00163734
	public static int CountAll<T>(this global::ListPool<T> pList, Predicate<T> pMatch)
	{
		int tCount = 0;
		for (int i = 0; i < pList.Count; i++)
		{
			if (pMatch(pList[i]))
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x06002DCB RID: 11723 RVA: 0x00165568 File Offset: 0x00163768
	public static IEnumerable<T> Where<T>(this global::ListPool<T> pList, Func<T, bool> pPredicate)
	{
		int num;
		for (int i = 0; i < pList.Count; i = num + 1)
		{
			if (pPredicate(pList[i]))
			{
				yield return pList[i];
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x00165580 File Offset: 0x00163780
	[Pure]
	public static bool ValuesEqual<T>(this global::ListPool<T> pList, global::ListPool<T> pOther)
	{
		if (pList.Count != pOther.Count)
		{
			return false;
		}
		long longHashCode = pList.GetLongHashCode<T>();
		long tOtherHash = pOther.GetLongHashCode<T>();
		return longHashCode == tOtherHash;
	}

	// Token: 0x06002DCD RID: 11725 RVA: 0x001655B0 File Offset: 0x001637B0
	[Pure]
	public static long GetLongHashCode<T>(this global::ListPool<T> pList)
	{
		long tHash = 0L;
		foreach (T ptr in pList)
		{
			T tItem = ptr;
			tHash += (long)tItem.GetHashCode();
		}
		return tHash;
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x00165614 File Offset: 0x00163814
	public static string AsString<T>(this global::ListPool<T> pListPool)
	{
		return pListPool.ToArray<T>().AsString<T>();
	}
}
