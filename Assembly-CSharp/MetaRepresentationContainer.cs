using System;
using System.Collections.Generic;

// Token: 0x020002A9 RID: 681
public class MetaRepresentationContainer : MetaRepresentationContainerBase
{
	// Token: 0x0600197D RID: 6525 RVA: 0x000F07B7 File Offset: 0x000EE9B7
	protected override void init()
	{
		base.init();
		this._meta_window = base.GetComponentInParent<IMetaWindow>();
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000F07CC File Offset: 0x000EE9CC
	protected override void fillDict(ref int pTotal, ref bool pAny, Dictionary<IMetaObject, int> pDict)
	{
		foreach (Actor tActor in this.getMetaObject().getUnits())
		{
			pTotal++;
			if (this.asset.check_has_meta(tActor))
			{
				pAny = true;
				IMetaObject tMeta = this.asset.meta_getter(tActor);
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

	// Token: 0x0600197F RID: 6527 RVA: 0x000F086C File Offset: 0x000EEA6C
	protected override void checkShowNone(bool pAny, int pNone, int pTotal)
	{
		if (pAny && this.asset.show_none_percent && pNone > 0)
		{
			string tNoneRow = base.amountWithPercent(pNone, pTotal);
			KeyValueField tField = base.showStatRow("statistics_breakdown_none", tNoneRow, ColorStyleLibrary.m.color_text_grey, MetaType.None, -1L, true, this.asset.general_icon_path, null, null, true);
			base.showBar(tField, pNone, pTotal, ColorStyleLibrary.m.color_text_grey);
		}
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000F08D2 File Offset: 0x000EEAD2
	private IMetaObject getMetaObject()
	{
		return this._meta_window.getCoreObject() as IMetaObject;
	}

	// Token: 0x04001400 RID: 5120
	private IMetaWindow _meta_window;
}
