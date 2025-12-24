using System;
using System.Collections.Generic;

// Token: 0x020001D5 RID: 469
public static class LoyaltyCalculator
{
	// Token: 0x06000DC3 RID: 3523 RVA: 0x000BE058 File Offset: 0x000BC258
	public static int calculate(City pCity)
	{
		LoyaltyCalculator.clear();
		foreach (LoyaltyAsset tAsset in AssetManager.loyalty_library.list)
		{
			int tResult = tAsset.calc(pCity);
			LoyaltyCalculator.total += tResult;
			if (tResult != 0)
			{
				LoyaltyCalculator.results.Add(tAsset, tResult);
			}
		}
		return LoyaltyCalculator.total;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x000BE0DC File Offset: 0x000BC2DC
	private static void clear()
	{
		LoyaltyCalculator.total = 0;
		LoyaltyCalculator.results.Clear();
	}

	// Token: 0x04000E1B RID: 3611
	public static Dictionary<LoyaltyAsset, int> results = new Dictionary<LoyaltyAsset, int>();

	// Token: 0x04000E1C RID: 3612
	public static int total = 0;
}
