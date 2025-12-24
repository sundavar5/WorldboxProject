using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

// Token: 0x0200058E RID: 1422
[Serializable]
public class MapStats
{
	// Token: 0x17000269 RID: 617
	// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0016C51E File Offset: 0x0016A71E
	[Preserve]
	[Obsolete("use .world_age_id instead", true)]
	public string era_id
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.world_age_id))
			{
				return;
			}
			this.world_age_id = value;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0016C53E File Offset: 0x0016A73E
	// (set) Token: 0x06002F1B RID: 12059 RVA: 0x0016C546 File Offset: 0x0016A746
	public long deaths { get; set; }

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06002F1C RID: 12060 RVA: 0x0016C54F File Offset: 0x0016A74F
	// (set) Token: 0x06002F1D RID: 12061 RVA: 0x0016C557 File Offset: 0x0016A757
	public long deaths_age { get; set; }

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06002F1E RID: 12062 RVA: 0x0016C560 File Offset: 0x0016A760
	// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0016C568 File Offset: 0x0016A768
	public long deaths_hunger { get; set; }

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06002F20 RID: 12064 RVA: 0x0016C571 File Offset: 0x0016A771
	// (set) Token: 0x06002F21 RID: 12065 RVA: 0x0016C579 File Offset: 0x0016A779
	public long deaths_eaten { get; set; }

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0016C582 File Offset: 0x0016A782
	// (set) Token: 0x06002F23 RID: 12067 RVA: 0x0016C58A File Offset: 0x0016A78A
	public long deaths_plague { get; set; }

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06002F24 RID: 12068 RVA: 0x0016C593 File Offset: 0x0016A793
	// (set) Token: 0x06002F25 RID: 12069 RVA: 0x0016C59B File Offset: 0x0016A79B
	public long deaths_poison { get; set; }

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06002F26 RID: 12070 RVA: 0x0016C5A4 File Offset: 0x0016A7A4
	// (set) Token: 0x06002F27 RID: 12071 RVA: 0x0016C5AC File Offset: 0x0016A7AC
	public long deaths_infection { get; set; }

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0016C5B5 File Offset: 0x0016A7B5
	// (set) Token: 0x06002F29 RID: 12073 RVA: 0x0016C5BD File Offset: 0x0016A7BD
	public long deaths_tumor { get; set; }

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06002F2A RID: 12074 RVA: 0x0016C5C6 File Offset: 0x0016A7C6
	// (set) Token: 0x06002F2B RID: 12075 RVA: 0x0016C5CE File Offset: 0x0016A7CE
	public long deaths_acid { get; set; }

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0016C5D7 File Offset: 0x0016A7D7
	// (set) Token: 0x06002F2D RID: 12077 RVA: 0x0016C5DF File Offset: 0x0016A7DF
	public long deaths_fire { get; set; }

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0016C5E8 File Offset: 0x0016A7E8
	// (set) Token: 0x06002F2F RID: 12079 RVA: 0x0016C5F0 File Offset: 0x0016A7F0
	public long deaths_divine { get; set; }

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0016C5F9 File Offset: 0x0016A7F9
	// (set) Token: 0x06002F31 RID: 12081 RVA: 0x0016C601 File Offset: 0x0016A801
	public long deaths_weapon { get; set; }

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06002F32 RID: 12082 RVA: 0x0016C60A File Offset: 0x0016A80A
	// (set) Token: 0x06002F33 RID: 12083 RVA: 0x0016C612 File Offset: 0x0016A812
	public long deaths_gravity { get; set; }

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06002F34 RID: 12084 RVA: 0x0016C61B File Offset: 0x0016A81B
	// (set) Token: 0x06002F35 RID: 12085 RVA: 0x0016C623 File Offset: 0x0016A823
	public long deaths_drowning { get; set; }

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06002F36 RID: 12086 RVA: 0x0016C62C File Offset: 0x0016A82C
	// (set) Token: 0x06002F37 RID: 12087 RVA: 0x0016C634 File Offset: 0x0016A834
	public long deaths_water { get; set; }

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06002F38 RID: 12088 RVA: 0x0016C63D File Offset: 0x0016A83D
	// (set) Token: 0x06002F39 RID: 12089 RVA: 0x0016C645 File Offset: 0x0016A845
	public long deaths_explosion { get; set; }

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06002F3A RID: 12090 RVA: 0x0016C64E File Offset: 0x0016A84E
	// (set) Token: 0x06002F3B RID: 12091 RVA: 0x0016C656 File Offset: 0x0016A856
	public long metamorphosis { get; set; }

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06002F3C RID: 12092 RVA: 0x0016C65F File Offset: 0x0016A85F
	// (set) Token: 0x06002F3D RID: 12093 RVA: 0x0016C667 File Offset: 0x0016A867
	public long evolutions { get; set; }

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06002F3E RID: 12094 RVA: 0x0016C670 File Offset: 0x0016A870
	// (set) Token: 0x06002F3F RID: 12095 RVA: 0x0016C678 File Offset: 0x0016A878
	public long deaths_other { get; set; }

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06002F40 RID: 12096 RVA: 0x0016C681 File Offset: 0x0016A881
	// (set) Token: 0x06002F41 RID: 12097 RVA: 0x0016C689 File Offset: 0x0016A889
	public long deaths_smile { get; set; }

	// Token: 0x06002F42 RID: 12098 RVA: 0x0016C694 File Offset: 0x0016A894
	public MapStats()
	{
		this.checkDefault();
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x0016C790 File Offset: 0x0016A990
	private void checkDefault()
	{
		if (this.world_ages_slots == null)
		{
			this.world_ages_slots = new string[8];
		}
		if (string.IsNullOrEmpty(this.player_name))
		{
			this.player_name = "The Creator";
		}
		if (string.IsNullOrEmpty(this.player_mood))
		{
			this.setDefaultMood();
		}
		if (this.custom_data == null)
		{
			this.custom_data = new SaveCustomData();
		}
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x0016C7F0 File Offset: 0x0016A9F0
	public void generateLifeDNA()
	{
		CultureInfo tCulture = CultureInfo.InvariantCulture;
		long tDateTimeAsLong = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHH", tCulture));
		this.life_dna = tDateTimeAsLong;
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x0016C823 File Offset: 0x0016AA23
	internal void updateStatsForPanel(float pElapsed)
	{
		if (this._timer_stats > 0f)
		{
			this._timer_stats -= pElapsed;
			return;
		}
		this._timer_stats = 0.1f;
		this.recalcCounters();
	}

	// Token: 0x06002F46 RID: 12102 RVA: 0x0016C854 File Offset: 0x0016AA54
	internal void updateWorldTime(float pElapsed)
	{
		this.world_time += (double)pElapsed;
		int tYearNow = Date.getCurrentYear();
		int tMonthNow = Date.getCurrentMonth();
		if (this._last_year != tYearNow)
		{
			World.world.updateObjectAge();
		}
		if (this._last_year != tYearNow)
		{
			this._last_month = -1;
		}
		this._last_year = tYearNow;
		this._last_month = tMonthNow;
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x0016C8AD File Offset: 0x0016AAAD
	public void load()
	{
		this.checkDefault();
		this._last_year = Date.getCurrentYear();
		this._last_month = Date.getCurrentMonth();
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x0016C8CC File Offset: 0x0016AACC
	private void recalcCounters()
	{
		this.current_infected = 0L;
		this.current_mobs = 0L;
		this.current_houses = 0L;
		this.current_vegetation = 0L;
		this.current_infected_plague = 0L;
		List<Actor> tActorList = World.world.units.getSimpleList();
		int i = 0;
		int tLen = tActorList.Count;
		while (i < tLen)
		{
			Actor tActor = tActorList[i];
			if (tActor.hasTrait("plague"))
			{
				this.current_infected_plague += 1L;
			}
			if (tActor.isSick())
			{
				this.current_infected += 1L;
			}
			if (tActor.asset.count_as_unit && !tActor.isSapient())
			{
				this.current_mobs += 1L;
			}
			i++;
		}
		List<Building> tBuildingList = World.world.buildings.getSimpleList();
		int j = 0;
		int tLen2 = tBuildingList.Count;
		while (j < tLen2)
		{
			Building tBuilding = tBuildingList[j];
			if (tBuilding.isCiv())
			{
				this.current_houses += 1L;
			}
			else if (tBuilding.asset.is_vegetation)
			{
				this.current_vegetation += 1L;
			}
			j++;
		}
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x0016C9F4 File Offset: 0x0016ABF4
	public long getNextId(string pType)
	{
		long tResult = 0L;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(pType);
		if (num <= 1248504905U)
		{
			if (num <= 400737157U)
			{
				if (num <= 230981954U)
				{
					if (num != 122451650U)
					{
						if (num == 230981954U)
						{
							if (pType == "city")
							{
								long num2 = this.id_city;
								this.id_city = num2 + 1L;
								return num2;
							}
						}
					}
					else if (pType == "kingdom")
					{
						long num2 = this.id_kingdom;
						this.id_kingdom = num2 + 1L;
						return num2;
					}
				}
				else if (num != 256343706U)
				{
					if (num == 400737157U)
					{
						if (pType == "war")
						{
							long num2 = this.id_war;
							this.id_war = num2 + 1L;
							return num2;
						}
					}
				}
				else if (pType == "projectile")
				{
					long num2 = this.id_projectile;
					this.id_projectile = num2 + 1L;
					return num2;
				}
			}
			else if (num <= 624960409U)
			{
				if (num != 487079104U)
				{
					if (num == 624960409U)
					{
						if (pType == "diplomacy")
						{
							long num2 = this.id_diplomacy;
							this.id_diplomacy = num2 + 1L;
							return num2;
						}
					}
				}
				else if (pType == "alliance")
				{
					long num2 = this.id_alliance;
					this.id_alliance = num2 + 1L;
					return num2;
				}
			}
			else if (num != 954139509U)
			{
				if (num != 1092942429U)
				{
					if (num == 1248504905U)
					{
						if (pType == "subspecies")
						{
							long num2 = this.id_subspecies;
							this.id_subspecies = num2 + 1L;
							return num2;
						}
					}
				}
				else if (pType == "clan")
				{
					long num2 = this.id_clan;
					this.id_clan = num2 + 1L;
					return num2;
				}
			}
			else if (pType == "building")
			{
				long num2 = this.id_building;
				this.id_building = num2 + 1L;
				return num2;
			}
		}
		else if (num <= 3119462523U)
		{
			if (num <= 1539931302U)
			{
				if (num != 1377633321U)
				{
					if (num == 1539931302U)
					{
						if (pType == "religion")
						{
							long num2 = this.id_religion;
							this.id_religion = num2 + 1L;
							return num2;
						}
					}
				}
				else if (pType == "family")
				{
					long num2 = this.id_family;
					this.id_family = num2 + 1L;
					return num2;
				}
			}
			else if (num != 2671260646U)
			{
				if (num != 2734401060U)
				{
					if (num == 3119462523U)
					{
						if (pType == "language")
						{
							long num2 = this.id_language;
							this.id_language = num2 + 1L;
							return num2;
						}
					}
				}
				else if (pType == "army")
				{
					long num2 = this.id_army;
					this.id_army = num2 + 1L;
					return num2;
				}
			}
			else if (pType == "item")
			{
				long num2 = this.id_item;
				this.id_item = num2 + 1L;
				return num2;
			}
		}
		else if (num <= 3250964130U)
		{
			if (num != 3247737400U)
			{
				if (num == 3250964130U)
				{
					if (pType == "plot")
					{
						long num2 = this.id_plot;
						this.id_plot = num2 + 1L;
						return num2;
					}
				}
			}
			else if (pType == "book")
			{
				long num2 = this.id_book;
				this.id_book = num2 + 1L;
				return num2;
			}
		}
		else if (num != 3303907537U)
		{
			if (num != 3657425719U)
			{
				if (num == 3904182791U)
				{
					if (pType == "unit")
					{
						long num2 = this.id_unit;
						this.id_unit = num2 + 1L;
						return num2;
					}
				}
			}
			else if (pType == "statuses")
			{
				long num2 = this.id_status;
				this.id_status = num2 + 1L;
				return num2;
			}
		}
		else if (pType == "culture")
		{
			long num2 = this.id_culture;
			this.id_culture = num2 + 1L;
			return num2;
		}
		Debug.LogError("NO pType for id " + pType);
		return tResult;
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x0016CE8C File Offset: 0x0016B08C
	public static string formatId(string pType, long pID)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(pType);
		if (num <= 1248504905U)
		{
			if (num <= 400737157U)
			{
				if (num <= 230981954U)
				{
					if (num != 122451650U)
					{
						if (num == 230981954U)
						{
							if (pType == "city")
							{
								return "c_" + pID.ToString();
							}
						}
					}
					else if (pType == "kingdom")
					{
						return "k_" + pID.ToString();
					}
				}
				else if (num != 256343706U)
				{
					if (num == 400737157U)
					{
						if (pType == "war")
						{
							return "w_" + pID.ToString();
						}
					}
				}
				else if (pType == "projectile")
				{
					return "pr_" + pID.ToString();
				}
			}
			else if (num <= 624960409U)
			{
				if (num != 487079104U)
				{
					if (num == 624960409U)
					{
						if (pType == "diplomacy")
						{
							return "d_" + pID.ToString();
						}
					}
				}
				else if (pType == "alliance")
				{
					return "a_" + pID.ToString();
				}
			}
			else if (num != 954139509U)
			{
				if (num != 1092942429U)
				{
					if (num == 1248504905U)
					{
						if (pType == "subspecies")
						{
							return "sp_" + pID.ToString();
						}
					}
				}
				else if (pType == "clan")
				{
					return "cl_" + pID.ToString();
				}
			}
			else if (pType == "building")
			{
				return "b_" + pID.ToString();
			}
		}
		else if (num <= 3119462523U)
		{
			if (num <= 1539931302U)
			{
				if (num != 1377633321U)
				{
					if (num == 1539931302U)
					{
						if (pType == "religion")
						{
							return "rel_" + pID.ToString();
						}
					}
				}
				else if (pType == "family")
				{
					return "fa_" + pID.ToString();
				}
			}
			else if (num != 2671260646U)
			{
				if (num != 2734401060U)
				{
					if (num == 3119462523U)
					{
						if (pType == "language")
						{
							return "lang_" + pID.ToString();
						}
					}
				}
				else if (pType == "army")
				{
					return "army_" + pID.ToString();
				}
			}
			else if (pType == "item")
			{
				return "it_" + pID.ToString();
			}
		}
		else if (num <= 3250964130U)
		{
			if (num != 3247737400U)
			{
				if (num == 3250964130U)
				{
					if (pType == "plot")
					{
						return "p_" + pID.ToString();
					}
				}
			}
			else if (pType == "book")
			{
				return "bo_" + pID.ToString();
			}
		}
		else if (num != 3303907537U)
		{
			if (num != 3657425719U)
			{
				if (num == 3904182791U)
				{
					if (pType == "unit")
					{
						return "u_" + pID.ToString();
					}
				}
			}
			else if (pType == "statuses")
			{
				return "st_" + pID.ToString();
			}
		}
		else if (pType == "culture")
		{
			return "c_" + pID.ToString();
		}
		Debug.LogError("NO pType for id " + pType);
		return "???_" + pID.ToString();
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x0016D2D4 File Offset: 0x0016B4D4
	public void debug(DebugTool pTool)
	{
		pTool.setText("(d)worldTime:", this.world_time, 0f, false, 0L, false, false, "");
		pTool.setText("(f)worldTime:", this.getWorldTime(), 0f, false, 0L, false, false, "");
		pTool.setText("cur month:", Date.getCurrentMonth(), 0f, false, 0L, false, false, "");
		pTool.setText("cur year:", Date.getCurrentYear(), 0f, false, 0L, false, false, "");
		pTool.setText("last_year:", this._last_year, 0f, false, 0L, false, false, "");
		pTool.setText("last_month:", this._last_month, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("months since 0:", Date.getMonthsSince(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("years since 0:", Date.getYearsSince(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("months since now:", Date.getMonthsSince(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setText("years since now:", Date.getYearsSince(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setText("month time:", Date.getMonthTime(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("getDate 0:", Date.getDate(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("getYearDate 0:", Date.getYearDate(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("getYear 0:", Date.getYear(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("getYear0 0:", Date.getYear0(0.0), 0f, false, 0L, false, false, "");
		pTool.setText("getDate now:", Date.getDate(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setText("getYearDate now:", Date.getYearDate(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setText("getYear now:", Date.getYear(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setText("getYear0 now:", Date.getYear0(this.world_time), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("max_float:", float.MaxValue, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x0016D60D File Offset: 0x0016B80D
	public float getWorldTime()
	{
		return (float)this.world_time;
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06002F4D RID: 12109 RVA: 0x0016D616 File Offset: 0x0016B816
	[JsonIgnore]
	public int year
	{
		get
		{
			return this._last_year;
		}
	}

	// Token: 0x06002F4E RID: 12110 RVA: 0x0016D620 File Offset: 0x0016B820
	public void initNewWorld()
	{
		this.generateLifeDNA();
		this.generatePlayerName();
		this.setDefaultMood();
		this.name = NameGenerator.getName("world_name", ActorSex.Male, false, null, null, false);
		AssetManager.gene_library.regenerateBasicDNACodesWithLifeSeed(this.life_dna);
	}

	// Token: 0x06002F4F RID: 12111 RVA: 0x0016D66C File Offset: 0x0016B86C
	private void generatePlayerName()
	{
		this.player_name = NameGenerator.getName("player_name", ActorSex.Male, false, null, null, true);
	}

	// Token: 0x06002F50 RID: 12112 RVA: 0x0016D696 File Offset: 0x0016B896
	private void setDefaultMood()
	{
		this.player_mood = "serene";
	}

	// Token: 0x1700027F RID: 639
	// (set) Token: 0x06002F51 RID: 12113 RVA: 0x0016D6A3 File Offset: 0x0016B8A3
	[Preserve]
	[JsonProperty("year")]
	[Obsolete("use .world_time instead", true)]
	public int year_obsolete
	{
		set
		{
			if (value == 0)
			{
				return;
			}
			this.world_time += (double)((float)value * 60f);
		}
	}

	// Token: 0x17000280 RID: 640
	// (set) Token: 0x06002F52 RID: 12114 RVA: 0x0016D6BF File Offset: 0x0016B8BF
	[Preserve]
	[JsonProperty("month")]
	[Obsolete("use .world_time instead", true)]
	public int month_obsolete
	{
		set
		{
			if (value == 0)
			{
				return;
			}
			this.world_time += (double)((float)value * 5f);
		}
	}

	// Token: 0x17000281 RID: 641
	// (set) Token: 0x06002F53 RID: 12115 RVA: 0x0016D6DB File Offset: 0x0016B8DB
	[Preserve]
	[JsonProperty("worldTime")]
	[Obsolete("use .world_time instead", true)]
	public double worldTime_obsolete
	{
		set
		{
			if (value == 0.0)
			{
				return;
			}
			this.world_time += value;
		}
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x0016D6F8 File Offset: 0x0016B8F8
	public ArchitectMood getArchitectMood()
	{
		if (string.IsNullOrEmpty(this.player_mood))
		{
			this.player_mood = "serene";
		}
		ArchitectMood tMood = AssetManager.architect_mood_library.get(this.player_mood);
		if (tMood == null)
		{
			tMood = AssetManager.architect_mood_library.get("serene");
		}
		return tMood;
	}

	// Token: 0x0400232D RID: 9005
	public string name = "WorldBox";

	// Token: 0x0400232E RID: 9006
	public string description = "";

	// Token: 0x0400232F RID: 9007
	public string player_name;

	// Token: 0x04002330 RID: 9008
	public string player_mood;

	// Token: 0x04002331 RID: 9009
	public SaveCustomData custom_data;

	// Token: 0x04002332 RID: 9010
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public double world_time;

	// Token: 0x04002333 RID: 9011
	public int history_current_year = -1;

	// Token: 0x04002334 RID: 9012
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string world_age_id;

	// Token: 0x04002335 RID: 9013
	public int world_age_slot_index;

	// Token: 0x04002336 RID: 9014
	public double world_age_started_at;

	// Token: 0x04002337 RID: 9015
	public double same_world_age_started_at;

	// Token: 0x04002338 RID: 9016
	public float current_world_ages_duration;

	// Token: 0x04002339 RID: 9017
	public float current_age_progress;

	// Token: 0x0400233A RID: 9018
	public bool is_world_ages_paused;

	// Token: 0x0400233B RID: 9019
	[DefaultValue(1f)]
	public float world_ages_speed_multiplier = 1f;

	// Token: 0x0400233C RID: 9020
	public string[] world_ages_slots;

	// Token: 0x04002351 RID: 9041
	public long housesBuilt;

	// Token: 0x04002352 RID: 9042
	public long housesDestroyed;

	// Token: 0x04002353 RID: 9043
	public long population;

	// Token: 0x04002354 RID: 9044
	public long creaturesBorn;

	// Token: 0x04002355 RID: 9045
	public long creaturesCreated;

	// Token: 0x04002356 RID: 9046
	public long subspeciesCreated;

	// Token: 0x04002357 RID: 9047
	public long subspeciesExtinct;

	// Token: 0x04002358 RID: 9048
	public long languagesCreated;

	// Token: 0x04002359 RID: 9049
	public long languagesForgotten;

	// Token: 0x0400235A RID: 9050
	public long booksWritten;

	// Token: 0x0400235B RID: 9051
	public long booksRead;

	// Token: 0x0400235C RID: 9052
	public long booksBurnt;

	// Token: 0x0400235D RID: 9053
	public long culturesCreated;

	// Token: 0x0400235E RID: 9054
	public long culturesForgotten;

	// Token: 0x0400235F RID: 9055
	public long religionsCreated;

	// Token: 0x04002360 RID: 9056
	public long religionsForgotten;

	// Token: 0x04002361 RID: 9057
	public long kingdomsCreated;

	// Token: 0x04002362 RID: 9058
	public long kingdomsDestroyed;

	// Token: 0x04002363 RID: 9059
	public long citiesCreated;

	// Token: 0x04002364 RID: 9060
	public long citiesConquered;

	// Token: 0x04002365 RID: 9061
	public long citiesRebelled;

	// Token: 0x04002366 RID: 9062
	public long citiesDestroyed;

	// Token: 0x04002367 RID: 9063
	public long alliancesMade;

	// Token: 0x04002368 RID: 9064
	public long alliancesDissolved;

	// Token: 0x04002369 RID: 9065
	public long warsStarted;

	// Token: 0x0400236A RID: 9066
	public long peacesMade;

	// Token: 0x0400236B RID: 9067
	public long familiesCreated;

	// Token: 0x0400236C RID: 9068
	public long armiesCreated;

	// Token: 0x0400236D RID: 9069
	public long armiesDestroyed;

	// Token: 0x0400236E RID: 9070
	public long familiesDestroyed;

	// Token: 0x0400236F RID: 9071
	public long clansCreated;

	// Token: 0x04002370 RID: 9072
	public long clansDestroyed;

	// Token: 0x04002371 RID: 9073
	public long plotsStarted;

	// Token: 0x04002372 RID: 9074
	public long plotsSucceeded;

	// Token: 0x04002373 RID: 9075
	public long plotsForgotten;

	// Token: 0x04002374 RID: 9076
	public double exploding_mushrooms_enabled_at;

	// Token: 0x04002375 RID: 9077
	[DefaultValue(1L)]
	public long id_unit = 1L;

	// Token: 0x04002376 RID: 9078
	[DefaultValue(1L)]
	public long id_building = 1L;

	// Token: 0x04002377 RID: 9079
	[DefaultValue(1L)]
	public long id_kingdom = 1L;

	// Token: 0x04002378 RID: 9080
	[DefaultValue(1L)]
	public long id_city = 1L;

	// Token: 0x04002379 RID: 9081
	[DefaultValue(1L)]
	public long id_culture = 1L;

	// Token: 0x0400237A RID: 9082
	[DefaultValue(1L)]
	public long id_clan = 1L;

	// Token: 0x0400237B RID: 9083
	[DefaultValue(1L)]
	public long id_alliance = 1L;

	// Token: 0x0400237C RID: 9084
	[DefaultValue(1L)]
	public long id_war = 1L;

	// Token: 0x0400237D RID: 9085
	[DefaultValue(1L)]
	public long id_projectile = 1L;

	// Token: 0x0400237E RID: 9086
	[DefaultValue(1L)]
	public long id_status = 1L;

	// Token: 0x0400237F RID: 9087
	[DefaultValue(1L)]
	public long id_plot = 1L;

	// Token: 0x04002380 RID: 9088
	[DefaultValue(1L)]
	public long id_book = 1L;

	// Token: 0x04002381 RID: 9089
	[DefaultValue(1L)]
	public long id_subspecies = 1L;

	// Token: 0x04002382 RID: 9090
	[DefaultValue(1L)]
	public long id_family = 1L;

	// Token: 0x04002383 RID: 9091
	[DefaultValue(1L)]
	public long id_army = 1L;

	// Token: 0x04002384 RID: 9092
	[DefaultValue(1L)]
	public long id_language = 1L;

	// Token: 0x04002385 RID: 9093
	[DefaultValue(1L)]
	public long id_religion = 1L;

	// Token: 0x04002386 RID: 9094
	[DefaultValue(1L)]
	public long id_item = 1L;

	// Token: 0x04002387 RID: 9095
	[DefaultValue(1L)]
	public long id_diplomacy = 1L;

	// Token: 0x04002388 RID: 9096
	[DefaultValue(1L)]
	public long life_dna = 1L;

	// Token: 0x04002389 RID: 9097
	[NonSerialized]
	public long current_infected;

	// Token: 0x0400238A RID: 9098
	[NonSerialized]
	public long current_mobs;

	// Token: 0x0400238B RID: 9099
	[NonSerialized]
	public long current_houses;

	// Token: 0x0400238C RID: 9100
	[NonSerialized]
	public long current_vegetation;

	// Token: 0x0400238D RID: 9101
	[NonSerialized]
	public long current_infected_plague;

	// Token: 0x0400238E RID: 9102
	private int _last_year = -1;

	// Token: 0x0400238F RID: 9103
	private int _last_month = -1;

	// Token: 0x04002390 RID: 9104
	private float _timer_stats = 0.1f;

	// Token: 0x04002391 RID: 9105
	public static string[] possible_formats = new string[]
	{
		"pr_",
		"st_",
		"w_",
		"a_",
		"c_",
		"u_",
		"b_",
		"k_",
		"c_",
		"cl_",
		"p_",
		"bo_",
		"sp_",
		"lang_",
		"rel_",
		"it_",
		"f_",
		"fa_",
		"army_",
		"d_"
	};
}
