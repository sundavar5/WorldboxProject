using System;
using UnityEngine;

// Token: 0x020005E7 RID: 1511
public class LoadingScreenSheepAnimation : MonoBehaviour
{
	// Token: 0x0600318F RID: 12687 RVA: 0x0017AE9E File Offset: 0x0017909E
	private void Update()
	{
		LoadingScreenSheepAnimation.angle += Time.deltaTime * 20f;
		base.transform.localEulerAngles = new Vector3(0f, 0f, LoadingScreenSheepAnimation.angle);
	}

	// Token: 0x0400256A RID: 9578
	internal static float angle;
}
