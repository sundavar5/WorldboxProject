using System;
using UnityEngine;

// Token: 0x02000762 RID: 1890
public class StatsWindow : TabbedWindow
{
	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06003BED RID: 15341 RVA: 0x001A238C File Offset: 0x001A058C
	public virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06003BEE RID: 15342 RVA: 0x001A2394 File Offset: 0x001A0594
	protected virtual void OnEnable()
	{
		MetaTypeAsset tMetaTypeAsset = AssetManager.meta_type_library.getAsset(this.meta_type);
		if (!string.IsNullOrEmpty(tMetaTypeAsset.power_tab_id))
		{
			SelectedObjects.setNanoObject(tMetaTypeAsset.get_selected());
			PowerTabController.showWorldTipSelected(tMetaTypeAsset.power_tab_id, false);
			ScrollWindow.skip_worldtip_hide = true;
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06003BEF RID: 15343 RVA: 0x001A23E4 File Offset: 0x001A05E4
	internal StatsRowsContainer stats_rows_container
	{
		get
		{
			if (this._stats_rows_container == null)
			{
				this._stats_rows_container = base.transform.FindRecursive("content_stats").GetComponent<StatsRowsContainer>();
				if (this._stats_rows_container == null)
				{
					Debug.LogError("No StatsRowsContainer 'content_stats' found in " + base.name);
				}
			}
			return this._stats_rows_container;
		}
	}

	// Token: 0x06003BF0 RID: 15344 RVA: 0x001A2443 File Offset: 0x001A0643
	protected override void create()
	{
		base.create();
		this._refreshable_elements = base.GetComponentsInChildren<IRefreshElement>(true);
	}

	// Token: 0x06003BF1 RID: 15345 RVA: 0x001A2458 File Offset: 0x001A0658
	protected void refreshMetaList()
	{
		MetaSwitchManager.refresh();
	}

	// Token: 0x06003BF2 RID: 15346 RVA: 0x001A2460 File Offset: 0x001A0660
	public void updateStats()
	{
		IRefreshElement[] refreshable_elements = this._refreshable_elements;
		for (int i = 0; i < refreshable_elements.Length; i++)
		{
			refreshable_elements[i].refresh();
		}
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x001A248A File Offset: 0x001A068A
	internal KeyValueField getStatRow(string pID)
	{
		return this.stats_rows_container.getStatRow(pID);
	}

	// Token: 0x06003BF4 RID: 15348 RVA: 0x001A2498 File Offset: 0x001A0698
	internal virtual void showStatsRows()
	{
		throw new NotImplementedException("showStatsRows not implemented");
	}

	// Token: 0x06003BF5 RID: 15349 RVA: 0x001A24A4 File Offset: 0x001A06A4
	public virtual void showMetaRows()
	{
	}

	// Token: 0x06003BF6 RID: 15350 RVA: 0x001A24A8 File Offset: 0x001A06A8
	internal KeyValueField showStatRow(string pId, object pValue, MetaType pMetaType = MetaType.None, long pMetaId = -1L, string pIconPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null)
	{
		return this.stats_rows_container.showStatRow(pId, pValue, null, pMetaType, pMetaId, false, pIconPath, pTooltipId, pTooltipData, true);
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x001A24D0 File Offset: 0x001A06D0
	internal KeyValueField showStatRow(string pId, object pValue, string pColor, MetaType pMetaType = MetaType.None, long pMetaId = -1L, bool pColorText = false, string pIconPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null, bool pLocalize = true)
	{
		return this.stats_rows_container.showStatRow(pId, pValue, pColor, pMetaType, pMetaId, pColorText, pIconPath, pTooltipId, pTooltipData, pLocalize);
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x001A24F9 File Offset: 0x001A06F9
	internal void updateStatsRows()
	{
		this.stats_rows_container.gameObject.SetActive(false);
		this.stats_rows_container.gameObject.SetActive(true);
	}

	// Token: 0x06003BF9 RID: 15353 RVA: 0x001A2520 File Offset: 0x001A0720
	protected void showStatRowMeta(string pId, object pValue, string pColor, MetaType pMetaType, long pMetaId, bool pColorText = false, string pIconPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null, bool pLocalize = true)
	{
		this.showStatRow(pId, pValue, pColor, pMetaType, pMetaId, true, pIconPath, pTooltipId, pTooltipData, pLocalize);
	}

	// Token: 0x06003BFA RID: 15354 RVA: 0x001A2544 File Offset: 0x001A0744
	private void showStatsMetaCity(City pCity, string pTitle)
	{
		ColorAsset color = pCity.kingdom.getColor();
		string tColorHex = (color != null) ? color.color_text : null;
		string tString = pCity.name;
		tString += Toolbox.coloredGreyPart(pCity.getPopulationPeople(), tColorHex, false);
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.City, pCity.getID(), false, "iconCity", null, null, true);
	}

	// Token: 0x06003BFB RID: 15355 RVA: 0x001A25A8 File Offset: 0x001A07A8
	private void showStatsMetaKingdom(Kingdom pKingdom, string pTitle = "kingdom")
	{
		string tString = "???";
		string tColorHex = (pKingdom != null) ? pKingdom.getColor().color_text : null;
		if (pKingdom != null)
		{
			tString = pKingdom.data.name;
			tString += Toolbox.coloredGreyPart(pKingdom.getPopulationPeople(), tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Kingdom, (pKingdom != null) ? pKingdom.getID() : -1L, false, "iconKingdom", null, null, true);
	}

	// Token: 0x06003BFC RID: 15356 RVA: 0x001A2618 File Offset: 0x001A0818
	private void showStatsMetaUnit(Actor pActor, string pTitle, string pIconPath = null)
	{
		string tString = "???";
		string tColorHex = (pActor != null) ? pActor.kingdom.getColor().color_text : null;
		if (pActor != null)
		{
			tString = pActor.getName();
			tString += Toolbox.coloredGreyPart(pActor.getAge(), tColorHex, true);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Unit, (pActor != null) ? pActor.getID() : -1L, false, pIconPath, null, null, true);
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x001A2684 File Offset: 0x001A0884
	private void showStatsMetaCulture(Culture pCulture, string pTitle = "culture")
	{
		string tString = "???";
		string tColor = (pCulture != null) ? pCulture.getColor().color_text : null;
		if (pCulture != null)
		{
			tString = pCulture.data.name;
			tString += Toolbox.coloredGreyPart(pCulture.units.Count, tColor, false);
		}
		this.showStatRowMeta(pTitle, tString, tColor, MetaType.Culture, (pCulture != null) ? pCulture.getID() : -1L, false, "iconCulture", null, null, true);
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x001A26F8 File Offset: 0x001A08F8
	private void showStatsMetaLanguage(Language pLanguage, string pTitle = "language")
	{
		string tString = "???";
		string tColorHex = (pLanguage != null) ? pLanguage.getColor().color_text : null;
		if (pLanguage != null)
		{
			tString = pLanguage.data.name;
			tString += Toolbox.coloredGreyPart(pLanguage.units.Count, tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Language, (pLanguage != null) ? pLanguage.getID() : -1L, false, "iconLanguage", null, null, true);
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x001A276C File Offset: 0x001A096C
	private void showStatsMetaReligion(Religion pReligion, string pTitle = "religion")
	{
		string tString = "???";
		string tColorHex = (pReligion != null) ? pReligion.getColor().color_text : null;
		if (pReligion != null)
		{
			tString = pReligion.data.name;
			tString += Toolbox.coloredGreyPart(pReligion.units.Count, tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Religion, (pReligion != null) ? pReligion.getID() : -1L, false, "iconReligion", null, null, true);
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x001A27E0 File Offset: 0x001A09E0
	private void showStatsMetaClan(Clan pClan, string pTitle = "clan")
	{
		string tString = "???";
		string tColorHex = (pClan != null) ? pClan.getColor().color_text : null;
		if (pClan != null)
		{
			tString = pClan.data.name;
			tString += Toolbox.coloredGreyPart(pClan.units.Count, tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Clan, (pClan != null) ? pClan.getID() : -1L, false, "iconClan", null, null, true);
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x001A2854 File Offset: 0x001A0A54
	private void showStatsMetaSubspecies(Subspecies pSubspecies, string pTitle = "subspecies")
	{
		string tString = "???";
		string tColorHex = (pSubspecies != null) ? pSubspecies.getColor().color_text : null;
		if (pSubspecies != null)
		{
			tString = pSubspecies.data.name;
			tString += Toolbox.coloredGreyPart(pSubspecies.units.Count, tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Subspecies, (pSubspecies != null) ? pSubspecies.getID() : -1L, false, "iconSpecies", null, null, true);
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x001A28C8 File Offset: 0x001A0AC8
	private void showStatsMetaFamily(Family pFamily, string pTitle = "family")
	{
		string tColorHex = pFamily.getColor().color_text;
		string tString = pFamily.name;
		tString += Toolbox.coloredGreyPart(pFamily.units.Count, tColorHex, false);
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Family, pFamily.getID(), false, "iconFamily", null, null, true);
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x001A2920 File Offset: 0x001A0B20
	private void showStatsMetaAlliance(Alliance pAlliance, string pTitle = "alliance")
	{
		string tString = "???";
		string tColorHex = (pAlliance != null) ? pAlliance.getColor().color_text : null;
		if (pAlliance != null)
		{
			tString = pAlliance.data.name;
			tString += Toolbox.coloredGreyPart(pAlliance.countPopulation(), tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Alliance, (pAlliance != null) ? pAlliance.getID() : -1L, false, "iconAlliance", null, null, true);
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x001A2990 File Offset: 0x001A0B90
	protected void tryToShowMetaFamily(string pTitle = "family", long pID = -1L, string pName = null, Family pObject = null)
	{
		Family tObject = (pObject != null) ? pObject : World.world.families.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaFamily(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconFamily");
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x001A29D4 File Offset: 0x001A0BD4
	protected void tryToShowMetaCulture(string pTitle = "culture", long pID = -1L, string pName = null, Culture pObject = null)
	{
		Culture tObject = (pObject != null) ? pObject : World.world.cultures.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaCulture(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconCulture");
	}

	// Token: 0x06003C06 RID: 15366 RVA: 0x001A2A18 File Offset: 0x001A0C18
	protected void tryToShowMetaLanguage(string pTitle = "language", long pID = -1L, string pName = null, Language pObject = null)
	{
		Language tObject = (pObject != null) ? pObject : World.world.languages.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaLanguage(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconLanguage");
	}

	// Token: 0x06003C07 RID: 15367 RVA: 0x001A2A5C File Offset: 0x001A0C5C
	protected void tryToShowMetaReligion(string pTitle = "religion", long pID = -1L, string pName = null, Religion pObject = null)
	{
		Religion tObject = (pObject != null) ? pObject : World.world.religions.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaReligion(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconReligion");
	}

	// Token: 0x06003C08 RID: 15368 RVA: 0x001A2AA0 File Offset: 0x001A0CA0
	protected void tryToShowMetaAlliance(string pTitle = "alliance", long pID = -1L, string pName = null, Alliance pObject = null)
	{
		Alliance tObject = (pObject != null) ? pObject : World.world.alliances.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaAlliance(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconAlliance");
	}

	// Token: 0x06003C09 RID: 15369 RVA: 0x001A2AE4 File Offset: 0x001A0CE4
	protected void tryToShowMetaCity(string pTitle, long pID = -1L, string pName = null, City pObject = null, string pIconPath = "iconCity")
	{
		City tObject = (pObject != null) ? pObject : World.world.cities.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaCity(tObject, pTitle);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, pIconPath);
	}

	// Token: 0x06003C0A RID: 15370 RVA: 0x001A2B44 File Offset: 0x001A0D44
	protected void tryToShowMetaSubspecies(string pTitle = "main_subspecies", long pID = -1L, string pName = null, Subspecies pObject = null)
	{
		Subspecies tObject = (pObject != null) ? pObject : World.world.subspecies.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaSubspecies(tObject, pTitle);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, "iconSpecies");
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x001A2BA4 File Offset: 0x001A0DA4
	protected void tryToShowMetaKingdom(string pTitle = "kingdom", long pID = -1L, string pName = null, Kingdom pObject = null)
	{
		Kingdom tObject = (pObject != null) ? pObject : (World.world.kingdoms.get(pID) ?? World.world.kingdoms.db_get(pID));
		if (!tObject.isRekt())
		{
			this.showStatsMetaKingdom(tObject, pTitle);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, "iconKingdom");
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x001A2C18 File Offset: 0x001A0E18
	protected void tryToShowMetaClan(string pTitle = "clan", long pID = -1L, string pName = null, Clan pObject = null)
	{
		Clan tObject = (pObject != null) ? pObject : World.world.clans.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaClan(tObject, pTitle);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, "iconClan");
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x001A2C78 File Offset: 0x001A0E78
	protected void tryToShowActor(string pTitle, long pID = -1L, string pName = null, Actor pObject = null, string pIconPath = null)
	{
		Actor tObject = (pObject != null) ? pObject : World.world.units.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaUnit(tObject, pTitle, pIconPath);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, pIconPath);
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x001A2CD7 File Offset: 0x001A0ED7
	private void showEmptyStatRow(string pTitle, string pContent = null, string pIconPath = null)
	{
		StatsWindow.showEmptyStatRow(pTitle, this.stats_rows_container, pContent, pIconPath);
	}

	// Token: 0x06003C0F RID: 15375 RVA: 0x001A2CE8 File Offset: 0x001A0EE8
	private static void showEmptyStatRow(string pTitle, StatsRowsContainer pContainer, string pContent = null, string pIconPath = null)
	{
		if (string.IsNullOrEmpty(pContent))
		{
			pContent = "???";
		}
		if (pContent == "neutral")
		{
			pContent = "???";
		}
		pContainer.showStatRow(pTitle, pContent, ColorStyleLibrary.m.color_dead_text, MetaType.None, -1L, false, pIconPath, null, null, true);
	}

	// Token: 0x06003C10 RID: 15376 RVA: 0x001A2D33 File Offset: 0x001A0F33
	protected void tryToShowMetaSpecies(string pTitle, string pId)
	{
		StatsWindow.tryToShowMetaSpecies(pTitle, pId, this.stats_rows_container);
	}

	// Token: 0x06003C11 RID: 15377 RVA: 0x001A2D44 File Offset: 0x001A0F44
	public static void tryToShowMetaSpecies(string pTitle, string pId, StatsRowsContainer pContainer)
	{
		if (!string.IsNullOrEmpty(pId))
		{
			ActorAsset tAsset = AssetManager.actor_library.get(pId);
			if (tAsset != null)
			{
				string tCreatorSpeciesTranslated = tAsset.getTranslatedName();
				pContainer.showStatRow(pTitle, tCreatorSpeciesTranslated, null, MetaType.None, -1L, false, "iconGene", "unit_species", new TooltipDataGetter(tAsset.getTooltip), true);
				return;
			}
		}
		StatsWindow.showEmptyStatRow(pTitle, pContainer, null, null);
	}

	// Token: 0x04002BD0 RID: 11216
	[SerializeField]
	protected StatsMetaRowsContainer meta_rows_container;

	// Token: 0x04002BD1 RID: 11217
	private StatsRowsContainer _stats_rows_container;

	// Token: 0x04002BD2 RID: 11218
	private IRefreshElement[] _refreshable_elements;

	// Token: 0x04002BD3 RID: 11219
	private const string EMPTY_META_OBJECT_STRING = "???";
}
