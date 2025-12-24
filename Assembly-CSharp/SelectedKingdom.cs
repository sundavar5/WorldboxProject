using System;
using UnityEngine;

// Token: 0x02000752 RID: 1874
public class SelectedKingdom : SelectedMetaWithUnit<Kingdom, KingdomData>
{
	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06003B47 RID: 15175 RVA: 0x001A03B4 File Offset: 0x0019E5B4
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06003B48 RID: 15176 RVA: 0x001A03B7 File Offset: 0x0019E5B7
	public override string unit_title_locale_key
	{
		get
		{
			return "titled_king";
		}
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x001A03BE File Offset: 0x0019E5BE
	public override bool hasUnit()
	{
		return !this.nano_object.king.isRekt();
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x001A03D3 File Offset: 0x0019E5D3
	public override Actor getUnit()
	{
		return this.nano_object.king;
	}

	// Token: 0x06003B4B RID: 15179 RVA: 0x001A03E0 File Offset: 0x0019E5E0
	protected override string getPowerTabAssetID()
	{
		return "selected_kingdom";
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x001A03E7 File Offset: 0x0019E5E7
	protected override void updateElementsOnChange(Kingdom pNano)
	{
		base.updateElementsOnChange(pNano);
		this._allies_container.update(pNano);
		this._wars_container.update(pNano);
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x001A0408 File Offset: 0x0019E608
	protected override void showStatsGeneral(Kingdom pKingdom)
	{
		base.showStatsGeneral(pKingdom);
		if (pKingdom.countCities() > pKingdom.getMaxCities())
		{
			base.setIconValue("i_cities", (float)pKingdom.countCities(), new float?((float)pKingdom.getMaxCities()), "#FB2C21", false, "", '/');
		}
		else
		{
			base.setIconValue("i_cities", (float)pKingdom.countCities(), new float?((float)pKingdom.getMaxCities()), "", false, "", '/');
		}
		base.setIconValue("i_food", (float)pKingdom.countTotalFood(), null, "", false, "", '/');
		base.setIconValue("i_money", (float)pKingdom.countTotalMoney(), null, "", false, "", '/');
		base.setIconValue("i_buildings", (float)pKingdom.countBuildings(), null, "", false, "", '/');
		base.setIconValue("i_army", (float)pKingdom.countTotalWarriors(), new float?((float)pKingdom.countWarriorsMax()), "", false, "", '/');
		base.setIconValue("i_territory", (float)pKingdom.countZones(), null, "", false, "", '/');
	}

	// Token: 0x06003B4E RID: 15182 RVA: 0x001A054D File Offset: 0x0019E74D
	public void openVillagesTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Villages");
	}

	// Token: 0x04002B98 RID: 11160
	[SerializeField]
	private KingdomSelectedAlliesContainer _allies_container;

	// Token: 0x04002B99 RID: 11161
	[SerializeField]
	private KingdomSelectedWarsContainer _wars_container;
}
