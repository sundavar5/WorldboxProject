using System;
using System.Collections.Generic;

// Token: 0x02000262 RID: 610
public static class OnomasticsEvolver
{
	// Token: 0x060016F5 RID: 5877 RVA: 0x000E52A1 File Offset: 0x000E34A1
	public static void add(OnomasticsEvolutionAsset pEvolution)
	{
		OnomasticsEvolver._pool.Add(pEvolution);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000E52AE File Offset: 0x000E34AE
	public static void shuffle()
	{
		OnomasticsEvolver._pool.Shuffle<OnomasticsEvolutionAsset>();
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000E52BC File Offset: 0x000E34BC
	public static bool scramble(OnomasticsData pData)
	{
		int tChanged = 0;
		foreach (KeyValuePair<string, OnomasticsDataGroup> tPair in pData.groups)
		{
			OnomasticsDataGroup tGroup = tPair.Value;
			if (!tGroup.isEmpty())
			{
				OnomasticsEvolver._replaced.Clear();
				string[] tSplitGroup = tGroup.characters_string.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < tSplitGroup.Length; i++)
				{
					OnomasticsEvolver._replaced.Add(i, false);
				}
				int tGroupChanged = 0;
				for (int j = 0; j < OnomasticsEvolver._pool.Count; j++)
				{
					if (tGroupChanged < 7)
					{
						OnomasticsEvolutionAsset tEvolution = OnomasticsEvolver._pool[j];
						bool tChangedThisReplacer = false;
						if (!tSplitGroup.Contains(tEvolution.to))
						{
							for (int k = 0; k < tSplitGroup.Length; k++)
							{
								if (!OnomasticsEvolver._replaced[k])
								{
									string tSplitString = tSplitGroup[k];
									if (tEvolution.replacer(tEvolution, ref tSplitString))
									{
										tChangedThisReplacer = true;
										OnomasticsEvolver._replaced[k] = true;
									}
									tSplitGroup[k] = tSplitString;
								}
							}
							if (tChangedThisReplacer)
							{
								tGroupChanged++;
							}
						}
					}
				}
				tGroup.characters_string = string.Join(' ', tSplitGroup);
				tGroup.characters = tSplitGroup;
				if (tGroupChanged > 0)
				{
					tChanged++;
				}
			}
		}
		return tChanged > 0;
	}

	// Token: 0x040012D8 RID: 4824
	private static List<OnomasticsEvolutionAsset> _pool = new List<OnomasticsEvolutionAsset>();

	// Token: 0x040012D9 RID: 4825
	private static Dictionary<int, bool> _replaced = new Dictionary<int, bool>();
}
