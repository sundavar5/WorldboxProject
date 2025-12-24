using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000476 RID: 1142
public static class ParallelHelper
{
	// Token: 0x0600273C RID: 10044 RVA: 0x0013E498 File Offset: 0x0013C698
	public static int getDynamicBatchSize(int pCount)
	{
		if (pCount <= 32)
		{
			return 4;
		}
		if (pCount <= 64)
		{
			return 8;
		}
		if (pCount <= 128)
		{
			return 16;
		}
		if (pCount <= 256)
		{
			return 32;
		}
		if (pCount <= 512)
		{
			return 64;
		}
		if (pCount <= 2048)
		{
			return 128;
		}
		return 256;
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x0013E4E7 File Offset: 0x0013C6E7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int calcTotalBatches(int pItemsTotal, int pBatchSize)
	{
		return Mathf.CeilToInt((float)pItemsTotal / (float)pBatchSize);
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x0013E4F3 File Offset: 0x0013C6F3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int calculateBatchEnd(int pItemsIndexStart, int pBatchSize, int pItemsTotal)
	{
		return Mathf.Min(pItemsIndexStart + pBatchSize, pItemsTotal);
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x0013E4FE File Offset: 0x0013C6FE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int calculateBatchBeg(int pBatchIndex, int pBatchSize)
	{
		return pBatchIndex * pBatchSize;
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x0013E504 File Offset: 0x0013C704
	public static void moveDebugBatchSize()
	{
		int tCurBatchIndex = Array.IndexOf<int>(ParallelHelper._batch_sizes, ParallelHelper.DEBUG_BATCH_SIZE);
		if (tCurBatchIndex == -1)
		{
			tCurBatchIndex = 0;
		}
		tCurBatchIndex = (tCurBatchIndex + 1) % ParallelHelper._batch_sizes.Length;
		ParallelHelper.DEBUG_BATCH_SIZE = ParallelHelper._batch_sizes[tCurBatchIndex];
	}

	// Token: 0x04001D78 RID: 7544
	public static int DEBUG_BATCH_SIZE = 128;

	// Token: 0x04001D79 RID: 7545
	private const int MAX_BATCH_SIZE = 256;

	// Token: 0x04001D7A RID: 7546
	private static readonly int[] _batch_sizes = new int[]
	{
		4,
		8,
		16,
		32,
		64,
		128,
		256,
		512,
		1024,
		2048,
		4096
	};
}
