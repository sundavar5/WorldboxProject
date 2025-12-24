using System;
using System.Collections.Generic;

// Token: 0x02000194 RID: 404
[Serializable]
public class CultureTrait : BaseTrait<CultureTrait>
{
	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000AC9DC File Offset: 0x000AABDC
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_culture;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000AC9EF File Offset: 0x000AABEF
	public override string typed_id
	{
		get
		{
			return "culture_trait";
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x000AC9F6 File Offset: 0x000AABF6
	protected override IEnumerable<ITraitsOwner<CultureTrait>> getRelatedMetaList()
	{
		return World.world.cultures;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000ACA02 File Offset: 0x000AAC02
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.culture_trait_groups.get(this.group_id);
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x000ACA14 File Offset: 0x000AAC14
	public void addWeaponSpecial(string pID)
	{
		if (this.related_weapons_ids == null)
		{
			this.related_weapons_ids = new List<string>();
		}
		this.related_weapons_ids.Add(pID);
		this.is_weapon_trait = true;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x000ACA3C File Offset: 0x000AAC3C
	public void addWeaponSubtype(string pSubtype)
	{
		if (this.related_weapon_subtype_ids == null)
		{
			this.related_weapon_subtype_ids = new List<string>();
		}
		this.related_weapon_subtype_ids.Add(pSubtype);
		this.is_weapon_trait = true;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000ACA64 File Offset: 0x000AAC64
	public void setTownLayoutPlan(PassableZoneChecker pZoneCheckerDelegate)
	{
		this.town_layout_plan = true;
		this.passable_zone_checker = pZoneCheckerDelegate;
	}

	// Token: 0x04000B5B RID: 2907
	public bool is_weapon_trait;

	// Token: 0x04000B5C RID: 2908
	public List<string> related_weapon_subtype_ids;

	// Token: 0x04000B5D RID: 2909
	public List<string> related_weapons_ids;

	// Token: 0x04000B5E RID: 2910
	public bool town_layout_plan;

	// Token: 0x04000B5F RID: 2911
	public PassableZoneChecker passable_zone_checker;
}
