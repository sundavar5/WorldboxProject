using System;

// Token: 0x02000665 RID: 1637
public class ClanElement : WindowMetaElement<Clan, ClanData>
{
	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06003508 RID: 13576 RVA: 0x00187BCA File Offset: 0x00185DCA
	protected Clan clan
	{
		get
		{
			return this.meta_object;
		}
	}
}
