using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000509 RID: 1289
public class BenchmarkShuffle
{
	// Token: 0x06002A95 RID: 10901 RVA: 0x00152B24 File Offset: 0x00150D24
	public BenchmarkShuffle(DebugToolAsset pAsset, int pAmount, int pMaxTiles)
	{
		if (BenchmarkShuffle._benchmarks.ContainsKey(pAsset.benchmark_group_id))
		{
			return;
		}
		this.amount = pAmount;
		this.max_tiles = pMaxTiles;
		this.benchmark_total_group_id = pAsset.benchmark_total_group;
		this.benchmark_group_id = pAsset.benchmark_group_id;
		this.test_tiles = new List<WorldTile>();
		BenchmarkShuffle._benchmarks.Add(pAsset.benchmark_group_id, this);
		this.setup();
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x00152B92 File Offset: 0x00150D92
	public static void update(DebugToolAsset pAsset)
	{
		BenchmarkShuffle._benchmarks[pAsset.benchmark_group_id].run();
	}

	// Token: 0x06002A97 RID: 10903 RVA: 0x00152BAC File Offset: 0x00150DAC
	public void setup()
	{
		if (!Config.game_loaded)
		{
			MapBox.on_world_loaded = (Action)Delegate.Combine(MapBox.on_world_loaded, new Action(delegate()
			{
				this.setup();
			}));
			return;
		}
		float num = (float)this.max_tiles;
		this.test_tiles.Clear();
		int tGenerateTiles = Mathf.CeilToInt(Mathf.Sqrt(num));
		tGenerateTiles *= tGenerateTiles;
		using (ListPool<WorldTile> tTiles = new ListPool<WorldTile>(World.world.tiles_list))
		{
			tTiles.Shuffle<WorldTile>();
			for (int i = 0; i < tGenerateTiles; i++)
			{
				this.test_tiles.Add(tTiles.Pop<WorldTile>());
			}
			this.test_tiles.Shuffle<WorldTile>();
		}
	}

	// Token: 0x06002A98 RID: 10904 RVA: 0x00152C5C File Offset: 0x00150E5C
	public void run()
	{
		int tAmount = this.amount;
		string tBenchmarkGroupId = this.benchmark_total_group_id;
		string tBenchmarkId = this.benchmark_group_id;
		int tCountTotal = 0;
		int tResult = 0;
		List<WorldTile> tTiles = this.test_tiles;
		for (int i = tAmount - 1; i >= 0; i--)
		{
			WorldTile tTile = tTiles[i];
			tResult += tTile.data.tile_id;
			tCountTotal++;
		}
		Bench.bench(tBenchmarkId, tBenchmarkGroupId, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("no_shuffle_for_{0}", tAmount), tBenchmarkId, false);
		for (int j = 0; j < tAmount; j++)
		{
			WorldTile tTile2 = tTiles[j];
			tResult += tTile2.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("no_shuffle_for_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_all_{0}", tAmount), tBenchmarkId, false);
		tTiles.Shuffle<WorldTile>();
		for (int k = 0; k < tAmount; k++)
		{
			WorldTile tTile3 = tTiles[k];
			tResult += tTile3.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("shuffle_all_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_one_new_list_{0}", tAmount), tBenchmarkId, false);
		ListPool<WorldTile> tNewList = new ListPool<WorldTile>(tTiles);
		for (int l = 0; l < tAmount; l++)
		{
			tNewList.ShuffleOne(l);
			WorldTile tTile4 = tNewList[l];
			tResult += tTile4.data.tile_id;
			tCountTotal++;
		}
		tNewList.Dispose();
		Bench.benchEnd(string.Format("shuffle_one_new_list_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_one_{0}", tAmount), tBenchmarkId, false);
		for (int m = 0; m < tAmount; m++)
		{
			tTiles.ShuffleOne(m);
			WorldTile tTile5 = tTiles[m];
			tResult += tTile5.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("shuffle_one_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_for_{0}", tAmount), tBenchmarkId, false);
		int tRandomStart = Randy.randomInt(0, tAmount);
		int tLength = tAmount + tRandomStart;
		for (int n = tRandomStart; n < tLength; n++)
		{
			int j2 = n % tAmount;
			WorldTile tTile6 = tTiles[j2];
			tResult += tTile6.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("shuffle_for_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_2for_{0}", tAmount), tBenchmarkId, false);
		tRandomStart = Randy.randomInt(0, tAmount);
		for (int i2 = tRandomStart; i2 < tAmount; i2++)
		{
			WorldTile tTile7 = tTiles[i2];
			tResult += tTile7.data.tile_id;
			tCountTotal++;
		}
		for (int i3 = 0; i3 < tRandomStart; i3++)
		{
			WorldTile tTile8 = tTiles[i3];
			tResult += tTile8.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("shuffle_2for_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_iterator_{0}", tAmount), tBenchmarkId, false);
		foreach (WorldTile tTile9 in tTiles.LoopRandom<WorldTile>())
		{
			tResult += tTile9.data.tile_id;
			tCountTotal++;
			if (tCountTotal == tAmount)
			{
				break;
			}
		}
		Bench.benchEnd(string.Format("shuffle_iterator_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("shuffle_iterator_limit_{0}", tAmount), tBenchmarkId, false);
		foreach (WorldTile tTile10 in tTiles.LoopRandom(tAmount))
		{
			tResult += tTile10.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd(string.Format("shuffle_iterator_limit_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		this.test_tiles.Shuffle<WorldTile>();
		tResult = 0;
		tCountTotal = 0;
		Bench.bench(string.Format("no_shuffle_iterator_{0}", tAmount), tBenchmarkId, false);
		foreach (WorldTile tTile11 in tTiles)
		{
			tResult += tTile11.data.tile_id;
			tCountTotal++;
			if (tCountTotal == tAmount)
			{
				break;
			}
		}
		Bench.benchEnd(string.Format("no_shuffle_iterator_{0}", tAmount), tBenchmarkId, true, (long)tCountTotal, false);
		Bench.benchEnd(tBenchmarkId, tBenchmarkGroupId, false, 0L, false);
		if (this.print_to_console)
		{
			Debug.Log("LAST:\n" + Bench.printableBenchResults(tBenchmarkId, false, new string[]
			{
				string.Format("no_shuffle_for_{0}", tAmount),
				string.Format("no_shuffle_iterator_{0}", tAmount),
				string.Format("shuffle_iterator_{0}", tAmount),
				string.Format("shuffle_iterator_limit_{0}", tAmount),
				string.Format("shuffle_for_{0}", tAmount),
				string.Format("shuffle_2for_{0}", tAmount),
				string.Format("shuffle_one_{0}", tAmount),
				string.Format("shuffle_one_new_list_{0}", tAmount),
				string.Format("shuffle_all_{0}", tAmount)
			}));
			Debug.Log("AVG:\n" + Bench.printableBenchResults(tBenchmarkId, true, new string[]
			{
				string.Format("no_shuffle_for_{0}", tAmount),
				string.Format("no_shuffle_iterator_{0}", tAmount),
				string.Format("shuffle_iterator_{0}", tAmount),
				string.Format("shuffle_iterator_limit_{0}", tAmount),
				string.Format("shuffle_for_{0}", tAmount),
				string.Format("shuffle_2for_{0}", tAmount),
				string.Format("shuffle_one_{0}", tAmount),
				string.Format("shuffle_one_new_list_{0}", tAmount),
				string.Format("shuffle_all_{0}", tAmount)
			}));
		}
		this.result = tResult;
	}

	// Token: 0x04001FDF RID: 8159
	public int result;

	// Token: 0x04001FE0 RID: 8160
	internal int max_tiles;

	// Token: 0x04001FE1 RID: 8161
	internal int amount;

	// Token: 0x04001FE2 RID: 8162
	internal string benchmark_total_group_id;

	// Token: 0x04001FE3 RID: 8163
	internal string benchmark_group_id;

	// Token: 0x04001FE4 RID: 8164
	internal List<WorldTile> test_tiles;

	// Token: 0x04001FE5 RID: 8165
	internal bool print_to_console;

	// Token: 0x04001FE6 RID: 8166
	internal static Dictionary<string, BenchmarkShuffle> _benchmarks = new Dictionary<string, BenchmarkShuffle>();
}
