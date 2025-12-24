using System;
using UnityEngine;

// Token: 0x020007BC RID: 1980
public class SignoutButton : MonoBehaviour
{
	// Token: 0x06003E99 RID: 16025 RVA: 0x001B2ECC File Offset: 0x001B10CC
	public void tryLogOut()
	{
		Auth.signOut();
		ScrollWindow.get("worldnet_logout").clickHide("right");
	}
}
