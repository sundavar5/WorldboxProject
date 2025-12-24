using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000710 RID: 1808
public class LanguageStatsElement : LanguageElement, IStatsElement, IRefreshElement
{
	// Token: 0x060039C8 RID: 14792 RVA: 0x0019B0CF File Offset: 0x001992CF
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x0019B0E7 File Offset: 0x001992E7
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x0019B100 File Offset: 0x00199300
	protected override IEnumerator showContent()
	{
		if (base.language == null)
		{
			yield break;
		}
		if (!base.language.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Language, LanguageData>(base.language);
		this.setIconValue("i_books", (float)base.language.books.count(), null, "", false, "", '/');
		this.setIconValue("i_kingdoms", (float)base.language.countKingdoms(), null, "", false, "", '/');
		this.setIconValue("i_cities", (float)base.language.countCities(), null, "", false, "", '/');
		this.setIconValue("i_books_written", (float)base.language.data.books_written, null, "", false, "", '/');
		yield break;
	}

	// Token: 0x060039CC RID: 14796 RVA: 0x0019B117 File Offset: 0x00199317
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002ACE RID: 10958
	private StatsIconContainer _stats_icons;
}
