using System;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class SelectedClan : SelectedMetaWithUnit<Clan, ClanData>
{
	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06003B20 RID: 15136 RVA: 0x001A000C File Offset: 0x0019E20C
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06003B21 RID: 15137 RVA: 0x001A000F File Offset: 0x0019E20F
	public override string unit_title_locale_key
	{
		get
		{
			return "titled_chief";
		}
	}

	// Token: 0x06003B22 RID: 15138 RVA: 0x001A0016 File Offset: 0x0019E216
	public override bool hasUnit()
	{
		return !this.nano_object.getChief().isRekt();
	}

	// Token: 0x06003B23 RID: 15139 RVA: 0x001A002B File Offset: 0x0019E22B
	public override Actor getUnit()
	{
		return this.nano_object.getChief();
	}

	// Token: 0x06003B24 RID: 15140 RVA: 0x001A0038 File Offset: 0x0019E238
	protected override string getPowerTabAssetID()
	{
		return "selected_clan";
	}

	// Token: 0x06003B25 RID: 15141 RVA: 0x001A0040 File Offset: 0x0019E240
	protected override void showStatsGeneral(Clan pClan)
	{
		base.showStatsGeneral(pClan);
		base.setIconValue("i_books_written", (float)pClan.data.books_written, null, "", false, "", '/');
	}

	// Token: 0x06003B26 RID: 15142 RVA: 0x001A0081 File Offset: 0x0019E281
	public void openPeopleTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("People");
	}

	// Token: 0x06003B27 RID: 15143 RVA: 0x001A00A2 File Offset: 0x0019E2A2
	protected override void updateElementsOnChange(Clan pNano)
	{
		base.updateElementsOnChange(pNano);
		this._banners_cities_kingdoms.update(pNano);
	}

	// Token: 0x04002B91 RID: 11153
	[SerializeField]
	private CitiesKingdomsContainersController _banners_cities_kingdoms;
}
