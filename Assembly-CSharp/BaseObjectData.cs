using System;
using System.ComponentModel;

// Token: 0x0200021A RID: 538
[Serializable]
public abstract class BaseObjectData : BaseSystemData
{
	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x0600134D RID: 4941 RVA: 0x000D8388 File Offset: 0x000D6588
	// (set) Token: 0x0600134E RID: 4942 RVA: 0x000D8390 File Offset: 0x000D6590
	[DefaultValue(100)]
	public int health { get; set; } = 100;
}
