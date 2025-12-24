using System;
using System.Collections.Generic;

// Token: 0x0200046B RID: 1131
public class KingdomCheckCache
{
	// Token: 0x060026A4 RID: 9892 RVA: 0x0013A920 File Offset: 0x00138B20
	public long getHash(Kingdom pK1, Kingdom pK2)
	{
		int tHash = pK1.GetHashCode();
		int tHash2 = pK2.GetHashCode();
		long tResult;
		if (tHash > tHash2)
		{
			tResult = (long)(tHash * 1000000 + tHash2);
		}
		else
		{
			tResult = (long)(tHash2 * 1000000 + tHash);
		}
		return tResult;
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x0013A958 File Offset: 0x00138B58
	public void clear()
	{
		this.dict.Clear();
	}

	// Token: 0x04001D1A RID: 7450
	public Dictionary<long, bool> dict = new Dictionary<long, bool>();
}
