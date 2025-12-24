using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class Family : MetaObject<FamilyData>, ISapient
{
	// Token: 0x1700015E RID: 350
	// (get) Token: 0x0600174A RID: 5962 RVA: 0x000E69F8 File Offset: 0x000E4BF8
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x0600174B RID: 5963 RVA: 0x000E69FB File Offset: 0x000E4BFB
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.families;
		}
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000E6A08 File Offset: 0x000E4C08
	public void newFamily(Actor pActor1, Actor pActor2, WorldTile pTile)
	{
		this.data.species_id = pActor1.asset.id;
		this.generateNewMetaObject();
		this.data.founder_actor_name_1 = pActor1.getName();
		this.data.founder_actor_name_2 = ((pActor2 != null) ? pActor2.getName() : null);
		if (pActor1.hasSubspecies())
		{
			this.data.subspecies_id = pActor1.subspecies.id;
			this.data.subspecies_name = pActor1.subspecies.name;
		}
		FamilyData data = this.data;
		City city = pActor1.city;
		data.founder_city_id = ((city != null) ? city.getID() : -1L);
		FamilyData data2 = this.data;
		City city2 = pActor1.city;
		data2.founder_city_name = ((city2 != null) ? city2.name : null);
		if (pActor1.kingdom.isCiv())
		{
			this.data.founder_kingdom_id = pActor1.kingdom.getID();
			this.data.founder_kingdom_name = pActor1.kingdom.data.name;
		}
		this.data.main_founder_id_1 = pActor1.getID();
		if (pActor2 != null)
		{
			this.data.main_founder_id_2 = pActor2.getID();
		}
		this.generateName(pActor1);
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000E6B34 File Offset: 0x000E4D34
	public bool areMostUnitsHungry()
	{
		if (World.world.getWorldTimeElapsedSince(this._timestamp_hungry_check) < 5f)
		{
			return this._cached_hungry_check_result;
		}
		if (this.countHungry() >= this.countUnits() / 2)
		{
			this._cached_hungry_check_result = true;
		}
		else
		{
			this._cached_hungry_check_result = false;
		}
		this._timestamp_hungry_check = World.world.getCurWorldTime();
		return this._cached_hungry_check_result;
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000E6B95 File Offset: 0x000E4D95
	public bool isFull()
	{
		return base.units.Count > this.getActorAsset().family_limit;
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000E6BB0 File Offset: 0x000E4DB0
	public bool isAlpha(Actor pActor)
	{
		Actor tAlpha = this._cached_alpha;
		return pActor == tAlpha;
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000E6BC8 File Offset: 0x000E4DC8
	private void removeAlpha()
	{
		this.data.alpha_id = -1L;
		this._cached_alpha = null;
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000E6BE0 File Offset: 0x000E4DE0
	public void checkAlpha()
	{
		if (this._cached_alpha != null)
		{
			if (!this._cached_alpha.isAlive())
			{
				this.removeAlpha();
			}
			else if (this._cached_alpha.family != this)
			{
				this.removeAlpha();
			}
		}
		if (this._cached_alpha == null)
		{
			Actor tActor = this.findAlpha();
			if (tActor != null)
			{
				this.setAlpha(tActor, true);
			}
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000E6C38 File Offset: 0x000E4E38
	public Actor getAlpha()
	{
		return this._cached_alpha;
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000E6C40 File Offset: 0x000E4E40
	public Actor findAlpha()
	{
		Actor tResult = null;
		double tBestTime = double.MaxValue;
		for (int i = 0; i < base.units.Count; i++)
		{
			Actor tActor = base.units[i];
			if (tActor.isAlive() && tActor.data.created_time <= tBestTime)
			{
				tBestTime = tActor.data.created_time;
				tResult = tActor;
			}
		}
		return tResult;
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000E6CA1 File Offset: 0x000E4EA1
	private void setAlpha(Actor pActor, bool pNew)
	{
		this.data.alpha_id = pActor.getID();
		if (pNew)
		{
			pActor.changeHappiness("become_alpha", 0);
		}
		this._cached_alpha = pActor;
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000E6CCB File Offset: 0x000E4ECB
	public void saveOriginFamily1(long pFamilyID)
	{
		this.data.original_family_1 = pFamilyID;
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000E6CD9 File Offset: 0x000E4ED9
	public void saveOriginFamily2(long pFamilyID)
	{
		if (pFamilyID == this.data.original_family_1)
		{
			return;
		}
		this.data.original_family_2 = pFamilyID;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000E6CF6 File Offset: 0x000E4EF6
	public IEnumerable<Family> getOriginFamilies()
	{
		if (this.data.original_family_1.hasValue())
		{
			Family tFamily = World.world.families.get(this.data.original_family_1);
			if (!tFamily.isRekt())
			{
				yield return tFamily;
			}
		}
		if (this.data.original_family_2.hasValue())
		{
			Family tFamily2 = World.world.families.get(this.data.original_family_2);
			if (!tFamily2.isRekt())
			{
				yield return tFamily2;
			}
		}
		yield break;
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000E6D08 File Offset: 0x000E4F08
	public override void generateBanner()
	{
		this.data.banner_background_id = AssetManager.family_banners_library.getNewIndexBackground();
		ActorAsset tActorAsset = this.getActorAsset();
		int tIndex;
		if (tActorAsset.family_banner_frame_only_inclusion)
		{
			tIndex = AssetManager.family_banners_library.main.frames.IndexOf(tActorAsset.family_banner_frame_generation_inclusion);
		}
		else
		{
			using (ListPool<string> tFrames = new ListPool<string>(AssetManager.family_banners_library.main.frames))
			{
				string tExclusion = tActorAsset.family_banner_frame_generation_exclusion;
				if (!string.IsNullOrEmpty(tExclusion))
				{
					tFrames.Remove(tExclusion);
				}
				string tInclusion = tActorAsset.family_banner_frame_generation_exclusion;
				if (!string.IsNullOrEmpty(tInclusion))
				{
					tFrames.Remove(tInclusion);
				}
				string tFrame = tFrames.GetRandom<string>();
				tIndex = AssetManager.family_banners_library.main.frames.IndexOf(tFrame);
			}
		}
		this.data.banner_frame_id = tIndex;
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000E6DE4 File Offset: 0x000E4FE4
	public Sprite getSpriteBackground()
	{
		return AssetManager.family_banners_library.getSpriteBackground(this.data.banner_background_id);
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000E6DFB File Offset: 0x000E4FFB
	public Sprite getSpriteFrame()
	{
		return AssetManager.family_banners_library.getSpriteFrame(this.data.banner_frame_id);
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000E6E12 File Offset: 0x000E5012
	public override ActorAsset getActorAsset()
	{
		if (this._cached_species == null)
		{
			this._cached_species = AssetManager.actor_library.get(this.data.species_id);
		}
		return this._cached_species;
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000E6E40 File Offset: 0x000E5040
	public bool isSapient()
	{
		Subspecies tSubspecies = World.world.subspecies.get(this.data.subspecies_id);
		return tSubspecies != null && tSubspecies.isSapient();
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000E6E73 File Offset: 0x000E5073
	public bool isMainFounder(Actor pActor)
	{
		return pActor.data.id == this.data.main_founder_id_1 || pActor.data.id == this.data.main_founder_id_2;
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x000E6EAA File Offset: 0x000E50AA
	public bool hasFounders()
	{
		this.checkFounders();
		return this._founder_1 != null || this._founder_2 != null;
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x000E6EC8 File Offset: 0x000E50C8
	public Actor getRandomFounder()
	{
		this.checkFounders();
		if (!this.hasFounders())
		{
			return null;
		}
		if (this._founder_1 != null && this._founder_2 == null)
		{
			return this._founder_1;
		}
		if (this._founder_2 != null && this._founder_1 == null)
		{
			return this._founder_2;
		}
		if (Randy.randomBool())
		{
			return this._founder_1;
		}
		return this._founder_2;
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000E6F27 File Offset: 0x000E5127
	public Actor getFounderFirst()
	{
		return this._founder_1;
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000E6F2F File Offset: 0x000E512F
	public Actor getFounderSecond()
	{
		return this._founder_2;
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000E6F38 File Offset: 0x000E5138
	private void checkFounders()
	{
		if (this._founders_dirty)
		{
			this.clearFounders();
			this._founders_dirty = false;
			foreach (Actor tActor in base.units)
			{
				if (tActor.data.id == this.data.main_founder_id_1)
				{
					this._founder_1 = tActor;
				}
				if (tActor.data.id == this.data.main_founder_id_2)
				{
					this._founder_2 = tActor;
				}
			}
		}
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000E6FD8 File Offset: 0x000E51D8
	private void clearFounders()
	{
		this._founder_1 = null;
		this._founder_2 = null;
		this._founders_dirty = true;
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000E6FEF File Offset: 0x000E51EF
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.families_colors_library;
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000E6FF6 File Offset: 0x000E51F6
	public bool isSameSpecies(string pAssetID)
	{
		return this.data.species_id == pAssetID;
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000E700C File Offset: 0x000E520C
	private void generateName(Actor pActor)
	{
		string tName = pActor.generateName(MetaType.Family, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = pActor.culture;
		data.name_culture_id = ((culture != null) ? culture.getID() : -1L);
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000E7050 File Offset: 0x000E5250
	public override void loadData(FamilyData pData)
	{
		base.loadData(pData);
		if (this.data.alpha_id.hasValue())
		{
			Actor tLoadedAlpha = World.world.units.get(this.data.alpha_id);
			if (tLoadedAlpha != null)
			{
				this.setAlpha(tLoadedAlpha, false);
			}
		}
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000E709C File Offset: 0x000E529C
	public override void updateDirty()
	{
		base.updateDirty();
		this.clearFounders();
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000E70AC File Offset: 0x000E52AC
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "family");
		this._cached_alpha = null;
		this._cached_species = null;
		this._timestamp_hungry_check = 0.0;
		this._cached_hungry_check_result = false;
		this._founder_1 = null;
		this._founder_2 = null;
		this._founders_dirty = true;
		base.Dispose();
	}

	// Token: 0x04001302 RID: 4866
	private Actor _cached_alpha;

	// Token: 0x04001303 RID: 4867
	private ActorAsset _cached_species;

	// Token: 0x04001304 RID: 4868
	private Actor _founder_1;

	// Token: 0x04001305 RID: 4869
	private Actor _founder_2;

	// Token: 0x04001306 RID: 4870
	private bool _founders_dirty;

	// Token: 0x04001307 RID: 4871
	private double _timestamp_hungry_check;

	// Token: 0x04001308 RID: 4872
	private bool _cached_hungry_check_result;
}
