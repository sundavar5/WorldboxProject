using System;
using System.Collections.Generic;
using ai.behaviours;
using FMOD.Studio;
using life.taxi;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class DebugWorldText : MonoBehaviour
{
	// Token: 0x06001BF0 RID: 7152 RVA: 0x000FEDD0 File Offset: 0x000FCFD0
	public void create()
	{
		this.text_mesh_bg_clone.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Debug");
		this.text_mesh_bg_clone.GetComponent<Renderer>().sortingOrder = 1;
		this.text_mesh.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Debug");
		this.text_mesh.GetComponent<Renderer>().sortingOrder = 2;
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x000FEE34 File Offset: 0x000FD034
	private void prepare(string pID, string pColor, float pSize = 0.25f)
	{
		this.text_mesh.color = Color.white;
		this.cur_string = pID;
		this.cur_color = "<color=" + pColor + ">";
		this.text_mesh_bg_clone.characterSize = pSize;
		this.text_mesh.characterSize = pSize;
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000FEE88 File Offset: 0x000FD088
	private void add(string pTitle, object pText)
	{
		this.cur_string = string.Concat(new string[]
		{
			this.cur_string,
			pTitle,
			": ",
			this.cur_color,
			(pText != null) ? pText.ToString() : null,
			"</color>\n"
		});
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000FEEDB File Offset: 0x000FD0DB
	public void setTextFmodSound(DebugMusicBoxData pData)
	{
		this.setTextFmodSound(pData, Color.white);
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000FEEEC File Offset: 0x000FD0EC
	public void setTextFmodSound(DebugMusicBoxData pData, Color pColor)
	{
		float tSize = pData.timer / 3f;
		this.prepare("#fmod\n", this._color_sounds, 0.5f);
		this.cur_string = "mb:" + pData.path;
		Color tColor = pColor;
		tColor.a = tSize;
		this.fin();
		this.text_mesh.color = tColor;
		this.text_mesh_bg_clone.color = tColor;
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000FEF5C File Offset: 0x000FD15C
	public void setTextFmodSound(EventInstance pInstance)
	{
		EventDescription eventDescription;
		pInstance.getDescription(out eventDescription);
		string tPath;
		eventDescription.getPath(out tPath);
		this.prepare("#fmod\n", this._color_sounds_attached, 0.5f);
		this.add("name", tPath);
		this.fin();
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000FEFA8 File Offset: 0x000FD1A8
	public void setTextZone(TileZone pZone)
	{
		this.prepare("#zone\n", this._color_actors, 0.5f);
		foreach (ValueTuple<string, int> tTuple in pZone.debug_args)
		{
			this.add(tTuple.Item1, tTuple.Item2);
		}
		this.fin();
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x000FF008 File Offset: 0x000FD208
	public void setTextBoat(Actor pActor)
	{
		Boat tBoat = pActor.getSimpleComponent<Boat>();
		TaxiRequest tRequest = tBoat.taxi_request;
		if (tBoat.hasPassengers() || tRequest != null)
		{
			this.prepare("#boat\n", this._color_kingdom, 0.8f);
		}
		else
		{
			this.prepare("#boat\n", this._color_actors, 0.4f);
		}
		if (pActor.ai.job != null)
		{
			this.add("job", string.Concat(new string[]
			{
				pActor.ai.job.id,
				"(",
				pActor.ai.task_index.ToString(),
				"/",
				pActor.ai.job.tasks.Count.ToString(),
				")"
			}));
		}
		if (pActor.hasTask())
		{
			string[] array = new string[5];
			array[0] = " [";
			array[1] = pActor.ai.action_index.ToString();
			array[2] = "/";
			int num = 3;
			BehaviourTaskActor task = pActor.ai.task;
			array[num] = ((task != null) ? new int?(task.list.Count) : null).ToString();
			array[4] = "]";
			string tActionIndex = string.Concat(array);
			this.add("task", pActor.ai.task.id + " " + tActionIndex);
			BehaviourActionActor action = pActor.ai.action;
			string tAction = (action != null) ? action.GetType().ToString() : null;
			if (tAction != null)
			{
				tAction = tAction.Replace("ai.behaviours.", "");
			}
			this.add("action", tAction);
		}
		this.add("timer", tBoat.actor.timer_action);
		this.fin();
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x000FF1E4 File Offset: 0x000FD3E4
	private void debugForce(Actor pActor)
	{
		this.add("force xy", pActor.velocity.x.ToString() + "-" + pActor.velocity.y.ToString());
		this.add("force z", pActor.velocity.z);
		this.add("zPosition", pActor.position_height);
		this.add("force_speed", pActor.velocity_speed);
		this.add("under_force", pActor.under_forces);
		this.add("mass", pActor.stats["mass"]);
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x000FF2A4 File Offset: 0x000FD4A4
	public void setTextActor(Actor pActor)
	{
		this.prepare("#unit\n", this._color_actors, 0.2f);
		this.add("name", pActor.data.name);
		this.add("timer_action", pActor.timer_action);
		if (pActor.isCarryingResources())
		{
			this.add("inv.count", pActor.inventory.countResources());
			this.add("inv.render", pActor.inventory.getItemIDToRender());
		}
		this.add("stats", pActor.asset.id);
		this.add("id", pActor.data.id);
		this.add("alive", pActor.isAlive());
		this.add("health", pActor.getHealth().ToString() + "/" + pActor.getMaxHealth().ToString());
		this.add("traits", pActor.countTraits());
		if (pActor.hasAnyStatusEffect())
		{
			this.add("statuses", pActor.countStatusEffects());
		}
		if (pActor.ai.job != null)
		{
			this.add("job", string.Concat(new string[]
			{
				pActor.ai.job.id,
				"(",
				pActor.ai.task_index.ToString(),
				"/",
				pActor.ai.job.tasks.Count.ToString(),
				")"
			}));
		}
		if (pActor.hasTask())
		{
			this.add("task", pActor.ai.task.id);
			BehaviourActionActor action = pActor.ai.action;
			string tAction = (action != null) ? action.GetType().ToString() : null;
			if (tAction != null)
			{
				tAction = tAction.Replace("ai.behaviours.", "");
			}
			string str = tAction;
			string str2 = pActor.ai.action_index.ToString();
			string str3 = "/";
			BehaviourTaskActor task = pActor.ai.task;
			tAction = str + str2 + str3 + ((task != null) ? new int?(task.list.Count) : null).ToString();
			this.add("action", tAction);
		}
		this.fin();
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x000FF518 File Offset: 0x000FD718
	public void setTextArmy(Army pArmy)
	{
		this.prepare("#army\n", this._color_building, 0.3f);
		this.add("captain", pArmy.getCaptain().getName());
		this.add("id", pArmy.id);
		this.add("units", pArmy.countUnits());
		this.add("alive", pArmy.isAlive());
		if (pArmy.getCity().isAlive())
		{
			this.add("city", pArmy.getCity().name);
		}
		else
		{
			this.add("city", "DESTROYED, SHOULD BE NULL");
		}
		this.fin();
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x000FF5D0 File Offset: 0x000FD7D0
	public void setTextBuilding(Building pObj)
	{
		this.prepare("#build\n", this._color_building, 0.3f);
		this.add("objectID", pObj.data.id);
		this.add("state", pObj.data.state);
		this.add("animationState", pObj.animation_state);
		this.add("ownership", pObj.state_ownership);
		this.add("kingdom", pObj.kingdom.id);
		if (pObj.asset.hasHousingSlots())
		{
			this.add("housing", pObj.countResidents().ToString() + "/" + pObj.asset.housing_slots.ToString());
		}
		this.fin();
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x000FF6B8 File Offset: 0x000FD8B8
	public void setTextCity(City pObj)
	{
		this.prepare("#city\n", this._color_city, 1.5f);
		bool tError = false;
		string tErrorWrongID = "";
		foreach (string tDictID in pObj.buildings_dict_id.Keys)
		{
			if (tError)
			{
				break;
			}
			foreach (Building tB in pObj.buildings_dict_id[tDictID])
			{
				if (!tB.isAlive())
				{
					tError = true;
					tErrorWrongID += "dead,";
				}
				if (tB.asset.id != tDictID)
				{
					tError = true;
					tErrorWrongID = tErrorWrongID + "wrong stats " + tB.asset.id;
				}
				if (tError)
				{
					break;
				}
			}
		}
		int tCountFiremen = 0;
		using (List<Actor>.Enumerator enumerator3 = pObj.units.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				if (enumerator3.Current.isTask("put_out_fire"))
				{
					tCountFiremen++;
				}
			}
		}
		this.add("on_fire", pObj.isCityUnderDangerFire());
		this.add("danger", pObj.isInDanger());
		this.add("firemen", tCountFiremen);
		this.add("total", pObj.status.population.ToString() + "/" + pObj.getPopulationMaximum().ToString());
		this.add("units", pObj.units.Count);
		this.add("buildings", pObj.buildings.Count);
		this.add("orders_psbl", pObj._debug_last_possible_build_orders);
		this.add("orders_no_res", pObj._debug_last_possible_build_orders_no_resources);
		this.add("order_last", pObj._debug_last_build_order_try);
		this.add("house_zone_limit", pObj.getHouseCurrent().ToString() + "/" + pObj.getHouseLimit().ToString());
		if (pObj.ai.job != null)
		{
			this.add("job", string.Concat(new string[]
			{
				pObj.ai.job.id,
				"(",
				pObj.ai.task_index.ToString(),
				"/",
				pObj.ai.job.tasks.Count.ToString(),
				")"
			}));
		}
		if (pObj.ai.task != null)
		{
			this.add("task", pObj.ai.task.id);
		}
		else
		{
			this.add("task", "-");
		}
		if (tError)
		{
			this.add("ERROR", tErrorWrongID);
		}
		this.fin();
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x000FF9F0 File Offset: 0x000FDBF0
	public void setTextCityTasks(City pCity)
	{
		this.prepare("#city_tasks\n", this._color_city, 0.5f);
		this.add("trees:", pCity.tasks.trees);
		this.add("stone:", pCity.tasks.minerals);
		this.add("minerals:", pCity.tasks.minerals);
		this.add("bushes:", pCity.tasks.bushes);
		this.add("plants:", pCity.tasks.plants);
		this.add("hives:", pCity.tasks.hives);
		this.add("farm_fields:", pCity.tasks.farm_fields);
		this.add("wheats:", pCity.tasks.wheats);
		this.add("ruins:", pCity.tasks.ruins);
		this.add("poops:", pCity.tasks.poops);
		this.add("roads:", pCity.tasks.roads);
		this.add("fire:", pCity.tasks.fire);
		this.add("", "");
		int tTotal = 0;
		int tTotalOcuppied = 0;
		foreach (CitizenJobAsset tAsset in pCity.jobs.jobs.Keys)
		{
			int tCount = pCity.jobs.jobs[tAsset];
			int tOccupied = 0;
			if (pCity.jobs.occupied.ContainsKey(tAsset))
			{
				tOccupied = pCity.jobs.occupied[tAsset];
			}
			tTotal += tCount;
			tTotalOcuppied += tOccupied;
			this.add(tAsset.id + ":", tOccupied.ToString() + "/" + tCount.ToString());
		}
		foreach (CitizenJobAsset tAsset2 in pCity.jobs.occupied.Keys)
		{
			if (!pCity.jobs.jobs.ContainsKey(tAsset2))
			{
				int tOccupied2 = pCity.jobs.occupied[tAsset2];
				tTotalOcuppied += tOccupied2;
				this.add(tAsset2.id + ":", tOccupied2.ToString() + "/" + 0.ToString());
			}
		}
		int tTotalAdults = 0;
		int tTotalWorkers = 0;
		foreach (Actor tActor in pCity.units)
		{
			if (tActor.isAdult())
			{
				tTotalAdults++;
			}
			if (tActor.hasTask() && tActor.citizen_job != null)
			{
				tTotalWorkers++;
			}
		}
		this.add("total:", tTotalOcuppied.ToString() + "/" + tTotal.ToString());
		this.add("pop|adults|workers:", string.Concat(new string[]
		{
			pCity.units.Count.ToString(),
			" | ",
			tTotalAdults.ToString(),
			" | ",
			tTotalWorkers.ToString()
		}));
		this.fin();
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000FFDB8 File Offset: 0x000FDFB8
	public void setTextKingdom(Kingdom pObj)
	{
		this.prepare("#kingdom\n", this._color_kingdom, 2f);
		this.add("total", pObj.getPopulationPeople().ToString() + "/" + pObj.getPopulationTotalPossible().ToString());
		this.add("units", pObj.units.Count);
		this.add("buildings", pObj.buildings.Count);
		this.add("timer_action", pObj.timer_action);
		this.add("timer_new_king", pObj.data.timer_new_king);
		if (pObj.ai.job != null)
		{
			this.add("job", string.Concat(new string[]
			{
				pObj.ai.job.id,
				"(",
				pObj.ai.task_index.ToString(),
				"/",
				pObj.ai.job.tasks.Count.ToString(),
				")"
			}));
		}
		if (pObj.ai.task != null)
		{
			this.add("task", pObj.ai.task.id);
		}
		else
		{
			this.add("task", "-");
		}
		this.fin();
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x000FFF34 File Offset: 0x000FE134
	private void fin()
	{
		this.text_mesh.text = this.cur_string;
		this.text_mesh_bg_clone.text = this.cur_string;
	}

	// Token: 0x04001562 RID: 5474
	public TextMesh text_mesh;

	// Token: 0x04001563 RID: 5475
	public TextMesh text_mesh_bg_clone;

	// Token: 0x04001564 RID: 5476
	private string _color_sounds_attached = "#FF1F44";

	// Token: 0x04001565 RID: 5477
	private string _color_sounds = "#607BFF";

	// Token: 0x04001566 RID: 5478
	private string _color_actors = "#FF8F44";

	// Token: 0x04001567 RID: 5479
	private string _color_building = "#00FFFF";

	// Token: 0x04001568 RID: 5480
	private string _color_city = "#A0FF93";

	// Token: 0x04001569 RID: 5481
	private string _color_kingdom = "#FF4242";

	// Token: 0x0400156A RID: 5482
	private string cur_string;

	// Token: 0x0400156B RID: 5483
	private string cur_color;
}
