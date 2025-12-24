using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020003D4 RID: 980
public class BehKingCheckNewCityFoundation : BehaviourActionActor
{
	// Token: 0x06002271 RID: 8817 RVA: 0x00120F1B File Offset: 0x0011F11B
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.uses_families = true;
		this.uses_cities = true;
		this.uses_kingdoms = true;
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x00120F38 File Offset: 0x0011F138
	public override BehResult execute(Actor pActor)
	{
		Kingdom tKingdom = pActor.kingdom;
		if (!tKingdom.hasCapital())
		{
			return BehResult.Stop;
		}
		if (this.hasCitiesWithoutPopulation(tKingdom))
		{
			return BehResult.Stop;
		}
		BehaviourActionBase<Actor>.world.city_zone_helper.city_place_finder.recalc();
		if (!BehaviourActionBase<Actor>.world.city_zone_helper.city_place_finder.hasPossibleZones())
		{
			return BehResult.Stop;
		}
		BehResult result;
		using (ListPool<City> tPossibleCitiesForExpansion = this.getCityListForExpansion(tKingdom))
		{
			if (tPossibleCitiesForExpansion.Count == 0)
			{
				result = BehResult.Stop;
			}
			else
			{
				City tCityToExpandFrom;
				TileZone tZoneToPlaceCity = this.findZoneForExpansion(pActor, tPossibleCitiesForExpansion, out tCityToExpandFrom);
				if (tZoneToPlaceCity == null)
				{
					result = BehResult.Stop;
				}
				else
				{
					City tNewCity = BehaviourActionBase<Actor>.world.cities.buildNewCity(pActor, tZoneToPlaceCity);
					this.moveSomeUnitsToNewCity(tNewCity, tCityToExpandFrom);
					result = BehResult.Continue;
				}
			}
		}
		return result;
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x00120FF4 File Offset: 0x0011F1F4
	private bool hasCitiesWithoutPopulation(Kingdom pKingdom)
	{
		WorldTile tCapitalTile = pKingdom.capital.getTile(false);
		if (tCapitalTile == null)
		{
			return false;
		}
		foreach (City tCity in pKingdom.getCities())
		{
			if (tCity.countUnits() <= 30)
			{
				WorldTile tCityTile = tCity.getTile(false);
				if (tCityTile != null && tCityTile.reachableFrom(tCapitalTile))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x00121074 File Offset: 0x0011F274
	private void moveSomeUnitsToNewCity(City pNewCity, City pFromCity)
	{
		int tUnitsToMove = Mathf.Min(pFromCity.units.Count, 6);
		int tUnitsMoved = 0;
		foreach (Actor tUnit in pFromCity.units.LoopRandom<Actor>())
		{
			if (this.isPossibleToMoveUnitToCity(tUnit, pNewCity))
			{
				this.moveToCity(tUnit, pNewCity);
				tUnitsMoved++;
				int tNewFamilyMoved = this.checkUnitFamilyAndLovers(tUnit, pNewCity, tUnitsMoved);
				tUnitsMoved += tNewFamilyMoved;
				if (tUnitsMoved >= tUnitsToMove)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x00121100 File Offset: 0x0011F300
	private int checkUnitFamilyAndLovers(Actor pActor, City pCity, int pMovedAlready)
	{
		int tNewMoved = 0;
		if (pActor.hasLover())
		{
			Actor tLover = pActor.lover;
			if (this.isPossibleToMoveUnitToCity(tLover, pCity))
			{
				this.moveToCity(tLover, pCity);
				tNewMoved++;
			}
		}
		if (pActor.hasFamily())
		{
			foreach (Actor tFamilyMember in pActor.family.units)
			{
				if (this.isPossibleToMoveUnitToCity(tFamilyMember, pCity))
				{
					this.moveToCity(tFamilyMember, pCity);
					tNewMoved++;
				}
				if (tNewMoved + pMovedAlready >= 6)
				{
					break;
				}
			}
		}
		return tNewMoved;
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x001211A0 File Offset: 0x0011F3A0
	private void moveToCity(Actor pActor, City pCity)
	{
		pActor.stopBeingWarrior();
		pActor.joinCity(pCity);
		pActor.cancelAllBeh();
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x001211B8 File Offset: 0x0011F3B8
	private bool isPossibleToMoveUnitToCity(Actor pUnit, City pCity)
	{
		if (pUnit.isRekt())
		{
			return false;
		}
		if (!pUnit.isAdult())
		{
			return false;
		}
		if (pUnit.isCityLeader())
		{
			return false;
		}
		if (pUnit.isKing())
		{
			return false;
		}
		if (pUnit.isArmyGroupLeader())
		{
			return false;
		}
		if (pUnit.army != null)
		{
			return false;
		}
		if (pUnit.hasLover())
		{
			if (pUnit.lover.isKing())
			{
				return false;
			}
			if (pUnit.lover.isCityLeader())
			{
				return false;
			}
		}
		return pUnit.city != pCity;
	}

	// Token: 0x06002278 RID: 8824 RVA: 0x00121234 File Offset: 0x0011F434
	private TileZone findZoneForExpansion(Actor pActor, ListPool<City> pPossibleCitiesToExpandFrom, out City pCityExpandFrom)
	{
		pCityExpandFrom = null;
		TileZone tResult = null;
		foreach (City ptr in pPossibleCitiesToExpandFrom)
		{
			City tCity = ptr;
			TileZone tGoodZone = this.findZoneForCityOnTheSameIsland2(pActor, tCity);
			if (tGoodZone != null)
			{
				pCityExpandFrom = tCity;
				tResult = tGoodZone;
				break;
			}
		}
		if (tResult == null)
		{
			foreach (City ptr2 in pPossibleCitiesToExpandFrom)
			{
				City tCity2 = ptr2;
				if (tCity2.hasTransportBoats())
				{
					TileZone tGoodZone2 = this.findZoneForCityOnFarIsland(pActor, tCity2);
					if (tGoodZone2 != null)
					{
						pCityExpandFrom = tCity2;
						tResult = tGoodZone2;
						break;
					}
				}
			}
		}
		return tResult;
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x001212F4 File Offset: 0x0011F4F4
	private TileZone findZoneForCityOnFarIsland(Actor pActor, City pCity)
	{
		TileZone tBestZone = null;
		int tBestDist = int.MaxValue;
		WorldTile tCityTile = pCity.getTile(false);
		if (tCityTile == null)
		{
			return null;
		}
		Vector2Int tCityPos = tCityTile.pos;
		foreach (TileZone tZone in BehaviourActionBase<Actor>.world.city_zone_helper.city_place_finder.zones)
		{
			int tTempDist = Toolbox.SquaredDistVec2(tZone.centerTile.pos, tCityPos);
			if (tTempDist <= tBestDist && tCityTile.reachableFrom(tZone.centerTile) && tZone.checkCanSettleInThisBiomes(pActor.subspecies))
			{
				tBestDist = tTempDist;
				tBestZone = tZone;
			}
		}
		return tBestZone;
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x001213AC File Offset: 0x0011F5AC
	private TileZone findZoneForCityOnTheSameIsland2(Actor pActor, City pMainCity)
	{
		WorldTile tMainTile = pMainCity.getTile(false);
		if (tMainTile == null)
		{
			return null;
		}
		TileZone result;
		using (ListPool<TileZone> tGoodZones = new ListPool<TileZone>())
		{
			bool tDebug = DebugConfig.isOn(DebugOption.CitySettleCalc);
			if (tDebug)
			{
				DebugHighlight.clear();
			}
			foreach (City tCity in pMainCity.kingdom.getCities())
			{
				if (tCity == pMainCity)
				{
					WorldTile tCityTile = tCity.getTile(false);
					if (tCityTile != null && tMainTile.isSameIsland(tCityTile))
					{
						foreach (TileZone tZone in tCity.neighbour_zones)
						{
							if (!tZone.hasCity())
							{
								this._wave.Add(tZone);
								this._checked_zones.Add(tZone);
							}
						}
					}
				}
			}
			int tCurWave = 0;
			while (this._wave.Count > 0)
			{
				if (tDebug)
				{
					if (tCurWave == 0)
					{
						DebugHighlight.newHighlightList(BehKingCheckNewCityFoundation._color1, this._wave, 3f);
					}
					else if (tCurWave == 1)
					{
						DebugHighlight.newHighlightList(BehKingCheckNewCityFoundation._color2, this._wave, 3f);
					}
					else if (tCurWave == 2)
					{
						DebugHighlight.newHighlightList(BehKingCheckNewCityFoundation._color3, this._wave, 3f);
					}
					else if (tCurWave == 3)
					{
						DebugHighlight.newHighlightList(BehKingCheckNewCityFoundation._color4, this._wave, 3f);
					}
				}
				this.startWave(tCurWave, tMainTile, tGoodZones, pActor);
				if (this._next_wave.Count > 0)
				{
					this._wave.AddRange(this._next_wave);
					this._next_wave.Clear();
					tCurWave++;
					if (tCurWave >= 4)
					{
						break;
					}
				}
			}
			this._wave.Clear();
			this._checked_zones.Clear();
			if (tGoodZones.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tGoodZones.GetRandom<TileZone>();
			}
		}
		return result;
	}

	// Token: 0x0600227B RID: 8827 RVA: 0x001215CC File Offset: 0x0011F7CC
	private void startWave(int pWave, WorldTile pCityTile, ListPool<TileZone> pGoodZones, Actor pActor)
	{
		List<TileZone> tWave = this._wave;
		HashSet<TileZone> tCheckedZones = this._checked_zones;
		while (tWave.Count > 0)
		{
			TileZone tParentZone = tWave.Pop<TileZone>();
			tCheckedZones.Add(tParentZone);
			if (pWave > 2 && tParentZone.isGoodForNewCity(pActor) && tParentZone.centerTile.isSameIsland(pCityTile))
			{
				pGoodZones.Add(tParentZone);
			}
			foreach (TileZone tNeighbour in tParentZone.neighbours)
			{
				if (tCheckedZones.Add(tNeighbour) && !tNeighbour.hasCity())
				{
					this._next_wave.Add(tNeighbour);
				}
			}
		}
	}

	// Token: 0x0600227C RID: 8828 RVA: 0x00121664 File Offset: 0x0011F864
	private ListPool<City> getCityListForExpansion(Kingdom pKingdom)
	{
		ListPool<City> tPossibleCitiesForExpansion = new ListPool<City>(pKingdom.countCities());
		foreach (City tCity in pKingdom.getCities())
		{
			if (tCity.getTile(false) != null && tCity.status.population_adults >= 30 && !tCity.needSettlers())
			{
				tPossibleCitiesForExpansion.Add(tCity);
			}
		}
		tPossibleCitiesForExpansion.Shuffle<City>();
		return tPossibleCitiesForExpansion;
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x001216E4 File Offset: 0x0011F8E4
	private TileZone findCityForMigration(City pCity)
	{
		WorldTile tCityTile = pCity.getTile(false);
		if (tCityTile == null)
		{
			return null;
		}
		foreach (City tCity in pCity.kingdom.getCities().LoopRandom<City>())
		{
			if (tCity != pCity)
			{
				WorldTile tTargetTile = tCity.getTile(false);
				if (tTargetTile != null && tCityTile.reachableFrom(tTargetTile) && tCity.needSettlers())
				{
					WorldTile tile = tCity.getTile(false);
					TileZone tBestZone = (tile != null) ? tile.zone : null;
					if (tBestZone != null)
					{
						return tBestZone;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x040018ED RID: 6381
	private const int MAX_MOVED = 6;

	// Token: 0x040018EE RID: 6382
	private List<TileZone> _next_wave = new List<TileZone>();

	// Token: 0x040018EF RID: 6383
	private List<TileZone> _wave = new List<TileZone>();

	// Token: 0x040018F0 RID: 6384
	private HashSet<TileZone> _checked_zones = new HashSet<TileZone>();

	// Token: 0x040018F1 RID: 6385
	private static Color _color1 = new Color(1f, 0f, 0f, 0.3f);

	// Token: 0x040018F2 RID: 6386
	private static Color _color2 = new Color(0f, 0f, 1f, 0.3f);

	// Token: 0x040018F3 RID: 6387
	private static Color _color3 = new Color(1f, 0.92f, 0.016f, 0.3f);

	// Token: 0x040018F4 RID: 6388
	private static Color _color4 = new Color(0f, 1f, 0f, 0.3f);
}
