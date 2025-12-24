using System;
using System.Collections.Generic;
using System.Linq;
using db;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class War : MetaObject<WarData>
{
	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00102F10 File Offset: 0x00101110
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.War;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00102F14 File Offset: 0x00101114
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.wars;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00102F20 File Offset: 0x00101120
	[CanBeNull]
	public Kingdom main_attacker
	{
		get
		{
			return this.getMainAttacker();
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00102F28 File Offset: 0x00101128
	[CanBeNull]
	public Kingdom main_defender
	{
		get
		{
			return this.getMainDefender();
		}
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x00102F30 File Offset: 0x00101130
	[CanBeNull]
	public Kingdom getMainAttacker()
	{
		return World.world.kingdoms.get(this.data.main_attacker) ?? World.world.kingdoms.db_get(this.data.main_attacker);
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00102F6A File Offset: 0x0010116A
	[CanBeNull]
	public Kingdom getMainDefender()
	{
		return World.world.kingdoms.get(this.data.main_defender) ?? World.world.kingdoms.db_get(this.data.main_defender);
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00102FA4 File Offset: 0x001011A4
	public bool isMainAttacker(Kingdom pKingdom)
	{
		return pKingdom.getID() == this.data.main_attacker;
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00102FB9 File Offset: 0x001011B9
	public bool isMainDefender(Kingdom pKingdom)
	{
		return pKingdom.getID() == this.data.main_defender;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00102FCE File Offset: 0x001011CE
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00102FD8 File Offset: 0x001011D8
	public override ColorAsset getColor()
	{
		Kingdom tMainAttacker = this.getMainAttacker();
		if (!tMainAttacker.isRekt())
		{
			return tMainAttacker.getColor();
		}
		using (IEnumerator<Kingdom> enumerator = (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current.getColor();
			}
		}
		Kingdom tMainDefender = this.getMainDefender();
		if (!tMainDefender.isRekt())
		{
			return tMainDefender.getColor();
		}
		using (IEnumerator<Kingdom> enumerator = (this.hasEnded() ? this.getAllDefenders() : this.getDefenders()).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current.getColor();
			}
		}
		return null;
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x001030B0 File Offset: 0x001012B0
	public WarTypeAsset getAsset()
	{
		if (this._asset == null)
		{
			this._asset = AssetManager.war_types_library.get(this.data.war_type);
		}
		return this._asset;
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x001030DC File Offset: 0x001012DC
	public void newWar(Kingdom pAttacker, Kingdom pDefender, WarTypeAsset pAsset)
	{
		this.data.main_attacker = pAttacker.id;
		if (pDefender != null)
		{
			this.data.main_defender = pDefender.id;
		}
		this._asset = pAsset;
		this.data.war_type = pAsset.id;
		this.joinAttackers(pAttacker);
		if (pDefender != null)
		{
			this.joinDefenders(pDefender);
		}
		this.prepare();
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0010313D File Offset: 0x0010133D
	public override void clearLastYearStats()
	{
		base.addRenown(1);
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x00103146 File Offset: 0x00101346
	public override void increaseBirths()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x00103158 File Offset: 0x00101358
	public override void increaseKills()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x0010316C File Offset: 0x0010136C
	public void increaseDeathsDefenders(AttackType pAttackType)
	{
		WarData data = this.data;
		int dead_defenders = data.dead_defenders;
		data.dead_defenders = dead_defenders + 1;
		this.increaseDeaths(pAttackType);
		base.addRenown(1);
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x0010319C File Offset: 0x0010139C
	public void increaseDeathsAttackers(AttackType pAttackType)
	{
		WarData data = this.data;
		int dead_attackers = data.dead_attackers;
		data.dead_attackers = dead_attackers + 1;
		this.increaseDeaths(pAttackType);
		base.addRenown(1);
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x001031CC File Offset: 0x001013CC
	public void leaveWar(Kingdom pKingdom)
	{
		base.addRenown(25);
		this.removeFromWar(pKingdom, true);
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x001031DE File Offset: 0x001013DE
	public void lostWar(Kingdom pKingdom)
	{
		base.addRenown(50);
		this.removeFromWar(pKingdom, false);
		this.update();
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x001031F8 File Offset: 0x001013F8
	internal void removeFromWar(Kingdom pKingdom, bool pInPeace)
	{
		if (this.isAttacker(pKingdom))
		{
			using (HashSet<Kingdom>.Enumerator enumerator = this._hashset_defenders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKDefender = enumerator.Current;
					World.world.diplomacy.getRelation(pKingdom, tKDefender).data.timestamp_last_war_ended = World.world.getCurWorldTime();
				}
				goto IL_B1;
			}
		}
		foreach (Kingdom tKAttacker in this._hashset_attackers)
		{
			World.world.diplomacy.getRelation(pKingdom, tKAttacker).data.timestamp_last_war_ended = World.world.getCurWorldTime();
		}
		IL_B1:
		this.removeAttacker(pKingdom, pInPeace);
		this.removeDefender(pKingdom, pInPeace);
		if (this.isMainAttacker(pKingdom) && !this.trySelectNewMainAttacker())
		{
			World.world.wars.endWar(this, pInPeace ? WarWinner.Peace : WarWinner.Defenders);
		}
		else if (this.isMainDefender(pKingdom) && !this.trySelectNewMainDefender())
		{
			World.world.wars.endWar(this, pInPeace ? WarWinner.Peace : WarWinner.Attackers);
		}
		else
		{
			pKingdom.checkEndWar();
			if (pInPeace)
			{
				pKingdom.madePeace(this);
			}
			else
			{
				pKingdom.lostWar(this);
			}
		}
		this.prepare();
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00103358 File Offset: 0x00101558
	public int countAttackers()
	{
		return this._list_attackers.Count;
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x00103368 File Offset: 0x00101568
	public bool trySelectNewMainAttacker()
	{
		if (this.countAttackers() <= 1)
		{
			return false;
		}
		this._list_attackers.Sort(new Comparison<Kingdom>(KingdomListComponent.sortByArmy));
		foreach (Kingdom tKingdom in this._list_attackers)
		{
			if (tKingdom.id != this.data.main_attacker)
			{
				this.data.main_attacker = tKingdom.id;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x00103404 File Offset: 0x00101604
	public bool trySelectNewMainDefender()
	{
		if (this.countDefenders() <= 1)
		{
			return false;
		}
		this._list_defenders.Sort(new Comparison<Kingdom>(KingdomListComponent.sortByArmy));
		foreach (Kingdom tKingdom in this._list_defenders)
		{
			if (tKingdom.id != this.data.main_defender)
			{
				this.data.main_defender = tKingdom.id;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x001034A0 File Offset: 0x001016A0
	public IEnumerable<Kingdom> getAttackers()
	{
		if (this.hasEnded())
		{
			return this.getHistoricAttackers();
		}
		return this._list_attackers;
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x001034B7 File Offset: 0x001016B7
	public IEnumerable<Kingdom> getHistoricAttackers()
	{
		foreach (long tId in this.data.list_attackers)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x001034C7 File Offset: 0x001016C7
	public IEnumerable<Kingdom> getAllAttackers()
	{
		foreach (Kingdom tKingdom in this.getAttackers())
		{
			yield return tKingdom;
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in this.getPastAttackers())
		{
			yield return tKingdom2;
		}
		enumerator = null;
		foreach (Kingdom tKingdom3 in this.getDiedAttackers())
		{
			yield return tKingdom3;
		}
		enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x001034D7 File Offset: 0x001016D7
	public IEnumerable<Kingdom> getAllDefenders()
	{
		foreach (Kingdom tKingdom in this.getDefenders())
		{
			yield return tKingdom;
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in this.getPastDefenders())
		{
			yield return tKingdom2;
		}
		enumerator = null;
		foreach (Kingdom tKingdom3 in this.getDiedDefenders())
		{
			yield return tKingdom3;
		}
		enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x001034E7 File Offset: 0x001016E7
	public IEnumerable<Kingdom> getPastAttackers()
	{
		foreach (long tId in this.data.past_attackers)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x001034F7 File Offset: 0x001016F7
	public IEnumerable<Kingdom> getDiedAttackers()
	{
		foreach (long tId in this.data.died_attackers)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x00103507 File Offset: 0x00101707
	public IEnumerable<Kingdom> getActiveParties()
	{
		bool tAttackersFirst = Randy.randomBool();
		foreach (Kingdom tKingdom in (tAttackersFirst ? this.getAttackers() : this.getDefenders()))
		{
			if (tKingdom.isAlive())
			{
				yield return tKingdom;
			}
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in (tAttackersFirst ? this.getDefenders() : this.getAttackers()))
		{
			if (tKingdom2.isAlive())
			{
				yield return tKingdom2;
			}
		}
		enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x00103518 File Offset: 0x00101718
	public string getAttackersColorTextString()
	{
		Kingdom tMainAttacker = this.getMainAttacker();
		if (tMainAttacker != null)
		{
			return tMainAttacker.getColor().color_text;
		}
		using (IEnumerator<Kingdom> enumerator = (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current.getColor().color_text;
			}
		}
		return "#F3961F";
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x00103598 File Offset: 0x00101798
	public string getDefendersColorTextString()
	{
		if (this.isTotalWar())
		{
			return "#F3961F";
		}
		Kingdom tMainDefender = this.getMainDefender();
		if (tMainDefender != null)
		{
			return tMainDefender.getColor().color_text;
		}
		return "#F3961F";
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x001035CE File Offset: 0x001017CE
	public int countDefenders()
	{
		if (!this.isTotalWar())
		{
			return this._list_defenders.Count;
		}
		return World.world.kingdoms.Count - 1;
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x001035F5 File Offset: 0x001017F5
	public IEnumerable<Kingdom> getDefenders()
	{
		if (this.hasEnded())
		{
			foreach (Kingdom tKingdom in this.getHistoricDefenders())
			{
				yield return tKingdom;
			}
			IEnumerator<Kingdom> enumerator = null;
			yield break;
		}
		if (!this.isTotalWar())
		{
			foreach (Kingdom tKingdom2 in this._list_defenders)
			{
				yield return tKingdom2;
			}
			List<Kingdom>.Enumerator enumerator2 = default(List<Kingdom>.Enumerator);
		}
		else
		{
			foreach (Kingdom tKingdom3 in World.world.kingdoms)
			{
				if (!this.isMainAttacker(tKingdom3))
				{
					yield return tKingdom3;
				}
			}
			IEnumerator<Kingdom> enumerator = null;
		}
		yield break;
		yield break;
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00103605 File Offset: 0x00101805
	public IEnumerable<Kingdom> getHistoricDefenders()
	{
		foreach (long tId in this.data.list_defenders)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x00103615 File Offset: 0x00101815
	public IEnumerable<Kingdom> getPastDefenders()
	{
		foreach (long tId in this.data.past_defenders)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00103625 File Offset: 0x00101825
	public IEnumerable<Kingdom> getDiedDefenders()
	{
		foreach (long tId in this.data.died_defenders)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				yield return tKingdom;
			}
			else
			{
				DeadKingdom tDeadKingdom = World.world.kingdoms.db_get(tId);
				if (tDeadKingdom != null)
				{
					yield return tDeadKingdom;
				}
			}
		}
		List<long>.Enumerator enumerator = default(List<long>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00103638 File Offset: 0x00101838
	public void update()
	{
		if (this.hasEnded())
		{
			return;
		}
		if (!this.main_attacker.isAlive())
		{
			this.lostWar(this.main_attacker);
			return;
		}
		if (this.isTotalWar())
		{
			if (World.world.kingdoms.Count <= 1)
			{
				World.world.wars.endWar(this, WarWinner.Attackers);
				return;
			}
		}
		else if (!this.main_defender.isAlive())
		{
			this.lostWar(this.main_defender);
			return;
		}
		if (this.getAge() > 10 && !this.isTotalWar())
		{
			if (this.main_attacker.countCities() == 0)
			{
				this.lostWar(this.main_attacker);
				return;
			}
			if (this.main_defender.countCities() == 0)
			{
				this.lostWar(this.main_defender);
				return;
			}
		}
		for (int i = 0; i < this._list_attackers.Count; i++)
		{
			Kingdom tKingdom = this._list_attackers[i];
			if (!tKingdom.isAlive())
			{
				this.lostWar(tKingdom);
				return;
			}
		}
		if (!this.isTotalWar())
		{
			for (int i = 0; i < this._list_defenders.Count; i++)
			{
				Kingdom tKingdom2 = this._list_defenders[i];
				if (!tKingdom2.isAlive())
				{
					this.lostWar(tKingdom2);
					return;
				}
			}
		}
		if (this.isTotalWar())
		{
			if (this._list_attackers.Count == 0 || World.world.kingdoms.Count == 1)
			{
				Debug.LogError("[1] should never happen here");
				return;
			}
		}
		else if (this._list_attackers.Count == 0 || this._list_defenders.Count == 0)
		{
			Debug.LogError("[2] should never happen here");
		}
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x001037B8 File Offset: 0x001019B8
	public bool isAttacker(Kingdom pKingdom)
	{
		return this._hashset_attackers.Contains(pKingdom);
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x001037C6 File Offset: 0x001019C6
	public bool isDefender(Kingdom pKingdom)
	{
		return (this.isTotalWar() && !this.isMainAttacker(pKingdom)) || this._hashset_defenders.Contains(pKingdom);
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x001037E7 File Offset: 0x001019E7
	public List<Kingdom> getOppositeSideKingdom(Kingdom pKingdom)
	{
		if (this.isAttacker(pKingdom))
		{
			return this._list_defenders;
		}
		if (this.isDefender(pKingdom))
		{
			return this._list_attackers;
		}
		return null;
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x0010380A File Offset: 0x00101A0A
	public bool isTotalWar()
	{
		return this.getAsset().total_war;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00103818 File Offset: 0x00101A18
	public bool isInWarWith(Kingdom pKingdom, Kingdom pTarget)
	{
		if (this.isTotalWar())
		{
			return this.isMainAttacker(pKingdom) || this.isMainAttacker(pTarget);
		}
		return (this.isAttacker(pKingdom) && this.isDefender(pTarget)) || (this.isDefender(pKingdom) && this.isAttacker(pTarget));
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0010386E File Offset: 0x00101A6E
	public bool onTheSameSide(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		return (this.isAttacker(pKingdom1) && this.isAttacker(pKingdom2)) || (this.isDefender(pKingdom1) && this.isDefender(pKingdom2));
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x00103899 File Offset: 0x00101A99
	public bool hasKingdom(Kingdom pKingdom)
	{
		return this.isTotalWar() || this.isAttacker(pKingdom) || this.isDefender(pKingdom);
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x001038BC File Offset: 0x00101ABC
	public void joinAttackers(Kingdom pKingdom)
	{
		if (this.data.list_attackers.Contains(pKingdom.id))
		{
			return;
		}
		base.addRenown(5);
		this.data.past_attackers.Remove(pKingdom.id);
		this.data.list_attackers.Add(pKingdom.id);
		this.prepare();
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x0010391C File Offset: 0x00101B1C
	public void joinDefenders(Kingdom pKingdom)
	{
		if (this.isTotalWar())
		{
			return;
		}
		if (this.data.list_defenders.Contains(pKingdom.id))
		{
			return;
		}
		base.addRenown(5);
		this.data.past_defenders.Remove(pKingdom.id);
		this.data.list_defenders.Add(pKingdom.id);
		this.prepare();
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x00103985 File Offset: 0x00101B85
	public override void loadData(WarData pData)
	{
		base.loadData(pData);
		this.prepare();
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x00103994 File Offset: 0x00101B94
	public void prepare()
	{
		this._list_attackers.Clear();
		this._list_defenders.Clear();
		this._hashset_attackers.Clear();
		this._hashset_defenders.Clear();
		if (this.data.died_attackers == null)
		{
			this.data.died_attackers = new List<long>();
		}
		if (this.data.died_defenders == null)
		{
			this.data.died_defenders = new List<long>();
		}
		if (this.data.past_attackers == null)
		{
			this.data.past_attackers = new List<long>();
		}
		if (this.data.past_defenders == null)
		{
			this.data.past_defenders = new List<long>();
		}
		foreach (long tId in this.data.list_attackers)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tId);
			if (tKingdom != null)
			{
				this._list_attackers.Add(tKingdom);
				this._hashset_attackers.Add(tKingdom);
			}
		}
		foreach (long tId2 in this.data.list_defenders)
		{
			Kingdom tKingdom2 = World.world.kingdoms.get(tId2);
			if (tKingdom2 != null)
			{
				this._list_defenders.Add(tKingdom2);
				this._hashset_defenders.Add(tKingdom2);
			}
		}
		World.world.wars.warStateChanged();
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00103B34 File Offset: 0x00101D34
	public int getDeadAttackers()
	{
		return this.data.dead_attackers;
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x00103B41 File Offset: 0x00101D41
	public int getDeadDefenders()
	{
		return this.data.dead_defenders;
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x00103B50 File Offset: 0x00101D50
	public void endForSides(WarWinner pWinner)
	{
		foreach (Kingdom tAttacker in this._hashset_attackers)
		{
			tAttacker.checkEndWar();
			switch (pWinner)
			{
			case WarWinner.Attackers:
				tAttacker.wonWar(this);
				break;
			case WarWinner.Defenders:
				tAttacker.lostWar(this);
				break;
			case WarWinner.Peace:
				tAttacker.madePeace(this);
				break;
			}
		}
		foreach (Kingdom tDefender in this._hashset_defenders)
		{
			tDefender.checkEndWar();
			switch (pWinner)
			{
			case WarWinner.Attackers:
				tDefender.lostWar(this);
				break;
			case WarWinner.Defenders:
				tDefender.wonWar(this);
				break;
			case WarWinner.Peace:
				tDefender.madePeace(this);
				break;
			}
		}
		if (pWinner == WarWinner.Merged)
		{
			return;
		}
		foreach (Kingdom tKAttackers in this._hashset_attackers)
		{
			foreach (Kingdom tKDefenders in this._hashset_defenders)
			{
				World.world.diplomacy.getRelation(tKAttackers, tKDefenders).data.timestamp_last_war_ended = World.world.getCurWorldTime();
			}
		}
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x00103CE4 File Offset: 0x00101EE4
	public int countKingdoms()
	{
		if (this.isTotalWar())
		{
			return World.world.kingdoms.Count;
		}
		return 0 + this.countAttackers() + this.countDefenders();
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x00103D0D File Offset: 0x00101F0D
	public int countCities()
	{
		return this.countAttackersCities() + this.countDefendersCities();
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00103D1C File Offset: 0x00101F1C
	public int countAttackersCities()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()))
		{
			tResult += tKingdom.countCities();
		}
		return tResult;
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x00103D80 File Offset: 0x00101F80
	public int countDefendersCities()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllDefenders() : this.getDefenders()))
		{
			tResult += tKingdom.countCities();
		}
		return tResult;
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x00103DE4 File Offset: 0x00101FE4
	public int countDefendersPopulation()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllDefenders() : this.getDefenders()))
		{
			tResult += tKingdom.getPopulationPeople();
		}
		return tResult;
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00103E48 File Offset: 0x00102048
	public int countDefendersWarriors()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllDefenders() : this.getDefenders()))
		{
			tResult += tKingdom.countTotalWarriors();
		}
		return tResult;
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00103EAC File Offset: 0x001020AC
	public int countDefendersMoney()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllDefenders() : this.getDefenders()))
		{
			tResult += tKingdom.countTotalMoney();
		}
		return tResult;
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x00103F10 File Offset: 0x00102110
	public int countAttackersPopulation()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()))
		{
			tResult += tKingdom.getPopulationPeople();
		}
		return tResult;
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x00103F74 File Offset: 0x00102174
	public int countAttackersWarriors()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()))
		{
			tResult += tKingdom.countTotalWarriors();
		}
		return tResult;
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x00103FD8 File Offset: 0x001021D8
	public int countAttackersMoney()
	{
		int tResult = 0;
		foreach (Kingdom tKingdom in (this.hasEnded() ? this.getAllAttackers() : this.getAttackers()))
		{
			tResult += tKingdom.countTotalMoney();
		}
		return tResult;
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x0010403C File Offset: 0x0010223C
	public int countTotalPopulation()
	{
		return this.countAttackersPopulation() + this.countDefendersPopulation();
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x0010404B File Offset: 0x0010224B
	public int countTotalArmy()
	{
		return this.countAttackersWarriors() + this.countDefendersWarriors();
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x0010405A File Offset: 0x0010225A
	public override int countUnits()
	{
		return this.countTotalPopulation();
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x00104062 File Offset: 0x00102262
	public override IEnumerable<Actor> getUnits()
	{
		foreach (Kingdom tKingdom in this.getAttackers())
		{
			foreach (Actor tActor in tKingdom.getUnits())
			{
				yield return tActor;
			}
			IEnumerator<Actor> enumerator2 = null;
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in this.getDefenders())
		{
			foreach (Actor tActor2 in tKingdom2.getUnits())
			{
				yield return tActor2;
			}
			IEnumerator<Actor> enumerator2 = null;
		}
		enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x00104074 File Offset: 0x00102274
	public override Actor getRandomUnit()
	{
		Actor result;
		using (ListPool<Kingdom> tRandomKingdoms = new ListPool<Kingdom>(this.getActiveParties()))
		{
			foreach (Kingdom kingdom in tRandomKingdoms.LoopRandom<Kingdom>())
			{
				Actor tUnit = kingdom.getRandomUnit();
				if (tUnit != null)
				{
					return tUnit;
				}
			}
			result = null;
		}
		return result;
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x001040F0 File Offset: 0x001022F0
	public override bool isReadyForRemoval()
	{
		return false;
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x001040F3 File Offset: 0x001022F3
	public bool hasEnded()
	{
		return !base.isAlive() || this.hasDied();
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x00104108 File Offset: 0x00102308
	public bool isSameAs(War pWar)
	{
		return !this.hasEnded() && pWar != null && !pWar.hasEnded() && (this._hashset_attackers.SetEquals(pWar._hashset_attackers) || this._hashset_defenders.SetEquals(pWar._hashset_attackers)) && (this._hashset_defenders.SetEquals(pWar._hashset_defenders) || this._hashset_attackers.SetEquals(pWar._hashset_defenders));
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x0010417F File Offset: 0x0010237F
	public int getYearEnded()
	{
		return Date.getYear(this.data.died_time);
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00104191 File Offset: 0x00102391
	public int getYearStarted()
	{
		return Date.getYear(this.data.created_time);
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x001041A3 File Offset: 0x001023A3
	public int getDuration()
	{
		if (this.hasEnded())
		{
			return this.getYearEnded() - this.getYearStarted();
		}
		return Date.getYearsSince(this.data.created_time);
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x001041CB File Offset: 0x001023CB
	public void setWinner(WarWinner pWinner)
	{
		if (pWinner == WarWinner.Nobody)
		{
			return;
		}
		this.data.winner = pWinner;
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x001041E0 File Offset: 0x001023E0
	public void removeAttacker(Kingdom pKingdom, bool pInPeace)
	{
		if (!this.data.list_attackers.Contains(pKingdom.id))
		{
			return;
		}
		this.data.list_attackers.Remove(pKingdom.id);
		if (!pInPeace || !pKingdom.isAlive())
		{
			this.data.died_attackers.Add(pKingdom.id);
			return;
		}
		this.data.past_attackers.Add(pKingdom.id);
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x00104258 File Offset: 0x00102458
	public void removeDefender(Kingdom pKingdom, bool pInPeace)
	{
		if (!this.data.list_defenders.Contains(pKingdom.id))
		{
			return;
		}
		this.data.list_defenders.Remove(pKingdom.id);
		if (!pInPeace || !pKingdom.isAlive())
		{
			this.data.died_defenders.Add(pKingdom.id);
			return;
		}
		this.data.past_defenders.Add(pKingdom.id);
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x001042D0 File Offset: 0x001024D0
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "war");
		this._list_attackers.Clear();
		this._list_defenders.Clear();
		this._hashset_attackers.Clear();
		this._hashset_defenders.Clear();
		this._asset = null;
		base.Dispose();
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x00104328 File Offset: 0x00102528
	public override string ToString()
	{
		string tResult = "War: ";
		tResult += (base.isAlive() ? "alive " : "dead ");
		if (base.isAlive())
		{
			tResult = tResult + base.id.ToString() + " ";
			tResult += " a:";
			tResult += string.Join<long>(",", from tKingdom in this.getAttackers()
			select tKingdom.id);
			tResult += " d:";
			tResult += string.Join<long>(",", from tKingdom in this.getDefenders()
			select tKingdom.id);
			tResult = tResult + " t:" + this.data.war_type;
			tResult = tResult + " w:" + this.data.winner.ToString();
			tResult = tResult + " e:" + this.hasEnded().ToString();
		}
		return tResult;
	}

	// Token: 0x040015D8 RID: 5592
	private readonly List<Kingdom> _list_attackers = new List<Kingdom>();

	// Token: 0x040015D9 RID: 5593
	private readonly List<Kingdom> _list_defenders = new List<Kingdom>();

	// Token: 0x040015DA RID: 5594
	private readonly HashSet<Kingdom> _hashset_attackers = new HashSet<Kingdom>();

	// Token: 0x040015DB RID: 5595
	private readonly HashSet<Kingdom> _hashset_defenders = new HashSet<Kingdom>();

	// Token: 0x040015DC RID: 5596
	private WarTypeAsset _asset;
}
