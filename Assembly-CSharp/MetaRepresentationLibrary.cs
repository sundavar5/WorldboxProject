using System;
using System.Collections.Generic;

// Token: 0x020002AB RID: 683
public class MetaRepresentationLibrary : AssetLibrary<MetaRepresentationAsset>
{
	// Token: 0x0600198B RID: 6539 RVA: 0x000F0C6C File Offset: 0x000EEE6C
	public override void init()
	{
		base.init();
		MetaRepresentationAsset metaRepresentationAsset = new MetaRepresentationAsset();
		metaRepresentationAsset.id = "alliance";
		metaRepresentationAsset.meta_type = MetaType.Alliance;
		metaRepresentationAsset.title_locale = "statistics_breakdown_alliances";
		metaRepresentationAsset.icon_getter = ((IMetaObject _) => "iconAlliance");
		metaRepresentationAsset.check_has_meta = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.isCiv() && pActor.kingdom.hasAlliance());
		metaRepresentationAsset.meta_getter = ((Actor pActor) => pActor.kingdom.getAlliance());
		metaRepresentationAsset.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.kingdom.getAlliance()
		});
		metaRepresentationAsset.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset.general_icon_path = "iconAlliance";
		metaRepresentationAsset.show_species_icon = false;
		this.add(metaRepresentationAsset);
		MetaRepresentationAsset metaRepresentationAsset2 = new MetaRepresentationAsset();
		metaRepresentationAsset2.id = "army";
		metaRepresentationAsset2.meta_type = MetaType.Army;
		metaRepresentationAsset2.title_locale = "statistics_breakdown_armies";
		metaRepresentationAsset2.icon_getter = ((IMetaObject _) => "iconArmy");
		metaRepresentationAsset2.check_has_meta = ((Actor pActor) => pActor.hasArmy());
		metaRepresentationAsset2.meta_getter = ((Actor pActor) => pActor.city.getArmy());
		metaRepresentationAsset2.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.city.getArmy()
		});
		metaRepresentationAsset2.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset2.general_icon_path = "iconArmy";
		this.add(metaRepresentationAsset2);
		MetaRepresentationAsset metaRepresentationAsset3 = new MetaRepresentationAsset();
		metaRepresentationAsset3.id = "city";
		metaRepresentationAsset3.meta_type = MetaType.City;
		metaRepresentationAsset3.title_locale = "statistics_breakdown_cities";
		metaRepresentationAsset3.icon_getter = ((IMetaObject _) => "iconCity");
		metaRepresentationAsset3.check_has_meta = ((Actor pActor) => pActor.hasCity());
		metaRepresentationAsset3.meta_getter = ((Actor pActor) => pActor.city);
		metaRepresentationAsset3.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.city
		});
		metaRepresentationAsset3.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset3.general_icon_path = "iconCity";
		this.add(metaRepresentationAsset3);
		MetaRepresentationAsset metaRepresentationAsset4 = new MetaRepresentationAsset();
		metaRepresentationAsset4.id = "clan";
		metaRepresentationAsset4.meta_type = MetaType.Clan;
		metaRepresentationAsset4.title_locale = "statistics_breakdown_clans";
		metaRepresentationAsset4.icon_getter = ((IMetaObject _) => "iconClan");
		metaRepresentationAsset4.check_has_meta = ((Actor pActor) => pActor.hasClan());
		metaRepresentationAsset4.meta_getter = ((Actor pActor) => pActor.clan);
		metaRepresentationAsset4.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.clan
		});
		metaRepresentationAsset4.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset4.general_icon_path = "iconClan";
		this.add(metaRepresentationAsset4);
		MetaRepresentationAsset metaRepresentationAsset5 = new MetaRepresentationAsset();
		metaRepresentationAsset5.id = "culture";
		metaRepresentationAsset5.meta_type = MetaType.Culture;
		metaRepresentationAsset5.title_locale = "statistics_breakdown_cultures";
		metaRepresentationAsset5.icon_getter = ((IMetaObject _) => "iconCulture");
		metaRepresentationAsset5.check_has_meta = ((Actor pActor) => pActor.hasCulture());
		metaRepresentationAsset5.meta_getter = ((Actor pActor) => pActor.culture);
		metaRepresentationAsset5.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.culture
		});
		metaRepresentationAsset5.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset5.general_icon_path = "iconCulture";
		metaRepresentationAsset5.show_none_percent = true;
		this.add(metaRepresentationAsset5);
		MetaRepresentationAsset metaRepresentationAsset6 = new MetaRepresentationAsset();
		metaRepresentationAsset6.id = "kingdom";
		metaRepresentationAsset6.meta_type = MetaType.Kingdom;
		metaRepresentationAsset6.title_locale = "statistics_breakdown_kingdoms";
		metaRepresentationAsset6.icon_getter = ((IMetaObject _) => "iconKingdom");
		metaRepresentationAsset6.check_has_meta = ((Actor pActor) => pActor.hasKingdom() && pActor.kingdom.isCiv());
		metaRepresentationAsset6.meta_getter = ((Actor pActor) => pActor.kingdom);
		metaRepresentationAsset6.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.kingdom
		});
		metaRepresentationAsset6.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset6.general_icon_path = "iconKingdom";
		this.add(metaRepresentationAsset6);
		MetaRepresentationAsset metaRepresentationAsset7 = new MetaRepresentationAsset();
		metaRepresentationAsset7.id = "language";
		metaRepresentationAsset7.meta_type = MetaType.Language;
		metaRepresentationAsset7.title_locale = "statistics_breakdown_languages";
		metaRepresentationAsset7.icon_getter = ((IMetaObject _) => "iconLanguage");
		metaRepresentationAsset7.check_has_meta = ((Actor pActor) => pActor.hasLanguage());
		metaRepresentationAsset7.meta_getter = ((Actor pActor) => pActor.language);
		metaRepresentationAsset7.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.language
		});
		metaRepresentationAsset7.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset7.general_icon_path = "iconLanguage";
		metaRepresentationAsset7.show_none_percent = true;
		this.add(metaRepresentationAsset7);
		MetaRepresentationAsset metaRepresentationAsset8 = new MetaRepresentationAsset();
		metaRepresentationAsset8.id = "religion";
		metaRepresentationAsset8.meta_type = MetaType.Religion;
		metaRepresentationAsset8.title_locale = "statistics_breakdown_religions";
		metaRepresentationAsset8.icon_getter = ((IMetaObject _) => "iconReligion");
		metaRepresentationAsset8.check_has_meta = ((Actor pActor) => pActor.hasReligion());
		metaRepresentationAsset8.meta_getter = ((Actor pActor) => pActor.religion);
		metaRepresentationAsset8.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.religion
		});
		metaRepresentationAsset8.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset8.general_icon_path = "iconReligion";
		metaRepresentationAsset8.show_none_percent = true;
		this.add(metaRepresentationAsset8);
		MetaRepresentationAsset metaRepresentationAsset9 = new MetaRepresentationAsset();
		metaRepresentationAsset9.id = "subspecies";
		metaRepresentationAsset9.meta_type = MetaType.Subspecies;
		metaRepresentationAsset9.title_locale = "statistics_breakdown_subspecies";
		metaRepresentationAsset9.icon_getter = ((IMetaObject pMeta) => pMeta.getActorAsset().icon);
		metaRepresentationAsset9.check_has_meta = ((Actor pActor) => pActor.hasSubspecies());
		metaRepresentationAsset9.meta_getter = ((Actor pActor) => pActor.subspecies);
		metaRepresentationAsset9.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>
		{
			pActor.subspecies
		});
		metaRepresentationAsset9.world_units_getter = (() => World.world.units.units_only_alive);
		metaRepresentationAsset9.general_icon_path = "iconSubspecies";
		metaRepresentationAsset9.show_species_icon = false;
		metaRepresentationAsset9.show_none_percent_for_total = false;
		this.add(metaRepresentationAsset9);
		MetaRepresentationAsset metaRepresentationAsset10 = new MetaRepresentationAsset();
		metaRepresentationAsset10.id = "war";
		metaRepresentationAsset10.meta_type = MetaType.War;
		metaRepresentationAsset10.title_locale = "statistics_breakdown_wars";
		metaRepresentationAsset10.icon_getter = ((IMetaObject _) => "iconWar");
		metaRepresentationAsset10.check_has_meta = delegate(Actor pActor)
		{
			if (!pActor.hasKingdom() || !pActor.kingdom.isCiv())
			{
				return false;
			}
			using (IEnumerator<War> enumerator = pActor.kingdom.getWars(false).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.hasEnded())
					{
						return true;
					}
				}
			}
			return false;
		};
		metaRepresentationAsset10.meta_getter_total = ((Actor pActor) => new ListPool<IMetaObject>(pActor.kingdom.getWars(false)));
		metaRepresentationAsset10.world_units_getter = (() => World.world.units.units_only_civ);
		metaRepresentationAsset10.general_icon_path = "iconWar";
		metaRepresentationAsset10.show_species_icon = false;
		this.add(metaRepresentationAsset10);
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x000F15D0 File Offset: 0x000EF7D0
	public MetaRepresentationAsset getAsset(MetaType pType)
	{
		return this.get(pType.AsString());
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x000F15DE File Offset: 0x000EF7DE
	public bool hasAsset(MetaType pType)
	{
		return this.has(pType.AsString());
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x000F15EC File Offset: 0x000EF7EC
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MetaRepresentationAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.title_locale);
		}
	}
}
