using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000112 RID: 274
[Serializable]
public class ItemAsset : BaseAugmentationAsset, IDescriptionAsset, ILocalizedAsset, IMultiLocalesAsset
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000873 RID: 2163 RVA: 0x000754AA File Offset: 0x000736AA
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_equipment;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000874 RID: 2164 RVA: 0x000754BD File Offset: 0x000736BD
	[JsonIgnore]
	public int get_total_cost
	{
		get
		{
			return this.cost_gold + this.cost_coins_resources;
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000754CC File Offset: 0x000736CC
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.item_groups.get(this.group_id);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x000754DE File Offset: 0x000736DE
	public string getLocaleRarity(Rarity pRarity)
	{
		return this.name_class + Item.getQualityString(pRarity);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x000754F1 File Offset: 0x000736F1
	public override string getLocaleID()
	{
		if (!this.has_locales)
		{
			return null;
		}
		string result;
		if ((result = this.translation_key) == null)
		{
			result = "item_" + (this.equipment_subtype ?? this.id);
		}
		return result;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00075521 File Offset: 0x00073721
	public string getDescriptionID()
	{
		if (!this.has_locales || this.isMod())
		{
			return null;
		}
		return this.id + "_description";
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00075545 File Offset: 0x00073745
	public string getMaterialID()
	{
		if (!this.has_locales || this.isMod())
		{
			return null;
		}
		return "item_mat_" + this.material;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00075569 File Offset: 0x00073769
	public IEnumerable<string> getLocaleIDs()
	{
		if (!this.has_locales)
		{
			yield break;
		}
		yield return this.getLocaleID();
		if (this.isMod())
		{
			yield break;
		}
		yield return this.getDescriptionID();
		if (this.material != "basic")
		{
			yield return this.getMaterialID();
		}
		foreach (object obj in Enum.GetValues(typeof(Rarity)))
		{
			Rarity tRarity = (Rarity)obj;
			yield return this.getLocaleRarity(tRarity);
		}
		IEnumerator enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0007557C File Offset: 0x0007377C
	public string getTranslatedName()
	{
		string tName = LocalizedTextManager.getText(this.getLocaleID(), null, false);
		if (!string.IsNullOrEmpty(this.material) && this.material != "basic")
		{
			string tMaterial = LocalizedTextManager.getText(this.getMaterialID(), null, false);
			tName = tName + " (" + tMaterial + ")";
		}
		return tName;
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000755D7 File Offset: 0x000737D7
	public string getTranslatedDescription()
	{
		return LocalizedTextManager.getText(this.getDescriptionID(), null, false);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000755E6 File Offset: 0x000737E6
	public void setCost(int pGoldCost, string pResourceID_1 = "none", int pCostResource_1 = 0, string pResourceID_2 = "none", int pCostResource_2 = 0)
	{
		this.cost_gold = pGoldCost;
		this.cost_resource_id_1 = pResourceID_1;
		this.cost_resource_1 = pCostResource_1;
		this.cost_resource_id_2 = pResourceID_2;
		this.cost_resource_2 = pCostResource_2;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0007560D File Offset: 0x0007380D
	public bool isMod()
	{
		return this.mod_type != null;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0007561C File Offset: 0x0007381C
	public string getRandomNameTemplate(Actor pActor = null)
	{
		foreach (string template_id in this.name_templates.LoopRandom<string>())
		{
			NameGeneratorAsset tNameAsset = AssetManager.name_generator.get(template_id);
			if (tNameAsset.check == null || tNameAsset.check(pActor))
			{
				return template_id;
			}
		}
		return null;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00075690 File Offset: 0x00073890
	public override bool unlock(bool pSaveData = true)
	{
		if (!base.unlock(pSaveData))
		{
			return false;
		}
		EquipmentAsset tThisEquipmentAsset = this as EquipmentAsset;
		List<EquipmentAsset> tWeaponPool = AssetManager.items.pot_weapon_assets_unlocked;
		if (this.is_pool_weapon && !tWeaponPool.Contains(tThisEquipmentAsset))
		{
			tWeaponPool.Add(tThisEquipmentAsset);
		}
		Dictionary<string, List<EquipmentAsset>> tEquipmentPool = AssetManager.items.pot_equipment_by_groups_unlocked;
		if (!this.is_pool_weapon)
		{
			if (!tEquipmentPool.ContainsKey(this.group_id))
			{
				tEquipmentPool.Add(this.group_id, new List<EquipmentAsset>());
			}
			List<EquipmentAsset> tList = tEquipmentPool[this.group_id];
			if (!tList.Contains(tThisEquipmentAsset))
			{
				tList.Add(tThisEquipmentAsset);
			}
		}
		return true;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00075722 File Offset: 0x00073922
	protected override bool isDebugUnlockedAll()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllEquipment);
	}

	// Token: 0x040008BA RID: 2234
	public string path_gameplay_sprite;

	// Token: 0x040008BB RID: 2235
	[NonSerialized]
	public Sprite[] gameplay_sprites;

	// Token: 0x040008BC RID: 2236
	public string material;

	// Token: 0x040008BD RID: 2237
	public bool colored;

	// Token: 0x040008BE RID: 2238
	public int minimum_city_storage_resource_1;

	// Token: 0x040008BF RID: 2239
	public int cost_gold;

	// Token: 0x040008C0 RID: 2240
	public int cost_coins_resources;

	// Token: 0x040008C1 RID: 2241
	public int cost_resource_1;

	// Token: 0x040008C2 RID: 2242
	[DefaultValue("none")]
	public string cost_resource_id_1 = "none";

	// Token: 0x040008C3 RID: 2243
	public int cost_resource_2;

	// Token: 0x040008C4 RID: 2244
	[DefaultValue("none")]
	public string cost_resource_id_2 = "none";

	// Token: 0x040008C5 RID: 2245
	[DefaultValue(1)]
	public int rarity = 1;

	// Token: 0x040008C6 RID: 2246
	public int equipment_value;

	// Token: 0x040008C7 RID: 2247
	public bool metallic;

	// Token: 0x040008C8 RID: 2248
	[DefaultValue(1)]
	public int mod_rank = 1;

	// Token: 0x040008C9 RID: 2249
	public string mod_type;

	// Token: 0x040008CA RID: 2250
	[DefaultValue(true)]
	public bool mod_can_be_given = true;

	// Token: 0x040008CB RID: 2251
	public string translation_key;

	// Token: 0x040008CC RID: 2252
	public bool animated;

	// Token: 0x040008CD RID: 2253
	public string pool;

	// Token: 0x040008CE RID: 2254
	[DefaultValue("")]
	public string path_slash_animation = string.Empty;

	// Token: 0x040008CF RID: 2255
	public string projectile;

	// Token: 0x040008D0 RID: 2256
	[DefaultValue(Rarity.R0_Normal)]
	public Rarity quality;

	// Token: 0x040008D1 RID: 2257
	[DefaultValue(WeaponType.Melee)]
	public WeaponType attack_type;

	// Token: 0x040008D2 RID: 2258
	public EquipmentType equipment_type;

	// Token: 0x040008D3 RID: 2259
	public string equipment_subtype;

	// Token: 0x040008D4 RID: 2260
	public string name_class;

	// Token: 0x040008D5 RID: 2261
	public List<string> name_templates;

	// Token: 0x040008D6 RID: 2262
	public string[] item_modifier_ids;

	// Token: 0x040008D7 RID: 2263
	[NonSerialized]
	public ItemModAsset[] item_modifiers;

	// Token: 0x040008D8 RID: 2264
	public bool is_pool_weapon;

	// Token: 0x040008D9 RID: 2265
	public int pool_rate;

	// Token: 0x040008DA RID: 2266
	[DefaultValue(100)]
	public int durability = 100;

	// Token: 0x040008DB RID: 2267
	[DefaultValue(1)]
	public int rigidity_rating = 1;
}
