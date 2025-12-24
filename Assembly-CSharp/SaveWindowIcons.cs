using System;
using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000596 RID: 1430
public class SaveWindowIcons : MonoBehaviour
{
	// Token: 0x06002FD0 RID: 12240 RVA: 0x0017272A File Offset: 0x0017092A
	public void Awake()
	{
		this._name_input.addListener(new UnityAction<string>(this.applyInputName));
		this._description_input.addListener(new UnityAction<string>(this.applyInputDescription));
	}

	// Token: 0x06002FD1 RID: 12241 RVA: 0x0017275C File Offset: 0x0017095C
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this._name_input.gameObject.SetActive(this._allow_edit);
		this._description_input.gameObject.SetActive(this._allow_edit);
		if (this._use_current_world_info)
		{
			SavedMap tMap = SaveManager.currentWorldToSavedMap();
			this.metaData = tMap.getMeta();
		}
		else if (SaveManager.currentWorkshopMapData != null)
		{
			this.metaData = SaveManager.currentWorkshopMapData.meta_data_map;
		}
		else
		{
			this.metaData = SaveManager.getCurrentMeta();
			this._save_path = SaveManager.currentSavePath;
		}
		if (this.metaData != null)
		{
			this.checkRaceIcons(this.metaData);
			MapStats tMapStats = this.metaData.mapStats;
			if (this._allow_edit)
			{
				if (!this._clear_name_on_load)
				{
					this._name_input.setText(tMapStats.name);
				}
				else
				{
					this._name_input.setText("");
				}
				this._description_input.setText(tMapStats.description);
			}
			MapSizeAsset tMapSizePreset = MapSizeLibrary.getPresetAsset(this.metaData.width);
			if (tMapSizePreset != null)
			{
				this._text_map_size.GetComponent<LocalizedText>().setKeyAndUpdate(tMapSizePreset.getLocaleID());
			}
			else
			{
				this._text_map_size.text = this.metaData.width.ToString() + "x" + this.metaData.height.ToString();
			}
			this._modded_icon.SetActive(this.metaData.modded);
			bool tCursed = this.metaData.cursed;
			this._cursed_icon.SetActive(tCursed);
			this._map_background_cursed.SetActive(tCursed);
			this._map_overlay_cursed.SetActive(tCursed);
			this._map_background_normal.SetActive(!tCursed);
			this._map_age.setValue((float)Date.getYear(tMapStats.world_time));
			this._population.setValue((float)this.metaData.population);
			this._mobs.setValue((float)this.metaData.mobs);
			this._vegetation.setValue((float)this.metaData.vegetation);
			this._deaths.setValue((float)this.metaData.deaths);
			this._kingdoms.setValue((float)this.metaData.kingdoms);
			this._cities.setValue((float)this.metaData.cities);
			this._buildings.setValue((float)this.metaData.buildings);
			this._equipment.setValue((float)this.metaData.equipment);
			this._books.setValue((float)this.metaData.books);
			this._wars.setValue((float)this.metaData.wars);
			this._alliances.setValue((float)this.metaData.alliances);
			this._families.setValue((float)this.metaData.families);
			this._clans.setValue((float)this.metaData.clans);
			this._cultures.setValue((float)this.metaData.cultures);
			this._religions.setValue((float)this.metaData.religions);
			this._languages.setValue((float)this.metaData.languages);
			this._subspecies.setValue((float)this.metaData.subspecies);
			this._favorites.setValue((float)this.metaData.favorites);
			this._favorite_items.setValue((float)this.metaData.favorite_items);
			this._map_name.text = tMapStats.name;
			this._map_name.color = tMapStats.getArchitectMood().getColorText();
			this._name_input.textField.color = tMapStats.getArchitectMood().getColorText();
			this._text_description.text = tMapStats.description;
			if (SaveManager.currentWorkshopMapData != null)
			{
				Steamworks.Ugc.Item tItem = SaveManager.currentWorkshopMapData.workshop_item;
				if (tItem.Owner.Id.ToString() == Config.steam_id)
				{
					this._map_name.color = Toolbox.makeColor("#3DDEFF");
					return;
				}
				this._map_name.color = Toolbox.makeColor("#FF9B1C");
				return;
			}
		}
		else
		{
			this._map_name.GetComponent<LocalizedText>().updateText(true);
		}
	}

	// Token: 0x06002FD2 RID: 12242 RVA: 0x00172B9C File Offset: 0x00170D9C
	private void applyInputName(string pInput)
	{
		if (string.IsNullOrEmpty(pInput))
		{
			return;
		}
		if (this._save_names_to_current_world)
		{
			World.world.map_stats.name = pInput;
		}
		else
		{
			this.metaData.mapStats.name = pInput;
		}
		if (this._save_meta_data_on_close && this.metaData != null)
		{
			SaveManager.saveMetaData(this.metaData, this._save_path);
		}
	}

	// Token: 0x06002FD3 RID: 12243 RVA: 0x00172C00 File Offset: 0x00170E00
	private void applyInputDescription(string pInput)
	{
		if (pInput == null)
		{
			return;
		}
		if (this._save_names_to_current_world)
		{
			if (World.world == null || World.world.map_stats == null)
			{
				return;
			}
			World.world.map_stats.description = pInput;
		}
		else
		{
			if (this.metaData == null || this.metaData.mapStats == null)
			{
				return;
			}
			this.metaData.mapStats.description = pInput;
		}
		if (this._save_meta_data_on_close && this.metaData != null)
		{
			SaveManager.saveMetaData(this.metaData, this._save_path);
		}
	}

	// Token: 0x06002FD4 RID: 12244 RVA: 0x00172C8D File Offset: 0x00170E8D
	private void OnDisable()
	{
		if (this._save_meta_data_on_close && this.metaData != null)
		{
			SaveManager.saveMetaData(this.metaData, this._save_path);
		}
	}

	// Token: 0x06002FD5 RID: 12245 RVA: 0x00172CB0 File Offset: 0x00170EB0
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this._save_meta_data_on_close && this.metaData != null)
		{
			this.metaData.mapStats.name = this._name_input.textField.text;
			this.metaData.mapStats.description = this._description_input.textField.text;
			SaveManager.saveMetaData(this.metaData, this._save_path);
		}
	}

	// Token: 0x06002FD6 RID: 12246 RVA: 0x00172D20 File Offset: 0x00170F20
	private void checkRaceIcons(MapMetaData pData)
	{
		this._icon_orc.gameObject.SetActive(false);
		this._icon_human.gameObject.SetActive(false);
		this._icon_elf.gameObject.SetActive(false);
		this._icon_dwarf.gameObject.SetActive(false);
	}

	// Token: 0x040023B4 RID: 9140
	[SerializeField]
	private bool _use_current_world_info;

	// Token: 0x040023B5 RID: 9141
	[SerializeField]
	private bool _allow_edit;

	// Token: 0x040023B6 RID: 9142
	[SerializeField]
	private bool _save_meta_data_on_close;

	// Token: 0x040023B7 RID: 9143
	[SerializeField]
	private bool _save_names_to_current_world;

	// Token: 0x040023B8 RID: 9144
	[SerializeField]
	private bool _clear_name_on_load;

	// Token: 0x040023B9 RID: 9145
	[SerializeField]
	private GameObject _icon_orc;

	// Token: 0x040023BA RID: 9146
	[SerializeField]
	private GameObject _icon_human;

	// Token: 0x040023BB RID: 9147
	[SerializeField]
	private GameObject _icon_elf;

	// Token: 0x040023BC RID: 9148
	[SerializeField]
	private GameObject _icon_dwarf;

	// Token: 0x040023BD RID: 9149
	[SerializeField]
	private Text _text_map_size;

	// Token: 0x040023BE RID: 9150
	[SerializeField]
	private StatsIcon _map_age;

	// Token: 0x040023BF RID: 9151
	[SerializeField]
	private StatsIcon _population;

	// Token: 0x040023C0 RID: 9152
	[SerializeField]
	private StatsIcon _mobs;

	// Token: 0x040023C1 RID: 9153
	[SerializeField]
	private StatsIcon _vegetation;

	// Token: 0x040023C2 RID: 9154
	[SerializeField]
	private StatsIcon _deaths;

	// Token: 0x040023C3 RID: 9155
	[SerializeField]
	private StatsIcon _kingdoms;

	// Token: 0x040023C4 RID: 9156
	[SerializeField]
	private StatsIcon _cities;

	// Token: 0x040023C5 RID: 9157
	[SerializeField]
	private StatsIcon _buildings;

	// Token: 0x040023C6 RID: 9158
	[SerializeField]
	private StatsIcon _equipment;

	// Token: 0x040023C7 RID: 9159
	[SerializeField]
	private StatsIcon _books;

	// Token: 0x040023C8 RID: 9160
	[SerializeField]
	private StatsIcon _wars;

	// Token: 0x040023C9 RID: 9161
	[SerializeField]
	private StatsIcon _alliances;

	// Token: 0x040023CA RID: 9162
	[SerializeField]
	private StatsIcon _families;

	// Token: 0x040023CB RID: 9163
	[SerializeField]
	private StatsIcon _clans;

	// Token: 0x040023CC RID: 9164
	[SerializeField]
	private StatsIcon _cultures;

	// Token: 0x040023CD RID: 9165
	[SerializeField]
	private StatsIcon _religions;

	// Token: 0x040023CE RID: 9166
	[SerializeField]
	private StatsIcon _languages;

	// Token: 0x040023CF RID: 9167
	[SerializeField]
	private StatsIcon _subspecies;

	// Token: 0x040023D0 RID: 9168
	[SerializeField]
	private StatsIcon _favorites;

	// Token: 0x040023D1 RID: 9169
	[SerializeField]
	private StatsIcon _favorite_items;

	// Token: 0x040023D2 RID: 9170
	[SerializeField]
	private GameObject _map_background_normal;

	// Token: 0x040023D3 RID: 9171
	[SerializeField]
	private GameObject _map_background_cursed;

	// Token: 0x040023D4 RID: 9172
	[SerializeField]
	private GameObject _map_overlay_cursed;

	// Token: 0x040023D5 RID: 9173
	[SerializeField]
	private GameObject _modded_icon;

	// Token: 0x040023D6 RID: 9174
	[SerializeField]
	private GameObject _cursed_icon;

	// Token: 0x040023D7 RID: 9175
	[SerializeField]
	private Text _map_name;

	// Token: 0x040023D8 RID: 9176
	[SerializeField]
	private Text _text_description;

	// Token: 0x040023D9 RID: 9177
	[SerializeField]
	private NameInput _name_input;

	// Token: 0x040023DA RID: 9178
	[SerializeField]
	private NameInput _description_input;

	// Token: 0x040023DB RID: 9179
	private string _save_path;

	// Token: 0x040023DC RID: 9180
	private MapMetaData metaData;
}
