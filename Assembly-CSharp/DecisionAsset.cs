using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000E2 RID: 226
[Serializable]
public class DecisionAsset : Asset, ILocalizedAsset
{
	// Token: 0x060006AC RID: 1708 RVA: 0x00063C58 File Offset: 0x00061E58
	public virtual Sprite getSprite()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this.cached_sprite;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00063C7C File Offset: 0x00061E7C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isPossible(Actor pActor)
	{
		if (this.only_hungry && !pActor.isHungry())
		{
			return false;
		}
		if (this.only_safe && pActor.isFighting())
		{
			return false;
		}
		if (this.only_herd && !pActor.asset.follow_herd)
		{
			return false;
		}
		if (this.only_adult && !pActor.isAdult())
		{
			return false;
		}
		if (this.only_mob && pActor.isKingdomCiv())
		{
			return false;
		}
		if (this.only_sapient && !pActor.isSapient())
		{
			return false;
		}
		if (this.city_must_be_safe && pActor.inOwnCityBorders())
		{
			ProfessionAsset profession_asset = pActor.profession_asset;
			if (profession_asset != null && profession_asset.can_capture)
			{
				City city = pActor.current_zone.city;
				if (city != null && city.isInDanger())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00063D3C File Offset: 0x00061F3C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isPossible(ref DecisionChecks pChecks)
	{
		return (!this.only_hungry || pChecks.is_hungry) && (!this.only_safe || !pChecks.is_fighting) && (!this.only_herd || pChecks.is_herd) && (!this.only_adult || pChecks.is_adult) && (!this.only_mob || !pChecks.is_civ) && (!this.only_sapient || pChecks.is_sapient) && (!this.city_must_be_safe || !pChecks.can_capture_city || !pChecks.city_is_in_danger);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00063DD0 File Offset: 0x00061FD0
	public string getLocalizedText()
	{
		return this.getLocaleID().Localize();
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00063DE0 File Offset: 0x00061FE0
	public string getLocaleID()
	{
		string tTaskID;
		if (string.IsNullOrEmpty(this.task_id))
		{
			tTaskID = this.id;
		}
		else
		{
			tTaskID = this.task_id;
		}
		return AssetManager.tasks_actor.get(tTaskID).getLocaleID();
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00063E1C File Offset: 0x0006201C
	public string getFiringRate()
	{
		if (this.cooldown <= 0)
		{
			return "N/A";
		}
		float tRate = 1f / (float)this.cooldown;
		if (tRate < 0.1f)
		{
			return string.Format("{0:0.000} Hz", tRate);
		}
		return string.Format("{0:0.00} Hz", tRate);
	}

	// Token: 0x04000751 RID: 1873
	public DecisionAction action_check_launch;

	// Token: 0x04000752 RID: 1874
	public float weight = 1f;

	// Token: 0x04000753 RID: 1875
	[NonSerialized]
	public bool has_weight_custom;

	// Token: 0x04000754 RID: 1876
	public DecisionActionWeight weight_calculate_custom;

	// Token: 0x04000755 RID: 1877
	public string task_id;

	// Token: 0x04000756 RID: 1878
	public int decision_index;

	// Token: 0x04000757 RID: 1879
	public int cooldown;

	// Token: 0x04000758 RID: 1880
	public string path_icon;

	// Token: 0x04000759 RID: 1881
	public NeuroLayer priority;

	// Token: 0x0400075A RID: 1882
	[NonSerialized]
	public int priority_int_cached;

	// Token: 0x0400075B RID: 1883
	public bool cooldown_on_launch_failure;

	// Token: 0x0400075C RID: 1884
	public bool only_special;

	// Token: 0x0400075D RID: 1885
	public bool unique;

	// Token: 0x0400075E RID: 1886
	public bool list_civ;

	// Token: 0x0400075F RID: 1887
	public bool list_baby;

	// Token: 0x04000760 RID: 1888
	public bool list_animal;

	// Token: 0x04000761 RID: 1889
	public bool only_adult;

	// Token: 0x04000762 RID: 1890
	public bool only_mob;

	// Token: 0x04000763 RID: 1891
	public bool only_herd;

	// Token: 0x04000764 RID: 1892
	public bool only_sapient;

	// Token: 0x04000765 RID: 1893
	public bool only_safe;

	// Token: 0x04000766 RID: 1894
	public bool only_hungry;

	// Token: 0x04000767 RID: 1895
	public bool city_must_be_safe;

	// Token: 0x04000768 RID: 1896
	[NonSerialized]
	public Sprite cached_sprite;
}
