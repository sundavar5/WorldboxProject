using System;

// Token: 0x02000061 RID: 97
[Serializable]
public class MonthAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x06000376 RID: 886 RVA: 0x0001FC80 File Offset: 0x0001DE80
	public string getLocaleID()
	{
		return this.id;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001FC88 File Offset: 0x0001DE88
	public string getDescriptionID()
	{
		return "inflected_" + this.id;
	}

	// Token: 0x04000312 RID: 786
	public string english_name;

	// Token: 0x04000313 RID: 787
	public int month_index;
}
