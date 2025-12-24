using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class NameplateLibrary : AssetLibrary<NameplateAsset>
{
	// Token: 0x06001A34 RID: 6708 RVA: 0x000F61C4 File Offset: 0x000F43C4
	public override void init()
	{
		base.init();
		this.add(new NameplateAsset
		{
			id = "plate_subspecies",
			path_sprite = "ui/nameplates/nameplate_subspecies",
			padding_left = 11,
			padding_right = 13,
			banner_only_mode_scale = 1.8f,
			map_mode = MetaType.Subspecies,
			overlap_for_fluid_mode = true,
			action_main = new NameplateBase(this.actionSubspecies)
		});
		this.add(new NameplateAsset
		{
			id = "plate_army",
			path_sprite = "ui/nameplates/nameplate_army",
			padding_left = 26,
			padding_right = 18,
			padding_top = -2,
			banner_only_mode_scale = 1.5f,
			map_mode = MetaType.Army,
			action_main = new NameplateBase(this.actionArmy)
		});
		this.add(new NameplateAsset
		{
			id = "plate_family",
			path_sprite = "ui/nameplates/nameplate_family",
			padding_left = 11,
			padding_right = 13,
			banner_only_mode_scale = 1.5f,
			map_mode = MetaType.Family,
			overlap_for_fluid_mode = true,
			action_main = new NameplateBase(this.actionFamily)
		});
		this.add(new NameplateAsset
		{
			id = "plate_religion",
			path_sprite = "ui/nameplates/nameplate_religion",
			padding_left = 11,
			padding_right = 13,
			map_mode = MetaType.Religion,
			action_main = new NameplateBase(this.actionReligion)
		});
		this.add(new NameplateAsset
		{
			id = "plate_culture",
			path_sprite = "ui/nameplates/nameplate_culture",
			padding_left = 11,
			padding_right = 13,
			map_mode = MetaType.Culture,
			action_main = new NameplateBase(this.actionCulture)
		});
		this.add(new NameplateAsset
		{
			id = "plate_language",
			path_sprite = "ui/nameplates/nameplate_language",
			padding_left = 11,
			padding_right = 13,
			map_mode = MetaType.Language,
			action_main = new NameplateBase(this.actionLanguage)
		});
		this.add(new NameplateAsset
		{
			id = "plate_alliance",
			path_sprite = "ui/nameplates/nameplate_alliance",
			map_mode = MetaType.Alliance,
			banner_only_mode_scale = 3f,
			padding_left = 14,
			padding_top = 2,
			action_main = new NameplateBase(this.actionAlliance)
		});
		this._plate_kingdom = this.add(new NameplateAsset
		{
			id = "plate_kingdom",
			path_sprite = "ui/nameplates/nameplate_kingdom",
			padding_left = 26,
			padding_right = 26,
			padding_top = -2,
			banner_only_mode_scale = 2.5f,
			map_mode = MetaType.Kingdom,
			action_main = new NameplateBase(this.actionKingdom)
		});
		this._plate_city = this.add(new NameplateAsset
		{
			id = "plate_city",
			path_sprite = "ui/nameplates/nameplate_city",
			map_mode = MetaType.City,
			banner_only_mode_scale = 2.5f,
			padding_left = 6,
			padding_right = 7,
			padding_top = -2,
			action_main = new NameplateBase(this.actionCity)
		});
		this.add(new NameplateAsset
		{
			id = "plate_clan",
			path_sprite = "ui/nameplates/nameplate_clan",
			map_mode = MetaType.Clan,
			padding_left = 17,
			padding_right = 24,
			action_main = new NameplateBase(this.actionClan)
		});
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x000F6536 File Offset: 0x000F4736
	private bool isWithinCamera(Vector2 pVector)
	{
		return World.world.move_camera.isWithinCameraViewNotPowerBar(pVector);
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000F6548 File Offset: 0x000F4748
	public override NameplateAsset add(NameplateAsset pAsset)
	{
		this.map_modes_nameplates.Add(pAsset.map_mode, pAsset);
		return base.add(pAsset);
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x000F6564 File Offset: 0x000F4764
	private void actionAlliance(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		foreach (Alliance tAlliance in World.world.alliances)
		{
			City tBestCity = null;
			Kingdom tBestKingdom = null;
			foreach (Kingdom tKingdom in tAlliance.kingdoms_hashset)
			{
				if (tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center) && (tBestKingdom == null || tBestKingdom.power < tKingdom.power))
				{
					tBestKingdom = tKingdom;
				}
			}
			if (tBestKingdom != null && tBestKingdom.hasCapital())
			{
				tBestCity = tBestKingdom.capital;
			}
			if (tBestCity != null)
			{
				pManager.prepareNext(pAsset, tAlliance).showTextAlliance(tAlliance, tBestCity);
			}
		}
		foreach (Kingdom tKingdom2 in World.world.kingdoms)
		{
			if (tKingdom2.hasCapital() && !tKingdom2.hasAlliance() && this.isWithinCamera(tKingdom2.capital.city_center))
			{
				if (tCurrent >= pAsset.max_nameplate_count)
				{
					break;
				}
				tCurrent++;
				pManager.prepareNext(this._plate_kingdom, tKingdom2).showTextKingdom(tKingdom2, tKingdom2.capital.city_center);
			}
		}
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x000F66EC File Offset: 0x000F48EC
	private void actionReligion(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.religion.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_107;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasReligion() && tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom.religion).showTextReligion(tKingdom.religion, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				if (tCity.hasReligion())
				{
					Religion tMeta = tCity.getReligion();
					if (this.isWithinCamera(tCity.city_center))
					{
						pManager.prepareNext(pAsset, tMeta).showTextReligion(tMeta, tCity.city_center);
					}
				}
			}
			return;
		}
		IL_107:
		foreach (Religion tMeta2 in World.world.religions)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta2, out tPosition, null))
			{
				pManager.prepareNext(pAsset, tMeta2).showTextReligion(tMeta2, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x000F688C File Offset: 0x000F4A8C
	private void actionLanguage(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.language.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_107;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasLanguage() && tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom.language).showTextLanguage(tKingdom.language, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				if (tCity.hasLanguage())
				{
					Language tMeta = tCity.getLanguage();
					if (this.isWithinCamera(tCity.city_center))
					{
						pManager.prepareNext(pAsset, tMeta).showTextLanguage(tMeta, tCity.city_center);
					}
				}
			}
			return;
		}
		IL_107:
		foreach (Language tMeta2 in World.world.languages)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta2, out tPosition, null))
			{
				pManager.prepareNext(pAsset, tMeta2).showTextLanguage(tMeta2, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000F6A2C File Offset: 0x000F4C2C
	private void actionCulture(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.culture.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_107;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasCulture() && tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom.culture).showTextCulture(tKingdom.culture, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				if (tCity.hasCulture())
				{
					Culture tMeta = tCity.getCulture();
					if (this.isWithinCamera(tCity.city_center))
					{
						pManager.prepareNext(pAsset, tMeta).showTextCulture(tMeta, tCity.city_center);
					}
				}
			}
			return;
		}
		IL_107:
		foreach (Culture tMeta2 in World.world.cultures)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta2, out tPosition, null))
			{
				pManager.prepareNext(pAsset, tMeta2).showTextCulture(tMeta2, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000F6BCC File Offset: 0x000F4DCC
	private void actionCity(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		using (ListPool<City> tSortedCities = new ListPool<City>(World.world.cities.list))
		{
			tSortedCities.Sort(new Comparison<City>(this.sortByMembers));
			if (MetaTypeLibrary.city.getZoneOptionState() == 0)
			{
				using (ListPool<City>.Enumerator enumerator = tSortedCities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ref City ptr = ref enumerator.Current;
						City tCity = ptr;
						if (tCurrent >= pAsset.max_nameplate_count)
						{
							break;
						}
						if (this.isWithinCamera(tCity.city_center))
						{
							pManager.prepareNext(this._plate_city, tCity).showTextCity(tCity, tCity.city_center);
							tCurrent++;
						}
					}
					return;
				}
			}
			foreach (City tMeta in World.world.cities)
			{
				if (tCurrent >= pAsset.max_nameplate_count)
				{
					break;
				}
				Actor tUnit = null;
				if (tMeta.hasLeader() && !tMeta.leader.isRekt() && tMeta.leader.is_visible)
				{
					tUnit = tMeta.leader;
				}
				Vector3 tPosition;
				if (this.getPositionForMeta(tMeta, out tPosition, tUnit))
				{
					pManager.prepareNext(pAsset, tMeta).showTextCity(tMeta, tPosition);
					tCurrent++;
				}
			}
		}
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x000F6D64 File Offset: 0x000F4F64
	private void actionKingdom(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		if (MetaTypeLibrary.kingdom.getZoneOptionState() == 0)
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom).showTextKingdom(tKingdom, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		foreach (Kingdom tMeta in World.world.kingdoms)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Actor tUnit = null;
			if (tMeta.hasKing() && !tMeta.king.isRekt() && tMeta.king.is_visible)
			{
				tUnit = tMeta.king;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta, out tPosition, tUnit))
			{
				pManager.prepareNext(pAsset, tMeta).showTextKingdom(tMeta, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000F6E8C File Offset: 0x000F508C
	private void actionSubspecies(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.subspecies.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_108;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					Subspecies tMeta = tKingdom.getMainSubspecies();
					if (!tMeta.isRekt() && tKingdom.hasCapital() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tMeta).showTextSubspecies(tMeta, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				Subspecies tMeta2 = tCity.getMainSubspecies();
				if (!tMeta2.isRekt() && this.isWithinCamera(tCity.city_center))
				{
					pManager.prepareNext(pAsset, tMeta2).showTextSubspecies(tMeta2, tCity.city_center);
				}
			}
			return;
		}
		IL_108:
		foreach (Subspecies tMeta3 in World.world.subspecies)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta3, out tPosition, null))
			{
				pManager.prepareNext(pAsset, tMeta3).showTextSubspecies(tMeta3, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x000F702C File Offset: 0x000F522C
	private bool getPositionForMeta(IMetaObject pMetaObject, out Vector3 pPosition, Actor pForceActor = null)
	{
		if (!pMetaObject.isAlive() || !pMetaObject.hasUnits())
		{
			pPosition = Vector3.zero;
			return false;
		}
		Actor tActorForPosition = pForceActor;
		if (tActorForPosition == null)
		{
			tActorForPosition = pMetaObject.getOldestVisibleUnitForNameplatesCached();
		}
		if (tActorForPosition == null)
		{
			pPosition = Vector3.zero;
			return false;
		}
		Vector3 tPositionResult = tActorForPosition.current_position;
		tPositionResult.y += tActorForPosition.getHeight();
		tPositionResult.y += -2f;
		pPosition = tPositionResult;
		return true;
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x000F70A8 File Offset: 0x000F52A8
	private int sortByMembers(IMetaObject pObject1, IMetaObject pObject2)
	{
		int tFavoriteComparison = pObject2.isFavorite().CompareTo(pObject1.isFavorite());
		if (tFavoriteComparison != 0)
		{
			return tFavoriteComparison;
		}
		int tSelectedComparison = pObject2.isSelected().CompareTo(pObject1.isSelected());
		if (tSelectedComparison != 0)
		{
			return tSelectedComparison;
		}
		return pObject2.countUnits().CompareTo(pObject1.countUnits());
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x000F7100 File Offset: 0x000F5300
	private void actionArmy(NameplateManager pManager, NameplateAsset pAsset)
	{
		MetaTypeLibrary.army.getZoneOptionState();
		using (ListPool<Army> tSortedArmies = new ListPool<Army>(World.world.armies.list))
		{
			tSortedArmies.Sort(new Comparison<Army>(this.sortByMembers));
			int tCurrent = 0;
			foreach (Army ptr in tSortedArmies)
			{
				Army tMeta = ptr;
				if (tCurrent >= pAsset.max_nameplate_count)
				{
					break;
				}
				Actor tUnit = null;
				if (tMeta.hasCaptain())
				{
					tUnit = tMeta.getCaptain();
				}
				Vector3 tPosition;
				if (this.getPositionForMeta(tMeta, out tPosition, tUnit))
				{
					pManager.prepareNext(pAsset, tMeta).showTextArmy(tMeta, tPosition);
					tCurrent++;
				}
			}
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x000F71D4 File Offset: 0x000F53D4
	private void actionFamily(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.family.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_138;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasCapital() && tKingdom.hasKing() && tKingdom.king.hasFamily() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom.king.family).showTextFamily(tKingdom.king.family, tKingdom.capital.city_center);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				if (tCity.hasLeader() && tCity.leader.hasFamily())
				{
					Family tMeta = tCity.leader.family;
					if (tMeta != null && this.isWithinCamera(tCity.city_center))
					{
						pManager.prepareNext(pAsset, tMeta).showTextFamily(tMeta, tCity.city_center);
					}
				}
			}
			return;
		}
		IL_138:
		using (ListPool<Family> tSortedFamilies = new ListPool<Family>(World.world.families.list))
		{
			tSortedFamilies.Sort(new Comparison<Family>(this.sortByMembers));
			foreach (Family ptr in tSortedFamilies)
			{
				Family tMeta2 = ptr;
				if (tCurrent >= pAsset.max_nameplate_count)
				{
					break;
				}
				Vector3 tPosition;
				if (this.getPositionForMeta(tMeta2, out tPosition, null))
				{
					pManager.prepareNext(pAsset, tMeta2).showTextFamily(tMeta2, tPosition);
					tCurrent++;
				}
			}
		}
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x000F73E0 File Offset: 0x000F55E0
	private void actionClan(NameplateManager pManager, NameplateAsset pAsset)
	{
		int tCurrent = 0;
		int zoneOptionState = MetaTypeLibrary.clan.getZoneOptionState();
		if (zoneOptionState != 0)
		{
			if (zoneOptionState != 1)
			{
				goto IL_105;
			}
		}
		else
		{
			using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom tKingdom = enumerator.Current;
					if (tKingdom.hasCapital() && tKingdom.hasKing() && tKingdom.king.hasClan() && this.isWithinCamera(tKingdom.capital.city_center))
					{
						pManager.prepareNext(pAsset, tKingdom.king.clan).showTextClanCity(tKingdom.king.clan, tKingdom.capital);
					}
				}
				return;
			}
		}
		using (IEnumerator<City> enumerator2 = World.world.cities.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				City tCity = enumerator2.Current;
				Clan tMeta = tCity.getRoyalClan();
				if (tMeta != null && this.isWithinCamera(tCity.city_center))
				{
					pManager.prepareNext(pAsset, tMeta).showTextClanCity(tMeta, tCity);
				}
			}
			return;
		}
		IL_105:
		foreach (Clan tMeta2 in World.world.clans)
		{
			if (tCurrent >= pAsset.max_nameplate_count)
			{
				break;
			}
			Vector3 tPosition;
			if (this.getPositionForMeta(tMeta2, out tPosition, null))
			{
				pManager.prepareNext(pAsset, tMeta2).showTextClanFluid(tMeta2, tPosition);
				tCurrent++;
			}
		}
	}

	// Token: 0x04001450 RID: 5200
	public readonly Dictionary<MetaType, NameplateAsset> map_modes_nameplates = new Dictionary<MetaType, NameplateAsset>();

	// Token: 0x04001451 RID: 5201
	private NameplateAsset _plate_kingdom;

	// Token: 0x04001452 RID: 5202
	private NameplateAsset _plate_city;

	// Token: 0x04001453 RID: 5203
	private const int OFFSET_UNIT_Y = -2;
}
