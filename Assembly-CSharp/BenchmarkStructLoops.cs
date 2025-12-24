using System;
using System.Collections.Generic;

// Token: 0x0200050B RID: 1291
public static class BenchmarkStructLoops
{
	// Token: 0x06002A9D RID: 10909 RVA: 0x0015349C File Offset: 0x0015169C
	public unsafe static void start()
	{
		int tCountTotal = BenchmarkStructLoops._test_world_tiles.Count;
		if (BenchmarkStructLoops._runs++ > 30 || BenchmarkStructLoops._test_world_tiles_arr == null)
		{
			BenchmarkStructLoops._runs = 0;
			ListPool<WorldTileDataStruct> test_world_tiles_pool = BenchmarkStructLoops._test_world_tiles_pool;
			if (test_world_tiles_pool != null)
			{
				test_world_tiles_pool.Dispose();
			}
			BenchmarkStructLoops._test_world_tiles.Clear();
			BenchmarkStructLoops._test_hashset.Clear();
			int tRepeats = Randy.randomInt(1, 5);
			int tCount = World.world.tiles_list.Length;
			for (int i = 0; i < tRepeats; i++)
			{
				for (int j = 0; j < tCount; j++)
				{
					WorldTile tTile = World.world.tiles_list[j];
					int tTileID = tTile.data.tile_id + i * tCount;
					BenchmarkStructLoops._test_world_tiles.Add(new WorldTileDataStruct(tTile, tTileID));
				}
				BenchmarkStructLoops._test_world_tiles.Shuffle<WorldTileDataStruct>();
			}
			BenchmarkStructLoops._test_hashset.UnionWith(BenchmarkStructLoops._test_world_tiles);
			BenchmarkStructLoops._test_world_tiles_pool = new ListPool<WorldTileDataStruct>(BenchmarkStructLoops._test_world_tiles);
			BenchmarkStructLoops._test_world_tiles_arr = BenchmarkStructLoops._test_world_tiles.ToArray();
		}
		Bench.bench("loops_struct_test", "loops_struct_test_total", false);
		Bench.bench("list_for", "loops_struct_test", false);
		int tResult = 0;
		tCountTotal = 0;
		for (int k = 0; k < BenchmarkStructLoops._test_world_tiles.Count; k++)
		{
			WorldTileDataStruct tTile2 = BenchmarkStructLoops._test_world_tiles[k];
			tResult += tTile2.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_for", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		for (int l = 0; l < BenchmarkStructLoops._test_world_tiles_pool.Count; l++)
		{
			WorldTileDataStruct tTile3 = BenchmarkStructLoops._test_world_tiles_pool[l];
			tResult += tTile3.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_span_for", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		Span<WorldTileDataStruct> tNewSpan = BenchmarkStructLoops._test_world_tiles_pool.AsSpan();
		for (int m = 0; m < tNewSpan.Length; m++)
		{
			WorldTileDataStruct tTile4 = *tNewSpan[m];
			tResult += tTile4.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_span_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("hashset_foreach", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTileDataStruct tWorldTile in BenchmarkStructLoops._test_hashset)
		{
			tResult += tWorldTile.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("hashset_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("list_for_local", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		List<WorldTileDataStruct> tList = BenchmarkStructLoops._test_world_tiles;
		for (int n = 0; n < tList.Count; n++)
		{
			WorldTileDataStruct tTile5 = tList[n];
			tResult += tTile5.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for_local", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_for_local", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		ListPool<WorldTileDataStruct> tListPool = BenchmarkStructLoops._test_world_tiles_pool;
		for (int i2 = 0; i2 < tListPool.Count; i2++)
		{
			WorldTileDataStruct tTile6 = tListPool[i2];
			tResult += tTile6.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_for_local", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_span_for_local", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		Span<WorldTileDataStruct> tSpan = BenchmarkStructLoops._test_world_tiles_pool.AsSpan();
		for (int i3 = 0; i3 < tSpan.Length; i3++)
		{
			WorldTileDataStruct tTile7 = *tSpan[i3];
			tResult += tTile7.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_span_for_local", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("list_for_local_len", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		tList = BenchmarkStructLoops._test_world_tiles;
		int tLen = tList.Count;
		for (int i4 = 0; i4 < tLen; i4++)
		{
			WorldTileDataStruct tTile8 = tList[i4];
			tResult += tTile8.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_for_local_len", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_for_local_len", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		tListPool = BenchmarkStructLoops._test_world_tiles_pool;
		tLen = tListPool.Count;
		for (int i5 = 0; i5 < tLen; i5++)
		{
			WorldTileDataStruct tTile9 = tListPool[i5];
			tResult += tTile9.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_for_local_len", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_span_for_local_len", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = BenchmarkStructLoops._test_world_tiles_pool.AsSpan();
		tLen = tSpan.Length;
		for (int i6 = 0; i6 < tLen; i6++)
		{
			WorldTileDataStruct tTile10 = *tSpan[i6];
			tResult += tTile10.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_span_for_local_len", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("list_foreach", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTileDataStruct tTile11 in BenchmarkStructLoops._test_world_tiles)
		{
			tResult += tTile11.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("list_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_foreach", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTileDataStruct ptr in BenchmarkStructLoops._test_world_tiles_pool)
		{
			WorldTileDataStruct tTile12 = ptr;
			tResult += tTile12.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("listpool_span_foreach", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		tSpan = BenchmarkStructLoops._test_world_tiles_pool.AsSpan();
		Span<WorldTileDataStruct> span = tSpan;
		for (int num = 0; num < span.Length; num++)
		{
			WorldTileDataStruct tTile13 = *span[num];
			tResult += tTile13.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("listpool_span_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("array_for", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		for (int i7 = 0; i7 < BenchmarkStructLoops._test_world_tiles_arr.Length; i7++)
		{
			WorldTileDataStruct tTile14 = BenchmarkStructLoops._test_world_tiles_arr[i7];
			tResult += tTile14.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("array_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("array_for_local", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTileDataStruct tTile15 in BenchmarkStructLoops._test_world_tiles_arr)
		{
			tResult += tTile15.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("array_for_local", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("array_for_local_len", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		WorldTileDataStruct[] tArr = BenchmarkStructLoops._test_world_tiles_arr;
		tLen = BenchmarkStructLoops._test_world_tiles_arr.Length;
		for (int i9 = 0; i9 < tLen; i9++)
		{
			WorldTileDataStruct tTile16 = tArr[i9];
			tResult += tTile16.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("array_for_local_len", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("array_foreach", "loops_struct_test", false);
		tResult = 0;
		tCountTotal = 0;
		foreach (WorldTileDataStruct tTile17 in BenchmarkStructLoops._test_world_tiles_arr)
		{
			tResult += tTile17.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("array_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("ro_span_foreach", "loops_struct_test", false);
		ReadOnlySpan<WorldTileDataStruct> tReadOnlySpan = new ReadOnlySpan<WorldTileDataStruct>(BenchmarkStructLoops._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		ReadOnlySpan<WorldTileDataStruct> readOnlySpan = tReadOnlySpan;
		for (int num = 0; num < readOnlySpan.Length; num++)
		{
			WorldTileDataStruct tTile18 = *readOnlySpan[num];
			tResult += tTile18.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("ro_span_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("ro_span_for", "loops_struct_test", false);
		tReadOnlySpan = new ReadOnlySpan<WorldTileDataStruct>(BenchmarkStructLoops._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		for (int i10 = 0; i10 < tReadOnlySpan.Length; i10++)
		{
			WorldTileDataStruct tTile19 = *tReadOnlySpan[i10];
			tResult += tTile19.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("ro_span_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("span_foreach", "loops_struct_test", false);
		tSpan = new Span<WorldTileDataStruct>(BenchmarkStructLoops._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		span = tSpan;
		for (int num = 0; num < span.Length; num++)
		{
			WorldTileDataStruct tTile20 = *span[num];
			tResult += tTile20.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("span_foreach", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.bench("span_for", "loops_struct_test", false);
		tSpan = new Span<WorldTileDataStruct>(BenchmarkStructLoops._test_world_tiles_arr);
		tResult = 0;
		tCountTotal = 0;
		for (int i11 = 0; i11 < tSpan.Length; i11++)
		{
			WorldTileDataStruct tTile21 = *tSpan[i11];
			tResult += tTile21.tile_id;
			tCountTotal++;
		}
		Bench.benchEnd("span_for", "loops_struct_test", true, (long)tCountTotal, false);
		Bench.benchEnd("loops_struct_test", "loops_struct_test_total", false, 0L, false);
	}

	// Token: 0x04001FE7 RID: 8167
	private static List<WorldTileDataStruct> _test_world_tiles = new List<WorldTileDataStruct>();

	// Token: 0x04001FE8 RID: 8168
	private static ListPool<WorldTileDataStruct> _test_world_tiles_pool;

	// Token: 0x04001FE9 RID: 8169
	private static HashSet<WorldTileDataStruct> _test_hashset = new HashSet<WorldTileDataStruct>();

	// Token: 0x04001FEA RID: 8170
	private static WorldTileDataStruct[] _test_world_tiles_arr;

	// Token: 0x04001FEB RID: 8171
	private static int _runs = 0;
}
