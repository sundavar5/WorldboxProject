using System;
using System.Collections.Generic;
using System.Globalization;
using life.taxi;
using UnityEngine;
using UnityPools;

// Token: 0x020000DD RID: 221
public class DebugToolLibrary : AssetLibrary<DebugToolAsset>
{
	// Token: 0x0600067A RID: 1658 RVA: 0x00060630 File Offset: 0x0005E830
	public override void init()
	{
		base.init();
		this.initBenchmarks();
		this.initMain();
		this.initGameplay();
		this.initMap();
		this.initAI();
		this.initCity();
		this.initSystems();
		this.initSubSystems();
		this.initFmod();
		this.initDiagnosticGameplay();
		this.initUI();
		this.initDebugConfigDefaults();
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0006068C File Offset: 0x0005E88C
	private void initDebugConfigDefaults()
	{
		foreach (string tID in DebugConfig.default_debug_tools)
		{
			DebugToolAsset tAsset = this.get(tID);
			if (tAsset != null)
			{
				tAsset.show_on_start = true;
			}
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x000606EC File Offset: 0x0005E8EC
	private void initDiagnosticGameplay()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "hotkeys_nanoobjects";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			HotkeyTabsData tDataMap = World.world.hotkey_tabs_data;
			Dictionary<string, PlayerOptionData> tDataGlobal = PlayerConfig.dict;
			pTool.setText("#map:", "-------------", 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_1:", tDataMap.hotkey_data_1, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_2:", tDataMap.hotkey_data_2, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_3:", tDataMap.hotkey_data_3, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_4:", tDataMap.hotkey_data_4, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_5:", tDataMap.hotkey_data_5, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_6:", tDataMap.hotkey_data_6, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_7:", tDataMap.hotkey_data_7, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_8:", tDataMap.hotkey_data_8, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_9:", tDataMap.hotkey_data_9, 0f, false, 0L, false, false, "");
			pTool.setText("hotkey_data_0:", tDataMap.hotkey_data_0, 0f, false, 0L, false, false, "");
			pTool.setText("#global_config:", "-------------", 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_1:", tDataGlobal["hotkey_1"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_2:", tDataGlobal["hotkey_2"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_3:", tDataGlobal["hotkey_3"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_4:", tDataGlobal["hotkey_4"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_5:", tDataGlobal["hotkey_5"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_6:", tDataGlobal["hotkey_6"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_7:", tDataGlobal["hotkey_7"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_8:", tDataGlobal["hotkey_8"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_9:", tDataGlobal["hotkey_9"].stringVal, 0f, false, 0L, false, false, "");
			pTool.setText("global_hotkey_0:", tDataGlobal["hotkey_0"].stringVal, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset);
		this.add(new DebugToolAsset
		{
			id = "reproduction_diagnostic_cursor",
			action_1 = delegate(DebugTool pTool)
			{
				WorldTile tTile = World.world.getMouseTilePos();
				if (tTile == null)
				{
					return;
				}
				if (Zones.showCityZones(false))
				{
					City tCity = tTile.zone.city;
					if (tCity == null)
					{
						return;
					}
					Subspecies tSubspecies = tCity.getMainSubspecies();
					if (tSubspecies == null)
					{
						return;
					}
					this.showReproductionDebugInfo(pTool, tSubspecies);
					return;
				}
				else
				{
					if (!Zones.showKingdomZones(false))
					{
						if (Zones.showSpeciesZones(false))
						{
							ZoneMetaData tData = ZoneMetaDataVisualizer.getZoneMetaData(tTile.zone);
							if (tData.meta_object != null && tData.meta_object.isAlive())
							{
								this.showReproductionDebugInfo(pTool, tData.meta_object as Subspecies);
							}
						}
						return;
					}
					City tCity2 = tTile.zone.city;
					if (tCity2 == null)
					{
						return;
					}
					Subspecies tSubspecies2 = tCity2.kingdom.getMainSubspecies();
					if (tSubspecies2 == null)
					{
						return;
					}
					this.showReproductionDebugInfo(pTool, tSubspecies2);
					return;
				}
			}
		});
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "reproduction_diagnostic_total";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			Dictionary<string, int> tTotal = UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Get();
			Dictionary<string, int> tLast = UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Get();
			foreach (Subspecies subspecies in World.world.subspecies)
			{
				foreach (RateCounter tCounter in subspecies.list_counters)
				{
					tTotal[tCounter.id] = tTotal.GetValueOrDefault(tCounter.id) + tCounter.getTotal();
					tLast[tCounter.id] = tLast.GetValueOrDefault(tCounter.id) + tCounter.getEventsPerMinute();
				}
			}
			foreach (KeyValuePair<string, int> tPair in tTotal)
			{
				pTool.setText(tPair.Key + ":", string.Format("{0} | tot: {1}", tLast[tPair.Key], tPair.Value), 0f, false, 0L, false, false, "");
			}
			UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Release(tTotal);
			UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Release(tLast);
		};
		this.add(debugToolAsset2);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0006079A File Offset: 0x0005E99A
	private void showReproductionDebugInfo(DebugTool pTool, Subspecies pSubspecies)
	{
		pSubspecies.debugReproductionEvents(pTool);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x000607A4 File Offset: 0x0005E9A4
	private void initCity()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "Cities";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			List<City> list = new List<City>(World.world.cities);
			list.Sort(new Comparison<City>(pTool.citySorter));
			foreach (City tCity in list)
			{
				if (pTool.textCount > 0)
				{
					pTool.setSeparator();
				}
				pTool.setText("#name:", tCity.name, 0f, false, 0L, false, false, "");
				pTool.setText("pep:", tCity.getPopulationPeople(), 0f, false, 0L, false, false, "");
				pTool.setText("units:", tCity.getUnitsTotal(), 0f, false, 0L, false, false, "");
				pTool.setText("boats:", tCity.countBoats(), 0f, false, 0L, false, false, "");
				pTool.setText("zones:", tCity.zones.Count, 0f, false, 0L, false, false, "");
				pTool.setText("buildings:", tCity.buildings.Count, 0f, false, 0L, false, false, "");
				pTool.setText("city_center:", tCity.city_center, 0f, false, 0L, false, false, "");
				if (pTool.textCount > 30)
				{
					pTool.setSeparator();
					pTool.setText("more...", "...", 0f, false, 0L, false, false, "");
					break;
				}
			}
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "City Loyalty";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			List<City> list = new List<City>(World.world.cities);
			list.Sort(new Comparison<City>(pTool.citySorter));
			int tAboveZero = 0;
			int tBelowZero = 0;
			using (List<City>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.getCachedLoyalty() >= 0)
					{
						tAboveZero++;
					}
					else
					{
						tBelowZero++;
					}
				}
			}
			pTool.setText("cities with loyalty above 0:", tAboveZero, 0f, false, 0L, false, false, "");
			pTool.setText("cities with loyalty below 0:", tBelowZero, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "City Capture";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			if (tCity.being_captured_by != null && tCity.being_captured_by.isAlive())
			{
				pTool.setText("capturing by:", tCity.being_captured_by.name, 0f, false, 0L, false, false, "");
			}
			pTool.setText("ticks:", tCity.getCaptureTicks(), 0f, false, 0L, false, false, "");
			tCity.debugCaptureUnits(pTool);
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "City Tasks";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			pTool.setText("trees:", tCity.tasks.trees, 0f, false, 0L, false, false, "");
			pTool.setText("stone:", tCity.tasks.minerals, 0f, false, 0L, false, false, "");
			pTool.setText("minerals:", tCity.tasks.minerals, 0f, false, 0L, false, false, "");
			pTool.setText("bushes:", tCity.tasks.bushes, 0f, false, 0L, false, false, "");
			pTool.setText("plants:", tCity.tasks.plants, 0f, false, 0L, false, false, "");
			pTool.setText("hives:", tCity.tasks.hives, 0f, false, 0L, false, false, "");
			pTool.setText("farm_fields:", tCity.tasks.farm_fields, 0f, false, 0L, false, false, "");
			pTool.setText("wheats:", tCity.tasks.wheats, 0f, false, 0L, false, false, "");
			pTool.setText("ruins:", tCity.tasks.ruins, 0f, false, 0L, false, false, "");
			pTool.setText("poops:", tCity.tasks.poops, 0f, false, 0L, false, false, "");
			pTool.setText("roads:", tCity.tasks.roads, 0f, false, 0L, false, false, "");
			pTool.setText("fire:", tCity.tasks.fire, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "city_jobs";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			int tTotal = 0;
			int tTotalOcuppied = 0;
			foreach (CitizenJobAsset tAsset in tCity.jobs.jobs.Keys)
			{
				int tCount = tCity.jobs.jobs[tAsset];
				int tOccupied = 0;
				if (tCity.jobs.occupied.ContainsKey(tAsset))
				{
					tOccupied = tCity.jobs.occupied[tAsset];
				}
				tTotal += tCount;
				tTotalOcuppied += tOccupied;
				pTool.setText(tAsset.id + ":", tOccupied.ToString() + "/" + tCount.ToString(), 0f, false, 0L, false, false, "");
			}
			foreach (CitizenJobAsset tAsset2 in tCity.jobs.occupied.Keys)
			{
				if (!tCity.jobs.jobs.ContainsKey(tAsset2))
				{
					int tOccupied2 = tCity.jobs.occupied[tAsset2];
					tTotalOcuppied += tOccupied2;
					pTool.setText(tAsset2.id + ":", tOccupied2.ToString() + "/" + 0.ToString(), 0f, false, 0L, false, false, "");
				}
			}
			pTool.setSeparator();
			pTool.setText("total JOBS:", tTotalOcuppied.ToString() + "/" + tTotal.ToString(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("pop:", tCity.getPopulationPeople().ToString() + " / " + tCity.getPopulationMaximum().ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("adults/children:", tCity.countAdults().ToString() + "/" + tCity.countChildren().ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("food:", tCity.countFood(), 0f, false, 0L, false, false, "");
			pTool.setText("hungry/starving:", tCity.countHungry().ToString() + "/" + tCity.countStarving().ToString(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "City Info";
		debugToolAsset6.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			pTool.setText("#name:", tCity.name, 0f, false, 0L, false, false, "");
			pTool.setText("city units all:", tCity.getUnitsTotal(), 0f, false, 0L, false, false, "");
			pTool.setText("city people:", tCity.getPopulationPeople(), 0f, false, 0L, false, false, "");
			pTool.setText("units:", tCity.getPopulationPeople().ToString() + "/" + tCity.getPopulationMaximum().ToString(), 0f, false, 0L, false, false, "");
			if (tCity.getPopulationMaximum() != tCity.status.housing_total)
			{
				pTool.setText("unit housing:", tCity.getPopulationPeople().ToString() + "/" + tCity.status.housing_total.ToString(), 0f, false, 0L, false, false, "");
			}
			pTool.setText("in houses:", tCity.countInHouses(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			if (tCity.hasLeader())
			{
				pTool.setText("leader:", tCity.leader.getName(), 0f, false, 0L, false, false, "");
			}
			if (tCity.hasKingdom())
			{
				pTool.setText("kingdom:", tCity.kingdom.name, 0f, false, 0L, false, false, "");
			}
			if (tCity.hasKingdom())
			{
				pTool.setText("#name:", tCity.kingdom.id, 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
			pTool.setText("zones:", tCity.zones.Count, 0f, false, 0L, false, false, "");
			pTool.setText("buildings:", tCity.buildings.Count, 0f, false, 0L, false, false, "");
			pTool.setText("homes free:", tCity.status.housing_free, 0f, false, 0L, false, false, "");
			pTool.setText("homes occupied:", tCity.status.housing_occupied, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setSeparator();
			pTool.setText("roads to build:", tCity.road_tiles_to_build.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setSeparator();
		};
		this.add(debugToolAsset6);
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "city_storage";
		debugToolAsset7.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			if (!tCity.hasStorages())
			{
				return;
			}
			for (int i = 0; i < tCity.storages.Count; i++)
			{
				foreach (string tResourceKey in tCity.storages[i].resources.getKeys())
				{
					pTool.setText(string.Concat(new string[]
					{
						"stock_",
						i.ToString(),
						":",
						tResourceKey,
						":"
					}), tCity.getResourcesAmount(tResourceKey), 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset7);
		DebugToolAsset debugToolAsset8 = new DebugToolAsset();
		debugToolAsset8.id = "City Buildings";
		debugToolAsset8.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			pTool.setSeparator();
			int tCountType = 0;
			int tCountName = 0;
			pTool.setText("#type", "", 0f, false, 0L, false, false, "");
			foreach (string tKey in tCity.buildings_dict_type.Keys)
			{
				pTool.setText(tKey + ":", tCity.buildings_dict_type[tKey].Count, 0f, false, 0L, false, false, "");
				tCountType += tCity.buildings_dict_type[tKey].Count;
			}
			pTool.setSeparator();
			pTool.setText("#name", "", 0f, false, 0L, false, false, "");
			foreach (string tKey2 in tCity.buildings_dict_id.Keys)
			{
				pTool.setText(tKey2 + ":", tCity.buildings_dict_id[tKey2].Count, 0f, false, 0L, false, false, "");
				tCountName += tCity.buildings_dict_id[tKey2].Count;
			}
			pTool.setSeparator();
			pTool.setText("total:", tCity.buildings.Count, 0f, false, 0L, false, false, "");
			pTool.setText("total by type:", tCountType, 0f, false, 0L, false, false, "");
			pTool.setText("total by name:", tCountName, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset8);
		DebugToolAsset debugToolAsset9 = new DebugToolAsset();
		debugToolAsset9.id = "City Professions";
		debugToolAsset9.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			pTool.setSeparator();
			pTool.setText("total:", tCity.units.Count, 0f, false, 0L, false, false, "");
			pTool.setText("king:", tCity.countProfession(UnitProfession.King), 0f, false, 0L, false, false, "");
			pTool.setText("leader:", tCity.countProfession(UnitProfession.Leader), 0f, false, 0L, false, false, "");
			pTool.setText("units:", tCity.countProfession(UnitProfession.Unit), 0f, false, 0L, false, false, "");
			pTool.setText("babies:", tCity.countChildren(), 0f, false, 0L, false, false, "");
			pTool.setText("warriors:", tCity.countProfession(UnitProfession.Warrior), 0f, false, 0L, false, false, "");
			pTool.setText("null:", tCity.countProfession(UnitProfession.Nothing), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset9);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000609D0 File Offset: 0x0005EBD0
	private void initSystems()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "Effects";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			ExplosionChecker.debug(pTool);
			foreach (BaseEffectController baseEffectController in World.world.stack_effects.list)
			{
				baseEffectController.debug(pTool);
			}
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "Auto Tester";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			if (World.world.auto_tester != null)
			{
				pTool.setText("active:", World.world.auto_tester.active, 0f, false, 0L, false, false, "");
				pTool.setText("d_string:", World.world.auto_tester.debugString, 0f, false, 0L, false, false, "");
				World.world.auto_tester.ai.debug(pTool);
			}
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "Controllable Units";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("isOverUI:", World.world.isOverUI(), 0f, false, 0L, false, false, "");
			pTool.setText("isGameplayControlsLocked:", World.world.isGameplayControlsLocked(), 0f, false, 0L, false, false, "");
			pTool.setText("controlsLocked:", MapBox.controlsLocked(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("IsselectedUnit:", SelectedUnit.isSet(), 0f, false, 0L, false, false, "");
			pTool.setText("Total Selected:", SelectedUnit.getAllSelected().Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("ControllableUnit:", ControllableUnit.isControllingUnit(), 0f, false, 0L, false, false, "");
			pTool.setText("Total Controlled:", ControllableUnit.getCotrolledUnits().Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("Square Selection:", World.world.player_control.square_selection_started, 0f, false, 0L, false, false, "");
			pTool.setText("Square Selection Pos:", World.world.player_control.square_selection_position_current, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Selected Unit";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			Actor tSelected = SelectedUnit.unit;
			if (tSelected == null)
			{
				return;
			}
			Actor tCursorTarget = World.world.getActorNearCursor();
			if (tCursorTarget != null)
			{
				float tDist = tSelected.distanceToObjectTarget(tCursorTarget);
				pTool.setText("dist to target:", tDist, 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "Window";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			ScrollWindow.debug(pTool);
			pTool.setSeparator();
			WindowHistory.debug(pTool);
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "Selected Meta";
		debugToolAsset6.action_1 = delegate(DebugTool pTool)
		{
			AssetManager.meta_type_library.debug(pTool);
		};
		this.add(debugToolAsset6);
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "Camera";
		debugToolAsset7.action_1 = delegate(DebugTool pTool)
		{
			World.world.move_camera.debug(pTool);
			pTool.setSeparator();
			pTool.setText("zoom", World.world.camera.orthographicSize, 0f, false, 0L, false, false, "");
			pTool.setText("aspect", World.world.camera.aspect, 0f, false, 0L, false, false, "");
			pTool.setText("zoom_bound_mod", World.world.quality_changer.getZoomRateBoundLow(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("visible zones", World.world.zone_camera.countVisibleZones().ToString() + "/" + World.world.zone_calculator.zones.Count.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("Input.touchCount", Input.touchCount, 0f, false, 0L, false, false, "");
			pTool.setText("origin_touch_dist", World.world.player_control.getDistanceBetweenOriginAndCurrentTouch(), 0f, false, 0L, false, false, "");
			pTool.setText("getDebugDragThreshold", World.world.player_control.getCurrentDragDistance().ToString("F3") + " / " + 0.007f.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("getDebugDragThreshold %", (World.world.player_control.getCurrentDragDistance() * 100f).ToString("F3") + "%", 0f, false, 0L, false, false, "");
			pTool.setText("isTouchMoreThanDragThreshold %", World.world.player_control.isTouchMoreThanDragThreshold(), 0f, false, 0L, false, false, "");
			pTool.setText("already_used_camera_drag", World.world.player_control.already_used_camera_drag, 0f, false, 0L, false, false, "");
			pTool.setText("inspect_timer_click", World.world.player_control.inspect_timer_click, 0f, false, 0L, false, false, "");
			pTool.setText("touch_timer", World.world.player_control.touch_ticks_skip, 0f, false, 0L, false, false, "");
			if (Input.touchCount > 0)
			{
				for (int i = 0; i < Input.touchCount; i++)
				{
					pTool.setText("Touch.fingerId[" + i.ToString() + "]", Input.GetTouch(i).fingerId, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.rawPosition[" + i.ToString() + "]", Input.GetTouch(i).rawPosition, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.pos[" + i.ToString() + "]", Input.GetTouch(i).position, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.dpos[" + i.ToString() + "]", Input.GetTouch(i).deltaPosition, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.delta[" + i.ToString() + "]", Input.GetTouch(i).deltaTime, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.radius[" + i.ToString() + "]", Input.GetTouch(i).radius, 0f, false, 0L, false, false, "");
					pTool.setText("Touch.pressure[" + i.ToString() + "]", Input.GetTouch(i).pressure, 0f, false, 0L, false, false, "");
				}
			}
			pTool.setText("Axis Vertical", Input.GetAxis("Vertical"), 0f, false, 0L, false, false, "");
			pTool.setText("Axis Horizontal", Input.GetAxis("Horizontal"), 0f, false, 0L, false, false, "");
			pTool.setText("Input.touchSupported", Input.touchSupported, 0f, false, 0L, false, false, "");
			pTool.setText("Input.touchPressureSupported", Input.touchPressureSupported, 0f, false, 0L, false, false, "");
			pTool.setText("Input.multiTouchEnabled", Input.multiTouchEnabled, 0f, false, 0L, false, false, "");
			pTool.setText("Input.stylusTouchSupported", Input.stylusTouchSupported, 0f, false, 0L, false, false, "");
			pTool.setText("Input.simulateMouseWithTouches", Input.simulateMouseWithTouches, 0f, false, 0L, false, false, "");
			pTool.setText("Input.mousePresent", Input.mousePresent, 0f, false, 0L, false, false, "");
			pTool.setText("Input.mousePosition", Input.mousePosition, 0f, false, 0L, false, false, "");
			pTool.setText("Input.mouseScrollDelta", Input.mouseScrollDelta, 0f, false, 0L, false, false, "");
			pTool.setText("Button 0", Input.GetMouseButton(0), 0f, false, 0L, false, false, "");
			pTool.setText("Button 1", Input.GetMouseButton(1), 0f, false, 0L, false, false, "");
			pTool.setText("Button 2", Input.GetMouseButton(2), 0f, false, 0L, false, false, "");
			pTool.setText("Axis ScrollWheel", Input.mouseScrollDelta.y, 0f, false, 0L, false, false, "");
			pTool.setText("Axis Mouse X", Input.GetAxis("Mouse X"), 0f, false, 0L, false, false, "");
			pTool.setText("Axis Mouse Y", Input.GetAxis("Mouse Y"), 0f, false, 0L, false, false, "");
			pTool.setText("Raw Mouse X", Input.GetAxisRaw("Mouse X"), 0f, false, 0L, false, false, "");
			pTool.setText("Raw Mouse Y", Input.GetAxisRaw("Mouse Y"), 0f, false, 0L, false, false, "");
			pTool.setText("Velocity", World.world.move_camera.getVelocity(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset7);
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x00060B84 File Offset: 0x0005ED84
	private void initMap()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "tile_info";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			pTool.setText("x", tTile.x, 0f, false, 0L, false, false, "");
			pTool.setText("y", tTile.y, 0f, false, 0L, false, false, "");
			pTool.setText("id", tTile.data.tile_id, 0f, false, 0L, false, false, "");
			pTool.setText("height", tTile.data.height, 0f, false, 0L, false, false, "");
			pTool.setText("type", tTile.Type.id, 0f, false, 0L, false, false, "");
			pTool.setText("layer", tTile.Type.layer_type, 0f, false, 0L, false, false, "");
			pTool.setText("main tile", (tTile.main_type != null) ? tTile.main_type.id : "-", 0f, false, 0L, false, false, "");
			pTool.setText("cap tile", (tTile.top_type != null) ? tTile.top_type.id : "-", 0f, false, 0L, false, false, "");
			pTool.setText("burned", tTile.burned_stages, 0f, false, 0L, false, false, "");
			pTool.setText("targetedBy", tTile.isTargeted(), 0f, false, 0L, false, false, "");
			pTool.setText("units", tTile.countUnits(), 0f, false, 0L, false, false, "");
			pTool.setText("good_for_boat", tTile.isGoodForBoat(), 0f, false, 0L, false, false, "");
			pTool.setText("heat", tTile.heat, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("--zone:", "", 0f, false, 0L, false, false, "");
			TileZone tZone = tTile.zone;
			if (tZone.hasAnyBuildingsInSet(BuildingList.Civs))
			{
				pTool.setText("buildings:", tZone.getHashset(BuildingList.Civs).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Ruins))
			{
				pTool.setText("ruins:", tZone.getHashset(BuildingList.Ruins).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Trees))
			{
				pTool.setText("trees:", tZone.getHashset(BuildingList.Trees).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Minerals))
			{
				pTool.setText("stone:", tZone.getHashset(BuildingList.Minerals).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Food))
			{
				pTool.setText("fruits:", tZone.getHashset(BuildingList.Food).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Hives))
			{
				pTool.setText("hives:", tZone.getHashset(BuildingList.Hives).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Poops))
			{
				pTool.setText("poops:", tZone.getHashset(BuildingList.Poops).Count, 0f, false, 0L, false, false, "");
			}
			if (tZone.isZoneOnFire())
			{
				pTool.setText("fire:", WorldBehaviourActionFire.countFires(tZone), 0f, false, 0L, false, false, "");
			}
			if (tZone.tiles_with_liquid > 0)
			{
				pTool.setText("water tiles:", tZone.tiles_with_liquid, 0f, false, 0L, false, false, "");
			}
			if (tZone.tiles_with_ground > 0)
			{
				pTool.setText("ground tiles:", tZone.tiles_with_ground, 0f, false, 0L, false, false, "");
			}
			if (tZone.city != null)
			{
				pTool.setText("city:", tZone.city.name, 0f, false, 0L, false, false, "");
			}
			if (tZone.city != null && tZone.city.kingdom != null)
			{
				pTool.setText("kingdom:", tZone.city.kingdom.name, 0f, false, 0L, false, false, "");
			}
			if (tTile.hasBuilding())
			{
				pTool.setSeparator();
				pTool.setText("--building:", "", 0f, false, 0L, false, false, "");
				pTool.setText("resources:", tTile.building.hasResourcesToCollect(), 0f, false, 0L, false, false, "");
				pTool.setText("alive:", tTile.building.isAlive(), 0f, false, 0L, false, false, "");
				pTool.setText("is_usable:", tTile.building.isUsable(), 0f, false, 0L, false, false, "");
				pTool.setText("city:", (tTile.building.city != null) ? tTile.building.city.name : "-", 0f, false, 0L, false, false, "");
				string pT = "kingdom:";
				City city = tTile.building.city;
				pTool.setText(pT, (((city != null) ? city.kingdom : null) != null) ? tTile.building.city.kingdom.name : "-", 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "Connections";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			RegionLinkHashes.debug(pTool);
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "Region";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			if (tTile.region == null)
			{
				return;
			}
			WorldTile tCursorTile = World.world.getMouseTilePos();
			if (tCursorTile == null)
			{
				return;
			}
			MapRegion tRegion = tCursorTile.region;
			if (tRegion == null)
			{
				return;
			}
			bool tEmptyRegionNeighbour = false;
			string tEmptyRegionNeighbourID = "";
			foreach (MapRegion tReg in tRegion.neighbours)
			{
				if (tReg.tiles.Count == 0)
				{
					tEmptyRegionNeighbour = true;
					tEmptyRegionNeighbourID = tReg.id.ToString();
					break;
				}
			}
			pTool.setText("- region id:", tRegion.id, 0f, false, 0L, false, false, "");
			pTool.setText("-chunk id:", tRegion.chunk.id, 0f, false, 0L, false, false, "");
			pTool.setText("-chunk xy:", tRegion.chunk.x.ToString() + " " + tRegion.chunk.y.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("- tEmptyRegionNeighbour:", tEmptyRegionNeighbour, 0f, false, 0L, false, false, "");
			if (tEmptyRegionNeighbour)
			{
				pTool.setText("- tEmptyRegionNeighbourID:", tEmptyRegionNeighbourID, 0f, false, 0L, false, false, "");
			}
			pTool.setText("- getEdgeTiles :", tRegion.getEdgeTiles().Count, 0f, false, 0L, false, false, "");
			pTool.setText("- used in path :", tRegion.used_by_path_lock.ToString() + " " + tRegion.region_path_id.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("- region wave:", tRegion.path_wave_id, 0f, false, 0L, false, false, "");
			pTool.setText("- centerRegion:", tRegion.center_region, 0f, false, 0L, false, false, "");
			pTool.setText("- region tiles:", tRegion.tiles.Count, 0f, false, 0L, false, false, "");
			pTool.setText("- region neigbours:", tRegion.neighbours.Count, 0f, false, 0L, false, false, "");
			pTool.setText("- created:", tRegion.created, 0f, false, 0L, false, false, "");
			pTool.setText("- island:", tRegion.island == null, 0f, false, 0L, false, false, "");
			pTool.setText("- getEdgeRegions:", tRegion.getEdgeRegions().Count, 0f, false, 0L, false, false, "");
			pTool.setText("- island connections:", tRegion.island.getConnectedIslands().Count, 0f, false, 0L, false, false, "");
			string pT = "- debug_connections_left:";
			List<WorldTile> debug_blink_edges_left = tRegion.debug_blink_edges_left;
			pTool.setText(pT, (debug_blink_edges_left != null) ? new int?(debug_blink_edges_left.Count) : null, 0f, false, 0L, false, false, "");
			string pT2 = "- debug_connections_right:";
			List<WorldTile> debug_blink_edges_right = tRegion.debug_blink_edges_right;
			pTool.setText(pT2, (debug_blink_edges_right != null) ? new int?(debug_blink_edges_right.Count) : null, 0f, false, 0L, false, false, "");
			string pT3 = "- debug_connections_up:";
			List<WorldTile> debug_blink_edges_up = tRegion.debug_blink_edges_up;
			pTool.setText(pT3, (debug_blink_edges_up != null) ? new int?(debug_blink_edges_up.Count) : null, 0f, false, 0L, false, false, "");
			string pT4 = "- debug_connections_down:";
			List<WorldTile> debug_blink_edges_down = tRegion.debug_blink_edges_down;
			pTool.setText(pT4, (debug_blink_edges_down != null) ? new int?(debug_blink_edges_down.Count) : null, 0f, false, 0L, false, false, "");
			tCursorTile.region.debugLinks(pTool);
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Zone Info";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			World.world.zone_calculator.debug(pTool);
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			MapChunk chunk = tTile.chunk;
			pTool.setText("visible:", tTile.zone.visible, 0f, false, 0L, false, false, "");
			string pT = "buildings:";
			HashSet<Building> hashset = tTile.zone.getHashset(BuildingList.Civs);
			pTool.setText(pT, (hashset != null) ? new int?(hashset.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("types:", tTile.zone.countNotNullTypes(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("id:", tTile.zone.id, 0f, false, 0L, false, false, "");
			pTool.setText("pos:", "x: " + tTile.zone.x.ToString() + ", y: " + tTile.zone.y.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("city:", tTile.zone.hasCity(), 0f, false, 0L, false, false, "");
			string pT2 = "bushes:";
			HashSet<Building> hashset2 = tTile.zone.getHashset(BuildingList.Food);
			pTool.setText(pT2, (hashset2 != null) ? new int?(hashset2.Count) : null, 0f, false, 0L, false, false, "");
			string pT3 = "hives:";
			HashSet<Building> hashset3 = tTile.zone.getHashset(BuildingList.Hives);
			pTool.setText(pT3, (hashset3 != null) ? new int?(hashset3.Count) : null, 0f, false, 0L, false, false, "");
			string pT4 = "trees:";
			HashSet<Building> hashset4 = tTile.zone.getHashset(BuildingList.Trees);
			pTool.setText(pT4, (hashset4 != null) ? new int?(hashset4.Count) : null, 0f, false, 0L, false, false, "");
			string pT5 = "poops:";
			HashSet<Building> hashset5 = tTile.zone.getHashset(BuildingList.Poops);
			pTool.setText(pT5, (hashset5 != null) ? new int?(hashset5.Count) : null, 0f, false, 0L, false, false, "");
			string pT6 = "deposits:";
			HashSet<Building> hashset6 = tTile.zone.getHashset(BuildingList.Minerals);
			pTool.setText(pT6, (hashset6 != null) ? new int?(hashset6.Count) : null, 0f, false, 0L, false, false, "");
			string pT7 = "flore:";
			HashSet<Building> hashset7 = tTile.zone.getHashset(BuildingList.Flora);
			pTool.setText(pT7, (hashset7 != null) ? new int?(hashset7.Count) : null, 0f, false, 0L, false, false, "");
			string pT8 = "buildings:";
			HashSet<Building> hashset8 = tTile.zone.getHashset(BuildingList.Civs);
			pTool.setText(pT8, (hashset8 != null) ? new int?(hashset8.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("buildings all:", tTile.zone.buildings_all.Count, 0f, false, 0L, false, false, "");
			string pT9 = "abandoned:";
			HashSet<Building> hashset9 = tTile.zone.getHashset(BuildingList.Abandoned);
			pTool.setText(pT9, (hashset9 != null) ? new int?(hashset9.Count) : null, 0f, false, 0L, false, false, "");
			string pT10 = "ruins:";
			HashSet<Building> hashset10 = tTile.zone.getHashset(BuildingList.Ruins);
			pTool.setText(pT10, (hashset10 != null) ? new int?(hashset10.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("tilesWithGround:", tTile.zone.tiles_with_ground, 0f, false, 0L, false, false, "");
			string pT11 = "count deep ocean:";
			HashSet<WorldTile> tilesOfType = tTile.zone.getTilesOfType(TileLibrary.deep_ocean);
			pTool.setText(pT11, (tilesOfType != null) ? new int?(tilesOfType.Count) : null, 0f, false, 0L, false, false, "");
			string pT12 = "count soil:";
			HashSet<WorldTile> tilesOfType2 = tTile.zone.getTilesOfType(TileLibrary.soil_low);
			pTool.setText(pT12, (tilesOfType2 != null) ? new int?(tilesOfType2.Count) : null, 0f, false, 0L, false, false, "");
			string pT13 = "count fuse:";
			HashSet<WorldTile> tilesOfType3 = tTile.zone.getTilesOfType(TopTileLibrary.fuse);
			pTool.setText(pT13, (tilesOfType3 != null) ? new int?(tilesOfType3.Count) : null, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "map_chunks";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			int tChunkCount = World.world.map_chunk_manager.chunks.Length;
			if (tChunkCount < 1)
			{
				return;
			}
			int tMinObjects = int.MaxValue;
			int tMaxObjects = 0;
			int tTotalObjects = 0;
			int tKMinObjects = int.MaxValue;
			int tKMaxObjects = 0;
			int tKTotalObjects = 0;
			foreach (MapChunk tChunk in World.world.map_chunk_manager.chunks)
			{
				int tKingdoms = tChunk.objects.kingdoms.Count;
				if (tKingdoms < tMinObjects)
				{
					tMinObjects = tKingdoms;
				}
				if (tKingdoms > tMaxObjects)
				{
					tMaxObjects = tKingdoms;
				}
				tTotalObjects += tKingdoms;
				foreach (List<Actor> list in tChunk.objects.getDebugUnits())
				{
					int tKingdomObjects = list.Count;
					if (tKingdomObjects < tKMinObjects)
					{
						tKMinObjects = tKingdomObjects;
					}
					if (tKingdomObjects > tKMaxObjects)
					{
						tKMaxObjects = tKingdomObjects;
					}
					tKTotalObjects += tKingdomObjects;
				}
				foreach (List<Building> list2 in tChunk.objects.getDebugBuildings())
				{
					int tKingdomObjects2 = list2.Count;
					if (tKingdomObjects2 < tKMinObjects)
					{
						tKMinObjects = tKingdomObjects2;
					}
					if (tKingdomObjects2 > tKMaxObjects)
					{
						tKMaxObjects = tKingdomObjects2;
					}
					tKTotalObjects += tKingdomObjects2;
				}
			}
			pTool.setText("batches:", DebugConfig.isOn(DebugOption.ChunkBatches), 0f, false, 0L, false, false, "");
			pTool.setText("debug_batch_size:", ParallelHelper.DEBUG_BATCH_SIZE, 0f, false, 0L, false, false, "");
			pTool.setText("chunks:", tChunkCount, 0f, false, 0L, false, false, "");
			pTool.setText("objects:", tTotalObjects, 0f, false, 0L, false, false, "");
			pTool.setText("objects min:", tMinObjects, 0f, false, 0L, false, false, "");
			pTool.setText("objects max:", tMaxObjects, 0f, false, 0L, false, false, "");
			pTool.setText("objects avg:", tTotalObjects / tChunkCount, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("kingdom objects:", tKTotalObjects, 0f, false, 0L, false, false, "");
			pTool.setText("kingdom objects min:", tKMinObjects, 0f, false, 0L, false, false, "");
			pTool.setText("kingdom objects max:", tKMaxObjects, 0f, false, 0L, false, false, "");
			pTool.setText("kingdom objects avg:", tKTotalObjects / tChunkCount, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "Map Chunk";
		debugToolAsset6.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			MapChunk tChunk = tTile.chunk;
			pTool.setText("chunk_id:", tChunk.id, 0f, false, 0L, false, false, "");
			pTool.setText("chunk_x/y:", tChunk.x.ToString() + "/" + tChunk.y.ToString(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("kingdoms:", tChunk.objects.kingdoms.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setSeparator();
			pTool.setText("total_units:", tChunk.objects.total_units, 0f, false, 0L, false, false, "");
			pTool.setText("total_buildings:", tChunk.objects.total_buildings, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("total:", tChunk.objects.total_units + tChunk.objects.total_buildings, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset6);
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "Island Info";
		debugToolAsset7.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			if (tTile.region == null)
			{
				return;
			}
			TileIsland tIsland = tTile.region.island;
			if (tIsland == null)
			{
				return;
			}
			pTool.setText("islands:", World.world.islands_calculator.islands.Count, 0f, false, 0L, false, false, "");
			pTool.setText("regions:", tIsland.regions.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("id:", tIsland.id, 0f, false, 0L, false, false, "");
			pTool.setText("hash:", tIsland.debug_hash_code, 0f, false, 0L, false, false, "");
			pTool.setText("tiles:", tIsland.getTileCount(), 0f, false, 0L, false, false, "");
			pTool.setText("unit limit:", tIsland.regions.Count * 4, 0f, false, 0L, false, false, "");
			pTool.setText("created:", tIsland.created, 0f, false, 0L, false, false, "");
			pTool.setText("type:", tIsland.type, 0f, false, 0L, false, false, "");
			string pT = "docks:";
			ListPool<Docks> docks = tIsland.docks;
			pTool.setText(pT, (docks != null) ? new int?(docks.Count) : null, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset7);
		DebugToolAsset debugToolAsset8 = new DebugToolAsset();
		debugToolAsset8.id = "Tilemap Renderer";
		debugToolAsset8.action_1 = delegate(DebugTool pTool)
		{
			World.world.tilemap.debug(pTool);
		};
		this.add(debugToolAsset8);
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x00060D74 File Offset: 0x0005EF74
	private void initSubSystems()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "boat";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			Actor tUn = null;
			int tBestDist = int.MaxValue;
			foreach (Actor tActor in World.world.units)
			{
				int tDist = Toolbox.SquaredDistTile(tActor.current_tile, tTile);
				if (tActor.asset.is_boat && tDist < tBestDist)
				{
					tUn = tActor;
					tBestDist = tDist;
				}
			}
			if (tUn == null)
			{
				return;
			}
			Boat tBoat = tUn.getSimpleComponent<Boat>();
			pTool.setSeparator();
			pTool.setText("units:", tBoat.countPassengers(), 0f, false, 0L, false, false, "");
			pTool.setText("passengerWaitCounter:", tBoat.passengerWaitCounter, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "taxi";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("requests:", TaxiManager.list.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			TaxiManager.list.Sort((TaxiRequest a, TaxiRequest b) => b.countActors().CompareTo(a.countActors()));
			TaxiManager.list.ForEach(delegate(TaxiRequest tRequest)
			{
				int tInsideTaxi = 0;
				if (tRequest.hasAssignedBoat())
				{
					tInsideTaxi = tRequest.getBoat().countPassengers();
				}
				pTool.setText("state", string.Concat(new string[]
				{
					tRequest.state.ToString(),
					" ",
					tInsideTaxi.ToString(),
					"/",
					tRequest.countActors().ToString(),
					" | ",
					tRequest.hasAssignedBoat().ToString()
				}), 0f, false, 0L, false, false, "");
			});
		};
		this.add(debugToolAsset2);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00060DFC File Offset: 0x0005EFFC
	private void initGameplay()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "World Laws";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			foreach (WorldLawAsset tAsset in AssetManager.world_laws_library.list)
			{
				pTool.setText(tAsset.id, tAsset.isEnabled().ToString() + " : " + tAsset.isEnabledRaw().ToString(), 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "Building Manager";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("buildings:", World.world.buildings.Count, 0f, false, 0L, false, false, "");
			int tVisible = 0;
			int tweens = 0;
			int tween_active = 0;
			foreach (Building building in World.world.buildings)
			{
				if (building.is_visible)
				{
					tVisible++;
				}
				if (building.scale_helper.active)
				{
					tween_active++;
				}
			}
			pTool.setText("visible:", tVisible.ToString() + "/" + World.world.buildings.Count.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("tweens:", tweens.ToString() + "/" + World.world.buildings.Count.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("tween_active:", tween_active.ToString() + "/" + World.world.buildings.Count.ToString(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "Cultures";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("cultures:", World.world.cultures.Count, 0f, false, 0L, false, false, "");
			foreach (Culture culture in World.world.cultures)
			{
				culture.debug(pTool);
			}
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Tile Types";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("tumor_low", TopTileLibrary.tumor_low.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("tumor_high", TopTileLibrary.tumor_high.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("biomass_low", TopTileLibrary.biomass_low.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("biomass_high", TopTileLibrary.biomass_high.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("pumpkin_low", TopTileLibrary.pumpkin_low.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("pumpkin_high", TopTileLibrary.pumpkin_high.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("cybertile_low", TopTileLibrary.cybertile_low.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("cybertile_high", TopTileLibrary.cybertile_high.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("deep_ocean", TileLibrary.deep_ocean.hashset.Count, 0f, false, 0L, false, false, "");
			pTool.setText("pit_deep_ocean", TileLibrary.pit_deep_ocean.hashset.Count, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "Jobs Buildings";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			World.world.buildings.debugJobManager(pTool);
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "Jobs Actors";
		debugToolAsset6.action_1 = delegate(DebugTool pTool)
		{
			World.world.units.debugJobManager(pTool);
		};
		this.add(debugToolAsset6);
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "Building Info";
		debugToolAsset7.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			Building tB = tTile.building;
			if (tB == null)
			{
				return;
			}
			if (tB.asset.docks)
			{
				pTool.setText("boats_fishing:", tB.component_docks.countBoatTypes("boat_type_fishing"), 0f, false, 0L, false, false, "");
				pTool.setText("boats_transport:", tB.component_docks.countBoatTypes("boat_type_transport"), 0f, false, 0L, false, false, "");
				pTool.setText("boats_trading:", tB.component_docks.countBoatTypes("boat_type_trading"), 0f, false, 0L, false, false, "");
			}
			pTool.setText("id:", tB.data.id, 0f, false, 0L, false, false, "");
			pTool.setText("hash:", tB.GetHashCode(), 0f, false, 0L, false, false, "");
			pTool.setText("animData_index:", tB.animData_index, 0f, false, 0L, false, false, "");
			pTool.setText("residents:", tB.countResidents().ToString() + "/" + tB.asset.housing_slots.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("kingdom:", tB.kingdom.id, 0f, false, 0L, false, false, "");
			pTool.setText("kingdom civ:", tB.isKingdomCiv(), 0f, false, 0L, false, false, "");
			pTool.setText("animationState:", tB.animation_state, 0f, false, 0L, false, false, "");
			pTool.setText("ownership:", tB.state_ownership, 0f, false, 0L, false, false, "");
			pTool.setText("state:", tB.data.state, 0f, false, 0L, false, false, "");
			pTool.setText("template:", tB.data.asset_id, 0f, false, 0L, false, false, "");
			pTool.setText("health:", tB.getHealth(), 0f, false, 0L, false, false, "");
			pTool.setText("health cur:", tB.getMaxHealth(), 0f, false, 0L, false, false, "");
			if (tB.hasKingdom())
			{
				pTool.setText("kingdom:", tB.kingdom.name, 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
			pTool.setText("tiles:", tB.tiles.Count, 0f, false, 0L, false, false, "");
			pTool.setText("zones:", tB.zones.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("alive:", tB.isAlive(), 0f, false, 0L, false, false, "");
			pTool.setText("usable:", tB.isUsable(), 0f, false, 0L, false, false, "");
			pTool.setText("under construction:", tB.isUnderConstruction(), 0f, false, 0L, false, false, "");
			pTool.setText("progress:", tB.getConstructionProgress(), 0f, false, 0L, false, false, "");
			if (tB.city != null)
			{
				pTool.setText("city:", tB.city.name, 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
			pTool.setText("tween_active:", tB.scale_helper.active, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("state:", tB.animation_state, 0f, false, 0L, false, false, "");
			pTool.setText("has_resources:", tB.hasResourcesToCollect(), 0f, false, 0L, false, false, "");
			pTool.setText("is_visible:", tB.is_visible, 0f, false, 0L, false, false, "");
			pTool.setText("scale_start:", tB.scale_helper.scale_start, 0f, false, 0L, false, false, "");
			pTool.setText("currentScale.y:", tB.current_scale.y, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("flip.x:", tB.flip_x, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset7);
		DebugToolAsset debugToolAsset8 = new DebugToolAsset();
		debugToolAsset8.id = "Debug Buildings Render";
		debugToolAsset8.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			Building tB = tTile.building;
			if (tB == null)
			{
				return;
			}
			pTool.setText("flip.x:", tB.flip_x, 0f, false, 0L, false, false, "");
			if (!tB.is_visible)
			{
				return;
			}
			if (World.world.quality_changer.isLowRes())
			{
				return;
			}
			int tLen = World.world.buildings.countVisibleBuildings();
			int tIndexInVisibleArray = 0;
			int tAlive = 0;
			int tDead = 0;
			Building[] tVisibleBuildings = World.world.buildings.getVisibleBuildings();
			HashSet<Building> tUniqueBuildings = UnsafeCollectionPool<HashSet<Building>, Building>.Get();
			for (int i = 0; i < tLen; i++)
			{
				Building tVisibleBuilding = tVisibleBuildings[i];
				if (tVisibleBuilding != null)
				{
					if (tVisibleBuilding == tB)
					{
						tIndexInVisibleArray = i;
						pTool.setText("visible id:", i.ToString() + "/" + tVisibleBuildings.Length.ToString(), 0f, false, 0L, false, false, "");
					}
					tUniqueBuildings.Add(tVisibleBuilding);
					if (tVisibleBuilding.isAlive())
					{
						tAlive++;
					}
					else
					{
						tDead++;
					}
				}
			}
			pTool.setText("alive:", tAlive.ToString() + "/" + tVisibleBuildings.Length.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("dead:", tDead.ToString() + "/" + tVisibleBuildings.Length.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("_visible_buildings_count:", tLen, 0f, false, 0L, false, false, "");
			pTool.setText("tUniqueBuildings:", tUniqueBuildings.Count, 0f, false, 0L, false, false, "");
			UnsafeCollectionPool<HashSet<Building>, Building>.Release(tUniqueBuildings);
			BuildingRenderData tRenderData = World.world.buildings.render_data;
			pTool.setText("render_data_flip:", tRenderData.flip_x_states[tIndexInVisibleArray].ToString(), 0f, false, 0L, false, false, "");
			QuantumSpriteAsset tQAsset = AssetManager.quantum_sprites.get("draw_buildings");
			QuantumSpriteCacheData tCacheData = tQAsset.group_system.getCacheData(tLen);
			if (tCacheData != null)
			{
				if (tCacheData.flip_x_states.Length <= tIndexInVisibleArray)
				{
					return;
				}
				pTool.setText("render_data_flip:", tCacheData.flip_x_states[tIndexInVisibleArray].ToString(), 0f, false, 0L, false, false, "");
			}
			QuantumSprite[] tQSprites = tQAsset.group_system.getFastActiveList(tLen);
			if (tQSprites.Length <= tIndexInVisibleArray)
			{
				return;
			}
			pTool.setText("q flip x:", tQSprites[tIndexInVisibleArray].sprite_renderer.flipX.ToString(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset8);
		DebugToolAsset debugToolAsset9 = new DebugToolAsset();
		debugToolAsset9.id = "Actor Statistics";
		debugToolAsset9.action_1 = delegate(DebugTool pTool)
		{
			Actor tActor = World.world.getActorNearCursor();
			if (tActor == null)
			{
				return;
			}
			pTool.setText("getSecondsLife:", StatTool.getStringSecondsLife(tActor), 0f, false, 0L, false, false, "");
			pTool.setText("getAmountBreeding:", StatTool.getStringAmountBreeding(tActor), 0f, false, 0L, false, false, "");
			pTool.setText("getAmountFood:", StatTool.getAmountFood(tActor), 0f, false, 0L, false, false, "");
			pTool.setText("getDPS:", StatTool.getDPS(tActor), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset9);
		DebugToolAsset debugToolAsset10 = new DebugToolAsset();
		debugToolAsset10.id = "Biome Adaptation";
		debugToolAsset10.action_1 = delegate(DebugTool pTool)
		{
			Actor tActor = SelectedUnit.unit;
			if (tActor == null)
			{
				return;
			}
			if (!tActor.hasSubspecies())
			{
				return;
			}
			WorldTile tCursorTile = World.world.getMouseTilePos();
			if (tCursorTile == null)
			{
				return;
			}
			tCursorTile.zone.checkCanSettleInThisBiomes(tActor.subspecies);
			pTool.setText("adapted:", TileZone.debug_adapted, 0f, false, 0L, false, false, "");
			pTool.setText("not_adapted:", TileZone.debug_not_adapted, 0f, false, 0L, false, false, "");
			pTool.setText("soil:", TileZone.debug_soil, 0f, false, 0L, false, false, "");
			pTool.setText("can_settle:", TileZone.debug_can_settle, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset10);
		DebugToolAsset debugToolAsset11 = new DebugToolAsset();
		debugToolAsset11.id = "Kingdoms Wild";
		debugToolAsset11.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("#wild_kingdoms:", World.world.kingdoms_wild.Count, 0f, false, 0L, false, false, "");
			foreach (Kingdom iKingdom in World.world.kingdoms_wild)
			{
				if (iKingdom.hasUnits() || iKingdom.hasBuildings())
				{
					pTool.setText(iKingdom.name, iKingdom.units.Count.ToString() + " " + iKingdom.buildings.Count.ToString(), 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset11);
		DebugToolAsset debugToolAsset12 = new DebugToolAsset();
		debugToolAsset12.id = "Buildings Check";
		debugToolAsset12.action_1 = delegate(DebugTool pTool)
		{
			int tLowerThanMax = 0;
			int tHigherThanMax = 0;
			foreach (Building tBuilding in World.world.buildings)
			{
				if (tBuilding.getHealth() <= tBuilding.getMaxHealth())
				{
					tLowerThanMax++;
				}
				else
				{
					tHigherThanMax++;
				}
			}
			pTool.setText("within max health:", tLowerThanMax, 0f, false, 0L, false, false, "");
			pTool.setText("higher than max health:", tHigherThanMax, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			foreach (Kingdom iKingdom in World.world.kingdoms)
			{
				bool tResults = iKingdom.buildings.Count == iKingdom.countBuildings();
				pTool.setText(iKingdom.name, string.Concat(new string[]
				{
					tResults.ToString(),
					" | ",
					iKingdom.buildings.Count.ToString(),
					" ",
					iKingdom.countBuildings().ToString()
				}), 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset12);
		DebugToolAsset debugToolAsset13 = new DebugToolAsset();
		debugToolAsset13.id = "Kingdoms Civ";
		debugToolAsset13.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("#kingdoms:", World.world.kingdoms.Count, 0f, false, 0L, false, false, "");
			pTool.setText("- units total:", World.world.units.Count, 0f, false, 0L, false, false, "");
			int tUnNoKingdom = 0;
			using (IEnumerator<Actor> enumerator = World.world.units.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.kingdom == null)
					{
						tUnNoKingdom++;
					}
				}
			}
			pTool.setText("- units no kingdom:", tUnNoKingdom, 0f, false, 0L, false, false, "");
			List<Kingdom> list = new List<Kingdom>(World.world.kingdoms);
			list.Sort(new Comparison<Kingdom>(pTool.kingdomSorter));
			foreach (Kingdom iKingdom in list)
			{
				if (pTool.textCount > 0)
				{
					pTool.setSeparator();
				}
				pTool.setText("#id", iKingdom.id, 0f, false, 0L, false, false, "");
				pTool.setText("#name", iKingdom.name, 0f, false, 0L, false, false, "");
				pTool.setText("age", iKingdom.getAge(), 0f, false, 0L, false, false, "");
				pTool.setText("units", iKingdom.units.Count, 0f, false, 0L, false, false, "");
				pTool.setText("army", iKingdom.countTotalWarriors().ToString() + "/" + iKingdom.countWarriorsMax().ToString(), 0f, false, 0L, false, false, "");
				pTool.setText("buildings", iKingdom.buildings.Count, 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset13);
		DebugToolAsset debugToolAsset14 = new DebugToolAsset();
		debugToolAsset14.id = "Behaviours";
		debugToolAsset14.action_1 = delegate(DebugTool pTool)
		{
			World.world.drop_manager.debug(pTool);
			pTool.setText("dirty last:", World.world.dirty_tiles_last, 0f, false, 0L, false, false, "");
			pTool.setText("dirty tiles:", World.world.tiles_dirty.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("tiles:", World.world.tiles_list.Length, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("water:", WorldBehaviourOcean.tiles.Count, 0f, false, 0L, false, false, "");
			pTool.setText("burned_tiles:", WorldBehaviourActionBurnedTiles.countBurnedTiles(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			string pT = "grey goo:";
			HashSet<WorldTile> hashset = World.world.grey_goo_layer.hashset;
			pTool.setText(pT, (hashset != null) ? new int?(hashset.Count) : null, 0f, false, 0L, false, false, "");
			string pT2 = "conway";
			HashSetWorldTile hashsetTiles = World.world.conway_layer.hashsetTiles;
			pTool.setText(pT2, (hashsetTiles != null) ? new int?(hashsetTiles.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("flash effect:", World.world.flash_effects.pixels_to_update.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			string pT3 = "explosion layer:";
			HashSetWorldTile hashsetTiles2 = World.world.explosion_layer.hashsetTiles;
			pTool.setText(pT3, (hashsetTiles2 != null) ? new int?(hashsetTiles2.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("bombDict:", World.world.explosion_layer.hashset_bombs.Count, 0f, false, 0L, false, false, "");
			pTool.setText("nextWave:", World.world.explosion_layer.nextWave.Count, 0f, false, 0L, false, false, "");
			pTool.setText("delayedBombs:", World.world.explosion_layer.nextWave.Count, 0f, false, 0L, false, false, "");
			pTool.setText("timedBombs:", World.world.explosion_layer.timedBombs.Count, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset14);
		DebugToolAsset debugToolAsset15 = new DebugToolAsset();
		debugToolAsset15.id = "Unit Info";
		debugToolAsset15.action_1 = delegate(DebugTool pTool)
		{
			Actor tUn = World.world.getActorNearCursor();
			if (tUn == null)
			{
				return;
			}
			if (tUn.hasAnyStatusEffect())
			{
				pTool.setText("status effects", tUn.countStatusEffects(), 0f, false, 0L, false, false, "");
			}
			pTool.setText("profession:", tUn.getProfession(), 0f, false, 0L, false, false, "");
			if (tUn.ai.job != null)
			{
				pTool.setText("current_job:", tUn.ai.job.id, 0f, false, 0L, false, false, "");
			}
			else
			{
				pTool.setText("job:", "-", 0f, false, 0L, false, false, "");
			}
			pTool.setText("id:", tUn.data.id, 0f, false, 0L, false, false, "");
			if (tUn.hasTask())
			{
				pTool.setText("task:", tUn.ai.task.id, 0f, false, 0L, false, false, "");
			}
			else
			{
				pTool.setText("task:", "-", 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
			pTool.setText("name:", tUn.getName(), 0f, false, 0L, false, false, "");
			pTool.setText("is_moving:", tUn.is_moving, 0f, false, 0L, false, false, "");
			pTool.setText("next_step:", tUn.next_step_position.x, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("stayingInBuilding:", tUn.inside_building != null, 0f, false, 0L, false, false, "");
			pTool.setText("bag.hasResources:", tUn.isCarryingResources(), 0f, false, 0L, false, false, "");
			pTool.setText("ignore:", tUn.countTargetsToIgnore(), 0f, false, 0L, false, false, "");
			string pT = "path global:";
			List<MapRegion> current_path_global = tUn.current_path_global;
			pTool.setText(pT, (current_path_global != null) ? new int?(current_path_global.Count) : null, 0f, false, 0L, false, false, "");
			pTool.setText("path local:", tUn.current_path.Count, 0f, false, 0L, false, false, "");
			pTool.setText("path local index:", tUn.current_path_index, 0f, false, 0L, false, false, "");
			pTool.setText("path split status:", tUn.split_path.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("health:", tUn.getHealth().ToString() + "/" + tUn.getMaxHealth().ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("damage:", tUn.asset.base_stats["damage"].ToString() + "/" + tUn.stats["damage"].ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("city:", (tUn.city == null) ? "-" : tUn.city.name, 0f, false, 0L, false, false, "");
			pTool.setText("kingdom:", (tUn.kingdom == null) ? "-" : tUn.kingdom.name, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("nutrition:", tUn.getNutrition().ToString() + "/" + tUn.getMaxNutrition().ToString(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			if (tUn.animation_container != null)
			{
				pTool.setText("actorAnimationData:", tUn.animation_container.id, 0f, false, 0L, false, false, "");
			}
			pTool.setText("stats name:", tUn.asset.id, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("timer_action:", tUn.timer_action, 0f, false, 0L, false, false, "");
			pTool.setText("_timeout_targets:", tUn._timeout_targets, 0f, false, 0L, false, false, "");
			pTool.setText("unitAttackTarget:", tUn.has_attack_target ? (tUn.isEnemyTargetAlive().ToString() ?? "") : "-", 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("attackTimer:", tUn.attack_timer, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setSeparator();
			pTool.setText("moveJumpOffset:", tUn.move_jump_offset.y, 0f, false, 0L, false, false, "");
			pTool.setText("alive:", tUn.isAlive(), 0f, false, 0L, false, false, "");
			pTool.setText("zPosition:", tUn.position_height, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("phenotype_index:", tUn.data.phenotype_index, 0f, false, 0L, false, false, "");
			pTool.setText("shade_id:", tUn.data.phenotype_shade, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset15);
		DebugToolAsset debugToolAsset16 = new DebugToolAsset();
		debugToolAsset16.id = "Actor Stats";
		debugToolAsset16.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			int tBestDist = int.MaxValue;
			Actor tUn = null;
			foreach (Actor tActor in World.world.units)
			{
				int tDist = Toolbox.SquaredDistTile(tActor.current_tile, tTile);
				if (tDist < tBestDist)
				{
					tUn = tActor;
					tBestDist = tDist;
				}
			}
			if (tUn == null)
			{
				return;
			}
			pTool.setText("name:", tUn.getName(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			List<BaseStatsContainer> tStats = tUn.stats.getList();
			foreach (BaseStatsContainer tStr in tStats)
			{
				pTool.setText(tStr.id, tUn.stats[tStr.id], 0f, false, 0L, false, false, "");
			}
			if (tStats.Count > 0)
			{
				pTool.setSeparator();
			}
			Dictionary<string, string> tActorData = tUn.data.debug();
			foreach (string tStr2 in tActorData.Keys)
			{
				pTool.setText(tStr2, tActorData[tStr2], 0f, false, 0L, false, false, "");
			}
			if (tActorData.Count > 0)
			{
				pTool.setSeparator();
			}
			string pT = "currentTile:";
			object pT2;
			if (tUn.current_tile != null)
			{
				WorldTile current_tile = tUn.current_tile;
				pT2 = (((current_tile != null) ? current_tile.ToString() : null) ?? "");
			}
			else
			{
				pT2 = "-";
			}
			pTool.setText(pT, pT2, 0f, false, 0L, false, false, "");
			if (tUn.current_tile != null)
			{
				pTool.setText("x / y", tUn.current_tile.x.ToString() + " " + tUn.current_tile.y.ToString(), 0f, false, 0L, false, false, "");
				pTool.setText("id", tUn.current_tile.data.tile_id, 0f, false, 0L, false, false, "");
				pTool.setText("height", tUn.current_tile.data.height, 0f, false, 0L, false, false, "");
				pTool.setText("type", tUn.current_tile.Type.id, 0f, false, 0L, false, false, "");
				pTool.setText("layer", tUn.current_tile.Type.layer_type, 0f, false, 0L, false, false, "");
				pTool.setText("main type", (tUn.current_tile.main_type != null) ? tUn.current_tile.main_type.id : "-", 0f, false, 0L, false, false, "");
				pTool.setText("top type", (tUn.current_tile.top_type != null) ? tUn.current_tile.top_type.id : "-", 0f, false, 0L, false, false, "");
				pTool.setText("targetedBy", tUn.current_tile.isTargeted(), 0f, false, 0L, false, false, "");
				pTool.setText("units", tUn.current_tile.countUnits(), 0f, false, 0L, false, false, "");
				pTool.setSeparator();
			}
		};
		this.add(debugToolAsset16);
		DebugToolAsset debugToolAsset17 = new DebugToolAsset();
		debugToolAsset17.id = "Unit Temperature";
		debugToolAsset17.action_1 = delegate(DebugTool pTool)
		{
			WorldBehaviourUnitTemperatures.debug(pTool);
		};
		this.add(debugToolAsset17);
		DebugToolAsset debugToolAsset18 = new DebugToolAsset();
		debugToolAsset18.id = "Zoom";
		debugToolAsset18.action_1 = delegate(DebugTool pTool)
		{
			World.world.quality_changer.debug(pTool);
		};
		this.add(debugToolAsset18);
		DebugToolAsset debugToolAsset19 = new DebugToolAsset();
		debugToolAsset19.id = "Mouse Cursor";
		debugToolAsset19.action_1 = delegate(DebugTool pTool)
		{
			MouseCursor.debug(pTool);
		};
		this.add(debugToolAsset19);
		DebugToolAsset debugToolAsset20 = new DebugToolAsset();
		debugToolAsset20.id = "Selected Power";
		debugToolAsset20.action_1 = delegate(DebugTool pTool)
		{
			if (!World.world.isAnyPowerSelected())
			{
				pTool.setText("no power selected", "", 0f, false, 0L, false, false, "");
				return;
			}
			pTool.setText("selectedPower:", World.world.getSelectedPowerID(), 0f, false, 0L, false, false, "");
			GodPower tSelectedPower = World.world.getSelectedPowerAsset();
			pTool.setText("type:", tSelectedPower.type, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("show_tool_sizes:", tSelectedPower.show_tool_sizes, 0f, false, 0L, false, false, "");
			pTool.setText("unselect_when_window:", tSelectedPower.unselect_when_window, 0f, false, 0L, false, false, "");
			pTool.setText("ignore_cursor_icon:", tSelectedPower.ignore_cursor_icon, 0f, false, 0L, false, false, "");
			pTool.setText("hold_action:", tSelectedPower.hold_action, 0f, false, 0L, false, false, "");
			pTool.setText("click_interval:", tSelectedPower.click_interval, 0f, false, 0L, false, false, "");
			pTool.setText("particle_interval:", tSelectedPower.particle_interval, 0f, false, 0L, false, false, "");
			pTool.setText("falling_chance:", tSelectedPower.falling_chance, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("click_brush_action:", tSelectedPower.click_brush_action, 0f, false, 0L, false, false, "");
			pTool.setText("click_action:", tSelectedPower.click_action, 0f, false, 0L, false, false, "");
			pTool.setText("click_special_action:", tSelectedPower.click_special_action, 0f, false, 0L, false, false, "");
			pTool.setText("click_power_brush_action:", tSelectedPower.click_power_brush_action, 0f, false, 0L, false, false, "");
			pTool.setText("click_power_action:", tSelectedPower.click_power_action, 0f, false, 0L, false, false, "");
			pTool.setText("select_button_action:", tSelectedPower.select_button_action, 0f, false, 0L, false, false, "");
			pTool.setText("toggle_action:", tSelectedPower.toggle_action, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("actor_asset_id:", tSelectedPower.actor_asset_id, 0f, false, 0L, false, false, "");
			pTool.setText("actor_asset_ids:", tSelectedPower.actor_asset_ids, 0f, false, 0L, false, false, "");
			pTool.setText("toggle_name:", tSelectedPower.toggle_name, 0f, false, 0L, false, false, "");
			pTool.setText("map_modes_switch:", tSelectedPower.map_modes_switch, 0f, false, 0L, false, false, "");
			pTool.setText("show_spawn_effect:", tSelectedPower.show_spawn_effect, 0f, false, 0L, false, false, "");
			pTool.setText("activate_on_hotkey_select:", tSelectedPower.activate_on_hotkey_select, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset20);
		DebugToolAsset debugToolAsset21 = new DebugToolAsset();
		debugToolAsset21.id = "Hotkeys";
		debugToolAsset21.action_1 = delegate(DebugTool pTool)
		{
			AssetManager.hotkey_library.debug(pTool);
		};
		this.add(debugToolAsset21);
		DebugToolAsset debugToolAsset22 = new DebugToolAsset();
		debugToolAsset22.id = "Armies";
		debugToolAsset22.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("groups:", World.world.armies.Count, 0f, false, 0L, false, false, "");
			foreach (Army tGroup in World.world.armies)
			{
				pTool.setText(": " + tGroup.id.ToString(), tGroup.getDebug(), 0f, false, 0L, false, false, "");
			}
			pTool.setSeparator();
		};
		this.add(debugToolAsset22);
		DebugToolAsset debugToolAsset23 = new DebugToolAsset();
		debugToolAsset23.id = "Magnet Debug";
		debugToolAsset23.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("hasUnits():", World.world.magnet.hasUnits(), 0f, false, 0L, false, false, "");
			pTool.setText("countUnits():", World.world.magnet.countUnits(), 0f, false, 0L, false, false, "");
			pTool.setText("magnetUnits.Count:", World.world.magnet.magnet_units.Count, 0f, false, 0L, false, false, "");
			int tUnitsWithMagnetStatus = 0;
			foreach (Actor tActor in World.world.units)
			{
				if (tActor.isAlive() && tActor.is_in_magnet)
				{
					tUnitsWithMagnetStatus++;
				}
			}
			pTool.setText("tUnitsWithMagnetStatus:", tUnitsWithMagnetStatus, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset23);
		this.add(new DebugToolAsset
		{
			id = "Mindmap Debug",
			action_1 = new DebugToolAssetAction(NeuronsOverview.debugTool)
		});
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00061398 File Offset: 0x0005F598
	private void initMain()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "Game Info";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("selected_unit?:", SelectedUnit.isSet(), 0f, false, 0L, false, false, "");
			pTool.setText("selected_unit_name:", SelectedUnit.isSet() ? SelectedUnit.unit.getName() : "-", 0f, false, 0L, false, false, "");
			pTool.setText("elapsed:", World.world.elapsed, 0f, false, 0L, false, false, "");
			pTool.setText("delta time:", World.world.delta_time, 0f, false, 0L, false, false, "");
			pTool.setText("actor0:", Bench.getBenchResult("actor0", "main", true), 0f, false, 0L, false, false, "");
			pTool.setText("actor1:", Bench.getBenchResult("actor1", "main", true), 0f, false, 0L, false, false, "");
			pTool.setText("actor2:", Bench.getBenchResult("actor2", "main", true), 0f, false, 0L, false, false, "");
			pTool.setText("actor_total:", Bench.getBenchResult("actor_total", "main", true), 0f, false, 0L, false, false, "");
			pTool.setText("test_follow:", Bench.getBenchResult("test_follow", "main", true), 0f, false, 0L, false, false, "");
			pTool.setText("rightClickTimer:", World.world.player_control.inspect_timer_click, 0f, false, 0L, false, false, "");
			pTool.setText("cache g paths:", World.world.region_path_finder.debug(), 0f, false, 0L, false, false, "");
			pTool.setText("units:", World.world.units.debugContainer(), 0f, false, 0L, false, false, "");
			pTool.setText("buildings:", World.world.buildings.debugContainer(), 0f, false, 0L, false, false, "");
			pTool.setText("cities:", World.world.cities.Count, 0f, false, 0L, false, false, "");
			pTool.setText("civ kingdoms:", World.world.kingdoms.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("(d)gameTime:", World.world.game_stats.data.gameTime, 0f, false, 0L, false, false, "");
			pTool.setText("(f)gameTime:", (float)World.world.game_stats.data.gameTime, 0f, false, 0L, false, false, "");
			World.world.map_stats.debug(pTool);
			pTool.setText("gameLaunches:", World.world.game_stats.data.gameLaunches, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("size tiles:", World.world.tiles_map.Length, 0f, false, 0L, false, false, "");
			pTool.setText("chunks:", World.world.map_chunk_manager.chunks.Length, 0f, false, 0L, false, false, "");
			pTool.setText("- regions:", World.world.map_chunk_manager.countRegions(), 0f, false, 0L, false, false, "");
			pTool.setText("- hashes:", RegionLinkHashes.getCount(), 0f, false, 0L, false, false, "");
			pTool.setText("- islands:", World.world.islands_calculator.islands.Count, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			int is_visible = World.world.units.visible_units.count;
			int is_visible_buildings = 0;
			using (IEnumerator<Building> enumerator = World.world.buildings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.is_visible)
					{
						is_visible_buildings++;
					}
				}
			}
			pTool.setSeparator();
			pTool.setText("visible buildings:", is_visible_buildings.ToString() + "/" + World.world.buildings.Count.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("visible buildings:", World.world.buildings.countVisibleBuildings().ToString() + "/" + World.world.buildings.getVisibleBuildings().Length.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("visible actors:", is_visible.ToString() + "/" + World.world.units.Count.ToString(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "Basic Info";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("Game Version:", Application.version, 0f, false, 0L, false, false, "");
			pTool.setText("Version Code:", Config.versionCodeText, 0f, false, 0L, false, false, "");
			pTool.setText("Git:", Config.gitCodeText, 0f, false, 0L, false, false, "");
			pTool.setText("Modded:", Config.MODDED, 0f, false, 0L, false, false, "");
			pTool.setText("operatingSystemFamily:", SystemInfo.operatingSystemFamily, 0f, false, 0L, false, false, "");
			pTool.setText("deviceModel:", SystemInfo.deviceModel, 0f, false, 0L, false, false, "");
			pTool.setText("deviceName:", SystemInfo.deviceName, 0f, false, 0L, false, false, "");
			pTool.setText("deviceType:", SystemInfo.deviceType, 0f, false, 0L, false, false, "");
			pTool.setText("systemMemorySize:", SystemInfo.systemMemorySize, 0f, false, 0L, false, false, "");
			pTool.setText("graphicsDeviceID:", SystemInfo.graphicsDeviceID, 0f, false, 0L, false, false, "");
			pTool.setText("graphicsActiveTier:", Graphics.activeTier.ToString(), 0f, false, 0L, false, false, "");
			pTool.setText("GC.GetTotalMemory:", (GC.GetTotalMemory(false) / 1000000L).ToString() + " mb", 0f, false, 0L, false, false, "");
			pTool.setText("graphicsMemorySize:", SystemInfo.graphicsMemorySize, 0f, false, 0L, false, false, "");
			pTool.setText("maxTextureSize:", SystemInfo.maxTextureSize, 0f, false, 0L, false, false, "");
			pTool.setText("operatingSystem:", SystemInfo.operatingSystem, 0f, false, 0L, false, false, "");
			pTool.setText("processorType:", SystemInfo.processorType, 0f, false, 0L, false, false, "");
			pTool.setText("installMode:", Application.installMode, 0f, false, 0L, false, false, "");
			pTool.setText("sandboxType:", Application.sandboxType, 0f, false, 0L, false, false, "");
			pTool.setText("FPS:", FPS.getFPS(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "sprite_atlas_manager";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			int tBestDist = int.MaxValue;
			Actor tUn = null;
			foreach (Actor tActor in World.world.units)
			{
				int tDist = Toolbox.SquaredDistTile(tActor.current_tile, tTile);
				if (tDist < tBestDist)
				{
					tUn = tActor;
					tBestDist = tDist;
				}
			}
			if (tUn == null)
			{
				return;
			}
			AssetManager.dynamic_sprites_library.debug(pTool, tUn);
			pTool.setSeparator();
			pTool.setText("sex:", tUn.data.sex, 0f, false, 0L, false, false, "");
			pTool.setText("head:", tUn.has_rendered_sprite_head ? tUn.cached_sprite_head.name : "-", 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Subspecies";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			using (ListPool<Subspecies> tList = new ListPool<Subspecies>(World.world.subspecies.list))
			{
				tList.Sort((Subspecies a, Subspecies b) => b.units.Count.CompareTo(a.units.Count));
				foreach (Subspecies ptr in tList)
				{
					Subspecies tSubspecies = ptr;
					pTool.setText(string.Concat(new string[]
					{
						"[",
						tSubspecies.getActorAsset().id,
						"] ",
						tSubspecies.name,
						": "
					}), tSubspecies.units.Count, 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "Items";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("total: ", World.world.items.Count, 0f, false, 0L, false, false, "");
			int tDead = 0;
			int tAlive = 0;
			int tHasActor = 0;
			int tHasCity = 0;
			int tHasDeadActor = 0;
			int tHasDeadCity = 0;
			int tHasUnitHasIt = 0;
			int tHasCityHasIt = 0;
			int tUnitHasEquipment = 0;
			int tUnitHasNoEquipment = 0;
			int tUnitHasItemInEquipment = 0;
			int tUnitMissingItemInEquipment = 0;
			foreach (Item tItem in World.world.items)
			{
				if (tItem.unit_has_it)
				{
					tHasUnitHasIt++;
				}
				if (tItem.city_has_it)
				{
					tHasCityHasIt++;
				}
				if (tItem.isRekt())
				{
					tDead++;
				}
				else
				{
					tAlive++;
					if (tItem.hasActor())
					{
						tHasActor++;
						if (tItem.getActor().isRekt())
						{
							tHasDeadActor++;
						}
						else if (tItem.getActor().hasEquipment())
						{
							tUnitHasEquipment++;
							bool tFound = false;
							using (IEnumerator<Item> enumerator2 = tItem.getActor().equipment.getItems().GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current == tItem)
									{
										tFound = true;
										break;
									}
								}
							}
							if (tFound)
							{
								tUnitHasItemInEquipment++;
							}
							else
							{
								tUnitMissingItemInEquipment++;
							}
						}
						else
						{
							tUnitHasNoEquipment++;
						}
					}
					if (tItem.hasCity())
					{
						tHasCity++;
						if (tItem.getCity().isRekt())
						{
							tHasDeadCity++;
						}
					}
				}
			}
			pTool.setSeparator();
			pTool.setText("alive: ", tAlive, 0f, false, 0L, false, false, "");
			pTool.setText("dead: ", tDead, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("has actor: ", tHasActor, 0f, false, 0L, false, false, "");
			pTool.setText("has unit has it: ", tHasUnitHasIt, 0f, false, 0L, false, false, "");
			pTool.setText("has dead actor: ", tHasDeadActor, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("has city: ", tHasCity, 0f, false, 0L, false, false, "");
			pTool.setText("has city has it: ", tHasCityHasIt, 0f, false, 0L, false, false, "");
			pTool.setText("has dead city: ", tHasDeadCity, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("unit has equipment: ", tUnitHasEquipment, 0f, false, 0L, false, false, "");
			pTool.setText("unit w/o equipment: ", tUnitHasNoEquipment, 0f, false, 0L, false, false, "");
			pTool.setText("unit has item equipped: ", tUnitHasItemInEquipment, 0f, false, 0L, false, false, "");
			pTool.setText("unit missing equipped item: ", tUnitMissingItemInEquipment, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "Decisions Globals Use";
		debugToolAsset6.action_1 = delegate(DebugTool pTool)
		{
			foreach (KeyValuePair<string, int> tPair in UtilityBasedDecisionSystem.debug_counter)
			{
				pTool.setText(tPair.Key, tPair.Value, 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset6);
		this.add(new DebugToolAsset
		{
			id = "Actor Decisions",
			action_1 = delegate(DebugTool pTool)
			{
				Actor tUn = World.world.getActorNearCursor();
				if (tUn == null)
				{
					return;
				}
				if (this._decision_system_debug == null)
				{
					this._decision_system_debug = new UtilityBasedDecisionSystem();
				}
				this._decision_system_debug.debug(tUn, pTool);
			}
		});
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "Items Errors";
		debugToolAsset7.action_1 = delegate(DebugTool pTool)
		{
			Dictionary<Item, string> tDictHasIt = new Dictionary<Item, string>();
			foreach (Item tItem in World.world.items)
			{
				tDictHasIt.Add(tItem, "nobody");
			}
			Dictionary<Item, int> tDict = new Dictionary<Item, int>();
			foreach (Actor tActor in World.world.units)
			{
				if (tActor.hasEquipment())
				{
					foreach (ActorEquipmentSlot tSlot in tActor.equipment)
					{
						if (!tSlot.isEmpty())
						{
							Item tItem2 = tSlot.getItem();
							tDictHasIt[tItem2] = "unit";
							if (!tDict.ContainsKey(tItem2))
							{
								tDict.Add(tItem2, 0);
							}
							Dictionary<Item, int> dictionary = tDict;
							Item key = tItem2;
							dictionary[key]++;
						}
					}
				}
			}
			foreach (City city in World.world.cities)
			{
				foreach (List<long> list in city.data.equipment.getAllEquipmentLists())
				{
					foreach (long tID in list)
					{
						Item tItem3 = World.world.items.get(tID);
						tDictHasIt[tItem3] = "city";
						if (!tDict.ContainsKey(tItem3))
						{
							tDict.Add(tItem3, 0);
						}
						Dictionary<Item, int> dictionary = tDict;
						Item key = tItem3;
						dictionary[key]++;
					}
				}
			}
			foreach (KeyValuePair<Item, string> tPair in tDictHasIt)
			{
				if (tPair.Value == "nobody")
				{
					Item tItem4 = tPair.Key;
					pTool.setText(tItem4.id.ToString(), "nobody", 0f, false, 0L, false, false, "");
				}
			}
			foreach (KeyValuePair<Item, int> tPair2 in tDict)
			{
				if (tPair2.Value > 1)
				{
					pTool.setText(tPair2.Key.id.ToString(), tPair2.Value, 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset7);
		DebugToolAsset debugToolAsset8 = new DebugToolAsset();
		debugToolAsset8.id = "Items Duplicates";
		debugToolAsset8.action_1 = delegate(DebugTool pTool)
		{
			Dictionary<Item, int> tDictSame = new Dictionary<Item, int>();
			foreach (Actor tActor in World.world.units)
			{
				if (tActor.hasEquipment())
				{
					foreach (ActorEquipmentSlot tSlot in tActor.equipment)
					{
						if (tSlot.getItem() != null)
						{
							Item tItem = tSlot.getItem();
							if (!tDictSame.ContainsKey(tItem))
							{
								tDictSame.Add(tItem, 0);
							}
							Dictionary<Item, int> dictionary = tDictSame;
							Item key = tItem;
							dictionary[key]++;
						}
					}
				}
			}
			foreach (KeyValuePair<Item, int> tPair in tDictSame)
			{
				if (tPair.Value >= 2)
				{
					string str = "Item ";
					ItemData data = tPair.Key.data;
					pTool.setText(str + (((data != null) ? data.id.ToString() : null) ?? "(dead)") + " shared between units: ", tPair.Value, 0f, false, 0L, false, false, "");
				}
			}
			tDictSame.Clear();
			foreach (City city in World.world.cities)
			{
				foreach (List<long> list in city.data.equipment.getAllEquipmentLists())
				{
					foreach (long tId in list)
					{
						Item tItem2 = World.world.items.get(tId);
						if (tItem2 != null)
						{
							if (!tDictSame.ContainsKey(tItem2))
							{
								tDictSame.Add(tItem2, 0);
							}
							Dictionary<Item, int> dictionary = tDictSame;
							Item key = tItem2;
							dictionary[key]++;
						}
					}
				}
			}
			foreach (KeyValuePair<Item, int> tPair2 in tDictSame)
			{
				if (tPair2.Value >= 2)
				{
					string str2 = "Item ";
					ItemData data2 = tPair2.Key.data;
					pTool.setText(str2 + (((data2 != null) ? data2.id.ToString() : null) ?? "(dead)") + " shared between сшешуы: ", tPair2.Value, 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset8);
		DebugToolAsset debugToolAsset9 = new DebugToolAsset();
		debugToolAsset9.id = "Items Ownership";
		debugToolAsset9.action_1 = delegate(DebugTool pTool)
		{
			int tFavorited = 0;
			int tOnlyFavorited = 0;
			int tEternal = 0;
			int tOnlyEternal = 0;
			int tEternalFavorite = 0;
			int tTotalOwnerless = 0;
			int tError = 0;
			int tRekt = 0;
			new Dictionary<Item, string>();
			foreach (Item tItem in World.world.items)
			{
				if (!tItem.unit_has_it && !tItem.city_has_it)
				{
					tTotalOwnerless++;
					if (tItem.isRekt())
					{
						tRekt++;
					}
					else
					{
						if (tItem.isFavorite())
						{
							tFavorited++;
						}
						if (tItem.isEternal())
						{
							tEternal++;
						}
						if (tItem.isFavorite() && tItem.isEternal())
						{
							tEternalFavorite++;
						}
						else if (tItem.isFavorite())
						{
							tOnlyFavorited++;
						}
						else if (tItem.isEternal())
						{
							tOnlyEternal++;
						}
						else
						{
							tError++;
						}
					}
				}
			}
			pTool.setText("total: ", World.world.items.Count, 0f, false, 0L, false, false, "");
			pTool.setText("total ownerless: ", tTotalOwnerless, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless favorited: ", tFavorited, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless favorited only: ", tOnlyFavorited, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless eternal: ", tEternal, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless eternal only: ", tOnlyEternal, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless favorited eternal: ", tEternalFavorite, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless error: ", tError, 0f, false, 0L, false, false, "");
			pTool.setText("ownerless rekt: ", tRekt, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset9);
		DebugToolAsset debugToolAsset10 = new DebugToolAsset();
		debugToolAsset10.id = "Families";
		debugToolAsset10.action_1 = delegate(DebugTool pTool)
		{
			float tTotalFamilyUnits = 0f;
			float tUnitsWithoutFamilies = 0f;
			using (List<Actor>.Enumerator enumerator = World.world.units.units_only_alive.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.hasFamily())
					{
						tTotalFamilyUnits += 1f;
					}
					else
					{
						tUnitsWithoutFamilies += 1f;
					}
				}
			}
			int tLonelyMemberFamily = 0;
			using (IEnumerator<Family> enumerator2 = World.world.families.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.units.Count == 1)
					{
						tLonelyMemberFamily++;
					}
				}
			}
			pTool.setText("total families", World.world.families.Count, 0f, false, 0L, false, false, "");
			pTool.setText("lonely families", tLonelyMemberFamily, 0f, false, 0L, false, false, "");
			pTool.setText("total units", tTotalFamilyUnits + tUnitsWithoutFamilies, 0f, false, 0L, false, false, "");
			pTool.setText("fam/no fam", tTotalFamilyUnits.ToString() + "/" + tUnitsWithoutFamilies.ToString(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			List<Family> list = new List<Family>(World.world.families);
			list.Sort((Family a, Family b) => b.units.Count.CompareTo(a.units.Count));
			foreach (Family tFamily in list)
			{
				if (tFamily.units.Count >= 2)
				{
					pTool.setText(string.Concat(new string[]
					{
						"[",
						tFamily.data.species_id,
						"] ",
						tFamily.name,
						": "
					}), tFamily.units.Count, 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset10);
		DebugToolAsset debugToolAsset11 = new DebugToolAsset();
		debugToolAsset11.id = "Languages";
		debugToolAsset11.action_1 = delegate(DebugTool pTool)
		{
			foreach (Language tLanguage in World.world.languages)
			{
				pTool.setText("[] " + tLanguage.name + ": ", tLanguage.units.Count, 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset11);
		DebugToolAsset debugToolAsset12 = new DebugToolAsset();
		debugToolAsset12.id = "Religions";
		debugToolAsset12.action_1 = delegate(DebugTool pTool)
		{
			foreach (Religion tReligion in World.world.religions)
			{
				pTool.setText("[] " + tReligion.name + ": ", tReligion.units.Count, 0f, false, 0L, false, false, "");
			}
		};
		this.add(debugToolAsset12);
		DebugToolAsset debugToolAsset13 = new DebugToolAsset();
		debugToolAsset13.id = "Actor Asset Units";
		debugToolAsset13.action_1 = delegate(DebugTool pTool)
		{
			foreach (ActorAsset tAsset in AssetManager.actor_library.list)
			{
				if (tAsset.units.Count != 0)
				{
					pTool.setText(tAsset.id + ": ", tAsset.units.Count, 0f, false, 0L, false, false, "");
				}
			}
		};
		this.add(debugToolAsset13);
		DebugToolAsset debugToolAsset14 = new DebugToolAsset();
		debugToolAsset14.id = "Population";
		debugToolAsset14.action_1 = delegate(DebugTool pTool)
		{
			int tUnits = 0;
			foreach (City iCity in World.world.cities)
			{
				tUnits += iCity.getPopulationPeople();
			}
			pTool.setText("city units:", tUnits, 0f, false, 0L, false, false, "");
			pTool.setText("unit list:", World.world.units.debugContainer(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset14);
		DebugToolAsset debugToolAsset15 = new DebugToolAsset();
		debugToolAsset15.id = "System Managers";
		debugToolAsset15.action_1 = delegate(DebugTool pTool)
		{
			foreach (BaseSystemManager baseSystemManager in World.world.list_all_sim_managers)
			{
				baseSystemManager.showDebugTool(pTool);
			}
		};
		this.add(debugToolAsset15);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00061754 File Offset: 0x0005F954
	private void initAI()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "Actor AI";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			int tBestDist = int.MaxValue;
			Actor tUn = null;
			foreach (Actor tActor in World.world.units)
			{
				if (!tActor.isInsideSomething())
				{
					int tDist = Toolbox.SquaredDistTile(tActor.current_tile, tTile);
					if (tDist < tBestDist)
					{
						tUn = tActor;
						tBestDist = tDist;
					}
				}
			}
			if (tUn == null)
			{
				return;
			}
			pTool.setText("timer_action:", tUn.timer_action, 0f, false, 0L, false, false, "");
			pTool.setText("stat id:", tUn.asset.id, 0f, false, 0L, false, false, "");
			tUn.ai.debug(pTool);
			string pT = "beh_tile_target";
			WorldTile beh_tile_target = tUn.beh_tile_target;
			string str = ((beh_tile_target != null) ? new int?(beh_tile_target.pos[0]) : null).ToString();
			string str2 = ":";
			WorldTile beh_tile_target2 = tUn.beh_tile_target;
			pTool.setText(pT, str + str2 + ((beh_tile_target2 != null) ? new int?(beh_tile_target2.pos[1]) : null).ToString(), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "Boat AI";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			int tBestDist = int.MaxValue;
			Actor tUn = null;
			foreach (Actor tActor in World.world.units)
			{
				if (!tActor.isInsideSomething() && tActor.asset.is_boat)
				{
					int tDist = Toolbox.SquaredDistTile(tActor.current_tile, tTile);
					if (tDist < tBestDist)
					{
						tUn = tActor;
						tBestDist = tDist;
					}
				}
			}
			if (tUn == null)
			{
				return;
			}
			pTool.setText("action_timer:", tUn.timer_action, 0f, false, 0L, false, false, "");
			pTool.setText("stat id:", tUn.asset.id, 0f, false, 0L, false, false, "");
			TaxiRequest tTaxiRequest = tUn.getSimpleComponent<Boat>().taxi_request;
			if (tTaxiRequest != null)
			{
				pTool.setText("taxi state:", tTaxiRequest.state, 0f, false, 0L, false, false, "");
				pTool.setText("taxi actors:", tTaxiRequest.countActors(), 0f, false, 0L, false, false, "");
				WorldTile tTargetTile = tTaxiRequest.getTileTarget();
				pTool.setText("taxi target:", (tTargetTile != null) ? (tTargetTile.pos[0].ToString() + ":" + tTargetTile.pos[1].ToString()) : "-", 0f, false, 0L, false, false, "");
				WorldTile tRequestTile = tTaxiRequest.getTileStart();
				pTool.setText("taxi start:", (tRequestTile != null) ? (tRequestTile.pos[0].ToString() + ":" + tRequestTile.pos[1].ToString()) : "-", 0f, false, 0L, false, false, "");
			}
			tUn.ai.debug(pTool);
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "City AI";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			pTool.setText("warrior_timer:", tCity.getTimerForNewWarrior(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			if (tCity.ai != null)
			{
				tCity.ai.debug(pTool);
			}
			pTool.setSeparator();
			pTool.setText("action_timer:", tCity.timer_action, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Kingdom AI";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			WorldTile tTile = World.world.getMouseTilePos();
			if (tTile == null)
			{
				return;
			}
			City tCity = tTile.zone.city;
			if (tCity == null)
			{
				return;
			}
			Kingdom tKingdom = tCity.kingdom;
			if (tKingdom.hasKing())
			{
				pTool.setText("personality:", tKingdom.king.s_personality.id, 0f, false, 0L, false, false, "");
				pTool.setText("agression:", tKingdom.king.stats["personality_aggression"], 0f, false, 0L, false, false, "");
				pTool.setText("administration:", tKingdom.king.stats["personality_administration"], 0f, false, 0L, false, false, "");
				pTool.setText("diplomatic:", tKingdom.king.stats["personality_diplomatic"], 0f, false, 0L, false, false, "");
				pTool.setSeparator();
			}
			pTool.setText("timer_action:", tKingdom.timer_action, 0f, false, 0L, false, false, "");
			pTool.setText("timer_new_king:", tKingdom.data.timer_new_king, 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("action_timer:", tKingdom.timer_action, 0f, false, 0L, false, false, "");
			if (tKingdom.ai != null)
			{
				tKingdom.ai.debug(pTool);
			}
		};
		this.add(debugToolAsset4);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00061854 File Offset: 0x0005FA54
	private void initFmod()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "FMOD";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			MusicBox.debug_fmod(pTool);
		};
		this.add(debugToolAsset);
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "FMOD World Params";
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			MusicBox.inst.debug_world_params(pTool);
		};
		this.add(debugToolAsset2);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "FMOD Unit Params";
		debugToolAsset3.action_1 = delegate(DebugTool pTool)
		{
			MusicBox.inst.debug_unit_params(pTool);
		};
		this.add(debugToolAsset3);
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "FMOD Params";
		debugToolAsset4.action_1 = delegate(DebugTool pTool)
		{
			MusicBox.inst.debug_params(pTool);
		};
		this.add(debugToolAsset4);
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "Cursor Speed";
		debugToolAsset5.action_1 = delegate(DebugTool pTool)
		{
			MapBox.cursor_speed.debug(pTool);
		};
		this.add(debugToolAsset5);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0006198D File Offset: 0x0005FB8D
	private void initUI()
	{
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "screen_orientation";
		debugToolAsset.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("width:", Screen.width, 0f, false, 0L, false, false, "");
			pTool.setText("height:", Screen.height, 0f, false, 0L, false, false, "");
			pTool.setText("last width:", CanvasMain.instance.getLastWidth(), 0f, false, 0L, false, false, "");
			pTool.setText("last height:", CanvasMain.instance.getLastHeight(), 0f, false, 0L, false, false, "");
			pTool.setText("orientation:", Screen.orientation, 0f, false, 0L, false, false, "");
			pTool.setText("saved orientation:", PlayerConfig.optionBoolEnabled("portrait") ? ScreenOrientation.Portrait : ScreenOrientation.LandscapeLeft, 0f, false, 0L, false, false, "");
			pTool.setText("rotation to portrait:", Screen.autorotateToPortrait, 0f, false, 0L, false, false, "");
			pTool.setText("rotation to landscape left:", Screen.autorotateToLandscapeLeft, 0f, false, 0L, false, false, "");
			pTool.setText("rotation to landscape right:", Screen.autorotateToLandscapeRight, 0f, false, 0L, false, false, "");
			pTool.setText("rotation to portrait reversed:", Screen.autorotateToPortraitUpsideDown, 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000619CC File Offset: 0x0005FBCC
	public override void post_init()
	{
		base.post_init();
		this.list.Sort((DebugToolAsset a, DebugToolAsset b) => a.priority.CompareTo(b.priority));
		this.list.Sort((DebugToolAsset a, DebugToolAsset b) => string.Compare(a.id, b.id, StringComparison.InvariantCultureIgnoreCase));
		TextInfo tTextInfo = CultureInfo.InvariantCulture.TextInfo;
		foreach (DebugToolAsset tAsset in this.list)
		{
			if (tAsset.id.ToLower() == tAsset.id)
			{
				tAsset.name = tTextInfo.ToTitleCase(tAsset.id.Replace('_', ' '));
			}
			else
			{
				tAsset.name = tAsset.id;
			}
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00061AC0 File Offset: 0x0005FCC0
	public override DebugToolAsset get(string pId)
	{
		foreach (DebugToolAsset tAsset in this.list)
		{
			if (tAsset.name == pId)
			{
				return tAsset;
			}
		}
		return base.get(pId);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00061B28 File Offset: 0x0005FD28
	private void initBenchmarks()
	{
		this.add(new DebugToolAsset
		{
			id = "Benchmark All",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			priority = 1,
			benchmark_group_id = "game_total",
			benchmark_total = "game_total",
			benchmark_total_group = "main",
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		DebugToolAsset debugToolAsset = new DebugToolAsset();
		debugToolAsset.id = "Benchmark Test Decisions";
		debugToolAsset.show_benchmark_buttons = true;
		debugToolAsset.type = DebugToolType.Benchmarks;
		debugToolAsset.priority = 50;
		debugToolAsset.benchmark_group_id = "decisions_test";
		debugToolAsset.benchmark_total = "decisions_test";
		debugToolAsset.benchmark_total_group = "decisions_test_total";
		debugToolAsset.split_benchmark = true;
		debugToolAsset.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = false;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Values;
		};
		debugToolAsset.action_update = delegate(DebugTool pTool)
		{
			if (World.world.units.Count == 0)
			{
				return;
			}
			Actor tActor = World.world.units.getSimpleList()[0];
			Bench.bench("decisions_test", "decisions_test_total", false);
			Bench.bench("decisions", "decisions_test", false);
			for (int i = 0; i < 5000; i++)
			{
				DecisionHelper.runSimulation(tActor);
			}
			Bench.benchEnd("decisions", "decisions_test", false, 0L, false);
			Bench.benchEnd("decisions_test", "decisions_test_total", false, 0L, false);
		};
		this.add(debugToolAsset);
		this.add(new DebugToolAsset
		{
			id = "Benchmark Zone Camera",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "zone_camera",
			benchmark_total = "zone_camera",
			benchmark_total_group = "zone_camera_total",
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		DebugToolAsset debugToolAsset2 = new DebugToolAsset();
		debugToolAsset2.id = "benchmark_chunks";
		debugToolAsset2.show_benchmark_buttons = true;
		debugToolAsset2.type = DebugToolType.Benchmarks;
		debugToolAsset2.benchmark_group_id = "chunks";
		debugToolAsset2.benchmark_total = "chunks";
		debugToolAsset2.benchmark_total_group = "chunks_total";
		debugToolAsset2.split_benchmark = true;
		debugToolAsset2.action_1 = delegate(DebugTool pTool)
		{
			double tBudget = this.getTotalFrameBudget();
			double tTotalGroup = Bench.getBenchResultAsDouble(pTool.asset.benchmark_total, pTool.asset.benchmark_total_group, pTool.isValueAverage());
			pTool.setText("group total:", this.trim(tTotalGroup, true), 100f, true, 0L, false, false, "");
			double tTotalTimeSpent = tTotalGroup / (double)Time.deltaTime * 100.0;
			pTool.setText("total frame time spent:", this.trimPercent(tTotalTimeSpent, true), (float)tTotalTimeSpent, true, 0L, false, false, "");
			double tBudgetTimeSpent = tTotalGroup * 1000.0 / tBudget * 100.0;
			pTool.setText("total budget time spent:", this.trimPercent(tBudgetTimeSpent, true), (float)tBudgetTimeSpent, true, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("########### last_dirty:", null, 0f, false, 0L, false, false, "");
			pTool.setText("chunks:", Bench.getBenchValue("m_dirtyChunks", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("new regions:", Bench.getBenchValue("m_newRegions", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("new links:", Bench.getBenchValue("m_newLinks", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("new islands:", Bench.getBenchValue("m_newIslands", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("last dirty islands:", Bench.getBenchValue("m_dirtyIslands", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("last dirty corners:", Bench.getBenchValue("m_dirtyCorners", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setText("dirty islands neighb:", Bench.getBenchValue("m_dirtyIslandNeighb", "chunks"), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("########### last_bench:", null, 0f, false, 0L, false, false, "");
		};
		debugToolAsset2.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset2.action_start = delegate(DebugTool pTool)
		{
			pTool.show_averages = false;
			pTool.show_counter = true;
			pTool.show_max = false;
			pTool.hide_zeroes = false;
			pTool.state = DebugToolState.Percent;
		};
		this.add(debugToolAsset2);
		this.add(new DebugToolAsset
		{
			id = "Benchmark Quantum Sprites",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "quantum_sprites",
			benchmark_total = "quantum_sprites",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Cache Manager",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "world_cache_manager",
			benchmark_total = "world_cache_manager",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.Values;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Sim Zones",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "sim_zones",
			benchmark_total = "sim_zones",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.Values;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark MusicBox",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "music_box",
			benchmark_total = "music_box",
			benchmark_total_group = "music_box_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.Values;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Nameplates",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "nameplates",
			benchmark_total = "nameplates",
			benchmark_total_group = "nameplates_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.FrameBudget;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Borderers Renderer",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "borders_renderer",
			benchmark_total = "borders_renderer",
			benchmark_total_group = "borders_renderer_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.FrameBudget;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Fluid Zones Data",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "fluid_zones_data",
			benchmark_total = "fluid_zones_data",
			benchmark_total_group = "fluid_zones_data_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = false;
				pTool.hide_zeroes = false;
				pTool.show_max = false;
				pTool.state = DebugToolState.FrameBudget;
			}
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark World Beh",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "world_beh",
			benchmark_total = "world_beh",
			benchmark_total_group = "game_total",
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Buildings",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "buildings",
			benchmark_total = "buildings",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		DebugToolAsset t = this.t;
		t.action_1 = (DebugToolAssetAction)Delegate.Combine(t.action_1, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			JobManagerBuildings tJobMan = World.world.buildings.getJobManager();
			pTool.setText("batches total/free:", tJobMan.debugBatchCount(), 0f, false, 0L, false, false, "");
			pTool.setText("active jobs:", tJobMan.debugJobCount(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
		}));
		this.add(new DebugToolAsset
		{
			id = "Benchmark Actors",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "actors",
			benchmark_total = "actors",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		DebugToolAsset t2 = this.t;
		t2.action_1 = (DebugToolAssetAction)Delegate.Combine(t2.action_1, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			JobManagerActors tJobMan = World.world.units.getJobManager();
			pTool.setText("batches total/free:", tJobMan.debugBatchCount(), 0f, false, 0L, false, false, "");
			pTool.setText("active jobs:", tJobMan.debugJobCount(), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
		}));
		this.add(new DebugToolAsset
		{
			id = "Benchmark AI Actions",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "ai_actions",
			benchmark_total = "ai_actions",
			benchmark_total_group = "ai_actions_total",
			split_benchmark = true,
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_max = false;
				pTool.show_averages = true;
				pTool.hide_zeroes = true;
				pTool.show_max = false;
				pTool.sort_by_names = false;
				pTool.sort_by_values = true;
				pTool.state = DebugToolState.Values;
			},
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		this.t.show_on_start = DebugConfig.isOn(DebugOption.BenchAiEnabled);
		this.add(new DebugToolAsset
		{
			id = "Benchmark AI Tasks",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "ai_tasks",
			benchmark_total = "ai_tasks",
			benchmark_total_group = "ai_tasks_total",
			split_benchmark = true,
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_max = false;
				pTool.show_averages = true;
				pTool.hide_zeroes = true;
				pTool.show_max = false;
				pTool.sort_by_names = false;
				pTool.sort_by_values = true;
				pTool.state = DebugToolState.Values;
			},
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		this.t.show_on_start = DebugConfig.isOn(DebugOption.BenchAiEnabled);
		DebugToolAsset debugToolAsset3 = new DebugToolAsset();
		debugToolAsset3.id = "$benchmark_loops$";
		debugToolAsset3.show_benchmark_buttons = true;
		debugToolAsset3.type = DebugToolType.Benchmarks;
		debugToolAsset3.benchmark_group_id = "loops_test_100";
		debugToolAsset3.benchmark_total = "loops_test_100";
		debugToolAsset3.benchmark_total_group = "loops_test_total_100";
		debugToolAsset3.show_last_count = true;
		debugToolAsset3.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset3.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset3.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = true;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Values;
		};
		debugToolAsset3.action_update = delegate(DebugTool pTool)
		{
			BenchmarkLoops.update(pTool.asset);
		};
		this.add(debugToolAsset3);
		this.clone("Benchmark Loops 10", "$benchmark_loops$");
		this.t.benchmark_group_id = "loops_test_10";
		this.t.benchmark_total = "loops_test_10";
		this.t.benchmark_total_group = "loops_test_total_10";
		DebugToolAsset t3 = this.t;
		t3.action_start = (DebugToolAssetAction)Delegate.Combine(t3.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkLoops(pTool.asset, 10);
		}));
		this.clone("Benchmark Loops 100", "$benchmark_loops$");
		this.t.benchmark_group_id = "loops_test_100";
		this.t.benchmark_total = "loops_test_100";
		this.t.benchmark_total_group = "loops_test_total_100";
		DebugToolAsset t4 = this.t;
		t4.action_start = (DebugToolAssetAction)Delegate.Combine(t4.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkLoops(pTool.asset, 100);
		}));
		this.clone("Benchmark Loops 1000", "$benchmark_loops$");
		this.t.benchmark_group_id = "loops_test_1000";
		this.t.benchmark_total = "loops_test_1000";
		this.t.benchmark_total_group = "loops_test_total_1000";
		DebugToolAsset t5 = this.t;
		t5.action_start = (DebugToolAssetAction)Delegate.Combine(t5.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkLoops(pTool.asset, 1000);
		}));
		this.clone("Benchmark Loops 10000", "$benchmark_loops$");
		this.t.benchmark_group_id = "loops_test_10000";
		this.t.benchmark_total = "loops_test_10000";
		this.t.benchmark_total_group = "loops_test_total_10000";
		DebugToolAsset t6 = this.t;
		t6.action_start = (DebugToolAssetAction)Delegate.Combine(t6.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkLoops(pTool.asset, 10000);
		}));
		DebugToolAsset debugToolAsset4 = new DebugToolAsset();
		debugToolAsset4.id = "Benchmark Distance";
		debugToolAsset4.show_benchmark_buttons = true;
		debugToolAsset4.type = DebugToolType.Benchmarks;
		debugToolAsset4.benchmark_group_id = "dist_test";
		debugToolAsset4.benchmark_total = "dist_test";
		debugToolAsset4.benchmark_total_group = "dist_test_total";
		debugToolAsset4.show_last_count = true;
		debugToolAsset4.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset4.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset4.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = false;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Percent;
			new BenchmarkDist();
		};
		debugToolAsset4.action_update = delegate(DebugTool _)
		{
			BenchmarkDist.update();
		};
		this.add(debugToolAsset4);
		this.clone("$benchmark_shuffle_loops$", "$benchmark_loops$");
		this.t.action_update = delegate(DebugTool pTool)
		{
			BenchmarkShuffle.update(pTool.asset);
		};
		this.clone("Benchmark Shuffle Loops 10", "$benchmark_shuffle_loops$");
		this.t.benchmark_group_id = "shuffle_test_10";
		this.t.benchmark_total = "shuffle_test_10";
		this.t.benchmark_total_group = "shuffle_test_total_10";
		DebugToolAsset t7 = this.t;
		t7.action_start = (DebugToolAssetAction)Delegate.Combine(t7.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkShuffle(pTool.asset, 10, 50);
		}));
		this.clone("Benchmark Shuffle Loops 100", "$benchmark_shuffle_loops$");
		this.t.benchmark_group_id = "shuffle_test_100";
		this.t.benchmark_total = "shuffle_test_100";
		this.t.benchmark_total_group = "shuffle_test_total_100";
		DebugToolAsset t8 = this.t;
		t8.action_start = (DebugToolAssetAction)Delegate.Combine(t8.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkShuffle(pTool.asset, 100, 500);
		}));
		this.clone("Benchmark Shuffle Loops 1000", "$benchmark_shuffle_loops$");
		this.t.benchmark_group_id = "shuffle_test_1000";
		this.t.benchmark_total = "shuffle_test_1000";
		this.t.benchmark_total_group = "shuffle_test_total_1000";
		DebugToolAsset t9 = this.t;
		t9.action_start = (DebugToolAssetAction)Delegate.Combine(t9.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkShuffle(pTool.asset, 1000, 5000);
		}));
		this.clone("Benchmark Shuffle Loops 10000", "$benchmark_shuffle_loops$");
		this.t.benchmark_group_id = "shuffle_test_10000";
		this.t.benchmark_total = "shuffle_test_10000";
		this.t.benchmark_total_group = "shuffle_test_total_10000";
		DebugToolAsset t10 = this.t;
		t10.action_start = (DebugToolAssetAction)Delegate.Combine(t10.action_start, new DebugToolAssetAction(delegate(DebugTool pTool)
		{
			new BenchmarkShuffle(pTool.asset, 10000, 25000);
		}));
		DebugToolAsset debugToolAsset5 = new DebugToolAsset();
		debugToolAsset5.id = "Benchmark Field Acess";
		debugToolAsset5.show_benchmark_buttons = true;
		debugToolAsset5.type = DebugToolType.Benchmarks;
		debugToolAsset5.benchmark_group_id = "field_acess_test";
		debugToolAsset5.benchmark_total = "field_acess_test";
		debugToolAsset5.benchmark_total_group = "field_acess_total";
		debugToolAsset5.split_benchmark = true;
		debugToolAsset5.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset5.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset5.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = false;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Values;
		};
		debugToolAsset5.action_update = delegate(DebugTool _)
		{
			BenchmarkFieldAccess.start();
		};
		this.add(debugToolAsset5);
		DebugToolAsset debugToolAsset6 = new DebugToolAsset();
		debugToolAsset6.id = "Benchmark Sprites";
		debugToolAsset6.show_benchmark_buttons = true;
		debugToolAsset6.type = DebugToolType.Benchmarks;
		debugToolAsset6.benchmark_group_id = "sprites_test";
		debugToolAsset6.benchmark_total = "sprites_test";
		debugToolAsset6.benchmark_total_group = "sprites_test_total";
		debugToolAsset6.split_benchmark = true;
		debugToolAsset6.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset6.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset6.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = false;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Values;
		};
		debugToolAsset6.action_update = delegate(DebugTool _)
		{
			BenchmarkSprites.start();
		};
		this.add(debugToolAsset6);
		DebugToolAsset debugToolAsset7 = new DebugToolAsset();
		debugToolAsset7.id = "Benchmark Struct Loops";
		debugToolAsset7.show_benchmark_buttons = true;
		debugToolAsset7.type = DebugToolType.Benchmarks;
		debugToolAsset7.benchmark_group_id = "loops_struct_test";
		debugToolAsset7.benchmark_total = "loops_struct_test";
		debugToolAsset7.benchmark_total_group = "loops_struct_test_total";
		debugToolAsset7.split_benchmark = true;
		debugToolAsset7.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset7.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset7.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = false;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.Values;
		};
		debugToolAsset7.action_update = delegate(DebugTool pTool)
		{
			BenchmarkStructLoops.start();
		};
		this.add(debugToolAsset7);
		this.add(new DebugToolAsset
		{
			id = "Benchmark ECS",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "ecs_test",
			benchmark_total = "ecs_test",
			benchmark_total_group = "ecs_test_total",
			split_benchmark = true,
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom),
			action_start = delegate(DebugTool pTool)
			{
				this.setBenchmarksDefaultValue(pTool);
				pTool.show_counter = true;
				pTool.show_averages = false;
				pTool.hide_zeroes = false;
				pTool.show_max = true;
				pTool.sort_by_names = true;
				pTool.state = DebugToolState.Percent;
			}
		});
		DebugToolAsset debugToolAsset8 = new DebugToolAsset();
		debugToolAsset8.id = "Benchmark Blacklist";
		debugToolAsset8.show_benchmark_buttons = true;
		debugToolAsset8.type = DebugToolType.Benchmarks;
		debugToolAsset8.benchmark_group_id = "blacklist_test";
		debugToolAsset8.benchmark_total = "blacklist_test";
		debugToolAsset8.benchmark_total_group = "blacklist_test_total";
		debugToolAsset8.split_benchmark = true;
		debugToolAsset8.action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop);
		debugToolAsset8.action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom);
		debugToolAsset8.action_start = delegate(DebugTool pTool)
		{
			this.setBenchmarksDefaultValue(pTool);
			pTool.show_counter = true;
			pTool.show_averages = true;
			pTool.hide_zeroes = false;
			pTool.show_max = true;
			pTool.sort_by_names = false;
			pTool.sort_by_values = true;
			pTool.state = DebugToolState.TimeSpent;
		};
		debugToolAsset8.action_update = delegate(DebugTool pTool)
		{
			BenchmarkBlacklist.start();
		};
		this.add(debugToolAsset8);
		this.add(new DebugToolAsset
		{
			id = "Benchmark Trait Effects",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "effects_traits",
			benchmark_total = "effects_traits",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		this.add(new DebugToolAsset
		{
			id = "Benchmark Item Effects",
			show_benchmark_buttons = true,
			type = DebugToolType.Benchmarks,
			benchmark_group_id = "effects_items",
			benchmark_total = "effects_items",
			benchmark_total_group = "game_total",
			split_benchmark = true,
			action_start = new DebugToolAssetAction(this.setBenchmarksDefaultValue),
			action_1 = new DebugToolAssetAction(this.showGroupBenchmarkTop),
			action_2 = new DebugToolAssetAction(this.showGroupBenchmarkBottom)
		});
		DebugToolAsset debugToolAsset9 = new DebugToolAsset();
		debugToolAsset9.id = "Benchmark";
		debugToolAsset9.type = DebugToolType.Benchmarks;
		debugToolAsset9.priority = 2;
		debugToolAsset9.action_1 = delegate(DebugTool pTool)
		{
			pTool.setText("CityBehCheckSettleTarget_tick:", Bench.getBenchResult("CityBehCheckSettleTarget", "main", false), 0f, false, 0L, false, false, "");
			pTool.setSeparator();
			pTool.setText("test_follow:", Bench.getBenchResult("test_follow", "main", true), 0f, false, 0L, false, false, "");
		};
		this.add(debugToolAsset9);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00062E0C File Offset: 0x0006100C
	private void setBenchmarksDefaultValue(DebugTool pTool)
	{
		pTool.sort_order_reversed = false;
		pTool.sort_by_names = false;
		pTool.sort_by_values = false;
		pTool.show_averages = true;
		pTool.hide_zeroes = true;
		pTool.show_counter = true;
		pTool.show_max = true;
		pTool.state = DebugToolState.FrameBudget;
		pTool.paused = false;
		pTool.percentage_slowest = false;
		if (Config.editor_mastef)
		{
			DebugConfig.debugToolMastefDefaults(pTool);
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00062E6C File Offset: 0x0006106C
	private void showGroupBenchmarkTop(DebugTool pTool)
	{
		float tDeltaTime = Time.deltaTime;
		double tBudget = this.getTotalFrameBudget();
		double tTotalGame = Bench.getBenchResultAsDouble("game_total", "main", pTool.isValueAverage());
		pTool.setText("game total:", this.trim(tTotalGame, true), 0f, false, 0L, false, false, "");
		pTool.setText("fps:", FPS.getFPS(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		double tTotalGroup = Bench.getBenchResultAsDouble(pTool.asset.benchmark_total, pTool.asset.benchmark_total_group, pTool.isValueAverage());
		if (pTool.asset.benchmark_total != "game_total")
		{
			pTool.setText("group total:", this.trim(tTotalGroup, true), 100f, true, 0L, false, false, "");
			double tTotalPercentage = tTotalGroup / tTotalGame * 100.0;
			pTool.setText("usage from total:", this.trimPercent(tTotalPercentage, true), (float)tTotalPercentage, true, 0L, false, false, "");
		}
		else
		{
			pTool.setSeparator();
			pTool.setSeparator();
		}
		double tTotalTimeSpent = tTotalGroup / (double)tDeltaTime * 100.0;
		pTool.setText("total frame time spent:", this.trimPercent(tTotalTimeSpent, true), (float)tTotalTimeSpent, true, 0L, false, false, "");
		double tBudgetTimeSpent = tTotalGroup * 1000.0 / tBudget * 100.0;
		pTool.setText("total budget time spent:", this.trimPercent(tBudgetTimeSpent, true), (float)tBudgetTimeSpent, true, 0L, false, false, "");
		pTool.setSeparator();
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00062FF0 File Offset: 0x000611F0
	private void showGroupBenchmarkBottom(DebugTool pTool)
	{
		double tBudget = this.getTotalFrameBudget();
		float tDeltaTime = Time.deltaTime;
		List<ToolBenchmarkData> tList = new List<ToolBenchmarkData>(Bench.getGroup(pTool.asset.benchmark_group_id).dict_data.Values);
		if (!pTool.percentage_slowest)
		{
			double tTotalGroup = Bench.getBenchResultAsDouble(pTool.asset.benchmark_total, pTool.asset.benchmark_total_group, pTool.isValueAverage());
			using (List<ToolBenchmarkData>.Enumerator enumerator = tList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ToolBenchmarkData tData = enumerator.Current;
					double tValue = tData.latest_result;
					if (pTool.isValueAverage())
					{
						tValue = tData.getAverage();
					}
					double tPercentage = tValue / tTotalGroup * 100.0;
					tData.calculated_percentage = tPercentage;
				}
				goto IL_194;
			}
		}
		double tSlowestValue = 0.0;
		foreach (ToolBenchmarkData tData2 in tList)
		{
			double tValue2 = pTool.isValueAverage() ? tData2.getAverage() : tData2.latest_result;
			if (tValue2 > tSlowestValue)
			{
				tSlowestValue = tValue2;
			}
		}
		foreach (ToolBenchmarkData tData3 in tList)
		{
			double tPercentage2 = (pTool.isValueAverage() ? tData3.getAverage() : tData3.latest_result) / tSlowestValue * 100.0;
			if (((float)tPercentage2).Equals(100f))
			{
				tPercentage2 += 1.0;
			}
			tData3.calculated_percentage = tPercentage2;
		}
		IL_194:
		if (pTool.sort_by_names)
		{
			tList.Sort((ToolBenchmarkData a, ToolBenchmarkData b) => b.id.CompareTo(a.id));
		}
		else if (pTool.isState(DebugToolState.Percent))
		{
			tList.Sort((ToolBenchmarkData a, ToolBenchmarkData b) => a.calculated_percentage.CompareTo(b.calculated_percentage));
		}
		else if (pTool.isValueAverage())
		{
			tList.Sort((ToolBenchmarkData a, ToolBenchmarkData b) => a.getAverage().CompareTo(b.getAverage()));
		}
		else
		{
			tList.Sort((ToolBenchmarkData a, ToolBenchmarkData b) => a.latest_result.CompareTo(b.latest_result));
		}
		if (!pTool.sort_order_reversed)
		{
			tList.Reverse();
		}
		foreach (ToolBenchmarkData tData4 in tList)
		{
			double tValue3 = tData4.latest_result;
			if (pTool.isValueAverage())
			{
				tValue3 = tData4.getAverage();
			}
			long tCounter = 0L;
			bool tShowCounter = false;
			bool tShowMax = pTool.show_max;
			string tMaxValue = string.Empty;
			if (pTool.asset.split_benchmark && pTool.show_counter)
			{
				tCounter = tData4.getAverageCount();
				tShowCounter = true;
			}
			else if (pTool.asset.show_last_count && pTool.show_counter)
			{
				tCounter = tData4.getLastCount();
				tShowCounter = true;
			}
			string tPT = string.Empty;
			string tPT2 = string.Empty;
			double tBarValue = 0.0;
			switch (pTool.state)
			{
			case DebugToolState.Values:
				if (pTool.hide_zeroes && tValue3 < 1E-06)
				{
					continue;
				}
				tPT = tData4.id + ":";
				tPT2 = this.trim(tValue3, false);
				tBarValue = tData4.calculated_percentage;
				tData4.saveLastMaxValue(tValue3);
				tMaxValue = this.trim(tData4.last_max_value, false);
				break;
			case DebugToolState.Percent:
				if (pTool.hide_zeroes && tData4.calculated_percentage < 0.1)
				{
					continue;
				}
				tPT = tData4.id + ":";
				tPT2 = this.trimPercent(tData4.calculated_percentage, true);
				tBarValue = tData4.calculated_percentage;
				tData4.saveLastMaxValue(tData4.calculated_percentage);
				tMaxValue = this.trimPercent(tData4.last_max_value, true);
				break;
			case DebugToolState.TimeSpent:
			{
				double tTimeSpent = tValue3 / (double)tDeltaTime * 100.0;
				if (pTool.hide_zeroes && tTimeSpent < 0.1)
				{
					continue;
				}
				tPT = tData4.id + ":";
				tPT2 = this.trimPercent(tTimeSpent, true);
				tBarValue = tTimeSpent;
				tData4.saveLastMaxValue(tTimeSpent);
				tMaxValue = this.trimPercent(tData4.last_max_value, true);
				break;
			}
			case DebugToolState.FrameBudget:
			{
				double tBudgetTime = tValue3 * 1000.0 / tBudget * 100.0;
				if (pTool.hide_zeroes && tBudgetTime < 0.1)
				{
					continue;
				}
				tPT = tData4.id + ":";
				tPT2 = this.trimPercent(tBudgetTime, true);
				tBarValue = tBudgetTime;
				tData4.saveLastMaxValue(tBudgetTime);
				tMaxValue = this.trimPercent(tData4.last_max_value, true);
				break;
			}
			}
			pTool.setText(tPT, tPT2, (float)tBarValue, true, tCounter, tShowCounter, tShowMax, tMaxValue);
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0006354C File Offset: 0x0006174C
	private string trim(double pValue, bool pAddMS = false)
	{
		pValue *= 1000.0;
		string tResult = pValue.ToString("F5");
		if (pAddMS)
		{
			tResult += " ms";
		}
		return tResult;
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00063584 File Offset: 0x00061784
	private string trimPercent(double pValue, bool pAddPercent = true)
	{
		string tResult = pValue.ToString("F1");
		if (pAddPercent)
		{
			tResult += "%";
		}
		return tResult;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000635B0 File Offset: 0x000617B0
	private double getTotalFrameBudget()
	{
		double tTargetFps = 60.0;
		if (Config.fps_lock_30)
		{
			tTargetFps = 30.0;
		}
		return 1000.0 / tTargetFps * 0.6499999761581421;
	}

	// Token: 0x04000748 RID: 1864
	private UtilityBasedDecisionSystem _decision_system_debug;
}
