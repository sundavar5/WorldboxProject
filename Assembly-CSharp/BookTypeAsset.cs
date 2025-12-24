using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[Serializable]
public class BookTypeAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x060005C1 RID: 1473 RVA: 0x00054982 File Offset: 0x00052B82
	public string getNewIconPath()
	{
		if (this._cached_icons == null)
		{
			this._cached_icons = SpriteTextureLoader.getSpriteList(this.getFullIconPath(), false);
		}
		return this._cached_icons.GetRandom<Sprite>().name;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000549AE File Offset: 0x00052BAE
	public string getFullIconPath()
	{
		return "books/book_icons/" + this.path_icons;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000549C0 File Offset: 0x00052BC0
	public string getTypeID()
	{
		return "book_type_" + this.id;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x000549D2 File Offset: 0x00052BD2
	public string getLocaleID()
	{
		return this.getTypeID();
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x000549DA File Offset: 0x00052BDA
	public string getDescriptionID()
	{
		return "book_type_info_" + this.id;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x000549EC File Offset: 0x00052BEC
	public string getDescriptionTranslated()
	{
		return LocalizedTextManager.getText(this.getDescriptionID(), null, false);
	}

	// Token: 0x040005D7 RID: 1495
	public int writing_rate = 1;

	// Token: 0x040005D8 RID: 1496
	public string name_template;

	// Token: 0x040005D9 RID: 1497
	public string path_icons;

	// Token: 0x040005DA RID: 1498
	public string color_text;

	// Token: 0x040005DB RID: 1499
	public BookReadAction read_action;

	// Token: 0x040005DC RID: 1500
	public bool save_culture = true;

	// Token: 0x040005DD RID: 1501
	public bool save_religion = true;

	// Token: 0x040005DE RID: 1502
	public BookRateCalc rate_calc;

	// Token: 0x040005DF RID: 1503
	public BookRequirementCheck requirement_check;

	// Token: 0x040005E0 RID: 1504
	private Sprite[] _cached_icons;

	// Token: 0x040005E1 RID: 1505
	public BaseStats base_stats = new BaseStats();
}
