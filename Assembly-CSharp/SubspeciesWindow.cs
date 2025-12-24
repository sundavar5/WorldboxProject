using System;

// Token: 0x02000772 RID: 1906
public class SubspeciesWindow : WindowMetaGeneric<Subspecies, SubspeciesData>, ITraitWindow<SubspeciesTrait, SubspeciesTraitButton>, IAugmentationsWindow<ITraitsEditor<SubspeciesTrait>>
{
	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06003C5F RID: 15455 RVA: 0x001A3592 File Offset: 0x001A1792
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06003C60 RID: 15456 RVA: 0x001A3595 File Offset: 0x001A1795
	protected override Subspecies meta_object
	{
		get
		{
			return SelectedMetas.selected_subspecies;
		}
	}

	// Token: 0x06003C61 RID: 15457 RVA: 0x001A359C File Offset: 0x001A179C
	public override void startShowingWindow()
	{
		base.startShowingWindow();
		ActorAsset tAsset = this.meta_object.getActorAsset();
		if (!tAsset.isAvailable())
		{
			tAsset.unlock(true);
		}
		AchievementLibrary.checkSubspeciesAchievements(this.meta_object);
	}

	// Token: 0x06003C62 RID: 15458 RVA: 0x001A35D8 File Offset: 0x001A17D8
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		foreach (Religion tReligion in World.world.religions)
		{
			if (!tReligion.isRekt() && tReligion.data.creator_subspecies_id == this.meta_object.getID())
			{
				tReligion.data.creator_subspecies_name = this.meta_object.data.name;
			}
		}
		foreach (Culture tCulture in World.world.cultures)
		{
			if (!tCulture.isRekt() && tCulture.data.creator_subspecies_id == this.meta_object.getID())
			{
				tCulture.data.creator_subspecies_name = this.meta_object.data.name;
			}
		}
		foreach (Clan tClan in World.world.clans)
		{
			if (!tClan.isRekt() && tClan.data.creator_subspecies_id == this.meta_object.getID())
			{
				tClan.data.creator_subspecies_name = this.meta_object.data.name;
			}
		}
		foreach (Language tLanguage in World.world.languages)
		{
			if (!tLanguage.isRekt() && tLanguage.data.creator_subspecies_id == this.meta_object.getID())
			{
				tLanguage.data.creator_subspecies_name = this.meta_object.data.name;
			}
		}
		foreach (Family tFamily in World.world.families)
		{
			if (!tFamily.isRekt() && tFamily.data.subspecies_id == this.meta_object.getID())
			{
				tFamily.data.subspecies_name = this.meta_object.data.name;
			}
		}
		return true;
	}

	// Token: 0x06003C63 RID: 15459 RVA: 0x001A3854 File Offset: 0x001A1A54
	internal override void showStatsRows()
	{
		base.tryShowPastNames();
		base.showStatRow("created", this.meta_object.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.showStatRow("generation", this.meta_object.getGeneration(), MetaType.None, -1L, "worldrules/icon_grow_trees_fast", null, null);
		base.showStatRow("world_population_percentage", this.meta_object.countPopulationPercentage().ToString() + "%", MetaType.None, -1L, "iconPopulation", null, null);
		if (this.meta_object.hasParentSubspecies())
		{
			Subspecies tSubspecies = World.world.subspecies.get(this.meta_object.data.parent_subspecies);
			if (tSubspecies == null)
			{
				base.showStatRow("subspecies_ancestor", LocalizedTextManager.getText("subspecies_extinct", null, false), ColorStyleLibrary.m.color_dead_text, MetaType.None, -1L, true, null, null, null, true);
			}
			else
			{
				base.tryToShowMetaSubspecies("subspecies_ancestor", -1L, null, tSubspecies);
			}
		}
		Subspecies tEvolvedSubspecies = World.world.subspecies.get(this.meta_object.data.evolved_into_subspecies);
		if (tEvolvedSubspecies != null)
		{
			base.tryToShowMetaSubspecies("evolution", -1L, null, tEvolvedSubspecies);
		}
	}

	// Token: 0x06003C64 RID: 15460 RVA: 0x001A397E File Offset: 0x001A1B7E
	public void debugClearExpLevel()
	{
		this.meta_object.debugClear();
		this.OnEnable();
	}

	// Token: 0x06003C66 RID: 15462 RVA: 0x001A3999 File Offset: 0x001A1B99
	T IAugmentationsWindow<ITraitsEditor<SubspeciesTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x04002BE0 RID: 11232
	public StatBar experienceBar;
}
