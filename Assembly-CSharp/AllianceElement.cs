using System;

// Token: 0x02000625 RID: 1573
public class AllianceElement : WindowMetaElement<Alliance, AllianceData>
{
	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06003371 RID: 13169 RVA: 0x001832C0 File Offset: 0x001814C0
	protected Alliance alliance
	{
		get
		{
			return this.meta_object;
		}
	}
}
