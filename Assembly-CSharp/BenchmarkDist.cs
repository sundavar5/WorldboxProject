using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

// Token: 0x02000504 RID: 1284
public class BenchmarkDist
{
	// Token: 0x06002A86 RID: 10886 RVA: 0x00150999 File Offset: 0x0014EB99
	public BenchmarkDist()
	{
		if (BenchmarkDist._instance != null)
		{
			return;
		}
		this.benchmark_group_id = "dist_test_total";
		this.benchmark_id = "dist_test";
		this.test_tiles = new List<WorldTile>();
		BenchmarkDist._instance = this;
		this.setup();
	}

	// Token: 0x06002A87 RID: 10887 RVA: 0x001509D6 File Offset: 0x0014EBD6
	public static void update()
	{
		BenchmarkDist._instance.run();
	}

	// Token: 0x06002A88 RID: 10888 RVA: 0x001509E4 File Offset: 0x0014EBE4
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
		this.test_tiles.AddRange(World.world.tiles_list);
		this.test_tiles.ShuffleHalf<WorldTile>();
		this.test_tiles.RemoveRange(this.test_tiles.Count / 2, this.test_tiles.Count / 2);
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x00150A60 File Offset: 0x0014EC60
	public void run()
	{
		string tBenchmarkGroupId = this.benchmark_group_id;
		string tBenchmarkId = this.benchmark_id;
		List<WorldTile> tTiles = this.test_tiles;
		tTiles.Shuffle<WorldTile>();
		int2[] tTestVec2s = new int2[tTiles.Count];
		for (int i = 0; i < tTiles.Count; i++)
		{
			tTestVec2s[i] = new int2(tTiles[i].x, tTiles[i].y);
		}
		float2[] tTestVec2sFloat = new float2[tTiles.Count];
		for (int j = 0; j < tTiles.Count; j++)
		{
			tTestVec2sFloat[j] = new float2((float)tTiles[j].x, (float)tTiles[j].y);
		}
		NativeArray<int2> tTestVec2sNative = new NativeArray<int2>(tTestVec2s, Allocator.TempJob);
		NativeArray<float2> tTestVec2sFloatNative = new NativeArray<float2>(tTestVec2sFloat, Allocator.TempJob);
		WorldTile tTestTile = tTiles[0];
		Vector2Int tTestVec2 = tTestTile.pos;
		Vector3 tTestVec3 = tTestTile.posV3;
		int2 tTestVec2Int2 = new int2(tTestTile.x, tTestTile.y);
		float2 tTestVec2Float2 = new float2((float)tTestTile.x, (float)tTestTile.y);
		Bench.bench(tBenchmarkId, tBenchmarkGroupId, false);
		int tBest = -1;
		float tBestDistFloat = float.MaxValue;
		double tResult = 0.0;
		int tCountTotal = 0;
		Bench.bench("DistTile", tBenchmarkId, false);
		for (int k = 1; k < tTiles.Count; k++)
		{
			WorldTile tTile = tTiles[k];
			float tDist = Toolbox.DistTile(tTestTile, tTile);
			if (tDist < tBestDistFloat)
			{
				tBestDistFloat = tDist;
				tBest = k;
			}
			tResult += (double)tDist;
			tCountTotal++;
		}
		Bench.benchEnd("DistTile", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("DistVec2", tBenchmarkId, false);
		for (int l = 1; l < tTiles.Count; l++)
		{
			WorldTile tTile2 = tTiles[l];
			float tDist2 = Toolbox.DistVec2(tTestVec2, tTile2.pos);
			if (tDist2 < tBestDistFloat)
			{
				tBestDistFloat = tDist2;
				tBest = l;
			}
			tResult += (double)tDist2;
			tCountTotal++;
		}
		Bench.benchEnd("DistVec2", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("DistVec3", tBenchmarkId, false);
		for (int m = 1; m < tTiles.Count; m++)
		{
			WorldTile tTile3 = tTiles[m];
			float tDist3 = Toolbox.DistVec3(tTestVec3, tTile3.posV3);
			if (tDist3 < tBestDistFloat)
			{
				tBestDistFloat = tDist3;
				tBest = m;
			}
			tResult += (double)tDist3;
			tCountTotal++;
		}
		Bench.benchEnd("DistVec3", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("Dist", tBenchmarkId, false);
		for (int n = 1; n < tTiles.Count; n++)
		{
			WorldTile tTile4 = tTiles[n];
			float tDist4 = Toolbox.Dist(tTestTile.x, tTestTile.y, tTile4.x, tTile4.y);
			if (tDist4 < tBestDistFloat)
			{
				tBestDistFloat = tDist4;
				tBest = n;
			}
			tResult += (double)tDist4;
			tCountTotal++;
		}
		Bench.benchEnd("Dist", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("DistFloat", tBenchmarkId, false);
		for (int i2 = 1; i2 < tTiles.Count; i2++)
		{
			WorldTile tTile5 = tTiles[i2];
			float tDist5 = BenchmarkDist.DistFloat((float)tTestTile.x, (float)tTestTile.y, (float)tTile5.x, (float)tTile5.y);
			if (tDist5 < tBestDistFloat)
			{
				tBestDistFloat = tDist5;
				tBest = i2;
			}
			tResult += (double)tDist5;
			tCountTotal++;
		}
		Bench.benchEnd("DistFloat", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("Dist.pos", tBenchmarkId, false);
		for (int i3 = 1; i3 < tTiles.Count; i3++)
		{
			Vector2Int tTile6 = tTiles[i3].pos;
			float tDist6 = Toolbox.Dist(tTestVec2.x, tTestVec2.y, tTile6.x, tTile6.y);
			if (tDist6 < tBestDistFloat)
			{
				tBestDistFloat = tDist6;
				tBest = i3;
			}
			tResult += (double)tDist6;
			tCountTotal++;
		}
		Bench.benchEnd("Dist.pos", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		int tBestDist = int.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDistTile", tBenchmarkId, false);
		for (int i4 = 1; i4 < tTiles.Count; i4++)
		{
			WorldTile tTile7 = tTiles[i4];
			int tDist7 = Toolbox.SquaredDistTile(tTestTile, tTile7);
			if (tDist7 < tBestDist)
			{
				tBestDist = tDist7;
				tBest = i4;
			}
			tResult += (double)tDist7;
			tCountTotal++;
		}
		Bench.benchEnd("FastDistTile", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDist = int.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDist", tBenchmarkId, false);
		for (int i5 = 1; i5 < tTiles.Count; i5++)
		{
			WorldTile tTile8 = tTiles[i5];
			int tDist8 = Toolbox.SquaredDist(tTestTile.x, tTestTile.y, tTile8.x, tTile8.y);
			if (tDist8 < tBestDist)
			{
				tBestDist = tDist8;
				tBest = i5;
			}
			tResult += (double)tDist8;
			tCountTotal++;
		}
		Bench.benchEnd("FastDist", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDistFloat", tBenchmarkId, false);
		for (int i6 = 1; i6 < tTiles.Count; i6++)
		{
			WorldTile tTile9 = tTiles[i6];
			float tDist9 = BenchmarkDist.FastDistFloat((float)tTestTile.x, (float)tTestTile.y, (float)tTile9.x, (float)tTile9.y);
			if (tDist9 < tBestDistFloat)
			{
				tBestDistFloat = tDist9;
				tBest = i6;
			}
			tResult += (double)tDist9;
			tCountTotal++;
		}
		Bench.benchEnd("FastDistFloat", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDist = int.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDistVec2", tBenchmarkId, false);
		for (int i7 = 1; i7 < tTiles.Count; i7++)
		{
			WorldTile tTile10 = tTiles[i7];
			int tDist10 = Toolbox.SquaredDistVec2(tTestVec2, tTile10.pos);
			if (tDist10 < tBestDist)
			{
				tBestDist = tDist10;
				tBest = i7;
			}
			tResult += (double)tDist10;
			tCountTotal++;
		}
		Bench.benchEnd("FastDistVec2", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDistVec3", tBenchmarkId, false);
		for (int i8 = 1; i8 < tTiles.Count; i8++)
		{
			WorldTile tTile11 = tTiles[i8];
			float tDist11 = Toolbox.SquaredDistVec3(tTestVec3, tTile11.posV3);
			if (tDist11 < tBestDistFloat)
			{
				tBestDistFloat = tDist11;
				tBest = i8;
			}
			tResult += (double)tDist11;
			tCountTotal++;
		}
		Bench.benchEnd("FastDistVec3", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("FastDist.pos", tBenchmarkId, false);
		for (int i9 = 1; i9 < tTiles.Count; i9++)
		{
			Vector2Int tTile12 = tTiles[i9].pos;
			float tDist12 = (float)Toolbox.SquaredDist(tTestVec2.x, tTestVec2.y, tTile12.x, tTile12.y);
			if (tDist12 < tBestDistFloat)
			{
				tBestDistFloat = tDist12;
				tBest = i9;
			}
			tResult += (double)tDist12;
			tCountTotal++;
		}
		Bench.benchEnd("FastDist.pos", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("distancesq", tBenchmarkId, false);
		for (int i10 = 1; i10 < tTiles.Count; i10++)
		{
			WorldTile tTile13 = tTiles[i10];
			float tDist13 = math.distancesq((float)tTestTile.x, (float)tTile13.x) + math.distancesq((float)tTestTile.y, (float)tTile13.y);
			if (tDist13 < tBestDistFloat)
			{
				tBestDistFloat = tDist13;
				tBest = i10;
			}
			tResult += (double)tDist13;
			tCountTotal++;
		}
		Bench.benchEnd("distancesq", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("float2", tBenchmarkId, false);
		for (int i11 = 1; i11 < tTiles.Count; i11++)
		{
			WorldTile tTile14 = tTiles[i11];
			float2 tTile15 = new float2((float)tTile14.x, (float)tTile14.y);
			float tDist14 = math.distancesq(tTestVec2Float2, tTile15);
			if (tDist14 < tBestDistFloat)
			{
				tBestDistFloat = tDist14;
				tBest = i11;
			}
			tResult += (double)tDist14;
			tCountTotal++;
		}
		Bench.benchEnd("float2", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("int2", tBenchmarkId, false);
		for (int i12 = 1; i12 < tTiles.Count; i12++)
		{
			WorldTile tTile16 = tTiles[i12];
			int2 tTile17 = new int2(tTile16.x, tTile16.y);
			float tDist15 = math.distancesq(tTestVec2Int2, tTile17);
			if (tDist15 < tBestDistFloat)
			{
				tBestDistFloat = tDist15;
				tBest = i12;
			}
			tResult += (double)tDist15;
			tCountTotal++;
		}
		Bench.benchEnd("int2", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("int2array", tBenchmarkId, false);
		for (int i13 = 1; i13 < tTestVec2s.Length; i13++)
		{
			float tDist16 = math.distancesq(tTestVec2Int2, tTestVec2s[i13]);
			if (tDist16 < tBestDistFloat)
			{
				tBestDistFloat = tDist16;
				tBest = i13;
			}
			tResult += (double)tDist16;
			tCountTotal++;
		}
		Bench.benchEnd("int2array", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("nint2array", tBenchmarkId, false);
		for (int i14 = 1; i14 < tTestVec2sFloatNative.Length; i14++)
		{
			float tDist17 = math.distancesq(tTestVec2Int2, tTestVec2sFloatNative[i14]);
			if (tDist17 < tBestDistFloat)
			{
				tBestDistFloat = tDist17;
				tBest = i14;
			}
			tResult += (double)tDist17;
			tCountTotal++;
		}
		Bench.benchEnd("nint2array", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("float2array", tBenchmarkId, false);
		for (int i15 = 1; i15 < tTestVec2sFloat.Length; i15++)
		{
			float tDist18 = math.distancesq(tTestVec2Float2, tTestVec2sFloat[i15]);
			if (tDist18 < tBestDistFloat)
			{
				tBestDistFloat = tDist18;
				tBest = i15;
			}
			tResult += (double)tDist18;
			tCountTotal++;
		}
		Bench.benchEnd("float2array", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tBest = -1;
		tBestDistFloat = float.MaxValue;
		tResult = 0.0;
		tCountTotal = 0;
		Bench.bench("nfloat2array", tBenchmarkId, false);
		for (int i16 = 1; i16 < tTestVec2sNative.Length; i16++)
		{
			float tDist19 = math.distancesq(tTestVec2Float2, tTestVec2sNative[i16]);
			if (tDist19 < tBestDistFloat)
			{
				tBestDistFloat = tDist19;
				tBest = i16;
			}
			tResult += (double)tDist19;
			tCountTotal++;
		}
		Bench.benchEnd("nfloat2array", tBenchmarkId, true, (long)tTiles[tBest].tile_id, false);
		tTestVec2sNative.Dispose();
		tTestVec2sFloatNative.Dispose();
		Bench.benchEnd(tBenchmarkId, tBenchmarkGroupId, false, 0L, false);
		if (this.print_to_console)
		{
			Debug.Log("LAST:\n" + Bench.printableBenchResults(tBenchmarkId, false, new string[]
			{
				"DistTile",
				"DistVec2",
				"DistVec3",
				"Dist",
				"DistFloat",
				"Dist.pos",
				"FastDistTile",
				"FastDistVec2",
				"FastDistVec3",
				"FastDist",
				"FastDistFloat",
				"FastDist.pos",
				"int2",
				"int2array",
				"nint2array",
				"float2",
				"float2array",
				"nfloat2array",
				"distancesq",
				"job_new",
				"job_prefill",
				"pjob_prefill",
				"BurstDist",
				"BurstDistFloat",
				"BurstFastDistFloat",
				"BurstDist.pos",
				"BurstFastDist",
				"BurstFastDist.pos"
			}));
			Debug.Log("AVG:\n" + Bench.printableBenchResults(tBenchmarkId, true, new string[]
			{
				"DistTile",
				"DistVec2",
				"DistVec3",
				"Dist",
				"DistFloat",
				"Dist.pos",
				"FastDistTile",
				"FastDistVec2",
				"FastDistVec3",
				"FastDist",
				"FastDistFloat",
				"FastDist.pos",
				"int2",
				"int2array",
				"nint2array",
				"float2",
				"float2array",
				"nfloat2array",
				"distancesq",
				"job_new",
				"job_prefill",
				"pjob_prefill",
				"BurstDist",
				"BurstDistFloat",
				"BurstFastDistFloat",
				"BurstDist.pos",
				"BurstFastDist",
				"BurstFastDist.pos"
			}));
		}
		this.result = (long)tResult;
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x00151955 File Offset: 0x0014FB55
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistFloat(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x0015196B File Offset: 0x0014FB6B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float FastDistFloat(float x1, float y1, float x2, float y2)
	{
		return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
	}

	// Token: 0x04001F94 RID: 8084
	public long result;

	// Token: 0x04001F95 RID: 8085
	internal string benchmark_group_id;

	// Token: 0x04001F96 RID: 8086
	internal string benchmark_id;

	// Token: 0x04001F97 RID: 8087
	internal List<WorldTile> test_tiles;

	// Token: 0x04001F98 RID: 8088
	internal bool print_to_console;

	// Token: 0x04001F99 RID: 8089
	private static BenchmarkDist _instance;
}
