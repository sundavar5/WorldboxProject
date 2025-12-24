using System;
using System.Collections.Generic;

// Token: 0x02000701 RID: 1793
public class KingdomsBannersContainer : BannersMetaContainer<KingdomBanner, Kingdom, KingdomData>
{
	// Token: 0x0600395D RID: 14685 RVA: 0x00198BED File Offset: 0x00196DED
	protected override IEnumerable<Kingdom> getMetaList(IMetaObject pMeta)
	{
		return pMeta.getKingdoms();
	}
}
