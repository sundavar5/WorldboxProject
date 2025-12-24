using System;
using System.Collections.Generic;

// Token: 0x02000279 RID: 633
public static class ItemCrafting
{
	// Token: 0x060017AB RID: 6059 RVA: 0x000E7BB8 File Offset: 0x000E5DB8
	public static bool tryToCraftRandomWeapon(Actor pActor, City pCity)
	{
		int tTries = pActor.asset.item_making_skill;
		if (pActor.hasCulture() && pActor.culture.hasTrait("weaponsmith_mastery"))
		{
			tTries += CultureTraitLibrary.getValue("weaponsmith_mastery");
		}
		return ItemCrafting.craftItem(pActor, pActor.getName(), EquipmentType.Weapon, tTries, pCity);
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000E7C08 File Offset: 0x000E5E08
	public static bool tryToCraftRandomArmor(Actor pActor, City pCity)
	{
		int tTries = pActor.asset.item_making_skill;
		if (pActor.hasCulture() && pActor.culture.hasTrait("armorsmith_mastery"))
		{
			tTries += CultureTraitLibrary.getValue("armorsmith_mastery");
		}
		EquipmentType tType = ItemCrafting.list_equipments.GetRandom<EquipmentType>();
		return ItemCrafting.craftItem(pActor, pActor.getName(), tType, tTries, pCity);
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x000E7C64 File Offset: 0x000E5E64
	public static bool tryToCraftRandomEquipment(Actor pActor, City pCity)
	{
		bool flag = ItemCrafting.tryToCraftRandomArmor(pActor, pCity);
		bool tNewWeapon = ItemCrafting.tryToCraftRandomWeapon(pActor, pCity);
		return flag || tNewWeapon;
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x000E7C84 File Offset: 0x000E5E84
	public static bool craftItem(Actor pActor, string pCreatorName, EquipmentType pType, int pTries, City pCity)
	{
		string tEquipmentSubtype = null;
		if (pType == EquipmentType.Weapon)
		{
			if (pActor.hasCulture())
			{
				tEquipmentSubtype = pActor.culture.getPreferredWeaponSubtypeIDs();
			}
			if (string.IsNullOrEmpty(tEquipmentSubtype))
			{
				tEquipmentSubtype = ItemLibrary.default_weapon_pool.GetRandom<string>();
			}
		}
		else
		{
			tEquipmentSubtype = AssetManager.items.getEquipmentType(pType);
		}
		EquipmentAsset tItemAssetToCraft = null;
		ActorEquipmentSlot tActorSlot = pActor.equipment.getSlot(pType);
		Item tCurrentItem = tActorSlot.getItem();
		if (tCurrentItem != null && tCurrentItem.isCursed())
		{
			return false;
		}
		int tCurrentItemValue = (tCurrentItem != null) ? tCurrentItem.asset.equipment_value : 0;
		if (pType == EquipmentType.Weapon && pActor.hasCulture() && pActor.culture.hasPreferredWeaponsToCraft() && Randy.randomBool())
		{
			tItemAssetToCraft = ItemCrafting.getItemAssetToCraft(pActor, pActor.culture.getPreferredWeaponAssets(), pCity, tCurrentItemValue, true);
		}
		if (tItemAssetToCraft == null)
		{
			List<EquipmentAsset> tItemsOfType = AssetManager.items.equipment_by_subtypes[tEquipmentSubtype];
			tItemAssetToCraft = ItemCrafting.getItemAssetToCraft(pActor, tItemsOfType, pCity, tCurrentItemValue, false);
		}
		if (tItemAssetToCraft == null)
		{
			return false;
		}
		Item tItem = World.world.items.generateItem(tItemAssetToCraft, pActor.kingdom, pCreatorName, pTries, pActor, 0, false);
		if (tActorSlot.isEmpty())
		{
			tActorSlot.setItem(tItem, pActor);
		}
		else
		{
			Item tOldItem = tActorSlot.getItem();
			tActorSlot.takeAwayItem();
			pCity.tryToPutItem(tOldItem);
			tActorSlot.setItem(tItem, pActor);
		}
		pActor.spendMoney(tItemAssetToCraft.get_total_cost);
		if (tItemAssetToCraft.cost_resource_id_1 != "none")
		{
			pCity.takeResource(tItemAssetToCraft.cost_resource_id_1, tItemAssetToCraft.cost_resource_1);
		}
		if (tItemAssetToCraft.cost_resource_id_2 != "none")
		{
			pCity.takeResource(tItemAssetToCraft.cost_resource_id_2, tItemAssetToCraft.cost_resource_2);
		}
		return true;
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x000E7E08 File Offset: 0x000E6008
	public static EquipmentAsset getItemAssetToCraft(Actor pActor, List<EquipmentAsset> pItemList, City pCity, int pCurrentItemValue, bool pShuffle = false)
	{
		if (pShuffle)
		{
			pItemList.Shuffle<EquipmentAsset>();
		}
		for (int i = pItemList.Count - 1; i >= 0; i--)
		{
			EquipmentAsset tItemAsset = pItemList[i];
			if (tItemAsset.equipment_value > pCurrentItemValue && ItemCrafting.hasEnoughResourcesToCraft(pActor, tItemAsset, pCity))
			{
				return tItemAsset;
			}
		}
		return null;
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000E7E50 File Offset: 0x000E6050
	private static bool hasEnoughResourcesToCraft(Actor pActor, EquipmentAsset pAsset, City pCity)
	{
		int tTotalCost = pAsset.get_total_cost;
		return pActor.hasEnoughMoney(tTotalCost) && (!(pAsset.cost_resource_id_1 != "none") || pAsset.cost_resource_1 <= pCity.getResourcesAmount(pAsset.cost_resource_id_1)) && (!(pAsset.cost_resource_id_2 != "none") || pAsset.cost_resource_2 <= pCity.getResourcesAmount(pAsset.cost_resource_id_2));
	}

	// Token: 0x0400132F RID: 4911
	private static readonly EquipmentType[] list_equipments = new EquipmentType[]
	{
		EquipmentType.Helmet,
		EquipmentType.Armor,
		EquipmentType.Boots,
		EquipmentType.Amulet,
		EquipmentType.Ring
	};
}
