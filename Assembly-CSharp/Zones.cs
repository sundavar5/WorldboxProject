using System;

// Token: 0x02000425 RID: 1061
public static class Zones
{
	// Token: 0x0600251D RID: 9501 RVA: 0x00133260 File Offset: 0x00131460
	internal static bool isPowerForceMapMode(MetaType pMode = MetaType.None)
	{
		return World.world.isAnyPowerSelected() && Zones._selected_power.force_map_mode == pMode;
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x00133280 File Offset: 0x00131480
	public static bool isPowerForcedMapModeEnabled()
	{
		MetaType tType = MetaType.None;
		if (World.world.isAnyPowerSelected() && !Zones._selected_power.force_map_mode.isNone())
		{
			tType = Zones._selected_power.force_map_mode;
		}
		return !tType.isNone();
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x001332C0 File Offset: 0x001314C0
	internal static MetaType getForcedMapMode()
	{
		MetaType tType = MetaType.None;
		if (Zones.isPowerForcedMapModeEnabled())
		{
			tType = Zones._selected_power.force_map_mode;
		}
		else if (SelectedObjects.isNanoObjectSet())
		{
			MetaTypeAsset tMetaTypeAsset = SelectedObjects.getSelectedNanoObject().getMetaTypeAsset();
			if (tMetaTypeAsset.force_zone_when_selected)
			{
				tType = tMetaTypeAsset.map_mode;
			}
		}
		return tType;
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x00133305 File Offset: 0x00131505
	public static bool hasPowerForceMapMode()
	{
		return !Zones.getForcedMapMode().isNone();
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x00133314 File Offset: 0x00131514
	public static MetaTypeAsset getMapMetaAsset()
	{
		if (Zones.hasPowerForceMapMode())
		{
			return Zones.getForcedMapMode().getAsset();
		}
		for (int i = 0; i < AssetManager.meta_type_library.list.Count; i++)
		{
			MetaTypeAsset tAsset = AssetManager.meta_type_library.list[i];
			if (!string.IsNullOrEmpty(tAsset.option_id) && (AssetManager.options_library.get(tAsset.option_id).isActive() || Zones.isPowerForceMapMode(tAsset.map_mode)))
			{
				return tAsset;
			}
		}
		return null;
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x00133392 File Offset: 0x00131592
	public static bool showMapNames()
	{
		return PlayerConfig.optionBoolEnabled("map_names") || Zones.hasPowerForceMapMode();
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x001333A7 File Offset: 0x001315A7
	public static bool showMapBorders()
	{
		return Zones.isBordersEnabled() || Zones.hasPowerForceMapMode();
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x001333B7 File Offset: 0x001315B7
	public static bool isBordersEnabled()
	{
		return PlayerConfig.optionBoolEnabled("map_layers");
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x001333C4 File Offset: 0x001315C4
	public static MetaType getCurrentMapBorderMode(bool pCheckOnlyOption = false)
	{
		MetaType tMode;
		if (Zones.showCultureZones(pCheckOnlyOption))
		{
			tMode = MetaType.Culture;
		}
		else if (Zones.showKingdomZones(pCheckOnlyOption))
		{
			tMode = MetaType.Kingdom;
		}
		else if (Zones.showClanZones(pCheckOnlyOption))
		{
			tMode = MetaType.Clan;
		}
		else if (Zones.showAllianceZones(pCheckOnlyOption))
		{
			tMode = MetaType.Alliance;
		}
		else if (Zones.showCityZones(pCheckOnlyOption))
		{
			tMode = MetaType.City;
		}
		else if (Zones.showSpeciesZones(pCheckOnlyOption))
		{
			tMode = MetaType.Subspecies;
		}
		else if (Zones.showFamiliesZones(pCheckOnlyOption))
		{
			tMode = MetaType.Family;
		}
		else if (Zones.showLanguagesZones(pCheckOnlyOption))
		{
			tMode = MetaType.Language;
		}
		else if (Zones.showReligionZones(pCheckOnlyOption))
		{
			tMode = MetaType.Religion;
		}
		else if (Zones.showArmyZones(pCheckOnlyOption))
		{
			tMode = MetaType.Army;
		}
		else
		{
			tMode = MetaType.None;
		}
		return tMode;
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x0013344E File Offset: 0x0013164E
	public static bool showCityZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.city.isActive(pCheckOnlyOption);
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x0013345B File Offset: 0x0013165B
	public static bool showKingdomZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.kingdom.isActive(pCheckOnlyOption);
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x00133468 File Offset: 0x00131668
	public static bool showClanZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.clan.isActive(pCheckOnlyOption);
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x00133475 File Offset: 0x00131675
	public static bool showAllianceZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.alliance.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x00133482 File Offset: 0x00131682
	public static bool showCultureZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.culture.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x0013348F File Offset: 0x0013168F
	public static bool showSpeciesZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.subspecies.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x0013349C File Offset: 0x0013169C
	public static bool showFamiliesZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.family.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x001334A9 File Offset: 0x001316A9
	public static bool showLanguagesZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.language.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x001334B6 File Offset: 0x001316B6
	public static bool showReligionZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.religion.isActive(pCheckOnlyOption);
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x001334C3 File Offset: 0x001316C3
	public static bool showArmyZones(bool pCheckOnlyOption = false)
	{
		return MetaTypeLibrary.army.isActive(pCheckOnlyOption);
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06002530 RID: 9520 RVA: 0x001334D0 File Offset: 0x001316D0
	private static GodPower _selected_power
	{
		get
		{
			return World.world.selected_power;
		}
	}
}
