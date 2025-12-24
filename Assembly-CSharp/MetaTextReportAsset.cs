using System;
using System.Collections.Generic;

// Token: 0x020002AD RID: 685
[Serializable]
public class MetaTextReportAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x000F1654 File Offset: 0x000EF854
	internal string get_locale_id
	{
		get
		{
			return "meta_report_" + this.id + "_";
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06001995 RID: 6549 RVA: 0x000F166C File Offset: 0x000EF86C
	internal string get_random_text
	{
		get
		{
			int tIndex = Randy.randomInt(0, this.amount);
			return LocalizedTextManager.getText(string.Format("{0}{1}", this.get_locale_id, tIndex), null, false);
		}
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000F16A3 File Offset: 0x000EF8A3
	public IEnumerable<string> getLocaleIDs()
	{
		int num;
		for (int i = 0; i < this.amount; i = num + 1)
		{
			yield return string.Format("{0}{1}", this.get_locale_id, i);
			num = i;
		}
		yield break;
	}

	// Token: 0x04001407 RID: 5127
	public MetaTextReportAction report_action;

	// Token: 0x04001408 RID: 5128
	public string color;

	// Token: 0x04001409 RID: 5129
	public int amount = 5;
}
