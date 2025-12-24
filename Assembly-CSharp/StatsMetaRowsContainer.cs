using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000760 RID: 1888
public class StatsMetaRowsContainer : StatsRowsContainer
{
	// Token: 0x06003BC7 RID: 15303 RVA: 0x001A181C File Offset: 0x0019FA1C
	protected override void showStats()
	{
		this.stats_window.showMetaRows();
		bool tAny = this.stats_rows.Count > 0;
		this._layout_element.ignoreLayout = !tAny;
		this._background.enabled = tAny;
		this._title.gameObject.SetActive(tAny);
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x001A1870 File Offset: 0x0019FA70
	protected void showStatRowMeta(string pId, object pValue, string pColor, MetaType pMetaType, long pMetaId, bool pColorText = false, string pIconPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null, bool pLocalize = true)
	{
		base.showStatRow(pId, pValue, pColor, pMetaType, pMetaId, true, pIconPath, pTooltipId, pTooltipData, pLocalize);
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x001A1894 File Offset: 0x0019FA94
	private void showStatsMetaCity(City pCity, string pTitle)
	{
		ColorAsset color = pCity.kingdom.getColor();
		string tColorHex = (color != null) ? color.color_text : null;
		string tString = pCity.name;
		tString += Toolbox.coloredGreyPart(pCity.getPopulationPeople(), tColorHex, false);
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.City, pCity.getID(), false, "iconCity", null, null, true);
	}

	// Token: 0x06003BCA RID: 15306 RVA: 0x001A18F8 File Offset: 0x0019FAF8
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

	// Token: 0x06003BCB RID: 15307 RVA: 0x001A1968 File Offset: 0x0019FB68
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

	// Token: 0x06003BCC RID: 15308 RVA: 0x001A19D4 File Offset: 0x0019FBD4
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

	// Token: 0x06003BCD RID: 15309 RVA: 0x001A1A48 File Offset: 0x0019FC48
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

	// Token: 0x06003BCE RID: 15310 RVA: 0x001A1ABC File Offset: 0x0019FCBC
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

	// Token: 0x06003BCF RID: 15311 RVA: 0x001A1B30 File Offset: 0x0019FD30
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

	// Token: 0x06003BD0 RID: 15312 RVA: 0x001A1BA4 File Offset: 0x0019FDA4
	private void showStatsMetaArmy(Army pArmy, string pTitle = "army")
	{
		string tString = "???";
		string tColorHex = (pArmy != null) ? pArmy.getColor().color_text : null;
		if (pArmy != null)
		{
			tString = pArmy.data.name;
			tString += Toolbox.coloredGreyPart(pArmy.units.Count, tColorHex, false);
		}
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Army, (pArmy != null) ? pArmy.getID() : -1L, false, "iconArmy", null, null, true);
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x001A1C18 File Offset: 0x0019FE18
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

	// Token: 0x06003BD2 RID: 15314 RVA: 0x001A1C8C File Offset: 0x0019FE8C
	private void showStatsMetaFamily(Family pFamily, string pTitle = "family")
	{
		string tColorHex = pFamily.getColor().color_text;
		string tString = pFamily.name;
		tString += Toolbox.coloredGreyPart(pFamily.units.Count, tColorHex, false);
		this.showStatRowMeta(pTitle, tString, tColorHex, MetaType.Family, pFamily.getID(), false, "iconFamily", null, null, true);
	}

	// Token: 0x06003BD3 RID: 15315 RVA: 0x001A1CE4 File Offset: 0x0019FEE4
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

	// Token: 0x06003BD4 RID: 15316 RVA: 0x001A1D54 File Offset: 0x0019FF54
	public void tryToShowMetaFamily(string pTitle = "family", long pID = -1L, string pName = null, Family pObject = null)
	{
		Family tObject = (pObject != null) ? pObject : World.world.families.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaFamily(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconFamily");
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x001A1D98 File Offset: 0x0019FF98
	public void tryToShowMetaCulture(string pTitle = "culture", long pID = -1L, string pName = null, Culture pObject = null)
	{
		Culture tObject = (pObject != null) ? pObject : World.world.cultures.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaCulture(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconCulture");
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x001A1DDC File Offset: 0x0019FFDC
	public void tryToShowMetaLanguage(string pTitle = "language", long pID = -1L, string pName = null, Language pObject = null)
	{
		Language tObject = (pObject != null) ? pObject : World.world.languages.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaLanguage(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconLanguage");
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x001A1E20 File Offset: 0x001A0020
	public void tryToShowMetaReligion(string pTitle = "religion", long pID = -1L, string pName = null, Religion pObject = null)
	{
		Religion tObject = (pObject != null) ? pObject : World.world.religions.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaReligion(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconReligion");
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x001A1E64 File Offset: 0x001A0064
	public void tryToShowMetaAlliance(string pTitle = "alliance", long pID = -1L, string pName = null, Alliance pObject = null)
	{
		Alliance tObject = (pObject != null) ? pObject : World.world.alliances.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaAlliance(tObject, pTitle);
			return;
		}
		this.showEmptyStatRow(pTitle, pName, "iconAlliance");
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x001A1EA8 File Offset: 0x001A00A8
	public void tryToShowMetaCity(string pTitle, long pID = -1L, string pName = null, City pObject = null, string pIconPath = "iconCity")
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

	// Token: 0x06003BDA RID: 15322 RVA: 0x001A1F08 File Offset: 0x001A0108
	public void tryToShowMetaArmy(string pTitle = "army", long pID = -1L, string pName = null, Army pObject = null)
	{
		Army tObject = (pObject != null) ? pObject : World.world.armies.get(pID);
		if (!tObject.isRekt())
		{
			this.showStatsMetaArmy(tObject, pTitle);
			return;
		}
		string tName = (pID != -1L && !string.IsNullOrEmpty(pName)) ? ("† " + pName) : pName;
		this.showEmptyStatRow(pTitle, tName, "iconArmy");
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x001A1F68 File Offset: 0x001A0168
	public void tryToShowMetaSubspecies(string pTitle = "main_subspecies", long pID = -1L, string pName = null, Subspecies pObject = null)
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

	// Token: 0x06003BDC RID: 15324 RVA: 0x001A1FC8 File Offset: 0x001A01C8
	public void tryToShowMetaKingdom(string pTitle = "kingdom", long pID = -1L, string pName = null, Kingdom pObject = null)
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

	// Token: 0x06003BDD RID: 15325 RVA: 0x001A203C File Offset: 0x001A023C
	public void tryToShowMetaClan(string pTitle = "clan", long pID = -1L, string pName = null, Clan pObject = null)
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

	// Token: 0x06003BDE RID: 15326 RVA: 0x001A209C File Offset: 0x001A029C
	public void tryToShowActor(string pTitle, long pID = -1L, string pName = null, Actor pObject = null, string pIconPath = null)
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

	// Token: 0x06003BDF RID: 15327 RVA: 0x001A20FB File Offset: 0x001A02FB
	public void tryToShowMetaSpecies(string pTitle, string pId)
	{
		StatsMetaRowsContainer.tryToShowMetaSpecies(pTitle, pId, this);
	}

	// Token: 0x06003BE0 RID: 15328 RVA: 0x001A2108 File Offset: 0x001A0308
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
		StatsMetaRowsContainer.showEmptyStatRow(pTitle, null, null, null);
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x001A2168 File Offset: 0x001A0368
	private void showEmptyStatRow(string pTitle, string pContent = null, string pIconPath = null)
	{
		StatsMetaRowsContainer.showEmptyStatRow(pTitle, this, pContent, pIconPath);
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x001A2174 File Offset: 0x001A0374
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

	// Token: 0x04002BC9 RID: 11209
	private const string EMPTY_META_OBJECT_STRING = "???";

	// Token: 0x04002BCA RID: 11210
	[SerializeField]
	private LocalizedText _title;

	// Token: 0x04002BCB RID: 11211
	[SerializeField]
	private Image _background;

	// Token: 0x04002BCC RID: 11212
	[SerializeField]
	private LayoutElement _layout_element;
}
