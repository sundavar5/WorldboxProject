using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class DiplomacyManager : CoreSystemManager<DiplomacyRelation, DiplomacyRelationData>
{
	// Token: 0x06001720 RID: 5920 RVA: 0x000E5BCB File Offset: 0x000E3DCB
	public DiplomacyManager()
	{
		this.type_id = "diplomacy";
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x000E5BEC File Offset: 0x000E3DEC
	public override List<DiplomacyRelationData> save(List<DiplomacyRelation> pList = null)
	{
		List<DiplomacyRelationData> tSavingList = new List<DiplomacyRelationData>();
		foreach (DiplomacyRelation tRel in this)
		{
			tRel.kingdom1 = World.world.kingdoms.get(tRel.data.kingdom1_id);
			tRel.kingdom2 = World.world.kingdoms.get(tRel.data.kingdom2_id);
			if (tRel.kingdom1 != null && tRel.kingdom2 != null)
			{
				tSavingList.Add(tRel.data);
			}
		}
		return tSavingList;
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000E5C90 File Offset: 0x000E3E90
	public override void loadFromSave(List<DiplomacyRelationData> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			DiplomacyRelationData tData = pList[i];
			bool flag = World.world.kingdoms.get(tData.kingdom1_id) != null;
			Kingdom tK2 = World.world.kingdoms.get(tData.kingdom2_id);
			if (flag && tK2 != null)
			{
				if (tData.id == -1L)
				{
					tData.id = World.world.map_stats.getNextId(this.type_id);
				}
				this.loadObject(tData);
			}
		}
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000E5D14 File Offset: 0x000E3F14
	public override DiplomacyRelation loadObject(DiplomacyRelationData pData)
	{
		Kingdom tK = World.world.kingdoms.get(pData.kingdom1_id);
		Kingdom tK2 = World.world.kingdoms.get(pData.kingdom2_id);
		pData.rel_id = pData.kingdom1_id.ToString() + "_" + pData.kingdom2_id.ToString();
		DiplomacyRelation tNewRelation = base.loadObject(pData);
		this._dict.Add(pData.rel_id, tNewRelation);
		tNewRelation.kingdom1 = tK;
		tNewRelation.kingdom2 = tK2;
		return tNewRelation;
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x000E5D9C File Offset: 0x000E3F9C
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (World.world.isPaused())
		{
			return;
		}
		if (this.diplomacyTick > 0f)
		{
			this.diplomacyTick -= pElapsed;
			return;
		}
		if (World.world.cities.isLocked())
		{
			return;
		}
		this.diplomacyTick = 2f;
		this.newDiplomacyTick();
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000E5DFC File Offset: 0x000E3FFC
	public void newDiplomacyTick()
	{
		this.findSupremeKingdom();
		this.checkAchievements();
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000E5E0A File Offset: 0x000E400A
	private void checkAchievements()
	{
		AchievementLibrary.world_war.check(null);
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000E5E18 File Offset: 0x000E4018
	private void findSupremeKingdom()
	{
		DiplomacyManager.kingdom_supreme = null;
		DiplomacyManager.kingdom_second = null;
		if (World.world.kingdoms.Count == 0)
		{
			return;
		}
		List<Kingdom> tKingdoms = DiplomacyManager._kingdom_sorter;
		tKingdoms.AddRange(World.world.kingdoms);
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			Kingdom tKingdom = tKingdoms[i];
			tKingdom.power = tKingdom.countTotalWarriors() * 2 + tKingdom.countCities() * 5 + 1;
		}
		tKingdoms.Sort(new Comparison<Kingdom>(this.sortByPower));
		DiplomacyManager.kingdom_supreme = tKingdoms[0];
		if (tKingdoms.Count > 1)
		{
			DiplomacyManager.kingdom_second = tKingdoms[1];
		}
		else
		{
			DiplomacyManager.kingdom_second = null;
		}
		tKingdoms.Clear();
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000E5ECC File Offset: 0x000E40CC
	public int sortByPower(Kingdom o1, Kingdom o2)
	{
		return o2.power.CompareTo(o1.power);
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000E5EE0 File Offset: 0x000E40E0
	private War startTotalWar(Kingdom pAttacker, WarTypeAsset pType)
	{
		if (World.world.kingdoms.Count == 1)
		{
			return null;
		}
		foreach (War tWar in pAttacker.getWars(false))
		{
			if (tWar.isMainAttacker(pAttacker) && tWar.isTotalWar())
			{
				return null;
			}
		}
		if (pAttacker.hasAlliance())
		{
			pAttacker.getAlliance().leave(pAttacker, true);
		}
		War tNewWar = World.world.wars.newWar(pAttacker, null, pType);
		War result;
		using (ListPool<War> tWars = new ListPool<War>(pAttacker.getWars(false)))
		{
			foreach (War ptr in tWars)
			{
				War tWar2 = ptr;
				if (!tWar2.isTotalWar())
				{
					if (tWar2.isAttacker(pAttacker))
					{
						if (!tWar2.isMainAttacker(pAttacker))
						{
							tWar2.leaveWar(pAttacker);
						}
						else
						{
							World.world.wars.endWar(tWar2, WarWinner.Merged);
						}
					}
					else if (tWar2.isDefender(pAttacker))
					{
						if (!tWar2.isMainDefender(pAttacker))
						{
							tWar2.leaveWar(pAttacker);
						}
						else
						{
							tWar2.lostWar(pAttacker);
						}
					}
				}
			}
			WorldLog.logNewTotalWar(pAttacker);
			result = tNewWar;
		}
		return result;
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000E6048 File Offset: 0x000E4248
	internal War startWar(Kingdom pAttacker, Kingdom pDefender, WarTypeAsset pAsset, bool pLog = true)
	{
		if (pAsset.total_war)
		{
			return this.startTotalWar(pAttacker, pAsset);
		}
		if (pAttacker == pDefender)
		{
			return null;
		}
		if (World.world.wars.getWar(pAttacker, pDefender, true) != null)
		{
			return null;
		}
		if (pLog)
		{
			WorldLog.logNewWar(pAttacker, pDefender);
		}
		War tNewWar = World.world.wars.newWar(pAttacker, pDefender, pAsset);
		if (pAsset.alliance_join)
		{
			Alliance tAllianceAttackers = pAttacker.getAlliance();
			Alliance tAllianceDefenders = pDefender.getAlliance();
			if (tAllianceAttackers != null)
			{
				foreach (Kingdom tKingdom in tAllianceAttackers.kingdoms_hashset)
				{
					tNewWar.joinAttackers(tKingdom);
				}
			}
			if (tAllianceDefenders != null)
			{
				foreach (Kingdom tKingdom2 in tAllianceDefenders.kingdoms_hashset)
				{
					tNewWar.joinDefenders(tKingdom2);
				}
			}
		}
		return tNewWar;
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000E614C File Offset: 0x000E434C
	public void eventSpite(Kingdom pKingdom)
	{
		if (World.world.kingdoms.Count <= 1)
		{
			return;
		}
		using (ListPool<Kingdom> toHighlight = new ListPool<Kingdom>(World.world.kingdoms.Count))
		{
			War tWar = this.startWar(pKingdom, null, WarTypeLibrary.spite, true);
			if (tWar != null)
			{
				pKingdom.affectKingByPowers();
				toHighlight.AddRange(tWar.getAttackers());
				toHighlight.AddRange(tWar.getDefenders());
				foreach (Kingdom ptr in toHighlight)
				{
					EffectsLibrary.highlightKingdomZones(ptr, Color.red, 0.3f);
				}
			}
		}
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x000E6214 File Offset: 0x000E4414
	public void eventFriendship(Kingdom pKingdom)
	{
		War tWar = World.world.wars.getRandomWarFor(pKingdom);
		if (tWar == null)
		{
			return;
		}
		using (ListPool<Kingdom> toHighlight = new ListPool<Kingdom>(World.world.kingdoms.Count))
		{
			if (tWar.isTotalWar() || tWar.isMainAttacker(pKingdom) || tWar.isMainDefender(pKingdom))
			{
				toHighlight.AddRange(tWar.getAttackers());
				toHighlight.AddRange(tWar.getDefenders());
			}
			Alliance tAlliance = pKingdom.getAlliance();
			if (tAlliance == null)
			{
				tWar.leaveWar(pKingdom);
				toHighlight.Add(pKingdom);
				pKingdom.affectKingByPowers();
			}
			else
			{
				foreach (Kingdom tAllianceKingdom in tAlliance.kingdoms_hashset)
				{
					tWar.leaveWar(tAllianceKingdom);
					toHighlight.Add(tAllianceKingdom);
					tAllianceKingdom.affectKingByPowers();
				}
			}
			if (tWar.isTotalWar())
			{
				World.world.wars.endWar(tWar, WarWinner.Peace);
			}
			foreach (Kingdom ptr in toHighlight)
			{
				EffectsLibrary.highlightKingdomZones(ptr, Color.green, 0.3f);
			}
		}
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x000E636C File Offset: 0x000E456C
	public KingdomOpinion getOpinion(Kingdom k1, Kingdom k2)
	{
		return this.getRelation(k1, k2).getOpinion(k1, k2);
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000E6380 File Offset: 0x000E4580
	public int sortID(Kingdom o1, Kingdom o2)
	{
		return o1.id.CompareTo(o2.id);
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x000E63A4 File Offset: 0x000E45A4
	public DiplomacyRelation getRelation(Kingdom pK1, Kingdom pK2)
	{
		Kingdom tOrder;
		Kingdom tOrder2;
		if (pK1.id.CompareTo(pK2.id) > 0)
		{
			tOrder = pK1;
			tOrder2 = pK2;
		}
		else
		{
			tOrder = pK2;
			tOrder2 = pK1;
		}
		string tID = tOrder.id.ToString() + "_" + tOrder2.id.ToString();
		DiplomacyRelation tRelation;
		if (this.tryGet(tID, out tRelation))
		{
			return tRelation;
		}
		tRelation = base.newObject();
		tRelation.data.rel_id = tID;
		this._dict.Add(tID, tRelation);
		tRelation.data.kingdom1_id = tOrder.id;
		tRelation.data.kingdom2_id = tOrder2.id;
		tRelation.kingdom1 = tOrder;
		tRelation.kingdom2 = tOrder2;
		return tRelation;
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x000E645C File Offset: 0x000E465C
	public void removeRelationsFor(Kingdom pKingdom)
	{
		foreach (DiplomacyRelation tRelation in this)
		{
			if (tRelation.kingdom1 == pKingdom || tRelation.kingdom2 == pKingdom)
			{
				DiplomacyManager._relations_remover.Add(tRelation);
			}
		}
		foreach (DiplomacyRelation tRelation2 in DiplomacyManager._relations_remover)
		{
			this.removeObject(tRelation2);
		}
		DiplomacyManager._relations_remover.Clear();
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000E6508 File Offset: 0x000E4708
	public bool tryGet(string pID, out DiplomacyRelation pObject)
	{
		return this._dict.TryGetValue(pID, out pObject);
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x000E6518 File Offset: 0x000E4718
	public DiplomacyRelation get(string pID)
	{
		if (string.IsNullOrEmpty(pID))
		{
			return null;
		}
		DiplomacyRelation tObject;
		this.tryGet(pID, out tObject);
		return tObject;
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x000E653A File Offset: 0x000E473A
	public override void removeObject(DiplomacyRelation pObject)
	{
		this._dict.Remove(pObject.data.rel_id);
		base.removeObject(pObject);
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x000E655A File Offset: 0x000E475A
	public override void clear()
	{
		this.diplomacyTick = 0f;
		DiplomacyManager.kingdom_supreme = null;
		DiplomacyManager.kingdom_second = null;
		this._dict.Clear();
		base.clear();
	}

	// Token: 0x040012DD RID: 4829
	public static Kingdom kingdom_supreme;

	// Token: 0x040012DE RID: 4830
	public static Kingdom kingdom_second;

	// Token: 0x040012DF RID: 4831
	public static List<Kingdom> superpowers = new List<Kingdom>();

	// Token: 0x040012E0 RID: 4832
	private float diplomacyTick;

	// Token: 0x040012E1 RID: 4833
	private static List<Kingdom> _kingdom_sorter = new List<Kingdom>();

	// Token: 0x040012E2 RID: 4834
	private static List<DiplomacyRelation> _relations_remover = new List<DiplomacyRelation>();

	// Token: 0x040012E3 RID: 4835
	protected readonly Dictionary<string, DiplomacyRelation> _dict = new Dictionary<string, DiplomacyRelation>();
}
