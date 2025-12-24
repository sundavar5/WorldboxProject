using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// Token: 0x0200045C RID: 1116
public static class HashSetExtensions
{
	// Token: 0x06002649 RID: 9801 RVA: 0x001390F4 File Offset: 0x001372F4
	[CanBeNull]
	public static T GetRandom<T>(this HashSet<T> pHashSet)
	{
		int tRandomIndex = Randy.randomInt(0, pHashSet.Count);
		int tIndex = 0;
		foreach (T item in pHashSet)
		{
			if (tIndex++ == tRandomIndex)
			{
				return item;
			}
		}
		return default(T);
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x00139164 File Offset: 0x00137364
	public static T[] ToArray<T>(this HashSet<T> pHashSet)
	{
		T[] tArray = new T[pHashSet.Count];
		pHashSet.CopyTo(tArray);
		return tArray;
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x00139185 File Offset: 0x00137385
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Any<T>(this HashSet<T> pHashSet)
	{
		return pHashSet != null && pHashSet.Count > 0;
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x00139198 File Offset: 0x00137398
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RemoveAll<T>(this HashSet<T> pHashSet, ICollection<T> pToRemove)
	{
		if (pToRemove == null)
		{
			throw new ArgumentNullException("pToRemove");
		}
		if (pToRemove.Count == 0 || pHashSet.Count == 0)
		{
			return false;
		}
		int count = pHashSet.Count;
		pHashSet.ExceptWith(pToRemove);
		int tNewCount = pHashSet.Count;
		return count != tNewCount;
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x001391E0 File Offset: 0x001373E0
	public static T Pop<T>(this HashSet<T> pHashSet)
	{
		if (pHashSet == null)
		{
			throw new ArgumentNullException("pHashSet");
		}
		if (pHashSet.Count == 0)
		{
			throw new InvalidOperationException("Cannot pop from an empty HashSet.");
		}
		int tLastIndex = pHashSet.Count - 1;
		int tIndex = 0;
		foreach (T tItem in pHashSet)
		{
			if (tIndex++ == tLastIndex)
			{
				pHashSet.Remove(tItem);
				return tItem;
			}
		}
		throw new InvalidOperationException("Unexpected error: HashSet is empty after iteration.");
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x00139278 File Offset: 0x00137478
	public static T Shift<T>(this HashSet<T> pHashSet)
	{
		if (pHashSet == null)
		{
			throw new ArgumentNullException("pHashSet");
		}
		if (pHashSet.Count == 0)
		{
			throw new InvalidOperationException("Cannot shift from an empty HashSet.");
		}
		using (HashSet<T>.Enumerator enumerator = pHashSet.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				T tItem = enumerator.Current;
				pHashSet.Remove(tItem);
				return tItem;
			}
		}
		throw new InvalidOperationException("Unexpected error: HashSet is empty after iteration.");
	}
}
