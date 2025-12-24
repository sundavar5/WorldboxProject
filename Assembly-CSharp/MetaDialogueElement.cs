using System;

// Token: 0x020007DB RID: 2011
public class MetaDialogueElement : MetaNeedsElementBase
{
	// Token: 0x06003F61 RID: 16225 RVA: 0x001B53FA File Offset: 0x001B35FA
	protected override string getText(IMetaObject pMeta, out Actor pActorResult)
	{
		pActorResult = null;
		return MetaTextReportHelper.addSingleUnitTextRandomUnit(pMeta, out pActorResult);
	}
}
