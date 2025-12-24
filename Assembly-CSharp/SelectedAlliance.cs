using System;
using UnityEngine;

// Token: 0x0200074B RID: 1867
public class SelectedAlliance : SelectedMeta<Alliance, AllianceData>
{
	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06003B13 RID: 15123 RVA: 0x0019FDC3 File Offset: 0x0019DFC3
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x0019FDC7 File Offset: 0x0019DFC7
	protected override string getPowerTabAssetID()
	{
		return "selected_alliance";
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x0019FDCE File Offset: 0x0019DFCE
	protected override void updateElementsOnChange(Alliance pNano)
	{
		base.updateElementsOnChange(pNano);
		this._kingdoms_container.update(pNano);
	}

	// Token: 0x06003B16 RID: 15126 RVA: 0x0019FDE4 File Offset: 0x0019DFE4
	protected override void showStatsGeneral(Alliance pAlliance)
	{
		base.showStatsGeneral(pAlliance);
		base.setIconValue("i_army", (float)pAlliance.countWarriors(), null, "", false, "", '/');
		base.setIconValue("i_kingdoms", (float)pAlliance.countKingdoms(), null, "", false, "", '/');
		base.setIconValue("i_zones", (float)pAlliance.countZones(), null, "", false, "", '/');
		base.setIconValue("i_cities", (float)pAlliance.countCities(), null, "", false, "", '/');
		base.setIconValue("i_money", (float)pAlliance.countTotalMoney(), null, "", false, "", '/');
		base.setIconValue("i_buildings", (float)pAlliance.countBuildings(), null, "", false, "", '/');
		base.setIconValue("i_territory", (float)pAlliance.countZones(), null, "", false, "", '/');
	}

	// Token: 0x06003B17 RID: 15127 RVA: 0x0019FF10 File Offset: 0x0019E110
	protected override void setTitleIcons(Alliance pAlliance)
	{
	}

	// Token: 0x04002B90 RID: 11152
	[SerializeField]
	private AllianceSelectedKingdomsContainer _kingdoms_container;
}
