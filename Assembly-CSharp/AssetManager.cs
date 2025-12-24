using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ai.behaviours;
using db;

// Token: 0x02000020 RID: 32
public class AssetManager
{
	// Token: 0x060001A6 RID: 422 RVA: 0x0000D946 File Offset: 0x0000BB46
	public static void clear()
	{
		AssetManager._instance = null;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000D94E File Offset: 0x0000BB4E
	public static void initMain()
	{
		if (AssetManager._instance != null)
		{
			return;
		}
		AssetManager._instance = new AssetManager();
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000D962 File Offset: 0x0000BB62
	public static void init()
	{
		AssetManager._instance.initLibs();
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000D970 File Offset: 0x0000BB70
	public AssetManager()
	{
		this._assetgv = Config.gv;
		this.add(AssetManager.game_language_library = new GameLanguageLibrary(), "game_languages");
		this.add(AssetManager.options_library = new OptionsLibrary(), "options_library");
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
	private void initLibs()
	{
		this.add(AssetManager.tile_tile_effects = new TileEffectsLibrary(), "tile_effects");
		this.add(AssetManager.base_stats_library = new BaseStatsLibrary(), "base_stats_library");
		this.add(AssetManager.world_log_library = new WorldLogLibrary(), "world_log_library");
		this.add(AssetManager.history_groups = new HistoryGroupLibrary(), "history_groups");
		this.add(AssetManager.decisions_library = new DecisionsLibrary(), "decisions_library");
		this.add(AssetManager.neural_layers = new NeuralLayerLibrary(), "neural_layer_library");
		this.add(AssetManager.graph_time_library = new GraphTimeLibrary(), "graph_time_library");
		this.add(AssetManager.history_data_library = new HistoryDataLibrary(), "history_data_library");
		this.add(AssetManager.history_meta_data_library = new HistoryMetaDataLibrary(), "history_meta_data_library");
		this.add(AssetManager.world_laws_library = new WorldLawLibrary(), "world_laws_library");
		this.add(AssetManager.world_law_groups = new WorldLawGroupLibrary(), "world_law_groups");
		this.add(AssetManager.meta_type_library = new MetaTypeLibrary(), "meta_type_library");
		this.add(AssetManager.meta_text_report_library = new MetaTextReportLibrary(), "meta_text_report_library");
		this.add(AssetManager.meta_customization_library = new MetaCustomizationLibrary(), "meta_customization_library");
		this.add(AssetManager.meta_representation_library = new MetaRepresentationLibrary(), "meta_representation_library");
		this.add(AssetManager.culture_banners_library = new CultureBannerLibrary(), "culture_banners_library");
		this.add(AssetManager.kingdom_banners_library = new KingdomBannerLibrary(), "kingdom_banners_library");
		this.add(AssetManager.clan_banners_library = new ClanBannerLibrary(), "clan_banners_library");
		this.add(AssetManager.religion_banners_library = new ReligionBannerLibrary(), "religion_banners_library");
		this.add(AssetManager.language_banners_library = new LanguageBannerLibrary(), "language_banners_library");
		this.add(AssetManager.subspecies_banners_library = new SubspeciesBannerLibrary(), "subspecies_banners_library");
		this.add(AssetManager.family_banners_library = new FamilysBannerLibrary(), "family_banners_library");
		this.add(AssetManager.time_scales = new WorldTimeScaleLibrary(), "world_time_scales_library");
		this.add(AssetManager.communication_library = new CommunicationLibrary(), "communication_library");
		this.add(AssetManager.communication_topic_library = new CommunicationTopicLibrary(), "communication_topic_library");
		this.add(AssetManager.city_build_orders = new BuildOrderLibrary(), "city_build_orders");
		this.add(AssetManager.architecture_library = new ArchitectureLibrary(), "architecture_library");
		this.add(AssetManager.book_types = new BookTypeLibrary(), "book_types_library");
		this.add(AssetManager.nameplates_library = new NameplateLibrary(), "nameplates_library");
		this.add(AssetManager.combat_action_library = new CombatActionLibrary(), "combat_action_library");
		this.add(AssetManager.biome_library = new BiomeLibrary(), "biome_library");
		this.add(AssetManager.phenotype_library = new PhenotypeLibrary(), "phenotype_library");
		this.add(AssetManager.dynamic_sprites_library = new DynamicSpritesLibrary(), "dynamic_sprites_library");
		this.add(AssetManager.debug_tool_library = new DebugToolLibrary(), "debug_tool_library");
		this.add(AssetManager.brush_library = new BrushLibrary(), "brush_library");
		this.add(AssetManager.chromosome_type_library = new ChromosomeTypeLibrary(), "chromosome_type_library");
		this.add(AssetManager.gene_library = new GeneLibrary(), "gene_library");
		this.add(AssetManager.loyalty_library = new LoyaltyLibrary(), "loyalty_library");
		this.add(AssetManager.opinion_library = new OpinionLibrary(), "opinion_library");
		this.add(AssetManager.happiness_library = new HappinessLibrary(), "happiness_library");
		this.add(AssetManager.hotkey_library = new HotkeyLibrary(), "hotkey_library");
		this.add(AssetManager.tooltips = new TooltipLibrary(), "tooltips");
		this.add(AssetManager.war_types_library = new WarTypeLibrary(), "war_types_library");
		this.add(AssetManager.sim_globals_library = new SimGlobals(), "sim_globals_library");
		this.add(AssetManager.color_style_library = new ColorStyleLibrary(), "color_style_library");
		this.add(AssetManager.effects_library = new EffectsLibrary(), "effects_library");
		this.add(AssetManager.kingdom_colors_library = new KingdomColorsLibrary(), "kingdom_colors_library");
		this.add(AssetManager.clan_colors_library = new ClanColorsLibrary(), "clan_colors_library");
		this.add(AssetManager.subspecies_colors_library = new SubspeciesColorsLibrary(), "species_colors_library");
		this.add(AssetManager.languages_colors_library = new LanguagesColorsLibrary(), "language_colors_library");
		this.add(AssetManager.families_colors_library = new FamiliesColorsLibrary(), "families_colors_library");
		this.add(AssetManager.armies_colors_library = new ArmiesColorsLibrary(), "armies_colors_library");
		this.add(AssetManager.culture_colors_library = new CultureColorsLibrary(), "culture_colors_library");
		this.add(AssetManager.religion_colors_library = new ReligionColorsLibrary(), "religion_colors_library");
		this.add(AssetManager.months = new MonthLibrary(), "month_library");
		this.add(AssetManager.era_library = new WorldAgeLibrary(), "era_library");
		this.add(AssetManager.clouds = new CloudLibrary(), "cloud_library");
		this.add(AssetManager.map_sizes = new MapSizeLibrary(), "map_sizes");
		this.add(AssetManager.music_box = new MusicBoxLibrary(), "music_box");
		this.add(AssetManager.tiles = new TileLibrary(), "tiles");
		this.add(AssetManager.top_tiles = new TopTileLibrary(), "top_tiles");
		this.add(AssetManager.culture_traits = new CultureTraitLibrary(), "culture_traits");
		this.add(AssetManager.culture_trait_groups = new CultureTraitGroupLibrary(), "culture_trait_groups");
		this.add(AssetManager.language_traits = new LanguageTraitLibrary(), "language_traits");
		this.add(AssetManager.language_trait_groups = new LanguageTraitGroupLibrary(), "language_trait_groups");
		this.add(AssetManager.clan_traits = new ClanTraitLibrary(), "clan_traits");
		this.add(AssetManager.clan_trait_groups = new ClanTraitGroupLibrary(), "clan_trait_groups");
		this.add(AssetManager.subspecies_traits = new SubspeciesTraitLibrary(), "subspecies_traits");
		this.add(AssetManager.subspecies_trait_groups = new SubspeciesTraitGroupLibrary(), "subspecies_trait_groups");
		this.add(AssetManager.religion_traits = new ReligionTraitLibrary(), "religion_traits");
		this.add(AssetManager.religion_trait_groups = new ReligionTraitGroupLibrary(), "religion_trait_groups");
		this.add(AssetManager.trait_rains = new TraitRainLibrary(), "trait_rains");
		this.add(AssetManager.professions = new ProfessionLibrary(), "professions");
		this.add(AssetManager.quantum_sprites = new QuantumSpriteLibrary(), "quantum_sprites");
		this.add(AssetManager.world_behaviours = new WorldBehaviourLibrary(), "world_behaviours");
		this.add(AssetManager.personalities = new PersonalityLibrary(), "personalities");
		this.add(AssetManager.drops = new DropsLibrary(), "drops");
		this.add(AssetManager.status = new StatusLibrary(), "status");
		this.add(AssetManager.spells = new SpellLibrary(), "spells");
		this.add(AssetManager.citizen_job_library = new CitizenJobLibrary(), "citizen_job_library");
		this.add(AssetManager.tasks_actor = new BehaviourTaskActorLibrary(), "beh_actor");
		this.add(AssetManager.tasks_city = new BehaviourTaskCityLibrary(), "beh_city");
		this.add(AssetManager.tasks_kingdom = new BehaviourTaskKingdomLibrary(), "beh_kingdom");
		this.add(AssetManager.traits = new ActorTraitLibrary(), "traits");
		this.add(AssetManager.trait_groups = new ActorTraitGroupLibrary(), "trait_groups");
		this.add(AssetManager.plots_library = new PlotsLibrary(), "plots");
		this.add(AssetManager.plot_category_library = new PlotCategoryLibrary(), "plot_group");
		this.add(AssetManager.kingdoms = new KingdomLibrary(), "kingdoms");
		this.add(AssetManager.kingdoms_traits_groups = new KingdomTraitGroupLibrary(), "kingdom_trait_group");
		this.add(AssetManager.kingdoms_traits = new KingdomTraitLibrary(), "kingdom_traits");
		this.add(AssetManager.actor_library = new ActorAssetLibrary(), "units");
		this.add(AssetManager.buildings = new BuildingLibrary(), "buildings");
		this.add(AssetManager.name_generator = new NameGeneratorLibrary(), "name_generator");
		this.add(AssetManager.name_sets = new NameSetsLibrary(), "name_sets");
		this.add(AssetManager.disasters = new DisasterLibrary(), "disasters");
		this.add(AssetManager.job_actor = new ActorJobLibrary(), "job_actor");
		this.add(AssetManager.job_city = new CityJobLibrary(), "job_city");
		this.add(AssetManager.job_kingdom = new KingdomJobLibrary(), "job_kingdom");
		this.add(AssetManager.powers = new PowerLibrary(), "powers");
		this.add(AssetManager.items = new ItemLibrary(), "items");
		this.add(AssetManager.items_modifiers = new ItemModifierLibrary(), "items_modifiers");
		this.add(AssetManager.item_groups = new ItemGroupLibrary(), "item_groups");
		this.add(AssetManager.unit_hand_tools = new UnitHandToolLibrary(), "tools");
		this.add(AssetManager.resources = new ResourceLibrary(), "resources");
		this.add(AssetManager.terraform = new TerraformLibrary(), "terraform");
		this.add(AssetManager.projectiles = new ProjectileLibrary(), "projectiles");
		this.add(AssetManager.signals = new SignalLibrary(), "signals");
		this.add(AssetManager.achievement_groups = new AchievementGroupLibrary(), "achievement_groups");
		this.add(AssetManager.map_gen_templates = new MapGenTemplateLibrary(), "map_gen_templates");
		this.add(AssetManager.map_gen_settings = new MapGenSettingsLibrary(), "map_gen_settings");
		this.add(AssetManager.statistics_library = new StatisticsLibrary(), "statistics_library");
		this.add(AssetManager.linguistics_library = new LinguisticsLibrary(), "linguistics_library");
		this.add(AssetManager.words_library = new WordsLibrary(), "words_library");
		this.add(AssetManager.sentences_library = new SentencesLibrary(), "sentences_library");
		this.add(AssetManager.story_library = new StoryLibrary(), "story_library");
		this.add(AssetManager.onomastics_library = new OnomasticsLibrary(), "onomastics_library");
		this.add(AssetManager.onomastics_evolution_library = new OnomasticsEvolutionLibrary(), "onomastics_evolution_library");
		this.add(AssetManager.rarity_library = new RarityLibrary(), "rarity_library");
		this.add(AssetManager.knowledge_library = new KnowledgeLibrary(), "knowledge_library");
		this.add(AssetManager.window_library = new WindowLibrary(), "window_library");
		this.add(AssetManager.list_window_library = new ListWindowLibrary(), "list_window_library");
		this.add(AssetManager.power_tab_library = new PowerTabLibrary(), "power_tab_library");
		this.add(AssetManager.architect_mood_library = new ArchitectMoodLibrary(), "architect_mood_library");
		this.add(AssetManager.achievements = new AchievementLibrary(), "achievements");
		if (DebugConfig.isOn(DebugOption.TesterLibs))
		{
			AssetManager.loadAutoTester();
		}
		this.add(AssetManager.locale_groups_library = new LocaleGroupLibrary(), "locale_groups");
		foreach (BaseAssetLibrary baseAssetLibrary in this._list)
		{
			baseAssetLibrary.post_init();
		}
		foreach (BaseAssetLibrary baseAssetLibrary2 in this._list)
		{
			baseAssetLibrary2.linkAssets();
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000E530 File Offset: 0x0000C730
	public static void loadAutoTester()
	{
		if (AssetManager.tester_jobs != null)
		{
			return;
		}
		AssetManager._instance.add(AssetManager.tester_jobs = new TesterJobLibrary(), "tester_jobs");
		AssetManager._instance.add(AssetManager.tester_tasks = new TesterBehTaskLibrary(), "tester_tasks");
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000E56E File Offset: 0x0000C76E
	internal static void generateMissingLocalesFile()
	{
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000E570 File Offset: 0x0000C770
	public void exportAssets()
	{
		if (!DebugConfig.isOn(DebugOption.ExportAssetLibraries))
		{
			return;
		}
		MiniBench tBench = new MiniBench("exportAssets", 25L);
		string tFolder = "GenAssets/wbassets";
		if (!Directory.Exists(tFolder))
		{
			Directory.CreateDirectory(tFolder);
		}
		ParallelOptions tOptions = new ParallelOptions
		{
			MaxDegreeOfParallelism = 3
		};
		Parallel.ForEach<BaseAssetLibrary>(this._list, tOptions, delegate(BaseAssetLibrary pLib)
		{
			pLib.exportAssets();
		});
		tBench.Dispose();
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000E5EB File Offset: 0x0000C7EB
	private void add(BaseAssetLibrary pLibrary, string pId)
	{
		if (this._assetgv[0] != '0')
		{
			return;
		}
		pLibrary.init();
		this._list.Add(pLibrary);
		this._dict.Add(pId, pLibrary);
		pLibrary.id = pId;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000E624 File Offset: 0x0000C824
	public static IEnumerable<BaseAssetLibrary> getList()
	{
		return AssetManager._instance._list;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000E630 File Offset: 0x0000C830
	public static bool has(string pLibraryID)
	{
		return AssetManager._instance._dict.ContainsKey(pLibraryID);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000E642 File Offset: 0x0000C842
	public static BaseAssetLibrary get(string pLibraryID)
	{
		return AssetManager._instance._dict[pLibraryID];
	}

	// Token: 0x040000CA RID: 202
	public static TileEffectsLibrary tile_tile_effects;

	// Token: 0x040000CB RID: 203
	public static KingdomBannerLibrary kingdom_banners_library;

	// Token: 0x040000CC RID: 204
	public static CultureBannerLibrary culture_banners_library;

	// Token: 0x040000CD RID: 205
	public static ClanBannerLibrary clan_banners_library;

	// Token: 0x040000CE RID: 206
	public static ReligionBannerLibrary religion_banners_library;

	// Token: 0x040000CF RID: 207
	public static LanguageBannerLibrary language_banners_library;

	// Token: 0x040000D0 RID: 208
	public static SubspeciesBannerLibrary subspecies_banners_library;

	// Token: 0x040000D1 RID: 209
	public static FamilysBannerLibrary family_banners_library;

	// Token: 0x040000D2 RID: 210
	public static WorldTimeScaleLibrary time_scales;

	// Token: 0x040000D3 RID: 211
	public static OptionsLibrary options_library;

	// Token: 0x040000D4 RID: 212
	public static WorldLawLibrary world_laws_library;

	// Token: 0x040000D5 RID: 213
	public static WorldLawGroupLibrary world_law_groups;

	// Token: 0x040000D6 RID: 214
	public static OnomasticsLibrary onomastics_library;

	// Token: 0x040000D7 RID: 215
	public static OnomasticsEvolutionLibrary onomastics_evolution_library;

	// Token: 0x040000D8 RID: 216
	public static LinguisticsLibrary linguistics_library;

	// Token: 0x040000D9 RID: 217
	public static WordsLibrary words_library;

	// Token: 0x040000DA RID: 218
	public static SentencesLibrary sentences_library;

	// Token: 0x040000DB RID: 219
	public static StoryLibrary story_library;

	// Token: 0x040000DC RID: 220
	public static RarityLibrary rarity_library;

	// Token: 0x040000DD RID: 221
	public static GraphTimeLibrary graph_time_library;

	// Token: 0x040000DE RID: 222
	public static HistoryDataLibrary history_data_library;

	// Token: 0x040000DF RID: 223
	public static HistoryMetaDataLibrary history_meta_data_library;

	// Token: 0x040000E0 RID: 224
	public static BaseStatsLibrary base_stats_library;

	// Token: 0x040000E1 RID: 225
	public static ChromosomeTypeLibrary chromosome_type_library;

	// Token: 0x040000E2 RID: 226
	public static GeneLibrary gene_library;

	// Token: 0x040000E3 RID: 227
	public static NameplateLibrary nameplates_library;

	// Token: 0x040000E4 RID: 228
	public static MetaTypeLibrary meta_type_library;

	// Token: 0x040000E5 RID: 229
	public static MetaCustomizationLibrary meta_customization_library;

	// Token: 0x040000E6 RID: 230
	public static MetaRepresentationLibrary meta_representation_library;

	// Token: 0x040000E7 RID: 231
	public static DecisionsLibrary decisions_library;

	// Token: 0x040000E8 RID: 232
	public static NeuralLayerLibrary neural_layers;

	// Token: 0x040000E9 RID: 233
	public static LoyaltyLibrary loyalty_library;

	// Token: 0x040000EA RID: 234
	public static OpinionLibrary opinion_library;

	// Token: 0x040000EB RID: 235
	public static HappinessLibrary happiness_library;

	// Token: 0x040000EC RID: 236
	public static KingdomJobLibrary job_kingdom;

	// Token: 0x040000ED RID: 237
	public static BehaviourTaskKingdomLibrary tasks_kingdom;

	// Token: 0x040000EE RID: 238
	public static WorldLogLibrary world_log_library;

	// Token: 0x040000EF RID: 239
	public static HistoryGroupLibrary history_groups;

	// Token: 0x040000F0 RID: 240
	public static CityJobLibrary job_city;

	// Token: 0x040000F1 RID: 241
	public static BehaviourTaskCityLibrary tasks_city;

	// Token: 0x040000F2 RID: 242
	public static ActorJobLibrary job_actor;

	// Token: 0x040000F3 RID: 243
	public static BehaviourTaskActorLibrary tasks_actor;

	// Token: 0x040000F4 RID: 244
	public static CitizenJobLibrary citizen_job_library;

	// Token: 0x040000F5 RID: 245
	public static CultureTraitLibrary culture_traits;

	// Token: 0x040000F6 RID: 246
	public static CultureTraitGroupLibrary culture_trait_groups;

	// Token: 0x040000F7 RID: 247
	public static LanguageTraitLibrary language_traits;

	// Token: 0x040000F8 RID: 248
	public static LanguageTraitGroupLibrary language_trait_groups;

	// Token: 0x040000F9 RID: 249
	public static SubspeciesTraitLibrary subspecies_traits;

	// Token: 0x040000FA RID: 250
	public static SubspeciesTraitGroupLibrary subspecies_trait_groups;

	// Token: 0x040000FB RID: 251
	public static ClanTraitLibrary clan_traits;

	// Token: 0x040000FC RID: 252
	public static ClanTraitGroupLibrary clan_trait_groups;

	// Token: 0x040000FD RID: 253
	public static ReligionTraitLibrary religion_traits;

	// Token: 0x040000FE RID: 254
	public static ReligionTraitGroupLibrary religion_trait_groups;

	// Token: 0x040000FF RID: 255
	public static TraitRainLibrary trait_rains;

	// Token: 0x04000100 RID: 256
	public static CommunicationLibrary communication_library;

	// Token: 0x04000101 RID: 257
	public static CommunicationTopicLibrary communication_topic_library;

	// Token: 0x04000102 RID: 258
	public static BookTypeLibrary book_types;

	// Token: 0x04000103 RID: 259
	public static PersonalityLibrary personalities;

	// Token: 0x04000104 RID: 260
	public static ProfessionLibrary professions;

	// Token: 0x04000105 RID: 261
	public static DropsLibrary drops;

	// Token: 0x04000106 RID: 262
	public static BuildingLibrary buildings;

	// Token: 0x04000107 RID: 263
	public static ActorAssetLibrary actor_library;

	// Token: 0x04000108 RID: 264
	public static ActorTraitLibrary traits;

	// Token: 0x04000109 RID: 265
	public static ActorTraitGroupLibrary trait_groups;

	// Token: 0x0400010A RID: 266
	public static KingdomLibrary kingdoms;

	// Token: 0x0400010B RID: 267
	public static KingdomTraitLibrary kingdoms_traits;

	// Token: 0x0400010C RID: 268
	public static KingdomTraitGroupLibrary kingdoms_traits_groups;

	// Token: 0x0400010D RID: 269
	public static NameGeneratorLibrary name_generator;

	// Token: 0x0400010E RID: 270
	public static NameSetsLibrary name_sets;

	// Token: 0x0400010F RID: 271
	public static DisasterLibrary disasters;

	// Token: 0x04000110 RID: 272
	public static PhenotypeLibrary phenotype_library;

	// Token: 0x04000111 RID: 273
	public static BiomeLibrary biome_library;

	// Token: 0x04000112 RID: 274
	public static ResourceLibrary resources;

	// Token: 0x04000113 RID: 275
	public static ItemLibrary items;

	// Token: 0x04000114 RID: 276
	public static ItemModifierLibrary items_modifiers;

	// Token: 0x04000115 RID: 277
	public static ItemGroupLibrary item_groups;

	// Token: 0x04000116 RID: 278
	public static UnitHandToolLibrary unit_hand_tools;

	// Token: 0x04000117 RID: 279
	public static ProjectileLibrary projectiles;

	// Token: 0x04000118 RID: 280
	public static BuildOrderLibrary city_build_orders;

	// Token: 0x04000119 RID: 281
	public static ArchitectureLibrary architecture_library;

	// Token: 0x0400011A RID: 282
	public static CloudLibrary clouds;

	// Token: 0x0400011B RID: 283
	public static MonthLibrary months;

	// Token: 0x0400011C RID: 284
	public static TileLibrary tiles;

	// Token: 0x0400011D RID: 285
	public static TopTileLibrary top_tiles;

	// Token: 0x0400011E RID: 286
	public static TerraformLibrary terraform;

	// Token: 0x0400011F RID: 287
	public static PowerLibrary powers;

	// Token: 0x04000120 RID: 288
	public static SpellLibrary spells;

	// Token: 0x04000121 RID: 289
	public static StatusLibrary status;

	// Token: 0x04000122 RID: 290
	public static TesterJobLibrary tester_jobs;

	// Token: 0x04000123 RID: 291
	public static TesterBehTaskLibrary tester_tasks;

	// Token: 0x04000124 RID: 292
	public static MusicBoxLibrary music_box;

	// Token: 0x04000125 RID: 293
	public static AchievementLibrary achievements;

	// Token: 0x04000126 RID: 294
	public static AchievementGroupLibrary achievement_groups;

	// Token: 0x04000127 RID: 295
	public static SignalLibrary signals;

	// Token: 0x04000128 RID: 296
	public static MapGenSettingsLibrary map_gen_settings;

	// Token: 0x04000129 RID: 297
	public static MapGenTemplateLibrary map_gen_templates;

	// Token: 0x0400012A RID: 298
	public static QuantumSpriteLibrary quantum_sprites;

	// Token: 0x0400012B RID: 299
	public static WorldBehaviourLibrary world_behaviours;

	// Token: 0x0400012C RID: 300
	public static MapSizeLibrary map_sizes;

	// Token: 0x0400012D RID: 301
	public static WorldAgeLibrary era_library;

	// Token: 0x0400012E RID: 302
	public static EffectsLibrary effects_library;

	// Token: 0x0400012F RID: 303
	public static SimGlobals sim_globals_library;

	// Token: 0x04000130 RID: 304
	public static ColorStyleLibrary color_style_library;

	// Token: 0x04000131 RID: 305
	public static ClanColorsLibrary clan_colors_library;

	// Token: 0x04000132 RID: 306
	public static SubspeciesColorsLibrary subspecies_colors_library;

	// Token: 0x04000133 RID: 307
	public static FamiliesColorsLibrary families_colors_library;

	// Token: 0x04000134 RID: 308
	public static ArmiesColorsLibrary armies_colors_library;

	// Token: 0x04000135 RID: 309
	public static LanguagesColorsLibrary languages_colors_library;

	// Token: 0x04000136 RID: 310
	public static KingdomColorsLibrary kingdom_colors_library;

	// Token: 0x04000137 RID: 311
	public static CultureColorsLibrary culture_colors_library;

	// Token: 0x04000138 RID: 312
	public static ReligionColorsLibrary religion_colors_library;

	// Token: 0x04000139 RID: 313
	public static ArchitectMoodLibrary architect_mood_library;

	// Token: 0x0400013A RID: 314
	public static PlotsLibrary plots_library;

	// Token: 0x0400013B RID: 315
	public static PlotCategoryLibrary plot_category_library;

	// Token: 0x0400013C RID: 316
	public static TooltipLibrary tooltips;

	// Token: 0x0400013D RID: 317
	public static WarTypeLibrary war_types_library;

	// Token: 0x0400013E RID: 318
	public static HotkeyLibrary hotkey_library;

	// Token: 0x0400013F RID: 319
	public static StatisticsLibrary statistics_library;

	// Token: 0x04000140 RID: 320
	public static BrushLibrary brush_library;

	// Token: 0x04000141 RID: 321
	public static DebugToolLibrary debug_tool_library;

	// Token: 0x04000142 RID: 322
	public static CombatActionLibrary combat_action_library;

	// Token: 0x04000143 RID: 323
	public static DynamicSpritesLibrary dynamic_sprites_library;

	// Token: 0x04000144 RID: 324
	public static KnowledgeLibrary knowledge_library;

	// Token: 0x04000145 RID: 325
	public static WindowLibrary window_library;

	// Token: 0x04000146 RID: 326
	public static MetaTextReportLibrary meta_text_report_library;

	// Token: 0x04000147 RID: 327
	public static ListWindowLibrary list_window_library;

	// Token: 0x04000148 RID: 328
	public static PowerTabLibrary power_tab_library;

	// Token: 0x04000149 RID: 329
	public static LocaleGroupLibrary locale_groups_library;

	// Token: 0x0400014A RID: 330
	public static GameLanguageLibrary game_language_library;

	// Token: 0x0400014B RID: 331
	private static AssetManager _instance;

	// Token: 0x0400014C RID: 332
	private readonly List<BaseAssetLibrary> _list = new List<BaseAssetLibrary>();

	// Token: 0x0400014D RID: 333
	private readonly Dictionary<string, BaseAssetLibrary> _dict = new Dictionary<string, BaseAssetLibrary>();

	// Token: 0x0400014E RID: 334
	private string _assetgv;

	// Token: 0x0400014F RID: 335
	public static HashSet<string> missing_locale_keys = new HashSet<string>();
}
