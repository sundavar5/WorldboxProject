using System;
using System.Collections.Generic;

// Token: 0x02000702 RID: 1794
public class KingdomsMetaContainer : ListMetaContainer<KingdomListElement, Kingdom, KingdomData>
{
	// Token: 0x0600395F RID: 14687 RVA: 0x00198BFD File Offset: 0x00196DFD
	protected override IEnumerable<Kingdom> getMetaList()
	{
		return base.getMeta().getKingdoms();
	}

	// Token: 0x06003960 RID: 14688 RVA: 0x00198C0A File Offset: 0x00196E0A
	protected override Comparison<Kingdom> getSorting()
	{
		return new Comparison<Kingdom>(ComponentListBase<KingdomListElement, Kingdom, KingdomData, KingdomListComponent>.sortByPopulation);
	}
}
