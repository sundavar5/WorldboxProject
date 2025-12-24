using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class WarManager : MetaSystemManager<War, WarData>
{
	// Token: 0x06001D01 RID: 7425 RVA: 0x001045DE File Offset: 0x001027DE
	public WarManager()
	{
		this.type_id = "war";
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x00104607 File Offset: 0x00102807
	protected override void updateDirtyUnits()
	{
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x00104609 File Offset: 0x00102809
	public override void clear()
	{
		base.clear();
		this.warStateChanged();
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x00104618 File Offset: 0x00102818
	public override void checkDeadObjects()
	{
		if (this.Count <= 20)
		{
			return;
		}
		foreach (War tWar in this)
		{
			if (!tWar.isFavorite() && tWar.hasEnded())
			{
				this._end_wars.Add(tWar);
			}
		}
		if (this._end_wars.Count == 0)
		{
			return;
		}
		int tToRemove = this.Count - 20;
		this._end_wars.Sort((War a, War b) => a.data.died_time.CompareTo(b.data.died_time));
		int tLength = Mathf.Min(this._end_wars.Count, tToRemove);
		for (int i = 0; i < tLength; i++)
		{
			War tWar2 = this._end_wars[i];
			this.removeObject(tWar2);
		}
		this._end_wars.Clear();
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x00104708 File Offset: 0x00102908
	public void warStateChanged()
	{
		this.cache_war_check.clear();
		Kingdom.cache_enemy_check.clear();
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x00104720 File Offset: 0x00102920
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		int i = this.list.Count;
		while (i-- > 0)
		{
			this.list[i].update();
		}
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0010475B File Offset: 0x0010295B
	public void stopAllWars()
	{
		this._end_wars.AddRange(this);
		this.endWars(WarWinner.Peace);
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x00104770 File Offset: 0x00102970
	private void endWars(WarWinner pWinner = WarWinner.Nobody)
	{
		if (this._end_wars.Count == 0)
		{
			return;
		}
		foreach (War tWar in this._end_wars)
		{
			this.endWar(tWar, pWinner);
		}
		this._end_wars.Clear();
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x001047E0 File Offset: 0x001029E0
	public bool isInWarWith(Kingdom pKingdom, Kingdom pKingdomTarget)
	{
		long tHashCode = this.cache_war_check.getHash(pKingdom, pKingdomTarget);
		bool tCacheResult;
		if (this.cache_war_check.dict.TryGetValue(tHashCode, out tCacheResult))
		{
			return tCacheResult;
		}
		using (IEnumerator<War> enumerator = pKingdom.getWars(false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isInWarWith(pKingdom, pKingdomTarget))
				{
					this.cache_war_check.dict.Add(tHashCode, true);
					return true;
				}
			}
		}
		this.cache_war_check.dict.Add(tHashCode, false);
		return false;
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x00104880 File Offset: 0x00102A80
	public War getWar(Kingdom pAttacker, Kingdom pDefender, bool pOnlyMain = true)
	{
		foreach (War tWar in this)
		{
			if (!tWar.hasEnded())
			{
				if (tWar.isMainAttacker(pAttacker) && tWar.isMainDefender(pDefender))
				{
					return tWar;
				}
				if (tWar.isMainAttacker(pDefender) && tWar.isMainDefender(pAttacker))
				{
					return tWar;
				}
			}
		}
		if (!pOnlyMain)
		{
			foreach (War tWar2 in this)
			{
				if (!tWar2.hasEnded() && tWar2.isInWarWith(pAttacker, pDefender))
				{
					return tWar2;
				}
			}
		}
		return null;
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x00104944 File Offset: 0x00102B44
	public War getForcedWar(Kingdom pKingdom)
	{
		foreach (War tWar in this)
		{
			if (!tWar.hasEnded() && tWar.getAsset().forced_war && tWar.isMainDefender(pKingdom))
			{
				return tWar;
			}
		}
		return null;
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x001049AC File Offset: 0x00102BAC
	public IEnumerable<War> getWars(Kingdom pKingdom, bool pRandom = false)
	{
		foreach (War tWar in (pRandom ? this.list.LoopRandom<War>() : this))
		{
			if (!tWar.hasEnded() && tWar.hasKingdom(pKingdom))
			{
				yield return tWar;
			}
		}
		IEnumerator<War> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x001049CA File Offset: 0x00102BCA
	public IEnumerable<War> getWars(Alliance pAlliance, bool pRandom = false)
	{
		foreach (War tWar in (pRandom ? this.list.LoopRandom<War>() : this))
		{
			if (!tWar.hasEnded())
			{
				foreach (Kingdom tKingdom in pAlliance.kingdoms_list)
				{
					if (tWar.hasKingdom(tKingdom))
					{
						yield return tWar;
						break;
					}
				}
				List<Kingdom>.Enumerator enumerator2 = default(List<Kingdom>.Enumerator);
			}
		}
		IEnumerator<War> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x001049E8 File Offset: 0x00102BE8
	public bool hasWars(Kingdom pKingdom)
	{
		using (IEnumerator<War> enumerator = this.getWars(pKingdom, false).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				War war = enumerator.Current;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x00104A38 File Offset: 0x00102C38
	public bool hasWars(Alliance pAlliance)
	{
		using (IEnumerator<War> enumerator = this.getWars(pAlliance, false).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				War war = enumerator.Current;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x00104A88 File Offset: 0x00102C88
	public bool haveCommonEnemy(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		bool result;
		using (ListPool<Kingdom> tEnemies = pKingdom1.getEnemiesKingdoms())
		{
			using (ListPool<Kingdom> tEnemies2 = pKingdom2.getEnemiesKingdoms())
			{
				foreach (Kingdom ptr in tEnemies)
				{
					Kingdom tKingdom = ptr;
					foreach (Kingdom ptr2 in tEnemies2)
					{
						Kingdom tKingdom2 = ptr2;
						if (tKingdom == tKingdom2)
						{
							return true;
						}
					}
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x00104B54 File Offset: 0x00102D54
	public War getRandomWarFor(Kingdom pKingdom)
	{
		using (IEnumerator<War> enumerator = this.getWars(pKingdom, true).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00104BA4 File Offset: 0x00102DA4
	public void getWarCities(Kingdom pKingdom, ListPool<City> pCities)
	{
		using (ListPool<Kingdom> tEnemiesKingdoms = this.getEnemiesOf(pKingdom))
		{
			for (int i = 0; i < tEnemiesKingdoms.Count; i++)
			{
				Kingdom tKingdom = tEnemiesKingdoms[i];
				pCities.AddRange(tKingdom.getCities());
			}
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x00104BFC File Offset: 0x00102DFC
	public ListPool<Kingdom> getNeutralKingdoms(Kingdom pKingdom, bool pOnlyWithoutWars = false, bool pOnlyWithoutAlliances = false)
	{
		ListPool<Kingdom> tListKingdoms = new ListPool<Kingdom>(World.world.kingdoms.Count);
		Alliance tAllianceMain = pKingdom.getAlliance();
		foreach (Kingdom iKingdom in World.world.kingdoms)
		{
			if (iKingdom != pKingdom && !this.isInWarWith(pKingdom, iKingdom) && (!pOnlyWithoutWars || !iKingdom.hasEnemies()))
			{
				Alliance tAlliance2 = iKingdom.getAlliance();
				if ((!pOnlyWithoutAlliances || tAlliance2 == null) && !Alliance.isSame(tAllianceMain, tAlliance2))
				{
					tListKingdoms.Add(iKingdom);
				}
			}
		}
		return tListKingdoms;
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x00104C9C File Offset: 0x00102E9C
	public ListPool<Kingdom> getEnemiesOf(Kingdom pKingdom)
	{
		ListPool<Kingdom> tListKingdoms = new ListPool<Kingdom>(World.world.kingdoms.Count);
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom != pKingdom && pKingdom.isEnemy(tKingdom))
			{
				tListKingdoms.Add(tKingdom);
			}
		}
		return tListKingdoms;
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x00104D10 File Offset: 0x00102F10
	public void endWar(War pWar, WarWinner pWinner = WarWinner.Nobody)
	{
		if (!pWar.isAlive())
		{
			return;
		}
		if (pWar.hasEnded())
		{
			return;
		}
		World.world.game_stats.data.peacesMade += 1L;
		World.world.map_stats.peacesMade += 1L;
		pWar.setWinner(pWinner);
		this.warStateChanged();
		WorldLog.logWarEnded(pWar);
		pWar.endForSides(pWinner);
		pWar.data.died_time = World.world.getCurWorldTime();
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x00104D94 File Offset: 0x00102F94
	public War newWar(Kingdom pAttacker, Kingdom pDefender, WarTypeAsset pType)
	{
		World.world.game_stats.data.warsStarted += 1L;
		World.world.map_stats.warsStarted += 1L;
		this.warStateChanged();
		War war = base.newObject();
		war.newWar(pAttacker, pDefender, pType);
		WarData data = war.data;
		Actor king = pAttacker.king;
		data.started_by_actor_name = ((king != null) ? king.getName() : null);
		WarData data2 = war.data;
		Actor king2 = pAttacker.king;
		data2.started_by_actor_id = ((king2 != null) ? king2.getID() : -1L);
		war.data.started_by_kingdom_name = pAttacker.data.name;
		war.data.started_by_kingdom_id = pAttacker.data.id;
		string name_template = pType.name_template;
		Kingdom tKingdomForName;
		if (pType.kingdom_for_name_attacker)
		{
			tKingdomForName = pAttacker;
		}
		else
		{
			tKingdomForName = pDefender;
		}
		string tName = NameGenerator.generateNameFromTemplate(name_template, null, tKingdomForName, false);
		war.setName(tName, true);
		WarManager.checkHappinessFromStartOfWar(pAttacker);
		return war;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x00104E80 File Offset: 0x00103080
	public static void checkHappinessFromStartOfWar(Kingdom pKingdom)
	{
		if (pKingdom.hasCulture() && pKingdom.culture.hasTrait("happiness_from_war"))
		{
			for (int i = 0; i < pKingdom.units.Count; i++)
			{
				Actor tActor = pKingdom.units[i];
				if (tActor.hasCulture() && tActor.culture.hasTrait("happiness_from_war"))
				{
					tActor.changeHappiness("just_started_war", 0);
				}
			}
		}
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x00104EF4 File Offset: 0x001030F4
	public long countActiveWars()
	{
		long tCount = 0L;
		foreach (War war in this.getActiveWars())
		{
			tCount += 1L;
		}
		return tCount;
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x00104F44 File Offset: 0x00103144
	public IEnumerable<War> getActiveWars()
	{
		foreach (War tWar in this)
		{
			if (!tWar.hasEnded())
			{
				yield return tWar;
			}
		}
		IEnumerator<War> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x00104F54 File Offset: 0x00103154
	public override bool hasAny()
	{
		if (this.Count == 0)
		{
			return false;
		}
		using (IEnumerator<War> enumerator = this.getActiveWars().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				War war = enumerator.Current;
				return true;
			}
		}
		return false;
	}

	// Token: 0x040015ED RID: 5613
	public KingdomCheckCache cache_war_check = new KingdomCheckCache();

	// Token: 0x040015EE RID: 5614
	private List<War> _end_wars = new List<War>();
}
