using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[Serializable]
public class ColorStyleAsset : Asset
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x0005E7B5 File Offset: 0x0005C9B5
	public string color_dead_text
	{
		get
		{
			return this.color_text_grey_dark;
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0005E7BD File Offset: 0x0005C9BD
	public Color getSelectorColor()
	{
		return Toolbox.makeColor(this.color_text_selector);
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0005E7CA File Offset: 0x0005C9CA
	public Color getSelectorRemoveColor()
	{
		return Toolbox.makeColor(this.color_text_selector_remove);
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0005E7D8 File Offset: 0x0005C9D8
	public string getColorForTaxonomy(string pID)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(pID);
		if (num <= 2219494011U)
		{
			if (num <= 1671819501U)
			{
				if (num != 370076495U)
				{
					if (num == 1671819501U)
					{
						if (pID == "taxonomy_class")
						{
							return this.taxonomy_class;
						}
					}
				}
				else if (pID == "taxonomy_genus")
				{
					return this.taxonomy_genus;
				}
			}
			else if (num != 2178547709U)
			{
				if (num == 2219494011U)
				{
					if (pID == "taxonomy_family")
					{
						return this.taxonomy_family;
					}
				}
			}
			else if (pID == "taxonomy_order")
			{
				return this.taxonomy_order;
			}
		}
		else if (num <= 2677668788U)
		{
			if (num != 2250286316U)
			{
				if (num == 2677668788U)
				{
					if (pID == "taxonomy_phylum")
					{
						return this.taxonomy_phylum;
					}
				}
			}
			else if (pID == "taxonomy_common_name")
			{
				return this.taxonomy_common_name;
			}
		}
		else if (num != 3582153604U)
		{
			if (num == 3901740944U)
			{
				if (pID == "taxonomy_subphylum")
				{
					return this.taxonomy_subphylum;
				}
			}
		}
		else if (pID == "taxonomy_kingdom")
		{
			return this.taxonomy_kingdom;
		}
		return "0xFFFFFF";
	}

	// Token: 0x040006F5 RID: 1781
	public string taxonomy_kingdom = "#76FFF8";

	// Token: 0x040006F6 RID: 1782
	public string taxonomy_phylum = "#74FFA3";

	// Token: 0x040006F7 RID: 1783
	public string taxonomy_subphylum = "#54FF8D";

	// Token: 0x040006F8 RID: 1784
	public string taxonomy_class = "#76FF4A";

	// Token: 0x040006F9 RID: 1785
	public string taxonomy_order = "#B9FF48";

	// Token: 0x040006FA RID: 1786
	public string taxonomy_family = "#FEFD46";

	// Token: 0x040006FB RID: 1787
	public string taxonomy_genus = "#F8AB4F";

	// Token: 0x040006FC RID: 1788
	public string taxonomy_common_name = "#DC8D4E";

	// Token: 0x040006FD RID: 1789
	public string color_text_grey = "#ADADAD";

	// Token: 0x040006FE RID: 1790
	public string color_text_grey_dark = "#7D7D7D";

	// Token: 0x040006FF RID: 1791
	public string color_text_selector = "#7FFF75AA";

	// Token: 0x04000700 RID: 1792
	public string color_text_selector_remove = "#FF182AAA";

	// Token: 0x04000701 RID: 1793
	public string color_text_pumpkin = "#FFA94C";

	// Token: 0x04000702 RID: 1794
	public string color_text_pumpkin_light = "#FFBC66";

	// Token: 0x04000703 RID: 1795
	public Color favorite_selected = Color.white;

	// Token: 0x04000704 RID: 1796
	public Color favorite_not_selected = new Color(0.7f, 0.7f, 0.7f, 0.3f);

	// Token: 0x04000705 RID: 1797
	public Color health_bar_main_green = Toolbox.makeColor("#00C21F");

	// Token: 0x04000706 RID: 1798
	public Color health_bar_main_red = Toolbox.makeColor("#FF4300");

	// Token: 0x04000707 RID: 1799
	public Color health_bar_background = Toolbox.makeColor("#303030");
}
