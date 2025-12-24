using System;
using System.Collections.Generic;

// Token: 0x02000455 RID: 1109
public static class EnumerableExtensions
{
	// Token: 0x06002628 RID: 9768 RVA: 0x001386FC File Offset: 0x001368FC
	public static T GetRandom<T>(this IEnumerable<T> pEnumerable)
	{
		List<T> tList = pEnumerable as List<T>;
		if (tList != null)
		{
			return tList.GetRandom<T>();
		}
		ListPool<T> tListPool = pEnumerable as ListPool<T>;
		if (tListPool != null)
		{
			return tListPool.GetRandom<T>();
		}
		T[] tArray = pEnumerable as T[];
		if (tArray != null)
		{
			return tArray.GetRandom<T>();
		}
		HashSet<T> tHashSet = pEnumerable as HashSet<T>;
		if (tHashSet == null)
		{
			T random;
			using (ListPool<T> tTempList = new ListPool<T>(pEnumerable))
			{
				random = tTempList.GetRandom<T>();
			}
			return random;
		}
		return tHashSet.GetRandom<T>();
	}
}
