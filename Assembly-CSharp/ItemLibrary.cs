using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x02000116 RID: 278
[ObfuscateLiterals]
public class ItemLibrary : ItemAssetLibrary<EquipmentAsset>
{
	// Token: 0x06000889 RID: 2185 RVA: 0x00075B0F File Offset: 0x00073D0F
	public override void init()
	{
		base.init();
		this.initTemplates();
		this.initNormalEquipment();
		this.initNormalWeapons();
		this.initWeaponsUnique();
		this.initBoats();
		this.initBaseAttacks();
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00075B3C File Offset: 0x00073D3C
	public override void post_init()
	{
		foreach (EquipmentAsset tEquipmentAsset in this.list)
		{
			if (tEquipmentAsset.is_pool_weapon)
			{
				tEquipmentAsset.path_gameplay_sprite = "items/weapons/w_" + tEquipmentAsset.id;
			}
			if (string.IsNullOrEmpty(tEquipmentAsset.path_icon))
			{
				tEquipmentAsset.path_icon = "ui/Icons/items/icon_" + tEquipmentAsset.id;
				int tResourcesGoldCostResources = 0;
				if (tEquipmentAsset.cost_resource_id_1 != "none")
				{
					ResourceAsset tResource = AssetManager.resources.get(tEquipmentAsset.cost_resource_id_1);
					tResourcesGoldCostResources += tResource.money_cost;
				}
				if (tEquipmentAsset.cost_resource_id_2 != "none")
				{
					ResourceAsset tResource2 = AssetManager.resources.get(tEquipmentAsset.cost_resource_id_2);
					tResourcesGoldCostResources += tResource2.money_cost;
				}
				tEquipmentAsset.cost_coins_resources = tResourcesGoldCostResources;
			}
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00075C34 File Offset: 0x00073E34
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (EquipmentAsset tAsset in this.list)
		{
			if (tAsset.item_modifier_ids != null)
			{
				tAsset.item_modifiers = new ItemModAsset[tAsset.item_modifier_ids.Length];
				for (int i = 0; i < tAsset.item_modifier_ids.Length; i++)
				{
					string tModID = tAsset.item_modifier_ids[i];
					ItemModAsset tModData = AssetManager.items_modifiers.get(tModID);
					if (tModData == null)
					{
						BaseAssetLibrary.logAssetError("ItemLibrary: Item Modifier Asset <e>not found</e>", tModID);
					}
					else
					{
						tAsset.item_modifiers[i] = tModData;
					}
				}
			}
		}
		this.fillSubtypesAndGroups();
		this.fillUnlockedPools();
		foreach (EquipmentAsset equipmentAsset in this.list)
		{
			equipmentAsset.linkSpells();
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00075D30 File Offset: 0x00073F30
	public override EquipmentAsset add(EquipmentAsset pAsset)
	{
		EquipmentAsset tNewAsset = base.add(pAsset);
		if (tNewAsset.base_stats == null)
		{
			tNewAsset.base_stats = new BaseStats();
		}
		return tNewAsset;
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00075D5C File Offset: 0x00073F5C
	public string getEquipmentType(EquipmentType pType)
	{
		switch (pType)
		{
		case EquipmentType.Weapon:
			return "weapon";
		case EquipmentType.Helmet:
			return "helmet";
		case EquipmentType.Armor:
			return "armor";
		case EquipmentType.Boots:
			return "boots";
		case EquipmentType.Ring:
			return "ring";
		case EquipmentType.Amulet:
			return "amulet";
		default:
			return null;
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00075DB0 File Offset: 0x00073FB0
	private void fillSubtypesAndGroups()
	{
		foreach (EquipmentAsset tAsset in this.list)
		{
			if (!this.equipment_by_subtypes.ContainsKey(tAsset.equipment_subtype))
			{
				this.equipment_by_subtypes.Add(tAsset.equipment_subtype, new List<EquipmentAsset>());
			}
			this.equipment_by_subtypes[tAsset.equipment_subtype].Add(tAsset);
			if (tAsset.is_pool_weapon)
			{
				this.pot_weapon_assets_all.Add(tAsset);
			}
			if (!tAsset.is_pool_weapon)
			{
				string tGroupId = tAsset.group_id;
				if (!this.pot_equipment_by_groups_all.ContainsKey(tGroupId))
				{
					this.pot_equipment_by_groups_all.Add(tGroupId, new List<EquipmentAsset>());
				}
				this.pot_equipment_by_groups_all[tGroupId].Add(tAsset);
			}
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00075E98 File Offset: 0x00074098
	private void fillUnlockedPools()
	{
		foreach (string tItem in GameProgress.instance.data.unlocked_equipment)
		{
			EquipmentAsset tAsset = this.get(tItem);
			if (tAsset != null)
			{
				if (tAsset.is_pool_weapon && !this.pot_weapon_assets_unlocked.Contains(tAsset))
				{
					this.pot_weapon_assets_unlocked.Add(tAsset);
				}
				if (!tAsset.is_pool_weapon)
				{
					string tGroupId = tAsset.group_id;
					if (!this.pot_equipment_by_groups_unlocked.ContainsKey(tGroupId))
					{
						this.pot_equipment_by_groups_unlocked.Add(tGroupId, new List<EquipmentAsset>());
					}
					List<EquipmentAsset> tList = this.pot_equipment_by_groups_unlocked[tGroupId];
					if (!tList.Contains(tAsset))
					{
						tList.Add(tAsset);
					}
				}
			}
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00075F70 File Offset: 0x00074170
	public string addToGameplayReport(string pWhat)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhat + "\n";
		foreach (EquipmentAsset tAsset in this.list)
		{
			if (tAsset.has_locales && !tAsset.isTemplateAsset())
			{
				string tName = tAsset.getTranslatedName();
				string tDescription = tAsset.getTranslatedDescription();
				string tLineInfo = "\n" + tName;
				tLineInfo += "\n";
				if (!string.IsNullOrEmpty(tDescription))
				{
					tLineInfo = tLineInfo + "1: " + tDescription;
				}
				tResult += tLineInfo;
			}
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00076038 File Offset: 0x00074238
	public void loadSprites()
	{
		foreach (EquipmentAsset tAsset in this.list)
		{
			if (tAsset.is_pool_weapon)
			{
				tAsset.gameplay_sprites = SpriteTextureLoader.getSpriteList(tAsset.path_gameplay_sprite, false);
				if (tAsset.gameplay_sprites.Length == 0)
				{
					Debug.LogError("Weapon Texture is Missing: " + tAsset.path_gameplay_sprite);
				}
			}
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x000760BC File Offset: 0x000742BC
	private void initBaseAttacks()
	{
		ItemLibrary.base_attack = this.clone("base_attack", "$melee");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_base";
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("hands", "$melee");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("fire_hands", "hands");
		this.t.has_locales = false;
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"flame"
		});
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("jaws", "$melee");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_jaws";
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("claws", "$melee");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_claws";
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("snowball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.base_stats["range"] = 6f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "snowball";
		this.t.base_stats["projectiles"] = 1f;
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"ice"
		});
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("bite", "$melee");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_jaws";
		this.t.attack_type = WeaponType.Melee;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("rocks", "$range");
		this.t.has_locales = false;
		this.t.projectile = "rock";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.base_stats["range"] = 15f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["projectiles"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00076420 File Offset: 0x00074620
	private void initBoats()
	{
		this.clone("boat_cannonball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_cannonball";
		this.t.base_stats["damage"] = 50f;
		this.t.base_stats["range"] = 14f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 3f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "cannonball";
		this.t.base_stats["projectiles"] = 1f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_arrow", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_bow";
		this.t.base_stats["damage"] = 30f;
		this.t.base_stats["range"] = 9f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 0f;
		this.t.base_stats["accuracy"] = 4f;
		this.t.base_stats["critical_chance"] = 0.2f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["attack_speed"] = 0.5f;
		this.t.projectile = "arrow";
		this.t.base_stats["projectiles"] = 5f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_snowball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_snowball";
		this.t.base_stats["damage"] = 50f;
		this.t.base_stats["range"] = 14f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 3f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "snowball";
		this.t.base_stats["projectiles"] = 1f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_plasma_ball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_plasma_ball";
		this.t.base_stats["damage"] = 65f;
		this.t.base_stats["range"] = 20f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 2f;
		this.t.base_stats["critical_chance"] = 0.2f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "plasma_ball";
		this.t.base_stats["projectiles"] = 1f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_necro_ball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_necro_ball";
		this.t.base_stats["damage"] = 45f;
		this.t.base_stats["range"] = 12f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 3f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "skull";
		this.t.base_stats["projectiles"] = 3f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_fireball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_cannonball";
		this.t.base_stats["damage"] = 30f;
		this.t.base_stats["range"] = 12f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 2f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "fireball";
		this.t.base_stats["projectiles"] = 1f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_freeze_ball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_snowball";
		this.t.base_stats["damage"] = 30f;
		this.t.base_stats["range"] = 12f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 2f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "freeze_orb";
		this.t.base_stats["projectiles"] = 3f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
		this.clone("boat_acid_ball", "$range");
		this.t.has_locales = false;
		this.t.path_slash_animation = "effects/slashes/slash_acid_ball";
		this.t.base_stats["damage"] = 50f;
		this.t.base_stats["range"] = 14f;
		this.t.base_stats["targets"] = 4f;
		this.t.base_stats["area_of_effect"] = 4f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.projectile = "acid_ball";
		this.t.base_stats["projectiles"] = 5f;
		this.t.show_in_meta_editor = false;
		this.t.show_in_knowledge_window = false;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00076CB9 File Offset: 0x00074EB9
	private void initNormalEquipment()
	{
		this.initArmors();
		this.initBoots();
		this.initHelmets();
		this.initRings();
		this.initAmulets();
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00076CDC File Offset: 0x00074EDC
	private void initAmulets()
	{
		this.clone("amulet_bone", "$amulet");
		this.t.material = "bone";
		this.t.equipment_value = 5;
		this.t.setCost(0, "bones", 1, "gems", 1);
		this.t.rigidity_rating = 1;
		this.t.base_stats["critical_chance"] = 0.02f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_copper", "$amulet");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "gems", 1);
		this.t.rigidity_rating = 2;
		this.t.base_stats["critical_chance"] = 0.03f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_bronze", "$amulet");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "gems", 1);
		this.t.rigidity_rating = 3;
		this.t.base_stats["critical_chance"] = 0.04f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_silver", "$amulet");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.setCost(0, "silver", 1, "gems", 1);
		this.t.rigidity_rating = 2;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 10f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_iron", "$amulet");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 2, "gems", 1);
		this.t.rigidity_rating = 4;
		this.t.base_stats["critical_chance"] = 0.06f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_steel", "$amulet");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 3, "gems", 1);
		this.t.rigidity_rating = 5;
		this.t.base_stats["critical_chance"] = 0.07f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_mythril", "$amulet");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.setCost(0, "mythril", 1, "gems", 1);
		this.t.rigidity_rating = 6;
		this.t.base_stats["critical_chance"] = 0.08f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("amulet_adamantine", "$amulet");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.setCost(0, "adamantine", 1, "gems", 1);
		this.t.rigidity_rating = 7;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["mana"] = 20f;
		this.t.base_stats["stamina"] = 5f;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0007719C File Offset: 0x0007539C
	private void initHelmets()
	{
		this.clone("helmet_leather", "$helmet");
		this.t.material = "leather";
		this.t.equipment_value = 5;
		this.t.setCost(0, "leather", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["armor"] = 2f;
		this.t.base_stats["stamina"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.clone("helmet_copper", "$helmet");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 3f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("helmet_bronze", "$helmet");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["armor"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("helmet_silver", "$helmet");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("helmet_iron", "$helmet");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["armor"] = 6f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("helmet_steel", "$helmet");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["armor"] = 7f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("helmet_mythril", "$helmet");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["armor"] = 8f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("helmet_adamantine", "$helmet");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x000776C8 File Offset: 0x000758C8
	private void initArmors()
	{
		this.clone("armor_leather", "$armor");
		this.t.material = "leather";
		this.t.equipment_value = 5;
		this.t.setCost(0, "leather", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["armor"] = 2f;
		this.t.base_stats["stamina"] = 20f;
		this.t.base_stats["speed"] = 1f;
		this.clone("armor_copper", "$armor");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 3f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("armor_bronze", "$armor");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["armor"] = 4f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("armor_silver", "$armor");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("armor_iron", "$armor");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["armor"] = 6f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("armor_steel", "$armor");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["armor"] = 7f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("armor_mythril", "$armor");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["armor"] = 8f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("armor_adamantine", "$armor");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 5f;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00077BF4 File Offset: 0x00075DF4
	private void initBoots()
	{
		this.clone("boots_leather", "$boots");
		this.t.material = "leather";
		this.t.equipment_value = 5;
		this.t.setCost(0, "leather", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["armor"] = 2f;
		this.t.base_stats["stamina"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.clone("boots_copper", "$boots");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 3f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("boots_bronze", "$boots");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["armor"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("boots_silver", "$boots");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["armor"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("boots_iron", "$boots");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["armor"] = 6f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("boots_steel", "$boots");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["armor"] = 7f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("boots_mythril", "$boots");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["armor"] = 8f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("boots_adamantine", "$boots");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00078120 File Offset: 0x00076320
	private void initRings()
	{
		this.clone("ring_bone", "$ring");
		this.t.material = "bone";
		this.t.equipment_value = 5;
		this.t.setCost(0, "bones", 1, "gems", 1);
		this.t.rigidity_rating = 1;
		this.t.base_stats["critical_chance"] = 0.02f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_copper", "$ring");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "gems", 1);
		this.t.rigidity_rating = 2;
		this.t.base_stats["critical_chance"] = 0.03f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_bronze", "$ring");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 1, "gems", 1);
		this.t.rigidity_rating = 3;
		this.t.base_stats["critical_chance"] = 0.04f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_silver", "$ring");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.setCost(0, "silver", 1, "gems", 1);
		this.t.rigidity_rating = 2;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 10f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_iron", "$ring");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 2, "gems", 1);
		this.t.rigidity_rating = 4;
		this.t.base_stats["critical_chance"] = 0.06f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_steel", "$ring");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "common_metals", 3, "gems", 1);
		this.t.rigidity_rating = 5;
		this.t.base_stats["critical_chance"] = 0.07f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_mythril", "$ring");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.setCost(0, "mythril", 1, "gems", 1);
		this.t.rigidity_rating = 6;
		this.t.base_stats["critical_chance"] = 0.08f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("ring_adamantine", "$ring");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.setCost(0, "adamantine", 1, "gems", 1);
		this.t.rigidity_rating = 7;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["mana"] = 20f;
		this.t.base_stats["stamina"] = 5f;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x000785E0 File Offset: 0x000767E0
	private void initTemplates()
	{
		EquipmentAsset equipmentAsset = new EquipmentAsset();
		equipmentAsset.id = "$equipment";
		equipmentAsset.pool = "equipment";
		equipmentAsset.equipment_subtype = "basic";
		EquipmentAsset pAsset = equipmentAsset;
		this.t = equipmentAsset;
		this.add(pAsset);
		this.initTemplatesEquipment();
		this.initTemplatesWeapons();
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00078630 File Offset: 0x00076830
	private void initTemplatesEquipment()
	{
		this.clone("$armor", "$equipment");
		this.t.equipment_type = EquipmentType.Armor;
		this.t.name_class = "item_class_armor";
		this.t.equipment_subtype = "armor";
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"armor_name"
		});
		this.t.group_id = "armor";
		this.clone("$boots", "$equipment");
		this.t.equipment_type = EquipmentType.Boots;
		this.t.name_class = "item_class_armor";
		this.t.equipment_subtype = "boots";
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"boots_name"
		});
		this.t.group_id = "boots";
		this.clone("$helmet", "$equipment");
		this.t.equipment_type = EquipmentType.Helmet;
		this.t.name_class = "item_class_armor";
		this.t.equipment_subtype = "helmet";
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"helmet_name"
		});
		this.t.group_id = "helmet";
		this.clone("$accessory", "$equipment");
		this.t.name_class = "item_class_accessory";
		this.clone("$ring", "$accessory");
		this.t.equipment_subtype = "ring";
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"ring_name"
		});
		this.t.equipment_type = EquipmentType.Ring;
		this.t.group_id = "ring";
		this.clone("$amulet", "$accessory");
		this.t.equipment_type = EquipmentType.Amulet;
		this.t.equipment_subtype = "amulet";
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"amulet_name"
		});
		this.t.group_id = "amulet";
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00078858 File Offset: 0x00076A58
	private void initTemplatesWeapons()
	{
		this.clone("$weapon", "$equipment");
		this.t.material = "basic";
		this.t.group_id = "sword";
		this.t.equipment_type = EquipmentType.Weapon;
		this.t.path_slash_animation = "effects/slashes/slash_base";
		this.t.name_class = "item_class_weapon";
		this.t.base_stats["damage_range"] = 0.5f;
		this.clone("$melee", "$weapon");
		this.t.pool = "melee";
		this.clone("$range", "$weapon");
		this.t.pool = "range";
		this.t.attack_type = WeaponType.Range;
		this.t.base_stats["projectiles"] = 1f;
		this.t.base_stats["damage_range"] = 0.6f;
		this.clone("$bow", "$range");
		this.t.equipment_subtype = "bow";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.projectile = "arrow";
		this.t.path_slash_animation = "effects/slashes/slash_bow";
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["recoil"] = 1f;
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "bow_name");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "bow";
		this.clone("$sword", "$melee");
		this.t.equipment_subtype = "sword";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.path_slash_animation = "effects/slashes/slash_sword";
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["damage_range"] = 0.8f;
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "sword_name");
		this.t.name_templates.AddTimes(3, "sword_name_king");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "sword";
		this.clone("$axe", "$melee");
		this.t.equipment_subtype = "axe";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.path_slash_animation = "effects/slashes/slash_axe";
		this.t.base_stats["damage_range"] = 0.6f;
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "axe_name");
		this.t.name_templates.AddTimes(3, "axe_name_king");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "axe";
		this.clone("$hammer", "$melee");
		this.t.equipment_subtype = "hammer";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.path_slash_animation = "effects/slashes/slash_hammer";
		this.t.base_stats["targets"] = 2f;
		this.t.base_stats["damage_range"] = 0.1f;
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "hammer_name");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "hammer";
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"stun"
		});
		this.clone("$spear", "$melee");
		this.t.equipment_subtype = "spear";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.path_slash_animation = "effects/slashes/slash_spear";
		this.t.base_stats["range"] = 1f;
		this.t.base_stats["damage_range"] = 0.7f;
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "spear_name");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "spear";
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00078F4E File Offset: 0x0007714E
	private void initNormalWeapons()
	{
		this.initWeaponsBasic();
		this.initWeaponsAdvanced();
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00078F5C File Offset: 0x0007715C
	private void initWeaponsAdvanced()
	{
		this.initWeaponsBows();
		this.initWeaponsSwords();
		this.initWeaponsAxes();
		this.initWeaponsSpears();
		this.initWeaponsHammers();
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00078F7C File Offset: 0x0007717C
	private void initWeaponsBasic()
	{
		this.clone("stick_wood", "$melee");
		this.t.equipment_subtype = "stick";
		this.t.material = "wood";
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 10;
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.name_templates = new List<string>();
		this.t.name_templates.AddTimes(30, "stick_name");
		this.t.name_templates.Add("weapon_name_city");
		this.t.name_templates.Add("weapon_name_kingdom");
		this.t.name_templates.Add("weapon_name_culture");
		this.t.name_templates.Add("weapon_name_enemy_king");
		this.t.name_templates.Add("weapon_name_enemy_kingdom");
		this.t.group_id = "staff";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["stamina"] = 5f;
		this.t.base_stats["mana"] = 5f;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00079110 File Offset: 0x00077310
	private void initWeaponsSwords()
	{
		this.clone("sword_wood", "$sword");
		this.t.material = "wood";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("sword_stone", "$sword");
		this.t.material = "stone";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "stone", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["speed"] = -2f;
		this.clone("sword_copper", "$sword");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("sword_bronze", "$sword");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["damage"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("sword_silver", "$sword");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("sword_iron", "$sword");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["damage"] = 6f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("sword_steel", "$sword");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["damage"] = 7f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("sword_mythril", "$sword");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 8f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("sword_adamantine", "$sword");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00079760 File Offset: 0x00077960
	private void initWeaponsBows()
	{
		this.clone("bow_wood", "$bow");
		this.t.material = "wood";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["range"] = 6f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("bow_copper", "$bow");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["range"] = 6f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("bow_bronze", "$bow");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["damage"] = 4f;
		this.t.base_stats["range"] = 7f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("bow_silver", "$bow");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["range"] = 8f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("bow_iron", "$bow");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["damage"] = 6f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["range"] = 9f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("bow_steel", "$bow");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["damage"] = 7f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["range"] = 10f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("bow_mythril", "$bow");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 8f;
		this.t.base_stats["range"] = 11f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("bow_adamantine", "$bow");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["range"] = 12f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00079DEC File Offset: 0x00077FEC
	private void initWeaponsAxes()
	{
		this.clone("axe_wood", "$axe");
		this.t.material = "wood";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("axe_stone", "$axe");
		this.t.material = "stone";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "stone", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["speed"] = -2f;
		this.clone("axe_copper", "$axe");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("axe_bronze", "$axe");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["damage"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("axe_silver", "$axe");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("axe_iron", "$axe");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["damage"] = 6f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("axe_steel", "$axe");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["damage"] = 7f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("axe_mythril", "$axe");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 8f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("axe_adamantine", "$axe");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0007A43C File Offset: 0x0007863C
	private void initWeaponsSpears()
	{
		this.clone("spear_wood", "$spear");
		this.t.material = "wood";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("spear_stone", "$spear");
		this.t.material = "stone";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "stone", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["speed"] = -2f;
		this.clone("spear_copper", "$spear");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("spear_bronze", "$spear");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["damage"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("spear_silver", "$spear");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("spear_iron", "$spear");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["damage"] = 6f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("spear_steel", "$spear");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["damage"] = 7f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("spear_mythril", "$spear");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 8f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("spear_adamantine", "$spear");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0007AA8C File Offset: 0x00078C8C
	private void initWeaponsHammers()
	{
		this.clone("hammer_wood", "$hammer");
		this.t.material = "wood";
		this.t.equipment_value = 1;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "wood", 1, "none", 0);
		this.t.rigidity_rating = 1;
		this.t.base_stats["damage"] = 1f;
		this.t.base_stats["stamina"] = 15f;
		this.clone("hammer_stone", "$hammer");
		this.t.material = "stone";
		this.t.equipment_value = 10;
		this.t.minimum_city_storage_resource_1 = 15;
		this.t.setCost(0, "stone", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["speed"] = -2f;
		this.clone("hammer_copper", "$hammer");
		this.t.material = "copper";
		this.t.equipment_value = 10;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 3f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("hammer_bronze", "$hammer");
		this.t.material = "bronze";
		this.t.equipment_value = 15;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 2, "none", 0);
		this.t.rigidity_rating = 3;
		this.t.base_stats["damage"] = 4f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("hammer_silver", "$hammer");
		this.t.material = "silver";
		this.t.equipment_value = 20;
		this.t.metallic = true;
		this.t.setCost(0, "silver", 1, "none", 0);
		this.t.rigidity_rating = 2;
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("hammer_iron", "$hammer");
		this.t.material = "iron";
		this.t.equipment_value = 30;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 3, "none", 0);
		this.t.rigidity_rating = 4;
		this.t.base_stats["damage"] = 6f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("hammer_steel", "$hammer");
		this.t.material = "steel";
		this.t.equipment_value = 40;
		this.t.metallic = true;
		this.t.minimum_city_storage_resource_1 = 10;
		this.t.setCost(0, "common_metals", 4, "none", 0);
		this.t.rigidity_rating = 5;
		this.t.base_stats["damage"] = 7f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["stamina"] = 5f;
		this.clone("hammer_mythril", "$hammer");
		this.t.material = "mythril";
		this.t.equipment_value = 50;
		this.t.metallic = true;
		this.t.setCost(0, "mythril", 1, "none", 0);
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 8f;
		this.t.base_stats["critical_chance"] = 0.05f;
		this.t.base_stats["mana"] = 25f;
		this.t.base_stats["stamina"] = 10f;
		this.clone("hammer_adamantine", "$hammer");
		this.t.material = "adamantine";
		this.t.equipment_value = 70;
		this.t.metallic = true;
		this.t.setCost(0, "adamantine", 1, "none", 0);
		this.t.rigidity_rating = 7;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["mana"] = 35f;
		this.t.base_stats["stamina"] = 10f;
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0007B0DC File Offset: 0x000792DC
	private void initWeaponsUnique()
	{
		this.clone("alien_blaster", "$range");
		this.t.setUnlockedWithAchievement("achievementEquipmentExplorer");
		this.t.equipment_subtype = "alien_blaster";
		this.t.setCost(100, "adamantine", 10, "gems", 20);
		this.t.rigidity_rating = 7;
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 1;
		this.t.path_icon = "ui/Icons/items/icon_alien_blaster";
		this.t.material = "basic";
		this.t.projectile = "plasma_ball";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.base_stats["range"] = 20f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["damage_range"] = 0.6f;
		this.t.base_stats["mana"] = 20f;
		this.t.base_stats["stamina"] = 20f;
		this.t.equipment_value = 500;
		this.t.base_stats["damage"] = 30f;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"blaster_name"
		});
		this.t.group_id = "firearm";
		this.clone("shotgun", "$range");
		this.t.setUnlockedWithAchievement("achievementTLDR");
		this.t.equipment_subtype = "shotgun";
		this.t.setCost(100, "adamantine", 10, "mythril", 5);
		this.t.rigidity_rating = 6;
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 1;
		this.t.path_icon = "ui/Icons/items/icon_shotgun";
		this.t.projectile = "shotgun_bullet";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.base_stats["projectiles"] = 12f;
		this.t.base_stats["range"] = 10f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["damage"] = 10f;
		this.t.base_stats["damage_range"] = 0.9f;
		this.t.base_stats["mana"] = 5f;
		this.t.base_stats["stamina"] = 10f;
		this.t.equipment_value = 600;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"shotgun_name"
		});
		this.t.group_id = "firearm";
		this.clone("flame_hammer", "$weapon");
		this.t.setUnlockedWithAchievement("achievementGodlySmithing");
		this.t.equipment_subtype = "flame_hammer";
		this.t.setCost(10, "dragon_scales", 3, "none", 0);
		this.t.is_pool_weapon = true;
		this.t.animated = true;
		this.t.pool_rate = 2;
		this.t.material = "basic";
		this.t.path_slash_animation = "effects/slashes/slash_hammer";
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 20f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["targets"] = 3f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 15f;
		this.t.equipment_value = 400;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"flame_hammer_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"flame"
		});
		this.t.group_id = "hammer";
		this.clone("ice_hammer", "$weapon");
		this.t.setUnlockedWithAchievement("achievementMakeWilhelmScream");
		this.t.equipment_subtype = "ice_hammer";
		this.t.setCost(10, "mythril", 10, "gems", 2);
		this.t.rigidity_rating = 6;
		this.t.is_pool_weapon = true;
		this.t.animated = true;
		this.t.pool_rate = 2;
		this.t.material = "basic";
		this.t.path_slash_animation = "effects/slashes/slash_hammer";
		this.t.base_stats["damage"] = 20f;
		this.t.base_stats["speed"] = 1f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.t.base_stats["targets"] = 3f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 15f;
		this.t.equipment_value = 400;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"ice_hammer_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"ice",
			"stun"
		});
		this.t.group_id = "hammer";
		this.clone("flame_sword", "$weapon");
		this.t.equipment_subtype = "flame_sword";
		this.t.setCost(0, "dragon_scales", 2, "none", 0);
		this.t.is_pool_weapon = true;
		this.t.animated = true;
		this.t.pool_rate = 2;
		this.t.material = "basic";
		this.t.path_slash_animation = "effects/slashes/slash_sword";
		this.t.rigidity_rating = 6;
		this.t.base_stats["damage"] = 33f;
		this.t.base_stats["targets"] = 2f;
		this.t.base_stats["critical_damage_multiplier"] = 0.1f;
		this.t.base_stats["mana"] = 15f;
		this.t.base_stats["stamina"] = 15f;
		this.t.equipment_value = 300;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"flame_sword_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"flame"
		});
		this.t.group_id = "sword";
		this.t.base_stats.addTag("building_immunity_fire");
		this.clone("necromancer_staff", "$range");
		this.t.equipment_subtype = "necromancer_staff";
		this.t.setCost(10, "mythril", 2, "gems", 3);
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 1;
		this.t.material = "basic";
		this.t.projectile = "skull";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.rigidity_rating = 5;
		this.t.base_stats["range"] = 13f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["mana"] = 40f;
		this.t.equipment_value = 500;
		this.t.base_stats["damage"] = 30f;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"necromancer_staff_name"
		});
		this.t.group_id = "staff";
		this.t.addSpell("spawn_skeleton");
		this.t.addSpell("cast_curse");
		this.clone("evil_staff", "$range");
		this.t.equipment_subtype = "evil_staff";
		this.t.setCost(20, "mythril", 3, "gems", 2);
		this.t.is_pool_weapon = true;
		this.t.durability = 300;
		this.t.pool_rate = 1;
		this.t.material = "basic";
		this.t.projectile = "red_orb";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.rigidity_rating = 5;
		this.t.base_stats["range"] = 13f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["mana"] = 40f;
		this.t.equipment_value = 500;
		this.t.base_stats["projectiles"] = 20f;
		this.t.base_stats["damage"] = 10f;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"evil_staff_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"flame"
		});
		this.t.group_id = "staff";
		this.t.base_stats.addTag("building_immunity_fire");
		this.t.addSpell("cast_fire");
		this.clone("white_staff", "$range");
		this.t.equipment_subtype = "white_staff";
		this.t.setCost(20, "mythril", 3, "gems", 2);
		this.t.is_pool_weapon = true;
		this.t.durability = 300;
		this.t.pool_rate = 3;
		this.t.material = "basic";
		this.t.projectile = "freeze_orb";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.rigidity_rating = 5;
		this.t.base_stats["range"] = 18f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["damage"] = 35f;
		this.t.base_stats["mana"] = 40f;
		this.t.equipment_value = 500;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"white_staff_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"ice"
		});
		this.t.group_id = "staff";
		this.t.addSpell("cast_blood_rain");
		this.t.addSpell("summon_lightning");
		this.clone("plague_doctor_staff", "$weapon");
		this.t.equipment_subtype = "plague_doctor_staff";
		this.t.setCost(5, "mythril", 2, "gems", 1);
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 3;
		this.t.material = "basic";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.rigidity_rating = 5;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 3f;
		this.t.base_stats["critical_damage_multiplier"] = 0.5f;
		this.t.base_stats["damage"] = 35f;
		this.t.base_stats["mana"] = 40f;
		this.t.equipment_value = 200;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"plague_doctor_staff_name"
		});
		this.t.group_id = "staff";
		this.t.addSpell("cast_fire");
		this.t.addSpell("cast_cure");
		this.clone("druid_staff", "$range");
		this.t.equipment_subtype = "druid_staff";
		this.t.setCost(7, "mythril", 3, "gems", 1);
		this.t.is_pool_weapon = true;
		this.t.pool_rate = 3;
		this.t.material = "basic";
		this.t.projectile = "green_orb";
		this.t.path_slash_animation = "effects/slashes/slash_punch";
		this.t.rigidity_rating = 5;
		this.t.base_stats["range"] = 20f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["critical_damage_multiplier"] = 0.3f;
		this.t.base_stats["damage"] = 12f;
		this.t.base_stats["mana"] = 40f;
		this.t.equipment_value = 300;
		this.t.base_stats["projectiles"] = 2f;
		this.t.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
		{
			"druid_staff_name"
		});
		this.t.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
		{
			"slowness"
		});
		this.t.group_id = "staff";
		this.t.addSpell("cast_blood_rain");
		this.t.addSpell("spawn_vegetation");
	}

	// Token: 0x040008DC RID: 2268
	[NonSerialized]
	public List<EquipmentAsset> pot_weapon_assets_all = new List<EquipmentAsset>();

	// Token: 0x040008DD RID: 2269
	[NonSerialized]
	public Dictionary<string, List<EquipmentAsset>> pot_equipment_by_groups_all = new Dictionary<string, List<EquipmentAsset>>();

	// Token: 0x040008DE RID: 2270
	[NonSerialized]
	public List<EquipmentAsset> pot_weapon_assets_unlocked = new List<EquipmentAsset>();

	// Token: 0x040008DF RID: 2271
	[NonSerialized]
	public Dictionary<string, List<EquipmentAsset>> pot_equipment_by_groups_unlocked = new Dictionary<string, List<EquipmentAsset>>();

	// Token: 0x040008E0 RID: 2272
	[NonSerialized]
	public Dictionary<string, List<EquipmentAsset>> equipment_by_subtypes = new Dictionary<string, List<EquipmentAsset>>();

	// Token: 0x040008E1 RID: 2273
	public static readonly string[] default_weapon_pool = new string[]
	{
		"sword",
		"axe",
		"hammer",
		"spear",
		"bow",
		"stick"
	};

	// Token: 0x040008E2 RID: 2274
	public static EquipmentAsset base_attack;

	// Token: 0x040008E3 RID: 2275
	private const string TEMPLATE_EQUIPMENT = "$equipment";

	// Token: 0x040008E4 RID: 2276
	private const string TEMPLATE_ARMOR = "$armor";

	// Token: 0x040008E5 RID: 2277
	private const string TEMPLATE_BOOTS = "$boots";

	// Token: 0x040008E6 RID: 2278
	private const string TEMPLATE_HELMET = "$helmet";

	// Token: 0x040008E7 RID: 2279
	private const string TEMPLATE_ACCESSORY = "$accessory";

	// Token: 0x040008E8 RID: 2280
	private const string TEMPLATE_RING = "$ring";

	// Token: 0x040008E9 RID: 2281
	private const string TEMPLATE_AMULET = "$amulet";

	// Token: 0x040008EA RID: 2282
	private const string TEMPLATE_WEAPON = "$weapon";

	// Token: 0x040008EB RID: 2283
	private const string TEMPLATE_MELEE = "$melee";

	// Token: 0x040008EC RID: 2284
	private const string TEMPLATE_RANGE = "$range";

	// Token: 0x040008ED RID: 2285
	private const string TEMPLATE_BOW = "$bow";

	// Token: 0x040008EE RID: 2286
	private const string TEMPLATE_SWORD = "$sword";

	// Token: 0x040008EF RID: 2287
	private const string TEMPLATE_AXE = "$axe";

	// Token: 0x040008F0 RID: 2288
	private const string TEMPLATE_HAMMER = "$hammer";

	// Token: 0x040008F1 RID: 2289
	private const string TEMPLATE_SPEAR = "$spear";
}
