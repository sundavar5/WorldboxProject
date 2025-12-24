using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class WorldLog
{
	// Token: 0x06000E5A RID: 3674 RVA: 0x000C1B32 File Offset: 0x000BFD32
	public WorldLog()
	{
		WorldLog.instance = this;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x000C1B40 File Offset: 0x000BFD40
	public static void logNewKing(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.king_new, pKingdom.name, pKingdom.king.getName(), null)
		{
			kingdom = pKingdom,
			unit = pKingdom.king,
			location = pKingdom.king.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x000C1BB4 File Offset: 0x000BFDB4
	public static void logRoyalClanNew(Kingdom pKingdom, Clan pClan)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_royal_clan_new, pKingdom.name, pClan.name, pKingdom.king.name)
		{
			kingdom = pKingdom,
			unit = pKingdom.king,
			location = pKingdom.king.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pClan.getColor().getColorText(),
			color_special3 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x000C1C40 File Offset: 0x000BFE40
	public static void logRoyalClanChanged(Kingdom pKingdom, Clan pOldClan, Clan pNewClan)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_royal_clan_changed, pKingdom.name, pOldClan.name, pNewClan.name)
		{
			kingdom = pKingdom,
			unit = pKingdom.king,
			location = pKingdom.king.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pOldClan.getColor().getColorText(),
			color_special3 = pNewClan.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x000C1CC8 File Offset: 0x000BFEC8
	public static void logRoyalClanNoMore(Kingdom pKingdom, Clan pClan)
	{
		WorldLogMessage tLog = new WorldLogMessage(WorldLogLibrary.kingdom_royal_clan_dead, pKingdom.name, pClan.name, null);
		tLog.kingdom = pKingdom;
		if (pKingdom.hasCapital())
		{
			tLog.location = pKingdom.capital.last_city_center;
		}
		tLog.color_special1 = pKingdom.getColor().getColorText();
		tLog.color_special2 = pClan.getColor().getColorText();
		tLog.add();
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x000C1D38 File Offset: 0x000BFF38
	public static void logKingFledCity(Kingdom pKingdom, Actor pActor)
	{
		new WorldLogMessage(WorldLogLibrary.king_fled_city, pKingdom.name, pActor.getName(), pActor.city.name)
		{
			kingdom = pKingdom,
			unit = pActor,
			location = pActor.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pKingdom.getColor().getColorText(),
			color_special3 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x000C1DB8 File Offset: 0x000BFFB8
	public static void logKingFledCapital(Kingdom pKingdom, Actor pActor)
	{
		new WorldLogMessage(WorldLogLibrary.king_fled_capital, pKingdom.name, pActor.getName(), pKingdom.capital.name)
		{
			kingdom = pKingdom,
			unit = pActor,
			location = pActor.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pKingdom.getColor().getColorText(),
			color_special3 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x000C1E38 File Offset: 0x000C0038
	public static void logKingLeft(Kingdom pKingdom, Actor pActor)
	{
		new WorldLogMessage(WorldLogLibrary.king_left, pKingdom.name, pActor.getName(), null)
		{
			kingdom = pKingdom,
			unit = pActor,
			location = pActor.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x000C1EA0 File Offset: 0x000C00A0
	public static void logKingDead(Kingdom pKingdom, Actor pActor)
	{
		new WorldLogMessage(WorldLogLibrary.king_dead, pKingdom.name, pActor.getName(), null)
		{
			kingdom = pKingdom,
			location = pActor.current_position,
			color_special1 = pKingdom.getColor().getColorText(),
			color_special2 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000C1F00 File Offset: 0x000C0100
	public static void logKingMurder(Kingdom pKingdom, Actor pActor, Actor pAttacker)
	{
		WorldLogMessage tLog = new WorldLogMessage(WorldLogLibrary.king_killed, pKingdom.name, pActor.getName(), (pAttacker != null) ? pAttacker.getName() : null);
		tLog.kingdom = pKingdom;
		tLog.color_special1 = pKingdom.getColor().getColorText();
		tLog.color_special2 = pKingdom.getColor().getColorText();
		bool flag;
		if (pAttacker == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pAttacker.kingdom;
			flag = (((kingdom != null) ? kingdom.getColor() : null) != null);
		}
		if (flag)
		{
			tLog.color_special3 = pAttacker.kingdom.getColor().getColorText();
		}
		if (pAttacker != null && pAttacker.asset.can_be_inspected)
		{
			tLog.unit = pAttacker;
		}
		tLog.location = pActor.current_position;
		tLog.add();
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x000C1FB4 File Offset: 0x000C01B4
	public static void logFavDead(Actor pActor)
	{
		WorldLogMessage tLog = new WorldLogMessage(WorldLogLibrary.favorite_dead, pActor.getName(), null, null);
		tLog.location = pActor.current_position;
		bool flag;
		if (pActor == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pActor.kingdom;
			flag = (((kingdom != null) ? kingdom.getColor() : null) != null);
		}
		if (flag)
		{
			tLog.kingdom = pActor.kingdom;
			tLog.color_special1 = pActor.kingdom.getColor().getColorText();
		}
		tLog.add();
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x000C2024 File Offset: 0x000C0224
	public static void logFavMurder(Actor pActor, Actor pAttacker)
	{
		WorldLogMessage tLog = new WorldLogMessage(WorldLogLibrary.favorite_killed, pActor.getName(), (pAttacker != null) ? pAttacker.getName() : null, null);
		bool flag;
		if (pActor == null)
		{
			flag = (null != null);
		}
		else
		{
			Kingdom kingdom = pActor.kingdom;
			flag = (((kingdom != null) ? kingdom.getColor() : null) != null);
		}
		if (flag)
		{
			tLog.kingdom = pActor.kingdom;
			tLog.color_special1 = pActor.kingdom.getColor().getColorText();
		}
		bool flag2;
		if (pAttacker == null)
		{
			flag2 = (null != null);
		}
		else
		{
			Kingdom kingdom2 = pAttacker.kingdom;
			flag2 = (((kingdom2 != null) ? kingdom2.getColor() : null) != null);
		}
		if (flag2)
		{
			tLog.color_special2 = pAttacker.kingdom.getColor().getColorText();
		}
		if (pAttacker != null && pAttacker.asset.can_be_inspected)
		{
			tLog.unit = pAttacker;
		}
		tLog.location = pActor.current_position;
		tLog.add();
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x000C20E4 File Offset: 0x000C02E4
	public static void logNewCity(City pCity)
	{
		new WorldLogMessage(WorldLogLibrary.city_new, pCity.name, null, null)
		{
			kingdom = pCity.kingdom,
			location = pCity.last_city_center,
			color_special1 = pCity.kingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x000C2136 File Offset: 0x000C0336
	public static void logCityRevolt(City pCity)
	{
		new WorldLogMessage(WorldLogLibrary.log_city_revolted, pCity.name, pCity.kingdom.name, null)
		{
			kingdom = pCity.kingdom,
			location = pCity.last_city_center
		}.add();
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x000C2171 File Offset: 0x000C0371
	public static void logWarEnded(War pWar)
	{
		new WorldLogMessage(WorldLogLibrary.diplomacy_war_ended, pWar.data.name, null, null)
		{
			color_special1 = pWar.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x000C21A0 File Offset: 0x000C03A0
	public static void logNewWar(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		new WorldLogMessage(WorldLogLibrary.diplomacy_war_started, pKingdom1.name, pKingdom2.name, null)
		{
			location = pKingdom1.location,
			color_special1 = pKingdom1.getColor().getColorText(),
			color_special2 = pKingdom2.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x000C21FC File Offset: 0x000C03FC
	public static void logNewTotalWar(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.total_war_started, pKingdom.name, null, null)
		{
			location = pKingdom.location,
			color_special1 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000C2237 File Offset: 0x000C0437
	public static void logAllianceCreated(Alliance pAlliance)
	{
		new WorldLogMessage(WorldLogLibrary.alliance_new, pAlliance.name, null, null)
		{
			color_special1 = pAlliance.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x000C2261 File Offset: 0x000C0461
	public static void logAllianceDisolved(Alliance pAlliance)
	{
		new WorldLogMessage(WorldLogLibrary.alliance_dissolved, pAlliance.name, null, null)
		{
			color_special1 = pAlliance.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000C228C File Offset: 0x000C048C
	public static void logNewKingdom(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_new, pKingdom.name, null, null)
		{
			kingdom = pKingdom,
			location = pKingdom.location,
			color_special1 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000C22DC File Offset: 0x000C04DC
	public static void logKingdomDestroyed(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_destroyed, pKingdom.name, null, null)
		{
			kingdom = pKingdom,
			location = pKingdom.location,
			color_special1 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x000C232C File Offset: 0x000C052C
	public static void logCityDestroyed(City pCity)
	{
		new WorldLogMessage(WorldLogLibrary.city_destroyed, pCity.name, null, null)
		{
			kingdom = pCity.kingdom,
			color_special1 = pCity.kingdom.getColor().getColorText(),
			location = pCity.last_city_center
		}.add();
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x000C2380 File Offset: 0x000C0580
	public static void logDisaster(DisasterAsset pAsset, WorldTile pTile, string pName = null, City pCity = null, Actor pUnit = null)
	{
		if (string.IsNullOrEmpty(pAsset.world_log))
		{
			return;
		}
		WorldLogMessage tLog = new WorldLogMessage(AssetManager.world_log_library.get(pAsset.world_log), null, null, null);
		tLog.location = pTile.posV3;
		tLog.special1 = pName;
		if (pCity != null)
		{
			tLog.special2 = pCity.name;
		}
		if (pUnit != null && pUnit.asset.can_be_inspected)
		{
			tLog.unit = pUnit;
		}
		tLog.add();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x000C23FB File Offset: 0x000C05FB
	public static void locationJump(Vector3 pVector)
	{
		HistoryHud.disableRaycasts();
		World.world.locatePosition(pVector, new Action(HistoryHud.enableRaycasts), new Action(HistoryHud.enableRaycasts));
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x000C2425 File Offset: 0x000C0625
	public static void locationFollow(Actor pActor)
	{
		if (pActor == null)
		{
			return;
		}
		if (!pActor.isAlive())
		{
			return;
		}
		HistoryHud.disableRaycasts();
		World.world.locateAndFollow(pActor, new Action(HistoryHud.enableRaycasts), new Action(HistoryHud.enableRaycasts));
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x000C245C File Offset: 0x000C065C
	public static void logShatteredCrown(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_shattered, pKingdom.name, null, null)
		{
			kingdom = pKingdom,
			location = pKingdom.location,
			color_special1 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x000C24AC File Offset: 0x000C06AC
	public static void logFracturedKingdom(Kingdom pKingdom)
	{
		new WorldLogMessage(WorldLogLibrary.kingdom_fractured, pKingdom.name, null, null)
		{
			kingdom = pKingdom,
			location = pKingdom.location,
			color_special1 = pKingdom.getColor().getColorText()
		}.add();
	}

	// Token: 0x04000EC0 RID: 3776
	public static WorldLog instance;

	// Token: 0x04000EC1 RID: 3777
	public const int WORLDLOG_LIMIT = 2000;
}
