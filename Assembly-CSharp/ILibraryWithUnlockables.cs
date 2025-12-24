using System;
using System.Collections.Generic;

// Token: 0x02000049 RID: 73
public interface ILibraryWithUnlockables
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060002FC RID: 764
	IEnumerable<BaseUnlockableAsset> elements_list { get; }

	// Token: 0x060002FD RID: 765 RVA: 0x0001CE20 File Offset: 0x0001B020
	int countTotalKnowledge()
	{
		int tTotalAmount = 0;
		using (IEnumerator<BaseUnlockableAsset> enumerator = this.elements_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.show_in_knowledge_window)
				{
					tTotalAmount++;
				}
			}
		}
		return tTotalAmount;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0001CE74 File Offset: 0x0001B074
	int countUnlockedByPlayer()
	{
		int tUnlockedAmount = 0;
		foreach (BaseUnlockableAsset tAsset in this.elements_list)
		{
			if (tAsset.show_in_knowledge_window && tAsset.isUnlockedByPlayer())
			{
				tUnlockedAmount++;
			}
		}
		return tUnlockedAmount;
	}
}
