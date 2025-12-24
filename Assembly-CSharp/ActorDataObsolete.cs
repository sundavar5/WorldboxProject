using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Scripting;

// Token: 0x0200020C RID: 524
[Preserve]
public class ActorDataObsolete
{
	// Token: 0x04001143 RID: 4419
	public List<long> saved_items;

	// Token: 0x04001144 RID: 4420
	[DefaultValue(null)]
	public ActorBag inventory;

	// Token: 0x04001145 RID: 4421
	public ActorData status;

	// Token: 0x04001146 RID: 4422
	[DefaultValue(-1L)]
	public long cityID = -1L;

	// Token: 0x04001147 RID: 4423
	public int x;

	// Token: 0x04001148 RID: 4424
	public int y;
}
