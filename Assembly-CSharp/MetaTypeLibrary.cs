using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class MetaTypeLibrary : AssetLibrary<MetaTypeAsset>
{
	// Token: 0x06001A04 RID: 6660 RVA: 0x000F21CC File Offset: 0x000F03CC
	public override void init()
	{
		base.init();
		MetaTypeAsset metaTypeAsset = new MetaTypeAsset();
		metaTypeAsset.id = "world";
		metaTypeAsset.window_name = "world_info";
		metaTypeAsset.get_list = (() => new NanoObject[0]);
		metaTypeAsset.has_any = (() => false);
		metaTypeAsset.get_selected = (() => World.world.world_object);
		metaTypeAsset.set_selected = delegate(NanoObject pElement)
		{
			World.world.world_object = (pElement as WorldObject);
		};
		metaTypeAsset.get = ((long _) => World.world.world_object);
		this.add(metaTypeAsset);
		MetaTypeAsset metaTypeAsset2 = new MetaTypeAsset();
		metaTypeAsset2.id = "item";
		metaTypeAsset2.window_name = "item";
		metaTypeAsset2.window_action_clear = delegate()
		{
			SelectedMetas.selected_item = null;
		};
		metaTypeAsset2.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.item = SelectedMetas.selected_item;
			if (!WindowHistory.hasHistory())
			{
				return;
			}
			if (WindowHistory.list.Last<WindowHistoryData>().window.GetComponent<ItemWindow>() == null)
			{
				return;
			}
			ScrollWindow.setPreviousWindowSprite(pHistoryData.item.getSprite());
		};
		metaTypeAsset2.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_item = pHistoryData.item;
		};
		metaTypeAsset2.get_list = (() => World.world.items);
		metaTypeAsset2.custom_sorted_list = delegate()
		{
			ListPool<NanoObject> tList = new ListPool<NanoObject>(64);
			foreach (Item tItem in World.world.items)
			{
				if (tItem.isFavorite())
				{
					tList.Add(tItem);
				}
			}
			return tList;
		};
		metaTypeAsset2.has_any = (() => World.world.items.hasAny());
		metaTypeAsset2.get_selected = (() => SelectedMetas.selected_item);
		metaTypeAsset2.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_item = (pElement as Item);
		};
		metaTypeAsset2.get = ((long pId) => World.world.items.get(pId));
		metaTypeAsset2.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Item tMeta = World.world.items.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "equipment", new TooltipData
			{
				item = tMeta
			});
		};
		metaTypeAsset2.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Item tMeta = World.world.items.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_item = tMeta;
			ScrollWindow.showWindow("item");
		};
		MetaTypeLibrary.item = this.add(metaTypeAsset2);
		MetaTypeAsset metaTypeAsset3 = new MetaTypeAsset();
		metaTypeAsset3.id = "unit";
		metaTypeAsset3.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset3.window_name = "unit";
		metaTypeAsset3.power_tab_id = "selected_unit";
		metaTypeAsset3.icon_single_path = "ui/icons/iconSpecies";
		metaTypeAsset3.window_action_clear = delegate()
		{
			if (SelectedUnit.isSet() && SelectedObjects.getSelectedNanoObject() is Actor)
			{
				PowerTabController.showTabSelectedUnit();
			}
		};
		metaTypeAsset3.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			if (!SelectedUnit.isSet())
			{
				return;
			}
			pHistoryData.unit = SelectedUnit.unit;
			if (!WindowHistory.hasHistory())
			{
				return;
			}
			WindowHistoryData tData = WindowHistory.list.Last<WindowHistoryData>();
			if (tData.window.GetComponent<UnitWindow>() == null)
			{
				return;
			}
			if (tData.unit.isRekt())
			{
				return;
			}
			ScrollWindow.setPreviousWindowSprite(tData.unit.asset.getSpriteIcon());
		};
		metaTypeAsset3.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			if (!pHistoryData.unit.isRekt())
			{
				SelectedUnit.clear();
				SelectedUnit.select(pHistoryData.unit, true);
				return;
			}
			SelectedUnit.clear();
		};
		metaTypeAsset3.get_list = (() => World.world.units);
		metaTypeAsset3.custom_sorted_list = delegate()
		{
			ListPool<NanoObject> tList = new ListPool<NanoObject>(64);
			foreach (Actor tActor in World.world.units)
			{
				if (!tActor.isRekt() && tActor.isFavorite())
				{
					tList.Add(tActor);
				}
			}
			return tList;
		};
		metaTypeAsset3.has_any = (() => World.world.units.Count > 0);
		metaTypeAsset3.get_selected = (() => SelectedUnit.unit);
		metaTypeAsset3.set_selected = delegate(NanoObject pElement)
		{
			SelectedUnit.select(pElement as Actor, true);
		};
		metaTypeAsset3.get = ((long pId) => World.world.units.get(pId));
		metaTypeAsset3.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Actor tMeta = World.world.units.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			string tTipType = "actor";
			if (tMeta.isKing())
			{
				tTipType = "actor_king";
			}
			if (tMeta.isCityLeader())
			{
				tTipType = "actor_leader";
			}
			Tooltip.show(pField, tTipType, new TooltipData
			{
				actor = tMeta
			});
		};
		metaTypeAsset3.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Actor tUnit = World.world.units.get(pMetaId);
			if (tUnit.isRekt())
			{
				return;
			}
			ActionLibrary.openUnitWindow(tUnit);
		};
		MetaTypeLibrary.unit = this.add(metaTypeAsset3);
		MetaTypeAsset metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "war";
		metaTypeAsset4.window_name = "war";
		metaTypeAsset4.icon_list = "iconWarList";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_war = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.war = SelectedMetas.selected_war;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_war = pHistoryData.war;
		};
		metaTypeAsset4.reports = new string[]
		{
			"war_high_casualties",
			"war_long",
			"war_fresh",
			"war_defenders_getting_captured",
			"war_attackers_getting_captured",
			"war_quiet",
			"war_full_on_battle"
		};
		metaTypeAsset4.get_list = (() => World.world.wars);
		metaTypeAsset4.has_any = (() => World.world.wars.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_war);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_war = (pElement as War);
		};
		metaTypeAsset4.get = ((long pId) => World.world.wars.get(pId));
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			War tMeta = World.world.wars.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "war", new TooltipData
			{
				war = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			War tMeta = World.world.wars.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_war = tMeta;
			ScrollWindow.showWindow("war");
		};
		MetaTypeLibrary.war = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "plot";
		metaTypeAsset4.window_name = "plot";
		metaTypeAsset4.icon_list = "iconPlotList";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_plot = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.plot = SelectedMetas.selected_plot;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_plot = pHistoryData.plot;
		};
		metaTypeAsset4.get_list = (() => World.world.plots);
		metaTypeAsset4.has_any = (() => World.world.plots.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_plot);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_plot = (pElement as Plot);
		};
		metaTypeAsset4.get = ((long pId) => World.world.plots.get(pId));
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Plot tMeta = World.world.plots.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "plot", new TooltipData
			{
				plot = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Plot tMeta = World.world.plots.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_plot = tMeta;
			ScrollWindow.showWindow("plot");
		};
		metaTypeAsset4.decision_ids = new string[]
		{
			"check_plot"
		};
		MetaTypeLibrary.plot = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "religion";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "religion";
		metaTypeAsset4.power_tab_id = "selected_religion";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconReligionList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconReligion";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_religion = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.religion = SelectedMetas.selected_religion;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_religion = pHistoryData.religion;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.religions);
		metaTypeAsset4.has_any = (() => World.world.religions.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_religion);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_religion = (pElement as Religion);
		};
		metaTypeAsset4.get = ((long pId) => World.world.religions.get(pId));
		metaTypeAsset4.map_mode = MetaType.Religion;
		metaTypeAsset4.option_id = "map_religion_layer";
		metaTypeAsset4.power_option_zone_id = "religion_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectReligion);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasReligion());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_religion = pActor.religion;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (MetaTypeLibrary.religion.getZoneOptionState() == 2)
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Religion tCursorMeta;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_FC;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				tCursorMeta = tMouseCity.kingdom.getReligion();
				if (tCursorMeta.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = tMouseCity.kingdom.getCities().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			tCursorMeta = tCursorCity.getReligion();
			if (tCursorMeta.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (iCity.getReligion() == tCursorMeta)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_FC:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getReligionOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom.religion;
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.religion;
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Religion tReligion = pMeta as Religion;
			if (tReligion.isRekt())
			{
				return;
			}
			string tType = "religion";
			Tooltip.hideTooltip(tReligion, true, tType);
			Tooltip.show(tReligion, tType, new TooltipData
			{
				religion = tReligion,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasReligion())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.religion, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Religion tMeta = World.world.religions.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "religion", new TooltipData
			{
				religion = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Religion tMeta = World.world.religions.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_religion = tMeta;
			ScrollWindow.showWindow("religion");
		};
		MetaTypeLibrary.religion = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "culture";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "culture";
		metaTypeAsset4.power_tab_id = "selected_culture";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconCultureList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconCulture";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_culture = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.culture = SelectedMetas.selected_culture;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_culture = pHistoryData.culture;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.cultures);
		metaTypeAsset4.has_any = (() => World.world.cultures.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_culture);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_culture = (pElement as Culture);
		};
		metaTypeAsset4.get = ((long pId) => World.world.cultures.get(pId));
		metaTypeAsset4.map_mode = MetaType.Culture;
		metaTypeAsset4.option_id = "map_culture_layer";
		metaTypeAsset4.power_option_zone_id = "culture_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectCulture);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasCulture());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_culture = pActor.culture;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Culture tCursorMeta;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_FC;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				tCursorMeta = tMouseCity.kingdom.getCulture();
				if (tCursorMeta.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = tMouseCity.kingdom.getCities().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			tCursorMeta = tCursorCity.getCulture();
			if (tCursorMeta.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (iCity.getCulture() == tCursorMeta)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_FC:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getCultureOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom.culture;
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.culture;
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Culture tCulture = pMeta as Culture;
			if (tCulture.isRekt())
			{
				return;
			}
			string tType = "culture";
			Tooltip.hideTooltip(tCulture, true, tType);
			Tooltip.show(tCulture, tType, new TooltipData
			{
				culture = tCulture,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasCulture())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.culture, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Culture tMeta = World.world.cultures.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "culture", new TooltipData
			{
				culture = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Culture tMeta = World.world.cultures.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_culture = tMeta;
			ScrollWindow.showWindow("culture");
		};
		MetaTypeLibrary.culture = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "family";
		metaTypeAsset4.window_name = "family";
		metaTypeAsset4.power_tab_id = "selected_family";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.unit_amount_alpha = true;
		metaTypeAsset4.icon_list = "iconFamilyList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconFamily";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_family = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.family = SelectedMetas.selected_family;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_family = pHistoryData.family;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.families);
		metaTypeAsset4.has_any = (() => World.world.families.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_family);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_family = (pElement as Family);
		};
		metaTypeAsset4.get = ((long pId) => World.world.families.get(pId));
		metaTypeAsset4.map_mode = MetaType.Family;
		metaTypeAsset4.option_id = "map_family_layer";
		metaTypeAsset4.power_option_zone_id = "family_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.decision_ids = new string[]
		{
			"family_check_existence",
			"family_alpha_move",
			"family_group_follow",
			"family_group_leave",
			"child_follow_parent"
		};
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectFamily);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasFamily());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_family = pActor.family;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Family tCursorMeta;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_161;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				if (!tMouseCity.kingdom.hasKing())
				{
					return;
				}
				if (!tMouseCity.kingdom.king.hasFamily())
				{
					return;
				}
				tCursorMeta = tMouseCity.kingdom.king.family;
				if (tCursorMeta.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = tMouseCity.kingdom.getCities().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			if (!tCursorCity.hasLeader())
			{
				return;
			}
			if (!tCursorCity.leader.hasFamily())
			{
				return;
			}
			tCursorMeta = tCursorCity.leader.family;
			if (tCursorMeta.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (!iCity.hasLeader())
					{
						break;
					}
					if (!iCity.leader.hasFamily())
					{
						break;
					}
					if (iCity.leader.family == tCursorMeta)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_161:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getFamilyOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			Actor king = city.kingdom.king;
			if (king == null)
			{
				return null;
			}
			return king.family;
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			Actor leader = city.leader;
			if (leader == null)
			{
				return null;
			}
			return leader.family;
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Family tFamily = pMeta as Family;
			if (tFamily.isRekt())
			{
				return;
			}
			string tType = "family";
			Tooltip.hideTooltip(tFamily, true, tType);
			Tooltip.show(tFamily, tType, new TooltipData
			{
				family = tFamily,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasFamily() && tActor.family.units.Count >= 2)
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.family, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Family tMeta = World.world.families.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "family", new TooltipData
			{
				family = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Family tMeta = World.world.families.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_family = tMeta;
			ScrollWindow.showWindow("family");
		};
		MetaTypeLibrary.family = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "army";
		metaTypeAsset4.window_name = "army";
		metaTypeAsset4.power_tab_id = "selected_army";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconArmyList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconArmy";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_army = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.army = SelectedMetas.selected_army;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_army = pHistoryData.army;
		};
		metaTypeAsset4.get_list = (() => World.world.armies);
		metaTypeAsset4.has_any = (() => World.world.armies.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_army);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_army = (pElement as Army);
		};
		metaTypeAsset4.get = ((long pId) => World.world.armies.get(pId));
		metaTypeAsset4.map_mode = MetaType.Army;
		metaTypeAsset4.option_id = "map_army_layer";
		metaTypeAsset4.power_option_zone_id = "army_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.dynamic_zone_option = 0;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectArmy);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasArmy());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_army = pActor.army;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy"
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			pMetaTypeAsset.getZoneOptionState();
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getArmyOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = ((TileZone _) => null);
		metaTypeAsset4.tile_get_metaobject_1 = ((TileZone _) => null);
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Army tMeta = pMeta as Army;
			if (tMeta.isRekt())
			{
				return;
			}
			string tType = "army";
			Tooltip.hideTooltip(tMeta, true, tType);
			Tooltip.show(tMeta, tType, new TooltipData
			{
				army = tMeta,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasArmy())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.army, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Army tMeta = World.world.armies.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "army", new TooltipData
			{
				army = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Army tMeta = World.world.armies.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_army = tMeta;
			ScrollWindow.showWindow("army");
		};
		MetaTypeLibrary.army = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "language";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "language";
		metaTypeAsset4.power_tab_id = "selected_language";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconLanguageList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconLanguage";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_language = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.language = SelectedMetas.selected_language;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_language = pHistoryData.language;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.languages);
		metaTypeAsset4.has_any = (() => World.world.languages.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_language);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_language = (pElement as Language);
		};
		metaTypeAsset4.get = ((long pId) => World.world.languages.get(pId));
		metaTypeAsset4.map_mode = MetaType.Language;
		metaTypeAsset4.option_id = "map_language_layer";
		metaTypeAsset4.power_option_zone_id = "language_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectLanguage);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasLanguage());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_language = pActor.language;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Language tCursorMeta;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_FC;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				tCursorMeta = tMouseCity.kingdom.getLanguage();
				if (tCursorMeta.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = tMouseCity.kingdom.getCities().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			tCursorMeta = tCursorCity.getLanguage();
			if (tCursorMeta.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (iCity.getLanguage() == tCursorMeta)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_FC:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getLanguageOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom.getLanguage();
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.getLanguage();
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Language tLanguage = pMeta as Language;
			if (tLanguage.isRekt())
			{
				return;
			}
			string tType = "language";
			Tooltip.hideTooltip(tLanguage, true, tType);
			Tooltip.show(tLanguage, tType, new TooltipData
			{
				language = tLanguage,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasLanguage())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.language, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Language tMeta = World.world.languages.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "language", new TooltipData
			{
				language = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Language tMeta = World.world.languages.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_language = tMeta;
			ScrollWindow.showWindow("language");
		};
		MetaTypeLibrary.language = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "subspecies";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "subspecies";
		metaTypeAsset4.power_tab_id = "selected_subspecies";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.unit_amount_alpha = true;
		metaTypeAsset4.icon_list = "iconSubspeciesList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconSpecies";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_subspecies = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.subspecies = SelectedMetas.selected_subspecies;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_subspecies = pHistoryData.subspecies;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.subspecies);
		metaTypeAsset4.has_any = (() => World.world.subspecies.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_subspecies);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_subspecies = (pElement as Subspecies);
		};
		metaTypeAsset4.get = ((long pId) => World.world.subspecies.get(pId));
		metaTypeAsset4.map_mode = MetaType.Subspecies;
		metaTypeAsset4.option_id = "map_subspecies_layer";
		metaTypeAsset4.power_option_zone_id = "subspecies_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectSubspecies);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasSubspecies());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_subspecies = pActor.subspecies;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Subspecies tCursorMeta;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_105;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				tCursorMeta = tMouseCity.kingdom.getMainSubspecies();
				if (tCursorMeta.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						if (tCity.getMainSubspecies() == tCursorMeta)
						{
							QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
						}
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			tCursorMeta = tCursorCity.getMainSubspecies();
			if (tCursorMeta.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (iCity.getMainSubspecies() == tCursorMeta)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_105:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getSubspeciesOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom.getMainSubspecies();
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.getMainSubspecies();
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Subspecies tSubspecies = pMeta as Subspecies;
			if (tSubspecies.isRekt())
			{
				return;
			}
			string tType = "subspecies";
			Tooltip.hideTooltip(tSubspecies, true, tType);
			Tooltip.show(tSubspecies, tType, new TooltipData
			{
				subspecies = tSubspecies,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasSubspecies())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.subspecies, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Subspecies tMeta = World.world.subspecies.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "subspecies", new TooltipData
			{
				subspecies = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Subspecies tMeta = World.world.subspecies.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_subspecies = tMeta;
			ScrollWindow.showWindow("subspecies");
		};
		MetaTypeLibrary.subspecies = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "city";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "city";
		metaTypeAsset4.power_tab_id = "selected_city";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconCityList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconCity";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_city = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.city = SelectedMetas.selected_city;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_city = pHistoryData.city;
		};
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.dynamic_zone_option = 1;
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"food_none",
			"food_plenty",
			"food_running_out",
			"wood_none",
			"stone_none",
			"gold_none",
			"metal_none",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.cities);
		metaTypeAsset4.has_any = (() => World.world.cities.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_city);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_city = (pElement as City);
		};
		metaTypeAsset4.get = ((long pId) => World.world.cities.get(pId));
		metaTypeAsset4.map_mode = MetaType.City;
		metaTypeAsset4.option_id = "map_city_layer";
		metaTypeAsset4.power_option_zone_id = "city_layer";
		metaTypeAsset4.decision_ids = new string[]
		{
			"give_tax",
			"store_resources",
			"make_items",
			"find_house",
			"try_to_take_city_item",
			"repair_equipment",
			"city_idle_walking",
			"replenish_energy",
			"put_out_fire"
		};
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectCity);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasCity());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_city = pActor.city;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
			this.drawForCities(pMetaTypeAsset, WildKingdomsManager.neutral.getCities(), this.getZoneDelegate(pMetaTypeAsset));
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasCity() && tActor.isKingdomCiv())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.city, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			bool tShowEnemies = PlayerConfig.optionBoolEnabled("highlight_kingdom_enemies");
			if (pMetaTypeAsset.getZoneOptionState() == 0)
			{
				if (pTile.zone.city.isRekt())
				{
					return;
				}
				QuantumSpriteLibrary.colorZones(pQAsset, pTile.zone.city.zones, pQAsset.color);
				if (tShowEnemies)
				{
					QuantumSpriteLibrary.colorEnemies(pQAsset, pTile.zone.city.kingdom);
					return;
				}
			}
			else
			{
				this.highlightDefault(pTile, pQAsset, pQAsset.color);
			}
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getCityOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = ((TileZone pZone) => pZone.city);
		metaTypeAsset4.tile_get_metaobject_1 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = ((TileZone pZone, MetaTypeAsset pAsset, int pZoneOption) => pAsset.tile_get_metaobject(pZone, pZoneOption) != null);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			City tCity = pMeta as City;
			if (tCity.isRekt())
			{
				return;
			}
			string tType = "city";
			Tooltip.hideTooltip(tCity, true, tType);
			Tooltip.show(tCity, tType, new TooltipData
			{
				city = tCity,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			City tMeta = World.world.cities.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "city", new TooltipData
			{
				city = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			City tMeta = World.world.cities.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_city = tMeta;
			ScrollWindow.showWindow("city");
		};
		MetaTypeLibrary.city = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "kingdom";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "kingdom";
		metaTypeAsset4.power_tab_id = "selected_kingdom";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconKingdomList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconKingdom";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_kingdom = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.kingdom = SelectedMetas.selected_kingdom;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_kingdom = pHistoryData.kingdom;
		};
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.dynamic_zone_option = 1;
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.kingdoms);
		metaTypeAsset4.has_any = (() => World.world.kingdoms.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_kingdom);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_kingdom = (pElement as Kingdom);
		};
		metaTypeAsset4.get = ((long pId) => World.world.kingdoms.get(pId));
		metaTypeAsset4.map_mode = MetaType.Kingdom;
		metaTypeAsset4.option_id = "map_kingdom_layer";
		metaTypeAsset4.power_option_zone_id = "kingdom_layer";
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectKingdom);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.isKingdomCiv());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_kingdom = pActor.kingdom;
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
			this.drawForCities(pMetaTypeAsset, WildKingdomsManager.neutral.getCities(), this.getZoneDelegate(pMetaTypeAsset));
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasKingdom() && tActor.isKingdomCiv())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.kingdom, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			bool tShowEnemies = PlayerConfig.optionBoolEnabled("highlight_kingdom_enemies");
			Color tColorAnimated = pQAsset.color;
			if (pMetaTypeAsset.getZoneOptionState() == 0)
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				foreach (City tCity in tMouseCity.kingdom.getCities())
				{
					QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
				}
				if (tShowEnemies)
				{
					QuantumSpriteLibrary.colorEnemies(pQAsset, tMouseCity.kingdom);
					return;
				}
			}
			else
			{
				this.highlightDefault(pTile, pQAsset, tColorAnimated);
			}
		};
		metaTypeAsset4.tile_get_metaobject = delegate(TileZone pZone, int pZoneOption)
		{
			IMetaObject tMeta = pZone.getKingdomOnZone(pZoneOption);
			if (tMeta == null)
			{
				return null;
			}
			if (((Kingdom)tMeta).isNeutral())
			{
				return null;
			}
			return tMeta;
		};
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom;
		};
		metaTypeAsset4.tile_get_metaobject_1 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = delegate(TileZone pZone, MetaTypeAsset pAsset, int pZoneOption)
		{
			IMetaObject tMeta = pAsset.tile_get_metaobject(pZone, pZoneOption);
			return tMeta != null && !((Kingdom)tMeta).isNeutral();
		};
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Kingdom tKingdom = pMeta as Kingdom;
			if (tKingdom.isRekt())
			{
				return;
			}
			string tType = "kingdom";
			Tooltip.hideTooltip(tKingdom, true, tType);
			Tooltip.show(tKingdom, tType, new TooltipData
			{
				kingdom = tKingdom,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Kingdom tKingdom = World.world.kingdoms.get(pMetaId);
			if (tKingdom.isRekt())
			{
				return;
			}
			if (tKingdom.isNeutral())
			{
				return;
			}
			Tooltip.show(pField, "kingdom", new TooltipData
			{
				kingdom = tKingdom
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Kingdom tKingdom = World.world.kingdoms.get(pMetaId);
			if (tKingdom.isRekt())
			{
				return;
			}
			if (tKingdom.isNeutral())
			{
				return;
			}
			SelectedMetas.selected_kingdom = tKingdom;
			ScrollWindow.showWindow("kingdom");
		};
		MetaTypeLibrary.kingdom = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "clan";
		metaTypeAsset4.ranks = MetaTypeLibrary.generateExponentialRanks(100.0, 1.5);
		metaTypeAsset4.window_name = "clan";
		metaTypeAsset4.power_tab_id = "selected_clan";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconClanList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconClan";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_clan = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.clan = SelectedMetas.selected_clan;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_clan = pHistoryData.clan;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.clans);
		metaTypeAsset4.has_any = (() => World.world.clans.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_clan);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_clan = (pElement as Clan);
		};
		metaTypeAsset4.get = ((long pId) => World.world.clans.get(pId));
		metaTypeAsset4.map_mode = MetaType.Clan;
		metaTypeAsset4.option_id = "map_clan_layer";
		metaTypeAsset4.power_option_zone_id = "clan_layer";
		metaTypeAsset4.has_dynamic_zones = true;
		metaTypeAsset4.dynamic_zone_option = 2;
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectClan);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.hasClan());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_clan = pActor.clan;
		};
		metaTypeAsset4.decision_ids = new string[]
		{
			"try_new_plot"
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			if (pMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				this.drawDefaultFluid(pMetaTypeAsset);
				return;
			}
			this.drawDefaultMeta(pMetaTypeAsset);
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			Color tColorAnimated = pQAsset.color;
			int zoneOptionState = pMetaTypeAsset.getZoneOptionState();
			Clan tCursorClan;
			if (zoneOptionState != 0)
			{
				if (zoneOptionState != 1)
				{
					goto IL_FC;
				}
			}
			else
			{
				City tMouseCity = pTile.zone.city;
				if (tMouseCity.isRekt())
				{
					return;
				}
				tCursorClan = tMouseCity.kingdom.getKingClan();
				if (tCursorClan.isRekt())
				{
					return;
				}
				using (IEnumerator<City> enumerator = tMouseCity.kingdom.getCities().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						City tCity = enumerator.Current;
						QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
					}
					return;
				}
			}
			City tCursorCity = pTile.zone.city;
			if (tCursorCity.isRekt())
			{
				return;
			}
			tCursorClan = tCursorCity.getRoyalClan();
			if (tCursorClan.isRekt())
			{
				return;
			}
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					City iCity = enumerator.Current;
					if (iCity.getRoyalClan() == tCursorClan)
					{
						QuantumSpriteLibrary.colorZones(pQAsset, iCity.zones, tColorAnimated);
					}
				}
				return;
			}
			IL_FC:
			this.highlightDefault(pTile, pQAsset, tColorAnimated);
		};
		metaTypeAsset4.tile_get_metaobject = ((TileZone pZone, int pZoneOption) => pZone.getClanOnZone(pZoneOption));
		metaTypeAsset4.tile_get_metaobject_0 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.kingdom.getKingClan();
		};
		metaTypeAsset4.tile_get_metaobject_1 = delegate(TileZone pZone)
		{
			City city = pZone.city;
			if (city == null)
			{
				return null;
			}
			return city.getRoyalClan();
		};
		metaTypeAsset4.tile_get_metaobject_2 = ((TileZone pZone) => ZoneMetaDataVisualizer.getZoneMetaData(pZone).meta_object);
		metaTypeAsset4.check_tile_has_meta = new MetaZoneTooltipAction(this.checkTileHasMetaDefault);
		metaTypeAsset4.check_cursor_tooltip = new MetaZoneTooltipAction(this.checkCursorTooltipDefault);
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Clan tClan = pMeta as Clan;
			if (tClan.isRekt())
			{
				return;
			}
			string tType = "clan";
			Tooltip.hideTooltip(tClan, true, tType);
			Tooltip.show(tClan, tType, new TooltipData
			{
				clan = tClan,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.dynamic_zones = delegate()
		{
			List<Actor> tList = World.world.units.getSimpleList();
			double tCurTimestamp = World.world.getCurWorldTime();
			int i = 0;
			int tLen = tList.Count;
			while (i < tLen)
			{
				Actor tActor = tList[i];
				if (tActor.asset.show_on_meta_layer)
				{
					TileZone tZone = tActor.current_tile.zone;
					if (tActor.hasClan())
					{
						ZoneMetaDataVisualizer.countMetaZone(tZone, tActor.clan, tCurTimestamp);
					}
				}
				i++;
			}
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Clan tMeta = World.world.clans.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "clan", new TooltipData
			{
				clan = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Clan tMeta = World.world.clans.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_clan = tMeta;
			ScrollWindow.showWindow("clan");
		};
		MetaTypeLibrary.clan = this.add(metaTypeAsset4);
		metaTypeAsset4 = new MetaTypeAsset();
		metaTypeAsset4.id = "alliance";
		metaTypeAsset4.window_name = "alliance";
		metaTypeAsset4.power_tab_id = "selected_alliance";
		metaTypeAsset4.force_zone_when_selected = true;
		metaTypeAsset4.set_icon_for_cancel_button = true;
		metaTypeAsset4.icon_list = "iconAllianceList";
		metaTypeAsset4.icon_single_path = "ui/icons/iconAlliance";
		metaTypeAsset4.window_action_clear = delegate()
		{
			SelectedMetas.selected_alliance = null;
		};
		metaTypeAsset4.window_history_action_update = delegate(ref WindowHistoryData pHistoryData)
		{
			pHistoryData.alliance = SelectedMetas.selected_alliance;
		};
		metaTypeAsset4.window_history_action_restore = delegate(ref WindowHistoryData pHistoryData)
		{
			SelectedMetas.selected_alliance = pHistoryData.alliance;
		};
		metaTypeAsset4.reports = new string[]
		{
			"happy",
			"unhappy",
			"many_children",
			"many_homeless"
		};
		metaTypeAsset4.get_list = (() => World.world.alliances);
		metaTypeAsset4.has_any = (() => World.world.alliances.hasAny());
		metaTypeAsset4.get_selected = (() => SelectedMetas.selected_alliance);
		metaTypeAsset4.set_selected = delegate(NanoObject pElement)
		{
			SelectedMetas.selected_alliance = (pElement as Alliance);
		};
		metaTypeAsset4.get = ((long pId) => World.world.alliances.get(pId));
		metaTypeAsset4.map_mode = MetaType.Alliance;
		metaTypeAsset4.option_id = "map_alliance_layer";
		metaTypeAsset4.power_option_zone_id = "alliance_layer";
		metaTypeAsset4.click_action_zone = new MetaZoneClickAction(ActionLibrary.inspectAlliance);
		metaTypeAsset4.selected_tab_action_meta = new MetaTypeActionAsset(this.defaultClickActionZone);
		metaTypeAsset4.check_unit_has_meta = ((Actor pActor) => pActor.kingdom.hasAlliance());
		metaTypeAsset4.set_unit_set_meta_for_meta_for_window = delegate(Actor pActor)
		{
			SelectedMetas.selected_alliance = pActor.kingdom.getAlliance();
		};
		metaTypeAsset4.draw_zones = delegate(MetaTypeAsset pMetaTypeAsset)
		{
			int tZoneOption = pMetaTypeAsset.getZoneOptionState();
			foreach (Alliance alliance in World.world.alliances)
			{
				foreach (Kingdom kingdom in alliance.kingdoms_hashset)
				{
					foreach (City city in kingdom.getCities())
					{
						foreach (TileZone tZone in city.zones)
						{
							this.zone_manager.drawBegin();
							this.zone_manager.drawZoneAlliance(tZone, tZoneOption);
							this.zone_manager.drawEnd(tZone);
						}
					}
				}
			}
			foreach (Kingdom tKingdom in World.world.kingdoms)
			{
				if (!tKingdom.hasAlliance())
				{
					foreach (City city2 in tKingdom.getCities())
					{
						foreach (TileZone tZone2 in city2.zones)
						{
							this.zone_manager.drawBegin();
							this.zone_manager.drawZoneCity(tZone2);
							this.zone_manager.drawEnd(tZone2);
						}
					}
				}
			}
		};
		metaTypeAsset4.check_cursor_highlight = delegate(MetaTypeAsset pMetaTypeAsset, WorldTile pTile, QuantumSpriteAsset pQAsset)
		{
			bool tShowEnemies = PlayerConfig.optionBoolEnabled("highlight_kingdom_enemies");
			Color tColorAnimated = pQAsset.color;
			City tMouseCity = pTile.zone.city;
			if (tMouseCity.isRekt())
			{
				return;
			}
			Kingdom tMouseKingdom = tMouseCity.kingdom;
			if (tMouseKingdom.hasAlliance())
			{
				using (HashSet<Kingdom>.Enumerator enumerator = tMouseKingdom.getAlliance().kingdoms_hashset.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Kingdom kingdom = enumerator.Current;
						foreach (City tKingdomCity in kingdom.getCities())
						{
							QuantumSpriteLibrary.colorZones(pQAsset, tKingdomCity.zones, tColorAnimated);
						}
					}
					goto IL_E8;
				}
			}
			foreach (City tCity in tMouseCity.kingdom.getCities())
			{
				QuantumSpriteLibrary.colorZones(pQAsset, tCity.zones, tColorAnimated);
			}
			IL_E8:
			if (tShowEnemies)
			{
				QuantumSpriteLibrary.colorEnemies(pQAsset, tMouseKingdom);
			}
		};
		metaTypeAsset4.check_tile_has_meta = delegate(TileZone pZone, MetaTypeAsset pAsset, int pZoneOption)
		{
			City tCity = pZone.city;
			return !tCity.isRekt() && !tCity.kingdom.getAlliance().isRekt();
		};
		metaTypeAsset4.check_cursor_tooltip = delegate(TileZone pZone, MetaTypeAsset pAsset, int pZoneOption)
		{
			City tMouseCity = pZone.city;
			if (tMouseCity.isRekt())
			{
				return false;
			}
			Alliance tAlliance = tMouseCity.kingdom.getAlliance();
			if (tAlliance.isRekt())
			{
				return MetaTypeLibrary.kingdom.check_cursor_tooltip(pZone, MetaTypeLibrary.kingdom, pZoneOption);
			}
			tAlliance.meta_type_asset.cursor_tooltip_action(tAlliance);
			return true;
		};
		metaTypeAsset4.cursor_tooltip_action = delegate(NanoObject pMeta)
		{
			Alliance tAlliance = pMeta as Alliance;
			if (tAlliance.isRekt())
			{
				return;
			}
			string tType = "alliance";
			Tooltip.hideTooltip(tAlliance, true, tType);
			Tooltip.show(tAlliance, tType, new TooltipData
			{
				alliance = tAlliance,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true
			});
		};
		metaTypeAsset4.stat_hover = delegate(long pMetaId, MonoBehaviour pField)
		{
			Alliance tMeta = World.world.alliances.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			Tooltip.show(pField, "alliance", new TooltipData
			{
				alliance = tMeta
			});
		};
		metaTypeAsset4.stat_click = delegate(long pMetaId, MonoBehaviour _)
		{
			Alliance tMeta = World.world.alliances.get(pMetaId);
			if (tMeta.isRekt())
			{
				return;
			}
			SelectedMetas.selected_alliance = tMeta;
			ScrollWindow.showWindow("alliance");
		};
		MetaTypeLibrary.alliance = this.add(metaTypeAsset4);
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x000F502C File Offset: 0x000F322C
	private MetaZoneGetMetaSimple getZoneDelegate(MetaTypeAsset pMetaTypeAsset)
	{
		switch (pMetaTypeAsset.getZoneOptionState())
		{
		case 0:
			return pMetaTypeAsset.tile_get_metaobject_0;
		case 1:
			return pMetaTypeAsset.tile_get_metaobject_1;
		case 2:
			return pMetaTypeAsset.tile_get_metaobject_2;
		default:
			return pMetaTypeAsset.tile_get_metaobject_2;
		}
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x000F5070 File Offset: 0x000F3270
	private void drawDefaultFluid(MetaTypeAsset pMetaTypeAsset)
	{
		foreach (ZoneMetaData tData in ZoneMetaDataVisualizer.zone_data_dict.Values)
		{
			if (tData.meta_object != null && tData.meta_object.isAlive())
			{
				this.zone_manager.drawBegin();
				this.zone_manager.drawGenericFluid(tData, pMetaTypeAsset);
				this.zone_manager.drawEnd(tData.zone);
			}
		}
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x000F5100 File Offset: 0x000F3300
	private void drawDefaultMeta(MetaTypeAsset pMetaTypeAsset)
	{
		MetaZoneGetMetaSimple tZoneGetDelegate = this.getZoneDelegate(pMetaTypeAsset);
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			this.drawForCities(pMetaTypeAsset, tKingdom.getCities(), tZoneGetDelegate);
		}
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000F5160 File Offset: 0x000F3360
	private void drawForCities(MetaTypeAsset pMetaTypeAsset, IEnumerable<City> pListCities, MetaZoneGetMetaSimple pZoneGetDelegate)
	{
		foreach (City tCity in pListCities)
		{
			this.drawZonesForMeta(pMetaTypeAsset, tCity.zones, pZoneGetDelegate);
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x000F51B0 File Offset: 0x000F33B0
	private void drawZonesForMeta(MetaTypeAsset pMetaTypeAsset, List<TileZone> pZones, MetaZoneGetMetaSimple pZoneGetDelegate)
	{
		foreach (TileZone tZone in pZones)
		{
			this.zone_manager.drawBegin();
			this.zone_manager.drawZoneMeta(tZone, pMetaTypeAsset, pZoneGetDelegate);
			this.zone_manager.drawEnd(tZone);
		}
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x000F521C File Offset: 0x000F341C
	private void defaultClickActionZone(MetaTypeAsset pMetaTypeAsset)
	{
		PowerTabController.showTabSelectedMeta(pMetaTypeAsset);
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x000F5224 File Offset: 0x000F3424
	private bool checkCursorTooltipDefault(TileZone pTile, MetaTypeAsset pAsset, int pZoneOption)
	{
		IMetaObject tMeta = pAsset.tile_get_metaobject(pTile, pZoneOption);
		if (tMeta == null)
		{
			return false;
		}
		pAsset.cursor_tooltip_action(tMeta as NanoObject);
		return true;
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000F5256 File Offset: 0x000F3456
	private bool checkTileHasMetaDefault(TileZone pZone, MetaTypeAsset pAsset, int pZoneOption)
	{
		return pAsset.tile_get_metaobject(pZone, pZoneOption) != null;
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x000F5268 File Offset: 0x000F3468
	private void highlightDefault(WorldTile pTile, QuantumSpriteAsset pQAsset, Color pColorAnimated)
	{
		ZoneMetaData tData = ZoneMetaDataVisualizer.getZoneMetaData(pTile.zone);
		if (tData.meta_object == null)
		{
			return;
		}
		if (!tData.meta_object.isAlive())
		{
			return;
		}
		using (ListPool<TileZone> tList = ZoneMetaDataVisualizer.getZonesWithMeta(tData.meta_object))
		{
			QuantumSpriteLibrary.colorZones(pQAsset, tList, pColorAnimated);
		}
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x000F52C8 File Offset: 0x000F34C8
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (MetaTypeAsset tAsset in this.list)
		{
			if (tAsset.decision_ids != null)
			{
				tAsset.decisions_assets = new DecisionAsset[tAsset.decision_ids.Length];
				for (int i = 0; i < tAsset.decision_ids.Length; i++)
				{
					string tDecisionID = tAsset.decision_ids[i];
					DecisionAsset tDecisionAsset = AssetManager.decisions_library.get(tDecisionID);
					tAsset.decisions_assets[i] = tDecisionAsset;
				}
			}
			if (!string.IsNullOrEmpty(tAsset.option_id))
			{
				tAsset.option_asset = AssetManager.options_library.get(tAsset.option_id);
			}
		}
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000F5390 File Offset: 0x000F3590
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x000F5398 File Offset: 0x000F3598
	public static int[] generateExponentialRanks(double pBasePoints, double pGrowthFactor)
	{
		int[] tArray = new int[10];
		double tRunningTotal = pBasePoints;
		for (int tRank = 1; tRank <= 10; tRank++)
		{
			tArray[tRank - 1] = MetaTypeLibrary.roundToNiceNumber(tRunningTotal);
			tRunningTotal += pBasePoints * Math.Pow(pGrowthFactor, (double)(tRank - 1));
		}
		return tArray;
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x000F53D8 File Offset: 0x000F35D8
	private static int roundToNiceNumber(double value)
	{
		if (value < 1000.0)
		{
			return (int)(Math.Round(value / 100.0) * 100.0);
		}
		return (int)(Math.Round(value / 500.0) * 500.0);
	}

	// Token: 0x06001A12 RID: 6674 RVA: 0x000F5428 File Offset: 0x000F3628
	public MetaTypeAsset getAsset(MetaType pType)
	{
		return this.get(pType.AsString());
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x000F5438 File Offset: 0x000F3638
	public MetaTypeAsset getFromPower(string pPower)
	{
		GodPower tPower = AssetManager.powers.get(pPower);
		return this.getFromPower(tPower);
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000F5458 File Offset: 0x000F3658
	public MetaTypeAsset getFromPower(GodPower pPower)
	{
		foreach (MetaTypeAsset tAsset in this.list)
		{
			if (tAsset.power_option_zone_id == pPower.id)
			{
				return tAsset;
			}
		}
		return null;
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000F54C0 File Offset: 0x000F36C0
	private ZoneCalculator zone_manager
	{
		get
		{
			return World.world.zone_calculator;
		}
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000F54CC File Offset: 0x000F36CC
	public void debug(DebugTool pTool)
	{
		foreach (MetaTypeAsset tMetaTypeAsset in AssetManager.meta_type_library.list)
		{
			NanoObject tSelected = tMetaTypeAsset.get_selected();
			if (!tSelected.isRekt())
			{
				pTool.setText(tMetaTypeAsset.id + ":", tSelected.getTypeID(), 0f, false, 0L, false, false, "");
			}
			else
			{
				pTool.setText(tMetaTypeAsset.id + ":", "-", 0f, false, 0L, false, false, "");
			}
		}
	}

	// Token: 0x04001438 RID: 5176
	[NonSerialized]
	public static MetaTypeAsset alliance;

	// Token: 0x04001439 RID: 5177
	[NonSerialized]
	public static MetaTypeAsset city;

	// Token: 0x0400143A RID: 5178
	[NonSerialized]
	public static MetaTypeAsset clan;

	// Token: 0x0400143B RID: 5179
	[NonSerialized]
	public static MetaTypeAsset culture;

	// Token: 0x0400143C RID: 5180
	[NonSerialized]
	public static MetaTypeAsset family;

	// Token: 0x0400143D RID: 5181
	[NonSerialized]
	public static MetaTypeAsset army;

	// Token: 0x0400143E RID: 5182
	[NonSerialized]
	public static MetaTypeAsset kingdom;

	// Token: 0x0400143F RID: 5183
	[NonSerialized]
	public static MetaTypeAsset language;

	// Token: 0x04001440 RID: 5184
	[NonSerialized]
	public static MetaTypeAsset plot;

	// Token: 0x04001441 RID: 5185
	[NonSerialized]
	public static MetaTypeAsset religion;

	// Token: 0x04001442 RID: 5186
	[NonSerialized]
	public static MetaTypeAsset subspecies;

	// Token: 0x04001443 RID: 5187
	[NonSerialized]
	public static MetaTypeAsset unit;

	// Token: 0x04001444 RID: 5188
	[NonSerialized]
	public static MetaTypeAsset war;

	// Token: 0x04001445 RID: 5189
	[NonSerialized]
	public static MetaTypeAsset item;
}
