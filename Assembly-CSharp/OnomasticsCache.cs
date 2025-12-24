using System;
using System.Collections.Generic;

// Token: 0x0200025F RID: 607
public static class OnomasticsCache
{
	// Token: 0x060016D3 RID: 5843 RVA: 0x000E46F8 File Offset: 0x000E28F8
	public static OnomasticsData getOriginalData(string pShortTemplate)
	{
		if (OnomasticsCache._cache == null)
		{
			OnomasticsCache._cache = new Dictionary<string, OnomasticsData>();
		}
		OnomasticsData tData;
		if (!OnomasticsCache._cache.TryGetValue(pShortTemplate, out tData))
		{
			tData = new OnomasticsData();
			tData.loadFromShortTemplate(pShortTemplate);
			OnomasticsCache._cache.Add(pShortTemplate, tData);
		}
		return tData;
	}

	// Token: 0x040012CD RID: 4813
	[ThreadStatic]
	private static Dictionary<string, OnomasticsData> _cache;
}
