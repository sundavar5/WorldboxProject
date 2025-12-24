using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000584 RID: 1412
[Serializable]
public class StatisticsAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x06002EC5 RID: 11973 RVA: 0x00167554 File Offset: 0x00165754
	public Sprite getIcon()
	{
		if (this._icon == null && !string.IsNullOrEmpty(this.path_icon))
		{
			this._icon = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._icon;
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x00167588 File Offset: 0x00165788
	public string getLocaleID()
	{
		return this.localized_key.Underscore() ?? this.id;
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x001675A0 File Offset: 0x001657A0
	public string getDescriptionID()
	{
		string tDescriptionID = this.getLocaleID() + "_description";
		if (!string.IsNullOrEmpty(this.localized_key_description))
		{
			tDescriptionID = this.localized_key_description;
		}
		if (LocalizedTextManager.stringExists(tDescriptionID))
		{
			return tDescriptionID;
		}
		return this.getLocaleID() + "_description";
	}

	// Token: 0x040022EA RID: 8938
	public string localized_key;

	// Token: 0x040022EB RID: 8939
	public string localized_key_description;

	// Token: 0x040022EC RID: 8940
	public LocaleGetter locale_getter;

	// Token: 0x040022ED RID: 8941
	public int rarity;

	// Token: 0x040022EE RID: 8942
	[DefaultValue("#Status_stat")]
	public string steam_activity = "#Status_stat";

	// Token: 0x040022EF RID: 8943
	public StatisticsStringAction string_action = delegate(StatisticsAsset pAsset)
	{
		if (pAsset.long_action != null)
		{
			return pAsset.long_action(pAsset).ToText();
		}
		return null;
	};

	// Token: 0x040022F0 RID: 8944
	public StatisticsLongAction long_action;

	// Token: 0x040022F1 RID: 8945
	public MetaIdGetter get_meta_id;

	// Token: 0x040022F2 RID: 8946
	[NonSerialized]
	public string last_value = string.Empty;

	// Token: 0x040022F3 RID: 8947
	public bool is_world_statistics;

	// Token: 0x040022F4 RID: 8948
	public bool is_game_statistics;

	// Token: 0x040022F5 RID: 8949
	public WorldStatsTabs world_stats_tabs;

	// Token: 0x040022F6 RID: 8950
	[DefaultValue(MetaType.None)]
	public MetaType world_stats_meta_type;

	// Token: 0x040022F7 RID: 8951
	[DefaultValue(MetaType.None)]
	public MetaType list_window_meta_type;

	// Token: 0x040022F8 RID: 8952
	public string path_icon;

	// Token: 0x040022F9 RID: 8953
	private Sprite _icon;
}
