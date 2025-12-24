using System;
using System.ComponentModel;

// Token: 0x0200077D RID: 1917
public class TooltipData : IDisposable
{
	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x001A56E9 File Offset: 0x001A38E9
	// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x001A56E0 File Offset: 0x001A38E0
	public string tip_name
	{
		get
		{
			return this._tip_name;
		}
		set
		{
			this._tip_name = value;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x001A56FA File Offset: 0x001A38FA
	// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x001A56F1 File Offset: 0x001A38F1
	public string tip_description
	{
		get
		{
			return this._tip_description;
		}
		set
		{
			this._tip_description = value;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x001A570B File Offset: 0x001A390B
	// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x001A5702 File Offset: 0x001A3902
	public string tip_description_2
	{
		get
		{
			return this._tip_description_2;
		}
		set
		{
			this._tip_description_2 = value;
		}
	}

	// Token: 0x06003CD7 RID: 15575 RVA: 0x001A5714 File Offset: 0x001A3914
	public void Dispose()
	{
		CustomDataContainer<long> customDataContainer = this.custom_data_long;
		if (customDataContainer != null)
		{
			customDataContainer.Dispose();
		}
		CustomDataContainer<int> customDataContainer2 = this.custom_data_int;
		if (customDataContainer2 != null)
		{
			customDataContainer2.Dispose();
		}
		CustomDataContainer<float> customDataContainer3 = this.custom_data_float;
		if (customDataContainer3 != null)
		{
			customDataContainer3.Dispose();
		}
		CustomDataContainer<bool> customDataContainer4 = this.custom_data_bool;
		if (customDataContainer4 != null)
		{
			customDataContainer4.Dispose();
		}
		CustomDataContainer<string> customDataContainer5 = this.custom_data_string;
		if (customDataContainer5 != null)
		{
			customDataContainer5.Dispose();
		}
		this.war = null;
		this.army = null;
		this.subspecies = null;
		this.family = null;
		this.language = null;
		this.culture = null;
		this.religion = null;
		this.city = null;
		this.clan = null;
		this.kingdom = null;
		this.alliance = null;
		this.plot = null;
		this.plot_asset = null;
		this.book = null;
		this.resource = null;
		this.item = null;
		this.item_asset = null;
		this.actor = null;
		this.trait = null;
		this.actor_asset = null;
		this.status = null;
		this.kingdom_asset = null;
		this.kingdom_trait = null;
		this.culture_trait = null;
		this.language_trait = null;
		this.subspecies_trait = null;
		this.clan_trait = null;
		this.religion_trait = null;
		this.onomastics_asset = null;
		this.onomastics_data = null;
		this.nano_object = null;
		this.chromosome = null;
		this.gene = null;
		this.locus = null;
		this.neuron = null;
		this.map_meta = null;
		this.power = null;
		this.achievement = null;
		this.world_law = null;
		ListPool<NameEntry> listPool = this.past_names;
		if (listPool != null)
		{
			listPool.Dispose();
		}
		this.past_names = null;
		ListPool<LeaderEntry> listPool2 = this.past_rulers;
		if (listPool2 != null)
		{
			listPool2.Dispose();
		}
		this.past_rulers = null;
		this.game_language_asset = null;
		this.is_editor_augmentation_button = false;
		this.meta_type = MetaType.None;
		this.sound_allowed = true;
	}

	// Token: 0x04002C2B RID: 11307
	[NonSerialized]
	private string _tip_name;

	// Token: 0x04002C2C RID: 11308
	[NonSerialized]
	private string _tip_description;

	// Token: 0x04002C2D RID: 11309
	[NonSerialized]
	private string _tip_description_2;

	// Token: 0x04002C2E RID: 11310
	public War war;

	// Token: 0x04002C2F RID: 11311
	public Army army;

	// Token: 0x04002C30 RID: 11312
	public Subspecies subspecies;

	// Token: 0x04002C31 RID: 11313
	public Family family;

	// Token: 0x04002C32 RID: 11314
	public Language language;

	// Token: 0x04002C33 RID: 11315
	public Culture culture;

	// Token: 0x04002C34 RID: 11316
	public Religion religion;

	// Token: 0x04002C35 RID: 11317
	public City city;

	// Token: 0x04002C36 RID: 11318
	public Clan clan;

	// Token: 0x04002C37 RID: 11319
	public Kingdom kingdom;

	// Token: 0x04002C38 RID: 11320
	public Alliance alliance;

	// Token: 0x04002C39 RID: 11321
	public Plot plot;

	// Token: 0x04002C3A RID: 11322
	public PlotAsset plot_asset;

	// Token: 0x04002C3B RID: 11323
	public Book book;

	// Token: 0x04002C3C RID: 11324
	public ResourceAsset resource;

	// Token: 0x04002C3D RID: 11325
	public Item item;

	// Token: 0x04002C3E RID: 11326
	public EquipmentAsset item_asset;

	// Token: 0x04002C3F RID: 11327
	public bool is_editor_augmentation_button;

	// Token: 0x04002C40 RID: 11328
	public Actor actor;

	// Token: 0x04002C41 RID: 11329
	public ActorTrait trait;

	// Token: 0x04002C42 RID: 11330
	public ActorAsset actor_asset;

	// Token: 0x04002C43 RID: 11331
	public Status status;

	// Token: 0x04002C44 RID: 11332
	public KingdomAsset kingdom_asset;

	// Token: 0x04002C45 RID: 11333
	public KingdomTrait kingdom_trait;

	// Token: 0x04002C46 RID: 11334
	public CultureTrait culture_trait;

	// Token: 0x04002C47 RID: 11335
	public LanguageTrait language_trait;

	// Token: 0x04002C48 RID: 11336
	public SubspeciesTrait subspecies_trait;

	// Token: 0x04002C49 RID: 11337
	public ClanTrait clan_trait;

	// Token: 0x04002C4A RID: 11338
	public ReligionTrait religion_trait;

	// Token: 0x04002C4B RID: 11339
	public OnomasticsAsset onomastics_asset;

	// Token: 0x04002C4C RID: 11340
	public OnomasticsData onomastics_data;

	// Token: 0x04002C4D RID: 11341
	public NanoObject nano_object;

	// Token: 0x04002C4E RID: 11342
	public Chromosome chromosome;

	// Token: 0x04002C4F RID: 11343
	public GeneAsset gene;

	// Token: 0x04002C50 RID: 11344
	public LocusElement locus;

	// Token: 0x04002C51 RID: 11345
	public NeuronElement neuron;

	// Token: 0x04002C52 RID: 11346
	public MapMetaData map_meta;

	// Token: 0x04002C53 RID: 11347
	public GodPower power;

	// Token: 0x04002C54 RID: 11348
	public Achievement achievement;

	// Token: 0x04002C55 RID: 11349
	public WorldLawAsset world_law;

	// Token: 0x04002C56 RID: 11350
	public ListPool<NameEntry> past_names;

	// Token: 0x04002C57 RID: 11351
	public ListPool<LeaderEntry> past_rulers;

	// Token: 0x04002C58 RID: 11352
	public GameLanguageAsset game_language_asset;

	// Token: 0x04002C59 RID: 11353
	public MetaType meta_type;

	// Token: 0x04002C5A RID: 11354
	public CustomDataContainer<long> custom_data_long;

	// Token: 0x04002C5B RID: 11355
	public CustomDataContainer<int> custom_data_int;

	// Token: 0x04002C5C RID: 11356
	public CustomDataContainer<float> custom_data_float;

	// Token: 0x04002C5D RID: 11357
	public CustomDataContainer<bool> custom_data_bool;

	// Token: 0x04002C5E RID: 11358
	public CustomDataContainer<string> custom_data_string;

	// Token: 0x04002C5F RID: 11359
	[DefaultValue(1f)]
	public float tooltip_scale = 1f;

	// Token: 0x04002C60 RID: 11360
	public bool sound_allowed = true;

	// Token: 0x04002C61 RID: 11361
	public bool is_sim_tooltip;
}
