using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200076D RID: 1901
public class SubspeciesStatsElement : SubspeciesElement, IStatsElement, IRefreshElement
{
	// Token: 0x06003C49 RID: 15433 RVA: 0x001A3376 File Offset: 0x001A1576
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x001A338E File Offset: 0x001A158E
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x001A33A7 File Offset: 0x001A15A7
	protected override IEnumerator showContent()
	{
		if (base.subspecies == null)
		{
			yield break;
		}
		if (!base.subspecies.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Subspecies, SubspeciesData>(base.subspecies);
		this.setIconValue("i_offspring", base.subspecies.base_stats["offspring"], null, "", false, "", '/');
		this.setIconValue("i_mutation_rate", base.subspecies.base_stats_meta["mutation"], null, "", false, "", '/');
		float num = base.subspecies.base_stats["lifespan"];
		float tLifespanMale = num + base.subspecies.base_stats_male["lifespan"];
		float tLifespanFemale = num + base.subspecies.base_stats_female["lifespan"];
		int tMana = (int)((float)((int)base.subspecies.base_stats["intelligence"]) * SimGlobals.m.MANA_PER_INTELLIGENCE);
		this.setIconValue("i_lifespan_male", tLifespanMale, null, "", false, "", '/');
		this.setIconValue("i_lifespan_female", tLifespanFemale, null, "", false, "", '/');
		this.setIconValue("i_maturation", base.subspecies.getMaturationTimeMonths(), null, "", false, "", '/');
		this.setIconValue("i_mana", (float)tMana, null, "", false, "", '/');
		this.showIconSubspecies("i_birth_rate", "birth_rate");
		this.showIconSubspecies("i_health", "health");
		this.showIconSubspecies("i_armor", "armor");
		this.showIconSubspecies("i_speed", "speed");
		this.showIconSubspecies("i_damage", "damage");
		this.showIconSubspecies("i_critical_chance", "critical_chance");
		this.showIconSubspecies("i_attack_speed", "attack_speed");
		this.showIconSubspecies("i_diplomacy", "diplomacy");
		this.showIconSubspecies("i_warfare", "warfare");
		this.showIconSubspecies("i_stewardship", "stewardship");
		this.showIconSubspecies("i_intelligence", "intelligence");
		this.showIconSubspecies("i_stamina", "stamina");
		int tCountMainKingdoms = base.subspecies.countMainKingdoms();
		int tCountMainCities = base.subspecies.countMainCities();
		this.setIconValue("i_kingdoms", (float)tCountMainKingdoms, null, "", false, "", '/');
		this.setIconValue("i_villages", (float)tCountMainCities, null, "", false, "", '/');
		yield break;
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x001A33B8 File Offset: 0x001A15B8
	private void showIconSubspecies(string pFieldID, string pStatID)
	{
		ActorAsset actorAsset = base.subspecies.getActorAsset();
		int tValue = (int)base.subspecies.nucleus.getStats().get(pStatID);
		int tOriginalSpecieValue = (int)actorAsset.base_stats[pStatID];
		foreach (GenomePart tGenomePart in actorAsset.genome_parts)
		{
			if (tGenomePart.id == pStatID)
			{
				tOriginalSpecieValue += (int)tGenomePart.value;
				break;
			}
		}
		string tColor;
		if (tValue > tOriginalSpecieValue)
		{
			tColor = "#43FF43";
		}
		else if (tValue < tOriginalSpecieValue)
		{
			tColor = "#FB2C21";
		}
		else
		{
			tColor = string.Empty;
		}
		float pMainVal = base.subspecies.base_stats[pStatID];
		string pColor = tColor;
		this.setIconValue(pFieldID, pMainVal, null, pColor, false, "", '/');
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x001A34A8 File Offset: 0x001A16A8
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002BDF RID: 11231
	private StatsIconContainer _stats_icons;
}
