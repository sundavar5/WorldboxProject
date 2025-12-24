using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class Alliance : MetaObject<AllianceData>
{
	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000D50DA File Offset: 0x000D32DA
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000D50DE File Offset: 0x000D32DE
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.alliances;
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x000D50EC File Offset: 0x000D32EC
	public void createNewAlliance()
	{
		string tName = NameGenerator.getName("alliance_name", ActorSex.Male, false, null, null, false);
		base.setName(tName, true);
		this.generateNewMetaObject();
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x000D511F File Offset: 0x000D331F
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this.power = 0;
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000D5130 File Offset: 0x000D3330
	public override int countTotalMoney()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countTotalMoney();
		}
		return tResult;
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x000D516C File Offset: 0x000D336C
	public override int countHappyUnits()
	{
		if (this.kingdoms_list.Count == 0)
		{
			return 0;
		}
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countHappyUnits();
		}
		return tResult;
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x000D51B4 File Offset: 0x000D33B4
	public override int countSick()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countSick();
		}
		return tResult;
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x000D51F0 File Offset: 0x000D33F0
	public override int countHungry()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countHungry();
		}
		return tResult;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x000D522C File Offset: 0x000D342C
	public override int countStarving()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countStarving();
		}
		return tResult;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x000D5268 File Offset: 0x000D3468
	public override int countChildren()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countChildren();
		}
		return tResult;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x000D52A4 File Offset: 0x000D34A4
	public override int countAdults()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countAdults();
		}
		return tResult;
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x000D52E0 File Offset: 0x000D34E0
	public override int countHomeless()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countHomeless();
		}
		return tResult;
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x000D5319 File Offset: 0x000D3519
	public override IEnumerable<Family> getFamilies()
	{
		List<Kingdom> tKingdoms = this.kingdoms_list;
		int num;
		for (int i = 0; i < tKingdoms.Count; i = num + 1)
		{
			Kingdom tKingdom = tKingdoms[i];
			foreach (Family tFamily in tKingdom.getFamilies())
			{
				yield return tFamily;
			}
			IEnumerator<Family> enumerator = null;
			num = i;
		}
		yield break;
		yield break;
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x000D532C File Offset: 0x000D352C
	public override bool hasFamilies()
	{
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			if (tKingdoms[i].hasFamilies())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000D5364 File Offset: 0x000D3564
	public override int countMales()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countMales();
		}
		return tResult;
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x000D53A0 File Offset: 0x000D35A0
	public override int countFemales()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countFemales();
		}
		return tResult;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000D53DC File Offset: 0x000D35DC
	public override int countHoused()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countHoused();
		}
		return tResult;
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000D5415 File Offset: 0x000D3615
	public void setType(AllianceType pType)
	{
		this.data.alliance_type = pType;
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x000D5423 File Offset: 0x000D3623
	public bool isForcedType()
	{
		return this.data.alliance_type == AllianceType.Forced;
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000D5433 File Offset: 0x000D3633
	public bool isNormalType()
	{
		return this.data.alliance_type == AllianceType.Normal;
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x000D5443 File Offset: 0x000D3643
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.kingdom_colors_library;
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x000D544C File Offset: 0x000D364C
	public override void generateBanner()
	{
		Sprite[] tBgs = World.world.alliances.getBackgroundsList();
		this.data.banner_background_id = Randy.randomInt(0, tBgs.Length);
		Sprite[] tIcons = World.world.alliances.getIconsList();
		this.data.banner_icon_id = Randy.randomInt(0, tIcons.Length);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000D54A4 File Offset: 0x000D36A4
	public void addFounders(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		this.data.founder_kingdom_name = pKingdom1.data.name;
		this.data.founder_kingdom_id = pKingdom1.getID();
		AllianceData data = this.data;
		Actor king = pKingdom1.king;
		data.founder_actor_name = ((king != null) ? king.getName() : null);
		AllianceData data2 = this.data;
		Actor king2 = pKingdom1.king;
		data2.founder_actor_id = ((king2 != null) ? king2.getID() : -1L);
		this.join(pKingdom1, true, true);
		this.join(pKingdom2, true, true);
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000D5528 File Offset: 0x000D3728
	public void update()
	{
		this.power = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			this.power += tKingdom.power;
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000D5570 File Offset: 0x000D3770
	public bool checkActive()
	{
		bool tChanged = false;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = tKingdoms.Count - 1; i >= 0; i--)
		{
			Kingdom tKingdom = tKingdoms[i];
			if (!tKingdom.isAlive())
			{
				this.leave(tKingdom, false);
				this.kingdoms_list.RemoveAt(i);
				tChanged = true;
			}
		}
		if (tChanged)
		{
			this.recalculate();
		}
		return this.kingdoms_list.Count >= 2;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000D55DC File Offset: 0x000D37DC
	public void dissolve()
	{
		foreach (Kingdom kingdom in this.kingdoms_hashset)
		{
			kingdom.allianceLeave(this);
		}
		this.kingdoms_hashset.Clear();
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000D5638 File Offset: 0x000D3838
	public void recalculate()
	{
		this.kingdoms_list.Clear();
		this.kingdoms_list.AddRange(this.kingdoms_hashset);
		this.mergeWars();
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000D565C File Offset: 0x000D385C
	public bool canJoin(Kingdom pKingdom)
	{
		foreach (Kingdom tAllianceKingdom in this.kingdoms_hashset)
		{
			if (!pKingdom.isOpinionTowardsKingdomGood(tAllianceKingdom))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000D56B8 File Offset: 0x000D38B8
	public bool join(Kingdom pKingdom, bool pRecalc = true, bool pForce = false)
	{
		if (this.hasKingdom(pKingdom))
		{
			return false;
		}
		if (!pForce && !this.canJoin(pKingdom))
		{
			return false;
		}
		this.kingdoms_hashset.Add(pKingdom);
		if (this.hasWars())
		{
			if (this.hasWarsWith(pKingdom))
			{
				foreach (War tWar in this.getAttackerWars())
				{
					if (tWar.isDefender(pKingdom))
					{
						tWar.leaveWar(pKingdom);
					}
				}
				foreach (War tWar2 in this.getDefenderWars())
				{
					if (tWar2.isAttacker(pKingdom))
					{
						tWar2.leaveWar(pKingdom);
					}
				}
			}
			foreach (War war in this.getAttackerWars())
			{
				war.joinAttackers(pKingdom);
			}
			foreach (War tWar3 in this.getDefenderWars())
			{
				if (!tWar3.isTotalWar())
				{
					tWar3.joinDefenders(pKingdom);
				}
			}
		}
		if (pKingdom.hasEnemies())
		{
			foreach (War tWar4 in pKingdom.getWars(false))
			{
				if (!tWar4.isTotalWar())
				{
					if (tWar4.isMainAttacker(pKingdom))
					{
						foreach (Kingdom tKingdom in this.kingdoms_list)
						{
							tWar4.joinAttackers(tKingdom);
						}
					}
					if (tWar4.isMainDefender(pKingdom))
					{
						foreach (Kingdom tKingdom2 in this.kingdoms_list)
						{
							tWar4.joinDefenders(tKingdom2);
						}
					}
				}
			}
		}
		pKingdom.allianceJoin(this);
		if (pRecalc)
		{
			this.recalculate();
		}
		this.data.timestamp_member_joined = World.world.getCurWorldTime();
		return true;
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000D5928 File Offset: 0x000D3B28
	public void leave(Kingdom pKingdom, bool pRecalc = true)
	{
		this.kingdoms_hashset.Remove(pKingdom);
		if (this.hasWars())
		{
			foreach (War tWar in this.getAttackerWars())
			{
				if (!tWar.isMainAttacker(pKingdom))
				{
					tWar.leaveWar(pKingdom);
				}
				else
				{
					foreach (Kingdom tKingdom in this.kingdoms_hashset)
					{
						tWar.leaveWar(tKingdom);
					}
				}
			}
			foreach (War tWar2 in this.getDefenderWars())
			{
				if (!tWar2.isMainDefender(pKingdom))
				{
					tWar2.leaveWar(pKingdom);
				}
				else
				{
					foreach (Kingdom tKingdom2 in this.kingdoms_hashset)
					{
						tWar2.leaveWar(tKingdom2);
					}
				}
			}
		}
		pKingdom.allianceLeave(this);
		if (pRecalc)
		{
			this.recalculate();
		}
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000D5A7C File Offset: 0x000D3C7C
	public override void save()
	{
		base.save();
		this.data.kingdoms = new List<long>();
		foreach (Kingdom tKingdom in this.kingdoms_hashset)
		{
			this.data.kingdoms.Add(tKingdom.id);
		}
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000D5AF4 File Offset: 0x000D3CF4
	public override void loadData(AllianceData pData)
	{
		base.loadData(pData);
		foreach (long tKingdomID in this.data.kingdoms)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tKingdomID);
			if (tKingdom != null)
			{
				this.kingdoms_hashset.Add(tKingdom);
			}
		}
		this.recalculate();
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000D5B74 File Offset: 0x000D3D74
	public int countBuildings()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countBuildings();
		}
		return tResult;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000D5BB0 File Offset: 0x000D3DB0
	public int countZones()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countZones();
		}
		return tResult;
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000D5BE9 File Offset: 0x000D3DE9
	public override int countUnits()
	{
		return this.countPopulation();
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000D5BF4 File Offset: 0x000D3DF4
	public int countPopulation()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.getPopulationPeople();
		}
		return tResult;
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000D5C30 File Offset: 0x000D3E30
	public int countCities()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countCities();
		}
		return tResult;
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x000D5C69 File Offset: 0x000D3E69
	public int countKingdoms()
	{
		return this.kingdoms_hashset.Count;
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x000D5C78 File Offset: 0x000D3E78
	public string getMotto()
	{
		if (string.IsNullOrEmpty(this.data.motto))
		{
			this.data.motto = NameGenerator.getName("alliance_mottos", ActorSex.Male, false, null, null, false);
		}
		return this.data.motto;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x000D5CC4 File Offset: 0x000D3EC4
	public int countWarriors()
	{
		int tResult = 0;
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tResult += tKingdom.countTotalWarriors();
		}
		return tResult;
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x000D5CFD File Offset: 0x000D3EFD
	public static bool isSame(Alliance pAlliance1, Alliance pAlliance2)
	{
		return pAlliance1 != null && pAlliance2 != null && pAlliance1 == pAlliance2;
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000D5D0C File Offset: 0x000D3F0C
	public bool hasWarsWith(Kingdom pKingdom)
	{
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tAllianceKingdom = tKingdoms[i];
			if (pKingdom.isInWarWith(tAllianceKingdom))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000D5D45 File Offset: 0x000D3F45
	public bool hasSupremeKingdom()
	{
		return DiplomacyManager.kingdom_supreme != null && this.hasKingdom(DiplomacyManager.kingdom_supreme);
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x000D5D5B File Offset: 0x000D3F5B
	public bool hasKingdom(Kingdom pKingdom)
	{
		return this.kingdoms_hashset.Contains(pKingdom);
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000D5D6C File Offset: 0x000D3F6C
	public bool hasSharedBordersWithKingdom(Kingdom pKingdom)
	{
		List<Kingdom> tKingdoms = this.kingdoms_list;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			if (DiplomacyHelpers.areKingdomsClose(pKingdom, tKingdom))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000D5DA5 File Offset: 0x000D3FA5
	public bool hasWars()
	{
		return World.world.wars.hasWars(this);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000D5DB7 File Offset: 0x000D3FB7
	public IEnumerable<War> getWars(bool pRandom = false)
	{
		return World.world.wars.getWars(this, pRandom);
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000D5DCC File Offset: 0x000D3FCC
	public void mergeWars()
	{
		if (!this.hasWars())
		{
			return;
		}
		using (ListPool<War> tWars = new ListPool<War>(this.getWars(false)))
		{
			for (int i = 0; i < tWars.Count; i++)
			{
				War tWar = tWars[i];
				if (!tWar.hasEnded())
				{
					for (int j = i + 1; j < tWars.Count; j++)
					{
						War tWar2 = tWars[j];
						if (!tWar2.hasEnded() && tWar.isSameAs(tWar2))
						{
							if (tWar.data.created_time < tWar2.data.created_time)
							{
								World.world.wars.endWar(tWar2, WarWinner.Merged);
							}
							else
							{
								World.world.wars.endWar(tWar, WarWinner.Merged);
							}
							this.mergeWars();
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x000D5EA8 File Offset: 0x000D40A8
	public IEnumerable<War> getAttackerWars()
	{
		foreach (War tWar in this.getWars(false))
		{
			foreach (Kingdom tKingdom in this.kingdoms_list)
			{
				if (tWar.isAttacker(tKingdom))
				{
					yield return tWar;
					break;
				}
			}
			List<Kingdom>.Enumerator enumerator2 = default(List<Kingdom>.Enumerator);
		}
		IEnumerator<War> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x000D5EB8 File Offset: 0x000D40B8
	public IEnumerable<War> getDefenderWars()
	{
		foreach (War tWar in this.getWars(false))
		{
			foreach (Kingdom tKingdom in this.kingdoms_list)
			{
				if (tWar.isDefender(tKingdom))
				{
					yield return tWar;
					break;
				}
			}
			List<Kingdom>.Enumerator enumerator2 = default(List<Kingdom>.Enumerator);
		}
		IEnumerator<War> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000D5EC8 File Offset: 0x000D40C8
	public override IEnumerable<Actor> getUnits()
	{
		List<Kingdom> tKingdoms = this.kingdoms_list;
		int num;
		for (int i = 0; i < tKingdoms.Count; i = num + 1)
		{
			Kingdom tKingdom = tKingdoms[i];
			foreach (Actor tActor in tKingdom.getUnits())
			{
				yield return tActor;
			}
			IEnumerator<Actor> enumerator = null;
			num = i;
		}
		yield break;
		yield break;
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x000D5ED8 File Offset: 0x000D40D8
	public override bool isReadyForRemoval()
	{
		return false;
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000D5EDB File Offset: 0x000D40DB
	public override Actor getRandomUnit()
	{
		return this.kingdoms_list.GetRandom<Kingdom>().getRandomUnit();
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x000D5EED File Offset: 0x000D40ED
	public Sprite getBackgroundSprite()
	{
		return World.world.alliances.getBackgroundsList()[this.data.banner_background_id];
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000D5F0A File Offset: 0x000D410A
	public Sprite getIconSprite()
	{
		return World.world.alliances.getIconsList()[this.data.banner_icon_id];
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000D5F27 File Offset: 0x000D4127
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "alliance");
		this.kingdoms_list.Clear();
		this.kingdoms_hashset.Clear();
		base.Dispose();
	}

	// Token: 0x04001149 RID: 4425
	public List<Kingdom> kingdoms_list = new List<Kingdom>();

	// Token: 0x0400114A RID: 4426
	public HashSet<Kingdom> kingdoms_hashset = new HashSet<Kingdom>();

	// Token: 0x0400114B RID: 4427
	public int power;
}
