using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000503 RID: 1283
public static class BenchmarkBlacklist
{
	// Token: 0x06002A83 RID: 10883 RVA: 0x0014FE80 File Offset: 0x0014E080
	public static void start()
	{
		if (BenchmarkBlacklist._runs-- <= 0)
		{
			BenchmarkBlacklist._names.Clear();
			BenchmarkBlacklist._names_set.Clear();
			BenchmarkBlacklist._max = Randy.randomInt(50, 255);
			BenchmarkBlacklist._runs = 5;
		}
		if (BenchmarkBlacklist._names.Count == 0)
		{
			BenchmarkBlacklist._good_words.Clear();
			BenchmarkBlacklist._bad_words.Clear();
			BenchmarkBlacklist._names_set.Clear();
			Blacklist.init();
			BlacklistTest.init();
			BlacklistTest2.init();
			BlacklistTest3.init();
			BlacklistTest4.init();
			BlacklistTest5.init();
			BlacklistTest6.init();
			BlacklistTest7.init();
			BlacklistTest8.init();
			BlacklistTest9.init();
			BlacklistTest10.init();
			BlacklistTest11.init();
			BlacklistTest12.init();
			BlacklistTest13.init();
			AssetManager.name_generator.list.Shuffle<NameGeneratorAsset>();
			foreach (NameGeneratorAsset nameGenAsset in AssetManager.name_generator.list)
			{
				if (BenchmarkBlacklist._names_set.Count > BenchmarkBlacklist._max)
				{
					break;
				}
				for (int i = 0; i < 150; i++)
				{
					string tInput = NameGenerator.generateNameFromTemplate(nameGenAsset, null, null, false, 0, null, null, false, null, ActorSex.None, false);
					if (string.IsNullOrEmpty(tInput))
					{
						Debug.LogError("name generator returned null or empty string " + nameGenAsset.id);
					}
					else
					{
						BenchmarkBlacklist._names_set.Add(tInput);
						if (BenchmarkBlacklist._names_set.Count > BenchmarkBlacklist._max)
						{
							break;
						}
					}
				}
			}
			BenchmarkBlacklist._names.AddRange(BenchmarkBlacklist._names_set);
			bool tOK = false;
			bool tOK2 = false;
			for (int j = 0; j < BenchmarkBlacklist._names.Count; j++)
			{
				if (!Blacklist.checkBlackList(BenchmarkBlacklist._names[j]))
				{
					tOK = true;
					BenchmarkBlacklist._good_words.Add(BenchmarkBlacklist._names[j]);
				}
				else
				{
					tOK2 = true;
					BenchmarkBlacklist._bad_words.Add(BenchmarkBlacklist._names[j]);
				}
			}
			if (!tOK || !tOK2)
			{
				BenchmarkBlacklist._runs = 0;
				BenchmarkBlacklist.start();
			}
			Debug.Log(string.Concat(new string[]
			{
				"Unique names for test ",
				BenchmarkBlacklist._names.Count.ToString(),
				" / ",
				BenchmarkBlacklist._max.ToString(),
				" => ",
				BenchmarkBlacklist._good_words.Count.ToString(),
				" / ",
				BenchmarkBlacklist._bad_words.Count.ToString()
			}));
		}
		BenchmarkBlacklist._result_good_words.Clear();
		BenchmarkBlacklist._result_bad_words.Clear();
		Bench.bench("blacklist_test", "blacklist_test_total", false);
		Bench.bench("current_blacklist_good", "blacklist_test", false);
		int tGood = 0;
		for (int k = 0; k < BenchmarkBlacklist._names.Count; k++)
		{
			if (!Blacklist.checkBlackList(BenchmarkBlacklist._names[k]))
			{
				tGood++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[k]);
			}
		}
		Bench.benchEnd("current_blacklist_good", "blacklist_test", true, (long)tGood, false);
		Bench.bench("current_blacklist_bad", "blacklist_test", false);
		int tBad = 0;
		for (int l = 0; l < BenchmarkBlacklist._names.Count; l++)
		{
			if (Blacklist.checkBlackList(BenchmarkBlacklist._names[l]))
			{
				tBad++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[l]);
			}
		}
		Bench.benchEnd("current_blacklist_bad", "blacklist_test", true, (long)tBad, false);
		BenchmarkBlacklist.checkResult("current_blacklist_bad");
		Bench.bench("three_blacklist_good_9", "blacklist_test", false);
		int tGood2 = 0;
		for (int m = 0; m < BenchmarkBlacklist._names.Count; m++)
		{
			if (!BlacklistTest9.checkBlackList(BenchmarkBlacklist._names[m]))
			{
				tGood2++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[m]);
			}
		}
		Bench.benchEnd("three_blacklist_good_9", "blacklist_test", true, (long)tGood2, false);
		Bench.bench("three_blacklist_bad_9", "blacklist_test", false);
		int tBad2 = 0;
		for (int n = 0; n < BenchmarkBlacklist._names.Count; n++)
		{
			if (BlacklistTest9.checkBlackList(BenchmarkBlacklist._names[n]))
			{
				tBad2++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[n]);
			}
		}
		Bench.benchEnd("three_blacklist_bad_9", "blacklist_test", true, (long)tBad2, false);
		BenchmarkBlacklist.checkResult("three_blacklist_bad_9");
		Bench.bench("old_blacklist_good_10", "blacklist_test", false);
		int tGood3 = 0;
		for (int i2 = 0; i2 < BenchmarkBlacklist._names.Count; i2++)
		{
			if (!BlacklistTest10.checkBlackList(BenchmarkBlacklist._names[i2]))
			{
				tGood3++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[i2]);
			}
		}
		Bench.benchEnd("old_blacklist_good_10", "blacklist_test", true, (long)tGood3, false);
		Bench.bench("old_blacklist_bad_10", "blacklist_test", false);
		int tBad3 = 0;
		for (int i3 = 0; i3 < BenchmarkBlacklist._names.Count; i3++)
		{
			if (BlacklistTest10.checkBlackList(BenchmarkBlacklist._names[i3]))
			{
				tBad3++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[i3]);
			}
		}
		Bench.benchEnd("old_blacklist_bad_10", "blacklist_test", true, (long)tBad3, false);
		BenchmarkBlacklist.checkResult("old_blacklist_bad_10");
		Bench.bench("slice_blacklist_good_11", "blacklist_test", false);
		int tGood4 = 0;
		for (int i4 = 0; i4 < BenchmarkBlacklist._names.Count; i4++)
		{
			if (!BlacklistTest11.checkBlackList(BenchmarkBlacklist._names[i4]))
			{
				tGood4++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[i4]);
			}
		}
		Bench.benchEnd("slice_blacklist_good_11", "blacklist_test", true, (long)tGood4, false);
		Bench.bench("slice_blacklist_bad_11", "blacklist_test", false);
		int tBad4 = 0;
		for (int i5 = 0; i5 < BenchmarkBlacklist._names.Count; i5++)
		{
			if (BlacklistTest11.checkBlackList(BenchmarkBlacklist._names[i5]))
			{
				tBad4++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[i5]);
			}
		}
		Bench.benchEnd("slice_blacklist_bad_11", "blacklist_test", true, (long)tBad4, false);
		BenchmarkBlacklist.checkResult("slice_blacklist_bad_11");
		Bench.bench("ref_blacklist_good_12", "blacklist_test", false);
		int tGood5 = 0;
		for (int i6 = 0; i6 < BenchmarkBlacklist._names.Count; i6++)
		{
			if (!BlacklistTest12.checkBlackList(BenchmarkBlacklist._names[i6]))
			{
				tGood5++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[i6]);
			}
		}
		Bench.benchEnd("ref_blacklist_good_12", "blacklist_test", true, (long)tGood5, false);
		Bench.bench("ref_blacklist_bad_12", "blacklist_test", false);
		int tBad5 = 0;
		for (int i7 = 0; i7 < BenchmarkBlacklist._names.Count; i7++)
		{
			if (BlacklistTest12.checkBlackList(BenchmarkBlacklist._names[i7]))
			{
				tBad5++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[i7]);
			}
		}
		Bench.benchEnd("ref_blacklist_bad_12", "blacklist_test", true, (long)tBad5, false);
		BenchmarkBlacklist.checkResult("ref_blacklist_bad_12");
		Bench.bench("idx_blacklist_good_13", "blacklist_test", false);
		int tGood6 = 0;
		for (int i8 = 0; i8 < BenchmarkBlacklist._names.Count; i8++)
		{
			if (!BlacklistTest13.checkBlackList(BenchmarkBlacklist._names[i8]))
			{
				tGood6++;
				BenchmarkBlacklist._result_good_words.Add(BenchmarkBlacklist._names[i8]);
			}
		}
		Bench.benchEnd("idx_blacklist_good_13", "blacklist_test", true, (long)tGood6, false);
		Bench.bench("idx_blacklist_bad_13", "blacklist_test", false);
		int tBad6 = 0;
		for (int i9 = 0; i9 < BenchmarkBlacklist._names.Count; i9++)
		{
			if (BlacklistTest13.checkBlackList(BenchmarkBlacklist._names[i9]))
			{
				tBad6++;
				BenchmarkBlacklist._result_bad_words.Add(BenchmarkBlacklist._names[i9]);
			}
		}
		Bench.benchEnd("idx_blacklist_bad_13", "blacklist_test", true, (long)tBad6, false);
		BenchmarkBlacklist.checkResult("idx_blacklist_bad_13");
		Bench.benchEnd("blacklist_test", "blacklist_test_total", false, 0L, false);
	}

	// Token: 0x06002A84 RID: 10884 RVA: 0x001506E8 File Offset: 0x0014E8E8
	public static void checkResult(string pBenchmarkName)
	{
		if (BenchmarkBlacklist._result_good_words.Count != BenchmarkBlacklist._good_words.Count || BenchmarkBlacklist._result_bad_words.Count != BenchmarkBlacklist._bad_words.Count)
		{
			Debug.LogError(string.Concat(new string[]
			{
				pBenchmarkName,
				": Blacklist check failed ",
				BenchmarkBlacklist._result_good_words.Count.ToString(),
				" ",
				BenchmarkBlacklist._good_words.Count.ToString(),
				" ",
				BenchmarkBlacklist._result_bad_words.Count.ToString(),
				" ",
				BenchmarkBlacklist._bad_words.Count.ToString()
			}));
			foreach (string tWord in BenchmarkBlacklist._result_good_words)
			{
				if (!BenchmarkBlacklist._good_words.Contains(tWord))
				{
					Debug.LogError(pBenchmarkName + ": Missing good word: " + tWord);
				}
			}
			foreach (string tWord2 in BenchmarkBlacklist._result_bad_words)
			{
				if (!BenchmarkBlacklist._bad_words.Contains(tWord2))
				{
					Debug.LogError(pBenchmarkName + ": Missing bad word: " + tWord2);
				}
			}
			foreach (string tWord3 in BenchmarkBlacklist._good_words)
			{
				if (!BenchmarkBlacklist._result_good_words.Contains(tWord3))
				{
					Debug.LogError(pBenchmarkName + ": Extra good word: " + tWord3);
				}
			}
			foreach (string tWord4 in BenchmarkBlacklist._bad_words)
			{
				if (!BenchmarkBlacklist._result_bad_words.Contains(tWord4))
				{
					Debug.LogError(pBenchmarkName + ": Extra bad word: " + tWord4);
				}
			}
		}
		BenchmarkBlacklist._result_good_words.Clear();
		BenchmarkBlacklist._result_bad_words.Clear();
	}

	// Token: 0x04001F89 RID: 8073
	private static List<WorldTile> _test_world_tiles = new List<WorldTile>();

	// Token: 0x04001F8A RID: 8074
	private static HashSet<WorldTile> _test_hashset = new HashSet<WorldTile>();

	// Token: 0x04001F8B RID: 8075
	private static WorldTile[] _test_world_tiles_arr;

	// Token: 0x04001F8C RID: 8076
	private static List<string> _names = new List<string>();

	// Token: 0x04001F8D RID: 8077
	private static HashSet<string> _names_set = new HashSet<string>();

	// Token: 0x04001F8E RID: 8078
	private static int _runs = 0;

	// Token: 0x04001F8F RID: 8079
	private static int _max = 250;

	// Token: 0x04001F90 RID: 8080
	private static HashSet<string> _good_words = new HashSet<string>();

	// Token: 0x04001F91 RID: 8081
	private static HashSet<string> _bad_words = new HashSet<string>();

	// Token: 0x04001F92 RID: 8082
	private static HashSet<string> _result_good_words = new HashSet<string>();

	// Token: 0x04001F93 RID: 8083
	private static HashSet<string> _result_bad_words = new HashSet<string>();
}
