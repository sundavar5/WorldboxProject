using System;
using System.Runtime.CompilerServices;

// Token: 0x0200056C RID: 1388
public static class LongExtension
{
	// Token: 0x06002D38 RID: 11576 RVA: 0x00160C61 File Offset: 0x0015EE61
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool hasValue(this long pLong)
	{
		return pLong != -1L;
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x00160C6C File Offset: 0x0015EE6C
	public static long? toNullLong(this long pLong)
	{
		if (!pLong.hasValue())
		{
			return null;
		}
		return new long?(pLong);
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x00160C94 File Offset: 0x0015EE94
	public static long toLong(this long? pLong)
	{
		long? num = pLong;
		if (num == null)
		{
			return -1L;
		}
		return num.GetValueOrDefault();
	}

	// Token: 0x040022A3 RID: 8867
	public const long NULL = -1L;
}
