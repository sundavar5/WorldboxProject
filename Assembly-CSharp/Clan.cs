using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class Clan : MetaObjectWithTraits<ClanData, ClanTrait>
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x000E2885 File Offset: 0x000E0A85
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06001650 RID: 5712 RVA: 0x000E2888 File Offset: 0x000E0A88
	protected override bool track_death_types
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x000E288B File Offset: 0x000E0A8B
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.clans;
		}
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000E2898 File Offset: 0x000E0A98
	public void newClan(Actor pFounder, bool pAddDefaultTraits)
	{
		this.data.original_actor_asset = pFounder.asset.id;
		this.generateNewMetaObject(pAddDefaultTraits);
		if (pFounder.kingdom.isCiv())
		{
			this.data.founder_kingdom_name = pFounder.kingdom.data.name;
			this.data.founder_kingdom_id = pFounder.kingdom.getID();
		}
		this.data.founder_actor_name = pFounder.getName();
		this.data.founder_actor_id = pFounder.getID();
		ClanData data = this.data;
		City city = pFounder.city;
		data.founder_city_name = ((city != null) ? city.name : null);
		ClanData data2 = this.data;
		City city2 = pFounder.city;
		data2.founder_city_id = ((city2 != null) ? city2.getID() : -1L);
		this.data.creator_subspecies_name = pFounder.subspecies.name;
		this.data.creator_subspecies_id = pFounder.subspecies.getID();
		this.data.creator_species_id = pFounder.asset.id;
		string tNewName = pFounder.generateName(MetaType.Clan, this.getID(), ActorSex.None);
		BaseSystemData data3 = this.data;
		Culture culture = pFounder.culture;
		data3.name_culture_id = ((culture != null) ? culture.getID() : -1L);
		base.setName(tNewName, true);
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000E29D4 File Offset: 0x000E0BD4
	protected override void recalcBaseStats()
	{
		base.recalcBaseStats();
		this.base_stats_female.clear();
		this.base_stats_male.clear();
		foreach (ClanTrait tTrait in base.getTraits())
		{
			this.base_stats_male.mergeStats(tTrait.base_stats_male, 1f);
			this.base_stats_female.mergeStats(tTrait.base_stats_female, 1f);
		}
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000E2A64 File Offset: 0x000E0C64
	public override void increaseBirths()
	{
		base.increaseBirths();
		base.addRenown(1);
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06001655 RID: 5717 RVA: 0x000E2A73 File Offset: 0x000E0C73
	protected override AssetLibrary<ClanTrait> trait_library
	{
		get
		{
			return AssetManager.clan_traits;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06001656 RID: 5718 RVA: 0x000E2A7A File Offset: 0x000E0C7A
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_clan_traits;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06001657 RID: 5719 RVA: 0x000E2A87 File Offset: 0x000E0C87
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06001658 RID: 5720 RVA: 0x000E2A94 File Offset: 0x000E0C94
	protected override string species_id
	{
		get
		{
			return this.data.original_actor_asset;
		}
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000E2AA1 File Offset: 0x000E0CA1
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000E2AA9 File Offset: 0x000E0CA9
	public override void listUnit(Actor pActor)
	{
		base.listUnit(pActor);
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000E2AB2 File Offset: 0x000E0CB2
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.clan_colors_library;
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000E2AB9 File Offset: 0x000E0CB9
	public override void generateBanner()
	{
		this.data.banner_background_id = AssetManager.clan_banners_library.getNewIndexBackground();
		this.data.banner_icon_id = AssetManager.clan_banners_library.getNewIndexIcon();
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000E2AE8 File Offset: 0x000E0CE8
	public string getMotto()
	{
		if (string.IsNullOrEmpty(this.data.motto))
		{
			this.data.motto = NameGenerator.getName("clan_mottos", ActorSex.Male, false, null, null, false);
		}
		return this.data.motto;
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000E2B34 File Offset: 0x000E0D34
	public override void save()
	{
		base.save();
		this.data.saved_traits = base.getTraitsAsStrings();
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000E2B50 File Offset: 0x000E0D50
	public void checkMembersForNewChief()
	{
		if (base.units.Count == 0)
		{
			return;
		}
		if (!this.getChief().isRekt())
		{
			return;
		}
		this.setChief(null);
		Actor tNewChief = this.getNextChief(null);
		if (tNewChief == null)
		{
			return;
		}
		this.setChief(tNewChief);
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000E2B93 File Offset: 0x000E0D93
	public void setChief(Actor pActor)
	{
		if (this.data == null)
		{
			return;
		}
		if (pActor.isRekt())
		{
			this.data.chief_id = -1L;
			this.chiefLeft();
			return;
		}
		this.data.chief_id = pActor.getID();
		this.addChief(pActor);
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x000E2BD4 File Offset: 0x000E0DD4
	public void updateChiefs()
	{
		if (this.data.past_chiefs == null)
		{
			return;
		}
		if (this.data.past_chiefs.Count == 0)
		{
			return;
		}
		foreach (LeaderEntry tEntry in this.data.past_chiefs)
		{
			Actor tRuler = World.world.units.get(tEntry.id);
			if (!tRuler.isRekt())
			{
				tEntry.name = tRuler.name;
			}
		}
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000E2C70 File Offset: 0x000E0E70
	public void addChief(Actor pActor)
	{
		ClanData data = this.data;
		if (data.past_chiefs == null)
		{
			data.past_chiefs = new List<LeaderEntry>();
		}
		this.chiefLeft();
		this.data.past_chiefs.Add(new LeaderEntry
		{
			id = pActor.getID(),
			name = pActor.name,
			color_id = this.data.color_id,
			timestamp_ago = World.world.getCurWorldTime()
		});
		if (this.data.past_chiefs.Count > 30)
		{
			this.data.past_chiefs.Shift<LeaderEntry>();
		}
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000E2D10 File Offset: 0x000E0F10
	public void chiefLeft()
	{
		if (this.data.past_chiefs == null)
		{
			return;
		}
		if (this.data.past_chiefs.Count == 0)
		{
			return;
		}
		LeaderEntry tLast = this.data.past_chiefs.Last<LeaderEntry>();
		if (tLast.timestamp_end >= tLast.timestamp_ago)
		{
			return;
		}
		tLast.timestamp_end = World.world.getCurWorldTime();
		this.updateChiefs();
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x000E2D74 File Offset: 0x000E0F74
	public void tryForgetChief(Actor pActor)
	{
		if (this.data.chief_id != pActor.getID())
		{
			return;
		}
		this.setChief(null);
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x000E2D94 File Offset: 0x000E0F94
	public Culture getClanCulture()
	{
		Culture tCulture = this.getChiefCulture();
		if (tCulture == null && this.data.culture_id.hasValue())
		{
			tCulture = World.world.cultures.get(this.data.culture_id);
		}
		if (tCulture == null)
		{
			this.data.culture_id = -1L;
		}
		else
		{
			this.data.culture_id = tCulture.getID();
		}
		return tCulture;
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000E2DFC File Offset: 0x000E0FFC
	private Culture getChiefCulture()
	{
		Actor tChief = this.getChief();
		if (tChief == null)
		{
			return null;
		}
		return tChief.culture;
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000E2E1C File Offset: 0x000E101C
	public Actor getNextChief(Actor pIgnore = null)
	{
		if (base.isDirtyUnits())
		{
			return null;
		}
		Actor result;
		using (ListPool<Actor> tList = new ListPool<Actor>(base.units))
		{
			ListSorters.sortUnitsSortedByAgeAndTraits(tList, this.getClanCulture());
			Actor tNewChief = null;
			Actor tCurrentChief = this.getChief();
			foreach (Actor ptr in tList)
			{
				Actor tActor = ptr;
				if (tActor != pIgnore && tActor.isAlive() && tCurrentChief != tActor)
				{
					tNewChief = tActor;
					break;
				}
			}
			result = tNewChief;
		}
		return result;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x000E2EC8 File Offset: 0x000E10C8
	public int getMaxMembers()
	{
		return (int)this.base_stats_meta["limit_clan_members"];
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000E2EDC File Offset: 0x000E10DC
	public Actor getChief()
	{
		Actor tChief = null;
		if (this.data.chief_id.hasValue())
		{
			tChief = World.world.units.get(this.data.chief_id);
		}
		return tChief;
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000E2F19 File Offset: 0x000E1119
	public bool hasChief()
	{
		return this.getChief() != null;
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000E2F24 File Offset: 0x000E1124
	public bool isFull()
	{
		int tMax = this.getMaxMembers();
		return tMax != 0 && base.units.Count >= tMax;
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000E2F4E File Offset: 0x000E114E
	public bool fitToRule(Actor pActor, Kingdom pKingdom)
	{
		return pActor.kingdom == pKingdom && pActor.isUnitFitToRule() && !pActor.isKing();
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000E2F70 File Offset: 0x000E1170
	public Sprite getBackgroundSprite()
	{
		return AssetManager.clan_banners_library.getSpriteBackground(this.data.banner_background_id);
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000E2F87 File Offset: 0x000E1187
	public Sprite getIconSprite()
	{
		return AssetManager.clan_banners_library.getSpriteIcon(this.data.banner_icon_id);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000E2F9E File Offset: 0x000E119E
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "clan");
		base.Dispose();
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000E2FB8 File Offset: 0x000E11B8
	public string getTextMaxMembers()
	{
		int tMax = this.getMaxMembers();
		if (tMax == 0)
		{
			return base.units.Count.ToString();
		}
		return string.Format("{0}/{1}", base.units.Count, tMax);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000E3004 File Offset: 0x000E1204
	public override bool hasCities()
	{
		using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getRoyalClan() == this)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x000E305C File Offset: 0x000E125C
	public override IEnumerable<City> getCities()
	{
		foreach (City tCity in World.world.cities)
		{
			if (tCity.getRoyalClan() == this)
			{
				yield return tCity;
			}
		}
		IEnumerator<City> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000E306C File Offset: 0x000E126C
	public override bool hasKingdoms()
	{
		using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getKingClan() == this)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000E30C4 File Offset: 0x000E12C4
	public override IEnumerable<Kingdom> getKingdoms()
	{
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.getKingClan() == this)
			{
				yield return tKingdom;
			}
		}
		IEnumerator<Kingdom> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x0400128D RID: 4749
	public BaseStats base_stats_male = new BaseStats();

	// Token: 0x0400128E RID: 4750
	public BaseStats base_stats_female = new BaseStats();
}
