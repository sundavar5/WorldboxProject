using System;
using FMOD;
using FMOD.Studio;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class DebugTextGroupSystem : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x06001BE1 RID: 7137 RVA: 0x000FE294 File Offset: 0x000FC494
	public override void create()
	{
		base.create();
		base.transform.name = "Debug Text";
		GameObject tPrefab = (GameObject)Resources.Load("Prefabs/PrefabDebugText");
		this.prefab = tPrefab.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000FE2D3 File Offset: 0x000FC4D3
	protected override GroupSpriteObject createNew()
	{
		GroupSpriteObject groupSpriteObject = base.createNew();
		groupSpriteObject.GetComponent<DebugWorldText>().create();
		return groupSpriteObject;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000FE2E8 File Offset: 0x000FC4E8
	public override void update(float pElapsed)
	{
		base.prepare();
		this.checkSoundsAttached();
		this.checkSounds();
		this.checkSoundsPlaying();
		this.checkActors();
		this.checkBoats();
		this.checkBuildings();
		this.checkCitiesOverlay();
		this.checkCitiesTasksOverlay();
		this.checkKingdoms();
		this.checkArmies();
		this.checkZones();
		base.update(pElapsed);
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000FE344 File Offset: 0x000FC544
	private void checkSoundsPlaying()
	{
		if (!DebugConfig.isOn(DebugOption.OverlaySoundsActive))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		foreach (DebugMusicBoxData tData in MusicBox.inst.debug_box.list)
		{
			if (tData.isPlaying())
			{
				GroupSpriteObject next = base.getNext();
				this._pos.x = tData.x;
				this._pos.y = tData.y;
				next.GetComponent<DebugWorldText>().setTextFmodSound(tData, Color.green);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000FE3FC File Offset: 0x000FC5FC
	private void checkSounds()
	{
		if (!DebugConfig.isOn(DebugOption.OverlaySounds))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		foreach (DebugMusicBoxData tData in MusicBox.inst.debug_box.list)
		{
			GroupSpriteObject next = base.getNext();
			this._pos.x = tData.x;
			this._pos.y = tData.y;
			next.GetComponent<DebugWorldText>().setTextFmodSound(tData);
			next.setPosOnly(ref this._pos);
		}
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000FE4A8 File Offset: 0x000FC6A8
	private void checkSoundsAttached()
	{
		if (!DebugConfig.isOn(DebugOption.OverlaySoundsAttached))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		foreach (EventInstance tInstance in MusicBox.inst.idle.currentAttachedSounds.Values)
		{
			GroupSpriteObject next = base.getNext();
			ATTRIBUTES_3D tAttributes;
			tInstance.get3DAttributes(out tAttributes);
			this._pos.x = tAttributes.position.x;
			this._pos.y = tAttributes.position.y;
			next.GetComponent<DebugWorldText>().setTextFmodSound(tInstance);
			next.setPosOnly(ref this._pos);
		}
		foreach (QuantumSpriteAsset quantumSpriteAsset in AssetManager.quantum_sprites.list)
		{
			int tActive = quantumSpriteAsset.group_system.countActive();
			QuantumSprite[] tQSprites = quantumSpriteAsset.group_system.getAll();
			for (int i = 0; i < tActive; i++)
			{
				QuantumSprite tSprite = tQSprites[i];
				if (tSprite.fmod_instance.isValid())
				{
					ATTRIBUTES_3D tAttributes2;
					tSprite.fmod_instance.get3DAttributes(out tAttributes2);
					this._pos.x = tAttributes2.position.x;
					this._pos.y = tAttributes2.position.y;
					GroupSpriteObject next2 = base.getNext();
					next2.GetComponent<DebugWorldText>().setTextFmodSound(tSprite.fmod_instance);
					next2.setPosOnly(ref this._pos);
				}
			}
		}
		Actor[] tArr = World.world.units.visible_units.array;
		int tLen = World.world.units.visible_units.count;
		for (int j = 0; j < tLen; j++)
		{
			Actor tActor = tArr[j];
			if (tActor.idle_loop_sound != null && tActor.idle_loop_sound.fmod_instance.isValid())
			{
				ATTRIBUTES_3D tAttributes3;
				tActor.idle_loop_sound.fmod_instance.get3DAttributes(out tAttributes3);
				this._pos.x = tAttributes3.position.x;
				this._pos.y = tAttributes3.position.y;
				GroupSpriteObject next3 = base.getNext();
				next3.GetComponent<DebugWorldText>().setTextFmodSound(tActor.idle_loop_sound.fmod_instance);
				next3.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000FE720 File Offset: 0x000FC920
	private void checkBoats()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayBoatTransport))
		{
			return;
		}
		foreach (Actor tActor in World.world.units)
		{
			bool tShow = false;
			if (tActor.asset.is_boat)
			{
				tShow = true;
			}
			if (tShow)
			{
				GroupSpriteObject next = base.getNext();
				this._pos.x = tActor.current_position.x;
				this._pos.y = tActor.current_position.y;
				next.GetComponent<DebugWorldText>().setTextBoat(tActor);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000FE7D4 File Offset: 0x000FC9D4
	private void checkActors()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayActorCivs) && !DebugConfig.isOn(DebugOption.OverlayCursorActor) && !DebugConfig.isOn(DebugOption.OverlayActorGroupLeaderOnly) && !DebugConfig.isOn(DebugOption.OverlayActorFavoritesOnly) && !DebugConfig.isOn(DebugOption.OverlayActorMobs))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		Actor[] tArr = World.world.units.visible_units.array;
		int tLen = World.world.units.visible_units.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			bool tShow = false;
			if (DebugConfig.isOn(DebugOption.OverlayCursorActor) && UnitSelectionEffect.last_actor == tActor)
			{
				tShow = true;
			}
			if (DebugConfig.isOn(DebugOption.OverlayActorFavoritesOnly) && tActor.isFavorite())
			{
				tShow = true;
			}
			if (DebugConfig.isOn(DebugOption.OverlayActorGroupLeaderOnly) && tActor.is_army_captain)
			{
				tShow = true;
			}
			if (tActor.isSapient() && DebugConfig.isOn(DebugOption.OverlayActorCivs))
			{
				tShow = true;
			}
			if (!tActor.isSapient() && DebugConfig.isOn(DebugOption.OverlayActorMobs))
			{
				tShow = true;
			}
			if (tShow)
			{
				GroupSpriteObject next = base.getNext();
				this._pos.x = tActor.current_position.x;
				this._pos.y = tActor.current_position.y;
				next.GetComponent<DebugWorldText>().setTextActor(tActor);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000FE92C File Offset: 0x000FCB2C
	private void checkBuildings()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayTrees) && !DebugConfig.isOn(DebugOption.OverlayPlants) && !DebugConfig.isOn(DebugOption.OverlayCivBuildings) && !DebugConfig.isOn(DebugOption.OverlayOtherBuildings))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		int tLen = World.world.buildings.countVisibleBuildings();
		Building[] tBuildings = World.world.buildings.getVisibleBuildings();
		int i = 0;
		while (i < tLen)
		{
			Building tObj = tBuildings[i];
			if (tObj.asset.city_building)
			{
				if (DebugConfig.isOn(DebugOption.OverlayCivBuildings))
				{
					goto IL_C9;
				}
			}
			else if (tObj.asset.building_type == BuildingType.Building_Tree)
			{
				if (DebugConfig.isOn(DebugOption.OverlayTrees))
				{
					goto IL_C9;
				}
			}
			else if (tObj.asset.building_type == BuildingType.Building_Plant)
			{
				if (DebugConfig.isOn(DebugOption.OverlayPlants))
				{
					goto IL_C9;
				}
			}
			else if (DebugConfig.isOn(DebugOption.OverlayOtherBuildings))
			{
				goto IL_C9;
			}
			IL_112:
			i++;
			continue;
			IL_C9:
			GroupSpriteObject next = base.getNext();
			this._pos.x = tObj.current_position.x;
			this._pos.y = tObj.current_position.y;
			next.GetComponent<DebugWorldText>().setTextBuilding(tObj);
			next.setPosOnly(ref this._pos);
			goto IL_112;
		}
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000FEA58 File Offset: 0x000FCC58
	private void checkZones()
	{
		if (!DebugConfig.isOn(DebugOption.DebugZones))
		{
			return;
		}
		foreach (TileZone tZone in World.world.zone_calculator.zones)
		{
			if (tZone.debug_show)
			{
				GroupSpriteObject next = base.getNext();
				this._pos.x = (float)tZone.centerTile.pos.x;
				this._pos.y = (float)tZone.centerTile.pos.y;
				next.GetComponent<DebugWorldText>().setTextZone(tZone);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x000FEB1C File Offset: 0x000FCD1C
	private void checkArmies()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayArmies))
		{
			return;
		}
		if (MapBox.isRenderMiniMap())
		{
			return;
		}
		foreach (Army tArmy in World.world.armies)
		{
			if (tArmy.hasCaptain())
			{
				Actor tCaptain = tArmy.getCaptain();
				GroupSpriteObject next = base.getNext();
				this._pos.x = tCaptain.current_position.x;
				this._pos.y = tCaptain.current_position.y;
				next.GetComponent<DebugWorldText>().setTextArmy(tArmy);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000FEBD4 File Offset: 0x000FCDD4
	private void checkCitiesOverlay()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayCity))
		{
			return;
		}
		foreach (City tCity in World.world.cities)
		{
			GroupSpriteObject next = base.getNext();
			this._pos.x = tCity.city_center.x;
			this._pos.y = tCity.city_center.y;
			next.GetComponent<DebugWorldText>().setTextCity(tCity);
			next.setPosOnly(ref this._pos);
		}
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x000FEC74 File Offset: 0x000FCE74
	private void checkCitiesTasksOverlay()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayCityTasks))
		{
			return;
		}
		foreach (City tCity in World.world.cities)
		{
			GroupSpriteObject next = base.getNext();
			this._pos.x = tCity.city_center.x;
			this._pos.y = tCity.city_center.y;
			next.GetComponent<DebugWorldText>().setTextCityTasks(tCity);
			next.setPosOnly(ref this._pos);
		}
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x000FED14 File Offset: 0x000FCF14
	private void checkKingdoms()
	{
		if (!DebugConfig.isOn(DebugOption.OverlayKingdom))
		{
			return;
		}
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.hasCapital())
			{
				GroupSpriteObject next = base.getNext();
				this._pos.x = tKingdom.capital.city_center.x;
				this._pos.y = tKingdom.capital.city_center.y;
				next.GetComponent<DebugWorldText>().setTextKingdom(tKingdom);
				next.setPosOnly(ref this._pos);
			}
		}
	}

	// Token: 0x04001561 RID: 5473
	private Vector2 _pos;
}
