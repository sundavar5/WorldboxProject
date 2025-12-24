using System;
using System.Collections.Generic;

// Token: 0x020000AF RID: 175
public class SubspeciesBannerLibrary : GenericBannerLibrary
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x00053D88 File Offset: 0x00051F88
	public override void init()
	{
		base.init();
		this.main = this.add(new BannerAsset
		{
			id = "main",
			backgrounds = new List<string>
			{
				"subspecies/background_00",
				"subspecies/background_01",
				"subspecies/background_02",
				"subspecies/background_03",
				"subspecies/background_04",
				"subspecies/background_05",
				"subspecies/background_06",
				"subspecies/background_07",
				"subspecies/background_08",
				"subspecies/background_09",
				"subspecies/background_10",
				"subspecies/background_11"
			}
		});
	}
}
