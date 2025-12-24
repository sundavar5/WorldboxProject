using System;

// Token: 0x020007DD RID: 2013
public class MetaNeedsAndDialogueElement : MetaNeedsElementBase
{
	// Token: 0x06003F68 RID: 16232 RVA: 0x001B546A File Offset: 0x001B366A
	protected override string getText(IMetaObject pMeta, out Actor pActorResult)
	{
		pActorResult = null;
		if (pMeta.countUnits() < 5)
		{
			return string.Empty;
		}
		return MetaTextReportHelper.getText(pMeta, pMeta.getMetaTypeAsset(), out pActorResult);
	}
}
