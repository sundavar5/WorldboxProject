using System;

// Token: 0x0200011A RID: 282
public class ItemTools
{
	// Token: 0x060008AE RID: 2222 RVA: 0x0007D61D File Offset: 0x0007B81D
	public static void calcItemValues(Item pItem, BaseStats pStats)
	{
		pStats.clear();
		ItemTools.s_value = 0;
		ItemTools.s_quality = Rarity.R0_Normal;
		pItem.action_attack_target = null;
		ItemTools.mergeStatsWithItem(pStats, pItem, true, 1f);
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0007D648 File Offset: 0x0007B848
	public static void mergeStatsWithItem(BaseStats pStats, Item pItem, bool pCalcGlobalValue = true, float pMultiplier = 1f)
	{
		EquipmentAsset tItemAsset = pItem.getAsset();
		ItemTools.mergeStats(pStats, tItemAsset, pCalcGlobalValue, pMultiplier);
		pItem.action_attack_target = null;
		ItemTools.addAction(pItem, tItemAsset.action_attack_target);
		foreach (string ptr in pItem.data.modifiers)
		{
			string tModID = ptr;
			ItemModAsset tModAsset = AssetManager.items_modifiers.get(tModID);
			ItemTools.mergeStats(pStats, tModAsset, pCalcGlobalValue, pMultiplier);
			ItemTools.addAction(pItem, tModAsset.action_attack_target);
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0007D6E0 File Offset: 0x0007B8E0
	private static void addAction(Item pItem, AttackAction pAction)
	{
		if (pAction == null)
		{
			return;
		}
		if (pItem.action_attack_target == null)
		{
			pItem.action_attack_target = pAction;
			return;
		}
		pItem.action_attack_target = (AttackAction)Delegate.Combine(pItem.action_attack_target, pAction);
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0007D710 File Offset: 0x0007B910
	public static void mergeStats(BaseStats pStats, ItemAsset pAsset, bool pCalcGlobalValue = true, float pMultiplier = 1f)
	{
		if (pAsset == null)
		{
			return;
		}
		pStats.mergeStats(pAsset.base_stats, pMultiplier);
		if (pCalcGlobalValue)
		{
			if (pAsset.quality > ItemTools.s_quality)
			{
				ItemTools.s_quality = pAsset.quality;
			}
			ItemTools.s_value += pAsset.equipment_value + pAsset.mod_rank * 2;
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0007D764 File Offset: 0x0007B964
	public static void getTooltipTitle(EquipmentAsset pAsset, out string pName, out string pMaterial)
	{
		string tName = pAsset.getLocaleID().Localize();
		string tMat = "";
		if (!string.IsNullOrEmpty(pAsset.material) && pAsset.material != "basic")
		{
			tMat = tMat + "(" + LocalizedTextManager.getText(pAsset.getMaterialID(), null, false) + ") ";
		}
		pName = tName;
		pMaterial = tMat;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0007D7C8 File Offset: 0x0007B9C8
	public static void getAssetIds(string pItemIdWithMaterial, out string pItemAssetId, out string pMaterialAssetId)
	{
		pItemAssetId = null;
		pMaterialAssetId = null;
		int tIndex = pItemIdWithMaterial.LastIndexOf('_');
		if (tIndex == -1)
		{
			return;
		}
		pItemAssetId = pItemIdWithMaterial.Substring(0, tIndex);
		pMaterialAssetId = pItemIdWithMaterial.Substring(tIndex + 1);
	}

	// Token: 0x040008FB RID: 2299
	public static int s_value;

	// Token: 0x040008FC RID: 2300
	public static Rarity s_quality;
}
