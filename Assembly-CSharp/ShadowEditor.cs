using System;
using UnityEngine;

// Token: 0x02000485 RID: 1157
public class ShadowEditor : MonoBehaviour
{
	// Token: 0x04001DEB RID: 7659
	public static ShadowEditor instance;

	// Token: 0x04001DEC RID: 7660
	public bool isEnabled;

	// Token: 0x04001DED RID: 7661
	public Vector2 shadow_bound = new Vector2(0.5f, 0.14f);

	// Token: 0x04001DEE RID: 7662
	public float shadow_distortion = 0.08f;
}
