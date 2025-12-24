using System;
using System.Collections.Generic;

// Token: 0x0200044F RID: 1103
public static class DictionaryExtensions
{
	// Token: 0x0600261D RID: 9757 RVA: 0x001380A4 File Offset: 0x001362A4
	public static int RemoveByValue<TKey, TValue>(this IDictionary<TKey, TValue> pDict, Predicate<TValue> pPredicate)
	{
		int count;
		using (ListPool<TKey> tKeysToRemove = new ListPool<TKey>(pDict.Count))
		{
			foreach (KeyValuePair<TKey, TValue> tPair in pDict)
			{
				if (pPredicate(tPair.Value))
				{
					tKeysToRemove.Add(tPair.Key);
				}
			}
			foreach (TKey ptr in tKeysToRemove)
			{
				TKey tKey = ptr;
				pDict.Remove(tKey);
			}
			count = tKeysToRemove.Count;
		}
		return count;
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x00138174 File Offset: 0x00136374
	public static int RemoveByKey<TKey, TValue>(this IDictionary<TKey, TValue> pDict, Predicate<TKey> pPredicate)
	{
		int count;
		using (ListPool<TKey> tKeysToRemove = new ListPool<TKey>(pDict.Count))
		{
			foreach (TKey tKey in pDict.Keys)
			{
				if (pPredicate(tKey))
				{
					tKeysToRemove.Add(tKey);
				}
			}
			foreach (TKey ptr in tKeysToRemove)
			{
				TKey tKey2 = ptr;
				pDict.Remove(tKey2);
			}
			count = tKeysToRemove.Count;
		}
		return count;
	}
}
