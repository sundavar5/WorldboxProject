using System;
using System.Collections.Generic;

// Token: 0x020001C0 RID: 448
public class BaseModule : BaseMapObject
{
	// Token: 0x06000CF0 RID: 3312 RVA: 0x000BA2EB File Offset: 0x000B84EB
	internal virtual void clear()
	{
	}

	// Token: 0x04000C91 RID: 3217
	internal HashSet<WorldTile> hashset;

	// Token: 0x04000C92 RID: 3218
	protected float timer;
}
