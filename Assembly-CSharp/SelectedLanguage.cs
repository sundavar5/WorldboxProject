using System;
using UnityEngine;

// Token: 0x02000753 RID: 1875
public class SelectedLanguage : SelectedMeta<Language, LanguageData>
{
	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06003B50 RID: 15184 RVA: 0x001A0576 File Offset: 0x0019E776
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x06003B51 RID: 15185 RVA: 0x001A0579 File Offset: 0x0019E779
	protected override string getPowerTabAssetID()
	{
		return "selected_language";
	}

	// Token: 0x06003B52 RID: 15186 RVA: 0x001A0580 File Offset: 0x0019E780
	protected override void showStatsGeneral(Language pLanguage)
	{
		base.showStatsGeneral(pLanguage);
		base.setIconValue("i_books", (float)pLanguage.books.count(), null, "", false, "", '/');
		base.setIconValue("i_kingdoms", (float)pLanguage.countKingdoms(), null, "", false, "", '/');
		base.setIconValue("i_cities", (float)pLanguage.countCities(), null, "", false, "", '/');
		base.setIconValue("i_books_written", (float)pLanguage.data.books_written, null, "", false, "", '/');
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x001A063E File Offset: 0x0019E83E
	protected override void updateElementsOnChange(Language pNano)
	{
		base.updateElementsOnChange(pNano);
		this._banners_cities_kingdoms.update(pNano);
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x001A0653 File Offset: 0x0019E853
	protected override void checkAchievements(Language pNano)
	{
		AchievementLibrary.multiply_spoken.checkBySignal(pNano);
	}

	// Token: 0x04002B9A RID: 11162
	[SerializeField]
	private CitiesKingdomsContainersController _banners_cities_kingdoms;
}
