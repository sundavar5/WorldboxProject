using System;

// Token: 0x02000703 RID: 1795
public class KingdomsNoItems : MetaListNoItems
{
	// Token: 0x06003962 RID: 14690 RVA: 0x00198C20 File Offset: 0x00196E20
	protected override bool hasMetas()
	{
		return base.meta_object.hasKingdoms();
	}
}
