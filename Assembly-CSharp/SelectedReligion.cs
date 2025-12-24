using System;
using UnityEngine;

// Token: 0x02000755 RID: 1877
public class SelectedReligion : SelectedMeta<Religion, ReligionData>
{
	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06003B5F RID: 15199 RVA: 0x001A0A86 File Offset: 0x0019EC86
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x06003B60 RID: 15200 RVA: 0x001A0A89 File Offset: 0x0019EC89
	protected override string getPowerTabAssetID()
	{
		return "selected_religion";
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x001A0A90 File Offset: 0x0019EC90
	protected override void showStatsGeneral(Religion pReligion)
	{
		base.showStatsGeneral(pReligion);
		base.setIconValue("i_kingdoms", (float)pReligion.countKingdoms(), null, "", false, "", '/');
		base.setIconValue("i_cities", (float)pReligion.countCities(), null, "", false, "", '/');
		base.setIconValue("i_books", (float)pReligion.books.count(), null, "", false, "", '/');
	}

	// Token: 0x06003B62 RID: 15202 RVA: 0x001A0B21 File Offset: 0x0019ED21
	protected override void updateElementsOnChange(Religion pNano)
	{
		base.updateElementsOnChange(pNano);
		this._banners_cities_kingdoms.update(pNano);
	}

	// Token: 0x06003B63 RID: 15203 RVA: 0x001A0B36 File Offset: 0x0019ED36
	protected override void checkAchievements(Religion pNano)
	{
		AchievementLibrary.not_just_a_cult.checkBySignal(pNano);
	}

	// Token: 0x04002BA7 RID: 11175
	[SerializeField]
	private CitiesKingdomsContainersController _banners_cities_kingdoms;
}
