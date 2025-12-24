using System;
using UnityEngine;

// Token: 0x0200074F RID: 1871
public class SelectedCulture : SelectedMeta<Culture, CultureData>
{
	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06003B35 RID: 15157 RVA: 0x001A0238 File Offset: 0x0019E438
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x06003B36 RID: 15158 RVA: 0x001A023B File Offset: 0x0019E43B
	protected override string getPowerTabAssetID()
	{
		return "selected_culture";
	}

	// Token: 0x06003B37 RID: 15159 RVA: 0x001A0244 File Offset: 0x0019E444
	protected override void showStatsGeneral(Culture pCulture)
	{
		base.showStatsGeneral(pCulture);
		base.setIconValue("i_kingdoms", (float)pCulture.countKingdoms(), null, "", false, "", '/');
		base.setIconValue("i_cities", (float)pCulture.countCities(), null, "", false, "", '/');
		base.setIconValue("i_books", (float)pCulture.books.count(), null, "", false, "", '/');
	}

	// Token: 0x06003B38 RID: 15160 RVA: 0x001A02D5 File Offset: 0x0019E4D5
	protected override void updateElementsOnChange(Culture pNano)
	{
		base.updateElementsOnChange(pNano);
		this._onomastics_names.load(pNano);
		this._banners_cities_kingdoms.update(pNano);
	}

	// Token: 0x06003B39 RID: 15161 RVA: 0x001A02F6 File Offset: 0x0019E4F6
	protected override void updateElementsAlways(Culture pNano)
	{
		base.updateElementsAlways(pNano);
		this._onomastics_names.update();
	}

	// Token: 0x06003B3A RID: 15162 RVA: 0x001A030A File Offset: 0x0019E50A
	public void openOnomasticsTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Onomastics");
	}

	// Token: 0x04002B94 RID: 11156
	[SerializeField]
	private CultureSelectedOnomasticsNames _onomastics_names;

	// Token: 0x04002B95 RID: 11157
	[SerializeField]
	private CitiesKingdomsContainersController _banners_cities_kingdoms;
}
