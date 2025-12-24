using System;
using System.Collections.Generic;

// Token: 0x02000062 RID: 98
public class MonthLibrary : AssetLibrary<MonthAsset>
{
	// Token: 0x06000379 RID: 889 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
	public override void init()
	{
		base.init();
		this.add(new MonthAsset
		{
			id = "month_1",
			english_name = "january",
			month_index = 1
		});
		this.add(new MonthAsset
		{
			id = "month_2",
			english_name = "february",
			month_index = 2
		});
		this.add(new MonthAsset
		{
			id = "month_3",
			english_name = "march",
			month_index = 3
		});
		this.add(new MonthAsset
		{
			id = "month_4",
			english_name = "april",
			month_index = 4
		});
		this.add(new MonthAsset
		{
			id = "month_5",
			english_name = "may",
			month_index = 5
		});
		this.add(new MonthAsset
		{
			id = "month_6",
			english_name = "june",
			month_index = 6
		});
		this.add(new MonthAsset
		{
			id = "month_7",
			english_name = "july",
			month_index = 7
		});
		this.add(new MonthAsset
		{
			id = "month_8",
			english_name = "august",
			month_index = 8
		});
		this.add(new MonthAsset
		{
			id = "month_9",
			english_name = "september",
			month_index = 9
		});
		this.add(new MonthAsset
		{
			id = "month_10",
			english_name = "october",
			month_index = 10
		});
		this.add(new MonthAsset
		{
			id = "month_11",
			english_name = "november",
			month_index = 11
		});
		this.add(new MonthAsset
		{
			id = "month_12",
			english_name = "december",
			month_index = 12
		});
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
	public override void linkAssets()
	{
		base.linkAssets();
		this._month_indexes = new Dictionary<int, MonthAsset>();
		foreach (MonthAsset tAsset in this.list)
		{
			this._month_indexes.Add(tAsset.month_index, tAsset);
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001FF18 File Offset: 0x0001E118
	public MonthAsset getMonth(int pMonthIndex)
	{
		return this._month_indexes[pMonthIndex];
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001FF28 File Offset: 0x0001E128
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MonthAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x04000314 RID: 788
	private Dictionary<int, MonthAsset> _month_indexes;
}
