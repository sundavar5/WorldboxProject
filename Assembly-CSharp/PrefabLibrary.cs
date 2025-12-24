using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E1 RID: 481
public class PrefabLibrary : MonoBehaviour
{
	// Token: 0x06000DEE RID: 3566 RVA: 0x000BEC8C File Offset: 0x000BCE8C
	private void Awake()
	{
		PrefabLibrary.instance = this;
	}

	// Token: 0x04000E50 RID: 3664
	internal static PrefabLibrary instance;

	// Token: 0x04000E51 RID: 3665
	public GameObject graphy;

	// Token: 0x04000E52 RID: 3666
	public DebugTool debugTool;

	// Token: 0x04000E53 RID: 3667
	public DragonAsset dragonAsset;

	// Token: 0x04000E54 RID: 3668
	public DragonAsset zombieDragonAsset;

	// Token: 0x04000E55 RID: 3669
	public Image iconLock;
}
