using System;

// Token: 0x02000650 RID: 1616
public class CitiesNoItems : MetaListNoItems
{
	// Token: 0x06003476 RID: 13430 RVA: 0x00185D69 File Offset: 0x00183F69
	protected override bool hasMetas()
	{
		return base.meta_object.hasCities();
	}
}
