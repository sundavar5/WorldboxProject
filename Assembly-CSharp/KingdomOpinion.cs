using System;
using System.Collections.Generic;
using UnityPools;

// Token: 0x0200026C RID: 620
public class KingdomOpinion : IDisposable
{
	// Token: 0x0600173D RID: 5949 RVA: 0x000E67B5 File Offset: 0x000E49B5
	public KingdomOpinion(Kingdom k1, Kingdom k2)
	{
		this.main = k1;
		this.target = k2;
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000E67D8 File Offset: 0x000E49D8
	internal void calculate(Kingdom pMain, Kingdom pTarget, DiplomacyRelation pRelation)
	{
		this.clear();
		foreach (OpinionAsset tAsset in AssetManager.opinion_library.list)
		{
			int tResult = tAsset.calc(pMain, pTarget);
			this.total += tResult;
			if (tResult != 0)
			{
				this.results.Add(tAsset, tResult);
			}
		}
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x000E685C File Offset: 0x000E4A5C
	private void clear()
	{
		this.total = 0;
		this.results.Clear();
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000E6870 File Offset: 0x000E4A70
	public void Dispose()
	{
		this.clear();
		UnsafeCollectionPool<Dictionary<OpinionAsset, int>, KeyValuePair<OpinionAsset, int>>.Release(this.results);
		this.main = null;
		this.target = null;
	}

	// Token: 0x040012EC RID: 4844
	public readonly Dictionary<OpinionAsset, int> results = UnsafeCollectionPool<Dictionary<OpinionAsset, int>, KeyValuePair<OpinionAsset, int>>.Get();

	// Token: 0x040012ED RID: 4845
	public int total;

	// Token: 0x040012EE RID: 4846
	public Kingdom main;

	// Token: 0x040012EF RID: 4847
	public Kingdom target;
}
