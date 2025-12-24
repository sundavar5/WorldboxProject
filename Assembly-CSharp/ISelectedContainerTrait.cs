using System;
using UnityEngine;

// Token: 0x02000748 RID: 1864
public interface ISelectedContainerTrait
{
	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06003B0A RID: 15114
	Transform transform { get; }

	// Token: 0x06003B0B RID: 15115
	void update(NanoObject pNano);
}
