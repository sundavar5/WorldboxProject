using System;
using System.Collections.Generic;

// Token: 0x02000508 RID: 1288
public class BenchmarkLoops
{
	// Token: 0x06002A91 RID: 10897 RVA: 0x00151B64 File Offset: 0x0014FD64
	public BenchmarkLoops(DebugToolAsset pAsset, int pMaxAmount)
	{
		if (BenchmarkLoops._benchmarks.ContainsKey(pAsset.benchmark_group_id))
		{
			return;
		}
		BenchmarkLoops._benchmarks.Add(pAsset.benchmark_group_id, this);
		this._max_amount = pMaxAmount;
		this._asset = pAsset;
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x00151BCA File Offset: 0x0014FDCA
	public static void update(DebugToolAsset pAsset)
	{
		BenchmarkLoops._benchmarks[pAsset.benchmark_group_id].run();
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x00151BE4 File Offset: 0x0014FDE4
	public unsafe void run()
	{
		string tGroupID = this._asset.benchmark_group_id;
		string tTotalGroupID = this._asset.benchmark_total_group;
		int tCountTotal = this._test_world_tiles.Count;
		this._counter = Randy.randomBool();
		int num = this._runs;
		this._runs = num + 1;
		if (num > 10 || this._test_world_tiles_arr == null)
		{
			this._runs = 0;
			ListPool<WorldTile> test_world_tiles_pool = this._test_world_tiles_pool;
			if (test_world_tiles_pool != null)
			{
				test_world_tiles_pool.Dispose();
			}
			this._test_hashset.Clear();
			WorldTile[] test_world_tiles_arr = this._test_world_tiles_arr;
			if (test_world_tiles_arr != null)
			{
				test_world_tiles_arr.Clear<WorldTile>();
			}
			this._test_world_tiles.Clear();
			foreach (WorldTile worldTile in this._new_tiles)
			{
				worldTile.Dispose();
			}
			this._new_tiles.Clear();
			for (int i = 0; i < this._max_amount; i++)
			{
				this._test_world_tiles.Add(World.world.tiles_list.GetRandom<WorldTile>());
			}
			this._test_hashset.UnionWith(this._test_world_tiles);
			this._test_world_tiles_pool = new ListPool<WorldTile>(this._test_world_tiles);
			this._test_world_tiles_arr = this._test_world_tiles.ToArray();
		}
		Bench.bench(tGroupID, tTotalGroupID, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		this._test_world_tiles.Shuffle<WorldTile>();
		this._test_world_tiles.Shuffle<WorldTile>();
		this._test_world_tiles.Shuffle<WorldTile>();
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("list_for_field", tGroupID, false);
		int tResult = 0;
		tCountTotal = 0;
		for (int j = 0; j < this._test_world_tiles.Count; j++)
		{
			WorldTile tTile = this._test_world_tiles[j];
			tResult += tTile.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_for_field", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		for (int k = 0; k < this._test_world_tiles_pool.Count; k++)
		{
			WorldTile tTile2 = this._test_world_tiles_pool[k];
			tResult += tTile2.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_for_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_span_for", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		Span<WorldTile> tNewSpan = this._test_world_tiles_pool.AsSpan();
		for (int l = 0; l < tNewSpan.Length; l++)
		{
			WorldTile tTile3 = *tNewSpan[l];
			tResult += tTile3.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_span_for", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_for_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		List<WorldTile> tList = this._test_world_tiles;
		for (int m = 0; m < tList.Count; m++)
		{
			WorldTile tTile4 = tList[m];
			tResult += tTile4.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_for_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		ListPool<WorldTile> tListPool = this._test_world_tiles_pool;
		for (int n = 0; n < tListPool.Count; n++)
		{
			WorldTile tTile5 = tListPool[n];
			tResult += tTile5.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_for_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_span_for_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		Span<WorldTile> tSpan = this._test_world_tiles_pool.AsSpan();
		for (int i2 = 0; i2 < tSpan.Length; i2++)
		{
			WorldTile tTile6 = *tSpan[i2];
			tResult += tTile6.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_span_for_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_for_local_len", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tList = this._test_world_tiles;
		int tLen = tList.Count;
		for (int i3 = 0; i3 < tLen; i3++)
		{
			WorldTile tTile7 = tList[i3];
			tResult += tTile7.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for_local_len", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_for_local_len", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tListPool = this._test_world_tiles_pool;
		tLen = tListPool.Count;
		for (int i4 = 0; i4 < tLen; i4++)
		{
			WorldTile tTile8 = tListPool[i4];
			tResult += tTile8.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_for_local_len", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_span_for_local_len", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = this._test_world_tiles_pool.AsSpan();
		tLen = tSpan.Length;
		for (int i5 = 0; i5 < tLen; i5++)
		{
			WorldTile tTile9 = *tSpan[i5];
			tResult += tTile9.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_span_for_local_len", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_foreach_field", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTile tTile10 in this._test_world_tiles)
		{
			tResult += tTile10.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_foreach_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_foreach_field", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTile ptr in this._test_world_tiles_pool)
		{
			WorldTile tTile11 = ptr;
			tResult += tTile11.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_foreach_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_foreach_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tList = this._test_world_tiles;
		foreach (WorldTile tTile12 in tList)
		{
			tResult += tTile12.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_foreach_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_foreach_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tListPool = this._test_world_tiles_pool;
		foreach (WorldTile ptr2 in tListPool)
		{
			WorldTile tTile13 = ptr2;
			tResult += tTile13.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_foreach_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_pool.Shuffle<WorldTile>();
		Bench.bench("lpool_span_foreach", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = this._test_world_tiles_pool.AsSpan();
		Span<WorldTile> span = tSpan;
		for (num = 0; num < span.Length; num++)
		{
			WorldTile tTile14 = *span[num];
			tResult += tTile14.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("lpool_span_foreach", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_span_for", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = this._test_world_tiles.AsSpan<WorldTile>();
		for (int i6 = 0; i6 < tSpan.Length; i6++)
		{
			WorldTile tTile15 = *tSpan[i6];
			tResult += tTile15.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_span_for", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_span_for_new", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		Span<WorldTile> tLocalSpan = this._test_world_tiles.AsSpan<WorldTile>();
		for (int i7 = 0; i7 < tLocalSpan.Length; i7++)
		{
			WorldTile tTile16 = *tLocalSpan[i7];
			tResult += tTile16.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_span_for_new", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_span_foreach", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = this._test_world_tiles.AsSpan<WorldTile>();
		span = tSpan;
		for (num = 0; num < span.Length; num++)
		{
			WorldTile tTile17 = *span[num];
			tResult += tTile17.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_span_foreach", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_span_foreach_new", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		span = this._test_world_tiles.AsSpan<WorldTile>();
		for (num = 0; num < span.Length; num++)
		{
			WorldTile tTile18 = *span[num];
			tResult += tTile18.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_span_foreach_new", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_rspan_for", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		ReadOnlySpan<WorldTile> tReadOnlySpan = this._test_world_tiles.AsReadOnlySpan<WorldTile>();
		for (int i8 = 0; i8 < tReadOnlySpan.Length; i8++)
		{
			WorldTile tTile19 = *tReadOnlySpan[i8];
			tResult += tTile19.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_rspan_for", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_rspan_for_new", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		ReadOnlySpan<WorldTile> tLocalReadOnlySpan = this._test_world_tiles.AsReadOnlySpan<WorldTile>();
		for (int i9 = 0; i9 < tLocalReadOnlySpan.Length; i9++)
		{
			WorldTile tTile20 = *tLocalReadOnlySpan[i9];
			tResult += tTile20.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_rspan_for_new", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_rspan_foreach", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tReadOnlySpan = this._test_world_tiles.AsReadOnlySpan<WorldTile>();
		ReadOnlySpan<WorldTile> readOnlySpan = tReadOnlySpan;
		for (num = 0; num < readOnlySpan.Length; num++)
		{
			WorldTile tTile21 = *readOnlySpan[num];
			tResult += tTile21.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_rspan_foreach", tGroupID, true, (long)tResult, false);
		this._test_world_tiles.Shuffle<WorldTile>();
		Bench.bench("list_rspan_foreach_new", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		readOnlySpan = this._test_world_tiles.AsReadOnlySpan<WorldTile>();
		for (num = 0; num < readOnlySpan.Length; num++)
		{
			WorldTile tTile22 = *readOnlySpan[num];
			tResult += tTile22.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_rspan_foreach_new", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_for_field", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		for (int i10 = 0; i10 < this._test_world_tiles_arr.Length; i10++)
		{
			WorldTile tTile23 = this._test_world_tiles_arr[i10];
			tResult += tTile23.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_for_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_for_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTile tTile24 in this._test_world_tiles_arr)
		{
			tResult += tTile24.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_for_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_for_local_len", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		WorldTile[] tArr = this._test_world_tiles_arr;
		tLen = tArr.Length;
		for (int i12 = 0; i12 < tLen; i12++)
		{
			WorldTile tTile25 = tArr[i12];
			tResult += tTile25.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_for_local_len", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_foreach_field", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTile tTile26 in this._test_world_tiles_arr)
		{
			tResult += tTile26.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_foreach_field", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_foreach_local", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tArr = this._test_world_tiles_arr;
		foreach (WorldTile tTile27 in tArr)
		{
			tResult += tTile27.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_foreach_local", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_rspan_foreach", tGroupID, false);
		tReadOnlySpan = new ReadOnlySpan<WorldTile>(this._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		readOnlySpan = tReadOnlySpan;
		for (num = 0; num < readOnlySpan.Length; num++)
		{
			WorldTile tTile28 = *readOnlySpan[num];
			tResult += tTile28.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_rspan_foreach", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_rspan_for", tGroupID, false);
		tReadOnlySpan = new ReadOnlySpan<WorldTile>(this._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		for (int i13 = 0; i13 < tReadOnlySpan.Length; i13++)
		{
			WorldTile tTile29 = *tReadOnlySpan[i13];
			tResult += tTile29.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_rspan_for", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_span_foreach", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = new Span<WorldTile>(this._test_world_tiles_arr);
		span = tSpan;
		for (num = 0; num < span.Length; num++)
		{
			WorldTile tTile30 = *span[num];
			tResult += tTile30.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_span_foreach", tGroupID, true, (long)tResult, false);
		this._test_world_tiles_arr.Shuffle<WorldTile>();
		Bench.bench("arr_span_for", tGroupID, false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = new Span<WorldTile>(this._test_world_tiles_arr);
		for (int i14 = 0; i14 < tSpan.Length; i14++)
		{
			WorldTile tTile31 = *tSpan[i14];
			tResult += tTile31.data.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("arr_span_for", tGroupID, true, (long)tResult, false);
		Bench.benchEnd(tGroupID, tTotalGroupID, false, 0L, false);
	}

	// Token: 0x04001FD5 RID: 8149
	private List<WorldTile> _test_world_tiles = new List<WorldTile>();

	// Token: 0x04001FD6 RID: 8150
	private ListPool<WorldTile> _test_world_tiles_pool;

	// Token: 0x04001FD7 RID: 8151
	private HashSet<WorldTile> _test_hashset = new HashSet<WorldTile>();

	// Token: 0x04001FD8 RID: 8152
	private WorldTile[] _test_world_tiles_arr;

	// Token: 0x04001FD9 RID: 8153
	private List<WorldTile> _new_tiles = new List<WorldTile>();

	// Token: 0x04001FDA RID: 8154
	private int _runs;

	// Token: 0x04001FDB RID: 8155
	private bool _counter;

	// Token: 0x04001FDC RID: 8156
	private int _max_amount;

	// Token: 0x04001FDD RID: 8157
	private DebugToolAsset _asset;

	// Token: 0x04001FDE RID: 8158
	internal static Dictionary<string, BenchmarkLoops> _benchmarks = new Dictionary<string, BenchmarkLoops>();
}
