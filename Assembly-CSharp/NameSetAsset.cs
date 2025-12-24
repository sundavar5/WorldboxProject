using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x0200013E RID: 318
[Serializable]
public class NameSetAsset : Asset
{
	// Token: 0x060009B0 RID: 2480 RVA: 0x0008D770 File Offset: 0x0008B970
	public string get(MetaType pType)
	{
		switch (pType)
		{
		case MetaType.Family:
			return this.family;
		case MetaType.Language:
			return this.language;
		case MetaType.Culture:
			return this.culture;
		case MetaType.Religion:
			return this.religion;
		case MetaType.Clan:
			return this.clan;
		case MetaType.City:
			return this.city;
		case MetaType.Kingdom:
			return this.kingdom;
		case MetaType.Unit:
			return this.unit;
		}
		return null;
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0008D7EC File Offset: 0x0008B9EC
	public static IEnumerable<MetaType> getTypes()
	{
		yield return MetaType.City;
		yield return MetaType.Clan;
		yield return MetaType.Culture;
		yield return MetaType.Family;
		yield return MetaType.Kingdom;
		yield return MetaType.Language;
		yield return MetaType.Unit;
		yield return MetaType.Religion;
		yield break;
	}

	// Token: 0x040009A3 RID: 2467
	[DefaultValue("")]
	public string city = string.Empty;

	// Token: 0x040009A4 RID: 2468
	[DefaultValue("")]
	public string clan = string.Empty;

	// Token: 0x040009A5 RID: 2469
	[DefaultValue("")]
	public string culture = string.Empty;

	// Token: 0x040009A6 RID: 2470
	[DefaultValue("")]
	public string family = string.Empty;

	// Token: 0x040009A7 RID: 2471
	[DefaultValue("")]
	public string kingdom = string.Empty;

	// Token: 0x040009A8 RID: 2472
	[DefaultValue("")]
	public string language = string.Empty;

	// Token: 0x040009A9 RID: 2473
	[DefaultValue("")]
	public string unit = string.Empty;

	// Token: 0x040009AA RID: 2474
	[DefaultValue("")]
	public string religion = string.Empty;
}
