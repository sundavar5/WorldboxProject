using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000502 RID: 1282
public static class Benchmark
{
	// Token: 0x06002A79 RID: 10873 RVA: 0x0014EAB8 File Offset: 0x0014CCB8
	public static void benchHashsetStart()
	{
		for (int i = 0; i < 1000; i++)
		{
			Benchmark.benchObjHashsetCreateVsAdd(5000);
		}
		Debug.Log("- BenchTest - list:" + Bench.getBenchResult("BenchTest - list", "main", true));
		Debug.Log("- BenchTest - hashset:" + Bench.getBenchResult("BenchTest - hashset", "main", true));
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x0014EB20 File Offset: 0x0014CD20
	public static void benchObjHashsetCreateVsAdd(int pAmount = 3000)
	{
		Bench.bench_enabled = true;
		List<BenchObject> tOriginalList = new List<BenchObject>();
		for (int i = 0; i < pAmount; i++)
		{
			tOriginalList.Add(new BenchObject());
		}
		int tTries = 10;
		List<BenchObject> tTest1List = new List<BenchObject>();
		tTest1List.AddRange(tOriginalList);
		Bench.bench("BenchTest - list", "main", false);
		for (int j = 0; j < tTries; j++)
		{
			BenchObject tObject = tTest1List.GetRandom<BenchObject>();
			tTest1List.Remove(tObject);
		}
		Bench.benchEnd("BenchTest - list", "main", false, 0L, false);
		HashSet<BenchObject> tSet = new HashSet<BenchObject>();
		tSet.UnionWith(tOriginalList);
		Bench.bench("BenchTest - hashset", "main", false);
		for (int k = 0; k < tTries; k++)
		{
			BenchObject tObject2 = tOriginalList.GetRandom<BenchObject>();
			tSet.Remove(tObject2);
		}
		tTest1List.Clear();
		tTest1List.AddRange(tSet);
		Bench.benchEnd("BenchTest - hashset", "main", false, 0L, false);
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x0014EC10 File Offset: 0x0014CE10
	public static void benchObjectsVsData(int pObjects)
	{
		double tTries = 1000.0;
		Bench.bench_enabled = true;
		int tAmount = pObjects;
		Debug.Log("----");
		Debug.Log("NEW BENCH - " + pObjects.ToString());
		BenchObject[] tObjects = new BenchObject[tAmount];
		for (int i = 0; i < tObjects.Length; i++)
		{
			tObjects[i] = new BenchObject();
		}
		Stopwatch stopwatch_objects = new Stopwatch();
		stopwatch_objects.Start();
		int iR = 0;
		while ((double)iR < tTries)
		{
			for (int j = 0; j < tObjects.Length; j++)
			{
				tObjects[j].update(0f);
			}
			iR++;
		}
		stopwatch_objects.Stop();
		tObjects = new BenchObject[tAmount];
		for (int k = 0; k < tObjects.Length; k++)
		{
			tObjects[k] = new BenchObject();
		}
		Stopwatch stopwatch_data = new Stopwatch();
		stopwatch_data.Start();
		int iR2 = 0;
		while ((double)iR2 < tTries)
		{
			for (int l = 0; l < tObjects.Length; l++)
			{
				tObjects[l].updateMove(0f);
				tObjects[l].updateMove(0f);
				tObjects[l].updateMove(0f);
				tObjects[l].updateMove(0f);
				tObjects[l].updateMove(0f);
			}
			iR2++;
		}
		stopwatch_data.Stop();
		tObjects = new BenchObject[tAmount];
		for (int m = 0; m < tObjects.Length; m++)
		{
			tObjects[m] = new BenchObject();
		}
		Stopwatch stopwatch_data_optimized = new Stopwatch();
		stopwatch_data_optimized.Start();
		int iR3 = 0;
		while ((double)iR3 < tTries)
		{
			foreach (BenchObject benchObject in tObjects)
			{
				benchObject.updateMove(0f);
				benchObject.updateMove(0f);
				benchObject.updateMove(0f);
				benchObject.updateMove(0f);
				benchObject.updateMove(0f);
			}
			iR3++;
		}
		stopwatch_data_optimized.Stop();
		tObjects = new BenchObject[tAmount];
		for (int i2 = 0; i2 < tObjects.Length; i2++)
		{
			tObjects[i2] = new BenchObject();
		}
		Stopwatch stopwatch_parallel = new Stopwatch();
		stopwatch_parallel.Start();
		int iR4 = 0;
		while ((double)iR4 < tTries)
		{
			Parallel.ForEach<BenchObject>(tObjects, World.world.parallel_options, delegate(BenchObject pObject)
			{
				pObject.updateMove(0f);
				pObject.updateMove(0f);
				pObject.updateMove(0f);
				pObject.updateMove(0f);
				pObject.updateMove(0f);
			});
			iR4++;
		}
		stopwatch_parallel.Stop();
		tObjects = new BenchObject[tAmount];
		for (int i3 = 0; i3 < tObjects.Length; i3++)
		{
			tObjects[i3] = new BenchObject();
		}
		Stopwatch stopwatch_data_index = new Stopwatch();
		stopwatch_data_index.Start();
		int iR5 = 0;
		while ((double)iR5 < tTries)
		{
			for (int i4 = 0; i4 < tObjects.Length; i4++)
			{
				tObjects[i4].derp += 22;
				if (tObjects[i4].derp == 1000)
				{
					tObjects[i4].derp += 10;
					if (tObjects[i4].derp < 10)
					{
						tObjects[i4].derp += 5;
					}
					else
					{
						tObjects[i4].derp -= 5;
					}
				}
			}
			for (int i5 = 0; i5 < tObjects.Length; i5++)
			{
				tObjects[i5].derp += 22;
				if (tObjects[i5].derp == 1000)
				{
					tObjects[i5].derp += 10;
					if (tObjects[i5].derp < 10)
					{
						tObjects[i5].derp += 5;
					}
					else
					{
						tObjects[i5].derp -= 5;
					}
				}
			}
			for (int i6 = 0; i6 < tObjects.Length; i6++)
			{
				tObjects[i6].derp += 22;
				if (tObjects[i6].derp == 1000)
				{
					tObjects[i6].derp += 10;
					if (tObjects[i6].derp < 10)
					{
						tObjects[i6].derp += 5;
					}
					else
					{
						tObjects[i6].derp -= 5;
					}
				}
			}
			for (int i7 = 0; i7 < tObjects.Length; i7++)
			{
				tObjects[i7].derp += 22;
				if (tObjects[i7].derp == 1000)
				{
					tObjects[i7].derp += 10;
					if (tObjects[i7].derp < 10)
					{
						tObjects[i7].derp += 5;
					}
					else
					{
						tObjects[i7].derp -= 5;
					}
				}
			}
			for (int i8 = 0; i8 < tObjects.Length; i8++)
			{
				tObjects[i8].derp += 22;
				if (tObjects[i8].derp == 1000)
				{
					tObjects[i8].derp += 10;
					if (tObjects[i8].derp < 10)
					{
						tObjects[i8].derp += 5;
					}
					else
					{
						tObjects[i8].derp -= 5;
					}
				}
			}
			iR5++;
		}
		stopwatch_data_index.Stop();
		tObjects = new BenchObject[tAmount];
		for (int i9 = 0; i9 < tObjects.Length; i9++)
		{
			tObjects[i9] = new BenchObject();
		}
		Stopwatch stopwatch_data_temp = new Stopwatch();
		stopwatch_data_temp.Start();
		int iR6 = 0;
		while ((double)iR6 < tTries)
		{
			foreach (BenchObject tObject in tObjects)
			{
				tObject.derp += 22;
				if (tObject.derp == 1000)
				{
					tObject.derp += 10;
					if (tObject.derp < 10)
					{
						tObject.derp += 5;
					}
					else
					{
						tObject.derp -= 5;
					}
				}
			}
			foreach (BenchObject tObject2 in tObjects)
			{
				tObject2.derp += 22;
				if (tObject2.derp == 1000)
				{
					tObject2.derp += 10;
					if (tObject2.derp < 10)
					{
						tObject2.derp += 5;
					}
					else
					{
						tObject2.derp -= 5;
					}
				}
			}
			foreach (BenchObject tObject3 in tObjects)
			{
				tObject3.derp += 22;
				if (tObject3.derp == 1000)
				{
					tObject3.derp += 10;
					if (tObject3.derp < 10)
					{
						tObject3.derp += 5;
					}
					else
					{
						tObject3.derp -= 5;
					}
				}
			}
			foreach (BenchObject tObject4 in tObjects)
			{
				tObject4.derp += 22;
				if (tObject4.derp == 1000)
				{
					tObject4.derp += 10;
					if (tObject4.derp < 10)
					{
						tObject4.derp += 5;
					}
					else
					{
						tObject4.derp -= 5;
					}
				}
			}
			foreach (BenchObject tObject5 in tObjects)
			{
				tObject5.derp += 22;
				if (tObject5.derp == 1000)
				{
					tObject5.derp += 10;
					if (tObject5.derp < 10)
					{
						tObject5.derp += 5;
					}
					else
					{
						tObject5.derp -= 5;
					}
				}
			}
			iR6++;
		}
		stopwatch_data_temp.Stop();
		Debug.Log("bench_objects " + ((double)stopwatch_objects.ElapsedTicks / tTries).ToString() + " 100%");
		Debug.Log("bench_data " + Benchmark.getResult(stopwatch_objects, stopwatch_data, tTries));
		Debug.Log("bench_data_index " + Benchmark.getResult(stopwatch_objects, stopwatch_data_index, tTries));
		Debug.Log("bench_data_temp " + Benchmark.getResult(stopwatch_objects, stopwatch_data_temp, tTries));
		Debug.Log("stopwatch_parallel " + Benchmark.getResult(stopwatch_objects, stopwatch_parallel, tTries));
		Debug.Log("stopwatch_data_optimized " + Benchmark.getResult(stopwatch_objects, stopwatch_data_optimized, tTries));
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x0014F424 File Offset: 0x0014D624
	private static string getResult(Stopwatch p1, Stopwatch p2, double pTries)
	{
		double num = (double)p1.ElapsedTicks / pTries;
		double tAv2 = (double)p2.ElapsedTicks / pTries;
		double tResult = num / tAv2 * 100.0 - 100.0;
		return tAv2.ToString() + ", " + tResult.ToString() + "%";
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x0014F478 File Offset: 0x0014D678
	public static void benchNativeECSAndOOP()
	{
		Bench.bench_enabled = true;
		int tAmount = 200000;
		NativeArray<Vector3> tNativVec = new NativeArray<Vector3>(tAmount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
		NativeArray<int> tNativX = new NativeArray<int>(tAmount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
		NativeArray<int> tNativY = new NativeArray<int>(tAmount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
		NativeArray<int> tNativHealth = new NativeArray<int>(tAmount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
		ActorData[] tNormaArray = new ActorData[tAmount];
		for (int i = 0; i < tAmount; i++)
		{
			tNormaArray[i] = new ActorData();
		}
		Bench.bench("test_native_vectors", "main", false);
		for (int j = 0; j < tAmount; j++)
		{
			Vector3 tVec = tNativVec[j];
			tVec.x = (float)j;
			tVec.y = (float)j;
			tNativVec[j] = tVec;
		}
		for (int k = 0; k < tAmount; k++)
		{
			tNativHealth[k] = k;
		}
		Bench.benchEnd("test_native_vectors", "main", false, 0L, false);
		Bench.bench("test_native_x_y", "main", false);
		for (int l = 0; l < tAmount; l++)
		{
			tNativX[l] = l;
			tNativY[l] = l;
		}
		for (int m = 0; m < tAmount; m++)
		{
			tNativHealth[m] = m;
		}
		Bench.benchEnd("test_native_x_y", "main", false, 0L, false);
		Bench.bench("test_normal_temp_var", "main", false);
		for (int n = 0; n < tAmount; n++)
		{
			ActorData actorData = tNormaArray[n];
			actorData.x = n;
			actorData.y = n;
		}
		for (int i2 = 0; i2 < tAmount; i2++)
		{
			tNormaArray[i2].health = i2;
		}
		Bench.benchEnd("test_normal_temp_var", "main", false, 0L, false);
		Bench.bench("test_normal_direct", "main", false);
		for (int i3 = 0; i3 < tAmount; i3++)
		{
			tNormaArray[i3].x = i3;
			tNormaArray[i3].y = i3;
		}
		for (int i4 = 0; i4 < tAmount; i4++)
		{
			tNormaArray[i4].health = i4;
		}
		Bench.benchEnd("test_normal_direct", "main", false, 0L, false);
		Debug.Log("-  - - - - - - ");
		Debug.Log("- BenchTest - test_native_vectors: " + Bench.getBenchResult("test_native_vectors", "main", false));
		Debug.Log("- BenchTest - test_native_x_y: " + Bench.getBenchResult("test_native_x_y", "main", false));
		Debug.Log("- BenchTest - test_normal_temp_var: " + Bench.getBenchResult("test_normal_temp_var", "main", false));
		Debug.Log("- BenchTest - test_normal_direct: " + Bench.getBenchResult("test_normal_direct", "main", false));
		Debug.Log("- BenchTest - test_job_native_vectors: " + Bench.getBenchResult("test_job_native_vectors", "main", false));
		Debug.Log("- BenchTest - test_job_native_xy: " + Bench.getBenchResult("test_job_native_xy", "main", false));
		tNativVec.Dispose();
		tNativX.Dispose();
		tNativY.Dispose();
		tNativHealth.Dispose();
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x0014F769 File Offset: 0x0014D969
	public static void benchReferenceVsDict()
	{
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x0014F76C File Offset: 0x0014D96C
	public static void testVirtual()
	{
		int tTries = 1000;
		BenchTest1 tTest = new BenchTest1();
		BenchTest2 tTest2 = new BenchTest2();
		Bench.bench("BenchTest - normal", "main", false);
		for (int i = 0; i < tTries; i++)
		{
			tTest.test();
		}
		Bench.benchEnd("BenchTest - normal", "main", false, 0L, false);
		Bench.bench("BenchTest - virtual", "main", false);
		for (int j = 0; j < tTries; j++)
		{
			tTest2.testVirtual();
		}
		Bench.benchEnd("BenchTest - virtual", "main", false, 0L, false);
		Debug.Log("Benchmark:");
		Debug.Log("- BenchTest - normal:" + Bench.getBenchResult("BenchTest - normal", "main", true));
		Debug.Log("- BenchTest - virtual:" + Bench.getBenchResult("BenchTest - virtual", "main", true));
	}

	// Token: 0x06002A80 RID: 10880 RVA: 0x0014F848 File Offset: 0x0014DA48
	public static void testQueue()
	{
		int tElements = 10000;
		List<TileType> tList = new List<TileType>();
		Queue<TileType> tQueue = new Queue<TileType>();
		LinkedList<TileType> tLinked = new LinkedList<TileType>();
		for (int i = 0; i < tElements; i++)
		{
			tList.Add(new TileType());
			tQueue.Enqueue(new TileType());
			tLinked.AddLast(new TileType());
		}
		Bench.bench("list", "main", false);
		for (int j = 0; j < tList.Count; j++)
		{
			TileType tileType = tList[0];
			tList.RemoveAt(0);
		}
		Bench.benchEnd("list", "main", false, 0L, false);
		Bench.bench("queue", "main", false);
		for (int k = 0; k < tQueue.Count; k++)
		{
			tQueue.Dequeue();
		}
		Bench.benchEnd("queue", "main", false, 0L, false);
		Bench.bench("linked", "main", false);
		for (int l = 0; l < tLinked.Count; l++)
		{
			LinkedListNode<TileType> first = tLinked.First;
			tLinked.RemoveFirst();
		}
		Bench.benchEnd("linked", "main", false, 0L, false);
		Debug.Log("!!!BENCH REMOVE AT 0 " + tElements.ToString());
		Bench.printBenchResult("list", "main", false);
		Bench.printBenchResult("queue", "main", false);
		Bench.printBenchResult("linked", "main", false);
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x0014F9B8 File Offset: 0x0014DBB8
	public static void testRemoveStructs()
	{
		int tTries = 100;
		int tObjects = 500;
		List<Vector3> tListObjects = new List<Vector3>();
		List<Vector3> tListToRemove = new List<Vector3>();
		List<Vector3> tList = new List<Vector3>();
		List<Vector3> tList2 = new List<Vector3>();
		HashSet<Vector3> tHash = new HashSet<Vector3>();
		for (int i = 0; i < tObjects; i++)
		{
			tListObjects.Add(new Vector3
			{
				x = (float)Randy.randomInt(0, 1000),
				y = (float)Randy.randomInt(0, 1000),
				z = (float)Randy.randomInt(0, 1000)
			});
		}
		tListObjects.Shuffle<Vector3>();
		for (int j = 0; j < tTries; j++)
		{
			tListToRemove.Add(tListObjects.GetRandom<Vector3>());
		}
		Bench.bench("remove", "main", false);
		foreach (Vector3 tVec in tListObjects)
		{
			tList.Add(tVec);
		}
		for (int k = 0; k < tTries; k++)
		{
			tList.Remove(tListToRemove[k]);
		}
		Bench.benchEnd("remove", "main", false, 0L, false);
		Bench.bench("RemoveAtSwapBack", "main", false);
		foreach (Vector3 tVec2 in tListObjects)
		{
			tList2.Add(tVec2);
		}
		for (int l = 0; l < tTries; l++)
		{
			tList2.RemoveAtSwapBack(tListToRemove[l]);
		}
		Bench.benchEnd("RemoveAtSwapBack", "main", false, 0L, false);
		Bench.benchEnd("remove_native", "main", false, 0L, false);
		Bench.bench("remove_hashset", "main", false);
		foreach (Vector3 tVec3 in tListObjects)
		{
			tHash.Add(tVec3);
		}
		for (int m = 0; m < tTries; m++)
		{
			tHash.Remove(tListToRemove[m]);
		}
		Bench.benchEnd("remove_hashset", "main", false, 0L, false);
		Debug.Log("Benchmark:");
		Debug.Log("- built-in remove:" + Bench.getBenchResult("remove", "main", true));
		Debug.Log("- own RemoveAtSwapBack: " + Bench.getBenchResult("RemoveAtSwapBack", "main", true));
		Debug.Log("- native RemoveAtSwapBack: " + Bench.getBenchResult("remove_native", "main", true));
		Debug.Log("- remove hashset: " + Bench.getBenchResult("remove_hashset", "main", true));
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x0014FC9C File Offset: 0x0014DE9C
	public static void testCapacity()
	{
		int tTicks = 100;
		int tValues = 100000;
		Bench.bench("new_list", "main", false);
		List<List<int>> tAllLists = new List<List<int>>(tTicks);
		for (int i = 0; i < tTicks; i++)
		{
			List<int> tList = new List<int>();
			tAllLists.Add(tList);
			for (int jj = 0; jj < tValues; jj++)
			{
				tList.Add(jj);
			}
		}
		Bench.benchEnd("new_list", "main", false, 0L, false);
		Bench.bench("new_list_reused", "main", false);
		for (int j = 0; j < tAllLists.Count; j++)
		{
			List<int> tList2 = tAllLists[j];
			tList2.Clear();
			for (int jj2 = 0; jj2 < tValues; jj2++)
			{
				tList2.Add(jj2);
			}
		}
		Bench.benchEnd("new_list_reused", "main", false, 0L, false);
		Bench.bench("new_list_set_capacity", "main", false);
		tAllLists = new List<List<int>>(tTicks);
		for (int k = 0; k < tTicks; k++)
		{
			List<int> tList3 = new List<int>(tValues);
			tAllLists.Add(tList3);
			for (int jj3 = 0; jj3 < tValues; jj3++)
			{
				tList3.Add(jj3);
			}
		}
		Bench.benchEnd("new_list_set_capacity", "main", false, 0L, false);
		Bench.bench("new_list_set_capacity_reused", "main", false);
		for (int l = 0; l < tAllLists.Count; l++)
		{
			List<int> tList4 = tAllLists[l];
			tList4.Clear();
			for (int jj4 = 0; jj4 < tValues; jj4++)
			{
				tList4.Add(jj4);
			}
		}
		Bench.benchEnd("new_list_set_capacity_reused", "main", false, 0L, false);
		Bench.printBenchResult("new_list", "main", false);
		Bench.printBenchResult("new_list_set_capacity", "main", false);
		Bench.printBenchResult("new_list_reused", "main", false);
		Bench.printBenchResult("new_list_set_capacity_reused", "main", false);
	}
}
