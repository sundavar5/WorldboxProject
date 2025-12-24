using System;
using Beebyte.Obfuscator;

// Token: 0x020000C5 RID: 197
[ObfuscateLiterals]
[Serializable]
public class ConstructionCost
{
	// Token: 0x0600061D RID: 1565 RVA: 0x0005E790 File Offset: 0x0005C990
	public ConstructionCost(int pWood = 0, int pStone = 0, int pCommonMetals = 0, int pGold = 0)
	{
		this.wood = pWood;
		this.stone = pStone;
		this.common_metals = pCommonMetals;
		this.gold = pGold;
	}

	// Token: 0x040006E4 RID: 1764
	public int wood;

	// Token: 0x040006E5 RID: 1765
	public int stone;

	// Token: 0x040006E6 RID: 1766
	public int common_metals;

	// Token: 0x040006E7 RID: 1767
	public int gold;
}
