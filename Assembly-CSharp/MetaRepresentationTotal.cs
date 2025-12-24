using System;
using System.Collections.Generic;

// Token: 0x020002A2 RID: 674
public class MetaRepresentationTotal : MetaRepresentationContainerBase
{
	// Token: 0x06001964 RID: 6500 RVA: 0x000F05E4 File Offset: 0x000EE7E4
	protected override void fillDict(ref int pTotal, ref bool pAny, Dictionary<IMetaObject, int> pDict)
	{
		List<Actor> tUnits = this.asset.world_units_getter();
		pTotal = tUnits.Count;
		foreach (Actor tActor in tUnits)
		{
			if (!this.asset.check_has_meta(tActor))
			{
				if (!this.asset.show_none_percent_for_total)
				{
					pTotal--;
				}
			}
			else
			{
				pAny = true;
				using (ListPool<IMetaObject> tMetas = this.asset.meta_getter_total(tActor))
				{
					foreach (IMetaObject ptr in tMetas)
					{
						IMetaObject tMeta = ptr;
						if (!pDict.ContainsKey(tMeta))
						{
							pDict.Add(tMeta, 0);
						}
						IMetaObject key = tMeta;
						int num = pDict[key];
						pDict[key] = num + 1;
					}
				}
			}
		}
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000F0708 File Offset: 0x000EE908
	protected override void checkShowNone(bool pAny, int pNone, int pTotal)
	{
		if (this.asset.show_none_percent_for_total && pAny && pNone > 0)
		{
			string tLocalizedTitle = "statistics_breakdown_none_list".Localize();
			tLocalizedTitle += Toolbox.coloredGreyPart(pNone, ColorStyleLibrary.m.color_text_grey, false);
			string tNoneRow = base.amountWithPercent(pNone, pTotal);
			KeyValueField tField = base.showStatRow(tLocalizedTitle, tNoneRow, ColorStyleLibrary.m.color_text_grey, MetaType.None, -1L, true, this.asset.general_icon_path, null, null, false);
			base.showBar(tField, pNone, pTotal, ColorStyleLibrary.m.color_text_grey);
		}
	}
}
