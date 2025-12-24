using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using db;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class Kingdom : MetaObjectWithTraits<KingdomData, KingdomTrait>
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060017DE RID: 6110 RVA: 0x000E915C File Offset: 0x000E735C
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060017DF RID: 6111 RVA: 0x000E915F File Offset: 0x000E735F
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.kingdoms;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000E916B File Offset: 0x000E736B
	protected override bool track_death_types
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000E9170 File Offset: 0x000E7370
	protected override void recalcBaseStats()
	{
		base.recalcBaseStats();
		this._cached_tax_local = SimGlobals.m.base_tax_rate_local;
		this._cached_tax_tribute = SimGlobals.m.base_tax_rate_tribute;
		foreach (KingdomTrait tTrait in base.getTraits())
		{
			if (tTrait.is_local_tax_trait)
			{
				this._cached_tax_local = tTrait.tax_rate;
			}
			if (tTrait.is_tribute_tax_trait)
			{
				this._cached_tax_tribute = tTrait.tax_rate;
			}
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000E9204 File Offset: 0x000E7404
	protected override AssetLibrary<KingdomTrait> trait_library
	{
		get
		{
			return AssetManager.kingdoms_traits;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060017E3 RID: 6115 RVA: 0x000E920B File Offset: 0x000E740B
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_kingdom_traits;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000E9218 File Offset: 0x000E7418
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x000E9225 File Offset: 0x000E7425
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this.power = 1;
		this.timer_action = 5f;
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x000E923F File Offset: 0x000E743F
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.kingdom_colors_library;
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000E9246 File Offset: 0x000E7446
	public void clearListCities()
	{
		this.cities.Clear();
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000E9253 File Offset: 0x000E7453
	public void clearBuildingList()
	{
		this.buildings.Clear();
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x000E9260 File Offset: 0x000E7460
	public override void increaseDeaths(AttackType pType)
	{
		if (!base.isAlive())
		{
			return;
		}
		base.increaseDeaths(pType);
		if (this.hasAlliance())
		{
			this.getAlliance().increaseDeaths(pType);
		}
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x000E9286 File Offset: 0x000E7486
	public override void increaseKills()
	{
		if (!base.isAlive())
		{
			return;
		}
		base.increaseKills();
		if (this.hasAlliance())
		{
			this.getAlliance().increaseKills();
		}
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x000E92AA File Offset: 0x000E74AA
	public override void increaseBirths()
	{
		if (!base.isAlive())
		{
			return;
		}
		base.increaseBirths();
		if (this.hasAlliance())
		{
			this.getAlliance().increaseBirths();
		}
		base.addRenown(1);
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x000E92D8 File Offset: 0x000E74D8
	public void increaseLeft()
	{
		if (!base.isAlive())
		{
			return;
		}
		KingdomData data = this.data;
		long left = data.left;
		data.left = left + 1L;
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000E9304 File Offset: 0x000E7504
	public void increaseJoined()
	{
		if (!base.isAlive())
		{
			return;
		}
		KingdomData data = this.data;
		long joined = data.joined;
		data.joined = joined + 1L;
		base.addRenown(1);
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000E9338 File Offset: 0x000E7538
	public void increaseMoved()
	{
		if (!base.isAlive())
		{
			return;
		}
		KingdomData data = this.data;
		long moved = data.moved;
		data.moved = moved + 1L;
		base.addRenown(2);
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000E936C File Offset: 0x000E756C
	public void increaseMigrants()
	{
		if (!base.isAlive())
		{
			return;
		}
		KingdomData data = this.data;
		long migrated = data.migrated;
		data.migrated = migrated + 1L;
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x000E9398 File Offset: 0x000E7598
	public long getTotalLeft()
	{
		return this.data.left;
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x000E93A5 File Offset: 0x000E75A5
	public long getTotalJoined()
	{
		return this.data.joined;
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000E93B2 File Offset: 0x000E75B2
	public long getTotalMoved()
	{
		return this.data.moved;
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000E93BF File Offset: 0x000E75BF
	public long getTotalMigrated()
	{
		return this.data.migrated;
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x000E93CC File Offset: 0x000E75CC
	public override bool isReadyForRemoval()
	{
		return this.buildings.Count <= 0 && this.getPopulationTotal() <= 0 && !this.hasCities() && !World.world.projectiles.hasActiveProjectiles(this) && base.isReadyForRemoval();
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x000E941D File Offset: 0x000E761D
	public bool hasBuildings()
	{
		return this.buildings.Count > 0;
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x000E942D File Offset: 0x000E762D
	public void addBuildings(List<Building> pListBuildings)
	{
		this.buildings.AddRange(pListBuildings);
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000E943B File Offset: 0x000E763B
	public void listCity(City pCity)
	{
		this.cities.Add(pCity);
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000E9449 File Offset: 0x000E7649
	public void listBuilding(Building pBuilding)
	{
		this.buildings.Add(pBuilding);
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000E9457 File Offset: 0x000E7657
	public Subspecies getMainSubspecies()
	{
		if (this.hasKing())
		{
			return this.king.subspecies;
		}
		if (base.units.Count == 0)
		{
			return null;
		}
		return base.units[0].subspecies;
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000E948D File Offset: 0x000E768D
	public void createWildKingdom()
	{
		this.asset.default_kingdom_color.initColor();
		this.wild = true;
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000E94A8 File Offset: 0x000E76A8
	public void createAI()
	{
		if (!Globals.AI_TEST_ACTIVE)
		{
			return;
		}
		if (this.ai == null)
		{
			this.ai = new AiSystemKingdom(this);
		}
		this.ai.next_job_delegate = new GetNextJobID(this.getNextJob);
		this.ai.jobs_library = AssetManager.job_kingdom;
		this.ai.task_library = AssetManager.tasks_kingdom;
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000E9508 File Offset: 0x000E7708
	public bool isOpinionTowardsKingdomGood(Kingdom pKingdom)
	{
		return this == pKingdom || World.world.diplomacy.getOpinion(this, pKingdom).total >= 0;
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x000E952C File Offset: 0x000E772C
	public string getNextJob()
	{
		return "kingdom";
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x000E9533 File Offset: 0x000E7733
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isCiv()
	{
		return this.asset.civ;
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x000E9540 File Offset: 0x000E7740
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isMobs()
	{
		return this.asset.mobs;
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000E954D File Offset: 0x000E774D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isNeutral()
	{
		return this.asset.neutral;
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x000E955A File Offset: 0x000E775A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isNature()
	{
		return this.asset.nature;
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000E9567 File Offset: 0x000E7767
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isNomads()
	{
		return this.asset.nomads;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x000E9574 File Offset: 0x000E7774
	public override void save()
	{
		base.save();
		if (this.hasCulture())
		{
			this.data.id_culture = this.culture.id;
		}
		if (this.hasReligion())
		{
			this.data.id_religion = this.religion.id;
		}
		if (this.hasLanguage())
		{
			this.data.id_language = this.language.id;
		}
		if (this.hasKing())
		{
			this.data.kingID = this.king.data.id;
		}
		else
		{
			this.data.kingID = -1L;
		}
		this.data.saved_traits = base.getTraitsAsStrings();
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000E9624 File Offset: 0x000E7824
	public IEnumerable<War> getWars(bool pRandom = false)
	{
		return World.world.wars.getWars(this, pRandom);
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000E9638 File Offset: 0x000E7838
	public bool isAttacker()
	{
		using (IEnumerator<War> enumerator = this.getWars(false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAttacker(this))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000E9690 File Offset: 0x000E7890
	public bool isDefender()
	{
		using (IEnumerator<War> enumerator = this.getWars(false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isDefender(this))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x000E96E8 File Offset: 0x000E78E8
	public bool isInWarWith(Kingdom pKingdom)
	{
		return World.world.wars.isInWarWith(this, pKingdom);
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x000E96FC File Offset: 0x000E78FC
	public bool isInWarOnSameSide(Kingdom pKingdom)
	{
		using (IEnumerator<War> enumerator = this.getWars(false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.onTheSameSide(pKingdom, this))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x000E9754 File Offset: 0x000E7954
	public bool isEnemy(Kingdom pKingdomTarget)
	{
		if (pKingdomTarget == null)
		{
			return true;
		}
		long tHashCode = Kingdom.cache_enemy_check.getHash(this, pKingdomTarget);
		bool tCacheResult;
		if (Kingdom.cache_enemy_check.dict.TryGetValue(tHashCode, out tCacheResult))
		{
			return tCacheResult;
		}
		if (this.isCiv() && pKingdomTarget.isCiv())
		{
			if (pKingdomTarget == this)
			{
				Kingdom.cache_enemy_check.dict[tHashCode] = false;
				return false;
			}
			if (World.world.wars.isInWarWith(this, pKingdomTarget))
			{
				Kingdom.cache_enemy_check.dict[tHashCode] = true;
				return true;
			}
			Kingdom.cache_enemy_check.dict[tHashCode] = false;
			return false;
		}
		else
		{
			if (this.asset.isFoe(pKingdomTarget.asset))
			{
				Kingdom.cache_enemy_check.dict[tHashCode] = true;
				return true;
			}
			Kingdom.cache_enemy_check.dict[tHashCode] = false;
			return false;
		}
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000E9824 File Offset: 0x000E7A24
	public bool isGettingCaptured()
	{
		using (IEnumerator<City> enumerator = this.getCities().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isGettingCaptured())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x0600180B RID: 6155 RVA: 0x000E9878 File Offset: 0x000E7A78
	[Obsolete("use .getColor() instead", false)]
	public ColorAsset kingdomColor
	{
		get
		{
			return this.getColor();
		}
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000E9880 File Offset: 0x000E7A80
	public override ColorAsset getColor()
	{
		if (this.isCiv())
		{
			return base.getColor();
		}
		return this.asset.default_kingdom_color;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000E989C File Offset: 0x000E7A9C
	internal void newCivKingdom(Actor pActor)
	{
		this.asset = AssetManager.kingdoms.get(pActor.asset.kingdom_id_civilization);
		this.data.original_actor_asset = pActor.asset.id;
		string tName = pActor.generateName(MetaType.Kingdom, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = this.culture;
		data.name_culture_id = ((culture != null) ? culture.id : -1L);
		this.generateNewMetaObject();
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x000E9915 File Offset: 0x000E7B15
	public override ActorAsset getActorAsset()
	{
		if (this.hasKing())
		{
			return this.king.getActorAsset();
		}
		return this.getFounderSpecies();
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x000E9931 File Offset: 0x000E7B31
	public ActorAsset getFounderSpecies()
	{
		return AssetManager.actor_library.get(this.data.original_actor_asset);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000E9948 File Offset: 0x000E7B48
	public string getSpecies()
	{
		if (string.IsNullOrEmpty(this.data.original_actor_asset))
		{
			return null;
		}
		ActorAsset tAsset = this.getActorAsset();
		if (tAsset == null)
		{
			return null;
		}
		return tAsset.id;
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000E997C File Offset: 0x000E7B7C
	public void trySetRoyalClan()
	{
		if (this.hasKing() && this.king.hasClan() && this.king.clan.id != this.data.royal_clan_id)
		{
			long tOldClanID = this.data.royal_clan_id;
			Clan tOldClan = World.world.clans.get(tOldClanID);
			if (tOldClan != null && tOldClan.isAlive())
			{
				this.logNewRoyalClanChanged(tOldClan, this.king.clan);
			}
			else if (this.king.clan.getRenown() >= 10)
			{
				this.logNewRoyalClan(this.king.clan);
			}
			this.data.royal_clan_id = this.king.clan.id;
		}
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x000E9A3D File Offset: 0x000E7C3D
	public void checkEndWar()
	{
		this.data.timestamp_last_war = World.world.getCurWorldTime();
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000E9A54 File Offset: 0x000E7C54
	public void madePeace(War pWar)
	{
		int tRenown = (int)((float)pWar.getRenown() * 0.25f);
		base.addRenown(tRenown);
		foreach (Actor actor in this.getUnits())
		{
			actor.madePeace(pWar);
		}
		if (this.hasAlliance())
		{
			this.getAlliance().addRenown(tRenown);
		}
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x000E9ACC File Offset: 0x000E7CCC
	public void wonWar(War pWar)
	{
		base.addRenown(pWar.getRenown());
		foreach (Actor actor in this.getUnits())
		{
			actor.warWon(pWar);
		}
		if (this.hasAlliance())
		{
			this.getAlliance().addRenown(pWar.getRenown());
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000E9B3C File Offset: 0x000E7D3C
	public void lostWar(War pWar)
	{
		int tRenown = (int)((float)pWar.getRenown() * 0.1f);
		base.addRenown(tRenown);
		foreach (Actor actor in this.getUnits())
		{
			actor.warLost(pWar);
		}
		if (this.hasAlliance())
		{
			this.getAlliance().addRenown(tRenown);
		}
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000E9BB4 File Offset: 0x000E7DB4
	internal void updateCiv(float pElapsed)
	{
		if (this.data.timer_new_king > 0f)
		{
			this.data.timer_new_king -= pElapsed;
		}
		if (this.ai != null)
		{
			if (this.timer_action > 0f)
			{
				this.timer_action -= pElapsed;
				return;
			}
			this.ai.update();
		}
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000E9C18 File Offset: 0x000E7E18
	public void setCapital(City pCity)
	{
		this.capital = pCity;
		if (this.capital != null && this.capital.isAlive())
		{
			this.data.capitalID = (this.data.last_capital_id = pCity.data.id);
			this.location = this.capital.city_center;
			return;
		}
		this.data.capitalID = -1L;
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x000E9C8C File Offset: 0x000E7E8C
	public void setKing(Actor pActor, bool pFromLoad = false)
	{
		this.king = pActor;
		this.king.setProfession(UnitProfession.King, true);
		if (!pFromLoad)
		{
			this.data.total_kings++;
			this.addRuler(pActor);
			this.data.timestamp_king_rule = World.world.getCurWorldTime();
			this.king.changeHappiness("become_king", 0);
		}
		this.trySetRoyalClan();
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x000E9CF7 File Offset: 0x000E7EF7
	internal void kingLeftEvent()
	{
		if (!this.hasKing())
		{
			return;
		}
		if (this.king.isAlive())
		{
			this.king.changeHappiness("lost_crown", 0);
		}
		this.logKingLeft(this.king);
		this.removeKing();
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000E9D34 File Offset: 0x000E7F34
	internal void kingFledCity()
	{
		if (!this.hasKing())
		{
			return;
		}
		if (this.king.city.isCapitalCity())
		{
			this.logKingFledCapital(this.king);
		}
		else
		{
			this.logKingFledCity(this.king);
		}
		this.king.setCity(null);
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000E9D84 File Offset: 0x000E7F84
	internal void removeKing()
	{
		if (!this.king.isRekt())
		{
			this.king.setProfession(UnitProfession.Unit, true);
		}
		this.rulerLeft();
		this.king = null;
		this.data.timer_new_king = Randy.randomFloat(5f, 20f);
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000E9DD4 File Offset: 0x000E7FD4
	public void updateRulers()
	{
		if (this.data.past_rulers == null)
		{
			return;
		}
		if (this.data.past_rulers.Count == 0)
		{
			return;
		}
		foreach (LeaderEntry tEntry in this.data.past_rulers)
		{
			Actor tRuler = World.world.units.get(tEntry.id);
			if (!tRuler.isRekt())
			{
				tEntry.name = tRuler.name;
			}
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000E9E70 File Offset: 0x000E8070
	public void addRuler(Actor pActor)
	{
		KingdomData data = this.data;
		if (data.past_rulers == null)
		{
			data.past_rulers = new List<LeaderEntry>();
		}
		this.rulerLeft();
		this.data.past_rulers.Add(new LeaderEntry
		{
			id = pActor.getID(),
			name = pActor.name,
			color_id = this.data.color_id,
			timestamp_ago = World.world.getCurWorldTime()
		});
		if (this.data.past_rulers.Count > 30)
		{
			this.data.past_rulers.Shift<LeaderEntry>();
		}
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x000E9F10 File Offset: 0x000E8110
	public void rulerLeft()
	{
		if (this.data.past_rulers == null)
		{
			return;
		}
		if (this.data.past_rulers.Count == 0)
		{
			return;
		}
		LeaderEntry tLast = this.data.past_rulers.Last<LeaderEntry>();
		if (tLast.timestamp_end >= tLast.timestamp_ago)
		{
			return;
		}
		tLast.timestamp_end = World.world.getCurWorldTime();
		this.updateRulers();
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000E9F74 File Offset: 0x000E8174
	public void logKingDead(Actor pActor)
	{
		if (!pActor.attackedBy.isRekt() && pActor.attackedBy.isActor())
		{
			WorldLog.logKingMurder(this, pActor, pActor.attackedBy.a);
			return;
		}
		WorldLog.logKingDead(this, pActor);
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000E9FAA File Offset: 0x000E81AA
	public void logKingFledCapital(Actor pActor)
	{
		WorldLog.logKingFledCapital(this, pActor);
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000E9FB3 File Offset: 0x000E81B3
	public void logKingFledCity(Actor pActor)
	{
		WorldLog.logKingFledCity(this, pActor);
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000E9FBC File Offset: 0x000E81BC
	public void logKingLeft(Actor pActor)
	{
		WorldLog.logKingLeft(this, pActor);
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000E9FC5 File Offset: 0x000E81C5
	public void logNewRoyalClanChanged(Clan pOldClan, Clan pNewClan)
	{
		WorldLog.logRoyalClanChanged(this, pOldClan, pNewClan);
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000E9FCF File Offset: 0x000E81CF
	public void logNewRoyalClan(Clan pClan)
	{
		WorldLog.logRoyalClanNew(this, pClan);
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000E9FD8 File Offset: 0x000E81D8
	public void logRoyalClanLost(Clan pClan)
	{
		WorldLog.logRoyalClanNoMore(this, pClan);
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000E9FE1 File Offset: 0x000E81E1
	internal void checkClearCapital(City pCity)
	{
		if (pCity.isCapitalCity())
		{
			this.clearCapital();
		}
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000E9FF1 File Offset: 0x000E81F1
	public void clearCapital()
	{
		this.data.capitalID = -1L;
		this.capital = null;
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000EA008 File Offset: 0x000E8208
	public bool hasNearbyKingdoms()
	{
		using (IEnumerator<City> enumerator = this.getCities().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.neighbours_kingdoms.Count > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x000EA064 File Offset: 0x000E8264
	public void capturedFrom(Kingdom pKingdom)
	{
		World.world.diplomacy.getRelation(this, pKingdom);
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000EA078 File Offset: 0x000E8278
	public virtual string getMotto()
	{
		if (string.IsNullOrEmpty(this.data.motto))
		{
			this.data.motto = NameGenerator.getName("kingdom_mottos", ActorSex.Male, false, null, null, false);
		}
		return this.data.motto;
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000EA0C4 File Offset: 0x000E82C4
	public override void generateBanner()
	{
		BannerAsset tAsset = AssetManager.kingdom_banners_library.get(this.getActorAsset().banner_id);
		this.data.banner_icon_id = Randy.randomInt(0, tAsset.icons.Count);
		this.data.banner_background_id = Randy.randomInt(0, tAsset.backgrounds.Count);
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000EA120 File Offset: 0x000E8320
	public override void loadData(KingdomData pData)
	{
		base.loadData(pData);
		if (this.data.id_culture.hasValue())
		{
			this.setCulture(World.world.cultures.get(this.data.id_culture));
		}
		if (this.data.id_language.hasValue())
		{
			this.setLanguage(World.world.languages.get(this.data.id_language));
		}
		if (this.data.id_religion.hasValue())
		{
			this.setReligion(World.world.religions.get(this.data.id_religion));
		}
		ActorAsset tAsset = this.getActorAsset();
		this.asset = AssetManager.kingdoms.get(tAsset.kingdom_id_civilization);
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000EA1E8 File Offset: 0x000E83E8
	internal void load2()
	{
		City tLoadedCapital = World.world.cities.get(this.data.capitalID);
		if (tLoadedCapital != null)
		{
			this.setCapital(tLoadedCapital);
		}
		if (this.data.kingID.hasValue())
		{
			Actor tLoadedUnitForKing = World.world.units.get(this.data.kingID);
			if (tLoadedUnitForKing != null)
			{
				this.setKing(tLoadedUnitForKing, true);
				tLoadedUnitForKing.setProfession(UnitProfession.King, true);
			}
		}
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x000EA25C File Offset: 0x000E845C
	public override bool updateColor(ColorAsset pColor)
	{
		bool tResult = base.updateColor(pColor);
		if (tResult)
		{
			foreach (Building building in this.buildings)
			{
				building.updateKingdomColors();
			}
		}
		return tResult;
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x000EA2B8 File Offset: 0x000E84B8
	public static float distanceBetweenKingdom(Kingdom pKingdom, Kingdom pTarget)
	{
		if (!pKingdom.hasCities() || !pTarget.hasCities())
		{
			return -1f;
		}
		float tBestFastDist = float.MaxValue;
		float result;
		using (ListPool<Vector2> tKingdomCenters = new ListPool<Vector2>())
		{
			using (ListPool<Vector2> tTargetCenters = new ListPool<Vector2>())
			{
				foreach (City tCity in pKingdom.getCities())
				{
					tKingdomCenters.Add(tCity.city_center);
				}
				foreach (City tCity2 in pTarget.getCities())
				{
					tTargetCenters.Add(tCity2.city_center);
				}
				foreach (Vector2 ptr in tKingdomCenters)
				{
					Vector2 tCity3 = ptr;
					foreach (Vector2 ptr2 in tTargetCenters)
					{
						Vector2 tCity4 = ptr2;
						float tFastDist = Toolbox.SquaredDistVec2Float(tCity3, tCity4);
						if (tFastDist < tBestFastDist)
						{
							tBestFastDist = tFastDist;
						}
					}
				}
				result = tBestFastDist;
			}
		}
		return result;
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000EA438 File Offset: 0x000E8638
	public override IEnumerable<City> getCities()
	{
		if (World.world.kingdoms.hasDirtyCities())
		{
			foreach (City tCity in World.world.cities)
			{
				if (!tCity.isRekt() && tCity.kingdom == this)
				{
					yield return tCity;
				}
			}
			IEnumerator<City> enumerator = null;
		}
		else
		{
			foreach (City tCity2 in this.cities)
			{
				if (!tCity2.isRekt())
				{
					yield return tCity2;
				}
			}
			List<City>.Enumerator enumerator2 = default(List<City>.Enumerator);
		}
		yield break;
		yield break;
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000EA448 File Offset: 0x000E8648
	public void clear()
	{
		this.buildings.Clear();
		this.cities.Clear();
		base.units.Clear();
		Kingdom.cache_enemy_check.clear();
		this.clearCapital();
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000EA47C File Offset: 0x000E867C
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "kingdom");
		this.clear();
		this.asset = null;
		this.king = null;
		this.capital = null;
		this.culture = null;
		this.language = null;
		this.religion = null;
		AiSystemKingdom aiSystemKingdom = this.ai;
		if (aiSystemKingdom != null)
		{
			aiSystemKingdom.reset();
		}
		base.Dispose();
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000EA4E0 File Offset: 0x000E86E0
	public bool hasEnemies()
	{
		return World.world.wars.hasWars(this);
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000EA4F2 File Offset: 0x000E86F2
	public ListPool<Kingdom> getEnemiesKingdoms()
	{
		return World.world.wars.getEnemiesOf(this);
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000EA504 File Offset: 0x000E8704
	public void makeSurvivorsToNomads()
	{
		if (base.units.Count == 0)
		{
			return;
		}
		for (int i = 0; i < base.units.Count; i++)
		{
			Actor tActor = base.units[i];
			if (tActor.isAlive())
			{
				if (tActor.asset.is_boat)
				{
					tActor.getHitFullHealth(AttackType.None);
				}
				else
				{
					tActor.cancelAllBeh();
					tActor.removeFromPreviousFaction();
					tActor.joinKingdom(World.world.kingdoms_wild.get(tActor.asset.kingdom_id_wild));
				}
			}
		}
		base.units.Clear();
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000EA598 File Offset: 0x000E8798
	public void clearKingData()
	{
		this.king = null;
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000EA5A1 File Offset: 0x000E87A1
	public void updateAge()
	{
		if (this.hasKing() && this.king.hasClan())
		{
			this.king.clan.addRenown(1);
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000EA5CC File Offset: 0x000E87CC
	public override int countCouples()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countCouples();
		}
		return tResult;
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000EA620 File Offset: 0x000E8820
	public override int countSingleMales()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countSingleMales();
		}
		return tResult;
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000EA674 File Offset: 0x000E8874
	public override int countSingleFemales()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countSingleFemales();
		}
		return tResult;
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x000EA6C8 File Offset: 0x000E88C8
	public int countZones()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countZones();
		}
		return tResult;
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000EA71C File Offset: 0x000E891C
	public int countBuildings()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countBuildings();
		}
		return tResult;
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000EA770 File Offset: 0x000E8970
	public int countCities()
	{
		if (!World.world.kingdoms.hasDirtyCities())
		{
			return this.cities.Count;
		}
		int tResult = 0;
		foreach (City city in this.getCities())
		{
			tResult++;
		}
		return tResult;
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000EA7DC File Offset: 0x000E89DC
	public override int getPopulationPeople()
	{
		if (!this._has_boats)
		{
			return base.units.Count;
		}
		int tResult = 0;
		int tBoats = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.getPopulationPeople();
			tBoats += tCity.countBoats();
		}
		if (tResult + tBoats == base.units.Count)
		{
			return tResult;
		}
		tResult = 0;
		using (IEnumerator<Actor> enumerator2 = this.getUnits().GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (!enumerator2.Current.asset.is_boat)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000EA8AC File Offset: 0x000E8AAC
	public override int countUnits()
	{
		return this.getPopulationPeople();
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x000EA8B4 File Offset: 0x000E8AB4
	public override IEnumerable<Actor> getUnits()
	{
		foreach (Actor tActor in base.units)
		{
			if (tActor.isAlive() && !tActor.asset.is_boat && tActor.kingdom == this)
			{
				yield return tActor;
			}
		}
		List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x000EA8C4 File Offset: 0x000E8AC4
	public override Actor getRandomUnit()
	{
		foreach (Actor tActor in base.units.LoopRandom<Actor>())
		{
			if (tActor.isAlive() && !tActor.asset.is_boat && tActor.kingdom == this)
			{
				return tActor;
			}
		}
		return null;
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x000EA934 File Offset: 0x000E8B34
	public int getPopulationTotal()
	{
		return base.units.Count;
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x000EA944 File Offset: 0x000E8B44
	public int countBoats()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countBoats();
		}
		return tResult;
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x000EA998 File Offset: 0x000E8B98
	public int getPopulationTotalPossible()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.getPopulationMaximum();
		}
		return tResult;
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000EA9EC File Offset: 0x000E8BEC
	public int countWeapons()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countWeapons();
		}
		return tResult;
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x000EAA40 File Offset: 0x000E8C40
	public int countTotalFood()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.getTotalFood();
		}
		return tResult;
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000EAA94 File Offset: 0x000E8C94
	public int countTotalWarriors()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.countWarriors();
		}
		return tResult;
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000EAAE8 File Offset: 0x000E8CE8
	public int countWarriorsMax()
	{
		int tResult = 0;
		foreach (City tCity in this.getCities())
		{
			tResult += tCity.getMaxWarriors();
		}
		return tResult;
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000EAB3C File Offset: 0x000E8D3C
	public int getMaxCities()
	{
		int tResult = this.getActorAsset().civ_base_cities;
		if (this.hasKing())
		{
			tResult += (int)this.king.stats["cities"];
		}
		if (tResult < 1)
		{
			tResult = 1;
		}
		return tResult;
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000EAB80 File Offset: 0x000E8D80
	public bool diceAgressionSuccess()
	{
		if (!this.hasKing())
		{
			return false;
		}
		int tCountCities = this.countCities();
		return tCountCities < this.getMaxCities() || (tCountCities >= this.getMaxCities() && Randy.randomChance(this.king.stats["personality_aggression"]));
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x000EABD1 File Offset: 0x000E8DD1
	public bool isSupreme()
	{
		return DiplomacyManager.kingdom_supreme == this;
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000EABDB File Offset: 0x000E8DDB
	public bool isSecondBest()
	{
		return DiplomacyManager.kingdom_second == this;
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000EABE5 File Offset: 0x000E8DE5
	public bool hasAlliance()
	{
		return this.getAlliance() != null;
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000EABF0 File Offset: 0x000E8DF0
	public Alliance getAlliance()
	{
		if (!this.data.allianceID.hasValue())
		{
			return null;
		}
		Alliance alliance = World.world.alliances.get(this.data.allianceID);
		if (alliance == null)
		{
			this.data.allianceID = -1L;
		}
		return alliance;
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000EAC30 File Offset: 0x000E8E30
	public void allianceLeave(Alliance pAlliance)
	{
		this.data.allianceID = -1L;
		this.data.timestamp_alliance = World.world.getCurWorldTime();
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000EAC54 File Offset: 0x000E8E54
	public void allianceJoin(Alliance pAlliance)
	{
		this.data.allianceID = pAlliance.data.id;
		this.data.timestamp_alliance = World.world.getCurWorldTime();
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x000EAC84 File Offset: 0x000E8E84
	public void calculateNeighbourCities()
	{
		foreach (City city in this.getCities())
		{
			city.recalculateNeighbourCities();
		}
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x000EACD0 File Offset: 0x000E8ED0
	public Culture getCulture()
	{
		return this.culture;
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x000EACD8 File Offset: 0x000E8ED8
	public void setCulture(Culture pCulture)
	{
		if (this.culture == pCulture)
		{
			return;
		}
		this.culture = pCulture;
		World.world.cultures.setDirtyKingdoms();
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x000EACFA File Offset: 0x000E8EFA
	public bool hasCulture()
	{
		if (this.culture != null && !this.culture.isAlive())
		{
			this.setCulture(null);
		}
		return this.culture != null;
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000EAD21 File Offset: 0x000E8F21
	public void setLanguage(Language pLanguage)
	{
		this.language = pLanguage;
		World.world.languages.setDirtyKingdoms();
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000EAD39 File Offset: 0x000E8F39
	public Language getLanguage()
	{
		return this.language;
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x000EAD41 File Offset: 0x000E8F41
	public bool hasLanguage()
	{
		if (this.language != null && !this.language.isAlive())
		{
			this.setLanguage(null);
		}
		return this.language != null;
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000EAD68 File Offset: 0x000E8F68
	public void setReligion(Religion pReligion)
	{
		if (this.religion == pReligion)
		{
			return;
		}
		this.religion = pReligion;
		World.world.religions.setDirtyKingdoms();
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000EAD8A File Offset: 0x000E8F8A
	public Religion getReligion()
	{
		return this.religion;
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000EAD92 File Offset: 0x000E8F92
	public bool hasReligion()
	{
		if (this.religion != null && !this.religion.isAlive())
		{
			this.setReligion(null);
		}
		return this.religion != null;
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000EADBC File Offset: 0x000E8FBC
	public bool isEnemyAroundZone(TileZone pZone)
	{
		foreach (TileZone tZone in pZone.neighbours)
		{
			if (tZone.city == null)
			{
				return true;
			}
			Kingdom tKingdom = tZone.city.kingdom;
			if (tKingdom != this)
			{
				return true;
			}
			if (tKingdom != this && tKingdom.isEnemy(this))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000EAE10 File Offset: 0x000E9010
	public override bool hasCities()
	{
		using (IEnumerator<City> enumerator = this.getCities().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				City city = enumerator.Current;
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000EAE60 File Offset: 0x000E9060
	public bool hasCapital()
	{
		return this.capital != null;
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000EAE6B File Offset: 0x000E906B
	public bool hasKing()
	{
		if (this.king == null)
		{
			return false;
		}
		if (!this.king.isAlive())
		{
			this.removeKing();
			return false;
		}
		return true;
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000EAE8D File Offset: 0x000E908D
	public void affectKingByPowers()
	{
		if (!this.hasKing())
		{
			return;
		}
		this.king.addStatusEffect("voices_in_my_head", 0f, true);
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000EAEB0 File Offset: 0x000E90B0
	public int countUnhappyCities()
	{
		int tResult = 0;
		using (IEnumerator<City> enumerator = this.getCities().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isHappy())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000EAF04 File Offset: 0x000E9104
	public Sprite getSpeciesIcon()
	{
		return this.getActorAsset().getSpriteIcon();
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x000EAF11 File Offset: 0x000E9111
	public Sprite getElementIcon()
	{
		return AssetManager.kingdom_banners_library.getSpriteIcon(this.data.banner_icon_id, this.getActorAsset().banner_id);
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000EAF33 File Offset: 0x000E9133
	public Sprite getElementBackground()
	{
		return AssetManager.kingdom_banners_library.getSpriteBackground(this.data.banner_background_id, this.getActorAsset().banner_id);
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x000EAF58 File Offset: 0x000E9158
	public void increaseHappinessFromNewCityCapture()
	{
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.hasHappinessEntry("was_conquered", 400f))
			{
				tActor.changeHappiness("conquered_city", 0);
			}
		}
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x000EAFC0 File Offset: 0x000E91C0
	public void increaseHappinessFromDestroyingCity()
	{
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.hasHappinessEntry("was_conquered", 400f))
			{
				tActor.changeHappiness("destroyed_city", 0);
			}
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x000EB028 File Offset: 0x000E9228
	public void decreaseHappinessFromLostCityCapture(City pCity)
	{
		foreach (Actor tActor in base.units)
		{
			if (!tActor.hasHappinessEntry("was_conquered", 400f))
			{
				if (pCity.isCapitalCity())
				{
					tActor.changeHappiness("lost_capital", 0);
				}
				else
				{
					tActor.changeHappiness("lost_city", 0);
				}
			}
		}
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x000EB0AC File Offset: 0x000E92AC
	public void decreaseHappinessFromRazedCity(City pCity)
	{
		foreach (Actor tActor in base.units)
		{
			if (!tActor.hasHappinessEntry("was_conquered", 400f))
			{
				if (pCity.isCapitalCity())
				{
					tActor.changeHappiness("razed_capital", 0);
				}
				else
				{
					tActor.changeHappiness("razed_city", 0);
				}
			}
		}
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x000EB130 File Offset: 0x000E9330
	public int getLootMin()
	{
		return 5;
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x000EB133 File Offset: 0x000E9333
	public float getTaxRateTribute()
	{
		return this._cached_tax_tribute;
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x000EB13B File Offset: 0x000E933B
	public float getTaxRateLocal()
	{
		return this._cached_tax_local;
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x000EB143 File Offset: 0x000E9343
	public void copyMetasFromOtherKingdom(Kingdom pKingdom)
	{
		if (pKingdom.hasCulture())
		{
			this.setCulture(pKingdom.culture);
		}
		if (pKingdom.hasLanguage())
		{
			this.setLanguage(pKingdom.language);
		}
		if (pKingdom.hasReligion())
		{
			this.setReligion(pKingdom.religion);
		}
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x000EB181 File Offset: 0x000E9381
	public void setUnitMetas(Actor pActor)
	{
		if (pActor.hasCulture())
		{
			this.setCulture(pActor.culture);
		}
		if (pActor.hasLanguage())
		{
			this.setLanguage(pActor.language);
		}
		if (pActor.hasReligion())
		{
			this.setReligion(pActor.religion);
		}
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x000EB1BF File Offset: 0x000E93BF
	public void setCityMetas(City pCity)
	{
		if (pCity.hasCulture())
		{
			this.setCulture(pCity.culture);
		}
		if (pCity.hasLanguage())
		{
			this.setLanguage(pCity.language);
		}
		if (pCity.hasReligion())
		{
			this.setReligion(pCity.religion);
		}
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x000EB1FD File Offset: 0x000E93FD
	public Clan getKingClan()
	{
		if (this.hasKing() && this.king.hasClan())
		{
			return this.king.clan;
		}
		return null;
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x000EB221 File Offset: 0x000E9421
	public override void listUnit(Actor pActor)
	{
		if (pActor.asset.is_boat)
		{
			this._has_boats = true;
		}
		base.listUnit(pActor);
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x000EB23E File Offset: 0x000E943E
	internal override void clearListUnits()
	{
		this._has_boats = false;
		base.clearListUnits();
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000EB250 File Offset: 0x000E9450
	public override string ToString()
	{
		if (this.data == null)
		{
			return "[Kingdom is null]";
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			tBuilder.Append(string.Format("[Kingdom:{0} ", base.id));
			if (!base.isAlive())
			{
				tBuilder.Append("[DEAD] ");
			}
			tBuilder.Append("\"" + this.name + "\" ");
			tBuilder.Append(string.Format("Cities:{0} ", this.cities.Count));
			if (World.world.kingdoms.hasDirtyCities())
			{
				tBuilder.Append(string.Format(" [Dirty:{0}] ", this.countCities()));
			}
			tBuilder.Append(string.Format("Units:{0} ", base.units.Count));
			if (base.isDirtyUnits())
			{
				tBuilder.Append("[Dirty] ");
			}
			if (this.hasKing())
			{
				tBuilder.Append(string.Format("King:{0} ", this.king.id));
			}
			result = tBuilder.ToString().Trim() + "]";
		}
		return result;
	}

	// Token: 0x0400133F RID: 4927
	public static KingdomCheckCache cache_enemy_check = new KingdomCheckCache();

	// Token: 0x04001340 RID: 4928
	public KingdomAsset asset;

	// Token: 0x04001341 RID: 4929
	public bool wild;

	// Token: 0x04001342 RID: 4930
	public float timer_action;

	// Token: 0x04001343 RID: 4931
	public Actor king;

	// Token: 0x04001344 RID: 4932
	public City capital;

	// Token: 0x04001345 RID: 4933
	public Culture culture;

	// Token: 0x04001346 RID: 4934
	public Language language;

	// Token: 0x04001347 RID: 4935
	public Religion religion;

	// Token: 0x04001348 RID: 4936
	public readonly List<Building> buildings = new List<Building>();

	// Token: 0x04001349 RID: 4937
	public readonly List<City> cities = new List<City>();

	// Token: 0x0400134A RID: 4938
	public int power;

	// Token: 0x0400134B RID: 4939
	public AiSystemKingdom ai;

	// Token: 0x0400134C RID: 4940
	public Vector3 location;

	// Token: 0x0400134D RID: 4941
	private float _cached_tax_local;

	// Token: 0x0400134E RID: 4942
	private float _cached_tax_tribute;

	// Token: 0x0400134F RID: 4943
	private bool _has_boats;
}
