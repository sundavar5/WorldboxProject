using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
public static class Epoch
{
	// Token: 0x06002629 RID: 9769 RVA: 0x00138780 File Offset: 0x00136980
	public static double Current()
	{
		return (DateTime.UtcNow - Epoch.epochStart).TotalSeconds;
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x001387A4 File Offset: 0x001369A4
	public static double SecondsElapsed(double t1)
	{
		return (double)Mathf.Abs((float)(Epoch.Current() - t1));
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x001387B4 File Offset: 0x001369B4
	public static int SecondsElapsed(int t1, int t2)
	{
		return Mathf.Abs(t1 - t2);
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x001387C0 File Offset: 0x001369C0
	internal static DateTime toDateTime(double epoch)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
	}

	// Token: 0x04001CDB RID: 7387
	private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
