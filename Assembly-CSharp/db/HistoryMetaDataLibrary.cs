using System;
using System.Collections.Generic;
using System.Reflection;
using db.tables;

namespace db
{
	// Token: 0x0200085F RID: 2143
	public class HistoryMetaDataLibrary : AssetLibrary<HistoryMetaDataAsset>
	{
		// Token: 0x060042FF RID: 17151 RVA: 0x001C6D94 File Offset: 0x001C4F94
		public override void init()
		{
			base.init();
			this.add(new HistoryMetaDataAsset
			{
				id = "world",
				meta_type = MetaType.World,
				table_type = typeof(WorldTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(WorldYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(WorldYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(WorldYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(WorldYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(WorldYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(WorldYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(WorldYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(WorldYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(WorldYearly10000)
					}
				}
			});
			this.t.collector = ((NanoObject _) => new WorldYearly1
			{
				id = 1L,
				alliances = new long?(StatsHelper.getStat("alliances")),
				alliances_dissolved = new long?(StatsHelper.getStat("world_statistics_alliances_dissolved")),
				alliances_made = new long?(StatsHelper.getStat("world_statistics_alliances_made")),
				books = new long?(StatsHelper.getStat("books")),
				books_burnt = new long?(StatsHelper.getStat("world_statistics_books_burnt")),
				books_read = new long?(StatsHelper.getStat("world_statistics_books_read")),
				books_written = new long?(StatsHelper.getStat("world_statistics_books_written")),
				cities = new long?(StatsHelper.getStat("villages")),
				cities_rebelled = new long?(StatsHelper.getStat("world_statistics_cities_rebelled")),
				cities_conquered = new long?(StatsHelper.getStat("world_statistics_cities_conquered")),
				cities_created = new long?(StatsHelper.getStat("world_statistics_cities_created")),
				cities_destroyed = new long?(StatsHelper.getStat("world_statistics_cities_destroyed")),
				clans = new long?(StatsHelper.getStat("clans")),
				clans_created = new long?(StatsHelper.getStat("world_statistics_clans_created")),
				clans_destroyed = new long?(StatsHelper.getStat("world_statistics_clans_destroyed")),
				creatures_born = new long?(StatsHelper.getStat("world_statistics_creatures_born")),
				creatures_created = new long?(StatsHelper.getStat("world_statistics_creatures_created")),
				cultures = new long?(StatsHelper.getStat("cultures")),
				cultures_created = new long?(StatsHelper.getStat("world_statistics_cultures_created")),
				cultures_forgotten = new long?(StatsHelper.getStat("world_statistics_cultures_forgotten")),
				deaths_eaten = new long?(StatsHelper.getStat("world_statistics_deaths_eaten")),
				deaths_hunger = new long?(StatsHelper.getStat("world_statistics_deaths_hunger")),
				deaths_natural = new long?(StatsHelper.getStat("world_statistics_deaths_natural")),
				deaths_poison = new long?(StatsHelper.getStat("world_statistics_deaths_poison")),
				deaths_infection = new long?(StatsHelper.getStat("world_statistics_deaths_infection")),
				deaths_tumor = new long?(StatsHelper.getStat("world_statistics_deaths_tumor")),
				deaths_acid = new long?(StatsHelper.getStat("world_statistics_deaths_acid")),
				deaths_fire = new long?(StatsHelper.getStat("world_statistics_deaths_fire")),
				deaths_divine = new long?(StatsHelper.getStat("world_statistics_deaths_divine")),
				deaths_weapon = new long?(StatsHelper.getStat("world_statistics_deaths_weapon")),
				deaths_gravity = new long?(StatsHelper.getStat("world_statistics_deaths_gravity")),
				deaths_drowning = new long?(StatsHelper.getStat("world_statistics_deaths_drowning")),
				deaths_water = new long?(StatsHelper.getStat("world_statistics_deaths_water")),
				deaths_explosion = new long?(StatsHelper.getStat("world_statistics_deaths_explosion")),
				metamorphosis = new long?(StatsHelper.getStat("world_statistics_metamorphosis")),
				evolutions = new long?(StatsHelper.getStat("world_statistics_evolutions")),
				deaths_other = new long?(StatsHelper.getStat("world_statistics_deaths_other")),
				deaths_plague = new long?(StatsHelper.getStat("world_statistics_deaths_plague")),
				deaths_total = new long?(StatsHelper.getStat("world_statistics_deaths_total")),
				families = new long?(StatsHelper.getStat("families")),
				families_created = new long?(StatsHelper.getStat("world_statistics_families_created")),
				families_destroyed = new long?(StatsHelper.getStat("world_statistics_families_destroyed")),
				houses = new long?(StatsHelper.getStat("world_statistics_houses")),
				houses_built = new long?(StatsHelper.getStat("world_statistics_houses_built")),
				houses_destroyed = new long?(StatsHelper.getStat("world_statistics_houses_destroyed")),
				infected = new long?(StatsHelper.getStat("world_statistics_infected")),
				islands = new long?(StatsHelper.getStat("world_statistics_islands")),
				kingdoms = new long?(StatsHelper.getStat("kingdoms")),
				kingdoms_created = new long?(StatsHelper.getStat("world_statistics_kingdoms_created")),
				kingdoms_destroyed = new long?(StatsHelper.getStat("world_statistics_kingdoms_destroyed")),
				languages = new long?(StatsHelper.getStat("languages")),
				languages_created = new long?(StatsHelper.getStat("world_statistics_languages_created")),
				languages_forgotten = new long?(StatsHelper.getStat("world_statistics_languages_forgotten")),
				peaces_made = new long?(StatsHelper.getStat("world_statistics_peaces_made")),
				plots = new long?(StatsHelper.getStat("plots")),
				plots_forgotten = new long?(StatsHelper.getStat("world_statistics_plots_forgotten")),
				plots_started = new long?(StatsHelper.getStat("world_statistics_plots_started")),
				plots_succeeded = new long?(StatsHelper.getStat("world_statistics_plots_succeeded")),
				population_beasts = new long?(StatsHelper.getStat("world_statistics_beasts")),
				population_civ = new long?(StatsHelper.getStat("world_statistics_population")),
				religions = new long?(StatsHelper.getStat("religions")),
				religions_created = new long?(StatsHelper.getStat("world_statistics_religions_created")),
				religions_forgotten = new long?(StatsHelper.getStat("world_statistics_religions_forgotten")),
				subspecies = new long?(StatsHelper.getStat("subspecies")),
				subspecies_created = new long?(StatsHelper.getStat("world_statistics_subspecies_created")),
				subspecies_extinct = new long?(StatsHelper.getStat("world_statistics_subspecies_extinct")),
				trees = new long?(StatsHelper.getStat("world_statistics_trees")),
				vegetation = new long?(StatsHelper.getStat("world_statistics_vegetation")),
				wars = new long?(StatsHelper.getStat("wars")),
				wars_started = new long?(StatsHelper.getStat("world_statistics_wars_started")),
				grass = new long?(StatsHelper.getStat("world_statistics_grass")),
				savanna = new long?(StatsHelper.getStat("world_statistics_savanna")),
				jungle = new long?(StatsHelper.getStat("world_statistics_jungle")),
				desert = new long?(StatsHelper.getStat("world_statistics_desert")),
				lemon = new long?(StatsHelper.getStat("world_statistics_lemon")),
				permafrost = new long?(StatsHelper.getStat("world_statistics_permafrost")),
				swamp = new long?(StatsHelper.getStat("world_statistics_swamp")),
				crystal = new long?(StatsHelper.getStat("world_statistics_crystal")),
				enchanted = new long?(StatsHelper.getStat("world_statistics_enchanted")),
				corruption = new long?(StatsHelper.getStat("world_statistics_corruption")),
				infernal = new long?(StatsHelper.getStat("world_statistics_infernal")),
				candy = new long?(StatsHelper.getStat("world_statistics_candy")),
				mushroom = new long?(StatsHelper.getStat("world_statistics_mushroom")),
				wasteland = new long?(StatsHelper.getStat("world_statistics_wasteland")),
				birch = new long?(StatsHelper.getStat("world_statistics_birch")),
				maple = new long?(StatsHelper.getStat("world_statistics_maple")),
				rocklands = new long?(StatsHelper.getStat("world_statistics_rocklands")),
				garlic = new long?(StatsHelper.getStat("world_statistics_garlic")),
				flower = new long?(StatsHelper.getStat("world_statistics_flower")),
				celestial = new long?(StatsHelper.getStat("world_statistics_celestial")),
				clover = new long?(StatsHelper.getStat("world_statistics_clover")),
				singularity = new long?(StatsHelper.getStat("world_statistics_singularity")),
				paradox = new long?(StatsHelper.getStat("world_statistics_paradox")),
				sand = new long?(StatsHelper.getStat("world_statistics_sand")),
				biomass = new long?(StatsHelper.getStat("world_statistics_biomass")),
				cybertile = new long?(StatsHelper.getStat("world_statistics_cybertile")),
				pumpkin = new long?(StatsHelper.getStat("world_statistics_pumpkin")),
				tumor = new long?(StatsHelper.getStat("world_statistics_tumor")),
				water = new long?(StatsHelper.getStat("world_statistics_water")),
				soil = new long?(StatsHelper.getStat("world_statistics_soil")),
				summit = new long?(StatsHelper.getStat("world_statistics_summit")),
				mountains = new long?(StatsHelper.getStat("world_statistics_mountains")),
				hills = new long?(StatsHelper.getStat("world_statistics_hills")),
				lava = new long?(StatsHelper.getStat("world_statistics_lava")),
				pit = new long?(StatsHelper.getStat("world_statistics_pit")),
				field = new long?(StatsHelper.getStat("world_statistics_field")),
				fireworks = new long?(StatsHelper.getStat("world_statistics_fireworks")),
				frozen = new long?(StatsHelper.getStat("world_statistics_frozen")),
				fuse = new long?(StatsHelper.getStat("world_statistics_fuse")),
				ice = new long?(StatsHelper.getStat("world_statistics_ice")),
				landmine = new long?(StatsHelper.getStat("world_statistics_landmine")),
				road = new long?(StatsHelper.getStat("world_statistics_road")),
				snow = new long?(StatsHelper.getStat("world_statistics_snow")),
				tnt = new long?(StatsHelper.getStat("world_statistics_tnt")),
				wall = new long?(StatsHelper.getStat("world_statistics_wall")),
				water_bomb = new long?(StatsHelper.getStat("world_statistics_water_bomb")),
				grey_goo = new long?(StatsHelper.getStat("world_statistics_grey_goo"))
			});
			this.add(new HistoryMetaDataAsset
			{
				id = "alliance",
				meta_type = MetaType.Alliance,
				table_type = typeof(AllianceTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(AllianceYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(AllianceYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(AllianceYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(AllianceYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(AllianceYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(AllianceYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(AllianceYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(AllianceYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(AllianceYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Alliance pAlliance = (Alliance)pNanoObject;
				return new AllianceYearly1
				{
					id = pAlliance.getID(),
					population = new long?((long)pAlliance.countPopulation()),
					adults = new long?((long)pAlliance.countAdults()),
					children = new long?((long)pAlliance.countChildren()),
					army = new long?((long)pAlliance.countWarriors()),
					sick = new long?((long)pAlliance.countSick()),
					hungry = new long?((long)pAlliance.countHungry()),
					starving = new long?((long)pAlliance.countStarving()),
					happy = new long?((long)pAlliance.countHappyUnits()),
					deaths = new long?(pAlliance.getTotalDeaths()),
					kills = new long?(pAlliance.getTotalKills()),
					births = new long?(pAlliance.getTotalBirths()),
					territory = new long?((long)pAlliance.countZones()),
					buildings = new long?((long)pAlliance.countBuildings()),
					homeless = new long?((long)pAlliance.countHomeless()),
					housed = new long?((long)pAlliance.countHoused()),
					families = new long?((long)pAlliance.countFamilies()),
					males = new long?((long)pAlliance.countMales()),
					females = new long?((long)pAlliance.countFemales()),
					kingdoms = new long?((long)pAlliance.countKingdoms()),
					cities = new long?((long)pAlliance.countCities()),
					renown = new long?((long)pAlliance.getRenown()),
					money = new long?((long)pAlliance.countTotalMoney())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "clan",
				meta_type = MetaType.Clan,
				table_type = typeof(ClanTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(ClanYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(ClanYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(ClanYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(ClanYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(ClanYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(ClanYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(ClanYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(ClanYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(ClanYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Clan pClan = (Clan)pNanoObject;
				return new ClanYearly1
				{
					id = pClan.getID(),
					population = new long?((long)pClan.countUnits()),
					adults = new long?((long)pClan.countAdults()),
					children = new long?((long)pClan.countChildren()),
					births = new long?(pClan.getTotalBirths()),
					deaths = new long?(pClan.getTotalDeaths()),
					kills = new long?(pClan.getTotalKills()),
					kings = new long?((long)pClan.countKings()),
					leaders = new long?((long)pClan.countLeaders()),
					renown = new long?((long)pClan.getRenown()),
					money = new long?((long)pClan.countTotalMoney()),
					deaths_eaten = new long?(pClan.getDeaths(AttackType.Eaten)),
					deaths_hunger = new long?(pClan.getDeaths(AttackType.Starvation)),
					deaths_natural = new long?(pClan.getDeaths(AttackType.Age)),
					deaths_plague = new long?(pClan.getDeaths(AttackType.Plague)),
					deaths_poison = new long?(pClan.getDeaths(AttackType.Poison)),
					deaths_infection = new long?(pClan.getDeaths(AttackType.Infection)),
					deaths_tumor = new long?(pClan.getDeaths(AttackType.Tumor)),
					deaths_acid = new long?(pClan.getDeaths(AttackType.Acid)),
					deaths_fire = new long?(pClan.getDeaths(AttackType.Fire)),
					deaths_divine = new long?(pClan.getDeaths(AttackType.Divine)),
					deaths_weapon = new long?(pClan.getDeaths(AttackType.Weapon)),
					deaths_gravity = new long?(pClan.getDeaths(AttackType.Gravity)),
					deaths_drowning = new long?(pClan.getDeaths(AttackType.Drowning)),
					deaths_water = new long?(pClan.getDeaths(AttackType.Water)),
					deaths_explosion = new long?(pClan.getDeaths(AttackType.Explosion)),
					deaths_other = new long?(pClan.getDeaths(AttackType.Other)),
					metamorphosis = new long?(pClan.getDeaths(AttackType.Metamorphosis)),
					evolutions = new long?(pClan.getEvolutions())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "city",
				meta_type = MetaType.City,
				table_type = typeof(CityTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(CityYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(CityYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(CityYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(CityYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(CityYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(CityYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(CityYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(CityYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(CityYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				City pCity = (City)pNanoObject;
				return new CityYearly1
				{
					id = pCity.getID(),
					population = new long?((long)pCity.countUnits()),
					adults = new long?((long)pCity.countAdults()),
					children = new long?((long)pCity.countChildren()),
					boats = new long?((long)pCity.countBoats()),
					army = new long?((long)pCity.countWarriors()),
					families = new long?((long)pCity.countFamilies()),
					males = new long?((long)pCity.countMales()),
					females = new long?((long)pCity.countFemales()),
					sick = new long?((long)pCity.countSick()),
					loyalty = new long?((long)pCity.getCachedLoyalty()),
					hungry = new long?((long)pCity.countHungry()),
					starving = new long?((long)pCity.countStarving()),
					happy = new long?((long)pCity.countHappyUnits()),
					deaths = new long?(pCity.getTotalDeaths()),
					births = new long?(pCity.getTotalBirths()),
					joined = new long?(pCity.getTotalJoined()),
					left = new long?(pCity.getTotalLeft()),
					moved = new long?(pCity.getTotalMoved()),
					migrated = new long?(pCity.getTotalMigrated()),
					territory = new long?((long)pCity.countZones()),
					buildings = new long?((long)pCity.countBuildings()),
					homeless = new long?((long)pCity.countHomeless()),
					housed = new long?((long)pCity.countHoused()),
					renown = new long?((long)pCity.getRenown()),
					money = new long?((long)pCity.countTotalMoney()),
					food = new long?((long)pCity.getTotalFood()),
					gold = new long?((long)pCity.getResourcesAmount("gold")),
					wood = new long?((long)pCity.getResourcesAmount("wood")),
					stone = new long?((long)pCity.getResourcesAmount("stone")),
					common_metals = new long?((long)pCity.getResourcesAmount("common_metals")),
					items = new long?((long)pCity.data.equipment.countItems()),
					deaths_eaten = new long?(pCity.getDeaths(AttackType.Eaten)),
					deaths_hunger = new long?(pCity.getDeaths(AttackType.Starvation)),
					deaths_natural = new long?(pCity.getDeaths(AttackType.Age)),
					deaths_plague = new long?(pCity.getDeaths(AttackType.Plague)),
					deaths_poison = new long?(pCity.getDeaths(AttackType.Poison)),
					deaths_infection = new long?(pCity.getDeaths(AttackType.Infection)),
					deaths_tumor = new long?(pCity.getDeaths(AttackType.Tumor)),
					deaths_acid = new long?(pCity.getDeaths(AttackType.Acid)),
					deaths_fire = new long?(pCity.getDeaths(AttackType.Fire)),
					deaths_divine = new long?(pCity.getDeaths(AttackType.Divine)),
					deaths_weapon = new long?(pCity.getDeaths(AttackType.Weapon)),
					deaths_gravity = new long?(pCity.getDeaths(AttackType.Gravity)),
					deaths_drowning = new long?(pCity.getDeaths(AttackType.Drowning)),
					deaths_water = new long?(pCity.getDeaths(AttackType.Water)),
					deaths_explosion = new long?(pCity.getDeaths(AttackType.Explosion)),
					deaths_other = new long?(pCity.getDeaths(AttackType.Other)),
					metamorphosis = new long?(pCity.getDeaths(AttackType.Metamorphosis)),
					evolutions = new long?(pCity.getEvolutions())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "culture",
				meta_type = MetaType.Culture,
				table_type = typeof(CultureTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(CultureYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(CultureYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(CultureYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(CultureYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(CultureYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(CultureYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(CultureYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(CultureYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(CultureYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Culture pCulture = (Culture)pNanoObject;
				return new CultureYearly1
				{
					id = pCulture.getID(),
					population = new long?((long)pCulture.countUnits()),
					cities = new long?((long)pCulture.countCities()),
					kingdoms = new long?((long)pCulture.countKingdoms()),
					births = new long?(pCulture.getTotalBirths()),
					deaths = new long?(pCulture.getTotalDeaths()),
					kills = new long?(pCulture.getTotalKills()),
					adults = new long?((long)pCulture.countAdults()),
					children = new long?((long)pCulture.countChildren()),
					kings = new long?((long)pCulture.countKings()),
					leaders = new long?((long)pCulture.countLeaders()),
					renown = new long?((long)pCulture.getRenown()),
					money = new long?((long)pCulture.countTotalMoney())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "family",
				meta_type = MetaType.Family,
				table_type = typeof(FamilyTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(FamilyYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(FamilyYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(FamilyYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(FamilyYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(FamilyYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(FamilyYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(FamilyYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(FamilyYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(FamilyYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Family pFamily = (Family)pNanoObject;
				return new FamilyYearly1
				{
					id = pFamily.getID(),
					population = new long?((long)pFamily.countUnits()),
					adults = new long?((long)pFamily.countAdults()),
					children = new long?((long)pFamily.countChildren()),
					births = new long?(pFamily.getTotalBirths()),
					deaths = new long?(pFamily.getTotalDeaths()),
					kills = new long?(pFamily.getTotalKills()),
					money = new long?((long)pFamily.countTotalMoney())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "army",
				meta_type = MetaType.Army,
				table_type = typeof(ArmyTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(ArmyYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(ArmyYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(ArmyYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(ArmyYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(ArmyYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(ArmyYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(ArmyYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(ArmyYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(ArmyYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Army tArmy = (Army)pNanoObject;
				return new ArmyYearly1
				{
					id = tArmy.getID(),
					population = new long?((long)tArmy.countUnits()),
					deaths = new long?(tArmy.getTotalDeaths()),
					kills = new long?(tArmy.getTotalKills())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "kingdom",
				meta_type = MetaType.Kingdom,
				table_type = typeof(KingdomTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(KingdomYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(KingdomYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(KingdomYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(KingdomYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(KingdomYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(KingdomYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(KingdomYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(KingdomYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(KingdomYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Kingdom pKingdom = (Kingdom)pNanoObject;
				return new KingdomYearly1
				{
					id = pKingdom.getID(),
					population = new long?((long)pKingdom.countUnits()),
					adults = new long?((long)pKingdom.countAdults()),
					children = new long?((long)pKingdom.countChildren()),
					boats = new long?((long)pKingdom.countBoats()),
					army = new long?((long)pKingdom.countTotalWarriors()),
					sick = new long?((long)pKingdom.countSick()),
					hungry = new long?((long)pKingdom.countHungry()),
					starving = new long?((long)pKingdom.countStarving()),
					happy = new long?((long)pKingdom.countHappyUnits()),
					deaths = new long?(pKingdom.getTotalDeaths()),
					births = new long?(pKingdom.getTotalBirths()),
					kills = new long?(pKingdom.getTotalKills()),
					joined = new long?(pKingdom.getTotalJoined()),
					left = new long?(pKingdom.getTotalLeft()),
					moved = new long?(pKingdom.getTotalMoved()),
					migrated = new long?(pKingdom.getTotalMigrated()),
					territory = new long?((long)pKingdom.countZones()),
					buildings = new long?((long)pKingdom.countBuildings()),
					homeless = new long?((long)pKingdom.countHomeless()),
					housed = new long?((long)pKingdom.countHoused()),
					food = new long?((long)pKingdom.countTotalFood()),
					families = new long?((long)pKingdom.countFamilies()),
					males = new long?((long)pKingdom.countMales()),
					females = new long?((long)pKingdom.countFemales()),
					cities = new long?((long)pKingdom.countCities()),
					renown = new long?((long)pKingdom.getRenown()),
					money = new long?((long)pKingdom.countTotalMoney()),
					deaths_eaten = new long?(pKingdom.getDeaths(AttackType.Eaten)),
					deaths_hunger = new long?(pKingdom.getDeaths(AttackType.Starvation)),
					deaths_natural = new long?(pKingdom.getDeaths(AttackType.Age)),
					deaths_plague = new long?(pKingdom.getDeaths(AttackType.Plague)),
					deaths_poison = new long?(pKingdom.getDeaths(AttackType.Poison)),
					deaths_infection = new long?(pKingdom.getDeaths(AttackType.Infection)),
					deaths_tumor = new long?(pKingdom.getDeaths(AttackType.Tumor)),
					deaths_acid = new long?(pKingdom.getDeaths(AttackType.Acid)),
					deaths_fire = new long?(pKingdom.getDeaths(AttackType.Fire)),
					deaths_divine = new long?(pKingdom.getDeaths(AttackType.Divine)),
					deaths_weapon = new long?(pKingdom.getDeaths(AttackType.Weapon)),
					deaths_gravity = new long?(pKingdom.getDeaths(AttackType.Gravity)),
					deaths_drowning = new long?(pKingdom.getDeaths(AttackType.Drowning)),
					deaths_water = new long?(pKingdom.getDeaths(AttackType.Water)),
					deaths_explosion = new long?(pKingdom.getDeaths(AttackType.Explosion)),
					deaths_other = new long?(pKingdom.getDeaths(AttackType.Other)),
					metamorphosis = new long?(pKingdom.getDeaths(AttackType.Metamorphosis)),
					evolutions = new long?(pKingdom.getEvolutions())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "language",
				meta_type = MetaType.Language,
				table_type = typeof(LanguageTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(LanguageYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(LanguageYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(LanguageYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(LanguageYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(LanguageYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(LanguageYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(LanguageYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(LanguageYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(LanguageYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Language pLanguage = (Language)pNanoObject;
				return new LanguageYearly1
				{
					id = pLanguage.getID(),
					population = new long?((long)pLanguage.countUnits()),
					adults = new long?((long)pLanguage.countAdults()),
					children = new long?((long)pLanguage.countChildren()),
					kingdoms = new long?((long)pLanguage.countKingdoms()),
					cities = new long?((long)pLanguage.countCities()),
					books = new long?((long)pLanguage.books.count()),
					books_written = new long?((long)pLanguage.countWrittenBooks()),
					speakers_new = new long?((long)pLanguage.getSpeakersNew()),
					speakers_lost = new long?((long)pLanguage.getSpeakersLost()),
					speakers_converted = new long?((long)pLanguage.getSpeakersConverted()),
					deaths = new long?(pLanguage.getTotalDeaths()),
					kills = new long?(pLanguage.getTotalKills()),
					renown = new long?((long)pLanguage.getRenown()),
					money = new long?((long)pLanguage.countTotalMoney())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "religion",
				meta_type = MetaType.Religion,
				table_type = typeof(ReligionTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(ReligionYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(ReligionYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(ReligionYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(ReligionYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(ReligionYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(ReligionYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(ReligionYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(ReligionYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(ReligionYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Religion pReligion = (Religion)pNanoObject;
				return new ReligionYearly1
				{
					id = pReligion.getID(),
					population = new long?((long)pReligion.countUnits()),
					kingdoms = new long?((long)pReligion.countKingdoms()),
					cities = new long?((long)pReligion.countCities()),
					sick = new long?((long)pReligion.countSick()),
					happy = new long?((long)pReligion.countHappyUnits()),
					hungry = new long?((long)pReligion.countHungry()),
					starving = new long?((long)pReligion.countStarving()),
					deaths = new long?(pReligion.getTotalDeaths()),
					births = new long?(pReligion.getTotalBirths()),
					kills = new long?(pReligion.getTotalKills()),
					adults = new long?((long)pReligion.countAdults()),
					children = new long?((long)pReligion.countChildren()),
					males = new long?((long)pReligion.countMales()),
					females = new long?((long)pReligion.countFemales()),
					homeless = new long?((long)pReligion.countHomeless()),
					housed = new long?((long)pReligion.countHoused()),
					kings = new long?((long)pReligion.countKings()),
					leaders = new long?((long)pReligion.countLeaders()),
					renown = new long?((long)pReligion.getRenown()),
					money = new long?((long)pReligion.countTotalMoney()),
					evolutions = new long?(pReligion.getEvolutions())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "subspecies",
				meta_type = MetaType.Subspecies,
				table_type = typeof(SubspeciesTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(SubspeciesYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(SubspeciesYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(SubspeciesYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(SubspeciesYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(SubspeciesYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(SubspeciesYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(SubspeciesYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(SubspeciesYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(SubspeciesYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				Subspecies pSubspecies = (Subspecies)pNanoObject;
				return new SubspeciesYearly1
				{
					id = pSubspecies.getID(),
					population = new long?((long)pSubspecies.countUnits()),
					adults = new long?((long)pSubspecies.countAdults()),
					children = new long?((long)pSubspecies.countChildren()),
					deaths = new long?(pSubspecies.getTotalDeaths()),
					births = new long?(pSubspecies.getTotalBirths()),
					kills = new long?(pSubspecies.getTotalKills()),
					renown = new long?((long)pSubspecies.getRenown()),
					money = new long?((long)pSubspecies.countTotalMoney()),
					deaths_eaten = new long?(pSubspecies.getDeaths(AttackType.Eaten)),
					deaths_hunger = new long?(pSubspecies.getDeaths(AttackType.Starvation)),
					deaths_natural = new long?(pSubspecies.getDeaths(AttackType.Age)),
					deaths_plague = new long?(pSubspecies.getDeaths(AttackType.Plague)),
					deaths_poison = new long?(pSubspecies.getDeaths(AttackType.Poison)),
					deaths_infection = new long?(pSubspecies.getDeaths(AttackType.Infection)),
					deaths_tumor = new long?(pSubspecies.getDeaths(AttackType.Tumor)),
					deaths_acid = new long?(pSubspecies.getDeaths(AttackType.Acid)),
					deaths_fire = new long?(pSubspecies.getDeaths(AttackType.Fire)),
					deaths_divine = new long?(pSubspecies.getDeaths(AttackType.Divine)),
					deaths_weapon = new long?(pSubspecies.getDeaths(AttackType.Weapon)),
					deaths_gravity = new long?(pSubspecies.getDeaths(AttackType.Gravity)),
					deaths_drowning = new long?(pSubspecies.getDeaths(AttackType.Drowning)),
					deaths_water = new long?(pSubspecies.getDeaths(AttackType.Water)),
					deaths_explosion = new long?(pSubspecies.getDeaths(AttackType.Explosion)),
					deaths_other = new long?(pSubspecies.getDeaths(AttackType.Other)),
					metamorphosis = new long?(pSubspecies.getDeaths(AttackType.Metamorphosis)),
					evolutions = new long?(pSubspecies.getEvolutions())
				};
			};
			this.add(new HistoryMetaDataAsset
			{
				id = "war",
				meta_type = MetaType.War,
				table_type = typeof(WarTable),
				table_types = new Dictionary<HistoryInterval, Type>
				{
					{
						HistoryInterval.Yearly1,
						typeof(WarYearly1)
					},
					{
						HistoryInterval.Yearly5,
						typeof(WarYearly5)
					},
					{
						HistoryInterval.Yearly10,
						typeof(WarYearly10)
					},
					{
						HistoryInterval.Yearly50,
						typeof(WarYearly50)
					},
					{
						HistoryInterval.Yearly100,
						typeof(WarYearly100)
					},
					{
						HistoryInterval.Yearly500,
						typeof(WarYearly500)
					},
					{
						HistoryInterval.Yearly1000,
						typeof(WarYearly1000)
					},
					{
						HistoryInterval.Yearly5000,
						typeof(WarYearly5000)
					},
					{
						HistoryInterval.Yearly10000,
						typeof(WarYearly10000)
					}
				}
			});
			this.t.collector = delegate(NanoObject pNanoObject)
			{
				War pWar = (War)pNanoObject;
				return new WarYearly1
				{
					id = pWar.getID(),
					population = new long?((long)pWar.countTotalPopulation()),
					army = new long?((long)pWar.countTotalArmy()),
					renown = new long?((long)pWar.getRenown()),
					kingdoms = new long?((long)pWar.countKingdoms()),
					cities = new long?((long)pWar.countCities()),
					deaths = new long?(pWar.getTotalDeaths()),
					population_attackers = new long?((long)pWar.countAttackersPopulation()),
					population_defenders = new long?((long)pWar.countDefendersPopulation()),
					army_attackers = new long?((long)pWar.countAttackersWarriors()),
					army_defenders = new long?((long)pWar.countDefendersWarriors()),
					deaths_attackers = new long?((long)pWar.getDeadAttackers()),
					deaths_defenders = new long?((long)pWar.getDeadDefenders()),
					money_attackers = new long?((long)pWar.countAttackersMoney()),
					money_defenders = new long?((long)pWar.countDefendersMoney())
				};
			};
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x001C7988 File Offset: 0x001C5B88
		public override void editorDiagnostic()
		{
			base.editorDiagnostic();
			HashSet<Type> tUniqueTypes = new HashSet<Type>();
			foreach (HistoryMetaDataAsset tAsset in this.list)
			{
				Dictionary<HistoryInterval, Type> tTableTypes = tAsset.table_types;
				foreach (object obj in Enum.GetValues(typeof(HistoryInterval)))
				{
					HistoryInterval tInterval = (HistoryInterval)obj;
					if (tInterval != HistoryInterval.None)
					{
						if (!tTableTypes.ContainsKey(tInterval))
						{
							BaseAssetLibrary.logAssetError(string.Format("<e>HistoryMetaDataLibrary</e>: Missing a table type for <b>{0}</b>", tInterval), tAsset.id);
						}
						else if (!tUniqueTypes.Add(tTableTypes[tInterval]))
						{
							BaseAssetLibrary.logAssetError(string.Format("<e>HistoryMetaDataLibrary</e>: Duplicate table type <b>{0}</b> for <b>{1}</b>", tTableTypes[tInterval], tInterval), tAsset.id);
						}
					}
				}
			}
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x001C7AA0 File Offset: 0x001C5CA0
		public HistoryMetaDataAsset[] getAssets(MetaType pMetaType)
		{
			return HistoryMetaDataLibrary._meta_data[pMetaType];
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x001C7AAD File Offset: 0x001C5CAD
		public HistoryMetaDataAsset[] getAssets(string pMetaType)
		{
			return HistoryMetaDataLibrary._meta_dict[pMetaType];
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x001C7ABC File Offset: 0x001C5CBC
		public override void linkAssets()
		{
			base.linkAssets();
			Dictionary<MetaType, ListPool<HistoryMetaDataAsset>> meta_data = new Dictionary<MetaType, ListPool<HistoryMetaDataAsset>>();
			foreach (HistoryMetaDataAsset tAsset in this.list)
			{
				TypeInfo typeInfo = tAsset.table_type.GetTypeInfo();
				List<string> tCategoryProps = new List<string>();
				foreach (PropertyInfo tProp in typeInfo.DeclaredProperties)
				{
					if (tProp.CanRead && tProp.CanWrite && tProp.GetMethod != null && tProp.SetMethod != null && tProp.GetMethod.IsPublic && tProp.SetMethod.IsPublic && !tProp.GetMethod.IsStatic && !tProp.SetMethod.IsStatic)
					{
						tCategoryProps.Add(tProp.Name);
					}
				}
				foreach (string tCategory in tCategoryProps)
				{
					HistoryDataAsset tHistoryAsset = AssetManager.history_data_library.get(tCategory);
					tAsset.categories.Add(tHistoryAsset);
				}
				if (!meta_data.ContainsKey(tAsset.meta_type))
				{
					meta_data.Add(tAsset.meta_type, new ListPool<HistoryMetaDataAsset>());
				}
				meta_data[tAsset.meta_type].Add(tAsset);
			}
			foreach (KeyValuePair<MetaType, ListPool<HistoryMetaDataAsset>> keyValuePair in meta_data)
			{
				MetaType metaType;
				ListPool<HistoryMetaDataAsset> listPool;
				keyValuePair.Deconstruct(out metaType, out listPool);
				MetaType tMetaType = metaType;
				ListPool<HistoryMetaDataAsset> tList = listPool;
				HistoryMetaDataLibrary._meta_data.Add(tMetaType, tList.ToArray<HistoryMetaDataAsset>());
				HistoryMetaDataLibrary._meta_dict.Add(tMetaType.AsString(), HistoryMetaDataLibrary._meta_data[tMetaType]);
			}
		}

		// Token: 0x040030FA RID: 12538
		public static readonly Dictionary<MetaType, HistoryMetaDataAsset[]> _meta_data = new Dictionary<MetaType, HistoryMetaDataAsset[]>();

		// Token: 0x040030FB RID: 12539
		public static readonly Dictionary<string, HistoryMetaDataAsset[]> _meta_dict = new Dictionary<string, HistoryMetaDataAsset[]>();
	}
}
