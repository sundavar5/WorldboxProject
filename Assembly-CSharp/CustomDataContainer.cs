using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityPools;

// Token: 0x02000263 RID: 611
[JsonConverter(typeof(CustomDataContainerConverter))]
[Serializable]
public class CustomDataContainer<TType> : IDisposable
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x000E5446 File Offset: 0x000E3646
	public bool TryGetValue(string pKey, out TType pValue)
	{
		return this.dict.TryGetValue(pKey, out pValue);
	}

	// Token: 0x17000153 RID: 339
	public TType this[string pKey]
	{
		get
		{
			return this.dict[pKey];
		}
		set
		{
			this.dict[pKey] = value;
		}
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000E5472 File Offset: 0x000E3672
	public void Remove(string pKey)
	{
		this.dict.Remove(pKey);
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060016FD RID: 5885 RVA: 0x000E5481 File Offset: 0x000E3681
	public IEnumerable<string> Keys
	{
		get
		{
			return this.dict.Keys;
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000E548E File Offset: 0x000E368E
	public void Dispose()
	{
		if (this.dict != null)
		{
			this.dict.Clear();
			UnsafeCollectionPool<Dictionary<string, TType>, KeyValuePair<string, TType>>.Release(this.dict);
		}
		this.dict = null;
	}

	// Token: 0x040012DA RID: 4826
	[NonSerialized]
	internal Dictionary<string, TType> dict = UnsafeCollectionPool<Dictionary<string, TType>, KeyValuePair<string, TType>>.Get();
}
