using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007C9 RID: 1993
public class WarListElement : WindowListElementBase<War, WarData>
{
	// Token: 0x06003EE7 RID: 16103 RVA: 0x001B3C28 File Offset: 0x001B1E28
	internal override void show(War pWar)
	{
		base.show(pWar);
		this.text_name.text = pWar.data.name;
		this.text_name.color = pWar.getColor().getColorText();
		this.war_type.setKeyAndUpdate(pWar.getAsset().localized_war_name);
		this.kingdoms.setValue(pWar.countKingdoms(), "");
		this.cities.setValue(pWar.countCities(), "");
		this.renown.setValue(pWar.getRenown(), "");
		this.dead.setValue((int)pWar.getTotalDeaths(), "");
		this.age.setValue(pWar.getAge(), "");
		this.duration.setValue(pWar.getDuration(), "");
		this.total_war_icon.gameObject.SetActive(false);
		this.clearBanners();
		WarWinner tWinner = pWar.data.winner;
		this.showKingdomBanners(pWar.getAttackers(), this.pool_mini_banners_attackers, false, tWinner == WarWinner.Attackers, tWinner == WarWinner.Defenders);
		this.showKingdomBanners(pWar.getDiedAttackers(), this.pool_mini_banners_attackers, false, false, false);
		this.showKingdomBanners(pWar.getPastAttackers(), this.pool_mini_banners_attackers, true, false, false);
		this.showKingdomBanners(pWar.getDefenders(), this.pool_mini_banners_defenders, false, tWinner == WarWinner.Defenders, tWinner == WarWinner.Attackers);
		this.showKingdomBanners(pWar.getDiedDefenders(), this.pool_mini_banners_defenders, false, false, false);
		this.showKingdomBanners(pWar.getPastDefenders(), this.pool_mini_banners_defenders, true, false, false);
		bool tAttackerPopAdvantage = pWar.countAttackersPopulation() > pWar.countDefendersPopulation();
		bool tAttackerDeadAdvantage = pWar.getDeadDefenders() > pWar.getDeadAttackers();
		bool tAttackerWarriorAdvantage = pWar.countAttackersWarriors() > pWar.countDefendersWarriors();
		pWar.countAttackersCities();
		pWar.countDefendersCities();
		this.setIconValue("i_attackers_population", pWar.countAttackersPopulation(), tAttackerPopAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue("i_attackers_army", pWar.countAttackersWarriors(), tAttackerWarriorAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue("i_attackers_dead", pWar.getDeadAttackers(), tAttackerDeadAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue("i_defenders_population", pWar.countDefendersPopulation(), tAttackerPopAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue("i_defenders_army", pWar.countDefendersWarriors(), tAttackerWarriorAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue("i_defenders_dead", pWar.getDeadDefenders(), tAttackerDeadAdvantage ? "#FB2C21" : "#43FF43");
	}

	// Token: 0x06003EE8 RID: 16104 RVA: 0x001B3EAC File Offset: 0x001B20AC
	private void checkCreation()
	{
		if (this.pool_mini_banners_attackers == null)
		{
			this.pool_mini_banners_attackers = new ObjectPoolGenericMono<KingdomBanner>(this.prefabMiniKingdomBanner, this.gridAttackers.transform);
			this.pool_mini_banners_defenders = new ObjectPoolGenericMono<KingdomBanner>(this.prefabMiniKingdomBanner, this.gridDefenders.transform);
		}
	}

	// Token: 0x06003EE9 RID: 16105 RVA: 0x001B3EF9 File Offset: 0x001B20F9
	public void clearBanners()
	{
		this.checkCreation();
		this.pool_mini_banners_attackers.clear(true);
		this.pool_mini_banners_defenders.clear(true);
	}

	// Token: 0x06003EEA RID: 16106 RVA: 0x001B3F1C File Offset: 0x001B211C
	public void showKingdomBanners(IEnumerable<Kingdom> pList, ObjectPoolGenericMono<KingdomBanner> pPool, bool pLeft = false, bool pWinner = false, bool pLoser = false)
	{
		this.checkCreation();
		int i = 6 - pPool.countActive();
		if (i <= 0)
		{
			return;
		}
		foreach (Kingdom tKingdom in pList)
		{
			if (tKingdom != null && tKingdom.isAlive())
			{
				KingdomBanner tElement = pPool.getNext();
				tElement.load(tKingdom);
				if (pLeft)
				{
					tElement.hasLeftWar();
				}
				if (pWinner)
				{
					tElement.hasWon();
				}
				if (pLoser)
				{
					tElement.hasLost();
				}
				tElement.GetComponentInChildren<RotateOnHover>().enabled = true;
				if (i-- <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06003EEB RID: 16107 RVA: 0x001B3FC0 File Offset: 0x001B21C0
	public void setIconValue(string pName, int pMainVal, string pColor)
	{
		Transform tIconContainer = base.transform.FindRecursive(pName);
		if (tIconContainer == null)
		{
			Debug.LogError("No icon with this name! " + pName);
			return;
		}
		Transform tIcon = tIconContainer.Find("Container/Text");
		if (tIcon == null)
		{
			Debug.LogError(pName + " doesn't have Container/Text");
			return;
		}
		tIcon.gameObject.SetActive(true);
		Graphic component = tIcon.GetComponent<Text>();
		CountUpOnClick tCounter = tIconContainer.GetComponent<CountUpOnClick>();
		component.color = Toolbox.makeColor(pColor);
		tCounter.setValue(pMainVal, "");
	}

	// Token: 0x06003EEC RID: 16108 RVA: 0x001B404A File Offset: 0x001B224A
	protected override void OnDisable()
	{
		base.OnDisable();
		ObjectPoolGenericMono<KingdomBanner> objectPoolGenericMono = this.pool_mini_banners_attackers;
		if (objectPoolGenericMono != null)
		{
			objectPoolGenericMono.clear(true);
		}
		ObjectPoolGenericMono<KingdomBanner> objectPoolGenericMono2 = this.pool_mini_banners_defenders;
		if (objectPoolGenericMono2 == null)
		{
			return;
		}
		objectPoolGenericMono2.clear(true);
	}

	// Token: 0x06003EED RID: 16109 RVA: 0x001B4075 File Offset: 0x001B2275
	protected override void tooltipAction()
	{
		Tooltip.show(this, "war", new TooltipData
		{
			war = this.meta_object
		});
	}

	// Token: 0x04002DC6 RID: 11718
	public Text text_name;

	// Token: 0x04002DC7 RID: 11719
	public LocalizedText war_type;

	// Token: 0x04002DC8 RID: 11720
	public CountUpOnClick age;

	// Token: 0x04002DC9 RID: 11721
	public CountUpOnClick duration;

	// Token: 0x04002DCA RID: 11722
	public CountUpOnClick kingdoms;

	// Token: 0x04002DCB RID: 11723
	public CountUpOnClick cities;

	// Token: 0x04002DCC RID: 11724
	public CountUpOnClick renown;

	// Token: 0x04002DCD RID: 11725
	public CountUpOnClick dead;

	// Token: 0x04002DCE RID: 11726
	public KingdomBanner prefabMiniKingdomBanner;

	// Token: 0x04002DCF RID: 11727
	public GameObject gridAttackers;

	// Token: 0x04002DD0 RID: 11728
	public GameObject gridDefenders;

	// Token: 0x04002DD1 RID: 11729
	protected ObjectPoolGenericMono<KingdomBanner> pool_mini_banners_attackers;

	// Token: 0x04002DD2 RID: 11730
	protected ObjectPoolGenericMono<KingdomBanner> pool_mini_banners_defenders;

	// Token: 0x04002DD3 RID: 11731
	public Image total_war_icon;
}
