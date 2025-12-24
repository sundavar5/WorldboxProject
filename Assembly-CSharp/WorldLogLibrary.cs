using System;

// Token: 0x020001BA RID: 442
public class WorldLogLibrary : AssetLibrary<WorldLogAsset>
{
	// Token: 0x06000CCC RID: 3276 RVA: 0x000B9344 File Offset: 0x000B7544
	private void updateText(ref string pText, WorldLogMessage pMessage, string pKey, int pSpecialId)
	{
		string tSpecialText = pMessage.getSpecial(pSpecialId);
		pText = pText.Replace(pKey, tSpecialText);
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x000B9368 File Offset: 0x000B7568
	public override void init()
	{
		base.init();
		WorldLogLibrary.king_new = this.add(new WorldLogAsset
		{
			id = "king_new",
			group = "kings",
			path_icon = "ui/Icons/iconKings",
			color = Toolbox.color_log_neutral,
			text_replacer = new WorldLogTextFormatter(this.kingReplacer)
		});
		WorldLogLibrary.king_left = this.add(new WorldLogAsset
		{
			id = "king_left",
			group = "kings",
			path_icon = "ui/Icons/actor_traits/iconStupid",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingReplacer)
		});
		WorldLogLibrary.king_fled_capital = this.add(new WorldLogAsset
		{
			id = "king_fled_capital",
			group = "kings",
			random_ids = 3,
			path_icon = "ui/Icons/actor_traits/iconStupid",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingCityReplacer)
		});
		WorldLogLibrary.king_fled_city = this.add(new WorldLogAsset
		{
			id = "king_fled_city",
			group = "kings",
			random_ids = 3,
			path_icon = "ui/Icons/actor_traits/iconStupid",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingCityReplacer)
		});
		WorldLogLibrary.king_dead = this.add(new WorldLogAsset
		{
			id = "king_dead",
			group = "kings",
			path_icon = "ui/Icons/iconDead",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingReplacer)
		});
		WorldLogLibrary.king_killed = this.add(new WorldLogAsset
		{
			id = "king_killed",
			group = "kings",
			random_ids = 3,
			path_icon = "ui/Icons/actor_traits/iconKingslayer",
			color = Toolbox.color_log_warning,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom$", 1);
				this.updateText(ref pText, pMessage, "$king$", 2);
				this.updateText(ref pText, pMessage, "$name$", 3);
			}
		});
		WorldLogLibrary.favorite_dead = this.add(new WorldLogAsset
		{
			id = "favorite_dead",
			group = "favorite_units",
			random_ids = 3,
			path_icon = "ui/Icons/iconFavoriteKilled",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.favorite_killed = this.add(new WorldLogAsset
		{
			id = "favorite_killed",
			group = "favorite_units",
			random_ids = 3,
			path_icon = "ui/Icons/iconFavoriteKilled",
			color = Toolbox.color_log_warning,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$name$", 1);
				this.updateText(ref pText, pMessage, "$killer$", 2);
			}
		});
		WorldLogLibrary.city_new = this.add(new WorldLogAsset
		{
			id = "city_new",
			group = "cities",
			path_icon = "ui/Icons/iconCitySelect",
			color = Toolbox.color_log_neutral,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.log_city_revolted = this.add(new WorldLogAsset
		{
			id = "log_city_revolted",
			group = "cities",
			path_icon = "ui/Icons/iconInspiration",
			color = Toolbox.color_log_good,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$name$", 1);
				this.updateText(ref pText, pMessage, "$kingdom$", 2);
			}
		});
		WorldLogLibrary.diplomacy_war_ended = this.add(new WorldLogAsset
		{
			id = "diplomacy_war_ended",
			group = "wars",
			path_icon = "ui/Icons/actor_traits/iconPacifist",
			color = Toolbox.color_log_good,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.diplomacy_war_started = this.add(new WorldLogAsset
		{
			id = "diplomacy_war_started",
			group = "wars",
			path_icon = "ui/Icons/iconWar",
			color = Toolbox.color_log_warning,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom1$", 1);
				this.updateText(ref pText, pMessage, "$kingdom2$", 2);
			}
		});
		WorldLogLibrary.total_war_started = this.add(new WorldLogAsset
		{
			id = "total_war_started",
			group = "wars",
			path_icon = "ui/Icons/iconWar",
			color = Toolbox.color_log_warning,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom$", 1);
			}
		});
		WorldLogLibrary.alliance_new = this.add(new WorldLogAsset
		{
			id = "alliance_new",
			path_icon = "ui/Icons/iconAlliance",
			color = Toolbox.color_log_neutral,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.alliance_dissolved = this.add(new WorldLogAsset
		{
			id = "alliance_dissolved",
			path_icon = "ui/Icons/iconAlliance",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.kingdom_new = this.add(new WorldLogAsset
		{
			id = "kingdom_new",
			group = "kingdoms",
			path_icon = "ui/Icons/iconKingdom",
			color = Toolbox.color_log_neutral,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.kingdom_destroyed = this.add(new WorldLogAsset
		{
			id = "kingdom_destroyed",
			group = "kingdoms",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogLibrary.kingdom_shattered = this.add(new WorldLogAsset
		{
			id = "kingdom_shattered",
			group = "kingdoms",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			random_ids = 3,
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingdomReplacer)
		});
		WorldLogLibrary.kingdom_fractured = this.add(new WorldLogAsset
		{
			id = "kingdom_fractured",
			group = "kingdoms",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			random_ids = 3,
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.kingdomReplacer)
		});
		WorldLogLibrary.kingdom_royal_clan_new = this.add(new WorldLogAsset
		{
			id = "kingdom_royal_clan_new",
			group = "clans",
			path_icon = "ui/Icons/iconClan",
			color = Toolbox.color_log_neutral,
			random_ids = 3,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom$", 1);
				this.updateText(ref pText, pMessage, "$clan$", 2);
				this.updateText(ref pText, pMessage, "$king$", 3);
			}
		});
		WorldLogLibrary.kingdom_royal_clan_changed = this.add(new WorldLogAsset
		{
			id = "kingdom_royal_clan_changed",
			group = "clans",
			path_icon = "ui/Icons/iconClan",
			color = Toolbox.color_log_neutral,
			random_ids = 3,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom$", 1);
				this.updateText(ref pText, pMessage, "$old_clan$", 2);
				this.updateText(ref pText, pMessage, "$new_clan$", 3);
			}
		});
		WorldLogLibrary.kingdom_royal_clan_dead = this.add(new WorldLogAsset
		{
			id = "kingdom_royal_clan_dead",
			group = "clans",
			path_icon = "ui/Icons/iconClan",
			color = Toolbox.color_log_warning,
			random_ids = 3,
			text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
			{
				this.updateText(ref pText, pMessage, "$kingdom$", 1);
				this.updateText(ref pText, pMessage, "$clan$", 2);
			}
		});
		WorldLogLibrary.city_destroyed = this.add(new WorldLogAsset
		{
			id = "city_destroyed",
			group = "cities",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			color = Toolbox.color_log_warning,
			text_replacer = new WorldLogTextFormatter(this.nameReplacer)
		});
		WorldLogAsset worldLogAsset = new WorldLogAsset();
		worldLogAsset.id = "auto_tester";
		worldLogAsset.path_icon = "ui/Icons/iconPlay";
		worldLogAsset.color = Toolbox.color_log_warning;
		worldLogAsset.text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
		{
			pText = pMessage.special1;
		};
		WorldLogLibrary.auto_tester = this.add(worldLogAsset);
		this.addDisasters();
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x000B9B0C File Offset: 0x000B7D0C
	private void addDisasters()
	{
		this.add(new WorldLogAsset
		{
			id = "$basic_disaster$",
			color = Toolbox.color_log_warning,
			group = "disasters"
		});
		this.clone("disaster_tornado", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_tornado";
		this.t.path_icon = "ui/Icons/iconTornado";
		this.clone("disaster_meteorite", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_meteorite";
		this.t.path_icon = "ui/Icons/iconMeteorite";
		this.clone("disaster_hellspawn", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_hellspawn";
		this.t.path_icon = "ui/Icons/iconDemon";
		this.clone("disaster_earthquake", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_earthquake";
		this.t.path_icon = "ui/Icons/iconEarthquake";
		this.clone("disaster_greg_abominations", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_greg_abominations";
		this.t.path_icon = "ui/Icons/iconGreg";
		this.clone("disaster_ice_ones", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_ice_ones";
		this.t.path_icon = "ui/Icons/iconWalker";
		this.clone("disaster_sudden_snowman", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_sudden_snowman";
		this.t.path_icon = "ui/Icons/iconSnowMan";
		this.clone("disaster_garden_surprise", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_garden_surprise";
		this.t.path_icon = "ui/Icons/iconSuperPumpkin";
		this.clone("disaster_dragon_from_farlands", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_dragon_from_farlands";
		this.t.path_icon = "ui/Icons/iconDragon";
		this.clone("disaster_bandits", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_bandits";
		this.t.path_icon = "ui/Icons/iconBandit";
		this.clone("disaster_alien_invasion", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_alien_invasion";
		this.t.path_icon = "ui/Icons/iconUfo";
		this.clone("disaster_biomass", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_biomass";
		this.t.path_icon = "ui/Icons/iconBiomass";
		this.clone("disaster_tumor", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_tumor";
		this.t.path_icon = "ui/Icons/iconTumor";
		this.clone("disaster_heatwave", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_heatwave";
		this.t.path_icon = "ui/Icons/iconFire";
		this.clone("disaster_evil_mage", "$basic_disaster$");
		this.t.locale_id = "worldlog_disaster_evil_mage";
		this.t.path_icon = "ui/Icons/iconEvilMage";
		this.t.text_replacer = new WorldLogTextFormatter(this.nameReplacer);
		this.clone("$city_name_disaster$", "$basic_disaster$");
		this.t.text_replacer = delegate(WorldLogMessage pMessage, ref string pText)
		{
			this.updateText(ref pText, pMessage, "$name$", 1);
			this.updateText(ref pText, pMessage, "$city$", 2);
		};
		this.clone("disaster_underground_necromancer", "$city_name_disaster$");
		this.t.locale_id = "worldlog_disaster_underground_necromancer";
		this.t.path_icon = "ui/Icons/iconNecromancer";
		this.clone("disaster_mad_thoughts", "$city_name_disaster$");
		this.t.locale_id = "worldlog_disaster_mad_thoughts";
		this.t.path_icon = "ui/Icons/actor_traits/iconMadness";
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x000B9EC6 File Offset: 0x000B80C6
	private void nameReplacer(WorldLogMessage pMessage, ref string pText)
	{
		this.updateText(ref pText, pMessage, "$name$", 1);
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x000B9ED6 File Offset: 0x000B80D6
	private void kingReplacer(WorldLogMessage pMessage, ref string pText)
	{
		this.updateText(ref pText, pMessage, "$kingdom$", 1);
		this.updateText(ref pText, pMessage, "$king$", 2);
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x000B9EF4 File Offset: 0x000B80F4
	private void kingCityReplacer(WorldLogMessage pMessage, ref string pText)
	{
		this.updateText(ref pText, pMessage, "$kingdom$", 1);
		this.updateText(ref pText, pMessage, "$king$", 2);
		this.updateText(ref pText, pMessage, "$city$", 3);
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x000B9F20 File Offset: 0x000B8120
	private void kingdomReplacer(WorldLogMessage pMessage, ref string pText)
	{
		this.updateText(ref pText, pMessage, "$kingdom$", 1);
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000B9F30 File Offset: 0x000B8130
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (WorldLogAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
	}

	// Token: 0x04000C61 RID: 3169
	public static WorldLogAsset king_new;

	// Token: 0x04000C62 RID: 3170
	public static WorldLogAsset king_left;

	// Token: 0x04000C63 RID: 3171
	public static WorldLogAsset king_fled_city;

	// Token: 0x04000C64 RID: 3172
	public static WorldLogAsset king_fled_capital;

	// Token: 0x04000C65 RID: 3173
	public static WorldLogAsset king_dead;

	// Token: 0x04000C66 RID: 3174
	public static WorldLogAsset king_killed;

	// Token: 0x04000C67 RID: 3175
	public static WorldLogAsset favorite_dead;

	// Token: 0x04000C68 RID: 3176
	public static WorldLogAsset favorite_killed;

	// Token: 0x04000C69 RID: 3177
	public static WorldLogAsset city_new;

	// Token: 0x04000C6A RID: 3178
	public static WorldLogAsset log_city_revolted;

	// Token: 0x04000C6B RID: 3179
	public static WorldLogAsset diplomacy_war_ended;

	// Token: 0x04000C6C RID: 3180
	public static WorldLogAsset diplomacy_war_started;

	// Token: 0x04000C6D RID: 3181
	public static WorldLogAsset total_war_started;

	// Token: 0x04000C6E RID: 3182
	public static WorldLogAsset alliance_new;

	// Token: 0x04000C6F RID: 3183
	public static WorldLogAsset alliance_dissolved;

	// Token: 0x04000C70 RID: 3184
	public static WorldLogAsset kingdom_new;

	// Token: 0x04000C71 RID: 3185
	public static WorldLogAsset kingdom_destroyed;

	// Token: 0x04000C72 RID: 3186
	public static WorldLogAsset kingdom_shattered;

	// Token: 0x04000C73 RID: 3187
	public static WorldLogAsset kingdom_fractured;

	// Token: 0x04000C74 RID: 3188
	public static WorldLogAsset city_destroyed;

	// Token: 0x04000C75 RID: 3189
	public static WorldLogAsset kingdom_royal_clan_new;

	// Token: 0x04000C76 RID: 3190
	public static WorldLogAsset kingdom_royal_clan_changed;

	// Token: 0x04000C77 RID: 3191
	public static WorldLogAsset kingdom_royal_clan_dead;

	// Token: 0x04000C78 RID: 3192
	public static WorldLogAsset auto_tester;
}
