using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public interface IHandRenderer
{
	// Token: 0x0600086F RID: 2159
	Sprite[] getSprites();

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000870 RID: 2160
	bool is_colored { get; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000871 RID: 2161
	bool is_animated { get; }

	// Token: 0x06000872 RID: 2162 RVA: 0x0007549D File Offset: 0x0007369D
	string getID()
	{
		return (this as Asset).id;
	}
}
