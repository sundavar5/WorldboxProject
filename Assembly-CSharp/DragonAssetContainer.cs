using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
[Serializable]
public class DragonAssetContainer
{
	// Token: 0x04001877 RID: 6263
	public string name;

	// Token: 0x04001878 RID: 6264
	public DragonState id;

	// Token: 0x04001879 RID: 6265
	public Sprite[] frames;

	// Token: 0x0400187A RID: 6266
	public DragonState[] states;

	// Token: 0x0400187B RID: 6267
	public float speed = 0.1f;
}
