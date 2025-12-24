using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class Item : CoreSystemObject<ItemData>
{
	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06001771 RID: 6001 RVA: 0x000E7363 File Offset: 0x000E5563
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Item;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06001772 RID: 6002 RVA: 0x000E7367 File Offset: 0x000E5567
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.items;
		}
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x000E7373 File Offset: 0x000E5573
	public override void setFavorite(bool pState)
	{
		base.setFavorite(pState);
		if (this.isReadyForRemoval())
		{
			World.world.items.setDirty();
		}
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000E7393 File Offset: 0x000E5593
	public bool isCursed()
	{
		return this.hasMod("cursed");
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000E73A0 File Offset: 0x000E55A0
	public bool isEternal()
	{
		return this.hasMod("eternal");
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000E73AD File Offset: 0x000E55AD
	public bool isDestroyable()
	{
		return !base.isFavorite() && !this.isEternal();
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x000E73C2 File Offset: 0x000E55C2
	public bool isReadyForRemoval()
	{
		return !this.hasCity() && !this.hasActor() && this.isDestroyable();
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000E73DC File Offset: 0x000E55DC
	public void fullRepair()
	{
		this.data.durability = 100;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000E73EB File Offset: 0x000E55EB
	public void getDamaged(int pDamage)
	{
		if (this.hasMod("eternal"))
		{
			return;
		}
		this.data.durability -= pDamage;
		if (this.data.durability < 0)
		{
			this.data.durability = 0;
		}
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x000E7428 File Offset: 0x000E5628
	public void newItem(EquipmentAsset pAsset)
	{
		this.setAsset(pAsset);
		this.data.durability = pAsset.durability;
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000E7442 File Offset: 0x000E5642
	public bool isBroken()
	{
		return this.getDurabilityCurrent() <= 0;
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000E7450 File Offset: 0x000E5650
	public bool needRepair()
	{
		return this.getDurabilityCurrent() < this.getDurabilityMax();
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000E7460 File Offset: 0x000E5660
	public int getDurabilityCurrent()
	{
		return this.data.durability;
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x000E746D File Offset: 0x000E566D
	public int getDurabilityMax()
	{
		return this._asset.durability;
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000E747C File Offset: 0x000E567C
	public string getDurabilityString()
	{
		return this.getDurabilityCurrent().ToString() + "/" + this.getDurabilityMax().ToString();
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000E74AF File Offset: 0x000E56AF
	private void setAsset(EquipmentAsset pAsset)
	{
		this._asset = pAsset;
		this.data.asset_id = this._asset.id;
		this.recalculateTexturePath();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000E74D4 File Offset: 0x000E56D4
	private void recalculateTexturePath()
	{
		this._texture_id = this.asset.path_gameplay_sprite;
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000E74E7 File Offset: 0x000E56E7
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this._asset = null;
		this.unit_has_it = false;
		this.city_has_it = false;
		this.shouldbe_removed = false;
		this._item_value = 0;
		this._quality = Rarity.R0_Normal;
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x000E751C File Offset: 0x000E571C
	public void reforge(int pTries)
	{
		this.clearForReforge();
		World.world.items.generateModsFor(this, pTries, null, true);
		string tName = NameGenerator.getName("reforged_item", ActorSex.Male, false, null, null, false);
		base.setName(tName, true);
		this.calculateValues();
		if (this._actor != null)
		{
			this._actor.addTrait("scar_of_divinity", false);
		}
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000E7582 File Offset: 0x000E5782
	private void clearForReforge()
	{
		this.data.modifiers.Clear();
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x000E7594 File Offset: 0x000E5794
	public void transmute()
	{
		this.clearForReforge();
		bool tIsCheatEnabled = this.isCheatEnabled();
		using (ListPool<EquipmentAsset> tPossibleAssets = new ListPool<EquipmentAsset>())
		{
			List<EquipmentAsset> tPotAssets;
			if (this._asset.is_pool_weapon)
			{
				if (tIsCheatEnabled)
				{
					tPotAssets = AssetManager.items.pot_weapon_assets_all;
				}
				else
				{
					tPotAssets = AssetManager.items.pot_weapon_assets_unlocked;
				}
			}
			else if (tIsCheatEnabled)
			{
				tPotAssets = AssetManager.items.pot_equipment_by_groups_all[this._asset.group_id];
			}
			else
			{
				tPotAssets = AssetManager.items.pot_equipment_by_groups_unlocked[this._asset.group_id];
			}
			foreach (EquipmentAsset tEquipmentAsset in tPotAssets)
			{
				if (tEquipmentAsset.isAvailable())
				{
					tPossibleAssets.Add(tEquipmentAsset);
				}
			}
			tPossibleAssets.Shuffle<EquipmentAsset>();
			foreach (EquipmentAsset ptr in tPossibleAssets)
			{
				EquipmentAsset tAsset = ptr;
				if (this._asset != tAsset)
				{
					this.setAsset(tAsset);
					break;
				}
			}
			this.reforge(5);
			this.calculateValues();
		}
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x000E76E0 File Offset: 0x000E58E0
	public bool hasMod(string pID)
	{
		return this.data.modifiers.Contains(pID);
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000E76F8 File Offset: 0x000E58F8
	public bool hasMod(ItemModAsset pModAsset)
	{
		return this.hasMod(pModAsset.id);
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000E7708 File Offset: 0x000E5908
	public bool addMod(string pModId)
	{
		ItemModAsset tNewMod = AssetManager.items_modifiers.get(pModId);
		return this.addMod(tNewMod);
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000E7728 File Offset: 0x000E5928
	public bool addMod(ItemModAsset pModAsset)
	{
		if (this.hasMod(pModAsset))
		{
			return false;
		}
		foreach (string ptr in this.data.modifiers)
		{
			string tModID = ptr;
			if (AssetManager.items_modifiers.get(tModID).mod_type == pModAsset.mod_type)
			{
				return false;
			}
		}
		this.data.modifiers.Add(pModAsset.id);
		World.world.items.setDirty();
		return true;
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000E77D0 File Offset: 0x000E59D0
	public bool removeMod(string pModId)
	{
		World.world.items.setDirty();
		return this.data.modifiers.Remove(pModId);
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000E77F2 File Offset: 0x000E59F2
	public void setUnitHasIt(Actor pActor)
	{
		this._actor = pActor;
		this._city = null;
		this.unit_has_it = true;
		this.city_has_it = false;
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000E7810 File Offset: 0x000E5A10
	public void setInCityStorage(City pCity)
	{
		this._actor = null;
		this._city = pCity;
		this.unit_has_it = false;
		this.city_has_it = true;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000E782E File Offset: 0x000E5A2E
	public void setShouldBeRemoved()
	{
		this.shouldbe_removed = true;
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000E7837 File Offset: 0x000E5A37
	public void clearUnit()
	{
		this._actor = null;
		this.unit_has_it = false;
		World.world.items.setDirty();
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000E7856 File Offset: 0x000E5A56
	public void clearCity()
	{
		this._city = null;
		this.city_has_it = false;
		World.world.items.setDirty();
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000E7875 File Offset: 0x000E5A75
	public Actor getActor()
	{
		return this._actor;
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000E787D File Offset: 0x000E5A7D
	public City getCity()
	{
		return this._city;
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000E7885 File Offset: 0x000E5A85
	public void calculateValues()
	{
		this.recalculateTexturePath();
		ItemTools.calcItemValues(this, this._total_stats);
		this._item_value = ItemTools.s_value;
		this._quality = ItemTools.s_quality;
		this.data.durability = this._asset.durability;
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000E78C5 File Offset: 0x000E5AC5
	public void initItem()
	{
		this.initFields();
		this.calculateValues();
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000E78D3 File Offset: 0x000E5AD3
	public override void loadData(ItemData pData)
	{
		base.loadData(pData);
		this.initFields();
		this.calculateValues();
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x000E78E8 File Offset: 0x000E5AE8
	private void initFields()
	{
		EquipmentAsset tAsset = AssetManager.items.get(this.data.asset_id);
		this.setAsset(tAsset);
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x000E7912 File Offset: 0x000E5B12
	public EquipmentAsset getAsset()
	{
		return this._asset;
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06001797 RID: 6039 RVA: 0x000E791A File Offset: 0x000E5B1A
	public EquipmentAsset asset
	{
		get
		{
			return this.getAsset();
		}
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x000E7924 File Offset: 0x000E5B24
	public string getItemDescription()
	{
		string tLocaleKey;
		if (this.data.created_by_player)
		{
			tLocaleKey = "item_template_description_full_player";
		}
		else
		{
			tLocaleKey = "item_template_description_full";
		}
		string tTemplateHistory = LocalizedTextManager.getText(tLocaleKey, null, false);
		string tTemplateUnknown = LocalizedTextManager.getText("item_template_description_age_only", null, false);
		if (string.IsNullOrEmpty(this.data.by))
		{
			tTemplateHistory = tTemplateUnknown;
		}
		int tAge = this.getAge();
		tTemplateHistory = tTemplateHistory.Replace("$item_creator_name$", Toolbox.coloredString(this.data.by, this.data.byColor));
		tTemplateHistory = tTemplateHistory.Replace("$item_creator_kingdom$", Toolbox.coloredString(this.data.from, this.data.fromColor));
		tTemplateHistory = tTemplateHistory.Replace("$item_creator_years$", tAge.ToString() ?? "");
		if (tAge == 1)
		{
			tTemplateHistory = tTemplateHistory.Replace("$year_ending$", LocalizedTextManager.getText("item_template_description_year", null, false) ?? "");
		}
		else
		{
			tTemplateHistory = tTemplateHistory.Replace("$year_ending$", LocalizedTextManager.getText("item_template_description_years", null, false) ?? "");
		}
		return tTemplateHistory;
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x000E7A30 File Offset: 0x000E5C30
	public string getItemKeyType()
	{
		return this._asset.getLocaleRarity(this.getQuality());
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000E7A43 File Offset: 0x000E5C43
	public void countKill()
	{
		this.data.kills++;
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x000E7A58 File Offset: 0x000E5C58
	public bool isRangeAttack()
	{
		return this.getAsset().attack_type == WeaponType.Range;
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x000E7A68 File Offset: 0x000E5C68
	public BaseStats getFullStats()
	{
		return this._total_stats;
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000E7A70 File Offset: 0x000E5C70
	public int getValue()
	{
		return this._item_value;
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000E7A78 File Offset: 0x000E5C78
	public Rarity getQuality()
	{
		return this._quality;
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000E7A80 File Offset: 0x000E5C80
	public string getTextureID()
	{
		return this._texture_id;
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000E7A88 File Offset: 0x000E5C88
	// (set) Token: 0x060017A1 RID: 6049 RVA: 0x000E7A91 File Offset: 0x000E5C91
	public override string name
	{
		get
		{
			return this.getName(false);
		}
		protected set
		{
			this.data.name = value;
		}
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x000E7AA0 File Offset: 0x000E5CA0
	public string getName(bool pWithMaterial = true)
	{
		if (!string.IsNullOrEmpty(this.data.name))
		{
			return this.data.name;
		}
		string tName;
		string tMaterial;
		ItemTools.getTooltipTitle(this.getAsset(), out tName, out tMaterial);
		return tMaterial + tName;
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000E7AE1 File Offset: 0x000E5CE1
	public string getQualityColor()
	{
		return this.getQuality().getRarityColorHex();
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000E7AF0 File Offset: 0x000E5CF0
	public static string getQualityString(Rarity pRarity)
	{
		string tQuality = "";
		switch (pRarity)
		{
		case Rarity.R0_Normal:
			tQuality = "";
			break;
		case Rarity.R1_Rare:
			tQuality = "_rare";
			break;
		case Rarity.R2_Epic:
			tQuality = "_epic";
			break;
		case Rarity.R3_Legendary:
			tQuality = "_legendary";
			break;
		}
		return tQuality;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000E7B3A File Offset: 0x000E5D3A
	public Sprite getSprite()
	{
		return this._asset.getSprite();
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x000E7B47 File Offset: 0x000E5D47
	public bool hasActor()
	{
		return this.getActor() != null;
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000E7B52 File Offset: 0x000E5D52
	public bool hasCity()
	{
		return this.getCity() != null;
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000E7B5D File Offset: 0x000E5D5D
	public override void Dispose()
	{
		this._asset = null;
		this._texture_id = null;
		this._total_stats.reset();
		this._actor = null;
		this.action_attack_target = null;
		base.Dispose();
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x000E7B8C File Offset: 0x000E5D8C
	private bool isCheatEnabled()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllEquipment) || WorldLawLibrary.world_law_cursed_world.isEnabled();
	}

	// Token: 0x04001324 RID: 4900
	private EquipmentAsset _asset;

	// Token: 0x04001325 RID: 4901
	private string _texture_id;

	// Token: 0x04001326 RID: 4902
	public bool unit_has_it;

	// Token: 0x04001327 RID: 4903
	public bool city_has_it;

	// Token: 0x04001328 RID: 4904
	public bool shouldbe_removed;

	// Token: 0x04001329 RID: 4905
	private Actor _actor;

	// Token: 0x0400132A RID: 4906
	private City _city;

	// Token: 0x0400132B RID: 4907
	private int _item_value;

	// Token: 0x0400132C RID: 4908
	private readonly BaseStats _total_stats = new BaseStats();

	// Token: 0x0400132D RID: 4909
	private Rarity _quality;

	// Token: 0x0400132E RID: 4910
	public AttackAction action_attack_target;
}
