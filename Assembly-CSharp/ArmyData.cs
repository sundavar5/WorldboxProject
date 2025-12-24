using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000214 RID: 532
public class ArmyData : MetaObjectData
{
	// Token: 0x06001337 RID: 4919 RVA: 0x000D7074 File Offset: 0x000D5274
	public override void Dispose()
	{
		base.Dispose();
		List<LeaderEntry> list = this.past_captains;
		if (list != null)
		{
			list.Clear();
		}
		this.past_captains = null;
	}

	// Token: 0x04001160 RID: 4448
	[DefaultValue(-1L)]
	public long id_city = -1L;

	// Token: 0x04001161 RID: 4449
	[DefaultValue(-1L)]
	public long id_captain = -1L;

	// Token: 0x04001162 RID: 4450
	[DefaultValue(-1L)]
	public long id_kingdom = -1L;

	// Token: 0x04001163 RID: 4451
	public List<LeaderEntry> past_captains;
}
