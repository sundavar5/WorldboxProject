using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
public static class RarityExtensions
{
	// Token: 0x06000AF1 RID: 2801 RVA: 0x000A087A File Offset: 0x0009EA7A
	public static string getRarityColorHex(this Rarity pRarity)
	{
		return pRarity.getAsset().color_hex;
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x000A0888 File Offset: 0x0009EA88
	public static RarityAsset getAsset(this Rarity pRarity)
	{
		string tID = pRarity.getStringID();
		return AssetManager.rarity_library.get(tID);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x000A08A7 File Offset: 0x0009EAA7
	public static Color getRarityColor(this Rarity pRarity)
	{
		return pRarity.getAsset().color_container.color;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x000A08B9 File Offset: 0x0009EAB9
	public static string getStringID(this Rarity pRarity)
	{
		return pRarity.ToString().ToLower();
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x000A08CD File Offset: 0x0009EACD
	public static int GetRate(this Rarity pRarity)
	{
		switch (pRarity)
		{
		case Rarity.R0_Normal:
			return 10;
		case Rarity.R1_Rare:
			return 6;
		case Rarity.R2_Epic:
			return 3;
		case Rarity.R3_Legendary:
			return 1;
		default:
			return 0;
		}
	}
}
