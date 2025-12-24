using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004DC RID: 1244
public class TesterBehSelectRandomMetaObjects : BehaviourActionTester
{
	// Token: 0x06002A0C RID: 10764 RVA: 0x0014AB94 File Offset: 0x00148D94
	public TesterBehSelectRandomMetaObjects(bool pPickSelectedObjects = false)
	{
		this._pick_selected_objects = pPickSelectedObjects;
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x0014ABA4 File Offset: 0x00148DA4
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (this._trait_editors == null)
		{
			this._trait_editors = new string[]
			{
				PowerLibrary.traits_delta_rain_edit.id,
				PowerLibrary.traits_gamma_rain_edit.id,
				PowerLibrary.traits_omega_rain_edit.id
			};
		}
		Config.selected_trait_editor = this._trait_editors.GetRandom<string>();
		SelectedMetas.selected_alliance = Randy.getRandom<Alliance>(BehaviourActionBase<AutoTesterBot>.world.alliances.list);
		SelectedMetas.selected_army = Randy.getRandom<Army>(BehaviourActionBase<AutoTesterBot>.world.armies.list);
		SelectedMetas.selected_city = Randy.getRandom<City>(BehaviourActionBase<AutoTesterBot>.world.cities.list);
		SelectedMetas.selected_clan = Randy.getRandom<Clan>(BehaviourActionBase<AutoTesterBot>.world.clans.list);
		SelectedMetas.selected_culture = Randy.getRandom<Culture>(BehaviourActionBase<AutoTesterBot>.world.cultures.list);
		SelectedMetas.selected_family = Randy.getRandom<Family>(BehaviourActionBase<AutoTesterBot>.world.families.list);
		SelectedMetas.selected_item = Randy.getRandom<Item>(BehaviourActionBase<AutoTesterBot>.world.items.list);
		SelectedMetas.selected_kingdom = Randy.getRandom<Kingdom>(BehaviourActionBase<AutoTesterBot>.world.kingdoms.list);
		SelectedMetas.selected_language = Randy.getRandom<Language>(BehaviourActionBase<AutoTesterBot>.world.languages.list);
		SelectedMetas.selected_plot = Randy.getRandom<Plot>(BehaviourActionBase<AutoTesterBot>.world.plots.list);
		SelectedMetas.selected_religion = Randy.getRandom<Religion>(BehaviourActionBase<AutoTesterBot>.world.religions.list);
		SelectedMetas.selected_subspecies = Randy.getRandom<Subspecies>(BehaviourActionBase<AutoTesterBot>.world.subspecies.list);
		SelectedMetas.selected_war = Randy.getRandom<War>(BehaviourActionBase<AutoTesterBot>.world.wars.list);
		int tTries = 10;
		while (tTries-- > 0)
		{
			Actor tUnit = BehaviourActionBase<AutoTesterBot>.world.units.GetRandom();
			if (!tUnit.isRekt() && tUnit.asset.can_be_inspected)
			{
				SelectedUnit.clear();
				SelectedUnit.select(tUnit, true);
				SelectedObjects.setNanoObject(tUnit);
				PowerTabController.showTabSelectedUnit();
				break;
			}
		}
		if (SelectedMetas.selected_item != null)
		{
			SelectedMetas.selected_item.data.favorite = Randy.randomBool();
		}
		if (SelectedUnit.isSet())
		{
			SelectedUnit.unit.data.favorite = Randy.randomBool();
		}
		Config.selected_objects_graph.Clear();
		if (this._pick_selected_objects)
		{
			List<NanoObject> tRandomItems = new List<NanoObject>();
			if (SelectedMetas.selected_alliance != null)
			{
				tRandomItems.Add(SelectedMetas.selected_alliance);
			}
			if (SelectedMetas.selected_city != null)
			{
				tRandomItems.Add(SelectedMetas.selected_city);
			}
			if (SelectedMetas.selected_clan != null)
			{
				tRandomItems.Add(SelectedMetas.selected_clan);
			}
			if (SelectedMetas.selected_culture != null)
			{
				tRandomItems.Add(SelectedMetas.selected_culture);
			}
			if (SelectedMetas.selected_family != null)
			{
				tRandomItems.Add(SelectedMetas.selected_family);
			}
			if (SelectedMetas.selected_kingdom != null)
			{
				tRandomItems.Add(SelectedMetas.selected_kingdom);
			}
			if (SelectedMetas.selected_language != null)
			{
				tRandomItems.Add(SelectedMetas.selected_language);
			}
			if (SelectedMetas.selected_religion != null)
			{
				tRandomItems.Add(SelectedMetas.selected_religion);
			}
			if (SelectedMetas.selected_subspecies != null)
			{
				tRandomItems.Add(SelectedMetas.selected_subspecies);
			}
			tRandomItems.Shuffle<NanoObject>();
			int i = 0;
			while (i < 3 && i < tRandomItems.Count)
			{
				if (tRandomItems[i] != null)
				{
					Config.selected_objects_graph.Add(tRandomItems[i]);
				}
				i++;
			}
		}
		return BehResult.Continue;
	}

	// Token: 0x04001F59 RID: 8025
	private bool _pick_selected_objects;

	// Token: 0x04001F5A RID: 8026
	private string[] _trait_editors;
}
