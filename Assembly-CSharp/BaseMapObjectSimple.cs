using System;

// Token: 0x020001C4 RID: 452
public class BaseMapObjectSimple
{
	// Token: 0x06000D50 RID: 3408 RVA: 0x000BB952 File Offset: 0x000B9B52
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x000BB954 File Offset: 0x000B9B54
	internal virtual void create()
	{
		this.created = true;
	}

	// Token: 0x04000CA9 RID: 3241
	internal bool created;
}
