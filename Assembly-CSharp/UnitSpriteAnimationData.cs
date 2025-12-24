using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[Serializable]
public class UnitSpriteAnimationData
{
	// Token: 0x06000494 RID: 1172 RVA: 0x000324D1 File Offset: 0x000306D1
	public UnitSpriteAnimationData()
	{
		this.head = default(Vector3);
		this.head = default(Vector3);
		this.backpack = default(Vector3);
	}

	// Token: 0x040004E2 RID: 1250
	public string name;

	// Token: 0x040004E3 RID: 1251
	public Vector3 head;

	// Token: 0x040004E4 RID: 1252
	public Vector3 item;

	// Token: 0x040004E5 RID: 1253
	public Vector3 backpack;

	// Token: 0x040004E6 RID: 1254
	public bool showHead;

	// Token: 0x040004E7 RID: 1255
	public bool showItem;
}
