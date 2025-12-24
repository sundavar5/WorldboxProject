using System;
using System.Collections.Generic;

// Token: 0x020000A8 RID: 168
public class CultureBannerLibrary : GenericBannerLibrary
{
	// Token: 0x06000580 RID: 1408 RVA: 0x000534A8 File Offset: 0x000516A8
	public override void init()
	{
		base.init();
		this.main = this.add(new BannerAsset
		{
			id = "main",
			icons = new List<string>
			{
				"cultures/culture_element_0",
				"cultures/culture_element_1",
				"cultures/culture_element_2",
				"cultures/culture_element_3",
				"cultures/culture_element_4",
				"cultures/culture_element_5",
				"cultures/culture_element_6",
				"cultures/culture_element_7"
			},
			backgrounds = new List<string>
			{
				"cultures/culture_decor_0",
				"cultures/culture_decor_1",
				"cultures/culture_decor_2",
				"cultures/culture_decor_3",
				"cultures/culture_decor_4",
				"cultures/culture_decor_5",
				"cultures/culture_decor_6",
				"cultures/culture_decor_7",
				"cultures/culture_decor_8"
			}
		});
	}
}
